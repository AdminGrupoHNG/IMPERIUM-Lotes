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

namespace UI_GestionLotes.Formularios.Marketing
{
    public partial class frmPopupCampanhaInfo : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public string cod_prospecto = "", cod_empresa = "", cod_proyecto = "";
        public frmPopupCampanhaInfo()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmPopupCampanhaInfo_Load(object sender, EventArgs e)
        {
            try{
                lblTitulo.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
                List<eCampanha> lstObj = new List<eCampanha>();
                lstObj = unit.Campanha.Listarcampanhas<eCampanha>(1, cod_prospecto: cod_prospecto);
                if (lstObj.Count <= 0)
                {
                    MessageBox.Show("No se encontró la campaña seleccionada", "Información de Campañas", MessageBoxButtons.OK);
                    this.Close();
                }
                else
                {
                    lblTitulo.Text = lstObj.First().dsc_campanha;
                    mmDescripcion.Text = lstObj.First().dsc_descripcion;
                    mmObjetivo.Text = lstObj.First().dsc_objetivo;
                    mmComentario.Text = lstObj.First().dsc_comentario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}