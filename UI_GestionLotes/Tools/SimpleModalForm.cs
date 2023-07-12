using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Tools
{
    public partial class SimpleModalForm : DevExpress.XtraEditors.XtraForm
    {
        bool isMaximized = false;
        bool _isMaximized = true;
        bool _isClosed = true;
        Point normalLocation;
        Size normalSize;
        [Browsable(true), Category("HNG Style")]
        public Color TitleBackColor { get { return divHeader.Appearance.BackColor; } set { divHeader.Appearance.BackColor = value; } }
        [Browsable(true), Category("HNG Style")]
        public bool IsMaximized { get => _isMaximized; set { _isMaximized = value; _winState.Visible = value; } }
        [Browsable(true), Category("HNG Style")]
        public bool IsClosed { get => _isClosed; set { _isClosed = value; _btnClose.Visible = value; } }
        public SimpleModalForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
        }

        private void SimpleModalForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == (char)Keys.Escape)
                e.Handled = true;
        }

        private void _title_MouseDown(object sender, MouseEventArgs e)
        {
            Tools.ToolHelper.MoveControls.Form(this);
        }

        private void _winState_Click(object sender, EventArgs e)
        {
            custom_size();
        }

        private void _btnClose_Click(object sender, EventArgs e)
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

        private void SimpleModalForm_Load(object sender, EventArgs e)
        {
            set_fore_color();
            this.normalSize = this.Size;
            this.normalLocation = this.Location;
            _title.Text = this.Text;
            new Tools.ToolHelper.ShadowBox().Form(this);
        }
        void custom_size()
        {
            isMaximized = !isMaximized;
            if (isMaximized)
            {
                //this.WindowState = FormWindowState.Maximized;
                this.Location = Screen.PrimaryScreen.WorkingArea.Location;
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            }
            else
            {
                this.Size = normalSize;
                this.Location = normalLocation;
                this.WindowState = FormWindowState.Normal;

            }
        }

        private void SimpleModalForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}