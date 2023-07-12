using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UI_GestionLotes.Tools.ToolHelper;

namespace UI_GestionLotes.Tools
{
    public partial class SplashScreenForm : DevExpress.XtraEditors.XtraForm
    {
        public SplashScreenForm()
        {
            InitializeComponent();
        }
        public string Title { get { return labelStatus.Text; } set { labelStatus.Text = value; } }
        public string SubTitle { get { return labelCopyright.Text; } set { labelCopyright.Text = value; } }
        private void SplashScreenForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == (char)Keys.Escape)
                e.Handled = true;
        }
        private void labelStatus_MouseDown(object sender, MouseEventArgs e) { MoveControls.Form(this); }
        internal void Exit() { try { if (this != null) this.Dispose(); } catch (Exception) { } }
    }
}