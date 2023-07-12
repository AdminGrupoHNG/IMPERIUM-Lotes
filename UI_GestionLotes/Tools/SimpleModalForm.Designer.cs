namespace UI_GestionLotes.Tools
{
    partial class SimpleModalForm
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
            this.divHeader = new DevExpress.XtraEditors.PanelControl();
            this._winState = new DevExpress.XtraEditors.SimpleButton();
            this._btnClose = new DevExpress.XtraEditors.SimpleButton();
            this._title = new DevExpress.XtraEditors.LabelControl();
            this.divFooter = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.divHeader)).BeginInit();
            this.divHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // divHeader
            // 
            this.divHeader.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divHeader.Appearance.Options.UseBackColor = true;
            this.divHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.divHeader.Controls.Add(this._winState);
            this.divHeader.Controls.Add(this._btnClose);
            this.divHeader.Controls.Add(this._title);
            this.divHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.divHeader.Location = new System.Drawing.Point(0, 0);
            this.divHeader.Name = "divHeader";
            this.divHeader.Padding = new System.Windows.Forms.Padding(6);
            this.divHeader.Size = new System.Drawing.Size(700, 34);
            this.divHeader.TabIndex = 1;
            // 
            // _winState
            // 
            this._winState.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._winState.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this._winState.Appearance.Options.UseBackColor = true;
            this._winState.Appearance.Options.UseFont = true;
            this._winState.Appearance.Options.UseTextOptions = true;
            this._winState.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._winState.Dock = System.Windows.Forms.DockStyle.Right;
            this._winState.Location = new System.Drawing.Point(646, 6);
            this._winState.Name = "_winState";
            this._winState.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this._winState.Size = new System.Drawing.Size(24, 22);
            this._winState.TabIndex = 2;
            this._winState.Text = "[-]";
            this._winState.Click += new System.EventHandler(this._winState_Click);
            // 
            // _btnClose
            // 
            this._btnClose.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this._btnClose.Appearance.Options.UseBackColor = true;
            this._btnClose.Appearance.Options.UseFont = true;
            this._btnClose.Appearance.Options.UseTextOptions = true;
            this._btnClose.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnClose.Location = new System.Drawing.Point(670, 6);
            this._btnClose.Name = "_btnClose";
            this._btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this._btnClose.Size = new System.Drawing.Size(24, 22);
            this._btnClose.TabIndex = 1;
            this._btnClose.Text = "X";
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // _title
            // 
            this._title.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._title.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this._title.Appearance.Options.UseBackColor = true;
            this._title.Appearance.Options.UseFont = true;
            this._title.Appearance.Options.UseTextOptions = true;
            this._title.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._title.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._title.Location = new System.Drawing.Point(6, 6);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(688, 22);
            this._title.TabIndex = 0;
            this._title.Text = "labelControl1";
            this._title.MouseDown += new System.Windows.Forms.MouseEventHandler(this._title_MouseDown);
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.divFooter.Controls.Add(this.labelControl1);
            this.divFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.divFooter.Location = new System.Drawing.Point(0, 370);
            this.divFooter.Name = "divFooter";
            this.divFooter.Size = new System.Drawing.Size(700, 10);
            this.divFooter.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(700, 2);
            this.labelControl1.TabIndex = 0;
            // 
            // SimpleModalForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(700, 380);
            this.Controls.Add(this.divFooter);
            this.Controls.Add(this.divHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "SimpleModalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpleModalForm";
            this.Load += new System.EventHandler(this.SimpleModalForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SimpleModalForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SimpleModalForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.divHeader)).EndInit();
            this.divHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl divHeader;
        private DevExpress.XtraEditors.SimpleButton _winState;
        private DevExpress.XtraEditors.SimpleButton _btnClose;
        private DevExpress.XtraEditors.LabelControl _title;
        public DevExpress.XtraEditors.PanelControl divFooter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}