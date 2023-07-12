using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Configuration;
using System.Globalization;
using System.Net;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using BE_GestionLotes;
using BL_GestionLotes;
using UI_GestionLotes.Formularios.Clientes_Y_Proveedores.Proveedores;
using DevExpress.Utils;
using System.Xml;
using UI_GestionLotes.Formularios.Sistema.Accesos;
using UI_GestionLotes.Formularios.Sistema.Sistema;
using UI_GestionLotes.Formularios.Sistema.Configuracion_del_Sistema;
using UI_GestionLotes.Formularios.Sistema.Configuraciones_Maestras;
using System.IO;
using UI_GestionLotes.Formularios.Lotes;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;
using UI_GestionLotes.Formularios.Operaciones;
using UI_GestionLotes.Formularios.Gestion_Lotes;
using UI_GestionLotes.Formularios.Gestion_Contratos;
using UI_GestionLotes.Formularios.Documento;
using UI_GestionLotes.Formularios.Marketing;
using System.Diagnostics;

namespace UI_GestionLotes
{
    public partial class frmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        public string cod_empresa = "", Entorno = "LOCAL", Servidor = "", BBDD = "", FormatoFecha = "";
        public string formName;
        private Timer timerLimpiarArchivos;

        public frmPrincipal()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            frmDashBoardPrincipal frm = new frmDashBoardPrincipal();
            frm.MdiParent = this;
            frm.Show();
            InhabilitarBotones();
            Inicializar();
            HabilitarBotones();
            btnEliminarExportados.Enabled = true;

            //bbi_Campania.Enabled = true;
            //bbi_AsigProspecto.Enabled = true;
            //bbi_Prospecto.Enabled = true;

            CrearSoluciones();

            //LimpiarArchivosTemporales();

            //timerLimpiarArchivos = new Timer();
            //timerLimpiarArchivos.Interval = 3600000; // Intervalo de una hora (3600000 ms)
            //timerLimpiarArchivos.Tick += TimerLimpiarArchivos_Tick;
            //timerLimpiarArchivos.Enabled = true;
        }
        #region Espacio de Soluciones
        void CrearSoluciones()
        {
            var modulos = unit.Sistema.ListarSolucion<eSolucionUsuario_Consulta>(
               opcion: 1, cod_usuario: Program.Sesion.Usuario.cod_usuario);
            if (modulos != null && modulos.Count > 0)
            {
                Program.Sesion.CrearButtonModulos(modulos, rpgSolucion);
            }
        }
        #endregion

        private void Inicializar()
        {
            string IP = ObtenerIP();
            //ObtenerUsuario();

            Entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            string Servidor = Entorno == "LOCAL" ? unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString()) : unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            string BBDD = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            string Version = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("VersionApp")].ToString());
            string nombrePC = Environment.MachineName;
            lblServidor.Caption = "Conectado a -> " + Servidor + " - " + BBDD;
            lblIPAddress.Caption = "IP : " + IP;
            lblHostName.Caption = "Nombre Equipo : " + nombrePC;
            lblVersion.Caption = "Versión: " + Version;
            lblUsuario.Caption = Program.Sesion.Usuario.dsc_usuario.ToUpper();
            //entorno = Entorno;
            switch (Entorno)
            {
                case "LOCAL": lblEntorno.Caption = "LOCAL"; lblEntorno.ItemAppearance.Normal.BackColor = Color.Green; lblEntorno.ItemAppearance.Normal.ForeColor = Color.White; break;
                case "REMOTO": lblEntorno.Caption = "REMOTO"; lblEntorno.ItemAppearance.Normal.BackColor = Color.DarkKhaki; lblEntorno.ItemAppearance.Normal.ForeColor = Color.Black; break;
            }
            lblEntorno.Caption = Entorno;
            SuperToolTip tool = new SuperToolTip();
            SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();
            args.Contents.Text = Servidor + " -> " + BBDD;
            tool.Setup(args);
            lblServidor.SuperTip = tool;
        }
        private void ObtenerUsuario()
        {
            Program.Sesion.Usuario = unit.Usuario.ObtenerUsuarioLogin<eUsuario>(1, Program.Sesion.Usuario.cod_usuario);
        }

        private void TimerLimpiarArchivos_Tick(object sender, EventArgs e)
        {
            LimpiarArchivosTemporales();
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

        private void InhabilitarBotones()
        {
            foreach (var item in ribbon.Items)
            {
                if (item.GetType() == typeof(BarButtonItem))
                {
                    if (((BarButtonItem)item).Name != "btnCambiarContraseña" && ((BarButtonItem)item).Name != "btnHistorialVersiones" &&
                        ((BarButtonItem)item).Name != "btnAcercaDeSistema")
                    {
                        ((BarButtonItem)item).Enabled = false;
                    }
                }
            }
        }



        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, null, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                for (int i = 0; i < listPermisos.Count; i++)
                {
                    foreach (var item in ribbon.Items)
                    {
                        if (item.GetType() == typeof(BarButtonItem))
                        {
                            if (((BarButtonItem)item).Name == listPermisos[i].dsc_menu)
                            {
                                ((BarButtonItem)item).Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void LimpiarArchivosTemporales()
        {
            string carpetasAEliminar =  "%temp%" ;

            //foreach (string carpeta in carpetasAEliminar)
            //{
                string rutaCarpeta = Environment.ExpandEnvironmentVariables(carpetasAEliminar);

                if (Directory.Exists(rutaCarpeta))
                {
                        string[] archivos = Directory.GetFiles(rutaCarpeta);

                    foreach (string archivo in archivos)
                    {
                        try
                        {
                            File.SetAttributes(archivo, FileAttributes.Normal);
                            File.Delete(archivo);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"No se pudo eliminar el archivo {archivo}. Error: {ex.Message}");
                        }
                    }

                }
            //}
        }



        private void btnListadoUsuario_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaUsuarios";
            if (Application.OpenForms["frmListadoUsuario"] != null)
            {
                Application.OpenForms["frmListadoUsuario"].Activate();
            }
            else
            {
                frmListadoUsuario frm = new frmListadoUsuario();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnOpcionesSistema_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaOpcionesSistema";
            if (Application.OpenForms["frmOpcionesSistema"] != null)
            {
                Application.OpenForms["frmOpcionesSistema"].Activate();
            }
            else
            {
                frmOpcionesSistema frm = new frmOpcionesSistema();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnAsignacionPermiso_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaPermisos";
            if (Application.OpenForms["frmAsignacionPermiso"] != null)
            {
                Application.OpenForms["frmAsignacionPermiso"].Activate();
            }
            else
            {
                frmAsignacionPermiso frm = new frmAsignacionPermiso();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnUndNegocioTipoGastoCostoEmpresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantUnidades_Negocio"] != null)
            {
                Application.OpenForms["frmMantUnidades_Negocio"].Activate();
            }
            else
            {
                frmMantUnidades_Negocio frm = new frmMantUnidades_Negocio();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnTipoCambio_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantTipoCambio"] != null)
            {
                Application.OpenForms["frmMantTipoCambio"].Activate();
            }
            else
            {
                frmMantTipoCambio frm = new frmMantTipoCambio();
                frm.ShowDialog();
            }
        }

        private void btnTipoGastoCosto_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantTipoGastoCosto"] != null)
            {
                Application.OpenForms["frmMantTipoGastoCosto"].Activate();
            }
            else
            {
                frmMantTipoGastoCosto frm = new frmMantTipoGastoCosto();
                //frm.MdiParent = this;
                frm.ShowDialog();
            }
        }

        private void btnEliminarExportados_ItemClick(object sender, ItemClickEventArgs e)
        {
            //OBTENEMOS LA RUTA DONDE ESTAN LOS ARCHIVOS DESCARGADOS
            var carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
            DirectoryInfo source = new DirectoryInfo(carpeta);
            FileInfo[] filesToCopy = source.GetFiles();
            foreach (FileInfo oFile in filesToCopy)
            {
                oFile.Delete();
            }
            MessageBox.Show("Se procedió a eliminar los archivos exportados del sistema", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHistorialVersiones_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "HistorialVersiones";
            if (Application.OpenForms["frmHistorialVersiones"] != null)
            {
                Application.OpenForms["frmHistorialVersiones"].Activate();
            }
            else
            {
                frmHistorialVersiones frm = new frmHistorialVersiones();
                frm.ShowDialog();
            }
        }

        private void btnConfiguracionCampania_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Campanha";
            if (Application.OpenForms["frmListadoCampanha"] != null)
            {
                Application.OpenForms["frmListadoCampanha"].Activate();
            }
            else
            {
                frmListadocampanha frm = new frmListadocampanha();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoProspecto_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListadoProspecto";
            if (Application.OpenForms["frmListadoProspecto"] != null)
            {
                Application.OpenForms["frmListadoProspecto"].Activate();
            }
            else
            {
                frmListadoProspecto frm = new frmListadoProspecto();
                if (Program.IsMinimized)
                {
                    frm.MdiParent = this;
                    Image img = Properties.Resources.no_reminders_32px;
                    frm.btnNotificacion.ImageOptions.LargeImage = img;
                    frm.btnNotificacion.Caption = "Desactivar Notificaciones";
                    frm.Show();
                }
                else
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
            }
        }

        private void btnAsignacionProspecto_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "AsigProspecto";
            if (Application.OpenForms["frmListadoAsigProspecto"] != null)
            {
                Application.OpenForms["frmListadoAsigProspecto"].Activate();
            }
            else
            {
                frmListadoAsigProspecto frm = new frmListadoAsigProspecto();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoProyectos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaProyectos";
            if (Application.OpenForms["frmListadoProyectos"] != null)
            {
                Application.OpenForms["frmListadoProyectos"].Activate();
            }
            else
            {
                frmListadoProyectos frm = new frmListadoProyectos();
                frm.MdiParent = this;
                frm.Show();
                frm.Activate();
            }
        }

        private void btnRegistroProyectos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Proyectos";

            if (Application.OpenForms["frmMantProyecto"] != null)
            {
                Application.OpenForms["frmMantProyecto"].Activate();
            }
            else
            {
                frmMantProyecto frm = new frmMantProyecto();
                frm.MiAccion = Proyecto.Nuevo;
                frm.ShowDialog();
            }
        }

        private void btnListadoControlLote_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaControlLotes";
            if (Application.OpenForms["frmListadoControlLotes"] != null)
            {
                Application.OpenForms["frmListadoControlLotes"].Activate();
            }
            else
            {
                frmListadoControlLotes frm = new frmListadoControlLotes();
                frm.MdiParent = this;
                frm.Show();
                frm.Activate();
            }
        }

        private void btnSeparacionLotes_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "LotesProspectos";
            if (Application.OpenForms["frmSeparacionLotesProspectos"] != null)
            {
                Application.OpenForms["frmSeparacionLotesProspectos"].Activate();
            }
            else
            {
                frmSeparacionLotesProspectos frm = new frmSeparacionLotesProspectos();
                frm.MdiParent = this;
                frm.Show();
                frm.Activate();
            }
        }

        private void ribbon_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            e.MergeOwner.SelectedPage = e.MergeOwner.MergedPages.GetPageByName(e.MergedChild.SelectedPage.Name);
        }

        private void btnListadoCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaClientes";
            if (Application.OpenForms["frmListadoClientes"] != null)
            {
                Application.OpenForms["frmListadoClientes"].Activate();
            }
            else
            {
                frmListadoClientes frm = new frmListadoClientes();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnRegistroCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Clientes";
            if (Application.OpenForms["frmMantCliente"] != null)
            {
                Application.OpenForms["frmMantCliente"].Activate();
            }
            else
            {
                frmMantCliente frm = new frmMantCliente();
                frm.ShowDialog();
            }
        }

        private void btnCotizacionLotes_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmListarCotizacion"] != null)
            {
                Application.OpenForms["frmListarCotizacion"].Activate();
            }
            else
            {
                frmListarCotizacion frm = new frmListarCotizacion();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnVisorGeneralLotes_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (Application.OpenForms["frmVisorLotes"] != null)
            {
                Application.OpenForms["frmVisorLotes"].Activate();
            }
            else
            {
                frmVisorLotes frm = new frmVisorLotes();
                //frm.MiAccion = Proyecto.Nuevo;
                frm.MdiParent = this;
                frm.Show();
                //frm.Activate();
            }
        }

        private void btnListadoContratos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaContratos";
            if (Application.OpenForms["frmListadoContratos"] != null)
            {
                Application.OpenForms["frmListadoContratos"].Activate();
            }
            else
            {
                frmListadoContratos frm = new frmListadoContratos();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnEmisionContratos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Contratos";

            if (Application.OpenForms["frmMantContratos"] != null)
            {
                Application.OpenForms["frmMantContratos"].Activate();
            }
            else
            {
                frmMantContratos frm = new frmMantContratos();
                //frm.MiAccion = Proyecto.Nuevo;
                frm.ShowDialog();
            }
        }

        private void btnConfigurarDocumentos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "FormatoMD_Vinculo";
            if (Application.OpenForms["frmFormatoMD_Vinculo"] != null)
            {
                Application.OpenForms["frmFormatoMD_Vinculo"].Activate();
            }
            else
            {
                frmFormatoMD_Vinculo frm = new frmFormatoMD_Vinculo();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnReporteProspectos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaReportes";
            if (Application.OpenForms["frmListadoReporteProspecto"] != null)
            {
                Application.OpenForms["frmListadoReporteProspecto"].Activate();
            }
            else
            {
                frmListadoReporteProspecto frm = new frmListadoReporteProspecto();
                frm.MdiParent = this;
                //frm.MiAccion = Proyecto.Nuevo;
                frm.Show();
            }
        }

        private void btnReporteCitasAsistidas_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaReportas";
            if (Application.OpenForms["frmListReportCitasAsis"] != null)
            {
                Application.OpenForms["frmListReportCitasAsis"].Activate();
            }
            else
            {
                frmListReportCitasAsis frm = new frmListReportCitasAsis();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoProveedores_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaProveedores";
            if (Application.OpenForms["frmListadoProveedores"] != null)
            {
                Application.OpenForms["frmListadoProveedores"].Activate();
            }
            else
            {
                frmListadoProveedores frm = new frmListadoProveedores();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnRegistroProveedores_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Proveedores";
            if (Application.OpenForms["frmMantProveedor"] != null)
            {
                Application.OpenForms["frmMantProveedor"].Activate();
            }
            else
            {
                frmMantProveedor frm = new frmMantProveedor();
                frm.ShowDialog();
            }
        }

        private void btnCambiarContraseña_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCambiarContraseña frm = new frmCambiarContraseña();
            //
            //
            //
            //
            frm.Show();
        }

        private void btnAcercaDeSistema_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmAcercaSistema frm = new frmAcercaSistema();
            frm.ShowDialog();
        }
    }
}