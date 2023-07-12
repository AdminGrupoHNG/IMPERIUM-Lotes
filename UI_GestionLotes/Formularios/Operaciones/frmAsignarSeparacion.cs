using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;      
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Formularios.Gestion_Contratos;

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class frmAsignarSeparacion : DevExpress.XtraEditors.XtraForm
    {
        System.Drawing.Image ImgRegistrado = Properties.Resources.pencil_16px;
        System.Drawing.Image imgValAdmin = Properties.Resources.manager_16px;
        System.Drawing.Image imgValBanco = Properties.Resources.bank_16px;
        System.Drawing.Image imgBoleteado = Properties.Resources.clipboard_16px;
        System.Drawing.Image imgTieneExtension = Properties.Resources.clone_figure_16px;
        System.Drawing.Image imgEsExtension = Properties.Resources.orthogonal_view_16px;
        private readonly UnitOfWork unit;
        frmMantCliente frmHandler;
        frmMantContratos frmHandlerMantContratos;
        Image ImgPago = DevExpress.Images.ImageResourceCache.Default.GetImage("images/richedit/differentoddevenpages_16x16.png");
        Brush ConCriterios = Brushes.Green;
        Brush SinCriterios = Brushes.Red;
        Brush NAplCriterio = Brushes.Orange;
        Brush PenCriterio = Brushes.Yellow;
        Brush Mensaje = Brushes.Transparent;
        string dsc_proyecto = "";
        Rectangle picRect, picRect2;
        int markWidth = 16;
        public string CodMenu, DscMenu, cod_empresa = "", cod_proyecto = "", Cod_campnhaFiltro = "";
        List<eLotes_Separaciones> listSeparaciones = new List<eLotes_Separaciones>();


        public frmAsignarSeparacion()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmAsignarSeparacion(frmMantCliente frm, frmMantContratos frmContratos)
        {
            InitializeComponent();
            frmHandler = frm;
            frmHandlerMantContratos = frmContratos;
            unit = new UnitOfWork();
        }
        private void frmAsignarSeparacion_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private void gvListaSeparaciones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaSeparaciones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

       

        private void gvListaSeparaciones_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int offset = 5, posInical = 0;
            //e.Handled = true;
            //e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.SeaShell), e.Bounds);
            e.DefaultDraw(); e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = Mensaje; Rectangle markRectangle;
            string priorityText = "Leyenda :";
            for (int i = 0; i < 2; i++)
            {

                posInical = i == 0 ? 0 : i == 1 ? 120 : i == 2 ? 400 : 680;
                markRectangle = new Rectangle(e.Bounds.X * (posInical) + offset, e.Bounds.Y + 10, markWidth, markWidth);
                if (i == 1) { priorityText = " - Separación con prospecto"; e.Graphics.DrawImage(ImgPago, markRectangle); }

                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                e.Appearance.Options.UseTextOptions = true;
                e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            }



            //USAR SI QUIERES PINTAR LA IMAGEN DEBAJO DE UNA COLUMA EN ESPECIFICO
            //e.Handled = true;
            //    e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.SeaShell), e.Bounds);
            //Point pI = CalcPosition(e, ImgPago, "flg_prospecto");
            //picRect = new Rectangle(pI, new Size(ImgPago.Width, ImgPago.Height));
            //e.Graphics.DrawImage(ImgPago, pI);





        }

        private void lkpProyecto_EditValueChanged(object sender, EventArgs e)
        {
            unit.Proyectos.CargaCombosChecked("Etapa", chkcbEtapas, "cod_etapa", "dsc_descripcion", "", lkpProyecto.EditValue.ToString());
            //chkcbEtapas.AutoSizeInLayoutControl = true;
            chkcbEtapas.Refresh();
            chkcbEtapas.CheckAll();
            if (lkpProyecto.EditValue != null)
            {
                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                if (dataRow != null)
                {
                    dsc_proyecto = dataRow["dsc_nombre"].ToString();


                }

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarListado();
        }

        private void gvListaSeparaciones_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;

                    if (frmHandler != null)
                    {
                        frmHandler.campos_separaciones = obj;
                        frmHandler.cod_cliente = obj.cod_cliente;
                        frmHandler.cod_empresa = obj.cod_empresa;
                        frmHandler.cod_proyecto = obj.cod_proyecto;

                        frmHandler.AsignarCamposClientesSeparaciones();
                        this.Close();
                    }
                    //if (validarFormulario == 1)
                    //{                        
                    //    frmHandler.campos_prospecto = obj;
                    //    frmHandler.AsignarCamposSeparacionProspecto();
                    //    this.Close();
                    //}
                    //else if (validarFormulario == 2)
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaSeparaciones_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "dsc_lote")
                {
                    e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }
        }

        private void gvListaSeparaciones_RowStyle_1(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaSeparaciones_CustomDrawCell_1(object sender, RowCellCustomDrawEventArgs e)
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
                    if (e.Column.FieldName == "fch_vct_cuota" && obj.fch_vct_cuota.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_cambio" && obj.fch_cambio.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_registro" && obj.fch_registro.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "dsc_status") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_registrado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Val_Admin") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Val_Banco") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Boleteado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_tiene_extension") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_es_extension") e.DisplayText = "";

                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Ene")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Ene", "1"); /*e.CellValue = 1;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Feb")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Feb", "2"); /*e.CellValue = 2;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Mar")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Mar", "3"); /*e.CellValue = 3;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Abr")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Abr", "4"); /*e.CellValue = 4;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("May")) e.DisplayText = obj.dsc_periodo.ToString().Replace("May", "5"); /*e.CellValue = 5;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Jun")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Jun", "6"); /*e.CellValue = 6;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Jul")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Jul", "7"); /*e.CellValue = 7;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Ago")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Ago", "8"); /*e.CellValue = 8;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Sep")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Sep", "9"); /*e.CellValue = 9;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Oct")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Oct", "10");/* e.CellValue = 10;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Nov")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Nov", "11");/* e.CellValue = 11;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Dic")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Dic", "12");/* e.CellValue = 12;*/

                    e.DefaultDraw();
                    if (e.Column.FieldName == "dsc_status")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "DESISTIMIENTO" || obj.cod_status.ToString() == "ESE00003") { b = SinCriterios; }
                        else if (obj.cod_status.ToString() == "ESE00001" && obj.contrato_firmado.ToString() != "SI") { b = NAplCriterio; }
                        else if (cellValue == "VENDIDO" && obj.contrato_firmado.ToString() == "SI") { b = ConCriterios; }
                        else { b = PenCriterio; }
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
                    if (e.Column.FieldName == "flg_tiene_extension" && obj.flg_tiene_extension == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgTieneExtension, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_es_extension" && obj.flg_es_extension == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgEsExtension, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    //if (e.Column.FieldName == "ctd_CECO") e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaSeparaciones_CustomDrawColumnHeader_1(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaSeparaciones_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;

                    frmHandlerMantContratos.AsignarDatosSeparacion(obj);
                    frmHandlerMantContratos.dsc_proyecto = dsc_proyecto;

                    //frmHandler.transferirDatos(obj);

                    this.Close();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVincularSep_Click(object sender, EventArgs e)
        {
            try
            {

                eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                if (obj != null)
                {
                    frmHandlerMantContratos.AsignarDatosSeparacion(obj);
                    frmHandlerMantContratos.dsc_proyecto = dsc_proyecto;

                    //frmHandler.transferirDatos(obj);

                    this.Close();
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void chkcbEtapas_Popup(object sender, EventArgs e)
        //{
        //CheckBox cb = new CheckBox();
        //cb.Text = "Text";
        //cb.AutoSize = true;
        //    Control c = (sender as IPopupControl).PopupWindow.Controls[1];
        //    cb.Location = new Point(c.Location.X + c.Width, c.Location.Y);
        //    (sender as IPopupControl).PopupWindow.Controls.Add(cb);

        //}

        private void CargarCombos()
        {
            unit.Proyectos.CargaCombosLookUp("ProyectoUnico", lkpProyecto, "cod_codigo", "dsc_nombre", cod_proyecto, "", valorDefecto: true);


        }



        private Point CalcPosition(RowObjectCustomDrawEventArgs e, Image img, string nombreColumn)
        {
            Point p = Point.Empty;
            GridColumn col = gvListaSeparaciones.Columns[nombreColumn];
            GridViewInfo info = (gvListaSeparaciones.GetViewInfo() as GridViewInfo);
            int indicatorW = info.ViewRects.IndicatorWidth;
            int left = info.GetColumnLeftCoord(col);
            p.X = left + indicatorW + (col.VisibleWidth - img.Width) / 2;
            p.Y = e.Bounds.Location.Y + (e.Bounds.Height - img.Height) / 2;
            return p;
        }

        private void gvListaSeparaciones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetRow(e.RowHandle) as eLotes_Separaciones;
                    if (e.Column.FieldName == "flg_prospecto") e.DisplayText = "";
                    e.DefaultDraw();
                    if (e.Column.FieldName == "flg_prospecto" && obj.flg_prospecto == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPago, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void Inicializar()
        {
            CargarCombos();
            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnVincularSep.Appearance.BackColor = Program.Sesion.Colores.Verde;
            CargarListado();
            //DateTime date = DateTime.Now;
            //DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            //DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            //dtFechaInicio.EditValue = oPrimerDiaDelMes;
            //dtFechaFin.EditValue = oUltimoDiaDelMes;
            //grdbFecha.SelectedIndex = 0;

            //CargarOpcionesMenu();
            //CargarListado("TODOS", "");

            //lblTitulo.ForeColor = Program.Sesion.Colores.Verde;

            //lblTitulo.Text = navBarControl1.SelectedLink.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            //picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            //navBarControl1.Groups[0].SelectedLinkIndex = 0;


            //CargarListadoResumen();
            //vistaResumen();
        }

        public void CargarListado()
        {
            try
            {
                cod_proyecto = "";
                string dsc_descripcion = "";
                string proyectos = "";
                string etapas = "";


                //cod_etapasmultiple = etapaMultiple;
                //cod_proyecto = proyectoMultiple;

                //listSeparaciones.Clear();
                listSeparaciones = unit.Proyectos.ListarSeparaciones<eLotes_Separaciones>("8", lkpProyecto.EditValue.ToString(), chkcbEtapas.EditValue.ToString().Replace(" ", ""), flg_activo: "SI"
                    //cod_proyecto,
                    //cod_etapas_multiple: etapaMultiple,
                    //grdbFecha.EditValue.ToString(),
                    //Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                    //Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                    //cod_estado_separacion
                    );

                bsLotesSeparaciones.DataSource = null; bsLotesSeparaciones.DataSource = listSeparaciones; gvListaSeparaciones.RefreshData();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }
}