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
using DevExpress.XtraGrid.Views.Grid;
using UI_GestionLotes.Formularios.Clientes_Y_Proveedores.Clientes;
using System.Configuration;
using DevExpress.XtraSplashScreen;
using UI_GestionLotes.Formularios.Marketing;
using System.ComponentModel.DataAnnotations;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;
using DevExpress.CodeParser;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Runtime.InteropServices;
using iTextSharp.text.pdf.codec.wmf;
using DevExpress.XtraBars.Navigation;

namespace UI_GestionLotes.Formularios.Lotes
{
    internal enum evento
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2,
        Nuevo_externo = 3
    }
    public partial class frmMantEvento : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        Brush SinCriterios = Brushes.Blue;
        Brush NAplCriterio = Brushes.Yellow;
        Brush ConCriterios = Brushes.Green;
        System.Drawing.Image ImgCircleGreen = Properties.Resources.green_circle_16px;
        System.Drawing.Image ImgCircleBlue = Properties.Resources.blue_circle_16px;
        System.Drawing.Image ImgCircleYellow = Properties.Resources.yellow_circle_16px;
        System.Drawing.Image ImgCircleRed = Properties.Resources.red_circle_16px;
        Brush Mensaje = Brushes.Transparent;
        int markWidth = 16;
        frmListadoProspecto frmHandler;
        public eUsuario user = new eUsuario();
        internal evento MiAccion = evento.Nuevo;
        List<eCampanha> Listeventos = new List<eCampanha>();
        List<eCampanha> Listasignaciones = new List<eCampanha>();
        List<eCampanha> Listcampanhas = new List<eCampanha>();
        List<eVentana> listPerfil = new List<eVentana>();

        public eCampanha o_eCamp;
        public eProspectosXLote o_eProspecto;

        private static IEnumerable<eCampanha> lstDetalleRespuesta;
        public string cod_empresa = "", cod_proyecto = "", cod_prospecto = "", cod_evento = "", cod_tipo_evento = "", cod_eventoProximo = "", cod_evento_confirmacion = "", cod_ejecutivo = "", cod_tipo_contacto = "", cod_respuesta = "", cod_expectativa = "", cod_tipo_contacto_2 = "", cod_estado_prospecto = "", cod_estado_prospecto_mascara = "", cod_tipo_confirmacion = "", cod_evento_principal = "", flg_habilitado = "NO";
        public string ActualizarListado = "NO", cod_IndProximo = "NO", cod_IndGrupo = "", IndicadorConfirmacionAuto = "";

        DataTable odtTipoContacto = new DataTable();
        DataTable odtBotonResultado = new DataTable();

        public string sEstadoFiltro = "", sTipoContactoFiltro = "", sCod_ejecutivoFiltro = "", CodMenu, DscMenu, sIndCalendario = "";
        public int validateConfirLlamada = 0, validateConfirWSP = 0, validateConfirMensaje = 0, validar = 0;
        public string codTipoEventoProspecto = "", codTipoProximoEventoProspecto = "", codTipoConfirmacionEventoProspecto = "";
        public string codEstadoProspectoNoAsignado = "", codEstadoProspectoAsignado = "", codEstadoProspectoEnProceso = "", codEstadoProspectoCerrado = "", codEstadoProspectoCliente = "";
        public string codResultadoEventoProspectoExitoso = "", codResultadoEventoProspectoSinRespuesta = "", codResultadoEventoProspectoContactoInvalido = "", codResultadoEventoProspectoAsistio = "", codResultadoEventoProspectoNoAsistio = "", codResultadoEventoProspectoPendiente = "", codResultadoEventoProspectoReconfirmacion = "";
        public string codTipoContactoProspectoCita = "", codTipoContactoProspectoVisita = "", codTipoContactoProspectoPresencial = "", codTipoContactoProspectoLlamada = "", codTipoContactoProspectoCorreo = "", codTipoContactoProspectoWhatsApp = "", codTipoContactoProspectoVideoLlamada = "";
        public string codEstadoProspectoDetalleResultadoContacto = "";
        public string codExpectativaProspecto = "", codExpectativaProspectoBaja = "", codExpectativaProspectoMedia = "", codExpectativaProspectoAlta = "", codExpectativaProspectoMuyAlta = "";
        public string sIndicadorCarga = "0";
        public int validarCargarListado = 0;
        //public string perfil = "";
        public bool btnControl = true;
        List<eCampanha> ListCalendario = new List<eCampanha>();
        List<eCampanha> ListTemp = new List<eCampanha>();
        List<SimpleButton> lstBotonesDinamicos = new List<SimpleButton>();

        public frmMantEvento()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        internal frmMantEvento(frmListadoProspecto frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmMantEvento_Load(object sender, EventArgs e)
        {
            codTipoEventoProspecto = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoEventoProspecto")].ToString());
            codTipoProximoEventoProspecto = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoProximoEventoProspecto")].ToString());
            codTipoConfirmacionEventoProspecto = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoConfirmacionEventoProspecto")].ToString());

            codEstadoProspectoNoAsignado = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoNoAsignado")].ToString());
            codEstadoProspectoAsignado = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoAsignado")].ToString());
            codEstadoProspectoEnProceso = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoEnProceso")].ToString());
            codEstadoProspectoCerrado = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoCerrado")].ToString());
            codEstadoProspectoCliente = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoCliente")].ToString());

            codResultadoEventoProspectoExitoso = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codResultadoEventoProspectoExitoso")].ToString());
            codResultadoEventoProspectoSinRespuesta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codResultadoEventoProspectoSinRespuesta")].ToString());
            codResultadoEventoProspectoContactoInvalido = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codResultadoEventoProspectoContactoInvalido")].ToString());
            codResultadoEventoProspectoAsistio = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codResultadoEventoProspectoAsistio")].ToString());
            codResultadoEventoProspectoNoAsistio = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codResultadoEventoProspectoNoAsistio")].ToString());
            codResultadoEventoProspectoPendiente = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codResultadoEventoProspectoPendiente")].ToString());
            codResultadoEventoProspectoReconfirmacion = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codResultadoEventoProspectoReconfirmacion")].ToString());

            codTipoContactoProspectoCita = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoContactoProspectoCita")].ToString());
            codTipoContactoProspectoVisita = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoContactoProspectoVisita")].ToString());
            codTipoContactoProspectoLlamada = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoContactoProspectoLlamada")].ToString());
            codTipoContactoProspectoCorreo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoContactoProspectoCorreo")].ToString());
            codTipoContactoProspectoWhatsApp = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoContactoProspectoWhatsApp")].ToString());
            codTipoContactoProspectoVideoLlamada = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoContactoProspectoVideoLlamada")].ToString());
            codTipoContactoProspectoPresencial = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codTipoContactoProspectoPresencial")].ToString());

            codEstadoProspectoDetalleResultadoContacto = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoDetalleResultadoContacto")].ToString());

            codExpectativaProspecto = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codExpectativaProspecto")].ToString());
            codExpectativaProspectoBaja = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codExpectativaProspectoBaja")].ToString());
            codExpectativaProspectoMedia = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codExpectativaProspectoMedia")].ToString());
            codExpectativaProspectoAlta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codExpectativaProspectoAlta")].ToString());
            codExpectativaProspectoMuyAlta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codExpectativaProspectoMuyAlta")].ToString());

            grcEventodetalle.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            grcProximoevento.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl3.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl4.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            //groupControl5.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl7.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            xtpMenuAsignaciones.Appearance.Header.ForeColor = Program.Sesion.Colores.Verde;
            xtpMenuEvento.Appearance.Header.ForeColor = Program.Sesion.Colores.Verde;
            xtpMenuProspecto.Appearance.Header.ForeColor = Program.Sesion.Colores.Verde;
            lblNombreprospecto.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            lytprimercontacto.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            lytEventoConfirmado.AppearanceItemCaption.ForeColor = Color.DarkBlue;
            lytEventoProgramado.AppearanceItemCaption.ForeColor = Color.DarkOrange;
            lblHistorial.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            pcChevron.BackColor = Program.Sesion.Colores.Verde;

            Inicializar();
            rbgtippersona.Select();
            HabilitarBotones();
            ocultarMostrarLeyenda();
            if (IndicadorConfirmacionAuto == "NO")
            {
                gvEventos_DoubleClick(null, null);
            }
            else
            {
                if (sIndCalendario == "1")
                {
                    gvEventos_DoubleClick(null, null);
                }
            }

            if (!btnControl)
            {
                layoutControlItem79.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem80.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            //if(perfil == "VISUALIZADOR")//CAMBIAR A VISUALIZADOR
            //{
            //    btnNuevo.Enabled = false;
            //    btnGuardar.Enabled = false;
            //    btn_HabilitarPro.Enabled = false;
            //    btnHistorial.Enabled = false;
            //    layoutControl5.Enabled = false;
            //}
            ccCalendarioEventos.TodayDate = DateTime.Now;

        }
        public void frmMantEvento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            //if (e.KeyCode == Keys.F5) this.Refresh();
            if (e.KeyCode == Keys.F5)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                validarCargarListado = 1;
                //xtpMenu_SelectedPageChanged(null, null);
                //rbgtippersona_SelectedIndexChanged(null, null);
                //LimpiarCamposEventos();
                LimpiarCamposEventos();
                ListarEventos();
                //ListarHistoricoAsignaciones();
                //ValidaEstadoProspecto();
                //Habilitar_evento(false);

                SplashScreenManager.CloseForm();
            }
        }
        private void frmMantEvento_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmHandler != null && validarCargarListado == 1)
            {
                int nRow = frmHandler.gvListaprospecto.FocusedRowHandle;

                //if (cod_estado_prospecto_mascara != cod_estado_prospecto) luego comentar cod_estado_prospecto_mascara revisar
                //{
                frmHandler.sEstadoFiltro = "";
                    frmHandler.sFiltroVisitasAsistidas = "";
                    frmHandler.sFiltroVisitasPendientes = "";
                    frmHandler.CodMenu = CodMenu;
                //}

                frmHandler.frmListadoProspecto_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                frmHandler.tileBar1.SelectedItem = frmHandler.tbiTotal;
                frmHandler.gvListaprospecto.FocusedRowHandle = nRow;
                frmHandler.sEstadoFiltro = sEstadoFiltro;
                frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                frmHandler.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
            }
        }

        private void picAnteriorCliente_Click(object sender, EventArgs e)
        {
            anteriorCliente();
        }
        private void picSiguienteCliente_Click(object sender, EventArgs e)
        {
            siguienteCliente();
        }

        //private DevExpress.XtraEditors.Controls.ImageComboBoxItemCollection CrearColeccion()
        //private void CrearColeccion()
        //{
        //    // Crear objeto RepositoryItemImageComboBox
        //    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();

        //    // Crear lista de objetos "Evento"
        //    List<eCampanha> eventos = unit.Campanha.ListarEjecuPros<eCampanha>(10, valor_1: cod_IndGrupo);

        //    // Crear y agregar objetos "ImageComboBoxItem" a la propiedad "Items" del objeto "RepositoryItemImageComboBox" 
        //    foreach (eCampanha evento in eventos)
        //    {

        //        repositoryItemImageComboBox1.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(evento.dsc_respuesta, evento.cod_respuesta, evento.valor_5));
        //    }

        //    //Crear objeto ImageComboBoxItemCollection y asignar la propiedad "Properties" con el objeto RepositoryItemImageComboBox creado anteriormente
        //    DevExpress.XtraEditors.Controls.ImageComboBoxItemCollection coleccion = new DevExpress.XtraEditors.Controls.ImageComboBoxItemCollection(repositoryItemImageComboBox1.Properties);

        //    gvEventos.Columns["colcod_respuesta"].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox() { Properties = { Items = coleccion } };
        //    //return coleccion;
        //}


        public class Evento
        {
            public string Descripcion { get; set; }
            public int IndiceImagen { get; set; }
            public string Valor { get; set; }
        }


        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) BloqueoControles(false, true, false);
            }
            HabilitarBotonesAsesorProximoEvento();
        }
        private void HabilitarBotonesAsesorProximoEvento()
        {
            if (listPerfil.Count() == 0)
            {
                listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);
            }
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 31 || x.cod_perfil == 16);
            eCampanha Listeventos_temp = Listeventos.Find(e => e.cod_tipo_evento == "TPEVN003" && (e.cod_respuesta == "RECON006" || e.cod_respuesta == ""));
            eCampanha Listeventos_temp_prox_evento = Listeventos.Find(e => e.cod_ejecutivo_citaProximo == Program.Sesion.Usuario.cod_usuario && (e.cod_respuesta == "RECON006" || e.cod_respuesta == ""));
            if (Listeventos_temp != null && o_eCamp.cod_ejecutivo != Program.Sesion.Usuario.cod_usuario && oPerfil == null)
            {
                BloqueoControles(false, true, false);
            }
            else if (Listeventos_temp_prox_evento != null && o_eCamp.cod_ejecutivo != Program.Sesion.Usuario.cod_usuario && oPerfil == null)
            {
                BloqueoControles(false, true, false);
            }
            else
            {
                BloqueoControles(true, false, true);
            }
        }


        void HabilitarBotonAceptar()
        {
            var respuestaDetalle = glkpeDetalleRespuesta.EditValue == null ? "" : glkpeDetalleRespuesta.EditValue.ToString();

            //if ((cod_IndProximo == "SI") && (respuestaDetalle != "DRECO007" && respuestaDetalle != "DRECO008" && respuestaDetalle != null))
            if (cod_IndProximo == "SI")
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void Inicializar()
        {
            sbepAsignado.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            sbepEnProceso.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            sbepCerrado.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            sbepCliente.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            peepFlecha2.Enabled = false;
            peepFlecha3.Enabled = false;
            peepFlecha4.Enabled = false;

            CargarCombos();
            switch (MiAccion)
            {
                case evento.Nuevo:
                    Nuevo();
                    break;
                case evento.Editar:
                    Editar();
                    glkpempresa.Enabled = false;
                    xtpMenu.SelectedTabPageIndex = 0;
                    break;
                case evento.Vista:
                    Editar();
                    layoutControlGroup2.Enabled = false;
                    break;
                case evento.Nuevo_externo:
                    Nuevo();
                    xtpMenuEvento.PageEnabled = false;
                    xtpMenuAsignaciones.PageEnabled = false;
                    break;
            }

            xtpMenu_SelectedPageChanged(null, null);
            rbgtippersona_SelectedIndexChanged(null, null);
            LimpiarCamposEventos();
            ListarEventos();
            ListarHistoricoAsignaciones();
            ValidaEstadoProspecto();
            Habilitar_evento(false);

            ListarCalendario();
        }
        private void Nuevo()
        {
            LimpiarCamposProspecto();
            lblNombreprospecto.Text = "";
            cod_prospecto = "";
            btnControl = false;
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.dsc_usuario;
            txtUsuarioCambio.Text = "";
            dtFechaRegistro.EditValue = DateTime.Now;
            dtFechaModificacion.EditValue = null;
        }
        private void Editar()
        {
            cod_empresa = o_eCamp.cod_empresa;
            cod_proyecto = o_eCamp.cod_proyecto;
            cod_prospecto = o_eCamp.cod_prospecto;

            glkpempresa.EditValue = o_eCamp.cod_empresa + "|" + o_eCamp.cod_proyecto;
            txtCodprospecto.Text = o_eCamp.cod_prospecto;
            rbgtippersona.EditValue = o_eCamp.cod_tipo_persona;
            glkpCanal.EditValue = o_eCamp.cod_canal;

            txtapepaterno.Text = o_eCamp.dsc_apellido_paterno;
            txtapematerno.Text = o_eCamp.dsc_apellido_materno;
            txtnombres.Text = o_eCamp.dsc_nombres;
            if(rbgtippersona.EditValue != null)
            {
                txtrazsoc.Text = rbgtippersona.EditValue.ToString() == "JU" ? o_eCamp.dsc_prospecto : "";
            }
           
            glkpdocide.EditValue = o_eCamp.cod_tipo_documento; //o_eCamp.cod_tipo_documento.Trim().ToString() == "" ? null : 
            lkpSegmento.EditValue = o_eCamp.cod_segmento;
            txtnrodocide.Text = o_eCamp.dsc_num_documento;
            de_fechanac.EditValue = Convert.ToDateTime(o_eCamp.fch_fec_nac);
            glkpsexo.EditValue = o_eCamp.cod_sexo; //o_eCamp.cod_sexo.Trim().ToString() == "" ? null : o_eCamp.cod_sexo;

            txtemail.Text = o_eCamp.dsc_email;
            txttelefono.Text = o_eCamp.dsc_telefono;
            txttelefonomovil.Text = o_eCamp.dsc_telefono_movil;

            txtprofesion.Text = o_eCamp.dsc_profesion;
            glkpestdocivil.EditValue = o_eCamp.cod_estado_civil; //o_eCamp.cod_estado_civil.Trim().ToString() == "" ? null : o_eCamp.cod_estado_civil;
            glkpnacionalidad.EditValue = o_eCamp.cod_nacionalidad; //o_eCamp.cod_nacionalidad.Trim().ToString() == "" ? null : o_eCamp.cod_nacionalidad;

            glkppais.EditValue = o_eCamp.cod_pais; //o_eCamp.cod_pais.Trim().ToString() == "" ? null : o_eCamp.cod_pais;
            glkpdepartamento.EditValue = o_eCamp.cod_departamento; //o_eCamp.cod_departamento.Trim().ToString() == "" ? null : o_eCamp.cod_departamento;
            glkpprovincia.EditValue = o_eCamp.cod_provincia; //o_eCamp.cod_provincia.Trim().ToString() == "" ? null : o_eCamp.cod_provincia;
            glkpdistrito.EditValue = o_eCamp.cod_distrito; //o_eCamp.cod_distrito.Trim().ToString() == "" ? null : o_eCamp.cod_distrito;
            txtdireccion.Text = o_eCamp.dsc_direccion;

            glkpgrupofam.EditValue = o_eCamp.cod_grupo_familiar; //o_eCamp.cod_grupo_familiar.Trim().ToString() == "" ? null : o_eCamp.cod_grupo_familiar;
            glkprangorent.EditValue = o_eCamp.cod_rango_renta; //o_eCamp.cod_rango_renta.Trim().ToString() == "" ? null : o_eCamp.cod_rango_renta;

            meObservacion.Text = o_eCamp.dsc_observacion;
            glkpCampana.EditValue = o_eCamp.cod_campanha;
            if (o_eCamp.cod_campanha == null || o_eCamp.cod_campanha == "") { btnVerCampanha.Enabled = false; }

            txtUsuarioRegistro.Text = o_eCamp.cod_usuario_registro;
            if (o_eCamp.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = o_eCamp.fch_registro; }
            txtUsuarioCambio.Text = o_eCamp.cod_usuario_cambio;
            if (o_eCamp.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = o_eCamp.fch_cambio; }

            lblNombreprospecto.Text = "Prospecto: " + o_eCamp.dsc_prospecto;

            cod_estado_prospecto = o_eCamp.cod_estado_prospecto;
            cod_estado_prospecto_mascara = o_eCamp.cod_estado_prospecto;
            SeleccionaBotonEstadoProspecto(cod_estado_prospecto);

            if (cod_estado_prospecto == codEstadoProspectoCerrado) { btn_HabilitarPro.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; } else { btn_HabilitarPro.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; }

            glkpEjecutivo.EditValue = o_eCamp.cod_ejecutivo;
            //glkpEjecutivo.Enabled = false;
            glkpOrigenprospecto.EditValue = o_eCamp.cod_origen_prospecto == null ? "" : o_eCamp.cod_origen_prospecto.ToString();
            //glkpCanal.EditValue= o_eCamp.cod_canal.Trim().ToString() == "" ? null : o_eCamp.cod_canal; ;

        }
        private void Editar_Evento(eCampanha obj)
        {

            cod_respuesta = obj.cod_respuesta;
            //LimpiarCamposEventos();
            if (obj.cod_tipo_evento == codTipoEventoProspecto) //EVENTO NORMAL
            {
                cod_evento = obj.cod_evento;

                deeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                tieFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                if (obj.cod_tipo_contacto == codTipoContactoProspectoCita) { petpcita_Click(null, null); }
                else if (obj.cod_tipo_contacto == codTipoContactoProspectoVisita) { petpvisita_Click(null, null); }
                else if (obj.cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); }
                else if (obj.cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); }
                else if (obj.cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null); }
                else if (obj.cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { petpvideollamada_Click(null, null); }
                else if (obj.cod_tipo_contacto == codTipoContactoProspectoPresencial) { petpPresencial_Click(null, null); }
                //if (obj.cod_respuesta == codResultadoEventoProspectoExitoso) { btneEfectiva_Click(null, null); }
                //else if (obj.cod_respuesta == codResultadoEventoProspectoAsistio) { btneEfectiva_Click(null, null); }
                //else if (obj.cod_respuesta == codResultadoEventoProspectoSinRespuesta) { btneNoEfectiva_Click(null, null); }
                //else if (obj.cod_respuesta == codResultadoEventoProspectoContactoInvalido) { btnContactoInvalido_Click(null, null); }
                glkpeDetalleRespuesta.EditValue = obj.cod_detalle_respuesta;
                meeObs.Text = obj.dsc_observacion;
                if (obj.cod_expectativa == codExpectativaProspectoBaja) { SeleccionaBotonExpectativa(btnBaja); cod_expectativa = codExpectativaProspectoBaja; }
                else if (obj.cod_expectativa == codExpectativaProspectoMedia) { SeleccionaBotonExpectativa(btnMedia); cod_expectativa = codExpectativaProspectoMedia; }
                else if (obj.cod_expectativa == codExpectativaProspectoAlta) { SeleccionaBotonExpectativa(btnAlta); cod_expectativa = codExpectativaProspectoAlta; }
                else if (obj.cod_expectativa == codExpectativaProspectoMuyAlta) { SeleccionaBotonExpectativa(btnMuyAlta); cod_expectativa = codExpectativaProspectoMuyAlta; }
                if (lciReceptivo.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    if (obj.flg_receptivo.ToString() == "SI")
                    {
                        chkReceptivo.Checked = true;
                    }
                    else
                    {
                        chkReceptivo.Checked = false;
                    }
                }
                //eCampanha eEventoCam = gvEventos.GetFocusedRow() as eCampanha;
                //if (eEventoCam.flg_confirmacion != "SI") { Habilitar_evento(false); } else { Habilitar_evento(true); }
                ValidaMotivoNoInteres(obj.cod_detalle_respuesta, obj.cod_motivo);
            }
            else if (obj.cod_tipo_evento == codTipoProximoEventoProspecto) // PROXIMO EVENTO
            {
                if (obj.cod_respuesta == codResultadoEventoProspectoPendiente) //PROXIMO EVENTO PENDINETE DE RESPUESTA
                {
                    cod_evento = obj.cod_evento;
                    cod_tipo_evento = obj.cod_tipo_evento;
                    //if (obj.flg_confirmacion.ToString() == "NO" & o_eCamp.cod_ejecutivo != null) // PROXIMO EVENTO PENDINETE DE RESPUESTA - NO CONFIRMADO
                    //{
                    //    frmMantConfirmacion frm = new frmMantConfirmacion(this);
                    //    frm.MiAccion = evento.Nuevo;
                    //    //frm.perfil = perfil;
                    //    frm.cod_empresa = cod_empresa;
                    //    frm.cod_evento = cod_evento;
                    //    frm.cod_proyecto = cod_proyecto;
                    //    frm.cod_prospecto = cod_prospecto;
                    //    frm.cod_ejecutivo = obj.cod_ejecutivo.ToString();
                    //    frm.o_eCamp = obj;
                    //    frm.Titulo = lblNombreprospecto.Text;
                    //    frm.sEstadoFiltro = sEstadoFiltro;
                    //    frm.sTipoContactoFiltro = sTipoContactoFiltro;
                    //    frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    //    frm.CodMenu = CodMenu;
                    //    frm.ShowDialog(this);
                    //}
                    //else // PROXIMO EVENTO PENDINETE DE RESPUESTA - CONFIRMADO
                    //{
                    eCampanha eEventoCam = gvEventos.GetFocusedRow() as eCampanha;
                    if (eEventoCam.flg_confirmacion != "SI") { Habilitar_evento(false); } else { Habilitar_evento(true); }

                    deeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    tieFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    if (obj.cod_tipo_contacto == codTipoContactoProspectoCita) { petpcita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVisita) { petpvisita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { petpvideollamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoPresencial) { petpPresencial_Click(null, null); }

                    ValidaMotivoNoInteres(obj.cod_detalle_respuesta, obj.cod_motivo);
                    //}
                }
                else // PROXIMO EVENTO CON RESPUESTA
                {
                    cod_tipo_evento = obj.cod_tipo_evento.ToString();
                    deeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    tieFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    if (obj.cod_tipo_contacto == codTipoContactoProspectoCita) { petpcita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVisita) { petpvisita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { petpvideollamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoPresencial) { petpPresencial_Click(null, null); }
                    //if (obj.cod_respuesta == codResultadoEventoProspectoExitoso) { btneEfectiva_Click(null, null); }
                    //if (obj.cod_respuesta == codResultadoEventoProspectoAsistio) { btneEfectiva_Click(null, null); }
                    //else if (obj.cod_respuesta == codResultadoEventoProspectoSinRespuesta) { btneNoEfectiva_Click(null, null); }
                    //else if (obj.cod_respuesta == codResultadoEventoProspectoContactoInvalido) { btnContactoInvalido_Click(null, null); }
                    //else if (obj.cod_respuesta == codResultadoEventoProspectoNoAsistio) { btneNoEfectiva_Click(null, null); }
                    glkpeDetalleRespuesta.EditValue = obj.cod_detalle_respuesta;
                    meeObs.Text = obj.dsc_observacion;
                    if (obj.cod_expectativa == codExpectativaProspectoBaja) { SeleccionaBotonExpectativa(btnBaja); cod_expectativa = codExpectativaProspectoBaja; }
                    else if (obj.cod_expectativa == codExpectativaProspectoMedia) { SeleccionaBotonExpectativa(btnMedia); cod_expectativa = codExpectativaProspectoMedia; }
                    else if (obj.cod_expectativa == codExpectativaProspectoAlta) { SeleccionaBotonExpectativa(btnAlta); cod_expectativa = codExpectativaProspectoAlta; }
                    else if (obj.cod_expectativa == codExpectativaProspectoMuyAlta) { SeleccionaBotonExpectativa(btnMuyAlta); cod_expectativa = codExpectativaProspectoMuyAlta; }
                    if (lciReceptivo.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    {
                        if (obj.flg_receptivo == "SI")
                        {
                            chkReceptivo.Checked = true;
                        }
                        else
                        {
                            chkReceptivo.Checked = false;
                        }
                    }
                    ValidaMotivoNoInteres(obj.cod_detalle_respuesta, obj.cod_motivo);

                }

            }
            else //eventoConfirmacion
            {
                cod_tipo_evento = obj.cod_tipo_evento.ToString();
                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //layoutControlItem55.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem61.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (obj.cod_respuesta == codResultadoEventoProspectoPendiente) //PROXIMO EVENTO PENDINETE DE RESPUESTA
                {
                    cod_evento = obj.cod_evento.ToString();
                    //if (obj.flg_confirmacion.ToString() == "NO" & o_eCamp.cod_ejecutivo != null) // PROXIMO EVENTO PENDINETE DE RESPUESTA - NO CONFIRMADO
                    //{
                    //    frmMantConfirmacion frm = new frmMantConfirmacion(this);
                    //    frm.MiAccion = evento.Nuevo;
                    //    //frm.perfil = perfil;
                    //    frm.cod_empresa = cod_empresa;
                    //    frm.cod_evento = cod_evento;
                    //    frm.cod_proyecto = cod_proyecto;
                    //    frm.cod_prospecto = cod_prospecto;
                    //    frm.cod_ejecutivo = obj.cod_ejecutivo.ToString();
                    //    frm.o_eCamp = obj;
                    //    frm.Titulo = lblNombreprospecto.Text;
                    //    frm.sEstadoFiltro = sEstadoFiltro;
                    //    frm.sTipoContactoFiltro = sTipoContactoFiltro;
                    //    frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    //    frm.CodMenu = CodMenu;
                    //    frm.ShowDialog(this);
                    //}
                    //else // PROXIMO EVENTO PENDINETE DE RESPUESTA - CONFIRMADO
                    //{

                    deeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    tieFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    if (obj.cod_tipo_contacto == codTipoContactoProspectoCita) { petpcita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVisita) { petpvisita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { petpvideollamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoPresencial) { petpPresencial_Click(null, null); }
                    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    Habilitar_evento(false);
                    ValidaMotivoNoInteres(obj.cod_detalle_respuesta, obj.cod_motivo);
                    //}
                }
                else // PROXIMO EVENTO CON RESPUESTA
                {
                    cod_tipo_evento = obj.cod_tipo_evento.ToString();
                    deeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    tieFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                    if (obj.cod_tipo_contacto == codTipoContactoProspectoCita) { petpcita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVisita) { petpvisita_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { petpvideollamada_Click(null, null); }
                    else if (obj.cod_tipo_contacto == codTipoContactoProspectoPresencial) { petpPresencial_Click(null, null); }
                    //if (obj.cod_respuesta == codResultadoEventoProspectoExitoso) { btneEfectiva_Click(null, null); }
                    //else if (obj.cod_respuesta == codResultadoEventoProspectoSinRespuesta) { btneNoEfectiva_Click(null, null); }
                    //else if (obj.cod_respuesta == codResultadoEventoProspectoContactoInvalido) { btnContactoInvalido_Click(null, null); }
                    //else if (obj.cod_respuesta == codResultadoEventoProspectoNoAsistio) { btneNoEfectiva_Click(null, null); }
                    glkpeDetalleRespuesta.EditValue = obj.cod_detalle_respuesta;
                    meeObs.Text = obj.dsc_observacion;
                    if (obj.cod_expectativa == codExpectativaProspectoBaja) { SeleccionaBotonExpectativa(btnBaja); }
                    else if (obj.cod_expectativa == codExpectativaProspectoMedia) { SeleccionaBotonExpectativa(btnMedia); }
                    else if (obj.cod_expectativa == codExpectativaProspectoAlta) { SeleccionaBotonExpectativa(btnAlta); }
                    else if (obj.cod_expectativa == codExpectativaProspectoMuyAlta) { SeleccionaBotonExpectativa(btnMuyAlta); }
                    if (lciReceptivo.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    {
                        if (obj.flg_receptivo == "SI")
                        {
                            chkReceptivo.Checked = true;
                        }
                        else
                        {
                            chkReceptivo.Checked = false;
                        }
                    }
                    ValidaMotivoNoInteres(obj.cod_detalle_respuesta, obj.cod_motivo);
                }

            }

            if (obj.cod_evento_ref != "" & obj.cod_respuesta != codResultadoEventoProspectoPendiente)
            {
                List<eCampanha> Listeventos_temp;
                if (obj.cod_tipo_evento == codTipoProximoEventoProspecto)
                {
                    Listeventos_temp = Listeventos.FindAll(e => e.cod_evento_ref == obj.cod_evento.ToString() && e.cod_evento != "00000001");
                }
                else
                {
                    Listeventos_temp = Listeventos.FindAll(e => e.cod_evento == obj.cod_evento_ref.ToString() && e.cod_evento != "00000001");
                }

                if (Listeventos_temp.Count > 0)
                {
                    cod_tipo_evento = obj.cod_tipo_evento.ToString();

                    if (Listeventos_temp[0].cod_tipo_evento == codTipoEventoProspecto && obj.cod_tipo_evento == codTipoEventoProspecto)
                    {
                        cod_eventoProximo = Listeventos_temp[0].cod_evento;
                        depeFecha.EditValue = Convert.ToDateTime(Listeventos_temp[0].fch_fecha + " " + Listeventos_temp[0].fch_hora);
                        tipeFecha.EditValue = Convert.ToDateTime(Listeventos_temp[0].fch_fecha + " " + Listeventos_temp[0].fch_hora);
                        if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoCita) { petpcita_Click(null, null); }
                        else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoVisita) { petpvisita_Click(null, null); llenarCamposConfirmacion(); }
                        else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); llenarCamposConfirmacion(); }
                        else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); }
                        else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null); }
                        else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { petpvideollamada_Click(null, null); }
                        else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoPresencial) { petpPresencial_Click(null, null); }
                        if (Listeventos_temp[0].cod_respuesta == codResultadoEventoProspectoExitoso) { btneEfectiva_Click(null, null); }
                        else if (Listeventos_temp[0].cod_respuesta == codResultadoEventoProspectoAsistio) { btneEfectiva_Click(null, null); }
                        else if (Listeventos_temp[0].cod_respuesta == codResultadoEventoProspectoSinRespuesta) { btneNoEfectiva_Click(null, null); }
                        else if (Listeventos_temp[0].cod_respuesta == codResultadoEventoProspectoContactoInvalido) { btnContactoInvalido_Click(null, null); }
                        glkpeDetalleRespuesta.EditValue = Listeventos_temp[0].cod_detalle_respuesta;
                        meeObs.Text = Listeventos_temp[0].dsc_observacion;

                        if (Listeventos_temp[0].cod_expectativa == codExpectativaProspectoBaja) { SeleccionaBotonExpectativa(btnBaja); }
                        else if (Listeventos_temp[0].cod_expectativa == codExpectativaProspectoMedia) { SeleccionaBotonExpectativa(btnMedia); }
                        else if (Listeventos_temp[0].cod_expectativa == codExpectativaProspectoAlta) { SeleccionaBotonExpectativa(btnAlta); }
                        else if (Listeventos_temp[0].cod_expectativa == codExpectativaProspectoMuyAlta) { SeleccionaBotonExpectativa(btnMuyAlta); }
                        if (lciReceptivo.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        {
                            if (obj.flg_receptivo == "SI")
                            {
                                chkReceptivo.Checked = true;
                            }
                            else
                            {
                                chkReceptivo.Checked = false;
                            }
                        }
                        ValidaMotivoNoInteres(obj.cod_detalle_respuesta, obj.cod_motivo);
                    }
                    else
                    {
                        if (Listeventos_temp[0].cod_tipo_evento == codTipoProximoEventoProspecto)
                        {
                            cod_eventoProximo = Listeventos_temp[0].cod_evento;
                            depeFecha.EditValue = Convert.ToDateTime(Listeventos_temp[0].fch_fecha + " " + Listeventos_temp[0].fch_hora);
                            tipeFecha.EditValue = Convert.ToDateTime(Listeventos_temp[0].fch_fecha + " " + Listeventos_temp[0].fch_hora);
                            if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoCita) { pepecita_Click(null, null); /*SeleccionaBotonTipoProceso_Proximo(pepecita);*/ llenarCamposConfirmacion(); cod_tipo_contacto_2 = codTipoContactoProspectoCita; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoVisita) { pepevisita_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepevisita); */ llenarCamposConfirmacion(); cod_tipo_contacto_2 = codTipoContactoProspectoVisita; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoLlamada) { pepellamada_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepellamada);*/  cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoCorreo) { pepecorreo_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepecorreo); */ cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { pepewtsp_Click(null, null); /*SeleccionaBotonTipoProceso_Proximo(pepewtsp); */ cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { pepevideollamada_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepevideollamada);*/ cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            mepeObs.Text = Listeventos_temp[0].dsc_observacion;
                            if (Listeventos_temp[0].cod_ejecutivo_cita != "")
                            {
                                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                glkpeEjecutivoCita.EditValue = Listeventos_temp[0].cod_ejecutivo_cita;
                            }
                            else
                            {
                                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                glkpeEjecutivoCita.EditValue = null;
                            }
                        }
                        else if (Listeventos_temp[0].cod_tipo_evento != codTipoConfirmacionEventoProspecto)
                        {
                            cod_eventoProximo = Listeventos_temp[0].cod_evento;
                            depeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                            tipeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
                            if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoCita) { pepecita_Click(null, null); /*SeleccionaBotonTipoProceso_Proximo(pepecita);*/ llenarCamposConfirmacion(); cod_tipo_contacto_2 = codTipoContactoProspectoCita; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoVisita) { pepevisita_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepevisita); */ llenarCamposConfirmacion(); cod_tipo_contacto_2 = codTipoContactoProspectoVisita; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoLlamada) { pepellamada_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepellamada);*/  cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoCorreo) { pepecorreo_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepecorreo); */ cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { pepewtsp_Click(null, null); /*SeleccionaBotonTipoProceso_Proximo(pepewtsp); */ cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            else if (Listeventos_temp[0].cod_tipo_contacto == codTipoContactoProspectoVideoLlamada) { pepevideollamada_Click(null, null);/*SeleccionaBotonTipoProceso_Proximo(pepevideollamada);*/ cod_tipo_contacto_2 = codTipoContactoProspectoLlamada; }
                            mepeObs.Text = obj.dsc_observacion;
                            ValidaMotivoNoInteres(obj.cod_detalle_respuesta, obj.cod_motivo);
                            if (obj.cod_ejecutivo_cita != "")
                            {
                                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                glkpeEjecutivoCita.EditValue = obj.cod_ejecutivo_cita;
                            }
                            else
                            {
                                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                glkpeEjecutivoCita.EditValue = null;
                            }
                        }
                    }
                    if (obj.flg_tiene_confirmacion_evento == "SI")
                    {
                        llenarCamposConfirmacion();
                    }
                }
            }

            if (obj.cod_respuesta == codResultadoEventoProspectoPendiente)
            {
                //Habilitar_evento(true);
            }
            else
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                Habilitar_evento(false);
            }
        }

        private void llenarCamposConfirmacion()
        {
            eCampanha oListMemoDesc = Listeventos.Find(x => x.cod_evento_ref == cod_evento);
            if (oListMemoDesc != null)
            {
                eCampanha oList = Listeventos.Find(x => x.cod_evento_ref == oListMemoDesc.cod_evento && x.cod_tipo_evento == "TPEVN003");
                if (oList == null) { cod_tipo_confirmacion = ""; return; }
                depeConfirmacion.EditValue = oList.fch_evento;
                tipeConfirmacion.EditValue = oList.fch_evento;
                HabilitarCamposConfirmacion(true);
                if (oList.cod_tipo_contacto.ToString() == codTipoContactoProspectoLlamada) { pepellamada_Click(null, null); /*SeleccionaBotonConfirmacion_Proximo(pepellamadaConfir);*/ }
                else if (oList.cod_tipo_contacto.ToString() == codTipoContactoProspectoWhatsApp) { pepewtsp_Click(null, null); /*SeleccionaBotonConfirmacion_Proximo(pepewtspConfir);*/ }
                else { pepecorreo_Click(null, null); /*SeleccionaBotonConfirmacion_Proximo(pepecorreoConfir);*/ }

            }

        }

        private void HabilitarCamposConfirmacion(bool read)
        {
            pepellamadaConfir.ReadOnly = read;
            pepewtspConfir.ReadOnly = read;
            pepecorreoConfir.ReadOnly = read;
            depeConfirmacion.ReadOnly = read;
            tipeConfirmacion.ReadOnly = read;
        }
        private void CargarCombos()
        {
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = cod_empresa;
            tmpCamp.cod_proyecto = cod_proyecto;

            if (MiAccion == evento.Nuevo || MiAccion == evento.Nuevo_externo) { tmpCamp.flg_activo = "SI"; }
            if (MiAccion == evento.Editar) { tmpCamp.flg_activo = ""; }

            unit.Campanha.CargarCombos_TablasMaestras("1", "canal", glkpCanal, "cod_canal", "dsc_canal", tmpCamp);

            //unit.Clientes.CargaCombosLookUp("tipo_campanha", glkpOrigenprospecto, "cod_tipo_campanha", "dsc_tipo_campanha", "");//
            unit.Clientes.CargaCombosLookUp("TipoDocumento", glkpdocide, "cod_tipo_documento", "dsc_tipo_documento", "");
            unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", glkpestdocivil, "cod_estado_civil", "dsc_estado_civil", "");
            unit.Clientes.CargaCombosLookUp("TipoPais", glkpnacionalidad, "cod_pais", "dsc_pais", "");
            unit.Clientes.CargaCombosLookUp("TipoPais", glkppais, "cod_pais", "dsc_pais", "");
            unit.Clientes.CargaCombosLookUp("TipoSexo", glkpsexo, "cod_sexo", "dsc_sexo", "");
            unit.Campanha.CargarCombos_TablasMaestras("1", "proyectos", glkpempresa, "cod_proyecto", "dsc_proyecto", tmpCamp);
            if (!String.IsNullOrEmpty(cod_empresa))
            {
                glkpempresa.EditValue = cod_empresa + "|" + cod_proyecto;
            }
            else
            {
                glkpempresa.EditValue = "00008" + "|" + "00003";
            }
            unit.Campanha.CargarCombos_TablasMaestras("1", "campanas", glkpCampana, "cod_campanha", "dsc_descripcion", tmpCamp);
            unit.Campanha.CargarCombos_TablasMaestras("1", "grupofamiliar", glkpgrupofam, "cod_grupo_familiar", "dsc_grupo_familiar", tmpCamp);
            unit.Campanha.CargarCombos_TablasMaestras("1", "rangorenta", glkprangorent, "cod_rango_renta", "dsc_rango_renta", tmpCamp);

            odtTipoContacto = unit.Campanha.ObtenerListadoGridLookup("tipocontacto", Program.Sesion.Usuario.cod_usuario, "", "", "");

            //unit.Campanha.CargarCombos_TablasMaestras("1", "ejecutivos", glkpEjecutivo, "cod_ejecutivo", "dsc_ejecutivo", tmpCamp);
            List<eUsuario> ListEjecutivos = new List<eUsuario>();
            int opcion = MiAccion == evento.Nuevo ? 1 : 5;
            ListEjecutivos = unit.Campanha.ListarEjecutivosVentasMenu<eUsuario>(opcion, "", cod_proyecto, Program.Sesion.Usuario.cod_usuario);
            glkpEjecutivo.Properties.ValueMember = "cod_usuario";
            glkpEjecutivo.Properties.DisplayMember = "dsc_usuario";
            glkpEjecutivo.Properties.DataSource = ListEjecutivos;
            var cod_usuario = ListEjecutivos.Where(x => x.cod_usuario == "BVEGA").Select(x => x.cod_usuario).ToArray();
            glkpEjecutivo.EditValue = cod_usuario.Count() > 0 ? cod_usuario[0] : null;

            unit.Campanha.CargarCombos_TablasMaestras("1", "ejecutivos", glkpeEjecutivoCita, "cod_ejecutivo", "dsc_ejecutivo", tmpCamp);

            unit.Campanha.CargarCombos_TablasMaestras("1", "motivonointeres", glkpeMotivoNoInteres, "cod_motivo", "dsc_motivo", tmpCamp);

        }
        void OpcionesBotonResultado(string cod_tipocontacto)
        {
            string _sqlWhere = "cod_contacto = '" + cod_tipocontacto + "'";
            DataTable _newDataTable = odtTipoContacto.Select(_sqlWhere).CopyToDataTable();
            if (_newDataTable.Rows.Count > 0)
            {
                cod_IndGrupo = _newDataTable.Rows[0][2].ToString();
            }
            else
            {
                cod_IndGrupo = "";
            }

            List<eCampanha> lstProspecto = unit.Campanha.ListarEjecuPros<eCampanha>(10, valor_1: cod_IndGrupo);

            //LDAC - Crear botones dinámicos
            //while (pnlWindow.Controls.Count > 0)
            //{
            //    pnlWindow.Controls[0].Dispose();
            //}
            //if(lstBotonesDinamicos.Count() > 0)
            //{
            //    foreach (var buton in lstBotonesDinamicos)
            //    {
            //        buton.Appearance.BackColor = Color.Transparent;
            //        buton.Appearance.ForeColor = Color.Black;
            //    }

            //}
            pnlWindow.Controls.Clear();
            lstBotonesDinamicos.Clear();

            foreach (var campana in lstProspecto)
            {
                pnlWindow.Controls.Add(CrearBotonDinamico(campana, lstProspecto.Count()));
            }

            glkpeDetalleRespuesta.Properties.DataSource = null;


            eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
            if (obj != null && obj.cod_respuesta != "")
            {
                SimpleButton oListBotones = new SimpleButton();
                oListBotones = lstBotonesDinamicos.Find(x => x.Name == "btn" + obj.cod_respuesta);
                if (oListBotones != null){
                    HabilitarBotonRespuesta(oListBotones);

                    //oListBotones.Appearance.ForeColor = Color.White;
                    //oListBotones.Appearance.BackColor = Color.FromArgb(89, 139, 125);
                }
            }
            //layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //layoutControlItem55.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //odtBotonResultado = unit.Campanha.ObtenerListadoGridLookup("resultadocontacto", Program.Sesion.Usuario.cod_usuario, "", "", cod_IndGrupo);
            //if (odtBotonResultado.Rows.Count > 0) { btneEfectiva.Text = odtBotonResultado.Rows[0][1].ToString(); layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }
            //if (odtBotonResultado.Rows.Count > 1) { btneNoEfectiva.Text = odtBotonResultado.Rows[1][1].ToString(); layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }
            //if (odtBotonResultado.Rows.Count > 2) { btnContactoInvalido.Text = odtBotonResultado.Rows[2][1].ToString(); layoutControlItem55.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }

            //btneEfectiva.Appearance.BackColor = Color.Transparent;
            //btneNoEfectiva.Appearance.BackColor = Color.Transparent;
            //btnContactoInvalido.Appearance.BackColor = Color.Transparent;
            //glkpeDetalleRespuesta.Properties.DataSource = null;
        }
        public void SeleccionaBotonEstadoProspecto(string sEstadoProspecto)
        {
            sbepAsignado.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            sbepEnProceso.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            sbepCerrado.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            sbepCliente.Appearance.BackColor = Color.FromArgb(210, 210, 210);
            peepFlecha2.Enabled = false;
            peepFlecha3.Enabled = false;
            peepFlecha4.Enabled = false;

            if (sEstadoProspecto.Length > 0)
            {
                string sValor = sEstadoProspecto.Substring(5, 3).ToString();

                if (Convert.ToInt32(sValor) >= 2)
                {
                    sbepAsignado.Appearance.BackColor = Color.FromArgb(23, 97, 143);
                }
                if (Convert.ToInt32(sValor) >= 3)
                {
                    sbepEnProceso.Appearance.BackColor = Color.FromArgb(23, 97, 143);
                    peepFlecha2.Enabled = true;
                }
                if (Convert.ToInt32(sValor) == 4)
                {
                    sbepCerrado.Appearance.BackColor = Color.FromArgb(106, 38, 13);
                    peepFlecha3.Enabled = true;
                }
                if (Convert.ToInt32(sValor) == 5)
                {
                    sbepCliente.Appearance.BackColor = Color.FromArgb(46, 85, 88);
                    peepFlecha4.Enabled = true;
                }
            }
        }

        private SimpleButton CrearBotonDinamico(eCampanha eCampana, int cantidad = 0) //LDAC - Generar botones dinámicos
        {
            int Width = cantidad == 4 ? 50 : 100;
            SimpleButton button = new SimpleButton()
            {
                Text = eCampana.dsc_respuesta,

                //Tag = eCampana.grupo,
                AutoSize = true,
                Dock = DockStyle.Left,
                Cursor = Cursors.Hand,
                //MaximumSize = new Size(149,22),
                //MinimumSize = new Size(120,22),
                Padding = new Padding(0, 0, 0, 0),
                //Width = 70,
                MinimumSize = new Size(Width, 28),
                //FlatStyle = FlatStyle.Flat,
                //BackColor = Color.Transparent,
                
                //ForeColor = Color.White,
                Name = "btn" + eCampana.cod_respuesta,
                //FlatAppearance = { BorderSize = 0, BorderColor = Color.Transparent },
                //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(1, 1, Width, Height, 15, 15))

            };
            button.Appearance.BackColor = Color.Transparent;
            button.Appearance.ForeColor = Color.Black;

            lstBotonesDinamicos.Add(button);

            button.Click += (sender, args) => HabilitarBotonRespuesta(button);
            return button;
        }

        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);


        private SimpleButton HabilitarBotonRespuesta(SimpleButton oSimpleButton) //LDAC - Generar botones dinámicos
        {
            foreach (var buton in lstBotonesDinamicos)
            {
                buton.Appearance.BackColor = Color.Transparent;
                buton.Appearance.ForeColor = Color.Black;
            }
            oSimpleButton.Appearance.ForeColor = Color.White;
            oSimpleButton.Appearance.BackColor = Color.FromArgb(89, 139, 125);

            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = "";

            cod_respuesta = ObtenerCodigoRespuestaDinamico(oSimpleButton.Name);
            //cod_respuesta = ObtenerCodigoRespuestaDinamico(oSimpleButton.Text);

            tmpCamp.valor_1 = cod_respuesta;
            //tmpCamp.valor_4 = cod_tipo_contacto;
            tmpCamp.valor_4 = cod_tipo_contacto;

            unit.Campanha.CargarCombos_TablasMaestras("1", "detalleresultadocontacto", glkpeDetalleRespuesta, "cod_detalle_respuesta", "dsc_detalle_respuesta", tmpCamp);
            lstDetalleRespuesta = unit.Campanha.CombosEnGridControl<eCampanha>("detalleresultadocontacto", tmpCamp.valor_1, tmpCamp.valor_4);

            OpcionVer_Expectativa(1);

            return oSimpleButton;
        }

        private string ObtenerCodigoRespuestaDinamico(string desc_respuesta)
        {
            string cod_respuesta = desc_respuesta.Substring(3);

            return cod_respuesta;
            //switch (desc_respuesta)
            //{
            //    case "EXITOSO": //RECON001
            //        {
            //            cod_respuesta = codResultadoEventoProspectoExitoso;
            //            break;
            //        }
            //    case "SIN RESPUESTA": //RECON002
            //        {
            //            cod_respuesta = codResultadoEventoProspectoSinRespuesta;
            //            break;
            //        }
            //    case "CONTACTO INVALIDO": //RECON003
            //        {
            //            cod_respuesta = codResultadoEventoProspectoContactoInvalido;
            //            break;
            //        }
            //    case "ASISTIO": //RECON004
            //        {
            //            cod_respuesta = "RECON004";
            //            break;
            //        }
            //    case "NO ASISTIO":
            //        {
            //            cod_respuesta = "RECON005";
            //            break;
            //        }
            //    case "PENDIENTE":
            //        {
            //            cod_respuesta = "RECON006";
            //            break;
            //        }
            //    case "RECONFIRMACION EXITOSA":
            //        {
            //            //cod_respuesta = codResultadoEventoProspectoExitoso;
            //            cod_respuesta = "RECON007";
            //            break;
            //        }
            //    case "PENDIENTE RESPUESTA":
            //        {
            //            cod_respuesta = "RECON008";
            //            break;
            //        }
            //}

        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevo.Enabled = Enabled;
            btnGuardar.Enabled = Enabled;
            layoutControlGroup2.Enabled = Enabled;
            glkpEjecutivo.ReadOnly = ReadOnly;
            btnConfirmar.Enabled = Enabled;
            btnHistorial.Enabled = Enabled;
        }

        public void ListarEventos()
        {
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            Listeventos = unit.Campanha.ListarEventos<eCampanha>(0, cod_empresa, cod_prospecto, Program.Sesion.Usuario.cod_usuario);
            bsEventos.DataSource = Listeventos;

            ListarCalendario();
            ccCalendarioEventos.Refresh();
            //SplashScreenManager.CloseForm();
        }
        public void ListarHistoricoAsignaciones()
        {
            Listasignaciones = unit.Campanha.ListarAsignacionesHistorico<eCampanha>(0, cod_empresa, cod_proyecto, cod_prospecto, Program.Sesion.Usuario.cod_usuario);
            bsAsignaciones.DataSource = Listasignaciones;
        }
        public void ListarCalendario()
        {
            ListCalendario = unit.Campanha.Listar_Eventos_Calendario<eCampanha>(0, cod_empresa, cod_proyecto, cod_ejecutivo, Program.Sesion.Usuario.cod_usuario);
            //bsListaCalendario.DataSource = null;
            //ccCalendarioEventos.Refresh();
            ccCalendarioEventos_EditValueChanged(null, null);
        }


        private void ActivaCamposPersonaNatural()
        {
            if (txtnombres.Text.Trim().Length == 0)
            {
                txtnombres.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            }
            txtrazsoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            txtrazsoc.Text = "";
            txtapepaterno.Enabled = true;
            txtapematerno.Enabled = true;
            txtnombres.Enabled = true;
            de_fechanac.Enabled = true;
            glkpsexo.Enabled = true;
            glkpestdocivil.Enabled = true;
            txtprofesion.Enabled = true;
            glkpgrupofam.Enabled = true;

            txtrazsoc.Enabled = false;
        }
        private void ActivaCamposPersonaJuridica()
        {
            txtnombres.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            txtnombres.Text = "";
            if (txtrazsoc.Text.Trim().Length == 0)
            {
                txtrazsoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            }

            txtapepaterno.Enabled = false;
            txtapematerno.Enabled = false;
            txtnombres.Enabled = false;
            de_fechanac.Enabled = false;
            glkpsexo.Enabled = false;
            glkpestdocivil.Enabled = false;
            txtprofesion.Enabled = false;
            glkpgrupofam.Enabled = false;

            txtrazsoc.Enabled = true;
        }
        private void LimpiarCamposProspecto()
        {
            if (!String.IsNullOrEmpty(cod_empresa))
            {
                glkpempresa.EditValue = cod_empresa + "|" + cod_proyecto;
            }
            else
            {
                glkpempresa.EditValue = "00008" + "|" + "00003";
            }
            //glkpempresa.EditValue = "00010|00001";
            txtCodprospecto.Text = "";

            rbgtippersona.EditValue = "NA";
            txtapepaterno.Text = "";
            txtapematerno.Text = "";
            txtnombres.Text = "";
            txtrazsoc.Text = "";
            glkpdocide.EditValue = null;
            txtnrodocide.Text = "";
            de_fechanac.EditValue = null;
            glkpsexo.EditValue = null;
            lkpSegmento.EditValue = null;

            txtemail.Text = "";
            txttelefono.Text = "01";
            txttelefonomovil.Text = "51";

            txtprofesion.Text = "";
            glkpestdocivil.EditValue = null;
            glkpnacionalidad.EditValue = "00001";

            glkppais.EditValue = "00001";
            glkpdepartamento.EditValue = "00015";
            glkpprovincia.EditValue = null;
            glkpdistrito.EditValue = null;
            txtdireccion.Text = "";

            glkpgrupofam.EditValue = null;
            glkprangorent.EditValue = null;
            glkpCanal.EditValue = null;

            meObservacion.Text = "";
            glkpCampana.EditValue = null;

            glkpCanal.EditValue = null;
            glkpOrigenprospecto.EditValue = null;
            //glkpEjecutivo.EditValue = null;

            List<eUsuario> ListEjecutivos = new List<eUsuario>();
            ListEjecutivos = unit.Campanha.ListarEjecutivosVentasMenu<eUsuario>(1, "", cod_proyecto, Program.Sesion.Usuario.cod_usuario);
            glkpEjecutivo.Properties.ValueMember = "cod_usuario";
            glkpEjecutivo.Properties.DisplayMember = "dsc_usuario";
            glkpEjecutivo.Properties.DataSource = ListEjecutivos;
            var cod_usuario = ListEjecutivos.Where(x => x.cod_usuario == Program.Sesion.Usuario.cod_usuario).Select(x => x.cod_usuario).ToArray();
            glkpEjecutivo.EditValue = cod_usuario.Count() > 0 ? cod_usuario[0] : null;

            txtUsuarioRegistro.Text = "";
            txtUsuarioCambio.Text = "";
            dtFechaRegistro.Text = "";
            dtFechaModificacion.Text = "";
            btnVerCampanha.Enabled = false;
        }
        private void ValidaEstadoProspecto()
        {
            if (cod_estado_prospecto == codEstadoProspectoCerrado)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                Habilitar_evento(false);
            }
        }
        private string Habilitar_Prospecto()
        {
            string result = "";

            eCampanha eCamp = AsignarValoresprospecto_Habilitar();
            eCamp = unit.Campanha.Habilitar_prospecto<eCampanha>(eCamp);
            if (eCamp != null)
            {
                cod_prospecto = eCamp.cod_prospecto;
                cod_proyecto = eCamp.cod_proyecto;
                result = "OK";
            }
            return result;
        }

        private string validacionGuardar()
        {
            if (glkpempresa.EditValue == null) { glkpempresa.ShowPopup(); return "Seleccione un proyecto"; }
            if (glkpCanal.EditValue == null) { glkpCanal.ShowPopup(); return "Seleccione un canal"; }
            if (glkpOrigenprospecto.EditValue == null) { glkpOrigenprospecto.ShowPopup(); return "Seleccione el punto de contacto del prospecto"; }
            if (rbgtippersona.EditValue.ToString() == "JU")
            {
                if (txtrazsoc.Text.Trim() == "") { txtrazsoc.Focus(); return "Ingrese una razon social"; }
            }
            else
            {
                if (txtnombres.Text.Trim() == "") { txtnombres.Focus(); return "Ingrese el nombre de la persona"; }
            }
            if (validarCantidadNumerosCadena(txttelefonomovil.Text.Trim()) < 11 && !new EmailAddressAttribute().IsValid(txtemail.Text.Trim()))
            {
                txtemail.Focus();
                return "Ingrese un correo electronico o telefono movil";
            }

            //if (glkpCampana.EditValue == null) { glkpCampana.ShowPopup(); return "Seleccione una campaña"; }

            return null;
        }

        void Iniciar_GuardarProspecto()
        {
            try
            {
                string mensaje = validacionGuardar();
                if (mensaje != null) { MessageBox.Show(mensaje, "Mantenimiento de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                string result = "";
                switch (MiAccion)
                {
                    case evento.Nuevo: result = Guardar_Prospecto(); break;
                    case evento.Nuevo_externo: result = Guardar_Prospecto(); break;
                    case evento.Editar: result = Modificar_Prospecto(); break;
                }

                if (result == "OK")
                {
                    MessageBox.Show("Se guardó el prospecto de manera satisfactoria", "Mantenimiento de prospectos", MessageBoxButtons.OK);

                    glkpempresa.Enabled = false;
                    ActualizarListado = "SI";
                    validarCargarListado = 1;
                    //if (MiAccion == evento.Nuevo)
                    //{
                    eCampanha ListProspectos_temp = unit.Campanha.ObtenerProspecto<eCampanha>(3, cod_empresa, cod_proyecto, cod_prospecto);
                    o_eProspecto = unit.Proyectos.ListarProspecto<eProspectosXLote>(3, cod_empresa, cod_proyecto, cod_prospecto).FirstOrDefault();

                    if (ListProspectos_temp == null) { return; }
                    txtCodprospecto.Text = ListProspectos_temp.cod_prospecto;
                    cod_estado_prospecto = ListProspectos_temp.cod_estado_prospecto;
                    
                    SeleccionaBotonEstadoProspecto(cod_estado_prospecto);



                    //AsignaEjecutivo_Prospecto("NO");
                    //if (frmHandler != null)
                    //{
                    //    int nRow = frmHandler.gvListaprospecto.FocusedRowHandle;
                    //    frmHandler.frmListadoProspecto_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                    //    frmHandler.gvListaprospecto.FocusedRowHandle = nRow;
                    //    gvEventos_FocusedRowChanged(null, null);
                    //    frmHandler.sEstadoFiltro = sEstadoFiltro;
                    //    frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                    //    frmHandler.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    //    frmHandler.CodMenu = CodMenu;
                    //    this.Close();
                    //}

                    layoutControlItem79.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItem80.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    glkpEjecutivo.Enabled = true;
                    if (MiAccion == evento.Nuevo_externo || MiAccion == evento.Nuevo)
                    {
                        o_eCamp = ListProspectos_temp;
                        xtpMenuEvento.PageEnabled = true;
                        xtpMenuAsignaciones.PageEnabled = true;
                        xtpMenu_SelectedPageChanged(null, null);
                        rbgtippersona_SelectedIndexChanged(null, null);
                        LimpiarCamposEventos();
                        ListarEventos();
                        ListarHistoricoAsignaciones();
                        ValidaEstadoProspecto();
                        Habilitar_evento(false);
                        MiAccion = evento.Editar;
                    }


                    //LimpiarCamposProspecto();
                    //}
                    //else if (MiAccion == evento.Nuevo_externo)
                    //{
                    //    //AsignaEjecutivo_Prospecto("NO");
                    //    //this.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Iniciar_HabilitarProspecto()
        {
            try
            {

                DialogResult msgresult = MessageBox.Show("¿Está seguro de habilitar este prospecto?", "Habilitar Prospecto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    string result = "";
                    result = Habilitar_Prospecto();

                    if (result == "OK")
                    {
                        MessageBox.Show("Se habilitó el prospecto de manera satisfactoria", "Mantenimiento de prospectos", MessageBoxButtons.OK);
                        glkpempresa.Enabled = false;
                        ActualizarListado = "SI";
                        validarCargarListado = 1;

                        cod_estado_prospecto = codEstadoProspectoEnProceso;
                        SeleccionaBotonEstadoProspecto(cod_estado_prospecto);

                        //AsignaEjecutivo_Prospecto("SI");
                        //if (frmHandler != null)
                        //{
                        //    int nRow = frmHandler.gvListaprospecto.FocusedRowHandle;
                        //    frmHandler.frmListadoProspecto_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        //    frmHandler.gvListaprospecto.FocusedRowHandle = nRow;
                        //    frmHandler.sEstadoFiltro = sEstadoFiltro;
                        //    frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                        //    frmHandler.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                        //    frmHandler.CodMenu = CodMenu;
                        //    eCampanha oPerfil = frmHandler.ListProspectos_grilla.Find(x => x.cod_proyecto == cod_proyecto && x.cod_prospecto == cod_prospecto);
                        //    //eCampanha obj = frmHandler.gvListaprospecto.GetFocusedRow() as eCampanha;
                        //    if (oPerfil == null) { return; }

                        //    //btnNuevo.Enabled = true;

                        //    MiAccion = evento.Editar;
                        //    o_eCamp = oPerfil;
                        //    //IndicadorConfirmacionAuto = obj.flg_confirmacion;
                        //    IndicadorConfirmacionAuto = oPerfil.flg_confirmacion;
                        //    cod_ejecutivo = oPerfil.cod_ejecutivo.ToString();
                        //    //cod_ejecutivo = obj.cod_ejecutivo.ToString();
                        //    Inicializar();
                        //    //this.Close();
                        //}

                        btn_HabilitarPro.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string Guardar_Prospecto()
        {
            string result = "";
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            eCampanha eCamp = AsignarValoresprospecto();
            eCampanha eCampResul = unit.Campanha.Guardar_Actualizar_prospecto<eCampanha>(eCamp, "Nuevo");
            if (eCampResul != null)
            {
                if (eCampResul.cod_prospecto == "")
                {
                    MessageBox.Show(eCampResul.mensaje, "Mantenimiento de prospectos", MessageBoxButtons.OK);
                    result = "NO";
                }
                else
                {
                    eCampanha eCampResultado = new eCampanha();
                    if (glkpEjecutivo.EditValue != null)
                    {
                        eCampanha obj = new eCampanha();
                        obj.cod_ejecutivo = glkpEjecutivo.EditValue.ToString();
                        obj.cod_empresa = eCamp.cod_empresa;
                        obj.cod_proyecto = eCamp.cod_proyecto;
                        obj.cod_prospecto = eCampResul.cod_prospecto;
                        obj.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                        //obj.cod_origen_prospecto = "";
                        eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(1, obj);
                    }
                    //if (eCampResultado.cod_prospecto != null)
                    //{
                    //    MessageBox.Show("Se asigno el prospecto de manera satisfactoria", "Asignando Prospecto", MessageBoxButtons.OK);
                    //}
                    txtCodprospecto.Text = eCampResul.cod_prospecto;

                    lblNombreprospecto.Text = txtnombres.Text + " " + txtapepaterno.Text;
                    cod_prospecto = eCampResul.cod_prospecto;
                    cod_proyecto = eCamp.cod_proyecto;
                    cod_empresa = eCamp.cod_empresa;
                    result = "OK";
                }

            }
            else
            {
                MessageBox.Show("Hubo un problema al guardar el prospecto", "Asignando Prospecto", MessageBoxButtons.OK);
            }
            SplashScreenManager.CloseForm();
            return result;
        }
        private string Modificar_Prospecto()
        {
            string result = "";

            eCampanha eCamp = AsignarValoresprospecto();
            eCamp = unit.Campanha.Guardar_Actualizar_prospecto<eCampanha>(eCamp, "Actualizar");

            if (eCamp != null)
            {
                if (eCamp.cod_prospecto == "")
                {
                    MessageBox.Show(eCamp.mensaje, "Mantenimiento de prospectos", MessageBoxButtons.OK);
                    result = "NO";
                }
                else
                {
                    cod_prospecto = eCamp.cod_prospecto;
                    result = "OK";
                }
            }
            return result;
        }
        void AsignaEjecutivo_Prospecto(string IndUltimo)
        {
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

            string[] Codigo = glkpempresa.EditValue.ToString().Split("|".ToCharArray());
            tmpCamp.cod_empresa = Codigo[0].ToString();
            tmpCamp.cod_proyecto = Codigo[1].ToString();

            DataTable tabla = new DataTable();
            tabla = unit.Campanha.ObtenerListadoGridLookup("ejecutivos", tmpCamp.cod_usuario, tmpCamp.cod_empresa, tmpCamp.cod_proyecto, tmpCamp.valor_1, tmpCamp.valor_4);

            if (IndUltimo == "SI")
            {
                string sUltimoEjecutivo = "";
                if (gvAsignaciones.RowCount > 0) { sUltimoEjecutivo = gvAsignaciones.GetRowCellValue(0, "cod_ejecutivo").ToString(); }
                else { sUltimoEjecutivo = Program.Sesion.Usuario.cod_usuario; }

                var rows = tabla.AsEnumerable().Where(row => row.Field<string>("cod_ejecutivo") == sUltimoEjecutivo);
                if (rows.Any())
                {
                    tabla = rows.CopyToDataTable<DataRow>();
                }

            }
            tabla.Rows.Add("", "[Sin Ejecutivo]");


            XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignación de Prospecto";
            LookUpEdit lkpEjecutivo = new LookUpEdit(); lkpEjecutivo.Width = 120;
            lkpEjecutivo.Properties.ValueMember = "cod_ejecutivo"; lkpEjecutivo.Properties.DisplayMember = "dsc_ejecutivo";
            lkpEjecutivo.Properties.NullText = "[Sin Ejecutivo]";
            lkpEjecutivo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[]
            {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_ejecutivo", "Ejecutivo"),
            });

            unit.Globales.CargarCombosGridLookup(tabla, lkpEjecutivo, "cod_ejecutivo", "dsc_ejecutivo", "", false);
            args.Editor = lkpEjecutivo;
            var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
            if ((res == DialogResult.OK || res == DialogResult.Yes) && lkpEjecutivo.EditValue != null)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Asignando Prospecto", "Cargando...");

                eCampanha eCampResultado = new eCampanha();
                eCampanha eCamp = new eCampanha();
                eCamp.cod_ejecutivo = lkpEjecutivo.EditValue.ToString();
                eCamp.cod_empresa = Codigo[0].ToString();
                eCamp.cod_proyecto = Codigo[1].ToString();
                eCamp.cod_prospecto = cod_prospecto;
                eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                eCamp.cod_origen_prospecto = "";
                eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(1, eCamp);

                SplashScreenManager.CloseForm();

                if (eCampResultado.cod_prospecto != null)
                {
                    MessageBox.Show("Se asigno el prospecto de manera satisfactoria", "Asignando Prospecto", MessageBoxButtons.OK);
                }
            }
        }


        private void LimpiarCamposEventos()
        {
            cod_evento = "";
            cod_tipo_contacto = "";
            cod_evento_principal = "";
            petpcita.BackColor = Color.FromArgb(240, 240, 240);
            petpvisita.BackColor = Color.FromArgb(240, 240, 240);
            petpllamada.BackColor = Color.FromArgb(240, 240, 240);
            petpcorreo.BackColor = Color.FromArgb(240, 240, 240);
            petpwtsp.BackColor = Color.FromArgb(240, 240, 240);
            petpvideollamada.BackColor = Color.FromArgb(240, 240, 240);
            petpPresencial.BackColor = Color.FromArgb(240, 240, 240);

            cod_respuesta = "";

            foreach (var buton in lstBotonesDinamicos)
            {
                buton.Appearance.BackColor = Color.Transparent;
                buton.Appearance.ForeColor = Color.Black;
            }

            cod_expectativa = "";
            btnBaja.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btnMedia.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btnAlta.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btnMuyAlta.Appearance.BackColor = Color.FromArgb(240, 240, 240);

            deeFecha.EditValue = null;
            tieFecha.EditValue = null;
            deeFecha.EditValue = DateTime.Now;
            tieFecha.EditValue = DateTime.Now;

            chkReceptivo.Checked = false;

            meeObs.Text = "";
            glkpeDetalleRespuesta.EditValue = null;
            ValidaMotivoNoInteres("", "");


            // Proximo
            depeFecha.EditValue = null;
            tipeFecha.EditValue = null;
            mepeObs.Text = "";
            cod_tipo_contacto_2 = "";
            cod_tipo_confirmacion = "";
            pepecita.BackColor = Color.FromArgb(240, 240, 240);
            pepevisita.BackColor = Color.FromArgb(240, 240, 240);
            pepellamada.BackColor = Color.FromArgb(240, 240, 240);
            pepecorreo.BackColor = Color.FromArgb(240, 240, 240);
            pepewtsp.BackColor = Color.FromArgb(240, 240, 240);
            pepevideollamada.BackColor = Color.FromArgb(240, 240, 240);

            glkpeEjecutivoCita.EditValue = null;
            lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            pepellamadaConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepewtspConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepecorreoConfir.BackColor = Color.FromArgb(240, 240, 240);
            depeConfirmacion.EditValue = null;
            tipeConfirmacion.EditValue = null;
            sIndicadorCarga = "0";
            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //layoutControlItem55.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;


        }
        void Habilitar_evento(bool bEstado)
        {
            petpcita.Properties.ReadOnly = !bEstado;
            petpvisita.Properties.ReadOnly = !bEstado;
            petpllamada.Properties.ReadOnly = !bEstado;
            petpcorreo.Properties.ReadOnly = !bEstado;
            petpwtsp.Properties.ReadOnly = !bEstado;
            petpvideollamada.Properties.ReadOnly = !bEstado;
            petpPresencial.Properties.ReadOnly = !bEstado;

            foreach (var buton in lstBotonesDinamicos)
            {
                buton.Enabled = bEstado;
            }


            btnBaja.Enabled = bEstado;
            btnMedia.Enabled = bEstado;
            btnAlta.Enabled = bEstado;
            btnMuyAlta.Enabled = bEstado;

            deeFecha.ReadOnly = !bEstado;
            tieFecha.ReadOnly = !bEstado;

            meeObs.ReadOnly = !bEstado;
            glkpeDetalleRespuesta.ReadOnly = !bEstado;

            glkpeMotivoNoInteres.ReadOnly = !bEstado;
            //if (bEstado == true)
            //{
            //    lciMotivoNoInteres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            //else if (bEstado == false)
            //{
            //    lciMotivoNoInteres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}

            // Proximo
            depeFecha.ReadOnly = !bEstado;
            tipeFecha.ReadOnly = !bEstado;
            mepeObs.ReadOnly = !bEstado;

            pepecita.Properties.ReadOnly = !bEstado;
            pepevisita.Properties.ReadOnly = !bEstado;
            pepellamada.Properties.ReadOnly = !bEstado;
            pepecorreo.Properties.ReadOnly = !bEstado;
            pepewtsp.Properties.ReadOnly = !bEstado;
            pepevideollamada.Properties.ReadOnly = !bEstado;

            glkpeEjecutivoCita.ReadOnly = !bEstado;
            chkReceptivo.ReadOnly = !bEstado;
            //lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //if (bEstado == true)
            //{
            //    lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            //else
            //{
            //    lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}




        }
        void Iniciar_GuardarEvento()
        {
            try
            {
                if (gvEventos.RowCount > 0)
                {
                    eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
                    if (obj == null) { return; }
                    //if (gvEventos.GetRowCellValue(0, "flg_confirmacion").ToString() == "NO" && gvEventos.GetRowCellValue(0, "flg_tiene_confirmacion_evento").ToString() == "SI" && gvEventos.GetRowCellValue(1, "cod_tipo_evento").ToString() != codTipoConfirmacionEventoProspecto)
                    if (obj.flg_confirmacion == "NO" && obj.flg_tiene_confirmacion_evento == "SI" && obj.cod_tipo_evento != codTipoConfirmacionEventoProspecto)
                    {
                        MessageBox.Show("Realice la confirmación del evento", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); gvEventos.Focus(); return;
                    }
                }

                if (deeFecha.EditValue == null) { MessageBox.Show("Seleccione una fecha de atención", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); deeFecha.Focus(); return; }
                if (glkpeDetalleRespuesta.Properties.View.RowCount > 0)
                {
                    if (glkpeDetalleRespuesta.EditValue == null) { MessageBox.Show("Seleccione un detalle de respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpeDetalleRespuesta.Show(); return; }
                }
                if (cod_tipo_contacto == "") { MessageBox.Show("Seleccione el tipo de contacto", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (cod_respuesta == "") { MessageBox.Show("Seleccione la respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpeDetalleRespuesta.Show(); return; }

                //if (simpleLabelItem5.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                if (layoutControlItem8.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && MiAccion == evento.Nuevo)
                {
                    if (cod_expectativa == "") { MessageBox.Show("Seleccione la expectativa", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }

                if (lciMotivoNoInteres.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    if (glkpeMotivoNoInteres.EditValue == null) { MessageBox.Show("Seleccione el motivo del no interes", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpeMotivoNoInteres.ShowPopup(); return; }
                }

                string result = "";
                result = Guardar_Evento();
                bool esSeparacion = false;

                if (result == "OK")
                {

                    //MessageBox.Show("Se guardó el evento de manera satisfactoria", "Registro de eventos", MessageBoxButtons.OK);
                    if (glkpeDetalleRespuesta.EditValue.ToString() == "DRECO008") { esSeparacion = true; }
                    LimpiarCamposEventos();
                    ListarEventos();

                    //List<eCampanha> ActProspecto = new List<eCampanha>();
                    //ActProspecto = unit.Campanha.ListarProspectos<eCampanha>(2, cod_empresa, cod_proyecto, cod_prospecto, Program.Sesion.Usuario.cod_usuario, "", "", "");
                    //o_eCamp = ActProspecto[0];
                    gvEventos_FocusedRowChanged(null, null);
                    //Editar();
                    ValidaEstadoProspecto();
                    Habilitar_evento(false);
                    sIndicadorCarga = "0";
                    validarCargarListado = 1;

                    if (esSeparacion)
                    {
                        string query = "select * from vtama_cliente where cod_prospecto ='" + o_eProspecto.cod_prospecto + "'";
                        eCliente existeCliente = unit.Clientes.ObtenerClientePorProspecto<eCliente>(query).FirstOrDefault();

                        frmSepararLote frmSepLote = new frmSepararLote();
                        frmSepLote.MiAccion = Separacion.Nuevo;
                        frmSepLote.cod_ejecutivo = cod_ejecutivo;
                        frmSepLote.codigo = cod_proyecto;
                        frmSepLote.codigoMultiple = "";
                        frmSepLote.cod_empresa = cod_empresa;
                        frmSepLote.dsc_proyecto = glkpempresa.Text;
                        frmSepLote.o_eProspecto = o_eProspecto;
                        frmSepLote.cod_prospecto = o_eProspecto.cod_prospecto;

                        if (existeCliente == null)
                        {
                            MessageBox.Show("Este prospecto no tiene un registro de cliente. \n Debe crear el cliente.", "Registro de eventos", MessageBoxButtons.OK);
                            frmSepLote.fromMantEvent = true;
                        }
                        else
                        {
                            frmSepLote.transferirDatos(existeCliente);
                        }
                        frmSepLote.ShowDialog();
                    }

                    //eCampanha ListProspectos_temp = unit.Campanha.ObtenerProspecto<eCampanha>(3, cod_empresa, cod_proyecto, cod_prospecto);

                    //if (ListProspectos_temp.cod_estado_prospecto != null)
                    //    SeleccionaBotonEstadoProspecto(ListProspectos_temp.cod_estado_prospecto);
                    //if (frmHandler != null)
                    //{
                    //    int nRow = frmHandler.gvListaprospecto.FocusedRowHandle;
                    //    frmHandler.frmListadoProspecto_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                    //    frmHandler.gvListaprospecto.FocusedRowHandle = nRow;
                    //    gvEventos_FocusedRowChanged(null, null);
                    //    frmHandler.sEstadoFiltro = sEstadoFiltro;
                    //    frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                    //    frmHandler.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    //    frmHandler.CodMenu = CodMenu;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Iniciar_GuardarEvento_Proximo()
        {
            try
            {
                if (gvEventos.RowCount > 0)
                {
                    if (gvEventos.GetRowCellValue(0, "flg_confirmacion").ToString() == "NO")
                    {
                        //MessageBox.Show("Realice la confirmación del evento", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); gvEventos.Focus(); return;
                    }
                }
                //eCampanha eEventoCam = gvEventos.GetRowHandle()

                if (deeFecha.EditValue == null) { MessageBox.Show("Seleccione una fecha de atención", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); deeFecha.ShowPopup(); return; }
                if (glkpeDetalleRespuesta.EditValue == null) { MessageBox.Show("Seleccione un detalle de respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpeDetalleRespuesta.ShowPopup(); return; }
                if (cod_tipo_contacto == "") { MessageBox.Show("Seleccione el tipo de contacto", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (cod_respuesta == "") { MessageBox.Show("Seleccione la respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (cod_expectativa == "") { MessageBox.Show("Seleccione la expectativa", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


                if (cod_IndProximo == "SI")
                {
                    if (depeFecha.EditValue == null) { MessageBox.Show("Seleccione la fecha de próxima atención", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); depeFecha.ShowPopup(); return; }
                    if (tipeFecha.EditValue == null) { MessageBox.Show("Seleccione la fecha de próxima atención", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); tipeFecha.ShowPopup(); return; }
                    if (String.IsNullOrEmpty(cod_tipo_contacto_2)) { MessageBox.Show("Seleccione el tipo de contacto", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    DateTime dtFechaEvento = new DateTime();
                    DateTime dtFechaProximoEvento = new DateTime();

                    dtFechaEvento = Convert.ToDateTime(deeFecha.Text.ToString() + " " + tieFecha.Text.ToString());
                    dtFechaProximoEvento = Convert.ToDateTime(depeFecha.Text.ToString() + " " + tipeFecha.Text.ToString());

                    //if (dtFechaEvento >= dtFechaProximoEvento)
                    //{
                    //    MessageBox.Show("La fecha del próximo evento no puede ser menor o igual a la del evento principal", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                    //}

                    if (lciEjecutivoCita.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always & glkpeEjecutivoCita.EditValue == null)
                    {
                        MessageBox.Show("Seleccione quien recibira la cita o visita", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                    }

                    if (lytConfirmar.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && depeConfirmacion.EditValue != null)
                    {
                        DateTime dtFechaConfirmacionEvento = new DateTime();
                        dtFechaConfirmacionEvento = Convert.ToDateTime(depeConfirmacion.Text.ToString() + " " + tipeConfirmacion.Text.ToString());
                        if (dtFechaConfirmacionEvento > dtFechaProximoEvento)
                        {
                            MessageBox.Show("La fecha confirmación del próximo evento no puede ser mayor a la del evento", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                        }

                        if (dtFechaConfirmacionEvento < dtFechaEvento)
                        {
                            MessageBox.Show("La fecha confirmación del próximo evento no puede ser menor a la del evento principal", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                        }
                    }
                }
                string result = "";
                result = Guardar_Evento_Proximo();

                if (result == "OK")
                {
                    //MessageBox.Show("Se guardó el evento de manera satisfactoria", "Registro de eventos", MessageBoxButtons.OK);
                    LimpiarCamposEventos();
                    ListarEventos();
                    Habilitar_evento(false);
                    sIndicadorCarga = "0";
                    validarCargarListado = 1;
                    eCampanha ListProspectos_temp = unit.Campanha.ObtenerProspecto<eCampanha>(3, cod_empresa, cod_proyecto, cod_prospecto);

                    if (ListProspectos_temp.cod_estado_prospecto != null)
                        SeleccionaBotonEstadoProspecto(ListProspectos_temp.cod_estado_prospecto);
                    //if (frmHandler != null)
                    //{
                    //    int nRow = frmHandler.gvListaprospecto.FocusedRowHandle;
                    //    frmHandler.frmListadoProspecto_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                    //    frmHandler.gvListaprospecto.FocusedRowHandle = nRow;
                    //    gvEventos_FocusedRowChanged(null, null);
                    //    frmHandler.sEstadoFiltro = sEstadoFiltro;
                    //    frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                    //    frmHandler.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    //    frmHandler.CodMenu = CodMenu;
                    //}


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string Guardar_Evento()
        {
            string result = "";

            eCampanha eCamp = AsignarValoresEventos();
            eCamp = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp, "1");
            if (eCamp != null)
            {
                cod_evento_principal = eCamp.cod_evento;
                cod_estado_prospecto = eCamp.cod_estado_prospecto;
                if (!String.IsNullOrEmpty(cod_estado_prospecto)) { SeleccionaBotonEstadoProspecto(cod_estado_prospecto); }
                result = "OK";
            }
            return result;
        }

        private string validarConfirmacionCampos()
        {
            if (depeConfirmacion.EditValue == null)
            {
                depeConfirmacion.ShowPopup();
                return "Debe seleccionar la fecha de confirmación";
            }
            if (tipeConfirmacion.EditValue == null)
            {
                tipeConfirmacion.ShowPopup();
                return "Debe seleccionar la hora de confirmación";
            }
            return null;
        }

        private string Guardar_Evento_Proximo()
        {
            string result = "";
            string resultado = "";
            if (cod_tipo_confirmacion != "")
            {
                resultado = validarConfirmacionCampos();
                if (resultado != null)
                {
                    MessageBox.Show(resultado, "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return "";
                }
            }
            //if (MiAccion == evento.Editar && cod_tipo_confirmacion == "") { string resultado = validarConfirmacionCampos(); }
            eCampanha eCamp = AsignarValoresEventos();
            eCamp.cod_evento = !String.IsNullOrEmpty(cod_evento_principal) ? cod_evento_principal : eCamp.cod_evento;
            eCamp = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp, "1");
            if (eCamp != null)
            {
                cod_evento = eCamp.cod_evento;
                cod_evento_principal = eCamp.cod_evento;
                cod_estado_prospecto = eCamp.cod_estado_prospecto;
                if (!String.IsNullOrEmpty(eCamp.cod_estado_prospecto)) { SeleccionaBotonEstadoProspecto(cod_estado_prospecto); }
                //cod_eventoProximo = eCamp.cod_evento_ref;

                if (cod_tipo_contacto_2 != "")
                {
                    eCampanha eCamp_pro = AsignarValoresEventos_Proximo();
                    //eCamp_pro.cod_evento = cod_eventoProximo;
                    eCamp_pro.cod_evento_ref = cod_evento;
                    eCamp_pro.cod_evento_principal = cod_evento_principal;

                    eCamp_pro = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp_pro, "2");
                    if (eCamp_pro != null)
                    {
                        cod_evento = eCamp_pro.cod_evento;
                        cod_eventoProximo = eCamp_pro.cod_evento;
                        if (cod_tipo_confirmacion != "")
                        {
                            eCampanha eCamp_conf = AsignarValoresEventos_Confirmacion();
                            eCamp_conf.cod_evento_ref = cod_eventoProximo;
                            eCamp_conf.cod_evento_principal = cod_evento_principal;
                            eCamp_conf = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp_conf, "7");

                            if (eCamp_conf != null)
                            {
                                cod_evento_confirmacion = eCamp_conf.cod_evento;
                                cod_evento = eCamp_conf.cod_evento;
                                result = "OK";
                            }

                        }
                        else
                        {
                            cod_evento = eCamp_pro.cod_evento;
                            result = "OK";
                        }

                    }
                }
                else
                {
                    if (eCamp != null)
                    {
                        cod_evento = eCamp.cod_evento;
                        result = "OK";
                    }
                }
            }

            return result;
        }

        private string Guardar_Evento_Confirmacion()
        {
            string result = "";

            eCampanha eCamp = AsignarValoresEventos();
            eCamp = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp, "1");
            if (eCamp != null)
            {
                cod_evento = eCamp.cod_evento;

                if (cod_tipo_contacto_2 != "")
                {
                    eCampanha eCamp_pro = AsignarValoresEventos_Proximo();
                    eCamp_pro.cod_evento_ref = cod_evento;
                    eCamp_pro = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp_pro, "2");
                    if (eCamp_pro != null)
                    {
                        cod_evento = eCamp_pro.cod_evento;
                        result = "OK";
                    }
                }
                else
                {
                    if (eCamp != null)
                    {
                        cod_evento = eCamp.cod_evento;
                        result = "OK";
                    }
                }
            }

            return result;
        }

        void OpcionVer_Expectativa(int Ind)
        {
            //simpleLabelItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem47.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem48.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem49.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControl4.Enabled = true;

            if (Ind == 1)
            {
                if (cod_respuesta == codResultadoEventoProspectoSinRespuesta | cod_respuesta == codResultadoEventoProspectoContactoInvalido)
                {
                    //simpleLabelItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem47.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem48.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem49.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControl4.Enabled = false;

                }
            }
            else if (Ind == 2)
            {
                if (glkpeDetalleRespuesta.Properties.View.RowCount > 0)
                {
                    if (glkpeDetalleRespuesta.EditValue == null)
                    {
                        //simpleLabelItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem47.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem48.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem49.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControl4.Enabled = false;

                    }
                    else if (glkpeDetalleRespuesta.EditValue.ToString() == "DRECO004" || glkpeDetalleRespuesta.EditValue.ToString() == "DRECO007" || glkpeDetalleRespuesta.EditValue.ToString() == "DRECO008")
                    {
                        //simpleLabelItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem47.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem48.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem49.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControl4.Enabled = false;

                    }
                }
            }
        }



        private eCampanha AsignarValoresprospecto_Habilitar()
        {
            eCampanha eCamp = new eCampanha();

            string[] Codigo = glkpempresa.EditValue.ToString().Split("|".ToCharArray());
            eCamp.cod_empresa = Codigo[0].ToString();
            eCamp.cod_proyecto = Codigo[1].ToString();
            eCamp.cod_prospecto = txtCodprospecto.Text;
            eCamp.cod_estado_prospecto = codEstadoProspectoNoAsignado;

            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

            return eCamp;
        }
        private eCampanha AsignarValoresprospecto()
        {
            eCampanha eCamp = new eCampanha();

            string[] Codigo = glkpempresa.EditValue.ToString().Split("|".ToCharArray());
            eCamp.cod_empresa = Codigo[0].ToString();
            eCamp.cod_proyecto = Codigo[1].ToString();

            eCamp.cod_prospecto = txtCodprospecto.Text;
            eCamp.cod_origen_prospecto = glkpOrigenprospecto.EditValue.ToString();
            eCamp.cod_segmento = lkpSegmento.EditValue == null ? null : lkpSegmento.EditValue.ToString();

            eCamp.cod_tipo_persona = rbgtippersona.EditValue.ToString();
            eCamp.dsc_apellido_paterno = txtapepaterno.Text;
            eCamp.dsc_apellido_materno = txtapematerno.Text;
            eCamp.dsc_nombres = txtnombres.Text;
            eCamp.dsc_prospecto = rbgtippersona.EditValue.ToString() == "JU" ? txtrazsoc.Text : "";

            eCamp.cod_tipo_documento = glkpdocide.EditValue == null ? "" : glkpdocide.EditValue.ToString();
            eCamp.dsc_num_documento = txtnrodocide.Text;
            eCamp.fch_fec_nac = de_fechanac.EditValue == null ? "" : de_fechanac.EditValue.ToString();
            eCamp.cod_sexo = glkpsexo.EditValue == null ? "" : glkpsexo.EditValue.ToString();

            eCamp.dsc_email = txtemail.Text;
            eCamp.dsc_telefono = txttelefono.Text;
            eCamp.dsc_telefono_movil = txttelefonomovil.Text;

            eCamp.dsc_profesion = txtprofesion.Text;
            eCamp.cod_estado_civil = glkpestdocivil.EditValue == null ? "" : glkpestdocivil.EditValue.ToString();
            eCamp.cod_nacionalidad = glkpnacionalidad.EditValue == null ? "" : glkpnacionalidad.EditValue.ToString();

            eCamp.cod_pais = glkppais.EditValue == null ? "" : glkppais.EditValue.ToString();
            eCamp.cod_departamento = glkpdepartamento.EditValue == null ? "" : glkpdepartamento.EditValue.ToString();
            eCamp.cod_provincia = glkpprovincia.EditValue == null ? "" : glkpprovincia.EditValue.ToString();
            eCamp.cod_distrito = glkpdistrito.EditValue == null ? "" : glkpdistrito.EditValue.ToString();
            eCamp.dsc_direccion = txtdireccion.Text;

            eCamp.cod_grupo_familiar = glkpgrupofam.EditValue == null ? "" : glkpgrupofam.EditValue.ToString();
            eCamp.cod_rango_renta = glkprangorent.EditValue == null ? "" : glkprangorent.EditValue.ToString();

            eCamp.cod_referencia_campanha = glkpCampana.EditValue == null ? "" : glkpCampana.EditValue.ToString();

            eCamp.dsc_observacion = meObservacion.Text;
            eCamp.cod_estado_prospecto = codEstadoProspectoNoAsignado;
            eCamp.cod_ejecutivo = glkpEjecutivo.EditValue == null ? "" : glkpEjecutivo.EditValue.ToString();
            eCamp.cod_canal = glkpCanal.EditValue.ToString();
            eCamp.cod_estado_prospecto = glkpEjecutivo.EditValue == null ? "ESTPR001" : "ESTPR002";
            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

            return eCamp;
        }
        private eCampanha AsignarValoresEventos()
        {
            eCampanha eCamp = new eCampanha();

            eCamp.cod_evento = cod_evento;
            eCamp.cod_evento_ref = cod_eventoProximo;
            eCamp.cod_empresa = cod_empresa;
            eCamp.cod_proyecto = cod_proyecto;
            eCamp.cod_prospecto = cod_prospecto;
            eCamp.cod_tipo_evento = codTipoEventoProspecto;

            //eCamp.fch_evento = deeFecha.Text.ToString() + " " + tieFecha.Text.ToString();
            string fechaHora = deeFecha.DateTime.ToString("dd-MM-yyyy");
            DateTime fecha = Convert.ToDateTime(fechaHora);
            DateTime hora = tieFecha.Time;
            DateTime fechaConvertida = fecha.AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second);
            eCamp.fch_evento = fechaConvertida;
            eCamp.cod_tipo_contacto = cod_tipo_contacto;
            eCamp.cod_respuesta = cod_respuesta;

            if (glkpeDetalleRespuesta.Properties.View.RowCount > 0)
            {
                eCamp.cod_detalle_respuesta = glkpeDetalleRespuesta.EditValue.ToString();
            }
            else
            {
                eCamp.cod_detalle_respuesta = "";
            }

            eCamp.dsc_observacion = meeObs.Text;

            //if (simpleLabelItem5.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            if (layoutControlItem8.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                eCamp.cod_expectativa = cod_expectativa;
            }
            else
            {
                eCamp.cod_expectativa = "";
            }

            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

            if (lciReceptivo.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                if (chkReceptivo.Checked == true)
                {
                    eCamp.flg_receptivo = "SI";
                }
                else
                {
                    eCamp.flg_receptivo = "NO";
                }
            }
            else
            {
                eCamp.flg_receptivo = "NO";
            }
            eCamp.cod_ejecutivo_cita = "";

            if (lciMotivoNoInteres.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                eCamp.cod_motivo = glkpeMotivoNoInteres.EditValue == null ? "" : glkpeMotivoNoInteres.EditValue.ToString();
            }
            else
            {
                eCamp.cod_motivo = "";
            }
            eCamp.cod_evento_ref = "";

            return eCamp;
        }


        private eCampanha AsignarValoresEventos_Proximo()
        {
            eCampanha eCamp = new eCampanha();

            eCamp.cod_evento = cod_eventoProximo;
            eCamp.cod_empresa = cod_empresa;
            eCamp.cod_proyecto = cod_proyecto;
            eCamp.cod_prospecto = cod_prospecto;
            eCamp.cod_tipo_evento = codTipoProximoEventoProspecto;
            //eCamp.fch_evento = depeFecha.Text.ToString() + " " + tipeFecha.Text.ToString();
            string fechaHora = depeFecha.DateTime.ToString("dd-MM-yyyy");
            DateTime fecha = Convert.ToDateTime(fechaHora);
            DateTime hora = tipeFecha.Time;
            DateTime fechaConvertida = fecha.AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second);
            eCamp.fch_evento = fechaConvertida;
            eCamp.cod_respuesta = codResultadoEventoProspectoPendiente;
            eCamp.cod_detalle_respuesta = "";
            eCamp.dsc_observacion = mepeObs.Text;
            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

            eCamp.cod_tipo_contacto = cod_tipo_contacto_2;
            eCamp.cod_expectativa = "";

            if (lciEjecutivoCita.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                eCamp.cod_ejecutivo_cita = glkpeEjecutivoCita.EditValue.ToString();
            }
            else
            {
                eCamp.cod_ejecutivo_cita = "";
            }

            eCamp.flg_receptivo = "NO";
            eCamp.cod_motivo = "";

            return eCamp;
        }

        private eCampanha AsignarValoresEventos_Confirmacion()
        {
            eCampanha eCamp = new eCampanha();

            eCamp.cod_evento = cod_evento_confirmacion;
            eCamp.cod_empresa = cod_empresa;
            eCamp.cod_proyecto = cod_proyecto;
            eCamp.cod_prospecto = cod_prospecto;
            eCamp.cod_tipo_evento = codTipoConfirmacionEventoProspecto;
            //eCamp.fch_evento = depeFecha.Text.ToString() + " " + tipeFecha.Text.ToString();
            string fechaHora = depeConfirmacion.DateTime.ToString("dd-MM-yyyy");
            DateTime fecha = Convert.ToDateTime(fechaHora);
            DateTime hora = tipeConfirmacion.Time;
            DateTime fechaConvertida = fecha.AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second);
            eCamp.fch_evento = fechaConvertida;
            eCamp.cod_respuesta = codResultadoEventoProspectoPendiente;
            eCamp.cod_detalle_respuesta = "";
            eCamp.dsc_observacion = "";
            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

            eCamp.cod_tipo_contacto = cod_tipo_confirmacion;
            eCamp.cod_expectativa = "";

            if (glkpeEjecutivoCita.EditValue != null)
            {
                eCamp.cod_ejecutivo_cita = glkpeEjecutivoCita.EditValue.ToString();
            }
            else
            {
                eCamp.cod_ejecutivo_cita = "";
            }

            eCamp.flg_receptivo = "NO";
            eCamp.cod_motivo = "";

            return eCamp;
        }


        private PictureEdit SeleccionaBotonTipoProceso(PictureEdit oPictureEdit)
        {

            petpcita.BackColor = Color.FromArgb(240, 240, 240);
            petpvisita.BackColor = Color.FromArgb(240, 240, 240);
            petpllamada.BackColor = Color.FromArgb(240, 240, 240);
            petpcorreo.BackColor = Color.FromArgb(240, 240, 240);
            petpwtsp.BackColor = Color.FromArgb(240, 240, 240);
            petpvideollamada.BackColor = Color.FromArgb(240, 240, 240);
            petpPresencial.BackColor = Color.FromArgb(240, 240, 240);
            oPictureEdit.BackColor = Color.FromArgb(89, 139, 125);

            return oPictureEdit;
        }
        private SimpleButton SeleccionaBotonRespuesta(SimpleButton oSimpleButton)
        {
            //btneEfectiva.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            //btneNoEfectiva.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            //btnContactoInvalido.Appearance.BackColor = Color.FromArgb(240, 240, 240);

            oSimpleButton.Appearance.BackColor = Color.FromArgb(89, 139, 125);

            return oSimpleButton;
        }
        private SimpleButton SeleccionaBotonExpectativa(SimpleButton oSimpleButton)
        {
            btnBaja.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btnMedia.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btnAlta.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btnMuyAlta.Appearance.BackColor = Color.FromArgb(240, 240, 240);

            oSimpleButton.Appearance.BackColor = Color.FromArgb(89, 139, 125);

            return oSimpleButton;
        }
        private PictureEdit SeleccionaBotonTipoProceso_Proximo(PictureEdit oPictureEdit)
        {
            pepecita.BackColor = Color.FromArgb(240, 240, 240);
            pepevisita.BackColor = Color.FromArgb(240, 240, 240);
            pepellamada.BackColor = Color.FromArgb(240, 240, 240);
            pepecorreo.BackColor = Color.FromArgb(240, 240, 240);
            pepewtsp.BackColor = Color.FromArgb(240, 240, 240);
            pepevideollamada.BackColor = Color.FromArgb(240, 240, 240);

            oPictureEdit.BackColor = Color.FromArgb(89, 139, 125);

            return oPictureEdit;
        }

        private PictureEdit SeleccionaBotonConfirmacion_Proximo(PictureEdit oPictureEdit)
        {
            pepellamadaConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepewtspConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepecorreoConfir.BackColor = Color.FromArgb(240, 240, 240);

            oPictureEdit.BackColor = Color.FromArgb(89, 139, 125);


            return oPictureEdit;
        }

        void ValidaMotivoNoInteres(string sCodDetalleRespuesta, string sCodMotivo)
        {
            if (sCodDetalleRespuesta == codEstadoProspectoDetalleResultadoContacto)
            {
                lciMotivoNoInteres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                if (sCodMotivo != "")
                {
                    glkpeMotivoNoInteres.EditValue = sCodMotivo;
                }
                else
                {
                    glkpeMotivoNoInteres.EditValue = null;
                }
            }
            else
            {
                lciMotivoNoInteres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                glkpeMotivoNoInteres.EditValue = null;
            }
        }



        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            layoutControlItem79.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem80.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            MiAccion = evento.Nuevo;
            cod_eventoProximo = "";
            if (xtpMenu.SelectedTabPageIndex == 0)
            {
                LimpiarCamposProspecto();
                xtpMenuEvento.PageEnabled = false;
                xtpMenuAsignaciones.PageEnabled = false;
                glkpempresa.Enabled = true;
                cod_estado_prospecto = "";

                SeleccionaBotonEstadoProspecto(cod_estado_prospecto);
            }
            else if (xtpMenu.SelectedTabPageIndex == 1)
            {
                if (cod_estado_prospecto == codEstadoProspectoCerrado)
                {
                    MessageBox.Show("El prospecto se encuentra en estado cerrado.", "Registro de eventos", MessageBoxButtons.OK);
                }
                else
                {
                    if (o_eCamp.cod_ejecutivo == null)
                    {
                        MessageBox.Show("El prospecto no tiene asignado un ejecutivo", "Mantenimiento de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //if (gvEventos.RowCount > 0)
                        //{
                        //    if (o_eCamp.flg_habilitado == "SI")
                        //    {
                        //        //LimpiarCamposEventos();
                        //        //Habilitar_evento(true);
                        //        sIndicadorCarga = "1";
                        //        //return;
                        //    }
                        //    else if (gvEventos.GetRowCellValue(0, "flg_confirmacion").ToString() == "NO" && gvEventos.GetRowCellValue(0, "cod_respuesta").ToString() == codResultadoEventoProspectoPendiente)
                        //    {
                        //        MessageBox.Show("Realice la confirmación del evento.", "Registro de eventos", MessageBoxButtons.OK);
                        //        return;
                        //    }
                        //}
                        LimpiarCamposEventos();
                        Habilitar_evento(true);
                        sIndicadorCarga = "1";
                    }
                }
            }
            else
            {

            }
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtpMenu.SelectedTabPageIndex == 0)
            {
                Iniciar_GuardarProspecto();
            }
            else if (xtpMenu.SelectedTabPageIndex == 1)
            {
            }
            else
            {

            }
        }



        private void btn_HabilitarPro_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Iniciar_HabilitarProspecto();
        }

        private void layoutControlItem69_Click(object sender, EventArgs e)
        {
            siguienteCliente();
        }
        void siguienteCliente()
        {
            //Siguiente Cliente
            try
            {
                int tRow = frmHandler.gvListaprospecto.RowCount - 1;
                int nRow = frmHandler.gvListaprospecto.FocusedRowHandle;
                frmHandler.gvListaprospecto.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;


                o_eCamp = frmHandler.gvListaprospecto.GetFocusedRow() as eCampanha;
                o_eCamp.cod_estado_prospecto = o_eCamp.cod_estado_prospecto.Replace("delete,", "");
                IndicadorConfirmacionAuto = o_eCamp.flg_confirmacion;
                Editar();
                glkpempresa.Enabled = false;
                xtpMenu.SelectedTabPageIndex = 0;

                xtpMenu_SelectedPageChanged(null, null);
                rbgtippersona_SelectedIndexChanged(null, null);
                LimpiarCamposEventos();
                ListarEventos();
                ListarHistoricoAsignaciones();
                ValidaEstadoProspecto();
                Habilitar_evento(false);

                rbgtippersona.Select();
                HabilitarBotones();
                //if (IndicadorConfirmacionAuto == "NO")
                //{
                //    gvEventos_DoubleClick(null, null);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void layoutControlItem46_Click(object sender, EventArgs e)
        {
            anteriorCliente();
        }

        void anteriorCliente()
        {
            //Anterior Cliente
            try
            {
                int tRow = frmHandler.gvListaprospecto.RowCount - 1;
                int nRow = frmHandler.gvListaprospecto.FocusedRowHandle;
                frmHandler.gvListaprospecto.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                o_eCamp = frmHandler.gvListaprospecto.GetFocusedRow() as eCampanha;
                o_eCamp.cod_estado_prospecto = o_eCamp.cod_estado_prospecto.Replace("delete,", "");
                IndicadorConfirmacionAuto = o_eCamp.flg_confirmacion;
                Editar();
                glkpempresa.Enabled = false;
                xtpMenu.SelectedTabPageIndex = 0;



                xtpMenu_SelectedPageChanged(null, null);
                rbgtippersona_SelectedIndexChanged(null, null);
                LimpiarCamposEventos();
                ListarEventos();
                ListarHistoricoAsignaciones();
                ValidaEstadoProspecto();
                Habilitar_evento(false);

                rbgtippersona.Select();
                HabilitarBotones();
                //if (IndicadorConfirmacionAuto == "NO")
                //{
                //    gvEventos_DoubleClick(null, null);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGrabarEvento_Click(object sender, EventArgs e)
        {
            Iniciar_GuardarEvento();
        }
        private void btnGrabarProximoEvento_Click(object sender, EventArgs e)
        {
            Iniciar_GuardarEvento_Proximo();
        }
        private void xtpMenu_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtpMenu.SelectedTabPageIndex == 0 || xtpMenu.SelectedTabPageIndex == 1)
            {
                btnGuardar.Enabled = true;
                btnNuevo.Enabled = true;

                if (xtpMenu.SelectedTabPageIndex == 0)
                {
                    btnGuardar.Enabled = true;
                }
                else
                {
                    btnGuardar.Enabled = false;
                }
            }
            else
            {
                btnGuardar.Enabled = false;
                btnNuevo.Enabled = false;
            }
            if (cod_estado_prospecto == codEstadoProspectoCerrado)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                Habilitar_evento(false);
            }
            HabilitarBotonesAsesorProximoEvento();
            //if(perfil == "VISUALIZADOR")
            //{
            //    btnGuardar.Enabled = false;
            //    btnNuevo.Enabled = false;
            //}
        }

        private void glkpCanal_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpCanal.EditValue == null)
            {
                glkpOrigenprospecto.ReadOnly = true;
                glkpCanal.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple; return;
            }
            else
            {
                glkpCanal.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                glkpOrigenprospecto.ReadOnly = false;
            }
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = cod_empresa;
            tmpCamp.cod_proyecto = "";
            tmpCamp.valor_1 = glkpCanal.EditValue == null ? "" : glkpCanal.EditValue.ToString();
            glkpOrigenprospecto.EditValue = null;
            unit.Campanha.CargarCombos_TablasMaestras("1", "tipo_campanha", glkpOrigenprospecto, "cod_tipo_campanha", "dsc_tipo_campanha", tmpCamp);
        }

        private void btnVerCampanha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                frmPopupCampanhaInfo frm = new frmPopupCampanhaInfo();
                frm.cod_prospecto = cod_prospecto;
                frm.cod_empresa = cod_empresa;
                frm.cod_proyecto = cod_proyecto;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picSiguienteCliente_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
            if (obj == null) { return; }

            int nFila = gvEventos.FocusedRowHandle;
            if (obj.cod_respuesta.ToString() == codResultadoEventoProspectoPendiente || obj.cod_respuesta.ToString() == "RECON001") //PROXIMO EVENTO PENDINETE DE RESPUESTA
            {

                abrirVentanaEventoConfirmacion();

                //if (obj.flg_confirmacion.ToString() != "SI" & o_eCamp.cod_ejecutivo != null) // PROXIMO EVENTO PENDINETE DE RESPUESTA - NO CONFIRMADO
                //{
                //    frmMantConfirmacion frm = new frmMantConfirmacion(this);
                //    frm.MiAccion = evento.Nuevo;
                //    //frm.perfil = perfil;
                //    frm.cod_empresa = cod_empresa;
                //    frm.cod_evento = obj.cod_evento_ref;
                //    frm.cod_evento_confirmacion = cod_evento;
                //    frm.cod_proyecto = cod_proyecto;
                //    frm.cod_prospecto = cod_prospecto;
                //    frm.cod_ejecutivo = obj.cod_ejecutivo.ToString();
                //    frm.o_eCamp = obj;
                //    frm.Titulo = lblNombreprospecto.Text;
                //    frm.sEstadoFiltro = sEstadoFiltro;
                //    frm.sTipoContactoFiltro = sTipoContactoFiltro;
                //    frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                //    frm.CodMenu = CodMenu;
                //    frm.ShowDialog(this);
                //}
                //else // PROXIMO EVENTO PENDINETE DE RESPUESTA - CONFIRMADO
                //{

                //}
            }
            else // PROXIMO EVENTO CON RESPUESTA
            {

            }
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            frmCalendarioProspecto frm = new frmCalendarioProspecto();
            frm.ListCalendario = ListTemp;
            frm.fecha = Convert.ToDateTime(ccCalendarioEventos.EditValue);
            frm.ShowDialog();
        }

        private void glkpempresa_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpempresa.EditValue == null)
            {
                glkpempresa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
            else
            {
                eCampanha tmpCamp = new eCampanha();
                tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                tmpCamp.cod_empresa = cod_empresa;
                tmpCamp.cod_proyecto = glkpempresa.EditValue.ToString();
                glkpempresa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                unit.Campanha.CargarCombos_TablasMaestras("1", "segmento", lkpSegmento, "cod_segmento", "dsc_segmento", tmpCamp);
                lkpSegmento.ReadOnly = false;
            }
        }


        private void glkpCampana_EditValueChanged(object sender, EventArgs e)
        {
            //if (glkpCampana.EditValue == null)
            //{
            //    glkpCampana.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //}
            //else
            //{
            //    glkpCampana.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            //}
        }

        private void pcChevron_Click(object sender, EventArgs e)
        {
            ocultarMostrarLeyenda();
        }

        private void gvEventos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCampanha obj = gvEventos.GetRow(e.RowHandle) as eCampanha;
                    if (obj.cod_tipo_evento == "TPEVN003" && obj.cod_respuesta == "RECON006") { e.Appearance.BackColor = Color.LightPink; e.Appearance.ForeColor = Color.Red; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    if (obj.cod_tipo_evento == "TPEVN003" && obj.cod_respuesta != "RECON006") { e.Appearance.BackColor = Color.LightBlue; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }

                    if (e.Column.FieldName == "cod_tipo_evento") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_confirmacion" && (obj.cod_tipo_contacto != "TPCON001" && obj.cod_tipo_contacto != "TPCON002" && obj.cod_tipo_contacto != "TPCON006")) e.DisplayText = "";

                    e.DefaultDraw();
                    if (e.Column.FieldName == "cod_tipo_evento")
                    {
                        string cellValue = e.CellValue.ToString();
                        Image circle = cellValue == "TPEVN001" && obj.cod_evento == "00000001" ? ImgCircleGreen : cellValue == "TPEVN003" && obj.cod_respuesta == "RECON006" ? ImgCircleRed : cellValue == "TPEVN003" && obj.cod_respuesta != "RECON006" ? ImgCircleBlue :ImgCircleYellow;
                        e.Handled = true; e.Graphics.DrawImage(circle, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvEventos_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Clicks == 2) abrirVentanaEventoConfirmacion();
                if (e.Clicks == 1) gvEventos_FocusedRowChanged(gvEventos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gvEventos.FocusedRowHandle - 1, gvEventos.FocusedRowHandle));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
            if (obj == null || obj.cod_respuesta != "RECON006") { return; }
            DialogResult msgresult = MessageBox.Show("Se cancelara el evento. \n¿Desea continuar?.", "Seguimiento de Eventos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                eCampanha eCamp = unit.Campanha.ActualizarEstadoEvento<eCampanha>(0, obj);
                if (eCamp != null && !String.IsNullOrEmpty(eCamp.cod_evento))
                {
                    //MessageBox.Show("Evento Cancelado", "Seguimiento de Eventos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listeventos = unit.Campanha.ListarEventos<eCampanha>(0, cod_empresa, cod_prospecto, Program.Sesion.Usuario.cod_usuario);
                    bsEventos.DataSource = Listeventos;
                    btnCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    validarCargarListado = 1;
                }
                else
                {
                    MessageBox.Show("No se pudo cancelar el evento", "Seguimiento de Eventos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void petpPresencial_Click(object sender, EventArgs e)
        {
            if (petpPresencial.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso(petpPresencial);
                cod_tipo_contacto = codTipoContactoProspectoPresencial;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void abrirVentanaEventoConfirmacion()
        {
            eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
            if (obj.cod_tipo_evento == "TPEVN003" && obj.flg_confirmacion != "SI")
            {
                frmMantConfirmacion frm = new frmMantConfirmacion(this);
                frm.MiAccion = evento.Editar;
                //frm.perfil = perfil;
                cod_evento = obj.cod_evento;
                frm.cod_empresa = cod_empresa;
                frm.cod_evento = obj.cod_evento_ref;
                frm.cod_evento_confirmacion = cod_evento;
                frm.cod_proyecto = cod_proyecto;
                frm.cod_prospecto = cod_prospecto;
                frm.cod_ejecutivo = obj.cod_ejecutivo;
                frm.o_eCamp = obj;
                string petpSeleccionado = petpllamada.BackColor == Color.FromArgb(89, 139, 125) ? petpllamada.ToolTip : petpwtsp.BackColor == Color.FromArgb(89, 139, 125) ? petpwtsp.ToolTip : petpcorreo.ToolTip;
                frm.Titulo = lblNombreprospecto.Text + $" | {petpSeleccionado} de Confirmación {Convert.ToDateTime(obj.fch_evento).ToString("dd/MM/yyyy  HH:mm")}";
                frm.sEstadoFiltro = sEstadoFiltro;
                frm.sTipoContactoFiltro = sTipoContactoFiltro;
                frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                frm.CodMenu = CodMenu;
                frm.ShowDialog(this);
            }
        }

        private void txtrazsoc_EditValueChanged(object sender, EventArgs e)
        {
            if (rbgtippersona.EditValue.ToString() == "JU")
            {
                if (txtrazsoc.Text.Trim() == "")
                {
                    txtrazsoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                }
                else
                {
                    txtrazsoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                }

            }
        }

        private void txtnombres_EditValueChanged(object sender, EventArgs e)
        {
            if (rbgtippersona.EditValue == "NA")
            {
                if (txtnombres.Text.Trim() == "")
                {
                    txtnombres.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                }
                else
                {
                    txtnombres.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                }
            }
        }

        private void txttelefonomovil_EditValueChanged(object sender, EventArgs e)
        {
            if (validarCantidadNumerosCadena(txttelefonomovil.Text.Trim()) < 11 && !new EmailAddressAttribute().IsValid(txtemail.Text.Trim()))
            {
                txttelefonomovil.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                txtemail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
            else
            {
                txttelefonomovil.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                txtemail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            }
        }


        private int validarCantidadNumerosCadena(string campoValidar)
        {
            string str = campoValidar;
            List<string> chars = new List<string>();
            chars.AddRange(str.Select(c => c.ToString()));


            int n = 0;
            foreach (var a in chars)
            {
                int[] k = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (int b in k)
                {
                    if (b.ToString() == a) n += 1;
                }
            }
            return n;
        }

        private void txtemail_EditValueChanged(object sender, EventArgs e)
        {
            if (validarCantidadNumerosCadena(txttelefonomovil.Text.Trim()) < 11 && !new EmailAddressAttribute().IsValid(txtemail.Text.Trim()))
            {
                txttelefonomovil.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                txtemail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
            else
            {
                txttelefonomovil.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                txtemail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            }
        }

        private void pepellamadaConfir_Click(object sender, EventArgs e)
        {
            if (pepellamadaConfir.ReadOnly == false)
            {
                if (pepellamadaConfir.BackColor == Color.FromArgb(89, 139, 125))
                {
                    pepellamadaConfir.BackColor = Color.FromArgb(240, 240, 240); cod_tipo_confirmacion = ""; return;
                }
                SeleccionaBotonConfirmacion_Proximo(pepellamadaConfir);
                cod_tipo_confirmacion = codTipoContactoProspectoLlamada;
            }
        }

        private void pepewtspConfir_Click(object sender, EventArgs e)
        {
            if (pepewtspConfir.ReadOnly == false)
            {
                if (pepewtspConfir.BackColor == Color.FromArgb(89, 139, 125))
                {
                    pepewtspConfir.BackColor = Color.FromArgb(240, 240, 240); cod_tipo_confirmacion = ""; return;
                }
                SeleccionaBotonConfirmacion_Proximo(pepewtspConfir);
                cod_tipo_confirmacion = codTipoContactoProspectoWhatsApp;
            }
        }

        private void pepecorreoConfir_Click(object sender, EventArgs e)
        {
            if (pepecorreoConfir.ReadOnly == false)
            {
                if (pepecorreoConfir.BackColor == Color.FromArgb(89, 139, 125))
                {
                    pepecorreoConfir.BackColor = Color.FromArgb(240, 240, 240); cod_tipo_confirmacion = ""; return;
                }
                SeleccionaBotonConfirmacion_Proximo(pepecorreoConfir);
                cod_tipo_confirmacion = codTipoContactoProspectoCorreo;
            }
        }

        private void gvListaEmpresas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void gvListaEmpresas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
        private void gvListaEmpresas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void btnHistorial_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (gvEventos.RowCount > 0)
            {
                if (gvEventos.FocusedRowHandle >= 0)
                {
                    frmMantConfirmacion frm = new frmMantConfirmacion(this);
                    eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
                    frm.MiAccion = evento.Vista;
                    frm.o_eCamp = obj;
                    frm.Titulo = lblNombreprospecto.Text;

                    frm.cod_empresa = cod_empresa;
                    frm.cod_evento = "";
                    frm.cod_proyecto = cod_proyecto;
                    frm.cod_evento = obj.cod_evento_ref;
                    frm.cod_evento_confirmacion = obj.cod_evento;
                    frm.cod_prospecto = cod_prospecto;
                    frm.cod_ejecutivo = obj.cod_ejecutivo;
                    frm.sEstadoFiltro = sEstadoFiltro;
                    frm.sTipoContactoFiltro = sTipoContactoFiltro;
                    frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    frm.CodMenu = CodMenu;
                    frm.ShowDialog(this);
                }
            }
        }

        private void gvEventos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvEventos.RowCount > 0)
                {
                    if (e != null && e.FocusedRowHandle >= 0)
                    {
                        eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
                        if (obj == null) { return; }
                        string sTipoEvento = obj.cod_tipo_contacto; // gvEventos.GetRowCellValue(e.FocusedRowHandle, "cod_tipo_contacto").ToString();
                        string sReconfirmacion = obj.flg_confirmacion; // gvEventos.GetRowCellValue(e.FocusedRowHandle, "flg_confirmacion").ToString();
                        cod_evento = obj.cod_evento;
                        cod_tipo_evento = obj.cod_tipo_evento;  //gvEventos.GetRowCellValue(e.FocusedRowHandle, "cod_tipo_evento").ToString();
                        cod_evento_principal = obj.cod_evento;
                        cod_eventoProximo = obj.flg_tiene_proximo_evento == "SI" ? obj.cod_proximo_evento : "";
                        cod_evento_confirmacion = obj.flg_tiene_confirmacion_evento == "SI" ? obj.cod_confirmacion_evento : "";
                        //cod_eventoProximo = gvEventos.GetRowCellValue(e.FocusedRowHandle, "cod_evento_ref").ToString();
                        //if ((sTipoEvento == "TPCON001" || sTipoEvento == "TPCON002") && (sReconfirmacion == "" || sReconfirmacion == "SI"))
                        if ((obj.cod_tipo_evento == "TPEVN003") && (sReconfirmacion == "" || sReconfirmacion == "SI" || obj.cod_respuesta != codResultadoEventoProspectoPendiente))
                        {
                            btnHistorial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                        else
                        {
                            btnHistorial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }
                        //if (sReconfirmacion == "NO")
                        if (obj.cod_tipo_evento == "TPEVN003" && sReconfirmacion != "SI" && obj.cod_respuesta == codResultadoEventoProspectoPendiente)
                        {
                            btnConfirmar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                        else
                        {
                            btnConfirmar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                        }
                        if (obj.cod_respuesta == codResultadoEventoProspectoPendiente && obj.cod_tipo_evento == "TPEVN002")
                        {
                            btnCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                        else
                        {
                            btnCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                        }

                        LimpiarCamposEventos();
                        sIndicadorCarga = "1";

                        Editar_Evento(obj);

                        sIndicadorCarga = "0";
                        eCampanha oListEventosConfirmacion = new eCampanha();
                        eCampanha oListEventosProximos = new eCampanha();
                        if (obj.flg_tiene_confirmacion_evento == "SI") { oListEventosConfirmacion = Listeventos.Find(x => x.cod_tipo_evento == "TPEVN003" && x.cod_evento == obj.cod_confirmacion_evento); }
                        else { oListEventosConfirmacion = Listeventos.Find(x => x.cod_tipo_evento == "TPEVN003" && x.cod_evento_ref == obj.cod_evento); }

                        if (obj.flg_tiene_proximo_evento == "SI") { oListEventosProximos = Listeventos.Find(x => x.cod_tipo_evento == "TPEVN002" && x.cod_evento == obj.cod_proximo_evento); }


                        //Listeventos
                        if (cod_tipo_evento == "TPEVN001" && oListEventosConfirmacion == null && Listeventos.Count > 1)
                        {
                            if (cod_tipo_contacto_2 == codTipoContactoProspectoVideoLlamada || cod_tipo_contacto_2 == codTipoContactoProspectoVisita)
                            {
                                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            }
                            else
                            {
                                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            }
                        }
                        else if (cod_tipo_evento == "TPEVN001" && oListEventosConfirmacion != null)
                        {
                            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            Habilitar_evento(false);
                            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        }



                        else if (cod_tipo_evento == "TPEVN002" && obj.flg_tiene_proximo_evento == "SI" && obj.flg_tiene_confirmacion_evento == "SI")
                        {
                            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            HabilitarCamposConfirmacion(true);
                            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        }
                        else if (cod_tipo_evento == "TPEVN002" && obj.flg_tiene_proximo_evento == "SI" && obj.flg_tiene_confirmacion_evento != "SI" && sReconfirmacion != "SI")
                        {
                            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            Habilitar_evento(false);
                            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            HabilitarCamposConfirmacion(false);
                        }
                        else if (cod_tipo_evento == "TPEVN002" && obj.flg_tiene_proximo_evento == "SI" && obj.flg_tiene_confirmacion_evento != "SI" && oListEventosConfirmacion != null)
                        {
                            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            Habilitar_evento(false);
                            if (sReconfirmacion != "SI")
                            {
                                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                Habilitar_evento(false);
                                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                HabilitarCamposConfirmacion(false);
                                //layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            }
                            else
                            {
                                HabilitarCamposConfirmacion(true);
                                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            }

                        }
                        else if (cod_tipo_evento == "TPEVN002" && obj.flg_tiene_proximo_evento != "SI" && obj.flg_tiene_confirmacion_evento != "SI" && sReconfirmacion != "SI" && oListEventosConfirmacion == null && obj.cod_respuesta != codResultadoEventoProspectoPendiente)
                        {
                            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            Habilitar_evento(false);
                            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            pepellamadaConfir.ReadOnly = pepellamada.ReadOnly;
                            pepewtspConfir.ReadOnly = pepellamada.ReadOnly;
                            pepecorreoConfir.ReadOnly = pepellamada.ReadOnly;

                            depeConfirmacion.Enabled = !pepellamada.ReadOnly;
                            tipeConfirmacion.Enabled = !pepellamada.ReadOnly;

                            btnGrabarProximoEvento.Enabled = !pepellamada.ReadOnly;
                        }
                        else if (cod_tipo_evento == "TPEVN002" && obj.flg_tiene_proximo_evento != "SI" && obj.flg_tiene_confirmacion_evento != "SI" && sReconfirmacion != "SI" && oListEventosConfirmacion == null )
                        {
                            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            Habilitar_evento(true);
                            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            pepellamadaConfir.ReadOnly = pepellamada.ReadOnly;
                            pepewtspConfir.ReadOnly = pepellamada.ReadOnly;
                            pepecorreoConfir.ReadOnly = pepellamada.ReadOnly;

                            depeConfirmacion.Enabled = !pepellamada.ReadOnly;
                            tipeConfirmacion.Enabled = !pepellamada.ReadOnly;

                            btnGrabarProximoEvento.Enabled = !pepellamada.ReadOnly;
                        }
                        //else if (cod_tipo_evento == "TPEVN002" && obj.flg_tiene_proximo_evento != "SI" && obj.flg_tiene_confirmacion_evento != "SI" && sReconfirmacion != "SI" && oListEventosConfirmacion != null)
                        //{
                        //    lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        //    Habilitar_evento(false);
                        //    layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        //}

                        //else if (cod_tipo_evento == "TPEVN002" && sReconfirmacion != "SI" && oListEventos != null && oListEventos.flg_confirmacion == "SI")
                        //{
                        //    //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        //    Habilitar_evento(true);
                        //    layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        //}
                        //else if (cod_tipo_evento == "TPEVN002" && sReconfirmacion != "SI" && oListEventos != null && oListEventos.flg_confirmacion != "SI")
                        //{
                        //    lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        //    lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        //    HabilitarCamposConfirmacion(true);
                        //}
                        //else if (cod_tipo_evento == "TPEVN002" &&  obj.flg_tiene_confirmacion_evento == "SI" && obj.flg_confirmacion == "NO" && obj.cod_respuesta == codResultadoEventoProspectoPendiente)
                        //{
                        //    lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        //    Habilitar_evento(true);
                        //    layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        //}
                        else
                        {
                            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            foreach (var buton in lstBotonesDinamicos)
                            {
                                buton.Enabled = !pepecorreo.ReadOnly;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ocultarMostrarLeyenda()
        {
            if (validar == 0)
            {
                System.Drawing.Image imgProyectoImagen = Properties.Resources.chevron_right_16px;
                pcChevron.EditValue = imgProyectoImagen;
                validar = 1;
                //layoutLeyendaOcultar.ContentVisible = false;
                layoutLeyendaOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                pcChevron.ToolTipTitle = "Ocultar Leyenda";
                return;
            }
            if (validar == 1)
            {
                System.Drawing.Image imgProyectoLogo = Properties.Resources.chevron_left_16px;
                pcChevron.EditValue = imgProyectoLogo;
                validar = 0;
                //layoutLeyendaOcultar.ContentVisible = true;
                layoutLeyendaOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                pcChevron.ToolTipTitle = "Mostrar Leyenda";
                return;
            }
        }

        private void gvEventos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvEventos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);

            if (e.RowHandle >= 0)
            {
                string sEstado = gvEventos.GetRowCellValue(e.RowHandle, "flg_activo").ToString();
                string sReconfirmacion = gvEventos.GetRowCellValue(e.RowHandle, "flg_confirmacion").ToString();

                if (sEstado == "NO")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (sReconfirmacion == "NO")
                {
                    GridView view = sender as GridView;

                    e.Appearance.ForeColor = Color.FromArgb(247, 177, 32);
                    e.Appearance.FontStyleDelta = FontStyle.Bold;

                    view.Appearance.FocusedRow.FontStyleDelta = FontStyle.Bold;
                    view.Appearance.FocusedRow.ForeColor = Color.FromArgb(23, 97, 143);
                    view.Appearance.FocusedCell.FontStyleDelta = FontStyle.Bold;
                    view.Appearance.FocusedCell.ForeColor = Color.FromArgb(23, 97, 143);
                    view.Appearance.HideSelectionRow.FontStyleDelta = FontStyle.Bold;
                    view.Appearance.HideSelectionRow.ForeColor = Color.FromArgb(23, 97, 143);
                    view.Appearance.SelectedRow.FontStyleDelta = FontStyle.Bold;
                    view.Appearance.SelectedRow.ForeColor = Color.FromArgb(23, 97, 143);
                }
            }
        }
        private void gvEventos_DoubleClick(object sender, EventArgs e)
        {
            if (gvEventos.RowCount > 0)
            {
                //if (gvEventos.FocusedRowHandle >= 0)
                //{
                //    LimpiarCamposEventos();
                //    sIndicadorCarga = "1";
                //    Editar_Evento(gvEventos.FocusedRowHandle);
                //    sIndicadorCarga = "0";
                //}
            }
        }

        private void gvAsignaciones_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void gvAsignaciones_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }


        private void petpcita_Click(object sender, EventArgs e)
        {
            if (petpcita.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso(petpcita);
                cod_tipo_contacto = codTipoContactoProspectoCita;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
            }
        }
        private void petpvisita_Click(object sender, EventArgs e)
        {
            if (petpvisita.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso(petpvisita);
                cod_tipo_contacto = codTipoContactoProspectoVisita;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
            }
        }
        private void petpllamada_Click(object sender, EventArgs e)
        {
            if (petpllamada.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso(petpllamada);
                cod_tipo_contacto = codTipoContactoProspectoLlamada;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        private void petpcorreo_Click(object sender, EventArgs e)
        {
            if (petpcorreo.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso(petpcorreo);
                cod_tipo_contacto = codTipoContactoProspectoCorreo;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        private void petpwtsp_Click(object sender, EventArgs e)
        {
            if (petpwtsp.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso(petpwtsp);
                cod_tipo_contacto = codTipoContactoProspectoWhatsApp;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        private void petpvideollamada_Click(object sender, EventArgs e)
        {
            if (petpvideollamada.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso(petpvideollamada);
                cod_tipo_contacto = codTipoContactoProspectoVideoLlamada;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                if (chkReceptivo.CheckState == CheckState.Unchecked)
                {
                    lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                //lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }




        private void btneEfectiva_Click(object sender, EventArgs e)
        {
            //SeleccionaBotonRespuesta(btneEfectiva);
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = "";
            cod_respuesta = codResultadoEventoProspectoExitoso;
            //cod_respuesta = cod_tipo_evento == "TPEVN003" ? codResultadoEventoProspectoReconfirmacion : codResultadoEventoProspectoExitoso;
            tmpCamp.valor_1 = cod_respuesta;
            tmpCamp.valor_4 = cod_tipo_contacto;
            cod_respuesta = cod_tipo_contacto == codTipoContactoProspectoLlamada ? codResultadoEventoProspectoExitoso :
                            cod_tipo_contacto == codTipoContactoProspectoWhatsApp ? codResultadoEventoProspectoExitoso :
                            cod_tipo_contacto == codTipoContactoProspectoCorreo ? codResultadoEventoProspectoExitoso :
                            cod_tipo_contacto == codTipoContactoProspectoPresencial ? codResultadoEventoProspectoExitoso :
                                                 codResultadoEventoProspectoAsistio;

            unit.Campanha.CargarCombos_TablasMaestras("1", "detalleresultadocontacto", glkpeDetalleRespuesta, "cod_detalle_respuesta", "dsc_detalle_respuesta", tmpCamp);
            lstDetalleRespuesta = unit.Campanha.CombosEnGridControl<eCampanha>("detalleresultadocontacto", tmpCamp.valor_1, tmpCamp.valor_4);
            OpcionVer_Expectativa(1);

        }
        private void btneNoEfectiva_Click(object sender, EventArgs e)
        {
            //SeleccionaBotonRespuesta(btneNoEfectiva);
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = "";

            //if (btneNoEfectiva.Text == "NO ASISTIO")
            //{
            //    cod_respuesta = codResultadoEventoProspectoNoAsistio;
            //}
            //else
            //{
            //    cod_respuesta = codResultadoEventoProspectoSinRespuesta;
            //}

            tmpCamp.valor_1 = cod_respuesta;
            tmpCamp.valor_4 = cod_tipo_contacto;

            unit.Campanha.CargarCombos_TablasMaestras("1", "detalleresultadocontacto", glkpeDetalleRespuesta, "cod_detalle_respuesta", "dsc_detalle_respuesta", tmpCamp);
            lstDetalleRespuesta = unit.Campanha.CombosEnGridControl<eCampanha>("detalleresultadocontacto", tmpCamp.valor_1, tmpCamp.valor_4);
            OpcionVer_Expectativa(1);
        }
        private void btnContactoInvalido_Click(object sender, EventArgs e)
        {
            //SeleccionaBotonRespuesta(btnContactoInvalido);
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = "";
            cod_respuesta = codResultadoEventoProspectoContactoInvalido;
            tmpCamp.valor_1 = cod_respuesta;
            tmpCamp.valor_4 = "";

            unit.Campanha.CargarCombos_TablasMaestras("1", "detalleresultadocontacto", glkpeDetalleRespuesta, "cod_detalle_respuesta", "dsc_detalle_respuesta", tmpCamp);
            OpcionVer_Expectativa(1);
        }



        private void pepellamada_Click(object sender, EventArgs e)
        {
            if (pepellamada.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso_Proximo(pepellamada);
                cod_tipo_contacto_2 = codTipoContactoProspectoLlamada;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                cod_tipo_confirmacion = "";
            }
        }
        private void pepecorreo_Click(object sender, EventArgs e)
        {
            if (pepecorreo.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso_Proximo(pepecorreo);
                cod_tipo_contacto_2 = codTipoContactoProspectoCorreo;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                cod_tipo_confirmacion = "";
            }
        }
        private void pepewtsp_Click(object sender, EventArgs e)
        {
            if (pepewtsp.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso_Proximo(pepewtsp);
                cod_tipo_contacto_2 = codTipoContactoProspectoWhatsApp;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                cod_tipo_confirmacion = "";
                depeFecha.EditValue = DateTime.Now;
                tipeFecha.EditValue = DateTime.Now;

            }
        }
        private void pepevideollamada_Click(object sender, EventArgs e)
        {
            if (pepevideollamada.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso_Proximo(pepevideollamada);
                cod_tipo_contacto_2 = codTipoContactoProspectoVideoLlamada;
                if (chkReceptivo.CheckState == CheckState.Unchecked)
                {
                    lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                glkpeEjecutivoCita.EditValue = o_eCamp.cod_ejecutivo;
                cod_tipo_confirmacion = "";
            }
        }
        private void pepecita_Click(object sender, EventArgs e)
        {
            if (pepecita.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso_Proximo(pepecita);
                cod_tipo_contacto_2 = codTipoContactoProspectoCita;
                if (chkReceptivo.CheckState == CheckState.Unchecked)
                {
                    lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                glkpeEjecutivoCita.EditValue = o_eCamp.cod_ejecutivo;
                HabilitarCamposConfirmacion(!true);
            }
        }
        private void pepevisita_Click(object sender, EventArgs e)
        {
            if (pepevisita.ReadOnly == false || sIndicadorCarga == "1")
            {
                SeleccionaBotonTipoProceso_Proximo(pepevisita);
                cod_tipo_contacto_2 = codTipoContactoProspectoVisita;
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                glkpeEjecutivoCita.EditValue = o_eCamp.cod_ejecutivo;
                HabilitarCamposConfirmacion(!true);
            }
        }


        private void btnBaja_Click(object sender, EventArgs e)
        {
            SeleccionaBotonExpectativa(btnBaja);
            cod_expectativa = codExpectativaProspectoBaja;
        }
        private void btnMedia_Click(object sender, EventArgs e)
        {
            SeleccionaBotonExpectativa(btnMedia);
            cod_expectativa = codExpectativaProspectoMedia;
        }
        private void btnAlta_Click(object sender, EventArgs e)
        {
            SeleccionaBotonExpectativa(btnAlta);
            cod_expectativa = codExpectativaProspectoAlta;
        }
        private void btnMuyAlta_Click(object sender, EventArgs e)
        {
            SeleccionaBotonExpectativa(btnMuyAlta);
            cod_expectativa = codExpectativaProspectoMuyAlta;
        }

        private void glkpeDetalleRespuesta_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpeDetalleRespuesta.EditValue == null) { return; }
            if (glkpeDetalleRespuesta.Properties.DataSource != null)
            {

                GridLookUpEdit lookUp = sender as GridLookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                if (dataRow != null)
                {
                    cod_IndProximo = dataRow["estado"].ToString();
                    var valor = "";
                    valor = glkpeDetalleRespuesta.EditValue == null ? null : glkpeDetalleRespuesta.EditValue.ToString() == "" ? null : glkpeDetalleRespuesta.EditValue.ToString();
                    ValidaMotivoNoInteres(valor, "");

                }



                //int count = glkpeDetalleRespuesta.Properties.View.RowCount;
                //if (count > 0)
                //{
                //    //DevExpress.XtraGrid.Views.Grid.GridView view = glkpeDetalleRespuesta.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                //    //object val = view.GetRowCellValue(view.FocusedRowHandle, "estado");
                //    var valor = "";
                //    valor = glkpeDetalleRespuesta.EditValue == null ? null : glkpeDetalleRespuesta.EditValue.ToString() == "" ? null : glkpeDetalleRespuesta.EditValue.ToString();
                //    if (valor != null)
                //    {

                //        var val = lstDetalleRespuesta.Where(c => c.cod_detalle_respuesta == valor).First().estado.ToString();
                //        //var val = lstDetalleRespuesta.Where(c => c.cod_detalle_respuesta == valor).First().estado.ToString();
                //        cod_IndProximo = val.ToString();

                //        ValidaMotivoNoInteres(valor, "");
                //    }
                //}
                else
                {
                    cod_IndProximo = "NO";
                }
                HabilitarBotonAceptar();
                OpcionVer_Expectativa(2);
            }
        }

        private void rbgtippersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rbgtippersona.EditValue == null) { return; }
            if (rbgtippersona.EditValue.ToString() == "JU")
            {
                ActivaCamposPersonaJuridica();
            }
            else
            {
                ActivaCamposPersonaNatural();
            }
        }
        private void glkppais_EditValueChanged(object sender, EventArgs e)
        {
            glkpdistrito.Properties.DataSource = null;
            glkpprovincia.Properties.DataSource = null;
            glkpdepartamento.Properties.DataSource = null;

            if (glkppais.EditValue != null)
            {
                unit.Clientes.CargaCombosLookUp("TipoDepartamento", glkpdepartamento, "cod_departamento", "dsc_departamento", "", cod_condicion: glkppais.EditValue.ToString());
            }
        }
        private void glkpdepartamento_EditValueChanged(object sender, EventArgs e)
        {
            glkpdistrito.Properties.DataSource = null;
            glkpprovincia.Properties.DataSource = null;

            if (glkpdepartamento.EditValue != null)
            {
                unit.Clientes.CargaCombosLookUp("TipoProvincia", glkpprovincia, "cod_provincia", "dsc_provincia", "", cod_condicion: glkpdepartamento.EditValue.ToString());
            }
        }
        private void glkpprovincia_EditValueChanged(object sender, EventArgs e)
        {
            glkpdistrito.Properties.DataSource = null;

            if (glkpprovincia.EditValue != null)
            {
                unit.Clientes.CargaCombosLookUp("TipoDistrito", glkpdistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: glkpprovincia.EditValue.ToString());
            }
        }
        private void glkpOrigenprospecto_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpOrigenprospecto.EditValue == null)
            {
                glkpOrigenprospecto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            }
            else
            {
                glkpOrigenprospecto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;

            }
        }


        private void ccCalendarioEventos_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (ListCalendario.Count > 0)
            {
                List<eCampanha> ListTemp = ListCalendario.Where(x => Convert.ToDateTime(x.fch_fecha) == Convert.ToDateTime(e.Date.ToString("dd/MM/yyyy"))).ToList();
                if (ListTemp.Count > 0)
                {
                    e.Style.BackColor = Color.FromArgb(146, 185, 99);
                }
            }
        }
        private void ccCalendarioEventos_EditValueChanged(object sender, EventArgs e)
        {
            ListTemp = ListCalendario.Where(x => Convert.ToDateTime(x.fch_fecha) == Convert.ToDateTime(ccCalendarioEventos.EditValue)).ToList();
            bsListaCalendario.DataSource = ListTemp;

            if (ListTemp.Count() <= 1) { layoutControlItem83.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; }
            else { layoutControlItem83.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }
        }


    }
}