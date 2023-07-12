using BE_GestionLotes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Marketing
{
    internal enum Segmentos
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantSegmento : Tools.ModalForm
    {

        private readonly UnitOfWork unit;
        internal Segmentos MiAccion = Segmentos.Nuevo;
        public string cod_proyectos_todos = "", cod_proyecto = "";
        int selec_proyecto = 0, num_proyectos = 0;
        List<eProyecto> ListProyecto = new List<eProyecto>();

        public frmMantSegmento()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            TitleBackColor = Program.Sesion.Colores.Verde;
        }

        private void cargarListado()
        {
            List<eCampanha> Listcampanha = new List<eCampanha>();
            Listcampanha = unit.Campanha.ListarSegmentos<eCampanha>(25, ListProyecto[selec_proyecto].cod_proyecto);
            bsCampanha.DataSource = Listcampanha;
        }


        public string validarCampos()
        {
            if (mmDescripcion.Text.Trim() == "")
            {
                mmDescripcion.Focus();
                return "Debe ingresar la descripción";
            }

            return null;
        }

        private string Guardar()
        {
            eVariablesGenerales eProEta = AsignarValoresSegmentos();
            eProEta = unit.Campanha.Mantenimiento_Segmentos<eVariablesGenerales>(eProEta);
            if (eProEta != null)
            {
                txtCodigoSeg.Text = eProEta.cod_variable;
                cargarListado();
                return "OK";
            }
            return null;
        }

        private string Modificar()
        {
            eVariablesGenerales eProEta = AsignarValoresSegmentos();
            eProEta = unit.Campanha.Mantenimiento_Segmentos<eVariablesGenerales>(eProEta);
            if (eProEta != null)
            {
                return "OK";
            }
            return null;
        }

        private eVariablesGenerales AsignarValoresSegmentos()
        {

            eVariablesGenerales ePro = new eVariablesGenerales();
            ePro.cod_variable = txtCodigoSeg.Text;
            ePro.dsc_Nombre = mmDescripcion.Text;
            ePro.valor_3 = ListProyecto[selec_proyecto].cod_proyecto;
            ePro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            //ePro.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            return ePro;
        }

        private void cargarProyecto()
        {
            ListProyecto = unit.Campanha.ListarSegmentos<eProyecto>(26, cod_proyectos_todos);
            num_proyectos = ListProyecto.Count;
            if (num_proyectos <= 1) { picAnterior.Enabled = false; picSiguiente.Enabled = false; }
        }
        private void cargarImagenes()
        {
            if (num_proyectos > 0)
            {
                if (ListProyecto[selec_proyecto].dsc_base64_imagen != null)
                {
                    picImagenProyecto.EditValue = convertirBytes(ListProyecto[selec_proyecto].dsc_base64_imagen);
                }
                else
                {
                    picImagenProyecto.EditValue = null;
                }

            }

        }
        private Bitmap convertirBytes(string base64_imagen)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64_imagen); // esto convertiria lo de la base de datos para mostrar

            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            return (Bitmap)Bitmap.FromStream(memoryStream);
        }
        private void frmMantSegmento_Load(object sender, EventArgs e)
        {
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl2.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl3.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            cargarProyecto();
            cargarImagenes();
            cargarListado();

        }

        private void frmMantSegmento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void gvSegmentos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvSegmentos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvSegmentos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eCampanha obj = gvSegmentos.GetFocusedRow() as eCampanha;
                    MiAccion = Segmentos.Editar;
                    txtCodigoSeg.Text = obj.cod_segmento;
                    mmDescripcion.Text = obj.dsc_segmento;
                    txtUsuarioRegistro.Text = obj.cod_usuario_registro;
                    if (obj.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = obj.fch_registro; }
                    txtUsuarioCambio.Text = obj.cod_usuario_cambio;
                    if (obj.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = obj.fch_cambio; }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picSiguiente_Click(object sender, EventArgs e)
        {
            siguienteProyecto();
        }

        private void picAnterior_Click(object sender, EventArgs e)
        {
            anteriorProyecto();
        }

        private void lytAnterior_Click(object sender, EventArgs e)
        {
            anteriorProyecto();
        }

        private void lytSiguiente_Click(object sender, EventArgs e)
        {
            siguienteProyecto();
        }

        private void siguienteProyecto()
        {

            limpiarCamposNuevo();
            if (selec_proyecto == num_proyectos - 1) { selec_proyecto = 0; } else { selec_proyecto++; }
            cargarListado();
            cargarImagenes();
        }

 
        
        private void gvSegmentos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                int nRow = e.RowHandle;
                eVariablesGenerales eVarGen = new eVariablesGenerales();
                eCampanha obj = gvSegmentos.GetRow(e.RowHandle) as eCampanha;
                if (e.Column.FieldName == "Seleccionado")
                {
                    eCampanha eVaGe = gvSegmentos.GetFocusedRow() as eCampanha;

                    string validateResult = CargarConfigSegmentos("ANULAR", obj.cod_segmento);
                    if (validateResult != null)
                    {
                        MessageBox.Show(validateResult, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        gvSegmentos.RefreshData();
                        return;
                    }

                    //obj.cod_etapa = cod_etapa;
                    //obj.cod_empresa = glkpEmpresa.EditValue.ToString();
                    obj.cod_proyecto = ListProyecto[selec_proyecto].cod_proyecto;
                    obj.flg_activo = obj.Seleccionado ? "SI" : "NO";
                    //obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eVarGen = unit.Campanha.Anular_Eliminar_Segmentos<eVariablesGenerales>(2,obj);

                    if (eVarGen == null)
                    { MessageBox.Show("Error al anular el segmento", "Anular Segmento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cargarListado(); return; }
                    cargarListado();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string CargarConfigSegmentos(string accion, string cod_segmento)
        {
            try
            {
                List<eVariablesGenerales> ListConfLote = new List<eVariablesGenerales>();
                ListConfLote = unit.Campanha.ListarRegistrosConSegmentos<eVariablesGenerales>(cod_segmento);
                if (ListConfLote.Count > 0)
                {
                    return $"ERROR AL {accion} EL SEGMENTO, \n SE ENCUENTRA CONFIGURADO EN LA BASE DE DATOS";
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }

        //private void rbtnEliminar_Click(object sender, EventArgs e)
        //{
           
        //}

        private void rchkSeleccionar_CheckStateChanged(object sender, EventArgs e)
        {
            gvSegmentos.PostEditor();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            limpiarCamposNuevo();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = validarCampos();
            if (mensaje == null)
            {
                try
                {
                    string result = "";
                    switch (MiAccion)
                    {
                        case Segmentos.Nuevo: result = Guardar(); break;
                        case Segmentos.Editar: result = Modificar(); break;
                    }

                    if (result == "OK")
                    {
                        XtraMessageBox.Show("Se guardó el segmento de manera satisfactoria", "Guardar Segmento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargarListado();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                XtraMessageBox.Show(mensaje, "Guardar Segmento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rbtnEliminar_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                eVariablesGenerales eVarGen = new eVariablesGenerales();
                eCampanha eVaGe = gvSegmentos.GetFocusedRow() as eCampanha;

                string validateResult = CargarConfigSegmentos("ELIMINAR", eVaGe.cod_segmento);
                if (validateResult != null)
                {
                    MessageBox.Show(validateResult, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este segmento? \nEsta acción es irreversible.", "Eliminar segmento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eVaGe.cod_proyecto = ListProyecto[selec_proyecto].cod_proyecto;
                    eVaGe.flg_activo = eVaGe.Seleccionado ? "SI" : "NO";
                    //obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    eVaGe.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eVarGen = unit.Campanha.Anular_Eliminar_Segmentos<eVariablesGenerales>(1, eVaGe);

                    if (eVarGen == null)
                    {
                        MessageBox.Show("Error al anular el segmento", "Anular Segmento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cargarListado(); return;
                    }
                    limpiarCamposNuevo();
                    cargarListado();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void anteriorProyecto()
        {
            if (num_proyectos > 1)
            {
                limpiarCamposNuevo();
                if (selec_proyecto == 0) { selec_proyecto = num_proyectos - 1; } else { selec_proyecto--; }
                cargarListado();
                cargarImagenes();
            }
        }

        private void limpiarCamposNuevo()
        {
            if (num_proyectos > 1)
            {
                txtCodigoSeg.Text = "";
                mmDescripcion.Text = "";
                txtUsuarioRegistro.Text = Program.Sesion.Usuario.dsc_usuario;
                dtFechaRegistro.EditValue = DateTime.Now;
                txtUsuarioCambio.Text = "";
                dtFechaModificacion.EditValue = null;
                MiAccion = Segmentos.Nuevo;
            }
        }
    }
}