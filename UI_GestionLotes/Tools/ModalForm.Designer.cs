namespace UI_GestionLotes.Tools
{
    partial class ModalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModalForm));
            this.divHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this._winState = new DevExpress.XtraEditors.SimpleButton();
            this._btnClose = new DevExpress.XtraEditors.SimpleButton();
            this._title = new DevExpress.XtraEditors.LabelControl();
            this.divFooter = new DevExpress.XtraEditors.PanelControl();
            this.layout_footer = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpcional = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutGuardar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCancelar = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptyBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptyLeft = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptyRight = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutOpcional = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.divHeader)).BeginInit();
            this.divHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).BeginInit();
            this.layout_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGuardar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancelar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutOpcional)).BeginInit();
            this.SuspendLayout();
            // 
            // divHeader
            // 
            this.divHeader.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divHeader.Appearance.Options.UseBackColor = true;
            this.divHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.divHeader.Controls.Add(this.btnAdd);
            this.divHeader.Controls.Add(this._winState);
            this.divHeader.Controls.Add(this._btnClose);
            this.divHeader.Controls.Add(this._title);
            this.divHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.divHeader.Location = new System.Drawing.Point(0, 0);
            this.divHeader.Name = "divHeader";
            this.divHeader.Padding = new System.Windows.Forms.Padding(6);
            this.divHeader.Size = new System.Drawing.Size(700, 38);
            this.divHeader.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(7, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnAdd.Size = new System.Drawing.Size(39, 30);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Visible = false;
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            // 
            // _winState
            // 
            this._winState.Appearance.BackColor = System.Drawing.Color.Gray;
            this._winState.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this._winState.Appearance.Options.UseBackColor = true;
            this._winState.Appearance.Options.UseFont = true;
            this._winState.Appearance.Options.UseTextOptions = true;
            this._winState.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._winState.Dock = System.Windows.Forms.DockStyle.Right;
            this._winState.Location = new System.Drawing.Point(646, 6);
            this._winState.Name = "_winState";
            this._winState.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this._winState.Size = new System.Drawing.Size(24, 26);
            this._winState.TabIndex = 2;
            this._winState.Text = "[-]";
            this._winState.Click += new System.EventHandler(this._winState_Click);
            // 
            // _btnClose
            // 
            this._btnClose.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
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
            this._btnClose.Size = new System.Drawing.Size(24, 26);
            this._btnClose.TabIndex = 1;
            this._btnClose.Text = "X";
            this._btnClose.Click += new System.EventHandler(this.simpleButton1_Click);
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
            this._title.Size = new System.Drawing.Size(688, 26);
            this._title.TabIndex = 0;
            this._title.Text = "labelControl1";
            this._title.MouseDown += new System.Windows.Forms.MouseEventHandler(this._title_MouseDown);
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.divFooter.Controls.Add(this.layout_footer);
            this.divFooter.Controls.Add(this.labelControl1);
            this.divFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.divFooter.Location = new System.Drawing.Point(0, 337);
            this.divFooter.Name = "divFooter";
            this.divFooter.Size = new System.Drawing.Size(700, 43);
            this.divFooter.TabIndex = 1;
            // 
            // layout_footer
            // 
            this.layout_footer.Controls.Add(this.btnCancelar);
            this.layout_footer.Controls.Add(this.btnGuardar);
            this.layout_footer.Controls.Add(this.btnOpcional);
            this.layout_footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_footer.Location = new System.Drawing.Point(0, 2);
            this.layout_footer.Name = "layout_footer";
            this.layout_footer.Root = this.Root;
            this.layout_footer.Size = new System.Drawing.Size(700, 41);
            this.layout_footer.TabIndex = 6;
            this.layout_footer.Text = "layoutControl1";
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
            this.btnCancelar.Location = new System.Drawing.Point(348, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 26);
            this.btnCancelar.StyleController = this.layout_footer;
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "CANCELAR";
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
            this.btnGuardar.Location = new System.Drawing.Point(461, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(111, 26);
            this.btnGuardar.StyleController = this.layout_footer;
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "GUARDAR";
            // 
            // btnOpcional
            // 
            this.btnOpcional.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnOpcional.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpcional.Appearance.Options.UseBackColor = true;
            this.btnOpcional.Appearance.Options.UseFont = true;
            this.btnOpcional.Location = new System.Drawing.Point(233, 2);
            this.btnOpcional.Name = "btnOpcional";
            this.btnOpcional.Size = new System.Drawing.Size(111, 26);
            this.btnOpcional.StyleController = this.layout_footer;
            this.btnOpcional.TabIndex = 6;
            this.btnOpcional.Text = "OPCIONAL......";
            this.btnOpcional.Visible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutGuardar,
            this.layoutCancelar,
            this.emptyBottom,
            this.emptyLeft,
            this.emptyRight,
            this.layoutOpcional});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(700, 41);
            this.Root.TextVisible = false;
            // 
            // layoutGuardar
            // 
            this.layoutGuardar.Control = this.btnGuardar;
            this.layoutGuardar.Location = new System.Drawing.Point(459, 0);
            this.layoutGuardar.MaxSize = new System.Drawing.Size(115, 30);
            this.layoutGuardar.MinSize = new System.Drawing.Size(115, 30);
            this.layoutGuardar.Name = "layoutGuardar";
            this.layoutGuardar.Size = new System.Drawing.Size(115, 30);
            this.layoutGuardar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutGuardar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutGuardar.TextVisible = false;
            // 
            // layoutCancelar
            // 
            this.layoutCancelar.Control = this.btnCancelar;
            this.layoutCancelar.Location = new System.Drawing.Point(346, 0);
            this.layoutCancelar.MaxSize = new System.Drawing.Size(113, 30);
            this.layoutCancelar.MinSize = new System.Drawing.Size(113, 30);
            this.layoutCancelar.Name = "layoutCancelar";
            this.layoutCancelar.Size = new System.Drawing.Size(113, 30);
            this.layoutCancelar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutCancelar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutCancelar.TextVisible = false;
            // 
            // emptyBottom
            // 
            this.emptyBottom.AllowHotTrack = false;
            this.emptyBottom.Location = new System.Drawing.Point(0, 30);
            this.emptyBottom.Name = "emptyBottom";
            this.emptyBottom.Size = new System.Drawing.Size(700, 11);
            this.emptyBottom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptyLeft
            // 
            this.emptyLeft.AllowHotTrack = false;
            this.emptyLeft.Location = new System.Drawing.Point(0, 0);
            this.emptyLeft.Name = "emptyLeft";
            this.emptyLeft.Size = new System.Drawing.Size(231, 30);
            this.emptyLeft.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptyRight
            // 
            this.emptyRight.AllowHotTrack = false;
            this.emptyRight.Location = new System.Drawing.Point(574, 0);
            this.emptyRight.Name = "emptyRight";
            this.emptyRight.Size = new System.Drawing.Size(126, 30);
            this.emptyRight.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutOpcional
            // 
            this.layoutOpcional.Control = this.btnOpcional;
            this.layoutOpcional.Location = new System.Drawing.Point(231, 0);
            this.layoutOpcional.MaxSize = new System.Drawing.Size(115, 30);
            this.layoutOpcional.MinSize = new System.Drawing.Size(115, 30);
            this.layoutOpcional.Name = "layoutOpcional";
            this.layoutOpcional.Size = new System.Drawing.Size(115, 30);
            this.layoutOpcional.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutOpcional.TextSize = new System.Drawing.Size(0, 0);
            this.layoutOpcional.TextVisible = false;
            this.layoutOpcional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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
            // ModalForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this._btnClose;
            this.ClientSize = new System.Drawing.Size(700, 380);
            this.ControlBox = false;
            this.Controls.Add(this.divFooter);
            this.Controls.Add(this.divHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ModalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ModalForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ModalForm_KeyPress);
            this.Resize += new System.EventHandler(this.ModalForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.divHeader)).EndInit();
            this.divHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).EndInit();
            this.layout_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGuardar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancelar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutOpcional)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl divHeader;
        private DevExpress.XtraEditors.SimpleButton _btnClose;
        private DevExpress.XtraEditors.LabelControl _title;
        public DevExpress.XtraEditors.PanelControl divFooter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.SimpleButton btnCancelar;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton _winState;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutGuardar;
        private DevExpress.XtraLayout.LayoutControlItem layoutCancelar;
        private DevExpress.XtraLayout.EmptySpaceItem emptyBottom;
        private DevExpress.XtraLayout.EmptySpaceItem emptyLeft;
        private DevExpress.XtraLayout.EmptySpaceItem emptyRight;
        public DevExpress.XtraEditors.SimpleButton btnAdd;
        public DevExpress.XtraLayout.LayoutControl layout_footer;
        private DevExpress.XtraLayout.LayoutControlItem layoutOpcional;
        public DevExpress.XtraEditors.SimpleButton btnOpcional;
    }
}