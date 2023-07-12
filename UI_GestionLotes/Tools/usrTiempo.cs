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
    public partial class usrTiempo : DevExpress.XtraEditors.XtraUserControl
    {
        Timer timer = new Timer();
        public usrTiempo()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += OnTick;
            timer.Start();
            OnTick(null, null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                DisableTimer();
            }
            base.Dispose(disposing);
        }

        void DisableTimer()
        {
            timer.Stop();
            timer = null;
        }

        void OnTick(object sender, EventArgs e)
        {
            if (IsDisposed)
            {
                DisableTimer();
                return;
            }
            System.DateTime currentDate = System.DateTime.Now;
            //labelControl1.Text = "<b>" + string.Format("{0:T}", currentDate) + "</b><br><size=10>" + currentDate.ToString("D");
            labelControl1.Text = "Proximamente";
        }
    }
}
