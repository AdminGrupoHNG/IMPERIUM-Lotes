namespace UI_GestionLotes.Formularios.Operaciones
{
    partial class frmDetProforma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetProforma));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListarCotizacion = new DevExpress.XtraGrid.GridControl();
            this.bsDetalleProformas = new System.Windows.Forms.BindingSource(this.components);
            this.gvListarCotizacion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_nombre_detalle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_fraccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtCuotas = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colimp_precio_venta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colprc_descuento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPorcentajes = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colimp_descuento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_precio_final = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_separacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprc_cuota_inicial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_cuota_inicial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprc_interes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_interes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_cuotaconigv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).BeginInit();
            this.layout_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListarCotizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetalleProformas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListarCotizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCuotas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentajes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 467);
            this.divFooter.Size = new System.Drawing.Size(1398, 43);
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
            this.btnCancelar.Location = new System.Drawing.Point(600, 2);
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
            this.btnGuardar.Location = new System.Drawing.Point(713, 2);
            this.btnGuardar.Text = "ACEPTAR";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            // 
            // layout_footer
            // 
            this.layout_footer.Size = new System.Drawing.Size(1398, 41);
            this.layout_footer.Controls.SetChildIndex(this.btnOpcional, 0);
            this.layout_footer.Controls.SetChildIndex(this.btnGuardar, 0);
            this.layout_footer.Controls.SetChildIndex(this.btnCancelar, 0);
            this.layout_footer.Controls.SetChildIndex(this.btnAdicional, 0);
            // 
            // btnOpcional
            // 
            this.btnOpcional.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnOpcional.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpcional.Appearance.Options.UseBackColor = true;
            this.btnOpcional.Appearance.Options.UseFont = true;
            this.btnOpcional.Location = new System.Drawing.Point(485, 2);
            // 
            // btnAdicional
            // 
            this.btnAdicional.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.btnAdicional.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicional.Appearance.Options.UseBackColor = true;
            this.btnAdicional.Appearance.Options.UseFont = true;
            this.btnAdicional.Location = new System.Drawing.Point(828, 2);
            this.btnAdicional.Size = new System.Drawing.Size(177, 27);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListarCotizacion);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 38);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1398, 429);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListarCotizacion
            // 
            this.gcListarCotizacion.DataSource = this.bsDetalleProformas;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListarCotizacion.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListarCotizacion.Location = new System.Drawing.Point(8, 8);
            this.gcListarCotizacion.MainView = this.gvListarCotizacion;
            this.gcListarCotizacion.Name = "gcListarCotizacion";
            this.gcListarCotizacion.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte,
            this.rtxtCuotas,
            this.rtxtPorcentajes});
            this.gcListarCotizacion.Size = new System.Drawing.Size(1382, 413);
            this.gcListarCotizacion.TabIndex = 4;
            this.gcListarCotizacion.UseEmbeddedNavigator = true;
            this.gcListarCotizacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListarCotizacion});
            // 
            // bsDetalleProformas
            // 
            this.bsDetalleProformas.DataSource = typeof(BE_GestionLotes.eProformas.eProformas_Detalle);
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
            this.gvListarCotizacion.ColumnPanelRowHeight = 35;
            this.gvListarCotizacion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_nombre_detalle,
            this.colnum_fraccion,
            this.colimp_precio_venta,
            this.colprc_descuento,
            this.colimp_descuento,
            this.colimp_precio_final,
            this.colimp_separacion,
            this.colprc_cuota_inicial,
            this.colimp_cuota_inicial,
            this.colprc_interes,
            this.colimp_interes,
            this.colimp_cuotaconigv});
            this.gvListarCotizacion.CustomizationFormBounds = new System.Drawing.Rectangle(1102, 13, 264, 392);
            this.gvListarCotizacion.GridControl = this.gcListarCotizacion;
            this.gvListarCotizacion.Name = "gvListarCotizacion";
            this.gvListarCotizacion.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gvListarCotizacion.OptionsPrint.ExpandAllDetails = true;
            this.gvListarCotizacion.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gvListarCotizacion.OptionsSelection.MultiSelect = true;
            this.gvListarCotizacion.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvListarCotizacion.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListarCotizacion.OptionsView.ShowGroupPanel = false;
            this.gvListarCotizacion.OptionsView.ShowIndicator = false;
            this.gvListarCotizacion.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True;
            this.gvListarCotizacion.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListarCotizacion_CustomDrawCell);
            this.gvListarCotizacion.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvListarCotizacion_ShowingEditor);
            this.gvListarCotizacion.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvListarCotizacion_CellValueChanged);
            // 
            // coldsc_nombre_detalle
            // 
            this.coldsc_nombre_detalle.Caption = "Escenario";
            this.coldsc_nombre_detalle.FieldName = "dsc_nombre_detalle";
            this.coldsc_nombre_detalle.Name = "coldsc_nombre_detalle";
            this.coldsc_nombre_detalle.OptionsColumn.AllowEdit = false;
            this.coldsc_nombre_detalle.Visible = true;
            this.coldsc_nombre_detalle.VisibleIndex = 1;
            this.coldsc_nombre_detalle.Width = 56;
            // 
            // colnum_fraccion
            // 
            this.colnum_fraccion.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_fraccion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_fraccion.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colnum_fraccion.Caption = "Nro. Cuotas";
            this.colnum_fraccion.ColumnEdit = this.rtxtCuotas;
            this.colnum_fraccion.FieldName = "num_fraccion";
            this.colnum_fraccion.Name = "colnum_fraccion";
            this.colnum_fraccion.OptionsColumn.FixedWidth = true;
            this.colnum_fraccion.Visible = true;
            this.colnum_fraccion.VisibleIndex = 2;
            this.colnum_fraccion.Width = 50;
            // 
            // rtxtCuotas
            // 
            this.rtxtCuotas.AutoHeight = false;
            this.rtxtCuotas.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtCuotas.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.rtxtCuotas.MaskSettings.Set("mask", "d");
            this.rtxtCuotas.Name = "rtxtCuotas";
            this.rtxtCuotas.UseMaskAsDisplayFormat = true;
            // 
            // colimp_precio_venta
            // 
            this.colimp_precio_venta.Caption = "Precio Lista";
            this.colimp_precio_venta.ColumnEdit = this.rtxtImporte;
            this.colimp_precio_venta.FieldName = "imp_precio_venta";
            this.colimp_precio_venta.Name = "colimp_precio_venta";
            this.colimp_precio_venta.OptionsColumn.AllowEdit = false;
            this.colimp_precio_venta.Visible = true;
            this.colimp_precio_venta.VisibleIndex = 3;
            this.colimp_precio_venta.Width = 93;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtImporte.MaskSettings.Set("mask", "c2");
            this.rtxtImporte.MaskSettings.Set("culture", "es-PE");
            this.rtxtImporte.Name = "rtxtImporte";
            // 
            // colprc_descuento
            // 
            this.colprc_descuento.Caption = "% Descuento";
            this.colprc_descuento.ColumnEdit = this.rtxtPorcentajes;
            this.colprc_descuento.FieldName = "prc_descuento";
            this.colprc_descuento.Name = "colprc_descuento";
            this.colprc_descuento.Visible = true;
            this.colprc_descuento.VisibleIndex = 4;
            this.colprc_descuento.Width = 93;
            // 
            // rtxtPorcentajes
            // 
            this.rtxtPorcentajes.AutoHeight = false;
            this.rtxtPorcentajes.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtPorcentajes.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.rtxtPorcentajes.MaskSettings.Set("mask", "p");
            this.rtxtPorcentajes.Name = "rtxtPorcentajes";
            this.rtxtPorcentajes.UseMaskAsDisplayFormat = true;
            // 
            // colimp_descuento
            // 
            this.colimp_descuento.Caption = "Descuento";
            this.colimp_descuento.ColumnEdit = this.rtxtImporte;
            this.colimp_descuento.FieldName = "imp_descuento";
            this.colimp_descuento.Name = "colimp_descuento";
            this.colimp_descuento.Visible = true;
            this.colimp_descuento.VisibleIndex = 5;
            this.colimp_descuento.Width = 74;
            // 
            // colimp_precio_final
            // 
            this.colimp_precio_final.Caption = "Precio Venta";
            this.colimp_precio_final.ColumnEdit = this.rtxtImporte;
            this.colimp_precio_final.FieldName = "imp_precio_final";
            this.colimp_precio_final.Name = "colimp_precio_final";
            this.colimp_precio_final.OptionsColumn.AllowEdit = false;
            this.colimp_precio_final.Visible = true;
            this.colimp_precio_final.VisibleIndex = 6;
            this.colimp_precio_final.Width = 97;
            // 
            // colimp_separacion
            // 
            this.colimp_separacion.Caption = "Separación";
            this.colimp_separacion.ColumnEdit = this.rtxtImporte;
            this.colimp_separacion.FieldName = "imp_separacion";
            this.colimp_separacion.Name = "colimp_separacion";
            this.colimp_separacion.Visible = true;
            this.colimp_separacion.VisibleIndex = 7;
            this.colimp_separacion.Width = 97;
            // 
            // colprc_cuota_inicial
            // 
            this.colprc_cuota_inicial.Caption = "% Cuota Inicial";
            this.colprc_cuota_inicial.ColumnEdit = this.rtxtPorcentajes;
            this.colprc_cuota_inicial.FieldName = "prc_cuota_inicial";
            this.colprc_cuota_inicial.Name = "colprc_cuota_inicial";
            this.colprc_cuota_inicial.Visible = true;
            this.colprc_cuota_inicial.VisibleIndex = 8;
            // 
            // colimp_cuota_inicial
            // 
            this.colimp_cuota_inicial.Caption = "Cuota Inicial";
            this.colimp_cuota_inicial.ColumnEdit = this.rtxtImporte;
            this.colimp_cuota_inicial.FieldName = "imp_cuota_inicial";
            this.colimp_cuota_inicial.Name = "colimp_cuota_inicial";
            this.colimp_cuota_inicial.Visible = true;
            this.colimp_cuota_inicial.VisibleIndex = 9;
            this.colimp_cuota_inicial.Width = 97;
            // 
            // colprc_interes
            // 
            this.colprc_interes.Caption = "% Interés";
            this.colprc_interes.ColumnEdit = this.rtxtPorcentajes;
            this.colprc_interes.FieldName = "prc_interes";
            this.colprc_interes.Name = "colprc_interes";
            this.colprc_interes.Visible = true;
            this.colprc_interes.VisibleIndex = 10;
            this.colprc_interes.Width = 80;
            // 
            // colimp_interes
            // 
            this.colimp_interes.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_interes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_interes.Caption = "Interés";
            this.colimp_interes.ColumnEdit = this.rtxtImporte;
            this.colimp_interes.FieldName = "imp_interes";
            this.colimp_interes.Name = "colimp_interes";
            this.colimp_interes.OptionsColumn.AllowEdit = false;
            this.colimp_interes.OptionsColumn.FixedWidth = true;
            this.colimp_interes.Visible = true;
            this.colimp_interes.VisibleIndex = 11;
            this.colimp_interes.Width = 100;
            // 
            // colimp_cuotaconigv
            // 
            this.colimp_cuotaconigv.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_cuotaconigv.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_cuotaconigv.Caption = "Valor Cuota";
            this.colimp_cuotaconigv.ColumnEdit = this.rtxtImporte;
            this.colimp_cuotaconigv.FieldName = "imp_valor_cuota";
            this.colimp_cuotaconigv.Name = "colimp_cuotaconigv";
            this.colimp_cuotaconigv.OptionsColumn.AllowEdit = false;
            this.colimp_cuotaconigv.OptionsColumn.FixedWidth = true;
            this.colimp_cuotaconigv.Visible = true;
            this.colimp_cuotaconigv.VisibleIndex = 12;
            this.colimp_cuotaconigv.Width = 100;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(1398, 429);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListarCotizacion;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1386, 417);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmDetProforma
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1398, 510);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmDetProforma";
            this.Text = "Simulador de Financiamiento";
            this.TitleForeColor = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmDetProforma_Load);
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).EndInit();
            this.layout_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListarCotizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetalleProformas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListarCotizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCuotas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentajes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcListarCotizacion;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListarCotizacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombre_detalle;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_fraccion;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtCuotas;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_precio_venta;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraGrid.Columns.GridColumn colprc_descuento;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentajes;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_descuento;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_precio_final;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_separacion;
        private DevExpress.XtraGrid.Columns.GridColumn colprc_cuota_inicial;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_cuota_inicial;
        private DevExpress.XtraGrid.Columns.GridColumn colprc_interes;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_interes;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_cuotaconigv;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsDetalleProformas;
    }
}