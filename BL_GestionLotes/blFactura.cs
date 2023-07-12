using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_GestionLotes;
using DA_GestionLotes;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace BL_GestionLotes
{
    public class blFactura
    {
        readonly daSQL sql;
        public blFactura(daSQL sql) { this.sql = sql; }

        public string EliminarMaestrosGenerales(int opcion, string cod_tipo_gasto = "", string cod_empresa = "", string cod_und_negocio = "")
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_tipo_gasto", cod_tipo_gasto },
                { "cod_empresa", cod_empresa },
                { "cod_und_negocio", cod_und_negocio }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_MaestrosGenerales", dictionary);
            return result;
        }
        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "",
                                    bool valorDefecto = false, string cod_proveedor = "", string cod_usuario = "", string cod_tipo_transaccion = "",
                                    string cod_empresa = "", string cod_cliente = "", string cod_solucion = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_FacturasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();



            try
            {
                switch (nCombo)
                {
                    case "ModalidadPago":
                        dictionary.Add("opcion", 2);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoObligacion":
                        dictionary.Add("opcion", 3);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoRegistro":
                        dictionary.Add("opcion", 5);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoPago":
                        dictionary.Add("opcion", 6);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoFecha":
                        dictionary.Add("opcion", 7);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EstadoDocumento":
                        dictionary.Add("opcion", 8);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EmpresaProveedor":
                        procedure = "usp_Consulta_ListarFacturasProveedor";
                        dictionary.Add("opcion", 8);
                        dictionary.Add("cod_proveedor", cod_proveedor);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EmpresasUsuarios":
                        procedure = "usp_Consulta_ListarProveedores";
                        dictionary.Add("opcion", 11);
                        dictionary.Add("cod_usuario", cod_usuario);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Pagar_A":
                        procedure = "usp_ConsultasVarias_FacturasProveedor";
                        dictionary.Add("opcion", 11);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoTransaccion":
                        dictionary.Add("opcion", 20);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ConceptoDetraccion":
                        dictionary.Add("opcion", 21);
                        dictionary.Add("cod_tipo_transaccion", cod_tipo_transaccion);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "DistribucionCECO":
                        dictionary.Add("opcion", 32);
                        dictionary.Add("cod_empresa", cod_empresa);
                        dictionary.Add("cod_cliente", cod_cliente);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;



                    case "GrupoFormatoDocumento":
                        procedure = "Usp_RHU_ConsultasVarias_FormatoMDocumento";
                        dictionary.Add("opcion", 2);
                        dictionary.Add("cod_solucion", cod_solucion);
                        tabla = this.sql.ListaDatatable(procedure, dictionary);
                        break;
                }



                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
                if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T InsertarUnidadNegocio<T>(eUnidadNegocio eObj) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eObj.cod_empresa },
                { "cod_und_negocio", eObj.cod_und_negocio },
                { "dsc_und_negocio", eObj.dsc_und_negocio },
                { "dsc_pref_ceco", eObj.dsc_pref_ceco },
                { "flg_activo", eObj.flg_activo },
                { "flg_defecto", eObj.flg_defecto },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_UnidadNegocio", dictionary);
            return obj;
        }

        public T InsertarTipoGastoCosto<T>(eTipoGastoCosto eObj) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_tipo_gasto", eObj.cod_tipo_gasto },
                { "dsc_tipo_gasto", eObj.dsc_tipo_gasto },
                { "dsc_pref_ceco", eObj.dsc_pref_ceco },
                { "dsc_ceco_ALTERNATVO", eObj.dsc_ceco_ALTERNATVO },
                { "cod_empresa", eObj.cod_empresa }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_TipoGastoCosto", dictionary);
            return obj;
        }

        public T InsertarTipoCambio<T>(eTipoCambio eObj) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "fch_cambio", eObj.fch_cambio },
                { "imp_cambio_compra", eObj.imp_cambio_compra },
                { "imp_cambio_venta", eObj.imp_cambio_venta },
                { "cod_moneda", eObj.cod_moneda },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_TipoCambio", dictionary);
            return obj;
        }

        public List<T> FiltroFactura<T>(int opcion, string cod_empresa = "", string tipo_documento = "", string cod_estado_registro = "", string cod_estado_pago = "", string cod_tipo_fecha = "",
                                        string FechaInicio = "", string FechaFin = "", int Anho = 0, int Mes = 0, string flg_IGV = "", string cod_proveedor = "",
                                        string serie_documento = "", decimal numero_documento = 0, int SinSaldo = 0, string cod_moneda = "", int Semana = 0,
                                        string aplicar_conversion = "NO", string flg_CajaChica = "NO", string flg_EntregasRendir = "NO", string cod_proyecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion}, { "cod_empresa", cod_empresa}, { "tipo_documento", tipo_documento}, { "cod_estado_registro", cod_estado_registro},
                { "cod_estado_pago", cod_estado_pago}, { "cod_tipo_fecha", cod_tipo_fecha}, { "FechaInicio", FechaInicio}, { "FechaFin", FechaFin},
                { "Anho", Anho}, { "Mes", Mes}, { "flg_IGV", flg_IGV }, { "cod_proveedor", cod_proveedor }, { "serie_documento", serie_documento },
                { "numero_documento", numero_documento }, { "SinSaldo", SinSaldo }, { "cod_moneda", cod_moneda }, { "Semana", Semana },
                { "aplicar_conversion", aplicar_conversion }, { "flg_CajaChica", flg_CajaChica }, { "flg_EntregasRendir", flg_EntregasRendir },
                { "cod_proyecto", cod_proyecto }
            };
            myList = sql.ListaconSP<T>("usp_Consulta_ListarFacturasProveedor", oDictionary);
            return myList;
        }

        public List<T> Obtener_MaestrosGenerales<T>(int opcion, string cod_empresa, string cod_proveedor = "", string cod_und_negocio = "", string cod_cliente = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_empresa", cod_empresa);
            dictionary.Add("cod_proveedor", cod_proveedor);
            dictionary.Add("cod_und_negocio", cod_und_negocio);
            dictionary.Add("cod_cliente", cod_cliente);

            myList = sql.ListaconSP<T>("usp_Consulta_ListarFacturasProveedor", dictionary);
            return myList;
        }

        public List<T> CombosEnGridControl<T>(string nCombo, string dato = "", string cod_proveedor = "", string cod_empresa = "", string tipo_documento = "", string documento = "", string cod_und_negocio = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            string procedure = "usp_Consulta_ListarFacturasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //dictionary.Add("dato", dato);

            switch (nCombo)
            {
                case "TipoServicio":
                    procedure = "usp_Consulta_ListarProveedores";
                    dictionary.Add("opcion", 9);
                    dictionary.Add("cod_proveedor", cod_proveedor);
                    break;
                case "UnidadNegocio":
                    dictionary.Add("opcion", 9);
                    dictionary.Add("cod_empresa", cod_empresa);
                    break;
                case "TipoGastoCosto":
                    dictionary.Add("opcion", 10);
                    dictionary.Add("cod_empresa", cod_empresa);
                    break;
                case "ClienteEmpresa":
                    dictionary.Add("opcion", 11);
                    dictionary.Add("cod_empresa", cod_empresa);
                    dictionary.Add("cod_und_negocio", cod_und_negocio);
                    break;
                case "EstadoProgramacion":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 10);
                    break;
                case "Pagar_A":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 11);
                    break;
                case "TipoDocumento":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 1);
                    break;
                case "Documento":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 15);
                    dictionary.Add("tipo_documento", tipo_documento);
                    break;
                case "DatosDocumento":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 16);
                    dictionary.Add("documento", documento);
                    break;
                case "Meses":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 19);
                    break;
                case "TipoGasto":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 23);
                    break;
                case "FlagDefecto":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 24);
                    break;
                case "FormaPago":
                    procedure = "usp_ConsultasVarias_FacturasProveedor";
                    dictionary.Add("opcion", 31);
                    break;
            }

            myList = sql.ListaconSP<T>(procedure, dictionary);
            return myList;
        }
        public T BuscarTipoCambio<T>(int opcion, DateTime fch_cambio) where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("fch_cambio", fch_cambio);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }
    }
}
    