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
    public partial class frmCalendarioProspecto : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public List<eCampanha> ListCalendario = new List<eCampanha>();
        public DateTime fecha;
        public frmCalendarioProspecto()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmCalendarioProspecto_Load(object sender, EventArgs e)
        {
            lbTitulo.Text = "Eventos del día " + (fecha.Date.Day.ToString()) + " de " + fecha.ToString("MMMM");            
            bsListaCalendario.DataSource = ListCalendario;
        }
    }
}