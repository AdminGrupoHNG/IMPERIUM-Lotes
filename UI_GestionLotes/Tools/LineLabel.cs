using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Tools
{
    internal class LineLabel : Label
    {
        private Label _line;
        private System.Drawing.Color _textColor = System.Drawing.Color.FromArgb(64, 64, 64);
        public override Color ForeColor { get => _textColor; set { _textColor = value; base.ForeColor = value; _line.BackColor = value; } }
        public LineLabel()
        {
            _line = new Label()
            {
                Dock = DockStyle.Bottom,
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                Text = "",
                Height = 2
            };
            this.Controls.Add(_line);
        }
    }
}
