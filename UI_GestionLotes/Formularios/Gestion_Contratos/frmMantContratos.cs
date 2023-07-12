
using BE_GestionLotes;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;
using UI_GestionLotes.Formularios.Operaciones;

namespace UI_GestionLotes.Formularios.Gestion_Contratos
{
    internal enum Contratos
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantContratos : DevExpress.XtraEditors.XtraForm
    {
        string probandoHTML = "", probandoHTMLIMAGEN = "", probandoXML1 = "", probandoXML2 = "";
        private readonly UnitOfWork unit;
        public eLotes_Separaciones campo_dias = new eLotes_Separaciones();
        public int validar = 0, validarCuotas = 0, pruebita = 1, num_linea = 0, num_cuotas = 0, num_adenda = 0, num_financiamiento = 0;
        string mensajeCUOTAS = "";
        public string cod_proyecto = "", cod_etapas = "", cod_lote = "", cod_empresa = "", cod_status = "", codigoMultiple = "", flg_activo = "", cod_cliente = "", dsc_proyecto = "", cod_separacion = "", cod_contrato = "", cod_documento_contrato = "", cod_forma_pago = "FI";
        public decimal imp_CUOI = 0, imp_penPago = 0;
        string varPathOrigen = "";
        string varNombreArchivo = "";
        private static string ClientId = "";
        private static string TenantId = "";
        private static string Instance = "https://login.microsoftonline.com/";
        public static IPublicClientApplication _clientApp;
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        internal Contratos MiAccion = Contratos.Nuevo;
        eContratos eLotCon = new eContratos();
        System.Drawing.Image imgCheck = Properties.Resources.checked_checkbox_16px;
        System.Drawing.Image imgUnCheck = Properties.Resources.unchecked_checkbox6_15px;
        public List<eContratos.eContratos_Documentos> mylistDocumentos = new List<eContratos.eContratos_Documentos>();
        public List<eContratos.eContratos_Observaciones> mylistObservaciones = new List<eContratos.eContratos_Observaciones>();
        public List<eContratos.eContratos_Adenda_Financiamiento> mylistAdenda = new List<eContratos.eContratos_Adenda_Financiamiento>();
        public List<eContratos.eContratos_Adenda_Financiamiento> mylistFinanciamiento = new List<eContratos.eContratos_Adenda_Financiamiento>();
        public List<eContratos.eContratos_Adenda_Financiamiento> mylistDetalleCuotas = new List<eContratos.eContratos_Adenda_Financiamiento>();
        public List<eContratos.eContratos_Adenda_Financiamiento> mylistDetalleCuotasHTML = new List<eContratos.eContratos_Adenda_Financiamiento>();
        int validateFormClose = 0;
        frmListadoContratos frmHandler;

        public Boolean Separaciones = false, copropietario = false;

        public frmMantContratos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmMantContratos(frmListadoContratos frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmMantContratos_Load(object sender, EventArgs e)
        {
            xtpageDatosG.Appearance.Header.ForeColor = Program.Sesion.Colores.Verde;
            xtpageDetalleFi.Appearance.Header.ForeColor = Program.Sesion.Colores.Verde;
            labelControl4.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            btnBuscarSeparacion.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnBuscarCliente.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnVerCli.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnVerSeparacion.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnBuscarCopropietario.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnVerCopro.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnBuscarLote.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnGrabarCuotas.Appearance.BackColor = Program.Sesion.Colores.Verde;
            lblEstado.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl1.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl2.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl3.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl4.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl5.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl6.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl7.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl8.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl9.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            lblVentaFinanciada.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            lblDocumentos.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            //chkConyuge.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chkFinanciado.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chkContado.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chkConfiguracion.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Contratos.Nuevo:
                    //llenarVariableDiasVencimientoSeparacion();
                    CargarCombos();
                    Nuevo();
                    //BloqueoControlesInformacionSeparación(true, false, false);
                    //BloqueoControlesInformacionCliente(true, true, false);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    BloqueoControlesInformacionVenta(false, true, false, true);
                    txtPreTerreno.ReadOnly = true;
                    txtDescuentoSol.ReadOnly = true;
                    txtPreFinalDescuento.ReadOnly = true;
                    txtValorCuotas.ReadOnly = true;
                    txtPorcInteres.ReadOnly = true;
                    txtPrecioFinalFinanciar.ReadOnly = true;
                    dtFecPagoCuota.ReadOnly = false;
                    dtFecVecCuota.ReadOnly = false;
                    break;
                case Contratos.Editar:
                    //llenarVariableDiasVencimientoSeparacion();
                    CargarCombos();
                    Editar();
                    //BloqueoControlesInformacionCliente(false, true, false);
                    //BloqueoControlesInformacionSeparación(true, false, !extension);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    BloqueoControlesInformacionVenta(true, true, true, true);
                    gvObsContratos.OptionsBehavior.Editable = true;
                    chkConfiguracion.Enabled = true;


                    break;

                case Contratos.Vista:
                    //llenarVariableDiasVencimientoSeparacion();
                    CargarCombos();
                    Editar();
                    //BloqueoControlesInformacionCliente(false, true, false);
                    //BloqueoControlesInformacionSeparación(true, true, true);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    BloqueoControlesInformacionVenta(true, true, true, true);
                    Ver(true);

                    //gvDocumentos.OptionsBehavior.Editable = false;
                    break;
            }
        }

        private void Ver(Boolean ReadOnly)
        {
            //gvObsSeparaciones.OptionsBehavior.Editable = Editable;
            //layoutControl2.Enabled = false;
            //layoutControl16.Enabled = false;
            btnNuevo.Enabled = ReadOnly;
            lkpTipoContrato.ReadOnly = ReadOnly;
            lkpAsesor.ReadOnly = ReadOnly;
            btnBuscarCopropietario.Enabled = false;
            btnGuardar.Enabled = !ReadOnly;
            btnFormatoContrato.Enabled = !ReadOnly;
            btnFirma.Enabled = !ReadOnly;
            btnAnular.Enabled = !ReadOnly;
            gvObsContratos.OptionsBehavior.Editable = !ReadOnly;
            chkConfiguracion.Enabled = false;

        }

        private void Nuevo()
        {
            Separaciones = false;
            cod_lote = "";
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.dsc_usuario;
            //txtUsuarioCambio.Text = "";
            //dtFecPagoSeparacion.EditValue = DateTime.Now;
            //lytEmitido.ImageOptions.Image = imgUnCheck;
            dtFechaRegistro.EditValue = DateTime.Now;
            dtEmitido.EditValue = DateTime.Today;
            chkFinanciado.CheckState = CheckState.Checked;
            DateTime date = DateTime.Now;
            //DateTime oInicioFechaNac = date.AddYears(-18).AddDays(-1); //new DateTime(date.Year, date.Month, 1);
            //DateTime oVencimiento = date.AddDays(campo_dias.num_dias_venc_sep); 
            layoutControl2.Enabled = true;
            btnGuardar.Enabled = false;
            picAnteriorSeparacion.Enabled = false;
            picSiguienteSeparacion.Enabled = false;
            eliminar.Enabled = false;
            btnBuscarCopropietario.Enabled = true;
            btnVerCli.Enabled = false;
            btnVerCopro.Enabled = false;
            btnFormatoContrato.Enabled = false;
            chkConfiguracion.Enabled = true;
            gvObsContratos.OptionsBehavior.Editable = true;

            AgregarAdenda();

            //AgregarFinanciado();
        }

        private void Editar()
        {
            string accion = "2";
            eLotCon = unit.Proyectos.ObtenerContratos<eContratos>(accion, cod_proyecto, codigoMultiple, cod_contrato);

            cod_empresa = eLotCon.cod_empresa;
            cod_proyecto = eLotCon.cod_proyecto;
            cod_contrato = eLotCon.cod_contrato;
            barActEstado.Enabled = true;
            //xtpageDetalleFi.PageEnabled = true;

            txtCodContrato.Text = eLotCon.cod_contrato;
            if (validarCadenaVacio(eLotCon.cod_separacion))
            {
                btnBuscarSeparacion.Enabled = false;
                lkpAsesor.ReadOnly = true;
            }
            else
            {
                txtCodSepara.Text = eLotCon.cod_separacion;
                cod_separacion = eLotCon.cod_separacion;
                btnBuscarSeparacion.Enabled = false;
                btnVerSeparacion.Enabled = true;
                if (eLotCon.cod_forma_pago.ToString() == "CO")
                {
                    lytImpSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lytFchSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    simpleLabelItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    spaceOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    lytImpSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lytFchSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lytPenPag.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lytFecPagoCon.TextLocation = DevExpress.Utils.Locations.Default;
                    lytFecPagoCon.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
                    lytFecPagoCon.TextSize = new Size(116, 13);
                    lytFecPagoCon.MaxSize = new Size(0, 24);
                    lytAream.TextSize = new Size(116, 13);
                    txtSeparacionCO.Text = eLotCon.imp_separacion.ToString();
                    txtPendientePago.Text = eLotCon.imp_pendiente_pago.ToString();
                    chkContado.CheckState = CheckState.Checked;
                    //BloqueoControlesInformacionVenta(true, false, true, false);
                }

                if (eLotCon.cod_forma_pago.ToString() == "FI")
                {
                    lytImpSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lytFchSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    simpleLabelItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    spaceOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lytImpSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lytFchSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lytPenPag.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lytFecPagoCon.TextLocation = DevExpress.Utils.Locations.Top;
                    lytAream.TextSize = new Size(81, 13);
                    lytFecPagoCon.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.UseParentOptions;
                    txtSeparacionFI.Text = eLotCon.imp_separacion.ToString();
                    chkFinanciado.CheckState = CheckState.Checked;
                    //BloqueoControlesInformacionVenta(true, true, false, false);
                }
            }

            if (eLotCon.cod_forma_pago.ToString() == "CO")
            {
                chkContado.CheckState = CheckState.Checked;
            }
            if (eLotCon.cod_forma_pago.ToString() == "FI")
            {
                chkFinanciado.CheckState = CheckState.Checked;
            }
            txtCodigoCliente.Text = eLotCon.cod_cliente;
            cod_cliente = eLotCon.cod_cliente;
            btnBuscarCliente.Enabled = false;
            btnVerCli.Enabled = true;
            glkpTipoDocumento.EditValue = eLotCon.cod_tipo_documento;
            txtNroDocumento.Text = eLotCon.dsc_documento;
            unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContacto, "num_linea_contacto_string", "dsc_nombre_contacto", "", valorDefecto: true, codigo: eLotCon.cod_cliente);
            lkpContacto.EditValue = eLotCon.dsc_linea_contacto;
            lkpContacto.ReadOnly = true;
            txtNombres.Text = eLotCon.dsc_nombre;
            lkpEstadoCivil.EditValue = eLotCon.cod_estadocivil;
            txtDireccion.Text = eLotCon.dsc_cadena_direccion;
            lkpAsesor.EditValue = eLotCon.cod_asesor;
            cod_lote = eLotCon.cod_lote.ToString();
            lkpEtapa.EditValue = codigoMultiple;
            lkpManzana.EditValue = eLotCon.cod_manzana;
            lkpLote.EditValue = eLotCon.cod_lote;
            txtPreTerreno.Text = eLotCon.imp_precio_lista.ToString();
            txtDescuentoPorc.Text = eLotCon.prc_descuento.ToString();
            txtDescuentoSol.Text = eLotCon.imp_descuento.ToString();
            txtPrecioFinalFinanciar.Text = eLotCon.imp_saldo_financiar.ToString();
            if (eLotCon.fch_Separacion.Year == 1) { dtFechaSeparacionFI.EditValue = null; } else { dtFechaSeparacionFI.EditValue = eLotCon.fch_Separacion; }
            if (eLotCon.fch_Separacion.Year == 1) { dtFechaSeparacionCO.EditValue = null; } else { dtFechaSeparacionCO.EditValue = eLotCon.fch_Separacion; }

            if (eLotCon.fch_emitido.Year == 1) { dtEmitido.EditValue = null; dtEmitido.ReadOnly = false; } else { dtEmitido.EditValue = eLotCon.fch_emitido; dtEmitido.ReadOnly = true; }
            if (eLotCon.fch_vct_cuota.Year == 1) { dtFecVecCuota.EditValue = null; } else { dtFecVecCuota.EditValue = eLotCon.fch_vct_cuota; }
            if (eLotCon.fch_pago_cuota.Year == 1) { dtFecPagoCuota.EditValue = null; } else { dtFecPagoCuota.EditValue = eLotCon.fch_pago_cuota; }
            if (eLotCon.fch_pago_contado.Year == 1) { dtFecPagoContado.EditValue = null; } else { dtFecPagoContado.EditValue = eLotCon.fch_pago_contado; }

            lkpCuotas.EditValue = eLotCon.cod_cuotas;
            lkpCuotasDE.EditValue = lkpCuotas.EditValue;
            txtFraccion.Text = eLotCon.num_fraccion.ToString();
            //lkpCuotas.Text = eLotSep.num_cuotas.ToString();
            txtValorCuotas.Text = eLotCon.imp_valor_cuota.ToString();
            if (eLotCon.cod_copropietario != null && eLotCon.cod_copropietario != "")
            {

                btnVerCopro.Enabled = true;
                txtCodigoCopropietario.Text = eLotCon.cod_copropietario;
                lkpEstadoCivilCopropietario.EditValue = eLotCon.cod_estadocivil_CO;
                glkpTipoDocumentoCopropietario.EditValue = eLotCon.cod_tipo_documento_CO;
                txtNroDocumentoCopropietario.Text = eLotCon.dsc_documento_CO;
                txtNombresCopropietario.Text = eLotCon.dsc_nombre_CO;
                txtDireccionCopropietario.Text = eLotCon.dsc_cadena_direccion_CO;
                txtTelefCopropietario.Text = eLotCon.dsc_telefono_1_CO;
                txtCorreoCopropietario.Text = eLotCon.dsc_email_CO;
                unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContactoCo, "num_linea_contacto_string", "dsc_nombre_contacto", "", valorDefecto: true, codigo: eLotCon.cod_copropietario);
                lkpContactoCo.EditValue = eLotCon.dsc_linea_contacto_copro;
                lkpContactoCo.ReadOnly = true;

            }
            int validar = 0;

            if (eLotCon.fch_abono.Year == 1) { dtbolEmitida.EditValue = null; lytbolEmitida.ImageOptions.Image = imgUnCheck; /*btnFirma.Enabled = true;*/ }
            else { dtbolEmitida.EditValue = eLotCon.fch_abono; lytbolEmitida.ImageOptions.Image = imgCheck; btnAnular.Enabled = false; btnBolEmitir.Enabled = false; btnFirma.Enabled = true;  btnRecepcionado.Enabled = true; dtbolEmitida.ReadOnly = true;  validar = 1; }

            if (eLotCon.fch_firmado.Year == 1 && eLotCon.flg_abono == "SI") { dtFirmado.EditValue = null; lytFirmado.ImageOptions.Image = imgUnCheck; /*btnFirma.Enabled = true;*/ }
            else if (eLotCon.fch_firmado.Year == 1 && eLotCon.flg_abono == "NO") { dtFirmado.EditValue = null; lytFirmado.ImageOptions.Image = imgUnCheck; dtFirmado.ReadOnly = true; btnFirma.Enabled = false; }
            else { dtFirmado.EditValue = eLotCon.fch_firmado; lytFirmado.ImageOptions.Image = imgCheck; btnAnular.Enabled = false; btnFirma.Enabled = false; btnRecepcionado.Enabled = true; dtFirmado.ReadOnly = true;  validar = 1; }

            if (eLotCon.fch_recepcionado.Year == 1) { dtRecepcionado.EditValue = null; lytRecepcionado.ImageOptions.Image = imgUnCheck;/* btnResuelto.Enabled = true;*/ }
            else { dtRecepcionado.EditValue = eLotCon.fch_recepcionado; lytRecepcionado.ImageOptions.Image = imgCheck; btnAnular.Enabled = false; btnRecepcionado.Enabled = false; btnResuelto.Enabled = true; validar = 1; }

            if (eLotCon.fch_resuelto.Year == 1) { dtResuelto.EditValue = null; lytResuelto.ImageOptions.Image = imgUnCheck;/* btnResuelto.Enabled = true;*/ }
            else { dtResuelto.EditValue = eLotCon.fch_resuelto; lytResuelto.ImageOptions.Image = imgCheck; btnAnular.Enabled = false; btnResuelto.Enabled = false;/* btnResuelto.Enabled = false;*/ validar = 1; }

            if (eLotCon.fch_anulado.Year == 1) { dtAnulado.EditValue = null; lytAnulado.ImageOptions.Image = imgUnCheck; /*barActEstado.Enabled = true;*/ if (validar == 0) { btnAnular.Enabled = true; } }
            else { dtAnulado.EditValue = eLotCon.fch_anulado; lytAnulado.ImageOptions.Image = imgCheck; /*btnFirma.Enabled = false;*/ /*barActEstado.Enabled = false;*/ btnAnular.Enabled = false; }

            //if (eLotCon.fch_firmado.Year != 1 && eLotCon.fch_resuelto.Year != 1)
            //{
            //    barActEstado.Enabled = true;

            //}

            if (eLotCon.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; }
            else { dtFechaRegistro.EditValue = eLotCon.fch_registro; txtUsuarioRegistro.Text = eLotCon.cod_usuario_registro.ToString(); }


            btnFormatoContrato.Enabled = true;
            btnFirma.Enabled = eLotCon.flg_firmado == "NO" && eLotCon.flg_abono == "SI";



            btnBuscarCopropietario.Enabled = false;
            btnGuardar.Enabled = true;


            ObtenerDatos_ObservacionesContratos();
            ObtenerDatos_AdendaContratos();
            chkFinanciado.Enabled = false;
            chkContado.Enabled = false;
            CargarListadoDocumentos("3");
            //obtenerListadoTipoDocumentoXSeparacion();


            txtPreFinalDescuento.Text = eLotCon.imp_precio_venta_final.ToString();
            txtCuoInicial.Text = eLotCon.imp_cuota_inicial.ToString();
            txtPendientePago.Text = eLotCon.imp_pendiente_pago.ToString();
            imp_CUOI = eLotCon.imp_cuota_inicial;
            imp_penPago = eLotCon.imp_pendiente_pago;
            txtPrecioFinalFinanciar.Text = eLotCon.imp_saldo_financiar.ToString();
            AsignarDatosDetalle(eLotCon.cod_forma_pago.ToString());
            txtPorcInteres.EditValue = eLotCon.prc_interes;
            txtPorcInteresDE.EditValue = eLotCon.prc_interes;
            txtImpInteres.EditValue = eLotCon.imp_interes;
            txtImpInteresDE.EditValue = txtImpInteres.EditValue;

        }


        public void AsignarDatosSeparacion(eLotes_Separaciones eLosep)
        {
            LimpiarCamposSeparacion();

            unit.Clientes.CargaCombosLookUp("Vendedor", lkpAsesor, "cod_asesor", "dsc_asesor", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario, cod_condicion: "2");

            txtCodSepara.Text = eLosep.cod_separacion;
            cod_separacion = eLosep.cod_separacion;
            cod_proyecto = eLosep.cod_proyecto;
            cod_etapas = eLosep.cod_etapa;
            cod_lote = eLosep.cod_lote;
            cod_cliente = eLosep.cod_cliente;
            if (lkpCuotas.EditValue == null)
            {
                unit.Proyectos.CargaCombosLookUp("Cuotas", lkpCuotas, "cod_cuotas", "dsc_cuotas", "", cod_uno: cod_proyecto);
                lkpCuotasDE.Properties.DataSource = lkpCuotas.Properties.DataSource;
                lkpCuotasDE.Properties.ValueMember = lkpCuotas.Properties.ValueMember;
                lkpCuotasDE.Properties.DisplayMember = lkpCuotas.Properties.DisplayMember;
                lkpCuotasDE.EditValue = lkpCuotas.EditValue;
            }

            //Campos del Contrato
            lkpAsesor.EditValue = eLosep.cod_asesor;
            //Campos del Cliente
            btnVerSeparacion.Enabled = true;
            btnVerCli.Enabled = true;
            txtCodigoCliente.Text = eLosep.cod_cliente;
            glkpTipoDocumento.EditValue = eLosep.cod_tipo_documento;
            txtNroDocumento.Text = eLosep.dsc_documento;
            lkpEstadoCivil.EditValue = eLosep.cod_estadocivil;
            txtNombres.Text = eLosep.dsc_cliente;
            txtDireccion.Text = eLosep.dsc_cadena_direccion;
            //txtTelef.Text = eLosep.dsc_telefono_1;
            //txtCorreo.Text = eLosep.dsc_email;
            unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContacto, "num_linea_contacto_string", "dsc_nombre_contacto", "0", valorDefecto: true, codigo: eLosep.cod_cliente);
            lkpContacto.ReadOnly = false;
            //Campos del Copropietario
            if (eLosep.cod_copropietario != null && eLosep.cod_copropietario != "")
            {
                btnVerCopro.Enabled = true;
                txtCodigoCopropietario.Text = eLosep.cod_copropietario;
                glkpTipoDocumentoCopropietario.EditValue = eLosep.cod_tipo_documento_CO;
                txtNroDocumentoCopropietario.Text = eLosep.dsc_documento_CO;
                lkpEstadoCivilCopropietario.EditValue = eLosep.cod_estadocivil_CO;
                txtNombresCopropietario.Text = eLosep.dsc_nombre_CO;
                txtDireccionCopropietario.Text = eLosep.dsc_cadena_direccion_CO;
                //txtTelefCopropietario.Text = eLosep.dsc_telefono_1_CO;
                //txtCorreoCopropietario.Text = eLosep.dsc_email_CO;
                unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContactoCo, "num_linea_contacto_string", "dsc_nombre_contacto", "0", valorDefecto: true, codigo: eLosep.cod_copropietario);
                lkpContactoCo.ReadOnly = false;
            }

            //Campos del Terreno
            if (eLosep.cod_forma_pago.ToString() == "CO")
            {
                lytImpSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytFchSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                simpleLabelItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                spaceOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lytImpSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytFchSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytPenPag.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytFecPagoCon.TextLocation = DevExpress.Utils.Locations.Default;
                lytFecPagoCon.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
                lytFecPagoCon.TextSize = new Size(116, 13);
                lytFecPagoCon.MaxSize = new Size(0, 24);
                lytAream.TextSize = new Size(116, 13);
                txtSeparacionCO.Text = eLosep.imp_separacion.ToString();
                txtPendientePago.Text = eLosep.imp_pendiente_pago.ToString();
                chkContado.CheckState = CheckState.Checked;

                //BloqueoControlesInformacionVenta(true, false, true, false);
            }
            if (eLosep.cod_forma_pago.ToString() == "FI")
            {
                lytImpSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytFchSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                simpleLabelItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                spaceOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytImpSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytFchSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytPenPag.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytFecPagoCon.TextLocation = DevExpress.Utils.Locations.Top;
                lytAream.TextSize = new Size(81, 13);
                lytFecPagoCon.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.UseParentOptions;
                txtSeparacionFI.Text = eLosep.imp_separacion.ToString();
                chkFinanciado.CheckState = CheckState.Checked;
                //BloqueoControlesInformacionVenta(true, true, false, false);
            }
            //BloqueoControlesInformacionTerreno(true, false, true);
            Separaciones = true;
            unit.Clientes.CargaCombosLookUp("EtapasFiltroProyecto", lkpEtapa, "cod_etapa", "dsc_descripcion", "", valorDefecto: true, codigo: cod_proyecto, codigoMultiple: cod_etapas);
            lkpEtapa.EditValue = eLosep.cod_etapa;
            lkpManzana.EditValue = eLosep.cod_manzana;
            lkpLote.EditValue = eLosep.cod_lote;
            txtPreTerreno.Text = eLosep.imp_precio_total.ToString();
            txtDescuentoPorc.Text = eLosep.prc_descuento.ToString();
            txtDescuentoSol.Text = eLosep.imp_descuento.ToString();
            //txtFraccion.Text = eLotSep.f
            txtPrecioFinalFinanciar.Text = eLosep.imp_precio_final.ToString();
            if (eLosep.fch_Separacion.Year == 1) { dtFechaSeparacionFI.EditValue = null; } else { dtFechaSeparacionFI.EditValue = eLosep.fch_Separacion; }
            if (eLosep.fch_Separacion.Year == 1) { dtFechaSeparacionCO.EditValue = null; } else { dtFechaSeparacionCO.EditValue = eLosep.fch_Separacion; }
            if (eLosep.fch_vct_cuota.Year == 1) { dtFecVecCuota.EditValue = null; } else { dtFecVecCuota.EditValue = eLosep.fch_vct_cuota; }
            if (eLosep.fch_pago_cuota.Year == 1) { dtFecPagoCuota.EditValue = null; } else { dtFecPagoCuota.EditValue = eLosep.fch_pago_cuota; }
            if (eLosep.fch_pago_total.Year == 1) { dtFecPagoContado.EditValue = null; } else { dtFecPagoContado.EditValue = eLosep.fch_pago_total; }

            lkpCuotas.EditValue = eLosep.cod_cuotas;
            lkpCuotasDE.EditValue = lkpCuotas.EditValue;
            txtFraccion.Text = eLosep.num_fraccion.ToString();
            num_cuotas = eLosep.num_fraccion > 0 &&  eLosep.num_fraccion < 12 ? eLosep.num_fraccion : num_cuotas;
            txtValorCuotas.Text = eLosep.imp_valor_cuota.ToString();
            chkFinanciado.Enabled = false;
            chkContado.Enabled = false;
            txtPreFinalDescuento.Text = eLosep.imp_precio_con_descuento.ToString();
            txtCuoInicial.Text = eLosep.imp_cuota_inicial.ToString();
            imp_CUOI = eLosep.imp_cuota_inicial;
            txtPrecioFinalFinanciar.Text = eLosep.imp_precio_final.ToString();
            cod_status = eLosep.cod_status;
            flg_activo = eLosep.flg_activo;
            txtPorcInteres.EditValue = eLosep.prc_interes;
            txtPorcInteresDE.EditValue = eLosep.prc_interes;
            txtImpInteres.EditValue = eLosep.imp_interes;
            txtImpInteresDE.EditValue = txtImpInteres.EditValue;
            btnBuscarCliente.Enabled = false;
            btnBuscarCopropietario.Enabled = false;
            btnGuardar.Enabled = true;
            lkpAsesor.ReadOnly = true;
            BloqueoControlesInformacionTerreno(true, true, false);
            BloqueoControlesInformacionVenta(true, true, true, true);
            AsignarDatosDetalle(eLosep.cod_forma_pago.ToString());
            AgregarDetalleCuotas();

        }

        public void AsignarDatosDetalle(string formaPago)
        {
            txtPreFinalDescuentoDE.Text = txtPreFinalDescuento.Text;
            txtPrecioFinalFinanciarDE.Text = txtPrecioFinalFinanciar.Text;

            if (formaPago == "FI")
            {
                if (dtFecVecCuota.EditValue == null) { return; }
                txtSeparacionDE.Text = txtSeparacionFI.Text;
                txtCuoInicialDE.Text = txtCuoInicial.Text;
                unit.Proyectos.CargaCombosLookUp("Cuotas", lkpCuotasDE, "cod_cuotas", "dsc_cuotas", "", cod_uno: cod_proyecto);
                lkpCuotasDE.EditValue = lkpCuotas.EditValue;
                txtNumCuotaEspecialDE.Text = txtFraccion.Text;
                txtPorcInteresDE.EditValue = txtPorcInteres.EditValue;
                txtImpInteres.EditValue = (((Convert.ToDecimal(txtPreTerreno.EditValue.ToString().Replace("S/", "")) * (1 - Convert.ToDecimal(txtDescuentoPorc.EditValue))) - Convert.ToDecimal(txtCuoInicial.EditValue) - (chkFinanciado.CheckState == CheckState.Checked ? Convert.ToDecimal(txtSeparacionFI.EditValue) : Convert.ToDecimal(txtSeparacionCO.EditValue))) * Convert.ToDecimal(txtPorcInteres.EditValue)) * (num_cuotas / 12);
                txtImpInteresDE.EditValue = txtImpInteres.EditValue;
                txtValorCuotasDE.Text = txtValorCuotas.Text;
                dtFecVecCuotaDE.EditValue = dtFecVecCuota.EditValue;
            }
            if (formaPago == "CO")
            {
                if (dtFecPagoContado.EditValue == null) { return; }
                txtSeparacionDE.Text = txtSeparacionCO.Text;
            }
        }
        private void chkActivoSeparacion_CheckStateChanged(object sender, EventArgs e)
        {
            //chkActivoContrato.Properties.Appearance.Font = chkActivoContrato.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);

        }


        private void chkContado_CheckStateChanged(object sender, EventArgs e)
        {
            chkContado.Properties.Appearance.Font = chkContado.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
            chkFinanciado.CheckState = chkContado.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
            if (chkContado.CheckState == CheckState.Checked)
            {
                LimpiarCamposFinanciado();
                BloqueoControlesInformacionVenta(true, true, false, false);
                txtFraccion.ReadOnly = true;
                if (validarCadenaVacio(txtCodSepara.Text))
                {
                    AsignarDatosDetalle(cod_forma_pago.ToString());
                }
                cod_forma_pago = "CO";

                if (MiAccion == Contratos.Nuevo) { dtFecPagoContado.EditValue = DateTime.Now; }
            }
            else
            {
                dtFecPagoContado.EditValue = null;
            }

            //if (chkContado.CheckState == CheckState.Checked)
            //{
            //    //OcultarCamposContado();
            //    LimpiarCamposFormaPago();
            //    BloqueoControlesInformacionVenta(true, true, false, false);
            //    txtFraccion.ReadOnly = true;
            //    //if (MiAccion == Separacion.Nuevo) { dtFecPagoContado.EditValue = DateTime.Now; }
            //}
            //else
            //{
            //    dtFecPagoContado.EditValue = null;
            //}
        }

        private void LimpiarCamposFinanciado()
        {
            num_cuotas = 0;
            txtCuoInicial.EditValue = "0";
            txtCuoInicial.Refresh();
            txtPrecioFinalFinanciar.EditValue = "0";
            txtPrecioFinalFinanciar.Refresh();
            txtPorcInteres.EditValue = "0";
            txtPorcInteres.Refresh();
            dtFecPagoCuota.EditValue = null;
            dtFecVecCuota.EditValue = null;
            txtValorCuotas.EditValue = "0";
            txtValorCuotas.Refresh();
            lkpCuotas.EditValue = null;
            txtFraccion.EditValue = "0";
            txtFraccion.Refresh();
        }

        private void BloqueoControlesInformacionVenta(bool Enabled, bool ReadOnly, bool Editable, bool Disabled)
        {
            //grdbContFinan.ReadOnly = Disabled;
            dtFecVecCuota.ReadOnly = ReadOnly;
            dtFecPagoCuota.ReadOnly = ReadOnly;
            lkpCuotas.ReadOnly = ReadOnly;
            txtCuoInicial.ReadOnly = ReadOnly;

            txtDescuentoPorc.ReadOnly = Disabled;

            dtFecPagoContado.ReadOnly = Editable;
            //dtFecVecPagoContado.ReadOnly = Editable;

            //txtFraccion.ReadOnly = Disabled;


            //lkpMoneda.ReadOnly = Enabled;
            //txtValorMostrar.ReadOnly = Enabled;
            //dtFecPagoCuota.ReadOnly = Enabled;
            txtPorcInteres.ReadOnly = Enabled;
            txtDescuentoSol.ReadOnly = Enabled;
            txtPreTerreno.ReadOnly = Enabled;
            txtValorCuotas.ReadOnly = Enabled;
            txtPreFinalDescuento.ReadOnly = Enabled;
            txtPrecioFinalFinanciar.ReadOnly = Enabled;

        }

        private Boolean validarCadenaVacio(string valor)
        {
            if (valor == null)
            {
                return true;
            }
            if (valor == "")
            {
                return true;
            }
            return false;
        }

        private void BloqueoControlesInformacionTerreno(bool Enabled, bool ReadOnly, bool Editable)
        {
            lkpEtapa.ReadOnly = ReadOnly;
            lkpManzana.ReadOnly = ReadOnly;
            lkpLote.ReadOnly = ReadOnly;
            btnBuscarLote.Enabled = Editable;
            txtAreaM2.ReadOnly = Enabled;
            txtprcAreaUE.ReadOnly = Enabled;

        }

        private void LimpiarCamposFormaPago()
        {
            txtDescuentoPorc.EditValue = "0";
            txtDescuentoPorc.Refresh();
            if (!validarCadenaVacio(txtPreTerreno.Text.Trim()))
            {
                txtPreFinalDescuento.EditValue = Convert.ToDecimal(txtPreTerreno.Text.Trim()) > 0 ? txtPreTerreno.Text.Trim() : "";
            }
            else
            {
                txtPreFinalDescuento.EditValue = "";
            }
            num_cuotas = 0;
            txtDescuentoSol.EditValue = "0";
            txtDescuentoSol.Refresh();
            txtCuoInicial.EditValue = "0";
            txtCuoInicial.Refresh();
            txtPrecioFinalFinanciar.EditValue = "0";
            txtPrecioFinalFinanciar.Refresh();
            txtPorcInteres.EditValue = "0";
            txtPorcInteres.Refresh();
            txtImpInteres.EditValue = "0";
            txtImpInteres.Refresh();
            dtFecPagoCuota.EditValue = null;
            //dtFecVecSeparacion.EditValue = DateTime.Today.AddDays(campo_dias.num_dias_venc_sep);
            dtFecVecCuota.EditValue = null;
            dtFecPagoContado.EditValue = null;
            //dtFecPagoSeparacion.EditValue = null;
            //dtFecVecCuota.EditValue = grdbContFinan.SelectedIndex == 0 ? Convert.ToDateTime("") : DateTime.Today; //DateTime.Now;
            //eDirec.cod_departamento = lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString();
            txtValorCuotas.EditValue = "0";
            txtValorCuotas.Refresh();
            lkpCuotas.EditValue = null;
            //txtSeparacion.EditValue = "0";
            //txtSeparacion.Refresh();
            txtFraccion.EditValue = "0";
            txtFraccion.Refresh();
            chkFinanciado.Enabled = true;
            chkContado.Enabled = true;
        }

        private void chkFinanciado_CheckStateChanged(object sender, EventArgs e)
        {
            chkFinanciado.Properties.Appearance.Font = chkFinanciado.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
            chkContado.CheckState = chkFinanciado.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
            if (chkFinanciado.CheckState == CheckState.Checked)
            {
                if (validarCadenaVacio(txtCodSepara.Text))
                {
                    AsignarDatosDetalle(cod_forma_pago.ToString());
                }
                cod_forma_pago = "FI";

                //LimpiarCamposFormaPago();
                dtFecPagoContado.EditValue = null;
                BloqueoControlesInformacionVenta(true, false, true, false);
            }

        }

        private void gvDocumentos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvDocumentos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvObsSeparaciones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvObsSeparaciones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void chkConfiguracion_CheckStateChanged(object sender, EventArgs e)
        {
            chkConfiguracion.Properties.Appearance.Font = chkConfiguracion.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
            if (chkConfiguracion.CheckState == CheckState.Unchecked)
            {
                limpiarConfiguracionCronograma();
                DesactivarConfiguracion();
                if (MiAccion == Contratos.Nuevo)
                {
                    AgregarDetalleCuotas();
                }
                else
                {
                    gvFinanciamiento_FocusedRowChanged(gvFinanciamiento, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gvFinanciamiento.FocusedRowHandle - 1, gvFinanciamiento.FocusedRowHandle));
                }
            }
            else
            {
                ActivarConfiguracion();
            }
        }

        private void gvDetalleCuotas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvDetalleCuotas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvFinanciamiento_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvFinanciamiento_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvAdenda_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvAdenda_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnGrabarCuotas_Click(object sender, EventArgs e)
        {

            //if (validarCuotas == 0)
            //{
            //    return;
            //}
            //if (validarCuotas == 1)
            //{
            //    mensajeCUOTAS += filaPintadaTablaColor($"CUOTA {pruebita}", "1,200.00", "11/12/2022");
            //    pruebita++;
            //    validarCuotas = 0;
            //    mmObservacion.Text = mensajeCUOTAS;
            //    return;
            //}
            string mensaje = validarCrnograma();
            if (mensaje != null)
            {
                MessageBox.Show(mensaje, "Generar Cronograma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var result = HNGMessageBox.Show("Actualizar importe de cuotas pendientes o posteriores.\n\nSeleccionar opción.",
               "Generar Cronograma", "Pendientes", "Posteriores", "¡Importe de Cuotas!");
            if (result == DialogResult.Yes)
            {
                //Posterior
                GenerarCronograma(2);
            }
            if (result == DialogResult.OK)
            {
                //Posterior
                GenerarCronograma(1);
            }


            //METODO DE LEYENDA
            //foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotasHTML)
            //{
            //    if (obj.num_cuota > 0)
            //    {
            //        mensajeCUOTAS += filaPintadaTablaBlanco(obj.dsc_periodo, obj.num_cuota, obj.dsc_cuota, obj.dsc_cuotas, obj.dsc_vct_cuota);

            //    }
            //}

            //probandoHTML = "";
            //List<eMemoDes> eMemoria = new List<eMemoDes>();
            //eMemoria = unit.Proyectos.ListarMemoriaDesc<eMemoDes>("2", cod_proyecto, "00003");
            //string textoXML = "", sApertura = "", sCierre = "";
            //textoXML = eMemoria[0].dsc_descripcion_html;
            //int nPosA = -1; int nPosB = -1;
            //if (nPosA == -1) { nPosA = textoXML.IndexOf("<tr style="); sApertura = "<tr style="; }
            //if (nPosB == -1) { nPosB = textoXML.IndexOf(">eliminar&nbsp;</span></p></td></tr>"); sCierre = ">eliminar&nbsp;</span></p></td></tr>"; }
            //string textP1 = textoXML.Substring(nPosA + 20, (nPosB - nPosA) + sCierre.Length - 20);

            //if (nPosA > 1) { nPosA = textP1.IndexOf("<tr style="); sApertura = "<tr style="; }
            //if (nPosB > 1) { nPosB = textP1.IndexOf(">eliminar&nbsp;</span></p></td></tr>"); sCierre = ">eliminar&nbsp;</span></p></td></tr>"; }
            //probandoHTML = textP1.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);

            //mmObservacion.Text = eMemoria[0].dsc_descripcion_html.Replace(probandoHTML, mensajeCUOTAS);

            //RichEditControl edit = new RichEditControl();
            //edit.HtmlText = eMemoria[0].dsc_descripcion_html.Replace(probandoHTML, mensajeCUOTAS);

            //edit.ShowPrintPreview();
            //edit.Dispose();
        }

        private string filaPintadaTablaBlanco(string periodo = "", int validatCuota = 0, string DesCuota = "", string ImpCuota = "", string FecVenPago = "")
        {
            string stilo1 = "", stilo2 = "", stilo3 = "", stilo4 = "", stilo5 = "", stilo6 = "", stilo7 = "", stilo8 = "", stilo9 = "", stilo10 = "", stilo11 = "", stilo12 = "";
            stilo1 = validatCuota % 2 == 0 ? "cs1D639760" : "cs2A40E650";
            stilo2 = validatCuota % 2 == 0 ? "csD88ED368" : "csD88ED368";
            stilo3 = validatCuota % 2 == 0 ? "csDC4A80" : "csDC4A80";
            stilo4 = validatCuota % 2 == 0 ? "cs13B55570" : "cs1969CE44";
            stilo5 = validatCuota % 2 == 0 ? "cs1159AD24" : "cs1159AD24";
            stilo6 = validatCuota % 2 == 0 ? "cs1B16EEB5" : "cs1B16EEB5";
            stilo7 = validatCuota % 2 == 0 ? "csFB2A2743" : "cs63F8FB84";
            stilo8 = validatCuota % 2 == 0 ? "csD88ED368" : "csD88ED368";
            stilo9 = validatCuota % 2 == 0 ? "cs1B16EEB5" : "cs1B16EEB5";


            string filaPintada = "";

            filaPintada = "<tr style=\"height: 18.05pt;\">" +
                $"<td class= \"{stilo1}\" valign=\"top\">" +
                $"<p class=\"{stilo2}\"><span class=\"{stilo3}\">{DesCuota}&nbsp;</span></p>" +
                "</td>" +
                $"<td class= \"{stilo4}\" valign=\"top\">" +
                $"<p class=\"{stilo5}\"><span class=\"{stilo6}\">{ImpCuota}&nbsp;</span></p>" +
                "</td>" +
                $"<td class=\"{stilo7}\" valign=\"top\">" +
                $"<p class=\"{stilo8}\"><span class=\"{stilo9}\"> {FecVenPago}</span></p>" +
                "</td>" +

                "</tr>";


            return filaPintada;
        }

        private string filaPintadaTablaColor(string DesCuota = "", string ImpCuota = "", string FecVenPago = "")
        {
            string filaPintada = "";
            filaPintada = "<tr style=\"height: 17.75pt;\">" +
                "<td class= \"cs39FE1BC3\" valign=\"top\">" +
                $"<p class=\"cs2E86D3A6\"><span class=\"csDC4A80\">{DesCuota}&nbsp;</span></p>" +
                "</td>" +
                "<td class=\"csA1043B91\" valign=\"top\">" +
                $"<p class=\"cs2E86D3A6\"><span class=\"cs1B16EEB5\">{ImpCuota}</span></p>" +
                "</td>" +
                 "<td class=\"csE7E90245\" valign=\"top\">" +
                $"<p class=\"cs2E86D3A6\"><span class=\"cs1B16EEB5\">{ImpCuota}</span></p>" +
                "</td>" +
                "<td class=\"csDD4F2A31\" valign=\"top\">" +
                $"<p class=\"cs2E86D3A6\"><span class=\"cs1B16EEB5\">{FecVenPago}&nbsp;</span></p>" +
                "</td>" +
                "</tr>";


            return filaPintada;
        }


        private string validarCrnograma()
        {
            string mensaje = null;

            if (Convert.ToInt32(txtCuotaDesde.Text) == 0)
            {
                txtCuotaDesde.Focus();
                return "La \"Cuota Desde\" no puede ser 0";
            }

            if (Convert.ToInt32(txtCuotaHasta.Text) == 0)
            {
                txtCuotaHasta.Focus();
                return "La \"Cuota Hasta\" no puede ser 0";

            }

            if (Convert.ToInt32(txtCuotaHasta.Text) >= 0)
            {
                if (Convert.ToInt32(txtCuotaHasta.Text.Trim()) <= Convert.ToInt32(txtCuotaDesde.Text.Trim()))
                {
                    txtCuotaHasta.EditValue = Convert.ToString(Convert.ToInt32(txtCuotaDesde.Text.Trim()) + 1);
                    txtCuotaHasta.Refresh();
                    txtCuotaHasta.Focus();
                    return "La \"Cuota Hasta\" no puede ser menor a la cuota desde";
                }
            }

            if (Convert.ToDecimal(txtImpCuotaConf.Text) == 0)
            {
                txtImpCuotaConf.Focus();
                return "El  \"Importe Cuota\" no puede ser 0";

            }



            return mensaje;

        }

        private void OcultarCamposFinanciados()
        {
            //lblVentaFinanciada.Text = "Datos de la Venta Financiado";
            lytCUOI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lytFchPagoCUOI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lytSaldoFina.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lytNumCuo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lytNumCuotas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lytInteres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lytValorCuotas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lytFecVecCuo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lytFchPagoContado.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private void OcultarCamposContado()
        {
            //lblVentaFinanciada.Text = "Datos de la Venta al Contado";
            lytCUOI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lytFchPagoCUOI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytSaldoFina.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytNumCuo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytNumCuotas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytInteres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lytValorCuotas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytFecVecCuo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lytFchPagoContado.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

        }

        private void CargarCombos()
        {
            string accion = MiAccion == Contratos.Nuevo ? "1" : "2";
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "DI001", valorDefecto: true);
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumentoCopropietario, "cod_tipo_documento", "dsc_tipo_documento", "DI001", valorDefecto: true);

            unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", lkpEstadoCivil, "cod_estado_civil", "dsc_estado_civil", "01", valorDefecto: true);
            unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", lkpEstadoCivilCopropietario, "cod_estado_civil", "dsc_estado_civil", "01", valorDefecto: true);
            //unit.Clientes.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "00001"); //lkpPais.EditValue = "00001";
            unit.Clientes.CargaCombosLookUp("Vendedor", lkpAsesor, "cod_asesor", "dsc_asesor", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario, cod_condicion: accion);
            //unit.Proyectos.CargaCombosLookUp("TipoMoneda", lkpMoneda, "cod_moneda", "dsc_simbolo", "SOL");
            unit.Proyectos.CargaCombosLookUp("Cuotas", lkpCuotas, "cod_cuotas", "dsc_cuotas", "", cod_uno: cod_proyecto);
            lkpCuotasDE.Properties.DataSource = lkpCuotas.Properties.DataSource;
            lkpCuotasDE.Properties.ValueMember = lkpCuotas.Properties.ValueMember;
            lkpCuotasDE.Properties.DisplayMember = lkpCuotas.Properties.DisplayMember;
            lkpCuotasDE.EditValue = lkpCuotas.EditValue;
            unit.Proyectos.CargaCombosLookUp("TipoContrato", lkpTipoContrato, "cod_tipo_contrato", "dsc_tipo_contrato", "TC001", valorDefecto: true);
            unit.Clientes.CargaCombosLookUp("EtapasFiltroProyecto", lkpEtapa, "cod_etapa", "dsc_descripcion", "", valorDefecto: true, codigo: cod_proyecto, codigoMultiple: cod_etapas);

            //unit.Clientes.CargaCombosLookUp("TipoEstadoSeparacion", lkpEstadoSeparacion, "cod_estado_separacion", "dsc_Nombre", "", cod_condicion:"1" , valorDefecto: true);
            if (MiAccion == Contratos.Nuevo)
            {
                picAnteriorSeparacion.Enabled = false; picSiguienteSeparacion.Enabled = false; //btnNuevo.Enabled = false;
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            //tabla = unit.Clientes.ObtenerListadoGridLookup(nCombo);
            tabla = unit.Proyectos.ObtenerListadoGridLookup(nCombo, codigo: cod_proyecto, codigoMultiple: cod_etapas);
            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            //if (campoValueMember == "cod_etapa")
            //{
            //    DataRow filaTabla = tabla.Rows[0];
            //    lkpEtapa.EditValue = filaTabla[0];
            //}
        }

        private void lkpManzana_EditValueChanged(object sender, EventArgs e)
        {
            lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtprcAreaUE.Text = "";
            txtPreTerreno.Text = "";
            if (lkpManzana.EditValue != null)
            {
                if (MiAccion == Contratos.Nuevo && !Separaciones)
                {
                    unit.Proyectos.CargaCombosLookUp("LoteXmanza", lkpLote, "cod_lote", "num_lote", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: cod_proyecto, cod_tres: lkpManzana.EditValue.ToString(), cod_condicion: "8", valorDefecto: true);
                }
                else
                {
                    unit.Proyectos.CargaCombosLookUp("LoteXmanza", lkpLote, "cod_lote", "num_lote", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: cod_proyecto, cod_tres: lkpManzana.EditValue.ToString(), cod_cuatro: cod_lote, cod_condicion: "9", valorDefecto: true);
                }
                //unit.Proyectos.CargaCombosLookUp("LoteXmanza", lkpLote, "cod_tipo_lote", "num_lote", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: codigo, cod_tres: lkpManzana.EditValue.ToString(), cod_condicion: "8", valorDefecto: true);
            }
        }

        private void lkpLote_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpLote.EditValue != null)
            {
                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    txtAreaM2.Text = dataRow["num_area_uex"].ToString();
                    txtPreTerreno.Text = dataRow["imp_precio_total_valor"].ToString();
                    double valor = Convert.ToDouble(dataRow["prc_uso_exclusivo"]);
                    txtprcAreaUE.EditValue = Math.Round(valor, 6);
                    //chkActivoSeparacion.CheckState = eLotSep.flg_activo == "SI" ? CheckState.Checked : CheckState.Unchecked;


                    //txtSaldoFinanciar.Text = dataRow["imp_precio_total"].ToString();
                    cod_lote = dataRow["cod_lote"].ToString();
                    cod_empresa = dataRow["cod_empresa"].ToString();
                    txtDescuentoPorc.EditValue = "0";
                    txtDescuentoPorc.Refresh();
                    txtDescuentoSol.EditValue = "0";
                    txtDescuentoSol.Refresh();
                    //txtSeparacion.Text = "";
                    txtCuoInicial.EditValue = "0";
                    lkpCuotas.EditValue = null;
                    lkpCuotasDE.EditValue = null;
                    txtValorCuotas.EditValue = "0";
                    //if (chkFinanciado.CheckState == CheckState.Checked)
                    //{
                    //    calcularPrecioFinal();
                    //    calcularSaldoFinanciar();
                    //}
                    //else
                    //{
                    //    calcularPrecioFinal();
                    //}

                    if (MiAccion == Contratos.Nuevo && txtCodSepara.Text.Trim() == "")
                    {
                        if (Convert.ToDecimal(txtPreTerreno.EditValue.ToString().Replace("S/", "")) != 0)
                        {
                            frmDetProforma frm = new frmDetProforma();
                            frm.cod_proyecto = cod_proyecto;
                            frm.montoFinal = Convert.ToDecimal(txtPreTerreno.EditValue.ToString().Replace("S/", ""));
                            frm.ShowDialog();
                            if (frm.objDetalle != null && frm.objDetalle.dsc_nombre_detalle != null)
                            {
                                chkFinanciado.CheckState = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? CheckState.Unchecked : CheckState.Checked;
                                chkContado.CheckState = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? CheckState.Checked : CheckState.Unchecked;
                                lytImpSepFI.Visibility = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                lytFchSepFI.Visibility = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                simpleLabelItem3.Visibility = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                if (chkContado.CheckState == CheckState.Checked)
                                {
                                    lytImpSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    lytFchSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    lytPenPag.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                }
                                else
                                {
                                    lytImpSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    lytFchSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    lytPenPag.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                }

                                txtDescuentoPorc.EditValue = frm.objDetalle.prc_descuento;
                                txtDescuentoSol.EditValue = frm.objDetalle.imp_descuento;
                                txtSeparacionFI.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? 0 : frm.objDetalle.imp_separacion;
                                txtSeparacionCO.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? frm.objDetalle.imp_separacion : 0;
                                txtCuoInicial.EditValue = frm.objDetalle.imp_cuota_inicial;
                                txtCuoInicialDE.EditValue = txtCuoInicial.EditValue;
                                txtPreFinalDescuento.EditValue = frm.objDetalle.imp_precio_final;
                                txtPorcInteres.EditValue = frm.objDetalle.prc_interes;
                                txtPorcInteresDE.EditValue = frm.objDetalle.prc_interes;
                                txtImpInteres.EditValue = frm.objDetalle.imp_interes.ToString();
                                txtImpInteresDE.EditValue = txtImpInteres.EditValue;
                                txtPrecioFinalFinanciar.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? 0 : (frm.objDetalle.imp_precio_final - frm.objDetalle.imp_cuota_inicial - frm.objDetalle.imp_separacion) + frm.objDetalle.imp_interes;
                                txtPendientePago.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? (frm.objDetalle.imp_precio_final - frm.objDetalle.imp_cuota_inicial - frm.objDetalle.imp_separacion) + frm.objDetalle.imp_interes : 0;
                                
                                lkpCuotas.EditValue = frm.objDetalle.cod_variable;
                                lkpCuotasDE.EditValue = lkpCuotas.EditValue;
                                num_cuotas = frm.objDetalle.num_fraccion;
                                txtFraccion.EditValue = frm.objDetalle.num_fraccion < 12 ? frm.objDetalle.num_fraccion : 0;
                                txtValorCuotas.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? 0 : Math.Round(frm.objDetalle.imp_valor_cuota, 2);
                                //txtFraccion.Tag = frm.objDetalle.num_fraccion;
                                
                                if (frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado"))
                                {
                                    dtFecPagoContado_EditValueChanged(dtFecPagoContado, new EventArgs());
                                }
                                else
                                {
                                    dtFecVecCuota_EditValueChanged(dtFecVecCuota, new EventArgs());
                                }
                                dtFechaSeparacionFI.ReadOnly = Convert.ToDecimal(txtSeparacionFI.EditValue) > 0 ? false : true;
                                dtFechaSeparacionCO.ReadOnly = Convert.ToDecimal(txtSeparacionCO.EditValue) > 0 ? false : true;
                            }
                        }
                    }
                    if (validarCadenaVacio(txtCodSepara.Text))
                    {
                        AsignarDatosDetalle(cod_forma_pago.ToString());
                    }
                }

            }

        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = Contratos.Nuevo;
            LimpiarCamposContratos();
            LimpiarCamposSeparacion();
            Inicializar();
        }

        private void LimpiarCamposContratos()
        {
            btnFirma.Enabled = false;
            //btnResuel.Enabled = false;
            btnAnular.Enabled = false;
            btnFormatoContrato.Enabled = false;
            barActEstado.Enabled = false;
            btnVerSeparacion.Enabled = false;
            btnBuscarSeparacion.Enabled = true;
            //xtpageDetalleFi.PageEnabled = false;
            txtCodContrato.Text = "";
            lkpTipoContrato.EditValue = null;
            txtUsuarioRegistro.Text = "";
            dtFechaRegistro.EditValue = null;
            txtCodSepara.Text = "";
            lkpAsesor.EditValue = null;
            lytFirmado.ImageOptions.Image = imgUnCheck;
            dtFirmado.EditValue = null;
            lytbolEmitida.ImageOptions.Image = imgUnCheck;
            dtbolEmitida.EditValue = null;
            lytRecepcionado.ImageOptions.Image = imgUnCheck;
            dtRecepcionado.EditValue = null;
            lytResuelto.ImageOptions.Image = imgUnCheck;
            dtResuelto.EditValue = null;
            lytAnulado.ImageOptions.Image = imgUnCheck;
            dtAnulado.EditValue = null;
            validateFormClose = 0;
        }

        private void btnVerCli_Click(object sender, EventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente();
                frm.cod_cliente = txtCodigoCliente.Text;
                frm.MiAccion = Cliente.Vista;
                frm.cod_proyecto_titulo = cod_proyecto;
                //frm.dsc_proyecto_titulo = dsc_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVerCopro_Click(object sender, EventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente();
                frm.cod_cliente = txtCodigoCopropietario.Text;
                frm.MiAccion = Cliente.Vista;
                frm.cod_proyecto_titulo = cod_proyecto;
                //frm.dsc_proyecto_titulo = dsc_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscarCopropietario_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones(null, this);
                frm.MiAccion = ListCliSeparacion.Nuevo;
                frm.codigo_proyecto = cod_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.dsc_proyecto = dsc_proyecto;
                frm.copropietario = true;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones(null, this);
                frm.MiAccion = ListCliSeparacion.Nuevo;
                frm.codigo_proyecto = cod_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.dsc_proyecto = dsc_proyecto;
                frm.cliente = true;
                frm.titular = true;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDescuentoPorc_EditValueChanged(object sender, EventArgs e)
        {
            //if (lkpLote.EditValue != null && Convert.ToDecimal(txtDescuentoPorc.EditValue) >= 0)
            //{
            //    if (Convert.ToDecimal(txtDescuentoPorc.EditValue) > 1)
            //    {
            //        txtDescuentoPorc.EditValue = "0";
            //        txtDescuentoPorc.Refresh();
            //    }
            //    calcularPrecioFinal();
            //    if (chkFinanciado.CheckState == CheckState.Checked)
            //    {
            //        calcularSaldoFinanciar();
            //        calcularValorCuotas();
            //    }
            //    else
            //    {
            //        decimal calcular = 0;
            //        calcular = Convert.ToDecimal(txtPreTerreno.Text) * (Convert.ToDecimal(txtDescuentoPorc.Text.Trim().Replace("%", "")) / 100);
            //        txtDescuentoSol.Text = calcular.ToString();
            //    }
            //    if (validarCadenaVacio(txtCodSepara.Text))
            //    {
            //        AsignarDatosDetalle(cod_forma_pago.ToString());
            //    }
            //    if (validarCadenaVacio(txtCodSepara.Text))
            //    {
            //        AgregarDetalleCuotas();

            //    }
            //    //calcularValorCuotasFraccion();
            //}
        }

        private void calcularValorCuotas()
        {
            //if(txtNumCuotas.Text.ToString() != "0")
            //{

            if (lkpCuotas.EditValue != null && chkFinanciado.CheckState == CheckState.Checked)
            {
                decimal calcularCuotas = 0, interes = 0;
                //numCuotas = Convert.ToDecimal(num_cuotas);
                if (num_cuotas != 0)
                {

                    calcularCuotas = Convert.ToDecimal(txtPrecioFinalFinanciar.Text.ToString()) / num_cuotas;
                    txtValorCuotas.Text = calcularCuotas.ToString();
                }
                else
                {
                    txtValorCuotas.EditValue = "0";
                }
            }
            //calcularCuotas = Convert.ToDecimal(txtPreFinal.Text.ToString()) / Convert.ToDecimal(txtNumCuotas.Text.ToString());

            //}


        }

        private void txtCuoInicial_EditValueChanged(object sender, EventArgs e)
        {
            //if (lkpLote.EditValue != null && Convert.ToDecimal(txtCuoInicial.EditValue) > 0)
            //{
            //    if (chkFinanciado.CheckState == CheckState.Checked)
            //    {
            //        //if (!extension)
            //        //{
            //        calcularSaldoFinanciar();
            //        calcularValorCuotas();
            //        //}
            //    }
            //    if (validarCadenaVacio(txtCodSepara.Text))
            //    {
            //        AsignarDatosDetalle(cod_forma_pago.ToString());
            //    }
            //    if (validarCadenaVacio(txtCodSepara.Text))
            //    {
            //        AgregarDetalleCuotas();

            //    }
            //    //calcularSaldoFinanciar();
            //    //calcularValorCuotas();
            //    //calcularValorCuotasFraccion();
            //}
        }

        private void lkpCuotas_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpCuotas.EditValue != null)
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    if (Convert.ToInt32(dataRow["num_cuotas"]) > 11)
                    {
                        num_cuotas = Convert.ToInt32(dataRow["num_cuotas"]);
                        txtFraccion.EditValue = 0;
                        //txtFraccion.ReadOnly = true;
                    }
                    //else
                    //{
                    //    txtFraccion.ReadOnly = false;
                    //    //num_cuotas = Convert.ToInt32(dataRow["num_cuotas"]);
                    //    //txtFraccion.Text = num_cuotas.ToString();
                    //}


                    //txtPorcInteres.EditValue = dataRow["prc_interes"].ToString();
                    ////calcularInteres();
                    //if (chkFinanciado.CheckState == CheckState.Checked)
                    //{
                    //    calcularSaldoFinanciar();
                    //    calcularValorCuotas();
                    //}

                    //if (validarCadenaVacio(txtCodSepara.Text))
                    //{
                    //    AsignarDatosDetalle(cod_forma_pago.ToString());
                    //}

                    //if (validarCadenaVacio(txtCodSepara.Text))
                    //{
                    //    AgregarDetalleCuotas();

                    //}

                }

            }
        }

        private void lkpContacto_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpContacto.EditValue != null)
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    txtTelef.Text = dataRow["dsc_celular_contacto"].ToString();
                    txtCorreo.Text = dataRow["dsc_email_contacto"].ToString();
                }
            }
        }

        private void lkpContactoCo_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpContactoCo.EditValue != null)
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    txtTelefCopropietario.Text = dataRow["dsc_celular_contacto"].ToString();
                    txtCorreoCopropietario.Text = dataRow["dsc_email_contacto"].ToString();
                }
            }
        }

        private void btnBuscarLote_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones();
                frm.MiAccion = ListCliSeparacion.Nuevo;

                frm.cotizacion = true;
                frm.codigo_proyecto = cod_proyecto;
                frm.cod_empresa = "";
                frm.dsc_proyecto = "";
                frm.ShowDialog();

                if (frm.dsc_lote != "")
                {
                    lkpEtapa.EditValue = null;
                    lkpEtapa.EditValue = frm.cod_etapa;
                    lkpManzana.EditValue = null;
                    lkpManzana.EditValue = frm.cod_manzana;
                    lkpLote.EditValue = null;
                    lkpLote.EditValue = frm.cod_lote;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string GuardarAdenda()
        {
            string result = "";
            eContratos.eContratos_Adenda_Financiamiento eLoSep = AsignarValoresContratosAdenda();
            eLoSep = unit.Proyectos.MantenimientoContratosAdenda<eContratos.eContratos_Adenda_Financiamiento>("1", eLoSep);
            if (eLoSep != null)
            {
                //num_adenda = eLoSep.num_adenda;
                result = "OK";
            }
            return result;
        }

        private string GuardarFinanciamiento()
        {
            string result = "";
            eContratos.eContratos_Adenda_Financiamiento eLoSep = AsignarValoresContratosFinanciamiento();
            eLoSep = unit.Proyectos.MantenimientoContratosFinanciamiento<eContratos.eContratos_Adenda_Financiamiento>("1", eLoSep);
            if (eLoSep != null)
            {
                //num_financiamiento = eLoSep.num_financiamiento;
                result = "OK";
            }
            return result;
        }

        private string GuardarDetalleCuotas()
        {
            string result = "";

            for (int i = 0; i < num_cuotas + 1; i++)
            {
                eContratos.eContratos_Adenda_Financiamiento eLoSep = AsignarValoresContratosDetalleCuotas(i);
                eLoSep = unit.Proyectos.MantenimientoContratosDetalleCuotas<eContratos.eContratos_Adenda_Financiamiento>("1", eLoSep);
                if (eLoSep != null)
                {
                    //num_financiamiento = eLoSep.num_financiamiento;
                    result = "OK";
                }
            }

            return result;
        }

        private string GuardarDetalleCuotasLista()
        {
            string result = "";
            int i = 0;
            eContratos.eContratos_Adenda_Financiamiento eLoSep = new eContratos.eContratos_Adenda_Financiamiento();
            foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotas)
            {
                obj.cod_contrato = txtCodContrato.Text.Trim();
                obj.cod_proyecto = cod_proyecto.Trim();
                obj.cod_empresa = cod_empresa.Trim();
                obj.num_financiamiento = 0;
                obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario.Trim();
                eLoSep = unit.Proyectos.MantenimientoContratosDetalleCuotas<eContratos.eContratos_Adenda_Financiamiento>("1", obj);
                if (eLoSep != null)
                {
                    //num_financiamiento = eLoSep.num_financiamiento;
                    result = "OK";
                }
            }

            return result;
        }



        private string Guardar()
        {
            string result = "";
            eContratos eLoSep = AsignarValoresContratos();
            eLoSep = unit.Proyectos.MantenimientoContratos<eContratos>(eLoSep);
            if (eLoSep != null)
            {
                cod_contrato = eLoSep.cod_contrato;
                txtCodContrato.Text = cod_contrato;
                result = "OK";
            }
            return result;
        }

        private string Modificar()
        {
            string result = "";
            eContratos eLoSep = AsignarValoresContratos();
            eLoSep = unit.Proyectos.MantenimientoContratos<eContratos>(eLoSep);
            if (eLoSep != null)
            {
                cod_contrato = eLoSep.cod_contrato;
                txtCodContrato.Text = cod_contrato;
                result = "OK";
            }
            return result;
        }
        private eContratos AsignarValoresContratos()
        {
            eContratos eLoSep = new eContratos();
            eLoSep.cod_contrato = txtCodContrato.Text.Trim();
            eLoSep.cod_separacion = txtCodSepara.Text.Trim();
            eLoSep.cod_tipo_contrato = lkpTipoContrato.EditValue.ToString();
            eLoSep.cod_asesor = lkpAsesor.EditValue != null ? lkpAsesor.EditValue.ToString() : "";
            eLoSep.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario.Trim();
            eLoSep.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario.Trim();
            eLoSep.fch_emitido = dtEmitido.EditValue == null || dtEmitido.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtEmitido.EditValue.ToString());
            eLoSep.cod_cliente = txtCodigoCliente.Text; // cod_cliente != null || cod_cliente.Trim() != "" ? cod_cliente.ToString().Trim() : "";
            eLoSep.cod_copropietario = txtCodigoCopropietario.Text;
            eLoSep.dsc_linea_contacto = lkpContacto.EditValue.ToString();
            eLoSep.dsc_linea_contacto_copro = lkpContactoCo.EditValue != null ? lkpContactoCo.EditValue.ToString() : "";
            eLoSep.cod_etapa = lkpEtapa.EditValue.ToString();
            eLoSep.cod_manzana = lkpManzana.EditValue.ToString();
            eLoSep.cod_lote = cod_lote.Trim();
            eLoSep.num_area_uex = Convert.ToDecimal(txtAreaM2.Text);
            eLoSep.cod_forma_pago = chkFinanciado.CheckState == CheckState.Checked ? "FI" : "CO"; //grdbEstado.EditValue.ToString();
            eLoSep.imp_precio_lista = Convert.ToDecimal(txtPreTerreno.Text.ToString());
            eLoSep.prc_descuento = Convert.ToDecimal(txtDescuentoPorc.Text.Trim().Replace("%", "")) / 100;
            eLoSep.imp_descuento = Convert.ToDecimal(txtDescuentoSol.Text);
            eLoSep.imp_precio_venta_final = Convert.ToDecimal(txtPreFinalDescuento.Text.Trim());
            eLoSep.imp_separacion = chkFinanciado.CheckState == CheckState.Checked ? Convert.ToDecimal(txtSeparacionFI.Text) : Convert.ToDecimal(txtSeparacionCO.Text);
            eLoSep.cod_empresa = cod_empresa.Trim();
            eLoSep.cod_proyecto = cod_proyecto.Trim();
            eLoSep.imp_cuota_inicial = Convert.ToDecimal(txtCuoInicial.Text);
            eLoSep.fch_pago_cuota = dtFecPagoCuota.EditValue == null || dtFecPagoCuota.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecPagoCuota.EditValue.ToString());
            eLoSep.imp_saldo_financiar = Convert.ToDecimal(txtPrecioFinalFinanciar.Text);
            if (chkFinanciado.CheckState == CheckState.Checked)
            {
                eLoSep.cod_cuotas = lkpCuotas.EditValue.ToString();
            }
            eLoSep.num_fraccion = Convert.ToInt32(txtFraccion.Text);
            eLoSep.imp_valor_cuota = Convert.ToDecimal(txtValorCuotas.Text.ToString());
            eLoSep.fch_vct_cuota = dtFecVecCuota.EditValue == null || dtFecVecCuota.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecVecCuota.EditValue);
            eLoSep.imp_pendiente_pago = Convert.ToDecimal(txtPendientePago.Text.Trim());
            eLoSep.fch_pago_contado = dtFecPagoContado.EditValue == null || dtFecPagoContado.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecPagoContado.EditValue);
            eLoSep.prc_interes = Convert.ToDecimal(txtPorcInteres.EditValue);
            eLoSep.imp_interes = Convert.ToDecimal(txtImpInteres.EditValue);

            return eLoSep;
        }

        private eContratos.eContratos_Adenda_Financiamiento AsignarValoresContratosAdenda()
        {
            eContratos.eContratos_Adenda_Financiamiento eLoSep = new eContratos.eContratos_Adenda_Financiamiento();
            eLoSep.cod_contrato = txtCodContrato.Text.Trim();
            eLoSep.cod_proyecto = cod_proyecto.Trim();
            eLoSep.cod_empresa = cod_empresa.Trim();
            eLoSep.num_adenda = 0;
            eLoSep.cod_tipo_adenda = lkpTipoContrato.EditValue.ToString();
            eLoSep.cod_cliente = txtCodigoCliente.Text; // cod_cliente != null || cod_cliente.Trim() != "" ? cod_cliente.ToString().Trim() : "";
            eLoSep.cod_lote = cod_lote.Trim();
            eLoSep.cod_etapa = lkpEtapa.EditValue.ToString();
            eLoSep.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario.Trim();

            return eLoSep;
        }

        private eContratos.eContratos_Adenda_Financiamiento AsignarValoresContratosFinanciamiento()
        {
            eContratos.eContratos_Adenda_Financiamiento eLoSep = new eContratos.eContratos_Adenda_Financiamiento();
            eContratos.eContratos_Adenda_Financiamiento obj = gvAdenda.GetFocusedRow() as eContratos.eContratos_Adenda_Financiamiento;

            eLoSep.cod_contrato = txtCodContrato.Text.Trim();
            eLoSep.cod_proyecto = cod_proyecto.Trim();
            eLoSep.cod_empresa = cod_empresa.Trim();
            eLoSep.num_adenda = 0;
            eLoSep.num_financiamiento = 0;
            eLoSep.cod_cuotas = lkpCuotas.EditValue != null ? lkpCuotas.EditValue.ToString() : "0";
            eLoSep.num_fraccion = Convert.ToInt32(txtFraccion.Text);
            eLoSep.imp_saldo_financiar = Convert.ToDecimal(txtPrecioFinalFinanciar.Text);
            eLoSep.fch_pago_cuota = dtFecPagoCuota.EditValue == null || dtFecPagoCuota.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecPagoCuota.EditValue.ToString());
            eLoSep.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario.Trim();
            return eLoSep;
        }

        private eContratos.eContratos_Adenda_Financiamiento AsignarValoresContratosDetalleCuotas(int num_cuotas = 0)
        {
            eContratos.eContratos_Adenda_Financiamiento eLoSep = new eContratos.eContratos_Adenda_Financiamiento();
            eContratos.eContratos_Adenda_Financiamiento obj = gvFinanciamiento.GetFocusedRow() as eContratos.eContratos_Adenda_Financiamiento;
            DateTime dateVencimiento = (DateTime)dtFecVecCuota.EditValue;
            eLoSep.cod_contrato = txtCodContrato.Text.Trim();
            eLoSep.cod_proyecto = cod_proyecto.Trim();
            eLoSep.cod_empresa = cod_empresa.Trim();
            eLoSep.num_financiamiento = 0;
            eLoSep.num_cuota = num_cuotas;
            eLoSep.fch_vct_cuota = num_cuotas == 0 ? dateVencimiento : dateVencimiento.AddMonths(num_cuotas);
            if (chkFinanciado.CheckState == CheckState.Checked)
            {
                eLoSep.imp_cuotas = num_cuotas == 0 ? Convert.ToDecimal(txtSeparacionFI.Text) + Convert.ToDecimal(txtCuoInicial.Text) : Convert.ToDecimal(txtValorCuotas.Text);
            }
            if (chkContado.CheckState == CheckState.Checked)
            {
                eLoSep.imp_cuotas = Convert.ToDecimal(txtPreFinalDescuento.Text);
            }
            eLoSep.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario.Trim();
            return eLoSep;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string mensaje = validarCampos();
                if (mensaje == null)
                {
                    //string NroDoc = ValidarLongitudDocumento();
                    //int Anho = DateTime.Now.Year - Convert.ToDateTime(dtFecNacimiento.EditValue).Year;
                    string result = "";
                    switch (MiAccion)
                    {
                        case Contratos.Nuevo: result = Guardar(); break;
                        case Contratos.Editar: result = Modificar(); break;
                    }
                    //if (copropietario || MiAccion == Contratos.Vista) { result = Modificar(); }
                    if (result == "OK")
                    {
                        MessageBox.Show("Se guardó el contrato de manera satisfactoria", "Guardar contrato", MessageBoxButtons.OK);
                        validateFormClose = 1;
                        lytEmitido.ImageOptions.Image = imgCheck;
                        dtEmitido.ReadOnly = true;
                        btnBolEmitir.Enabled = false;
                        btnFirma.Enabled = false;
                        btnAnular.Enabled = true;
                        btnFormatoContrato.Enabled = true;
                        lkpTipoContrato.ReadOnly = true;
                        //xtpageDetalleFi.PageEnabled = true;
                        if (MiAccion == Contratos.Nuevo)
                        {
                            MiAccion = Contratos.Editar;
                            string resulAdenda = "";
                            resulAdenda = GuardarAdenda();
                            if (resulAdenda == "OK")
                            {
                                ObtenerDatos_AdendaContratos();

                                string resulAdenda2 = "";
                                resulAdenda2 = GuardarFinanciamiento();
                                if (resulAdenda2 == "OK")
                                {
                                    ObtenerDatos_FinanciamientoContratos();
                                    string resulAdenda3 = "";
                                    resulAdenda3 = GuardarDetalleCuotasLista();
                                    //resulAdenda3 = GuardarDetalleCuotas();

                                    if (resulAdenda3 == "OK")
                                    {
                                        ObtenerDatos_DetalleCuotasContratos();
                                    }
                                }
                            }
                        }
                        else
                        {
                            string resulAdenda3 = "";
                            resulAdenda3 = GuardarDetalleCuotasLista();
                            //resulAdenda3 = GuardarDetalleCuotas();
                            if (resulAdenda3 == "OK")
                            {
                                ObtenerDatos_DetalleCuotasContratos();
                            }
                        }
                    }
                    eContratos eLotSep = new eContratos();
                    eLotSep = unit.Proyectos.ObtenerContratos<eContratos>("2", cod_proyecto, codigoMultiple, cod_contrato);
                    if (eLotSep == null) { return; }
                    eLotCon = eLotSep;
                    AsignarDatosDetalle(eLotSep.cod_forma_pago.ToString());

                    if (validarCadenaVacio(eLotSep.cod_separacion))
                    {
                        //   string resultado = unit.Proyectos.Actualizar_Status_Separacion("2", eLotSep.cod_separacion, eLotSep.cod_proyecto, eLotSep.cod_etapa, eLotSep.cod_lote, Program.Sesion.Usuario.cod_usuario);
                        //    if (resultado == "OK")
                        //    {
                        //        btnBuscarSeparacion.Enabled = false;
                        //    }
                        //}
                        //else
                        //{
                        string resultado2 = unit.Proyectos.Actualizar_Lote_Contrato(eLotSep.cod_lote, eLotSep.cod_etapa, eLotSep.cod_proyecto, Program.Sesion.Usuario.cod_usuario);
                        if (resultado2 == "OK")
                        {
                            if (!validarCadenaVacio(eLotSep.cod_cliente)) { btnBuscarCliente.Enabled = false; btnVerCli.Enabled = true; }
                            if (!validarCadenaVacio(eLotSep.cod_copropietario)) { btnBuscarCopropietario.Enabled = false; btnVerCopro.Enabled = true; }
                            btnBuscarSeparacion.Enabled = false;
                            chkContado.Enabled = false;
                            chkFinanciado.Enabled = false;
                            BloqueoControlesInformacionTerreno(true, true, false);
                            BloqueoControlesInformacionVenta(true, true, true, true);
                        }
                        //}
                    }
                    string mensaje2 = GuardarObservaciones();
                    if (mensaje2 != null)
                    {
                        ObtenerDatos_ObservacionesContratos();
                    }
                    CargarListadoDocumentos("3");
                    btnBuscarSeparacion.Enabled = false;
                    
                }
                else
                {
                    XtraMessageBox.Show(mensaje, "Guardar separación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtnNuevo_Click(object sender, EventArgs e)
        {
            AgregarDocContrato();
        }

        private void gvDocumentos_Click(object sender, EventArgs e)
        {
            //eContratos.eContratos_Documentos oListMemoDesc = mylistDocumentos.Find(x => x.cod_documento_contrato == "0" && x.dsc_nombre_doc == "" || x.dsc_nombre_doc == null);
            //if (oListMemoDesc != null)
            //{
            //    mylistDocumentos.Remove(oListMemoDesc);
            //    gvDocumentos.RefreshData();
            //}
        }

        private void gvDocumentos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eContratos.eContratos_Documentos obj = gvDocumentos.GetFocusedRow() as eContratos.eContratos_Documentos;
                    cod_documento_contrato = obj.cod_documento_contrato;
                    coldsc_nombre_doc_ref.OptionsColumn.AllowEdit = true;
                    //colnum_orden_doc.OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvDocumentos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                int nRow = e.RowHandle;
                eContratos.eContratos_Documentos objDoc = new eContratos.eContratos_Documentos();
                eContratos.eContratos_Documentos obj = gvDocumentos.GetFocusedRow() as eContratos.eContratos_Documentos;
                if (obj == null || obj.cod_documento_contrato == "0") { return; }
                if (e.Column.FieldName == "dsc_nombre_doc_ref" && e.Value != null)
                {
                    coldsc_nombre_doc_ref.OptionsColumn.AllowEdit = false;
                    //colnum_orden_doc.OptionsColumn.AllowEdit = false; 
                    //cod_documento_contrato = "";

                    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    objDoc = unit.Proyectos.Mantenimiento_Documento_Contrato<eContratos.eContratos_Documentos>(obj);
                    if (objDoc == null) { MessageBox.Show("Error al agregar el documento", "Documento Separación", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    gvDocumentos.RefreshData();
                    CargarListadoDocumentos("3");
                    //obtenerListadoTipoDocumentoXSeparacion();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void rbtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                eContratos.eContratos_Documentos oListSepDoc = gvDocumentos.GetFocusedRow() as eContratos.eContratos_Documentos;
                if (oListSepDoc.cod_documento_contrato == "0")
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

                        string result = unit.Proyectos.Eliminar_TipoDocumento("3", oListSepDoc.cod_documento_contrato, cod_contrato, cod_proyecto, oListSepDoc.flg_PDF);
                        if (result == "OK")
                        {
                            string result2 = unit.Proyectos.Actualizar_Status_Contrato("5", cod_contrato, cod_proyecto,"", DateTime.Now);
                            if (result2 != "OK")
                            {
                                MessageBox.Show("No se pudo actualizar el estado del contrato \"" + result2 + "\"", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el tipo documento \"" + oListSepDoc.dsc_nombre_doc + "\"", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        gvDocumentos.RefreshData();
                        CargarListadoDocumentos("3");
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

        private string GuardarObservaciones()
        {
            txtNroDocumento.Focus();
            txtNroDocumento.Select();
            gvObsContratos.PostEditor();
            gvObsContratos.RefreshData();
            eContratos.eContratos_Observaciones eObsFact = new eContratos.eContratos_Observaciones();

            for (int y = 0; y < gvObsContratos.DataRowCount; y++)
            {
                eContratos.eContratos_Observaciones obj = gvObsContratos.GetRow(y) as eContratos.eContratos_Observaciones;
                if (obj == null) continue;
                obj.cod_contrato = cod_contrato; obj.cod_proyecto = cod_proyecto; obj.cod_empresa = cod_empresa; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                eObsFact = unit.Proyectos.InsertarObservacionesContratos<eContratos.eContratos_Observaciones>(obj);
            }

            if (eObsFact != null) { return "OK"; }

            return null;


        }

        private void ObtenerDatos_ObservacionesContratos()
        {
            try
            {
                mylistObservaciones = unit.Proyectos.Obtener_LineasDetalleContrato<eContratos.eContratos_Observaciones>(1, cod_contrato, cod_proyecto);
                bsLotesContratos.DataSource = mylistObservaciones;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_AdendaContratos()
        {
            try
            {
                mylistAdenda = unit.Proyectos.ObtenerListadoAdenda<eContratos.eContratos_Adenda_Financiamiento>(cod_contrato, cod_proyecto);
                bsLotesAdenda.DataSource = mylistAdenda;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ObtenerDatos_FinanciamientoContratos(int num_adenda = 0)
        {
            try
            {
                eContratos.eContratos_Adenda_Financiamiento obj = gvAdenda.GetFocusedRow() as eContratos.eContratos_Adenda_Financiamiento;

                mylistFinanciamiento = unit.Proyectos.ObtenerListadoFinanciamiento<eContratos.eContratos_Adenda_Financiamiento>(cod_contrato, cod_proyecto, num_adenda);
                bsLotesFinanciamiento.DataSource = mylistFinanciamiento;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ObtenerDatos_DetalleCuotasContratos(int num_financiamiento = 0)
        {
            try
            {
                mylistDetalleCuotas = unit.Proyectos.ObtenerListadoDetalleCuotas<eContratos.eContratos_Adenda_Financiamiento>("1", cod_contrato, cod_proyecto, num_financiamiento);
                bsLotesDetalleCuotas.DataSource = mylistDetalleCuotas;
                mylistDetalleCuotasHTML = unit.Proyectos.ObtenerListadoDetalleCuotas<eContratos.eContratos_Adenda_Financiamiento>("2", cod_contrato, cod_proyecto, num_financiamiento);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void gvObsSeparaciones_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eContratos.eContratos_Observaciones obj = gvObsContratos.GetFocusedRow() as eContratos.eContratos_Observaciones;
            if (obj == null)
            {
                mylistObservaciones.Add(new eContratos.eContratos_Observaciones() { num_linea = 0 });
                bsLotesContratos.DataSource = mylistObservaciones;
                obj = gvObsContratos.GetFocusedRow() as eContratos.eContratos_Observaciones;
            }
            obj.fch_registro = DateTime.Today; obj.dsc_usuario_registro = Program.Sesion.Usuario.dsc_usuario;
            gvObsContratos.RefreshData();
        }

        private void rbtnEliminarObservacion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                eContratos.eContratos_Observaciones obj = gvObsContratos.GetFocusedRow() as eContratos.eContratos_Observaciones;
                if (obj == null) return;
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta observación?", "Eliminar observación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eContratos.eContratos_Observaciones eObs = gvObsContratos.GetFocusedRow() as eContratos.eContratos_Observaciones;
                    string result = unit.Proyectos.Eliminar_ContratosObservaciones(cod_contrato, cod_proyecto, eObs.num_linea);
                    ObtenerDatos_ObservacionesContratos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picAnteriorSeparacion_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposContratos();
                LimpiarCamposSeparacion();
                int tRow = frmHandler.gvListaContratos.RowCount - 1;
                int nRow = frmHandler.gvListaContratos.FocusedRowHandle;
                frmHandler.gvListaContratos.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;
                eContratos obj = frmHandler.gvListaContratos.GetFocusedRow() as eContratos;
                cod_proyecto = obj.cod_proyecto;
                cod_contrato = obj.cod_contrato;
                codigoMultiple = obj.cod_etapa;
                flg_activo = obj.flg_activo;
                MiAccion = obj.flg_firmado == "SI" || obj.flg_activo == "NO" ? Contratos.Vista : Contratos.Editar;

                Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFirmado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("¿Firmar contrato?", "Firmar Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                eContratos eLotSep = new eContratos();
                eLotSep = unit.Proyectos.ObtenerContratos<eContratos>("2", cod_proyecto, codigoMultiple, cod_contrato);

                DateTime date = dtFirmado.EditValue == null ? DateTime.Now : Convert.ToDateTime(dtFirmado.EditValue);

                string result = unit.Proyectos.Actualizar_Status_Contrato("1", cod_contrato, cod_proyecto,"", date);
                if (result == "OK")
                {
                    XtraMessageBox.Show("Contrato firmado.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string resultado = unit.Proyectos.Actualizar_Status_Separacion("2", eLotSep.cod_separacion, eLotSep.cod_proyecto, eLotSep.cod_etapa, eLotSep.cod_lote, Program.Sesion.Usuario.cod_usuario);
                    if (resultado != "OK")
                    {
                        XtraMessageBox.Show(resultado, "No se actualizó la separación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    btnAnular.Enabled = false;
                    btnFirma.Enabled = false;
                    btnRecepcionado.Enabled = true;
                    lytFirmado.ImageOptions.Image = imgCheck;
                    dtFirmado.EditValue = dtFirmado.EditValue == null ? DateTime.Now : dtFirmado.EditValue;
                    validateFormClose = 1;
                }
            }

        }

        private void btnResuelto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("¿Resolver contrato?", "Resolver Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                string result = unit.Proyectos.Actualizar_Status_Contrato("2", cod_contrato, cod_proyecto, cod_separacion, DateTime.Now);
                if (result == "OK")
                {
                    XtraMessageBox.Show("Contrato resuelto.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnAnular.Enabled = false;
                    btnResuelto.Enabled = false;
                    lytResuelto.ImageOptions.Image = imgCheck;
                    dtResuelto.EditValue = DateTime.Now;
                }
            }
        }

        private void gvAdenda_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            eContratos.eContratos_Adenda_Financiamiento obj = gvAdenda.GetFocusedRow() as eContratos.eContratos_Adenda_Financiamiento;
            if (obj == null) { return; }

            ObtenerDatos_FinanciamientoContratos(obj.num_adenda);

            if (MiAccion == Contratos.Nuevo) { AgregarFinanciado(); }
        }

        private void gvFinanciamiento_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            eContratos.eContratos_Adenda_Financiamiento obj = gvFinanciamiento.GetFocusedRow() as eContratos.eContratos_Adenda_Financiamiento;
            if (obj == null) { return; }
            if (MiAccion != Contratos.Nuevo)
            {
                ObtenerDatos_DetalleCuotasContratos(obj.num_financiamiento);
            }

        }

        private void gvDetalleCuotas_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            decimal sum_cuotas = 0, sum_cuotas2 = 0, sum_cuotas3 = 0;
            if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "imp_cuotas")
            {
                foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotas)
                {
                    if (obj.num_cuota != 0 && chkFinanciado.CheckState == CheckState.Checked)
                    {
                        sum_cuotas += obj.imp_cuotas;
                    }
                    else if (chkContado.CheckState == CheckState.Checked)
                    {
                        sum_cuotas += obj.imp_cuotas;
                    }
                }
                e.TotalValue = sum_cuotas;
            }
            if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "imp_cuo_sin_interes")
            {
                foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotas)
                {
                    if (obj.num_cuota != 0 && chkFinanciado.CheckState == CheckState.Checked)
                    {
                        sum_cuotas2 += Math.Round(obj.imp_cuo_sin_interes, 2);
                    }
                    else if (chkContado.CheckState == CheckState.Checked)
                    {
                        sum_cuotas2 += Math.Round(obj.imp_cuo_sin_interes, 2);
                    }
                }
                e.TotalValue = sum_cuotas2;
            }
            if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "imp_interes")
            {
                foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotas)
                {
                    if (obj.num_cuota != 0 && chkFinanciado.CheckState == CheckState.Checked)
                    {
                        sum_cuotas3 += Math.Round(obj.imp_interes, 2);
                    }
                    else if (chkContado.CheckState == CheckState.Checked)
                    {
                        sum_cuotas3 += Math.Round(obj.imp_interes, 2);
                    }
                }
                e.TotalValue = sum_cuotas3;
            }
        }

        private void limpiarConfiguracionCronograma()
        {
            txtCuotaDesde.EditValue = "0";
            txtCuotaDesde.Refresh();
            txtCuotaHasta.EditValue = "0";
            txtCuotaHasta.Refresh();
            txtImpCuotaConf.EditValue = "0";
            txtImpCuotaConf.Refresh();
            mmObservacion.Text = "";

        }

        private void btnAnulado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("¿Anular contrato?", "Anular Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                string result = unit.Proyectos.Actualizar_Status_Contrato("3", cod_contrato, cod_proyecto, cod_separacion,DateTime.Now);
                if (result == "OK")
                {
                    XtraMessageBox.Show("Contrato Anulado.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnAnular.Enabled = false;
                    btnFirma.Enabled = false;
                    lytAnulado.ImageOptions.Image = imgCheck;
                    dtAnulado.EditValue = DateTime.Now;
                }
            }

        }

        private void dtFecVecCuota_EditValueChanged(object sender, EventArgs e)
        {

            if (validarCadenaVacio(txtCodSepara.Text) && dtFecVecCuota.EditValue != null && dtFecPagoCuota.EditValue != null)
            {
                AgregarDetalleCuotas();

            }

        }

        private void txtFraccion_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txtFraccion.Text) > 0)
            //{
            //    if (Convert.ToInt32(txtFraccion.Text.Trim()) <= 10)
            //    {

            //        num_cuotas = Convert.ToInt32(txtFraccion.Text.Trim());
            //        calcularValorCuotas();

            //    }
            //    else
            //    {
            //        txtFraccion.EditValue = "1";
            //        txtFraccion.Refresh();
            //        num_cuotas = Convert.ToInt32(txtFraccion.Text.Trim());
            //        calcularValorCuotas();

            //    }
            //}
            //if (lkpCuotas.EditValue != null && lkpCuotas.EditValue.ToString() == "EFI0006")
            //{
            //    txtFraccion.ReadOnly = false;
            //}
            //else
            //{
            //    txtFraccion.ReadOnly = true;

            //}
            //if (validarCadenaVacio(txtCodSepara.Text))
            //{
            //    AsignarDatosDetalle(cod_forma_pago.ToString());
            //}
            //if (validarCadenaVacio(txtCodSepara.Text))
            //{
            //    AgregarDetalleCuotas();

            //}
        }

        private void picSiguienteSeparacion_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposContratos();
                LimpiarCamposSeparacion();
                int tRow = frmHandler.gvListaContratos.RowCount - 1;
                int nRow = frmHandler.gvListaContratos.FocusedRowHandle;
                frmHandler.gvListaContratos.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eContratos obj = frmHandler.gvListaContratos.GetFocusedRow() as eContratos;
                cod_proyecto = obj.cod_proyecto;
                cod_contrato = obj.cod_contrato;
                codigoMultiple = obj.cod_etapa;
                flg_activo = obj.flg_activo;
                MiAccion = obj.flg_firmado == "SI" || obj.flg_activo == "NO" ? Contratos.Vista : Contratos.Editar;
                Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMantContratos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }



        private void dtFecPagoCuota_EditValueChanged(object sender, EventArgs e)
        {
            if (validarCadenaVacio(txtCodSepara.Text) && dtFecVecCuota.EditValue != null && dtFecPagoCuota.EditValue != null)
            {
                AgregarDetalleCuotas();
            }
        }

        private void gvDocumentos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eContratos.eContratos_Documentos eDocumentos = gvDocumentos.GetRow(e.FocusedRowHandle) as eContratos.eContratos_Documentos;
                    if (eDocumentos == null) { return; }
                    if (eDocumentos.cod_documento_contrato == cod_documento_contrato)
                    {
                        coldsc_nombre_doc_ref.OptionsColumn.AllowEdit = true;
                        //colnum_orden_doc.OptionsColumn.AllowEdit = true;

                    }
                    else
                    {
                        coldsc_nombre_doc_ref.OptionsColumn.AllowEdit = false;
                        //colnum_orden_doc.OptionsColumn.AllowEdit = false;
                        cod_documento_contrato = "";
                    }
                    gvDocumentos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCuotaDesde_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCuotaDesde.Text) >= 0)
            {
                if (Convert.ToInt32(txtCuotaDesde.Text.Trim()) > num_cuotas)
                {
                    txtCuotaDesde.EditValue = "0";
                    txtCuotaDesde.Refresh();
                }
            }
        }

        private void txtCuotaHasta_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCuotaHasta.Text) >= 0)
            {
                if (Convert.ToInt32(txtCuotaHasta.Text.Trim()) > num_cuotas)
                {
                    txtCuotaHasta.EditValue = "0";
                    txtCuotaHasta.Refresh();
                }
            }
        }

        private void dtFecPagoContado_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpLote.EditValue == null) { return; }
            if (validarCadenaVacio(txtCodSepara.Text) && dtFecPagoContado.EditValue != null)
            {
                AgregarDetalleCuotas();
            }
        }

        private async void rbtnAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                eContratos.eContratos_Documentos obj = gvDocumentos.GetFocusedRow() as eContratos.eContratos_Documentos;
                if (obj == null) { return; }
                await AdjuntarDocumentosVarios(obj);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
        private async Task AdjuntarDocumentosVarios(eContratos.eContratos_Documentos eSepDoc, string nombreDocAdicional = "")
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
                    if (TamañoDoc < 18000)
                    {
                        varPathOrigen = myFileDialog.FileName;
                        //varNombreArchivo = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd") + "."+ Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivo = eSepDoc.dsc_nombre_doc + "-" + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + (nombreDocAdicional != "" ? " " + nombreDocAdicional : "") + "-" + eSepDoc.num_orden_doc + Path.GetExtension(myFileDialog.SafeFileName);
                        //varNombreArchivoSinExtension = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd");
                        Extension = Path.GetExtension(myFileDialog.SafeFileName);
                    }
                    else
                    {
                        MessageBox.Show("Solo puede subir archivos hasta 4MB de tamaño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                    }
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Adjuntando documento...", "Cargando...");
                    //SplashScreen.Open("Por favor espere...", "Cargando...");
                    //SplashScreen.Open("Adjuntando documento...", "Cargando...");
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

                    eLotCon = unit.Proyectos.ObtenerContratos<eContratos>("2", cod_proyecto, codigoMultiple, cod_contrato);
                    //eLotSep.idCarpeta_separacion;
                    if (eLotCon.idCarpeta_contrato == null || eLotCon.idCarpeta_contrato == "")
                    {
                        var driveItem = new Microsoft.Graph.DriveItem
                        {
                            //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                            Name = eLotCon.dsc_documento + " " + eLotCon.dsc_nombre.ToUpper(),
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
                        IdCarpetaCliente = eLotCon.idCarpeta_contrato;
                    }
                    if (eSepDoc.idPDF != null && eSepDoc.idPDF != "") { await Mover_Eliminar_ArchivoOneDrive(); }
                    //crea archivo en el OneDrive
                    byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                    using (Stream stream = new MemoryStream(data))
                    {
                        string res = "";
                        var DriveItem = await GraphClient.Me.Drive.Items[IdCarpetaCliente].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                        idArchivoPDF = DriveItem.Id;

                        eSepDoc.cod_contrato = cod_contrato;
                        eSepDoc.cod_empresa = cod_empresa;
                        eSepDoc.cod_proyecto = cod_proyecto;
                        eSepDoc.flg_PDF = "SI";
                        eSepDoc.idPDF = idArchivoPDF;
                        eSepDoc.dsc_nombre_doc = varNombreArchivo;
                        eSepDoc.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                        if (eLotCon.idCarpeta_contrato == null || eLotCon.idCarpeta_contrato == "")
                        {
                            eLotCon.idCarpeta_contrato = IdCarpetaCliente;
                            eContratos objLotSep = unit.Proyectos.MantenimientoContratos<eContratos>(eLotCon);
                        }
                        eContratos.eContratos_Documentos resdoc = unit.Proyectos.Mantenimiento_documento_contratos<eContratos.eContratos_Documentos>(eSepDoc);
                        if (resdoc != null)
                        {
                            mensajito = "Se registró el documento satisfactoriamente";

                            CargarListadoDocumentos("3");
                            btnFirma.Enabled = true;

                        }
                        else
                        {
                            mensajito = "Hubieron problemas al registrar el documento";
                            //MessageBox.Show("Hubieron problemas al registrar el documento", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    //SplashScreen.Close();
                    SplashScreenManager.CloseForm(false);

                    MessageBox.Show(mensajito, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                //SplashScreen.Close();
                SplashScreenManager.CloseForm(false);

                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async Task Mover_Eliminar_ArchivoOneDrive()
        {
            try
            {
                eContratos.eContratos_Documentos obj = new eContratos.eContratos_Documentos();
                //    if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                //    {
                obj = gvDocumentos.GetFocusedRow() as eContratos.eContratos_Documentos;
                string IdCarpetaCliente = "", Extension = "";
                if (obj == null) { return; }

                ////eFacturaProveedor obj = gvFacturasProveedor.GetRow(nRow) as eFacturaProveedor;
                //obj.periodo_tributario = FechaPeriodo.ToString("MM-yyyy");
                //if (/*gvFacturasProveedor.SelectedRowsCount == 1 && */(obj.periodo_tributario == null || obj.periodo_tributario == "")) { MessageBox.Show("Debe asignar un periodo tributario para mover los archivos adjuntos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //if (obj.periodo_tributario == null || obj.periodo_tributario == "") return;
                //string dsc_Carpeta = glkpTipoDocumento.EditValue.ToString() == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
                //dsc_Carpeta = CajaChica == "SI" ? "Caja Chica" : EntregaRendir == "SI" ? "Entrega Rendir" : dsc_Carpeta;
                //int Anho = Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)); int Mes = Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)); string NombreMes = Convert.ToDateTime(obj.periodo_tributario).ToString("MMMM");
                //string IdArchivoAnho = "", IdArchivoMes = "";
                ////varNombreArchivo = obj.NombreArchivo;
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
                eLotCon = unit.Proyectos.ObtenerContratos<eContratos>("2", cod_proyecto, codigoMultiple, cod_contrato);
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
                //}
                //else //Si existe folder obtener id
                //{
                //    IdCarpetaCliente = eLotSep.idCarpeta_separacion;
                //}

                //eFacturaProveedor IdCarpetaAnho = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);

                //var targetItemFolderIdAnho = IdArchivoAnho;

                //eFacturaProveedor IdCarpetaMes = blFact.ObtenerDatosOneDrive<eFacturaProveedor>(14, lkpEmpresaProveedor.EditValue.ToString(), Mes: Convert.ToDateTime(dtFechaRegistro.EditValue).Month);

                await GraphClient.Me.Drive.Items[obj.idPDF].Request().DeleteAsync();
                //if (opcion == "ELIMINAR") await GraphClient.Directory.DeletedItems[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                throw;
            }
        }

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

        private void btnRecepcionado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("¿Recepcionar contrato?", "Recepcionar Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                DateTime date = dtRecepcionado.EditValue == null ? DateTime.Now : Convert.ToDateTime(dtRecepcionado.EditValue);

                string result = unit.Proyectos.Actualizar_Status_Contrato("4", cod_contrato, cod_proyecto, "", date);
                if (result == "OK")
                {
                    XtraMessageBox.Show("Contrato recepcionado.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnAnular.Enabled = false;
                    btnResuel.Enabled = true;
                    btnRecepcionado.Enabled = false;
                    lytRecepcionado.ImageOptions.Image = imgCheck;
                    dtRecepcionado.EditValue = dtRecepcionado.EditValue == null ? DateTime.Now : dtRecepcionado.EditValue;
                    validateFormClose = 1;
                }
            }
        }

        private async Task VerDocumentosVarios(string nombreDocAdicional = "")
        {
            try
            {

                eContratos.eContratos_Documentos obj = new eContratos.eContratos_Documentos();


                //    if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                //    {
                obj = gvDocumentos.GetFocusedRow() as eContratos.eContratos_Documentos;
                if (obj == null || obj.cod_documento_contrato == "0") { return; }



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
                        //SplashScreen.Open("Abriendo documento...", "Cargando...");
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                        //SplashScreen.Open("Abriendo documento", "Cargando...");
                        //eEmpresa eEmp = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
                        //eEmpresa eEmpre = unit.Proyectos.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
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
                        SplashScreenManager.CloseForm(false);

                        //SplashScreen.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SplashScreenManager.CloseForm(false);

                        //SplashScreen.Close();

                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {

                //SplashScreen.Close();
                SplashScreenManager.CloseForm(false);

                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtFechaSeparacionCO_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpLote.EditValue == null) { return; }
            if (validarCadenaVacio(txtCodSepara.Text) && dtFechaSeparacionCO.EditValue != null)
            {
                AgregarDetalleCuotas();
            }
        }

        private void dtFechaSeparacionFI_EditValueChanged(object sender, EventArgs e)
        {
            if (validarCadenaVacio(txtCodSepara.Text) && dtFecVecCuota.EditValue != null && dtFecPagoCuota.EditValue != null)
            {
                AgregarDetalleCuotas();
            }
        }

        private void frmMantContratos_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmHandler != null && validateFormClose == 1)
            {
                frmHandler.frmListadoContratos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
            }
        }

        private void btnBolEmitir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("¿Contrato con Boleta Emitida?", "Boleta Emitida Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                eContratos eLotSep = new eContratos();
                eLotSep = unit.Proyectos.ObtenerContratos<eContratos>("2", cod_proyecto, codigoMultiple, cod_contrato);

                DateTime date = dtbolEmitida.EditValue == null ? DateTime.Now : Convert.ToDateTime(dtbolEmitida.EditValue);

                string result = unit.Proyectos.Actualizar_Status_Contrato("6", cod_contrato, cod_proyecto, "", date);
                if (result == "OK")
                {
                    XtraMessageBox.Show("Contrato con Boleta Emitida.", "Boleta Emitida Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnBolEmitir.Enabled = false;
                    btnAnular.Enabled = false;
                    btnFirma.Enabled = true;
                    dtFirmado.ReadOnly = false;
                    btnRecepcionado.Enabled = false;
                    lytbolEmitida.ImageOptions.Image = imgCheck;
                    dtbolEmitida.EditValue = dtbolEmitida.EditValue == null ? DateTime.Now : dtbolEmitida.EditValue;
                    validateFormClose = 1;
                }
            }
        }

        private string CodigoDelFormato()
        {
            if ((eLotCon.cod_estadocivil != "02" || (eLotCon.cod_estadocivil == "02" && eLotCon.flg_bienes_separados == "SI")) && validarCadenaVacio(eLotCon.cod_estadocivil_CO))
            {
                return "00002";
            }
            if (eLotCon.cod_estadocivil == "02" && eLotCon.flg_bienes_separados == "NO" && validarCadenaVacio(eLotCon.cod_estadocivil_CO))
            {
                return "00003";
            }
            if ((eLotCon.cod_estadocivil != "02" || (eLotCon.cod_estadocivil == "02" && eLotCon.flg_bienes_separados == "SI")) && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && (eLotCon.cod_estadocivil_CO != "02" || (eLotCon.cod_estadocivil_CO == "02" && eLotCon.flg_bienes_separados_CO == "SI")))
            {
                return "00004";
            }
            if (eLotCon.cod_estadocivil == "02" && eLotCon.flg_bienes_separados == "NO" && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && (eLotCon.cod_estadocivil_CO != "02" || (eLotCon.cod_estadocivil_CO == "02" && eLotCon.flg_bienes_separados_CO == "SI")))
            {
                return "00005";
            }
            if ((eLotCon.cod_estadocivil != "02" || (eLotCon.cod_estadocivil == "02" && eLotCon.flg_bienes_separados == "SI")) && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && eLotCon.cod_estadocivil_CO == "02" && eLotCon.flg_bienes_separados_CO == "NO")
            {
                return "00006";
            }
            if (eLotCon.cod_estadocivil == "02" && eLotCon.flg_bienes_separados == "NO" && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && eLotCon.cod_estadocivil_CO == "02" && eLotCon.flg_bienes_separados_CO == "NO")
            {
                return "00007";
            }
            return null;
        }

        private void btnFormatoContrato_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MiAccion == Contratos.Nuevo) return;
            //xtpageDetalleFi.check
            xtraTabControl1.SelectedTabPageIndex = 2;
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando contrato", "Cargando...");

            string codigoFormato = "";
            string[] cabecera = { };
            codigoFormato = CodigoDelFormato();

            if (codigoFormato == null) { SplashScreenManager.CloseForm(false); return; }


            var xml = new FormatoXmlHelper("@tabla1", codigoFormato, cod_empresa);
            if(Convert.ToDecimal(txtPorcInteres.EditValue) > 0)
            {
                cabecera = new string[] { "CUOTA", "IMPORTE CUOTA", "FECHA DE PAGO","CAPITAL","INTERES" };

            }
            else
            {
                 cabecera = new string[] { "CUOTA", "IMPORTE CUOTA", "FECHA DE PAGO" };
            }
            List<String[]> filas = new List<string[]>();

            foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotasHTML)
            {
                if (obj.num_cuota > 0)
                {
                    if (Convert.ToDecimal(txtPorcInteres.EditValue) > 0)
                    {
                        filas.Add(new string[] { obj.dsc_cuota, obj.dsc_cuotas, obj.dsc_vct_cuota, obj.imp_cuo_sin_interes.ToString(), obj.imp_interes.ToString() });


                    }
                    else
                    {
                        filas.Add(new string[] { obj.dsc_cuota, obj.dsc_cuotas, obj.dsc_vct_cuota });

                    }
                }
            }
            eReportes abc = unit.Proyectos.ObtenerFormatoContrato<eReportes>(Convert.ToInt32(codigoFormato) - 1, cod_cliente, cod_contrato, cod_proyecto);
            if (String.IsNullOrEmpty(eLotCon.cod_forma_pago))
            {
                eLotCon.cod_forma_pago = chkFinanciado.CheckState == CheckState.Checked ? "FI" : "CO";
            }
            xml.ShowReport(abc, cabecera, filas, eLotCon.cod_forma_pago, "@tabla1");
            SplashScreenManager.CloseForm(false);


            ////var abc = unit.Proyectos.ObtenerFormatoContrato<eReportes>(cod_cliente, cod_contrato, cod_proyecto);
            //probandoHTMLIMAGEN = "";



            ///* CERRANDO METODO PROBANDO */




            //RichEditControl editImagen1 = new RichEditControl();
            //editImagen1.WordMLText = abc.dsc_img_lote;
            ////txtXMLextraer2 = editImagen1.HtmlText;


            ////edit.HtmlText = txtXMLDocumento.Replace("@imagen1", "PROBANDO");
            ///


            //int nPosA3 = -1; int nPosB3 = -1; int nPosC3 = -1;
            //if (nPosA3 == -1) { nPosA3 = editTabla4.WordMLText.IndexOf("LINDEROS:"); sApertura3 = "<w:r><w:pict>"; }
            //if (nPosB3 == -1) { nPosB3 = editTabla4.WordMLText.IndexOf("LINDEROS:"); sCierre3 = "</w:pict></w:r>"; }
            //string textP3 = editTabla4.WordMLText.Substring(nPosA3, (nPosB3 - nPosA3) + sCierre3.Length + 999999);

            //if (nPosA3 > 1) { nPosA3 = textP3.IndexOf("<w:r><w:pict>"); sApertura3 = "<w:r><w:pict>"; }
            //if (nPosB3 > 1) { nPosB3 = textP3.IndexOf("</w:pict></w:r>"); sCierre3 = "</w:pict></w:r>"; }

            //probandoHTMLIMAGEN = textP3.Substring(nPosA3, (nPosB3 - nPosA3) + sCierre3.Length);




            //editTabla4.WordMLText = editTabla4.WordMLText.Replace(probandoHTMLIMAGEN, txtXMLImagen);

            //if (abc != null)
            //{
            //    var formatoContrato = new FormatoWordHelper();
            //    formatoContrato.ShowWordReportContrato<eReportes>(abc, cod_empresa, "00002", "");
            //}

        }







        //private void reemplazarTablaConHTML()          /ESTO SE USABA ANTES
        //{
        //string textoXML = "", sApertura = "", sCierre = "", txtXMLDocumento = "", txtXMLextraer2 = "", txtXMLImagen = "",
        //    sApertura2 = "", sCierre2 = "", sApertura3 = "", sCierre3 = "", sApertura4 = "", sCierre4 = "",textoXML4 = "",
        //    sApertura5 = "", sCierre5 = "", textoXML5 = "";

        //var formato = unit.FormatoDocumento.Obtener_PlantillaDeFormatos(cod_empresa: cod_empresa, cod_formato: "00002");

        //RichEditControl rec = new RichEditControl();
        //rec.WordMLText = formato.dsc_wordMLText;
        //textoXML4 = formato.dsc_wordMLText;

        //textoXML = rec.HtmlText;
        //int numDataCuotas = mylistDetalleCuotasHTML.Count;
        //int i = 1;
        //string dscFecha = "";
        //foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotasHTML)
        //{
        //    if (obj.num_cuota > 0)
        //    {
        //        mensajeCUOTAS += filaPintadaTablaBlanco(obj.dsc_periodo, obj.num_cuota, obj.dsc_cuota, obj.dsc_cuotas, obj.dsc_vct_cuota);
        //        if (i == numDataCuotas) { dscFecha = obj.dsc_vct_cuota; }
        //    }
        //    i++;
        //}

        //probandoHTML = "";

        //int nPosA = -1; int nPosB = -1;
        //if (nPosA == -1) { nPosA = textoXML.IndexOf("FECHA DE PAGO"); sApertura = "<tr style="; }
        //if (nPosB == -1) { nPosB = textoXML.IndexOf(">@tabla1</span></p></td></tr>"); sCierre = ">@tabla1</span></p></td></tr>"; }
        //string textP1 = textoXML.Substring(nPosA + 20, (nPosB - nPosA) + sCierre.Length - 20);

        //if (nPosA > 1) { nPosA = textP1.IndexOf("<tr style="); sApertura = "<tr style="; }
        //if (nPosB > 1) { nPosB = textP1.IndexOf(">@tabla1</span></p></td></tr>"); sCierre = ">@tabla1</span></p></td></tr>"; }
        //probandoHTML = textP1.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);



        //RichEditControl editTabla1 = new RichEditControl();
        //editTabla1.HtmlText = textoXML.Replace(probandoHTML, mensajeCUOTAS);
        ///* PROBANDO METODO PARA EL XML1*/
        //probandoXML1 = "";

        //int nPosA4 = -1; int nPosB4 = -1;
        //if (nPosA4 == -1) { nPosA4 = textoXML4.IndexOf("ETALLE DE CUOTAS"); sApertura4 = "<w:tbl>"; }
        //if (nPosB4 == -1) { nPosB4 = textoXML4.IndexOf(">@tabla1</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"); sCierre4 = ">@tabla1</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"; }
        //string textP4 = textoXML4.Substring(nPosA4 + 20, (nPosB4 - nPosA4) + sCierre4.Length - 20);

        //if (nPosA4 > 1) { nPosA4 = textP4.IndexOf("<w:tbl>"); sApertura4 = "<w:tbl>"; }
        //if (nPosB4 > 1) { nPosB4 = textP4.IndexOf(">@tabla1</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"); sCierre4 = ">@tabla1</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"; }
        //probandoXML1 = textP4.Substring(nPosA4, (nPosB4 - nPosA4) + sCierre4.Length);

        ///*PROBANDO CON LA TABLA YA EDITADA HTML A XML   $"<td class= \"{stilo1}\" valign=\"top\">" +*/

        //int nPosA5 = -1; int nPosB5 = -1;
        //if (nPosA5 == -1) { nPosA5 = editTabla1.WordMLText.IndexOf("ETALLE DE CUOTAS"); sApertura5 = "<w:tbl>"; }
        //if (nPosB5 == -1) { nPosB5 = editTabla1.WordMLText.IndexOf($">{dscFecha}</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"); sCierre5 = $">{dscFecha}</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"; }
        //string textP5 = editTabla1.WordMLText.Substring(nPosA5 + 20, (nPosB5 - nPosA5) + sCierre5.Length - 20);

        //if (nPosA5 > 1) { nPosA5 = textP5.IndexOf("<w:tbl>"); sApertura5 = "<w:tbl>"; }
        //if (nPosB5 > 1) { nPosB5 = textP5.IndexOf($">{dscFecha}</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"); sCierre5 = $">{dscFecha}</w:t></w:r></w:p></w:tc></w:tr></w:tbl>"; }
        //probandoXML2 = textP5.Substring(nPosA5, (nPosB5 - nPosA5) + sCierre5.Length);


        //RichEditControl editTabla4 = new RichEditControl();
        //editTabla4.WordMLText = formato.dsc_wordMLText.Replace(probandoXML1, probandoXML2);
        //}

        private void eliminarArchivosExportados()
        {
            var carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
            DirectoryInfo source = new DirectoryInfo(carpeta);
            FileInfo[] filesToCopy = source.GetFiles();
            foreach (FileInfo oFile in filesToCopy)
            {
                oFile.Delete();
            }
            //MessageBox.Show("Se procedió a eliminar los archivos exportados del sistema", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void gvDocumentos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eContratos.eContratos_Documentos obj = gvDocumentos.GetRow(e.RowHandle) as eContratos.eContratos_Documentos;
                    if (obj.flg_PDF == "SI") { /*e.Appearance.BackColor = Color.LightGreen;*/ e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CargarListadoDocumentos(string accion, string cod_memoria_desc = "")
        {
            try
            {
                int nRow = gvDocumentos.FocusedRowHandle;
                mylistDocumentos = unit.Proyectos.ListarDocumentoContratos<eContratos.eContratos_Documentos>(accion, cod_contrato, cod_proyecto);
                eLotesDocumentos.DataSource = mylistDocumentos;
                gvDocumentos.FocusedRowHandle = nRow;
                gvDocumentos_FocusedRowChanged(gvDocumentos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(nRow - 1, nRow));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public string validarCampos()
        {
            if (lkpAsesor.EditValue == null)
            {
                lkpAsesor.ShowPopup();
                return "Debe seleccionar el asesor";
            }
            if (lkpEtapa.EditValue == null)
            {
                lkpEtapa.ShowPopup();
                return "Debe seleccionar la etapa";
            }
            if (lkpManzana.EditValue == null)
            {
                lkpManzana.ShowPopup();
                return "Debe seleccionar la manzana";
            }
            if (lkpLote.EditValue == null)
            {
                lkpLote.ShowPopup();
                return "Debe seleccionar el nro. lote";
            }
            if (dtEmitido.EditValue == null)
            {
                dtEmitido.ShowPopup();
                return "Debe seleccionar la fecha de emisión";
            }
            if (txtCuoInicial.Text.Trim() == "0.00" && chkFinanciado.CheckState == CheckState.Checked)
            {
                txtCuoInicial.Focus();
                return "Debe ingresar la cuota inicial";
            }
            if (lkpCuotas.EditValue == null && chkFinanciado.CheckState == CheckState.Checked)
            {
                lkpCuotas.ShowPopup();
                return "Seleccionar N° de cuotas";
            }
            return null;
        }


        public void transferirDatosCopropietario(eCliente eLotSepCli)
        {
            txtCodigoCopropietario.Text = eLotSepCli.cod_cliente;

            //cod_prospecto = eLotSepCli.cod_prospecto;
            glkpTipoDocumentoCopropietario.EditValue = eLotSepCli.cod_tipo_documento;
            txtNroDocumentoCopropietario.Text = eLotSepCli.dsc_documento;
            //txtTelefCopropietario.Text = eLotSepCli.dsc_telefono_1;
            //txtCorreoCopropietario.Text = eLotSepCli.dsc_email;
            unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContactoCo, "num_linea_contacto_string", "dsc_nombre_contacto", "0", valorDefecto: true, codigo: eLotSepCli.cod_cliente);
            lkpContactoCo.ReadOnly = false;
            txtNombresCopropietario.Text = eLotSepCli.dsc_apellido_paterno + " " + eLotSepCli.dsc_apellido_materno + " " + eLotSepCli.dsc_nombre;
            lkpEstadoCivilCopropietario.EditValue = eLotSepCli.cod_estadocivil;
            txtDireccionCopropietario.Text = eLotSepCli.dsc_cadena_direccion;
            //eLotSep.flg_cliente = "SI";
            //lkpAsesor.EditValue = eLotSepCli.cod_asesor;
            //btnGuardar.Enabled = true;
            btnVerCopro.Enabled = true;
            btnGuardar.Enabled = true;
            copropietario = true;

        }


        public void transferirDatos(eCliente eLotSepCli)
        {
            txtCodigoCliente.Text = eLotSepCli.cod_cliente;
            cod_cliente = eLotSepCli.cod_cliente;
            glkpTipoDocumento.EditValue = eLotSepCli.cod_tipo_documento;
            txtNroDocumento.Text = eLotSepCli.dsc_documento;
            //txtTelef.Text = eLotSepCli.dsc_telefono_1;
            //txtCorreo.Text = eLotSepCli.dsc_email;
            txtNombres.Text = eLotSepCli.dsc_apellido_paterno + " " + eLotSepCli.dsc_apellido_materno + " " + eLotSepCli.dsc_nombre;
            lkpEstadoCivil.EditValue = eLotSepCli.cod_estadocivil;
            txtDireccion.Text = eLotSepCli.dsc_cadena_direccion;
            unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContacto, "num_linea_contacto_string", "dsc_nombre_contacto", "0", valorDefecto: true, codigo: eLotSepCli.cod_cliente);
            lkpContacto.ReadOnly = false;

            btnGuardar.Enabled = true;
            btnVerCli.Enabled = true;
            unit.Clientes.CargaCombosLookUp("EtapasTodas", lkpEtapa, "cod_etapa", "dsc_descripcion", "", valorDefecto: true, codigo: cod_proyecto, codigoMultiple: cod_etapas);
            BloqueoControlesInformacionTerreno(true, false, true);


        }
        private void LimpiarCamposSeparacion()
        {
            btnBuscarLote.Enabled = true;
            btnVerCli.Enabled = false;
            cod_cliente = null;
            cod_contrato = null;
            txtCodigoCliente.Text = "";
            //txtCodigoProspecto.Text = "";
            //grdbEstado.SelectedIndex = 0;
            txtCodSepara.Text = "";

            txtDescuentoPorc.EditValue = "0";
            txtDescuentoPorc.Refresh();
            lkpAsesor.EditValue = null;
            glkpTipoDocumento.EditValue = "DI001";
            txtTelef.Text = "51";
            txtCorreo.Text = "";
            txtNombres.Text = "";
            //txtApePaterno.Text = "";
            //txtApeMaterno.Text = "";
            //dtFecNacimiento.EditValue = DateTime.Today.AddYears(-18).AddDays(-1);
            lkpEstadoCivil.EditValue = "01";
            //eDirec.cod_departamento = lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString();
            //txtOcupacion.Text = "";
            txtDireccion.Text = "";
            lkpContacto.EditValue = null;
            lkpContacto.ReadOnly = true;
            btnBuscarCliente.Enabled = true;
            btnBuscarCopropietario.Enabled = true;
            lkpEtapa.ReadOnly = true;
            lkpManzana.ReadOnly = true;
            lkpLote.ReadOnly = true;
            txtprcAreaUE.ReadOnly = true;
            txtAreaM2.ReadOnly = true;
            btnVerCopro.Enabled = false;
            txtCodigoCopropietario.Text = "";
            glkpTipoDocumentoCopropietario.EditValue = "DI001";
            txtNroDocumentoCopropietario.Text = "";
            lkpEstadoCivilCopropietario.EditValue = "01";
            txtNombresCopropietario.Text = "";
            txtTelefCopropietario.Text = "51";
            txtCorreoCopropietario.Text = "";
            txtDireccionCopropietario.Text = "";
            lkpContactoCo.EditValue = null;
            lkpContactoCo.ReadOnly = true;

            lkpEtapa.EditValue = null;
            lkpManzana.Properties.DataSource = null;
            lkpLote.Properties.DataSource = null;
            lkpAsesor.ReadOnly = false;

            //lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtprcAreaUE.Text = "";
            txtPreTerreno.Text = "";
            LimpiarCamposFormaPago();
            limpiarCamposDetalleFinan();
            txtNroDocumento.Text = "";
            txtSeparacionFI.EditValue = "0";
            txtSeparacionFI.Refresh();
            txtSeparacionFI.Focus();
            lytImpSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytFchSepFI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            simpleLabelItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            spaceOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytImpSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytFchSepCO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytPenPag.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lytFecPagoCon.TextLocation = DevExpress.Utils.Locations.Top;
            lytAream.TextSize = new Size(81, 13);
            lytFecPagoCon.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.UseParentOptions;
            chkFinanciado.CheckState = CheckState.Checked;
            BloqueoControlesInformacionVenta(true, false, true, false);
            eLotesDocumentos.DataSource = null;
            gvDocumentos.RefreshData();
            bsLotesContratos.DataSource = null;
            gvObsContratos.RefreshData();
            bsLotesDetalleCuotas.DataSource = null;
            gvDetalleCuotas.RefreshData();
            if (MiAccion != Contratos.Nuevo)
            {
                bsLotesAdenda.DataSource = null;
                gvAdenda.RefreshData();
                bsLotesFinanciamiento.DataSource = null;
                gvFinanciamiento.RefreshData();
            }
        }

        private void limpiarCamposDetalleFinan()
        {
            txtSeparacionDE.EditValue = "0";
            txtSeparacionDE.Refresh();
            txtPreFinalDescuentoDE.EditValue = "0";
            txtPreFinalDescuentoDE.Refresh();
            txtCuoInicialDE.EditValue = "0";
            txtCuoInicialDE.Refresh();
            txtPrecioFinalFinanciarDE.EditValue = "0";
            txtPrecioFinalFinanciarDE.Refresh();
            txtNumCuotaEspecialDE.EditValue = "0";
            txtNumCuotaEspecialDE.Refresh();
            txtPorcInteresDE.EditValue = "0";
            txtPorcInteresDE.Refresh();
            txtImpInteresDE.EditValue = "0";
            txtImpInteresDE.Refresh();
            txtValorCuotasDE.EditValue = "0";
            txtValorCuotasDE.Refresh();
            dtFecVecCuotaDE.EditValue = null;
            dtFecVecCuotaDE.Refresh();
            lkpCuotasDE.EditValue = null;
            lkpCuotasDE.Refresh();
        }
        private void calcularSaldoFinanciar()
        {

            decimal calcular = 0, precioFinal = 0, impSeparacion = 0, cuoInicial = 0, interes = 0;

            calcular = Convert.ToDecimal(txtPreTerreno.Text.ToString()) * (Convert.ToDecimal(txtDescuentoPorc.Text.Trim().Replace("%", "")) / 100);

            if (!validarCadenaVacio(txtSeparacionFI.Text.Trim()))
            {
                impSeparacion = Convert.ToDecimal(txtSeparacionFI.Text.Trim());
            }
            if (!validarCadenaVacio(txtCuoInicial.Text.Trim()))
            {
                cuoInicial = Convert.ToDecimal(txtCuoInicial.Text.Trim());
            }

            precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcular) - impSeparacion - cuoInicial;


            //if ( != null)
            //{
            interes = precioFinal * (Convert.ToDecimal(txtPorcInteres.Text.Trim().Replace("%", "")) / 100); //Convert.ToDecimal(txtValorInteres.Text.Trim());
            //}
            precioFinal = precioFinal + interes;
            txtDescuentoSol.Text = calcular.ToString();
            //txtValorInteres.Text = interes.ToString();
            txtPrecioFinalFinanciar.Text = precioFinal.ToString();

        }

        private void btnVerSeparacion_Click(object sender, EventArgs e)
        {
            try
            {
                frmSepararLote frm = new frmSepararLote();
                frm.codigo = cod_proyecto;
                //frm.dsc_proyecto = dsc_proyecto;
                frm.cod_separacion = txtCodSepara.Text;
                frm.codigoMultiple = cod_etapas;
                frm.cod_status = cod_status;
                frm.flg_activo = flg_activo;
                frm.verContrato = true;
                //frm.cod_cliente = obj.cod_cliente;
                frm.MiAccion = Separacion.Vista;

                //frm.cod_cliente = txtCodigoCliente.Text;
                //frm.MiAccion = Separacion.Vista;
                //frm.codigo = cod_proyecto;
                //frm.dsc_proyecto_titulo = dsc_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void calcularPrecioFinal()
        {
            decimal calcularDescuentoPorc = 0, precioFinal = 0, impSeparacion = 0, cuoInicial = 0, pendientePago = 0;

            calcularDescuentoPorc = Convert.ToDecimal(txtPreTerreno.Text.ToString()) * Convert.ToDecimal(txtDescuentoPorc.EditValue);
            precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcularDescuentoPorc);

            if (chkContado.CheckState == CheckState.Checked)
            {
                pendientePago = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcularDescuentoPorc - Convert.ToDecimal(txtSeparacionCO.Text.Trim()));
            }

            //precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcular) - Convert.ToDecimal(txtSeparacion.Text.Trim()) - Convert.ToDecimal(txtCuoInicial.Text.Trim());
            txtPreFinalDescuento.Text = precioFinal.ToString();
            txtPendientePago.Text = pendientePago.ToString();

        }

        private void lkpEtapa_EditValueChanged(object sender, EventArgs e)
        {
            lkpManzana.EditValue = null;
            lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtprcAreaUE.Text = "";
            txtPreTerreno.Text = "";
            if (lkpEtapa.EditValue != null)
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null && validarCadenaVacio(cod_proyecto))
                {
                    cod_proyecto = dataRow["cod_proyecto"].ToString();
                }

                if (MiAccion == Contratos.Nuevo)
                {
                    unit.Proyectos.CargaCombosLookUp("ManzanaXEtapa", lkpManzana, "cod_manzana", "dsc_manzana", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: cod_proyecto, cod_condicion: "3", valorDefecto: true);
                }
                else
                {
                    unit.Proyectos.CargaCombosLookUp("ManzanaXEtapa", lkpManzana, "cod_manzana", "dsc_manzana", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: cod_proyecto, cod_cuatro: cod_lote, cod_condicion: "4", valorDefecto: true);

                }
            }
        }

        private void btnBuscarSeparacion_Click(object sender, EventArgs e)
        {
            try
            {
                frmAsignarSeparacion frm = new frmAsignarSeparacion(null, this);
                //frm.MiAccion = ListCliSeparacion.Nuevo;
                frm.cod_proyecto = String.IsNullOrEmpty(cod_proyecto)  && String.IsNullOrEmpty(cod_empresa) ? "00001" : cod_proyecto;
                frm.cod_empresa = String.IsNullOrEmpty(cod_proyecto) && String.IsNullOrEmpty(cod_empresa) ? "00010" : cod_empresa;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void AgregarDocContrato()
        {
            try
            {
                int numOrden = mylistDocumentos.Count() + 1;
                eContratos.eContratos_Documentos ListAddDocSep = new eContratos.eContratos_Documentos
                {
                    cod_documento_contrato = "0",
                    num_orden_doc = numOrden,
                    flg_activo = "SI"
                };
                cod_documento_contrato = ListAddDocSep.cod_documento_contrato;
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

        public void AgregarAdenda()
        {
            try
            {
                //int numOrden = mylistAdenda.Count() + 1;
                mylistAdenda = new List<eContratos.eContratos_Adenda_Financiamiento>();
                eContratos.eContratos_Adenda_Financiamiento Listadenda = new eContratos.eContratos_Adenda_Financiamiento
                {
                    num_adenda = 0,
                    dsc_tipo_adenda = "CONTRATO DE TERRENO",
                    fch_adenda = DateTime.Now
                };
                mylistAdenda.Add(Listadenda);
                bsLotesAdenda.DataSource = mylistAdenda;
                gvAdenda.RefreshData();
                gvAdenda.FocusedRowHandle = gvAdenda.RowCount - 1;

                //rtxtNombre.EnableCustomMaskTextInput(args => { args.Cancel(); return; });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void AgregarFinanciado()
        {
            try
            {
                //int numOrden = mylistAdenda.Count() + 1;
                mylistFinanciamiento = new List<eContratos.eContratos_Adenda_Financiamiento>();
                eContratos.eContratos_Adenda_Financiamiento ListaFinanciamiento = new eContratos.eContratos_Adenda_Financiamiento
                {
                    num_financiamiento = 0,
                    dsc_tipo_financiamiento = "ORIGINAL",
                    fch_financiamiento = DateTime.Now,
                    cod_usuario_registro = Program.Sesion.Usuario.dsc_usuario
                };
                mylistFinanciamiento.Add(ListaFinanciamiento);
                bsLotesFinanciamiento.DataSource = mylistFinanciamiento;
                gvFinanciamiento.RefreshData();
                gvFinanciamiento.FocusedRowHandle = gvFinanciamiento.RowCount - 1;

                //rtxtNombre.EnableCustomMaskTextInput(args => { args.Cancel(); return; });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GenerarCronograma(int accion = 0)
        {
            if (accion == 1)
            {
                int numeroModificado = mylistDetalleCuotas.Where(c => c.num_cuota != 0 && c.num_cuota >= Convert.ToInt32(txtCuotaDesde.Text) && c.num_cuota <= Convert.ToInt32(txtCuotaHasta.Text)).Count();
                int numeroSinModificar = mylistDetalleCuotas.Where(c => c.num_cuota != 0).Count() - numeroModificado;
                decimal totalImporteModificado = Convert.ToDecimal(txtImpCuotaConf.EditValue) * numeroModificado;
                decimal totalImportaAModificar = Convert.ToDecimal(txtPrecioFinalFinanciarDE.EditValue) - totalImporteModificado;
                if (totalImportaAModificar > 0)
                {
                    decimal importePorCuota = totalImportaAModificar / numeroSinModificar;
                    foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotas)
                    {
                        if (obj.num_cuota != 0 && obj.num_cuota >= Convert.ToInt32(txtCuotaDesde.Text) && obj.num_cuota <= Convert.ToInt32(txtCuotaHasta.Text))
                        {
                            obj.imp_cuotas = Convert.ToDecimal(txtImpCuotaConf.Text);
                        }
                        else if (obj.num_cuota != 0)
                        {
                            obj.imp_cuotas = importePorCuota;
                        }
                    }
                    gvDetalleCuotas.RefreshData();
                }
                else
                {
                    MessageBox.Show("Importe Seleccionado incorrecto.", "Generar Cronograma", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            if (accion == 2)
            {
                int numeroModificado = mylistDetalleCuotas.Where(c => c.num_cuota != 0 && c.num_cuota >= Convert.ToInt32(txtCuotaDesde.Text) && c.num_cuota <= Convert.ToInt32(txtCuotaHasta.Text)).Count();
                int numeroSinModificarAnteriores = mylistDetalleCuotas.Where(c => c.num_cuota != 0 && c.num_cuota < Convert.ToInt32(txtCuotaDesde.Text)).Count();
                int numeroSinModificarPosteriores = mylistDetalleCuotas.Where(c => c.num_cuota != 0 && c.num_cuota > Convert.ToInt32(txtCuotaHasta.Text)).Count();
                decimal totalImporteModificado = Convert.ToDecimal(txtImpCuotaConf.EditValue) * numeroModificado;
                decimal totalImportaAModificarAnterior = Convert.ToDecimal(txtValorCuotasDE.EditValue) * numeroSinModificarAnteriores;
                decimal totalImportaAModificarPosterior = Convert.ToDecimal(txtPrecioFinalFinanciarDE.EditValue) - totalImporteModificado - totalImportaAModificarAnterior;
                if (totalImportaAModificarPosterior > 0)
                {
                    decimal importePorCuotaPosterior = totalImportaAModificarPosterior / numeroSinModificarPosteriores;
                    foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotas)
                    {
                        if (obj.num_cuota != 0 && obj.num_cuota >= Convert.ToInt32(txtCuotaDesde.Text) && obj.num_cuota <= Convert.ToInt32(txtCuotaHasta.Text))
                        {
                            obj.imp_cuotas = Convert.ToDecimal(txtImpCuotaConf.Text);
                        }
                        else if (obj.num_cuota != 0 && obj.num_cuota > Convert.ToInt32(txtCuotaHasta.Text))
                        {
                            obj.imp_cuotas = importePorCuotaPosterior;
                        }
                    }
                    gvDetalleCuotas.RefreshData();
                }
                else
                {
                    MessageBox.Show("Importe Seleccionado incorrecto.", "Generar Cronograma", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                //mylistDetalleCuotas
            }
        }




        public void AgregarDetalleCuotas()
        {
            try
            {
                if (chkFinanciado.CheckState == CheckState.Checked)
                {
                    if (!String.IsNullOrEmpty(txtCodSepara.Text) || Convert.ToDecimal(txtSeparacionFI.EditValue) > 0)
                    {
                        if (dtFecVecCuota.EditValue == null || num_cuotas == 0 || dtFecPagoCuota.EditValue == null || dtFechaSeparacionFI.EditValue == null) { return; }
                        //int numOrden = mylistAdenda.Count() + 1;
                        mylistDetalleCuotas = new List<eContratos.eContratos_Adenda_Financiamiento>();
                        decimal importeSumatoria = 0;
                        decimal importeInteres = 0;

                        DateTime dateVencimiento = (DateTime)dtFecVecCuota.EditValue;
                        DateTime dateSep = (DateTime)dtFechaSeparacionFI.EditValue;
                        DateTime dateCUOI = (DateTime)dtFecPagoCuota.EditValue;

                        for (int i = 0; i <= num_cuotas + 1; i++)
                        {
                            importeSumatoria += i != 0 && i != 1 && i - 1 != num_cuotas ? Convert.ToDecimal(txtValorCuotas.EditValue) : 0;
                            importeInteres += i != 0 && i != 1 && i - 1 != num_cuotas ? Math.Round(Convert.ToDecimal(txtImpInteres.EditValue) / Convert.ToDecimal(num_cuotas),2) : 0;
                            eContratos.eContratos_Adenda_Financiamiento ListDetalleCuota = new eContratos.eContratos_Adenda_Financiamiento();
                            //eLoSep.num_financiamiento = 0;
                            ListDetalleCuota.num_orden_det_cuo = i + 1;
                            ListDetalleCuota.num_cuota = i == 0 ? 0 : i == 1 ? 0 : i - 1;
                            ListDetalleCuota.dsc_cuota = i == 0 ? "Separación" : i == 1 ? "Inicial" : "Cuota";
                            ListDetalleCuota.dsc_estado = "Pendiente";
                            ListDetalleCuota.fch_vct_cuota = i == 0 ? dateSep : i == 1 ? dateCUOI : dateVencimiento.Day >= 28 ? obtenerUltimoDiaDelMes(dateVencimiento.AddMonths(i - 2)) : dateVencimiento.AddMonths(i - 2);
                            //ListDetalleCuota.imp_cuotas = i - 1 == num_cuotas ? Math.Abs((Convert.ToDecimal(txtPrecioFinalFinanciar.EditValue) + (Convert.ToDecimal(txtPrecioFinalFinanciar.EditValue) * Convert.ToDecimal(txtPorcInteres.EditValue))) - Math.Round(importeSumatoria, 2)) : i == 0 ? Convert.ToDecimal(txtSeparacionFI.Text) : i == 1 ? Convert.ToDecimal(txtCuoInicial.Text) : Convert.ToDecimal(txtValorCuotas.Text);
                            ListDetalleCuota.imp_cuotas = i - 1 == num_cuotas ? Math.Abs(Convert.ToDecimal(txtPrecioFinalFinanciar.EditValue) - Math.Round(importeSumatoria, 2)) : i == 0 ? Convert.ToDecimal(txtSeparacionFI.Text) : i == 1 ? Convert.ToDecimal(txtCuoInicial.Text) : Convert.ToDecimal(txtValorCuotas.Text);
                            //ListDetalleCuota.imp_cuo_sin_interes = i - 1 == num_cuotas ? Math.Round(ListDetalleCuota.imp_cuotas, 2) - Math.Round(ListDetalleCuota.imp_interes, 2) : i < 2 ? 0 : ListDetalleCuota.imp_cuo_sin_interes;
                            ListDetalleCuota.imp_interes = i - 1 == num_cuotas ? Math.Abs(Convert.ToDecimal(txtImpInteres.EditValue) - Math.Round(importeInteres, 2)) : i < 2 ? 0 : Math.Round(Convert.ToDecimal(txtImpInteres.EditValue) / Convert.ToDecimal(num_cuotas),2);
                            ListDetalleCuota.imp_cuo_sin_interes = i < 2 ? 0 : ListDetalleCuota.imp_cuotas - ListDetalleCuota.imp_interes;

                            mylistDetalleCuotas.Add(ListDetalleCuota);
                        }

                        bsLotesDetalleCuotas.DataSource = mylistDetalleCuotas;
                        gvDetalleCuotas.RefreshData();
                        gvDetalleCuotas.FocusedRowHandle = gvDetalleCuotas.RowCount - 1;
                    }
                    else
                    {
                        if (dtFecVecCuota.EditValue == null || num_cuotas == 0 || dtFecPagoCuota.EditValue == null /*|| dtFechaSeparacionFI.EditValue == null*/) { return; }
                        //int numOrden = mylistAdenda.Count() + 1;
                        mylistDetalleCuotas = new List<eContratos.eContratos_Adenda_Financiamiento>();
                        decimal importeSumatoria = 0;
                        DateTime dateVencimiento = (DateTime)dtFecVecCuota.EditValue;
                        DateTime dateCUOI = (DateTime)dtFecPagoCuota.EditValue;

                        for (int i = 0; i <= num_cuotas; i++)
                        {
                            importeSumatoria += i != 0 && i != num_cuotas ? Convert.ToDecimal(txtValorCuotas.EditValue) : 0;
                            //eContratos.eContratos_Adenda_Financiamiento ListDetalleCuota = new eContratos.eContratos_Adenda_Financiamiento
                            //{
                            eContratos.eContratos_Adenda_Financiamiento ListDetalleCuota = new eContratos.eContratos_Adenda_Financiamiento();
                            ListDetalleCuota.num_orden_det_cuo = i + 1;
                            ListDetalleCuota.num_cuota = i; //== 0 ? 0 : i == 1 ? 0 : i - 1;
                            ListDetalleCuota.dsc_cuota = i == 0 ? "Inicial" : "Cuota"; //"Separación" : i == 1 ? "Inicial" : "Cuota";
                            ListDetalleCuota.dsc_estado = "Pendiente";
                            ListDetalleCuota.fch_vct_cuota = i == 0 ? dateCUOI : dateVencimiento.Day >= 28 ? obtenerUltimoDiaDelMes(dateVencimiento.AddMonths(i - 1)) : dateVencimiento.AddMonths(i - 1);
                            ListDetalleCuota.imp_cuotas = i == num_cuotas ? Math.Abs(Convert.ToDecimal(txtPrecioFinalFinanciar.EditValue) - Math.Round(importeSumatoria, 2)) : i == 0 ? Convert.ToDecimal(txtCuoInicial.Text) : Convert.ToDecimal(txtValorCuotas.Text);
                            ListDetalleCuota.imp_cuo_sin_interes = i < 1 ? 0 : (Convert.ToDecimal(txtPrecioFinalFinanciar.EditValue) - Convert.ToDecimal(txtImpInteres.EditValue)) / Convert.ToDecimal(num_cuotas/*txtFraccion.Tag*/);
                            ListDetalleCuota.imp_interes = i < 1 ? 0 : Convert.ToDecimal(txtImpInteres.EditValue) / Convert.ToDecimal(num_cuotas/*txtFraccion.Tag*/);
                            ListDetalleCuota.imp_cuo_sin_interes = i == num_cuotas ? Math.Round(ListDetalleCuota.imp_cuotas, 2) - Math.Round(ListDetalleCuota.imp_interes, 2) : ListDetalleCuota.imp_cuo_sin_interes;
                            //};
                            mylistDetalleCuotas.Add(ListDetalleCuota);
                        }

                        bsLotesDetalleCuotas.DataSource = mylistDetalleCuotas;
                        gvDetalleCuotas.RefreshData();
                        gvDetalleCuotas.FocusedRowHandle = gvDetalleCuotas.RowCount - 1;
                    }

                }



                if (chkContado.CheckState == CheckState.Checked)
                {
                    if (!validarCadenaVacio(txtCodSepara.Text) || Convert.ToDecimal(txtSeparacionCO.EditValue) > 0)
                    {
                        if (dtFecPagoContado.EditValue == null || dtFechaSeparacionCO.EditValue == null) { return; }

                        mylistDetalleCuotas = new List<eContratos.eContratos_Adenda_Financiamiento>();
                        DateTime dateSep = (DateTime)dtFechaSeparacionCO.EditValue;
                        DateTime datePagoCO = (DateTime)dtFecPagoContado.EditValue;
                        for (int i = 0; i < 2; i++)
                        {
                            eContratos.eContratos_Adenda_Financiamiento ListDetalleCuota = new eContratos.eContratos_Adenda_Financiamiento
                            {
                                num_orden_det_cuo = i + 1,
                                num_cuota = 0,
                                dsc_cuota = i == 0 ? "Separación" : "Pendiente de Pago",
                                dsc_estado = "Pendiente",
                                fch_vct_cuota = i == 0 ? dateSep : datePagoCO,
                                imp_cuotas = i == 0 ? Convert.ToDecimal(txtSeparacionCO.Text) : Convert.ToDecimal(txtPendientePago.Text)

                            };
                            mylistDetalleCuotas.Add(ListDetalleCuota);
                        }
                        bsLotesDetalleCuotas.DataSource = mylistDetalleCuotas;
                        gvDetalleCuotas.RefreshData();
                        gvDetalleCuotas.FocusedRowHandle = gvDetalleCuotas.RowCount - 1;
                    }
                    else
                    {
                        if (dtFecPagoContado.EditValue == null) { return; }

                        mylistDetalleCuotas = new List<eContratos.eContratos_Adenda_Financiamiento>();
                        DateTime datePagoCO = (DateTime)dtFecPagoContado.EditValue;
                        eContratos.eContratos_Adenda_Financiamiento ListDetalleCuota = new eContratos.eContratos_Adenda_Financiamiento
                        {
                            num_orden_det_cuo = 1,
                            num_cuota = 0,
                            dsc_cuota = "Pendiente de Pago",
                            dsc_estado = "Pendiente",
                            fch_vct_cuota = (DateTime)dtFecPagoContado.EditValue,
                            imp_cuotas = Convert.ToDecimal(txtPreFinalDescuento.EditValue)
                        };
                        mylistDetalleCuotas.Add(ListDetalleCuota);
                        bsLotesDetalleCuotas.DataSource = mylistDetalleCuotas;
                        gvDetalleCuotas.RefreshData();
                        gvDetalleCuotas.FocusedRowHandle = gvDetalleCuotas.RowCount - 1;
                    }

                }




                //rtxtNombre.EnableCustomMaskTextInput(args => { args.Cancel(); return; });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DateTime obtenerUltimoDiaDelMes(DateTime dateVencimiento)
        {
            // Obtiene el primer día del mes siguiente a la fecha dada
            DateTime primerDiaMesSiguiente = new DateTime(dateVencimiento.Year, dateVencimiento.Month, 1).AddMonths(1);
            // Resta un día para obtener el último día del mes dado
            DateTime ultimoDiaMes = primerDiaMesSiguiente.AddDays(-1);

            // Si el último día del mes es 31, entonces restamos un día
            if (ultimoDiaMes.Day == 31)
            {
                return ultimoDiaMes.AddDays(-1);
            }

            return ultimoDiaMes;
        }


        private void ActivarConfiguracion()
        {
            txtCuotaDesde.ReadOnly = false;
            txtCuotaHasta.ReadOnly = false;
            txtImpCuotaConf.ReadOnly = false;
            //lkpInteresporc.ReadOnly = false;
            mmObservacion.ReadOnly = false;
            btnGrabarCuotas.Enabled = true;

        }
        private void DesactivarConfiguracion()
        {
            txtCuotaDesde.ReadOnly = true;
            txtCuotaHasta.ReadOnly = true;
            txtImpCuotaConf.ReadOnly = true;
            //lkpInteresporc.ReadOnly = true;
            mmObservacion.ReadOnly = true;
            btnGrabarCuotas.Enabled = false;


        }
    }
}