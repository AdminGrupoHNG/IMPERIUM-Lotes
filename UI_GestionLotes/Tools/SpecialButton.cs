using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Tools
{
    [DefaultEvent("BtnClick")]
    public partial class SpecialButton : DevExpress.XtraEditors.XtraUserControl
    {
        private Color _iconBackColor;
        private Image _iconImage;
        private string _caption;
        private Font _captionFont;

        [Browsable(true), Localizable(true), Category("HNG Style")]
        public Color IconBackColor { get => _iconBackColor; set { _iconBackColor = value; pictureEdit1.BackColor = _iconBackColor; } }
        [Browsable(true), Localizable(true), Category("HNG Style")]
        public Image IconImage { get => _iconImage; set { _iconImage = value; pictureEdit1.Image = _iconImage; } }
        [Browsable(true), Localizable(true), Category("HNG Style")]
        public string Caption { get => _caption; set { _caption = value; button1.Text = _caption; } }
        [Browsable(true), Localizable(true), Category("HNG Style")]
        public Font CaptionFont { get => _captionFont; set { _captionFont = value; button1.Font = _captionFont; } }

        [Browsable(true), Localizable(true), Category("HNG Style")]
        public event EventHandler BtnClick;

        public SpecialButton()
        {
            InitializeComponent();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void SpecialButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == (char)Keys.Escape)
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BtnClick != null)
                BtnClick.Invoke(sender, e);
        }
    }
}
