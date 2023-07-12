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
using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.XtraSplashScreen;
using System.IO;

namespace UI_GestionLotes.Formularios.Marketing
{
    public partial class frmPopupProyectoInfo : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        
        public string cod_proyecto = "", cod_empresa = "";
        public eProyecto proyecto = new eProyecto();
        List<eProyecto> eProImagen = new List<eProyecto>();

        private void frmPopupProyectoInfo_Load(object sender, EventArgs e)
        {
            gcCantidad.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            
            mostrarProyecto(cod_proyecto, cod_empresa);
            CargarListadoEtapas("3");
            CargarListadoStatusXEtapas("10");
        }


        public frmPopupProyectoInfo()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        public void mostrarProyecto(string cod_proyecto = "", string cod_empresa = "")
        {
            proyecto = unit.Proyectos.ObtenerListadoEmpresaSeleccionada<eProyecto>("18", cod_empresa: cod_empresa, cod_proyecto: cod_proyecto);
            //lblTitulo.Text = proyecto.dsc_nombre;            
            mmDescripcion.Text = proyecto.dsc_descripcion;
            txtArea.Text = proyecto.num_total_metros;
            txtEtapas.Text = proyecto.num_etapas;
            mmDescripcion.DeselectAll();
            //txtLotes.Text = proyecto.num_etapas_lotes;
            //txtLotesLibres.Text = proyecto.num_lotes_libres;
            eProImagen = unit.Proyectos.ObtenerProyectoImagenes<eProyecto>("1", cod_proyecto);

            if (eProImagen != null)
            {
                foreach (eProyecto obj in eProImagen)
                {
                    if (obj == null) continue;
                    if (obj.cod_imagenes == "00001") picLogoProyecto.EditValue = convertirBytes(eProImagen[0].dsc_base64_imagen);
                    if (obj.cod_imagenes == "00002") picImagenProyecto.EditValue = convertirBytes(eProImagen[1].dsc_base64_imagen);
                    //if (obj.cod_imagenes == "00003") picImagenProyecto2.EditValue = convertirBytes(eProImagen[2].dsc_base64_imagen);
                }

            }

            //if (proyecto.cod_proyecto == "00001")
            //{
            //    Image imgImagenProyecto = Properties.Resources.TerrenoCalifornia;
            //    picImagenProyecto.EditValue = imgImagenProyecto;
            //    Image imgProyectoLogo = Properties.Resources.LOGO_FINAL_05__1_;
            //    picLogoProyecto.EditValue = imgProyectoLogo;
            //}
        }

        private Bitmap convertirBytes(string base64_imagen)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64_imagen); // esto convertiria lo de la base de datos para mostrar

            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            return (Bitmap)Bitmap.FromStream(memoryStream);
        }

        public void CargarListadoEtapas(string accion)
        {
            try
            {
                List<eProyecto_Etapa> ListProyectoEtapa = new List<eProyecto_Etapa>();
                ListProyectoEtapa = unit.Proyectos.ListarEtapa<eProyecto_Etapa>(accion, "", cod_proyecto);
                bsEtapa.DataSource = ListProyectoEtapa;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarListadoSumaPrecios(string accion, string etapa = "")
        {
            try
            {
                List<eLotesxProyecto> ListSumaPrecio = new List<eLotesxProyecto>();
                ListSumaPrecio = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>(accion, cod_proyecto, etapa);
                bsPrecioSumXetapa.DataSource = ListSumaPrecio;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       
        public void CargarListadoStatusXEtapas(string accion, string etapa = "")
        {
            try
            {
                List<eLotesxProyecto> ListStatusEtapa = new List<eLotesxProyecto>();
                ListStatusEtapa = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>(accion, cod_proyecto, etapa);
                gcCantidad.Text = "CANTIDAD DE LOTES  :  " + ListStatusEtapa[0].num_total_lotes;
                bsStatusXetapa.DataSource = ListStatusEtapa;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void chartEtapasxProyecto_BackgroundImageChanged(object sender, EventArgs e)
        {
            
        }

        private void chartEtapasxProyecto_SelectedItemsChanged(object sender, DevExpress.XtraCharts.SelectedItemsChangedEventArgs e)
        {
            try
            {

                foreach (eProyecto_Etapa obj in chartEtapasxProyecto.SelectedItems)
                {
                    if (obj == null) continue;
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reportes", "Cargando...");
                    //CargarListadoStatusXEtapas("5", obj.cod_etapa);
                    CargarListadoSumaPrecios("12", obj.cod_etapa);
                    //CargarTipoDocumento(obj.nOrden);
                    //CargarTipoServicio(obj.nOrden);
                    //xtraTabControl1.SelectedTabPage = xtabVistaProv;
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbgtipmapa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(eProImagen.Count > 0)
            {
                if (rbgtipmapa.EditValue.ToString() == "GE")
                {
                    if (eProImagen[1].dsc_base64_imagen != null) picImagenProyecto.EditValue = convertirBytes(eProImagen[1].dsc_base64_imagen);

                    //Image imgImagenProyecto = Properties.Resources.TerrenoCalifornia;
                    //picImagenProyecto.EditValue = imgImagenProyecto;
                }
                else
                {
                    if (eProImagen[2].dsc_base64_imagen != null) picImagenProyecto.EditValue = convertirBytes(eProImagen[2].dsc_base64_imagen);

                    //    Image imgImagenProyecto = Properties.Resources.MapaDistribucion;
                    //    picImagenProyecto.EditValue = imgImagenProyecto;
                }
            }
           
        }

       

        private void frmPopupProyectoInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}