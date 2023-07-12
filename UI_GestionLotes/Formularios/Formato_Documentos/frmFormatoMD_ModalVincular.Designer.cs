namespace UI_GestionLotes.Formularios.FormatoDocumentos
{
    partial class frmFormatoMD_ModalVincular
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormatoMD_ModalVincular));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoEmpresas = new DevExpress.XtraGrid.GridControl();
            this.bsListadoEmpresas = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoEmpresas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoFormatos = new DevExpress.XtraGrid.GridControl();
            this.bsListadoFormatos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoFormatos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_formatoMD_grupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_formatoMD_grupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_formatoMD_general = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_formatoMD_general = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_obligatorio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_modelo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_jerarquia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoFormatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoFormatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoFormatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 395);
            this.divFooter.Size = new System.Drawing.Size(652, 43);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.btnCancelar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Appearance.Options.UseForeColor = true;
            this.btnCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.ImageOptions.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(222, 2);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.Lime;
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.ImageOptions.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(335, 2);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.groupControl2);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 34);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(652, 361);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Controls.Add(this.layoutControl3);
            this.groupControl2.Location = new System.Drawing.Point(310, 8);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(334, 345);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "VINCULAR A...";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.gcListadoEmpresas);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(2, 23);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(330, 320);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gcListadoEmpresas
            // 
            this.gcListadoEmpresas.DataSource = this.bsListadoEmpresas;
            this.gcListadoEmpresas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoEmpresas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoEmpresas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoEmpresas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoEmpresas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoEmpresas.Location = new System.Drawing.Point(4, 4);
            this.gcListadoEmpresas.MainView = this.gvListadoEmpresas;
            this.gcListadoEmpresas.Name = "gcListadoEmpresas";
            this.gcListadoEmpresas.Size = new System.Drawing.Size(322, 312);
            this.gcListadoEmpresas.TabIndex = 0;
            this.gcListadoEmpresas.UseEmbeddedNavigator = true;
            this.gcListadoEmpresas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoEmpresas});
            // 
            // bsListadoEmpresas
            // 
            this.bsListadoEmpresas.DataSource = typeof(BE_GestionLotes.eProveedor_Empresas);
            // 
            // gvListadoEmpresas
            // 
            this.gvListadoEmpresas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoEmpresas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoEmpresas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoEmpresas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoEmpresas.ColumnPanelRowHeight = 35;
            this.gvListadoEmpresas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_empresa,
            this.coldsc_empresa});
            this.gvListadoEmpresas.GridControl = this.gcListadoEmpresas;
            this.gvListadoEmpresas.Name = "gvListadoEmpresas";
            this.gvListadoEmpresas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoEmpresas.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoEmpresas.OptionsView.ShowGroupPanel = false;
            this.gvListadoEmpresas.OptionsView.ShowIndicator = false;
            // 
            // colcod_empresa
            // 
            this.colcod_empresa.FieldName = "cod_empresa";
            this.colcod_empresa.Name = "colcod_empresa";
            // 
            // coldsc_empresa
            // 
            this.coldsc_empresa.Caption = "EMPRESAS";
            this.coldsc_empresa.FieldName = "dsc_empresa";
            this.coldsc_empresa.Name = "coldsc_empresa";
            this.coldsc_empresa.Visible = true;
            this.coldsc_empresa.VisibleIndex = 0;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup2.Size = new System.Drawing.Size(330, 320);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcListadoEmpresas;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(326, 316);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.BackColor = System.Drawing.Color.Lime;
            this.groupControl1.AppearanceCaption.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.layoutControl2);
            this.groupControl1.Location = new System.Drawing.Point(8, 8);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(298, 345);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "QUIERO ESTOS FORMATOS";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcListadoFormatos);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 23);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(294, 320);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcListadoFormatos
            // 
            this.gcListadoFormatos.DataSource = this.bsListadoFormatos;
            this.gcListadoFormatos.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoFormatos.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoFormatos.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoFormatos.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoFormatos.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoFormatos.Location = new System.Drawing.Point(4, 4);
            this.gcListadoFormatos.MainView = this.gvListadoFormatos;
            this.gcListadoFormatos.Name = "gcListadoFormatos";
            this.gcListadoFormatos.Size = new System.Drawing.Size(286, 292);
            this.gcListadoFormatos.TabIndex = 10;
            this.gcListadoFormatos.UseEmbeddedNavigator = true;
            this.gcListadoFormatos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoFormatos});
            // 
            // bsListadoFormatos
            // 
            this.bsListadoFormatos.DataSource = typeof(BE_GestionLotes.eFormatoMDGeneral_Tree);
            // 
            // gvListadoFormatos
            // 
            this.gvListadoFormatos.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoFormatos.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoFormatos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoFormatos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoFormatos.ColumnPanelRowHeight = 35;
            this.gvListadoFormatos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_formatoMD_grupo,
            this.coldsc_formatoMD_grupo,
            this.colcod_formatoMD_general,
            this.coldsc_formatoMD_general,
            this.colflg_obligatorio,
            this.colnum_modelo,
            this.colnum_jerarquia});
            this.gvListadoFormatos.GridControl = this.gcListadoFormatos;
            this.gvListadoFormatos.GroupCount = 1;
            this.gvListadoFormatos.Name = "gvListadoFormatos";
            this.gvListadoFormatos.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoFormatos.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoFormatos.OptionsView.ShowGroupPanel = false;
            this.gvListadoFormatos.OptionsView.ShowIndicator = false;
            this.gvListadoFormatos.PaintStyleName = "Skin";
            this.gvListadoFormatos.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldsc_formatoMD_grupo, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldsc_formatoMD_general, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvListadoFormatos.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvListadoFormatos_RowCellClick);
            this.gvListadoFormatos.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvListadoFormatos_PopupMenuShowing);
            // 
            // colcod_formatoMD_grupo
            // 
            this.colcod_formatoMD_grupo.FieldName = "cod_formatoMD_grupo";
            this.colcod_formatoMD_grupo.Name = "colcod_formatoMD_grupo";
            // 
            // coldsc_formatoMD_grupo
            // 
            this.coldsc_formatoMD_grupo.Caption = "GRUPO";
            this.coldsc_formatoMD_grupo.FieldName = "dsc_formatoMD_grupo";
            this.coldsc_formatoMD_grupo.Name = "coldsc_formatoMD_grupo";
            this.coldsc_formatoMD_grupo.OptionsColumn.ShowCaption = false;
            this.coldsc_formatoMD_grupo.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.coldsc_formatoMD_grupo.Visible = true;
            this.coldsc_formatoMD_grupo.VisibleIndex = 0;
            // 
            // colcod_formatoMD_general
            // 
            this.colcod_formatoMD_general.FieldName = "cod_formatoMD_general";
            this.colcod_formatoMD_general.Name = "colcod_formatoMD_general";
            // 
            // coldsc_formatoMD_general
            // 
            this.coldsc_formatoMD_general.Caption = "FORMATOS";
            this.coldsc_formatoMD_general.FieldName = "dsc_formatoMD_general";
            this.coldsc_formatoMD_general.Name = "coldsc_formatoMD_general";
            this.coldsc_formatoMD_general.Visible = true;
            this.coldsc_formatoMD_general.VisibleIndex = 0;
            this.coldsc_formatoMD_general.Width = 203;
            // 
            // colflg_obligatorio
            // 
            this.colflg_obligatorio.AppearanceCell.Options.UseImage = true;
            this.colflg_obligatorio.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_obligatorio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_obligatorio.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_obligatorio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_obligatorio.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_obligatorio.Caption = "Obligatorio";
            this.colflg_obligatorio.FieldName = "flg_obligatorio";
            this.colflg_obligatorio.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colflg_obligatorio.Name = "colflg_obligatorio";
            this.colflg_obligatorio.OptionsColumn.AllowEdit = false;
            this.colflg_obligatorio.OptionsColumn.FixedWidth = true;
            this.colflg_obligatorio.Width = 48;
            // 
            // colnum_modelo
            // 
            this.colnum_modelo.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_modelo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_modelo.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_modelo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_modelo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_modelo.Caption = "Vesión";
            this.colnum_modelo.FieldName = "num_modelo";
            this.colnum_modelo.Name = "colnum_modelo";
            this.colnum_modelo.OptionsColumn.AllowEdit = false;
            this.colnum_modelo.OptionsColumn.FixedWidth = true;
            this.colnum_modelo.Width = 48;
            // 
            // colnum_jerarquia
            // 
            this.colnum_jerarquia.FieldName = "num_jerarquia";
            this.colnum_jerarquia.Name = "colnum_jerarquia";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(294, 320);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcListadoFormatos;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(290, 296);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleLabelItem1.ImageOptions.Image")));
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 296);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(290, 20);
            this.simpleLabelItem1.Text = "(Doble Click) sobre el Item para más opciones.";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(242, 16);
            // 
            // Root
            // 
            this.Root.AppearanceGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Root.AppearanceGroup.Options.UseBackColor = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(652, 361);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(302, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(302, 77);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(302, 349);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(302, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(338, 349);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmFormatoMD_ModalVincular
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 438);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmFormatoMD_ModalVincular";
            this.Text = "frmFormatoMD_ModalVincular";
            this.TitleForeColor = System.Drawing.Color.Black;
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoFormatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoFormatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoFormatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.GridControl gcListadoEmpresas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoEmpresas;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.BindingSource bsListadoFormatos;
        private System.Windows.Forms.BindingSource bsListadoEmpresas;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa;
        private DevExpress.XtraGrid.GridControl gcListadoFormatos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoFormatos;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_formatoMD_grupo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_formatoMD_grupo;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_formatoMD_general;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_formatoMD_general;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_obligatorio;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_modelo;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_jerarquia;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}