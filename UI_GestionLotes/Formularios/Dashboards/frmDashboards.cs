using BE_GestionLotes;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Dashboards
{
    public partial class frmDashboards : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        public frmDashboards()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void Dashboards_Load(object sender, EventArgs e)
        {
            List<eLotesxProyecto> ListProspectos_grilla = unit.Campanha.ListarProspectos<eLotesxProyecto>(99, "00010", "00001", "",
                    Program.Sesion.Usuario.cod_usuario, "ALAURA,ANRAMIREZ,AVALLEJO,CCHAVEZ,CRICRA,CTAMBO,FBENITEZ,FEVASQUEZ,GVARGAS,HALARCON,IMORALEZ,JARCOS,JCAMACHO,JCHUNGA,JCORDOVA,JOTERO,KSANCHEZ,LGARCIA,LOCHAVANO,LSANTOME,MBUSTAMANTE,MCERVARNTES,MRUBINA,NQUISPE,PCARLOS,RCAVERO,YAGUADO", 
                    "", "TPCON001,TPCON002,TPCON003,TPCON004,TPCON005,TPCON006,TPCON007",
                    "", "",
                     "02",
                    "20230301",
                    "20230430",
                     "CAPRO001,CAPRO002,CAPRO003,CAPRO004,CAPRO005,CAPRO006,CAPRO007,CAPRO008,CAPRO009,CAPRO010,CAPRO011,CAPRO012,CAPRO013,CAPRO014", "01");
            bsListadoProspecto.DataSource = ListProspectos_grilla;

            List<eLotesxProyecto> ListProspectos_grilla2 = unit.Campanha.ListarProspectos<eLotesxProyecto>(98, "00012", "00001", "",
                    Program.Sesion.Usuario.cod_usuario, "ALAURA,ANRAMIREZ,AVALLEJO,CCHAVEZ,CRICRA,CTAMBO,FBENITEZ,FEVASQUEZ,GVARGAS,HALARCON,IMORALEZ,JARCOS,JCAMACHO,JCHUNGA,JCORDOVA,JOTERO,KSANCHEZ,LGARCIA,LOCHAVANO,LSANTOME,MBUSTAMANTE,MCERVARNTES,MRUBINA,NQUISPE,PCARLOS,RCAVERO,YAGUADO",
                    "ESTPR003", "TPCON001,TPCON002,TPCON003,TPCON004,TPCON005,TPCON006,TPCON007",
                    "CITAS", "CITAS",
                     "02",
                    "20230301",
                    "20230430",
                     "CAPRO001,CAPRO002,CAPRO003,CAPRO004,CAPRO005,CAPRO006,CAPRO007,CAPRO008,CAPRO009,CAPRO010,CAPRO011,CAPRO012,CAPRO013,CAPRO014", "02");
            bsListadoProspecto2.DataSource = ListProspectos_grilla2;
        }
    }
}