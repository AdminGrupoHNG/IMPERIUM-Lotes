using DevExpress.XtraReports.Design;
using DevExpress.XtraSplashScreen;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Tools;

namespace UI_GestionLotes
{
    public class SplashScreen
    {
        private static Thread t;
        private static string[] text;
        public static void Open(string stringTitle, string stringSubTitle = "Cargando...")
        {
            text = new string[] { stringTitle, stringSubTitle };
            t = new Thread(new ThreadStart(StartForm)); t.Start();
        }

        private static void StartForm()
        {
            try { Application.Run(new SplashScreenForm() { Title = text[0], SubTitle = text[1] });  }
            catch (Exception) { Close();  }
        }
        public static void Close() { t.Abort(); }
    }
}
