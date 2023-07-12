using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class rptFichaReciboSimple : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly UnitOfWork unit;
        SqlConnection Conexion_Reporte = new SqlConnection();
        public rptFichaReciboSimple()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void sqlDataSourceReciboSimple_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {

        }
    }
}
