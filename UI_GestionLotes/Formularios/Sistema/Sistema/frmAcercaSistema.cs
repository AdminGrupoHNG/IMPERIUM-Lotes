using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Configuration;
using BL_GestionLotes;
using BE_GestionLotes;

namespace UI_GestionLotes.Formularios.Sistema.Sistema
{
    public partial class frmAcercaSistema : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        

        public frmAcercaSistema()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmAcercaSistema_Load_1(object sender, EventArgs e)
        {
            groupBox1.BackColor = Program.Sesion.Colores.Verde;
            groupBox2.BackColor = Program.Sesion.Colores.Plomo;

            lblHostName.Text = Environment.MachineName;
            lblUsuarioWindows.Text = Environment.UserName;
            lblNombreDominio.Text = Environment.UserDomainName;
            lblIPAddress.Text = ObtenerIP();
            lblMemoriaRAM.Text = PerformanceInfo.GetTotalMemoryInMiB().ToString() + " GB";

          
            try {
                lblModo.Text = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
                lblServidor.Text = lblModo.Text == "LOCAL" ? unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString()) : unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
                lblIPServidor.Text = lblModo.Text == "LOCAL" ? unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString()) : unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
                //lblEmpresa.Text = ObtenerEmpresa(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UltimaEmpresa")].ToString()));
                lblBaseDatos.Text = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
                lblVersion.Text = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("VersionApp")].ToString());
            } catch
            {
                lblModo.Text = ConfigurationManager.AppSettings["conexion"].ToString();
                lblServidor.Text = lblModo.Text == "LOCAL" ? ConfigurationManager.AppSettings["ServidorLOCAL"].ToString() : ConfigurationManager.AppSettings["ServidorREMOTO"].ToString(); 
                lblIPServidor.Text = lblModo.Text == "LOCAL" ? ConfigurationManager.AppSettings["ServidorLOCAL"].ToString() : ConfigurationManager.AppSettings["ServidorREMOTO"].ToString();
                //lblEmpresa.Text = ObtenerEmpresa(ConfigurationManager.AppSettings["UltimaEmpresa"].ToString());
                lblBaseDatos.Text = ConfigurationManager.AppSettings["BBDD"].ToString();
                lblVersion.Text=ConfigurationManager.AppSettings["VersionApp"].ToString();
            }


           
        }
        public string ObtenerEmpresa(string cod_empresa)
        {
            string NombreEmpresa = "";
            List<eSistema> eSist = unit.Sistema.Obtener_ParamterosSistema<eSistema>(4, cod_empresa:cod_empresa);
            if (eSist.Count > 0) { 
            NombreEmpresa = eSist[0].dsc_valor;
            }
            return NombreEmpresa;
        } 
        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            //Process.Start(hyperlinkLabelControl1.Text);
        }

        public static class PerformanceInfo
        {
            [DllImport("psapi.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

            [StructLayout(LayoutKind.Sequential)]
            public struct PerformanceInformation
            {
                public int Size;
                public IntPtr CommitTotal;
                public IntPtr CommitLimit;
                public IntPtr CommitPeak;
                public IntPtr PhysicalTotal;
                public IntPtr PhysicalAvailable;
                public IntPtr SystemCache;
                public IntPtr KernelTotal;
                public IntPtr KernelPaged;
                public IntPtr KernelNonPaged;
                public IntPtr PageSize;
                public int HandlesCount;
                public int ProcessCount;
                public int ThreadCount;
            }

            public static Int64 GetTotalMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64(Math.Round((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / Convert.ToDecimal(1073741824)), 0));
                }
                else
                {
                    return -1;
                }
            }
        }

        private string ObtenerIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        private void frmAcercaSistema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}