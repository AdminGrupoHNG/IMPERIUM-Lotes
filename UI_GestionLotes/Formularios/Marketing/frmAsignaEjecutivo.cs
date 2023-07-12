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

namespace UI_GestionLotes.Formularios.Lotes
{
    public partial class frmAsignaEjecutivo : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmMantEvento frmHandler;
        public eUsuario user = new eUsuario();
        internal campanha MiAccion = campanha.Nuevo;
        List<eCampanha> Listcampanhas= new List<eCampanha>();
        
        public string cod_campanha = "", cod_empresa = "", cod_proyecto = "", cod_prospecto="";
        public eCampanha o_eCamp;

        public frmAsignaEjecutivo()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmAsignaEjecutivo(frmMantEvento frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmAsignaEjecutivo_Load(object sender, EventArgs e)
        {
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();
            lkpCampanha.Select();
        }
        private void Inicializar()
        {
            CargarCombos();
        }
        private void CargarCombos()
        {
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = cod_empresa;
            tmpCamp.cod_proyecto = cod_proyecto;

            unit.Campanha.CargarCombos_TablasMaestras("1", "campanhaempresa", lkpCampanha, "cod_campanha", "dsc_campanha", tmpCamp);
            unit.Campanha.CargarCombos_TablasMaestras("1", "ejecutivos", lkpEjecutivos, "cod_ejecutivo", "dsc_ejecutivo", tmpCamp);
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (lkpCampanha.EditValue==null) { MessageBox.Show("Seleccione una campaña", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpCampanha.Focus(); return; }
                //if (lkpEjecutivos.EditValue == null) { MessageBox.Show("Seleccione un ejecutivo", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpEjecutivos.Focus(); return; }


                eCampanha eCampResultado = new eCampanha();
                eCampanha eCamp = new eCampanha();
                eCamp.cod_campanha = lkpCampanha.EditValue.ToString();
                eCamp.cod_ejecutivo = lkpEjecutivos.EditValue.ToString();
                eCamp.cod_empresa = cod_empresa;
                eCamp.cod_proyecto = cod_proyecto;
                eCamp.cod_prospecto = cod_prospecto;
                eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

                eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(1,eCamp);

                if (eCampResultado.cod_prospecto != null)
                {
                    MessageBox.Show("Se asigno el prospecto de manera satisfactoria", "Asignando ejecutivo", MessageBoxButtons.OK);
                    this.Close();
                }

                //string result = "";
                //switch (MiAccion)
                //{
                //    case campanha.Nuevo: result = Guardar(); break;
                //    case campanha.Editar: result = Modificar(); break;
                //}

                //if (result == "OK")
                //{
                //    MessageBox.Show("Se guardó la campaña de manera satisfactoria", "Registro de campaña", MessageBoxButtons.OK);
                //    ActualizarListado = "SI";
                //    if (frmHandler != null)
                //    {
                //        int nRow = frmHandler.gvListacampanha.FocusedRowHandle;
                //        frmHandler.frmListadocampanha_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                //        frmHandler.gvListacampanha.FocusedRowHandle = nRow;
                //        this.Close();
                //    }

                //    if (MiAccion == campanha.Nuevo)
                //    {
                //        MiAccion = campanha.Editar;
                //        LimpiarCampos();
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
             

    }
}