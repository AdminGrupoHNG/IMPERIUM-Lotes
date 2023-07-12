using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Security;
using Microsoft.Identity.Client;
using Microsoft.Office.Interop.Excel;
using Rectangle = System.Drawing.Rectangle;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;

namespace UI_GestionLotes.UserControls
{
    public partial class usrListados : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly UnitOfWork unit;
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");
        Image ImgXML = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxml_16x16.png");
        List<eLotes_Separaciones> listSeparaciones = new List<eLotes_Separaciones>();
        DateTime oPrimerDiaDelMes = new DateTime();
        DateTime oUltimoDiaDelMes = new DateTime();
        System.Drawing.Image ImgRegistrado = Properties.Resources.pencil_16px;
        System.Drawing.Image imgValAdmin = Properties.Resources.manager_16px;
        System.Drawing.Image imgValBanco = Properties.Resources.bank_16px;
        System.Drawing.Image imgBoleteado = Properties.Resources.clipboard_16px;
        Brush ConCriterios = Brushes.Green;
        Brush SinCriterios = Brushes.Red;
        Brush NAplCriterio = Brushes.Orange;
        Brush Mensaje = Brushes.Transparent;
        int markWidth = 16;
        string cod_empresa = "";

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        public usrListados()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void ucListados_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            oPrimerDiaDelMes = new DateTime(date.Year, 1, 1);
            oUltimoDiaDelMes = DateTime.Now.AddMonths(1).AddDays(-1);
            ////List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
            //if (list.Count >= 1) cod_empresa = list[0].cod_empresa;
            CargarListaSeparaciones(cod_empresa);
        }        

        private void CargarListaSeparaciones(string cod_empresa) 
        {
            listSeparaciones.Clear();
            listSeparaciones = unit.Proyectos.ListarSeparaciones<eLotes_Separaciones>("4", "00001",
                            cod_etapas_multiple: "00001,00002",
                            "01",
                            oPrimerDiaDelMes.ToString("yyyyMMdd"),
                            oUltimoDiaDelMes.ToString("yyyyMMdd"),
                            "ALL",
                            "SI",
                            "ESE00002"
                            );

            bsLotesSeparaciones.DataSource = null; bsLotesSeparaciones.DataSource = listSeparaciones; gvListaSeparaciones.RefreshData();
        }

        private void gcListaSeparaciones_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) CargarListaSeparaciones(cod_empresa);
        }

        private void gvListaSeparaciones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetRow(e.RowHandle) as eLotes_Separaciones;
                    if (e.Column.FieldName == "fch_Separacion" && obj.fch_Separacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_vct_separacion" && obj.fch_vct_separacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago_separacion" && obj.fch_pago_separacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago" && obj.fch_pago.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "dsc_status") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_registrado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Val_Admin") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Val_Banco") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Boleteado") e.DisplayText = "";
                    e.DefaultDraw();
                    if (e.Column.FieldName == "dsc_status")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "DESISTIMIENTO") { b = SinCriterios; } else if (cellValue == "VENDIDO") { b = ConCriterios; } else { b = NAplCriterio; }
                        //b = ConCriterios;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                    if (e.Column.FieldName == "flg_registrado" && obj.flg_registrado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgRegistrado, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_Val_Admin" && obj.flg_Val_Admin == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgValAdmin, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_Val_Banco" && obj.flg_Val_Banco == "SI")
                    {

                        e.Handled = true; e.Graphics.DrawImage(imgValBanco, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_Boleteado" && obj.flg_Boleteado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgBoleteado, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }

                    //if (e.Column.FieldName == "ctd_CECO") e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaSeparaciones_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "dsc_lote")
                {
                    e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }
        }

        private void gvListaSeparaciones_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaSeparaciones_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                    frmSepararLote frm = new frmSepararLote();
                    frm.codigo = obj.cod_proyecto;
                    frm.dsc_proyecto = "";
                    frm.cod_separacion = obj.cod_separacion;
                    frm.codigoMultiple = obj.cod_etapa;
                    frm.MiAccion = Separacion.Vista;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaSeparaciones_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
    }
}
