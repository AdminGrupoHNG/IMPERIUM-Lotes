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
using System.Collections;

namespace UI_GestionLotes.Formularios.Lotes
{
    internal enum confirmacion
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantConfirmacion : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmMantEvento frmHandler;
        internal evento MiAccion = evento.Nuevo;
        List<eCampanha> Listcampanhas = new List<eCampanha>();
        List<eCampanha> Listeventos = new List<eCampanha>();
        List<eCampanha> Listasignaciones = new List<eCampanha>();

        private static IEnumerable<eCampanha> lstDetalleRespuesta;
        public string cod_empresa = "", cod_proyecto = "", cod_prospecto = "", cod_evento = "", cod_confirmacion = "", cod_ejecutivo = "", cod_tipo_contacto = "", cod_respuesta = "", cod_tipo_contacto_2 = "", cod_estado_prospecto = "", cod_evento_confirmacion = "", cod_eventoProximo = "";
        public string ActualizarListado = "NO", cod_IndProximo = "NO", cod_IndGrupo = "", Titulo = "", cod_tipo_confirmacion = "", codTipoConfirmacionEventoProspecto = "";
        public eCampanha o_eCamp;
        DataTable odtTipoContacto = new DataTable();
        DataTable odtBotonResultado = new DataTable();
        public string sEstadoFiltro = "", sTipoContactoFiltro = "", sCod_ejecutivoFiltro = "", CodMenu, DscMenu;//, perfil = "";
        public int num_validar_evento = 0;
        public string codTipoEventoProspecto = "", codTipoProximoEventoProspecto = "";
        public string codEstadoProspectoNoAsignado = "", codEstadoProspectoAsignado = "", codEstadoProspectoEnProceso = "", codEstadoProspectoCerrado = "", codEstadoProspectoCliente = "";
        public string codResultadoEventoProspectoExitoso = "", codResultadoEventoProspectoSinRespuesta = "", codResultadoEventoProspectoContactoInvalido = "", codResultadoEventoProspectoAsistio = "", codResultadoEventoProspectoNoAsistio = "", codResultadoEventoProspectoPendiente = "", codResultadoEventoProspectoReconfirmacion = "";
        public string codTipoContactoProspectoCita = "", codTipoContactoProspectoVisita = "", codTipoContactoProspectoLlamada = "", codTipoContactoProspectoCorreo = "", codTipoContactoProspectoWhatsApp = "", codTipoContactoProspectoVideoLlamada = "";
        List<eCampanha> ListCalendario = new List<eCampanha>();


        public frmMantConfirmacion()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        internal frmMantConfirmacion(frmMantEvento frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmMantConfirmacion_Load(object sender, EventArgs e)
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

            grcEventodetalle.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            grcProximoevento.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl5.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            gcCalProximos.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;

            lblNombreprospecto.ForeColor = Program.Sesion.Colores.Verde;

            Inicializar();
            HabilitarBotones();

            //if (perfil == "VISUALIZADOR")
            //{
            //    btnNuevo.Enabled = false;
            //    btnGuardar.Enabled = false;
            //}
        }
        public void frmMantConfirmacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }
        private void frmMantConfirmacion_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmHandler != null)
            {
                int nRow = frmHandler.gvEventos.FocusedRowHandle;
                frmHandler.sEstadoFiltro = sEstadoFiltro;
                frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                frmHandler.CodMenu = CodMenu;
                frmHandler.frmMantEvento_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                frmHandler.gvEventos.FocusedRowHandle = nRow;
                frmHandler.validarCargarListado = 1;
            }
        }


        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) BloqueoControles(false, true, false);
            }
        }
        void HabilitarBotonAceptar()
        {
            if (cod_IndProximo == "SI")
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                Habilitar_Proximo_evento(true);
                Habilitar_evento_confirmaicon(true);
            }
            else
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                Habilitar_Proximo_evento(false);
                Habilitar_evento_confirmaicon(false);
            }
        }

        private void Inicializar()
        {
            CargarCombos();
            LimpiarCamposEventos();
            ListarEventos();
            ListarCalendario();
            lblNombreprospecto.Text = Titulo;
            ccCalendarioEventos.TodayDate = DateTime.Now;

            switch (MiAccion)
            {
                case evento.Nuevo:
                    Nuevo();
                    Habilitar_evento(false);
                    Habilitar_Proximo_evento(true);
                    Habilitar_evento_confirmaicon(true);
                    break;
                case evento.Editar:
                    EditarCamposConfirmacion();
                    Habilitar_evento(true);
                    Habilitar_Proximo_evento(true);
                    Habilitar_evento_confirmaicon(true);
                    //btnNuevo.Enabled = false;
                    //btnGuardar.Enabled = false;
                    break;
                case evento.Vista:
                    Habilitar_evento(false);
                    Habilitar_Proximo_evento(false);
                    Habilitar_evento_confirmaicon(false);
                    //layoutControlGroup2.Enabled = false;
                    break;
            }


        }
        private void Nuevo()
        {
            lblNombreprospecto.Text = "";
            cod_confirmacion = "";
            //lblNombreprospecto.Text = "Prospecto: " + o_eCamp.dsc_prospecto + " - Origen: " + o_eCamp.dsc_origen_prospecto;
        }
        private void EditarCamposConfirmacion()
        {
            cod_empresa = o_eCamp.cod_empresa;
            cod_proyecto = o_eCamp.cod_proyecto;
            cod_prospecto = o_eCamp.cod_prospecto;
            //cod_evento = o_eCamp.cod_evento;
            deeFecha.EditValue = o_eCamp.fch_evento;
            tieFecha.EditValue = o_eCamp.fch_evento;

            if (o_eCamp.cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); /*SeleccionaBotonTipoProceso(petpcorreo);*/ }
            else if (o_eCamp.cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null);  /*SeleccionaBotonTipoProceso(petpwtsp);*/ }
            else if (o_eCamp.cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); /*SeleccionaBotonTipoProceso(petpllamada);*/ }

            //lblNombreprospecto.Text = "Prospecto: "+o_eCamp.dsc_prospecto+" - Origen: "+ o_eCamp.dsc_origen_prospecto;

            cod_estado_prospecto = o_eCamp.cod_estado_prospecto;
        }
        private void Editar_Evento(eCampanha obj)
        {
            //LimpiarCamposEventos();

            deeFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
            tieFecha.EditValue = Convert.ToDateTime(obj.fch_fecha + " " + obj.fch_hora);
            if (obj.cod_tipo_contacto == codTipoContactoProspectoLlamada) { petpllamada_Click(null, null); }
            else if (obj.cod_tipo_contacto == codTipoContactoProspectoCorreo) { petpcorreo_Click(null, null); }
            else if (obj.cod_tipo_contacto == codTipoContactoProspectoWhatsApp) { petpwtsp_Click(null, null); }

            if (obj.cod_respuesta == codResultadoEventoProspectoReconfirmacion) { btneEfectiva_Click(null, null); }
            else if (obj.cod_respuesta == codResultadoEventoProspectoSinRespuesta) { btneNoEfectiva_Click(null, null); }

            //if (Listeventos[nFila].cod_respuesta.ToString() == codResultadoEventoProspectoExitoso) { btneEfectiva_Click(null, null); }
            //else if (Listeventos[nFila].cod_respuesta.ToString() == codResultadoEventoProspectoAsistio) { btneEfectiva_Click(null, null); }
            //else if (Listeventos[nFila].cod_respuesta.ToString() == codResultadoEventoProspectoSinRespuesta) { btneNoEfectiva_Click(null, null); }
            //else if (Listeventos[nFila].cod_respuesta.ToString() == codResultadoEventoProspectoContactoInvalido) { btnContactoInvalido_Click(null, null); }
            glkpeDetalleRespuesta.EditValue = obj.cod_detalle_respuesta;
            meeObs.Text = obj.dsc_observacion;
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

            if (obj.fch_fechaProximo != "")
            {
                depeFecha.EditValue = Convert.ToDateTime(obj.fch_fechaProximo + " " + obj.fch_horaProximo);
                tipeFecha.EditValue = Convert.ToDateTime(obj.fch_fechaProximo + " " + obj.fch_horaProximo);
                if (obj.cod_tipo_contactoProximo == codTipoContactoProspectoCita) { pepecita_Click(null, null);  /*SeleccionaBotonTipoProceso_Proximo(pepecita);*/ }
                else if (obj.cod_tipo_contactoProximo == codTipoContactoProspectoVisita) { pepevisita_Click(null, null); /* comentando por si se vuelve a utilizar SeleccionaBotonTipoProceso_Proximo(pepevisita);     */}
                else if (obj.cod_tipo_contactoProximo == codTipoContactoProspectoLlamada) { pepellamada_Click(null, null); /* comentando por si se vuelve a utilizar SeleccionaBotonTipoProceso_Proximo(pepellamada);    */}
                else if (obj.cod_tipo_contactoProximo == codTipoContactoProspectoCorreo) { pepecorreo_Click(null, null); /* comentando por si se vuelve a utilizar SeleccionaBotonTipoProceso_Proximo(pepecorreo);     */}
                else if (obj.cod_tipo_contactoProximo == codTipoContactoProspectoWhatsApp) { pepewtsp_Click(null, null); /* comentando por si se vuelve a utilizar SeleccionaBotonTipoProceso_Proximo(pepewtsp);       */}
                else if (obj.cod_tipo_contactoProximo == codTipoContactoProspectoVideoLlamada) { pepevideollamada_Click(null, null);  /* comentando por si se vuelve a utilizar eleccionaBotonTipoProceso_Proximo(pepevideollamada);*/}
                mepeObs.Text = obj.dsc_observacionProximo;
                if (obj.cod_ejecutivo_cita != "")
                {
                    glkpeEjecutivoCita.EditValue = obj.cod_ejecutivo_cita;
                }
                else
                {
                    glkpeEjecutivoCita.EditValue = null;
                }
            }

            if (obj.fch_fechaEventoConfirmacion != "")
            {
                depeConfirmacion.EditValue = Convert.ToDateTime(obj.fch_fechaEventoConfirmacion + " " + obj.fch_horaEventoConfirmacion);
                tipeConfirmacion.EditValue = Convert.ToDateTime(obj.fch_fechaEventoConfirmacion + " " + obj.fch_horaEventoConfirmacion);
                if (obj.cod_tipo_contactoEventoConfirmacion.ToString() == codTipoContactoProspectoLlamada) { pepellamadaConfir_Click(null, null); /*SeleccionaBotonTipoProceso_Proximo(pepellamadaConfir);*/ }
                else if (obj.cod_tipo_contactoEventoConfirmacion.ToString() == codTipoContactoProspectoCorreo) { pepecorreoConfir_Click(null, null); /*SeleccionaBotonTipoProceso_Proximo(pepecorreoConfir);*/ }
                else if (obj.cod_tipo_contactoEventoConfirmacion.ToString() == codTipoContactoProspectoWhatsApp) { pepewtspConfir_Click(null, null); /*SeleccionaBotonTipoProceso_Proximo(pepewtspConfir);*/ }

            }


            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem59.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void CargarCombos()
        {
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = cod_empresa;
            tmpCamp.cod_proyecto = "";

            odtTipoContacto = unit.Campanha.ObtenerListadoGridLookup("tipocontacto", Program.Sesion.Usuario.cod_usuario, "", "", "");
            unit.Campanha.CargarCombos_TablasMaestras("1", "ejecutivos", glkpeEjecutivoCita, "cod_ejecutivo", "dsc_ejecutivo", tmpCamp);

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

            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            odtBotonResultado = unit.Campanha.ObtenerListadoGridLookup("resultadocontacto", Program.Sesion.Usuario.cod_usuario, "", "", cod_IndGrupo);
            if (odtBotonResultado.Rows.Count > 0) { btneEfectiva.Text = odtBotonResultado.Rows[0][1].ToString(); layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }
            if (odtBotonResultado.Rows.Count > 1) { btneNoEfectiva.Text = odtBotonResultado.Rows[1][1].ToString(); layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }

            btneEfectiva.Appearance.BackColor = Color.Transparent;
            btneNoEfectiva.Appearance.BackColor = Color.Transparent;
            glkpeDetalleRespuesta.Properties.DataSource = null;
        }
        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevo.Enabled = Enabled;
            btnGuardar.Enabled = Enabled;

        }

        public void ListarEventos()
        {
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            Listeventos = unit.Campanha.Listar_Eventos_Confirmacion<eCampanha>(0, cod_empresa, cod_evento, cod_prospecto, Program.Sesion.Usuario.cod_usuario);
            bsEventos.DataSource = Listeventos;

            ListarCalendario();
            ccCalendarioEventos.Refresh();
            //SplashScreenManager.CloseForm();
        }
        public void ListarCalendario()
        {
            ListCalendario = unit.Campanha.Listar_Eventos_Calendario<eCampanha>(0, cod_empresa, cod_proyecto, cod_ejecutivo, Program.Sesion.Usuario.cod_usuario);
            //bsListaCalendario.DataSource = null;
            //ccCalendarioEventos.Refresh();
        }
        private void LimpiarCamposEventos()
        {
            cod_confirmacion = "";
            cod_tipo_contacto = "";
            petpllamada.BackColor = Color.FromArgb(240, 240, 240);
            petpcorreo.BackColor = Color.FromArgb(240, 240, 240);
            petpwtsp.BackColor = Color.FromArgb(240, 240, 240);

            cod_respuesta = "";
            btneEfectiva.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btneNoEfectiva.Appearance.BackColor = Color.FromArgb(240, 240, 240);

            deeFecha.EditValue = null;
            tieFecha.EditValue = null;
            deeFecha.EditValue = DateTime.Now;
            tieFecha.EditValue = DateTime.Now;

            chkReceptivo.Checked = false;

            meeObs.Text = "";
            glkpeDetalleRespuesta.EditValue = null;


            // Proximo
            depeFecha.EditValue = null;
            tipeFecha.EditValue = null;
            mepeObs.Text = "";
            cod_tipo_contacto_2 = "";
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

            depeConfirmacion.EditValue = null;
            tipeConfirmacion.EditValue = null;
            pepellamadaConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepewtspConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepecorreoConfir.BackColor = Color.FromArgb(240, 240, 240);
            lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
            if (obj == null)
            {
                EditarCamposConfirmacion();
            }
        }



        void Habilitar_evento(bool bEstado)
        {
            petpllamada.Properties.ReadOnly = !bEstado;
            petpcorreo.Properties.ReadOnly = !bEstado;
            petpwtsp.Properties.ReadOnly = !bEstado;
            chkReceptivo.ReadOnly = !bEstado;
            btneEfectiva.Enabled = bEstado;
            btneNoEfectiva.Enabled = bEstado;

            deeFecha.ReadOnly = !bEstado;
            tieFecha.ReadOnly = !bEstado;

            meeObs.ReadOnly = !bEstado;
            glkpeDetalleRespuesta.ReadOnly = !bEstado;



        }

        void Habilitar_Proximo_evento(bool bEstado)
        {

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

        }
        void Habilitar_evento_confirmaicon(bool bEstado)
        {
            pepellamadaConfir.Properties.ReadOnly = !bEstado;
            pepewtspConfir.Properties.ReadOnly = !bEstado;
            pepecorreoConfir.Properties.ReadOnly = !bEstado;

            depeConfirmacion.ReadOnly = !bEstado;
            tipeConfirmacion.ReadOnly = !bEstado;
        }

        void Iniciar_GuardarConfirmacion()
        {
            try
            {
                if (deeFecha.EditValue == null) { MessageBox.Show("Seleccione una fecha de atención", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); deeFecha.Focus(); return; }
                if (glkpeDetalleRespuesta.Properties.View.RowCount > 0)
                {
                    if (glkpeDetalleRespuesta.EditValue == null) { MessageBox.Show("Seleccione un detalle de respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpeDetalleRespuesta.Focus(); return; }
                }
                if (cod_tipo_contacto == "") { MessageBox.Show("Seleccione el tipo de contacto", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (cod_respuesta == "") { MessageBox.Show("Seleccione la respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


                string result = "";
                result = Guardar_Confirmacion();

                if (result == "OK")
                {
                    //MessageBox.Show("Se guardó la confirmación de manera satisfactoria", "Registro de eventos", MessageBoxButtons.OK);

                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvEventos.FocusedRowHandle;
                        frmHandler.sEstadoFiltro = sEstadoFiltro;
                        frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                        frmHandler.CodMenu = CodMenu;
                        frmHandler.frmMantEvento_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        if (!String.IsNullOrEmpty(cod_estado_prospecto)) { frmHandler.SeleccionaBotonEstadoProspecto(cod_estado_prospecto); }
                        frmHandler.gvEventos.FocusedRowHandle = nRow;
                        frmHandler.gvEventos.RefreshData();
                        frmHandler.btnConfirmar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        frmHandler.sEstadoFiltro = sEstadoFiltro;
                        frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                        frmHandler.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                        frmHandler.CodMenu = CodMenu;
                        frmHandler.IndicadorConfirmacionAuto = "";
                        //LimpiarCamposEventos();
                        ListarEventos();
                        //this.Close();
                    }
                    else
                    {
                        LimpiarCamposEventos();
                        ListarEventos();
                    }
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
                if (deeFecha.EditValue == null) { MessageBox.Show("Seleccione una fecha de atención", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); deeFecha.ShowPopup(); return; }
                if (glkpeDetalleRespuesta.EditValue == null) { MessageBox.Show("Seleccione un detalle de respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpeDetalleRespuesta.ShowPopup(); return; }
                if (cod_tipo_contacto == "") { MessageBox.Show("Seleccione el tipo de contacto", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (cod_respuesta == "") { MessageBox.Show("Seleccione la respuesta", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (depeFecha.EditValue == null) { MessageBox.Show("Seleccione la fecha de próxima atención", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); depeFecha.ShowPopup(); return; }
                if (cod_tipo_contacto_2 == "") { MessageBox.Show("Seleccione el tipo de contacto", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                DateTime dtFechaEvento = new DateTime();
                DateTime dtFechaProximoEvento = new DateTime();
                DateTime dtFechaProximoEventoAnterior = new DateTime();

                dtFechaEvento = Convert.ToDateTime(deeFecha.Text.ToString() + " " + tieFecha.Text.ToString());
                dtFechaProximoEvento = Convert.ToDateTime(depeFecha.Text.ToString() + " " + tipeFecha.Text.ToString());
                dtFechaProximoEventoAnterior = Convert.ToDateTime(o_eCamp.fch_fecha.ToString() + " " + o_eCamp.fch_hora.ToString());

                //if (dtFechaEvento >= dtFechaProximoEvento)
                //{
                //    MessageBox.Show("La fecha del próximo evento no puede ser menor o igual a la del evento principal", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                //}

                //if (dtFechaProximoEventoAnterior >= dtFechaProximoEvento)
                //{
                //    MessageBox.Show("La fecha de la reasignación no puede ser menor o igual a la fecha del evento anterior", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                //}

                if (lciEjecutivoCita.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always & glkpeEjecutivoCita.EditValue == null)
                {
                    MessageBox.Show("Seleccione quien recibira la cita o visita", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }

                string result = "";
                result = Guardar_Evento_Proximo();

                if (result == "OK")
                {
                    //MessageBox.Show("Se guardó el evento de manera satisfactoria", "Registro de eventos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvEventos.FocusedRowHandle;
                        frmHandler.sEstadoFiltro = sEstadoFiltro;
                        frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                        frmHandler.CodMenu = CodMenu;
                        frmHandler.frmMantEvento_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        frmHandler.gvEventos.FocusedRowHandle = nRow;
                        frmHandler.gvEventos.RefreshData();
                        frmHandler.btnConfirmar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        frmHandler.sEstadoFiltro = sEstadoFiltro;
                        frmHandler.sTipoContactoFiltro = sTipoContactoFiltro;
                        frmHandler.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                        frmHandler.CodMenu = CodMenu;
                        frmHandler.IndicadorConfirmacionAuto = "";
                        LimpiarCamposEventos();
                        ListarEventos();
                        //this.Close();
                    }
                    else
                    {
                        LimpiarCamposEventos();
                        ListarEventos();
                        Habilitar_evento(false);
                        Habilitar_Proximo_evento(false);
                        Habilitar_evento_confirmaicon(false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string Guardar_Confirmacion()
        {
            string result = "";

            eCampanha eCamp = AsignarValoresEventos();
            //num_validar_evento = 1;
            eCamp = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp, "Nuevo");
            if (eCamp != null)
            {
                cod_confirmacion = eCamp.cod_confirmacion;
                cod_estado_prospecto = eCamp.cod_estado_prospecto;
                result = "OK";
            }
            return result;
        }
        private string Guardar_Evento_Proximo()
        {
            string result = "";

            eCampanha eCamp = AsignarValoresEventos();
            eCampanha eCampProxEve = new eCampanha();

            eCamp = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp, "Nuevo");
            if (eCamp != null)
            {
                if (eCamp.cod_detalle_respuesta != "DRECO014")
                {
                    cod_confirmacion = eCamp.cod_confirmacion;
                    eCampanha eCamp_pro = AsignarValoresEventos_Proximo();
                    //eCamp_pro.cod_confirmacion = cod_confirmacion;
                    //eCamp_pro.cod_evento_principal = cod_evento;
                    eCamp_pro = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp_pro, "Nuevo");
                    //eCamp_pro = unit.Campanha.VolverAgendar_Proximo_Eventos<eCampanha>(eCamp_pro);
                    if (eCamp_pro != null)
                    {
                        eCamp = AsignarValoresEventos();
                        eCamp.cod_confirmacion = cod_confirmacion;
                        eCamp.cod_proximo_evento = eCamp_pro.cod_confirmacion;
                        eCampProxEve = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp, "2");
                        //cod_evento = eCamp_pro.cod_evento;
                        cod_eventoProximo = eCampProxEve.cod_confirmacion;
                        if (cod_tipo_confirmacion != "")
                        {
                            eCampanha eCamp_conf = AsignarValoresEventos_Confirmacion();
                            //eCamp_conf.cod_evento_ref = cod_eventoProximo;
                            //eCamp_conf.cod_evento_principal = cod_evento;
                            //eCamp_conf = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp_conf, "7");
                            eCamp_conf = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp_conf, "Nuevo");

                            if (eCamp_conf != null)
                            {
                                eCamp.cod_confirmacion_evento = eCamp_conf.cod_confirmacion;
                                eCamp = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp, "3");
                                //cod_evento_confirmacion = eCamp_conf.cod_evento;
                                //cod_evento = eCamp_conf.cod_evento;
                                result = "OK";
                            }
                        }
                        else
                        {
                            //cod_evento_confirmacion = eCamp_pro.cod_evento;
                            result = "OK";
                        }
                        //cod_confirmacion = eCamp_pro.cod_confirmacion;
                        result = "OK";
                    }
                }
                else
                {
                    cod_confirmacion = eCamp.cod_confirmacion;
                    eCampanha eCamp_pro = AsignarValoresEventos_Proximo();
                    //eCamp_pro.cod_confirmacion = cod_confirmacion;
                    eCamp_pro.cod_evento_principal = cod_evento;
                    eCamp_pro.cod_reconfirmacion = cod_evento_confirmacion;
                    //eCamp_pro = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp_pro, "Nuevo");
                    eCamp_pro = unit.Campanha.VolverAgendar_Proximo_Eventos<eCampanha>(eCamp_pro);
                    if (eCamp_pro != null)
                    {
                        //    eCamp = AsignarValoresEventos();
                        //    eCamp.cod_confirmacion = cod_confirmacion;
                        //    eCamp.cod_proximo_evento = eCamp_pro.cod_confirmacion;
                        //    eCampProxEve = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp, "2");
                        //cod_evento = eCamp_pro.cod_evento;
                        cod_eventoProximo = eCamp_pro.cod_evento;
                        if (cod_tipo_confirmacion != "")
                        {
                            eCampanha eCamp_conf = AsignarValoresEventos_Confirmacion();
                            eCamp_conf.cod_evento_ref = cod_eventoProximo;
                            eCamp_conf.cod_evento_principal = cod_evento;
                            //eCamp_conf.cod_reconfirmacion = cod_evento_confirmacion;
                            eCamp_conf = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp_conf, "7");
                            //eCamp_conf = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp_conf, "Nuevo");

                            if (eCamp_conf != null)
                            {
                                eCamp.cod_confirmacion_evento = eCamp_conf.cod_confirmacion;
                                //eCamp = unit.Campanha.Guardar_Actualizar_Eventos_Confirmacion<eCampanha>(eCamp, "3");
                                //cod_evento_confirmacion = eCamp_conf.cod_evento;
                                //cod_evento = eCamp_conf.cod_evento;
                                result = "OK";
                            }

                        }
                        else
                        {
                            //cod_evento_confirmacion = eCamp_pro.cod_evento;
                            result = "OK";
                        }



                        //cod_confirmacion = eCamp_pro.cod_confirmacion;
                        result = "OK";
                    }
                }


            }

            return result;
        }


        private eCampanha AsignarValoresEventos_Confirmacion()
        {
            eCampanha eCamp = new eCampanha();
            eCamp.cod_confirmacion = "";
            eCamp.cod_evento = cod_evento;
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

        private void gvEventos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;

                    if (obj == null) { return; }
                    LimpiarCamposEventos();
                    Editar_Evento(obj);
                    if (obj.flg_tiene_proximo_evento == "SI" && obj.flg_tiene_confirmacion_evento == "NO")
                    {
                        Habilitar_evento(false);
                        Habilitar_Proximo_evento(false);
                        Habilitar_evento_confirmaicon(true);
                    }
                    else if (obj.flg_tiene_proximo_evento == "SI" && obj.flg_tiene_confirmacion_evento == "SI")
                    {
                        Habilitar_evento(false);
                        Habilitar_Proximo_evento(false);
                        Habilitar_evento_confirmaicon(false);
                    }
                    else
                    {
                        Habilitar_evento(true);
                        Habilitar_Proximo_evento(true);
                        Habilitar_evento_confirmaicon(true);
                    }
                    cod_confirmacion = obj.cod_confirmacion;



                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void gvEventos_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Clicks == 1) gvEventos_FocusedRowChanged(gvEventos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gvEventos.FocusedRowHandle - 1, gvEventos.FocusedRowHandle));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void HabilitarCamposConfirmacion(bool read)
        {
            pepellamadaConfir.ReadOnly = read;
            pepewtspConfir.ReadOnly = read;
            pepecorreoConfir.ReadOnly = read;
            depeConfirmacion.ReadOnly = read;
            tipeConfirmacion.ReadOnly = read;
        }

        private eCampanha AsignarValoresEventos()
        {
            eCampanha eCamp = new eCampanha();
            eCamp.cod_confirmacion = String.IsNullOrEmpty(cod_confirmacion) ? "" :cod_confirmacion;
            eCamp.cod_evento = cod_evento;
            eCamp.cod_empresa = cod_empresa;
            eCamp.cod_proyecto = cod_proyecto;
            eCamp.cod_prospecto = cod_prospecto;
            eCamp.cod_tipo_evento = codTipoEventoProspecto;
            eCamp.cod_reconfirmacion = cod_evento_confirmacion;
            string fechaHora = deeFecha.DateTime.ToString("dd-MM-yyyy");
            DateTime fecha = Convert.ToDateTime(fechaHora);
            DateTime hora = tieFecha.Time;
            DateTime fechaConvertida = fecha.AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second);

            eCamp.fch_evento = fechaConvertida;
            //eCamp.fch_evento = deeFecha.Text.ToString()+" "+ tieFecha.Text.ToString();
            eCamp.cod_tipo_contacto = cod_tipo_contacto;
            eCamp.cod_respuesta = cod_respuesta;
            eCamp.cod_ejecutivo_cita = "";
            if (glkpeDetalleRespuesta.Properties.View.RowCount > 0)
            {
                eCamp.cod_detalle_respuesta = glkpeDetalleRespuesta.EditValue.ToString();
            }
            else
            {
                eCamp.cod_detalle_respuesta = "";
            }

            eCamp.dsc_observacion = meeObs.Text;

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
            eCamp.cod_motivo = "";
            eCamp.cod_expectativa = "";

            return eCamp;
        }
        private eCampanha AsignarValoresEventos_Proximo()
        {
            eCampanha eCamp = new eCampanha();

            eCamp.cod_confirmacion = "";
            eCamp.cod_evento = cod_evento;
            eCamp.cod_empresa = cod_empresa;
            eCamp.cod_proyecto = cod_proyecto;
            eCamp.cod_prospecto = cod_prospecto;
            eCamp.cod_tipo_evento = codTipoProximoEventoProspecto;

            string fechaHora = depeFecha.DateTime.ToString("dd-MM-yyyy");

            DateTime fecha = Convert.ToDateTime(fechaHora);
            DateTime hora = tipeFecha.Time;
            DateTime fechaConvertida = fecha.AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second);

            eCamp.fch_evento = fechaConvertida;

            //eCamp.fch_evento = depeFecha.Text.ToString() + " " + tipeFecha.Text.ToString();

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
            eCamp.cod_evento_ref = "";
            eCamp.cod_motivo = "";

            return eCamp;
        }


        private PictureEdit SeleccionaBotonTipoProceso(PictureEdit oPictureEdit)
        {
            petpllamada.BackColor = Color.FromArgb(240, 240, 240);
            petpcorreo.BackColor = Color.FromArgb(240, 240, 240);
            petpwtsp.BackColor = Color.FromArgb(240, 240, 240);

            oPictureEdit.BackColor = Color.FromArgb(89, 139, 125);

            return oPictureEdit;
        }
        private SimpleButton SeleccionaBotonRespuesta(SimpleButton oSimpleButton)
        {
            btneEfectiva.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            btneNoEfectiva.Appearance.BackColor = Color.FromArgb(240, 240, 240);

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




        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = evento.Nuevo;
            LimpiarCamposEventos();
            Habilitar_evento(true);
            Habilitar_Proximo_evento(false);
            Habilitar_evento_confirmaicon(false);
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        private void btnGrabarEvento_Click(object sender, EventArgs e)
        {
            Iniciar_GuardarConfirmacion();
        }
        private void btnGrabarProximoEvento_Click(object sender, EventArgs e)
        {
            Iniciar_GuardarEvento_Proximo();
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
        private void gvListaEmpresas_Click(object sender, EventArgs e)
        {

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
                //string sReconfirmacion = gvEventos.GetRowCellValue(e.RowHandle, "flg_confirmacion").ToString();

                if (sEstado == "NO")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }
        private void gvEventos_DoubleClick(object sender, EventArgs e)
        {
            if (gvEventos.RowCount > 0)
            {
                if (gvEventos.FocusedRowHandle >= 0)
                {
                    LimpiarCamposEventos();
                    eCampanha obj = gvEventos.GetFocusedRow() as eCampanha;
                    if (obj == null) { return; }
                    Editar_Evento(obj);
                }
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




        private void petpllamada_Click(object sender, EventArgs e)
        {
            if (petpllamada.ReadOnly == false)
            {
                SeleccionaBotonTipoProceso(petpllamada);
                cod_tipo_contacto = codTipoContactoProspectoLlamada;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        private void petpcorreo_Click(object sender, EventArgs e)
        {
            if (petpcorreo.ReadOnly == false)
            {
                SeleccionaBotonTipoProceso(petpcorreo);
                cod_tipo_contacto = codTipoContactoProspectoCorreo;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        private void petpwtsp_Click(object sender, EventArgs e)
        {
            if (petpwtsp.ReadOnly == false)
            {
                SeleccionaBotonTipoProceso(petpwtsp);
                cod_tipo_contacto = codTipoContactoProspectoWhatsApp;
                OpcionesBotonResultado(cod_tipo_contacto);
                HabilitarBotonAceptar();
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }



        private void btneEfectiva_Click(object sender, EventArgs e)
        {
            SeleccionaBotonRespuesta(btneEfectiva);
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = "";
            cod_respuesta = codResultadoEventoProspectoReconfirmacion;
            tmpCamp.valor_1 = cod_respuesta;
            tmpCamp.valor_4 = cod_tipo_contacto;

            unit.Campanha.CargarCombos_TablasMaestras("1", "detalleresultadocontacto", glkpeDetalleRespuesta, "cod_detalle_respuesta", "dsc_detalle_respuesta", tmpCamp);
            lstDetalleRespuesta = unit.Campanha.CombosEnGridControl<eCampanha>("detalleresultadocontacto", tmpCamp.valor_1, tmpCamp.valor_4);
        }
        private void btneNoEfectiva_Click(object sender, EventArgs e)
        {
            SeleccionaBotonRespuesta(btneNoEfectiva);
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = "";

            if (btneNoEfectiva.Text == "NO ASISTIO")
            {
                cod_respuesta = codResultadoEventoProspectoNoAsistio;
            }
            else
            {
                cod_respuesta = codResultadoEventoProspectoSinRespuesta;
            }

            tmpCamp.valor_1 = cod_respuesta;
            tmpCamp.valor_4 = cod_tipo_contacto;

            unit.Campanha.CargarCombos_TablasMaestras("1", "detalleresultadocontacto", glkpeDetalleRespuesta, "cod_detalle_respuesta", "dsc_detalle_respuesta", tmpCamp);
        }



        private void pepellamada_Click(object sender, EventArgs e)
        {
            if (pepellamada.ReadOnly == false)
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
            if (pepecorreo.ReadOnly == false)
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
            if (pepewtsp.ReadOnly == false)
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
            if (pepevideollamada.ReadOnly == false)
            {
                SeleccionaBotonTipoProceso_Proximo(pepevideollamada);
                cod_tipo_contacto_2 = codTipoContactoProspectoVideoLlamada;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                glkpeEjecutivoCita.EditValue = o_eCamp.cod_ejecutivo;
                cod_tipo_confirmacion = "";
            }
        }
        private void pepecita_Click(object sender, EventArgs e)
        {
            if (pepecita.ReadOnly == false)
            {
                SeleccionaBotonTipoProceso_Proximo(pepecita);
                cod_tipo_contacto_2 = codTipoContactoProspectoCita;
                if (chkReceptivo.CheckState == CheckState.Unchecked)
                {
                    lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                glkpeEjecutivoCita.EditValue = o_eCamp.cod_ejecutivo;
                HabilitarCamposConfirmacion(!true);
            }
        }
        private void pepevisita_Click(object sender, EventArgs e)
        {
            if (pepevisita.ReadOnly == false)
            {
                SeleccionaBotonTipoProceso_Proximo(pepevisita);
                cod_tipo_contacto_2 = codTipoContactoProspectoVisita;
                lciReceptivo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciEjecutivoCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lytConfirmar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                glkpeEjecutivoCita.EditValue = o_eCamp.cod_ejecutivo;
                HabilitarCamposConfirmacion(!true);
            }
        }


        private void glkpeDetalleRespuesta_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpeDetalleRespuesta.Properties.DataSource != null)
            {
                //int count = glkpeDetalleRespuesta.Properties.View.RowCount;
                //if (count > 0)
                if (glkpeDetalleRespuesta.EditValue != null)

                {
                    if (glkpeDetalleRespuesta.EditValue.ToString() == "DRECO014")
                    {
                        lytProxEventoCitaOficina.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lytProxEventoCitaVideoLlamada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lytProxEventoCitaTerreno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lytProxEventoLlamada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        lytProxEventoWsp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        lytProxEventoCorreo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }
                    else
                    {
                        lytProxEventoCitaOficina.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        lytProxEventoCitaVideoLlamada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        lytProxEventoCitaTerreno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        lytProxEventoLlamada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lytProxEventoWsp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lytProxEventoCorreo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }

                    GridLookUpEdit lookUp = sender as GridLookUpEdit;
                    // Access the currently selected data row
                    DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                    if (dataRow != null)
                    {
                        cod_IndProximo = dataRow["estado"].ToString();
                    }
                    //    //DevExpress.XtraGrid.Views.Grid.GridView view = glkpeDetalleRespuesta.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                    //    //object val = view.GetRowCellValue(view.FocusedRowHandle, "estado");
                    //    //cod_IndProximo = val.ToString();
                    //    var valor = "";
                    //valor = glkpeDetalleRespuesta.EditValue == null ? null : glkpeDetalleRespuesta.EditValue.ToString() == "" ? null : glkpeDetalleRespuesta.EditValue.ToString();
                    //if (valor != null)
                    //{
                    //    var val = lstDetalleRespuesta.Where(c => c.cod_detalle_respuesta == valor).First().estado.ToString();
                    //    cod_IndProximo = val.ToString();
                    //}
                }
                else
                {
                    cod_IndProximo = "NO";
                }
                HabilitarBotonAceptar();

            }
        }

        private PictureEdit SeleccionaBotonConfirmacion_Proximo(PictureEdit oPictureEdit)
        {
            pepellamadaConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepewtspConfir.BackColor = Color.FromArgb(240, 240, 240);
            pepecorreoConfir.BackColor = Color.FromArgb(240, 240, 240);

            oPictureEdit.BackColor = Color.FromArgb(89, 139, 125);


            return oPictureEdit;
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
            List<eCampanha> ListTemp = ListCalendario.Where(x => Convert.ToDateTime(x.fch_fecha) == Convert.ToDateTime(ccCalendarioEventos.EditValue)).ToList();
            bsListaCalendario.DataSource = ListTemp;
        }

    }
}