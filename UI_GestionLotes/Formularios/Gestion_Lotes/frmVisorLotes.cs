using BE_GestionLotes;
using DevExpress.Images;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Formularios.Marketing;
using UI_GestionLotes.Formularios.Operaciones;

namespace UI_GestionLotes.Formularios.Gestion_Lotes
{
    public partial class frmVisorLotes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        List<eTreeProyEtaStatus> listadoTreeList = new List<eTreeProyEtaStatus>();
        List<eLotesxProyecto> listLotesProyecto = new List<eLotesxProyecto>();

        Image imgLibres = Properties.Resources.green_circle_24px;
        Image imgpersonM = Properties.Resources.person_30px;
        Image imgContrato = Properties.Resources.page_with_curl_24px;
        Image imgpersonF = Properties.Resources.user_female_red_hair_24px;
        Image imgSeparados = Properties.Resources.yellow_circle_24px;
        Image imgVendidos50 = Properties.Resources.ubicacion_verde_x64;
        Image imgSeparados50 = Properties.Resources.ubicacion_amarillo_x64;
        Image imgLibres50 = Properties.Resources.ubicacion_azul_x64;

        public string cod_etapa = "";
        public string cod_proyecto = "", cod_etapasmultiple = "", dsc_proyecto = "", cod_status = "ALL";
        public string cod_empresa = "";
        public string caption = "";
        int validar = 0;

        public frmVisorLotes()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            //unit.Globales.ConfigurarGridView_TreeStyle(gridControl1, gridView1);
        }
        private void frmVisorLotes_Load(object sender, EventArgs e)
        {
            Inicializar();
            CargarListado();
            cargarTitulo();
            HabilitarBotones();
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
            }

        }

        private void Inicializar()
        {
            CargarTreeListCuatroNodos();
            //CargarOpcionesMenu();

            //CargarListado(navBarControl1.Groups[0].Caption, "", "4");
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            pcChevron.BackColor = Program.Sesion.Colores.Verde;
            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            layoutControlItem32.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            layoutControlItem12.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            //lblTitulo.Text = navBarControl1.Groups[0].Caption + ": MÁS RECIENTE";
            //lblTitulo.Text = navBarControl2.Groups[0].Caption + ": MÁS RECIENTE" + navBarControl2.SelectedLink.Item.Caption;
            //picTitulo.Image = navProyecto.ImageOptions.LargeImage;
            //navBarControl1.Groups[0].SelectedLinkIndex = 0;
            //Buscar = true;
            //CargarOpcionesMenuEtapas();
        }

        private void itemCardLotes1_Clicks(object sender, EventArgs e)
        {
            MessageBox.Show("dd");
        }

        private void CargarTreeListCuatroNodos()
        {
            listadoTreeList = unit.Proyectos.ListarOpcionesMenu<eTreeProyEtaStatus>("4");
            if (listadoTreeList != null && listadoTreeList.Count > 0)
            {
                new Tools.TreeListHelper(treeListProyectos).
                    TreeViewParaCuatroNodos<eTreeProyEtaStatus>(
                    listadoTreeList, "cod_pro", "dsc_pro",
                    "cod_proyecto", "dsc_proyecto",
                    "cod_etapa", "dsc_etapa", "cod_manzana", "dsc_manzana");

                treeListProyectos.Refresh();
                ListarRadiosCheck();
                treeListProyectos.CheckAll();

            }
        }

        private void ListarRadiosCheck()
        {
            treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            for (int i = 0; i < treeListProyectos.Nodes.Count; i++)
            {
                treeListProyectos.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;

                for (int j = 0; j < treeListProyectos.Nodes[i].Nodes.Count(); j++)
                {
                    //if (j == 0)
                    //{
                    //    treeListProyectos.Nodes[i].Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                    //}
                    //else
                    //{
                    //    treeListProyectos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                    //}
                    treeListProyectos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                    //final
                    for (int k = 0; k < treeListProyectos.Nodes[i].Nodes[j].Nodes.Count(); k++)
                    {
                        treeListProyectos.Nodes[i].Nodes[j].Nodes[k].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                    }

                }
            }

            treeListProyectos.ExpandAll();
        }

        private void CargarTreeListTresNodos()
        {
            treeListProyectos.Appearance.Row.BackColor = Color.Transparent;
            treeListProyectos.Appearance.Empty.BackColor = Color.Transparent;
            treeListProyectos.BackColor = Color.Transparent;
            treeListProyectos.TreeViewFieldName = "Name";
            treeListProyectos.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            treeListProyectos.OptionsBehavior.Editable = false;
            treeListProyectos.OptionsBehavior.ReadOnly = true;
            treeListProyectos.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeListProyectos.NodeCellStyle += OnNodeCellStyle;
            treeListProyectos.BeforeFocusNode += OnBeforeFocusNode;

            listadoTreeList = unit.Proyectos.ListarOpcionesMenu<eTreeProyEtaStatus>("5");
            if (listadoTreeList != null && listadoTreeList.Count > 0)
            {
                new Tools.TreeListHelper(treeListProyectos).
                    TreeViewParaTresNodos<eTreeProyEtaStatus>(
                    listadoTreeList, "cod_pro", "dsc_pro",
                    "cod_proyecto", "dsc_proyecto",
                    "cod_etapa", "dsc_etapa");

                treeListProyectos.Refresh();

                //treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                //treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;


                treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int i = 0; i < treeListProyectos.Nodes.Count; i++)
                {
                    treeListProyectos.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                    for (int j = 0; j < treeListProyectos.Nodes[i].Nodes.Count(); j++)
                    {
                        treeListProyectos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        //final
                        //for (int k = 0; k < treeListProyectos.Nodes[i].Nodes[j].Nodes.Count(); k++)
                        //{
                        //    treeListProyectos.Nodes[i].Nodes[j].Nodes[k].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        //}

                    }
                }
                treeListProyectos.CheckAll();

            }
        }

        void cargarTitulo()
        {
            eTreeProyEtaStatus objM = listadoTreeList.Find(x => x.cod_proyecto == cod_proyecto);
            lblTitulo.Text = objM.dsc_proyecto.ToString();
            //lblTitulo.Text = objM.dsc_pro.ToString() + " : " + objM.dsc_proyecto.ToString();
            dsc_proyecto = objM.dsc_proyecto.ToString();
        }

        public void CargarListado()//solo le mando el codigo de proyecto y el codigo de la manana
        {
            try
            {
                cod_proyecto = "";
                string dsc_descripcion = "";
                string proyectos = "";
                string etapas = "";

                var tools = new Tools.TreeListHelper(treeListProyectos);
                var proyectoMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(1);
                var etapaMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(2);
                var manzanas = tools.ObtenerCodigoConcatenadoDeNodoIndex(3);
                cod_etapasmultiple = etapaMultiple;
                cod_etapa = etapaMultiple;
                cod_proyecto = proyectoMultiple;

                listLotesProyecto.Clear();
                listLotesProyecto = unit.Proyectos.ListarConfLotes<eLotesxProyecto>("2", cod_proyecto, cod_proyecto_multiple: cod_proyecto, cod_etapas_multiple: cod_etapasmultiple, cod_status_multiple: manzanas, cod_status: cod_status);
                if(listLotesProyecto.Count == 0) { return; }
                bsLotesxProyecto.DataSource = null; bsLotesxProyecto.DataSource = listLotesProyecto; //cvListaLotesProyectos.RefreshData();
                tvListaLotesProyectos.RefreshData();
                int nTotal, nSeparados, nVendidos, nLibres;
                decimal nprcSeparados, nprcVendidos, nprcLibres;
                nTotal = listLotesProyecto.Count();
                if (cod_status == "ALL")
                {
                    tbiTotal.Elements[2].Text = nTotal.ToString() + " ";
                    nSeparados = listLotesProyecto.FindAll(x => x.cod_status == "STL00003").Count();
                    nVendidos = listLotesProyecto.FindAll(x => x.cod_status == "STL00004").Count();
                    nLibres = listLotesProyecto.FindAll(x => x.cod_status == "STL00001").Count();
                    nprcSeparados = ((decimal)nSeparados / nTotal) * 100;
                    nprcVendidos = ((decimal)nVendidos / nTotal) * 100;
                    nprcLibres = ((decimal)nLibres / nTotal) * 100;
                    nprcSeparados = decimal.Round(nprcSeparados, 2);
                    nprcVendidos = decimal.Round(nprcVendidos, 2);
                    nprcLibres = decimal.Round(nprcLibres, 2);

                    tbiSeparado.Elements[2].Text = nSeparados.ToString() + "   (" + nprcSeparados + "%) ";
                    tbiVendido.Elements[2].Text = nVendidos.ToString() + "   (" + nprcVendidos + "%) ";
                    tbiLibre.Elements[2].Text = nLibres.ToString() + "   (" + nprcLibres + "%) ";

                }
                //fltVisorLote.Controls.Clear();
                //foreach (eLotesxProyecto obj in listLotesProyecto)
                //{
                //    var casa = new Tools.ItemCardLotes();
                //    casa.DscLote = obj.dsc_lote;
                //    casa.UEMts = obj.num_area_uex.ToString();
                //    casa.PrecioTot = obj.imp_precio_total.ToString();
                //    casa.Semaforo = obj.cod_status == "STL00001" ? Color.Green : Color.Yellow ;
                //    fltVisorLote.Controls.Add(casa);
                //    //cod_empresa = obj.cod_empresa.ToString();

                //    //if (obj.cod_etapa == cod_etapalkp && obj.dsc_manzana == dsc_manzana)
                //    //{

                //    //    codigoManzana = obj.cod_manzana.ToString();
                //    //    contador++;
                //    //}
                //}


                //for (int i = 0; i < 10; i++)
                //{

                //    var casa = new Tools.ItemCardLotes();
                //    casa.DscLote = 
                //    //casa.Dock = DockStyle.Left;
                //    fltVisorLote.Controls.Add(casa);
                //}

                //int count = cod_etapasmultiple.Count(f => f == ',');
                lblTitulo.Text = navBarControl11.Groups[0].Caption + ": " + dsc_descripcion;

                //if (count == 0)
                //{

                //    CargarComboEnGrid(2);
                //    CargarListadoEtapas("2");
                //}
                //else
                //{
                //    CargarComboEnGrid(1);
                //    CargarListadoEtapas("1");
                //}


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cod_status = "ALL";
            CargarListado();
            cargarTitulo();
        }

        private void cvListaLotesProyectos_CustomCardCaptionImage(object sender, DevExpress.XtraGrid.Views.Card.CardCaptionImageEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotesxProyecto obj = cvListaLotesProyectos.GetRow(e.RowHandle) as eLotesxProyecto;
                    if (obj.cod_status == "STL00001") { e.Image = imgLibres; } else { e.Image = imgSeparados; }
                    //if (obj.cod_status == "STL00003") { e.Appearance.BackColor = Color.LightYellow; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }

                    //if (e.Image == "configurado") e.DisplayText = "";
                    //e.DefaultDraw();
                    //if (e.Column.FieldName == "configurado")
                    //{
                    //    Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //    string cellValue = e.CellValue.ToString();
                    //    if (cellValue == "NO") { b = SinCriterios; } else if (cellValue == "SI") { b = ConCriterios; } else { b = NAplCriterio; }
                    //    //b = ConCriterios;
                    //    e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void tbiSeparado_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                cod_status = "STL00003";
                CargarListado();
                cargarTitulo();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiVendido_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_status = "STL00004";
                CargarListado();
                cargarTitulo();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiLibre_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_status = "STL00001";
                CargarListado();
                cargarTitulo();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiTotal_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_status = "ALL";
                CargarListado();
                cargarTitulo();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pcChevron_Click(object sender, EventArgs e)
        {
            if (validar == 0)
            {
                System.Drawing.Image imgProyectoLogo = Properties.Resources.chevron_down_20px;
                pcChevron.EditValue = imgProyectoLogo;
                validar = 1;
                lytOcultar.ContentVisible = false;
                lytOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                pcChevron.ToolTipTitle = "Mostrar Filtros";
                return;
            }
            if (validar == 1)
            {
                System.Drawing.Image imgProyectoImagen = Properties.Resources.chevron_up_20px;
                pcChevron.EditValue = imgProyectoImagen;
                validar = 0;
                lytOcultar.ContentVisible = true;
                lytOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                pcChevron.ToolTipTitle = "Ocultar Filtros";
                return;
            }
        }

        private void scBuscarLote_QueryIsSearchColumn(object sender, QueryIsSearchColumnEventArgs args)
        {
            if (args.FieldName != "dsc_lote")
                args.IsSearchColumn = false;
        }

        private void tvListaLotesProyectos_ItemDoubleClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            try
            {
                eLotesxProyecto obj = tvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                if (obj.cod_status == "STL00003" || obj.cod_status == "STL00004")
                {
                    frmPopupVisorLotes frm = new frmPopupVisorLotes(this);
                    frm.codigo = obj.cod_proyecto;
                    frm.dsc_proyecto = dsc_proyecto;
                    frm.cod_lote = obj.cod_lote;
                    frm.codigoMultiple = obj.cod_etapa;
                    //frm.cod_cliente = obj.cod_cliente;
                    //frm.MiAccion = obj.cod_status != "ESE00002" || obj.flg_activo == "NO" ? Separacion.Vista : Separacion.Editar;
                    //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            frmPopupProyectoInfo frm = new frmPopupProyectoInfo();
            frm.cod_proyecto = cod_proyecto;
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
        }

        private void btnbtnOcultarFiltro1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (layoutControlItem8.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (layoutControlItem8.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                return;
            }


            //navBarControl11.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            //navBarControl11.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            //navBarControl11.OptionsNavPane.CollapsedWidth = 50;
            //navBarControl11.OptionsNavPane.ExpandedWidth = 200;
            //if (layoutControlItem8.ContentVisible == true)
            //{
            //    layoutControlItem8.ContentVisible = false;
            //    layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
            //    btnOcultarFiltro.ImageOptions.LargeImage = img;
            //    borrar.Caption = "Mostrar Filtro";
            //    layoutControlItem8.Width = 50;
            //    return;
            //}
            //if (layoutControlItem8.ContentVisible == false)
            //{
            //    layoutControlItem8.ContentVisible = true;
            //    layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
            //    btnOcultarFiltro.ImageOptions.LargeImage = img;
            //    btnOcultarFiltro.Caption = "Ocultar Filtro";
            //    return;
            //}
        }

        private void navBarControl11_Paint(object sender, PaintEventArgs e)
        {
            ListarRadiosCheck();
        }

        private void btnVerPadronAreaUE_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eLotesxProyecto ePro = tvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                var formato = new FormatoWordHelper();
                formato.ShowWordReportFormatoGeneral(ePro.cod_empresa, "00010");

                ////unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                ////eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                //if (cod_proyecto == null || cod_proyecto == "") { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                //rptFichaPadronArea report = new rptFichaPadronArea();
                //ReportPrintTool printTool = new ReportPrintTool(report);
                ////detalleLotes printTool = new detalleLotes(report);
                //report.RequestParameters = false;
                //printTool.AutoShowParametersPanel = false;
                //report.Parameters["cod_proyecto"].Value = cod_proyecto;
                ////report.BackColor = Color.FromArgb(0, 157, 150);
                SplashScreenManager.CloseForm(false);


                //report.ShowPreviewDialog();
                ////SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDenominacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");



                eLotesxProyecto obj = tvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                if (obj.cod_lote == null) { MessageBox.Show("Debe seleccionar lote.", "Ficha del lote", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                eReportes ePro = new eReportes();
                ePro = unit.Proyectos.ObtenerProyectoLotes<eReportes>("3", obj.cod_proyecto, obj.cod_lote);
                var xml = new FormatoXmlHelper("@tabla1", "00008", obj.cod_empresa);
                xml.ShowReportLindero(ePro);
                SplashScreenManager.CloseForm(false);

                ////unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                //eLotesxProyecto obj = tvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                //if (obj.cod_lote == null) { MessageBox.Show("Debe seleccionar lote.", "Ficha del lote", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                //rptFichaDenominacion report = new rptFichaDenominacion();
                //ReportPrintTool printTool = new ReportPrintTool(report);
                ////detalleLotes printTool = new detalleLotes(report);
                //report.RequestParameters = false;
                //printTool.AutoShowParametersPanel = false;
                //report.Parameters["cod_proyecto"].Value = obj.cod_proyecto;
                //report.Parameters["cod_lote"].Value = obj.cod_lote;
                ////report.BackColor = Color.FromArgb(0, 157, 150);
                //SplashScreen.Close();
                //report.ShowPreviewDialog();
                //SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreen.Close();
                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tvListaLotesProyectos_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            if (e.Item == null || e.Item.Elements.Count == 0) return;

            if ((string)tvListaLotesProyectos.GetRowCellValue(e.RowHandle, colcod_status3) == "STL00004")
            {
                e.Item.Elements[7].Image = imgVendidos50;
                e.Item.Elements[6].Image = imgContrato;
                e.Item.Elements[0].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[1].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[2].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[3].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[4].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[5].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                //e.Item.Elements[6].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[7].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                //e.Item.Elements[8].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                //e.Item.AppearanceItem.Normal.BackColor = Program.Sesion.Colores.Verde;

            }
            else if ((string)tvListaLotesProyectos.GetRowCellValue(e.RowHandle, colcod_status3) == "STL00003")
            {
                e.Item.Elements[7].Image = imgSeparados50;
                e.Item.Elements[6].Image = imgpersonM;
                e.Item.Elements[0].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[1].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[2].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[3].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[4].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[5].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[6].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[7].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                //e.Item.Elements[8].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
            }
            else
            {
                e.Item.Elements[7].Image = imgLibres50;
                e.Item.Elements[0].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[1].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[2].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[3].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[4].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);

                e.Item.Elements[5].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[6].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                e.Item.Elements[7].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
                //e.Item.Elements[8].Appearance.Normal.ForeColor = Color.FromArgb(64, 64, 64);
            }
            e.Item.Elements[5].Appearance.Normal.BackColor = Color.FromArgb(249, 178, 52);
            //e.Item.Elements[0].Appearance.Normal.ForeColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[1].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[2].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[3].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[4].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[5].Appearance.Normal.BackColor = Color.FromArgb(249, 178, 52);
            //e.Item.Elements[5].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[6].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[7].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            //e.Item.Elements[9].Appearance.Normal.BackColor = Color.FromArgb(237, 237, 237);
            e.Item.AppearanceItem.Normal.BackColor = Color.WhiteSmoke;
            //e.Item.AppearanceItem.Normal.BackColor = Color.LightGray;
            //e.Item.AppearanceItem.Normal.BackColor = Color.MintCream;

        }



        void OnNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }
        void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }
    }
}