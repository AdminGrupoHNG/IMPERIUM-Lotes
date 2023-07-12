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
using UI_GestionLotes.Formularios.Marketing;

namespace UI_GestionLotes.Formularios.Lotes
{
    internal enum campanha
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantcampanha : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListadocampanha frmHandler;
        internal campanha MiAccion = campanha.Nuevo;
        List<eCampanha> Listcampanhas = new List<eCampanha>();

        public string cod_campanha = "", cod_empresa = "", cod_proyecto = "";
        public string ActualizarListado = "NO";

        public string GrupoSeleccionado = "";
        public string ItemSeleccionado = "";
        public eCampanha o_eCamp;

        public frmMantcampanha()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmMantcampanha(frmListadocampanha frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmMantcampanha_Load(object sender, EventArgs e)
        {
            grcInformacion.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            grcAuditoria.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;

            Inicializar();
            txtcampanha.Select();
            HabilitarBotones();
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) BloqueoControles(false, true, false);
            }
        }
        private void Inicializar()
        {
            CargarCombos();
            switch (MiAccion)
            {
                case campanha.Nuevo:
                    Nuevo();
                    break;
                case campanha.Editar:
                    Editar();
                    break;
                case campanha.Vista:
                    Editar();
                    layoutControlGroup2.Enabled = false;
                    break;
            }
        }
        private void Nuevo()
        {
            LimpiarCampos();
        }
        private void Editar()
        {
            txtCodcampanha.Text = o_eCamp.cod_campanha;
            txtcampanha.Text = o_eCamp.dsc_campanha;
            deFecha_inicio.EditValue = Convert.ToDateTime(o_eCamp.fch_inicio_campanha);
            deFecha_fin.EditValue = Convert.ToDateTime(o_eCamp.fch_fin_campanha);
            txt_codigo_referencia.Text = o_eCamp.cod_referencia_campanha;
            lkpCanal.EditValue = o_eCamp.cod_canal;
            lkpSegmento.EditValue = o_eCamp.cod_segmento;
            meComentario.Text = o_eCamp.dsc_comentario;
            meObjetivo.Text = o_eCamp.dsc_objetivo;
            glkpResponsable.EditValue = o_eCamp.cod_responsable;
            glkptipocampanha.EditValue = o_eCamp.cod_tipo_campanha;
            txtImporte.Text = o_eCamp.imp_importe.ToString();
            glkptipofrecuencia.EditValue = o_eCamp.cod_tipo_frecuencia.ToString();

            glkpproyecto.EditValue = o_eCamp.cod_empresa + "|" + o_eCamp.cod_proyecto;
            CargarCombosResponsableCampaña(glkpResponsable);

            glkpResponsable.EditValue = o_eCamp.cod_responsable;
            glkptipocampanha.EditValue = o_eCamp.cod_tipo_campanha;
            glkptipomoneda.EditValue = o_eCamp.cod_moneda;

            txtImporte.Text = o_eCamp.imp_importe.ToString();
            txtImporteFacturado.Text = o_eCamp.imp_importe_facturado.ToString();
            mmDescripcion.Text = o_eCamp.dsc_descripcion;
            txtUsuarioRegistro.Text = o_eCamp.cod_usuario_registro;
            if (o_eCamp.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = o_eCamp.fch_registro; }
            //dtFechaRegistro.Text = o_eCamp.fch_registro;
            txtUsuarioCambio.Text = o_eCamp.cod_usuario_cambio;
            if (o_eCamp.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = o_eCamp.fch_cambio; }

            //dtFechaModificacion.Text = o_eCamp.fch_cambio;
            btnDetalle.Enabled = true;
        }
        private void CargarCombos()
        {
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = "";

            unit.Campanha.CargarCombos_TablasMaestras("1", "proyectos", glkpproyecto, "cod_proyecto", "dsc_proyecto", tmpCamp);
            unit.Campanha.CargarCombos_TablasMaestras("1", "canal", lkpCanal, "cod_canal", "dsc_canal", tmpCamp);
           
            unit.Campanha.CargarCombos_TablasMaestras("1", "tipo_frecuencia", glkptipofrecuencia, "cod_tipo_frecuencia", "dsc_tipo_frecuencia", tmpCamp);
            unit.Campanha.CargarCombos_TablasMaestras("2", "TipoMoneda", glkptipomoneda, "cod_moneda", "dsc_simbolo", tmpCamp);
            // glkptipomoneda
        }

        private void CargarCombosResponsableCampaña(GridLookUpEdit combo)
        {
            DataTable tabla = new DataTable();
            string[] aCodigos = glkpproyecto.EditValue.ToString().Split("|".ToCharArray());
            if (aCodigos.Length > 1)
            {
                cod_empresa = aCodigos[0];
            }
            else
            {
                cod_empresa = "";
            }

            tabla = unit.Campanha.ObtenerListadoGridLookup("responsablecampanha", Program.Sesion.Usuario.cod_usuario, cod_empresa);
            unit.Globales.CargarCombosGridLookup(tabla, combo, "cod_responsable", "dsc_responsable", "", false);
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevo.Enabled = Enabled;
            btnGuardar.Enabled = Enabled;
        }

        private string Guardar()
        {
            string result = "";

            eCampanha eCamp = AsignarValorescampanha();
            eCamp = unit.Campanha.Guardar_Actualizar_campanha<eCampanha>(eCamp, "Nuevo");
            if (eCamp != null)
            {
                cod_campanha = eCamp.cod_campanha;
                txtCodcampanha.Text = cod_campanha;

                result = "OK";
            }
            return result;
        }
        private string Modificar()
        {
            string result = "";

            eCampanha eCamp = AsignarValorescampanha();
            eCamp = unit.Campanha.Guardar_Actualizar_campanha<eCampanha>(eCamp, "Actualizar");

            if (eCamp != null)
            {
                cod_campanha = eCamp.cod_campanha;
                result = "OK";
            }
            return result;
        }


        private eCampanha AsignarValorescampanha()
        {
            eCampanha eCamp = new eCampanha();

            eCamp.cod_campanha = txtCodcampanha.Text;
            eCamp.dsc_campanha = txtcampanha.Text;
            eCamp.fch_inicio_campanha = deFecha_inicio.EditValue.ToString();
            eCamp.fch_fin_campanha = deFecha_fin.EditValue.ToString();

            string[] aCodigos = glkpproyecto.EditValue.ToString().Split("|".ToCharArray());
            if (aCodigos.Length > 1)
            {
                eCamp.cod_empresa = aCodigos[0];
                eCamp.cod_proyecto = aCodigos[1];
            }
            else
            {
                eCamp.cod_empresa = "";
                eCamp.cod_proyecto = "";
            }
            eCamp.cod_canal = lkpCanal.EditValue.ToString();
            eCamp.cod_segmento = lkpSegmento.EditValue == null ? null : lkpSegmento.EditValue.ToString();
            eCamp.cod_referencia_campanha=txt_codigo_referencia.Text;
            eCamp.cod_responsable = glkpResponsable.EditValue.ToString();
            eCamp.cod_tipo_campanha = glkptipocampanha.EditValue.ToString();
            eCamp.cod_moneda = glkptipomoneda.EditValue.ToString();
            eCamp.dsc_comentario = meComentario.Text;
            eCamp.dsc_objetivo = meObjetivo.Text;
            eCamp.imp_importe = Convert.ToDecimal(txtImporte.Text.Replace(",",""));
            eCamp.cod_tipo_frecuencia = glkptipofrecuencia.EditValue.ToString();
            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            eCamp.dsc_descripcion = mmDescripcion.Text;
            return eCamp;
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

        private void glkpproyecto_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpproyecto.EditValue ==null) {  return;  }
            CargarCombosResponsableCampaña(glkpResponsable);
            glkpResponsable.Properties.View.ActiveFilterString = String.Empty;
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = "";
            tmpCamp.cod_proyecto = glkpproyecto.EditValue.ToString();
            unit.Campanha.CargarCombos_TablasMaestras("1", "segmento", lkpSegmento, "cod_segmento", "dsc_segmento", tmpCamp);
        }

        private void LimpiarCampos()
        {
            txtCodcampanha.Text = "";
            txtcampanha.Text = "";
            lkpCanal.EditValue = null;
            lkpSegmento.EditValue = null;
            deFecha_inicio.EditValue = null;
            deFecha_fin.EditValue = null;
            mmDescripcion.Text = "";
            txt_codigo_referencia.Text = "";
            meComentario.Text = "";
            meObjetivo.Text = "";
            glkpResponsable.EditValue = null;
            glkpResponsable.Properties.View.ActiveFilterString = String.Empty;
            glkptipocampanha.EditValue = null;
            txtImporte.Text = "0";
            txtImporteFacturado.Text = "0";
            glkptipofrecuencia.EditValue = null;

            glkptipomoneda.EditValue = "SOL";

            txtUsuarioRegistro.Text = "";
            txtUsuarioCambio.Text = "";
            dtFechaRegistro.Text = "";
            dtFechaModificacion.Text = "";

            btnDetalle.Enabled = false;
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.dsc_usuario;
            txtUsuarioCambio.Text = "";
            dtFechaRegistro.EditValue = DateTime.Now;
            dtFechaModificacion.EditValue = null;
        }



        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = campanha.Nuevo;
            LimpiarCampos();
        }

        private void btnDetalle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                frmMantDetalleCampanha frm = new frmMantDetalleCampanha();
                frm.cod_campanha = cod_campanha;
                frm.cod_empresa = cod_empresa;
                frm.cod_proyecto = cod_proyecto;
                frm.dsc_campanha = txtcampanha.Text;
                frm.ShowDialog();

                eCampanha obj = new eCampanha();
                txtImporteFacturado.Text = unit.Campanha.Listarcampanhas<eCampanha>(0, cod_proyecto, Program.Sesion.Usuario.cod_usuario, cod_campanha).First().imp_importe_facturado.ToString();
                txtImporteFacturado.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lkpCanal_EditValueChanged(object sender, EventArgs e)
        {
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = cod_empresa;
            tmpCamp.cod_proyecto = "";
            tmpCamp.valor_1 = lkpCanal.EditValue == null ? "" : lkpCanal.EditValue.ToString();
            glkptipocampanha.EditValue = null;
            unit.Campanha.CargarCombos_TablasMaestras("1", "tipo_campanha", glkptipocampanha, "cod_tipo_campanha", "dsc_tipo_campanha", tmpCamp);
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtcampanha.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre de la campaña", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); txtcampanha.Focus(); return; }
                if (deFecha_inicio.Text == "") { MessageBox.Show("Debe ingresar una fecha de inicio", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); deFecha_inicio.Focus(); return; }
                if (deFecha_fin.Text == "") { MessageBox.Show("Debe ingresar una fecha de fin", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); deFecha_fin.Focus(); return; }
                if (Convert.ToDateTime(deFecha_fin.Text) < Convert.ToDateTime(deFecha_inicio.Text)) { MessageBox.Show("La fecha de fin no puede ser menor a la de inicio", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); deFecha_fin.Focus(); return; }

                if (glkpproyecto.EditValue==null) { MessageBox.Show("Seleccione una empresa", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpproyecto.Focus(); return; }
                if (glkpResponsable.EditValue == null) { MessageBox.Show("Seleccione un responsable", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpResponsable.Focus(); return; }
                if (lkpCanal.EditValue == null) { MessageBox.Show("Seleccione un canal ", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpCanal.Focus(); return; }
                if (glkptipocampanha.EditValue == null) { MessageBox.Show("Seleccione un punto de contacto", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); glkptipocampanha.Focus(); return; }
                if (mmDescripcion.Text.Trim() == "") { MessageBox.Show("Debe ingresar la descripción de la campaña", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); mmDescripcion.Focus(); return; }
                if (glkptipofrecuencia.EditValue == null) { MessageBox.Show("Seleccione el tipo de frecuencia", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); glkptipofrecuencia.ShowPopup(); return; }
                if (glkptipomoneda.EditValue == null) { MessageBox.Show("Seleccione el tipo de moneda", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); glkptipomoneda.Focus(); return; }
                
                string result = "";
                switch (MiAccion)
                {
                    case campanha.Nuevo: result = Guardar(); break;
                    case campanha.Editar: result = Modificar(); break;
                }

                if (result == "OK")
                {
                    MessageBox.Show("Se guardó la campaña de manera satisfactoria", "Registro de campaña", MessageBoxButtons.OK);
                    ActualizarListado = "SI";
                    btnDetalle.Enabled = true;
                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvListacampanha.FocusedRowHandle;
                        frmHandler.frmListadocampanha_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        frmHandler.gvListacampanha.FocusedRowHandle = nRow;

                    }

                    if (MiAccion == campanha.Nuevo)
                    {
                        MiAccion = campanha.Editar;
                        //LimpiarCampos();
                    }
                }
                else
                {
                    MessageBox.Show("Hubo un problema al guardar la campaña", "Registro de campaña", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmMantcampanha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

             

    }
}