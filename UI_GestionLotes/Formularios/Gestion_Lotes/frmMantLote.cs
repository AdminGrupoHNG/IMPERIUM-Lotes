using BE_GestionLotes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;

namespace UI_GestionLotes.Formularios.Gestion_Lotes
{
    internal enum Lote
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantLote : Form
    {
        private readonly UnitOfWork unit;
        frmListadoControlLotes frmHandler;
        public string cod_proyecto = "", cod_etapasmultiple = "", dsc_proyecto = "", cod_lote= "";
        List<eVariablesGenerales> lstTipoLote = new List<eVariablesGenerales>();
        internal Lote MiAccion = Lote.Nuevo;


        public frmMantLote()
        {
            InitializeComponent();
            unit = new UnitOfWork();


        }

        private void frmMantLote_Load(object sender, EventArgs e)
        {
            groupControl2.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();

        }

        internal frmMantLote(frmListadoControlLotes frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();

        }

        private void CargarCombos()
        {
            unit.Proyectos.CargaCombosLookUp("TipoLoteEtapa", lkpTipoLote, "cod_tipo_lote", "dsc_Nombre", "", cod_uno:cod_etapasmultiple);

        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Lote.Nuevo:
                   
                    break;
                case Lote.Editar:
                    CargarCombos();
                    Editar();
                    break;
                case Lote.Vista:
                    //CargarCombos();
                    //CargarListadoEtapas("1");
                    //richEditControl1.WordMLText;
                    //Editar();
                    //CargarListadoMemoriaDesc("1");
                    //Ver(false, true, false, false);
                    break;
            }
            //unit.Globales.ConfigurarGridView_ClasicStyle(gcListaMemoriaDescriptiva, gvListaMemoriaDescriptiva);
        }

        public string validarCampos()
        {
            if (lkpTipoLote.EditValue == null)
            {
                lkpTipoLote.ShowPopup();
                return "Debe seleccionar el tipo de lote";
            }

            if (Convert.ToDecimal(txtFrente.EditValue)  <= 0)
            {
                txtFrente.Focus();
                return "Debe seleccionar el frente(m)";
            }
            if (Convert.ToDecimal(txtDerecha.EditValue) <= 0)
            {
                txtDerecha.Focus();
                return "Debe seleccionar el derecha(m)";
            }
            if (Convert.ToDecimal(txtIzquierda.EditValue) <= 0)
            {
                txtIzquierda.Focus();
                return "Debe seleccionar el izquierda(m)";
            }
            if (Convert.ToDecimal(txtFondo.EditValue) <= 0)
            {

                txtFondo.Focus();
                return "Debe seleccionar el fondo(m)";
            }
            //if (!Regex.IsMatch(txtManzana.Text.Trim(), "^[a-zA-Z0-9]*$"))
            //{
            //    txtManzana.Focus();
            //    return "Debe ingresar la Manzana";
            //}

            return null;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string mensaje = validarCampos();
            if (mensaje == null)
            {
                try
                {
                    string result = "";
                    switch (MiAccion)
                    {
                        case Lote.Nuevo: result = Guardar(); break;
                        case Lote.Editar: result = Modificar(); break;
                    }

                    if (result == "OK")
                    {
                        XtraMessageBox.Show("Se guardó el proyecto de manera satisfactoria", "Guardar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (frmHandler != null)
                        {
                            int nRow = frmHandler.gvListaLotesProyectos.FocusedRowHandle;
                            frmHandler.frmListadoControlLotes_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                            frmHandler.gvListaLotesProyectos.FocusedRowHandle = nRow;
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                XtraMessageBox.Show(mensaje, "Guardar Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string Modificar()
        {

            eLotesxProyecto ePro = AsignarValoresLote();
            ePro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("6",ePro);

            if (ePro != null)
            {
                cod_lote = ePro.cod_lote;
                return "OK";
            }
            return null;
        }


        private eLotesxProyecto AsignarValoresLote()
        {
            eLotesxProyecto ePro = new eLotesxProyecto();
            ePro.cod_proyecto = cod_proyecto;
            ePro.cod_etapa = cod_etapasmultiple;
            //ePro.dsc_Nombre_Legal_Pro = txtNombreLegalPro.Text;
            ePro.cod_lote = cod_lote;
            //ePro.dsc_plan_vias = mmPlanVias.Text;
            ePro.cod_tipo_lote = lkpTipoLote.EditValue.ToString();
            ePro.num_frente = Convert.ToDecimal(txtFrente.Text);
            ePro.num_derecha = Convert.ToDecimal(txtDerecha.Text);
            ePro.num_izquierda = Convert.ToDecimal(txtIzquierda.Text);
            ePro.num_fondo = Convert.ToDecimal(txtFondo.Text);
            ePro.dsc_descripcion_html = rchLote.HtmlText;
            ePro.dsc_descripcion_word = rchLote.WordMLText;
            ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            //ePro.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();


            return ePro;
        }

        private string Guardar()
        {
            return null;
        }

        private void picAnteriorLote_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaLotesProyectos.RowCount - 1;
                int nRow = frmHandler.gvListaLotesProyectos.FocusedRowHandle;
                frmHandler.gvListaLotesProyectos.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                eLotesxProyecto obj = frmHandler.gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
               cod_lote = obj.cod_lote;
               cod_proyecto = obj.cod_proyecto;
               cod_etapasmultiple = obj.cod_etapa;
                CargarCombos();
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteLote_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaLotesProyectos.RowCount - 1;
                int nRow = frmHandler.gvListaLotesProyectos.FocusedRowHandle;
                frmHandler.gvListaLotesProyectos.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eLotesxProyecto obj = frmHandler.gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                cod_lote = obj.cod_lote;
                cod_proyecto = obj.cod_proyecto;
                cod_etapasmultiple = obj.cod_etapa;
                CargarCombos();
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMantLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void Editar()
        {
            eLotesxProyecto ePro = new eLotesxProyecto();
            ePro = unit.Proyectos.ObtenerProyectoLotes<eLotesxProyecto>("3", cod_proyecto,cod_lote);
            txtCodigoLote.Text = ePro.dsc_lote;
            txtFrente.Text = ePro.num_frente.ToString();
            txtDerecha.Text = ePro.num_derecha.ToString();
            txtIzquierda.Text = ePro.num_izquierda.ToString();
            txtFondo.Text = ePro.num_fondo.ToString();
            lkpTipoLote.EditValue = ePro.cod_tipo_lote;
            rchLote.WordMLText = ePro.dsc_descripcion_word;

        }

        public void CargarComboEnGrid()
        {
            try
            {
                    lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLoteEtapa", cod_etapasmultiple);
                    lkpTipoLote.EditValue = lstTipoLote;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                frmPopupControlLotes frm = new frmPopupControlLotes(frmHandler);
                if (Application.OpenForms["frmPopupControlLotes"] != null)
                {
                    Application.OpenForms["frmPopupControlLotes"].Activate();
                }
                else
                {
                    frm.cod_proyecto = cod_proyecto;                 
                    
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
