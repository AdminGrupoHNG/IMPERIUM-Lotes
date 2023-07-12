namespace UI_GestionLotes.UserControls
{
    partial class usrAccesos
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.picImagen3 = new DevExpress.XtraEditors.PictureEdit();
            this.picImagen2 = new DevExpress.XtraEditors.PictureEdit();
            this.picImagen1 = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.picImagen3);
            this.layoutControl1.Controls.Add(this.picImagen2);
            this.layoutControl1.Controls.Add(this.picImagen1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(300, 210);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // picImagen3
            // 
            this.picImagen3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picImagen3.EditValue = global::UI_GestionLotes.Properties.Resources.onedrive2;
            this.picImagen3.Location = new System.Drawing.Point(12, 138);
            this.picImagen3.Name = "picImagen3";
            this.picImagen3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picImagen3.Properties.Appearance.Options.UseBackColor = true;
            this.picImagen3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.picImagen3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picImagen3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picImagen3.Size = new System.Drawing.Size(276, 60);
            this.picImagen3.StyleController = this.layoutControl1;
            this.picImagen3.TabIndex = 10;
            this.picImagen3.Click += new System.EventHandler(this.picImagen3_Click);
            // 
            // picImagen2
            // 
            this.picImagen2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picImagen2.EditValue = global::UI_GestionLotes.Properties.Resources.sunat;
            this.picImagen2.Location = new System.Drawing.Point(12, 75);
            this.picImagen2.Name = "picImagen2";
            this.picImagen2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picImagen2.Properties.Appearance.Options.UseBackColor = true;
            this.picImagen2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.picImagen2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picImagen2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picImagen2.Size = new System.Drawing.Size(276, 59);
            this.picImagen2.StyleController = this.layoutControl1;
            this.picImagen2.TabIndex = 9;
            this.picImagen2.Click += new System.EventHandler(this.picImagen2_Click);
            // 
            // picImagen1
            // 
            this.picImagen1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picImagen1.EditValue = global::UI_GestionLotes.Properties.Resources.office_365;
            this.picImagen1.Location = new System.Drawing.Point(12, 12);
            this.picImagen1.Name = "picImagen1";
            this.picImagen1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picImagen1.Properties.Appearance.Options.UseBackColor = true;
            this.picImagen1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.picImagen1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picImagen1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picImagen1.Size = new System.Drawing.Size(276, 59);
            this.picImagen1.StyleController = this.layoutControl1;
            this.picImagen1.TabIndex = 8;
            this.picImagen1.Click += new System.EventHandler(this.picImagen1_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem6});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(300, 210);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.picImagen1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(280, 63);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.picImagen2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 63);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(280, 63);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.picImagen3;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 126);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(280, 64);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // usrAccesos
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "usrAccesos";
            this.Size = new System.Drawing.Size(300, 210);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImagen3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PictureEdit picImagen3;
        private DevExpress.XtraEditors.PictureEdit picImagen2;
        private DevExpress.XtraEditors.PictureEdit picImagen1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
