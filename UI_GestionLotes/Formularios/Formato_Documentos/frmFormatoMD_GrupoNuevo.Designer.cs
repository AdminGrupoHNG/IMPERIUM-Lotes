namespace UI_GestionLotes.Formularios.FormatoDocumentos
{
    partial class frmFormatoMD_GrupoNuevo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormatoMD_GrupoNuevo));
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoGrupoFormato = new DevExpress.XtraGrid.GridControl();
            this.bsListadoGrupoFormato = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoGrupoFormato = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_formatoMD_grupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_formatoMD_grupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_jerarquia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.txtJerarquia = new DevExpress.XtraEditors.TextEdit();
            this.btnSubir = new DevExpress.XtraEditors.SimpleButton();
            this.btnBajar = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).BeginInit();
            this.layout_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoGrupoFormato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoGrupoFormato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoGrupoFormato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJerarquia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 405);
            this.divFooter.Size = new System.Drawing.Size(476, 43);
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
            this.btnCancelar.Location = new System.Drawing.Point(240, 2);
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
            this.btnGuardar.Location = new System.Drawing.Point(353, 2);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Visible = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            // 
            // layout_footer
            // 
            this.layout_footer.Size = new System.Drawing.Size(476, 41);
            this.layout_footer.Controls.SetChildIndex(this.btnOpcional, 0);
            this.layout_footer.Controls.SetChildIndex(this.btnGuardar, 0);
            this.layout_footer.Controls.SetChildIndex(this.btnCancelar, 0);
            // 
            // btnOpcional
            // 
            this.btnOpcional.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnOpcional.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpcional.Appearance.Options.UseBackColor = true;
            this.btnOpcional.Appearance.Options.UseFont = true;
            this.btnOpcional.Location = new System.Drawing.Point(125, 2);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcListadoGrupoFormato);
            this.layoutControl2.Controls.Add(this.txtDescripcion);
            this.layoutControl2.Controls.Add(this.txtJerarquia);
            this.layoutControl2.Controls.Add(this.btnSubir);
            this.layoutControl2.Controls.Add(this.btnBajar);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 38);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(476, 367);
            this.layoutControl2.TabIndex = 2;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcListadoGrupoFormato
            // 
            this.gcListadoGrupoFormato.DataSource = this.bsListadoGrupoFormato;
            this.gcListadoGrupoFormato.Location = new System.Drawing.Point(8, 8);
            this.gcListadoGrupoFormato.MainView = this.gvListadoGrupoFormato;
            this.gcListadoGrupoFormato.Name = "gcListadoGrupoFormato";
            this.gcListadoGrupoFormato.Size = new System.Drawing.Size(460, 293);
            this.gcListadoGrupoFormato.TabIndex = 4;
            this.gcListadoGrupoFormato.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoGrupoFormato});
            // 
            // bsListadoGrupoFormato
            // 
            this.bsListadoGrupoFormato.DataSource = typeof(BE_GestionLotes.eFormatoMD_Grupo);
            // 
            // gvListadoGrupoFormato
            // 
            this.gvListadoGrupoFormato.Appearance.HeaderPanel.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.gvListadoGrupoFormato.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoGrupoFormato.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_formatoMD_grupo,
            this.coldsc_formatoMD_grupo,
            this.colnum_jerarquia});
            this.gvListadoGrupoFormato.GridControl = this.gcListadoGrupoFormato;
            this.gvListadoGrupoFormato.Name = "gvListadoGrupoFormato";
            this.gvListadoGrupoFormato.OptionsView.ShowIndicator = false;
            this.gvListadoGrupoFormato.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoGrupoFormato_CustomDrawColumnHeader);
            this.gvListadoGrupoFormato.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvListadoGrupoFormato_SelectionChanged);
            // 
            // colcod_formatoMD_grupo
            // 
            this.colcod_formatoMD_grupo.Caption = "Código";
            this.colcod_formatoMD_grupo.FieldName = "cod_formatoMD_grupo";
            this.colcod_formatoMD_grupo.Name = "colcod_formatoMD_grupo";
            this.colcod_formatoMD_grupo.OptionsColumn.AllowSize = false;
            this.colcod_formatoMD_grupo.Visible = true;
            this.colcod_formatoMD_grupo.VisibleIndex = 0;
            this.colcod_formatoMD_grupo.Width = 68;
            // 
            // coldsc_formatoMD_grupo
            // 
            this.coldsc_formatoMD_grupo.Caption = "Descripción";
            this.coldsc_formatoMD_grupo.FieldName = "dsc_formatoMD_grupo";
            this.coldsc_formatoMD_grupo.Name = "coldsc_formatoMD_grupo";
            this.coldsc_formatoMD_grupo.Visible = true;
            this.coldsc_formatoMD_grupo.VisibleIndex = 1;
            this.coldsc_formatoMD_grupo.Width = 217;
            // 
            // colnum_jerarquia
            // 
            this.colnum_jerarquia.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_jerarquia.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_jerarquia.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_jerarquia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_jerarquia.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_jerarquia.Caption = "Jerarquía";
            this.colnum_jerarquia.FieldName = "num_jerarquia";
            this.colnum_jerarquia.Name = "colnum_jerarquia";
            this.colnum_jerarquia.OptionsColumn.AllowEdit = false;
            this.colnum_jerarquia.OptionsColumn.AllowSize = false;
            this.colnum_jerarquia.OptionsFilter.AllowFilter = false;
            this.colnum_jerarquia.Visible = true;
            this.colnum_jerarquia.VisibleIndex = 2;
            this.colnum_jerarquia.Width = 129;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(108, 305);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Size = new System.Drawing.Size(268, 20);
            this.txtDescripcion.StyleController = this.layoutControl2;
            this.txtDescripcion.TabIndex = 5;
            // 
            // txtJerarquia
            // 
            this.txtJerarquia.EditValue = "0";
            this.txtJerarquia.Enabled = false;
            this.txtJerarquia.Location = new System.Drawing.Point(108, 329);
            this.txtJerarquia.Name = "txtJerarquia";
            this.txtJerarquia.Size = new System.Drawing.Size(268, 20);
            this.txtJerarquia.StyleController = this.layoutControl2;
            this.txtJerarquia.TabIndex = 6;
            this.txtJerarquia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJerarquia_KeyPress);
            this.txtJerarquia.Leave += new System.EventHandler(this.txtJerarquia_Leave);
            // 
            // btnSubir
            // 
            this.btnSubir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSubir.ImageOptions.Image")));
            this.btnSubir.Location = new System.Drawing.Point(380, 305);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSubir.Size = new System.Drawing.Size(41, 36);
            this.btnSubir.StyleController = this.layoutControl2;
            this.btnSubir.TabIndex = 9;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // btnBajar
            // 
            this.btnBajar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBajar.ImageOptions.Image")));
            this.btnBajar.Location = new System.Drawing.Point(425, 305);
            this.btnBajar.Name = "btnBajar";
            this.btnBajar.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnBajar.Size = new System.Drawing.Size(43, 36);
            this.btnBajar.StyleController = this.layoutControl2;
            this.btnBajar.TabIndex = 10;
            this.btnBajar.Click += new System.EventHandler(this.btnBajar_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.simpleLabelItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(476, 367);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoGrupoFormato;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(464, 297);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.txtJerarquia;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 321);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(372, 24);
            this.layoutControlItem3.Text = "Jerarquía :";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(88, 16);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 345);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(372, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.txtDescripcion;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 297);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(372, 24);
            this.layoutControlItem2.Text = "Descripción :";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(88, 16);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnBajar;
            this.layoutControlItem7.Location = new System.Drawing.Point(417, 297);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(47, 40);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSubir;
            this.layoutControlItem6.Location = new System.Drawing.Point(372, 297);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(45, 40);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.Location = new System.Drawing.Point(372, 337);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(92, 18);
            this.simpleLabelItem1.Text = "Ordenar Jerarquía";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(88, 13);
            // 
            // frmFormatoMD_GrupoNuevo
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 448);
            this.Controls.Add(this.layoutControl2);
            this.IsMaximized = false;
            this.Name = "frmFormatoMD_GrupoNuevo";
            this.Text = "frmFormatoMD_GrupoNuevo";
            this.TitleForeColor = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmFormatoMD_GrupoNuevo_Load);
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).EndInit();
            this.layout_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoGrupoFormato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoGrupoFormato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoGrupoFormato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJerarquia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcListadoGrupoFormato;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoGrupoFormato;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.TextEdit txtJerarquia;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource bsListadoGrupoFormato;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_formatoMD_grupo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_formatoMD_grupo;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_jerarquia;
        private DevExpress.XtraEditors.SimpleButton btnSubir;
        private DevExpress.XtraEditors.SimpleButton btnBajar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
    }
}