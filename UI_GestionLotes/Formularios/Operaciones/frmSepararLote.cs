using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.Data.Mask.Internal;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Formularios.Operaciones;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    internal enum Separacion
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmSepararLote : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        eLotes_Separaciones eLotSep = new eLotes_Separaciones();
        public eProspectosXLote campos_prospecto = new eProspectosXLote();
        public eCampanha ecamp_prospecto = new eCampanha();

        public eProspectosXLote o_eProspecto; //Será asignado por el formulario MantEvento
        public bool fromMantEvent = false; //Asignado del formulario MantEvento...true sólo cuando venga de ahí

        public List<eLotes_Separaciones.eSeparaciones_Observaciones> mylistObservaciones = new List<eLotes_Separaciones.eSeparaciones_Observaciones>();
        public List<eLotes_Separaciones> mylistTipoSeparacion = new List<eLotes_Separaciones>();
        public List<eLotes_Separaciones.eSeparaciones_Documentos> mylistvalidar = new List<eLotes_Separaciones.eSeparaciones_Documentos>();
        public List<eLotes_Separaciones.eSeparaciones_Documentos> mylistDocumentos = new List<eLotes_Separaciones.eSeparaciones_Documentos>();
        public List<eLotes_Separaciones.eSeparaciones_Documentos> mylistDocumentosSeparacion = new List<eLotes_Separaciones.eSeparaciones_Documentos>();
        public List<eProformas.eProformas_Detalle> lstProDetalle = new List<eProformas.eProformas_Detalle>();
        DataTable tabla = new DataTable();

        frmSeparacionLotesProspectos frmHandler;

        public string cod_cliente = "", cod_empresa = "", cod_ejecutivo = "", cod_prospecto = "", dsc_proyecto = "", codigo = "", codigoMultiple = "", cod_lote = "", cod_separacion = "", estado = "", cod_documento_separacion = "", cod_separacion_padre = "";
        public string cod_status = "", flg_activo = "";
        internal Separacion MiAccion = Separacion.Nuevo;
        public eLotes_Separaciones campo_dias = new eLotes_Separaciones();
        public int validar = 0, validar2 = 0, num_linea = 0, num_cuotas = 0;
        public decimal imp_CUOI = 0, imp_penPago = 0;
        public Boolean extension = false, extensionFI = false, extensionCO = false, copropietario = false, verContrato = false, campoImpSep = false;
        decimal imp_separacion = 0;
        Image imgPrincipal = Properties.Resources.ok_16px;
        Brush Mensaje = Brushes.Transparent;
        Rectangle picRect, picRect2;
        int markWidth = 16;
        string varPathOrigen = "";
        string varNombreArchivo = "";
        int rowCount = 0;
        int validateFormClose = 0;
        private static string ClientId = "";
        private static string TenantId = "";
        private static string Instance = "https://login.microsoftonline.com/";
        public static IPublicClientApplication _clientApp;
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        public frmSepararLote()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        //LDAC - Creé esta clase para pasar el frmSepararLote. Será usada sólo cuando se registre un prospecto como cliente al guardar un evento como Separación.
        internal frmSepararLote(frmSeparacionLotesProspectos frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void gvObsSeparaciones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvObsSeparaciones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void frmSepararLote_Load(object sender, EventArgs e)
        {
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            labelAuditoria.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl1.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl2.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl3.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl4.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl5.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl6.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl7.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            lblDocumentos.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            lblEstado.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            btnBuscarLote.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnBuscarCopropietario.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnVerCopro.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnVerCli.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnBuscarProspecto.Appearance.BackColor = Program.Sesion.Colores.Verde;
            lblseguimiento.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chckValAdmin.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chckValBanco.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chkFinanciado.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chkContado.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            //chkActivoSeparacion.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            chckBoleteado.Properties.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            pcChevron.BackColor = Program.Sesion.Colores.Verde;
            txtSeparacion.Select();
            Inicializar();
            ocultarMostrarAuditoria();
            HabilitarBotones();
            //HabilitarCamposAdministrador();

            if (fromMantEvent)
            {
                frmMantCliente frm = new frmMantCliente(null, this, null);
                frm.AsignarCamposClientesProspecto(o_eProspecto);
                frm.cod_proyecto = o_eProspecto.cod_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.cod_etapas_multiple = "";

                frm.ShowDialog();
            }
            if (!string.IsNullOrEmpty(cod_ejecutivo)) { lkpAsesor.EditValue = cod_ejecutivo; }
        }
        private void ObtenerDatos_ObservacionesSeparaciones()
        {
            try
            {
                mylistObservaciones = unit.Proyectos.Obtener_LineasDetalleSeparacion<eLotes_Separaciones.eSeparaciones_Observaciones>(1, cod_separacion, codigo);
                bsLotesSeparaciones.DataSource = mylistObservaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarListadoDocumentos(string accion, string cod_memoria_desc = "")
        {
            try
            {
                int nRow = gvDocumentos.FocusedRowHandle;

                mylistDocumentos = unit.Proyectos.ListarDocumentoSeparacion<eLotes_Separaciones.eSeparaciones_Documentos>(accion, cod_separacion, codigo);
                eLotesDocumentos.DataSource = mylistDocumentos;
                gvDocumentos.FocusedRowHandle = nRow;
                gvDocumentos_FocusedRowChanged(gvDocumentos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(nRow - 1, nRow));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public string validarTipoDocExistente()
        {
            try
            {
                eLotes_Separaciones.eSeparaciones_Documentos obj = gvDocumentos.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Documentos;

                mylistvalidar = unit.Proyectos.ListarDocumentoSeparacion<eLotes_Separaciones.eSeparaciones_Documentos>("2", cod_cliente, codigo, obj.cod_documento_separacion);
                if (mylistvalidar.Count() > 0)
                {
                    return "Error al eliminar el tipo de documento \"" + mylistvalidar[0].dsc_nombre_doc + "\".\nSe encuentra registrado en lote \"" + mylistvalidar[0].dsc_lote + "\".";
                }
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private void obtenerListadoTipoDocumentoXSeparacion()
        {
            eLotesDocumentos.DataSource = null; eLotesDocumentos.DataSource = mylistDocumentos;
            if (MiAccion != Separacion.Nuevo)
            {
                List<eLotes_Separaciones.eSeparaciones_Documentos> lista = unit.Proyectos.ListarDocumentoSeparacion<eLotes_Separaciones.eSeparaciones_Documentos>("3", cod_separacion, codigo);
                //List<eVariablesGenerales> lista = unit.Proyectos.ListarTipoLotexEtapas<eVariablesGenerales>("3", cod_etapa);
                mylistDocumentosSeparacion = lista;

                foreach (eLotes_Separaciones.eSeparaciones_Documentos obj in lista)
                {
                    eLotes_Separaciones.eSeparaciones_Documentos oLoteEtap = mylistDocumentos.Find(x => x.cod_documento_separacion == obj.cod_documento_separacion);
                    if (oLoteEtap != null)
                    {
                        oLoteEtap.flg_PDF = obj.flg_PDF; oLoteEtap.idPDF = obj.idPDF;
                        oLoteEtap.dsc_nombre_doc_ref = obj.dsc_nombre_doc_ref; oLoteEtap.dsc_nombre_doc = obj.dsc_nombre_doc;
                    }
                }
            }
            gvDocumentos.RefreshData();
        }

        public void CargarListadoTipoSeparaciones(string accion)
        {
            try
            {
                int nRow = gvExtenciones.FocusedRowHandle;

                mylistTipoSeparacion = unit.Proyectos.ListarTipoSeparacion<eLotes_Separaciones>(accion, txtCodigoCliente.Text, codigo.Trim(), cod_lote.Trim(), cod_separacion.Trim());
                eTipoSeparaciones.DataSource = mylistTipoSeparacion;
                gvExtenciones.FocusedRowHandle = nRow;

                //gvDocumentos_FocusedRowChanged(gvDocumentos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(nRow - 1, nRow));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void grdbClienteTitular_SelectedIndexChanged(object sender, EventArgs e)
        {
            //unit.Clientes.CargaCombosLookUp("TipoEstadoSeparacion", lkpEstadoSeparacion, "cod_estado_separacion", "dsc_Nombre", "", cod_condicion: grdbEstado.SelectedIndex == 1 ? "2" : "1", valorDefecto: true);

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
                if (MiAccion == Separacion.Nuevo)
                {
                    unit.Proyectos.CargaCombosLookUp("ManzanaXEtapa", lkpManzana, "cod_manzana", "dsc_manzana", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: codigo, cod_condicion: "3", valorDefecto: true);
                }
                else
                {
                    unit.Proyectos.CargaCombosLookUp("ManzanaXEtapa", lkpManzana, "cod_manzana", "dsc_manzana", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: codigo, cod_cuatro: cod_lote, cod_condicion: "4", valorDefecto: true);

                }
            }
        }

        private void lkpManzana_EditValueChanged(object sender, EventArgs e)
        {
            lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtprcAreaUE.Text = "";
            txtPreTerreno.Text = "";
            if (lkpManzana.EditValue != null)
            {
                if (MiAccion == Separacion.Nuevo)
                {
                    unit.Proyectos.CargaCombosLookUp("LoteXmanza", lkpLote, "cod_lote", "num_lote", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: codigo, cod_tres: lkpManzana.EditValue.ToString(), cod_condicion: "8", valorDefecto: true);
                }
                else
                {
                    unit.Proyectos.CargaCombosLookUp("LoteXmanza", lkpLote, "cod_lote", "num_lote", "", cod_uno: lkpEtapa.EditValue.ToString(), cod_dos: codigo, cod_tres: lkpManzana.EditValue.ToString(), cod_cuatro: cod_lote, cod_condicion: "9", valorDefecto: true);

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
                    txtPreTerreno.EditValue = dataRow["imp_precio_total_valor"].ToString();
                    double valor = Convert.ToDouble(dataRow["prc_uso_exclusivo"]);
                    txtprcAreaUE.EditValue = Math.Round(valor, 6);
                    //txtprcAreaUE.Text = dataRow["prc_uso_exclusivo"].ToString();
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

                    if (MiAccion == Separacion.Nuevo)
                    {
                        if (Convert.ToDecimal(txtPreTerreno.EditValue.ToString().Replace("S/", "")) != 0)
                        {
                            frmDetProforma frm = new frmDetProforma();
                            frm.cod_proyecto = codigo;
                            frm.montoFinal = Convert.ToDecimal(txtPreTerreno.EditValue.ToString().Replace("S/", ""));
                            frm.ShowDialog();
                            if (frm.objDetalle != null && frm.objDetalle.dsc_nombre_detalle != null)
                            {
                                chkFinanciado.CheckState = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? CheckState.Unchecked : CheckState.Checked;
                                chkContado.CheckState = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? CheckState.Checked : CheckState.Unchecked;
                                txtDescuentoPorc.EditValue = frm.objDetalle.prc_descuento;
                                txtDescuentoSol.EditValue = frm.objDetalle.imp_descuento;
                                txtSeparacion.EditValue = frm.objDetalle.imp_separacion;
                                txtCuoInicial.EditValue = frm.objDetalle.imp_cuota_inicial;
                                txtPreFinalDescuento.EditValue = frm.objDetalle.imp_precio_final;
                                txtPorcInteres.EditValue = frm.objDetalle.prc_interes;
                                txtImpInteres.EditValue = frm.objDetalle.imp_interes;
                                txtPrecioFinalFinanciar.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? 0 : frm.objDetalle.imp_precio_final - frm.objDetalle.imp_cuota_inicial - frm.objDetalle.imp_separacion + frm.objDetalle.imp_interes;
                                txtFraccion.EditValue = frm.objDetalle.num_fraccion <= 10 ? frm.objDetalle.num_fraccion : 0;
                                lkpCuotas.EditValue = frm.objDetalle.cod_variable;
                                txtValorCuotas.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? 0 : frm.objDetalle.imp_valor_cuota;
                                txtPendientePago.EditValue = frm.objDetalle.dsc_nombre_detalle.ToLower().Contains("contado") ? frm.objDetalle.imp_valor_cuota : 0;
                            }
                        }
                    }
                }

            }
        }

        private void llenarVariableDiasVencimientoSeparacion()//y empresa
        {
            campo_dias = unit.Proyectos.ObtenerDiasSeparaciones<eLotes_Separaciones>(codigo);
            cod_empresa = campo_dias.cod_empresa;
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
                        case Separacion.Nuevo: result = Guardar(); break;
                        case Separacion.Editar: result = Modificar(); break;
                    }
                    //if (copropietario || MiAccion == Separacion.Vista) { result = Modificar(); }

                    if (result == "OK")
                    {
                        MessageBox.Show("Se guardó la separación de manera satisfactoria", "Guardar separación", MessageBoxButtons.OK);
                        //ActualizarListado = "SI";

                        validateFormClose = 1;
                        if (MiAccion == Separacion.Nuevo)
                        {
                            MiAccion = Separacion.Editar;
                            //GuardarTipoSeparacion();

                        }
                        //eLotes_Separaciones eLotSep = new eLotes_Separaciones();
                        eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("2", codigo, codigoMultiple, cod_separacion);
                        if (eLotSep.cod_cliente != null)
                        {
                            eCliente ePro = new eCliente();
                            ePro.cod_cliente = txtCodigoCliente.Text;
                            ePro.cod_empresa = eLotSep.cod_empresa;
                            ePro.cod_proyecto = eLotSep.cod_proyecto;
                            ePro.flg_activo = "SI";
                            ePro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                            ePro = unit.Proyectos.Guardar_Actualizar_ClienteProyecto<eCliente>(ePro);
                            if (ePro != null)
                            {
                                eCliente_Empresas eCliEmp = new eCliente_Empresas();
                                eCliEmp.cod_cliente = txtCodigoCliente.Text;
                                eCliEmp.cod_empresa = eLotSep.cod_empresa;
                                eCliEmp.cod_proyecto = eLotSep.cod_proyecto;
                                eCliEmp.flg_activo = "SI";
                                eCliEmp.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                eCliEmp.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                                eCliEmp.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                eCliEmp = unit.Clientes.Guardar_Actualizar_ClienteEmpresas<eCliente_Empresas>(eCliEmp);
                            }


                        }
                        if (eLotSep.fch_Reg_Val_Admin.Year == 1) { dtUsuarioRegValAdm.EditValue = null; }
                        else { dtUsuarioRegValAdm.EditValue = eLotSep.fch_Reg_Val_Admin; txtUsuarioRegValAdm.Text = eLotSep.cod_usuario_Val_Admin.ToString(); }


                        if (eLotSep.fch_Reg_Val_Banco.Year == 1) { dtUsuarioRegValBanco.EditValue = null; }
                        else { dtUsuarioRegValBanco.EditValue = eLotSep.fch_Reg_Val_Banco; txtUsuarioRegValBanco.Text = eLotSep.cod_usuario_Reg_Val_Banco.ToString(); }

                        if (eLotSep.fch_Reg_Boleteado.Year == 1) { dtUsuarioRegBolet.EditValue = null; }
                        else { dtUsuarioRegBolet.EditValue = eLotSep.fch_Reg_Boleteado; txtUsuarioRegBolet.Text = eLotSep.cod_usuario_Boleteado.ToString(); }

                        if (eLotSep.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; }
                        else { dtFechaRegistro.EditValue = eLotSep.fch_registro; txtUsuarioRegistro.Text = eLotSep.cod_usuario_registro.ToString(); }

                        if (eLotSep.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; }
                        else { dtFechaModificacion.EditValue = eLotSep.fch_cambio; txtUsuarioCambio.Text = eLotSep.cod_usuario_cambio.ToString(); }

                        //txtUsuarioRegistro.Text = eLotSep.cod_usuario_registro;
                        //if (eLotSep.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = eLotSep.fch_registro; }
                        //txtUsuarioCambio.Text = eLotSep.cod_usuario_cambio;
                        //if (eLotSep.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = eLotSep.fch_cambio; }
                        if (eLotSep.cod_status == "ESE00002")
                        {
                            btnDesistir.Enabled = true;
                            btnVender.Enabled = true;
                            barActEstado.Enabled = true;
                            btnImprimir.Enabled = true;
                            btnAdjuntar.Enabled = true;
                            btnVerDocumento.Enabled = true;
                            barActSegui.Enabled = true;
                            btnExtender.Enabled = true;
                            BloqueoControlesInformacionTerreno(true, true, false);
                        }
                        //if (eLotSep.cod_status == "ESE00002" && eLotSep.cod_forma_pago == "CO")
                        //{
                        //    btnExtender.Enabled = false;
                        //}

                        if (eLotSep.cod_status == "ESE00002" && eLotSep.flg_registrado == "SI" && eLotSep.flg_Val_Admin == "NO" && eLotSep.flg_Val_Banco == "NO" && eLotSep.flg_Boleteado == "NO")
                        {
                            btnAnular.Enabled = true;
                        }
                        else
                        {
                            btnAnular.Enabled = false;
                        }
                        barImprimir.Enabled = true;
                        string mensajito = GuardarObservaciones();
                        if (mensajito != null)
                        {
                            ObtenerDatos_ObservacionesSeparaciones();
                        }
                        if (frmHandler != null)
                        {
                            int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                            //frmHandler.limpiarFiltroTbi();
                            //frmHandler.CargarListado();
                            //frmHandler.CargarListadoResumen();
                            frmHandler.frmSeparacionLotesProspectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                            frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow;
                        }

                        btnVerCliente.Enabled = true;
                        btnVerCli.Enabled = true;
                        //AGREGO LOS CAMPOS PROSPECTOS QUE FALTAN
                        CargarListadoDocumentos("3");
                        //obtenerListadoTipoDocumentoXSeparacion();

                        if (cod_prospecto != null && cod_prospecto.Trim() != "")
                        {
                            Guardar_Prospecto();
                        }
                        //if (extension)
                        //{
                        //    CargarListadoTipoSeparaciones("1");
                        //}
                    }

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

        private string Guardar_Prospecto()
        {
            string result = "";

            eCampanha eCamp = AsignarValoresprospecto();
            eCampanha eCampResul = unit.Proyectos.Guardar_Actualizar_prospecto<eCampanha>(eCamp, "7");
            if (eCampResul != null)
            {
                if (eCampResul.cod_prospecto == "")
                {
                    //MessageBox.Show(eCampResul.mensaje, "Mantenimiento de prospectos", MessageBoxButtons.OK);
                    result = "NO";
                }
                else
                {
                    cod_prospecto = eCampResul.cod_prospecto;
                    result = "OK";
                }

            }
            return result;
        }

        private eCampanha AsignarValoresprospecto()
        {
            eCampanha eCamp = new eCampanha();

            //string[] Codigo = glkpempresa.EditValue.ToString().Split("|".ToCharArray());
            eCamp.cod_empresa = cod_empresa;
            eCamp.cod_proyecto = codigo;
            eCamp.cod_prospecto = cod_prospecto;
            //eCamp.cod_prospecto = txtCodigoProspecto.Text;
            //eCamp.dsc_apellido_paterno = txtApePaterno.Text;
            //eCamp.dsc_apellido_materno = txtApeMaterno.Text;
            eCamp.dsc_nombres = txtNombres.Text;
            eCamp.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            eCamp.dsc_num_documento = txtNroDocumento.Text;
            //eCamp.fch_fec_nac = Convert.ToString(dtFecNacimiento.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacimiento.EditValue));
            //de_fechanac.EditValue == null ? "" : de_fechanac.EditValue.ToString();
            //eCamp.cod_sexo = glkpsexo.EditValue == null ? "" : glkpsexo.EditValue.ToString();

            eCamp.dsc_email = txtCorreo.Text;
            eCamp.dsc_telefono_movil = txtTelef.Text;

            //eCamp.dsc_profesion = txtOcupacion.Text;
            eCamp.cod_estado_civil = lkpEstadoCivil.EditValue.ToString();
            //eCamp.cod_pais = lkpPais.EditValue.ToString();
            //eCamp.cod_departamento = lkpDepartamento.EditValue.ToString();
            //eCamp.cod_provincia = lkpProvincia.EditValue.ToString();
            //eCamp.cod_distrito = glkpDistrito.EditValue.ToString();
            eCamp.dsc_direccion = txtDireccion.Text;
            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            return eCamp;
        }

        private string GuardarObservaciones()
        {
            txtNroDocumento.Focus();
            txtNroDocumento.Select();
            gvObsSeparaciones.PostEditor();
            gvObsSeparaciones.RefreshData();
            eLotes_Separaciones.eSeparaciones_Observaciones eObsFact = new eLotes_Separaciones.eSeparaciones_Observaciones();

            for (int y = 0; y < gvObsSeparaciones.DataRowCount; y++)
            {
                eLotes_Separaciones.eSeparaciones_Observaciones obj = gvObsSeparaciones.GetRow(y) as eLotes_Separaciones.eSeparaciones_Observaciones;
                if (obj == null) continue;
                obj.cod_separacion = cod_separacion; obj.cod_proyecto = codigo; obj.cod_empresa = cod_empresa; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                eObsFact = unit.Proyectos.InsertarObservacionesSeparacion<eLotes_Separaciones.eSeparaciones_Observaciones>(obj);

            }

            if (eObsFact != null) { return "OK"; }

            return null;


        }
        private string GuardarObservacionDesistir(string observacion)
        {
            eLotes_Separaciones.eSeparaciones_Observaciones objObservacion = new eLotes_Separaciones.eSeparaciones_Observaciones();
            objObservacion.cod_separacion = cod_separacion; objObservacion.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario; objObservacion.num_linea = 0;
            objObservacion.dsc_observaciones = observacion;
            objObservacion = unit.Proyectos.InsertarObservacionesSeparacion<eLotes_Separaciones.eSeparaciones_Observaciones>(objObservacion);

            if (objObservacion != null) { return "OK"; }
            return null;

        }

        public string validarCampos()
        {
            if (lkpAsesor.EditValue == null)
            {
                lkpAsesor.ShowPopup();
                return "Debe seleccionar el asesor";
            }
            //if (txtNroDocumento.Text.Trim() == "")
            //{
            //    txtNroDocumento.Focus();
            //    return "Debe ingresar el número de documento";
            //}
            //if (txtTelef.Text.Trim() == "")
            //{
            //    txtTelef.Focus();
            //    return "Debe ingresar un teléfono";
            //}
            //if (txtCorreo.Text.Trim().Length > 0)
            //{
            //    if (!new EmailAddressAttribute().IsValid(txtCorreo.Text.Trim()))
            //    {
            //        txtCorreo.Focus();
            //        return "Debe seleccionar un correo válido";
            //    }
            //}
            //if (txtNombres.Text.Trim() == "")
            //{
            //    txtNombres.Focus();
            //    return "Debe ingresar el nombre";
            //}

            //if (txtApePaterno.Text.Trim() == "")
            //{
            //    txtApePaterno.Focus();
            //    return "Debe ingresar el apellido paterno";
            //}
            //if (txtApeMaterno.Text.Trim() == "")
            //{
            //    txtApeMaterno.Focus();
            //    return "Debe ingresar el apellido materno";
            //}
            //if (dtFecNacimiento.EditValue == null)
            //{
            //    dtFecNacimiento.ShowPopup();
            //    return "Debe ingresar la fecha de nacimiento";
            //}
            //if (lkpEstadoCivil.EditValue == null)
            //{
            //    lkpEstadoCivil.ShowPopup();
            //    return "Debe ingresar el estado civil";
            //}
            //if (txtOcupacion.Text.Trim() == "")
            //{
            //    txtOcupacion.Focus();
            //    return "Debe ingresar ocupación";
            //}
            //if (mmDireccion.Text.Trim() == "")
            //{
            //    mmDireccion.Focus();
            //    return "Debe ingresar la dirección";
            //}
            if (dtFecVecSeparacion.EditValue == null)
            {
                dtFecVecSeparacion.ShowPopup();
                return "Debe ingresar la fecha vencimiento de la separación";

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
            //if (txtAreaM2.Text.Trim() == "")
            //{
            //    txtAreaM2.Focus();
            //    return "Debe ingresar el Área m²";
            //}
            //if (txtAreaM2.Text.Trim() == "0")
            //{
            //    txtAreaM2.Focus();
            //    return "Debe ingresar el Área m²";
            //}
            //if (txtPreTerreno.Text.Trim() == "")
            //{
            //    txtPreTerreno.Focus();
            //    return "Debe ingresar el Precio del Lote";
            //}
            //if (txtDescuentoPorc.Text.Trim() == "0.00 %")
            //{
            //    txtDescuentoPorc.Focus();
            //    return "Debe ingresar el descuento %";
            //}
            //if (txtSeparacion.Text.Trim() == "0.00")
            //{
            //    txtSeparacion.Focus();
            //    return "Debe ingresar el Importe de separación";
            //}
            if (txtCuoInicial.Text.Trim() == "0.00" && chkFinanciado.CheckState == CheckState.Checked)
            {
                txtCuoInicial.Focus();
                return "Debe ingresar la cuota inicial";
            }
            //if (chkFinanciado.CheckState == CheckState.Checked)
            //{
            if (lkpCuotas.EditValue == null && chkFinanciado.CheckState == CheckState.Checked)
            {
                lkpCuotas.ShowPopup();
                return "Seleccionar N° de cuotas";
            }

            if (dtFecPagoCuota.EditValue == null && chkFinanciado.CheckState == CheckState.Checked)
            {
                dtFecPagoCuota.ShowPopup();
                return "Debe ingresar la fecha de pago CUOI";
            }

            if (dtFecVecCuota.EditValue == null && chkFinanciado.CheckState == CheckState.Checked)
            {
                dtFecVecCuota.ShowPopup();
                return "Debe ingresar la fecha de vencimiento 1° cuota";
            }

            if (dtFecPagoContado.EditValue == null && chkContado.CheckState == CheckState.Checked)
            {
                dtFecPagoContado.ShowPopup();
                return "Debe ingresar la fecha Pago al Contado";
            }
            //if (txtValorCuotas.Text.Trim() == "0.00" && grdbContFinan.SelectedIndex == 1)
            //{
            //    txtValorCuotas.Focus();
            //    return "Debe ingresar el valor de cuotas";
            //}


            //if (txtFraccion.Text.Trim() == "0.00" && grdbContFinan.SelectedIndex == 0)
            //{
            //    txtFraccion.Focus();
            //    return "Debe ingresar la fracción";
            //}
            return null;
        }

        private void LimpiarCamposSeparacion()
        {
            cod_cliente = null;
            txtCodigoCliente.Text = "";
            //txtCodigoProspecto.Text = "";
            //grdbEstado.SelectedIndex = 0;
            chkFinanciado.CheckState = CheckState.Checked;
            txtCodSepara.Text = "";

            txtDescuentoPorc.EditValue = "0";
            txtDescuentoPorc.Refresh();
            lkpAsesor.EditValue = null;
            lkpContacto.EditValue = null;
            lkpContactoCo.EditValue = null;
            glkpTipoDocumento.EditValue = "DI001";
            txtTelef.Text = "";
            txtCorreo.Text = "";
            txtNombres.Text = "";
            //txtApePaterno.Text = "";
            //txtApeMaterno.Text = "";
            //dtFecNacimiento.EditValue = DateTime.Today.AddYears(-18).AddDays(-1);
            lkpEstadoCivil.EditValue = "01";
            //eDirec.cod_departamento = lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString();
            //txtOcupacion.Text = "";
            txtDireccion.Text = "";

            txtCodigoCopropietario.Text = "";
            glkpTipoDocumentoCopropietario.EditValue = "DI001";
            txtNroDocumentoCopropietario.Text = "";
            lkpEstadoCivilCopropietario.EditValue = "01";
            txtNombresCopropietario.Text = "";
            txtTelefCopropietario.Text = "";
            txtCorreoCopropietario.Text = "";
            txtDireccionCopropietario.Text = "";

            lkpEtapa.EditValue = null;
            lkpManzana.Properties.DataSource = null;
            lkpLote.Properties.DataSource = null;
            //lkpLote.EditValue = null;
            txtAreaM2.Text = "";
            txtprcAreaUE.Text = "";
            txtPreTerreno.Text = "";
            LimpiarCamposFormaPago();

            txtNroDocumento.Text = "";
            txtSeparacion.EditValue = "0";
            txtSeparacion.Refresh();
            txtSeparacion.Focus();

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
            txtPendientePago.EditValue = "0";
            txtPendientePago.Refresh();
            txtAnteriorCotizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            txtSiguienteCotizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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
            txtImpInteres.EditValue = "0";
            txtImpInteres.Refresh();
            dtFecPagoCuota.EditValue = null;
            dtFecVecCuota.EditValue = null;
            txtValorCuotas.EditValue = "0";
            txtValorCuotas.Refresh();
            lkpCuotas.EditValue = null;
            txtFraccion.EditValue = "0";
            txtFraccion.Refresh();
        }


        private void LimpiarAuditoria()
        {
            dtFechaModificacion.EditValue = null;
            txtUsuarioCambio.Text = "";
            dtUsuarioRegValAdm.EditValue = null;
            txtUsuarioRegValAdm.Text = "";
            dtUsuarioRegDesistimiento.EditValue = null;
            txtUsuarioRegDesistimiento.Text = "";
            dtUsuarioRegValBanco.EditValue = null;
            txtUsuarioRegValBanco.Text = "";
            dtUsuarioRegAnul.EditValue = null;
            txtUsuarioRegAnul.Text = "";
            dtUsuarioRegBolet.EditValue = null;
            txtUsuarioRegBolet.Text = "";
        }

        private string Guardar()
        {
            string result = "";
            eLotes_Separaciones eLoSep = AsignarValoresSeparaciones();
            eLoSep = unit.Proyectos.MantenimientoSeparaciones<eLotes_Separaciones>(eLoSep);
            if (eLoSep != null)
            {
                cod_separacion = eLoSep.cod_separacion;
                txtCodSepara.Text = cod_separacion;

                //    GuardarClienteDirecciones();
                //    //GuardarClienteContactos();
                //    //GuardarClienteCentroResponsabilidad();
                //    //GuardarDireccionContactos();
                //    //GuardarDireccionUbicaciones();

                //    eCliente_Direccion eDirec = new eCliente_Direccion();
                //    eDirec = AsignarValoresDireccion();

                //    if (MiAccion == Cliente.Nuevo)
                //    {
                //        eDirec = unit.Clientes.Guardar_Actualizar_ClienteDireccion<eCliente_Direccion>(eDirec, txtCodDireccion.Text == "0" ? "Nuevo" : "Actualizar");
                //    }

                //    ObtenerListadoDirecciones();
                //    ObtenerListadoClientesContactos();
                //    //ObtenerListadoCentroResponsabilidad();
                //    //ObtenerListadoEmpresasCliente();
                //    CargarListadoProyecto("TODOS");
                result = "OK";
            }
            return result;
        }

        private string GuardarTipoSeparacion()
        {
            string result = "";
            eLotes_Separaciones eLoSep = AsignarValoresTipoSeparaciones();
            eLoSep = unit.Proyectos.ExtencionSeparacion<eLotes_Separaciones>("1", eLoSep);
            if (eLoSep != null)
            {

                result = "OK";
            }
            return result;
        }

        private eLotes_Separaciones AsignarValoresTipoSeparaciones()
        {
            eLotes_Separaciones eLoSep = new eLotes_Separaciones();
            eLoSep.cod_separacion = txtCodSepara.Text.Trim();
            eLoSep.cod_empresa = cod_empresa.Trim();
            eLoSep.cod_proyecto = codigo.Trim();
            eLoSep.cod_cliente = txtCodigoCliente.Text;
            eLoSep.cod_lote = cod_lote.Trim();
            eLoSep.cod_etapa = lkpEtapa.EditValue.ToString();
            eLoSep.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario.Trim();

            return eLoSep;
        }

        private string Modificar()
        {
            string result = "";
            eLotes_Separaciones eLoSep = new eLotes_Separaciones();
            eLoSep = AsignarValoresSeparaciones();
            eLoSep = unit.Proyectos.MantenimientoSeparaciones<eLotes_Separaciones>(eLoSep);
            if (eLoSep != null)
            {
                cod_separacion = eLoSep.cod_separacion;

                result = "OK";
            }
            return result;
        }

        private eLotes_Separaciones AsignarValoresSeparaciones()
        {
            eLotes_Separaciones eLoSep = new eLotes_Separaciones();
            eLoSep.cod_separacion = txtCodSepara.Text.Trim();
            eLoSep.cod_asesor = lkpAsesor.EditValue != null ? lkpAsesor.EditValue.ToString() : "";
            //eLoSep.dsc_apellido_paterno = txtApePaterno.Text.Trim();
            //eLoSep.dsc_apellido_materno = txtApeMaterno.Text.Trim();
            eLoSep.dsc_nombre = txtNombres.Text.Trim();
            eLoSep.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            eLoSep.dsc_documento = txtNroDocumento.Text.Trim();
            eLoSep.dsc_email = txtCorreo.Text.Trim();
            eLoSep.dsc_telefono_1 = txtTelef.Text.Trim();
            eLoSep.fch_nacimiento = new DateTime();
            //eLoSep.fch_nacimiento = dtFecNacimiento.EditValue == null || dtFecNacimiento.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecNacimiento.EditValue);
            eLoSep.cod_estadocivil = lkpEstadoCivil.EditValue == null ? "" : lkpEstadoCivil.EditValue.ToString();
            //eLoSep.dsc_ocupacion = txtOcupacion.Text.Trim();
            eLoSep.dsc_direccion = txtDireccion.Text.Trim();
            eLoSep.cod_forma_pago = chkFinanciado.CheckState == CheckState.Checked ? "FI" : "CO"; //grdbEstado.EditValue.ToString();
            eLoSep.cod_lote = cod_lote.Trim();
            eLoSep.cod_etapa = lkpEtapa.EditValue.ToString();
            eLoSep.cod_empresa = cod_empresa.Trim();
            eLoSep.cod_proyecto = codigo.Trim();
            eLoSep.cod_manzana = lkpManzana.EditValue.ToString();
            eLoSep.num_area_uex = Convert.ToDecimal(txtAreaM2.Text);
            //eLoSep.cod_forma_pago = grdbContFinan.EditValue.ToString();
            eLoSep.imp_precio_total = Convert.ToDecimal(txtPreTerreno.Text.ToString());
            eLoSep.prc_descuento = Convert.ToDecimal(txtDescuentoPorc.Text.Trim().Replace("%", "")) / 100;
            eLoSep.imp_descuento = Convert.ToDecimal(txtDescuentoSol.Text);
            eLoSep.imp_precio_final = Convert.ToDecimal(txtPrecioFinalFinanciar.Text);
            eLoSep.prc_interes = Convert.ToDecimal(txtPorcInteres.EditValue);
            eLoSep.imp_interes = Convert.ToDecimal(txtImpInteres.EditValue);
            eLoSep.imp_separacion = Convert.ToDecimal(txtSeparacion.Text);
            eLoSep.imp_cuota_inicial = Convert.ToDecimal(txtCuoInicial.Text);
            if (chkFinanciado.CheckState == CheckState.Checked)
            {
                eLoSep.cod_cuotas = lkpCuotas.EditValue.ToString();
            }

            //eLoSep.num_cuotas = Convert.ToInt32(num_cuotas);
            eLoSep.num_fraccion = Convert.ToInt32(txtFraccion.Text);
            eLoSep.imp_valor_cuota = Convert.ToDecimal(txtValorCuotas.Text.ToString());
            eLoSep.fch_vct_cuota = dtFecVecCuota.EditValue == null || dtFecVecCuota.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecVecCuota.EditValue);
            eLoSep.fch_vct_separacion = dtFecVecSeparacion.EditValue == null || dtFecVecSeparacion.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecVecSeparacion.EditValue);
            eLoSep.fch_vct_pago_total = dtFecPagoContado.EditValue == null || dtFecPagoContado.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecPagoContado.EditValue);
            eLoSep.fch_pago_separacion = dtFecPagoSeparacion.EditValue == null || dtFecPagoSeparacion.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecPagoSeparacion.EditValue);
            eLoSep.fch_pago_cuota = dtFecPagoCuota.EditValue == null || dtFecPagoCuota.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecPagoCuota.EditValue.ToString());
            eLoSep.fch_pago_total = dtFecPagoContado.EditValue == null || dtFecPagoContado.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFecPagoContado.EditValue);
            eLoSep.flg_activo = "SI";
            //eLoSep.flg_activo = chkActivoSeparacion.CheckState == CheckState.Checked ? "SI" : "NO";
            eLoSep.fch_Separacion = dtFechaSeparacion.EditValue == null || dtFechaSeparacion.EditValue.ToString() == "" ? new DateTime() : Convert.ToDateTime(dtFechaSeparacion.EditValue);

            eLoSep.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario.Trim();
            eLoSep.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario.Trim();
            //eLoSep.cod_cliente = cod_cliente;
            eLoSep.cod_cliente = txtCodigoCliente.Text; // cod_cliente != null || cod_cliente.Trim() != "" ? cod_cliente.ToString().Trim() : "";
            eLoSep.cod_copropietario = txtCodigoCopropietario.Text; // txtCodigoCopropietario.Text != null || txtCodigoCopropietario.Text.Trim() != "" ? txtCodigoCopropietario.Text.Trim() : null;
            //eLoSep.cod_prospecto = "";
            eLoSep.cod_prospecto = cod_prospecto;
            //eLoSep.cod_prospecto = txtCodigoProspecto.Text.ToString();
            eLoSep.flg_cliente = txtCodigoCliente.Text.ToString().Trim() != "" ? "SI" : "NO";// cod_cliente != null || cod_cliente.Trim() != "" ? "SI" : "NO";
            eLoSep.flg_prospecto = "NO";
            if (cod_prospecto != null)
            {
                eLoSep.flg_prospecto = cod_prospecto.Trim() != "" ? "SI" : "NO";

            }
            else
            {
                eLoSep.flg_prospecto = "NO";
            }
            eLoSep.flg_es_extension = extension ? "SI" : "NO";
            eLoSep.cod_separacion_padre = cod_separacion_padre;
            eLoSep.imp_precio_con_descuento = Convert.ToDecimal(txtPreFinalDescuento.Text.Trim());
            eLoSep.imp_pendiente_pago = Convert.ToDecimal(txtPendientePago.Text.Trim());
            eLoSep.dsc_linea_contacto = lkpContacto.EditValue.ToString();
            eLoSep.dsc_linea_contacto_copro = lkpContactoCo.EditValue != null ? lkpContactoCo.EditValue.ToString() : "";

            //if (grdbEstado.EditValue.ToString() == "0")
            //{
            //    eLoSep.fch_registro = DateTime.Now;
            //    eLoSep.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            //}
            //if (grdbEstado.EditValue.ToString() == "1")
            //{
            //    eLoSep.fch_Reg_Val_Admin = DateTime.Now;
            //    eLoSep.cod_usuario_Val_Admin = Program.Sesion.Usuario.cod_usuario;
            //}
            //if (grdbEstado.EditValue.ToString() == "2")
            //{
            //    eLoSep.fch_Reg_Val_Banco = DateTime.Now;
            //    eLoSep.cod_usuario_Reg_Val_Banco = Program.Sesion.Usuario.cod_usuario;
            //}
            //if (grdbEstado.EditValue.ToString() == "3")
            //{
            //    eLoSep.fch_Reg_Boleteado = DateTime.Now;
            //    eLoSep.cod_usuario_Boleteado= Program.Sesion.Usuario.cod_usuario;
            //}

            return eLoSep;
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LimpiarCamposSeparacion();
            LimpiarAuditoria();
            LimpiarSeguimiento();
            MiAccion = Separacion.Nuevo;
            codigoMultiple = "ALL";
            extension = false;
            campoImpSep = false;
            extensionFI = false;
            extensionCO = false;
            Inicializar();
            chkFinanciado.CheckState = CheckState.Unchecked;
            chkFinanciado.CheckState = CheckState.Checked;
            chkFinanciado.Enabled = true;
            chkContado.Enabled = true;

            //Nuevo();
            //lkpEtapa.DataBindings

            //unit.Clientes.CargaCombosLookUp("EtapasFiltroProyecto", lkpEtapa, "cod_etapa", "dsc_descripcion", "", valorDefecto: true, codigo: codigo, codigoMultiple: codigoMultiple);

            //BloqueoControlesInformacionSeparación(true, false, false);
            //BloqueoControlesInformacionTerreno(true, false, false);

            //btnBuscarProspecto.Enabled = true;

            bsLotesSeparaciones.DataSource = null;
            eLotesDocumentos.DataSource = null;
            eTipoSeparaciones.DataSource = null;
            gvExtenciones.RefreshData();
            gvDocumentos.RefreshData();
            gvObsSeparaciones.RefreshData();

        }

        private void dtFecNacimiento_EditValueChanged(object sender, EventArgs e)
        {
            //if (dtFecNacimiento.EditValue != null)
            //{
            //    FechaMayorEdad();
            //}

        }

        private void FechaMayorEdad()
        {
            //DateTime date = DateTime.Today;

            //DateTime fechaSeleccionada = (DateTime)dtFecNacimiento.EditValue;
            //if (fechaSeleccionada.AddYears(18) > date)
            //{
            //    MessageBox.Show("Error, tiene que ser persona mayor a 18 años", "Se requiere persona mayor de edad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    DateTime oInicioFechaNac = date.AddYears(-18).AddDays(-1); //new DateTime(date.Year, date.Month, 1);
            //    dtFecNacimiento.EditValue = oInicioFechaNac;
            //}
        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
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

            //    //calcularValorCuotasFraccion();
            //}

        }

        private void HabilitarCamposAdministrador()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarPerfilesUsuario<eVentana>(4,Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                eVentana oFreeAdmiin = listPermisos.Find(x => x.cod_perfil == 33 || x.cod_perfil == 31);
                if(oFreeAdmiin != null)
                {
                    chkFinanciado.Enabled = true;
                    chkContado.Enabled = true;
                    txtCuoInicial.ReadOnly = false;
                    lkpCuotas.ReadOnly = false;
                    dtFecPagoCuota.ReadOnly = false;
                    dtFecVecCuota.ReadOnly = false;
                    txtPrecioFinalFinanciar.ReadOnly = false;
                    txtPendientePago.ReadOnly = false;
                    dtFecPagoContado.ReadOnly = false;
                }
                else
                {
                    barActEstado.Enabled = false;
                    barActSegui.Enabled = false;
                }
            }
        }

        private void txtSeparacion_EditValueChanged(object sender, EventArgs e)
        {
            //txtImpSepVisible.Text = txtSeparacion.Text;
            if (lkpLote.EditValue != null)
            {
                if (!extension)
                {
                    //if (chkFinanciado.CheckState == CheckState.Checked)
                    //{
                    //    calcularSaldoFinanciar();
                    //    calcularValorCuotas();
                    //}
                    //else
                    //{
                    //    calcularPreFinalDescuentos();
                    //}
                }
                else
                {

                    if (extensionFI)
                    {
                        var calcularCUOI = imp_CUOI - Convert.ToDecimal(txtSeparacion.EditValue);
                        if (calcularCUOI < 0)
                        {
                            txtSeparacion.EditValue = "0";
                            txtSeparacion.Refresh();
                            txtCuoInicial.EditValue = imp_CUOI;
                            txtCuoInicial.Refresh();

                        }
                        else
                        {
                            txtCuoInicial.EditValue = imp_CUOI - Convert.ToDecimal(txtSeparacion.EditValue);
                        }
                        eLotes_Separaciones oLoteEtap = mylistTipoSeparacion.Find(x => x.cod_separacion == "0");
                        if (oLoteEtap != null) { oLoteEtap.imp_separacion = Convert.ToDecimal(txtSeparacion.Text); }
                        gvExtenciones.RefreshData();

                    }
                    if (extensionCO)
                    {
                        var calcularPreFi = imp_penPago - Convert.ToDecimal(txtSeparacion.EditValue);
                        //decimal sumSeparacion = 0;
                        //foreach (eLotes_Separaciones obj in mylistTipoSeparacion)
                        //{                           
                        //    if (obj.cod_separacion != "0")
                        //    {
                        //        sumSeparacion += obj.imp_separacion;
                        //    }
                        //}
                        //eLotes_Separaciones oLoteEtap1 = mylistTipoSeparacion.Find(x => x.cod_separacion != "0");
                        //if (oLoteEtap1 != null) { sumSeparacion += oLoteEtap1.imp_separacion; }
                        //gvExtenciones.RefreshData();

                        if (calcularPreFi < 0)
                        {
                            txtSeparacion.EditValue = "0";
                            txtSeparacion.Refresh();
                            txtPreFinalDescuento.EditValue = imp_penPago;
                            txtPreFinalDescuento.Refresh();

                        }
                        else
                        {
                            txtPendientePago.EditValue = imp_penPago - Convert.ToDecimal(txtSeparacion.EditValue);
                        }
                        eLotes_Separaciones oLoteEtap = mylistTipoSeparacion.Find(x => x.cod_separacion == "0");
                        if (oLoteEtap != null) { oLoteEtap.imp_separacion = Convert.ToDecimal(txtSeparacion.Text); }
                        gvExtenciones.RefreshData();

                    }
                }

                //calcularSaldoFinanciar();
                //calcularValorCuotas();
                //calcularValorCuotasFraccion();
            }
        }

        private void txtCuoInicial_EditValueChanged(object sender, EventArgs e)
        {
            //if (lkpLote.EditValue != null && Convert.ToDecimal(txtCuoInicial.EditValue) > 0)
            //{
            //    if (chkFinanciado.CheckState == CheckState.Checked)
            //    {
            //        if (!extension)
            //        {
            //            calcularSaldoFinanciar();
            //            calcularValorCuotas();
            //        }

            //    }
            //    //calcularSaldoFinanciar();
            //    //calcularValorCuotas();
            //    //calcularValorCuotasFraccion();
            //}
        }

        private void txtNumCuotas_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpCuotas.EditValue != null)
            {
                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                    if (Convert.ToInt32(dataRow["num_cuotas"]) > 11)
                    {
                        //num_cuotas = Convert.ToInt32(dataRow["num_cuotas"]);
                        //txtFraccion.Text = "";
                        //txtFraccion.ReadOnly = true;
                    }
                    else
                    {
                        //txtFraccion.ReadOnly = false;
                        //num_cuotas = Convert.ToInt32(dataRow["num_cuotas"]);
                        //txtFraccion.Text = num_cuotas.ToString();
                    }


                    //txtPorcInteres.EditValue = Convert.ToDecimal(txtPorcInteres.EditValue) > 0 ? txtPorcInteres.EditValue : dataRow["prc_interes"].ToString();
                    //calcularInteres();
                    //if (chkFinanciado.CheckState == CheckState.Checked)
                    //{
                    //    calcularSaldoFinanciar();
                    //    calcularValorCuotas();
                    //}

                    //calcularSaldoFinanciar();
                    //calcularValorCuotas();

                }

            }


        }

        private decimal calcularInteres()
        {
            decimal interes = 0;
            interes = Convert.ToDecimal(txtPrecioFinalFinanciar.Text.ToString()) * (Convert.ToDecimal(txtPorcInteres.Text.Trim().Replace("%", "")) / 100);
            //txtValorInteres.Text = interes.ToString();
            return interes;

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
        }

        private void calcularPreFinalDescuentos()
        {
            if (lkpLote.EditValue != null)
            {
                decimal calcularDescuentoPorc = 0, precioFinal = 0, impSeparacion = 0, cuoInicial = 0, pendientePago = 0;

                calcularDescuentoPorc = Convert.ToDecimal(txtPreTerreno.Text.ToString()) * Convert.ToDecimal(txtDescuentoPorc.EditValue);

                precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcularDescuentoPorc);

                pendientePago = precioFinal - Convert.ToDecimal(txtSeparacion.Text.Trim());

                //precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcular) - Convert.ToDecimal(txtSeparacion.Text.Trim()) - Convert.ToDecimal(txtCuoInicial.Text.Trim());
                txtPreFinalDescuento.Text = precioFinal.ToString();
                txtPendientePago.Text = pendientePago.ToString();
            }

        }


        private void calcularValorCuotasFraccion()
        {
            //if(txtNumCuotas.Text.ToString() != "0")
            //{

            if (!validarCadenaVacio(txtFraccion.Text.Trim()) && chkContado.CheckState == CheckState.Checked)
            {
                decimal calcularCuotas = 0, numCuotas = 0;
                numCuotas = Convert.ToDecimal(txtFraccion.Text.Trim());
                //if (numCuotas > 0 && numCuotas < 4)
                if (numCuotas < 11)
                {
                    calcularCuotas = Convert.ToDecimal(txtPrecioFinalFinanciar.Text.ToString()) / numCuotas;
                    txtValorCuotas.Text = calcularCuotas.ToString();
                }
                else
                {
                    txtValorCuotas.Text = "";
                }
            }
            //calcularCuotas = Convert.ToDecimal(txtPreFinal.Text.ToString()) / Convert.ToDecimal(txtNumCuotas.Text.ToString());

            //}


        }

        private void calcularSaldoFinanciar()
        {

            decimal calcular = 0, precioFinal = 0, impSeparacion = 0, cuoInicial = 0, interes = 0;

            calcular = Convert.ToDecimal(txtPreTerreno.Text.ToString()) * (Convert.ToDecimal(txtDescuentoPorc.Text.Trim().Replace("%", "")) / 100);

            if (!validarCadenaVacio(txtSeparacion.Text.Trim()))
            {
                impSeparacion = Convert.ToDecimal(txtSeparacion.Text.Trim());
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


        private void calcularPrecioFinal()
        {

            decimal calcularDescuentoPorc = 0, precioFinal = 0, impSeparacion = 0, cuoInicial = 0, pendientePago = 0;

            calcularDescuentoPorc = Convert.ToDecimal(txtPreTerreno.Text.ToString()) * Convert.ToDecimal(txtDescuentoPorc.EditValue);
            precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcularDescuentoPorc);
            if (chkContado.CheckState == CheckState.Checked)
            {
                pendientePago = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcularDescuentoPorc - Convert.ToDecimal(txtSeparacion.Text.Trim()));
            }


            //precioFinal = (Convert.ToDecimal(txtPreTerreno.Text.Trim()) - calcular) - Convert.ToDecimal(txtSeparacion.Text.Trim()) - Convert.ToDecimal(txtCuoInicial.Text.Trim());
            txtPreFinalDescuento.Text = precioFinal.ToString();
            txtPendientePago.Text = pendientePago.ToString();
        }

        private Boolean validarCadenaVacio(string valor)
        {
            if (valor == null)
            {
                return true;
            }
            if (valor.Trim() == "")
            {
                return true;
            }
            if (valor.Trim().Length == 0)
            {
                return true;
            }
            return false;
        }

        private void txtFraccion_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtFraccion.Text) > 0)
            {
                //if (Convert.ToInt32(txtFraccion.Text.Trim()) <= 10)
                //{
                //    num_cuotas = Convert.ToInt32(txtFraccion.Text.Trim());
                //    calcularValorCuotas();
                //}
                //else
                //{
                //    txtFraccion.EditValue = "1";
                //    txtFraccion.Refresh();
                //    num_cuotas = Convert.ToInt32(txtFraccion.Text.Trim());
                //    calcularValorCuotas();
                //}
            }
            //if (lkpCuotas.EditValue != null && num_cuotas <= 10)
            //{
            //    txtFraccion.ReadOnly = false;
            //}
            //else
            //{
            //    txtFraccion.ReadOnly = true;

            //}

        }

        private void gvObsSeparaciones_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eLotes_Separaciones.eSeparaciones_Observaciones obj = gvObsSeparaciones.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Observaciones;
            if (obj == null)
            {
                mylistObservaciones.Add(new eLotes_Separaciones.eSeparaciones_Observaciones() { num_linea = 0 });
                bsLotesSeparaciones.DataSource = mylistObservaciones;
                obj = gvObsSeparaciones.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Observaciones;
            }
            obj.fch_registro = DateTime.Today; obj.dsc_usuario_registro = Program.Sesion.Usuario.dsc_usuario;
            gvObsSeparaciones.RefreshData();

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                eLotes_Separaciones.eSeparaciones_Observaciones obj = gvObsSeparaciones.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Observaciones;
                if (obj == null) return;
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta observación?", "Eliminar observación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eLotes_Separaciones.eSeparaciones_Observaciones eObs = gvObsSeparaciones.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Observaciones;
                    string result = unit.Proyectos.Eliminar_SeparacionObservaciones(cod_separacion, eObs.num_linea);
                    ObtenerDatos_ObservacionesSeparaciones();
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
                extension = false;
                campoImpSep = false;
                extensionFI = false;
                extensionCO = false;
                int tRow = frmHandler.gvListaSeparaciones.RowCount - 1;
                int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                eLotes_Separaciones obj = frmHandler.gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                codigo = obj.cod_proyecto;
                cod_separacion = obj.cod_separacion;
                codigoMultiple = obj.cod_etapa;
                cod_cliente = obj.cod_cliente;
                cod_status = obj.cod_status;
                flg_activo = obj.flg_activo;
                MiAccion = obj.cod_status != "ESE00002" || obj.flg_activo == "NO" ? Separacion.Vista : Separacion.Editar;
                Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void siguienteCotizacion()
        {
            int tRow = lstProDetalle.Count;
            if (rowCount == 0 && rowCount < tRow)
            {
                rowCount++;
                if (lstProDetalle[rowCount].cod_variable == "EFI0001") { chkContado.CheckState = CheckState.Checked; } else { chkFinanciado.CheckState = CheckState.Checked; }
                txtSeparacion.Text = lstProDetalle[rowCount].imp_separacion.ToString();
                txtDescuentoPorc.Text = lstProDetalle[rowCount].prc_descuento.ToString();
                txtCuoInicial.Text = lstProDetalle[rowCount].imp_cuota_inicial.ToString();
                lkpCuotas.EditValue = lstProDetalle[rowCount].cod_variable;
            }

        }

        private void anteriorCotizacion()
        {
            int tRow = lstProDetalle.Count;
            if (rowCount > 0 && rowCount < tRow)
            {
                rowCount--;
                if (lstProDetalle[rowCount].cod_variable == "EFI0001") { chkContado.CheckState = CheckState.Checked; } else { chkFinanciado.CheckState = CheckState.Checked; }
                txtSeparacion.Text = lstProDetalle[rowCount].imp_separacion.ToString();
                txtDescuentoPorc.Text = lstProDetalle[rowCount].prc_descuento.ToString();
                txtCuoInicial.Text = lstProDetalle[rowCount].imp_cuota_inicial.ToString();
                lkpCuotas.EditValue = lstProDetalle[rowCount].cod_variable;
            }

        }

        private void picSiguienteSeparacion_Click(object sender, EventArgs e)
        {
            try
            {
                extension = false;
                campoImpSep = false;
                extensionFI = false;
                extensionCO = false;
                int tRow = frmHandler.gvListaSeparaciones.RowCount - 1;
                int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eLotes_Separaciones obj = frmHandler.gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                codigo = obj.cod_proyecto;
                cod_separacion = obj.cod_separacion;
                codigoMultiple = obj.cod_etapa;
                cod_cliente = obj.cod_cliente;
                cod_status = obj.cod_status;
                flg_activo = obj.flg_activo;



                MiAccion = obj.cod_status != "ESE00002" || obj.flg_activo == "NO" ? Separacion.Vista : Separacion.Editar;
                Inicializar();
                //Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente(null, this, null);
                //Si abro desde nuevo solo me creara un cliente y me retornara todos sus datos
                if (MiAccion == Separacion.Nuevo) { frm.MiAccionSeparacion = Cliente.Nuevo; }

                //Si abro desde editar primero tengo que consultar si mi lote ya tiene un cliente
                if (MiAccion == Separacion.Editar)
                {

                    //eCliente eCli = new eCliente();
                    frm.MiAccionSeparacion = Cliente.Editar;
                    //si si existe abrira a ese cliente o de lo contrario solo le seteara los campos parecidos de separaciones
                    //eCli = unit.Clientes.Obtener_ClienteExistente<eCliente>(43, cod_cliente);
                    //if (eCli != null)
                    //{
                    if (txtCodigoCliente.Text.ToString().Trim() != "")
                    {
                        frm.cod_cliente = txtCodigoCliente.Text;
                        frm.MiAccion = Cliente.Vista;
                    }

                    //}
                    //else
                    //{
                    //    frm.MiAccionSeparacion = Cliente.Editar;
                    //}
                }
                if (MiAccion == Separacion.Vista) { frm.MiAccionSeparacion = Cliente.Vista; }

                frm.cod_separacion = cod_separacion;
                frm.cod_proyecto = codigo;
                frm.cod_empresa = cod_empresa;
                frm.cod_etapas_multiple = codigoMultiple;




                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones(this, null);
                frm.MiAccion = ListCliSeparacion.Nuevo;




                frm.codigo_proyecto = codigo;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void glkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpTipoDocumento.EditValue == null) { return; }

            if (glkpTipoDocumento.EditValue.ToString() == "DI004")
            {
                txtNroDocumento.Properties.MaskSettings.Configure<MaskSettings.Simple>(settings =>
                {
                    settings.MaskExpression = "99999999999";
                    //settings.AutoHideDecimalSeparator = false;
                    //settings.HideInsignificantZeros = true;
                });

            }

            if (glkpTipoDocumento.EditValue.ToString() == "DI001")
            {
                txtNroDocumento.Properties.MaskSettings.Configure<MaskSettings.Simple>(settings =>
                {
                    settings.MaskExpression = "99999999";
                    //settings.AutoHideDecimalSeparator = false;
                    //settings.HideInsignificantZeros = true;
                });
            }

            if (glkpTipoDocumento.EditValue.ToString() != "DI001" && glkpTipoDocumento.EditValue.ToString() != "DI004")
            {
                txtNroDocumento.Properties.ResetMaskSettings();

            }
            txtNroDocumento.Text = "";
            txtSeparacion.Focus();
        }

        public void AsignarCamposSeparacionProspecto()
        {
            glkpTipoDocumento.EditValue = campos_prospecto.cod_tipo_documento != null ? campos_prospecto.cod_tipo_documento.ToString() : "";
            txtNroDocumento.Text = campos_prospecto.dsc_num_documento != null ? campos_prospecto.dsc_num_documento.ToString() : "";
            cod_prospecto = campos_prospecto.cod_prospecto != null ? campos_prospecto.cod_prospecto.ToString() : "";
            //txtCodigoProspecto.Text = campos_prospecto.cod_prospecto != null ? campos_prospecto.cod_prospecto.ToString() : "";
            txtNombres.Text = campos_prospecto.dsc_nombres != null ? campos_prospecto.dsc_apellido_paterno.ToString() + " " + campos_prospecto.dsc_nombres.ToString() : "";

            //txtApePaterno.Text = campos_prospecto.dsc_apellido_paterno != null ? campos_prospecto.dsc_apellido_paterno.ToString() : "";
            //txtApeMaterno.Text = campos_prospecto.dsc_apellido_materno != null ? campos_prospecto.dsc_apellido_materno.ToString() : "";
            //txtNombres.Text = campos_prospecto.dsc_nombres != null ? campos_prospecto.dsc_nombres.ToString() : "";
            txtTelef.Text = campos_prospecto.dsc_telefono_movil != null ? campos_prospecto.dsc_telefono_movil.ToString() : "";
            txtCorreo.Text = campos_prospecto.dsc_email != null ? campos_prospecto.dsc_email.ToString() : "";
            //if (campos_prospecto.fch_fec_nac.Year == 1) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = campos_prospecto.fch_fec_nac; }
            lkpEstadoCivil.EditValue = campos_prospecto.cod_estado_civil != null ? campos_prospecto.cod_estado_civil.ToString() : "";
            //txtOcupacion.Text = campos_prospecto.dsc_profesion != null ? campos_prospecto.dsc_profesion.ToString() : "";
            txtDireccion.Text = campos_prospecto.dsc_direccion != null ? campos_prospecto.dsc_direccion.ToString() : "";

            //if (campos_prospecto.fch_fec_nac.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = Convert.ToDateTime(campos_prospecto.fch_fec_nac); }

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones(this, null);
                frm.MiAccion = ListCliSeparacion.Nuevo;




                frm.codigo_proyecto = codigo;
                frm.cod_empresa = cod_empresa;
                frm.dsc_proyecto = dsc_proyecto;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



       

        private void dtFecPagoSeparacion_EditValueChanged(object sender, EventArgs e)
        {
            if (dtFecPagoSeparacion.EditValue != null)
            {
                DateTime date = (DateTime)dtFecPagoSeparacion.EditValue;
                dtFecVecSeparacion.EditValue = date.AddDays(10);
                //string fecha = validarFechaSeparacion((DateTime)dtFecPagoSeparacion.EditValue);
                //if (fecha != null)
                //{
                //    MessageBox.Show(fecha, "Guardar Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    dtFecPagoSeparacion.EditValue = DateTime.Now;
                //}
                //DateTime date = DateTime.Now;
                //DateTime oInicioFechaNac = date.AddYears(-18).AddDays(-1); //new DateTime(date.Year, date.Month, 1);
                //DateTime oVencimiento = date.AddDays(campo_dias.num_dias_venc_sep);
            }
        }


        private string validarFechaSeparacion(DateTime Fecha)//Valida si la fecha seleccionada es mayor a la actual
        {
            if (Fecha != null)
            {
                DateTime date = DateTime.Now;
                DateTime fechaSeleccionada = Fecha;
                if (fechaSeleccionada > date)
                {
                    return "Error, la fecha de pago de la separación no puede ser mayor a la fecha actual";

                }
            }
            return null;
        }

        //private void dtFecPagoCuota_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (dtFecPagoCuota.EditValue == null) { return; }
        //    DateTime date = DateTime.Today;
        //    DateTime fechaSeleccionada = (DateTime)dtFecPagoCuota.EditValue;
        //    if (fechaSeleccionada > date)
        //    {
        //        MessageBox.Show("Error, la fecha de pago de la cuota inicial no puede ser mayor a la fecha actual", "Guardar Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        dtFecPagoCuota.EditValue = date;
        //    }
        //}

        private void dtFecPagoContado_EditValueChanged(object sender, EventArgs e)
        {
            //if (dtFecPagoContado.EditValue == null) { return; }
            //DateTime date = DateTime.Now;
            //DateTime fechaSeleccionada = (DateTime)dtFecPagoContado.EditValue;
            //if (fechaSeleccionada > date)
            //{
            //    MessageBox.Show("Error, la fecha de pago de la cuota al contado no puede ser mayor a la fecha actual", "Guardar Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dtFecPagoContado.EditValue = date;
            //}
        }

        private void btnDesistir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de desistir la separación? \nEsta acción es irreversible.", "Desistir Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    string result = unit.Proyectos.Actualizar_Status_Separacion("1", eLotSep.cod_separacion, eLotSep.cod_proyecto, eLotSep.cod_etapa, eLotSep.cod_lote, Program.Sesion.Usuario.cod_usuario);
                    if (result == "OK")
                    {
                        XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingresar motivo de lote desistido";
                        MemoEdit rbtntxtObser = new MemoEdit(); rbtntxtObser.Width = 120;
                        eLotesxProyecto objLotPro = new eLotesxProyecto();
                        rbtntxtObser.Properties.Name = "rtxtFrenteM";
                        args.Editor = rbtntxtObser;
                        var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                        if ((res == DialogResult.OK || res == DialogResult.Yes) && (rbtntxtObser.Text.Trim() != ""))
                        {
                            string mensajito = GuardarObservacionDesistir(rbtntxtObser.Text);
                            if (mensajito != null)
                            {
                                ObtenerDatos_ObservacionesSeparaciones();
                            }
                        }
                        MessageBox.Show("Separación desistida.", "Desistir Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnVenders.Enabled = false;
                        btnDesistirs.Enabled = false;
                        btnAnular.Enabled = false;
                        btnAdjuntar.Enabled = false;
                        dtUsuarioRegDesistimiento.EditValue = DateTime.Now;
                        txtUsuarioRegDesistimiento.Text = Program.Sesion.Usuario.cod_usuario.ToString();
                        if (frmHandler != null)
                        {
                            int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                            frmHandler.frmSeparacionLotesProspectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                            frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow;
                        }
                        MiAccion = Separacion.Vista;
                        Ver(true);
                        BloqueoControlesInformacionCliente(false, true, false);
                        BloqueoControlesInformacionSeparación(true, true, true);
                        BloqueoControlesInformacionTerreno(true, true, false);
                        BloqueoControlesInformacionVenta(true, true, true, true);
                        btnNuevo.Enabled = true;

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //CAMBIA EL ESTADO DE LA SEPARACION EN DESISITIDO Y TAMBIEN EL ESTADO DEL LOTE DE CONFIGURACION DE LOTES A LIBRE

        }

        private void grdbContFinan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCamposFormaPago();
            if (chkFinanciado.CheckState == CheckState.Checked) //grdbContFinan.EditValue.ToString() == "FI")
            {
                BloqueoControlesInformacionVenta(true, false, true, false);
                //txtCuoInicial.Enabled = true;
                //txtNumCuotas.Enabled = true;
                //dtFecVecCuota.Enabled = true;
                //txtFraccion.Enabled = false;
                //dtFecPagoCuota.Enabled = true;
                //dtFecPagoContado.Enabled = false;
                //dtFecVecPagoContado.Enabled = false;
            }
            if (chkContado.CheckState == CheckState.Checked) // grdbContFinan.EditValue.ToString() == "CO")
            {
                BloqueoControlesInformacionVenta(true, true, false, false);
                txtFraccion.ReadOnly = true;
                //txtCuoInicial.Enabled = false;
                //txtNumCuotas.Enabled = false;
                //dtFecVecCuota.Enabled = false;
                //txtFraccion.Enabled = true;
                //dtFecPagoCuota.Enabled = false;
                //dtFecPagoContado.Enabled = true;
                //dtFecVecPagoContado.Enabled = true;
            }
        }

        private void btnAdjuntarVoucher_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void lkpPais_EditValueChanged(object sender, EventArgs e)
        {
            //glkpDistrito.Properties.DataSource = null;
            //lkpProvincia.Properties.DataSource = null;
            //lkpDepartamento.Properties.DataSource = null;

            //unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "", cod_condicion: lkpPais.EditValue != null ? lkpPais.EditValue.ToString() : "");

        }

        private void lkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            //glkpDistrito.Properties.DataSource = null;
            //lkpProvincia.Properties.DataSource = null;
            //unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "", cod_condicion: lkpDepartamento.EditValue != null ? lkpDepartamento.EditValue.ToString() : "");

        }

        private void lkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            //glkpDistrito.Properties.DataSource = null;
            //unit.Clientes.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue != null ? lkpProvincia.EditValue.ToString() : "");

        }

        private void btnAnular_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de anular la separación? \nEsta acción es irreversible.", "Anular Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    string result = unit.Proyectos.Actualizar_Status_Separacion("3", eLotSep.cod_separacion, eLotSep.cod_proyecto, eLotSep.cod_etapa, eLotSep.cod_lote, Program.Sesion.Usuario.cod_usuario);
                    if (result == "OK")
                    {
                        XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingresar motivo de lote anulado";
                        MemoEdit rbtntxtObser = new MemoEdit(); rbtntxtObser.Width = 120;
                        eLotesxProyecto objLotPro = new eLotesxProyecto();
                        rbtntxtObser.Properties.Name = "rtxtFrenteM";
                        args.Editor = rbtntxtObser;
                        var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                        if ((res == DialogResult.OK || res == DialogResult.Yes) && (rbtntxtObser.Text.Trim() != ""))
                        {
                            string mensajito = GuardarObservacionDesistir(rbtntxtObser.Text);
                            if (mensajito != null)
                            {
                                ObtenerDatos_ObservacionesSeparaciones();
                            }
                        }
                        MessageBox.Show("Separación anulada.", "Anular Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnVenders.Enabled = false;
                        btnDesistirs.Enabled = false;
                        btnAnular.Enabled = false;
                        dtUsuarioRegAnul.EditValue = DateTime.Now;
                        txtUsuarioRegAnul.Text = Program.Sesion.Usuario.cod_usuario.ToString();
                        if (frmHandler != null)
                        {
                            int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                            frmHandler.frmSeparacionLotesProspectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                            frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow;
                        }
                        MiAccion = Separacion.Vista;
                        Ver(true);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmSepararLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void CargarCombos()
        {
            string accion = MiAccion == Separacion.Nuevo ? "1" : "2";
            //CargarCombosGridLookup("EtapasFiltroProyecto", lkpEtapa, "cod_etapa", "dsc_descripcion", "");
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "DI001", valorDefecto: true);
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumentoCopropietario, "cod_tipo_documento", "dsc_tipo_documento", "DI001", valorDefecto: true);
            unit.Clientes.CargaCombosLookUp("EtapasFiltroProyecto", lkpEtapa, "cod_etapa", "dsc_descripcion", "", valorDefecto: true, codigo: codigo, codigoMultiple: codigoMultiple);
            unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", lkpEstadoCivil, "cod_estado_civil", "dsc_estado_civil", "01", valorDefecto: true);
            unit.Clientes.CargaCombosLookUp("TipoEstadoCivil", lkpEstadoCivilCopropietario, "cod_estado_civil", "dsc_estado_civil", "01", valorDefecto: true);
            //unit.Clientes.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "00001"); //lkpPais.EditValue = "00001";
            unit.Clientes.CargaCombosLookUp("Vendedor", lkpAsesor, "cod_asesor", "dsc_asesor", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario, cod_condicion: accion);
            //unit.Proyectos.CargaCombosLookUp("TipoMoneda", lkpMoneda, "cod_moneda", "dsc_simbolo", "SOL");
            unit.Proyectos.CargaCombosLookUp("Cuotas", lkpCuotas, "cod_cuotas", "dsc_cuotas", "",cod_uno: codigo);
            //unit.Clientes.CargaCombosLookUp("TipoEstadoSeparacion", lkpEstadoSeparacion, "cod_estado_separacion", "dsc_Nombre", "", cod_condicion:"1" , valorDefecto: true);
            if (MiAccion == Separacion.Nuevo)
            {
                picAnteriorSeparacion.Enabled = false; picSiguienteSeparacion.Enabled = false; //btnNuevo.Enabled = false;
            }
        }

        private void btnVender_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string result = unit.Proyectos.Actualizar_Status_Separacion("2", eLotSep.cod_separacion, eLotSep.cod_proyecto, eLotSep.cod_etapa, eLotSep.cod_lote, Program.Sesion.Usuario.cod_usuario);
            if (result == "OK")
            {
                XtraMessageBox.Show("Separación vendida con éxito.", "Guardar Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                barActEstado.Enabled = false;
                btnVenders.Enabled = false;
                btnDesistirs.Enabled = false;
                btnAnular.Enabled = false;
                btnBuscarProspecto.Enabled = false;
                btnBuscarCopropietario.Enabled = false;
                btnAdjuntar.Enabled = false;
                eLotSep.cod_status = "ESE00001";
                Ver(true);
                BloqueoControlesInformacionCliente(false, true, false);
                BloqueoControlesInformacionSeparación(true, true, true);
                BloqueoControlesInformacionTerreno(true, true, false);
                BloqueoControlesInformacionVenta(true, true, true, true);
                //statusVendido();

                //cambiarStatusLote(objLT.cod_lote.ToString(), "LIBRE");
            }
        }

        private void btnBuscarProspecto_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones(this, null);
                frm.MiAccion = ListCliSeparacion.Nuevo;
                frm.codigo_proyecto = codigo;
                frm.cod_empresa = cod_empresa;
                frm.dsc_proyecto = dsc_proyecto;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chckRegistrado_CheckStateChanged(object sender, EventArgs e)
        {
            //chckRegistrado.Properties.Appearance.Font = chckRegistrado.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
        }

        private void chckValAdmin_CheckStateChanged(object sender, EventArgs e)
        {
            chckValAdmin.Properties.Appearance.Font = chckValAdmin.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
        }

        private void chckValBanco_CheckedChanged(object sender, EventArgs e)
        {
            chckValBanco.Properties.Appearance.Font = chckValBanco.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
        }

        private void chckBoleteado_CheckedChanged(object sender, EventArgs e)
        {
            chckBoleteado.Properties.Appearance.Font = chckBoleteado.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
        }

        private void btnValAdmin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string result = unit.Proyectos.Actualizar_Seguimiento_Separacion("1", eLotSep.cod_separacion, eLotSep.cod_proyecto, Program.Sesion.Usuario.cod_usuario);
            if (result == "OK")
            {
                XtraMessageBox.Show("Seguimiento Actualizado con éxito.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnVenders.Enabled = false;
                //btnDesistirs.Enabled = false;
                //btnAnular.Enabled = false;
                //eLotSep.cod_status = "ESE00001";
                //statusVendido();
                chckValAdmin.CheckState = CheckState.Checked;
                if (frmHandler != null)
                {
                    int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                    frmHandler.frmSeparacionLotesProspectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                    frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow;
                }
                Editar();
                //cambiarStatusLote(objLT.cod_lote.ToString(), "LIBRE");
            }
        }

        private void btnValBanco_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string result = unit.Proyectos.Actualizar_Seguimiento_Separacion("2", eLotSep.cod_separacion, eLotSep.cod_proyecto, Program.Sesion.Usuario.cod_usuario);
            if (result == "OK")
            {
                XtraMessageBox.Show("Seguimiento Actualizado con éxito.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnVenders.Enabled = false;
                //btnDesistirs.Enabled = false;
                //btnAnular.Enabled = false;
                //eLotSep.cod_status = "ESE00001";
                //statusVendido();
                chckValBanco.CheckState = CheckState.Checked;
                if (frmHandler != null)
                {
                    int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                    frmHandler.frmSeparacionLotesProspectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                    frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow;
                }
                Editar();
                //cambiarStatusLote(objLT.cod_lote.ToString(), "LIBRE");
            }
        }

        private void btnDenominacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                //eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                if (codigo == null) { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                rptFichaDenominacion report = new rptFichaDenominacion();
                ReportPrintTool printTool = new ReportPrintTool(report);
                //detalleLotes printTool = new detalleLotes(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_proyecto"].Value = codigo;
                report.Parameters["cod_lote"].Value = cod_lote;
                //report.BackColor = Color.FromArgb(0, 157, 150);
                //SplashScreen.Close();
                SplashScreenManager.CloseForm(false);

                report.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBoleteado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string result = unit.Proyectos.Actualizar_Seguimiento_Separacion("3", eLotSep.cod_separacion, eLotSep.cod_proyecto, Program.Sesion.Usuario.cod_usuario);
            if (result == "OK")
            {
                XtraMessageBox.Show("Seguimiento Actualizado con éxito.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnVenders.Enabled = false;
                //btnDesistirs.Enabled = false;
                //btnAnular.Enabled = false;
                //eLotSep.cod_status = "ESE00001";
                //statusVendido();
                chckBoleteado.CheckState = CheckState.Checked;
                if (frmHandler != null)
                {
                    int nRow = frmHandler.gvListaSeparaciones.FocusedRowHandle;
                    frmHandler.frmSeparacionLotesProspectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                    frmHandler.gvListaSeparaciones.FocusedRowHandle = nRow;
                }
                Editar();
                //cambiarStatusLote(objLT.cod_lote.ToString(), "LIBRE");
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            //tabla = unit.Clientes.ObtenerListadoGridLookup(nCombo);
            tabla = unit.Proyectos.ObtenerListadoGridLookup(nCombo, codigo: codigo, codigoMultiple: codigoMultiple);
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

        private void Nuevo()
        {
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.dsc_usuario;
            txtUsuarioCambio.Text = "";
            //dtFecPagoSeparacion.EditValue = DateTime.Now;

            dtFechaRegistro.EditValue = DateTime.Now;
            //dtEstadoSeparacion.EditValue = DateTime.Now;
            dtFechaModificacion.EditValue = null;
            //lkpEstadoSeparacion.EditValue = "ESE00002";
            DateTime date = DateTime.Now;
            DateTime oInicioFechaNac = date.AddYears(-18).AddDays(-1); //new DateTime(date.Year, date.Month, 1);
            DateTime oVencimiento = date.AddDays(campo_dias.num_dias_venc_sep); //new DateTime(date.Year, date.Month, 1);
            //dtFecNacimiento.EditValue = oInicioFechaNac;
            dtFecVecSeparacion.EditValue = oVencimiento;

            //DateTime oTerminoDiaDelMes = oInicioDiaDelMes.AddMonths(1).AddDays(-1);
            //DateTime oEntregaDiaDelMes = oInicioDiaDelMes.AddYears(1);

            //grdbEstado.SelectedIndex = 0;
            //grdbContFinan.SelectedIndex = 1;
            //btnBuscarProspectos.Enabled = true;
            //picBuscarCliente.Enabled = true;

            //grdbClienteTitular_SelectedIndexChanged(grdbEstado, new EventArgs());
            //grdbContFinan_SelectedIndexChanged(grdbContFinan, new EventArgs());
            layoutControl2.Enabled = true;
            //layoutControl16.Enabled = true;
            btnGuardar.Enabled = false;
            picAnteriorSeparacion.Enabled = false;
            picSiguienteSeparacion.Enabled = false;
            barActEstado.Enabled = false;
            barActSegui.Enabled = false;
            btnBuscarProspecto.Enabled = true;
            btnBuscarCopropietario.Enabled = true;
            btnExtender.Enabled = false;
            btnVerCliente.Enabled = false;
            btnVerCli.Enabled = false;
            btnVerCopro.Enabled = false;
            //txtSeparacion.Enabled = true;
            //dtFechaSeparacion.Enabled = true;
            //dtFecVecSeparacion.Enabled = true;
            //lkpEtapa.Enabled = true;
            //lkpManzana.Enabled = true;
            //lkpLote.Enabled = true;
            //glkpDistrito.EditValue = null;
            //lkpProvincia.EditValue = null;
            //lkpDepartamento.EditValue = null;
            btnAnular.Enabled = false;
            //chkActivoSeparacion.CheckState = CheckState.Checked;
            lblEstado.Text = "ESTADO DE SEPARACIÓN   :   SEPARADO";

            chkFinanciado.CheckState = CheckState.Checked;

            //chkFlgJuridica_CheckStateChanged(chkFlgJuridica, new EventArgs());
            //xtraTabControl1.Enabled = false;
            dtFechaSeparacion.EditValue = DateTime.Now;
            dtFecPagoSeparacion.EditValue = DateTime.Now;

            OcultarExtension();
            //CargarListadoTipoSeparaciones("2");            
            //CargarListadoDocumentos("1");
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Separacion.Nuevo:
                    llenarVariableDiasVencimientoSeparacion();
                    CargarCombos();
                    Nuevo();
                    BloqueoControlesInformacionSeparación(true, false, false);
                    BloqueoControlesInformacionCliente(true, true, false);
                    BloqueoControlesInformacionTerreno(true, false, true);
                    break;
                case Separacion.Editar:
                    llenarVariableDiasVencimientoSeparacion();
                    CargarCombos();
                    Editar();
                    BloqueoControlesInformacionCliente(false, true, false);
                    BloqueoControlesInformacionSeparación(true, false, !extension);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    break;

                case Separacion.Vista:
                    llenarVariableDiasVencimientoSeparacion();
                    CargarCombos();
                    Editar();
                    Ver(true);
                    BloqueoControlesInformacionCliente(false, true, false);
                    BloqueoControlesInformacionSeparación(true, true, !verContrato);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    BloqueoControlesInformacionVenta(true, true, true, true);
                    gvObsSeparaciones.OptionsBehavior.Editable = false;
                    //gvDocumentos.OptionsBehavior.Editable = false;
                    break;
            }
        }


        private void OcultarExtension()
        {
            layoutControlItem84.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            splitterItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }
        private void VerExtension()
        {
            layoutControlItem84.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            splitterItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

        }

        private void Ver(Boolean ReadOnly)
        {
            //gvObsSeparaciones.OptionsBehavior.Editable = Editable;
            //layoutControl2.Enabled = false;
            //layoutControl16.Enabled = false;
            btnNuevo.Enabled = ReadOnly;
            btnBuscarProspecto.Enabled = false;
            btnBuscarCopropietario.Enabled = false;
            btnAdjuntarVoucher.Enabled = false;
            btnDesistirs.Enabled = false;
            btnAnular.Enabled = false;
            btnVenders.Enabled = false;
            btnGuardar.Enabled = false;
            barActSegui.Enabled = false;
            barActEstado.Enabled = false;
            btnExtender.Enabled = false;
            txtFraccion.ReadOnly = true;
            //if (estado != "ESE00002")
            //{
            //    btnNuevo.Enabled = true;
            //}
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false)
                {
                    Ver(false);
                    BloqueoControlesInformacionCliente(false, true, false);
                    BloqueoControlesInformacionSeparación(true, true, true);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    BloqueoControlesInformacionVenta(true, true, true, true);
                }

            }

            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);
            eVentana oPerfilVisualizador = listPerfil.Find(x => x.cod_perfil == 32);
            if (oPerfilVisualizador != null)
            {
                if (MiAccion == Separacion.Editar)
                {
                    Ver(false);
                    BloqueoControlesInformacionCliente(false, true, false);
                    BloqueoControlesInformacionSeparación(true, true, true);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    BloqueoControlesInformacionVenta(true, true, true, true);
                }
                if (MiAccion == Separacion.Vista)
                {
                    Ver(false);
                    BloqueoControlesInformacionCliente(false, true, false);
                    BloqueoControlesInformacionSeparación(true, true, false);
                    BloqueoControlesInformacionTerreno(true, true, false);
                    BloqueoControlesInformacionVenta(true, true, true, true);
                }
            }
            //eVentana oPerfilRegistrador = listPerfil.Find(x => x.cod_perfil == 34);
            //if (oPerfilRegistrador != null)
            //{
            //    //if (MiAccion == Separacion.Editar)
            //    //{
            //    //    Ver(true, false, true, true);
            //    //}
            //    //if (MiAccion == Separacion.Vista)
            //    //{
            //    //    Ver(true, false, true, false);
            //    //}
            //}
        }
        private void btnFormatoSimple_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                if (cod_lote == null) { MessageBox.Show("Debe seleccionar lote.", "Ficha del lote", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }


                if (chkFinanciado.CheckState == CheckState.Checked)
                {
                    rptFichaSeparacionFinanciado reportFinanciado = new rptFichaSeparacionFinanciado();
                    ReportPrintTool printTool = new ReportPrintTool(reportFinanciado);
                    //detalleLotes printTool = new detalleLotes(report);
                    reportFinanciado.RequestParameters = false;
                    printTool.AutoShowParametersPanel = false;
                    reportFinanciado.Parameters["cod_proyecto"].Value = codigo;
                    reportFinanciado.Parameters["cod_separacion"].Value = cod_separacion;
                    //report.BackColor = Color.FromArgb(0, 157, 150);
                    reportFinanciado.ShowPreviewDialog();
                }
                else
                {
                    rptFichaSeparacionContado reportContado = new rptFichaSeparacionContado();
                    ReportPrintTool printTool = new ReportPrintTool(reportContado);
                    //detalleLotes printTool = new detalleLotes(report);
                    reportContado.RequestParameters = false;
                    printTool.AutoShowParametersPanel = false;
                    reportContado.Parameters["cod_proyecto"].Value = codigo;
                    reportContado.Parameters["cod_separacion"].Value = cod_separacion;
                    //report.BackColor = Color.FromArgb(0, 157, 150);
                    reportContado.ShowPreviewDialog();
                }

                //SplashScreen.Close();
                SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReciboSimple_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                if (cod_separacion == null) { MessageBox.Show("Debe seleccionar separación.", "Ficha del separación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                rptFichaReciboSimple report = new rptFichaReciboSimple();
                ReportPrintTool printTool = new ReportPrintTool(report);
                //detalleLotes printTool = new detalleLotes(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_proyecto"].Value = codigo;
                report.Parameters["cod_separacion"].Value = cod_separacion;
                //report.BackColor = Color.FromArgb(0, 157, 150);
                SplashScreenManager.CloseForm(false);

                //SplashScreen.Close();
                report.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        //private void MostrarTotalValor()
        //{
        //    if (grdbContFinan.SelectedIndex == 0)
        //    {
        //        LayoutValorCompra.Text = layoutPrecioFinal.Text;
        //        txtValorMostrar.Text = txtPreFinal.Text;
        //    }
        //    else
        //    {
        //        LayoutValorCompra.Text = layoutSaldoFinanciar.Text;
        //        txtValorMostrar.Text = txtSaldoFinanciar.Text;

        //    }
        //}

        //private void txtSaldoFinanciar_EditValueChanged(object sender, EventArgs e)
        //{
        //    MostrarTotalValor();
        //}

        //private void txtPreFinal_EditValueChanged(object sender, EventArgs e)
        //{
        //    MostrarTotalValor();
        //}



        private void chkAplicaIGV_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void chkFinanciado_CheckStateChanged(object sender, EventArgs e)
        {

            chkFinanciado.Properties.Appearance.Font = chkFinanciado.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
            chkContado.CheckState = chkFinanciado.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
            if (chkFinanciado.CheckState == CheckState.Checked)
            {
                //LimpiarCamposFormaPago();
                dtFecPagoContado.EditValue = null;
                BloqueoControlesInformacionVenta(true, false, true, false);
                //calcularPrecioFinal();
                //calcularSaldoFinanciar();
            }
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
                if (MiAccion == Separacion.Nuevo) { dtFecPagoContado.EditValue = DateTime.Now; }
                //calcularPreFinalDescuentos();
            }
            else
            {
                dtFecPagoContado.EditValue = null;
            }
        }

        private void chkActivoSeparacion_CheckStateChanged(object sender, EventArgs e)
        {
            //chkActivoSeparacion.Properties.Appearance.Font = chkActivoSeparacion.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
        }

        private void dtFechaSeparacion_EditValueChanged(object sender, EventArgs e)
        {
            string fecha = validarFechaSeparacion((DateTime)dtFechaSeparacion.EditValue);

            if (fecha != null)
            {
                MessageBox.Show(fecha, "Guardar Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtFechaSeparacion.EditValue = DateTime.Now;
            }
            if (dtFechaSeparacion.EditValue != null) dtFecPagoSeparacion.EditValue = dtFechaSeparacion.EditValue;
        }

        private void gvDocumentos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eLotes_Separaciones.eSeparaciones_Documentos eDocumentos = gvDocumentos.GetRow(e.FocusedRowHandle) as eLotes_Separaciones.eSeparaciones_Documentos;
                    if (eDocumentos == null) { return; }
                    if (eDocumentos.cod_documento_separacion == cod_documento_separacion)
                    {
                        coldsc_nombre_doc.OptionsColumn.AllowEdit = true;
                        //colnum_orden_doc.OptionsColumn.AllowEdit = true;

                    }
                    else
                    {
                        coldsc_nombre_doc.OptionsColumn.AllowEdit = false;
                        //colnum_orden_doc.OptionsColumn.AllowEdit = false;
                        cod_documento_separacion = "";
                    }
                    gvDocumentos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvDocumentos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDocumentos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void rbtnNueno_Click(object sender, EventArgs e)
        {
            AgregarDocSeparacion();
        }

        private void gvDocumentos_Click(object sender, EventArgs e)
        {
            //eLotes_Separaciones.eSeparaciones_Documentos oListMemoDesc = mylistDocumentos.Find(x => x.cod_documento_separacion == "0" && x.dsc_nombre_doc == "" || x.dsc_nombre_doc == null);
            //if (oListMemoDesc != null)
            //{
            //    mylistDocumentos.Remove(oListMemoDesc);
            //    gvDocumentos.RefreshData();
            //}
        }
        //private async void rbtnAdjuntar_ClickAsync(object sender, EventArgs e)

        private async void rbtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                eLotes_Separaciones.eSeparaciones_Documentos oListSepDoc = gvDocumentos.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Documentos;

                if (oListSepDoc.cod_documento_separacion == "0")
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

                        string result = unit.Proyectos.Eliminar_TipoDocumento("1", oListSepDoc.cod_documento_separacion, cod_separacion, codigo, oListSepDoc.flg_PDF);
                        if (result == null)
                        {
                            MessageBox.Show("No se pudo eliminar el tipo documento \"" + oListSepDoc.dsc_nombre_doc + "\"", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        gvDocumentos.RefreshData();
                        CargarListadoDocumentos("3");
                        //obtenerListadoTipoDocumentoXSeparacion();
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
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarLote_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones();
                frm.MiAccion = ListCliSeparacion.Nuevo;

                frm.cotizacion = true;
                frm.codigo_proyecto = codigo;
                frm.cod_empresa = "";
                frm.dsc_proyecto = "";
                frm.ShowDialog();

                if (frm.dsc_lote != "")
                {
                    lkpEtapa.EditValue = frm.cod_etapa;
                    lkpManzana.EditValue = frm.cod_manzana;
                    lkpLote.EditValue = null;
                    lkpLote.EditValue = frm.cod_lote;

                    //cod_lote = frm.cod_lote;
                    //txtLote.Text = frm.dsc_lote;
                    //txtMonto.Text = frm.imp_precio_final.ToString();
                    //txtPrcUsoExclusivo.EditValue = frm.prc_uso_exclusivo;
                    //grupoListarCotizacion.Enabled = true;
                    //txtPrcUsoExclusivo.Text = frm.prc_uso_exclusivo;
                    ////txtSeparacion.Enabled = true;
                    ////txtCuotaInicial.Enabled = true;
                    ///*
                    //txtCodCliente.Text = frm.cod_cliente;
                    //txtNroLte.Text = frm.dsc_lote;
                    //txtDscCliente.Text = frm.dsc_cliente;
                    //txtMontoInicial.Text = frm.imp_precio_final.ToString();
                    //*/

                    //listDetalle.Clear();
                    //listDetalle = unit.Proyectos.obtenerConsultasVariasLotes<eProformas.eProformas_Detalle>(19, frm.imp_precio_final);

                    //bsDetalleProformas.DataSource = null; bsDetalleProformas.DataSource = listDetalle; gvListarCotizacion.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void gvExtenciones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvExtenciones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnVerCliente_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente();
                frm.cod_cliente = txtCodigoCliente.Text;
                frm.MiAccion = Cliente.Vista;
                frm.cod_proyecto_titulo = codigo;
                frm.dsc_proyecto_titulo = dsc_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void rbtnAdjuntar_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                eLotes_Separaciones.eSeparaciones_Documentos obj = gvDocumentos.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Documentos;
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

        private async Task AdjuntarDocumentosVarios(eLotes_Separaciones.eSeparaciones_Documentos eSepDoc, string nombreDocAdicional = "")
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
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Adjuntando documento...", "Cargando...");
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
                    if(eDatos == null) { SplashScreenManager.CloseForm(false); MessageBox.Show("No se escontro o no existe la carpeta para adjuntar los documentos en el onedrive.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    var targetItemFolderId = eDatos.idCarpeta;
                    var targetItemFolderIdLote = eDatos.idCarpetaAnho;

                    eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("2", codigo, codigoMultiple, cod_separacion);
                    //eLotSep.idCarpeta_separacion;
                    if (eLotSep.idCarpeta_separacion == null || eLotSep.idCarpeta_separacion == "")
                    {
                        var driveItem = new Microsoft.Graph.DriveItem
                        {
                            //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                            Name = eLotSep.dsc_documento + " " + eLotSep.dsc_nombre.ToUpper(),
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
                        IdCarpetaCliente = eLotSep.idCarpeta_separacion;
                    }
                    if (eSepDoc.idPDF != null && eSepDoc.idPDF != "") { await Mover_Eliminar_ArchivoOneDrive(); }
                    //crea archivo en el OneDrive
                    byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                    using (Stream stream = new MemoryStream(data))
                    {
                        string res = "";
                        var DriveItem = await GraphClient.Me.Drive.Items[IdCarpetaCliente].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                        idArchivoPDF = DriveItem.Id;

                        eSepDoc.cod_separacion = cod_separacion;
                        eSepDoc.cod_empresa = cod_empresa;
                        eSepDoc.cod_proyecto = codigo;
                        eSepDoc.flg_PDF = "SI";
                        eSepDoc.idPDF = idArchivoPDF;
                        eSepDoc.dsc_nombre_doc = varNombreArchivo;
                        eSepDoc.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                        if (eLotSep.idCarpeta_separacion == null || eLotSep.idCarpeta_separacion == "")
                        {
                            eLotSep.idCarpeta_separacion = IdCarpetaCliente;
                            eLotes_Separaciones objLotSep = unit.Proyectos.MantenimientoSeparaciones<eLotes_Separaciones>(eLotSep);
                        }
                        eLotes_Separaciones.eSeparaciones_Documentos resdoc = unit.Proyectos.Mantenimiento_documento_sep<eLotes_Separaciones.eSeparaciones_Documentos>(eSepDoc);
                        if (resdoc != null)
                        {
                            mensajito = "Se registró el documento satisfactoriamente";
                            CargarListadoDocumentos("3");

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


            //try
            //{
            //    DateTime FechaRegistro = DateTime.Today;
            //    string nombreCarpeta = "";

            //    //eLotes_Separaciones resultado = unit.Proyectos.Obtener_DocumentoSeparacion<eLotes_Separaciones>(2, cod_separacion, cod_empresa);
            //    //if (resultado == null) { MessageBox.Show("Antes de adjuntar los docuentos debe crear al trabajador.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            //    OpenFileDialog myFileDialog = new OpenFileDialog();
            //    myFileDialog.Filter = "Archivos (*.*)|; *.*";
            //    myFileDialog.FilterIndex = 1;
            //    myFileDialog.InitialDirectory = "C:\\";
            //    myFileDialog.Title = "Abrir archivo";
            //    myFileDialog.CheckFileExists = false;
            //    myFileDialog.Multiselect = false;

            //    DialogResult result = myFileDialog.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        string IdCarpetaCliente = "", Extension = "";
            //        var idArchivoPDF = "";
            //        var TamañoDoc = new FileInfo(myFileDialog.FileName).Length / 1024;
            //        if (TamañoDoc < 4000)
            //        {
            //            varPathOrigen = myFileDialog.FileName;
            //            //varNombreArchivo = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd") + "."+ Path.GetExtension(myFileDialog.SafeFileName);
            //            varNombreArchivo = eSepDoc.dsc_nombre_doc + "-" + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + (nombreDocAdicional != "" ? " " + nombreDocAdicional : "") + "-" + eSepDoc.num_orden_doc +  Path.GetExtension(myFileDialog.SafeFileName);
            //            //varNombreArchivoSinExtension = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd");
            //            Extension = Path.GetExtension(myFileDialog.SafeFileName);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Solo puede subir archivos hasta 4MB de tamaño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        }
            //        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
            //        eEmpresa eEmp = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
            //        if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
            //        { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            //        ClientId = eEmp.ClientIdOnedrive;
            //        TenantId = eEmp.TenantOnedrive;
            //        Appl();
            //        var app = PublicClientApp;
            //        string correo = eEmp.UsuarioOnedrivePersonal;
            //        string password = eEmp.ClaveOnedrivePersonal;

            //        var securePassword = new SecureString();
            //        foreach (char c in password)
            //            securePassword.AppendChar(c);

            //        authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

            //        GraphClient = new Microsoft.Graph.GraphServiceClient(
            //          new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
            //          {
            //              requestMessage
            //                  .Headers
            //                  .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
            //              return Task.FromResult(0);
            //          }));

            //        //var targetItemFolderId = eEmp.idCarpetaFacturasOnedrive;
            //        eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
            //        eDatos = unit.Proyectos.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, dsc_Carpeta: "Lotes");
            //        var targetItemFolderId = eDatos.idCarpeta;
            //        var targetItemFolderIdLote = eDatos.idCarpetaAnho;

            //        eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("2", codigo, codigoMultiple, cod_separacion);
            //        //eLotSep.idCarpeta_separacion;
            //        if (eLotSep.idCarpeta_separacion == null || eLotSep.idCarpeta_separacion == "")
            //        {
            //            var driveItem = new Microsoft.Graph.DriveItem
            //            {
            //                //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
            //                Name = eLotSep.dsc_documento + "-" + eLotSep.dsc_apellido_paterno.ToUpper() + " " + eLotSep.dsc_nombre.ToUpper(),
            //                Folder = new Microsoft.Graph.Folder
            //                {
            //                },
            //                AdditionalData = new Dictionary<string, object>()
            //            {
            //            {"@microsoft.graph.conflictBehavior", "rename"}
            //            }
            //            };

            //            var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderIdLote].Children.Request().AddAsync(driveItem);
            //            IdCarpetaCliente = driveItemInfo.Id;
            //        }
            //        else //Si existe folder obtener id
            //        {
            //            IdCarpetaCliente = eLotSep.idCarpeta_separacion;
            //        }
            //        if (eSepDoc.idPDF != null && eSepDoc.idPDF != "") { await Mover_Eliminar_ArchivoOneDrive(); }
            //        //crea archivo en el OneDrive
            //        byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
            //        using (Stream stream = new MemoryStream(data))
            //        {
            //            string res = "";
            //            var DriveItem = await GraphClient.Me.Drive.Items[IdCarpetaCliente].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
            //            idArchivoPDF = DriveItem.Id;
            //            eSepDoc.cod_separacion = cod_separacion;
            //            eSepDoc.cod_empresa = cod_empresa;
            //            eSepDoc.cod_proyecto = codigo;
            //            eSepDoc.flg_PDF = "SI";
            //            eSepDoc.idPDF = idArchivoPDF;
            //            eSepDoc.dsc_nombre_doc = varNombreArchivo;
            //            eSepDoc.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            //            if (eLotSep.idCarpeta_separacion == null || eLotSep.idCarpeta_separacion == "")
            //            {
            //                eLotSep.idCarpeta_separacion = IdCarpetaCliente;
            //                eLotes_Separaciones objLotSep = unit.Proyectos.MantenimientoSeparaciones<eLotes_Separaciones>(eLotSep);
            //            }
            //            eLotes_Separaciones.eSeparaciones_Documentos resdoc = unit.Proyectos.Mantenimiento_documento_sep<eLotes_Separaciones.eSeparaciones_Documentos>(eSepDoc);
            //            if (resdoc != null)
            //            {
            //                MessageBox.Show("Se registró el documento satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                CargarListadoDocumentos("1");
            //                obtenerListadoTipoDocumentoXSeparacion();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Hubieron problemas al registrar el documento", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //        SplashScreenManager.CloseForm();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    SplashScreenManager.CloseForm();
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }


        private async Task Mover_Eliminar_ArchivoOneDrive()
        {
            try
            {
                eLotes_Separaciones.eSeparaciones_Documentos obj = new eLotes_Separaciones.eSeparaciones_Documentos();
                //    if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                //    {
                obj = gvDocumentos.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Documentos;
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
                eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>("2", codigo, codigoMultiple, cod_separacion);
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

        private async Task VerDocumentosVarios(string nombreDocAdicional = "")
        {
            try
            {

                eLotes_Separaciones.eSeparaciones_Documentos obj = new eLotes_Separaciones.eSeparaciones_Documentos();

                //    if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                //    {
                obj = gvDocumentos.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Documentos;
                if (obj == null || obj.cod_documento_separacion == "0") { return; }



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

        //private async Task rbtnVer_ClickAsync(object sender, EventArgs e)
        //{

        //}

        private async void rbtnVer_ClickAsync(object sender, EventArgs e)
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

        private void gvDocumentos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                int nRow = e.RowHandle;
                eLotes_Separaciones.eSeparaciones_Documentos objDoc = new eLotes_Separaciones.eSeparaciones_Documentos();
                eLotes_Separaciones.eSeparaciones_Documentos obj = gvDocumentos.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Documentos;
                if (obj == null || obj.cod_documento_separacion == "0") { return; }

                if (e.Column.FieldName == "dsc_nombre_doc_ref" && e.Value != null)
                {
                    coldsc_nombre_doc.OptionsColumn.AllowEdit = false;
                    //colnum_orden_doc.OptionsColumn.AllowEdit = false;
                    //cod_documento_separacion = "";

                    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_separacion = cod_separacion;
                    obj.cod_proyecto = codigo;
                    objDoc = unit.Proyectos.Mantenimiento_Documento<eLotes_Separaciones.eSeparaciones_Documentos>(obj);
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

        private void gvDocumentos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eLotes_Separaciones.eSeparaciones_Documentos obj = gvDocumentos.GetFocusedRow() as eLotes_Separaciones.eSeparaciones_Documentos;

                    cod_documento_separacion = obj.cod_documento_separacion;
                    coldsc_nombre_doc.OptionsColumn.AllowEdit = true;
                    //colnum_orden_doc.OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscarCopropietario_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones(this, null);
                frm.MiAccion = ListCliSeparacion.Nuevo;
                frm.codigo_proyecto = codigo;
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

        private void gvDocumentos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotes_Separaciones.eSeparaciones_Documentos obj = gvDocumentos.GetRow(e.RowHandle) as eLotes_Separaciones.eSeparaciones_Documentos;
                    if (obj.flg_PDF == "SI") { /*e.Appearance.BackColor = Color.LightGreen;*/ e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSiguienteCotizacion_Click(object sender, EventArgs e)
        {
            siguienteCotizacion();
        }

        private void txtAnteriorCotizacion_Click(object sender, EventArgs e)
        {
            anteriorCotizacion();
        }

        private void picSiguienteCotizacion_Click(object sender, EventArgs e)
        {
            siguienteCotizacion();
        }

        private void picAnteriorCotizacion_Click(object sender, EventArgs e)
        {
            anteriorCotizacion();
        }

        private void frmSepararLote_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmHandler != null && validateFormClose == 1)
            {
                frmHandler.frmSeparacionLotesProspectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
            }
        }








        //private void rbtnAdjuntar_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("mensaje");
        //}

        private void btnExtender_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                frmSepararLote frm = new frmSepararLote(frmHandler);
                frm.codigo = codigo;
                frm.dsc_proyecto = dsc_proyecto;
                frm.cod_separacion = cod_separacion;
                frm.codigoMultiple = codigoMultiple;
                frm.extension = true;
                frm.MiAccion = cod_status != "ESE00002" || flg_activo == "NO" ? Separacion.Vista : Separacion.Editar;
                this.Close();
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chckCopropietario_CheckStateChanged(object sender, EventArgs e)
        {
            //chkFinanciado.Properties.Appearance.Font = chkFinanciado.CheckState == CheckState.Checked ? new Font(Appearance.Font, FontStyle.Bold) : new Font(Appearance.Font, FontStyle.Regular);
            //chkContado.CheckState = chkFinanciado.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
        }

        private void btnVerCopro_Click(object sender, EventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente();
                frm.cod_cliente = txtCodigoCopropietario.Text;
                frm.MiAccion = Cliente.Vista;
                frm.cod_proyecto_titulo = codigo;
                frm.dsc_proyecto_titulo = dsc_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lkpAsesor_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpAsesor.EditValue != null && (MiAccion == Separacion.Editar || MiAccion == Separacion.Vista))
            {
                btnGuardar.Enabled = true;
            }

        }

        private void btnVerCli_Click(object sender, EventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente();
                frm.cod_cliente = txtCodigoCliente.Text;
                frm.MiAccion = Cliente.Vista;
                frm.cod_proyecto_titulo = codigo;
                frm.dsc_proyecto_titulo = dsc_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvExtenciones_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gvExtenciones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvExtenciones.GetRow(e.RowHandle) as eLotes_Separaciones;
                    if (e.Column.FieldName == "flg_tiene_extension") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_tiene_extension" && obj.flg_tiene_extension == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgPrincipal, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvExtenciones_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int offset = 5, posInical = 0;
            //e.Handled = true;
            //e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.SeaShell), e.Bounds);
            e.DefaultDraw(); e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = Mensaje; Rectangle markRectangle;
            string priorityText = " ";
            //for (int i = 0; i < 2; i++)
            //{

            //    posInical = i == 0 ? 0 : i == 1 ? 120 : i == 2 ? 400 : 680;
            markRectangle = new Rectangle(e.Bounds.X * (posInical) + offset, e.Bounds.Y + 10, markWidth, markWidth);

            priorityText = " Separación Principal"; e.Graphics.DrawImage(imgPrincipal, markRectangle);
            //if (i == 1) { priorityText = " Separación Principal"; e.Graphics.DrawImage(imgPrincipal, markRectangle); }

            e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
            e.Appearance.ForeColor = Color.Blue;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            e.Appearance.Options.UseTextOptions = true;
            e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            //}
        }

        private void pcChevron_Click(object sender, EventArgs e)
        {
            ocultarMostrarAuditoria();
        }

        private void ocultarMostrarAuditoria()
        {
            if (validar == 0)
            {
                System.Drawing.Image imgProyectoLogo = Properties.Resources.chevron_up_20px;
                pcChevron.EditValue = imgProyectoLogo;
                validar = 1;
                layoutAuditoriaOcultar.ContentVisible = false;
                layoutAuditoriaOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                pcChevron.ToolTipTitle = "Mostrar Auditoria";
                return;
            }
            if (validar == 1)
            {
                System.Drawing.Image imgProyectoImagen = Properties.Resources.chevron_down_20px;
                pcChevron.EditValue = imgProyectoImagen;
                validar = 0;
                layoutAuditoriaOcultar.ContentVisible = true;
                layoutAuditoriaOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                pcChevron.ToolTipTitle = "Ocultar Auditoria";
                return;
            }
        }


        private void BloqueoControlesInformacionSeparación(bool Enabled, bool ReadOnly, bool Editable)
        {
            picAnteriorSeparacion.Enabled = Editable;
            picSiguienteSeparacion.Enabled = Editable;
            //txtSeparacion.ReadOnly = campoImpSep == true ? campoImpSep : ReadOnly;
            dtFecVecSeparacion.ReadOnly = ReadOnly;
            dtFecPagoSeparacion.ReadOnly = ReadOnly;
            dtFechaSeparacion.ReadOnly = ReadOnly;
            lkpAsesor.ReadOnly = ReadOnly;

            txtCodSepara.ReadOnly = Enabled;
            chckValAdmin.ReadOnly = Enabled;
            chckValBanco.ReadOnly = Enabled;
            chckBoleteado.ReadOnly = Enabled;

        }

        private void BloqueoControlesInformacionCliente(bool Enabled, bool ReadOnly, bool Editable)
        {
            txtCodigoCliente.ReadOnly = ReadOnly;
            btnBuscarProspecto.Enabled = Enabled;
            btnBuscarCopropietario.Enabled = Enabled;
            //txtCodigoProspecto.ReadOnly = ReadOnly;
            //chkActivoSeparacion.ReadOnly = ReadOnly;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtNombres.ReadOnly = ReadOnly;
            //txtApePaterno.ReadOnly = ReadOnly;
            //txtApeMaterno.ReadOnly = ReadOnly;
            txtNroDocumento.ReadOnly = ReadOnly;
            lkpEstadoCivil.ReadOnly = ReadOnly;
            //txtOcupacion.ReadOnly = ReadOnly;
            //dtFecNacimiento.ReadOnly = ReadOnly;
            txtTelef.ReadOnly = ReadOnly;
            //txtFono2.ReadOnly = ReadOnly;
            txtCorreo.ReadOnly = ReadOnly;
            //lkpPais.ReadOnly = ReadOnly;
            //lkpDepartamento.ReadOnly = ReadOnly;
            //lkpProvincia.ReadOnly = ReadOnly;
            //glkpDistrito.ReadOnly = ReadOnly;
            txtDireccion.ReadOnly = ReadOnly;

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

        private void BloqueoControlesInformacionVenta(bool Enabled, bool ReadOnly, bool Editable, bool Disabled)
        {
            //grdbContFinan.ReadOnly = Disabled;
            dtFecVecCuota.ReadOnly = ReadOnly;
            dtFecPagoCuota.ReadOnly = ReadOnly;
            //lkpCuotas.ReadOnly = ReadOnly;
            //txtCuoInicial.ReadOnly = ReadOnly;

            //txtDescuentoPorc.ReadOnly = Disabled;

            dtFecPagoContado.ReadOnly = Editable;
            //dtFecVecPagoContado.ReadOnly = Editable;

            //txtFraccion.ReadOnly = Disabled;


            //lkpMoneda.ReadOnly = Enabled;
            //txtValorMostrar.ReadOnly = Enabled;
            //dtFecPagoCuota.ReadOnly = Enabled;
            txtPorcInteres.ReadOnly = Enabled;
            txtImpInteres.ReadOnly = Enabled;
            txtDescuentoSol.ReadOnly = Enabled;
            txtPreTerreno.ReadOnly = Enabled;
            txtValorCuotas.ReadOnly = Enabled;
            txtPreFinalDescuento.ReadOnly = Enabled;
            txtPrecioFinalFinanciar.ReadOnly = Enabled;

        }
        private void LimpiarSeguimiento()

        {
            //chckRegistrado.CheckState = CheckState.Unchecked;
            chckValAdmin.CheckState = CheckState.Unchecked;
            chckValBanco.CheckState = CheckState.Unchecked;
            chckBoleteado.CheckState = CheckState.Unchecked;
        }

        private void Editar()
        {
            //eCliente eCli = new eCliente();
            string accion = "2";
            if (extension) { accion = "7"; }
            eLotSep = unit.Proyectos.ObtenerSeparaciones<eLotes_Separaciones>(accion, codigo, codigoMultiple, cod_separacion);
            //eCli = unit.Clientes.ObtenerCliente<eCliente>(20, eLotSep.cod_cliente);
            //Nullable<int> vs = new Nullable<int>();
            //string ? is IsNullable(typeof(string)) ? "";
            if (eLotSep.flg_cliente == "SI") { txtCodigoCliente.Text = eLotSep.cod_cliente.ToString(); } else { txtCodigoCliente.Text = ""; }
            if (eLotSep.flg_prospecto == "SI") { cod_prospecto = eLotSep.cod_prospecto.ToString(); } else { cod_prospecto = ""; }

            cod_empresa = eLotSep.cod_empresa;


            //chkActivoSeparacion.CheckState = eLotSep.flg_activo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            if (eLotSep.cod_forma_pago.ToString() == "CO")
            {
                chkContado.CheckState = CheckState.Checked;
                if (extension) { extensionCO = true; }
                //BloqueoControlesInformacionVenta(true, false, true, false);
            }
            if (eLotSep.cod_forma_pago.ToString() == "FI")
            {
                chkFinanciado.CheckState = CheckState.Checked;
                if (extension) { extensionFI = true; }
                //BloqueoControlesInformacionVenta(true, true, false, false);
            }


            //grdbEstado.EditValue = eLotSep.cod_estado_separacion;
            //chckRegistrado.CheckState = eLotSep.flg_registrado == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chckValAdmin.CheckState = eLotSep.flg_Val_Admin == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chckValBanco.CheckState = eLotSep.flg_Val_Banco == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chckBoleteado.CheckState = eLotSep.flg_Boleteado == "SI" ? CheckState.Checked : CheckState.Unchecked;
            //lblEstado.Text = eLotSep.cod_status == "ESE00001" && eLotSep.contrato_firmado != "SI" ? "ESTADO DE SEPARACIÓN   :   EMITIDO" : eLotSep.cod_status == "ESE00001" && eLotSep.contrato_firmado == "SI" ? "ESTADO DE SEPARACIÓN   :   " : eLotSep.cod_status == "ESE00002" ? "ESTADO DE SEPARACIÓN   :   SEPARADO" : "ESTADO DE SEPARACIÓN   :   DESISTIDO";
            lblEstado.Text = eLotSep.cod_status == "ESE00001" && eLotSep.contrato_firmado != "SI" ? "ESTADO DE SEPARACIÓN   :   EMITIDO" : eLotSep.cod_status == "ESE00001" && eLotSep.contrato_firmado == "SI" ? "ESTADO DE SEPARACIÓN   :   FIRMADO" :  eLotSep.cod_status == "ESE00002" ? "ESTADO DE SEPARACIÓN   :   SEPARADO" : "ESTADO DE SEPARACIÓN   :   DESISTIDO";
            glkpTipoDocumento.EditValue = eLotSep.cod_tipo_documento;
            txtNroDocumento.Text = eLotSep.dsc_documento;
            unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContacto, "num_linea_contacto_string", "dsc_nombre_contacto", "", valorDefecto: true, codigo: eLotSep.cod_cliente);
            lkpContacto.EditValue = eLotSep.dsc_linea_contacto;
            lkpContacto.ReadOnly = eLotSep.cod_status == "ESE00002" ? false : true;
            //txtTelef.Text = eLotSep.dsc_telefono_1;
            //txtCorreo.Text = eLotSep.dsc_email;
            txtNombres.Text = eLotSep.dsc_nombre;
            //txtApePaterno.Text = eCli.dsc_apellido_paterno;
            //txtApeMaterno.Text = eCli.dsc_apellido_materno;
            //if (eCli.fch_nacimiento.Year == 1) { dtFecNacimiento.EditValue = null; } else { dtFecNacimiento.EditValue = eCli.fch_nacimiento; }
            if (eLotSep.fch_Separacion.Year == 1) { dtFechaSeparacion.EditValue = null; } else { dtFechaSeparacion.EditValue = eLotSep.fch_Separacion; }
            lkpEstadoCivil.EditValue = eLotSep.cod_estadocivil;
            //txtOcupacion.Text = eCli.dsc_profesion;
            txtDireccion.Text = eLotSep.dsc_cadena_direccion;
            //lkpPais.EditValue = eCli.cod_pais;
            //lkpDepartamento.EditValue = eCli.cod_departamento;
            //lkpProvincia.EditValue = eCli.cod_provincia;
            //glkpDistrito.EditValue = eCli.cod_distrito;
            //txtFono2.Text = eCli.dsc_telefono_2;
            lkpAsesor.EditValue = eLotSep.cod_asesor;
            cod_lote = eLotSep.cod_lote.ToString();
            lkpEtapa.EditValue = codigoMultiple;
            lkpManzana.EditValue = eLotSep.cod_manzana;
            lkpLote.EditValue = eLotSep.cod_lote;
            //grdbContFinan.EditValue = eLotSep.cod_forma_pago;
            txtPreTerreno.Text = eLotSep.imp_precio_total.ToString();
            txtDescuentoPorc.Text = eLotSep.prc_descuento.ToString();
            txtDescuentoSol.Text = eLotSep.imp_descuento.ToString();
            //txtFraccion.Text = eLotSep.f
            txtPrecioFinalFinanciar.Text = eLotSep.imp_precio_final.ToString();
            if (eLotSep.fch_vct_separacion.Year == 1) { dtFecVecSeparacion.EditValue = null; } else { dtFecVecSeparacion.EditValue = eLotSep.fch_vct_separacion; }
            if (eLotSep.fch_vct_cuota.Year == 1) { dtFecVecCuota.EditValue = null; } else { dtFecVecCuota.EditValue = eLotSep.fch_vct_cuota; }
            //if (eLotSep.fch_vct_pago_total.Year == 1) { dtFecVecPagoContado.EditValue = null; } else { dtFecVecPagoContado.EditValue = eLotSep.fch_vct_pago_total; }
            if (eLotSep.fch_pago_separacion.Year == 1) { dtFecPagoSeparacion.EditValue = null; } else { dtFecPagoSeparacion.EditValue = eLotSep.fch_pago_separacion; }
            if (eLotSep.fch_pago_cuota.Year == 1) { dtFecPagoCuota.EditValue = null; } else { dtFecPagoCuota.EditValue = eLotSep.fch_pago_cuota; }
            if (eLotSep.fch_pago_total.Year == 1) { dtFecPagoContado.EditValue = null; } else { dtFecPagoContado.EditValue = eLotSep.fch_pago_total; }

            lkpCuotas.EditValue = eLotSep.cod_cuotas;
            txtFraccion.Text = eLotSep.num_fraccion.ToString();
            //lkpCuotas.Text = eLotSep.num_cuotas.ToString();
            txtValorCuotas.Text = eLotSep.imp_valor_cuota.ToString();
            if (eLotSep.cod_copropietario != null && eLotSep.cod_copropietario != "")
            {
                btnVerCopro.Enabled = true;
                txtCodigoCopropietario.Text = eLotSep.cod_copropietario;
                lkpEstadoCivilCopropietario.EditValue = eLotSep.cod_estadocivil_CO;
                glkpTipoDocumentoCopropietario.EditValue = eLotSep.cod_tipo_documento_CO;
                txtNroDocumentoCopropietario.Text = eLotSep.dsc_documento_CO;
                txtNombresCopropietario.Text = eLotSep.dsc_nombre_CO;
                txtDireccionCopropietario.Text = eLotSep.dsc_cadena_direccion_CO;
                txtTelefCopropietario.Text = eLotSep.dsc_telefono_1_CO;
                txtCorreoCopropietario.Text = eLotSep.dsc_email_CO;
                unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContactoCo, "num_linea_contacto_string", "dsc_nombre_contacto", "", valorDefecto: true, codigo: eLotSep.cod_copropietario);
                lkpContactoCo.EditValue = eLotSep.dsc_linea_contacto_copro;
                lkpContactoCo.ReadOnly = eLotSep.cod_status == "ESE00002" ? false : true;

            }









            if (eLotSep.fch_Separacion.Year == 1) { dtFechaSeparacion.EditValue = null; } else { dtFechaSeparacion.EditValue = eLotSep.fch_Separacion; }

            if (eLotSep.fch_Reg_Val_Admin.Year == 1) { dtUsuarioRegValAdm.EditValue = null; }
            else { dtUsuarioRegValAdm.EditValue = eLotSep.fch_Reg_Val_Admin; txtUsuarioRegValAdm.Text = eLotSep.cod_usuario_Val_Admin.ToString(); }

            if (eLotSep.fch_Desistimiento.Year == 1) { dtUsuarioRegDesistimiento.EditValue = null; }
            else { dtUsuarioRegDesistimiento.EditValue = eLotSep.fch_Desistimiento; txtUsuarioRegDesistimiento.Text = eLotSep.cod_usuario_Desistimiento.ToString(); }

            if (eLotSep.fch_Reg_Val_Banco.Year == 1) { dtUsuarioRegValBanco.EditValue = null; }
            else { dtUsuarioRegValBanco.EditValue = eLotSep.fch_Reg_Val_Banco; txtUsuarioRegValBanco.Text = eLotSep.cod_usuario_Reg_Val_Banco.ToString(); }

            if (eLotSep.fch_Reg_Boleteado.Year == 1) { dtUsuarioRegBolet.EditValue = null; }
            else { dtUsuarioRegBolet.EditValue = eLotSep.fch_Reg_Boleteado; txtUsuarioRegBolet.Text = eLotSep.cod_usuario_Boleteado.ToString(); }


            if (eLotSep.fch_Anulacion.Year == 1) { dtUsuarioRegAnul.EditValue = null; }
            else { dtUsuarioRegAnul.EditValue = eLotSep.fch_Anulacion; txtUsuarioRegAnul.Text = eLotSep.cod_usuario_Anulacion.ToString(); }

            if (eLotSep.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; }
            else { dtFechaRegistro.EditValue = eLotSep.fch_registro; txtUsuarioRegistro.Text = eLotSep.cod_usuario_registro.ToString(); }

            if (eLotSep.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; }
            else { dtFechaModificacion.EditValue = eLotSep.fch_cambio; txtUsuarioCambio.Text = eLotSep.cod_usuario_cambio.ToString(); }

            barImprimir.Enabled = true;

            if (eLotSep.cod_status == "ESE00002")
            {
                btnDesistir.Enabled = true;
                btnVender.Enabled = true;
                barActEstado.Enabled = true;
                barActSegui.Enabled = true;
                btnExtender.Enabled = true;
                //estado = "ESE00002";
                btnImprimir.Enabled = true;
                btnAdjuntar.Enabled = true;
                btnVerDocumento.Enabled = true;
                if (eLotSep.cod_forma_pago.ToString() == "CO")
                {
                    //btnExtender.Enabled = false;

                    //chkContado.CheckState = CheckState.Checked;
                    BloqueoControlesInformacionVenta(true, true, false, false);

                }
                if (eLotSep.cod_forma_pago.ToString() == "FI")
                {
                    //chkFinanciado.CheckState = CheckState.Checked;
                    BloqueoControlesInformacionVenta(true, false, true, false);

                }
                //if (eLotSep.cod_forma_pago.ToString() == "FI")
                //{
                //    BloqueoControlesInformacionVenta(true, false, true, false);

                //}
                //if (eLotSep.cod_forma_pago.ToString() == "CO")
                //{

                //    BloqueoControlesInformacionVenta(true, true, false, false);

                //}

            }

            if (eLotSep.cod_status == "ESE00002" && eLotSep.flg_registrado == "SI" && eLotSep.flg_Val_Admin == "NO" && eLotSep.flg_Val_Banco == "NO" && eLotSep.flg_Boleteado == "NO")
            {
                btnAnular.Enabled = true;
            }
            else
            {
                btnAnular.Enabled = false;
            }
            btnBuscarProspecto.Enabled = false;
            btnBuscarCopropietario.Enabled = false;
            btnGuardar.Enabled = true;
            if (eLotSep.cod_status == "ESE00001")
            {
                BloqueoControlesInformacionCliente(false, true, false);
                BloqueoControlesInformacionSeparación(true, true, true);
                BloqueoControlesInformacionTerreno(true, true, false);
                BloqueoControlesInformacionVenta(true, true, true, true);
                //statusVendido();
            }

            //if (eLotSep.cod_status == "ESE00003")
            //{
            //    estado = "DESISTIDO";
            //}

            ObtenerDatos_ObservacionesSeparaciones();
            chkFinanciado.Enabled = false;
            chkContado.Enabled = false;
            CargarListadoDocumentos("3");
            //obtenerListadoTipoDocumentoXSeparacion();


            if (!extension)
            {
                if (eLotSep.flg_tiene_extension == "NO" && eLotSep.flg_es_extension == "NO")
                {
                    OcultarExtension();
                    txtCodSepara.Text = eLotSep.cod_separacion;
                    txtSeparacion.Text = eLotSep.imp_separacion.ToString();
                    //CargarListadoTipoSeparaciones("1");
                }
                else
                {
                    VerExtension();
                    CargarListadoTipoSeparaciones("4");
                    txtCodSepara.Text = eLotSep.cod_separacion;
                    txtSeparacion.Text = eLotSep.imp_separacion.ToString();
                    campoImpSep = true;
                    txtCuoInicial.ReadOnly = true;
                    txtDescuentoPorc.ReadOnly = true;
                    lkpCuotas.ReadOnly = true;
                }
            }
            else
            {

                CargarListadoTipoSeparaciones("3");
                txtCuoInicial.ReadOnly = true;
                txtDescuentoPorc.ReadOnly = true;
                barActEstado.Enabled = false;
                barActSegui.Enabled = false;
                btnAnular.Enabled = false;
                btnImprimir.Enabled = false;
                btnExtender.Enabled = false;

                lkpCuotas.ReadOnly = true;
                //txtPrecioFinalFinanciar.Text = eLotSep.imp_precio_final.ToString();
                if (eLotSep.flg_es_extension == "NO")
                {
                    cod_separacion_padre = eLotSep.cod_separacion;
                }
                else
                {
                    cod_separacion_padre = eLotSep.cod_separacion_padre;
                }
                MiAccion = Separacion.Nuevo;
                gvExtenciones.RefreshData();
                gvExtenciones.FocusedRowHandle = gvExtenciones.RowCount - 1;

            }
            txtPreFinalDescuento.Text = eLotSep.imp_precio_con_descuento.ToString();
            txtCuoInicial.Text = eLotSep.imp_cuota_inicial.ToString();
            txtPendientePago.Text = eLotSep.imp_pendiente_pago.ToString();
            imp_CUOI = eLotSep.imp_cuota_inicial;
            imp_penPago = eLotSep.imp_pendiente_pago;
            txtPrecioFinalFinanciar.Text = eLotSep.imp_precio_final.ToString();
            txtPorcInteres.EditValue = eLotSep.prc_interes;
            txtImpInteres.EditValue = eLotSep.imp_interes;

        }

        public void AgregarDocSeparacion()
        {

            try
            {
                int numOrden = mylistDocumentos.Count() + 1;
                eLotes_Separaciones.eSeparaciones_Documentos ListAddDocSep = new eLotes_Separaciones.eSeparaciones_Documentos
                {
                    cod_documento_separacion = "0",
                    num_orden_doc = numOrden,
                    flg_activo = "SI"
                };
                cod_documento_separacion = ListAddDocSep.cod_documento_separacion;
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

        private void statusVendido()
        {
            //lkpEtapa.ReadOnly = true;
            //lkpManzana.ReadOnly = true;
            //lkpLote.ReadOnly = true;
            //txtAreaM2.ReadOnly = true;
            //grdbContFinan.ReadOnly = true;
            txtPreTerreno.ReadOnly = true;
            txtDescuentoPorc.ReadOnly = true;
            txtDescuentoSol.ReadOnly = true;
            //dtFecPagoSeparacion.Enabled = false;
            //dtFecVecSeparacion.Enabled = false;
            //txtSeparacion.Enabled = false;
            txtCuoInicial.ReadOnly = true;
            dtFecVecCuota.ReadOnly = true;
            //dtFecVecCuota.ReadOnly = true;
            lkpCuotas.ReadOnly = true;
            txtValorCuotas.ReadOnly = true;
            dtFecPagoContado.ReadOnly = true;
            //dtFecVecPagoContado.ReadOnly = true;
            txtFraccion.ReadOnly = true;
            txtPrecioFinalFinanciar.ReadOnly = true;
        }

        public void transferirDatos(eCliente eLotSepCli)
        {
            txtCodigoCliente.Text = eLotSepCli.cod_cliente;
            cod_prospecto = !String.IsNullOrEmpty(cod_prospecto) ? cod_prospecto : eLotSepCli.cod_prospecto;
            glkpTipoDocumento.EditValue = eLotSepCli.cod_tipo_documento;
            txtNroDocumento.Text = eLotSepCli.dsc_documento;
            //txtTelef.Text = eLotSepCli.dsc_telefono_1;
            //txtCorreo.Text = eLotSepCli.dsc_email;
            txtNombres.Text = eLotSepCli.dsc_apellido_paterno + " " + eLotSepCli.dsc_apellido_materno + " " + eLotSepCli.dsc_nombre;
            lkpEstadoCivil.EditValue = eLotSepCli.cod_estadocivil;
            txtDireccion.Text = eLotSepCli.dsc_cadena_direccion;
            eLotSep.flg_cliente = "SI";
            unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContacto, "num_linea_contacto_string", "dsc_nombre_contacto", "0", valorDefecto: true, codigo: eLotSepCli.cod_cliente);
            lkpContacto.ReadOnly = false;
            btnGuardar.Enabled = true;
            btnVerCliente.Enabled = true;
            btnVerCli.Enabled = true;
            if (lstProDetalle != null && lstProDetalle.Count > 1)
            {
                txtAnteriorCotizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtSiguienteCotizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                if (lstProDetalle[rowCount].cod_variable == "EFI0001") { chkContado.CheckState = CheckState.Checked; }
                txtSeparacion.Text = lstProDetalle[rowCount].imp_separacion.ToString();
                txtDescuentoPorc.Text = lstProDetalle[rowCount].prc_descuento.ToString();
                txtCuoInicial.Text = lstProDetalle[rowCount].imp_cuota_inicial.ToString();
                lkpCuotas.EditValue = lstProDetalle[rowCount].cod_variable;
            }
            else if (lstProDetalle != null && lstProDetalle.Count == 1)
            {
                if (lstProDetalle[rowCount].cod_variable == "EFI0001") { chkContado.CheckState = CheckState.Checked; }
                txtSeparacion.Text = lstProDetalle[rowCount].imp_separacion.ToString();
                txtDescuentoPorc.Text = lstProDetalle[rowCount].prc_descuento.ToString();
                txtCuoInicial.Text = lstProDetalle[rowCount].imp_cuota_inicial.ToString();
                lkpCuotas.EditValue = lstProDetalle[rowCount].cod_variable;
            }
        }

        public void transferirDatosProforma(eProformas eProfCli)
        {
            txtCodigoCliente.Text = eProfCli.cod_cliente;
            glkpTipoDocumento.EditValue = eProfCli.cod_tipo_documento;
            txtNroDocumento.Text = eProfCli.dsc_documento;
            txtNombres.Text = eProfCli.dsc_cliente;
            lkpEstadoCivil.EditValue = eProfCli.cod_estado_civil;
            txtDireccion.Text = eProfCli.dsc_cadena_direccion;
            eLotSep.flg_cliente = "SI";
            unit.Clientes.CargaCombosLookUp("ContactosCliente", lkpContacto, "num_linea_contacto_string", "dsc_nombre_contacto", "0", valorDefecto: true, codigo: eProfCli.cod_cliente);
            lkpEtapa.EditValue = eProfCli.cod_etapa;
            lkpEtapa.ReadOnly = true;
            lkpManzana.EditValue = eProfCli.cod_manzana;
            lkpManzana.ReadOnly = true;
            lkpLote.EditValue = eProfCli.cod_lote;
            lkpLote.ReadOnly = true;
            lkpContacto.ReadOnly = false;
            btnGuardar.Enabled = true;
            btnVerCliente.Enabled = true;
            btnVerCli.Enabled = true;
            lstProDetalle = unit.Proyectos.ObtenerListadoProformas<eProformas.eProformas_Detalle>(5, cod_proforma: eProfCli.cod_proforma, cod_proyecto: eProfCli.cod_proyecto);
            if (lstProDetalle != null && lstProDetalle.Count > 1)
            {
                txtAnteriorCotizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtSiguienteCotizacion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                if (lstProDetalle[rowCount].cod_variable == "EFI0001") { chkContado.CheckState = CheckState.Checked; }
                txtSeparacion.Text = lstProDetalle[rowCount].imp_separacion.ToString();
                txtDescuentoPorc.Text = lstProDetalle[rowCount].prc_descuento.ToString();
                txtCuoInicial.Text = lstProDetalle[rowCount].imp_cuota_inicial.ToString();
                lkpCuotas.EditValue = lstProDetalle[rowCount].cod_variable;
            }
            else if (lstProDetalle != null && lstProDetalle.Count == 1)
            {
                if (lstProDetalle[rowCount].cod_variable == "EFI0001") { chkContado.CheckState = CheckState.Checked; }
                txtSeparacion.Text = lstProDetalle[rowCount].imp_separacion.ToString();
                txtDescuentoPorc.Text = lstProDetalle[rowCount].prc_descuento.ToString();
                txtCuoInicial.Text = lstProDetalle[rowCount].imp_cuota_inicial.ToString();
                lkpCuotas.EditValue = lstProDetalle[rowCount].cod_variable;
            }
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

    }
}