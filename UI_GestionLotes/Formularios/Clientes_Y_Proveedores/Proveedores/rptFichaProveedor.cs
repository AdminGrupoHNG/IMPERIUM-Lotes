using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.ConnectionParameters;
using System.Configuration;
using BE_GestionLotes;
using BL_GestionLotes;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace UI_GestionLotes.Formularios.Clientes_Y_Proveedores.Proveedores
{
    public partial class rptFichaProveedor : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly UnitOfWork unit;
        public eUsuario user = new eUsuario();
        SqlConnection Conexion_Reporte = new SqlConnection();
        public rptFichaProveedor()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            string Servidor = unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            string BBDD = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            string UserID = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
            string Password = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());

            e.ConnectionParameters = new MsSqlConnectionParameters(Servidor, BBDD, UserID, Password, MsSqlAuthorizationType.SqlServer); 
        }
    }
}
