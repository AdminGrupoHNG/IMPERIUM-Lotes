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
using UI_GestionLotes.UserControls;
using UI_GestionLotes.Tools;

namespace UI_GestionLotes
{
    public partial class frmDashBoardPrincipal : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        TaskScheduler scheduler;
        Timer oTimerLoadMtto;

        public frmDashBoardPrincipal()
        {
            InitializeComponent();
            unit = new UnitOfWork();
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
            List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
            eTipoCambio obj = unit.Factura.BuscarTipoCambio<eTipoCambio>(9, DateTime.Today);

            //Tile Fecha
            TileBarItemFecha.Elements[1].Text = DateTime.Now.ToShortDateString();
            //Tile Tipo Cambio
            TileBarItemTipoCambio.Elements[1].Text = obj == null ? "0.00" : obj.imp_cambio_venta.ToString("#.000");
            //Tile Empresas
            TileBarItemEmpresas.Elements[1].Text = list.Count().ToString();
        }

        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            //navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private void widgetView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            usrListados frmListados = new usrListados();
            if (e.Control == null)
                e.Control = new System.Windows.Forms.Control();
            if (e.Document == docListados)
                e.Control = frmListados;
            if (e.Document == docAccesos)
                e.Control = new usrAccesos();
            if (e.Document == docCalendarioOutlook)
                e.Control = new usrTiempo();
        }
    }
}