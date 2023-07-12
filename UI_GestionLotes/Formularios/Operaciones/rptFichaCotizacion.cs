using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevExpress.DataAccess.ConnectionParameters;
using System.Configuration;
using BE_GestionLotes;
using BL_GestionLotes;
using System.Data.SqlClient;

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class rptFichaCotizacion : DevExpress.XtraReports.UI.XtraReport
    {
        //private readonly UnitOfWork unit;
        public eProformas obj = new eProformas();
        blEncrypta blEncryp = new blEncrypta(Program.Sesion.Key);
        SqlConnection Conexion_Reporte = new SqlConnection();
        public rptFichaCotizacion()
        {
            InitializeComponent();
        }
        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string entorno = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("Conexion_Reporte")].ToString());
            string Servidor = blEncryp.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[blEncryp.Encrypta("ServidorREMOTO")].ToString());
            string BBDD = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("BBDD")].ToString());
            string UserID = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("UserID")].ToString());
            string Password = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("Password")].ToString());

            e.ConnectionParameters = new MsSqlConnectionParameters(Servidor, BBDD, UserID, Password, MsSqlAuthorizationType.SqlServer);
        }
    }
}
