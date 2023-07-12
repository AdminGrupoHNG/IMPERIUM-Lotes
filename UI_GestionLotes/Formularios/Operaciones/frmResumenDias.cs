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

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class frmResumenDias : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        List<eLotes_Separaciones.eResumen> listResumenSeparaciones = new List<eLotes_Separaciones.eResumen>();
        frmResumenXsemana frmHandler;
        public DateTime oPrimerDiaDelMes, oUltimoDiaDelMes;
        public int num_mes = 0;
        public string dsc_mes = "", cod_proyecto = "", cod_status = "";
        public frmResumenDias()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        internal frmResumenDias(frmResumenXsemana frm)
        {
            InitializeComponent();
            unit = new UnitOfWork();
            frmHandler = frm;
        }
        private void frmResumenDias_Load(object sender, EventArgs e)
        {
            gcTitulo.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            layoutControlItem3.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            layoutControlItem4.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            CargarListadoSemanas();
            cargarTitulo();
        }
        private void frmResumenDias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        public void CargarListadoSemanas()
        {
            try
            {
                List<eLotes_Separaciones.eResumen> ListSeparacionSemana = new List<eLotes_Separaciones.eResumen>();
                ListSeparacionSemana = unit.Proyectos.ListarSeparacionSemana<eLotes_Separaciones.eResumen>(cod_proyecto, oPrimerDiaDelMes, oUltimoDiaDelMes, num_mes, cod_status);
                bsResumenSemana.DataSource = ListSeparacionSemana;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void picAnteriorMes_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.bdListaResumen.RowCount - 1;
                int nRow = frmHandler.bdListaResumen.FocusedRowHandle;
                frmHandler.bdListaResumen.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;
                num_mes = frmHandler.bdListaResumen.FocusedRowHandle + 1;
                eLotes_Separaciones.eResumen oPerfil = frmHandler.listResumenSeparaciones.Find(x => x.num_mes == num_mes);
                dsc_mes = oPerfil.dsc_mes;
                CargarListadoSemanas();                
                cargarTitulo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarTitulo()
        {
            try
            {
                gcTitulo.Text = "Separaciones de " + dsc_mes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void layoutControlItem3_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.bdListaResumen.RowCount - 1;
                int nRow = frmHandler.bdListaResumen.FocusedRowHandle;
                frmHandler.bdListaResumen.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;
                num_mes = frmHandler.bdListaResumen.FocusedRowHandle + 1;
                eLotes_Separaciones.eResumen oPerfil = frmHandler.listResumenSeparaciones.Find(x => x.num_mes == num_mes);
                dsc_mes = oPerfil.dsc_mes;
                CargarListadoSemanas();
                cargarTitulo();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void layoutControlItem4_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.bdListaResumen.RowCount - 1;
                int nRow = frmHandler.bdListaResumen.FocusedRowHandle;
                frmHandler.bdListaResumen.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;
                num_mes = frmHandler.bdListaResumen.FocusedRowHandle + 1;
                eLotes_Separaciones.eResumen oPerfil = frmHandler.listResumenSeparaciones.Find(x => x.num_mes == num_mes);
                dsc_mes = oPerfil.dsc_mes;
                CargarListadoSemanas();
                cargarTitulo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteMes_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.bdListaResumen.RowCount - 1;
                int nRow = frmHandler.bdListaResumen.FocusedRowHandle;
                frmHandler.bdListaResumen.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;
                num_mes = frmHandler.bdListaResumen.FocusedRowHandle + 1;
                eLotes_Separaciones.eResumen oPerfil = frmHandler.listResumenSeparaciones.Find(x => x.num_mes == num_mes);
                dsc_mes = oPerfil.dsc_mes;
                CargarListadoSemanas();
                cargarTitulo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}