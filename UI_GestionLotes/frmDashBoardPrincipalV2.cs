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
using UI_GestionLotes.Tools;

namespace UI_GestionLotes
{
    public partial class frmDashBoardPrincipalV2 : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public eUsuario user = new eUsuario();
        TaskScheduler scheduler;
        Timer oTimerLoadMtto;

        public frmDashBoardPrincipalV2()
        {
            InitializeComponent();
            oTimerLoadMtto = new Timer();
            oTimerLoadMtto.Interval = 500;
            oTimerLoadMtto.Tick += oTimerLoadMtto_Tick;
        }

        private void frmDashBoardComercial_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            oTimerLoadMtto.Start();
        }

        private void oTimerLoadMtto_Tick(object sender, EventArgs e)
        {
            oTimerLoadMtto.Stop();
            Inicializar();
        }

        public void Inicializar()
        {
            
        }
                
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            //navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private void widgetView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            if (e.Control == null)
                e.Control = new System.Windows.Forms.Control();
            if (e.Document == docCalendario)
                e.Control = new usrCalendario();
            if (e.Document == docHoraFecha)
                e.Control = new usrTiempo();
            if (e.Document == docLogoEmpresas)
                e.Control = new usrLogoEmpresas();
        }
        
    }
}