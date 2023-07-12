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
    public partial class ItemCardLotes : DevExpress.XtraEditors.XtraUserControl
    {
        [Browsable(true), Localizable(true), Category("HNG Style")]
        public string DscLote { get { return lblDscLote.Text; } set { lblDscLote.Text = value; } }
        [Browsable(true), Localizable(true), Category("HNG Style")]
        public string UEMts { get { return lblUSoMts.Text; } set { lblUSoMts.Text = value; } }
        [Browsable(true), Localizable(true), Category("HNG Style")]
        public string PrecioTot { get { return lblPrecioTot.Text; } set { lblPrecioTot.Text = value; } }
        [Browsable(true), Localizable(true), Category("HNG Style")]
        public Color Semaforo { get { return lblDscLote.AppearanceItemCaption.BackColor; } set { lblDscLote.AppearanceItemCaption.BackColor = value; } }


        [Browsable(true), Localizable(true), Category("HNG Style")]
        public event EventHandler Clicks;
        public ItemCardLotes()
        {
            InitializeComponent();
        }


        private int _borderRadius = 6;
        private int _borderSize = 2;
        private Color _borderColor = Color.Black;


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);



            Graphics graphics = e.Graphics;
            if (_borderRadius > 1 && _borderSize > 0)
            {
                var rectangleBorderSmooth = this.ClientRectangle;
                var rectangleBorder = Rectangle.Inflate(rectangleBorderSmooth, -_borderSize, -_borderSize);
                int smoothSize = _borderSize > 0 ? _borderSize : 1;
                using (GraphicsPath pathSmooth = GetGraphicsPath(rectangleBorderSmooth, _borderRadius))
                using (GraphicsPath pathBorder = GetGraphicsPath(rectangleBorder, (_borderRadius - _borderSize)))
                using (Pen penSmooth = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(_borderColor, _borderSize))
                {
                    this.Region = new Region(pathSmooth);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;



                    graphics.DrawPath(penSmooth, pathSmooth);
                    graphics.DrawPath(penBorder, pathBorder);
                }
            }
        }
        public GraphicsPath GetGraphicsPath(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rectangle.X, rectangle.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rectangle.Right - curveSize, rectangle.Y, curveSize, curveSize, 270, 90);
            path.AddArc((rectangle.Right - curveSize), (rectangle.Bottom - curveSize), curveSize, curveSize, 0, 90);
            path.AddArc(rectangle.X, (rectangle.Bottom - curveSize), curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }


        private void lblDscLote_Click(object sender, EventArgs e)
        {
            if (Clicks != null)
                Clicks.Invoke(sender, e);
        }
    }
}
