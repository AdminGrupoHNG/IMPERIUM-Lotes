using BE_GestionLotes;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Formularios.Lotes;

namespace UI_GestionLotes.Formularios.Operaciones
{
    internal enum Proforma
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2,
        Duplicar = 3,
    }
    public partial class frmMantProformas : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListarCotizacion frmHandler;
        internal Proforma MiAccion = Proforma.Nuevo;
        public string cod_empresa = "", cod_proyecto = "", cod_lote = "", cod_proforma = ""; //, perfil = "";
        decimal imp_sep_12 = 0, imp_sep_18 = 0, imp_sep_24 = 0, imp_sep_36 = 0, imp_sep_48 = 0, imp_sep_60 = 0, imp_sep_especial = 0;
        decimal imp_ini_12 = 0, imp_ini_18 = 0, imp_ini_24 = 0, imp_ini_36 = 0, imp_ini_48 = 0, imp_ini_60 = 0, imp_ini_especial = 0;
        decimal prc_descuento_maximo = 0;
        public eProformas pro = new eProformas();
        public eProformas.eProformas_Detalle ePro_Detalle = new eProformas.eProformas_Detalle();
        List<eProformas.eProformas_Detalle> listDetalle = new List<eProformas.eProformas_Detalle>();
        public frmMantProformas()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurarFormulario();
        }
        internal frmMantProformas(frmListarCotizacion frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
            configurarFormulario();
        }

        void configurarFormulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListarCotizacion, gvListarCotizacion, editable: true, ShowAutoFilterRow: false);
        }

        private void frmMantProformas_Load(object sender, EventArgs e)
        {
            HabilitarBotones();
            Inicializar();
            gcInformacionGeneral.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            gcLote.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl3.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            //if(perfil == "VISUALIZADOR")
            //{
            //    layoutControl4.Enabled = false;
            //    layoutControl6.Enabled = false;
            //    layoutControl3.Enabled = false;
            //    btnNuevo.Enabled = false;
            //    btnVerDocumento.Enabled = false;
            //    btnGuardar.Enabled = false;
            //}
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false)
                {
                    layoutControl4.Enabled = false;
                    layoutControl6.Enabled = false;
                    layoutControl3.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnVerDocumento.Enabled = false;
                    btnGuardar.Enabled = false;
                }
            }
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Proforma.Nuevo:
                    Nuevo();
                    break;
                case Proforma.Editar:
                    Editar();
                    break;
                case Proforma.Vista:
                    Vista();
                    layoutControl4.Enabled = false;
                    layoutControl6.Enabled = false;
                    layoutControl3.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnVerDocumento.Enabled = false;
                    btnGuardar.Enabled = false;
                    break;
            }

        }

        private void Nuevo()
        {
            CargarLookUpEdit();
            grupoListarCotizacion.Enabled = false; //INHABILITA LA GRILLA
            dtFechaProforma.EditValue = DateTime.Today;
        }


        private void Editar()
        {
            CargarLookUpEdit();
            List<eProformas> eProformas = new List<eProformas>();

            eProformas = unit.Proyectos.ObtenerListadoProformas<eProformas>(2, cod_proforma, cod_proyecto);
            lkpEtapa.EditValue = eProformas[0].cod_etapa;
            txtCodigoProforma.Text = eProformas[0].cod_proforma;
            dtFechaProforma.EditValue = Convert.ToDateTime(eProformas[0].fch_proforma);
            txtLote.Text = eProformas[0].dsc_lote;
            txtCodCliente.Text = eProformas[0].cod_cliente;
            txtNombres.Text = eProformas[0].dsc_nombre;
            lkpManzana.EditValue = eProformas[0].cod_manzana;
            txtApellidoPaterno.Text = eProformas[0].dsc_apellido_paterno;
            txtApellidoMaterno.Text = eProformas[0].dsc_apellido_materno;
            lkpTipoDocumento.EditValue = eProformas[0].cod_tipo_documento;
            txtNroDocumento.Text = eProformas[0].dsc_documento;
            txtTelefono.Text = eProformas[0].dsc_telefono;
            txtCorreo.Text = eProformas[0].dsc_email;
            lkpLote.EditValue = eProformas[0].cod_lote;
            lkpEstadoCivil.EditValue = eProformas[0].cod_estado_civil;
            lkpEjecutivo.EditValue = eProformas[0].cod_ejecutivo;
            txtDireccion.EditValue = eProformas[0].dsc_cadena_direccion;
            mmObservaciones.Text = eProformas[0].dsc_observaciones;
            txtMonto.Text = eProformas[0].monto.ToString();
            txtUsuarioRegistro.Text = eProformas[0].dsc_usuario_registro;
            txtUsuarioCambio.Text = eProformas[0].dsc_usuario_cambio;
            dtFechaRegistro.EditValue = eProformas[0].fch_registro;
            dtFechaModificacion.EditValue = eProformas[0].fch_cambio;
            lkpNivelInteres.EditValue = eProformas[0].cod_variable;
            cod_lote = eProformas[0].cod_lote;

            if (eProformas[0].cod_cliente != "")
            {
                txtNombres.Enabled = false;
                txtApellidoPaterno.Enabled = false;
                txtApellidoMaterno.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                txtNroDocumento.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
                lkpEstadoCivil.Enabled = false;
                //lkpEjecutivo.Enabled = false;
                txtDireccion.Enabled = false;
            }

            btnVerDocumento.Enabled = true;
            btnNuevo.Enabled = true;

            List<eProformas.eProformas_Detalle> lstProDetalle = new List<eProformas.eProformas_Detalle>();
            lstProDetalle = unit.Proyectos.ObtenerListadoProformas<eProformas.eProformas_Detalle>(3, cod_proforma: cod_proforma, cod_proyecto: cod_proyecto);
            bsDetalleProformas.DataSource = lstProDetalle;

            for (int x = gvListarCotizacion.RowCount - 1; x >= 0; x--)
            {
                eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(x) as eProformas.eProformas_Detalle;
                if (obj.flg_seleccion == "SI") gvListarCotizacion.SelectRow(x);
            }

        }
        private void Vista()
        {
        }
        private void CargarLookUpEdit()
        {
            try
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                unit.Clientes.CargaCombosLookUp("TipoDocumento", lkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "");
                unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", lkpEstadoCivil, "cod_estado_civil", "dsc_estado_civil", "");

                List<eUsuario> ListEjecutivos = new List<eUsuario>();
                int opcion = MiAccion == Proforma.Nuevo ? 4 : 6;
                ListEjecutivos = unit.Campanha.ListarEjecutivosVentasMenu<eUsuario>(opcion, "", cod_proyecto, Program.Sesion.Usuario.cod_usuario);
                lkpEjecutivo.Properties.ValueMember = "cod_usuario";
                lkpEjecutivo.Properties.DisplayMember = "dsc_usuario";
                lkpEjecutivo.Properties.DataSource = ListEjecutivos;
                var cod_usuario = ListEjecutivos.Where(x => x.cod_usuario == Program.Sesion.Usuario.cod_usuario).Select(x => x.cod_usuario).ToArray();
                lkpEjecutivo.EditValue = cod_usuario.Count() > 0 ? cod_usuario[0] : null;

                //unit.Clientes.CargaCombosLookUp("Asesor", lkpEjecutivo, "cod_usuario", "dsc_usuario", "", valorDefecto: true);
                unit.Clientes.CargaCombosLookUp("NivelInteres", lkpNivelInteres, "cod_variable", "dsc_Nombre", "", valorDefecto: true);
                unit.Clientes.CargaCombosLookUp("EtapasFiltroProyecto", lkpEtapa, "cod_etapa", "dsc_descripcion", "", valorDefecto: true, codigo: cod_proyecto, codigoMultiple: "ALL");
                lkpTipoDocumento.EditValue = "DI001";
                lkpNivelInteres.EditValue = "INT0002";
                //blReq.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: user.cod_usuario);

                //if (list.Count >= 1) { lkpEmpresa.EditValue = list[0].cod_empresa; txtRuc.EditValue = list[0].dsc_ruc; }
                //lkpEmpresa.EditValue = "00005";
                //blReq.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
                //lkpSede.EditValue = "00001";
                //List<eEmpresa> list = lkpEmpresa.Properties.DataSource as List<eEmpresa>;
                //var v = lkpEmpresa.Properties.DataSource as List<eEmpresa>;
                //lkpEmpresa.EditValue = v.FirstOrDefault().cod_empresa;
                //blReq.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
                //lkpEmpresa.EditValue = list[0].cod_empresa;
                //lkpSede.EditValue = "00001";*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lkpEtapa_EditValueChanged(object sender, EventArgs e)
        {
            listDetalle.Clear();
            bsDetalleProformas.DataSource = null;
            grupoListarCotizacion.Enabled = false;
            lkpManzana.EditValue = null;
            lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtMonto.Text = "";
            cod_lote = "";
            txtPrcUsoExclusivo.Text = "";
            if (lkpEtapa.EditValue != null)
            {
                unit.Proyectos.CargaCombosLookUp("ManzanaXEtapa", lkpManzana, "cod_manzana", "dsc_manzana", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: cod_proyecto, cod_condicion: "3", valorDefecto: true);
            }
        }

        private void lkpManzana_EditValueChanged(object sender, EventArgs e)
        {
            listDetalle.Clear();
            bsDetalleProformas.DataSource = null;
            grupoListarCotizacion.Enabled = false;
            lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtMonto.Text = "";
            txtPrcUsoExclusivo.Text = "";
            if (lkpManzana.EditValue != null)
            {
                unit.Proyectos.CargaCombosLookUp("LoteXmanza", lkpLote, "cod_lote", "num_lote", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: cod_proyecto, cod_tres: lkpManzana.EditValue.ToString(), cod_condicion: "8", valorDefecto: true);
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
                    decimal montoFinal;
                    txtAreaM2.Text = dataRow["num_area_uex"].ToString();
                    txtMonto.Text = dataRow["imp_precio_total"].ToString();
                    cod_lote = dataRow["cod_lote"].ToString();
                    cod_empresa = dataRow["cod_empresa"].ToString();
                    txtPrcUsoExclusivo.Text = dataRow["prc_uso_exclusivo_descripcion"].ToString();

                    grupoListarCotizacion.Enabled = true;
                    decimal.TryParse(txtMonto.Text, System.Globalization.NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out montoFinal);
                    listDetalle.Clear();
                    listDetalle = unit.Proyectos.obtenerConsultasVariasLotes<eProformas.eProformas_Detalle>(19, montoFinal, cod_proyecto);
                    prc_descuento_maximo = listDetalle[1].prc_descuento_maximo * 100;
                    bsDetalleProformas.DataSource = null; bsDetalleProformas.DataSource = listDetalle; gvListarCotizacion.RefreshData();
                }
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            cod_proforma = "";
            cod_lote = "";
            CargarLookUpEdit();
            grupoListarCotizacion.Enabled = false; //INHABILITA LA GRILLA
            dtFechaProforma.EditValue = DateTime.Today;

            txtNombres.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            lkpTipoDocumento.Enabled = true;
            txtNroDocumento.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            lkpEstadoCivil.Enabled = true;
            lkpEjecutivo.Enabled = true;
            txtDireccion.Enabled = true;

            txtCodCliente.Text = "";
            txtNombres.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            lkpTipoDocumento.EditValue = "DI001";
            txtNroDocumento.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            lkpEstadoCivil.EditValue = "";
            lkpEjecutivo.EditValue = "";
            txtDireccion.Text = "";
            mmObservaciones.Text = "";

            listDetalle.Clear();
            bsDetalleProformas.DataSource = null;
            grupoListarCotizacion.Enabled = false;
            lkpManzana.EditValue = null;
            lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtMonto.Text = "";

            btnVerDocumento.Enabled = false;
        }

        private void btnVerDocumento_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");

                rptProforma reporte = new rptProforma();
                ReportPrintTool printTool = new ReportPrintTool(reporte);
                reporte.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                reporte.Parameters["cod_proforma"].Value = cod_proforma;
                reporte.Parameters["cod_proyecto"].Value = cod_proyecto;
                printTool.ShowPreviewDialog();

                //SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //SplashScreenManager.CloseForm();
            }
        }

        private void txtNroDocumento_Validating(object sender, CancelEventArgs e)
        {
            List<eCliente> ListCliente = new List<eCliente>();
            ListCliente = unit.Clientes.ListarClientes<eCliente>(21, dsc_documento: txtNroDocumento.Text);

            if (ListCliente.Count > 0)
            {
                txtCodCliente.Text = ListCliente[0].cod_cliente;
                txtNombres.Text = ListCliente[0].dsc_nombre;
                txtApellidoPaterno.Text = ListCliente[0].dsc_apellido_paterno;
                txtApellidoMaterno.Text = ListCliente[0].dsc_apellido_materno;
                lkpTipoDocumento.EditValue = ListCliente[0].cod_tipo_documento;
                txtNroDocumento.Text = ListCliente[0].dsc_documento;
                txtTelefono.Text = ListCliente[0].dsc_telefono_1 != "" ? ListCliente[0].dsc_telefono_1 : ListCliente[0].dsc_telefono_2;
                txtCorreo.Text = ListCliente[0].dsc_email;
                lkpEstadoCivil.EditValue = ListCliente[0].cod_estadocivil;
                //lkpEjecutivo.EditValue = ListCliente[0].cod_asesor;
                txtDireccion.Text = ListCliente[0].dsc_cadena_direccion + ", " + ListCliente[0].dsc_distrito + ", " + ListCliente[0].dsc_provincia + ", " + ListCliente[0].dsc_departamento;

                txtNombres.Enabled = false;
                txtApellidoPaterno.Enabled = false;
                txtApellidoMaterno.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                txtNroDocumento.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
                lkpEstadoCivil.Enabled = false;
                //lkpEjecutivo.Enabled = false;
                txtDireccion.Enabled = false;
            }

        }

        public bool AnalizarNombreCuota(string texto, string validar)
        {
            string cadenaSinEspacios = texto.Replace(" ", "");
            string cadenaEnMinusculas = cadenaSinEspacios.ToLower();
            if (cadenaEnMinusculas.Contains(validar))
            {
                return true;
            }
            return false;

        }

        private void gvListarCotizacion_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                eProformas.eProformas_Detalle obj = gvListarCotizacion.GetFocusedRow() as eProformas.eProformas_Detalle;
                if (obj == null) return;
                if (gvListarCotizacion.FocusedColumn.FieldName == "num_fraccion")
                {
                    e.Cancel = AnalizarNombreCuota(obj.dsc_nombre_detalle, "especial") ? false : true; // 2 es igual a cuota especial
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListarCotizacion_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(e.RowHandle) as eProformas.eProformas_Detalle;
                    if (obj.cod_variable == "")
                    {
                        e.Appearance.BackColor = Color.Orange;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoDocumento.EditValue == null) { return; }

            //if (lkpTipoDocumento.EditValue.ToString() == "DI004")
            //{
            //    txtNroDocumento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //    txtNroDocumento.Properties.Mask.EditMask = "##########X";
            //    txtNroDocumento.Properties.Mask.UseMaskAsDisplayFormat = true;
            //    txtNroDocumento.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            //    txtNroDocumento.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;


            //    //txtNroDocumento.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
            //    //{
            //    //    settings. = "99999999999";
            //    //    //settings.AutoHideDecimalSeparator = false;
            //    //    //settings.HideInsignificantZeros = true;
            //    //});
            //}

            if (lkpTipoDocumento.EditValue.ToString() == "DI001")
            {
                txtNroDocumento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtNroDocumento.Properties.Mask.EditMask = "########";
                txtNroDocumento.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtNroDocumento.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                //txtNroDocumento.Properties.MaskSettings.Configure<MaskSettings.Simple>(settings =>
                //{
                //    settings.MaskExpression = "99999999";
                //    //settings.AutoHideDecimalSeparator = false;
                //    //settings.HideInsignificantZeros = true;
                //});
            }

            if (lkpTipoDocumento.EditValue.ToString() != "DI001" /*&& lkpTipoDocumento.EditValue.ToString() != "DI004"*/)
            {
                txtNroDocumento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
                txtNroDocumento.Properties.Mask.EditMask = "";
                txtNroDocumento.Properties.Mask.UseMaskAsDisplayFormat = false;


                //txtNroDocumento.Properties.ResetMaskSettings();

            }
            txtNroDocumento.Focus();

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
                    lkpEtapa.EditValue = frm.cod_etapa;
                    cod_lote = frm.cod_lote;
                    txtLote.Text = frm.dsc_lote;
                    lkpManzana.EditValue = frm.cod_manzana;
                    txtMonto.Text = frm.imp_precio_final.ToString();
                    txtPrcUsoExclusivo.EditValue = frm.prc_uso_exclusivo;
                    grupoListarCotizacion.Enabled = true;
                    lkpLote.EditValue = frm.cod_lote;
                    txtPrcUsoExclusivo.Text = frm.prc_uso_exclusivo;
                }
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
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones();
                frm.MiAccion = ListCliSeparacion.Nuevo;

                frm.proforma = true;
                frm.codigo_proyecto = "ALL";
                frm.cod_empresa = "";
                frm.dsc_proyecto = "";
                frm.ShowDialog();

                if (frm.clienteProforma.cod_cliente != null)
                {
                    limpiarCampos();
                    txtCodCliente.Text = frm.clienteProforma.cod_cliente;
                    txtNombres.Text = frm.clienteProforma.dsc_nombre;
                    txtApellidoPaterno.Text = frm.clienteProforma.dsc_apellido_paterno;
                    txtApellidoMaterno.Text = frm.clienteProforma.dsc_apellido_materno;
                    lkpTipoDocumento.EditValue = frm.clienteProforma.cod_tipo_documento == null ? lkpTipoDocumento.EditValue : frm.clienteProforma.cod_tipo_documento;
                    txtNroDocumento.Text = frm.clienteProforma.dsc_documento;
                    txtTelefono.Text = frm.clienteProforma.dsc_telefono_1 != "" ? frm.clienteProforma.dsc_telefono_1 : frm.clienteProforma.dsc_telefono_2;
                    txtCorreo.Text = frm.clienteProforma.dsc_email;
                    lkpEstadoCivil.EditValue = frm.clienteProforma.cod_estadocivil;
                    //lkpEjecutivo.EditValue = frm.clienteProforma.cod_asesor;
                    txtDireccion.Text = frm.clienteProforma.dsc_cadena_direccion + ", " + frm.clienteProforma.dsc_distrito + ", " + frm.clienteProforma.dsc_provincia + ", " + frm.clienteProforma.dsc_departamento;

                    txtNombres.Enabled = false;
                    txtApellidoPaterno.Enabled = false;
                    txtApellidoMaterno.Enabled = false;
                    lkpTipoDocumento.Enabled = false;
                    txtNroDocumento.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtCorreo.Enabled = false;
                    lkpEstadoCivil.Enabled = false;
                    //lkpEjecutivo.Enabled = false;
                    txtDireccion.Enabled = false;
                    /*
                    txtCodCliente.Text = frm.cod_cliente;
                    txtNroLte.Text = frm.dsc_lote;
                    txtDscCliente.Text = frm.dsc_cliente;
                    txtMontoSolicitado.Text = frm.imp_precio_final.ToString();
                    txtMontoInicial.Text = frm.imp_precio_final.ToString();
                    */
                }
                if (frm.prospectoProforma.cod_prospecto != null)
                {
                    limpiarCampos();
                    txtNombres.Text = frm.prospectoProforma.dsc_nombres;
                    txtApellidoPaterno.Text = frm.prospectoProforma.dsc_apellido_paterno;
                    txtApellidoMaterno.Text = frm.prospectoProforma.dsc_apellido_materno;
                    lkpTipoDocumento.EditValue = frm.prospectoProforma.cod_tipo_documento == null ? lkpTipoDocumento.EditValue : frm.prospectoProforma.cod_tipo_documento;
                    txtNroDocumento.Text = frm.prospectoProforma.dsc_num_documento;
                    txtTelefono.Text = frm.prospectoProforma.dsc_telefono_movil != "" ? frm.prospectoProforma.dsc_telefono_movil : frm.prospectoProforma.dsc_telefono;
                    txtCorreo.Text = frm.prospectoProforma.dsc_email;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtCodCliente.Text == "")
            {
                if (String.IsNullOrEmpty(dtFechaProforma.Text)) { MessageBox.Show("Debe ingresar la fecha de la proforma", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFechaProforma.Focus(); return; }
                if (String.IsNullOrEmpty(txtNombres.Text)) { MessageBox.Show("Debe ingresar el nombre del cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNombres.Focus(); return; }
                //if (String.IsNullOrEmpty(txtNroDocumento.Text)){ MessageBox.Show("Debe ingresar el documento del cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (String.IsNullOrEmpty(txtCorreo.Text) && String.IsNullOrEmpty(txtTelefono.Text)) { MessageBox.Show("Debe ingresar el correo o teléfono del cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTelefono.Focus(); return; }
            }
            if (lkpEjecutivo.EditValue == null || lkpEjecutivo.EditValue.ToString().Trim() == "") { MessageBox.Show("Debe seleccionar un ejecutivo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpEjecutivo.ShowPopup(); return; }
            if (cod_lote == "") { MessageBox.Show("Debe seleccionar un lote", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (gvListarCotizacion.GetSelectedRows().Count() == 0) { MessageBox.Show("Debe seleccionar al menos un escenario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            bool validador = true;
            try
            {
                gvListarCotizacion.PostEditor(); gvListarCotizacion.RefreshData();
                if (gvListarCotizacion.RowCount > 0)
                {
                    for (int x = gvListarCotizacion.RowCount - 1; x >= 0; x--)
                    {
                        validador = filtro(x);
                        if (!validador) { MessageBox.Show("Favor de revisar que todos los campos estén correctos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; }
                    }

                    if (validador)
                    {
                        for (int x = gvListarCotizacion.RowCount - 1; x >= 0; x--)
                        {
                            eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(x) as eProformas.eProformas_Detalle;
                            obj.flg_seleccion = "";
                        }
                        foreach (int nRow in gvListarCotizacion.GetSelectedRows())
                        {
                            eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(nRow) as eProformas.eProformas_Detalle;
                            obj.flg_seleccion = "SI";
                        }

                        pro = AsignarValores_Proforma();
                        pro = unit.Proyectos.Guardar_Actualizar_Proforma<eProformas>(pro, 1);
                        if (pro != null)
                        {
                            cod_proforma = pro.cod_proforma;
                            txtCodigoProforma.Text = pro.cod_proforma;
                            txtUsuarioRegistro.Text = pro.dsc_usuario_registro;
                            txtUsuarioCambio.Text = pro.dsc_usuario_cambio;
                            dtFechaRegistro.EditValue = pro.fch_registro;
                            dtFechaModificacion.EditValue = pro.fch_cambio;
                            btnNuevo.Enabled = true;
                            btnVerDocumento.Enabled = true;
                        }


                        for (int x = gvListarCotizacion.RowCount - 1; x >= 0; x--)
                        {
                            eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(x) as eProformas.eProformas_Detalle;
                            if (obj.cod_referencia != "")
                            {
                                ePro_Detalle = AsignarValores_Proforma_Detalle(x);
                                ePro_Detalle = unit.Proyectos.Guardar_Actualizar_Proforma_Detalle<eProformas.eProformas_Detalle>(ePro_Detalle, 1);
                            }
                        }

                        MessageBox.Show("La proforma se ha registrado correctamente", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No hay información para guardar en el sistema", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        bool filtro(int x)
        {
            eProformas.eProformas_Detalle objCaptura = gvListarCotizacion.GetRow(x) as eProformas.eProformas_Detalle;
            //if (objCaptura.cod_empresa == null) { /*MessageBox.Show("Debe seleccionar una empresa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);*/ return false; }
            return true;
        }

        private void txtSeparacion_EditValueChanged(object sender, EventArgs e)
        {
            //gvListarCotizacion.RefreshData();
            //if (Convert.ToDecimal(txtSeparacion.EditValue) >= 0)
            //{
            //    for (int i = 0; i < gvListarCotizacion.RowCount; i++)
            //    {
            //        eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
            //        if (obj2.num_fraccion != 0)
            //        {
            //            obj2.imp_valor_cuota = (obj2.imp_precio_final - Convert.ToDecimal(txtSeparacion.EditValue) - Convert.ToDecimal(txtCuotaInicial.EditValue)) / obj2.num_fraccion;
            //            obj2.imp_separacion = Convert.ToDecimal(txtSeparacion.EditValue);
            //        }
            //    }
            //}
        }

        private void txtCuotaInicial_EditValueChanged(object sender, EventArgs e)
        {
            //gvListarCotizacion.RefreshData();
            //if (Convert.ToDecimal(txtCuotaInicial.EditValue) >= 0)
            //{
            //    for (int i = 0; i < gvListarCotizacion.RowCount; i++)
            //    {
            //        eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
            //        if (obj2.num_fraccion != 0)
            //        {
            //            obj2.imp_valor_cuota = (obj2.imp_precio_final - Convert.ToDecimal(txtSeparacion.EditValue) - Convert.ToDecimal(txtCuotaInicial.EditValue)) / obj2.num_fraccion;
            //            obj2.imp_cuota_inicial = Convert.ToDecimal(txtCuotaInicial.EditValue);
            //        }

            //    }
            //}
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombres.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            lkpTipoDocumento.Enabled = true;
            txtNroDocumento.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            lkpEstadoCivil.Enabled = true;
            lkpEjecutivo.Enabled = true;
            txtDireccion.Enabled = true;

            txtCodCliente.Text = "";
            txtNombres.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            lkpTipoDocumento.EditValue = "DI001";
            txtNroDocumento.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            lkpEstadoCivil.EditValue = "";
            lkpEjecutivo.EditValue = null;
            txtDireccion.Text = "";
        }

        private void Calculo_Cotizador(eProformas.eProformas_Detalle obj, Boolean sts_CUIO)
        {
            if (obj.num_fraccion == 0 && obj.dsc_nombre_detalle.ToLower().Contains("contado"))
            {
                //obj.imp_separacion = 0;
                obj.imp_cuota_inicial = 0;
                obj.prc_cuota_inicial = 0;
                obj.prc_interes = 0;
            }
            obj.imp_descuento = obj.imp_precio_venta * obj.prc_descuento;
            obj.imp_precio_final = obj.imp_precio_venta - obj.imp_descuento;
            if (obj.imp_cuota_inicial + obj.imp_separacion > obj.imp_precio_final) { obj.imp_separacion = 0; obj.imp_cuota_inicial = 0; }
            obj.imp_cuota_inicial = obj.imp_cuota_inicial > 0 && sts_CUIO ? obj.imp_cuota_inicial : obj.prc_cuota_inicial == 0 ? 0 : (obj.imp_precio_final * obj.prc_cuota_inicial) - obj.imp_separacion;
            obj.imp_interes = (((obj.imp_precio_venta * (1 - obj.prc_descuento)) - obj.imp_cuota_inicial - obj.imp_separacion) * obj.prc_interes) * (obj.num_fraccion / 12);
            obj.prc_cuota_inicial = sts_CUIO ? (obj.imp_cuota_inicial + obj.imp_separacion) / obj.imp_precio_final : obj.prc_cuota_inicial;
            obj.imp_valor_cuota = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial + obj.imp_interes) / (obj.num_fraccion == 0 ? 1 : obj.num_fraccion);
        }

        private void gvListarCotizacion_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvListarCotizacion.RefreshData();
            eProformas.eProformas_Detalle obj = gvListarCotizacion.GetFocusedRow() as eProformas.eProformas_Detalle;
            if (obj == null) return;
            decimal prc_descuento = obj.prc_descuento, imp_separacion = obj.imp_separacion, prc_cuota_inicial = obj.prc_cuota_inicial, prc_interes = obj.prc_interes;



            if (e.Column.FieldName == "prc_descuento" && ((obj.prc_descuento * 100) > prc_descuento_maximo || (obj.prc_descuento * 100) < 0))
            {
                obj.prc_descuento = 0;
                MessageBox.Show("El porcentaje de descuento no debe ser mayor de " + prc_descuento_maximo.ToString("0.00") + ", ni menor de 0", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            var _sts_CUOI = (e.Column.FieldName == "imp_cuota_inicial") ? true : false;
            if (obj.cod_variable == "") //si se ingresa valor en la fila del filtro, actualiza todos los registros
            {
                for (int i = 0; i < gvListarCotizacion.RowCount; i++)
                {
                    eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
                    if (e.Column.FieldName == "prc_descuento") obj2.prc_descuento = prc_descuento;
                    if (e.Column.FieldName == "imp_separacion") obj2.imp_separacion = imp_separacion;
                    if (e.Column.FieldName == "prc_cuota_inicial") obj2.prc_cuota_inicial = prc_cuota_inicial;
                    if (e.Column.FieldName == "prc_interes") obj2.prc_interes = prc_interes;
                    Calculo_Cotizador(obj2, _sts_CUOI);
                }
            }
            else
            {
                Calculo_Cotizador(obj, _sts_CUOI);
            }



            if (e.Column.FieldName == "num_fraccion" && obj.cod_variable != "")
            {
                if (obj.num_fraccion <= 1 || obj.num_fraccion >= 11)
                {
                    MessageBox.Show("La cantidad de cuotas no debe ser mayor de 10, ni menor de 2", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    obj.num_fraccion = 2; return;
                }
                gvListarCotizacion.SelectRow(gvListarCotizacion.RowCount - 1);
                Calculo_Cotizador(obj, _sts_CUOI);
                return;
            }
            gvListarCotizacion.RefreshData();
        }


        private void gvListarCotizacion2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvListarCotizacion.RefreshData();
            //bsDetalleProformas.DataSource = listDetalle; gvListarCotizacion.RefreshData();
            eProformas.eProformas_Detalle obj = gvListarCotizacion.GetFocusedRow() as eProformas.eProformas_Detalle;
            decimal separacion = obj.imp_separacion,
                porcentaje_cuota_inicial = obj.prc_cuota_inicial,
                cuota_inicial = obj.imp_cuota_inicial,
                descuento = obj.prc_descuento;
            //porcentajeCOUI = (cuota_inicial / (obj.imp_precio_final - separacion)) * 100,
            //sumaCUOI = (obj.imp_precio_final - separacion) * porcentaje_cuota_inicial;
            //string cod_variable = obj.cod_variable;
            //bsDetalleProformas.DataSource = listDetalle; gvListarCotizacion.RefreshData();
            if (obj == null) return;
            if ((e.Column.FieldName == "imp_separacion" || e.Column.FieldName == "imp_cuota_inicial" || e.Column.FieldName == "prc_cuota_inicial"))
            {
                if (obj.cod_variable != "")
                {
                    if (separacion > obj.imp_precio_final || cuota_inicial > obj.imp_precio_final || porcentaje_cuota_inicial > 1)
                    {
                        obj.imp_separacion = 0; obj.imp_cuota_inicial = 0; obj.prc_cuota_inicial = 0; return;
                    }
                    if (AnalizarNombreCuota(obj.cod_variable, "contado")) { obj.imp_separacion = 0; obj.imp_cuota_inicial = 0; obj.prc_cuota_inicial = 0; return; }

                    if (obj.num_fraccion != 0)
                    {
                        if (cuota_inicial + separacion > obj.imp_precio_final)
                        {
                            if (e.Column.FieldName == "imp_separacion") obj.imp_separacion = 0;
                            if (e.Column.FieldName == "imp_cuota_inicial") obj.imp_cuota_inicial = 0;
                        }
                        else
                        {
                            if (e.Column.FieldName == "imp_cuota_inicial") obj.prc_cuota_inicial = (cuota_inicial / (obj.imp_precio_final - separacion));
                            if (e.Column.FieldName == "prc_cuota_inicial") obj.imp_cuota_inicial = (obj.imp_precio_final - separacion) * porcentaje_cuota_inicial;
                        }

                        obj.imp_separacion = obj.imp_separacion;
                        obj.imp_cuota_inicial = obj.prc_cuota_inicial == 0 ? 0 : (obj.imp_precio_final * obj.prc_cuota_inicial) - obj.imp_separacion;
                        obj.imp_interes = (((obj.imp_precio_venta * (1 - obj.prc_descuento)) - obj.imp_cuota_inicial - obj.imp_separacion) * obj.prc_interes) * (obj.num_fraccion / 12);
                        obj.imp_valor_cuota = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial + obj.imp_interes) / obj.num_fraccion;
                    }
                    /*
                    for (int i = 0; i < gvListarCotizacion.RowCount; i++)
                    {
                        eProformas.eProformas_Detalle objCambio = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
                        if (objCambio.cod_variable == "EFI0001") { objCambio.imp_separacion = 0; objCambio.imp_cuota_inicial = 0; }
                        if (objCambio.cod_variable == "EFI0002") { objCambio.imp_separacion = imp_sep_12; objCambio.imp_cuota_inicial = imp_ini_12; }
                        if (objCambio.cod_variable == "EFI0003") { objCambio.imp_separacion = imp_sep_24; objCambio.imp_cuota_inicial = imp_ini_24; }
                        if (objCambio.cod_variable == "EFI0004") { objCambio.imp_separacion = imp_sep_36; objCambio.imp_cuota_inicial = imp_ini_36; }
                        if (objCambio.cod_variable == "EFI0005") { objCambio.imp_separacion = imp_sep_48; objCambio.imp_cuota_inicial = imp_ini_48; }
                        if (objCambio.cod_variable == "EFI0006") { objCambio.imp_separacion = imp_sep_especial; objCambio.imp_cuota_inicial = imp_ini_especial; }
                    }*/
                }

                if (obj.cod_variable == "")
                {
                    //int numero = gvListarCotizacion.RowCount;

                    if (separacion >= 0 && (cuota_inicial >= 0 || porcentaje_cuota_inicial >= 0))
                    {
                        //eProformas.eProformas_Detalle objEliminar = gvListarCotizacion.GetRow(numero-1) as eProformas.eProformas_Detalle;
                        //bsDetalleProformas.Remove(objEliminar);

                        for (int i = 0; i < gvListarCotizacion.RowCount; i++)
                        {
                            eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
                            if (AnalizarNombreCuota(obj2.dsc_nombre_detalle, "contado") || (separacion + cuota_inicial > obj2.imp_precio_final) || (obj2.imp_precio_final - separacion) * porcentaje_cuota_inicial > obj2.imp_precio_final)
                            {
                                if (e.Column.FieldName == "imp_separacion") obj2.imp_separacion = 0;
                                if (e.Column.FieldName == "imp_cuota_inicial") obj2.imp_cuota_inicial = 0;
                                if (e.Column.FieldName == "prc_cuota_inicial") obj2.prc_cuota_inicial = 0;
                            }
                            else if (e.Column.FieldName == "imp_cuota_inicial" && obj2.cod_variable != "")
                            {
                                //obj2.prc_cuota_inicial = (cuota_inicial / (obj2.imp_precio_final - separacion));
                                obj2.imp_separacion = separacion;
                                obj2.prc_cuota_inicial = (cuota_inicial / (obj2.imp_precio_final - separacion));
                                obj2.imp_cuota_inicial = cuota_inicial;
                                obj2.imp_interes = (((obj2.imp_precio_venta * (1 - obj2.prc_descuento)) - obj2.imp_cuota_inicial - obj2.imp_separacion) * obj2.prc_interes) * (obj2.num_fraccion / 12);
                            }
                            else if (e.Column.FieldName == "prc_cuota_inicial" && obj2.cod_variable != "")
                            {
                                obj2.imp_separacion = separacion;
                                obj2.prc_cuota_inicial = porcentaje_cuota_inicial;
                                //obj2.imp_cuota_inicial = (obj2.imp_precio_final - separacion) * porcentaje_cuota_inicial;
                                obj2.imp_cuota_inicial = (obj2.imp_precio_final * porcentaje_cuota_inicial) - separacion;
                                obj2.imp_interes = (((obj2.imp_precio_venta * (1 - obj2.prc_descuento)) - obj2.imp_cuota_inicial - obj2.imp_separacion) * obj2.prc_interes) * (obj2.num_fraccion / 12);
                            }
                            else
                            {
                                obj2.imp_separacion = separacion;
                                obj2.imp_cuota_inicial = obj2.prc_cuota_inicial == 0 ? 0 : (obj2.imp_precio_final * obj2.prc_cuota_inicial) - obj2.imp_separacion;
                                obj2.imp_interes = (((obj2.imp_precio_venta * (1 - obj2.prc_descuento)) - obj2.imp_cuota_inicial - obj2.imp_separacion) * obj2.prc_interes) * (obj2.num_fraccion / 12);
                                obj2.imp_valor_cuota = obj2.num_fraccion == 0 ? obj2.imp_valor_cuota : (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial + obj2.imp_interes) / obj2.num_fraccion;
                            }



                            if (obj2.cod_variable == "") { obj2.imp_separacion = separacion; obj2.imp_cuota_inicial = cuota_inicial; obj2.prc_cuota_inicial = porcentaje_cuota_inicial > 1 ? 0 : porcentaje_cuota_inicial; }
                            if (obj2.num_fraccion != 0)
                            {

                                //obj2.imp_valor_cuota = (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial + obj.imp_interes) / obj2.num_fraccion;

                                //if (obj2.cod_variable == "EFI0001") { obj2.imp_separacion = 0; obj2.imp_cuota_inicial = 0; }

                                if (AnalizarNombreCuota(obj2.cod_variable, "12")) { imp_sep_12 = obj2.imp_separacion; imp_ini_12 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "18")) { imp_sep_18 = obj2.imp_separacion; imp_ini_18 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "24")) { imp_sep_24 = obj2.imp_separacion; imp_ini_24 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "36")) { imp_sep_36 = obj2.imp_separacion; imp_ini_36 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "48")) { imp_sep_48 = obj2.imp_separacion; imp_ini_48 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "60")) { imp_sep_60 = obj2.imp_separacion; imp_ini_60 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "especial")) { imp_sep_especial = obj2.imp_separacion; imp_ini_especial = obj2.imp_cuota_inicial; }
                            }

                        }
                    }
                    //gvListarCotizacion.RefreshData();
                }
            }

            if (e.Column.FieldName == "prc_interes")
            {
                if (obj.cod_variable != "")
                {
                    if (obj.prc_interes > 1 || AnalizarNombreCuota(obj.cod_variable, "contado")) { obj.prc_interes = 0; return; }

                    obj.imp_interes = (((obj.imp_precio_venta * (1 - obj.prc_descuento)) - obj.imp_cuota_inicial - obj.imp_separacion) * obj.prc_interes) * (obj.num_fraccion / 12);
                    //obj.imp_interes = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial) * obj.prc_interes;

                    if (obj.num_fraccion != 0)
                    {
                        obj.imp_valor_cuota = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial + obj.imp_interes) / obj.num_fraccion;
                    }
                }

                if (obj.cod_variable == "")
                {
                    if (obj.prc_interes >= 0)
                    {
                        for (int i = 0; i < gvListarCotizacion.RowCount; i++)
                        {
                            eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;

                            if (obj2.prc_interes > 1 || AnalizarNombreCuota(obj2.cod_variable, "contado")) { obj2.prc_interes = 0; continue; }
                            obj2.prc_interes = obj.prc_interes;
                            //obj2.imp_interes = obj2.cod_variable == "" ? 0 : (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial) * obj2.prc_interes;
                            obj2.imp_interes = obj2.cod_variable == "" ? 0 : (((obj2.imp_precio_venta * (1 - obj2.prc_descuento)) - obj2.imp_cuota_inicial - obj2.imp_separacion) * obj2.prc_interes) * (obj2.num_fraccion / 12);


                            if (obj2.num_fraccion != 0)
                            {
                                obj2.imp_valor_cuota = (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial + obj2.imp_interes) / obj2.num_fraccion;
                            }
                        }
                    }
                    //gvListarCotizacion.RefreshData();
                }
            }
            if (e.Column.FieldName == "prc_descuento")
            {
                if (AnalizarNombreCuota(obj.cod_variable, "48")) { obj.prc_descuento = 0; return; }
                if (obj.cod_variable != ""  && !AnalizarNombreCuota(obj.cod_variable, "48"))
                {
                    if ((obj.prc_descuento * 100) > prc_descuento_maximo || (obj.prc_descuento * 100) < 0)
                    {
                        obj.prc_descuento = 0;
                        MessageBox.Show("El porcentaje de descuento no debe ser mayor de " + prc_descuento_maximo.ToString("0.00") + ", ni menor de 0", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    obj.imp_descuento = obj.imp_precio_venta * obj.prc_descuento;
                    obj.imp_precio_final = obj.imp_precio_venta - obj.imp_descuento;
                    if (AnalizarNombreCuota(obj.cod_variable, "contado"))
                    {
                        obj.imp_interes = (((obj.imp_precio_venta * (1 - obj.prc_descuento)) - obj.imp_cuota_inicial - obj.imp_separacion) * obj.prc_interes) * (obj.num_fraccion / 12);
                        obj.imp_valor_cuota = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial + obj.imp_interes) / 1;
                    }
                    else
                    {
                        if (obj.imp_cuota_inicial + obj.imp_separacion > obj.imp_precio_final) { obj.imp_separacion = 0; obj.imp_cuota_inicial = 0; }
                        obj.imp_cuota_inicial = obj.prc_cuota_inicial == 0 ? 0 : (obj.imp_precio_final * obj.prc_cuota_inicial) - obj.imp_separacion;
                        obj.imp_interes = (((obj.imp_precio_venta * (1 - obj.prc_descuento)) - obj.imp_cuota_inicial - obj.imp_separacion) * obj.prc_interes) * (obj.num_fraccion / 12);
                        obj.imp_valor_cuota = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial + obj.imp_interes) / (obj.num_fraccion == 0 ? 1 : obj.num_fraccion);
                    }

                }
                if (obj.cod_variable == "")
                {
                    if ((descuento * 100) <= prc_descuento_maximo && (descuento * 100) >= 0)
                    {
                        //eProformas.eProformas_Detalle objEliminar = gvListarCotizacion.GetRow(numero-1) as eProformas.eProformas_Detalle;
                        //bsDetalleProformas.Remove(objEliminar);
                        for (int i = 0; i < gvListarCotizacion.RowCount; i++)
                        {
                            eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
                            if ((obj2.num_fraccion != 0 || AnalizarNombreCuota(obj2.cod_variable, "contado")) && obj2.cod_variable != "" && /*obj2.cod_variable != "EFI0004" &&*/ obj2.cod_variable != "EFI0005")
                            {
                                //obj2.imp_cuota_inicial = cuota_inicial;
                                //obj2.imp_separacion = separacio|n;
                                obj2.prc_descuento = descuento;
                                obj2.imp_descuento = obj2.imp_precio_venta * obj2.prc_descuento;
                                obj2.imp_precio_final = obj2.imp_precio_venta - obj2.imp_descuento;
                                if (AnalizarNombreCuota(obj2.cod_variable, "contado"))
                                {
                                    //obj2.imp_cuota_inicial = (obj2.imp_precio_final * obj2.prc_cuota_inicial) - obj2.imp_separacion;
                                    obj2.imp_cuota_inicial = obj2.prc_cuota_inicial == 0 ? 0 : (obj2.imp_precio_final * obj2.prc_cuota_inicial) - obj2.imp_separacion;
                                    obj2.imp_interes = (((obj2.imp_precio_venta * (1 - obj2.prc_descuento)) - obj2.imp_cuota_inicial - obj2.imp_separacion) * obj2.prc_interes) * (obj2.num_fraccion / 12);
                                    obj2.imp_valor_cuota = (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial + obj2.imp_interes) / 1;
                                }
                                else
                                {
                                    //obj2.imp_cuota_inicial = (obj2.imp_precio_final * obj2.prc_cuota_inicial) - obj2.imp_separacion;
                                    obj2.imp_cuota_inicial = obj2.prc_cuota_inicial == 0 ? 0 : (obj2.imp_precio_final * obj2.prc_cuota_inicial) - obj2.imp_separacion;
                                    obj2.imp_interes = (((obj2.imp_precio_venta * (1 - obj2.prc_descuento)) - obj2.imp_cuota_inicial - obj2.imp_separacion) * obj2.prc_interes) * (obj2.num_fraccion / 12);
                                    obj2.imp_valor_cuota = (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial + obj2.imp_interes) / obj2.num_fraccion;
                                }


                                //if (obj2.cod_variable == "EFI0001") { obj2.imp_separacion = 0; obj2.imp_cuota_inicial = 0; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "12")) { imp_sep_12 = obj2.imp_separacion; imp_ini_12 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "18")) { imp_sep_18 = obj2.imp_separacion; imp_ini_18 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "24")) { imp_sep_24 = obj2.imp_separacion; imp_ini_24 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "36")) { imp_sep_36 = obj2.imp_separacion; imp_ini_36 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "48")) { imp_sep_48 = obj2.imp_separacion; imp_ini_48 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "60")) { imp_sep_60 = obj2.imp_separacion; imp_ini_60 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "especial")) { imp_sep_especial = obj2.imp_separacion; imp_ini_especial = obj2.imp_cuota_inicial; }
                            }
                        }
                    }
                    else
                    {
                        descuento = 0;

                        for (int i = 0; i < gvListarCotizacion.RowCount; i++)
                        {
                            eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;
                            if ((obj2.num_fraccion != 0 || AnalizarNombreCuota(obj2.cod_variable, "contado")) && obj2.cod_variable != "" /*&& obj2.cod_variable != "EFI0004"*/ && obj2.cod_variable != "EFI0005")
                            {
                                //obj2.imp_cuota_inicial = cuota_inicial;
                                //obj2.imp_separacion = separacion;
                                obj2.prc_descuento = descuento;
                                obj2.imp_descuento = obj2.imp_precio_final * obj2.prc_descuento;
                                obj2.imp_precio_final = obj2.imp_precio_final - obj2.imp_descuento;
                                if (AnalizarNombreCuota(obj2.cod_variable, "contado"))
                                {
                                    obj2.imp_valor_cuota = (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial + obj2.imp_interes) / 1;
                                }
                                else
                                {
                                    obj2.imp_valor_cuota = (obj2.imp_precio_final - obj2.imp_separacion - obj2.imp_cuota_inicial + obj.imp_interes) / obj2.num_fraccion;
                                }

                                //if (obj2.cod_variable == "EFI0001") { obj2.imp_separacion = 0; obj2.imp_cuota_inicial = 0; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "12")) { imp_sep_12 = obj2.imp_separacion; imp_ini_12 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "18")) { imp_sep_18 = obj2.imp_separacion; imp_ini_18 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "24")) { imp_sep_24 = obj2.imp_separacion; imp_ini_24 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "36")) { imp_sep_36 = obj2.imp_separacion; imp_ini_36 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "48")) { imp_sep_48 = obj2.imp_separacion; imp_ini_48 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "60")) { imp_sep_60 = obj2.imp_separacion; imp_ini_60 = obj2.imp_cuota_inicial; }
                                if (AnalizarNombreCuota(obj2.cod_variable, "especial")) { imp_sep_especial = obj2.imp_separacion; imp_ini_especial = obj2.imp_cuota_inicial; }
                            }
                            else
                            {
                                obj2.prc_descuento = descuento;
                            }
                        }
                        MessageBox.Show("El porcentaje de descuento no debe ser mayor de " + prc_descuento_maximo.ToString("0.00") + ", ni menor de 0", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }

            if (e.Column.FieldName == "num_fraccion" && obj.cod_variable != "")
            {
                //if (obj.cod_variable == "EFI0001" && obj.cod_variable == "EFI0007" && obj.cod_variable == "EFI0014")
                //{
                //    obj.prc_descuento = 0;
                //    obj.num_fraccion = 0;
                //}
                //if (obj.cod_variable == "EFI0015")
                //{
                //    obj.num_fraccion = 18;
                //}

                //    if (obj.cod_variable == "EFI0002")
                //{
                //    obj.num_fraccion = 12;
                //}

                //if (obj.cod_variable == "EFI0003")
                //{
                //    obj.num_fraccion = 24;
                //}

                //if (obj.cod_variable == "EFI0004")
                //{
                //    obj.num_fraccion = 36;
                //}

                //if (obj.cod_variable == "EFI0005")
                //{
                //    obj.num_fraccion = 48;
                //}

                if (obj.num_fraccion <= 1 || obj.num_fraccion >= 11)
                {
                    MessageBox.Show("La cantidad de cuotas no debe ser mayor de 10, ni menor de 2", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    obj.num_fraccion = 2; return;
                }

                //if (obj.cod_variable == "EFI0006")
                //{
                gvListarCotizacion.SelectRow(gvListarCotizacion.RowCount - 1);
                obj.imp_valor_cuota = (obj.imp_precio_final - obj.imp_separacion - obj.imp_cuota_inicial + obj.imp_interes) / obj.num_fraccion;
                //}

                gvListarCotizacion.RefreshData();
                return;
            }
            //for (int i = 0; i < gvListarCotizacion.RowCount; i++)
            //{
            //    eProformas.eProformas_Detalle obj2 = gvListarCotizacion.GetRow(i) as eProformas.eProformas_Detalle;

            //}
        }

        private void validaciones()
        {
            if (txtCodCliente.Text == "")
            {

                if (String.IsNullOrEmpty(txtNombres.Text)) MessageBox.Show("Debe ingresar el nombre del cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                if (String.IsNullOrEmpty(txtNroDocumento.Text)) MessageBox.Show("Debe ingresar el documento del cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                if (String.IsNullOrEmpty(txtCorreo.Text) && String.IsNullOrEmpty(txtTelefono.Text)) MessageBox.Show("Debe ingresar el correo o teléfono del cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (String.IsNullOrEmpty(lkpEjecutivo.EditValue.ToString())) MessageBox.Show("Debe seleccionar un ejecutivo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
        }
        private eProformas AsignarValores_Proforma()
        {
            eProformas obj = new eProformas();
            obj.cod_proforma = txtCodigoProforma.Text;
            obj.cod_lote = cod_lote;
            obj.cod_empresa = cod_empresa;
            obj.cod_proyecto = cod_proyecto;
            obj.fch_proforma = dtFechaProforma.EditValue == null ? "" : dtFechaProforma.EditValue.ToString();
            obj.cod_cliente = txtCodCliente.Text;
            obj.cod_ejecutivo = lkpEjecutivo.EditValue.ToString();
            obj.dsc_nombre = txtNombres.Text;
            obj.dsc_apellido_paterno = txtApellidoPaterno.Text;
            obj.dsc_apellido_materno = txtApellidoMaterno.Text;
            obj.dsc_telefono = txtTelefono.Text;
            obj.cod_tipo_documento = lkpTipoDocumento.EditValue.ToString();
            obj.dsc_documento = txtNroDocumento.Text;
            obj.cod_estado_civil = lkpEstadoCivil.EditValue == null ? "" : lkpEstadoCivil.EditValue.ToString();
            obj.dsc_email = txtCorreo.Text;
            obj.dsc_observaciones = mmObservaciones.Text;
            obj.cod_variable = lkpNivelInteres.EditValue.ToString();
            obj.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            obj.dsc_cadena_direccion = txtDireccion.Text;
            return obj;
        }

        public eProformas.eProformas_Detalle AsignarValores_Proforma_Detalle(int x)
        {
            eProformas.eProformas_Detalle obj = gvListarCotizacion.GetRow(x) as eProformas.eProformas_Detalle;
            //eProformas.eProformas_Detalle obj = new eProformas.eProformas_Detalle();
            obj.cod_proforma = txtCodigoProforma.Text;
            obj.cod_proyecto = cod_proyecto;
            if (obj.flg_seleccion != "SI") obj.flg_seleccion = "NO";
            return obj;
        }
    }
}