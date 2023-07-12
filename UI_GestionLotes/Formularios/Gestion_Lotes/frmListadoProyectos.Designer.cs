
namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    partial class frmListadoProyectos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoProyectos));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle3 = new DevExpress.XtraCharts.ChartTitle();
            this.bsPrecioSumXetapa = new System.Windows.Forms.BindingSource(this.components);
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnActivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnInactivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnConfLotes = new DevExpress.XtraBars.BarButtonItem();
            this.btnOcultarFiltro = new DevExpress.XtraBars.BarButtonItem();
            this.btnSeleccionMultriple = new DevExpress.XtraBars.BarButtonItem();
            this.btnVistaResumen = new DevExpress.XtraBars.BarButtonItem();
            this.btnVistaDetallada = new DevExpress.XtraBars.BarButtonItem();
            this.btnVerPadronAreaUE = new DevExpress.XtraBars.BarButtonItem();
            this.btnVerMemoriaDescriptiva = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoEdicion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcListaProyectos = new DevExpress.XtraGrid.GridControl();
            this.bsListaProyectos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaProyectos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_proyecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_jefe_proyecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_arquitecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_precio_terreno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtPrecioTerre = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colfch_inicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_termino = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_entrega = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_pais = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_soles = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_total_suma_metros = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtTotalMetrosCuadrados = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_usuario_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_usuario_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_alcabala = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_otros_gastos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_invercion_inicial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl8 = new DevExpress.XtraLayout.LayoutControl();
            this.gcPrecios = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl11 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl7 = new DevExpress.XtraLayout.LayoutControl();
            this.gcCantidad = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl10 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.bsStatusXetapa = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.gcEtapas = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl9 = new DevExpress.XtraLayout.LayoutControl();
            this.chartEtapasxProyecto = new DevExpress.XtraCharts.ChartControl();
            this.bsEtapa = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnMaestroLotes = new DevExpress.XtraEditors.SimpleButton();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.bsPrecioSumXetapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaProyectos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaProyectos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaProyectos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecioTerre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTotalMetrosCuadrados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).BeginInit();
            this.layoutControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrecios)).BeginInit();
            this.gcPrecios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).BeginInit();
            this.layoutControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).BeginInit();
            this.layoutControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCantidad)).BeginInit();
            this.gcCantidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).BeginInit();
            this.layoutControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStatusXetapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEtapas)).BeginInit();
            this.gcEtapas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).BeginInit();
            this.layoutControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEtapasxProyecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEtapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsPrecioSumXetapa
            // 
            this.bsPrecioSumXetapa.DataSource = typeof(BE_GestionLotes.eLotesxProyecto);
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ItemPanelStyle = DevExpress.XtraBars.Ribbon.RibbonItemPanelStyle.Classic;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevo,
            this.btnActivar,
            this.btnInactivar,
            this.btnEliminar,
            this.btnExportarExcel,
            this.btnImprimir,
            this.barStaticItem1,
            this.btnConfLotes,
            this.btnOcultarFiltro,
            this.btnSeleccionMultriple,
            this.btnVistaResumen,
            this.btnVistaDetallada,
            this.btnVerPadronAreaUE,
            this.btnVerMemoriaDescriptiva});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 18;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1398, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 1;
            this.btnNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.Image")));
            this.btnNuevo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.LargeImage")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnActivar
            // 
            this.btnActivar.Caption = "Activar";
            this.btnActivar.Enabled = false;
            this.btnActivar.Id = 3;
            this.btnActivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivar.ImageOptions.Image")));
            this.btnActivar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnActivar.ImageOptions.LargeImage")));
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActivar_ItemClick);
            // 
            // btnInactivar
            // 
            this.btnInactivar.Caption = "Inactivar";
            this.btnInactivar.Enabled = false;
            this.btnInactivar.Id = 4;
            this.btnInactivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInactivar.ImageOptions.Image")));
            this.btnInactivar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnInactivar.ImageOptions.LargeImage")));
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInactivar_ItemClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "Eliminar";
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Id = 5;
            this.btnEliminar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.ImageOptions.Image")));
            this.btnEliminar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.ImageOptions.LargeImage")));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar a Excel";
            this.btnExportarExcel.Id = 6;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.LargeImage")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 8;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.LargeImage")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado";
            this.barStaticItem1.CausesValidation = true;
            this.barStaticItem1.Id = 9;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnConfLotes
            // 
            this.btnConfLotes.Caption = "Configuración de Lotes";
            this.btnConfLotes.Id = 10;
            this.btnConfLotes.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfLotes.ImageOptions.SvgImage")));
            this.btnConfLotes.Name = "btnConfLotes";
            this.btnConfLotes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnConfLotes_ItemClick);
            // 
            // btnOcultarFiltro
            // 
            this.btnOcultarFiltro.Caption = "Ocultar Filtro";
            this.btnOcultarFiltro.Id = 11;
            this.btnOcultarFiltro.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOcultarFiltro.ImageOptions.Image")));
            this.btnOcultarFiltro.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOcultarFiltro.ImageOptions.LargeImage")));
            this.btnOcultarFiltro.Name = "btnOcultarFiltro";
            this.btnOcultarFiltro.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOcultarFiltro_ItemClick);
            // 
            // btnSeleccionMultriple
            // 
            this.btnSeleccionMultriple.Caption = "Selección Multiple";
            this.btnSeleccionMultriple.Enabled = false;
            this.btnSeleccionMultriple.Id = 12;
            this.btnSeleccionMultriple.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSeleccionMultriple.ImageOptions.Image")));
            this.btnSeleccionMultriple.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSeleccionMultriple.ImageOptions.LargeImage")));
            this.btnSeleccionMultriple.Name = "btnSeleccionMultriple";
            // 
            // btnVistaResumen
            // 
            this.btnVistaResumen.Caption = "Vista Resumen";
            this.btnVistaResumen.Enabled = false;
            this.btnVistaResumen.Id = 13;
            this.btnVistaResumen.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnVistaResumen.ImageOptions.Image")));
            this.btnVistaResumen.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnVistaResumen.ImageOptions.LargeImage")));
            this.btnVistaResumen.Name = "btnVistaResumen";
            // 
            // btnVistaDetallada
            // 
            this.btnVistaDetallada.Caption = "Vista Detallada";
            this.btnVistaDetallada.Enabled = false;
            this.btnVistaDetallada.Id = 14;
            this.btnVistaDetallada.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnVistaDetallada.ImageOptions.Image")));
            this.btnVistaDetallada.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnVistaDetallada.ImageOptions.LargeImage")));
            this.btnVistaDetallada.Name = "btnVistaDetallada";
            // 
            // btnVerPadronAreaUE
            // 
            this.btnVerPadronAreaUE.Caption = "Ver Padrón Áreas UE";
            this.btnVerPadronAreaUE.Id = 16;
            this.btnVerPadronAreaUE.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnVerPadronAreaUE.ImageOptions.Image")));
            this.btnVerPadronAreaUE.Name = "btnVerPadronAreaUE";
            this.btnVerPadronAreaUE.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem1.Text = "Ver Padrón de Áreas de Uso Exclusivo";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.btnVerPadronAreaUE.SuperTip = superToolTip1;
            this.btnVerPadronAreaUE.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnVerPadronAreaUE_ItemClick);
            // 
            // btnVerMemoriaDescriptiva
            // 
            this.btnVerMemoriaDescriptiva.Caption = "Ver Memoria Descriptiva";
            this.btnVerMemoriaDescriptiva.Id = 17;
            this.btnVerMemoriaDescriptiva.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnVerMemoriaDescriptiva.ImageOptions.Image")));
            this.btnVerMemoriaDescriptiva.Name = "btnVerMemoriaDescriptiva";
            this.btnVerMemoriaDescriptiva.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnVerMemoriaDescriptiva.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnVerMemoriaDescriptiva.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnVerMemoriaDescriptiva_ItemClick_1);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoEdicion,
            this.ribbonPageGroup3,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones de Proyectos";
            // 
            // grupoEdicion
            // 
            this.grupoEdicion.ItemLinks.Add(this.btnNuevo);
            this.grupoEdicion.ItemLinks.Add(this.btnActivar);
            this.grupoEdicion.ItemLinks.Add(this.btnInactivar);
            this.grupoEdicion.ItemLinks.Add(this.btnEliminar);
            this.grupoEdicion.Name = "grupoEdicion";
            this.grupoEdicion.Text = "Edición";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnOcultarFiltro);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnSeleccionMultriple);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnVistaDetallada);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Personalizar Vistas";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnExportarExcel);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnImprimir);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnVerMemoriaDescriptiva);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnVerPadronAreaUE);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Reportes";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 608);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1398, 24);
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            // 
            // gcListaProyectos
            // 
            this.gcListaProyectos.DataSource = this.bsListaProyectos;
            this.gcListaProyectos.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaProyectos.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaProyectos.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaProyectos.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaProyectos.Location = new System.Drawing.Point(66, 50);
            this.gcListaProyectos.MainView = this.gvListaProyectos;
            this.gcListaProyectos.MenuManager = this.ribbon;
            this.gcListaProyectos.Name = "gcListaProyectos";
            this.gcListaProyectos.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtPrecioTerre,
            this.rtxtTotalMetrosCuadrados});
            this.gcListaProyectos.Size = new System.Drawing.Size(1320, 162);
            this.gcListaProyectos.TabIndex = 0;
            this.gcListaProyectos.UseEmbeddedNavigator = true;
            this.gcListaProyectos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaProyectos});
            // 
            // bsListaProyectos
            // 
            this.bsListaProyectos.DataSource = typeof(BE_GestionLotes.eProyecto);
            // 
            // gvListaProyectos
            // 
            this.gvListaProyectos.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvListaProyectos.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListaProyectos.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaProyectos.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListaProyectos.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gvListaProyectos.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvListaProyectos.Appearance.FooterPanel.Options.UseFont = true;
            this.gvListaProyectos.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvListaProyectos.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvListaProyectos.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gvListaProyectos.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaProyectos.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaProyectos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaProyectos.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListaProyectos.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaProyectos.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListaProyectos.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaProyectos.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListaProyectos.ColumnPanelRowHeight = 35;
            this.gvListaProyectos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_proyecto,
            this.coldsc_nombre,
            this.coldsc_descripcion,
            this.coldsc_jefe_proyecto,
            this.coldsc_arquitecto,
            this.colimp_precio_terreno,
            this.colfch_inicio,
            this.colfch_termino,
            this.colfch_entrega,
            this.coldsc_pais,
            this.coldsc_soles,
            this.colflg_activo,
            this.colnum_total_suma_metros,
            this.colfch_registro,
            this.colcod_usuario_registro,
            this.colfch_cambio,
            this.colcod_usuario_cambio,
            this.colimp_alcabala,
            this.colimp_otros_gastos,
            this.colimp_invercion_inicial});
            this.gvListaProyectos.GridControl = this.gcListaProyectos;
            this.gvListaProyectos.Name = "gvListaProyectos";
            this.gvListaProyectos.OptionsBehavior.Editable = false;
            this.gvListaProyectos.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaProyectos.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaProyectos.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaProyectos.OptionsView.RowAutoHeight = true;
            this.gvListaProyectos.OptionsView.ShowFooter = true;
            this.gvListaProyectos.OptionsView.ShowGroupPanel = false;
            this.gvListaProyectos.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListaProyectos_RowClick);
            this.gvListaProyectos.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaProyectos_CustomDrawColumnHeader);
            this.gvListaProyectos.CustomDrawFooter += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gvListaProyectos_CustomDrawFooter);
            this.gvListaProyectos.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListaProyectos_RowStyle);
            this.gvListaProyectos.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvListaProyectos_FocusedRowChanged);
            // 
            // colcod_proyecto
            // 
            this.colcod_proyecto.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_proyecto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_proyecto.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_proyecto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_proyecto.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_proyecto.Caption = "Código";
            this.colcod_proyecto.FieldName = "cod_proyecto";
            this.colcod_proyecto.Name = "colcod_proyecto";
            this.colcod_proyecto.Visible = true;
            this.colcod_proyecto.VisibleIndex = 0;
            this.colcod_proyecto.Width = 78;
            // 
            // coldsc_nombre
            // 
            this.coldsc_nombre.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_nombre.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_nombre.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_nombre.Caption = "Nombre";
            this.coldsc_nombre.FieldName = "dsc_nombre";
            this.coldsc_nombre.Name = "coldsc_nombre";
            this.coldsc_nombre.Visible = true;
            this.coldsc_nombre.VisibleIndex = 1;
            this.coldsc_nombre.Width = 145;
            // 
            // coldsc_descripcion
            // 
            this.coldsc_descripcion.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_descripcion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_descripcion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_descripcion.Caption = "Descripción";
            this.coldsc_descripcion.FieldName = "dsc_descripcion";
            this.coldsc_descripcion.Name = "coldsc_descripcion";
            this.coldsc_descripcion.OptionsColumn.AllowEdit = false;
            this.coldsc_descripcion.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.coldsc_descripcion.Visible = true;
            this.coldsc_descripcion.VisibleIndex = 2;
            this.coldsc_descripcion.Width = 243;
            // 
            // coldsc_jefe_proyecto
            // 
            this.coldsc_jefe_proyecto.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_jefe_proyecto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_jefe_proyecto.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_jefe_proyecto.Caption = "Jefe de Proyecto";
            this.coldsc_jefe_proyecto.FieldName = "dsc_jefe_proyecto";
            this.coldsc_jefe_proyecto.Name = "coldsc_jefe_proyecto";
            this.coldsc_jefe_proyecto.OptionsColumn.AllowEdit = false;
            this.coldsc_jefe_proyecto.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.coldsc_jefe_proyecto.Visible = true;
            this.coldsc_jefe_proyecto.VisibleIndex = 3;
            this.coldsc_jefe_proyecto.Width = 158;
            // 
            // coldsc_arquitecto
            // 
            this.coldsc_arquitecto.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_arquitecto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_arquitecto.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_arquitecto.Caption = "Arquitecto";
            this.coldsc_arquitecto.FieldName = "dsc_arquitecto";
            this.coldsc_arquitecto.Name = "coldsc_arquitecto";
            this.coldsc_arquitecto.OptionsColumn.AllowEdit = false;
            this.coldsc_arquitecto.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.coldsc_arquitecto.Visible = true;
            this.coldsc_arquitecto.VisibleIndex = 4;
            this.coldsc_arquitecto.Width = 154;
            // 
            // colimp_precio_terreno
            // 
            this.colimp_precio_terreno.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_precio_terreno.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_precio_terreno.AppearanceHeader.Options.UseTextOptions = true;
            this.colimp_precio_terreno.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_precio_terreno.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colimp_precio_terreno.Caption = "Valor Compra Terreno";
            this.colimp_precio_terreno.ColumnEdit = this.txtPrecioTerre;
            this.colimp_precio_terreno.FieldName = "imp_precio_terreno";
            this.colimp_precio_terreno.Name = "colimp_precio_terreno";
            this.colimp_precio_terreno.OptionsColumn.AllowEdit = false;
            this.colimp_precio_terreno.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_precio_terreno", "{0:#,#.00}")});
            this.colimp_precio_terreno.Visible = true;
            this.colimp_precio_terreno.VisibleIndex = 9;
            this.colimp_precio_terreno.Width = 101;
            // 
            // txtPrecioTerre
            // 
            this.txtPrecioTerre.AutoHeight = false;
            this.txtPrecioTerre.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtPrecioTerre.MaskSettings.Set("mask", "n2");
            this.txtPrecioTerre.Name = "txtPrecioTerre";
            this.txtPrecioTerre.UseMaskAsDisplayFormat = true;
            // 
            // colfch_inicio
            // 
            this.colfch_inicio.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_inicio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_inicio.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_inicio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_inicio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_inicio.Caption = "Fecha Inicio";
            this.colfch_inicio.FieldName = "fch_inicio";
            this.colfch_inicio.Name = "colfch_inicio";
            this.colfch_inicio.Visible = true;
            this.colfch_inicio.VisibleIndex = 5;
            this.colfch_inicio.Width = 102;
            // 
            // colfch_termino
            // 
            this.colfch_termino.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_termino.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_termino.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_termino.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_termino.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_termino.Caption = "Fecha Término";
            this.colfch_termino.FieldName = "fch_termino";
            this.colfch_termino.Name = "colfch_termino";
            this.colfch_termino.Visible = true;
            this.colfch_termino.VisibleIndex = 6;
            this.colfch_termino.Width = 108;
            // 
            // colfch_entrega
            // 
            this.colfch_entrega.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_entrega.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_entrega.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_entrega.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_entrega.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_entrega.Caption = "Fecha Entrega";
            this.colfch_entrega.FieldName = "fch_entrega";
            this.colfch_entrega.Name = "colfch_entrega";
            this.colfch_entrega.Visible = true;
            this.colfch_entrega.VisibleIndex = 7;
            this.colfch_entrega.Width = 102;
            // 
            // coldsc_pais
            // 
            this.coldsc_pais.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_pais.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_pais.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_pais.Caption = "País";
            this.coldsc_pais.FieldName = "dsc_pais";
            this.coldsc_pais.Name = "coldsc_pais";
            // 
            // coldsc_soles
            // 
            this.coldsc_soles.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_soles.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_soles.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_soles.Caption = "Moneda Principal";
            this.coldsc_soles.FieldName = "dsc_soles";
            this.coldsc_soles.Name = "coldsc_soles";
            // 
            // colflg_activo
            // 
            this.colflg_activo.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_activo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activo.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_activo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_activo.Caption = "Estado";
            this.colflg_activo.FieldName = "flg_activo";
            this.colflg_activo.Name = "colflg_activo";
            // 
            // colnum_total_suma_metros
            // 
            this.colnum_total_suma_metros.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_total_suma_metros.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colnum_total_suma_metros.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_total_suma_metros.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_total_suma_metros.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_total_suma_metros.Caption = "Total m²";
            this.colnum_total_suma_metros.ColumnEdit = this.rtxtTotalMetrosCuadrados;
            this.colnum_total_suma_metros.FieldName = "num_total_suma_metros";
            this.colnum_total_suma_metros.Name = "colnum_total_suma_metros";
            this.colnum_total_suma_metros.Visible = true;
            this.colnum_total_suma_metros.VisibleIndex = 8;
            this.colnum_total_suma_metros.Width = 104;
            // 
            // rtxtTotalMetrosCuadrados
            // 
            this.rtxtTotalMetrosCuadrados.AutoHeight = false;
            this.rtxtTotalMetrosCuadrados.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtTotalMetrosCuadrados.MaskSettings.Set("mask", "n2");
            this.rtxtTotalMetrosCuadrados.Name = "rtxtTotalMetrosCuadrados";
            this.rtxtTotalMetrosCuadrados.UseMaskAsDisplayFormat = true;
            // 
            // colfch_registro
            // 
            this.colfch_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_registro.Caption = "Fecha Creación";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.OptionsColumn.FixedWidth = true;
            // 
            // colcod_usuario_registro
            // 
            this.colcod_usuario_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_usuario_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_usuario_registro.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_usuario_registro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_usuario_registro.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_usuario_registro.Caption = "Usuario Creación";
            this.colcod_usuario_registro.FieldName = "cod_usuario_registro";
            this.colcod_usuario_registro.Name = "colcod_usuario_registro";
            this.colcod_usuario_registro.OptionsColumn.FixedWidth = true;
            // 
            // colfch_cambio
            // 
            this.colfch_cambio.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cambio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_cambio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_cambio.Caption = "Fecha Modificación";
            this.colfch_cambio.FieldName = "fch_cambio";
            this.colfch_cambio.Name = "colfch_cambio";
            this.colfch_cambio.OptionsColumn.FixedWidth = true;
            // 
            // colcod_usuario_cambio
            // 
            this.colcod_usuario_cambio.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_usuario_cambio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_usuario_cambio.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_usuario_cambio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_usuario_cambio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_usuario_cambio.Caption = "Usuario Modificación";
            this.colcod_usuario_cambio.FieldName = "cod_usuario_cambio";
            this.colcod_usuario_cambio.Name = "colcod_usuario_cambio";
            this.colcod_usuario_cambio.OptionsColumn.FixedWidth = true;
            // 
            // colimp_alcabala
            // 
            this.colimp_alcabala.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_alcabala.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_alcabala.AppearanceHeader.Options.UseTextOptions = true;
            this.colimp_alcabala.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_alcabala.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colimp_alcabala.Caption = "Alcabala";
            this.colimp_alcabala.ColumnEdit = this.txtPrecioTerre;
            this.colimp_alcabala.FieldName = "imp_alcabala";
            this.colimp_alcabala.Name = "colimp_alcabala";
            this.colimp_alcabala.OptionsColumn.AllowEdit = false;
            // 
            // colimp_otros_gastos
            // 
            this.colimp_otros_gastos.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_otros_gastos.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_otros_gastos.AppearanceHeader.Options.UseTextOptions = true;
            this.colimp_otros_gastos.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_otros_gastos.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colimp_otros_gastos.Caption = "Otros Gastos";
            this.colimp_otros_gastos.ColumnEdit = this.txtPrecioTerre;
            this.colimp_otros_gastos.FieldName = "imp_otros_gastos";
            this.colimp_otros_gastos.Name = "colimp_otros_gastos";
            this.colimp_otros_gastos.OptionsColumn.AllowEdit = false;
            // 
            // colimp_invercion_inicial
            // 
            this.colimp_invercion_inicial.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_invercion_inicial.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_invercion_inicial.AppearanceHeader.Options.UseTextOptions = true;
            this.colimp_invercion_inicial.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_invercion_inicial.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colimp_invercion_inicial.Caption = "Invercion Inicial";
            this.colimp_invercion_inicial.ColumnEdit = this.txtPrecioTerre;
            this.colimp_invercion_inicial.FieldName = "imp_invercion_inicial";
            this.colimp_invercion_inicial.Name = "colimp_invercion_inicial";
            this.colimp_invercion_inicial.OptionsColumn.AllowEdit = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListaProyectos;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl8);
            this.layoutControl1.Controls.Add(this.layoutControl7);
            this.layoutControl1.Controls.Add(this.layoutControl6);
            this.layoutControl1.Controls.Add(this.btnMaestroLotes);
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.navBarControl1);
            this.layoutControl1.Controls.Add(this.gcListaProyectos);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1398, 450);
            this.layoutControl1.TabIndex = 8;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl8
            // 
            this.layoutControl8.Controls.Add(this.gcPrecios);
            this.layoutControl8.Location = new System.Drawing.Point(996, 218);
            this.layoutControl8.Name = "layoutControl8";
            this.layoutControl8.Root = this.layoutControlGroup7;
            this.layoutControl8.Size = new System.Drawing.Size(390, 220);
            this.layoutControl8.TabIndex = 11;
            this.layoutControl8.Text = "layoutControl8";
            // 
            // gcPrecios
            // 
            this.gcPrecios.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gcPrecios.AppearanceCaption.Options.UseFont = true;
            this.gcPrecios.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gcPrecios.Controls.Add(this.layoutControl11);
            this.gcPrecios.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.gcPrecios.Location = new System.Drawing.Point(2, 2);
            this.gcPrecios.Name = "gcPrecios";
            this.gcPrecios.Size = new System.Drawing.Size(386, 216);
            this.gcPrecios.TabIndex = 4;
            this.gcPrecios.Text = "TOTAL PRECIO DE VENTA";
            // 
            // layoutControl11
            // 
            this.layoutControl11.Controls.Add(this.chartControl2);
            this.layoutControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl11.Location = new System.Drawing.Point(2, 23);
            this.layoutControl11.Name = "layoutControl11";
            this.layoutControl11.Root = this.layoutControlGroup10;
            this.layoutControl11.Size = new System.Drawing.Size(382, 191);
            this.layoutControl11.TabIndex = 0;
            this.layoutControl11.Text = "layoutControl11";
            // 
            // chartControl2
            // 
            this.chartControl2.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnLoad;
            this.chartControl2.DataSource = this.bsPrecioSumXetapa;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.GridLines.Visible = false;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.EnableAxisYZooming = true;
            this.chartControl2.Diagram = xyDiagram1;
            this.chartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl2.Location = new System.Drawing.Point(2, 2);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.PaletteBaseColorNumber = 5;
            series1.ArgumentDataMember = "dsc_Nombre";
            sideBySideBarSeriesLabel1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(72)))), ((int)(((byte)(6)))));
            sideBySideBarSeriesLabel1.Border.Thickness = 3;
            sideBySideBarSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            sideBySideBarSeriesLabel1.DXFont = new DevExpress.Drawing.DXFont("Microsoft Sans Serif", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
            sideBySideBarSeriesLabel1.TextPattern = "{HINT}";
            series1.Label = sideBySideBarSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.Name = "VentaPrecios";
            series1.ToolTipHintDataMember = "imp_sum_precio_total_moneda";
            series1.ValueDataMembersSerializable = "imp_sum_precio_total";
            sideBySideBarSeriesView1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(108)))), ((int)(((byte)(9)))));
            sideBySideBarSeriesView1.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(150)))), ((int)(((byte)(70)))));
            sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Gradient;
            series1.View = sideBySideBarSeriesView1;
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl2.Size = new System.Drawing.Size(378, 187);
            this.chartControl2.TabIndex = 4;
            chartTitle1.Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Bottom;
            chartTitle1.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 14.25F, DevExpress.Drawing.DXFontStyle.Bold);
            chartTitle1.Text = "TOTAL PRECIOS VENTAS";
            chartTitle1.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl2.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup10.GroupBordersVisible = false;
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10});
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup10.Size = new System.Drawing.Size(382, 191);
            this.layoutControlGroup10.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.chartControl2;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(382, 191);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup7.GroupBordersVisible = false;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem17});
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(390, 220);
            this.layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.gcPrecios;
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(390, 220);
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControl7
            // 
            this.layoutControl7.Controls.Add(this.gcCantidad);
            this.layoutControl7.Location = new System.Drawing.Point(557, 218);
            this.layoutControl7.Name = "layoutControl7";
            this.layoutControl7.Root = this.layoutControlGroup6;
            this.layoutControl7.Size = new System.Drawing.Size(425, 220);
            this.layoutControl7.TabIndex = 10;
            this.layoutControl7.Text = "layoutControl7";
            // 
            // gcCantidad
            // 
            this.gcCantidad.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gcCantidad.AppearanceCaption.Options.UseFont = true;
            this.gcCantidad.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gcCantidad.Controls.Add(this.layoutControl10);
            this.gcCantidad.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.gcCantidad.Location = new System.Drawing.Point(2, 2);
            this.gcCantidad.Name = "gcCantidad";
            this.gcCantidad.Size = new System.Drawing.Size(421, 216);
            this.gcCantidad.TabIndex = 4;
            this.gcCantidad.Text = "CANTIDAD DE LOTES";
            // 
            // layoutControl10
            // 
            this.layoutControl10.Controls.Add(this.chartControl1);
            this.layoutControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl10.Location = new System.Drawing.Point(2, 23);
            this.layoutControl10.Name = "layoutControl10";
            this.layoutControl10.Root = this.layoutControlGroup9;
            this.layoutControl10.Size = new System.Drawing.Size(417, 191);
            this.layoutControl10.TabIndex = 0;
            this.layoutControl10.Text = "layoutControl10";
            // 
            // chartControl1
            // 
            this.chartControl1.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnLoad;
            this.chartControl1.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.DataSource = this.bsStatusXetapa;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.GridLines.Visible = false;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.Rotated = true;
            this.chartControl1.Diagram = xyDiagram2;
            this.chartControl1.Legend.Title.Text = "Grupo";
            this.chartControl1.Legend.Title.Visible = true;
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(2, 2);
            this.chartControl1.Name = "chartControl1";
            series2.ArgumentDataMember = "dsc_Nombre";
            sideBySideBarSeriesLabel2.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(97)))), ((int)(((byte)(40)))));
            sideBySideBarSeriesLabel2.Border.Thickness = 3;
            sideBySideBarSeriesLabel2.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            sideBySideBarSeriesLabel2.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 8.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            sideBySideBarSeriesLabel2.TextPattern = "Cantidad  :  {V}  ({HINT})";
            series2.Label = sideBySideBarSeriesLabel2;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Status";
            series2.ToolTipHintDataMember = "prc_status";
            series2.ValueDataMembersSerializable = "num_filas";
            sideBySideBarSeriesView2.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(97)))), ((int)(((byte)(40)))));
            sideBySideBarSeriesView2.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            sideBySideBarSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(146)))), ((int)(((byte)(60)))));
            series2.View = sideBySideBarSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl1.SeriesTemplate.ArgumentDataMember = "dsc_Nombre";
            this.chartControl1.SeriesTemplate.ToolTipHintDataMember = "prc_status";
            this.chartControl1.SeriesTemplate.ValueDataMembersSerializable = "num_filas";
            this.chartControl1.Size = new System.Drawing.Size(413, 187);
            this.chartControl1.TabIndex = 4;
            chartTitle2.Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Bottom;
            chartTitle2.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 14.25F, DevExpress.Drawing.DXFontStyle.Bold);
            chartTitle2.Text = "CANTIDAD";
            chartTitle2.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
            // 
            // bsStatusXetapa
            // 
            this.bsStatusXetapa.DataSource = typeof(BE_GestionLotes.eLotesxProyecto);
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup9.GroupBordersVisible = false;
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Size = new System.Drawing.Size(417, 191);
            this.layoutControlGroup9.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.chartControl1;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(417, 191);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.GroupBordersVisible = false;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem16});
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(425, 220);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.gcCantidad;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(425, 220);
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.gcEtapas);
            this.layoutControl6.Location = new System.Drawing.Point(66, 218);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.Root = this.layoutControlGroup5;
            this.layoutControl6.Size = new System.Drawing.Size(477, 220);
            this.layoutControl6.TabIndex = 9;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // gcEtapas
            // 
            this.gcEtapas.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gcEtapas.AppearanceCaption.Options.UseFont = true;
            this.gcEtapas.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gcEtapas.Controls.Add(this.layoutControl9);
            this.gcEtapas.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.gcEtapas.Location = new System.Drawing.Point(2, 2);
            this.gcEtapas.Name = "gcEtapas";
            this.gcEtapas.Size = new System.Drawing.Size(473, 216);
            this.gcEtapas.TabIndex = 4;
            this.gcEtapas.Text = " ETAPAS  m²";
            // 
            // layoutControl9
            // 
            this.layoutControl9.Controls.Add(this.chartEtapasxProyecto);
            this.layoutControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl9.Location = new System.Drawing.Point(2, 23);
            this.layoutControl9.Name = "layoutControl9";
            this.layoutControl9.Root = this.layoutControlGroup8;
            this.layoutControl9.Size = new System.Drawing.Size(469, 191);
            this.layoutControl9.TabIndex = 0;
            this.layoutControl9.Text = "layoutControl9";
            // 
            // chartEtapasxProyecto
            // 
            this.chartEtapasxProyecto.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnLoad;
            this.chartEtapasxProyecto.DataSource = this.bsEtapa;
            this.chartEtapasxProyecto.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartEtapasxProyecto.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
            this.chartEtapasxProyecto.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartEtapasxProyecto.Legend.DXFont = new DevExpress.Drawing.DXFont("Arial", 10F);
            this.chartEtapasxProyecto.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartEtapasxProyecto.Location = new System.Drawing.Point(2, 2);
            this.chartEtapasxProyecto.Name = "chartEtapasxProyecto";
            this.chartEtapasxProyecto.PaletteName = "Green";
            this.chartEtapasxProyecto.SelectionMode = DevExpress.XtraCharts.ElementSelectionMode.Single;
            this.chartEtapasxProyecto.SeriesSelectionMode = DevExpress.XtraCharts.SeriesSelectionMode.Argument;
            series3.ArgumentDataMember = "dsc_descripcion";
            pieSeriesLabel1.Border.Thickness = 3;
            pieSeriesLabel1.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 8.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            pieSeriesLabel1.TextPattern = "{A}  {VP:0%}";
            series3.Label = pieSeriesLabel1;
            series3.Name = "Serie1";
            series3.ValueDataMembersSerializable = "num_total_lotizacion";
            series3.View = pieSeriesView1;
            this.chartEtapasxProyecto.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.chartEtapasxProyecto.Size = new System.Drawing.Size(465, 187);
            this.chartEtapasxProyecto.TabIndex = 4;
            chartTitle3.Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Bottom;
            chartTitle3.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 14.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            chartTitle3.Text = " ETAPAS  m²";
            chartTitle3.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartEtapasxProyecto.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle3});
            this.chartEtapasxProyecto.SelectedItemsChanged += new DevExpress.XtraCharts.SelectedItemsChangedEventHandler(this.chartEtapasxProyecto_SelectedItemsChanged);
            // 
            // bsEtapa
            // 
            this.bsEtapa.DataSource = typeof(BE_GestionLotes.eProyecto_Etapa);
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup8.GroupBordersVisible = false;
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(469, 191);
            this.layoutControlGroup8.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.chartEtapasxProyecto;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(469, 191);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem15});
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(477, 220);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.gcEtapas;
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(477, 220);
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextVisible = false;
            // 
            // btnMaestroLotes
            // 
            this.btnMaestroLotes.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnMaestroLotes.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnMaestroLotes.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaestroLotes.Appearance.Options.UseBackColor = true;
            this.btnMaestroLotes.Appearance.Options.UseBorderColor = true;
            this.btnMaestroLotes.Appearance.Options.UseFont = true;
            this.btnMaestroLotes.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMaestroLotes.ImageOptions.Image")));
            this.btnMaestroLotes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMaestroLotes.Location = new System.Drawing.Point(1167, 12);
            this.btnMaestroLotes.Name = "btnMaestroLotes";
            this.btnMaestroLotes.Size = new System.Drawing.Size(219, 34);
            this.btnMaestroLotes.StyleController = this.layoutControl1;
            this.btnMaestroLotes.TabIndex = 8;
            this.btnMaestroLotes.Text = "Ver Maestro de Lotes";
            this.btnMaestroLotes.Click += new System.EventHandler(this.btnMaestroLotes_Click);
            // 
            // picTitulo
            // 
            this.picTitulo.CausesValidation = false;
            this.picTitulo.Location = new System.Drawing.Point(66, 12);
            this.picTitulo.MenuManager = this.ribbon;
            this.picTitulo.Name = "picTitulo";
            this.picTitulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitulo.Properties.Appearance.Options.UseBackColor = true;
            this.picTitulo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitulo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitulo.Size = new System.Drawing.Size(115, 34);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 6;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(185, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(487, 34);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
            // 
            // navBarControl1
            // 
            this.navBarControl1.BackColor = System.Drawing.Color.Transparent;
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navBarControl1.Location = new System.Drawing.Point(12, 12);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.CollapsedWidth = 50;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 200;
            this.navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            this.navBarControl1.OptionsNavPane.ShowGroupImageInHeader = true;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(50, 426);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.StandardSkinNavigationPaneViewInfoRegistrator("DevExpress Style");
            this.navBarControl1.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
            this.navBarControl1.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControl1_ActiveGroupChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.splitterItem1,
            this.splitterItem2,
            this.simpleSeparator1,
            this.simpleSeparator2,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1398, 450);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcListaProyectos;
            this.layoutControlItem2.Location = new System.Drawing.Point(54, 38);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1324, 166);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.navBarControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(54, 430);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblTitulo;
            this.layoutControlItem4.Location = new System.Drawing.Point(173, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 38);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(222, 38);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(491, 38);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.picTitulo;
            this.layoutControlItem5.Location = new System.Drawing.Point(54, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(119, 38);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnMaestroLotes;
            this.layoutControlItem11.Location = new System.Drawing.Point(1155, 0);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(0, 38);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(188, 38);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(223, 38);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.layoutControl6;
            this.layoutControlItem12.Location = new System.Drawing.Point(54, 206);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(481, 224);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.layoutControl7;
            this.layoutControlItem13.Location = new System.Drawing.Point(545, 206);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(429, 224);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.layoutControl8;
            this.layoutControlItem14.Location = new System.Drawing.Point(984, 206);
            this.layoutControlItem14.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(394, 224);
            this.layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(535, 206);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(10, 224);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(974, 206);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(10, 224);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(54, 205);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(1324, 1);
            // 
            // simpleSeparator2
            // 
            this.simpleSeparator2.AllowHotTrack = false;
            this.simpleSeparator2.Location = new System.Drawing.Point(54, 204);
            this.simpleSeparator2.Name = "simpleSeparator2";
            this.simpleSeparator2.Size = new System.Drawing.Size(1324, 1);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(664, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(491, 38);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmListadoProyectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1398, 632);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListadoProyectos";
            this.Ribbon = this.ribbon;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado de Proyectos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoProyectos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoProyectos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsPrecioSumXetapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaProyectos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaProyectos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaProyectos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecioTerre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTotalMetrosCuadrados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).EndInit();
            this.layoutControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPrecios)).EndInit();
            this.gcPrecios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).EndInit();
            this.layoutControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).EndInit();
            this.layoutControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCantidad)).EndInit();
            this.gcCantidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).EndInit();
            this.layoutControl10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStatusXetapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcEtapas)).EndInit();
            this.gcEtapas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).EndInit();
            this.layoutControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEtapasxProyecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEtapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoEdicion;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.GridControl gcListaProyectos;
        public DevExpress.XtraGrid.Views.Grid.GridView gvListaProyectos;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proyecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombre;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_jefe_proyecto;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activo;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_usuario_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_usuario_cambio;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnActivar;
        private DevExpress.XtraBars.BarButtonItem btnInactivar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_inicio;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_termino;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_entrega;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_arquitecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_pais;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_soles;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_precio_terreno;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtPrecioTerre;
        private System.Windows.Forms.BindingSource bsListaProyectos;
        private DevExpress.XtraCharts.ChartControl chartEtapasxProyecto;
        private System.Windows.Forms.BindingSource bsEtapa;
        private DevExpress.XtraBars.BarButtonItem btnConfLotes;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_total_suma_metros;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtTotalMetrosCuadrados;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.BindingSource bsStatusXetapa;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem btnOcultarFiltro;
        private DevExpress.XtraBars.BarButtonItem btnSeleccionMultriple;
        private DevExpress.XtraBars.BarButtonItem btnVistaResumen;
        private DevExpress.XtraBars.BarButtonItem btnVistaDetallada;
        private System.Windows.Forms.BindingSource bsPrecioSumXetapa;
        private DevExpress.XtraCharts.ChartControl chartControl2;
        private DevExpress.XtraEditors.SimpleButton btnMaestroLotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControl layoutControl8;
        private DevExpress.XtraEditors.GroupControl gcPrecios;
        private DevExpress.XtraLayout.LayoutControl layoutControl11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControl layoutControl7;
        private DevExpress.XtraEditors.GroupControl gcCantidad;
        private DevExpress.XtraLayout.LayoutControl layoutControl10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraEditors.GroupControl gcEtapas;
        private DevExpress.XtraLayout.LayoutControl layoutControl9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraBars.BarButtonItem btnVerPadronAreaUE;
        private DevExpress.XtraBars.BarButtonItem btnVerMemoriaDescriptiva;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_alcabala;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_otros_gastos;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_invercion_inicial;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}