namespace UI_GestionLotes.Tools
{
    partial class frmMessageBox
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
            this._backgroundPanel = new DevExpress.XtraEditors.PanelControl();
            this._accept = new DevExpress.XtraEditors.SimpleButton();
            this._cancel = new DevExpress.XtraEditors.SimpleButton();
            this._category = new DevExpress.XtraEditors.LabelControl();
            this._content = new DevExpress.XtraEditors.LabelControl();
            this._icon = new DevExpress.XtraEditors.PictureEdit();
            this._caption = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._backgroundPanel)).BeginInit();
            this._backgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._icon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _backgroundPanel
            // 
            this._backgroundPanel.Appearance.BackColor = System.Drawing.Color.White;
            this._backgroundPanel.Appearance.Options.UseBackColor = true;
            this._backgroundPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._backgroundPanel.Controls.Add(this._accept);
            this._backgroundPanel.Controls.Add(this._cancel);
            this._backgroundPanel.Controls.Add(this._category);
            this._backgroundPanel.Controls.Add(this._content);
            this._backgroundPanel.Controls.Add(this._icon);
            this._backgroundPanel.Controls.Add(this._caption);
            this._backgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._backgroundPanel.Location = new System.Drawing.Point(6, 6);
            this._backgroundPanel.Name = "_backgroundPanel";
            this._backgroundPanel.Padding = new System.Windows.Forms.Padding(4);
            this._backgroundPanel.Size = new System.Drawing.Size(291, 251);
            this._backgroundPanel.TabIndex = 0;
            // 
            // _accept
            // 
            this._accept.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(117)))), ((int)(((byte)(198)))));
            this._accept.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this._accept.Appearance.Options.UseBackColor = true;
            this._accept.Appearance.Options.UseFont = true;
            this._accept.Location = new System.Drawing.Point(20, 200);
            this._accept.Name = "_accept";
            this._accept.Size = new System.Drawing.Size(248, 40);
            this._accept.TabIndex = 0;
            this._accept.Text = "Accept";
            this._accept.Click += new System.EventHandler(this._accept_Click);
            // 
            // _cancel
            // 
            this._cancel.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this._cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this._cancel.Appearance.Options.UseBackColor = true;
            this._cancel.Appearance.Options.UseFont = true;
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(53, 200);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(81, 40);
            this._cancel.TabIndex = 1;
            this._cancel.Text = "No";
            this._cancel.Click += new System.EventHandler(this._cancel_Click);
            // 
            // _category
            // 
            this._category.Appearance.Font = new System.Drawing.Font("Verdana", 10.75F);
            this._category.Appearance.Options.UseFont = true;
            this._category.Location = new System.Drawing.Point(12, 69);
            this._category.Name = "_category";
            this._category.Size = new System.Drawing.Size(79, 17);
            this._category.TabIndex = 0;
            this._category.Text = "CATEGORY";
            this._category.MouseDown += new System.Windows.Forms.MouseEventHandler(this._content_MouseDown);
            // 
            // _content
            // 
            this._content.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this._content.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this._content.Appearance.Options.UseFont = true;
            this._content.Appearance.Options.UseForeColor = true;
            this._content.Appearance.Options.UseTextOptions = true;
            this._content.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this._content.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._content.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._content.Dock = System.Windows.Forms.DockStyle.Fill;
            this._content.Location = new System.Drawing.Point(4, 62);
            this._content.Margin = new System.Windows.Forms.Padding(0, 0, 80, 0);
            this._content.Name = "_content";
            this._content.Padding = new System.Windows.Forms.Padding(10, 34, 10, 56);
            this._content.Size = new System.Drawing.Size(283, 185);
            this._content.TabIndex = 0;
            this._content.Text = "...";
            this._content.MouseDown += new System.Windows.Forms.MouseEventHandler(this._content_MouseDown);
            // 
            // _icon
            // 
            this._icon.EditValue = global::UI_GestionLotes.Properties.Resources.approved;
            this._icon.Location = new System.Drawing.Point(7, 3);
            this._icon.Name = "_icon";
            this._icon.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this._icon.Properties.Appearance.Options.UseBackColor = true;
            this._icon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._icon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this._icon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this._icon.Size = new System.Drawing.Size(68, 60);
            this._icon.TabIndex = 1;
            this._icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this._content_MouseDown);
            // 
            // _caption
            // 
            this._caption.Appearance.Font = new System.Drawing.Font("Verdana", 14.25F);
            this._caption.Appearance.Options.UseFont = true;
            this._caption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._caption.Dock = System.Windows.Forms.DockStyle.Top;
            this._caption.Location = new System.Drawing.Point(4, 4);
            this._caption.Margin = new System.Windows.Forms.Padding(0, 0, 80, 0);
            this._caption.Name = "_caption";
            this._caption.Padding = new System.Windows.Forms.Padding(74, 12, 2, 0);
            this._caption.Size = new System.Drawing.Size(283, 58);
            this._caption.TabIndex = 0;
            this._caption.Text = "-";
            this._caption.MouseDown += new System.Windows.Forms.MouseEventHandler(this._content_MouseDown);
            // 
            // frmMessageBox
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 263);
            this.ControlBox = false;
            this.Controls.Add(this._backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmMessageBox";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.HNGMessageBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HNGMessageBox_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HNGMessageBox_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this._backgroundPanel)).EndInit();
            this._backgroundPanel.ResumeLayout(false);
            this._backgroundPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._icon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl _backgroundPanel;
        private DevExpress.XtraEditors.LabelControl _caption;
        private DevExpress.XtraEditors.PictureEdit _icon;
        private DevExpress.XtraEditors.LabelControl _content;
        private DevExpress.XtraEditors.LabelControl _category;
        private DevExpress.XtraEditors.SimpleButton _accept;
        private DevExpress.XtraEditors.SimpleButton _cancel;
    }
}