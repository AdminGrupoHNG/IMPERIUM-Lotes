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
using System.IO;
using System.Diagnostics;
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;
using System.Net;
using Tesseract;
using RestSharp;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using DevExpress.XtraTab;
using System.ComponentModel.DataAnnotations;
using DevExpress.Data.Mask.Internal;
using DevExpress.XtraEditors.Mask;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;
using UI_GestionLotes.Formularios.Operaciones;
using Microsoft.Identity.Client;
using System.Security;
using System.Net.Http.Headers;
using UI_GestionLotes.Formularios.Gestion_Contratos;
using System.Windows.Media;
using Color = System.Drawing.Color;
using UI_GestionLotes.Formularios.Lotes;
using DevExpress.XtraReports.Design;
using RestSharp.Validation;

namespace UI_GestionLotes.Clientes_Y_Proveedores.Clientes
{
    internal enum Cliente
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    //internal enum ClienteSeparacion
    //{
    //    Nuevo = 0,
    //    Editar = 1,
    //    Vista = 2
    //}

    public partial class frmMantCliente : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        eCliente eCli = new eCliente();
        public List<eCliente.eCliente_Contactos> mylistContactos = new List<eCliente.eCliente_Contactos>();
        public List<eLotes_Separaciones> mySeparacionValidar = new List<eLotes_Separaciones>();
        public List<eCliente.eCliente_Observaciones> mylistObservaciones = new List<eCliente.eCliente_Observaciones>();
        public List<eCliente.eCliente_Documentos> mylistDocumentosCliente = new List<eCliente.eCliente_Documentos>();
        public List<eCliente.eCliente_Documentos> mylistDocumentos = new List<eCliente.eCliente_Documentos>();
        public List<eCliente.eCliente_Documentos> mylistvalidar = new List<eCliente.eCliente_Documentos>();
        public List<eProformas.eProformas_Detalle> lstProDetalle = new List<eProformas.eProformas_Detalle>();

        List<eProyecto> lstProyecto = new List<eProyecto>();
        frmListadoClientes frmHandler;
        frmSepararLote frmHandlerSeparacion;
        frmMantContratos frmHandlerContrato;
        internal Cliente MiAccion = Cliente.Nuevo;
        internal Cliente MiAccionSeparacion = Cliente.Nuevo;
        public eProformas campos_proforma = new eProformas();
        //public eProspectosXLote campos_prospecto = new eProspectosXLote();
        public eLotes_Separaciones campos_separaciones = new eLotes_Separaciones();
        eLotes_Separaciones eLotSep = new eLotes_Separaciones();
        List<eCliente> ListClienteProyecto = new List<eCliente>();
        public eCampanha ecamp_prospecto = new eCampanha();
        List<eProyecto> ListProyecto = new List<eProyecto>();
        List<eCliente_Contactos> ListClienteContacto = new List<eCliente_Contactos>();
        List<eCliente_Direccion> ListDirecc = new List<eCliente_Direccion>();
        eProspectosXLote transferenciaDatosDirec = new eProspectosXLote();
        List<eCliente_Contactos> ListDireccionContacto = new List<eCliente_Contactos>();
        List<eCliente_Ubicacion> ListUbic = new List<eCliente_Ubicacion>();
        List<eCliente_CentroResponsabilidad> ListCentroResp = new List<eCliente_CentroResponsabilidad>();
        List<eCliente_Empresas> ListEmpresasCliente = new List<eCliente_Empresas>();
        public string cod_cliente = "", cod_empresa = "00010", cod_prospecto = "", cod_proyecto = "", cod_proyecto_titulo = "", dsc_proyecto_titulo = "", cod_separacion = "", cod_etapas_multiple = "", cod_documento_cliente = "";
        public string ActualizarListado = "NO", dsc_distrito = "", dsc_provincia = "";
        string varPathOrigen = "";
        string varNombreArchivo = "";
        public bool copropietario = false;
        private static string ClientId = "";
        private static string TenantId = "";
        private static string Instance = "https://login.microsoftonline.com/";
        public static IPublicClientApplication _clientApp;
        public int validate = 0;
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }

        public string GrupoSeleccionado = "";
        public string ItemSeleccionado = "";

        public frmMantCliente()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            unit = new UnitOfWork();
        }

        internal frmMantCliente(frmListadoClientes frm, frmSepararLote frmSeparacion, frmMantContratos frmContrato)
        {
            InitializeComponent();
            frmHandler = frm;
            frmHandlerSeparacion = frmSeparacion;
            frmHandlerContrato = frmContrato;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            unit = new UnitOfWork();
        }

        private void frmMantCliente_Load(object sender, EventArgs e)
        {
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl2.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl3.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl5.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            grcProximoevento.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            lblDocumentos.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            lblDirecciones.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();
            txtNroDocumento.Select();
            HabilitarBotones();
            //Size size  = new Size(Size.Height, 20);
            //grdbNatJuridi.Size = size;
            //size.Height = 20;
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) Ver(false, true, false, false);


            }

            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);
            eVentana oPerfilVisualizador = listPerfil.Find(x => x.cod_perfil == 3);
            if (oPerfilVisualizador != null)
            {
                if (MiAccion == Cliente.Editar)
                {
                    Ver(false, true, false, true);
                }
                if (MiAccion == Cliente.Vista)
                {
                    Ver(false, true, false, false);
                }
            }
            eVentana oPerfilRegistrador = listPerfil.Find(x => x.cod_perfil == 2);
            if (oPerfilRegistrador != null)
            {
                if (MiAccion == Cliente.Editar)
                {
                    Ver(true, false, true, true);
                }
                if (MiAccion == Cliente.Vista)
                {
                    Ver(true, false, true, false);
                }
            }
        }
        private void Inicializar()
        {
            if (frmHandlerSeparacion != null)
            {
                switch (MiAccionSeparacion)
                {
                    case Cliente.Nuevo:
                        CargarCombos();
                        Nuevo();
                        camposClientes(false);
                        btnBuscarProspecto.Enabled = false;
                        btnBuscarSeparacion.Enabled = false;
                        break;
                    case Cliente.Editar:
                        CargarCombos();
                        if (MiAccion == Cliente.Nuevo) { EditarSeparacion(); }

                        if (MiAccion == Cliente.Editar) { Editar(); }

                        if (MiAccion == Cliente.Vista)
                        {
                            Editar();
                            Ver(false, true, false, false);
                            camposClientes(true);
                        }
                        gvProyectosVinculadas.OptionsBehavior.Editable = true;
                        break;
                    case Cliente.Vista:
                        CargarCombos();
                        Editar();
                        Ver(false, true, false, false);
                        gvDocumentos.OptionsBehavior.Editable = true;
                        break;
                }
            }
            else
            {
                switch (MiAccion)
                {
                    case Cliente.Nuevo:
                        CargarCombos();
                        Nuevo();
                        camposClientes(false);
                        btnBuscarProspecto.Enabled = true;
                        btnBuscarSeparacion.Enabled = true;
                        break;
                    case Cliente.Editar:
                        CargarCombos();
                        Editar();
                        camposClientes(false);
                        gvProyectosVinculadas.OptionsBehavior.Editable = true;

                        break;
                    case Cliente.Vista:
                        CargarCombos();
                        Editar();
                        Ver(false, true, false, false);
                        gvDocumentos.OptionsBehavior.Editable = true;
                        break;
                }
            }


        }

        private void EditarSeparacion()
        {
            eLotes_Separaciones eLotSep = new eLotes_Separaciones();
            eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("2", cod_proyecto, cod_etapas_multiple, cod_separacion);

            //eCliente eCli = new eCliente();
            //eCli = unit.Clientes.ObtenerCliente<eCliente>(2, cod_cliente);
            cod_separacion = eLotSep.cod_separacion;
            chkActivoCliente.Checked = true;
            //txtCodCliente.Text = eCli.cod_cliente;
            grdbNatJuridi.SelectedIndex = 0;
            //chkFlgJuridica.Checked = eCli.flg_juridico == "SI" ? true : false;
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            txtNroDocumento.Text = eLotSep.dsc_documento;
            //if (eCli.fch_nacimiento.Year == 1) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = eCli.fch_nacimiento; }
            if (eLotSep.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = Convert.ToDateTime(eLotSep.fch_nacimiento); }
            txtApellPaterno.Text = eLotSep.dsc_apellido_paterno;
            txtApellMaterno.Text = eLotSep.dsc_apellido_materno;
            txtNombre.Text = eLotSep.dsc_nombre;
            //txtRazonSocial.Text = eCli.dsc_razon_social;
            //lkpTipoCliente.EditValue = eCli.cod_tipo_cliente;
            txtFono1.Text = eLotSep.dsc_telefono_1;
            //txtFono2.Text = eLotSep.dsc_telefono_2;
            //txtCodTarjeta.Text = eCli.cod_tarjeta_cliente;
            lkpEstadoCivil.EditValue = eLotSep.cod_estadocivil;
            //txtCodVendedor.Text = eCli.cod_vendedor;
            //txtVendedor.Text = eCli.dsc_vendedor;
            //lkpSexo.EditValue = eCli.cod_sexo;
            txtCorreoPersonal.Text = eLotSep.dsc_email;
            //txtCorreoTrabajo.Text = eCli.dsc_mail_trabajo;
            //txtCorreoFE.Text = eCli.dsc_mail_fe;
            //glkpTipoContacto.EditValue = eCli.cod_tipo_contacto;
            //glkpCalificacion.EditValue = eCli.cod_calificacion;
            //glkpCategoria.EditValue = eCli.cod_categoria;
            //chkCodigoManual.CheckState = eCli.flg_codigo_autogenerado == "SI" ? CheckState.Checked : CheckState.Unchecked;

            //txtCodigoERP.Text = eCli.cod_proveedor_ERP;
            //txtNombreComercial.Text = eCli.dsc_razon_comercial;
            //txtCodigoProspecto.Text = eCli.cod_prospecto
            if (eLotSep.cod_empresa != "" || eLotSep.cod_empresa != null) { cod_empresa = eLotSep.cod_empresa; }

            //cod_empresa = eLotSep.cod_empresa;
            cod_proyecto = eLotSep.cod_proyecto;
            //glkpTipoDocumentoConyuge.EditValue = eCli.cod_tipo_documento_conyuge;
            //txtNroDocumentoConyuge.Text = eCli.dsc_documento_conyuge;
            //txtApellPaternoConyuge.Text = eCli.dsc_apellido_paterno_conyuge;
            //txtApellMaternoConyuge.Text = eCli.dsc_apellido_materno_conyuge;
            //txtNombreConyuge.Text = eCli.dsc_nombre_conyuge;
            //if (eCli.fch_nacimiento_conyuge.ToString().Contains("1/01/0001")) { dtFecNacimientoConyuge.EditValue = null; } else { dtFecNacimientoConyuge.EditValue = Convert.ToDateTime(eCli.fch_nacimiento_conyuge); }

            grdbNatJuridi_SelectedIndexChanged(grdbNatJuridi, new EventArgs());

            //chkFlgJuridica_CheckStateChanged(chkFlgJurdica, new EventArgs());
            //ObtenerListadoDirecciones();
            //ObtenerListadoClientesContactos();
            //ObtenerListadoCentroResponsabilidad();
            //CargarListadoProyecto("TODOS");
            //obtenerListadoTipoLoteXEtapa();
            //ObtenerDatos_ObservacionesCliente();
            //ObtenerListadoEmpresasCliente();
            //btnNuevo.Enabled = true;
            xtraTabControl1.Enabled = false;


        }

        private void Nuevo()
        {
            grdbNatJuridi_SelectedIndexChanged(grdbNatJuridi, new EventArgs());
            //chkFlgJuridica_CheckStateChanged(chkFlgJuridica, new EventArgs());
            xtraTabControl1.Enabled = false;
        }

        private void Editar()
        {
            eCli = unit.Clientes.ObtenerCliente<eCliente>(2, cod_cliente);

            txtCodCliente.Text = eCli.cod_cliente;
            grdbNatJuridi.SelectedIndex = eCli.flg_juridico == "SI" ? 1 : 0;
            //chkFlgJuridica.Checked = eCli.flg_juridico == "SI" ? true : false;
            txtUsuarioRegistro.Text = eCli.dsc_usuario_registro;
            if (eCli.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = eCli.fch_registro; }
            txtUsuarioCambio.Text = eCli.dsc_usuario_cambio;
            if (eCli.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = eCli.fch_cambio; }
            glkpTipoDocumento.EditValue = eCli.cod_tipo_documento;
            txtNroDocumento.Text = eCli.dsc_documento;
            //if (eCli.fch_nacimiento.Year == 1) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = eCli.fch_nacimiento; }
            if (eCli.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = Convert.ToDateTime(eCli.fch_nacimiento); }

            txtApellPaterno.Text = eCli.dsc_apellido_paterno;
            txtApellMaterno.Text = eCli.dsc_apellido_materno;
            txtNombre.Text = eCli.dsc_nombre;
            txtRazonSocial.Text = eCli.dsc_razon_social;
            //lkpTipoCliente.EditValue = eCli.cod_tipo_cliente;
            txtFono1.Text = eCli.dsc_telefono_1;
            txtFono2.Text = eCli.dsc_telefono_2;
            //txtCodTarjeta.Text = eCli.cod_tarjeta_cliente;
            lkpEstadoCivil.EditValue = eCli.cod_estadocivil;
            //txtCodVendedor.Text = eCli.cod_vendedor;
            //txtVendedor.Text = eCli.dsc_vendedor;
            //lkpSexo.EditValue = eCli.cod_sexo;
            txtCorreoPersonal.Text = eCli.dsc_email;
            //txtCorreoTrabajo.Text = eCli.dsc_mail_trabajo;
            //txtCorreoFE.Text = eCli.dsc_mail_fe;
            glkpTipoContacto.EditValue = eCli.cod_tipo_contacto;
            //glkpCalificacion.EditValue = eCli.cod_calificacion;
            glkpCategoria.EditValue = eCli.cod_categoria;
            chkCodigoManual.CheckState = eCli.flg_codigo_autogenerado == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkBienesSepCliente.CheckState = eCli.flg_bienes_separados == "SI" ? CheckState.Checked : CheckState.Unchecked;

            //lkpAsesor.EditValue = eCli.cod_asesor;
            //txtCodigoERP.Text = eCli.cod_proveedor_ERP;
            txtNombreComercial.Text = eCli.dsc_razon_comercial;
            txtCodigoProspecto.Text = eCli.cod_prospecto;
            if (eCli.cod_empresa != "" || eCli.cod_empresa != null) { cod_empresa = eCli.cod_empresa; }

            //cod_empresa = eCli.cod_empresa;
            cod_proyecto = eCli.cod_proyecto;
            glkpTipoDocumentoConyuge.EditValue = eCli.cod_tipo_documento_conyuge;
            txtNroDocumentoConyuge.Text = eCli.dsc_documento_conyuge;
            txtApellPaternoConyuge.Text = eCli.dsc_apellido_paterno_conyuge;
            txtApellMaternoConyuge.Text = eCli.dsc_apellido_materno_conyuge;
            txtNombreConyuge.Text = eCli.dsc_nombre_conyuge;
            txtOcupacion.Text = eCli.dsc_profesion;
            if (eCli.fch_nacimiento_conyuge.ToString().Contains("1/01/0001")) { dtFecNacimientoConyuge.EditValue = null; } else { dtFecNacimientoConyuge.EditValue = Convert.ToDateTime(eCli.fch_nacimiento_conyuge); }
            txtDireccionConyuge.Text = eCli.dsc_direccion_conyuge;
            mmDireccionConyuge.Text = eCli.dsc_direccion_conyuge;
            lkpEstadoCivilConyuge.EditValue = eCli.cod_estadocivil_conyuge;
            txtOcupacionConyuge.Text = eCli.dsc_profesion_conyuge;
            txtCorreoConyuge.Text = eCli.dsc_email_conyuge;
            txtFono1Conyugue.Text = eCli.dsc_telefono_1_conyuge;
            grdbNatJuridi_SelectedIndexChanged(grdbNatJuridi, new EventArgs());

            //chkFlgJuridica_CheckStateChanged(chkFlgJurdica, new EventArgs());
            ObtenerListadoDirecciones();
            ObtenerListadoClientesContactos();
            ObtenerListadoCentroResponsabilidad();
            CargarListadoProyecto("TODOS");
            obtenerListadoTipoLoteXEtapa();
            CargarComboEnGrid();
            ObtenerDatos_ObservacionesCliente();
            ObtenerDatos_ContactosCliente();
            //ObtenerListadoEmpresasCliente();
            btnNuevo.Enabled = true;
            xtraTabControl1.Enabled = true;

            //List<eCliente_Empresas> listEmpresasUsuario = unit.Proveedores.ListarEmpresasProveedor<eCliente_Empresas>(11, "", Program.Sesion.Usuario.cod_usuario);
            //List<eCliente_Empresas> listEmpresas = unit.Clientes.ListarEmpresasCliente<eCliente_Empresas>(15, cod_cliente);
            //eCliente_Empresas objEmp = new eCliente_Empresas();
            //int validar = 0;
            //foreach (eCliente_Empresas obj in listEmpresasUsuario)
            //{
            //    objEmp = listEmpresas.Find(x => x.cod_empresa == obj.cod_empresa);
            //    validar = validar > 0 ? validar : objEmp != null ? 1 : 0;
            //}
            //if (validar == 0) BloqueoControles(false, true, false);
            //if (validar == 1) BloqueoControles(true, false, true);
            CargarListadoDocumentosClientes("3");
            //obtenerListadoTipoDocumentoXCliente();

        }

        public string validarContactoExistente(eCliente.eCliente_Contactos obj)
        {
            try
            {

                mySeparacionValidar = unit.Proyectos.ListarvalidarSeparacion<eLotes_Separaciones>(3, cod_cliente, obj.num_linea_contacto);
                if (mySeparacionValidar.Count() > 0)
                {
                    return "Error al eliminar el contacto \"" + obj.dsc_nombre_contacto + "\". \nSe encuentra registrado en el lote " + "\"" + mySeparacionValidar[0].dsc_lote + "\".";
                }
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private void Ver(Boolean ReadOnlyBotones, Boolean ReadOnlyCampos, Boolean ReadOnlyGrilla, Boolean ReadOnlyFlecha)
        {
            btnNuevo.Enabled = ReadOnlyBotones;
            btnGuardar.Enabled = ReadOnlyBotones;
            btnConsultarSunat.Enabled = ReadOnlyBotones;
            btnConsultarDNIreniec.Enabled = ReadOnlyBotones;
            btnBuscarProspecto.Enabled = ReadOnlyBotones;
            btnBuscarSeparacion.Enabled = ReadOnlyBotones;
            txtCodCliente.ReadOnly = ReadOnlyCampos;
            picBuscarCliente.Enabled = ReadOnlyBotones;
            picAnteriorCliente.Enabled = ReadOnlyFlecha;
            picSiguienteCliente.Enabled = ReadOnlyFlecha;
            chkActivoCliente.Enabled = ReadOnlyBotones;
            grdbNatJuridi.Enabled = ReadOnlyBotones;
            chkCodigoManual.Enabled = ReadOnlyBotones;
            glkpTipoDocumento.ReadOnly = ReadOnlyCampos;
            txtNroDocumento.ReadOnly = ReadOnlyCampos;
            txtCodigoProspecto.ReadOnly = ReadOnlyCampos;
            //lkpAsesor.ReadOnly = ReadOnlyCampos;
            txtApellPaterno.ReadOnly = ReadOnlyCampos;
            txtApellMaterno.ReadOnly = ReadOnlyCampos;
            txtNombre.ReadOnly = ReadOnlyCampos;
            dtFecNacimiento.ReadOnly = ReadOnlyCampos;
            txtRazonSocial.ReadOnly = ReadOnlyCampos;
            txtNombreComercial.ReadOnly = ReadOnlyCampos;
            txtOcupacion.ReadOnly = ReadOnlyCampos;
            lkpEstadoCivil.ReadOnly = ReadOnlyCampos;
            glkpCategoria.ReadOnly = ReadOnlyCampos;
            txtCorreoPersonal.ReadOnly = ReadOnlyCampos;
            //txtCorreoTrabajo.ReadOnly = ReadOnlyCampos;
            glkpTipoContacto.ReadOnly = ReadOnlyCampos;
            //glkpCalificacion.ReadOnly = ReadOnlyCampos;
            txtFono1.ReadOnly = ReadOnlyCampos;
            txtFono2.ReadOnly = ReadOnlyCampos;
            glkpTipoDocumentoConyuge.ReadOnly = ReadOnlyCampos;
            txtNroDocumentoConyuge.ReadOnly = ReadOnlyCampos;
            lkpEstadoCivilConyuge.ReadOnly = ReadOnlyCampos;
            txtOcupacionConyuge.ReadOnly = ReadOnlyCampos;
            txtApellPaternoConyuge.ReadOnly = ReadOnlyCampos;
            txtApellMaternoConyuge.ReadOnly = ReadOnlyCampos;
            txtNombreConyuge.ReadOnly = ReadOnlyCampos;
            dtFecNacimientoConyuge.ReadOnly = ReadOnlyCampos;
            //txtDireccionConyuge.ReadOnly = ReadOnlyCampos;
            txtCorreoConyuge.ReadOnly = ReadOnlyCampos;
            txtOcupacionConyuge.ReadOnly = ReadOnlyCampos;
            //dtFecNacimientoConyuge.ReadOnly = ReadOnlyCampos;
            txtFono1Conyugue.ReadOnly = ReadOnlyCampos;
            gvListaDirecciones.OptionsBehavior.Editable = ReadOnlyGrilla;
            txtCodDireccion.ReadOnly = ReadOnlyCampos;
            glkpTipoDireccion.ReadOnly = ReadOnlyCampos;
            txtNombreDireccion.ReadOnly = ReadOnlyCampos;
            mmDireccion.ReadOnly = ReadOnlyCampos;
            txtReferecia.ReadOnly = ReadOnlyCampos;
            lkpPais.ReadOnly = ReadOnlyCampos;
            lkpDepartamento.ReadOnly = ReadOnlyCampos;
            lkpProvincia.ReadOnly = ReadOnlyCampos;
            glkpDistrito.ReadOnly = ReadOnlyCampos;
            txtFono1Direccion.ReadOnly = ReadOnlyCampos;
            txtFono2Direccion.ReadOnly = ReadOnlyCampos;
            gvDocumentos.OptionsBehavior.Editable = ReadOnlyGrilla;
            gvProyectosVinculadas.OptionsBehavior.Editable = ReadOnlyGrilla;
            btnNuevoDireccion.Enabled = ReadOnlyBotones;
            btnGuardarDireccion.Enabled = ReadOnlyBotones;
            btnEliminarDireccion.Enabled = ReadOnlyBotones;
            gvEventos.OptionsBehavior.Editable = ReadOnlyGrilla;
            depeFecha.ReadOnly = ReadOnlyCampos;
            tipeFecha.ReadOnly = ReadOnlyCampos;
            pepellamada.ReadOnly = ReadOnlyCampos;
            pepewtsp.ReadOnly = ReadOnlyCampos;
            pepecorreo.ReadOnly = ReadOnlyCampos;
            pepecita.ReadOnly = ReadOnlyCampos;
            pepevisita.ReadOnly = ReadOnlyCampos;
            pepevideollamada.ReadOnly = ReadOnlyCampos;
            glkpeEjecutivoCita.ReadOnly = ReadOnlyCampos;
            mepeObs.ReadOnly = ReadOnlyCampos;
            gvObservacionesCliente.OptionsBehavior.Editable = ReadOnlyGrilla;
            txtUsuarioRegistro.ReadOnly = ReadOnlyCampos;
            txtUsuarioCambio.ReadOnly = ReadOnlyCampos;
            dtFechaRegistro.ReadOnly = ReadOnlyCampos;
            dtFechaModificacion.ReadOnly = ReadOnlyCampos;
            chkBienesSepCliente.Enabled = ReadOnlyBotones;
        }
        private void camposClientes(Boolean camposEditar)
        {
            txtNroDocumento.ReadOnly = camposEditar;
            txtApellPaterno.ReadOnly = camposEditar;
            txtApellMaterno.ReadOnly = camposEditar;
            txtNombre.ReadOnly = camposEditar;
            glkpTipoDocumento.ReadOnly = camposEditar;
            btnConsultarDNIreniec.Enabled = !camposEditar;
        }

        public void AsignarCamposClientesProspecto(eProspectosXLote campos_prospecto)
        {
            
            glkpTipoDocumento.EditValue = !String.IsNullOrEmpty(campos_prospecto.cod_tipo_documento) ? campos_prospecto.cod_tipo_documento.ToString() : glkpTipoDocumento.EditValue;
            txtNroDocumento.Text = !String.IsNullOrEmpty(campos_prospecto.dsc_num_documento) ? campos_prospecto.dsc_num_documento.ToString() : txtNroDocumento.Text;
            txtCodigoProspecto.Text = !String.IsNullOrEmpty(campos_prospecto.cod_prospecto) ? campos_prospecto.cod_prospecto.ToString() : txtCodigoProspecto.Text;
            txtOcupacion.Text = !String.IsNullOrEmpty(campos_prospecto.dsc_profesion) ? campos_prospecto.dsc_profesion.ToString() : txtOcupacion.Text;
            txtApellPaterno.Text = !String.IsNullOrEmpty(campos_prospecto.dsc_apellido_paterno) ? campos_prospecto.dsc_apellido_paterno.ToString() : txtApellPaterno.Text;
            txtApellMaterno.Text = !String.IsNullOrEmpty(campos_prospecto.dsc_apellido_materno) ? campos_prospecto.dsc_apellido_materno.ToString() : txtApellMaterno.Text;
            txtNombre.Text = !String.IsNullOrEmpty(campos_prospecto.dsc_nombres) ? campos_prospecto.dsc_nombres.ToString() : txtNombre.Text;
            txtFono1.Text = !String.IsNullOrEmpty(campos_prospecto.dsc_telefono_movil) ? campos_prospecto.dsc_telefono_movil.ToString() : txtFono1.Text;
            txtCorreoPersonal.Text = !String.IsNullOrEmpty(campos_prospecto.dsc_email) ? campos_prospecto.dsc_email.ToString() : txtCorreoPersonal.Text;
            if (campos_prospecto.fch_fec_nac.Year == 1) { dtFecNacimiento.EditValue = DBNull.Value; } else { dtFecNacimiento.EditValue = campos_prospecto.fch_fec_nac; dtFecNacimiento.Refresh(); }
            lkpEstadoCivil.EditValue = !String.IsNullOrEmpty(campos_prospecto.cod_estado_civil) ? campos_prospecto.cod_estado_civil.ToString() : lkpEstadoCivil.EditValue;
            //dtFecNacimiento.EditValue = campos_prospecto.fch_fec_nac;
            transferenciaDatosDirec = campos_prospecto;
            //AsignarCamposDireccionClienteProspecto(campos_prospecto);
            //if (campos_prospecto.fch_fec_nac.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = Convert.ToDateTime(campos_prospecto.fch_fec_nac); }
        }


        public void AsignarCamposClientesProforma(eProformas campos_proforma)
        {
            lstProDetalle = unit.Proyectos.ObtenerListadoProformas<eProformas.eProformas_Detalle>(5, cod_proforma: campos_proforma.cod_proforma, cod_proyecto: campos_proforma.cod_proyecto);
            glkpTipoDocumento.EditValue = campos_proforma.cod_tipo_documento != null ? campos_proforma.cod_tipo_documento.ToString() : "";
            txtNroDocumento.Text = campos_proforma.dsc_documento != null ? campos_proforma.dsc_documento.ToString() : "";
            txtApellPaterno.Text = campos_proforma.dsc_apellido_paterno != null ? campos_proforma.dsc_apellido_paterno.ToString() : "";
            txtApellMaterno.Text = campos_proforma.dsc_apellido_materno != null ? campos_proforma.dsc_apellido_materno.ToString() : "";
            txtNombre.Text = campos_proforma.dsc_nombre != null ? campos_proforma.dsc_nombre.ToString() : "";
            txtFono1.Text = campos_proforma.dsc_telefono != null ? campos_proforma.dsc_telefono.ToString() : "";
            txtCorreoPersonal.Text = campos_proforma.dsc_email != null ? campos_proforma.dsc_email.ToString() : "";
            lkpEstadoCivil.EditValue = campos_proforma.cod_estado_civil != null ? campos_proforma.cod_estado_civil.ToString() : "";

        }

        public void AsignarCamposClientesSeparaciones()
        {
            eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("2", campos_separaciones.cod_proyecto, campos_separaciones.cod_etapa, campos_separaciones.cod_separacion);

            glkpTipoDocumento.EditValue = eLotSep.cod_tipo_documento != null ? eLotSep.cod_tipo_documento.ToString() : "";
            txtCodigoProspecto.Text = eLotSep.cod_prospecto != null ? eLotSep.cod_prospecto.ToString() : "";
            cod_separacion = eLotSep.cod_separacion;
            chkActivoCliente.Checked = true;
            grdbNatJuridi.SelectedIndex = 0;
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            txtNroDocumento.Text = eLotSep.dsc_documento;
            if (eLotSep.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = Convert.ToDateTime(eLotSep.fch_nacimiento); }
            txtApellPaterno.Text = eLotSep.dsc_apellido_paterno;
            txtApellMaterno.Text = eLotSep.dsc_apellido_materno;
            txtNombre.Text = eLotSep.dsc_nombre;
            txtFono1.Text = eLotSep.dsc_telefono_1;
            lkpEstadoCivil.EditValue = eLotSep.cod_estadocivil;
            txtCorreoPersonal.Text = eLotSep.dsc_email;
          
            if (eLotSep.cod_empresa != "" || eLotSep.cod_empresa != null) { cod_empresa = eLotSep.cod_empresa; }

            //cod_empresa = eLotSep.cod_empresa;
            cod_proyecto = eLotSep.cod_proyecto;
            grdbNatJuridi_SelectedIndexChanged(grdbNatJuridi, new EventArgs());
            xtraTabControl1.Enabled = false;








            //txtNroDocumento.Text = campos_separaciones.dsc_num_documento != null ? campos_prospecto.dsc_num_documento.ToString() : "";

            //txtApellPaterno.Text = campos_prospecto.dsc_apellido_paterno != null ? campos_prospecto.dsc_apellido_paterno.ToString() : "";
            //txtApellMaterno.Text = campos_prospecto.dsc_apellido_materno != null ? campos_prospecto.dsc_apellido_materno.ToString() : "";
            //txtNombre.Text = campos_prospecto.dsc_nombres != null ? campos_prospecto.dsc_nombres.ToString() : "";
            //txtFono1.Text = campos_prospecto.dsc_telefono_movil != null ? campos_prospecto.dsc_telefono_movil.ToString() : "";
            //txtCorreoPersonal.Text = campos_prospecto.dsc_email != null ? campos_prospecto.dsc_email.ToString() : "";
            //if (campos_prospecto.fch_fec_nac != null) { dtFecNacimiento.EditValue = Convert.ToDateTime(campos_prospecto.fch_fec_nac); }
            //lkpEstadoCivil.EditValue = campos_prospecto.cod_estado_civil != null ? campos_prospecto.cod_estado_civil.ToString() : "";


            //if (campos_prospecto.fch_fec_nac.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = Convert.ToDateTime(campos_prospecto.fch_fec_nac); }

        }

        private void obtenerListadoTipoLoteXEtapa()
        {
            bsListaProyectos.DataSource = null; bsListaProyectos.DataSource = ListProyecto;
            if (MiAccion != Cliente.Nuevo)
            {

                List<eCliente> lista = unit.Proyectos.ListarProyectoxCliente<eCliente>("1", cod_cliente);
                ListClienteProyecto = lista;



                foreach (eCliente obj in lista)
                {
                    eProyecto oProCLi = ListProyecto.Find(x => x.cod_proyecto == obj.cod_proyecto);
                    if (oProCLi != null)
                    {
                        oProCLi.Seleccionado = true; oProCLi.dsc_lotes_asig = obj.dsc_lotes_asig;


                    }
                }
            }
            gvProyectosVinculadas.RefreshData();
        }


        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevo.Enabled = Enabled;
            btnGuardar.Enabled = Enabled;
            btnConsultarSunat.Enabled = Enabled;
            //txtCodCliente.ReadOnly = ReadOnly;
            grdbNatJuridi.ReadOnly = ReadOnly;
            chkCodigoManual.ReadOnly = ReadOnly;
            chkActivoCliente.ReadOnly = ReadOnly;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtNroDocumento.ReadOnly = ReadOnly;
            dtFecNacimiento.ReadOnly = ReadOnly;
            //txtCodigoERP.ReadOnly = ReadOnly;
            txtApellPaterno.ReadOnly = ReadOnly;
            txtApellMaterno.ReadOnly = ReadOnly;
            txtNombre.ReadOnly = ReadOnly;
            txtRazonSocial.ReadOnly = ReadOnly;
            txtNombreComercial.ReadOnly = ReadOnly;
            //txtVendedor.ReadOnly = ReadOnly;
            //txtCodVendedor.ReadOnly = ReadOnly;
            //picBuscarVendedor.ReadOnly = ReadOnly;
            lkpEstadoCivil.ReadOnly = ReadOnly;
            //lkpSexo.ReadOnly = ReadOnly;
            txtCorreoPersonal.ReadOnly = ReadOnly;
            //txtCorreoTrabajo.ReadOnly = ReadOnly;
            //txtCorreoFE.ReadOnly = ReadOnly;
            txtFono1.ReadOnly = ReadOnly;
            txtFono2.ReadOnly = ReadOnly;
            glkpTipoContacto.ReadOnly = ReadOnly;
            //glkpCalificacion.ReadOnly = ReadOnly;
            glkpCategoria.ReadOnly = ReadOnly;
            //lkpTipoCliente.ReadOnly = ReadOnly;

            //Direccion Cliente
            btnNuevoDireccion.Enabled = Enabled;
            btnGuardarDireccion.Enabled = Enabled;
            btnEliminarDireccion.Enabled = Enabled;
            gvListaDirecciones.OptionsBehavior.Editable = Editable;
            txtCodClienteContacto.ReadOnly = ReadOnly;
            glkpTipoDireccion.ReadOnly = ReadOnly;
            mmDireccion.ReadOnly = ReadOnly;
            txtReferecia.ReadOnly = ReadOnly;
            lkpPais.ReadOnly = ReadOnly;
            lkpDepartamento.ReadOnly = ReadOnly;
            lkpProvincia.ReadOnly = ReadOnly;
            glkpDistrito.ReadOnly = ReadOnly;
            txtFono1Direccion.ReadOnly = ReadOnly;
            txtFono2.ReadOnly = ReadOnly;
            //chkFlgComprobante.ReadOnly = ReadOnly;
            //chkFlgCobranza.ReadOnly = ReadOnly;

            //Direccion Contacto
            btnNuevoDireccionContacto.Enabled = Enabled;
            btnGuardarDireccionContacto.Enabled = Enabled;
            btnEliminarDireccionContacto.Enabled = Enabled;
            gvListaDireccionContactos.OptionsBehavior.Editable = Editable;
            txtCodDireccionContacto.ReadOnly = ReadOnly;
            txtNombreDireccionContacto.ReadOnly = ReadOnly;
            txtApellidoDireccionContacto.ReadOnly = ReadOnly;
            dtFecNacDireccionContacto.ReadOnly = ReadOnly;
            txtCorreoDireccionContacto.ReadOnly = ReadOnly;
            txtFono1DireccionContacto.ReadOnly = ReadOnly;
            txtFono2DireccionContacto.ReadOnly = ReadOnly;
            txtCargoDireccionContacto.ReadOnly = ReadOnly;
            mmObservacionDireccionContacto.ReadOnly = ReadOnly;

            //Direccion Ubicaciones
            btnExportarUbicaciones.Enabled = Enabled;
            btnUbicacionMasiva.Enabled = Enabled;
            btnNuevoUbicacion.Enabled = Enabled;
            btnGuardarUbicacion.Enabled = Enabled;
            btnInactivarUbicacion.Enabled = Enabled;
            txtCodUbicacion.ReadOnly = ReadOnly;
            chkActivoUbicacion.ReadOnly = ReadOnly;
            txtCodPerUbicacion.ReadOnly = ReadOnly;
            mmDescripcionUbicacion.ReadOnly = ReadOnly;
            lkpNivelUbicacion.ReadOnly = ReadOnly;
            lkpNivelSuperiorUbicacion.ReadOnly = ReadOnly;
            lkpResponsableUbicacion.ReadOnly = ReadOnly;
            mmObservacionUbicacion.ReadOnly = ReadOnly;

            //Contactos Cliente
            btnNuevoClienteContacto.Enabled = Enabled;
            btnGuardarClienteContacto.Enabled = Enabled;
            btnEliminarClienteContacto.Enabled = Enabled;
            txtCodClienteContacto.ReadOnly = ReadOnly;
            txtNombreClienteContacto.ReadOnly = ReadOnly;
            txtApellidoClienteContacto.ReadOnly = ReadOnly;
            dtFecNacClienteContacto.ReadOnly = ReadOnly;
            txtCorreoClienteContacto.ReadOnly = ReadOnly;
            txtFono1ClienteContacto.ReadOnly = ReadOnly;
            txtFono2ClienteContacto.ReadOnly = ReadOnly;
            txtCargoClienteContacto.ReadOnly = ReadOnly;
            mmObservacionClienteContacto.ReadOnly = ReadOnly;

            //Empresas Vinculadas Cliente
            gvProyectosVinculadas.OptionsBehavior.Editable = Editable;
        }

        private void ObtenerDatos_ObservacionesCliente()
        {
            try
            {
                mylistObservaciones = unit.Clientes.Obtener_LineasDetalleCliente<eCliente.eCliente_Observaciones>(1, cod_cliente);
                bsClienteObservaciones.DataSource = mylistObservaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_ContactosCliente()
        {
            try
            {
                mylistContactos = unit.Clientes.Obtener_LineasDetalleClienteContactos<eCliente.eCliente_Contactos>(1, cod_cliente);
                bsClienteContactosAdicionales.DataSource = mylistContactos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarCombos()
        {
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true); glkpTipoDocumento.EditValue = "DI001";
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumentoConyuge, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true); glkpTipoDocumentoConyuge.EditValue = "DI001";
            CargarCombosGridLookup("TipoContacto", glkpTipoContacto, "cod_tipo_contacto", "dsc_tipo_contacto", "", valorDefecto: true);
            //CargarCombosGridLookup("TipoCalificacion", glkpCalificacion, "cod_calificacion", "dsc_calificacion", "", valorDefecto: true);
            CargarCombosGridLookup("TipoCategoria", glkpCategoria, "cod_categoria", "dsc_categoria", "", valorDefecto: true);
            CargarCombosGridLookup("TipoDireccion", glkpTipoDireccion, "cod_tipo_direccion", "dsc_tipo_direccion", "");
            CargarCombosGridLookup("TipoDirecConYuge", glkpTipoDireccionConyuge, "cod_tipo_direccion", "dsc_tipo_direccion", "");
            //CargarCombosGridLookup("TipoCalle", glkpTipoCalle, "cod_tipo_via", "dsc_tipo_via", "");
            //CargarCombosGridLookup("TipoAvenida", glkpTipoAvenida, "cod_calle", "dsc_calle", "");
            //CargarCombosGridLookup("TipoUrbanizacion", glkpTipoUrbanizacion, "cod_tipo_zona", "dsc_tipo_zona", "");
            //CargarCombosGridLookup("TipoEtapa", glkpTipoEtapa, "cod_urbanizacion", "dsc_urbanizacion", "");
            //CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "");

            //unit.Clientes.CargaCombosLookUp("TipoCliente", lkpTipoCliente, "cod_tipo_cliente", "dsc_tipo_cliente", "", valorDefecto: true);
            unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", lkpEstadoCivil, "cod_estado_civil", "dsc_estado_civil", "", valorDefecto: true);
            unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", lkpEstadoCivilConyuge, "cod_estado_civil", "dsc_estado_civil", "", valorDefecto: true);



            //unit.Clientes.CargaCombosLookUp("TipoSexo", lkpSexo, "cod_sexo", "dsc_sexo", "");
            unit.Clientes.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "00001"); //lkpPais.EditValue = "00001";
            unit.Clientes.CargaCombosLookUp("TipoPais", lkpPaisConyuge, "cod_pais", "dsc_pais", "00001"); //lkpPais.EditValue = "00001";

            unit.Clientes.CargaCombosLookUp("NivelUbicacion", lkpNivelUbicacion, "cod_nivel", "dsc_nivel", "");
            unit.Clientes.CargaCombosLookUp("NivelUbicacion", lkpNivelCentroResponsabilidad, "nro_nivel", "dsc_nivel", "");
            unit.Clientes.CargaCombosLookUp("ResponsableCentroResponsabilidad", lkpResponsableCentroResponsabilidad, "cod_contacto", "dsc_nombre_completo", "", cod_cliente);
            unit.Clientes.CargaCombosLookUp("Vendedor", lkpAsesor, "cod_asesor", "dsc_asesor", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario); //lkpAsesor.EditValue

            if (MiAccion == Cliente.Nuevo)
            {
                picAnteriorCliente.Enabled = false; picSiguienteCliente.Enabled = false; btnNuevo.Enabled = false;
            }
            if (frmHandlerSeparacion != null)
            {
                picAnteriorCliente.Enabled = false; picSiguienteCliente.Enabled = false; btnNuevo.Enabled = false;
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = unit.Clientes.ObtenerListadoGridLookup(nCombo);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }
        private void ObtenerListadoClientesContactos()
        {
            ListClienteContacto = unit.Clientes.ListarContactos<eCliente_Contactos>(7, cod_cliente, 0);
            bsClienteContactos.DataSource = null; bsClienteContactos.DataSource = ListClienteContacto;
            gvListaClienteContactos.RefreshData();
        }

        private void ObtenerListadoDirecciones()
        {
            ListDirecc = unit.Clientes.ListarDirecciones<eCliente_Direccion>(3, cod_cliente);

            bsListaDirecciones.DataSource = null; bsListaDirecciones.DataSource = ListDirecc;
            if (ListDirecc == null || ListDirecc.Count() < 1) { LimpiarCamposDireccion(); }
            gvListaDirecciones.RefreshData();
        }


        private void ObtenerListadoDireccionesContactos()
        {
            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;

            ListDireccionContacto = unit.Clientes.ListarContactos<eCliente_Contactos>(9, cod_cliente, eDirec.num_linea);
            bsDireccionContactos.DataSource = null; bsDireccionContactos.DataSource = ListDireccionContacto;
            gvListaDireccionContactos.RefreshData();

        }

        private void ObtenerListadoUbicaciones()
        {
            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            ListUbic = unit.Clientes.ListarUbicaciones<eCliente_Ubicacion>(5, cod_cliente, eDirec.num_linea);
            bsDireccionUbicaciones.DataSource = null; bsDireccionUbicaciones.DataSource = ListUbic;

            //tlUbicacionesDireccion.RefreshData();
        }

        private void ObtenerListadoCentroResponsabilidad()
        {
            ListCentroResp = unit.Clientes.ListarCentroResponsabilidad<eCliente_CentroResponsabilidad>(11, cod_cliente);
            bsClienteCentroResponsabilidad.DataSource = null; bsClienteCentroResponsabilidad.DataSource = ListCentroResp;
        }

        public void CargarListadoProyecto(string NombreGrupo, string Codigo = "")
        {
            try
            {
                string cod_proyecto = "", accion = "", cod_empresa = "";

                switch (NombreGrupo)
                {
                    case "TODOS": accion = "1"; break;
                    case "Por Cliente": cod_empresa = Codigo; accion = "4"; break;
                }

                ListProyecto = unit.Proyectos.ListarProyectos<eProyecto>(accion, cod_proyecto, cod_empresa);

                bsListaProyectos.DataSource = ListProyecto;
                gvProyectosVinculadas.RefreshData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerListadoEmpresasCliente()
        {
            ListEmpresasCliente = unit.Clientes.ListarEmpresasCliente<eCliente_Empresas>(16, cod_cliente, Program.Sesion.Usuario.cod_usuario);
            bsEmpresasCliente.DataSource = null; bsEmpresasCliente.DataSource = ListEmpresasCliente;

            if (MiAccion == Cliente.Editar)
            {
                List<eCliente_Empresas> lista = unit.Clientes.ListarEmpresasCliente<eCliente_Empresas>(15, cod_cliente);
                foreach (eCliente_Empresas obj in lista)
                {
                    eCliente_Empresas oCliEmp = ListEmpresasCliente.Find(x => x.cod_empresa == obj.cod_empresa);
                    if (oCliEmp != null) { oCliEmp.Seleccionado = true; oCliEmp.valorRating = obj.valorRating; oCliEmp.dsc_pref_ceco = obj.dsc_pref_ceco; }
                }
            }

            gvProyectosVinculadas.RefreshData();
        }
        private void chkFlgJuridica_CheckStateChanged(object sender, EventArgs e)
        {

            txtRazonSocial.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true; //chkFlgJuridica.CheckState == CheckState.Checked ? true : false;

            txtNombreComercial.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true;  //chkFlgJuridica.CheckState == CheckState.Checked ? true : false;
            txtApellPaterno.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtApellMaterno.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtNombre.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            dtFecNacimiento.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true; //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            //dtFecNacimiento.EditValue = grdbNatJuridi.SelectedIndex == 1 ? true : false;  //chkFlgJuridica.CheckState == CheckState.Checked ? null : dtFecNacimiento.EditValue;
            //txtEdad.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            //lkpSexo.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true; 
            lkpEstadoCivil.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true; //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            glkpTipoDocumento.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            if (grdbNatJuridi.SelectedIndex == 1) glkpTipoDocumento.EditValue = "DI004";
        }

        private void frmMantCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void gvListaDirecciones_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    //int validar = 0;

                    eCliente_Direccion obj = gvListaDirecciones.GetRow(e.FocusedRowHandle) as eCliente_Direccion;

                    if (obj.num_linea == 99)
                    {
                        /*************************************************DIRECCIÓN CONYUGE****************************************************/
                        xtraTabControl2.SelectedTabPageIndex = 3;
                        eCliente_Direccion eDirecConyuge = new eCliente_Direccion();
                        eDirecConyuge = unit.Clientes.ObtenerDireccion<eCliente_Direccion>(4, cod_cliente, 99);
                        if (eDirecConyuge == null) { return; }
                        txtCodDireccionConyuge.Text = eDirecConyuge.num_linea.ToString();
                        glkpTipoDireccionConyuge.EditValue = eDirecConyuge.cod_tipo_direccion;
                        txtNombreDireccionConyuge.Text = eDirecConyuge.dsc_nombre_direccion;
                        mmDireccionConyuge.Text = eDirecConyuge.dsc_cadena_direccion;
                        lkpPaisConyuge.EditValue = eDirecConyuge.cod_pais;
                        lkpDepartamentoConyuge.EditValue = eDirecConyuge.cod_departamento;
                        lkpProvinciaConyuge.EditValue = eDirecConyuge.cod_provincia;
                        glkpDistritoConyuge.EditValue = eDirecConyuge.cod_distrito;
                        txtFono1DireccionConyuge.Text = eDirecConyuge.dsc_telefono_1;
                        txtFono2DireccionConyuge.Text = eDirecConyuge.dsc_telefono_2;
                        txtRefereciaConyuge.Text = eDirecConyuge.dsc_referencia;
                    }
                    else
                    {
                        xtraTabControl2.SelectedTabPageIndex = 0;
                        LimpiarCamposDireccion();
                        LimpiarCamposDireccionContacto();
                        LimpiarCamposUbicacion();
                        eCliente_Direccion eDirec = new eCliente_Direccion();
                        eDirec = unit.Clientes.ObtenerDireccion<eCliente_Direccion>(4, cod_cliente, obj.num_linea);
                        if (eDirec == null) { return; }
                        txtCodDireccion.Text = eDirec.num_linea.ToString();
                        glkpTipoDireccion.EditValue = eDirec.cod_tipo_direccion;
                        txtNombreDireccion.Text = eDirec.dsc_nombre_direccion;
                        //glkpTipoCalle.EditValue = eDirec.cod_tipo_via;
                        //glkpTipoAvenida.Text = eDirec.cod_calle_direccion;
                        //glkpTipoUrbanizacion.EditValue = eDirec.cod_tipo_zona;
                        //glkpTipoEtapa.Text = eDirec.cod_urbanizacion;
                        //txtNumero.Text = eDirec.cod_numero;
                        //txtInterior.Text = eDirec.cod_interior;
                        //txtManzana.Text = eDirec.cod_manzana;
                        //txtLote.Text = eDirec.cod_lote;
                        //txtSubLote.Text = eDirec.cod_sublote;
                        mmDireccion.Text = eDirec.dsc_cadena_direccion;
                        lkpPais.EditValue = eDirec.cod_pais;
                        lkpDepartamento.EditValue = eDirec.cod_departamento;
                        lkpProvincia.EditValue = eDirec.cod_provincia;
                        glkpDistrito.EditValue = eDirec.cod_distrito;
                        txtFono1Direccion.Text = eDirec.dsc_telefono_1;
                        txtFono2Direccion.Text = eDirec.dsc_telefono_2;
                        txtReferecia.Text = eDirec.dsc_referencia;
                        //chkFlgComprobante.Checked = eDirec.flg_comprobante == "SI" ? true : false;
                        //chkFlgCobranza.Checked = eDirec.flg_direccion_cobranza == "SI" ? true : false;
                    }

                    unit.Clientes.CargaCombosLookUp("ResponsableUbicacion", lkpResponsableUbicacion, "cod_contacto", "dsc_nombre_completo", "", cod_cliente, num_linea: obj.num_linea);
                    ObtenerListadoDireccionesContactos();
                    ObtenerListadoUbicaciones();

                    //if (validar == 0)
                    //{
                    //    validar = 1;
                    //    gvListaDirecciones.FocusedRowHandle = gvListaDirecciones.RowCount - 1;
                    //}


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaDirecciones_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListaDirecciones_FocusedRowChanged(gvListaDirecciones, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void gvlkpTipoDocumento_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoDocumento_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoContacto_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoContacto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpCalificacion_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpCalificacion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpCategoria_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpCategoria_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoDireccion_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoDireccion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoCalle_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoCalle_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoAvenida_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoAvenida_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoUrbanizacion_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoUrbanizacion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpTipoEtapa_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpTipoEtapa_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvlkpDistrito_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView vw = sender as GridView;
                if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gvlkpDistrito_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView vw = sender as GridView;
                    if (e.RowHandle >= 0) if (vw.GetRowCellDisplayText(e.RowHandle, vw.Columns["flg_activo"]).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ValidarLongitudDocumento()
        {
            string result = "";
            int ctd = Convert.ToInt32(((System.Data.DataRowView)(glkpTipoDocumento.Properties.GetRowByKeyValue(glkpTipoDocumento.EditValue))).Row.ItemArray[4]);
            result = ctd == txtNroDocumento.Text.Trim().Length ? "OK" : ctd.ToString();

            return result;
        }

        public string validarCamposVaciosCliente()
        {
            if (glkpTipoDocumento.EditValue == null)
            {
                //glkpTipoDocumento.ShowPopup();
                return "Debe seleccionar un tipo de documento";
            }
            if (txtNroDocumento.Text.Trim() == "")
            {
                txtNroDocumento.Focus();
                return "Debe ingresar un número de documento";
            }
            //if (lkpAsesor.EditValue == null)
            //{
            //    lkpAsesor.ShowPopup();
            //    return "Debe seleccionar el asesor";
            //}
            //if (txtApellMaterno.Text.Trim() == "" && grdbNatJuridi.EditValue.ToString() == "N")
            //{
            //    txtApellMaterno.Focus();
            //    return "Debe ingresar un apellido materno";
            //}
            if (txtApellPaterno.Text.Trim() == "" && grdbNatJuridi.EditValue.ToString() == "N")
            {
                txtApellPaterno.Focus();
                return "Debe ingresar un apellido paterno";
            }
            if (txtNombre.Text.Trim() == "" && grdbNatJuridi.EditValue.ToString() == "N")
            {
                txtNombre.Focus();
                return "Debe ingresar un nombre";
            }


            if (txtCorreoPersonal.Text.Trim().Length > 0)
            {
                if (!new EmailAddressAttribute().IsValid(txtCorreoPersonal.Text.Trim()))
                {
                    txtCorreoPersonal.Focus();
                    return "Debe seleccionar un correo válido";
                }
            }

            //if (txtCorreoTrabajo.Text.Trim().Length > 0)
            //{
            //    if (!new EmailAddressAttribute().IsValid(txtCorreoTrabajo.Text.Trim()))
            //    {
            //        txtCorreoTrabajo.Focus();
            //        return "Debe seleccionar un correo válido";
            //    }
            //}

            if (dtFecNacimiento.EditValue != null)
            {
                DateTime date = DateTime.Today;
                DateTime fechaSeleccionada = (DateTime)dtFecNacimiento.EditValue;
                if (fechaSeleccionada.AddYears(18) > date)
                {
                    dtFecNacimiento.ShowPopup();
                    return "Error, tiene que ser persona mayor a 18 años";
                    DateTime oInicioFechaNac = date.AddYears(-18).AddDays(-1); //new DateTime(date.Year, date.Month, 1);
                    dtFecNacimiento.EditValue = oInicioFechaNac;
                    dtFecNacimiento.ShowPopup();
                }
            }
            else
            {
                dtFecNacimiento.ShowPopup();
                return "Debe seleccionar la fecha de nacimiento";
            }

            if (lkpEstadoCivil.EditValue == null && grdbNatJuridi.EditValue.ToString() == "N")
            {
                lkpEstadoCivil.ShowPopup();
                return "Debe seleccionar el estado civil";
            }
            if (txtRazonSocial.Text.Trim() == "" && grdbNatJuridi.EditValue.ToString() == "J")
            {
                txtRazonSocial.Focus();
                return "Debe ingresar la razón social";
            }
            //if (glkpTipoContacto.EditValue == null)
            //{
            //    glkpTipoContacto.ShowPopup();
            //    return "Debe seleccionar el tipo de contacto";
            //}
            //if (glkpCalificacion.EditValue == null)
            //{
            //    glkpCalificacion.ShowPopup();
            //    return "Debe seleccionar la calificación";
            //}
            //if (glkpCategoria.EditValue == null)
            //{
            //    glkpCategoria.ShowPopup();
            //    return "Debe seleccionar la categoría";

            //}
            if ((lkpEstadoCivil.EditValue.ToString() == "02" || lkpEstadoCivil.EditValue.ToString() == "04") && chkBienesSepCliente.CheckState == CheckState.Unchecked)
            {
                if (glkpTipoDocumentoConyuge.EditValue == null)
                {
                    glkpTipoDocumentoConyuge.ShowPopup();
                    return "Debe seleccionar un tipo de documento del cónyuge";
                }

            }
            if ((lkpEstadoCivil.EditValue.ToString() == "02" || lkpEstadoCivil.EditValue.ToString() == "04") && chkBienesSepCliente.CheckState == CheckState.Unchecked)
            {
                if (txtNroDocumentoConyuge.Text.Trim() == "")
                {
                    txtNroDocumentoConyuge.Focus();
                    return "Debe ingresar un número de documento del cónyuge";
                }

            }

            if ((lkpEstadoCivil.EditValue.ToString() == "02" || lkpEstadoCivil.EditValue.ToString() == "04") && chkBienesSepCliente.CheckState == CheckState.Unchecked)
            {
                if (lkpEstadoCivilConyuge.EditValue == null)
                {
                    lkpEstadoCivilConyuge.ShowPopup();
                    return "Debe seleccionar el estado civil del cónyuge";
                }

            }
            if ((lkpEstadoCivil.EditValue.ToString() == "02" || lkpEstadoCivil.EditValue.ToString() == "04") && chkBienesSepCliente.CheckState == CheckState.Unchecked)
            {
                if (txtApellPaternoConyuge.Text.Trim() == "")
                {
                    txtApellPaternoConyuge.Focus();
                    return "Debe ingresar el apellido paterno del cónyuge";
                }

            }
            if ((lkpEstadoCivil.EditValue.ToString() == "02" || lkpEstadoCivil.EditValue.ToString() == "04") && chkBienesSepCliente.CheckState == CheckState.Unchecked)
            {
                if (txtNombreConyuge.Text.Trim() == "")
                {
                    txtNombreConyuge.Focus();
                    return "Debe ingresar el nombre del cónyuge";
                }

            }

            return null;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtCodCliente.Focus();
                string mensaje = validarCamposVaciosCliente();
                if (mensaje == null)
                {

                    string NroDoc = ValidarLongitudDocumento();
                    int Anho = DateTime.Now.Year - Convert.ToDateTime(dtFecNacimiento.EditValue).Year;

                    string result = "";
                    switch (MiAccion)
                    {
                        case Cliente.Nuevo: txtNroDocumento_Leave(txtNroDocumento, EventArgs.Empty);  result = Guardar(); break;
                        case Cliente.Editar: result = Modificar(); break;
                    }

                    if (result == "OK")
                    {
                        
                        MessageBox.Show("Se guardó el cliente de manera satisfactoria", "Guardar cliente", MessageBoxButtons.OK);
                        btnBuscarProspecto.Enabled = false;
                        ActualizarListado = "SI";
                        if (frmHandler != null)
                        {
                            int nRow = frmHandler.gvListaClientes.FocusedRowHandle;
                            frmHandler.frmListadoClientes_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                            frmHandler.gvListaClientes.FocusedRowHandle = nRow;
                        }

                        if (MiAccion == Cliente.Nuevo)
                        {
                            MiAccion = Cliente.Editar;
                            xtraTabControl1.Enabled = true;

                        }
                        eCliente eCli = new eCliente();
                        eCli = unit.Clientes.ObtenerCliente<eCliente>(2, cod_cliente);
                        if (frmHandlerSeparacion != null)
                        {
                            eLotes_Separaciones eLotSep = new eLotes_Separaciones();

                            if (MiAccionSeparacion == Cliente.Nuevo)
                            {
                                if (copropietario)
                                {
                                    frmHandlerSeparacion.transferirDatosCopropietario(eCli);
                                }
                                else
                                {
                                    if (lstProDetalle != null && lstProDetalle.Count > 0) { frmHandlerSeparacion.lstProDetalle = lstProDetalle; }
                                    frmHandlerSeparacion.transferirDatos(eCli);
                                }
                                /*actualizarProyectoXClienteSeparacion(); obtenerListadoTipoLoteXEtapa();*/
                                if (transferenciaDatosDirec != null && !copropietario && !String.IsNullOrEmpty(transferenciaDatosDirec.cod_pais) && !String.IsNullOrEmpty(transferenciaDatosDirec.cod_departamento) && !String.IsNullOrEmpty(transferenciaDatosDirec.cod_provincia) && !String.IsNullOrEmpty(transferenciaDatosDirec.cod_distrito))
                                {
                                    glkpTipoDireccion.EditValue = glkpTipoDireccion.Properties.GetKeyValue(0);
                                    lkpPais.EditValue = transferenciaDatosDirec.cod_pais.ToString();
                                    lkpDepartamento.EditValue = transferenciaDatosDirec.cod_departamento.ToString();
                                    lkpProvincia.EditValue = transferenciaDatosDirec.cod_provincia.ToString();
                                    glkpDistrito.EditValue = transferenciaDatosDirec.cod_distrito.ToString();
                                    mmDireccion.Text = transferenciaDatosDirec.dsc_direccion;
                                }

                            }
                            if (MiAccionSeparacion == Cliente.Editar)
                            {
                                frmHandlerSeparacion.transferirDatos(eCli);
                                ModificarSeparacion();
                                AsignarProyectoPorClienteSeparaciones();
                                obtenerListadoTipoLoteXEtapa();
                                CargarComboEnGrid();
                                //actualizarProyectoXClienteSeparacion();
                            }
                        }

                        if (frmHandlerContrato != null)
                        {
                            if (copropietario)
                            {
                                frmHandlerContrato.transferirDatosCopropietario(eCli);
                            }
                            else
                            {
                                frmHandlerContrato.transferirDatos(eCli);
                            }
                        }


                        txtUsuarioRegistro.Text = eCli.dsc_usuario_registro;
                        if (eCli.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = eCli.fch_registro; }
                        txtUsuarioCambio.Text = eCli.dsc_usuario_cambio;
                        if (eCli.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = eCli.fch_cambio; }


                        if (eLotSep != null && !string.IsNullOrEmpty(eLotSep.cod_separacion))
                        {
                            ModificarSeparacion();
                            AsignarProyectoPorClienteSeparaciones();
                            obtenerListadoTipoLoteXEtapa();
                            CargarComboEnGrid();
                            if (eLotSep.flg_prospecto == "SI")
                            {
                                eLotes_Separaciones eLo = new eLotes_Separaciones();
                                eLo = unit.Clientes.Actualizar_Estado_Prospecto<eLotes_Separaciones>(eLotSep);
                                if (eLo != null)
                                {
                                    result = "OK";
                                }
                            }
                        }
                        AsignarProyectoPorClienteSeparaciones();
                        obtenerListadoTipoLoteXEtapa();
                        CargarComboEnGrid();
                        CargarListadoDocumentosClientes("3");
                        //obtenerListadoTipoDocumentoXCliente();

                        if (mmDireccion.Text.Trim() != "")
                        {
                            btnGuardarDireccion.PerformClick();
                        }

                    }
                    string mensaje2 = GuardarObservaciones();
                    if (mensaje2 != null)
                    {
                        ObtenerDatos_ObservacionesCliente();
                    }
                    string mensaje3 = GuardarContactos();
                    if (mensaje3 != null)
                    {
                        ObtenerDatos_ContactosCliente();
                    }

                    btnNuevo.Enabled = true;
                    btnConsultarSunat.Enabled = false;
                    btnConsultarDNIreniec.Enabled = false;
                }
                else
                {
                    XtraMessageBox.Show(mensaje, "Guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarProyectoXClienteSeparacion()
        {
            eCliente ePro = new eCliente();
            ePro.cod_cliente = cod_cliente;
            ePro.cod_empresa = cod_empresa;
            ePro.cod_proyecto = cod_proyecto;
            ePro.flg_activo = "SI";
            ePro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            ePro = unit.Proyectos.Guardar_Actualizar_ClienteProyecto<eCliente>(ePro);
        }

        private string GuardarObservaciones()
        {
            txtNroDocumento.Focus();
            txtNroDocumento.Select();
            gvObservacionesCliente.PostEditor();
            gvObservacionesCliente.RefreshData();
            eCliente.eCliente_Observaciones eObsFact = new eCliente.eCliente_Observaciones();

            for (int y = 0; y < gvObservacionesCliente.DataRowCount; y++)
            {
                eCliente.eCliente_Observaciones obj = gvObservacionesCliente.GetRow(y) as eCliente.eCliente_Observaciones;
                if (obj == null) continue;
                obj.cod_cliente = cod_cliente; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                eObsFact = unit.Clientes.InsertarObservacionesCliente<eCliente.eCliente_Observaciones>(obj);

            }

            if (eObsFact != null) { return "OK"; }

            return null;

        }

        private string GuardarContactos()
        {
            txtNroDocumento.Focus();
            txtNroDocumento.Select();
            gvContactosAdicionales.PostEditor(); gvContactosAdicionales.RefreshData();
            eCliente.eCliente_Contactos eConAdi = new eCliente.eCliente_Contactos();

            for (int y = 0; y < gvContactosAdicionales.DataRowCount; y++)
            {
                eCliente.eCliente_Contactos obj = gvContactosAdicionales.GetRow(y) as eCliente.eCliente_Contactos;
                if (obj == null) continue;
                obj.cod_cliente = cod_cliente; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                eConAdi = unit.Clientes.InsertarContactosCliente<eCliente.eCliente_Contactos>(obj);

            }
            if (eConAdi != null) { return "OK"; }
            return null;
        }

        private string Guardar()
        {
            string result = "";
            eCliente eCli = AsignarValoresCliente();
            eCli = unit.Clientes.Guardar_Actualizar_Cliente<eCliente>(eCli, "Nuevo");
            if (eCli != null)
            {
                cod_cliente = eCli.cod_cliente;
                txtCodCliente.Text = cod_cliente;

                GuardarClienteDirecciones();
                //GuardarClienteContactos();
                //GuardarClienteCentroResponsabilidad();
                //GuardarDireccionContactos();
                //GuardarDireccionUbicaciones();

                eCliente_Direccion eDirec = new eCliente_Direccion();
                eDirec = AsignarValoresDireccion();

                if (MiAccion == Cliente.Nuevo)
                {
                    eDirec = unit.Clientes.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirec, txtCodDireccion.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoDirecciones();
                ObtenerListadoClientesContactos();
                //ObtenerListadoCentroResponsabilidad();
                //ObtenerListadoEmpresasCliente();
                CargarListadoProyecto("TODOS");
                result = "OK";
            }
            return result;
        }

        private string ModificarSeparacion() //Agrega el codigo de cliente al lote separado
        {
            string result = "";
            eLotes_Separaciones eLo = AsignarValoresSeparaciones();
            eLo = unit.Clientes.Actualizar_Separacion_Cliente<eLotes_Separaciones>(eLo);
            if (eLo != null)
            {
                result = "OK";
            }
            return result;
        }


        private string Modificar()
        {
            string result = "";
            eCliente eCli = AsignarValoresCliente();
            eCli = unit.Clientes.Guardar_Actualizar_Cliente<eCliente>(eCli, "Actualizar");

            if (eCli != null)
            {
                cod_cliente = eCli.cod_cliente;
                result = "OK";
            }
            return result;
        }

        private eCliente AsignarValoresCliente()
        {
            eCliente eCli = new eCliente();

            eCli.flg_juridico = grdbNatJuridi.SelectedIndex == 1 ? "SI" : "NO";
            eCli.cod_cliente = txtCodCliente.Text;
            eCli.flg_codigo_autogenerado = chkCodigoManual.CheckState == CheckState.Checked ? "SI" : "NO";
            eCli.flg_activo = chkActivoCliente.CheckState == CheckState.Checked ? "SI" : "NO";
            eCli.flg_bienes_separados = chkBienesSepCliente.CheckState == CheckState.Checked ? "SI" : "NO";
            eCli.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            eCli.dsc_documento = txtNroDocumento.Text.Trim();
            eCli.cod_prospecto = txtCodigoProspecto.Text.Trim();
            eCli.cod_empresa = cod_empresa; //== "ALL" ? "00010" : cod_empresa;
            eCli.cod_proyecto = cod_proyecto;
            eCli.dsc_apellido_paterno = txtApellPaterno.Text.Trim();
            eCli.dsc_apellido_materno = txtApellMaterno.Text.Trim();
            eCli.dsc_nombre = txtNombre.Text.Trim();
            eCli.fch_nacimiento = dtFecNacimiento.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacimiento.EditValue);
            eCli.dsc_razon_social = txtRazonSocial.Text.Trim();
            eCli.dsc_razon_comercial = txtNombreComercial.Text.Trim();
            eCli.cod_vendedor = "";
            eCli.cod_estadocivil = lkpEstadoCivil.EditValue == null ? "" : lkpEstadoCivil.EditValue.ToString();
            eCli.cod_categoria = glkpCategoria.EditValue.ToString();
            eCli.dsc_email = txtCorreoPersonal.Text.Trim();
            //eCli.dsc_mail_trabajo = txtCorreoTrabajo.Text.Trim();
            eCli.cod_tipo_contacto = glkpTipoContacto.EditValue == null ? "" : glkpTipoContacto.EditValue.ToString();
            //eCli.cod_calificacion = glkpCalificacion.EditValue.ToString();
            eCli.dsc_telefono_1 = txtFono1.Text.Trim();
            eCli.dsc_telefono_2 = txtFono2.Text.Trim();
            if (lkpEstadoCivil.EditValue.ToString() == "02" || lkpEstadoCivil.EditValue.ToString() == "04")
            {
                eCli.cod_tipo_documento_conyuge = glkpTipoDocumentoConyuge.EditValue == null ? "" : glkpTipoDocumentoConyuge.EditValue.ToString();
                eCli.dsc_documento_conyuge = txtNroDocumentoConyuge.Text.Trim();
                eCli.dsc_apellido_paterno_conyuge = txtApellPaternoConyuge.Text.Trim();
                eCli.dsc_apellido_materno_conyuge = txtApellMaternoConyuge.Text.Trim();
                eCli.dsc_nombre_conyuge = txtNombreConyuge.Text.Trim();
                eCli.fch_nacimiento_conyuge = dtFecNacimientoConyuge.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacimientoConyuge.EditValue);
                eCli.dsc_direccion_conyuge = txtDireccionConyuge.Text.Trim();
                eCli.cod_estadocivil_conyuge = lkpEstadoCivilConyuge.EditValue == null ? "" : lkpEstadoCivilConyuge.EditValue.ToString();
                eCli.dsc_profesion_conyuge = txtOcupacionConyuge.Text.Trim();
                eCli.dsc_email_conyuge = txtCorreoConyuge.Text.Trim();
                eCli.dsc_telefono_1_conyuge = txtFono1Conyugue.Text.Trim();
            }

            //eCli.fch_registro = Convert.ToDateTime(dtFechaRegistro.EditValue);
            eCli.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            eCli.dsc_cliente = grdbNatJuridi.SelectedIndex == 1 ? txtRazonSocial.Text : txtApellPaterno.Text + " " + txtApellMaterno.Text + " " + txtNombre.Text;
            eCli.cod_tipo_cliente = "";
            //eCli.cod_tarjeta_cliente = txtCodTarjeta.Text;  
            eCli.cod_sexo = "";
            //eCli.cod_sexo = lkpSexo.EditValue == null ? "" : lkpSexo.EditValue.ToString();
            eCli.dsc_mail_fe = "";
            eCli.cod_proveedor_ERP = "";
            eCli.flg_proyecto = "SI";
            eCli.flg_prospecto = ValidarStringVacio(txtCodigoProspecto.Text.Trim()) ? "NO" : "SI";
            eCli.dsc_profesion = txtOcupacion.Text.Trim();
            //eCli.cod_asesor = lkpAsesor.EditValue.ToString();


            return eCli;
        }

        private Boolean ValidarStringVacio(string campo)
        {
            if (campo == null)
            {
                return true;
            }
            if (campo.ToString() == "")
            {
                return true;
            }

            return false;
        }

        private eLotes_Separaciones AsignarValoresSeparaciones()
        {
            eLotes_Separaciones eLo = new eLotes_Separaciones();

            eLo.cod_cliente = cod_cliente;
            eLo.flg_cliente = "SI";
            eLo.cod_proyecto = cod_proyecto;
            eLo.cod_separacion = cod_separacion;
            eLo.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            return eLo;
        }

        private void AsignarProyectoPorClienteSeparaciones()
        {
            eCliente ePro = new eCliente();
            ePro.cod_cliente = cod_cliente;
            ePro.cod_empresa = cod_empresa;
            ePro.cod_proyecto = cod_proyecto != null || cod_proyecto != "" ? cod_proyecto : cod_proyecto_titulo;
            ePro.flg_activo = cod_proyecto != null || cod_proyecto != "" ? "SI" : "NO";
            ePro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            ePro = unit.Proyectos.Guardar_Actualizar_ClienteProyecto<eCliente>(ePro);

        }

        private void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            if (xtraTabControl2.SelectedTabPageIndex == 3)
            {
                try
                {
                    if (glkpTipoDireccionConyuge.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDireccionConyuge.ShowPopup(); return; }
                    if (txtNombreDireccionConyuge.Text == "" || txtNombreDireccionConyuge.Text == "[VACÍO]") { MessageBox.Show("Debe ingresar el nombre de la dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDireccionConyuge.ShowPopup(); return; }
                    if (mmDireccionConyuge.Text == "") { MessageBox.Show("Debe ingresar la dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); mmDireccionConyuge.Focus(); return; }
                    if (lkpPaisConyuge.EditValue == null) { lkpPaisConyuge.ShowPopup(); MessageBox.Show("Debe seleccionar un país", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (lkpDepartamentoConyuge.EditValue == null) { lkpDepartamentoConyuge.ShowPopup(); MessageBox.Show("Debe seleccionar un departamento", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (lkpProvinciaConyuge.EditValue == null) { lkpProvinciaConyuge.ShowPopup(); MessageBox.Show("Debe seleccionar una provincia", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (glkpDistritoConyuge.EditValue == null) { glkpDistritoConyuge.ShowPopup(); MessageBox.Show("Debe seleccionar un distrito", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    eCliente_Direccion eDirecConyuge = new eCliente_Direccion();
                    eDirecConyuge = unit.Clientes.ObtenerDireccion<eCliente_Direccion>(4, cod_cliente, 99);
                    if (eDirecConyuge != null)
                    {
                        eDirecConyuge = AsignarValoresDireccionConyuge();
                        eDirecConyuge = unit.Clientes.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirecConyuge, "Actualizar");
                    }
                    else
                    {
                        eDirecConyuge = AsignarValoresDireccionConyuge();
                        eDirecConyuge = unit.Clientes.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirecConyuge, "Nuevo");
                    }

                    ObtenerListadoDirecciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                try
                {
                    if (glkpTipoDireccion.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDireccion.ShowPopup(); return; }
                    //if (glkpTipoDireccion.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDireccion.ShowPopup(); return; }
                    if (txtNombreDireccion.Text == "" || txtNombreDireccion.Text == "[VACÍO]") { MessageBox.Show("Debe ingresar el nombre de la dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpTipoDireccion.ShowPopup(); return; }
                    if (mmDireccion.Text == "") { MessageBox.Show("Debe ingresar la dirección", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); mmDireccion.Focus(); return; }
                    if (lkpPais.EditValue == null) { lkpPais.ShowPopup(); MessageBox.Show("Debe seleccionar un país", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (lkpDepartamento.EditValue == null) { lkpDepartamento.ShowPopup(); MessageBox.Show("Debe seleccionar un departamento", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (lkpProvincia.EditValue == null) { lkpProvincia.ShowPopup(); MessageBox.Show("Debe seleccionar una provincia", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (glkpDistrito.EditValue == null) { glkpDistrito.ShowPopup(); MessageBox.Show("Debe seleccionar un distrito", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //int validarNumeros = validarCantidadNumerosCadena(txtFono1Direccion.Text.Trim());
                    //if (validarNumeros < 8) { MessageBox.Show("Debe ingresar un teléfono", "Guardar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1Direccion.Focus(); return; }

                    eCliente_Direccion eDirec = new eCliente_Direccion();
                    eDirec = AsignarValoresDireccion();

                    if (MiAccion == Cliente.Nuevo)
                    {
                        ListDirecc.Add(eDirec);
                        gvListaDirecciones.RefreshData();
                    }
                    else
                    {
                        eDirec = unit.Clientes.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirec, txtCodDireccion.Text == "0" ? "Nuevo" : "Actualizar");
                    }

                    if (frmHandlerSeparacion != null)
                    {
                        eCliente eCli = new eCliente();
                        eCli = unit.Clientes.ObtenerCliente<eCliente>(20, cod_cliente);
                        eLotes_Separaciones eLotSep = new eLotes_Separaciones();

                        //if (MiAccionSeparacion == Cliente.Nuevo)
                        //{
                        if (copropietario)
                        {
                            frmHandlerSeparacion.transferirDatosCopropietario(eCli); /*actualizarProyectoXClienteSeparacion(); obtenerListadoTipoLoteXEtapa();*/
                            frmHandlerSeparacion.txtDireccionCopropietario.Text = mmDireccion.Text + dsc_distrito + dsc_provincia;
                        }
                        else
                        {
                            frmHandlerSeparacion.transferirDatos(eCli); /*actualizarProyectoXClienteSeparacion(); obtenerListadoTipoLoteXEtapa();*/
                            frmHandlerSeparacion.txtDireccion.Text = mmDireccion.Text + dsc_distrito + dsc_provincia;
                        }
                        /*actualizarProyectoXClienteSeparacion(); obtenerListadoTipoLoteXEtapa();*/

                        AsignarProyectoPorClienteSeparaciones();
                        obtenerListadoTipoLoteXEtapa();
                        CargarComboEnGrid();
                        //}
                        //if (MiAccionSeparacion == Cliente.Editar)
                        //{
                        //    frmHandlerSeparacion.transferirDatos(eCli);
                        //    ModificarSeparacion();
                        //    AsignarProyectoPorClienteSeparaciones();
                        //    obtenerListadoTipoLoteXEtapa();
                        //    CargarComboEnGrid();

                        //    //actualizarProyectoXClienteSeparacion();
                        //}
                    }

                    if (frmHandlerContrato != null)
                    {
                        eCliente eCli = new eCliente();
                        eCli = unit.Clientes.ObtenerCliente<eCliente>(20, cod_cliente);
                        eLotes_Separaciones eLotSep = new eLotes_Separaciones();

                        //if (MiAccionSeparacion == Cliente.Nuevo)
                        //{
                        if (copropietario)
                        {
                            frmHandlerContrato.transferirDatosCopropietario(eCli); /*actualizarProyectoXClienteSeparacion(); obtenerListadoTipoLoteXEtapa();*/
                            frmHandlerContrato.txtDireccionCopropietario.Text = mmDireccion.Text + dsc_distrito + dsc_provincia;
                        }
                        else
                        {
                            frmHandlerContrato.transferirDatos(eCli); /*actualizarProyectoXClienteSeparacion(); obtenerListadoTipoLoteXEtapa();*/
                            frmHandlerContrato.txtDireccion.Text = mmDireccion.Text + dsc_distrito + dsc_provincia;
                        }
                        /*actualizarProyectoXClienteSeparacion(); obtenerListadoTipoLoteXEtapa();*/

                        AsignarProyectoPorClienteSeparaciones();
                        obtenerListadoTipoLoteXEtapa();
                        CargarComboEnGrid();
                        //}
                        //if (MiAccionSeparacion == Cliente.Editar)
                        //{
                        //    frmHandlerSeparacion.transferirDatos(eCli);
                        //    ModificarSeparacion();
                        //    AsignarProyectoPorClienteSeparaciones();
                        //    obtenerListadoTipoLoteXEtapa();
                        //    CargarComboEnGrid();

                        //    //actualizarProyectoXClienteSeparacion();
                        //}
                    }
                    //if (frmHandlerSeparacion != null)
                    //{
                    //    frmHandlerSeparacion.mmDireccion.Text = mmDireccion.Text;
                    //}
                    ObtenerListadoDirecciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void GuardarClienteDirecciones()
        {
            for (int x = 0; x <= gvListaDirecciones.RowCount - 1; x++)
            {
                eCliente_Direccion eDirec = gvListaDirecciones.GetRow(x) as eCliente_Direccion;
                if (eDirec != null)
                {
                    eDirec.cod_cliente = cod_cliente;
                    eDirec = unit.Clientes.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirec, "Nuevo");
                }
            }
        }
        private void GuardarClienteContactos()
        {
            for (int x = 0; x <= gvListaClienteContactos.RowCount - 1; x++)
            {
                eCliente_Contactos eContact = gvListaClienteContactos.GetRow(x) as eCliente_Contactos;
                if (eContact != null)
                {
                    eContact.cod_cliente = cod_cliente;
                    eContact = unit.Clientes.Guardar_Actualizar_ClienteContacto<eCliente_Contactos>(eContact, "Nuevo");
                }
            }
        }

        private void GuardarClienteCentroResponsabilidad()
        {
            for (int x = 0; x <= tlClienteCentroResponsabilidad.AllNodesCount - 1; x++)
            {
                eCliente_CentroResponsabilidad eCentroResp = tlClienteCentroResponsabilidad.GetRow(x) as eCliente_CentroResponsabilidad;
                if (eCentroResp != null)
                {
                    eCentroResp.cod_cliente = cod_cliente;
                    eCentroResp = unit.Clientes.Guardar_Actualizar_ClienteCentroResponsabilidad<eCliente_CentroResponsabilidad>(eCentroResp, "Nuevo");
                }
            }
        }

        private void GuardarDireccionContactos()
        {
            for (int x = 0; x <= gvListaDireccionContactos.RowCount - 1; x++)
            {
                eCliente_Contactos eContact = gvListaDireccionContactos.GetRow(x) as eCliente_Contactos;
                if (eContact != null)
                {
                    eContact.cod_cliente = cod_cliente;
                    eContact = unit.Clientes.Guardar_Actualizar_ClienteDireccionContacto<eCliente_Contactos>(eContact, "Nuevo");
                }
            }
        }

        private void GuardarDireccionUbicaciones()
        {
            for (int x = 0; x <= tlUbicacionesDireccion.AllNodesCount - 1; x++)
            {
                eCliente_Ubicacion eUbic = tlUbicacionesDireccion.GetRow(x) as eCliente_Ubicacion;
                if (eUbic != null)
                {
                    eUbic.cod_cliente = cod_cliente;
                    eUbic = unit.Clientes.Guardar_Actualizar_ClienteUbicacion<eCliente_Ubicacion>(eUbic, "Nuevo");
                }
            }
        }

        private eCliente_Direccion AsignarValoresDireccion()
        {
            eCliente_Direccion eDirec = new eCliente_Direccion();
            eDirec.cod_cliente = cod_cliente;
            eDirec.num_linea = Convert.ToInt32(txtCodDireccion.Text);
            eDirec.cod_tipo_direccion = glkpTipoDireccion.EditValue == null ? "" : glkpTipoDireccion.EditValue.ToString();
            eDirec.dsc_tipo_direccion = glkpTipoDireccion.EditValue == null ? "" : glkpTipoDireccion.Text;
            eDirec.dsc_nombre_direccion = txtNombreDireccion.Text;
            //eDirec.cod_tipo_via = glkpTipoCalle.EditValue == null ? "" : glkpTipoCalle.EditValue.ToString();
            //eDirec.cod_calle_direccion = glkpTipoAvenida.EditValue;
            //eDirec.cod_tipo_zona = glkpTipoUrbanizacion.EditValue == null ? "" : glkpTipoUrbanizacion.EditValue.ToString();
            //eDirec.cod_urbanizacion = glkpTipoEtapa.Text;
            //eDirec.dsc_urbanizacion = glkpTipoEtapa.Text;
            //eDirec.cod_numero = txtNumero.Text;
            //eDirec.cod_interior = txtInterior.Text;
            //eDirec.cod_manzana = txtManzana.Text;
            //eDirec.cod_lote = txtLote.Text;
            //eDirec.cod_sublote = txtSubLote.Text;
            eDirec.dsc_cadena_direccion = mmDireccion.Text;
            eDirec.cod_pais = lkpPais.EditValue == null ? "" : lkpPais.EditValue.ToString();
            eDirec.cod_departamento = lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString();
            eDirec.cod_provincia = lkpProvincia.EditValue == null ? "" : lkpProvincia.EditValue.ToString();
            eDirec.cod_distrito = glkpDistrito.EditValue == null ? "" : glkpDistrito.EditValue.ToString();
            eDirec.dsc_telefono_1 = txtFono1Direccion.Text;
            eDirec.dsc_telefono_2 = txtFono2Direccion.Text;
            eDirec.dsc_referencia = txtReferecia.Text;
            //eDirec.flg_comprobante = chkFlgComprobante.CheckState == CheckState.Checked ? "SI" : "NO";
            //eDirec.flg_direccion_cobranza = chkFlgCobranza.CheckState == CheckState.Checked ? "SI" : "NO";

            return eDirec;
        }

        private eCliente_Direccion AsignarValoresDireccionConyuge()
        {
            eCliente_Direccion eDirec = new eCliente_Direccion();
            eDirec.cod_cliente = cod_cliente;
            eDirec.num_linea = Convert.ToInt32(txtCodDireccionConyuge.Text);
            eDirec.cod_tipo_direccion = glkpTipoDireccionConyuge.EditValue == null ? "" : glkpTipoDireccionConyuge.EditValue.ToString();
            eDirec.dsc_tipo_direccion = glkpTipoDireccionConyuge.EditValue == null ? "" : glkpTipoDireccionConyuge.Text;
            eDirec.dsc_nombre_direccion = txtNombreDireccionConyuge.Text;
            //eDirec.cod_tipo_via = glkpTipoCalle.EditValue == null ? "" : glkpTipoCalle.EditValue.ToString();
            //eDirec.cod_calle_direccion = glkpTipoAvenida.EditValue;
            //eDirec.cod_tipo_zona = glkpTipoUrbanizacion.EditValue == null ? "" : glkpTipoUrbanizacion.EditValue.ToString();
            //eDirec.cod_urbanizacion = glkpTipoEtapa.Text;
            //eDirec.dsc_urbanizacion = glkpTipoEtapa.Text;
            //eDirec.cod_numero = txtNumero.Text;
            //eDirec.cod_interior = txtInterior.Text;
            //eDirec.cod_manzana = txtManzana.Text;
            //eDirec.cod_lote = txtLote.Text;
            //eDirec.cod_sublote = txtSubLote.Text;
            eDirec.dsc_cadena_direccion = mmDireccionConyuge.Text;
            eDirec.cod_pais = lkpPaisConyuge.EditValue == null ? "" : lkpPaisConyuge.EditValue.ToString();
            eDirec.cod_departamento = lkpDepartamentoConyuge.EditValue == null ? "" : lkpDepartamentoConyuge.EditValue.ToString();
            eDirec.cod_provincia = lkpProvinciaConyuge.EditValue == null ? "" : lkpProvinciaConyuge.EditValue.ToString();
            eDirec.cod_distrito = glkpDistritoConyuge.EditValue == null ? "" : glkpDistritoConyuge.EditValue.ToString();
            eDirec.dsc_telefono_1 = txtFono1DireccionConyuge.Text;
            eDirec.dsc_telefono_2 = txtFono2DireccionConyuge.Text;
            eDirec.dsc_referencia = txtRefereciaConyuge.Text;
            //eDirec.flg_comprobante = chkFlgComprobante.CheckState == CheckState.Checked ? "SI" : "NO";
            //eDirec.flg_direccion_cobranza = chkFlgCobranza.CheckState == CheckState.Checked ? "SI" : "NO";

            return eDirec;
        }

        private void rbtnEliminarDireccion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta dirección?", "Eliminar dirección", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                    //eCliente_Contactos eContact = new eCliente_Contactos();
                    //eContact = unit.Clientes.ValidacionEliminar<eCliente_Contactos>(29, cod_cliente, eDirec.num_linea);
                    //if (eContact != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene contactos vinculados.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    //eUbic = unit.Clientes.ValidacionEliminar<eCliente_Ubicacion>(30, cod_cliente, eDirec.num_linea);
                    //if (eUbic != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene ubicaciones vinculadas.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = unit.Clientes.Eliminar_ClienteDireccion(cod_cliente, eDirec.num_linea);
                    ObtenerListadoDirecciones();
                    if (ListDirecc.Count == 0) LimpiarCamposDireccion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposDireccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCamposDireccion()
        {
            //MessageBox.Show("valor : " + glkpTipoDireccion.EditValue);
            txtCodDireccion.Text = "0";
            glkpTipoDireccion.EditValue = null;
            //txtNombreDireccion.Text = "";
            //glkpTipoCalle.EditValue = null;
            //glkpTipoAvenida.Text= "";
            //txtNumero.Text = "";
            //txtInterior.Text = "";
            //glkpTipoUrbanizacion.EditValue = null;
            //glkpTipoEtapa.Text = "";
            //txtManzana.Text = "";
            //txtLote.Text = "";
            //txtSubLote.Text = "";
            mmDireccion.Text = "";

            //lkpDepartamento.EditValue = null;
            //lkpProvincia.EditValue = null;
            //glkpDistrito.EditValue = null;
            txtFono1Direccion.Text = "";
            txtFono2Direccion.Text = "";
            txtReferecia.Text = "";
            //chkFlgComprobante.CheckState = CheckState.Unchecked;
            //chkFlgCobranza.CheckState = CheckState.Unchecked;

            glkpDistrito.EditValue = null;
            lkpProvincia.EditValue = null;
            lkpDepartamento.EditValue = null;
            lkpPais.EditValue = "00001";
            //unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "00015", cod_condicion: "00001");
            //unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "00128", cod_condicion: "00015");
            //unit.Clientes.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: "00128");

            glkpTipoDireccion.Focus();
        }

        private void btnEliminarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta dirección?", "Eliminar dirección", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                    eCliente_Contactos eContact = new eCliente_Contactos();
                    //eContact = unit.Clientes.ValidacionEliminar<eCliente_Contactos>(29, cod_cliente, eDirec.num_linea);
                    //if (eContact != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene contactos vinculados.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    //eUbic = unit.Clientes.ValidacionEliminar<eCliente_Ubicacion>(30, cod_cliente, eDirec.num_linea);
                    //if (eUbic != null) { MessageBox.Show("No se puede eliminar la dirección ya que tiene ubicaciones vinculadas.", "Eliminar dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = unit.Clientes.Eliminar_ClienteDireccion(cod_cliente, eDirec.num_linea);
                    ObtenerListadoDirecciones();
                    if (ListDirecc.Count == 0) LimpiarCamposDireccion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodDireccion_TextChanged(object sender, EventArgs e)
        {
            btnEliminarDireccion.Enabled = txtCodDireccion.Text != "0" && txtCodDireccion.Text != "" ? true : false;
        }


        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = Cliente.Nuevo;
            LimpiarCamposCliente();
            //LimpiarCamposDireccion();
            xtraTabControl1.Enabled = false;
            btnBuscarProspecto.Enabled = true;
            btnBuscarSeparacion.Enabled = true;
            camposClientes(false);
            this.xtraTabControl1.SelectedTabPage = xtabDireccionCliente;
        }

        private void LimpiarCamposCliente()
        {
            grdbNatJuridi.SelectedIndex = 0;
            txtCodCliente.Text = "";
            chkCodigoManual.CheckState = CheckState.Unchecked;
            chkActivoCliente.CheckState = CheckState.Unchecked;
            chkBienesSepCliente.CheckState = CheckState.Unchecked;
            txtNroDocumento.Text = "";
            txtCodigoProspecto.Text = "";
            txtOcupacion.Text = "";
            //chkFlgJuridica.CheckState = CheckState.Unchecked;
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            //glkpTipoDocumento.EditValue = null;

            dtFecNacimiento.EditValue = null;
            txtApellPaterno.Text = "";
            txtApellMaterno.Text = "";
            txtNombre.Text = "";
            txtRazonSocial.Text = "";
            txtNombreComercial.Text = "";
            lkpEstadoCivil.EditValue = null;
            //txtCodVendedor.Text = "";
            //txtVendedor.Text = "";
            glkpCategoria.EditValue = "";
            txtCorreoPersonal.Text = "";
            //txtCorreoTrabajo.Text = "";
            glkpTipoContacto.EditValue = null;
            //glkpCalificacion.EditValue = null;
            //lkpTipoCliente.EditValue = null;
            txtFono1.Text = "";
            txtFono2.Text = "";
            //txtCodTarjeta.Text = "";

            //lkpSexo.EditValue = null;

            //txtCorreoFE.Text = "";


            if (MiAccion == Cliente.Nuevo)
            {
                picAnteriorCliente.Enabled = false; picSiguienteCliente.Enabled = false;
            }
            bsClienteObservaciones.DataSource = null; gvObservacionesCliente.RefreshData();
            bsListaDirecciones.DataSource = null; gvListaDirecciones.RefreshData();
            bsDireccionContactos.DataSource = null; gvListaDireccionContactos.RefreshData();
            bsDireccionUbicaciones.DataSource = null; tlUbicacionesDireccion.Refresh();
            LimpiarCamposCpnyugecliente();
            LimpiarCamposDireccion();
            LimpiarCamposDireccionContacto();
            LimpiarCamposUbicacion();

            bsClienteContactos.DataSource = null; gvListaClienteContactos.RefreshData();
            bsClienteCentroResponsabilidad.DataSource = null; tlClienteCentroResponsabilidad.Refresh();
            LimpiarCamposClienteContacto();
            LimpiarCamposCentroResponsabilidad();

            btnNuevo.Enabled = false;
            glkpTipoDocumento.EditValue = "DI001";
            txtNroDocumento.Focus();
        }

        private void picAnteriorCliente_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaClientes.RowCount - 1;
                int nRow = frmHandler.gvListaClientes.FocusedRowHandle;
                frmHandler.gvListaClientes.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                eCliente obj = frmHandler.gvListaClientes.GetFocusedRow() as eCliente;
                cod_cliente = obj.cod_cliente;
                MiAccion = Cliente.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteCliente_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaClientes.RowCount - 1;
                int nRow = frmHandler.gvListaClientes.FocusedRowHandle;
                frmHandler.gvListaClientes.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eCliente obj = frmHandler.gvListaClientes.GetFocusedRow() as eCliente;
                cod_cliente = obj.cod_cliente;
                MiAccion = Cliente.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlUbicacionesDireccion_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if (e.Node == null) { LimpiarCamposUbicacion(); return; }
                if (e.Node.Nodes != null)
                {
                    txtCodUbicacion.Text = e.Node.GetValue(colcod_ubicacion) == null ? "" : e.Node.GetValue(colcod_ubicacion).ToString();
                    chkActivoUbicacion.CheckState = e.Node.GetValue(colflg_activo) == null ? CheckState.Unchecked : e.Node.GetValue(colflg_activo).ToString() == "SI" ? CheckState.Checked : CheckState.Unchecked;
                    mmDescripcionUbicacion.Text = e.Node.GetValue(coldsc_ubicacion) == null ? "" : e.Node.GetValue(coldsc_ubicacion).ToString();
                    lkpNivelUbicacion.EditValue = e.Node.GetValue(colcod_nivel) == null ? "" : e.Node.GetValue(colcod_nivel).ToString();
                    lkpNivelSuperiorUbicacion.EditValue = e.Node.GetValue(colcod_ubicacion_sup) == null ? "" : e.Node.GetValue(colcod_ubicacion_sup).ToString();
                    lkpResponsableUbicacion.EditValue = e.Node.GetValue(colcod_contacto) == null ? "" : e.Node.GetValue(colcod_contacto).ToString();
                    mmObservacionUbicacion.Text = e.Node.GetValue(coldsc_observacion) == null ? "" : e.Node.GetValue(coldsc_observacion).ToString();
                    txtCodPerUbicacion.Text = e.Node.GetValue(colcod_ubicacion_per) == null ? "" : e.Node.GetValue(colcod_ubicacion_per).ToString();
                    //mmDireccionUbicacion.Text = e.Node.GetValue(coldsc_direccion) == null ? "" : e.Node.GetValue(coldsc_direccion).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposUbicacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (mmDescripcionUbicacion.Text == "") { MessageBox.Show("Debe ingresar una descripcion", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); mmDescripcionUbicacion.Focus(); return; }
                if (lkpNivelUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelUbicacion.Focus(); return; }
                if (lkpNivelSuperiorUbicacion.EditValue == null && lkpNivelUbicacion.EditValue.ToString() != "N01") { MessageBox.Show("Debe seleccionar un nivel superior", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelSuperiorUbicacion.Focus(); return; }
                //if (lkpResponsableUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un contacto responsable", "Guardar ubicación", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpResponsableUbicacion.Focus(); return; }

                eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                eUbic = AsignarValoresUbicacion();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListUbic.Add(eUbic);
                }
                else
                {
                    eUbic = unit.Clientes.Guardar_Actualizar_ClienteUbicacion<eCliente_Ubicacion>(eUbic, txtCodUbicacion.Text == "00000000" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoUbicaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInactivarUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar esta ubicación?", "Inactivar ubicación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Ubicacion eUbic = tlUbicacionesDireccion.GetFocusedRow() as eCliente_Ubicacion;
                    string result = unit.Clientes.Inactivar_ClienteUbicacion(cod_cliente, eUbic.cod_ubicacion);
                    ObtenerListadoUbicaciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private eCliente_Ubicacion AsignarValoresUbicacion()
        {
            eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
            eUbic.cod_cliente = cod_cliente;
            eUbic.cod_ubicacion = txtCodUbicacion.Text;
            eUbic.flg_activo = chkActivoUbicacion.CheckState == CheckState.Checked ? "SI" : "NO";
            eUbic.dsc_ubicacion = mmDescripcionUbicacion.Text;
            eUbic.cod_nivel = lkpNivelUbicacion.EditValue.ToString();
            eUbic.cod_ubicacion_sup = lkpNivelSuperiorUbicacion.EditValue == null ? "" : lkpNivelSuperiorUbicacion.EditValue.ToString();
            eUbic.dsc_observacion = mmObservacionUbicacion.Text;
            eUbic.cod_ubicacion_per = txtCodPerUbicacion.Text;
            eUbic.cod_contacto = lkpResponsableUbicacion.EditValue == null ? 0 : Convert.ToInt32(lkpResponsableUbicacion.EditValue);

            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            eUbic.num_linea = eDirec.num_linea;

            return eUbic;
        }
        private void LimpiarCamposUbicacion()
        {
            txtCodUbicacion.Text = "00000000";
            chkActivoUbicacion.CheckState = CheckState.Checked;
            mmDescripcionUbicacion.Text = "";
            lkpNivelUbicacion.EditValue = null;
            lkpNivelSuperiorUbicacion.EditValue = null;
            mmObservacionUbicacion.Text = "";
            txtCodPerUbicacion.Text = "";
            lkpResponsableUbicacion.EditValue = null;
            mmDescripcionUbicacion.Focus();
        }

        private void lkpNivelUbicacion_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lkpNivelUbicacion.EditValue != null)
                {
                    eCliente_Direccion obj = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                    unit.Clientes.CargaCombosLookUp("NivelSuperiorUbicacion", lkpNivelSuperiorUbicacion, "cod_ubicacion", "dsc_ubicacion", "", cod_cliente, lkpNivelUbicacion.EditValue.ToString(), obj.num_linea);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlUbicacionesDireccion_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e.Node.Nodes != null) { if (e.Node.GetValue(colflg_activo).ToString() == "NO") e.Appearance.ForeColor = Color.Red; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoClienteContacto_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposClienteContacto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCamposClienteContacto()
        {
            txtCodClienteContacto.Text = "0";
            txtNombreClienteContacto.Text = "";
            txtApellidoClienteContacto.Text = "";
            dtFecNacClienteContacto.EditValue = null;
            txtCorreoClienteContacto.Text = "";
            txtFono1ClienteContacto.Text = "";
            txtFono2ClienteContacto.Text = "";
            txtCargoClienteContacto.Text = "";
            txtUsuarioWebClienteContacto.Text = "";
            txtClaveWebClienteContacto.Text = "";
            mmObservacionClienteContacto.Text = "";
            txtNombreClienteContacto.Focus();
        }
        private void btnGuardarClienteContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un nombre", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreClienteContacto.Focus(); return; }
                if (txtApellidoClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un apellido", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellidoClienteContacto.Focus(); return; }
                //if (dtFecNacClienteContacto.EditValue == null) { MessageBox.Show("Debe ingresar una fecha de nacimiento", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); dtFecNacClienteContacto.Focus(); return; }
                if (txtCorreoClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un correo", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoClienteContacto.Focus(); return; }
                if (txtFono1ClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1ClienteContacto.Focus(); return; }
                if (txtCargoClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un cargo", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCargoClienteContacto.Focus(); return; }
                //if (txtUsuarioWebClienteContacto.Text == "") { MessageBox.Show("Debe ingresar un usuario web", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtUsuarioWebClienteContacto.Focus(); return; }
                //if (txtClaveWebClienteContacto.Text == "") { MessageBox.Show("Debe ingresar una clave web", "Guardar contacto del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); txtClaveWebClienteContacto.Focus(); return; }

                eCliente_Contactos eContact = new eCliente_Contactos();
                eContact = AsignarValoresClienteContacto();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListClienteContacto.Add(eContact);
                }
                else
                {
                    eContact = unit.Clientes.Guardar_Actualizar_ClienteContacto<eCliente_Contactos>(eContact, txtCodClienteContacto.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoClientesContactos();
                unit.Clientes.CargaCombosLookUp("ResponsableCentroResponsabilidad", lkpResponsableCentroResponsabilidad, "cod_contacto", "dsc_nombre_completo", "", cod_cliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private eCliente_Contactos AsignarValoresClienteContacto()
        {
            eCliente_Contactos eContact = new eCliente_Contactos();
            eContact.cod_cliente = cod_cliente;
            eContact.cod_contacto = Convert.ToInt32(txtCodClienteContacto.Text);
            eContact.dsc_nombre = txtNombreClienteContacto.Text;
            eContact.dsc_apellidos = txtApellidoClienteContacto.Text;
            eContact.fch_nacimiento = dtFecNacClienteContacto.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacClienteContacto.EditValue);
            eContact.dsc_correo = txtCorreoClienteContacto.Text;
            eContact.dsc_telefono1 = txtFono1ClienteContacto.Text;
            eContact.dsc_telefono2 = txtFono2ClienteContacto.Text;
            eContact.dsc_cargo = txtCargoClienteContacto.Text;
            eContact.cod_usuario_web = txtUsuarioWebClienteContacto.Text;
            eContact.cod_clave_web = txtClaveWebClienteContacto.Text;
            eContact.dsc_observaciones = mmObservacionClienteContacto.Text;
            eContact.cod_usuario_reg = Program.Sesion.Usuario.cod_usuario;

            return eContact;
        }
        private void btnEliminarClienteContacto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaClienteContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
                    eCentroResp = unit.Clientes.ValidacionEliminar<eCliente_CentroResponsabilidad>(27, cod_cliente, cod_contacto: eContact.cod_contacto);
                    if (eCentroResp != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a un centro de responsabilidad.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = unit.Clientes.Eliminar_ClienteContacto(cod_cliente, eContact.cod_contacto);
                    ObtenerListadoClientesContactos();
                    if (ListClienteContacto.Count == 0) LimpiarCamposClienteContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoDireccionContacto_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposDireccionContacto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCamposDireccionContacto()
        {
            txtCodDireccionContacto.Text = "0";
            txtNombreDireccionContacto.Text = "";
            txtApellidoDireccionContacto.Text = "";
            dtFecNacDireccionContacto.EditValue = null;
            txtCorreoDireccionContacto.Text = "";
            txtFono1DireccionContacto.Text = "";
            txtFono2DireccionContacto.Text = "";
            txtCargoDireccionContacto.Text = "";
            mmObservacionDireccionContacto.Text = "";
            txtUsuarioWebDireccionContacto.Text = "";
            txtClaveWebDireccionContacto.Text = "";
            txtNombreDireccionContacto.Focus();
        }
        private void btnGuardarDireccionContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un nombre", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNombreDireccionContacto.Focus(); return; }
                if (txtApellidoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un apellido", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtApellidoDireccionContacto.Focus(); return; }
                //if (dtFecNacDireccionContacto.EditValue == null) { MessageBox.Show("Debe ingresar una fecha de nacimiento", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); dtFecNacDireccionContacto.Focus(); return; }
                if (txtCorreoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un correo", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCorreoDireccionContacto.Focus(); return; }
                if (txtFono1DireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un teléfono", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFono1DireccionContacto.Focus(); return; }
                if (txtCargoDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un cargo", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCargoDireccionContacto.Focus(); return; }
                //if (txtUsuarioWebDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar un usuario web", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtUsuarioWebDireccionContacto.Focus(); return; }
                //if (txtClaveWebDireccionContacto.Text == "") { MessageBox.Show("Debe ingresar una clave web", "Guardar contacto de la dirección", MessageBoxButtons.OK, MessageBoxIcon.Error); txtClaveWebDireccionContacto.Focus(); return; }

                eCliente_Contactos eContact = new eCliente_Contactos();
                eContact = AsignarValoresDireccionContacto();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListClienteContacto.Add(eContact);
                }
                else
                {
                    eContact = unit.Clientes.Guardar_Actualizar_ClienteDireccionContacto<eCliente_Contactos>(eContact, txtCodDireccionContacto.Text == "0" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoDireccionesContactos();
                eCliente_Direccion obj = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
                unit.Clientes.CargaCombosLookUp("ResponsableUbicacion", lkpResponsableUbicacion, "cod_contacto", "dsc_nombre_completo", "", cod_cliente, num_linea: obj.num_linea);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private eCliente_Contactos AsignarValoresDireccionContacto()
        {
            eCliente_Contactos eContact = new eCliente_Contactos();
            eContact.cod_cliente = cod_cliente;
            eContact.cod_contacto = Convert.ToInt32(txtCodDireccionContacto.Text);
            eContact.dsc_nombre = txtNombreDireccionContacto.Text;
            eContact.dsc_apellidos = txtApellidoDireccionContacto.Text;
            eContact.fch_nacimiento = dtFecNacDireccionContacto.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacDireccionContacto.EditValue);
            eContact.dsc_correo = txtCorreoDireccionContacto.Text;
            eContact.dsc_telefono1 = txtFono1DireccionContacto.Text;
            eContact.dsc_telefono2 = txtFono2DireccionContacto.Text;
            eContact.dsc_cargo = txtCargoDireccionContacto.Text;
            eContact.dsc_observaciones = mmObservacionDireccionContacto.Text;
            eContact.cod_usuario_web = txtUsuarioWebDireccionContacto.Text;
            eContact.cod_clave_web = txtClaveWebDireccionContacto.Text;
            eContact.cod_usuario_reg = Program.Sesion.Usuario.cod_usuario;

            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            eContact.num_linea = eDirec.num_linea;

            return eContact;
        }
        private void btnEliminarDireccionContacto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaDireccionContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    eUbic = unit.Clientes.ValidacionEliminar<eCliente_Ubicacion>(28, cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    if (eUbic != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a una ubicación.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = unit.Clientes.Eliminar_ClienteDireccionContacto(cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    ObtenerListadoDireccionesContactos();
                    if (ListDireccionContacto.Count == 0) LimpiarCamposDireccionContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoCentroResponsabilidad_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposCentroResponsabilidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposCpnyugecliente()
        {
            glkpTipoDocumentoConyuge.EditValue = "DI001";
            txtNroDocumentoConyuge.Text = "";
            txtApellPaternoConyuge.Text = "";
            txtApellMaternoConyuge.Text = "";
            txtNombreConyuge.Text = "";
            txtDireccionConyuge.Text = "";
            lkpEstadoCivilConyuge.EditValue = null;
            txtOcupacionConyuge.Text = "";
            txtCorreoConyuge.Text = "";
            txtFono1Conyugue.Text = "";
            dtFecNacimientoConyuge.EditValue = null;
        }

        private void LimpiarCamposCentroResponsabilidad()
        {
            txtCodCentroResponsabilidad.Text = "00000";
            txtDescripcionCentroResponsabilidad.Text = "";
            lkpNivelCentroResponsabilidad.EditValue = null;
            lkpNivelSupCentroResponsabilidad.EditValue = null;
            chkActivoCentroResponsabilidad.CheckState = CheckState.Checked;
            chkConsolidadorCentroResponsabilidad.CheckState = CheckState.Unchecked;
            mmObservacionCentroResponsabilidad.Text = "";
            lkpResponsableCentroResponsabilidad.EditValue = null;
            txtDescripcionCentroResponsabilidad.Focus();
        }

        private void btnGuardarCentroResponsabilidad_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionCentroResponsabilidad.Text == "") { MessageBox.Show("Debe ingresar una descripción", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDescripcionCentroResponsabilidad.Focus(); return; }
                if (lkpNivelCentroResponsabilidad.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelCentroResponsabilidad.Focus(); return; }
                if (lkpNivelSupCentroResponsabilidad.EditValue == null && Convert.ToInt32(lkpNivelCentroResponsabilidad.EditValue) != 1) { MessageBox.Show("Debe seleccionar un nivel superior", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpNivelSupCentroResponsabilidad.Focus(); return; }
                //if (lkpResponsableCentroResponsabilidad.EditValue == null) { MessageBox.Show("Debe seleccionar un responsable", "Guardar centro de responsabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpResponsableCentroResponsabilidad.Focus(); return; }

                eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
                eCentroResp = AsignarValoresClienteCentroResponsabilidad();

                if (MiAccion == Cliente.Nuevo)
                {
                    ListCentroResp.Add(eCentroResp);
                }
                else
                {
                    eCentroResp = unit.Clientes.Guardar_Actualizar_ClienteCentroResponsabilidad<eCliente_CentroResponsabilidad>(eCentroResp, txtCodCentroResponsabilidad.Text == "00000" ? "Nuevo" : "Actualizar");
                }

                ObtenerListadoCentroResponsabilidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private eCliente_CentroResponsabilidad AsignarValoresClienteCentroResponsabilidad()
        {
            eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
            eCentroResp.cod_cliente = cod_cliente;
            eCentroResp.cod_centroresp = txtCodCentroResponsabilidad.Text;
            eCentroResp.dsc_centroresp = txtDescripcionCentroResponsabilidad.Text;
            eCentroResp.num_nivel = Convert.ToInt32(lkpNivelCentroResponsabilidad.EditValue);
            eCentroResp.cod_centroresp_sup = lkpNivelSupCentroResponsabilidad.EditValue == null ? "" : lkpNivelSupCentroResponsabilidad.EditValue.ToString();
            eCentroResp.flg_activo = chkActivoCentroResponsabilidad.CheckState == CheckState.Checked ? "SI" : "NO";
            eCentroResp.dsc_observaciones = mmObservacionCentroResponsabilidad.Text;
            eCentroResp.cod_contacto = lkpResponsableCentroResponsabilidad.EditValue == null ? 0 : Convert.ToInt32(lkpResponsableCentroResponsabilidad.EditValue);
            eCentroResp.flg_consolidador = chkConsolidadorCentroResponsabilidad.CheckState == CheckState.Checked ? "SI" : "NO";

            return eCentroResp;
        }

        private void btnInactivarCentroResponsabilidad_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar este centro de responsabilidad?", "Inactivar centro de responsabilidad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_CentroResponsabilidad eCentroResp = tlClienteCentroResponsabilidad.GetFocusedRow() as eCliente_CentroResponsabilidad;
                    string result = unit.Clientes.Inactivar_ClienteCentroResponsabilidad(cod_cliente, eCentroResp.cod_centroresp);
                    ObtenerListadoCentroResponsabilidad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlClienteCentroResponsabilidad_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e.Node.Nodes != null) if (e.Node.GetValue(colflg_activo).ToString() == "NO") e.Appearance.ForeColor = Color.Red;
            }
            catch
            {
                //txtCodCentroResponsabilidad.Text = "00000";
                //MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlClienteCentroResponsabilidad_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if (e.Node == null) { LimpiarCamposUbicacion(); return; }
                if (e.Node.Nodes != null)
                {
                    txtCodCentroResponsabilidad.Text = e.Node.GetValue(colcod_centroresp) == null ? "" : e.Node.GetValue(colcod_centroresp).ToString();
                    chkActivoCentroResponsabilidad.CheckState = e.Node.GetValue(colflg_activo1) == null ? CheckState.Unchecked : e.Node.GetValue(colflg_activo1).ToString() == "SI" ? CheckState.Checked : CheckState.Unchecked;
                    txtDescripcionCentroResponsabilidad.Text = e.Node.GetValue(coldsc_centroresp) == null ? "" : e.Node.GetValue(coldsc_centroresp).ToString();
                    lkpNivelCentroResponsabilidad.EditValue = e.Node.GetValue(colnum_nivel) == null ? 0 : Convert.ToInt32(e.Node.GetValue(colnum_nivel));
                    lkpNivelSupCentroResponsabilidad.EditValue = e.Node.GetValue(colcod_centroresp_sup) == null ? "" : e.Node.GetValue(colcod_centroresp_sup).ToString();
                    lkpResponsableCentroResponsabilidad.EditValue = e.Node.GetValue(colcod_contacto1) == null ? 0 : Convert.ToInt32(e.Node.GetValue(colcod_contacto1));
                    mmObservacionCentroResponsabilidad.Text = e.Node.GetValue(coldsc_observaciones) == null ? "" : e.Node.GetValue(coldsc_observaciones).ToString();
                    chkConsolidadorCentroResponsabilidad.CheckState = e.Node.GetValue(colflg_consolidador) == null ? CheckState.Unchecked : e.Node.GetValue(colflg_consolidador).ToString() == "SI" ? CheckState.Checked : CheckState.Unchecked;
                }
            }
            catch (Exception ex)
            {
                txtCodCentroResponsabilidad.Text = "00000";
                // MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpNivelCentroResponsabilidad_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lkpNivelCentroResponsabilidad.EditValue != null)
                {
                    unit.Clientes.CargaCombosLookUp("NivelSuperiorCentroResponsabilidad", lkpNivelSupCentroResponsabilidad, "cod_centroresp", "dsc_centroresp", "", cod_cliente, num_nivel: lkpNivelCentroResponsabilidad.EditValue == null ? 0 : Convert.ToInt32(lkpNivelCentroResponsabilidad.EditValue));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaDireccionContactos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eCliente_Contactos obj = gvListaDireccionContactos.GetRow(e.FocusedRowHandle) as eCliente_Contactos;
                    eCliente_Contactos eContact = new eCliente_Contactos();
                    eContact = unit.Clientes.ObtenerDireccionContacto<eCliente_Contactos>(10, cod_cliente, obj.num_linea, obj.cod_contacto);

                    txtCodDireccionContacto.Text = eContact.cod_contacto.ToString();
                    txtNombreDireccionContacto.Text = eContact.dsc_nombre;
                    txtApellidoDireccionContacto.Text = eContact.dsc_apellidos;
                    //dtFecNacDireccionContacto.EditValue = eContact.fch_nacimiento;
                    if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacDireccionContacto.EditValue = null; } else { dtFecNacDireccionContacto.EditValue = Convert.ToDateTime(eContact.fch_nacimiento); }
                    txtCorreoDireccionContacto.Text = eContact.dsc_correo;
                    txtFono1DireccionContacto.Text = eContact.dsc_telefono1;
                    txtFono2DireccionContacto.Text = eContact.dsc_telefono2;
                    mmObservacionDireccionContacto.Text = eContact.dsc_observaciones;
                    txtUsuarioWebDireccionContacto.Text = eContact.cod_usuario_web;
                    txtClaveWebDireccionContacto.Text = eContact.cod_clave_web;
                    txtCargoDireccionContacto.Text = eContact.dsc_cargo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaDireccionContactos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListaDireccionContactos_FocusedRowChanged(gvListaDireccionContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void gvListaClienteContactos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eCliente_Contactos obj = gvListaClienteContactos.GetRow(e.FocusedRowHandle) as eCliente_Contactos;
                    eCliente_Contactos eContact = new eCliente_Contactos();
                    eContact = unit.Clientes.ObtenerClienteContacto<eCliente_Contactos>(8, cod_cliente, cod_contacto: obj.cod_contacto);

                    txtCodClienteContacto.Text = eContact.cod_contacto.ToString();
                    txtNombreClienteContacto.Text = eContact.dsc_nombre;
                    txtApellidoClienteContacto.Text = eContact.dsc_apellidos;
                    //dtFecNacClienteContacto.EditValue = eContact.fch_nacimiento;
                    if (eContact.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacClienteContacto.EditValue = null; } else { dtFecNacClienteContacto.EditValue = Convert.ToDateTime(eContact.fch_nacimiento); }
                    txtCorreoClienteContacto.Text = eContact.dsc_correo;
                    txtFono1ClienteContacto.Text = eContact.dsc_telefono1;
                    txtFono2ClienteContacto.Text = eContact.dsc_telefono2;
                    txtUsuarioWebClienteContacto.Text = eContact.cod_usuario_web;
                    txtClaveWebClienteContacto.Text = eContact.cod_clave_web;
                    mmObservacionClienteContacto.Text = eContact.dsc_observaciones;
                    txtCargoClienteContacto.Text = eContact.dsc_cargo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaClienteContactos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                gvListaClienteContactos_FocusedRowChanged(gvListaClienteContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
            }
        }

        private void rbtnEliminarDireccionContacto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaDireccionContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_Ubicacion eUbic = new eCliente_Ubicacion();
                    eUbic = unit.Clientes.ValidacionEliminar<eCliente_Ubicacion>(28, cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    if (eUbic != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a una ubicación.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = unit.Clientes.Eliminar_ClienteDireccionContacto(cod_cliente, eContact.num_linea, eContact.cod_contacto);
                    ObtenerListadoDireccionesContactos();
                    if (ListDireccionContacto.Count == 0) LimpiarCamposDireccionContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtnEliminarClienteContacto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto?", "Eliminar contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente_Contactos eContact = gvListaClienteContactos.GetFocusedRow() as eCliente_Contactos;
                    eCliente_CentroResponsabilidad eCentroResp = new eCliente_CentroResponsabilidad();
                    eCentroResp = unit.Clientes.ValidacionEliminar<eCliente_CentroResponsabilidad>(27, cod_cliente, cod_contacto: eContact.cod_contacto);
                    if (eCentroResp != null) { MessageBox.Show("No se puede eliminar el contacto ya que está vinculado a un centro de responsabilidad.", "Eliminar contacto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    string result = unit.Clientes.Eliminar_ClienteContacto(cod_cliente, eContact.cod_contacto);
                    ObtenerListadoClientesContactos();
                    if (ListClienteContacto.Count == 0) LimpiarCamposClienteContacto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picBuscarVendedor_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmBusquedaVendedor frm = new frmBusquedaVendedor();
            //    frm.ShowDialog();
            //    if (frm.codigo == "") { return; }
            //    txtCodVendedor.Text = frm.codigo;
            //    txtVendedor.Text = frm.descripcion;
            //    if (frm.descripcion == "") { txtCodVendedor.Text = ""; txtVendedor.Text = ""; }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void dtFecNacimiento_EditValueChanged(object sender, EventArgs e)
        {


            //if (dtFecNacimiento.EditValue != null) {
            //int Anho = DateTime.Now.Year- Convert.ToDateTime(dtFecNacimiento.EditValue).Year;

            //if (DateTime.Today < Convert.ToDateTime(dtFecNacimiento.EditValue).AddYears(Anho))
            //{
            //    txtEdad.Text = (Anho - 1).ToString();
            //}
            //else {
            //    txtEdad.Text = (Anho).ToString();
            //}
            //}
        }


        private void lkpPais_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            lkpDepartamento.Properties.DataSource = null;

            unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "", cod_condicion: lkpPais.EditValue != null ? lkpPais.EditValue.ToString() : "");

        }
        private void lkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "", cod_condicion: lkpDepartamento.EditValue != null ? lkpDepartamento.EditValue.ToString() : "");

        }

        private void lkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpProvincia.EditValue != null)
            {
                glkpDistrito.Properties.DataSource = null;
                unit.Clientes.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue != null ? lkpProvincia.EditValue.ToString() : "");

                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    dsc_provincia = ", " + dataRow["dsc_provincia"].ToString();

                }

            }
        }



        private void glkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            //grdbNatJuridi.SelectedIndex = 1 
            //chkFlgJuridica.Checked = glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" ? true : false;
            if (glkpTipoDocumento.EditValue == null) { return; }
            grdbNatJuridi.SelectedIndex = glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" ? 1 : 0;

            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                txtNroDocumento.Properties.MaskSettings.Configure<MaskSettings.Simple>(settings =>
                {
                    settings.MaskExpression = "99999999999";
                    //settings.AutoHideDecimalSeparator = false;
                    //settings.HideInsignificantZeros = true;
                });
                glkpTipoDireccion.EditValue = "TD022";
                btnConsultarSunat.Enabled = true;
                btnConsultarDNIreniec.Enabled = false;
            }

            if (glkpTipoDocumento.EditValue.ToString() == "DI001")
            {
                txtNroDocumento.Properties.MaskSettings.Configure<MaskSettings.Simple>(settings =>
                {
                    settings.MaskExpression = "99999999";
                    //settings.AutoHideDecimalSeparator = false;
                    //settings.HideInsignificantZeros = true;
                });
                glkpTipoDireccion.EditValue = "TD021";
                btnConsultarSunat.Enabled = false;
                btnConsultarDNIreniec.Enabled = true;
            }

            if (glkpTipoDocumento.EditValue.ToString() != "DI001" && glkpTipoDocumento.EditValue.ToString() != "DI004")
            {
                btnConsultarSunat.Enabled = false;
                btnConsultarDNIreniec.Enabled = false;
                txtNroDocumento.Properties.ResetMaskSettings();

                glkpTipoDireccion.EditValue = "TD021";
            }
            txtNroDocumento.Focus();

        }

        private void btnUbicacionMasiva_Click(object sender, EventArgs e)
        {
            if (lkpNivelUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel.", "Carga ubicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (lkpNivelSuperiorUbicacion.EditValue == null) { MessageBox.Show("Debe seleccionar un nivel superior.", "Carga ubicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            eCliente_Direccion eDirec = gvListaDirecciones.GetFocusedRow() as eCliente_Direccion;
            frmCargaMasivaUbicaciones frm = new frmCargaMasivaUbicaciones();
            frm.cod_cliente = cod_cliente;
            frm.num_linea = eDirec.num_linea;
            frm.cod_nivel = lkpNivelUbicacion.EditValue.ToString();
            frm.cod_ubicacion_sup = lkpNivelSuperiorUbicacion.EditValue.ToString();
            frm.dsc_larga_ubicacion = lkpNivelSuperiorUbicacion.Text;
            frm.cod_contacto = lkpResponsableUbicacion.EditValue == null ? null : lkpResponsableUbicacion.EditValue.ToString();
            frm.ShowDialog();
            if (frm.ActualizarListado == "SI") ObtenerListadoUbicaciones();
        }

        private void btnExportarUbicaciones_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\DireccionUbicacion" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                tlUbicacionesDireccion.ExportToXlsx(archivo);
                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaDirecciones_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaDirecciones_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaDireccionContactos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaClienteContactos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvEmpresasVinculadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
            if (e.RowHandle >= 0)
            {
                GridView vw = sender as GridView;
                bool estado = Convert.ToBoolean(vw.GetRowCellValue(e.RowHandle, vw.Columns["Seleccionado"]));
                if (estado) e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void gvEmpresasVinculadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        //private void gvEmpresasVinculadas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    try
        //    {
        //        //if (e.RowHandle < 0) return;
        //        //eCliente_Empresas eCliEmp = new eCliente_Empresas();
        //        //eCliente_Empresas obj = gvProyectosVinculadas.GetRow(e.RowHandle) as eCliente_Empresas;

        //        //if ((e.Column.FieldName == "valorRating" || e.Column.FieldName == "dsc_pref_ceco") && !obj.Seleccionado) { MessageBox.Show("Debe vincular la empresa para poder calificarla.", "Calificar empresa", MessageBoxButtons.OK, MessageBoxIcon.Information); obj.valorRating = 0; return; }
        //        //if (e.Column.FieldName == "Seleccionado" || e.Column.FieldName == "valorRating" || e.Column.FieldName == "dsc_pref_ceco")
        //        //{
        //        //    obj.cod_cliente = cod_cliente; obj.flg_activo = obj.Seleccionado ? "SI" : "NO"; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //        //    eCliEmp = unit.Clientes.Guardar_Actualizar_ClienteEmpresas<eCliente_Empresas>(obj);
        //        //    if (eCliEmp == null) { MessageBox.Show("Error al vincular empresa", "Vincular empresa", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void rchkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            gvProyectosVinculadas.PostEditor();
        }

        private void rRatingCalificacion_EditValueChanged(object sender, EventArgs e)
        {
            gvProyectosVinculadas.PostEditor();
        }

        private async void consultarSunat(int opcion = 0)
        {
            try
            {

                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" && validarCantidadNumerosCadena(txtNroDocumento.Text) != 11 && opcion == 0)
                {
                    MessageBox.Show("El RUC debe tener 11 digitos", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }

                if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI001" && validarCantidadNumerosCadena(txtNroDocumento.Text) != 8 && opcion == 0)
                {
                    MessageBox.Show("El DNI debe tener 8 digitos", "Validación DNI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumento.Select();
                    return;
                }

                if (glkpTipoDocumentoConyuge.EditValue != null && glkpTipoDocumentoConyuge.EditValue.ToString() == "DI001" && validarCantidadNumerosCadena(txtNroDocumentoConyuge.Text) != 8 && opcion == 1)
                {
                    MessageBox.Show("El DNI del cónyugue debe tener 8 digitos", "Validación DNI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNroDocumentoConyuge.Select();
                    return;
                }

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Consultando en SUNAT", "Cargando...");
                
                await ConsultaSUNAT5(opcion, opcion == 0 ? txtNroDocumento.Text.Trim() : txtNroDocumentoConyuge.Text.Trim());

                SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("DOCUMENTO INGRESADO INVALIDO: \n" + ex.ToString(), "NO SE ENCONTRÓ EL DNI EN LA SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private  void btnConsultarSunat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            consultarSunat();
        }

        private async void ConsultaSUNAT4(string nDocumento)
        {
            eSistema objLink = unit.Version.ObtenerVersion<eSistema>(7); //Link Descarga
            eSistema objRUC = unit.Version.ObtenerVersion<eSistema>(8); //Token RUC
            eSistema objDNI = unit.Version.ObtenerVersion<eSistema>(9); //Token DNI
            var client = new RestClient(objLink.dsc_valor);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            EnvioJSONRUC datosRUC = new EnvioJSONRUC()
            {
                token = objRUC.dsc_valor,
                ruc = nDocumento
            };
            EnvioJSONDNI datosDNI = new EnvioJSONDNI()
            {
                token = objDNI.dsc_valor,
                dni = nDocumento
            };

            var eJSON = "";
            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                eJSON = JsonConvert.SerializeObject(datosRUC);
            }
            else
            {
                eJSON = JsonConvert.SerializeObject(datosDNI);
            }
            request.AddHeader("Authorization", "Basic token");
            request.AddParameter("application/json", eJSON, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic blogObject = js.Deserialize<dynamic>(response.Content);

            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                txtRazonSocial.Text = blogObject["nombre_o_razon_social"];
                txtNombreComercial.Text = blogObject["nombre_o_razon_social"];
                mmDireccion.Text = blogObject["direccion_completa"];

                if (mmDireccion.Text.Trim() != "")
                {
                    glkpTipoDireccion.EditValue = "TD001";

                    List<eCiudades> listDepartamento = new List<eCiudades>();
                    List<eCiudades> listProvincia = new List<eCiudades>();
                    List<eCiudades> listDistrito = new List<eCiudades>();
                    lkpPais.EditValue = "00001";
                    listDepartamento = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(14, cod_condicion: lkpPais.EditValue.ToString());
                    eCiudades objDepart = listDepartamento.Find(x => x.dsc_departamento.ToUpper() == blogObject["departamento"].ToUpper());
                    if (objDepart != null)
                    {
                        lkpDepartamento.EditValue = objDepart.cod_departamento;
                        listProvincia = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: objDepart.cod_departamento);
                        eCiudades objProv = listProvincia.Find(x => x.dsc_provincia.ToUpper() == blogObject["provincia"].ToUpper());
                        if (objProv != null)
                        {
                            lkpProvincia.EditValue = objProv.cod_provincia;
                            listDistrito = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: objProv.cod_provincia);
                            eCiudades objDist = listDistrito.Find(x => x.dsc_distrito.ToUpper() == blogObject["distrito"].ToUpper());
                            if (objDist != null) glkpDistrito.EditValue = objDist.cod_distrito;
                        }
                    }
                }
            }
            else
            {
                string nombre_completo = blogObject["nombre_completo"];
                string[] nombres = nombre_completo.Split(' ');
                txtApellPaterno.Text = nombres[0];
                txtApellMaterno.Text = nombres[1];
                txtNombre.Text = nombres.Length > 3 ? nombres[2] + ' ' + nombres[3] : nombres[2];
            }
        }

        private async Task ConsultaSUNAT5(int opcion = 0, string nDocumento = "")
        {
            string endpoint = @"https://api.apis.net.pe/v1/ruc?numero=" + nDocumento;
            if (opcion == 0)
            {
                if (glkpTipoDocumento.EditValue.ToString() == "DI004")
                {
                    endpoint = @"https://api.apis.net.pe/v1/ruc?numero=" + nDocumento;
                }
                else
                {
                    endpoint = @"https://api.apis.net.pe/v1/dni?numero=" + nDocumento;
                }
            }
            else
            {
                endpoint = @"https://api.apis.net.pe/v1/dni?numero=" + nDocumento;
            }
           
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            myWebRequest.CookieContainer = cokkie;

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myhttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);

            string xDat = "";
            string validar = "";

            while (!myStreamReader.EndOfStream)
            {
                xDat = myStreamReader.ReadLine();
                if (xDat != "")
                {
                    string Datos = xDat;
                    char[] separadores = { ',' };
                    string[] palabras = Datos.Replace("\"", "").Replace("{", "").Replace("}", "").Split(separadores);
                    if (opcion == 0)
                    {
                        if (glkpTipoDocumento.EditValue.ToString() == "DI004")
                        {
                            var empresa = JsonConvert.DeserializeObject<eJsonEmpresa>(Datos);

                            txtRazonSocial.Text = empresa.Nombre.ToUpper();
                            txtNombreComercial.Text = empresa.Nombre.ToUpper();
                            mmDireccion.Text = empresa.direccion.ToUpper();

                            if (mmDireccion.Text.Trim() != "")
                            {
                                glkpTipoDireccion.EditValue = "TD0022";
                                List<eCiudades> listDepartamento = new List<eCiudades>();
                                List<eCiudades> listProvincia = new List<eCiudades>();
                                List<eCiudades> listDistrito = new List<eCiudades>();
                                lkpPais.EditValue = "00001";
                                listDepartamento = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(14, cod_condicion: lkpPais.EditValue.ToString());
                                eCiudades objDepart = listDepartamento.Find(x => x.dsc_departamento.ToUpper() == empresa.departamento.ToUpper());
                                if (objDepart != null)
                                {
                                    lkpDepartamento.EditValue = objDepart.cod_departamento;
                                    listProvincia = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: objDepart.cod_departamento);
                                    eCiudades objProv = listProvincia.Find(x => x.dsc_provincia.ToUpper() == empresa.provincia.ToUpper());
                                    if (objProv != null)
                                    {
                                        lkpProvincia.EditValue = objProv.cod_provincia;
                                        listDistrito = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: objProv.cod_provincia);
                                        eCiudades objDist = listDistrito.Find(x => x.dsc_distrito.ToUpper() == empresa.distrito.ToUpper());
                                        if (objDist != null) glkpDistrito.EditValue = objDist.cod_distrito;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var persona = JsonConvert.DeserializeObject<eJsonPersona>(Datos);
                            txtApellPaterno.Text = persona.apellidoPaterno.ToUpper();
                            txtApellMaterno.Text = persona.apellidoMaterno.ToUpper();
                            txtNombre.Text = persona.nombres.ToUpper();
                        }
                    }
                    else
                    {
                        var persona = JsonConvert.DeserializeObject<eJsonPersona>(Datos);
                        txtApellPaternoConyuge.Text = persona.apellidoPaterno.ToUpper();
                        txtApellMaternoConyuge.Text = persona.apellidoMaterno.ToUpper();
                        txtNombreConyuge.Text = persona.nombres.ToUpper();
                    }
                    

                    validar = "OK";
                }
            }//TERMINA EL WHILE

            if (validar == "OK")
            {
                xtraTabControl1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error al traer datos del proveedor.", "Traer datos SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public class EnvioJSONRUC
        {
            public string token { get; set; }
            public string ruc { get; set; }
        }
        public class EnvioJSONDNI
        {
            public string token { get; set; }
            public string dni { get; set; }
        }

        private void ConsultaSUNAT()
        {
            ObtenerCap();
            string endpoint = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + txtNroDocumento.Text.Trim() + "&codigo= " + texto.ToUpper() + "&tipdoc=1";

            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            myWebRequest.CookieContainer = cokkie;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myhttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);

            string xDat = "";
            int pos = 0;
            int pocision = 0;
            string dato = "";
            int posicionPersonaNatural = 0;
            string validar = "";

            while (!myStreamReader.EndOfStream)
            {
                xDat = myStreamReader.ReadLine();
                pos += 1;
                if (xDat.ToString() == "            <td width=\"18%\" colspan=1  class=\"bgn\">N&uacute;mero de RUC: </td>")
                {
                    dato = "Razon Social";
                    pocision = pos + 1;
                    validar = "OK";
                }
                else if (xDat.ToString() == "            <td class=\"bgn\" colspan=1>Tipo Contribuyente: </td>")
                {
                    dato = "Persona Natural";
                    pocision = pos + 1;
                }

                else if (xDat.ToString() == "            <td class=\"bgn\" colspan=1>Tipo de Documento: </td>")
                {
                    dato = "Tipo Documento";
                    pocision = pos + 1;
                }
                else if (xDat.ToString() == "\t              <td class=\"bgn\"colspan=1>Condici&oacute;n del Contribuyente:</td>")
                {
                    dato = "Condicion";   //habido y no habido
                    pocision = pos + 3;
                }
                else if (xDat.ToString() == "              <td class=\"bgn\" colspan=1 >Nombre Comercial: </td>")
                {
                    dato = "Nombre Comercial";
                    pocision = pos + 1;
                }
                else if (xDat.ToString() == "              <td class=\"bgn\" colspan=1>Direcci&oacute;n del Domicilio Fiscal:</td>")
                {
                    dato = "Direccion";
                    pocision = pos + 1;
                }

                if (posicionPersonaNatural == pos)
                {
                    string NombresaApellidos = xDat.Substring(16);
                    char[] separadores = { ' ', ',' };
                    string[] palabras1 = NombresaApellidos.Split(separadores);
                    string ApellidosPat = palabras1[0];
                    string ApellidosMat = palabras1[1];
                    string Nombres = NombresaApellidos.Replace(ApellidosPat + " " + ApellidosMat + ", ", "");

                    txtNombre.Text = Nombres;
                    txtApellPaterno.Text = ApellidosPat;
                    txtApellMaterno.Text = ApellidosMat;
                    glkpTipoDocumento.EditValue = "DI001";

                    txtRazonSocial.Text = "";
                    posicionPersonaNatural = 0;
                }

                if (pocision == pos)
                {
                    if (dato == "Razon Social")
                    {
                        string razon = getDatafromXML(xDat, 25);
                        txtRazonSocial.Text = razon.Substring(15);
                        glkpTipoDocumento.EditValue = "DI004";
                    }
                    else if (dato == "Nombre Comercial")
                    {
                        txtNombreComercial.Text = getDatafromXML(xDat, 25);
                        if (txtNombreComercial.Text == "-")
                        {
                            txtNombreComercial.Text = txtRazonSocial.Text;
                        }
                    }
                    else if (dato == "Condicion")
                    {
                        string estado = getDatafromXML(xDat, 0).ToLower();
                        //chkNoHabido.CheckState = estado == "habido" ? CheckState.Unchecked : CheckState.Checked;
                    }

                    else if (dato == "Persona Natural")
                    {
                        string personanatural = getDatafromXML(xDat, 25);
                        if (personanatural.Contains("PERSONA NATURAL"))
                        {
                            grdbNatJuridi.SelectedIndex = 0;
                            //chkFlgJuridica.CheckState = CheckState.Unchecked;
                        }
                        else
                        {
                            txtNombre.Text = "";
                            txtApellPaterno.Text = "";
                            txtApellMaterno.Text = "";
                        }

                    }
                    else if (dato == "Tipo Documento")
                    {
                        string personanatural = getDatafromXML(xDat, 25).ToString();
                        char[] separadores = { ' ' };
                        string[] palabras = personanatural.Split(separadores);

                        string TipoDocumento = palabras[0];
                        string NumeroDocumento = palabras[1];

                        txtNroDocumento.Text = NumeroDocumento;
                        glkpTipoDocumento.Text = TipoDocumento;
                        posicionPersonaNatural = pos + 2;
                    }

                    else if (dato == "Direccion")
                    {
                        string direccion = getDatafromXML(xDat, 25);
                        mmDireccion.Text = direccion;

                        if (direccion != "-")
                        {
                            ObtenerUbigeo();
                            List<eCiudades> listDepartamento = new List<eCiudades>();
                            List<eCiudades> listProvincia = new List<eCiudades>();
                            List<eCiudades> listDistrito = new List<eCiudades>();
                            lkpPais.EditValue = "00001";
                            listDepartamento = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(14, cod_condicion: lkpPais.EditValue.ToString());
                            eCiudades objDepart = listDepartamento.Find(x => x.dsc_departamento.ToUpper() == SuDepartamento.ToUpper());
                            if (objDepart != null)
                            {
                                lkpDepartamento.EditValue = objDepart.cod_departamento;
                                listProvincia = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(15, cod_condicion: objDepart.cod_departamento);
                                eCiudades objProv = listProvincia.Find(x => x.dsc_provincia.ToUpper() == SuProvincia.ToUpper());
                                if (objProv != null)
                                {
                                    lkpProvincia.EditValue = objProv.cod_provincia;
                                    listDistrito = unit.Clientes.ListarOpcionesVariasCliente<eCiudades>(16, cod_condicion: objProv.cod_provincia);
                                    eCiudades objDist = listDistrito.Find(x => x.dsc_distrito.ToUpper() == SUDistrito.ToUpper());
                                    if (objDist != null) glkpDistrito.EditValue = objDist.cod_distrito;
                                }
                            }
                        }
                        pocision = 0;
                        dato = "";
                    }
                }
            }//TERMINA EL WHILE

            if (validar == "OK")
            {
            }
            else
            {
                MessageBox.Show("El RUC " + txtNroDocumento.Text + "  no existe", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        string captcha = "";
        CookieContainer cokkie = new CookieContainer();
        string[] nrosRuc = new string[] { };
        string texto = "";
        public void ObtenerCap()
        {

            try
            {
                ///////https://cors-anywhere.herokuapp.com/wmtechnology.org/Consultar-RUC/?modo=1&btnBuscar=Buscar&nruc=
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                request.CookieContainer = cokkie;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                var image = new Bitmap(responseStream);
                //pictureLogo.EditValue = image;
                string ruta = "C:\\IMPERIUM-Software\\tessdata";
                var ocr = new TesseractEngine(ruta, "eng", EngineMode.Default);
                ocr.DefaultPageSegMode = PageSegMode.SingleBlock;
                Tesseract.Page p = ocr.Process(image);
                texto = p.GetText().Trim().ToUpper().Replace(" ", "");
            }
            catch (Exception ex)
            {
                //mensaje de error
            }
        }

        public string getDatafromXML(string lineRead, int len = 0)
        {

            try
            {
                lineRead = lineRead.Trim();
                lineRead = lineRead.Remove(0, len);
                lineRead = lineRead.Replace("</td>", "");
                while (lineRead.Contains("  "))
                {
                    lineRead = lineRead.Replace("  ", " ");
                }
                return lineRead;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        string SUDistrito;
        string SuProvincia;
        string SuDepartamento;
        public void ObtenerUbigeo()
        {
            string direccion = mmDireccion.Text;
            int index = 0;
            for (int i = 0; i < direccion.Length; i++)
            {
                if (direccion[i].ToString() == "-")
                {
                    index = i + 2;
                }
            }
            SUDistrito = direccion.Substring(index).ToLower();
            direccion = direccion.Replace(" - " + SUDistrito.ToUpper(), "");
            index = 0;

            for (int i = 0; i < direccion.Length; i++)
            {
                if (direccion[i].ToString() == "-")
                {
                    index = i + 2;
                }
            }
            SuProvincia = direccion.Substring(index).ToLower();
            direccion = direccion.Replace(" - " + SuProvincia.ToUpper(), "");
            index = 0;

            for (int i = 0; i < direccion.Length; i++)
            {
                if (direccion[i].ToString() == "-")
                {
                    index = i + 3;
                }
            }
            SuDepartamento = direccion.Substring(index).ToLower();
            if (index == 0) SuDepartamento = SuProvincia;
            direccion = direccion.Replace(" - " + SuDepartamento.ToUpper(), "");
            index = 0;
        }
        private void gvEmpresasVinculadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            bool estado = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["Seleccionado"]));
            if (estado) e.Appearance.ForeColor = Color.Blue;
        }

        private void txtNroDocumento_Leave(object sender, EventArgs e)
        {
            try
            {
                int validar = validarCantidadNumerosCadena(txtNroDocumento.Text.Trim());
                //MessageBox.Show("numeros : " + validar);
                if (txtNroDocumento.Text.Trim() == "") return;
                //if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI004" && validar != 11 && validar > 0)
                //{
                //    MessageBox.Show("El RUC debe tener 11 digitos", "Validación RUC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtNroDocumento.Select();
                //    return;
                //}
                //if (glkpTipoDocumento.EditValue != null && glkpTipoDocumento.EditValue.ToString() == "DI001" && validar != 8 && validar > 0)
                //{
                //    MessageBox.Show("El DNI debe tener 8 digitos", "Validación DNI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtNroDocumento.Select();
                //    return;
                //}
                if (MiAccion == Cliente.Nuevo)
                {
                    if (txtNroDocumento.Text.Trim() == "") return;
                    eCliente obj = new eCliente();
                    obj = unit.Clientes.Validar_NroDocumento<eCliente>(37, txtNroDocumento.Text.Trim());
                    if (obj != null)
                    {
                        if (MessageBox.Show("El número de documento ingresado ya se encuentra registrado en el sistema." + Environment.NewLine +
                                        "¿Desea visualizar la información del cliente?", //y vincularlo a su empresa?", 
                                        "Validar número de documento", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            cod_cliente = obj.cod_cliente;
                            MiAccion = Cliente.Editar;
                            Editar();
                            BloqueoControles(true, false, true);
                            xtabDireccionCliente.PageEnabled = true;
                            xtabDetalleDireccion.Enabled = true;
                            xtabContactoCliente.PageEnabled = true;
                            xtabContactosDireccion.PageEnabled = true;
                            xtabDireccionConyuge.PageEnabled = true;
                            xtabUbicacionesDireccion.PageEnabled = true;
                            xtabCentroResponsabilidad.PageEnabled = true;
                            xtabProyectosVinculados.PageEnabled = true;
                        }
                        //txtNroDocumento.Select();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void lkpEstadoCivil_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpEstadoCivil.EditValue != null)
            {
                if (lkpEstadoCivil.EditValue.ToString() == "02")
                {
                    chkBienesSepCliente.Enabled = true;
                    chkBienesSepCliente.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    chkBienesSepCliente.Enabled = false;
                    chkBienesSepCliente.ForeColor = System.Drawing.Color.Black;
                    chkBienesSepCliente.CheckState = CheckState.Unchecked;
                }

                if (lkpEstadoCivil.EditValue.ToString() == "02" || lkpEstadoCivil.EditValue.ToString() == "04")
                {
                    layoutdoc.Enabled = true;
                    layoutnumdoc.Enabled = true;
                    layoutpat.Enabled = true;
                    layoutmat.Enabled = true;
                    layoutNom.Enabled = true;
                    layoutNac.Enabled = true;
                    layoutDirec.Enabled = true;
                    layoutemail.Enabled = true;
                    layoutesci.Enabled = true;
                    layoutocupa.Enabled = true;
                    layoutTel.Enabled = true;
                    glkpTipoDocumentoConyuge.EditValue = "DI001";
                    txtNroDocumentoConyuge.Focus();
                    lkpEstadoCivilConyuge.EditValue = lkpEstadoCivil.EditValue;

                    xtabDireccionConyuge.PageEnabled = true;
                }
                else
                {
                    layoutdoc.Enabled = false;
                    layoutnumdoc.Enabled = false;
                    layoutpat.Enabled = false;
                    layoutmat.Enabled = false;
                    layoutNom.Enabled = false;
                    layoutNac.Enabled = false;
                    layoutDirec.Enabled = false;
                    layoutemail.Enabled = false;
                    layoutesci.Enabled = false;
                    layoutocupa.Enabled = false;
                    layoutTel.Enabled = false;

                    glkpTipoDocumentoConyuge.EditValue = "DI001";
                    txtApellPaternoConyuge.Text = "";
                    txtNroDocumentoConyuge.Text = "";
                    txtApellMaternoConyuge.Text = "";
                    txtNombreConyuge.Text = "";
                    dtFecNacimientoConyuge.EditValue = DateTime.Today;
                    txtNroDocumento.Focus();

                    xtabDireccionConyuge.PageEnabled = false;
                }

            }
        }

        private void gvObservacionesCliente_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }



        private void gvObservacionesCliente_CustomDrawColumnHeader_1(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvObservacionesCliente_InitNewRow_1(object sender, InitNewRowEventArgs e)
        {
            //foreach (eProyecto obje in ListProyecto)
            //{
            eCliente.eCliente_Observaciones obj = gvObservacionesCliente.GetFocusedRow() as eCliente.eCliente_Observaciones;

            //bsClienteObservaciones.DataSource = ListProyecto;
            //obj.dsc_nombre = obje.dsc_nombre;
            //obj.dsc_proyectos_vinculadas = 
            obj.fch_registro = DateTime.Today; obj.dsc_usuario_registro = Program.Sesion.Usuario.dsc_usuario;
            //colcod_proyecto.OptionsColumn.AllowEdit = true;
            gvObservacionesCliente.RefreshData();


            //}

        }

        public void CargarComboEnGrid()
        {
            try
            {
                lstProyecto = unit.Proyectos.CombosEnGridControl<eProyecto>("Proyecto");
                rlkpProyecto.DataSource = lstProyecto;
                //lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLote");
                //if (opcion == 1)
                //{
                //    rlkpTipoLote.DataSource = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLotes");
                //    colcod_tipo_lote.OptionsColumn.AllowEdit = false;
                //}
                //else
                //{
                //    lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLoteEtapa", cod_etapasmultiple);
                //    rlkpTipoLote.DataSource = lstTipoLote;
                //    colcod_tipo_lote.OptionsColumn.AllowEdit = true;
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvObservacionesCliente_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }



        private void gvProyectosVinculadas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                int nRow = e.RowHandle;
                eCliente_Empresas eCliEmp = new eCliente_Empresas();
                eCliente eCliPro = new eCliente();
                eCliente ePro = new eCliente();
                eProyecto obj = gvProyectosVinculadas.GetRow(e.RowHandle) as eProyecto;
                if (e.Column.FieldName == "Seleccionado")
                {
                    eProyecto eVaGe = gvProyectosVinculadas.GetFocusedRow() as eProyecto;

                    //string validateResult = CargarConfigLotesTipoLotes("ANULAR TIPO LOTE", obj);
                    //if (validateResult != null)
                    //{
                    //MessageBox.Show(validateResult, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //gvTipoLote.RefreshData();
                    //CargarListadoLote("TODOS", "");
                    //obtenerListadoTipoLoteXEtapa();
                    //return;
                    //}

                    ePro.cod_cliente = cod_cliente;
                    ePro.cod_empresa = obj.cod_empresa;
                    if (obj.cod_empresa != "" || obj.cod_empresa != null) { cod_empresa = obj.cod_empresa; }

                    ePro.cod_proyecto = obj.cod_proyecto;
                    ePro.flg_activo = obj.Seleccionado ? "SI" : "NO";
                    ePro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eCliPro = unit.Proyectos.Guardar_Actualizar_ClienteProyecto<eCliente>(ePro);

                    if (eCliPro == null)
                    { MessageBox.Show("Error al vincular el proyecto", "Vincular proyecto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    eCliEmp.cod_cliente = cod_cliente;
                    eCliEmp.cod_empresa = obj.cod_empresa;
                    eCliEmp.cod_proyecto = obj.cod_proyecto;
                    eCliEmp.flg_activo = obj.Seleccionado ? "SI" : "NO";
                    eCliEmp.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    eCliEmp.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eCliEmp.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    eCliEmp = unit.Clientes.Guardar_Actualizar_ClienteEmpresas<eCliente_Empresas>(eCliEmp);

                    ActualizarListado = "SI";
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rlkpProyecto_EditValueChanged(object sender, EventArgs e)
        {
            //eCliente.eCliente_Observaciones obj = gvObservacionesCliente.GetFocusedRow() as eCliente.eCliente_Observaciones;
            //eProyecto objPro = lstProyecto.Find(x => x.cod_proyecto == obj.cod_proyecto);
            //if (objPro != null) { obj.dsc_proyectos_vinculadas = objPro.dsc_nombre; }
            ////bsClienteObservaciones.DataSource = ListProyecto;
            ////obj.dsc_nombre = obje.dsc_nombre;
            ////obj.dsc_proyectos_vinculadas = 
            ////obj.fch_registro = DateTime.Today; obj.dsc_usuario_registro = Program.Sesion.Usuario.dsc_usuario;
            //gvObservacionesCliente.RefreshData();
        }

        private void gvEventos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvEventos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvObservacionesCliente_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                eCliente.eCliente_Observaciones obj = gvObservacionesCliente.GetFocusedRow() as eCliente.eCliente_Observaciones;
                if (obj == null) return;
                if (gvObservacionesCliente.FocusedColumn.FieldName == "cod_proyecto")
                {
                    e.Cancel = obj.num_linea >= 1 ? true : false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscarSeparacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAsignarSeparacion frm = new frmAsignarSeparacion(this, null);





            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;
            //frm.validarFormulario = 1;
            frm.ShowDialog();
        }

        private void rbtnEliminarObs_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                eCliente.eCliente_Observaciones obj = gvObservacionesCliente.GetFocusedRow() as eCliente.eCliente_Observaciones;
                if (obj == null) return;
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta observación?", "Eliminar observación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCliente.eCliente_Observaciones eObs = gvObservacionesCliente.GetFocusedRow() as eCliente.eCliente_Observaciones;
                    string result = unit.Clientes.Eliminar_ClienteObservaciones(cod_cliente, eObs.num_linea);
                    ObtenerDatos_ObservacionesCliente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarReniec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private async void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            consultarSunat(1);
        }

        private void gvDocumentos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDocumentos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        public void CargarListadoDocumentosClientes(string accion)
        {
            try
            {
                int nRow = gvDocumentos.FocusedRowHandle;

                mylistDocumentos = unit.Clientes.ListarDocumentoCliente<eCliente.eCliente_Documentos>(accion, cod_cliente);
                eClienteDocumentos.DataSource = mylistDocumentos;
                gvDocumentos.FocusedRowHandle = nRow;
                //gvDocumentos_FocusedRowChanged(gvDocumentos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(nRow - 1, nRow));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtnNuevo_Click(object sender, EventArgs e)
        {
            AgregarDocCliente();
        }

        private void gvDocumentos_Click(object sender, EventArgs e)
        {
            //eCliente.eCliente_Documentos oListMemoDesc = mylistDocumentos.Find(x => x.cod_documento_cliente == "0" && x.dsc_nombre_doc == "" || x.dsc_nombre_doc == null);
            //if (oListMemoDesc != null)
            //{
            //    mylistDocumentos.Remove(oListMemoDesc);
            //    gvDocumentos.RefreshData();
            //}
        }

        private void gvDocumentos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eCliente.eCliente_Documentos eDocumentos = gvDocumentos.GetRow(e.FocusedRowHandle) as eCliente.eCliente_Documentos;
                    if (eDocumentos == null) { return; }

                    if (eDocumentos.cod_documento_cliente == cod_documento_cliente)
                    {
                        coldsc_nombre_doc.OptionsColumn.AllowEdit = true;
                        //colnum_orden_doc.OptionsColumn.AllowEdit = true;

                    }
                    else
                    {
                        coldsc_nombre_doc.OptionsColumn.AllowEdit = false;
                        //colnum_orden_doc.OptionsColumn.AllowEdit = false;

                        cod_documento_cliente = "";
                    }


                    gvDocumentos.RefreshData();

                }
                //else
                //{
                //    rtxtDescripcionMemoriaDescriptiva.WordMLText = "";
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void rbtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                eCliente.eCliente_Documentos oListSepDoc = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;
                if (oListSepDoc.cod_documento_cliente == "0")
                {
                    mylistDocumentos.Remove(oListSepDoc);
                    gvDocumentos.RefreshData();
                }
                else
                {
                    //string tipdoc = validarTipoDocExistente();
                    //if (tipdoc == null)
                    //{
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este documento? \nEsta acción es irreversible.", "Eliminar tipo documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        if (oListSepDoc.flg_PDF == "SI")
                        {
                            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Eliminando documento...", "Cargando...");
                            await Mover_Eliminar_ArchivoOneDrive();
                            SplashScreenManager.CloseForm(false);
                        }
                        string result = unit.Proyectos.Eliminar_TipoDocumento("2", cod_documento_referencia: oListSepDoc.cod_documento_cliente, cod_cliente, "", oListSepDoc.flg_PDF);
                        if (result == null)
                        {
                            MessageBox.Show("No se pudo eliminar el tipo documento \"" + oListSepDoc.dsc_nombre_doc + "\"", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        gvDocumentos.RefreshData();
                        CargarListadoDocumentosClientes("3");
                        //obtenerListadoTipoDocumentoXCliente();
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show(tipdoc, "Eliminar tipo de documento", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //}

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string validarTipoDocExistente()
        {
            try
            {
                eCliente.eCliente_Documentos obj = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;

                mylistvalidar = unit.Clientes.ListarDocumentoCliente<eCliente.eCliente_Documentos>("2", cod_cliente: cod_cliente, cod_documento_cliente: obj.cod_documento_cliente);
                if (mylistvalidar.Count() > 0)
                {
                    return "Error al eliminar el tipo de documento \"" + mylistvalidar[0].dsc_nombre_doc + "\".\nSe encuentra registrado en el cliente \"" + mylistvalidar[0].dsc_cliente + "\" con N° Documento \"" + mylistvalidar[0].dsc_documento + "\".";
                }
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private void gvDocumentos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                int nRow = e.RowHandle;
                eCliente.eCliente_Documentos objDoc = new eCliente.eCliente_Documentos();
                eCliente.eCliente_Documentos obj = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;
                if (obj == null || obj.cod_documento_cliente == "0") { return; }

                if (e.Column.FieldName == "dsc_nombre_doc_ref" && e.Value != null)
                {
                    coldsc_nombre_doc.OptionsColumn.AllowEdit = false;
                    //colnum_orden_doc.OptionsColumn.AllowEdit = false;
                    //cod_documento_cliente = "";
                    //if (obj.dsc_Nombre == "" || obj.dsc_Nombre == null)
                    //{
                    //    MessageBox.Show("Descripción tipo de lote inválido", "Tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                    //    gvTipoLote.RefreshData();
                    //    return;
                    //}

                    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_cliente = cod_cliente;
                    objDoc = unit.Proyectos.Mantenimiento_Documento_Cliente<eCliente.eCliente_Documentos>(obj);
                    if (objDoc == null) { MessageBox.Show("Error al agregar el documento", "Documento Separación", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    gvDocumentos.RefreshData();
                    CargarListadoDocumentosClientes("3");
                    //CargarListadoDocumentos("1");
                    //obtenerListadoTipoDocumentoXCliente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void obtenerListadoTipoDocumentoXCliente()
        {
            eClienteDocumentos.DataSource = null; eClienteDocumentos.DataSource = mylistDocumentos;
            if (MiAccion != Cliente.Nuevo)
            {
                List<eCliente.eCliente_Documentos> lista = unit.Clientes.ListarDocumentoCliente<eCliente.eCliente_Documentos>("3", cod_cliente);
                //List<eVariablesGenerales> lista = unit.Proyectos.ListarTipoLotexEtapas<eVariablesGenerales>("3", cod_etapa);
                mylistDocumentosCliente = lista;

                foreach (eCliente.eCliente_Documentos obj in lista)
                {
                    eCliente.eCliente_Documentos oLoteEtap = mylistDocumentos.Find(x => x.cod_documento_cliente == obj.cod_documento_cliente);
                    if (oLoteEtap != null)
                    {
                        oLoteEtap.flg_PDF = obj.flg_PDF; oLoteEtap.idPDF = obj.idPDF;
                        oLoteEtap.dsc_nombre_doc_ref = obj.dsc_nombre_doc_ref; oLoteEtap.dsc_nombre_doc = obj.dsc_nombre_doc;
                    }
                }
            }
            gvDocumentos.RefreshData();
        }

        private void gvDocumentos_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eCliente.eCliente_Documentos obj = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;
                    cod_documento_cliente = obj.cod_documento_cliente;
                    coldsc_nombre_doc.OptionsColumn.AllowEdit = true;
                    //colnum_orden_doc.OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void rbtnAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                eCliente.eCliente_Documentos obj = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;
                if (obj == null) { return; }
                await AdjuntarDocumentosVarios(obj);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
        private async Task AdjuntarDocumentosVarios(eCliente.eCliente_Documentos eSepDoc, string nombreDocAdicional = "")
        {
            try
            {
                DateTime FechaRegistro = DateTime.Today;
                string nombreCarpeta = "", mensajito = "";

                //eLotes_Separaciones resultado = unit.Proyectos.Obtener_DocumentoSeparacion<eLotes_Separaciones>(2, cod_separacion, cod_empresa);
                //if (resultado == null) { MessageBox.Show("Antes de adjuntar los docuentos debe crear al trabajador.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                OpenFileDialog myFileDialog = new OpenFileDialog();
                myFileDialog.Filter = "Archivos (*.*)|; *.*";
                myFileDialog.FilterIndex = 1;
                myFileDialog.InitialDirectory = "C:\\";
                myFileDialog.Title = "Abrir archivo";
                myFileDialog.CheckFileExists = false;
                myFileDialog.Multiselect = false;

                DialogResult result = myFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string IdCarpetaCliente = "", Extension = "";
                    var idArchivoPDF = "";
                    var TamañoDoc = new FileInfo(myFileDialog.FileName).Length / 1024;
                    if (TamañoDoc < 4000)
                    {
                        varPathOrigen = myFileDialog.FileName;
                        //varNombreArchivo = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd") + "."+ Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivo = eSepDoc.dsc_nombre_doc + "-" + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + (nombreDocAdicional != "" ? " " + nombreDocAdicional : "") + "-" + eSepDoc.num_orden_doc + Path.GetExtension(myFileDialog.SafeFileName);
                        //varNombreArchivoSinExtension = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd");
                        Extension = Path.GetExtension(myFileDialog.SafeFileName);
                    }
                    else
                    {
                        MessageBox.Show("Solo puede subir archivos hasta 4MB de tamaño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //SplashScreen.Open("Por favor espere...", "Cargando...");
                    eEmpresa eEmp = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    //SplashScreen.Open("Adjuntando documento...", "Cargando...");
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Adjuntando documento...", "Cargando...");

                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;
                    string correo = eEmp.UsuarioOnedrivePersonal;
                    string password = eEmp.ClaveOnedrivePersonal;

                    var securePassword = new SecureString();
                    foreach (char c in password)
                        securePassword.AppendChar(c);

                    authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                    GraphClient = new Microsoft.Graph.GraphServiceClient(
                      new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                      {
                          requestMessage
                              .Headers
                              .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                          return Task.FromResult(0);
                      }));

                    //var targetItemFolderId = eEmp.idCarpetaFacturasOnedrive;
                    eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
                    eDatos = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, dsc_Carpeta: "Lotes");
                    var targetItemFolderId = eDatos.idCarpeta;
                    var targetItemFolderIdLote = eDatos.idCarpetaAnho;
                    eCli = unit.Clientes.ObtenerCliente<eCliente>(2, cod_cliente);
                    //eLotSep.idCarpeta_separacion;
                    if (eCli.idCarpeta_cliente == null || eCli.idCarpeta_cliente == "")
                    {
                        var driveItem = new Microsoft.Graph.DriveItem
                        {
                            //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                            Name = eCli.dsc_documento + "-" + eCli.dsc_apellido_paterno.ToUpper() + " " + eCli.dsc_nombre.ToUpper(),
                            Folder = new Microsoft.Graph.Folder
                            {
                            },
                            AdditionalData = new Dictionary<string, object>()
                        {
                        {"@microsoft.graph.conflictBehavior", "rename"}
                        }
                        };

                        var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderIdLote].Children.Request().AddAsync(driveItem);
                        IdCarpetaCliente = driveItemInfo.Id;

                    }
                    else //Si existe folder obtener id
                    {
                        IdCarpetaCliente = eCli.idCarpeta_cliente;
                    }
                    if (eSepDoc.idPDF != null && eSepDoc.idPDF != "") { await Mover_Eliminar_ArchivoOneDrive(); }
                    //crea archivo en el OneDrive
                    byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                    using (Stream stream = new MemoryStream(data))
                    {
                        string res = "";
                        var DriveItem = await GraphClient.Me.Drive.Items[IdCarpetaCliente].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                        idArchivoPDF = DriveItem.Id;
                        eSepDoc.cod_cliente = cod_cliente;
                        //eSepDoc.cod_empresa = cod_empresa;
                        //eSepDoc.cod_proyecto = codigo;
                        eSepDoc.flg_PDF = "SI";
                        eSepDoc.idPDF = idArchivoPDF;
                        eSepDoc.dsc_nombre_doc = varNombreArchivo;
                        eSepDoc.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                        if (eCli.idCarpeta_cliente == null || eCli.idCarpeta_cliente == "")
                        {

                            eCliente eCli = AsignarValoresCliente();
                            eCli.idCarpeta_cliente = IdCarpetaCliente;
                            //eCli = unit.Clientes.Guardar_Actualizar_Cliente<eCliente>(eCli, "Actualizar");
                            eCli = unit.Clientes.Guardar_Actualizar_Cliente<eCliente>(eCli, "Actualizar");
                            //eLotes_Separaciones objLotSep = unit.Proyectos.MantenimientoSeparaciones<eLotes_Separaciones>(eLotSep);
                        }
                        eCliente.eCliente_Documentos resdoc = unit.Clientes.Mantenimiento_documento_cli<eCliente.eCliente_Documentos>(eSepDoc);
                        //SplashScreenManager.CloseForm(false);

                        //SplashScreen.Close();


                        if (resdoc != null)
                        {
                            mensajito = "Se registró el documento satisfactoriamente";
                            //MessageBox.Show("Se registró el documento satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarListadoDocumentosClientes("3");
                            //obtenerListadoTipoDocumentoXCliente();
                        }
                        else
                        {
                            mensajito = "Hubieron problemas al registrar el documento";

                            //MessageBox.Show("Hubieron problemas al registrar el documento", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                    SplashScreenManager.CloseForm(false);

                    //SplashScreen.Close();
                    MessageBox.Show(mensajito, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                //SplashScreen.Close();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async Task VerDocumentosVarios(string nombreDocAdicional = "")
        {
            try
            {

                eCliente.eCliente_Documentos obj = new eCliente.eCliente_Documentos();

                //    if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                //    {
                obj = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;
                if (obj == null || obj.cod_documento_cliente == "0") { return; }



                if (obj.flg_PDF == "NO")
                {
                    MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    eliminarArchivosExportados();
                    DateTime FechaRegistro = DateTime.Today;
                    //eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("2", codigo, codigoMultiple, cod_separacion);
                    eEmpresa eEmp = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //var app = App.PublicClientApp;
                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;



                    try
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                        //eEmpresa eEmp = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
                        //eEmpresa eEmpre = unit.Proyectos.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
                        //SplashScreen.Open("Abriendo documento...", "Cargando...");
                        string correo = eEmp.UsuarioOnedrivePersonal;
                        string password = eEmp.ClaveOnedrivePersonal;



                        var securePassword = new SecureString();
                        foreach (char c in password)
                            securePassword.AppendChar(c);



                        authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();



                        GraphClient = new Microsoft.Graph.GraphServiceClient(
                         new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                         {
                             requestMessage
                                 .Headers
                                 .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                             return Task.FromResult(0);
                         }));



                        string IdOneDriveDoc = obj.idPDF;
                        string Extension = ".*";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        //string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encrypta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + "PDF" + eLotSep.dsc_documento + "-" + eLotSep.dsc_apellido_paterno.ToUpper() + " " + eLotSep.dsc_nombre.ToUpper() + Extension;
                        //string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + "PDF" + eLotSep.dsc_documento + "-" + eLotSep.dsc_apellido_paterno.ToUpper() + " " + eLotSep.dsc_nombre.ToUpper() + "-" + obj.num_orden_doc + Extension;
                        string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + obj.dsc_nombre_doc;
                        if (!System.IO.File.Exists(ruta))
                        {
                            using (var fileStream = new FileStream(ruta, FileMode.Create, System.IO.FileAccess.Write))
                                fileContent.CopyTo(fileStream);
                        }

                        if (!System.IO.Directory.Exists(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()))) System.IO.Directory.CreateDirectory(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()));
                        System.Diagnostics.Process.Start(ruta);
                        //SplashScreen.Close();
                        SplashScreenManager.CloseForm(false);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //SplashScreen.Close();
                        SplashScreenManager.CloseForm(false);

                        /*SplashScreenManager.CloseForm()*/
                        ;

                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void eliminarArchivosExportados()
        {
            var carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
            DirectoryInfo source = new DirectoryInfo(carpeta);
            FileInfo[] filesToCopy = source.GetFiles();

            foreach (FileInfo oFile in filesToCopy)
            {
                //if (oFile. != ".xlsx")
                ////if (oFile.Extension != ".xlsx")
                //    {
                oFile.Delete();

                //}
            }
            //MessageBox.Show("Se procedió a eliminar los archivos exportados del sistema", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private async Task Mover_Eliminar_ArchivoOneDrive()
        {
            try
            {
                eCliente.eCliente_Documentos obj = new eCliente.eCliente_Documentos();

                obj = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;
                string IdCarpetaCliente = "", Extension = "";
                if (obj == null) { return; }

                eEmpresa eEmp = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
                if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


                ClientId = eEmp.ClientIdOnedrive;
                TenantId = eEmp.TenantOnedrive;
                Appl();
                var app = PublicClientApp;
                string correo = eEmp.UsuarioOnedrivePersonal;
                string password = eEmp.ClaveOnedrivePersonal;

                var securePassword = new SecureString();
                foreach (char c in password)
                    securePassword.AppendChar(c);

                authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                GraphClient = new Microsoft.Graph.GraphServiceClient(
                  new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                  {
                      requestMessage
                          .Headers
                          .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                      return Task.FromResult(0);
                  }));

                //var targetItemFolderId = eEmp.idCarpetaFacturasOnedrive;
                eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
                eDatos = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, dsc_Carpeta: "Lotes");
                var targetItemFolderId = eDatos.idCarpeta;
                var targetItemFolderIdLote = eDatos.idCarpetaAnho;
                eCli = unit.Clientes.ObtenerCliente<eCliente>(2, cod_cliente);
                //eLotSep.idCarpeta_separacion;
                //if (eLotSep.idCarpeta_separacion == null || eLotSep.idCarpeta_separacion == "")
                //{
                var driveItem = new Microsoft.Graph.DriveItem
                {
                    //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                    Name = obj.dsc_nombre_doc,
                    Folder = new Microsoft.Graph.Folder
                    {
                    },
                    AdditionalData = new Dictionary<string, object>()
                        {
                        {"@microsoft.graph.conflictBehavior", "rename"}
                        }
                };

                var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderIdLote].Children.Request().AddAsync(driveItem);
                IdCarpetaCliente = driveItemInfo.Id;

                await GraphClient.Me.Drive.Items[obj.idPDF].Request().DeleteAsync();

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                throw;
            }
        }

        static void Appl()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{TenantId}")
                .WithDefaultRedirectUri()
                .Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }

        private void gvDocumentos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCliente.eCliente_Documentos obj = gvDocumentos.GetRow(e.RowHandle) as eCliente.eCliente_Documentos;
                    if (obj.flg_PDF == "SI") { /*e.Appearance.BackColor = Color.LightGreen;*/ e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvContactosAdicionales_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvContactosAdicionales_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvContactosAdicionales_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            eCliente.eCliente_Contactos obj = gvContactosAdicionales.GetFocusedRow() as eCliente.eCliente_Contactos;

            obj.fch_registro = DateTime.Today; obj.dsc_usuario_registro = Program.Sesion.Usuario.dsc_usuario;
            gvContactosAdicionales.RefreshData();
        }

        private void rbtnEliminarContactoAdi_Click(object sender, EventArgs e)
        {
            try
            {
                eCliente.eCliente_Contactos obj = gvContactosAdicionales.GetFocusedRow() as eCliente.eCliente_Contactos;
                if (obj == null || obj.num_linea_contacto == 0) { return; }
                string tipCon = validarContactoExistente(obj);
                if (tipCon == null)
                {
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este contacto? \nEsta acción es irreversible.", "Eliminar tipo documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        string result = unit.Proyectos.Eliminar_Contacto(cod_cliente, obj.num_linea_contacto);
                        if (result == null)
                        {
                            MessageBox.Show("No se pudo eliminar el contacto \"" + obj.dsc_nombre_contacto + "\"", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        gvContactosAdicionales.RefreshData();
                        ObtenerDatos_ContactosCliente();
                        //CargarListadoDocumentosClientes("1");
                        //obtenerListadoTipoDocumentoXCliente();
                    }
                }
                else
                {
                    MessageBox.Show(tipCon, "Eliminar Contacto", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutorizacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            var abc = unit.Clientes.ObtenerFormatoAutorizacionCliente<eReportes>(cod_cliente);
            if (abc != null)
            {
                var formato = new FormatoWordHelper();

                formato.ShowWordReport<eReportes>(abc, cod_empresa, "00001");
            }

        }

        private void chkBienesSepCliente_CheckStateChanged(object sender, EventArgs e)
        {
            chkBienesSepCliente.Properties.Appearance.Font = chkBienesSepCliente.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
        }

        private void glkpTipoDireccionConyuge_EditValueChanged(object sender, EventArgs e)
        {
            txtNombreDireccionConyuge.Text = glkpTipoDireccionConyuge.Text;
        }

        private void lkpPaisConyuge_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistritoConyuge.Properties.DataSource = null;
            lkpProvinciaConyuge.Properties.DataSource = null;
            lkpDepartamentoConyuge.Properties.DataSource = null;

            unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamentoConyuge, "cod_departamento", "dsc_departamento", "", cod_condicion: lkpPaisConyuge.EditValue != null ? lkpPaisConyuge.EditValue.ToString() : "");
        }

        private void lkpDepartamentoConyuge_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistritoConyuge.Properties.DataSource = null;
            lkpProvinciaConyuge.Properties.DataSource = null;
            unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvinciaConyuge, "cod_provincia", "dsc_provincia", "", cod_condicion: lkpDepartamentoConyuge.EditValue != null ? lkpDepartamentoConyuge.EditValue.ToString() : "");
        }

        private void lkpProvinciaConyuge_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpProvinciaConyuge.EditValue != null)
            {
                glkpDistritoConyuge.Properties.DataSource = null;
                unit.Clientes.CargaCombosLookUp("TipoDistrito", glkpDistritoConyuge, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvinciaConyuge.EditValue != null ? lkpProvinciaConyuge.EditValue.ToString() : "");

                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    dsc_provincia = ", " + dataRow["dsc_provincia"].ToString();
                }
            }
        }

        private void glkpDistritoConyuge_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpDistritoConyuge.EditValue != null)
            {
                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    dsc_distrito = ", " + dataRow["dsc_distrito"].ToString();

                }

            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabControl2.SelectedTabPageIndex == 3)
            {
                eCliente_Direccion eDirecConyuge = new eCliente_Direccion();
                eDirecConyuge = unit.Clientes.ObtenerDireccion<eCliente_Direccion>(4, cod_cliente, 99);
                if (eDirecConyuge == null) { return; }
                txtCodDireccionConyuge.Text = eDirecConyuge.num_linea.ToString();
                glkpTipoDireccionConyuge.EditValue = eDirecConyuge.cod_tipo_direccion;
                txtNombreDireccionConyuge.Text = eDirecConyuge.dsc_nombre_direccion;
                mmDireccionConyuge.Text = eDirecConyuge.dsc_cadena_direccion;
                lkpPaisConyuge.EditValue = eDirecConyuge.cod_pais;
                lkpDepartamentoConyuge.EditValue = eDirecConyuge.cod_departamento;
                lkpProvinciaConyuge.EditValue = eDirecConyuge.cod_provincia;
                glkpDistritoConyuge.EditValue = eDirecConyuge.cod_distrito;
                txtFono1DireccionConyuge.Text = eDirecConyuge.dsc_telefono_1;
                txtFono2DireccionConyuge.Text = eDirecConyuge.dsc_telefono_2;
                txtRefereciaConyuge.Text = eDirecConyuge.dsc_referencia;
            }
        }

        private void mmDireccionConyuge_KeyUp(object sender, KeyEventArgs e)
        {
            txtDireccionConyuge.Text = mmDireccionConyuge.Text;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            consultarSunat(0);
        }

        private void txtFono1_Leave(object sender, EventArgs e)
        {
            if (MiAccion == Cliente.Nuevo)
            {
                int validar = validarCantidadNumerosCadena(txtFono1.Text.Trim());
                if (validar == 0) return;
                eProspectosXLote obj = new eProspectosXLote();
                obj = unit.Proyectos.ObtenerListadoEmpresaSeleccionada<eProspectosXLote>(nCombo:"22",  dsc_telefono_movil: txtFono1.Text.Trim());
                if (obj != null && validate == 0)
                {
                    if (MessageBox.Show("El número de teléfono ingresado se encontro en el prospecto "+ obj.dsc_persona + "." + Environment.NewLine +
                                    "¿Desea visualizar la información del prospecto?", //y vincularlo a su empresa?", 
                                    "Validar número de teléfono", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        validate = 1;
                        AsignarCamposClientesProspecto(obj);
                        return;
                    }
                    //txtFono1.Select();
                }
            }
        }

        //private void gvDocumentos_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        eCliente.eCliente_Documentos obj = gvDocumentos.GetFocusedRow() as eCliente.eCliente_Documentos;
        //        if (obj == null) return;
        //        if (gvDocumentos.FocusedColumn.FieldName != "dsc_nombre_doc_ref" || gvDocumentos.FocusedColumn.FieldName != "num_orden_doc")
        //        {
        //            e.Cancel = obj.num_orden_doc == 0 ? true : false;
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private async void rbtnVer_Click(object sender, EventArgs e)
        {
            try
            {
                await VerDocumentosVarios();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void glkpDistrito_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpDistrito.EditValue != null)
            {
                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    dsc_distrito = ", " + dataRow["dsc_distrito"].ToString();

                }

            }
        }

        public void AgregarDocCliente()
        {

            try
            {
                int numOrden = mylistDocumentos.Count() + 1;
                eCliente.eCliente_Documentos ListAddDocSep = new eCliente.eCliente_Documentos
                {
                    cod_documento_cliente = "0",
                    num_orden_doc = numOrden,
                    flg_activo = "SI"
                };
                cod_documento_cliente = ListAddDocSep.cod_documento_cliente;
                mylistDocumentos.Add(ListAddDocSep);
                gvDocumentos.RefreshData();
                gvDocumentos.FocusedRowHandle = gvDocumentos.RowCount - 1;

                //rtxtNombre.EnableCustomMaskTextInput(args => { args.Cancel(); return; });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private void btnBuscarProspecto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //formName = "Prospecto";

            frmAsignarProspecto frm = new frmAsignarProspecto(null, this);



            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto_titulo;
            frm.dsc_proyecto = dsc_proyecto_titulo;
            //frm.validarFormulario = 1;
            frm.ShowDialog();
        }

        private void glkpTipoDireccion_EditValueChanged(object sender, EventArgs e)
        {
            txtNombreDireccion.Text = glkpTipoDireccion.Text;

        }

        private void grdbNatJuridi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRazonSocial.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true; //chkFlgJuridica.CheckState == CheckState.Checked ? true : false;

            txtNombreComercial.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? false : true;  //chkFlgJuridica.CheckState == CheckState.Checked ? true : false;
            txtApellPaterno.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? true : false;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtApellMaterno.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? true : false;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            txtNombre.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? true : false;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            dtFecNacimiento.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? true : false; //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            dtFecNacimiento.EditValue = grdbNatJuridi.SelectedIndex == 1 && frmHandlerSeparacion == null ? null : dtFecNacimiento.EditValue;  //chkFlgJuridica.CheckState == CheckState.Checked ? null : dtFecNacimiento.EditValue;
            //txtEdad.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            //lkpSexo.Enabled = chkFlgJuridica.CheckState == CheckState.Checked ? false : true; 
            lkpEstadoCivil.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? true : false; //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            glkpTipoDocumento.ReadOnly = grdbNatJuridi.SelectedIndex == 1 ? true : false;  //chkFlgJuridica.CheckState == CheckState.Checked ? false : true;
            if (grdbNatJuridi.SelectedIndex == 1) glkpTipoDocumento.EditValue = "DI004";
        }

        private void chkCodigoManual_CheckStateChanged(object sender, EventArgs e)
        {
            if (MiAccion == Cliente.Nuevo) txtCodCliente.Enabled = chkCodigoManual.CheckState == CheckState.Checked ? true : false;
        }

        private void gvListaClienteContactos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaDireccionContactos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void tlUbicacionesDireccion_CustomDrawColumnHeader(object sender, DevExpress.XtraTreeList.CustomDrawColumnHeaderEventArgs e)
        {

        }
    }
}