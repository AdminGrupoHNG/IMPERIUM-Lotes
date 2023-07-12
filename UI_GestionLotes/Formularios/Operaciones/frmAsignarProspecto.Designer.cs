
namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    partial class frmAsignarProspecto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarProspecto));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.gcListaAsigProspecto = new DevExpress.XtraGrid.GridControl();
            this.bsListaAsigProspecto = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaAsigProspecto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_proyecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_prospecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_persona = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_telefono_movil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_origen_prospecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_origen_prospecto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_ejecutivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_ejecutivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_num_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_lotes_asig = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_tipo_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_apelido_paterno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_apellido_materno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_estado_civil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_fec_nac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_profesion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rRatingCalificacion = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.rmmTexto = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaAsigProspecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaAsigProspecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaAsigProspecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.gcListaAsigProspecto);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1218, 482);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(64, 7);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1147, 43);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
            // 
            // picTitulo
            // 
            this.picTitulo.EditValue = ((object)(resources.GetObject("picTitulo.EditValue")));
            this.picTitulo.Location = new System.Drawing.Point(7, 7);
            this.picTitulo.Margin = new System.Windows.Forms.Padding(4);
            this.picTitulo.Name = "picTitulo";
            this.picTitulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitulo.Properties.Appearance.Options.UseBackColor = true;
            this.picTitulo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitulo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitulo.Size = new System.Drawing.Size(53, 43);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 6;
            // 
            // gcListaAsigProspecto
            // 
            this.gcListaAsigProspecto.DataSource = this.bsListaAsigProspecto;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaAsigProspecto.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gcListaAsigProspecto.Location = new System.Drawing.Point(7, 54);
            this.gcListaAsigProspecto.MainView = this.gvListaAsigProspecto;
            this.gcListaAsigProspecto.Margin = new System.Windows.Forms.Padding(4);
            this.gcListaAsigProspecto.Name = "gcListaAsigProspecto";
            this.gcListaAsigProspecto.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rRatingCalificacion,
            this.rmmTexto});
            this.gcListaAsigProspecto.Size = new System.Drawing.Size(1204, 421);
            this.gcListaAsigProspecto.TabIndex = 4;
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
            this.coldsc_email,
            this.colfch_registro,
            this.colcod_origen_prospecto,
            this.coldsc_origen_prospecto,
            this.colcod_ejecutivo,
            this.coldsc_ejecutivo,
            this.colflg_activo,
            this.coldsc_num_documento,
            this.coldsc_lotes_asig,
            this.colcod_tipo_documento,
            this.coldsc_nombre,
            this.coldsc_apelido_paterno,
            this.coldsc_apellido_materno,
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
            this.coldsc_persona.Visible = true;
            this.coldsc_persona.VisibleIndex = 1;
            this.coldsc_persona.Width = 431;
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
            this.coldsc_telefono_movil.Visible = true;
            this.coldsc_telefono_movil.VisibleIndex = 2;
            this.coldsc_telefono_movil.Width = 270;
            // 
            // coldsc_email
            // 
            this.coldsc_email.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_email.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_email.Caption = "Email";
            this.coldsc_email.FieldName = "dsc_email";
            this.coldsc_email.MinWidth = 23;
            this.coldsc_email.Name = "coldsc_email";
            this.coldsc_email.OptionsColumn.AllowEdit = false;
            this.coldsc_email.Visible = true;
            this.coldsc_email.VisibleIndex = 3;
            this.coldsc_email.Width = 308;
            // 
            // colfch_registro
            // 
            this.colfch_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.Caption = "Fecha registro";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.MinWidth = 23;
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.OptionsColumn.AllowEdit = false;
            this.colfch_registro.OptionsColumn.FixedWidth = true;
            this.colfch_registro.Width = 135;
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
            this.coldsc_ejecutivo.Visible = true;
            this.coldsc_ejecutivo.VisibleIndex = 4;
            this.coldsc_ejecutivo.Width = 292;
            // 
            // colflg_activo
            // 
            this.colflg_activo.AppearanceHeader.Options.UseTextOptions = true;
            this.colflg_activo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_activo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colflg_activo.Caption = "flg_activo";
            this.colflg_activo.FieldName = "flg_activo";
            this.colflg_activo.MinWidth = 23;
            this.colflg_activo.Name = "colflg_activo";
            this.colflg_activo.OptionsColumn.AllowEdit = false;
            this.colflg_activo.OptionsColumn.FixedWidth = true;
            this.colflg_activo.Width = 23;
            // 
            // coldsc_num_documento
            // 
            this.coldsc_num_documento.Caption = "N° Documento";
            this.coldsc_num_documento.FieldName = "dsc_num_documento";
            this.coldsc_num_documento.MinWidth = 23;
            this.coldsc_num_documento.Name = "coldsc_num_documento";
            this.coldsc_num_documento.OptionsColumn.AllowEdit = false;
            this.coldsc_num_documento.Visible = true;
            this.coldsc_num_documento.VisibleIndex = 0;
            this.coldsc_num_documento.Width = 165;
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
            // coldsc_nombre
            // 
            this.coldsc_nombre.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_nombre.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_nombre.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_nombre.Caption = "Nombre";
            this.coldsc_nombre.FieldName = "dsc_nombre";
            this.coldsc_nombre.MinWidth = 23;
            this.coldsc_nombre.Name = "coldsc_nombre";
            this.coldsc_nombre.OptionsColumn.AllowEdit = false;
            this.coldsc_nombre.OptionsColumn.FixedWidth = true;
            this.coldsc_nombre.Width = 87;
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
            // rRatingCalificacion
            // 
            this.rRatingCalificacion.AutoHeight = false;
            this.rRatingCalificacion.CheckedGlyph = global::UI_GestionLotes.Properties.Resources.estrella_azul;
            this.rRatingCalificacion.FillPrecision = DevExpress.XtraEditors.RatingItemFillPrecision.Exact;
            this.rRatingCalificacion.ItemCount = 4;
            this.rRatingCalificacion.ItemIndent = 10;
            this.rRatingCalificacion.Name = "rRatingCalificacion";
            // 
            // rmmTexto
            // 
            this.rmmTexto.Name = "rmmTexto";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1218, 482);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListaAsigProspecto;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 47);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1208, 425);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lblTitulo;
            this.layoutControlItem2.Location = new System.Drawing.Point(57, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(259, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1151, 47);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.picTitulo;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(57, 47);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(57, 47);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(57, 47);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmAsignarProspecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 482);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAsignarProspecto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Prospectos";
            this.Load += new System.EventHandler(this.frmAsignarProspecto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAsignarProspecto_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaAsigProspecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaAsigProspecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaAsigProspecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcListaAsigProspecto;
        public DevExpress.XtraGrid.Views.Grid.GridView gvListaAsigProspecto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proyecto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_prospecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_persona;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_telefono_movil;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_email;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_origen_prospecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_origen_prospecto;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_ejecutivo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_ejecutivo;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_activo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_num_documento;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl rRatingCalificacion;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmmTexto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListaAsigProspecto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_lotes_asig;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo_documento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombre;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_apelido_paterno;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_apellido_materno;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_estado_civil;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_fec_nac;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_profesion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_direccion;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}