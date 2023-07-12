using BE_GestionLotes;
using DevExpress.Utils;
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

namespace UI_GestionLotes.Formularios.Gestion_Lotes
{
    public partial class frmPopupVisorLotes : DevExpress.XtraEditors.XtraForm
    {
        Image imgLibres24 = Properties.Resources.green_circle_24px;
        Image imgSeparados24 = Properties.Resources.yellow_circle_24px;
        Image imgVendidos24 = Properties.Resources.green_circle_24px;
        public List<eLotes_Separaciones> mylistTipoSeparacion = new List<eLotes_Separaciones>();
        Image imgpersonM = Properties.Resources.person_24px;
        Image imgpersonF = Properties.Resources.user_female_red_hair_24px;
        private readonly UnitOfWork unit;
        eLotes_Separaciones eLotSep = new eLotes_Separaciones();
        frmVisorLotes frmHandler;
        public string cod_cliente = "", cod_empresa = "", cod_prospecto = "", dsc_proyecto = "", codigo = "", codigoMultiple = "", cod_lote = "", cod_separacion = "", estado = "";
        Image imgPrincipal = Properties.Resources.ok_16px;
        Brush Mensaje = Brushes.Transparent;
        Rectangle picRect, picRect2;
        int markWidth = 16;

        private void gvExtenciones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvExtenciones_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int offset = 5, posInical = 0;
            //e.Handled = true;
            //e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.SeaShell), e.Bounds);
            e.DefaultDraw(); e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = Mensaje; Rectangle markRectangle;
            string priorityText = " ";
            //for (int i = 0; i < 2; i++)
            //{

            //    posInical = i == 0 ? 0 : i == 1 ? 120 : i == 2 ? 400 : 680;
            markRectangle = new Rectangle(e.Bounds.X * (posInical) + offset, e.Bounds.Y + 10, markWidth, markWidth);

            priorityText = " Separación Principal"; e.Graphics.DrawImage(imgPrincipal, markRectangle);
            //if (i == 1) { priorityText = " Separación Principal"; e.Graphics.DrawImage(imgPrincipal, markRectangle); }

            e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
            e.Appearance.ForeColor = Color.Blue;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            e.Appearance.Options.UseTextOptions = true;
            e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            //}
        }

        private void gvExtenciones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvExtenciones.GetRow(e.RowHandle) as eLotes_Separaciones;
                    if (e.Column.FieldName == "flg_tiene_extension") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_tiene_extension" && obj.flg_tiene_extension == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgPrincipal, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvExtenciones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void frmPopupVisorLotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        public frmPopupVisorLotes()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmPopupVisorLotes_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        internal frmPopupVisorLotes(frmVisorLotes frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }
        private void Inicializar()
        {
            //lblTitulo2.ForeColor = Program.Sesion.Colores.Verde;
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl2.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl3.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl4.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            lblpago.ForeColor = Program.Sesion.Colores.Verde;
            mmObservacion.Focus();
            AsignarDatosGenerales();
            calcularPrecioFinal();

        }

        private Boolean validarCadenaVacio(string valor)
        {
            if (valor == null)
            {
                return true;
            }
            if (valor.Trim() == "")
            {
                return true;
            }
            if (valor.Trim().Length == 0)
            {
                return true;
            }
            return false;
        }

        private void AsignarDatosGenerales()
        {
            eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("6", codigo, codigoMultiple, cod_separacion, cod_lote);
            if(eLotSep == null) { MessageBox.Show("Lote no configurado.", "Visor de Lotes", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            //txtSeparacion.Text = eLotSep.imp_separacion.ToString();
            //if (eLotSep.fch_Separacion.Year == 1) { dtFechaSeparacion.EditValue = null; } else { dtFechaSeparacion.EditValue = eLotSep.fch_Separacion; }
            lblTitulo1.Text = eLotSep.dsc_titulo;
            lblTitulo2.Text = eLotSep.dsc_titulo2;
            txtNombresCopropietario.Text = eLotSep.dsc_copropietario;
            txtNroDocumentoCopropietario.Text = eLotSep.dsc_documento_CO;
            txtTelefCopropietario.Text = eLotSep.dsc_telefono_1_CO;
            txtCorreoPersonalCopropietario.Text = eLotSep.dsc_email_CO;
            txtNombres.Text = eLotSep.dsc_cliente;
            cod_separacion = eLotSep.cod_separacion;
            txtNroDocumento.Text = eLotSep.dsc_documento;
            txtTelef.Text = eLotSep.dsc_telefono_1;
            txtPreTerreno.Text = eLotSep.imp_precio_total.ToString();
            txtDescuentoPorc.Text = eLotSep.prc_descuento.ToString();
            txtDescuentoSol.Text = eLotSep.imp_descuento.ToString();
            txtCuoInicial.Text = eLotSep.imp_cuota_inicial.ToString();
            txtCorreoPersonal.Text = eLotSep.dsc_email;
            cod_cliente = eLotSep.cod_cliente;
            //txtFraccion.Text = eLotSep.f
            PrecioFinalFinanciar.Text = eLotSep.imp_precio_final.ToString();
            mmObservacion.Text = eLotSep.dsc_observacion;
            if (validarCadenaVacio(eLotSep.dsc_copropietario))
            {
                Image imgCopro = Properties.Resources.male_user_120px;
                picCopro.EditValue = imgCopro;
            }
            

            //Image imgHombre = Properties.Resources.user_84px;
            //picUser.EditValue = imgHombre;
            if (eLotSep.cod_forma_pago == "CO")
            {
                lblpago.Text = "CONTADO";
                lytSaldoFinanciar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytCoutaInicial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytPago.Text = "Fecha Pago al Contado :";
                if (eLotSep.fch_pago_total.Year == 1) { dtFecPagoCuota.EditValue = null; } else { dtFecPagoCuota.EditValue = eLotSep.fch_pago_total; }

            }
            else
            {
                if (eLotSep.fch_pago_cuota.Year == 1) { dtFecPagoCuota.EditValue = null; } else { dtFecPagoCuota.EditValue = eLotSep.fch_pago_cuota; }
            }
            CargarListadoTipoSeparaciones("5");
            //if (eLotSep.cod_status == "STL00004")
            //{
            //    picStatus.EditValue = imgVendidos24;


            //}
            //else if (eLotSep.cod_status == "STL00003")
            //{
            //    picStatus.EditValue = imgSeparados24;

            //}
            //else
            //{
            //    picStatus.EditValue = imgLibres24;
            //}



        }

        public void CargarListadoTipoSeparaciones(string accion)
        {
            try
            {
                int nRow = gvExtenciones.FocusedRowHandle;

                mylistTipoSeparacion = unit.Proyectos.ListarTipoSeparacion<eLotes_Separaciones>(accion, cod_cliente, codigo.Trim(), cod_lote.Trim(), cod_separacion.Trim());
                eTipoSeparaciones.DataSource = mylistTipoSeparacion;
                gvExtenciones.FocusedRowHandle = nRow;

                //gvDocumentos_FocusedRowChanged(gvDocumentos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(nRow - 1, nRow));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void calcularPrecioFinal()
        {

            decimal calcularDescuentoPorc = 0, precioFinal = 0, impSeparacion = 0, cuoInicial = 0;

            calcularDescuentoPorc = Convert.ToDecimal(txtPreTerreno.Text.ToString()) * Convert.ToDecimal(txtDescuentoPorc.EditValue);

            precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcularDescuentoPorc);
            //precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcular) - Convert.ToDecimal(txtSeparacion.Text.Trim()) - Convert.ToDecimal(txtCuoInicial.Text.Trim());
            txtPreFinalDescuento.Text = precioFinal.ToString();

        }
    }
}