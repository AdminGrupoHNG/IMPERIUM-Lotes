using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DA_GestionLotes
{
    public class daSQL
    {
        SqlConnection dbConn;
        readonly daEncrypta daEncryp;
        public daSQL(string key)
        {
            dbConn = new SqlConnection();
            daEncryp = new daEncrypta(key);
        }
        private string GetSettings(string parametro) { return daEncryp.Desencrypta(ConfigurationManager.AppSettings[daEncryp.Encrypta(parametro)]).ToString(); }
        private ConnectionStringSettings GetConnectionStringSettings()
        {
            string entorno = GetSettings("conexion");
            string Servidor = entorno == "LOCAL" ? GetSettings("ServidorLOCAL") : GetSettings("ServidorREMOTO");
            string BBDD = GetSettings("BBDD");
            string UserID = GetSettings("UserID");
            string Password = GetSettings("Password");
            string AppName = GetSettings("AppName");

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = Servidor;
            builder.InitialCatalog = BBDD;
            builder.UserID = UserID;
            builder.Password = Password;
            //if (entorno == "LOCAL") builder.IntegratedSecurity = true;  //false si se usa USER y PASS, o true si se usa credenciales de windows
            builder.IntegratedSecurity = false;
            builder.ApplicationName = AppName;

            return new ConnectionStringSettings("dbConn", builder.ConnectionString);
        }

        public void GetConnection()
        {
            dbConn = new SqlConnection(GetConnectionStringSettings().ConnectionString);
            try
            {
                dbConn.Open();
                //daEncryp.Encrypta();
            }
            catch (Exception ex)
            {
                //daEncryp.Encrypta();
                var message = ex.Message;
                throw;
            }
        }

        public string TestConnection()
        {
            string result = "";
            try
            {
                dbConn = new SqlConnection(GetConnectionStringSettings().ConnectionString);
                dbConn.Open();
                result = dbConn.State == ConnectionState.Open ? "OK" : "";
                //oEncryp.Encrypta();
                return result;
            }
            catch (Exception ex)
            {
                //oEncryp.Encrypta();
                dbConn.Close();
                return ex.ToString();
                //return;
            }
        }

        public T ConsultarEntidad<T>(string procedure, Dictionary<string, object> dictionary) where T : class, new()
        {
            if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
            T entidad = new T();
            DataTable table = GetTable(procedure, dictionary);
            try
            {
                DataRow row = table.Rows[0];
                entidad = ToObject<T>(row);
                //dbConn.Close();

                return entidad;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }
        public List<T> ListaconSP<T>(string procedure, Dictionary<string, object> dictionary) where T : class, new()
        {
            if (dbConn.State == ConnectionState.Closed) { GetConnection(); };
            DataTable table = GetTable(procedure, dictionary);
            try
            {
                List<T> myList = new List<T>();
                foreach (DataRow row in table.Rows)
                {
                    T obj = new T();
                    obj = ToObject<T>(row);
                    myList.Add(obj);
                }
                return myList;
            }
            catch (Exception ex)
            {
                return new List<T>();
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }


        public DataTable ListaDatatable(string procedure, Dictionary<string, object> dictionary)
        {
            if (dbConn.State == ConnectionState.Closed) { GetConnection(); };
            DataTable table = GetTable(procedure, dictionary);
            try
            {
                return table;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                dbConn.Close();
            }
        }


        public List<T> ListasinSP<T>(string query) where T : class, new()
        {
            try
            {
                if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
                List<T> myList = new List<T>();
                SqlDataAdapter reader = new SqlDataAdapter();
                DataTable table = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = dbConn;
                reader.SelectCommand = cmd;
                reader.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    T obj = new T();
                    obj = ToObject<T>(row);
                    myList.Add(obj);
                }

                return myList;
            }
            catch (Exception ex)
            {
                return new List<T>();
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public const string sNullable = "Nullable`1";
        public static T ToObject<T>(DataRow row) where T : class, new()
        {
            T obj = new T();

            foreach (DataColumn col in row.Table.Columns)
            {
                PropertyInfo prop = obj.GetType().GetProperty(col.ColumnName);
                if (prop != null)
                {
                    string propName = prop.PropertyType.Name;
                    if (propName == sNullable)
                    {
                        propName = Nullable.GetUnderlyingType(prop.PropertyType).Name;
                    }

                    if (prop.CanWrite & !object.ReferenceEquals(row[col], DBNull.Value) & col.DataType.Name == propName)
                    {
                        prop.SetValue(obj, row[col], null);
                    }
                }
            }

            return obj;
        }

        public DataTable GetTable(string procedure, Dictionary<string, object> dictionary)
        {
            try
            {
                DataTable tabla = new DataTable();
                SqlDataAdapter reader = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(procedure, dbConn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                agregarParametros(cmd, dictionary);

                reader.SelectCommand = cmd;
                reader.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                return new DataTable();
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void agregarParametros(SqlCommand cmd, Dictionary<string, object> dictionary)
        {
            foreach (KeyValuePair<string, object> k in dictionary)
            {
                cmd.Parameters.AddWithValue(k.Key, k.Value);
            }
        }
        public DataSet TableSet(string sProcedure, Dictionary<string, object> aParametros = null, int TimeOut = 0)
        {
            DataSet oDataSet = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter reader = new SqlDataAdapter();

            try
            {
                if (dbConn.State == ConnectionState.Closed) { GetConnection(); };

                if (TimeOut == 0)
                    cmd = new SqlCommand(sProcedure, dbConn) { CommandType = CommandType.StoredProcedure };
                else
                    cmd = new SqlCommand(sProcedure, dbConn) { CommandType = CommandType.StoredProcedure, CommandTimeout = TimeOut };

                if (aParametros != null)
                    agregarParametros(cmd, aParametros);

                reader.SelectCommand = cmd;
                reader.Fill(oDataSet);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return oDataSet;
        }

        public string ExecuteSPRetornoValor(string procedure, Dictionary<string, object> dictionary)
        {
            try
            {
                if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = dbConn;
                agregarParametros(cmd, dictionary);

                //En el SP agregar -->      @ValorDeSalida VARCHAR(100) = '' OUT
                SqlParameter pvNewId = new SqlParameter("@ValorDeSalida", SqlDbType.VarChar, 100);
                pvNewId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvNewId);
                var val = cmd.ExecuteNonQuery();

                if (val >= 1)
                {
                    return "OK" + cmd.Parameters["@ValorDeSalida"].Value.ToString();
                }
                else
                {
                    return "ERROR";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                throw;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public string ExecuteScalarWithParams(string procedure, Dictionary<string, object> dictionary)
        {
            try
            {
                if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = dbConn;

                agregarParametros(cmd, dictionary);
                var val = cmd.ExecuteNonQuery();

                //return "OK";
                if (val < 0)
                {
                    return "ERROR";
                }
                else
                {
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                throw;
            }
            finally
            {
                dbConn.Close();
            }
        }
        public void CargaCombosLookUp(string procedure, LookUpEdit combo, Dictionary<string, object> dictionary, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "")
        {
            try
            {
                if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
                combo.Properties.DataSource = GetTable(procedure, dictionary);
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
            }
            catch (Exception)// ex)
            {
                // throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }
        public void CargaCombosChecked(string procedure, CheckedComboBoxEdit combo, Dictionary<string, object> dictionary, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "")
        {
            try
            {
                if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
                combo.Properties.DataSource = GetTable(procedure, dictionary);
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.EditValue = ""; } else { combo.SetEditValue(campoSelectedValue); }
            }
            catch (Exception)// ex)
            {
                // throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }
    }
}