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
using BE_GestionLotes;
using BL_GestionLotes;
using System.Diagnostics;
using System.Configuration;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace UI_GestionLotes
{
    public partial class frmAlertaVersion : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public frmLogin frmHandler = new frmLogin();
        eVersion eVersion = new eVersion();
        public string Entorno = "";
        //public eVersion objDescargaOrigen;
        
        
        public frmAlertaVersion()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        public frmAlertaVersion(frmLogin frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmAlertaVersion_Load(object sender, EventArgs e)
        {
            //layoutControl1.BackColor = Program.Sesion.Colores.Verde;
            Entorno = "REMOTO"; //ConfigurationManager.AppSettings["conexion"];
            if (Entorno == "REMOTO") lblTipoActualizacion.Text = "Actualización remota";
            //else { lblTipoActualizacion.Text = "Actualización local"; }
                bsAlerta.DataSource = null;
            //bsAlerta.DataSource = blVersion.ObtenerListaDetalle<eVersion>(lblVersion.Text);

            CargarHistorialVersiones(lblVersion.Text.Replace("Versión ", ""));
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                lblTipoActualizacion.Text = "Actualizando, por favor espere... ";
                //lblTipoActualizacion.ForeColor = Color.Red ;
                btnAceptar.Enabled = false;
                btnAceptar.Appearance.ForeColor = System.Drawing.Color.Gray;

                //Eliminar carpeta DESCARGAS
                if (Directory.Exists(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas"))
                {
                    System.IO.Directory.Delete(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas", true);
                }

                //Crear archivo de lotes
                if (File.Exists(@"C:\IMPERIUM-Software\Soluciones\LOTES\ActualizaIMPERIUM-GestionLotes.bat"))
                {
                    File.Delete(@"C:\IMPERIUM-Software\Soluciones\LOTES\ActualizaIMPERIUM-GestionLotes.bat");
                }

                string Ejecutable = @"C:\IMPERIUM-Software\Soluciones\LOTES\Imperium-GestionLotes.exe " + Program.SolucionAbrir.Solucion + ", " + Program.SolucionAbrir.Token + ", " + Program.SolucionAbrir.User + ", " + Program.SolucionAbrir.Key + ", " + Program.SolucionAbrir.Entorno;
                string CD = @"CD \ ";
                string ArchivoBAT = "";

                if (Entorno == "REMOTO")
                {
                    string LineaCopia = @"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas C:\IMPERIUM-Software\Soluciones\LOTES";
                    string LineaBorra = @"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas";

                    //ArchivoBAT = "ECHO OFF \nECHO Copiando archivos del sistema...\nTASKKILL /F /IM Imperium-Software.exe\nC:\n" + CD + "\nXCOPY " + LineaCopia + " /s/y/d\nRD " + LineaBorra + " /S /Q\nECHO Ejecutando el sistema...\nSTART " + Ejecutable + "\nEXIT";
                    ArchivoBAT = "ECHO OFF \nECHO Copiando archivos del sistema...\nTASKKILL /F /IM Imperium-GestionLotes.exe\nC:\n" + CD + "\nXCOPY " + LineaCopia + " /s/y\nRD /S /Q " + LineaBorra 
                        + "\nECHO Ejecutando el sistema...\nSTART " + Ejecutable + " \nDEL /F /Q /S " + @"C:\IMPERIUM-Software\Soluciones\LOTES\ActualizaIMPERIUM-GestionLotes.bat" + "\nEXIT";
                }
                //else
                //{
                //    string LineaCopia = @"\\sl-limfs01\NSV-COLTUR\Sistema C:\IMPERIUM-Software";


                //    ArchivoBAT = "ECHO OFF \nECHO Copiando archivos del sistema...\nTASKKILL /F /IM Imperium-Sonftware.exe\nC:\n" + CD + "\nXCOPY " + LineaCopia + " /s/y/d\nECHO Ejecutando el sistema...\nSTART " + Ejecutable + "\nEXIT";
                //}

                File.WriteAllText(@"C:\IMPERIUM-Software\Soluciones\LOTES\ActualizaIMPERIUM-GestionLotes.bat", ArchivoBAT);


                if (Entorno == "REMOTO")
                {
                    WebClient webClient = new WebClient();

                    if (!Directory.Exists(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas"))
                    {
                        Directory.CreateDirectory(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas");
                    }

                    if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\IMPERIUM-GestionLotes.zip"))
                    {
                        System.IO.File.Delete(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\IMPERIUM-GestionLotes.zip");
                    }

                    //objDescargaOrigen = unit.Version.ObtenerVersion<eVersion>(6);
                    string DescargaOrigen = "";
                    //if (objDescargaOrigen != null) DescargaOrigen = objDescargaOrigen.OrigenDescarga;
                    if (Program.Sesion.RutaDescarga != "") DescargaOrigen = Program.Sesion.RutaDescarga;

                    if (DescargaOrigen != "")
                    {
                        webClient.DownloadFileAsync(new Uri(DescargaOrigen), @"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\IMPERIUM-GestionLotes.zip");
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completado);
                        webClient.DownloadProgressChanged += Wc_DownloadProgressChanged;
                    }
                }
                //else //Produccion, Desarrollo, QA
                //{
                //    frmPrincipal frmMain = new frmPrincipal();
                //    this.Close();
                //    frmMain.Close();
                //    Process.Start(ConfigurationManager.AppSettings["rutaBatActualiza"].ToString());

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontro el sitio web " + Program.Sesion.RutaDescarga + Environment.NewLine + ex.ToString(), "Acceso no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                frmHandler.Close();
            }
        }
        private void Wc_DownloadProgressChanged(object sender,DownloadProgressChangedEventArgs e)
        {
            progressBarActualizado.EditValue = e.ProgressPercentage;
        }

        public void Completado(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\IMPERIUM-GestionLotes.zip"))
                {
                    ZipFile.ExtractToDirectory(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\IMPERIUM-GestionLotes.zip", @"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas");
                }
                if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\IMPERIUM-GestionLotes.zip"))
                {
                    System.IO.File.Delete(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\IMPERIUM-GestionLotes.zip");
                }
                if (System.IO.File.Exists(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\ActualizaIMPERIUM-GestionLotes.bat"))
                {
                    System.IO.File.Delete(@"C:\IMPERIUM-Software\Soluciones\LOTES\Descargas\ActualizaIMPERIUM-GestionLotes.bat");
                }
                this.Close();
                //frmHandler.Close();
                Process.Start(@"C:\IMPERIUM-Software\Soluciones\LOTES\ActualizaIMPERIUM-GestionLotes.bat");
            }
            catch(Exception ex)
            {
                MessageBox.Show("No se encontro el sitio web "+ Program.Sesion.RutaDescarga + Environment.NewLine + ex.ToString(), "Acceso no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                frmHandler.Close();
            }
        }

        private void CargarHistorialVersiones(string version)
        {
            List<eVersion.eVersionDetalle> histVersion = unit.Version.Cargar_HistorialVersiones_Detalle<eVersion.eVersionDetalle>(3, 0, version, Program.SolucionAbrir.Solucion);

            bsListadoHistorialDetalle.DataSource = histVersion;
        }

        private void gvHistorialVersiones_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvHistorialVersiones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvHistorialVersiones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvHistorialVersiones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
    }
}