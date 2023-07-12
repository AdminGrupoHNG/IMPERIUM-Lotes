using BE_GestionLotes;
using BL_GestionLotes;
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
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    public partial class frmAsignarProspecto : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        List<eProspectosXLote> Listcampanha_grilla = new List<eProspectosXLote>();
        frmSepararLote frmHandler;
        frmMantCliente frmHandlerCliente;
        
        bool Buscar = false;
        public string sCodigoEmpresaTab;
        public string CodMenu, DscMenu, cod_empresa = "", cod_proyecto = "", dsc_proyecto = "", Cod_campnhaFiltro = "";
        public int validarFormulario = 1;


        public int nEstadoFiltro = 2, MenuIndice;
        //List<eCampanha> Listcampanha_grilla = new List<eCampanha>();
        public frmAsignarProspecto()
        {
            
            InitializeComponent();
            unit = new UnitOfWork();

        }
        internal frmAsignarProspecto(frmSepararLote frm, frmMantCliente frmMantCliente)
        {
            InitializeComponent();
            unit = new UnitOfWork();
            frmHandler = frm;
            frmHandlerCliente = frmMantCliente;

        }

        private void frmAsignarProspecto_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void frmAsignarProspecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void gvListaAsigProspecto_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProspectosXLote obj = gvListaAsigProspecto.GetFocusedRow() as eProspectosXLote;
                    if(frmHandlerCliente != null)
                    {
                        //frmHandlerCliente.campos_prospecto = obj;
                        frmHandlerCliente.AsignarCamposClientesProspecto(obj);
                        this.Close();
                    }
                    //if (frmHandler != null)
                    //{
                    //    frmHandler.campos_prospecto = obj;
                    //    frmHandler.AsignarCamposSeparacionProspecto();
                    //    this.Close();
                    //}
                    //if (validarFormulario == 1)
                    //{                        
                    //    frmHandler.campos_prospecto = obj;
                    //    frmHandler.AsignarCamposSeparacionProspecto();
                    //    this.Close();
                    //}
                    //else if (validarFormulario == 2)
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       

        private void gvListaAsigProspecto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void Inicializar()
        {

            CargarListado();
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            lblTitulo.Text = "PROSPECTOS: " + dsc_proyecto;
        }

        private void gvListaAsigProspecto_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);

        }

       

        public void CargarListado()
        {
            try
            {
                if (frmHandlerCliente != null)
                {
                    Listcampanha_grilla = unit.Proyectos.ListarLotesProspectos<eProspectosXLote>(2, cod_proyecto);
                }
                else 
                {                   
                    Listcampanha_grilla = unit.Proyectos.ListarLotesProspectos<eProspectosXLote>(1, cod_proyecto);
                    //coldsc_lotes_asig.Visible = true;
                    //gvListaAsigProspecto.Columns["dsc_num_documento"].VisibleIndex = 1;
                    //gvListaAsigProspecto.Columns["dsc_persona"].VisibleIndex = 2;
                    //gvListaAsigProspecto.Columns["dsc_telefono_movil"].VisibleIndex = 3;
                    //gvListaAsigProspecto.Columns["dsc_email"].VisibleIndex = 4;
                    //gvListaAsigProspecto.Columns["dsc_lotes_asig"].VisibleIndex = 5;
                    //Listcampanha_grilla = unit.Proyectos.ListarLotesProspectosAsignados<eCampanha>();
                }

                bsListaAsigProspecto.DataSource = Listcampanha_grilla;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}