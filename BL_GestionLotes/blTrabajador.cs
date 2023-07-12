using DA_GestionLotes;
using BE_GestionLotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Data;

namespace BL_GestionLotes
{
    public class blTrabajador
    {
        readonly daSQL sql;
        public blTrabajador(daSQL sql) { this.sql = sql; }

        public List<T> ListarTrabajadores<T>(int opcion, string cod_trabajador, string cod_empresa) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_trabajador", cod_trabajador },
                { "cod_empresa", cod_empresa }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarTrabajador", oDictionary);
            return myList;
        }

    }
}
