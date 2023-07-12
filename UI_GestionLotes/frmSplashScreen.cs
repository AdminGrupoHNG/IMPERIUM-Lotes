using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_GestionLotes;
using BL_GestionLotes;
using System.Xml;

namespace UI_GestionLotes
{
    public partial class frmSplashScreen : DevExpress.XtraEditors.XtraForm
    {
        blEncrypta blEncryp;

        public frmSplashScreen()
        {
            InitializeComponent();
            blEncryp = new blEncrypta(Program.Sesion.Key);
        }
        private int[] IntColor(string color)
        { return blEncryp.Desencrypta(ConfigurationManager.AppSettings[color]).ToString().Split(',').Select(n => Convert.ToInt32(n)).ToArray(); }


        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            /*  Cargar valores del entorno. */
            Asignar_VariablesGlobales();
            /*  Cargar colores en la sesión.*/
            Program.Sesion.Colores = new eColor(IntColor(blEncryp.Encrypta("colorVerde")), IntColor(blEncryp.Encrypta("colorPlomo")), IntColor(blEncryp.Encrypta("colorEventRow")), IntColor(blEncryp.Encrypta("colorFocus")));
            panel2.BackColor = Program.Sesion.Colores.Verde;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 5;
            if (panel2.Width >= 700)
            {
                timer1.Stop();
                ValidarVersion();
            }
        }


        private void ValidarVersion()
        {
            this.Hide();
            if (Program.Sesion.Global.VersionApp == Program.Sesion.Version)
                new frmPrincipal().ShowDialog();
            else
            {
                frmAlertaVersion frmversion = new frmAlertaVersion();
                frmversion.lblVersion.Text = "Versión " + Program.Sesion.Version;
                frmversion.ShowDialog();
            }

        }
        private void Asignar_VariablesGlobales()
        {
            var eGlobal = new eGlobales();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", "").Replace("Config", "config"));
            //xmlDoc.Load("C:\\SG5-Software\\kq_SG5_Controlador.exe.config");
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;
                        switch (blEncryp.Desencrypta(node.Attributes[0].Value))
                        {
                            case "conexion": eGlobal.Entorno = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "ServidorLOCAL": eGlobal.ServidorLOCAL = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "ServidorREMOTO": eGlobal.ServidorREMOTO = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "BBDD": eGlobal.BBDD = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "FormatoFecha": eGlobal.FormatoFecha = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "SeparadorListas": eGlobal.SeparadorListas = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "SeparadorDecimal": eGlobal.SeparadorDecimal = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            //case "UltimoLocalidad": eGlobal.UltimoLocalidad = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "UltimaEmpresa": eGlobal.UltimaEmpresa = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "UltimoUsuario": eGlobal.UltimoUsuario = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                            case "VersionApp": eGlobal.VersionApp = blEncryp.Desencrypta(node.Attributes[1].Value); break;
                        }
                    }
                }
            }
            Program.Sesion.Global = new eGlobales();
            Program.Sesion.Global = eGlobal;
        }

    }
}