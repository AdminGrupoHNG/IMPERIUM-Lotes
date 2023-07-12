using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.DataAccess.ConnectionParameters;

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class rptProforma_detalle : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly UnitOfWork unit2;
        SqlConnection Conexion_Reporte = new SqlConnection();

        public rptProforma_detalle()
        {
            InitializeComponent();
            unit2 = new UnitOfWork();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string entorno = unit2.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit2.Encripta.Encrypta("conexion")].ToString());
            string Servidor = unit2.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit2.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit2.Encripta.Encrypta("ServidorREMOTO")].ToString());
            string BBDD = unit2.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit2.Encripta.Encrypta("BBDD")].ToString());
            string UserID = unit2.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit2.Encripta.Encrypta("UserID")].ToString());
            string Password = unit2.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit2.Encripta.Encrypta("Password")].ToString());

            e.ConnectionParameters = new MsSqlConnectionParameters(Servidor, BBDD, UserID, Password, MsSqlAuthorizationType.SqlServer);
        }
    }
}
