using DevExpress.XtraLayout.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UI_GestionLotes.Tools
{
    public partial class ModalForm : DevExpress.XtraEditors.XtraForm
    {
        bool isMaximized = false;
        bool _isButtonClose = true;
        bool _isButtonAditional = false;
        bool _isMaximized = true;
        bool _isClosed = true;
        bool _cancelVisible = true;
        System.Drawing.Point normalLocation;
        System.Drawing.Size normalSize;

        [Browsable(true), Category("HNG Style")]
        public Color TitleBackColor { get { return divHeader.Appearance.BackColor; } set { divHeader.Appearance.BackColor = value; } }
        [Browsable(true), Category("HNG Style")]
        public Color TitleForeColor { get { return _title.Appearance.ForeColor; } set { _title.Appearance.ForeColor = value; _winState.Appearance.ForeColor = value; _btnClose.Appearance.ForeColor = value; } }
        [Browsable(true), Category("HNG Style")]
        public bool IsButtonClose { get => _isButtonClose; set => _isButtonClose = value; }
        [Browsable(true), Category("HNG Style")]
        public bool IsButtonAditional { get => _isButtonAditional; set { _isButtonAditional = value; layoutOpcional.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never; } }
        [Browsable(true), Category("HNG Style")]
        public bool IsMaximized { get => _isMaximized; set { _isMaximized = value; _winState.Visible = value; } }
        [Browsable(true), Category("HNG Style")]
        public bool IsClosed { get => _isClosed; set { _isClosed = value; _btnClose.Visible = value; } }
        [Browsable(true), Category("HNG Style")]
        public bool CancelVisible { get => _cancelVisible; set { _cancelVisible = value; layoutCancelar.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never; } }

        public ModalForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
        }

        //[ReadOnly(true), Browsable(false)]
        //public new FormWindowState WindowState { get; set; }

        private void _title_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isMaximized)
                Tools.ToolHelper.MoveControls.Form(this);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void set_fore_color()
        {
            if (divHeader.BackColor.GetBrightness() >= 0.8F)
            {
                _title.ForeColor = Color.Black;
                _btnClose.ForeColor = Color.Black;
                _winState.ForeColor = Color.Black;
            }
            else
            {
                _title.ForeColor = Color.White;
                _btnClose.ForeColor = Color.White;
                _winState.ForeColor = Color.White;
            }
        }

        private void ModalForm_Load(object sender, EventArgs e)
        {
            set_fore_color();
            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    this.Location = Screen.GetWorkingArea(this).Location;
            //    this.Size = Screen.GetWorkingArea(this).Size;
            //    this.isMaximized = true;

            //    this.normalSize = new Size(this.Width - 80, this.Height - 120);
            //}
            //else
            //{
            //    //this.normalSize = this.Size;
            //    this.normalLocation = this.Location;
            //}
            this.normalSize = this.Size;

            _title.Text = this.Text;
            new Tools.ToolHelper.ShadowBox().Form(this);

            widthButtons = (layoutGuardar.Width + (_cancelVisible ? layoutCancelar.Width : 1) + (_isButtonAditional ? layoutOpcional.Width : 1));
            locationButtons();
        }

        private void ModalForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == (char)Keys.Escape)
                e.Handled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_isButtonClose)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        void custom_size()
        {
            if (isMaximized = !isMaximized)
            {
                this.Location = Screen.GetWorkingArea(this).Location;
                this.Size = Screen.GetWorkingArea(this).Size;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = normalSize;
                CenterLocation();
            }
            locationButtons();
        }

        void CenterLocation()
        {
            // Save values for future(for example, to center a form on next launch)
            int screen_x = Screen.FromControl(this).WorkingArea.X;
            int screen_y = Screen.FromControl(this).WorkingArea.Y;

            // Move it and center using correct screen/monitor
            this.Left = screen_x;
            this.Top = screen_y;
            this.Left += (Screen.FromControl(this).WorkingArea.Width - this.Width) / 2;
            this.Top += (Screen.FromControl(this).WorkingArea.Height - this.Height) / 2;
            normalLocation = new Point(this.Left, this.Top); //this.Location;
        }

        int widthButtons = 117;
        void locationButtons()
        {
            int widthTotal = layout_footer.Width - 2; //this.Width - 2;// emptyBottom.Width;
            int width = widthTotal - (widthButtons-2);
            emptyLeft.Width = (width / 2);
            emptyRight.Width = (width / 2);
            //var mitad = width / 2;
            //emptyLeft.Width = mitad;
            //emptyRight.Width = mitad;
            //emptyLeft.Location = new Point(0, 0);
            //emptyBottom.Location = new Point(0, 30);

            //MessageBox.Show($"total: {widthTotal},  button: {widthButtons},  width: {width}, left: {emptyLeft.Width},  right:{emptyRight.Width}, mitad: {mitad},  abajo: {emptyBottom.Width}");
            //MessageBox.Show($"left: {emptyLeft.Location}, right: {emptyRight.Location}, abajo {emptyBottom.Location}");
        }
        private void _winState_Click(object sender, EventArgs e)
        {
            custom_size();
        }

        private void ModalForm_Resize(object sender, EventArgs e)
        {
            //locationButtons();
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.Appearance.BackColor = TitleBackColor;
        }
    }
}