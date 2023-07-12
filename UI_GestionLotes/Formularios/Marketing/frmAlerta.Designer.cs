
namespace UI_GestionLotes.Formularios.Marketing
{
    partial class frmAlerta
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.picCampana = new DevExpress.XtraEditors.PictureEdit();
            this.lkpMinutos = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).BeginInit();
            this.layout_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCampana.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMinutos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 103);
            this.divFooter.Size = new System.Drawing.Size(382, 43);
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
            this.btnCancelar.Location = new System.Drawing.Point(146, 2);
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
            this.btnGuardar.Location = new System.Drawing.Point(259, 2);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.Appearance.Options.UseBackColor = true;
            // 
            // layout_footer
            // 
            this.layout_footer.Size = new System.Drawing.Size(382, 41);
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
            this.btnOpcional.Location = new System.Drawing.Point(31, 2);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.picCampana);
            this.layoutControl1.Controls.Add(this.lkpMinutos);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 38);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(382, 65);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(382, 65);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 34);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(362, 11);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(362, 10);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // picCampana
            // 
            this.picCampana.EditValue = global::UI_GestionLotes.Properties.Resources.alarm_32px1;
            this.picCampana.Location = new System.Drawing.Point(339, 22);
            this.picCampana.Name = "picCampana";
            this.picCampana.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picCampana.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picCampana.Size = new System.Drawing.Size(31, 20);
            this.picCampana.StyleController = this.layoutControl1;
            this.picCampana.TabIndex = 10;
            this.picCampana.Click += new System.EventHandler(this.picCampana_Click);
            // 
            // lkpMinutos
            // 
            this.lkpMinutos.Location = new System.Drawing.Point(57, 22);
            this.lkpMinutos.Name = "lkpMinutos";
            this.lkpMinutos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMinutos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_minutos", "Minutos", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lkpMinutos.Size = new System.Drawing.Size(278, 20);
            this.lkpMinutos.StyleController = this.layoutControl1;
            this.lkpMinutos.TabIndex = 9;
            this.lkpMinutos.EditValueChanged += new System.EventHandler(this.lkpMinutos_EditValueChanged);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lkpMinutos;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 10);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(327, 24);
            this.layoutControlItem2.Text = "Aviso :";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(33, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.picCampana;
            this.layoutControlItem6.Location = new System.Drawing.Point(327, 10);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(35, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(35, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(35, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "1";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // frmAlerta
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 146);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAlerta";
            this.Text = "Notificación Próximo Evento";
            this.TitleForeColor = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmAlerta_Load);
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).EndInit();
            this.layout_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCampana.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMinutos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraEditors.LookUpEdit lkpMinutos;
        private DevExpress.XtraEditors.PictureEdit picCampana;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}