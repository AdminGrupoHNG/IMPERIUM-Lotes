
using DevExpress.XtraGrid.Columns;

namespace UI_GestionLotes.Formularios.Operaciones
{
    partial class frmListarClienteSeparaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarClienteSeparaciones));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.btnVincularCliente = new DevExpress.XtraEditors.SimpleButton();
            this.gcListaAsigProspecto = new DevExpress.XtraGrid.GridControl();
            this.bsListaAsigProspecto = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaAsigProspecto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_proyecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_prospecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_persona = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_telefono_movil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_origen_prospecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_origen_prospecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_ejecutivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_ejecutivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_num_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_lotes_asig = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_tipo_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_apelido_paterno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_cliente2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_estado_civil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_fec_nac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_profesion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRatingControl1 = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.lblTituloProspecto = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.btnNuevaSeparacion = new DevExpress.XtraEditors.SimpleButton();
            this.gcListarCotizacion = new DevExpress.XtraGrid.GridControl();
            this.bsListadoCronograma = new System.Windows.Forms.BindingSource(this.components);
            this.gvListarCotizacion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_proforma = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_proforma = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_estado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmonto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.coldsc_parametro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_separacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblTituloProformas = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTituloCliente = new DevExpress.XtraEditors.LabelControl();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.gcListaClientes = new DevExpress.XtraGrid.GridControl();
            this.bsListaClientes = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaClientes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_asesor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_ocupacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colctd_Deuda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLotesAdquiridos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_cliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_apellido_paterno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_apellido_materno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_razon_comercial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_cliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_calificacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_categoria = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_contacto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_prospectoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_telefono_1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_telefono_2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cadena_direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_pais = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_distrito = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_provincia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_departamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_sexo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_juridico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colvalorRating = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rRatingCalificacion = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.coldsc_empresas_vinculadas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rmmTexto = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.rgLotes = new DevExpress.XtraEditors.RadioGroup();
            this.gcListaLotesSeparados = new DevExpress.XtraGrid.GridControl();
            this.bsListaLotesSep = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaLotesSeparados = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_lote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_area_uex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporteValor = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colnum_area_uco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_precio_final = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaAsigProspecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaAsigProspecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaAsigProspecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListarCotizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCronograma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListarCotizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgLotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaLotesSeparados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaLotesSep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaLotesSeparados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporteValor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1082, 655);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            this.xtraTabControl1.Size = new System.Drawing.Size(1058, 631);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage2,
            this.xtraTabPage4,
            this.xtraTabPage1,
            this.xtraTabPage3});
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.layoutControl3);
            this.xtraTabPage2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage2.ImageOptions.Image")));
            this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1056, 595);
            this.xtraTabPage2.Text = "Listado de Prospectos";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.btnVincularCliente);
            this.layoutControl3.Controls.Add(this.gcListaAsigProspecto);
            this.layoutControl3.Controls.Add(this.lblTituloProspecto);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(1320, 744);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // btnVincularCliente
            // 
            this.btnVincularCliente.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnVincularCliente.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnVincularCliente.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVincularCliente.Appearance.Options.UseBackColor = true;
            this.btnVincularCliente.Appearance.Options.UseBorderColor = true;
            this.btnVincularCliente.Appearance.Options.UseFont = true;
            this.btnVincularCliente.ImageOptions.Image = global::UI_GestionLotes.Properties.Resources.proforma_invoice_16px;
            this.btnVincularCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVincularCliente.Location = new System.Drawing.Point(1386, 5);
            this.btnVincularCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnVincularCliente.MaximumSize = new System.Drawing.Size(163, 0);
            this.btnVincularCliente.MinimumSize = new System.Drawing.Size(163, 0);
            this.btnVincularCliente.Name = "btnVincularCliente";
            this.btnVincularCliente.Size = new System.Drawing.Size(163, 30);
            this.btnVincularCliente.StyleController = this.layoutControl3;
            this.btnVincularCliente.TabIndex = 10;
            this.btnVincularCliente.Text = "Vincular Prospecto";
            this.btnVincularCliente.Click += new System.EventHandler(this.btnVincularCliente_Click);
            // 
            // gcListaAsigProspecto
            // 
            this.gcListaAsigProspecto.DataSource = this.bsListaAsigProspecto;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcListaAsigProspecto.Location = new System.Drawing.Point(4, 38);
            this.gcListaAsigProspecto.MainView = this.gvListaAsigProspecto;
            this.gcListaAsigProspecto.Margin = new System.Windows.Forms.Padding(4);
            this.gcListaAsigProspecto.Name = "gcListaAsigProspecto";
            this.gcListaAsigProspecto.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRatingControl1,
            this.repositoryItemMemoEdit1});
            this.gcListaAsigProspecto.Size = new System.Drawing.Size(1640, 878);
            this.gcListaAsigProspecto.TabIndex = 9;
            this.gcListaAsigProspecto.UseEmbeddedNavigator = true;
            this.gcListaAsigProspecto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaAsigProspecto});
            // 
            // bsListaAsigProspecto
            // 
            this.bsListaAsigProspecto.DataSource = typeof(BE_GestionLotes.eProspectosXLote);
            // 
            // gvListaAsigProspecto
            // 
            this.gvListaAsigProspecto.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvListaAsigProspecto.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListaAsigProspecto.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaAsigProspecto.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListaAsigProspecto.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.gvListaAsigProspecto.Appearance.GroupRow.ForeColor = System.Drawing.Color.Blue;
            this.gvListaAsigProspecto.Appearance.GroupRow.Options.UseFont = true;
            this.gvListaAsigProspecto.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvListaAsigProspecto.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaAsigProspecto.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaAsigProspecto.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaAsigProspecto.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaAsigProspecto.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaAsigProspecto.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListaAsigProspecto.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaAsigProspecto.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListaAsigProspecto.ColumnPanelRowHeight = 43;
            this.gvListaAsigProspecto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_empresa,
            this.colcod_proyecto,
            this.colcod_prospecto,
            this.coldsc_persona,
            this.coldsc_telefono_movil,
            this.gridColumn1,
            this.gridColumn2,
            this.colcod_origen_prospecto,
            this.coldsc_origen_prospecto,
            this.colcod_ejecutivo,
            this.coldsc_ejecutivo,
            this.gridColumn3,
            this.coldsc_num_documento,
            this.coldsc_lotes_asig,
            this.colcod_tipo_documento,
            this.gridColumn4,
            this.coldsc_apelido_paterno,
            this.gridColumn5,
            this.colcod_cliente2,
            this.colcod_estado_civil,
            this.colfch_fec_nac,
            this.coldsc_profesion,
            this.coldsc_direccion});
            this.gvListaAsigProspecto.DetailHeight = 431;
            this.gvListaAsigProspecto.GridControl = this.gcListaAsigProspecto;
            this.gvListaAsigProspecto.GroupCount = 1;
            this.gvListaAsigProspecto.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, "")});
            this.gvListaAsigProspecto.Name = "gvListaAsigProspecto";
            this.gvListaAsigProspecto.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaAsigProspecto.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvListaAsigProspecto.OptionsBehavior.Editable = false;
            this.gvListaAsigProspecto.OptionsPrint.ExpandAllDetails = true;
            this.gvListaAsigProspecto.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaAsigProspecto.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaAsigProspecto.OptionsView.RowAutoHeight = true;
            this.gvListaAsigProspecto.OptionsView.ShowAutoFilterRow = true;
            this.gvListaAsigProspecto.OptionsView.ShowGroupPanel = false;
            this.gvListaAsigProspecto.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldsc_ejecutivo, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvListaAsigProspecto.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListaAsigProspecto_RowClick);
            this.gvListaAsigProspecto.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaAsigProspecto_CustomDrawColumnHeader);
            this.gvListaAsigProspecto.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListaAsigProspecto_RowStyle);
            // 
            // colcod_empresa
            // 
            this.colcod_empresa.Caption = "cod_empresa";
            this.colcod_empresa.FieldName = "cod_empresa";
            this.colcod_empresa.MinWidth = 23;
            this.colcod_empresa.Name = "colcod_empresa";
            this.colcod_empresa.OptionsColumn.AllowEdit = false;
            this.colcod_empresa.OptionsColumn.FixedWidth = true;
            this.colcod_empresa.Width = 121;
            // 
            // colcod_proyecto
            // 
            this.colcod_proyecto.Caption = "cod_proyecto";
            this.colcod_proyecto.FieldName = "cod_proyecto";
            this.colcod_proyecto.MinWidth = 23;
            this.colcod_proyecto.Name = "colcod_proyecto";
            this.colcod_proyecto.OptionsColumn.AllowEdit = false;
            this.colcod_proyecto.OptionsColumn.FixedWidth = true;
            this.colcod_proyecto.Width = 121;
            // 
            // colcod_prospecto
            // 
            this.colcod_prospecto.Caption = "cod_prospecto";
            this.colcod_prospecto.FieldName = "cod_prospecto";
            this.colcod_prospecto.MinWidth = 23;
            this.colcod_prospecto.Name = "colcod_prospecto";
            this.colcod_prospecto.OptionsColumn.AllowEdit = false;
            this.colcod_prospecto.OptionsColumn.FixedWidth = true;
            this.colcod_prospecto.Width = 121;
            // 
            // coldsc_persona
            // 
            this.coldsc_persona.Caption = "Prospecto";
            this.coldsc_persona.FieldName = "dsc_persona";
            this.coldsc_persona.MinWidth = 23;
            this.coldsc_persona.Name = "coldsc_persona";
            this.coldsc_persona.OptionsColumn.AllowEdit = false;
            this.coldsc_persona.OptionsColumn.FixedWidth = true;
            this.coldsc_persona.Visible = true;
            this.coldsc_persona.VisibleIndex = 1;
            this.coldsc_persona.Width = 219;
            // 
            // coldsc_telefono_movil
            // 
            this.coldsc_telefono_movil.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_telefono_movil.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_telefono_movil.Caption = "Telefono movil";
            this.coldsc_telefono_movil.FieldName = "dsc_telefono_movil";
            this.coldsc_telefono_movil.MinWidth = 23;
            this.coldsc_telefono_movil.Name = "coldsc_telefono_movil";
            this.coldsc_telefono_movil.OptionsColumn.AllowEdit = false;
            this.coldsc_telefono_movil.OptionsColumn.FixedWidth = true;
            this.coldsc_telefono_movil.Visible = true;
            this.coldsc_telefono_movil.VisibleIndex = 2;
            this.coldsc_telefono_movil.Width = 105;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Email";
            this.gridColumn1.FieldName = "dsc_email";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 157;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Fecha registro";
            this.gridColumn2.FieldName = "fch_registro";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Width = 135;
            // 
            // colcod_origen_prospecto
            // 
            this.colcod_origen_prospecto.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_origen_prospecto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_origen_prospecto.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_origen_prospecto.Caption = "cod_origen_prospecto";
            this.colcod_origen_prospecto.FieldName = "cod_origen_prospecto";
            this.colcod_origen_prospecto.MinWidth = 23;
            this.colcod_origen_prospecto.Name = "colcod_origen_prospecto";
            this.colcod_origen_prospecto.OptionsColumn.AllowEdit = false;
            this.colcod_origen_prospecto.OptionsColumn.FixedWidth = true;
            this.colcod_origen_prospecto.Width = 65;
            // 
            // coldsc_origen_prospecto
            // 
            this.coldsc_origen_prospecto.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_origen_prospecto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_origen_prospecto.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_origen_prospecto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_origen_prospecto.Caption = "Origen Prospecto";
            this.coldsc_origen_prospecto.FieldName = "dsc_origen_prospecto";
            this.coldsc_origen_prospecto.MinWidth = 23;
            this.coldsc_origen_prospecto.Name = "coldsc_origen_prospecto";
            this.coldsc_origen_prospecto.OptionsColumn.AllowEdit = false;
            this.coldsc_origen_prospecto.OptionsColumn.FixedWidth = true;
            this.coldsc_origen_prospecto.Width = 126;
            // 
            // colcod_ejecutivo
            // 
            this.colcod_ejecutivo.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_ejecutivo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_ejecutivo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_ejecutivo.Caption = "cod_ejecutivo";
            this.colcod_ejecutivo.FieldName = "cod_ejecutivo";
            this.colcod_ejecutivo.MinWidth = 23;
            this.colcod_ejecutivo.Name = "colcod_ejecutivo";
            this.colcod_ejecutivo.OptionsColumn.AllowEdit = false;
            this.colcod_ejecutivo.OptionsColumn.FixedWidth = true;
            this.colcod_ejecutivo.Width = 23;
            // 
            // coldsc_ejecutivo
            // 
            this.coldsc_ejecutivo.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_ejecutivo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_ejecutivo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_ejecutivo.Caption = "Asesor";
            this.coldsc_ejecutivo.FieldName = "dsc_ejecutivo";
            this.coldsc_ejecutivo.MinWidth = 23;
            this.coldsc_ejecutivo.Name = "coldsc_ejecutivo";
            this.coldsc_ejecutivo.OptionsColumn.AllowEdit = false;
            this.coldsc_ejecutivo.OptionsColumn.FixedWidth = true;
            this.coldsc_ejecutivo.Visible = true;
            this.coldsc_ejecutivo.VisibleIndex = 1;
            this.coldsc_ejecutivo.Width = 292;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "flg_activo";
            this.gridColumn3.FieldName = "flg_activo";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Width = 23;
            // 
            // coldsc_num_documento
            // 
            this.coldsc_num_documento.Caption = "N° Documento";
            this.coldsc_num_documento.FieldName = "dsc_num_documento";
            this.coldsc_num_documento.MinWidth = 23;
            this.coldsc_num_documento.Name = "coldsc_num_documento";
            this.coldsc_num_documento.OptionsColumn.AllowEdit = false;
            this.coldsc_num_documento.OptionsColumn.FixedWidth = true;
            this.coldsc_num_documento.Visible = true;
            this.coldsc_num_documento.VisibleIndex = 0;
            this.coldsc_num_documento.Width = 83;
            // 
            // coldsc_lotes_asig
            // 
            this.coldsc_lotes_asig.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_lotes_asig.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_lotes_asig.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_lotes_asig.Caption = "Lotes Asignados";
            this.coldsc_lotes_asig.FieldName = "dsc_lotes_asig";
            this.coldsc_lotes_asig.MinWidth = 23;
            this.coldsc_lotes_asig.Name = "coldsc_lotes_asig";
            this.coldsc_lotes_asig.OptionsColumn.AllowEdit = false;
            this.coldsc_lotes_asig.OptionsColumn.FixedWidth = true;
            this.coldsc_lotes_asig.Width = 87;
            // 
            // colcod_tipo_documento
            // 
            this.colcod_tipo_documento.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_tipo_documento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_tipo_documento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_tipo_documento.Caption = "Tipo Documento";
            this.colcod_tipo_documento.MinWidth = 23;
            this.colcod_tipo_documento.Name = "colcod_tipo_documento";
            this.colcod_tipo_documento.OptionsColumn.AllowEdit = false;
            this.colcod_tipo_documento.OptionsColumn.FixedWidth = true;
            this.colcod_tipo_documento.Width = 87;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "Nombre";
            this.gridColumn4.FieldName = "dsc_nombre";
            this.gridColumn4.MinWidth = 23;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Width = 87;
            // 
            // coldsc_apelido_paterno
            // 
            this.coldsc_apelido_paterno.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_apelido_paterno.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_apelido_paterno.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_apelido_paterno.Caption = "Apellido Paterno";
            this.coldsc_apelido_paterno.FieldName = "dsc_apelido_paterno";
            this.coldsc_apelido_paterno.MinWidth = 23;
            this.coldsc_apelido_paterno.Name = "coldsc_apelido_paterno";
            this.coldsc_apelido_paterno.OptionsColumn.AllowEdit = false;
            this.coldsc_apelido_paterno.OptionsColumn.FixedWidth = true;
            this.coldsc_apelido_paterno.Width = 87;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn5.Caption = "Apellido Materno";
            this.gridColumn5.FieldName = "dsc_apellido_materno";
            this.gridColumn5.MinWidth = 23;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Width = 87;
            // 
            // colcod_cliente2
            // 
            this.colcod_cliente2.Caption = "Código Cliente";
            this.colcod_cliente2.FieldName = "cod_cliente";
            this.colcod_cliente2.MinWidth = 23;
            this.colcod_cliente2.Name = "colcod_cliente2";
            this.colcod_cliente2.OptionsColumn.AllowEdit = false;
            this.colcod_cliente2.Width = 87;
            // 
            // colcod_estado_civil
            // 
            this.colcod_estado_civil.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_estado_civil.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_estado_civil.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_estado_civil.Caption = "Código Estado Civil";
            this.colcod_estado_civil.FieldName = "cod_estado_civil";
            this.colcod_estado_civil.MinWidth = 23;
            this.colcod_estado_civil.Name = "colcod_estado_civil";
            this.colcod_estado_civil.OptionsColumn.AllowEdit = false;
            this.colcod_estado_civil.OptionsColumn.FixedWidth = true;
            this.colcod_estado_civil.Width = 87;
            // 
            // colfch_fec_nac
            // 
            this.colfch_fec_nac.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_fec_nac.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_fec_nac.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_fec_nac.Caption = "Fecha Nacimiento";
            this.colfch_fec_nac.FieldName = "fch_fec_nac";
            this.colfch_fec_nac.MinWidth = 23;
            this.colfch_fec_nac.Name = "colfch_fec_nac";
            this.colfch_fec_nac.OptionsColumn.AllowEdit = false;
            this.colfch_fec_nac.OptionsColumn.FixedWidth = true;
            this.colfch_fec_nac.Width = 87;
            // 
            // coldsc_profesion
            // 
            this.coldsc_profesion.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_profesion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_profesion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_profesion.Caption = "Ocupación";
            this.coldsc_profesion.FieldName = "dsc_profesion";
            this.coldsc_profesion.MinWidth = 23;
            this.coldsc_profesion.Name = "coldsc_profesion";
            this.coldsc_profesion.OptionsColumn.AllowEdit = false;
            this.coldsc_profesion.OptionsColumn.FixedWidth = true;
            this.coldsc_profesion.Width = 87;
            // 
            // coldsc_direccion
            // 
            this.coldsc_direccion.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_direccion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_direccion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_direccion.Caption = "Dirección";
            this.coldsc_direccion.FieldName = "dsc_direccion";
            this.coldsc_direccion.MinWidth = 23;
            this.coldsc_direccion.Name = "coldsc_direccion";
            this.coldsc_direccion.OptionsColumn.AllowEdit = false;
            this.coldsc_direccion.OptionsColumn.FixedWidth = true;
            this.coldsc_direccion.Width = 87;
            // 
            // repositoryItemRatingControl1
            // 
            this.repositoryItemRatingControl1.AutoHeight = false;
            this.repositoryItemRatingControl1.CheckedGlyph = ((System.Drawing.Image)(resources.GetObject("repositoryItemRatingControl1.CheckedGlyph")));
            this.repositoryItemRatingControl1.FillPrecision = DevExpress.XtraEditors.RatingItemFillPrecision.Exact;
            this.repositoryItemRatingControl1.ItemCount = 4;
            this.repositoryItemRatingControl1.ItemIndent = 10;
            this.repositoryItemRatingControl1.Name = "repositoryItemRatingControl1";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // lblTituloProspecto
            // 
            this.lblTituloProspecto.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTituloProspecto.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTituloProspecto.Appearance.Options.UseFont = true;
            this.lblTituloProspecto.Appearance.Options.UseForeColor = true;
            this.lblTituloProspecto.Location = new System.Drawing.Point(4, 4);
            this.lblTituloProspecto.Margin = new System.Windows.Forms.Padding(4);
            this.lblTituloProspecto.Name = "lblTituloProspecto";
            this.lblTituloProspecto.Size = new System.Drawing.Size(1376, 38);
            this.lblTituloProspecto.StyleController = this.layoutControl3;
            this.lblTituloProspecto.TabIndex = 7;
            this.lblTituloProspecto.Text = "<<Titulo de grupo>>";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1320, 744);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lblTituloProspecto;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(259, 34);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(1105, 34);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.gcListaAsigProspecto;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(1316, 706);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnVincularCliente;
            this.layoutControlItem10.Location = new System.Drawing.Point(1105, 0);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(153, 34);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(211, 34);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.layoutControl5);
            this.xtraTabPage4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage4.ImageOptions.Image")));
            this.xtraTabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1056, 595);
            this.xtraTabPage4.Text = "Listado de Proformas";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.btnNuevaSeparacion);
            this.layoutControl5.Controls.Add(this.gcListarCotizacion);
            this.layoutControl5.Controls.Add(this.lblTituloProformas);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup4;
            this.layoutControl5.Size = new System.Drawing.Size(1056, 595);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // btnNuevaSeparacion
            // 
            this.btnNuevaSeparacion.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevaSeparacion.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnNuevaSeparacion.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnNuevaSeparacion.Appearance.Options.UseBackColor = true;
            this.btnNuevaSeparacion.Appearance.Options.UseFont = true;
            this.btnNuevaSeparacion.Appearance.Options.UseForeColor = true;
            this.btnNuevaSeparacion.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.btnNuevaSeparacion.AppearanceDisabled.Options.UseForeColor = true;
            this.btnNuevaSeparacion.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnNuevaSeparacion.AppearanceHovered.Options.UseForeColor = true;
            this.btnNuevaSeparacion.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btnNuevaSeparacion.AppearancePressed.Options.UseForeColor = true;
            this.btnNuevaSeparacion.ImageOptions.Image = global::UI_GestionLotes.Properties.Resources.proforma2_invoice_16px;
            this.btnNuevaSeparacion.Location = new System.Drawing.Point(884, 4);
            this.btnNuevaSeparacion.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevaSeparacion.MaximumSize = new System.Drawing.Size(163, 0);
            this.btnNuevaSeparacion.MinimumSize = new System.Drawing.Size(163, 0);
            this.btnNuevaSeparacion.Name = "btnNuevaSeparacion";
            this.btnNuevaSeparacion.Size = new System.Drawing.Size(163, 30);
            this.btnNuevaSeparacion.StyleController = this.layoutControl5;
            this.btnNuevaSeparacion.TabIndex = 12;
            this.btnNuevaSeparacion.Text = "Vincular Proforma";
            this.btnNuevaSeparacion.Click += new System.EventHandler(this.btnNuevaSeparacion_Click);
            // 
            // gcListarCotizacion
            // 
            this.gcListarCotizacion.DataSource = this.bsListadoCronograma;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcListarCotizacion.Location = new System.Drawing.Point(4, 38);
            this.gcListarCotizacion.MainView = this.gvListarCotizacion;
            this.gcListarCotizacion.Margin = new System.Windows.Forms.Padding(4);
            this.gcListarCotizacion.Name = "gcListarCotizacion";
            this.gcListarCotizacion.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcListarCotizacion.Size = new System.Drawing.Size(1048, 553);
            this.gcListarCotizacion.TabIndex = 9;
            this.gcListarCotizacion.UseEmbeddedNavigator = true;
            this.gcListarCotizacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListarCotizacion});
            // 
            // bsListadoCronograma
            // 
            this.bsListadoCronograma.DataSource = typeof(BE_GestionLotes.eProformas.eProformas_Detalle);
            // 
            // gvListarCotizacion
            // 
            this.gvListarCotizacion.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gvListarCotizacion.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvListarCotizacion.Appearance.FooterPanel.Options.UseFont = true;
            this.gvListarCotizacion.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvListarCotizacion.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvListarCotizacion.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gvListarCotizacion.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.gvListarCotizacion.Appearance.GroupRow.ForeColor = System.Drawing.Color.Blue;
            this.gvListarCotizacion.Appearance.GroupRow.Options.UseFont = true;
            this.gvListarCotizacion.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvListarCotizacion.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListarCotizacion.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListarCotizacion.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListarCotizacion.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListarCotizacion.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListarCotizacion.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gvListarCotizacion.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListarCotizacion.ColumnPanelRowHeight = 43;
            this.gvListarCotizacion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_proforma,
            this.colfch_proforma,
            this.coldsc_estado,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.colmonto,
            this.coldsc_parametro,
            this.colcod_separacion});
            this.gvListarCotizacion.CustomizationFormBounds = new System.Drawing.Rectangle(1102, 13, 308, 482);
            this.gvListarCotizacion.DetailHeight = 431;
            this.gvListarCotizacion.GridControl = this.gcListarCotizacion;
            this.gvListarCotizacion.Name = "gvListarCotizacion";
            this.gvListarCotizacion.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.gvListarCotizacion.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvListarCotizacion.OptionsPrint.ExpandAllDetails = true;
            this.gvListarCotizacion.OptionsSelection.CheckBoxSelectorColumnWidth = 35;
            this.gvListarCotizacion.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gvListarCotizacion.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.gvListarCotizacion.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListarCotizacion.OptionsView.ShowAutoFilterRow = true;
            this.gvListarCotizacion.OptionsView.ShowGroupPanel = false;
            this.gvListarCotizacion.OptionsView.ShowIndicator = false;
            this.gvListarCotizacion.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvListarCotizacion.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListarCotizacion_RowClick);
            this.gvListarCotizacion.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListarCotizacion_CustomDrawColumnHeader);
            this.gvListarCotizacion.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListarCotizacion_RowStyle);
            // 
            // colcod_proforma
            // 
            this.colcod_proforma.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_proforma.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_proforma.Caption = "Cod. Proforma";
            this.colcod_proforma.FieldName = "cod_proforma";
            this.colcod_proforma.MinWidth = 23;
            this.colcod_proforma.Name = "colcod_proforma";
            this.colcod_proforma.OptionsColumn.AllowEdit = false;
            this.colcod_proforma.OptionsColumn.FixedWidth = true;
            this.colcod_proforma.Visible = true;
            this.colcod_proforma.VisibleIndex = 0;
            this.colcod_proforma.Width = 113;
            // 
            // colfch_proforma
            // 
            this.colfch_proforma.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_proforma.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_proforma.Caption = "Fecha Proforma";
            this.colfch_proforma.FieldName = "fch_proforma";
            this.colfch_proforma.MinWidth = 23;
            this.colfch_proforma.Name = "colfch_proforma";
            this.colfch_proforma.OptionsColumn.AllowEdit = false;
            this.colfch_proforma.OptionsColumn.FixedWidth = true;
            this.colfch_proforma.Visible = true;
            this.colfch_proforma.VisibleIndex = 1;
            this.colfch_proforma.Width = 143;
            // 
            // coldsc_estado
            // 
            this.coldsc_estado.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_estado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_estado.Caption = "Estado";
            this.coldsc_estado.FieldName = "dsc_estado";
            this.coldsc_estado.MinWidth = 23;
            this.coldsc_estado.Name = "coldsc_estado";
            this.coldsc_estado.OptionsColumn.FixedWidth = true;
            this.coldsc_estado.Width = 117;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Cliente";
            this.gridColumn6.FieldName = "dsc_cliente";
            this.gridColumn6.MinWidth = 23;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 496;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Dirección";
            this.gridColumn7.FieldName = "dsc_cadena_direccion";
            this.gridColumn7.MinWidth = 23;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Width = 233;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "N° Lote";
            this.gridColumn8.FieldName = "dsc_lote";
            this.gridColumn8.MinWidth = 23;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 98;
            // 
            // colmonto
            // 
            this.colmonto.Caption = "Precio de Lista Lote";
            this.colmonto.ColumnEdit = this.repositoryItemTextEdit1;
            this.colmonto.FieldName = "monto";
            this.colmonto.MinWidth = 23;
            this.colmonto.Name = "colmonto";
            this.colmonto.OptionsColumn.AllowEdit = false;
            this.colmonto.OptionsColumn.FixedWidth = true;
            this.colmonto.Visible = true;
            this.colmonto.VisibleIndex = 4;
            this.colmonto.Width = 93;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit1.MaskSettings.Set("mask", "c2");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // coldsc_parametro
            // 
            this.coldsc_parametro.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_parametro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_parametro.Caption = "Nivel de Interés";
            this.coldsc_parametro.FieldName = "dsc_interes";
            this.coldsc_parametro.MinWidth = 23;
            this.coldsc_parametro.Name = "coldsc_parametro";
            this.coldsc_parametro.OptionsColumn.FixedWidth = true;
            this.coldsc_parametro.Width = 117;
            // 
            // colcod_separacion
            // 
            this.colcod_separacion.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_separacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_separacion.Caption = "Cod. Separación";
            this.colcod_separacion.FieldName = "cod_separacion";
            this.colcod_separacion.MinWidth = 23;
            this.colcod_separacion.Name = "colcod_separacion";
            this.colcod_separacion.OptionsColumn.FixedWidth = true;
            this.colcod_separacion.Width = 93;
            // 
            // lblTituloProformas
            // 
            this.lblTituloProformas.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTituloProformas.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTituloProformas.Appearance.Options.UseFont = true;
            this.lblTituloProformas.Appearance.Options.UseForeColor = true;
            this.lblTituloProformas.Location = new System.Drawing.Point(4, 4);
            this.lblTituloProformas.Margin = new System.Windows.Forms.Padding(4);
            this.lblTituloProformas.Name = "lblTituloProformas";
            this.lblTituloProformas.Size = new System.Drawing.Size(876, 30);
            this.lblTituloProformas.StyleController = this.layoutControl5;
            this.lblTituloProformas.TabIndex = 8;
            this.lblTituloProformas.Text = "<<Titulo de grupo>>";
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem14});
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup4.Size = new System.Drawing.Size(1056, 595);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.lblTituloProformas;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(259, 34);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(880, 34);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.gcListarCotizacion;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(1052, 557);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnNuevaSeparacion;
            this.layoutControlItem14.Location = new System.Drawing.Point(880, 0);
            this.layoutControlItem14.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem14.MinSize = new System.Drawing.Size(146, 34);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(172, 34);
            this.layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextVisible = false;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.layoutControl2);
            this.xtraTabPage1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage1.ImageOptions.Image")));
            this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1056, 595);
            this.xtraTabPage1.Text = "Listado de Clientes";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.lblTituloCliente);
            this.layoutControl2.Controls.Add(this.btnAgregar);
            this.layoutControl2.Controls.Add(this.btnBuscar);
            this.layoutControl2.Controls.Add(this.gcListaClientes);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1320, 744);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // lblTituloCliente
            // 
            this.lblTituloCliente.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTituloCliente.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTituloCliente.Appearance.Options.UseFont = true;
            this.lblTituloCliente.Appearance.Options.UseForeColor = true;
            this.lblTituloCliente.Location = new System.Drawing.Point(4, 4);
            this.lblTituloCliente.Margin = new System.Windows.Forms.Padding(4);
            this.lblTituloCliente.Name = "lblTituloCliente";
            this.lblTituloCliente.Size = new System.Drawing.Size(1088, 38);
            this.lblTituloCliente.StyleController = this.layoutControl2;
            this.lblTituloCliente.TabIndex = 11;
            this.lblTituloCliente.Text = "<<Titulo de grupo>>";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Appearance.Options.UseForeColor = true;
            this.btnAgregar.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.AppearanceDisabled.Options.UseForeColor = true;
            this.btnAgregar.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.AppearanceHovered.Options.UseForeColor = true;
            this.btnAgregar.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.AppearancePressed.Options.UseForeColor = true;
            this.btnAgregar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.ImageOptions.Image")));
            this.btnAgregar.Location = new System.Drawing.Point(1383, 5);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregar.MaximumSize = new System.Drawing.Size(163, 0);
            this.btnAgregar.MinimumSize = new System.Drawing.Size(163, 0);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(163, 30);
            this.btnAgregar.StyleController = this.layoutControl2;
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Crear Nuevo Cliente";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnBuscar.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Appearance.Options.UseForeColor = true;
            this.btnBuscar.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.AppearanceDisabled.Options.UseForeColor = true;
            this.btnBuscar.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.AppearanceHovered.Options.UseForeColor = true;
            this.btnBuscar.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.AppearancePressed.Options.UseForeColor = true;
            this.btnBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.ImageOptions.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(1098, 5);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.MaximumSize = new System.Drawing.Size(163, 0);
            this.btnBuscar.MinimumSize = new System.Drawing.Size(163, 0);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(163, 30);
            this.btnBuscar.StyleController = this.layoutControl2;
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.Text = "Vincular Cliente";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // gcListaClientes
            // 
            this.gcListaClientes.DataSource = this.bsListaClientes;
            this.gcListaClientes.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcListaClientes.Location = new System.Drawing.Point(4, 38);
            this.gcListaClientes.MainView = this.gvListaClientes;
            this.gcListaClientes.Margin = new System.Windows.Forms.Padding(4);
            this.gcListaClientes.Name = "gcListaClientes";
            this.gcListaClientes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rRatingCalificacion,
            this.rmmTexto});
            this.gcListaClientes.Size = new System.Drawing.Size(1640, 878);
            this.gcListaClientes.TabIndex = 4;
            this.gcListaClientes.UseEmbeddedNavigator = true;
            this.gcListaClientes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaClientes});
            // 
            // bsListaClientes
            // 
            this.bsListaClientes.DataSource = typeof(BE_GestionLotes.eCliente);
            // 
            // gvListaClientes
            // 
            this.gvListaClientes.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaClientes.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaClientes.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaClientes.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListaClientes.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListaClientes.ColumnPanelRowHeight = 43;
            this.gvListaClientes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_asesor,
            this.coldsc_ocupacion,
            this.colflg_activo,
            this.colctd_Deuda,
            this.colLotesAdquiridos,
            this.colcod_cliente,
            this.coldsc_apellido_paterno,
            this.coldsc_apellido_materno,
            this.coldsc_nombre,
            this.coldsc_cliente,
            this.coldsc_razon_comercial,
            this.coldsc_tipo_documento,
            this.coldsc_documento,
            this.coldsc_tipo_cliente,
            this.coldsc_calificacion,
            this.coldsc_categoria,
            this.coldsc_tipo_contacto,
            this.colcod_prospectoc,
            this.coldsc_email,
            this.coldsc_telefono_1,
            this.coldsc_telefono_2,
            this.coldsc_tipo_direccion,
            this.coldsc_cadena_direccion,
            this.coldsc_pais,
            this.coldsc_distrito,
            this.coldsc_provincia,
            this.coldsc_departamento,
            this.coldsc_sexo,
            this.colflg_juridico,
            this.colfch_registro,
            this.coldsc_usuario_registro,
            this.colfch_cambio,
            this.coldsc_usuario_cambio,
            this.colvalorRating,
            this.coldsc_empresas_vinculadas});
            this.gvListaClientes.DetailHeight = 431;
            this.gvListaClientes.GridControl = this.gcListaClientes;
            this.gvListaClientes.Name = "gvListaClientes";
            this.gvListaClientes.OptionsBehavior.Editable = false;
            this.gvListaClientes.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaClientes.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaClientes.OptionsView.RowAutoHeight = true;
            this.gvListaClientes.OptionsView.ShowAutoFilterRow = true;
            this.gvListaClientes.OptionsView.ShowGroupPanel = false;
            this.gvListaClientes.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListaClientes_RowClick);
            this.gvListaClientes.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaClientes_CustomDrawColumnHeader_1);
            this.gvListaClientes.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListaClientes_RowStyle_1);
            // 
            // colcod_asesor
            // 
            this.colcod_asesor.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_asesor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_asesor.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_asesor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_asesor.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_asesor.Caption = "Asesor";
            this.colcod_asesor.FieldName = "cod_asesor";
            this.colcod_asesor.MinWidth = 23;
            this.colcod_asesor.Name = "colcod_asesor";
            this.colcod_asesor.OptionsColumn.AllowEdit = false;
            this.colcod_asesor.OptionsColumn.FixedWidth = true;
            this.colcod_asesor.Width = 87;
            // 
            // coldsc_ocupacion
            // 
            this.coldsc_ocupacion.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_ocupacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_ocupacion.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_ocupacion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_ocupacion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_ocupacion.Caption = "Profesión";
            this.coldsc_ocupacion.FieldName = "dsc_profesion";
            this.coldsc_ocupacion.MinWidth = 23;
            this.coldsc_ocupacion.Name = "coldsc_ocupacion";
            this.coldsc_ocupacion.OptionsColumn.AllowEdit = false;
            this.coldsc_ocupacion.OptionsColumn.FixedWidth = true;
            this.coldsc_ocupacion.Width = 87;
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
            this.colflg_activo.MinWidth = 23;
            this.colflg_activo.Name = "colflg_activo";
            this.colflg_activo.OptionsColumn.AllowEdit = false;
            this.colflg_activo.OptionsColumn.FixedWidth = true;
            this.colflg_activo.Width = 87;
            // 
            // colctd_Deuda
            // 
            this.colctd_Deuda.AppearanceCell.Options.UseFont = true;
            this.colctd_Deuda.AppearanceCell.Options.UseTextOptions = true;
            this.colctd_Deuda.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colctd_Deuda.AppearanceHeader.Options.UseTextOptions = true;
            this.colctd_Deuda.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colctd_Deuda.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colctd_Deuda.Caption = " ";
            this.colctd_Deuda.MinWidth = 23;
            this.colctd_Deuda.Name = "colctd_Deuda";
            this.colctd_Deuda.OptionsColumn.AllowEdit = false;
            this.colctd_Deuda.OptionsColumn.FixedWidth = true;
            this.colctd_Deuda.ToolTip = "¿Existe Deuda?";
            this.colctd_Deuda.Width = 34;
            // 
            // colLotesAdquiridos
            // 
            this.colLotesAdquiridos.AppearanceHeader.Options.UseTextOptions = true;
            this.colLotesAdquiridos.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLotesAdquiridos.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLotesAdquiridos.Caption = "Lotes Adquiridos";
            this.colLotesAdquiridos.MinWidth = 23;
            this.colLotesAdquiridos.Name = "colLotesAdquiridos";
            this.colLotesAdquiridos.OptionsColumn.AllowEdit = false;
            this.colLotesAdquiridos.OptionsColumn.FixedWidth = true;
            this.colLotesAdquiridos.Width = 118;
            // 
            // colcod_cliente
            // 
            this.colcod_cliente.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_cliente.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_cliente.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_cliente.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_cliente.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colcod_cliente.Caption = "Código";
            this.colcod_cliente.FieldName = "cod_cliente";
            this.colcod_cliente.MinWidth = 23;
            this.colcod_cliente.Name = "colcod_cliente";
            this.colcod_cliente.OptionsColumn.AllowEdit = false;
            this.colcod_cliente.OptionsColumn.FixedWidth = true;
            this.colcod_cliente.Width = 105;
            // 
            // coldsc_apellido_paterno
            // 
            this.coldsc_apellido_paterno.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_apellido_paterno.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_apellido_paterno.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_apellido_paterno.Caption = "Apellido Paterno";
            this.coldsc_apellido_paterno.FieldName = "dsc_apellido_paterno";
            this.coldsc_apellido_paterno.MinWidth = 23;
            this.coldsc_apellido_paterno.Name = "coldsc_apellido_paterno";
            this.coldsc_apellido_paterno.OptionsColumn.AllowEdit = false;
            this.coldsc_apellido_paterno.OptionsColumn.FixedWidth = true;
            this.coldsc_apellido_paterno.Width = 87;
            // 
            // coldsc_apellido_materno
            // 
            this.coldsc_apellido_materno.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_apellido_materno.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_apellido_materno.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_apellido_materno.Caption = "Apellido Materno";
            this.coldsc_apellido_materno.FieldName = "dsc_apellido_materno";
            this.coldsc_apellido_materno.MinWidth = 23;
            this.coldsc_apellido_materno.Name = "coldsc_apellido_materno";
            this.coldsc_apellido_materno.OptionsColumn.AllowEdit = false;
            this.coldsc_apellido_materno.OptionsColumn.FixedWidth = true;
            this.coldsc_apellido_materno.Width = 87;
            // 
            // coldsc_nombre
            // 
            this.coldsc_nombre.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_nombre.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_nombre.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_nombre.Caption = "Nombres";
            this.coldsc_nombre.FieldName = "dsc_nombre";
            this.coldsc_nombre.MinWidth = 23;
            this.coldsc_nombre.Name = "coldsc_nombre";
            this.coldsc_nombre.OptionsColumn.AllowEdit = false;
            this.coldsc_nombre.OptionsColumn.FixedWidth = true;
            this.coldsc_nombre.Width = 87;
            // 
            // coldsc_cliente
            // 
            this.coldsc_cliente.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_cliente.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_cliente.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_cliente.Caption = "Razón Social / Apellidos y Nombres";
            this.coldsc_cliente.FieldName = "dsc_cliente";
            this.coldsc_cliente.MinWidth = 23;
            this.coldsc_cliente.Name = "coldsc_cliente";
            this.coldsc_cliente.OptionsColumn.AllowEdit = false;
            this.coldsc_cliente.Visible = true;
            this.coldsc_cliente.VisibleIndex = 1;
            this.coldsc_cliente.Width = 366;
            // 
            // coldsc_razon_comercial
            // 
            this.coldsc_razon_comercial.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_razon_comercial.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_razon_comercial.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_razon_comercial.Caption = "Nombre Comercial";
            this.coldsc_razon_comercial.FieldName = "dsc_razon_comercial";
            this.coldsc_razon_comercial.MinWidth = 23;
            this.coldsc_razon_comercial.Name = "coldsc_razon_comercial";
            this.coldsc_razon_comercial.OptionsColumn.AllowEdit = false;
            this.coldsc_razon_comercial.OptionsColumn.FixedWidth = true;
            this.coldsc_razon_comercial.Width = 233;
            // 
            // coldsc_tipo_documento
            // 
            this.coldsc_tipo_documento.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_tipo_documento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_tipo_documento.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_tipo_documento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_tipo_documento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_tipo_documento.Caption = "Tipo Doc";
            this.coldsc_tipo_documento.FieldName = "dsc_tipo_documento";
            this.coldsc_tipo_documento.MinWidth = 23;
            this.coldsc_tipo_documento.Name = "coldsc_tipo_documento";
            this.coldsc_tipo_documento.OptionsColumn.AllowEdit = false;
            this.coldsc_tipo_documento.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_documento.Width = 114;
            // 
            // coldsc_documento
            // 
            this.coldsc_documento.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_documento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_documento.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_documento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_documento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_documento.Caption = "N° de Documento";
            this.coldsc_documento.FieldName = "dsc_documento";
            this.coldsc_documento.MinWidth = 23;
            this.coldsc_documento.Name = "coldsc_documento";
            this.coldsc_documento.OptionsColumn.AllowEdit = false;
            this.coldsc_documento.Visible = true;
            this.coldsc_documento.VisibleIndex = 0;
            this.coldsc_documento.Width = 126;
            // 
            // coldsc_tipo_cliente
            // 
            this.coldsc_tipo_cliente.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_tipo_cliente.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_tipo_cliente.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_tipo_cliente.Caption = "Tipo de Cliente";
            this.coldsc_tipo_cliente.FieldName = "dsc_tipo_cliente";
            this.coldsc_tipo_cliente.MinWidth = 23;
            this.coldsc_tipo_cliente.Name = "coldsc_tipo_cliente";
            this.coldsc_tipo_cliente.OptionsColumn.AllowEdit = false;
            this.coldsc_tipo_cliente.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_cliente.Width = 140;
            // 
            // coldsc_calificacion
            // 
            this.coldsc_calificacion.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_calificacion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_calificacion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_calificacion.Caption = "Clasificación";
            this.coldsc_calificacion.FieldName = "dsc_calificacion";
            this.coldsc_calificacion.MinWidth = 23;
            this.coldsc_calificacion.Name = "coldsc_calificacion";
            this.coldsc_calificacion.OptionsColumn.AllowEdit = false;
            this.coldsc_calificacion.OptionsColumn.FixedWidth = true;
            this.coldsc_calificacion.Width = 140;
            // 
            // coldsc_categoria
            // 
            this.coldsc_categoria.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_categoria.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_categoria.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_categoria.Caption = "Categoría";
            this.coldsc_categoria.FieldName = "dsc_categoria";
            this.coldsc_categoria.MinWidth = 23;
            this.coldsc_categoria.Name = "coldsc_categoria";
            this.coldsc_categoria.OptionsColumn.AllowEdit = false;
            this.coldsc_categoria.OptionsColumn.FixedWidth = true;
            this.coldsc_categoria.Width = 140;
            // 
            // coldsc_tipo_contacto
            // 
            this.coldsc_tipo_contacto.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_tipo_contacto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_tipo_contacto.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_tipo_contacto.Caption = "Tipo Contacto";
            this.coldsc_tipo_contacto.FieldName = "dsc_tipo_contacto";
            this.coldsc_tipo_contacto.MinWidth = 23;
            this.coldsc_tipo_contacto.Name = "coldsc_tipo_contacto";
            this.coldsc_tipo_contacto.OptionsColumn.AllowEdit = false;
            this.coldsc_tipo_contacto.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_contacto.Width = 114;
            // 
            // colcod_prospectoc
            // 
            this.colcod_prospectoc.Caption = "Código Prospecto";
            this.colcod_prospectoc.FieldName = "cod_prospecto";
            this.colcod_prospectoc.MinWidth = 60;
            this.colcod_prospectoc.Name = "colcod_prospectoc";
            this.colcod_prospectoc.OptionsColumn.AllowEdit = false;
            this.colcod_prospectoc.Width = 128;
            // 
            // coldsc_email
            // 
            this.coldsc_email.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_email.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_email.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_email.Caption = "Email";
            this.coldsc_email.FieldName = "dsc_email";
            this.coldsc_email.MinWidth = 23;
            this.coldsc_email.Name = "coldsc_email";
            this.coldsc_email.OptionsColumn.AllowEdit = false;
            this.coldsc_email.Visible = true;
            this.coldsc_email.VisibleIndex = 2;
            this.coldsc_email.Width = 326;
            // 
            // coldsc_telefono_1
            // 
            this.coldsc_telefono_1.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_telefono_1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_telefono_1.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_telefono_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_telefono_1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_telefono_1.Caption = "Teléfono 1";
            this.coldsc_telefono_1.FieldName = "dsc_telefono_1";
            this.coldsc_telefono_1.MinWidth = 23;
            this.coldsc_telefono_1.Name = "coldsc_telefono_1";
            this.coldsc_telefono_1.OptionsColumn.AllowEdit = false;
            this.coldsc_telefono_1.Visible = true;
            this.coldsc_telefono_1.VisibleIndex = 3;
            this.coldsc_telefono_1.Width = 200;
            // 
            // coldsc_telefono_2
            // 
            this.coldsc_telefono_2.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_telefono_2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_telefono_2.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_telefono_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_telefono_2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_telefono_2.Caption = "Teléfono 2";
            this.coldsc_telefono_2.FieldName = "dsc_telefono_2";
            this.coldsc_telefono_2.MinWidth = 23;
            this.coldsc_telefono_2.Name = "coldsc_telefono_2";
            this.coldsc_telefono_2.OptionsColumn.AllowEdit = false;
            this.coldsc_telefono_2.OptionsColumn.FixedWidth = true;
            this.coldsc_telefono_2.Width = 96;
            // 
            // coldsc_tipo_direccion
            // 
            this.coldsc_tipo_direccion.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_tipo_direccion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_tipo_direccion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_tipo_direccion.Caption = "Tipo dirección";
            this.coldsc_tipo_direccion.FieldName = "dsc_tipo_direccion";
            this.coldsc_tipo_direccion.MinWidth = 23;
            this.coldsc_tipo_direccion.Name = "coldsc_tipo_direccion";
            this.coldsc_tipo_direccion.OptionsColumn.AllowEdit = false;
            this.coldsc_tipo_direccion.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_direccion.Width = 72;
            // 
            // coldsc_cadena_direccion
            // 
            this.coldsc_cadena_direccion.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_cadena_direccion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_cadena_direccion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.coldsc_cadena_direccion.Caption = "Dirección";
            this.coldsc_cadena_direccion.FieldName = "dsc_cadena_direccion";
            this.coldsc_cadena_direccion.MinWidth = 23;
            this.coldsc_cadena_direccion.Name = "coldsc_cadena_direccion";
            this.coldsc_cadena_direccion.OptionsColumn.AllowEdit = false;
            this.coldsc_cadena_direccion.OptionsColumn.FixedWidth = true;
            this.coldsc_cadena_direccion.Width = 210;
            // 
            // coldsc_pais
            // 
            this.coldsc_pais.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_pais.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_pais.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_pais.Caption = "País";
            this.coldsc_pais.FieldName = "dsc_pais";
            this.coldsc_pais.MinWidth = 23;
            this.coldsc_pais.Name = "coldsc_pais";
            this.coldsc_pais.OptionsColumn.AllowEdit = false;
            this.coldsc_pais.OptionsColumn.FixedWidth = true;
            this.coldsc_pais.Width = 117;
            // 
            // coldsc_distrito
            // 
            this.coldsc_distrito.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_distrito.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_distrito.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_distrito.Caption = "Distrito";
            this.coldsc_distrito.FieldName = "dsc_distrito";
            this.coldsc_distrito.MinWidth = 23;
            this.coldsc_distrito.Name = "coldsc_distrito";
            this.coldsc_distrito.OptionsColumn.AllowEdit = false;
            this.coldsc_distrito.OptionsColumn.FixedWidth = true;
            this.coldsc_distrito.Width = 64;
            // 
            // coldsc_provincia
            // 
            this.coldsc_provincia.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_provincia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_provincia.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_provincia.Caption = "Provincia";
            this.coldsc_provincia.FieldName = "dsc_provincia";
            this.coldsc_provincia.MinWidth = 23;
            this.coldsc_provincia.Name = "coldsc_provincia";
            this.coldsc_provincia.OptionsColumn.AllowEdit = false;
            this.coldsc_provincia.OptionsColumn.FixedWidth = true;
            this.coldsc_provincia.Width = 65;
            // 
            // coldsc_departamento
            // 
            this.coldsc_departamento.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_departamento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_departamento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_departamento.Caption = "Departamento";
            this.coldsc_departamento.FieldName = "dsc_departamento";
            this.coldsc_departamento.MinWidth = 23;
            this.coldsc_departamento.Name = "coldsc_departamento";
            this.coldsc_departamento.OptionsColumn.AllowEdit = false;
            this.coldsc_departamento.OptionsColumn.FixedWidth = true;
            this.coldsc_departamento.Width = 117;
            // 
            // coldsc_sexo
            // 
            this.coldsc_sexo.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_sexo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_sexo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_sexo.Caption = "Sexo";
            this.coldsc_sexo.FieldName = "dsc_sexo";
            this.coldsc_sexo.MinWidth = 23;
            this.coldsc_sexo.Name = "coldsc_sexo";
            this.coldsc_sexo.OptionsColumn.AllowEdit = false;
            this.coldsc_sexo.OptionsColumn.FixedWidth = true;
            this.coldsc_sexo.Width = 82;
            // 
            // colflg_juridico
            // 
            this.colflg_juridico.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_juridico.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_juridico.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_juridico.Caption = "Es Jurid.";
            this.colflg_juridico.FieldName = "flg_juridico";
            this.colflg_juridico.MinWidth = 23;
            this.colflg_juridico.Name = "colflg_juridico";
            this.colflg_juridico.OptionsColumn.AllowEdit = false;
            this.colflg_juridico.OptionsColumn.FixedWidth = true;
            this.colflg_juridico.ToolTip = "Es persona jurídica";
            this.colflg_juridico.Width = 82;
            // 
            // colfch_registro
            // 
            this.colfch_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_registro.Caption = "Fecha registro";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.MinWidth = 23;
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.OptionsColumn.AllowEdit = false;
            this.colfch_registro.OptionsColumn.FixedWidth = true;
            this.colfch_registro.Width = 93;
            // 
            // coldsc_usuario_registro
            // 
            this.coldsc_usuario_registro.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_usuario_registro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_usuario_registro.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_usuario_registro.Caption = "Usuario registro";
            this.coldsc_usuario_registro.FieldName = "dsc_usuario_registro";
            this.coldsc_usuario_registro.MinWidth = 23;
            this.coldsc_usuario_registro.Name = "coldsc_usuario_registro";
            this.coldsc_usuario_registro.OptionsColumn.AllowEdit = false;
            this.coldsc_usuario_registro.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_registro.Width = 140;
            // 
            // colfch_cambio
            // 
            this.colfch_cambio.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cambio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_cambio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colfch_cambio.Caption = "Fecha modificación";
            this.colfch_cambio.FieldName = "fch_cambio";
            this.colfch_cambio.MinWidth = 23;
            this.colfch_cambio.Name = "colfch_cambio";
            this.colfch_cambio.OptionsColumn.AllowEdit = false;
            this.colfch_cambio.OptionsColumn.FixedWidth = true;
            this.colfch_cambio.Width = 93;
            // 
            // coldsc_usuario_cambio
            // 
            this.coldsc_usuario_cambio.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_usuario_cambio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_usuario_cambio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_usuario_cambio.Caption = "Usuario modificación";
            this.coldsc_usuario_cambio.FieldName = "dsc_usuario_cambio";
            this.coldsc_usuario_cambio.MinWidth = 23;
            this.coldsc_usuario_cambio.Name = "coldsc_usuario_cambio";
            this.coldsc_usuario_cambio.OptionsColumn.AllowEdit = false;
            this.coldsc_usuario_cambio.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_cambio.Width = 140;
            // 
            // colvalorRating
            // 
            this.colvalorRating.AppearanceHeader.Options.UseTextOptions = true;
            this.colvalorRating.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colvalorRating.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colvalorRating.Caption = "Calificación";
            this.colvalorRating.ColumnEdit = this.rRatingCalificacion;
            this.colvalorRating.FieldName = "valorRating";
            this.colvalorRating.MinWidth = 23;
            this.colvalorRating.Name = "colvalorRating";
            this.colvalorRating.OptionsColumn.AllowEdit = false;
            this.colvalorRating.OptionsColumn.FixedWidth = true;
            this.colvalorRating.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colvalorRating.Width = 117;
            // 
            // rRatingCalificacion
            // 
            this.rRatingCalificacion.AutoHeight = false;
            this.rRatingCalificacion.FillPrecision = DevExpress.XtraEditors.RatingItemFillPrecision.Exact;
            this.rRatingCalificacion.ItemCount = 4;
            this.rRatingCalificacion.ItemIndent = 10;
            this.rRatingCalificacion.Name = "rRatingCalificacion";
            // 
            // coldsc_empresas_vinculadas
            // 
            this.coldsc_empresas_vinculadas.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_empresas_vinculadas.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_empresas_vinculadas.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_empresas_vinculadas.Caption = "Empresas vinculadas";
            this.coldsc_empresas_vinculadas.ColumnEdit = this.rmmTexto;
            this.coldsc_empresas_vinculadas.FieldName = "dsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.MinWidth = 23;
            this.coldsc_empresas_vinculadas.Name = "coldsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.OptionsColumn.AllowEdit = false;
            this.coldsc_empresas_vinculadas.OptionsColumn.FixedWidth = true;
            this.coldsc_empresas_vinculadas.Width = 159;
            // 
            // rmmTexto
            // 
            this.rmmTexto.Name = "rmmTexto";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem8,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1320, 744);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnBuscar;
            this.layoutControlItem3.Location = new System.Drawing.Point(874, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(132, 34);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(209, 34);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListaClientes;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1316, 706);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnAgregar;
            this.layoutControlItem5.Location = new System.Drawing.Point(1102, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(161, 34);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(214, 34);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lblTituloCliente;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(259, 34);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(874, 34);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(1083, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(19, 34);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.layoutControl4);
            this.xtraTabPage3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage3.ImageOptions.Image")));
            this.xtraTabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.PageVisible = false;
            this.xtraTabPage3.Size = new System.Drawing.Size(1056, 595);
            this.xtraTabPage3.Text = "Listado de Lotes Libres";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.rgLotes);
            this.layoutControl4.Controls.Add(this.gcListaLotesSeparados);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(1056, 595);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // rgLotes
            // 
            this.rgLotes.EditValue = "SE";
            this.rgLotes.Location = new System.Drawing.Point(2, 2);
            this.rgLotes.Margin = new System.Windows.Forms.Padding(4);
            this.rgLotes.Name = "rgLotes";
            this.rgLotes.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("SE", "Lotes Separados"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("DS", "Lotes Disponibles")});
            this.rgLotes.Size = new System.Drawing.Size(1052, 54);
            this.rgLotes.StyleController = this.layoutControl4;
            this.rgLotes.TabIndex = 5;
            this.rgLotes.EditValueChanged += new System.EventHandler(this.rgLotes_EditValueChanged);
            // 
            // gcListaLotesSeparados
            // 
            this.gcListaLotesSeparados.DataSource = this.bsListaLotesSep;
            this.gcListaLotesSeparados.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaLotesSeparados.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaLotesSeparados.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaLotesSeparados.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaLotesSeparados.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaLotesSeparados.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcListaLotesSeparados.Location = new System.Drawing.Point(2, 60);
            this.gcListaLotesSeparados.MainView = this.gvListaLotesSeparados;
            this.gcListaLotesSeparados.Margin = new System.Windows.Forms.Padding(4);
            this.gcListaLotesSeparados.Name = "gcListaLotesSeparados";
            this.gcListaLotesSeparados.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporteValor,
            this.rtxtImporte});
            this.gcListaLotesSeparados.Size = new System.Drawing.Size(1052, 533);
            this.gcListaLotesSeparados.TabIndex = 4;
            this.gcListaLotesSeparados.UseEmbeddedNavigator = true;
            this.gcListaLotesSeparados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaLotesSeparados});
            // 
            // bsListaLotesSep
            // 
            this.bsListaLotesSep.DataSource = typeof(BE_GestionLotes.eLotes_Separaciones);
            // 
            // gvListaLotesSeparados
            // 
            this.gvListaLotesSeparados.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvListaLotesSeparados.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListaLotesSeparados.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaLotesSeparados.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListaLotesSeparados.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaLotesSeparados.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaLotesSeparados.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaLotesSeparados.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaLotesSeparados.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListaLotesSeparados.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaLotesSeparados.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListaLotesSeparados.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaLotesSeparados.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListaLotesSeparados.ColumnPanelRowHeight = 43;
            this.gvListaLotesSeparados.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_lote,
            this.colnum_area_uex,
            this.colnum_area_uco,
            this.colimp_precio_final});
            this.gvListaLotesSeparados.DetailHeight = 431;
            this.gvListaLotesSeparados.GridControl = this.gcListaLotesSeparados;
            this.gvListaLotesSeparados.Name = "gvListaLotesSeparados";
            this.gvListaLotesSeparados.OptionsBehavior.Editable = false;
            this.gvListaLotesSeparados.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaLotesSeparados.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaLotesSeparados.OptionsView.RowAutoHeight = true;
            this.gvListaLotesSeparados.OptionsView.ShowAutoFilterRow = true;
            this.gvListaLotesSeparados.OptionsView.ShowGroupPanel = false;
            this.gvListaLotesSeparados.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListaClientes_RowClick);
            this.gvListaLotesSeparados.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaLotesSeparados_CustomDrawColumnHeader);
            this.gvListaLotesSeparados.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListaLotesSeparados_RowStyle);
            // 
            // coldsc_lote
            // 
            this.coldsc_lote.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_lote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_lote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.coldsc_lote.Caption = "Lote";
            this.coldsc_lote.FieldName = "dsc_lote";
            this.coldsc_lote.MinWidth = 23;
            this.coldsc_lote.Name = "coldsc_lote";
            this.coldsc_lote.Visible = true;
            this.coldsc_lote.VisibleIndex = 0;
            this.coldsc_lote.Width = 87;
            // 
            // colnum_area_uex
            // 
            this.colnum_area_uex.Caption = "Uso Exclusivo";
            this.colnum_area_uex.ColumnEdit = this.rtxtImporteValor;
            this.colnum_area_uex.FieldName = "num_area_uex";
            this.colnum_area_uex.MinWidth = 23;
            this.colnum_area_uex.Name = "colnum_area_uex";
            this.colnum_area_uex.Visible = true;
            this.colnum_area_uex.VisibleIndex = 1;
            this.colnum_area_uex.Width = 87;
            // 
            // rtxtImporteValor
            // 
            this.rtxtImporteValor.AutoHeight = false;
            this.rtxtImporteValor.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporteValor.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtImporteValor.MaskSettings.Set("mask", "n2");
            this.rtxtImporteValor.Name = "rtxtImporteValor";
            // 
            // colnum_area_uco
            // 
            this.colnum_area_uco.Caption = "Uso Común";
            this.colnum_area_uco.ColumnEdit = this.rtxtImporteValor;
            this.colnum_area_uco.FieldName = "num_area_uco";
            this.colnum_area_uco.MinWidth = 23;
            this.colnum_area_uco.Name = "colnum_area_uco";
            this.colnum_area_uco.Visible = true;
            this.colnum_area_uco.VisibleIndex = 2;
            this.colnum_area_uco.Width = 87;
            // 
            // colimp_precio_final
            // 
            this.colimp_precio_final.Caption = "Monto";
            this.colimp_precio_final.ColumnEdit = this.rtxtImporteValor;
            this.colimp_precio_final.FieldName = "imp_precio_final";
            this.colimp_precio_final.MinWidth = 23;
            this.colimp_precio_final.Name = "colimp_precio_final";
            this.colimp_precio_final.Visible = true;
            this.colimp_precio_final.VisibleIndex = 3;
            this.colimp_precio_final.Width = 87;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.Mask.EditMask = "c2";
            this.rtxtImporte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporte.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte.Name = "rtxtImporte";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1056, 595);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcListaLotesSeparados;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 58);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1056, 537);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.rgLotes;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1056, 58);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1082, 655);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.xtraTabControl1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1062, 635);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // frmListarClienteSeparaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 655);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmListarClienteSeparaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado Clientes Separaciones";
            this.Load += new System.EventHandler(this.frmListarClienteSeparaciones_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListarClienteSeparaciones_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListaAsigProspecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaAsigProspecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaAsigProspecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListarCotizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCronograma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListarCotizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgLotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaLotesSeparados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaLotesSep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaLotesSeparados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporteValor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.BindingSource bsListaClientes;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcListaClientes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListaClientes;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activo;
        private DevExpress.XtraGrid.Columns.GridColumn colctd_Deuda;
        private DevExpress.XtraGrid.Columns.GridColumn colLotesAdquiridos;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_cliente;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_apellido_paterno;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_apellido_materno;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombre;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cliente;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_razon_comercial;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_documento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_documento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_cliente;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_calificacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_categoria;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_contacto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_email;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_telefono_1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_telefono_2;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_direccion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cadena_direccion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_pais;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_distrito;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_provincia;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_departamento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_sexo;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_juridico;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn colvalorRating;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl rRatingCalificacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresas_vinculadas;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmmTexto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.LabelControl lblTituloProspecto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.GridControl gcListaAsigProspecto;
        public DevExpress.XtraGrid.Views.Grid.GridView gvListaAsigProspecto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proyecto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_prospecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_persona;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_telefono_movil;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_origen_prospecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_origen_prospecto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_ejecutivo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ejecutivo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_num_documento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_lotes_asig;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo_documento;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_apelido_paterno;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_estado_civil;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_fec_nac;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_profesion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_direccion;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl repositoryItemRatingControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private System.Windows.Forms.BindingSource bsListaAsigProspecto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_asesor;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ocupacion;
        private DevExpress.XtraEditors.SimpleButton btnVincularCliente;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.LabelControl lblTituloCliente;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraGrid.GridControl gcListaLotesSeparados;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListaLotesSeparados;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource bsListaLotesSep;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_lote;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_precio_final;
        private DevExpress.XtraEditors.RadioGroup rgLotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_area_uex;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_area_uco;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporteValor;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraEditors.LabelControl lblTituloProformas;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGrid.GridControl gcListarCotizacion;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListarCotizacion;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proforma;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_proforma;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_estado;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn colmonto;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_parametro;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_separacion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.SimpleButton btnNuevaSeparacion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private System.Windows.Forms.BindingSource bsListadoCronograma;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_cliente2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private GridColumn colcod_prospectoc;
    }
}