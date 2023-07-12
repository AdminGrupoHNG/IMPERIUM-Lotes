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

namespace UI_GestionLotes.Formularios.Marketing
{
    public partial class frmMantDetalleCampanha : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public string cod_campanha = "", cod_empresa = "", cod_proyecto = "", cod_proyectoInicial = "", dsc_campanha = "";
        public decimal total = 0;
        List<eCampanha.eCampanha_Detalle> ListcampanhasDetalle = new List<eCampanha.eCampanha_Detalle>();
        public frmMantDetalleCampanha()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmMantDetalleCampanha_Load(object sender, EventArgs e)
        {
            cod_proyectoInicial = cod_proyecto;
            string[] aCodigos = cod_proyecto.Split("|".ToCharArray());
            cod_proyecto = aCodigos[1];

            txtCampanha.Text = dsc_campanha;

            ListcampanhasDetalle = unit.Campanha.ListarcampanhasDetalle<eCampanha.eCampanha_Detalle>(1, cod_empresa, cod_proyecto, cod_campanha);
            bsCampanhaDetalle.DataSource = ListcampanhasDetalle;
            if(ListcampanhasDetalle.Count > 0) { txtTotal.EditValue = ListcampanhasDetalle[0].imp_importe_facturado_total; }
            cargarCombo();
        }
        public void cargarCombo()
        {
            rlkpEstado.DataSource = unit.Campanha.CombosEnGridControl<eCampanha.eCampanha_Detalle>("Estado");
        }

        private void gvListadoCampanhaDetalle_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gvListadoCampanhaDetalle_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoCampanhaDetalle_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoCampanhaDetalle_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle >= 0)
            //    {
            //        eCampanha.eCampanha_Detalle obj = gvListadoCampanhaDetalle.GetRow(e.RowHandle) as eCampanha.eCampanha_Detalle;
            //        if (obj.cod_campanha_detalle != "" && obj.cod_campanha_detalle != null)
            //        {
            //            e.Appearance.BackColor = Color.LightGreen;
            //            e.Appearance.ForeColor = Color.DarkBlue;
            //            e.Appearance.FontStyleDelta = FontStyle.Bold;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = Color.OrangeRed;
            //            e.Appearance.ForeColor = Color.DarkBlue;
            //            e.Appearance.FontStyleDelta = FontStyle.Bold;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eCampanha.eCampanha_Detalle obj = gvListadoCampanhaDetalle.GetFocusedRow() as eCampanha.eCampanha_Detalle;
            DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este registro?", "Detalle campaña", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                if (obj == null) { return; }
                obj.imp_importe_facturado_total = Convert.ToDecimal(txtTotal.EditValue) - obj.imp_importe;
                //txtTotal.EditValue = obj.imp_importe_facturado_total;
                unit.Campanha.Guardar_Actualizar_campanha_Detalle<eCampanha.eCampanha_Detalle>(obj, 2);
                txtTotal.Text = unit.Campanha.Listarcampanhas<eCampanha>(0, cod_proyectoInicial, Program.Sesion.Usuario.cod_usuario, cod_campanha).First().imp_importe_facturado.ToString();
                txtTotal.Refresh();
                MessageBox.Show("Se ha eliminado correctamente.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bsCampanhaDetalle.Remove(obj);
            }
        }

        private void gvListadoCampanhaDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void gvListadoCampanhaDetalle_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                total = 0;
                eCampanha.eCampanha_Detalle obj = gvListadoCampanhaDetalle.GetFocusedRow() as eCampanha.eCampanha_Detalle;
                gvListadoCampanhaDetalle.PostEditor(); gvListadoCampanhaDetalle.RefreshData();
                if (obj == null) { e.Valid = false; return; }
                if (obj.fch_registro_detalle == null) { e.Valid = false; return; }
                if (String.IsNullOrEmpty(obj.dsc_identificador_transaccion)) { e.Valid = false; return; }
                if (String.IsNullOrEmpty(obj.dsc_metodo_pago)) { e.Valid = false; return; }
                if (obj.imp_importe <= 0) { e.Valid = false; return; }
                if (String.IsNullOrEmpty(obj.cod_estado_pago)) { e.Valid = false; return; }

                for (int x = 0; x < bsCampanhaDetalle.List.Count; x++)
                {
                    eCampanha.eCampanha_Detalle objSuma = bsCampanhaDetalle.List[x] as eCampanha.eCampanha_Detalle;
                    total = objSuma.imp_importe + total;
                }

                obj.cod_campanha = cod_campanha;
                obj.cod_empresa = cod_empresa;
                obj.cod_proyecto = cod_proyecto;
                obj.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                obj.imp_importe_facturado_total = total;
                eCampanha.eCampanha_Detalle objCampanha = unit.Campanha.Guardar_Actualizar_campanha_Detalle<eCampanha.eCampanha_Detalle>(obj, 1);
                if (objCampanha == null) MessageBox.Show("Error al guardar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtTotal.Text = unit.Campanha.Listarcampanhas<eCampanha>(0, cod_proyectoInicial, Program.Sesion.Usuario.cod_usuario, cod_campanha).First().imp_importe_facturado.ToString();
                    txtTotal.Refresh();
                    MessageBox.Show("Se ha gardado satisfactoriamente.", "GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    obj.cod_campanha_detalle = objCampanha.cod_campanha_detalle;
                    gvListadoCampanhaDetalle.RefreshData();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}