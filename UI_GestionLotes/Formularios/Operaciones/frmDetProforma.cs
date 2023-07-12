using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_GestionLotes;
using System.Web.UI.WebControls;

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class frmDetProforma : HNG_Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        List<eProformas.eProformas_Detalle> listDetalle = new List<eProformas.eProformas_Detalle>();
        public eProformas.eProformas_Detalle objDetalle = new eProformas.eProformas_Detalle();
        public string cod_proyecto;
        public decimal montoFinal,  prc_descuento_maximo = 0;

        public frmDetProforma()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListarCotizacion, gvListarCotizacion);
            gvListarCotizacion.OptionsBehavior.Editable = true;
            gvListarCotizacion.OptionsView.ShowIndicator = false;
            gvListarCotizacion.OptionsView.ShowAutoFilterRow = false;
        }

        private void frmDetProforma_Load(object sender, EventArgs e)
        {
            listDetalle = unit.Proyectos.obtenerConsultasVariasLotes<eProformas.eProformas_Detalle>(19, montoFinal, cod_proyecto);
            prc_descuento_maximo = listDetalle[1].prc_descuento_maximo * 100;
            bsDetalleProformas.DataSource = null; bsDetalleProformas.DataSource = listDetalle; gvListarCotizacion.RefreshData();

        }

        private void gvListarCotizacion_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                eProformas.eProformas_Detalle obj = gvListarCotizacion.GetFocusedRow() as eProformas.eProformas_Detalle;
                if (obj == null) return;
                if (gvListarCotizacion.FocusedColumn.FieldName == "num_fraccion")
                {
                    e.Cancel = AnalizarNombreCuota(obj.dsc_nombre_detalle, "especial") ? false : true; // 2 es igual a cuota especial
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool AnalizarNombreCuota(string texto, string validar)
        {
            string cadenaSinEspacios = texto.Replace(" ", "");
            string cadenaEnMinusculas = cadenaSinEspacios.ToLower();
            if (cadenaEnMinusculas.Contains(validar))
            {
                return true;
            }
            return false;

        }

        private void gvListarCotizacion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(e.RowHandle) as eProformas.eProformas_Detalle;
                    if (obj.cod_variable == "")
                    {
                        e.Appearance.BackColor = Color.Orange;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListarCotizacion_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvListarCotizacion.RefreshData();
            eProformas.eProformas_Detalle obj = gvListarCotizacion.GetFocusedRow() as eProformas.eProformas_Detalle;
            if (obj == null) return;
            decimal prc_descuento = obj.prc_descuento, imp_separacion = obj.imp_separacion, prc_cuota_inicial = obj.prc_cuota_inicial, 
                prc_interes = obj.prc_interes, imp_descuento = obj.imp_descuento, imp_cuota_inicial = obj.imp_cuota_inicial;

            if (e.Column.FieldName == "prc_descuento" && ((obj.prc_descuento * 100) > prc_descuento_maximo || (obj.prc_descuento * 100) < 0))
            {
                obj.prc_descuento = 0;
                MessageBox.Show("El porcentaje de descuento no debe ser mayor de " + prc_descuento_maximo.ToString("0.00") + ", ni menor de 0", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            var _sts_CUOI = (e.Column.FieldName == "imp_cuota_inicial") ? true : false;
            var _sts_descuento = (e.Column.FieldName == "imp_descuento") ? true : false;
            if (obj.cod_variable == "") //si se ingresa valor en la fila del filtro, actualiza todos los registros
            {
                for (int i = 0; i < gvListarCotizacion.RowCount; i++)
                {
                    eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
                    if (e.Column.FieldName == "prc_descuento") obj2.prc_descuento = prc_descuento;
                    if (e.Column.FieldName == "imp_separacion") obj2.imp_separacion = imp_separacion;
                    if (e.Column.FieldName == "imp_descuento") obj2.imp_descuento = imp_descuento;
                    if (e.Column.FieldName == "imp_cuota_inicial") obj2.imp_cuota_inicial = imp_cuota_inicial;
                    if (e.Column.FieldName == "prc_cuota_inicial") obj2.prc_cuota_inicial = prc_cuota_inicial;
                    if (e.Column.FieldName == "prc_interes") obj2.prc_interes = prc_interes;
                    Calculo_Cotizador(obj2, _sts_CUOI, _sts_descuento);
                }
            }
            else
            {
                Calculo_Cotizador(obj, _sts_CUOI, _sts_descuento);
            }



            if (e.Column.FieldName == "num_fraccion" && obj.cod_variable != "")
            {
                if (obj.num_fraccion <= 1 || obj.num_fraccion >= 11)
                {
                    MessageBox.Show("La cantidad de cuotas no debe ser mayor de 10, ni menor de 2", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    obj.num_fraccion = 2; return;
                }
                gvListarCotizacion.SelectRow(gvListarCotizacion.RowCount - 1);
                Calculo_Cotizador(obj, _sts_CUOI, _sts_descuento);
                return;
            }
            gvListarCotizacion.RefreshData();
        }

        private void Calculo_Cotizador(eProformas.eProformas_Detalle obj, Boolean sts_CUIO, Boolean sts_descuento)
        {
            if (obj.num_fraccion == 0 && obj.dsc_nombre_detalle.ToLower().Contains("contado"))
            {
                //obj.imp_separacion = 0;
                obj.imp_cuota_inicial = 0;
                obj.prc_cuota_inicial = 0;
                obj.prc_interes = 0;
                //obj.prc_descuento = 0;
            }
            obj.imp_descuento = obj.imp_descuento > 0 && sts_descuento ? obj.imp_descuento : obj.imp_precio_venta * obj.prc_descuento;
            obj.prc_descuento = sts_descuento ? obj.imp_descuento / obj.imp_precio_venta : obj.prc_descuento > 0 ? obj.prc_descuento : 0;
            obj.imp_precio_final = obj.imp_precio_venta - obj.imp_descuento;
            if (obj.imp_cuota_inicial + obj.imp_separacion > obj.imp_precio_final) { obj.imp_separacion = 0; obj.imp_cuota_inicial = 0; }
            obj.imp_cuota_inicial = obj.imp_cuota_inicial > 0 && sts_CUIO ? obj.imp_cuota_inicial : obj.prc_cuota_inicial == 0 ? 0 : (obj.imp_precio_final * obj.prc_cuota_inicial) - obj.imp_separacion;
            obj.imp_interes = (((obj.imp_precio_venta * (1 - obj.prc_descuento)) - obj.imp_cuota_inicial - obj.imp_separacion) * obj.prc_interes) * (obj.num_fraccion / 12);
            obj.prc_cuota_inicial = sts_CUIO ? (obj.imp_cuota_inicial + obj.imp_separacion) / obj.imp_precio_final : obj.prc_cuota_inicial > 0 ? obj.prc_cuota_inicial : 0;
            obj.imp_valor_cuota = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial + obj.imp_interes) / (obj.num_fraccion == 0 ? 1 : obj.num_fraccion);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                gvListarCotizacion.PostEditor(); gvListarCotizacion.RefreshData();
                if (gvListarCotizacion.RowCount > 0)
                {
                    if (gvListarCotizacion.SelectedRowsCount != 1) { HNG.MessageError("Debe seleccionar solo 1 escenario.", "ERROR"); return; }
                    foreach (int nRow in gvListarCotizacion.GetSelectedRows())
                    {
                        eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(nRow) as eProformas.eProformas_Detalle;
                        if (obj != null) objDetalle = obj;
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

    }
}