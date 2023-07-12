using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using BE_GestionLotes;
using BL_GestionLotes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.Images;
using DevExpress.XtraSplashScreen;
using System.Drawing.Drawing2D;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using UI_GestionLotes.Formularios.Operaciones;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    public partial class frmListadoProyectos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        Rectangle picRect, picRect2;
        int markWidth = 50;
        public string cod_etapa = "";
        public string cod_proyecto = "";
        bool Buscar = false;


        public frmListadoProyectos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoProyectos_Load(object sender, EventArgs e)
        {
            Inicializar();
            btnMaestroLotes.Appearance.BackColor = Program.Sesion.Colores.Verde;
            gcEtapas.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            gcCantidad.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            gcPrecios.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            HabilitarBotones();

            //navBarControl1.Visible = true;
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                //ribbonPageGroup1.Enabled = listPermisos[0].flg_escritura;
                //grupoPersonalizarVistas.Enabled = listPermisos[0].flg_escritura;
                //btnCargaMasivaEMO.Enabled = listPermisos[0].flg_escritura;
            }
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 10 || x.cod_perfil == 11 || x.cod_perfil == 12);
            if (oPerfil == null)
            {
                btnMaestroLotes.Enabled = false;
            }
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantProyecto frm = new frmMantProyecto();
                if (Application.OpenForms["frmMantProyecto"] != null)
                {
                    Application.OpenForms["frmMantProyecto"].Activate();
                }
                else
                {
                    frm.MiAccion = Proyecto.Nuevo;

                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void gvListaProyectos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    CargarListadoStatusXEtapas("10");
                    eProyecto obj = gvListaProyectos.GetFocusedRow() as eProyecto;
                    frmMantProyecto frm = new frmMantProyecto(this);
                    frm.cod_proyecto = obj.cod_proyecto;
                    frm.MiAccion = Proyecto.Editar;
                    frm.MiAccionEtapa = Proyecto.Editar;




                    //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaProyectos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eProyecto obj = gvListaProyectos.GetFocusedRow() as eProyecto;
                    if(obj == null) { return; }
                    cod_proyecto = obj.cod_proyecto;
                    CargarListadoEtapas("3");
                    bsStatusXetapa.DataSource = null;
                    CargarListadoStatusXEtapas("10");

                    CargarListadoSumaPrecios("11");


                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            inactivar();
        }

        private void inactivar()
        {
            //eProyecto obj = gvListaProyectos.GetFocusedRow() as eProyecto;

            //obj = unit.Proyectos.Mantenimiento_Proyecto<eProyecto>(obj, "3");

            //if (obj != null)
            //{
            //    XtraMessageBox.Show(""+ obj.dsc_nombre + " inactivado de manera satisfactoria", "Inactivar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void activar()
        {
            eProyecto obj = gvListaProyectos.GetFocusedRow() as eProyecto;

            //obj = unit.Proyectos.Mantenimiento_Proyecto<eProyecto>(obj, "4");
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            //int nRow = gvListaProyectos.FocusedRowHandle;
            //CargarListado(navBarControl1.SelectedLink.Group.Caption, navBarControl1.SelectedLink.Item.Tag.ToString());
            //gvListaProyectos.FocusedRowHandle = nRow;
            //SplashScreenManager.CloseForm();

            if (obj != null)
            {
                XtraMessageBox.Show("" + obj.dsc_nombre + " activado de manera satisfactoria", "Activar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void eliminar()
        {
            eProyecto obj = gvListaProyectos.GetFocusedRow() as eProyecto;

            //obj = unit.Proyectos.Mantenimiento_Proyecto<eProyecto>(obj, "5");

            if (obj != null)
            {
                XtraMessageBox.Show("" + obj.dsc_nombre + " eliminado de manera satisfactoria", "Eliminar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            activar();
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            var result = XtraMessageBox.Show("¿Seguro que desea eliminar el Proyecto?", "Eliminar Proyecto", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                eliminar();
            }

        }

        public void CargarListadoEtapas(string accion)
        {
            try
            {
                List<eProyecto_Etapa> ListProyectoEtapa = new List<eProyecto_Etapa>();
                ListProyectoEtapa = unit.Proyectos.ListarEtapa<eProyecto_Etapa>(accion, "", cod_proyecto);
                if (ListProyectoEtapa.Count > 0)
                {
                    bsEtapa.DataSource = ListProyectoEtapa;
                }
                else
                {
                    bsEtapa.DataSource = null;
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarListadoStatusXEtapas(string accion, string etapa = "")
        {
            try
            {
                List<eLotesxProyecto> ListStatusEtapa = new List<eLotesxProyecto>();
                ListStatusEtapa = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>(accion, cod_proyecto, etapa);
                if(ListStatusEtapa.Count > 0)
                {
                    gcCantidad.Text = "CANTIDAD DE LOTES  :  " + ListStatusEtapa[0].num_total_lotes;
                    bsStatusXetapa.DataSource = ListStatusEtapa;
                }
                else
                {
                    gcCantidad.Text = "CANTIDAD DE LOTES  :  " + 0;
                    bsStatusXetapa.DataSource = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void CargarListadoSumaPrecios(string accion, string etapa = "")
        {
            try
            {
                List<eLotesxProyecto> ListSumaPrecio = new List<eLotesxProyecto>();
                ListSumaPrecio = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>(accion, cod_proyecto, etapa);
                if (ListSumaPrecio.Count > 0)
                {
                    gcPrecios.Text = "TOTAL PRECIO DE VENTA  :  " + ListSumaPrecio[0].dsc_sum_precio_total;
                    bsPrecioSumXetapa.DataSource = ListSumaPrecio;

                }
                else
                {
                    gcPrecios.Text = "TOTAL PRECIO DE VENTA  :  " + 0;
                    bsPrecioSumXetapa.DataSource = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eProyecto_Empresas> ListProyecEmp = unit.Proyectos.ListarOpcionesMenu<eProyecto_Empresas>("1");
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
            NavEmpresa.Caption = "Por Empresa"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
            NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

            foreach (eProyecto_Empresas obj in ListProyecEmp)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_empresa; NavDetalle.Name = obj.cod_empresa;
                NavDetalle.Caption = obj.dsc_empresa; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavEmpresa.ItemLinks.Add(NavDetalle);
            }
        }

        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            //SplashScreen.Open("Obteniendo listado", "Cargando...");
            CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
            SplashScreenManager.CloseForm(false);
            //SplashScreen.Close();
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            CargarListado("TODOS", "");

            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;

            lblTitulo.Text = navBarControl1.SelectedLink.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
        }

        public void CargarListado(string NombreGrupo, string Codigo)
        {
            try
            {
                string cod_proyecto = "", accion = "", cod_empresa = "";

                switch (NombreGrupo)
                {
                    case "TODOS": accion = "1"; break;
                    case "Por Empresa": cod_empresa = Codigo; accion = "3"; break;
                }
                List<eProyecto> ListProyecto = new List<eProyecto>();
                ListProyecto = unit.Proyectos.ListarProyectos<eProyecto>(accion, cod_proyecto, cod_empresa);

                bsListaProyectos.DataSource = ListProyecto;
                gvListaProyectos.RefreshData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void navBarControl1_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {
            e.Group.SelectedLinkIndex = 0;
            navBarControl1_SelectedLinkChanged(navBarControl1, new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs(e.Group, e.Group.SelectedLink));

        }

        private void navBarControl1_SelectedLinkChanged(object sender, DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs e)
        {
            if (!Buscar) e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null && Buscar)
            {
                ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //SplashScreen.Open("Obteniendo listado", "Cargando...");
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm(false);
                //SplashScreen.Close();
            }
        }

        void ActiveGroupChanged(string caption, Image imagen)
        {
            lblTitulo.Text = caption; picTitulo.Image = imagen;
        }

        private void gvListaProyectos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaProyectos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaProyectos_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //e.Handled = true;
            //e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.SeaShell), e.Bounds);
            //Point pI = CalcPosition(e, txtFooter, "imp_precio_terreno");
            //picRect = new Rectangle(pI, new Size(txtFooter.Width, txtFooter.Height));

            //e.Graphics.DrawImage(ImgPago, pI);
            //CalcPosition(DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs, txtFooter, "");
        }

        private Point CalcPosition(TextEdit text, string nombreColumn)
        {
            Point p = Point.Empty;
            GridColumn col = gvListaProyectos.Columns[nombreColumn];
            GridViewInfo info = (gvListaProyectos.GetViewInfo() as GridViewInfo);
            int indicatorW = info.ViewRects.IndicatorWidth;
            int left = info.GetColumnLeftCoord(col);
            p.X = left + indicatorW + (col.VisibleWidth - text.Width) / 2;
            p.Y = gcListaProyectos.Bounds.Location.Y + (gcListaProyectos.Bounds.Height - text.Height) / 2;
            return p;
        }

        private void btnConfLotes_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantProyecto frm = new frmMantProyecto();
                if (Application.OpenForms["frmMantProyecto"] != null)
                {
                    Application.OpenForms["frmMantProyecto"].Activate();
                }
                else
                {
                    frm.cod_proyecto = cod_proyecto;
                    frm.MiAccion = Proyecto.Editar;
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void frmListadoProyectos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //XtraMessageBox.Show("Actualizado", "F5", MessageBoxButtons.OK, MessageBoxIcon.Information);
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //SplashScreen.Open("Obteniendo listado", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm(false);
                //SplashScreen.Close();
            }
        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            navBarControl1.OptionsNavPane.CollapsedWidth = 50;
            navBarControl1.OptionsNavPane.ExpandedWidth = 200;
            if (layoutControlItem3.ContentVisible == true)
            {
                layoutControlItem3.ContentVisible = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                return;
            }
            if (layoutControlItem3.ContentVisible == false)
            {
                layoutControlItem3.ContentVisible = true;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                return;
            }
        }

        private void btnMaestroLotes_Click(object sender, EventArgs e)
        {
            //formName = "ListaControlLotes";
            if (Application.OpenForms["frmListadoControlLotes"] != null)
            {
                Application.OpenForms["frmListadoControlLotes"].Activate();
            }
            else
            {
                //eProyecto ePro = gvListaProyectos.GetFocusedRow() as eProyecto;
                frmListadoControlLotes frm = new frmListadoControlLotes();
                //if (ePro != null) { frm.dsc_proyecto = ePro.dsc_nombre; frm.cod_proyecto = ePro.cod_proyecto; }
                frm.MdiParent = MdiParent;
                frm.Show();
                frm.Activate();
            }
        }

        private void btnVerMemoriaDescriptiva_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnVerPadronAreaUE_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eProyecto ePro = gvListaProyectos.GetFocusedRow() as eProyecto;
                if(ePro == null) { SplashScreenManager.CloseForm(false); return; } 
                var formato = new FormatoWordHelper();
                formato.ShowWordReportFormatoGeneral(ePro.cod_empresa, "00010");

                ////SplashScreen.Open("Obteniendo reporte", "C/*a*/rgando...");
                ////eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                //if (cod_proyecto == null) { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                //rptFichaPadronArea report = new rptFichaPadronArea();
                //ReportPrintTool printTool = new ReportPrintTool(report);
                ////detalleLotes printTool = new detalleLotes(report);
                //report.RequestParameters = false;
                //printTool.AutoShowParametersPanel = false;
                //report.Parameters["cod_proyecto"].Value = cod_proyecto;
                ////report.BackColor = Color.FromArgb(0, 157, 150);
                ////SplashScreen.Close();
                SplashScreenManager.CloseForm(false);

                //report.ShowPreviewDialog();
                ////SplashScreenManager.CloseForm();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVerMemoriaDescriptiva_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eProyecto ePro = gvListaProyectos.GetFocusedRow() as eProyecto;

                var formato = new FormatoWordHelper();
                formato.ShowWordReportFormatoGeneral(ePro.cod_empresa, "00009");
                ////SplashScreen.Open("Obteniendo reporte", "Cargando...");
                ////eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                //if (cod_proyecto == null || cod_proyecto == "") { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                //rptVerMemoriaDescriptiva report = new rptVerMemoriaDescriptiva();
                //ReportPrintTool printTool = new ReportPrintTool(report);
                //report.RequestParameters = false;
                //printTool.AutoShowParametersPanel = false;
                //report.Parameters["cod_proyecto"].Value = cod_proyecto;
                ////SplashScreen.Close();

                //report.ShowPreviewDialog();
                SplashScreenManager.CloseForm(false);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



            //try
            //{
            //    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
            //    eProyecto ePro = gvListaProyectos.GetFocusedRow() as eProyecto;
            //    if (ePro.cod_proyecto == null) { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            //    rptVerMemoriaDescriptiva report = new rptVerMemoriaDescriptiva();
            //    ReportPrintTool printTool = new ReportPrintTool(report);
            //    report.RequestParameters = false;
            //    printTool.AutoShowParametersPanel = false;
            //    report.Parameters["cod_proyecto"].Value = ePro.cod_proyecto;
            //    report.ShowPreviewDialog();
            //    SplashScreenManager.CloseForm();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Tools.Exportar().ExportarExcel(gcListaProyectos, "Listado Proyecto");
        }

       

        private void chartEtapasxProyecto_SelectedItemsChanged(object sender, DevExpress.XtraCharts.SelectedItemsChangedEventArgs e)
        {
            try
            {
                foreach (eProyecto_Etapa obj in chartEtapasxProyecto.SelectedItems)
                {
                    if (obj == null) continue;
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reportes", "Cargando...");
                    //SplashScreen.Open("Obteniendo reporte", "Cargando...");

                    CargarListadoStatusXEtapas("5", obj.cod_etapa);
                    CargarListadoSumaPrecios("12", obj.cod_etapa);
                    //CargarTipoDocumento(obj.nOrden);
                    //CargarTipoServicio(obj.nOrden);
                    //xtraTabControl1.SelectedTabPage = xtabVistaProv;
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //public void CargarListado()
        //{
        //    try
        //    {
        //        //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
        //        List<eProyecto> ListProyecto = new List<eProyecto>();
        //        ListProyecto = blProy.ListarProyectos<eProyecto>("1", "1", "1");
        //        /*bsListaClientes.DataSource = null; */
        //        bsListaProyectos.DataSource = ListProyecto;
        //        //SplashScreenManager.CloseForm();
        //    }
        //    catch (Exception e)
        //    {
        //        XtraMessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
    }
}