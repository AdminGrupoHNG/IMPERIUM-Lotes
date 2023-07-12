using DevExpress.Charts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UI_GestionLotes.Tools.ToolHelper;

namespace UI_GestionLotes.Tools
{
    public partial class frmMessageBox : DevExpress.XtraEditors.XtraForm
    {
        MessageBoxIcon category;
        Color _acceptBackColor = Color.FromArgb(41, 118, 198);
        Color _acceptBorderColor = Color.FromArgb(55, 185, 243);

        public frmMessageBox()
        {
            InitializeComponent();
        }
        public frmMessageBox(string content, string caption, MessageBoxIcon category, string category_text = "")
        {
            InitializeComponent();
            this.category = category;
            _caption.Text = caption;
            _content.Text = content;
            _icon.Image = GetCategoryIcon;
            _category.Text = GetCategoryText;
            if (category_text.Length > 0) _category.Text = category_text;
            customButton();
        }
        public frmMessageBox(string content, string caption, MessageBoxIcon category,
            string button_left, string button_right, string category_text = "")
        {
            InitializeComponent();
            this.category = category;
            _caption.Text = caption;
            _content.Text = content;
            _icon.Image = GetCategoryIcon;
            _category.Text = GetCategoryText;

            customButton();
            _cancel.Text = button_left;
            _accept.Text = button_right;
            if (category_text.Length > 0) _category.Text = category_text;

        }
        public frmMessageBox(string content, string caption, MessageBoxIcon category,
            Color acceptBackColor, Color acceptBorderColor, string category_text = "")
        {
            InitializeComponent();
            this.category = category;
            _caption.Text = caption;
            _content.Text = content;
            _icon.Image = GetCategoryIcon;
            _category.Text = GetCategoryText;

            _acceptBackColor = acceptBackColor;
            _acceptBorderColor = acceptBorderColor;
            if (category_text.Length > 0) _category.Text = category_text;
            customButton();
        }
        void customButton()
        {
            if (category == System.Windows.Forms.MessageBoxIcon.Question)
            {
                _accept.Size = new System.Drawing.Size(96, 40);
                _accept.Location = new System.Drawing.Point(140, 200);
                _accept.Text = "Yes";
            }
            else
            {
                _accept.Size = new System.Drawing.Size(248, 40);
                _accept.Location = new System.Drawing.Point(20, 200);
                _accept.Text = "Accept";
            }
            _accept.Appearance.BackColor = _acceptBackColor;
        }
        string GetCategoryText
        {
            get { return CategoryMessageBox(this.category); }
        }
        Image GetCategoryIcon
        {
            get { return Properties.Resources.approved; }// MessageBoxIcon(this.category); }
        }
        void Complete()
        {
            if (category == MessageBoxIcon.Question)
                base.DialogResult = DialogResult.Yes;
            else
                base.DialogResult = DialogResult.OK;
        }

        string CategoryMessageBox(MessageBoxIcon messageBox)
        {
            int value = (int)(MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), messageBox.ToString());
            string img = "INFORMATION";
            switch (value)
            {
                case 16:
                    img = "ERROR!";
                    break;
                case 32:
                    img = "QUESTIONS?";
                    break;
                case 48:
                    img = "WARNING!";
                    break;
                case 64:
                    img = "COMPLETED!";
                    break;
                default:
                    img = "INFORMATION!";
                    break;
            }
            return img;
        }

        private void HNGMessageBox_Load(object sender, EventArgs e)
        {
            new Tools.ToolHelper.ShadowBox().Form(this);

            //RadiusStyle.Form(this, 26, 26);
            //RadiusStyle.Panel(_backgroundPanel, 26, 26);
        }

        private void HNGMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == (char)Keys.Escape)
                e.Handled = true;
        }

        private void _cancel_Click(object sender, EventArgs e)
        {
            if (category == MessageBoxIcon.Question)
            {
                if (_cancel.Text == "No")
                    base.DialogResult = DialogResult.No;
                else
                    base.DialogResult = DialogResult.OK;
            }
            else
                base.DialogResult = DialogResult.Cancel;
        }

        private void _accept_Click(object sender, EventArgs e)
        {
            Complete();
        }

        private void _content_MouseDown(object sender, MouseEventArgs e)
        {
            MoveControls.Form(this);
        }

        private void HNGMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
                Clipboard.SetText(_content.Text);
            if (e.KeyCode == Keys.Escape)
            {
                if (category == MessageBoxIcon.Question)
                    base.DialogResult = DialogResult.No;
                else
                    base.DialogResult = DialogResult.Cancel;
            }
        }
    }
}