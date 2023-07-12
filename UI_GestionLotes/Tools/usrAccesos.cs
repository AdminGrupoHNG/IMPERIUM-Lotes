using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using BE_GestionLotes;
using BL_GestionLotes;

namespace UI_GestionLotes.UserControls
{
    public partial class usrAccesos : DevExpress.XtraEditors.XtraUserControl
    {

        public usrAccesos()
        {
            InitializeComponent();
        }

        private void picImagen1_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "https://www.office.com/";
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void picImagen2_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "https://www.sunat.gob.pe/sol.html";
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void picImagen3_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "https://onedrive.live.com/about/es-419/signin/";
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }
    }
}
