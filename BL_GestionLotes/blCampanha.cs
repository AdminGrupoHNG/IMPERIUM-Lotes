using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_GestionLotes;
using DA_GestionLotes;
using DevExpress.XtraEditors;
using System.Data;


namespace BL_GestionLotes
{
    public class blCampanha
    {
        readonly daSQL sql;
        readonly string key;
        public blCampanha(daSQL sql, string key) { this.sql = sql; this.key = key; }

        public List<T> ListarEjecuPros<T>(int opcion, string usuario = "", string cod_empresa = "", string cod_proyecto = "", string valor_1 = "", string valor_4 = "", string flg_activo = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_usuario", usuario },
                { "cod_empresa", cod_empresa },
                { "cod_proyecto", cod_proyecto },
                { "valor_1", valor_1},
                { "valor_4", valor_4},
                { "flg_activo", flg_activo}

            };

            myList = sql.ListaconSP<T>("usp_lte_consultasvarias_lotes", oDictionary);
            return myList;
        }

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_uno = "", string cod_dos = "", string cod_tres = "", string cod_cuatro = "", int num_linea = 0, int num_nivel = 0, string cod_condicion = "", bool valorDefecto = false)
        {
            combo.Text = "";
            string procedure = "usp_lte_listar_minutos_alerta";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "Minutos":
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;

                }

                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
                if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void pDatosAExcel(string Coneccion, Microsoft.Office.Interop.Excel.Application xls, string cadSql, string Nombre, string celda, bool AjustarColumnas = false, bool NoCab = false)
        {
            {
                var withBlock = xls.ActiveSheet.QueryTables.Add(Connection: Coneccion
                  , Destination: xls.Range[celda]);
                withBlock.CommandText = cadSql;
                withBlock.Name = Nombre;
                withBlock.FieldNames = !(NoCab); // True
                withBlock.RowNumbers = false;
                withBlock.FillAdjacentFormulas = false;
                withBlock.PreserveFormatting = true;
                withBlock.RefreshOnFileOpen = false;
                withBlock.BackgroundQuery = true;
                withBlock.SavePassword = true;
                withBlock.SaveData = true;
                if (AjustarColumnas == true)
                    withBlock.AdjustColumnWidth = true;
                withBlock.RefreshPeriod = 0;
                withBlock.PreserveColumnInfo = true;
                withBlock.Refresh(BackgroundQuery: false);
            }
        }

        public DataTable ObtenerListadoGridLookup(string nCombo, string usuario = "", string cod_empresa = "", string cod_proyecto = "", string valor_1 = "", string valor_4 = "", string flg_activo = "")
        {
            string procedure = "usp_lte_consultasvarias_lotes"; //17
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();
            dictionary.Add("cod_usuario", usuario);
            dictionary.Add("cod_empresa", cod_empresa);
            dictionary.Add("cod_proyecto", cod_proyecto);
            dictionary.Add("valor_1", valor_1);
            dictionary.Add("valor_4", valor_4);
            dictionary.Add("flg_activo", flg_activo);

            try
            {
                switch (nCombo)
                {
                    case "empresas":
                        dictionary.Add("opcion", 0);
                        break;
                    case "campanhaempresa":
                        dictionary.Add("opcion", 2);
                        break;
                    case "responsablecampanha":
                        dictionary.Add("opcion", 3);
                        break;
                    case "tipo_campanha":
                        dictionary.Add("opcion", 4);
                        break;
                    case "tipo_frecuencia":
                        dictionary.Add("opcion", 5);
                        break;
                    case "proyectos":
                        dictionary.Add("opcion", 6);
                        break;
                    case "ejecutivos":
                        dictionary.Add("opcion", 7);
                        break;
                    case "grupofamiliar":
                        dictionary.Add("opcion", 8);
                        break;
                    case "rangorenta":
                        dictionary.Add("opcion", 9);
                        break;
                    case "resultadocontacto":
                        dictionary.Add("opcion", 10);
                        break;
                    case "detalleresultadocontacto":
                        dictionary.Add("opcion", 11);
                        break;
                    case "expectativaprospecto":
                        dictionary.Add("opcion", 12);
                        break;
                    case "estadoprospecto":
                        dictionary.Add("opcion", 13);
                        break;
                    case "tipocontacto":
                        dictionary.Add("opcion", 14);
                        break;
                    case "proyectoempresa":
                        dictionary.Add("opcion", 15);
                        break;
                    case "motivonointeres":
                        dictionary.Add("opcion", 16);
                        break;
                    case "canal":
                        dictionary.Add("opcion", 17);
                        break;
                    case "campanas":
                        dictionary.Add("opcion", 21);
                        break;
                    case "segmento":
                        dictionary.Add("opcion", 22);
                        break;
                        ;
                }
                tabla = sql.ListaDatatable(procedure, dictionary);

                return tabla;
            }
            catch (Exception ex)
            {
                return new DataTable();
                throw;
            }
        }

        public List<T> CombosEnGridControl<T>(string nCombo, string valor_1 = "", string valor_4 = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            string procedure = "usp_lte_consultasvarias_lotes";
            Dictionary<string, object> oDictionary = new Dictionary<string, object>();
            oDictionary.Add("valor_1", valor_1);
            oDictionary.Add("valor_4", valor_4);
            switch (nCombo)
            {
                case "Campanha":
                    oDictionary.Add("opcion", 27);
                    break;
                case "Estado":
                    oDictionary.Add("opcion", 20);
                    break;
                case "Canal":
                    oDictionary.Add("opcion", 17);
                    break;
                case "Punto Contacto":
                    oDictionary.Add("opcion", 4);
                    break;
                case "detalleresultadocontacto":
                    oDictionary.Add("opcion", 11);
                    break;
            }

            myList = sql.ListaconSP<T>(procedure, oDictionary);
            return myList;
        }

        public DataTable ObtenerListadoGridLookup_Otros(string nCombo, string usuario = "", string cod_empresa = "", string cod_proyecto = "")
        {
            string procedure = "usp_ConsultasVarias_Proyectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();
            try
            {
                switch (nCombo)
                {
                    case "TipoMoneda":
                        dictionary.Add("accion", 14);
                        break;
                        ;
                }
                tabla = sql.ListaDatatable(procedure, dictionary);

                return tabla;
            }
            catch (Exception ex)
            {
                return new DataTable();
                throw;
            }
        }
        public void CargarCombos_TablasMaestras(string sInd, string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, eCampanha eCamp)
        {
            DataTable tabla = new DataTable();
            if (sInd == "1")
            {
                tabla = ObtenerListadoGridLookup(nCombo, eCamp.cod_usuario, eCamp.cod_empresa, eCamp.cod_proyecto, eCamp.valor_1, eCamp.valor_4);
            }
            else if (sInd == "2")
            {
                tabla = ObtenerListadoGridLookup_Otros(nCombo, eCamp.cod_usuario, eCamp.cod_empresa, eCamp.cod_proyecto);
            }

            new blGlobales(sql, key).CargarCombosGridLookup(tabla, combo, campoValueMember, campoDispleyMember, "", false);
        }
        public void CargarCombos_TablasMaestras(string sInd, string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, eCampanha eCamp)
        {
            DataTable tabla = new DataTable();
            if (sInd == "1")
            {
                tabla = ObtenerListadoGridLookup(nCombo, eCamp.cod_usuario, eCamp.cod_empresa, eCamp.cod_proyecto, eCamp.valor_1, flg_activo: eCamp.flg_activo);
            }
            else if (sInd == "2")
            {
                tabla = ObtenerListadoGridLookup_Otros(nCombo, eCamp.cod_usuario, eCamp.cod_empresa, eCamp.cod_proyecto);
            }

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;

            //new blGlobales(sql, key).CargarCombosGridLookup(tabla, combo, campoValueMember, campoDispleyMember, "", false);
        }


        public List<T> ListarProyectoscampanhasMenu<T>(int opcion, string empresa, string proyecto, string usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", empresa },
                {"cod_proyecto", proyecto },
                {"cod_usuario", usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_menu_proyectos_campanha", oDictionary);
            return myList;
        }
        public List<T> Listarcampanhas<T>(int opcion = 0, string filtro = "", string cod_usuario = "", string cod_campanha = "", string estado = "", string cod_prospecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"filtro", filtro },
                {"cod_usuario", cod_usuario },
                {"cod_campanha", cod_campanha },
                {"estado", estado },
                {"cod_prospecto", cod_prospecto }
            };

            myList = sql.ListaconSP<T>("usp_lte_Listar_campanha", oDictionary);
            return myList;
        }
        public List<T> ListarSegmentos<T>(int opcion = 0, string cod_proyecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_proyecto", cod_proyecto }
            };

            myList = sql.ListaconSP<T>("usp_lte_consultasvarias_lotes", oDictionary);
            return myList;
        }

        public T Mantenimiento_Segmentos<T>(eVariablesGenerales eVa) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Segmento";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_variable", eVa.cod_variable },
                { "dsc_Nombre", eVa.dsc_Nombre },
                { "valor_3", eVa.valor_3 },
                { "cod_usuario_registro", eVa.cod_usuario_registro },
                { "cod_usuario_cambio", eVa.cod_usuario_cambio }
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public List<T> ListarRegistrosConSegmentos<T>(string cod_variable) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_variable", cod_variable }
            };

            myList = sql.ListaconSP<T>("usp_lte_listarLotesConSegmentos", oDictionary);
            return myList;
        }
        public List<T> ListarcampanhasDetalle<T>(int opcion, string cod_empresa, string cod_proyecto, string cod_campanha) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_campanha", cod_campanha },

            };

            myList = sql.ListaconSP<T>("usp_lte_listar_campanha_detalle", oDictionary);
            return myList;
        }

        public T Guardar_Actualizar_campanha<T>(eCampanha eCamp, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_campanha";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", MiAccion == "Nuevo" ? "1" : "2" },
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_campanha", eCamp.cod_campanha },
                { "dsc_campanha", eCamp.dsc_campanha },
                { "fch_inicio_campanha", Convert.ToDateTime(eCamp.fch_inicio_campanha) },
                { "fch_fin_campanha", Convert.ToDateTime(eCamp.fch_fin_campanha) },
                { "cod_referencia_campanha", eCamp.cod_referencia_campanha },
                { "dsc_descripcion ", eCamp.dsc_descripcion },
                { "dsc_comentario", eCamp.dsc_comentario },
                { "dsc_objetivo", eCamp.dsc_objetivo },
                { "cod_responsable", eCamp.cod_responsable },
                { "cod_canal", eCamp.cod_canal },
                { "cod_tipo_campanha", eCamp.cod_tipo_campanha },
                { "cod_moneda", eCamp.cod_moneda },
                { "imp_importe", eCamp.imp_importe },
                { "cod_segmento", eCamp.cod_segmento },
                { "cod_tipo_frecuencia", eCamp.cod_tipo_frecuencia },
                { "flg_activo_excel", eCamp.flg_activo_excel },
                { "cod_usuario", eCamp.cod_usuario }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public T Guardar_Actualizar_campanha_Detalle<T>(eCampanha.eCampanha_Detalle eCamp, int opcion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_campanha_detalle";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_campanha", eCamp.cod_campanha },
                { "cod_campanha_detalle", eCamp.cod_campanha_detalle },
                { "fch_registro_detalle", eCamp.fch_registro_detalle },
                { "dsc_identificador_transaccion", eCamp.dsc_identificador_transaccion },
                { "dsc_metodo_pago", eCamp.dsc_metodo_pago },
                { "imp_importe", eCamp.imp_importe },
                { "imp_importe_facturado_total", eCamp.imp_importe_facturado_total },
                { "cod_estado_pago", eCamp.cod_estado_pago },
                { "cod_usuario", eCamp.cod_usuario }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public T Eliminar_campanha<T>(string cod_campanha, string cod_usuario) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
               { "opcion",  "3" },
                { "cod_campanha", cod_campanha },
                { "cod_usuario", cod_usuario }
            };

            obj = sql.ConsultarEntidad<T>("usp_lte_insertar_actualizar_eliminar_campanha", dictionary);
            return obj;
        }
        public T Activar_Inactivar_campanha<T>(string MiAccion, string cod_campanha, string cod_usuario) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", MiAccion /*== "Activar" ? "4" : "5"*/ },
                { "cod_campanha", cod_campanha },
                { "cod_usuario", cod_usuario }
            };

            obj = sql.ConsultarEntidad<T>("usp_lte_insertar_actualizar_eliminar_campanha", dictionary);
            return obj;
        }

        public List<T> ListarOpcionesMenu<T>(string cod_usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"cod_usuario", cod_usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_treeListProspectos", oDictionary);
            return myList;
        }


        public List<T> ListarEjecutivosVentasMenu<T>(int opcion, string empresa, string proyecto, string Usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", empresa },
                {"cod_proyecto", proyecto },
                {"cod_usuario", Usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_menu_ejecutivosventas", oDictionary);
            return myList;
        }
        public List<T> ListarAsignacionProspectoConf<T>(int opcion, string cod_empresa, string cod_proyecto, string cod_usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_usuario", cod_usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_conf_asignacion_prospecto", oDictionary);
            return myList;
        }
        public T Guardar_Actualizar_AsignacionProspectoConf<T>(eCampanha eCamp) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_conf_asignacion_prospecto";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", "1"},
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_ejecutivo", eCamp.cod_ejecutivo },
                { "cod_origen_prospecto", eCamp.cod_origen_prospecto },
                { "prc_asignacion", eCamp.prc_asignacion },
                { "cod_usuario", eCamp.cod_usuario }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public DataSet ListarPreviewProspectosAsignados(int opcion, string cod_empresa, string cod_proyecto, string cod_usuario)
        {
            DataSet myList = new DataSet();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_usuario", cod_usuario }
            };

            myList = sql.TableSet("usp_lte_listar_prospecto_ejecutivos_automatico", oDictionary);
            return myList;
        }
        public T Anular_Eliminar_Segmentos<T>(int opcion, eCampanha eEtaLo) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_EliminarAnularSegmento";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_variable", eEtaLo.cod_segmento },
                { "valor_3", eEtaLo.cod_proyecto },
                { "flg_activo", eEtaLo.flg_activo },
                { "cod_usuario_cambio", eEtaLo.cod_usuario_cambio }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public T Modificar_AsignacionProspecto<T>(int opcion, eCampanha eCamp) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_modificar_asignacion_prospecto_ejecutivos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_ejecutivo", eCamp.cod_ejecutivo },
                { "cod_prospecto", eCamp.cod_prospecto },
                //{ "cod_origen_prospecto", eCamp.cod_origen_prospecto },
                { "cnt_gestiones", eCamp.cnt_gestiones },
                { "cod_usuario", eCamp.cod_usuario },
                { "cod_evento", eCamp.cod_evento },
                { "cod_evento_cita", eCamp.cod_evento_cita_proxima }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Modificar_Reasignacion_Proyecto<T>(eCampanha ePro, string _cod_empresa_new, string _cod_proyecto_new) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_actualizar_empresa_proyecto_prospectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_empresa_old", ePro.cod_empresa },
                { "cod_proyecto_old", ePro.cod_proyecto },
                { "cod_empresa_new", _cod_empresa_new },
                { "cod_proyecto_new", _cod_proyecto_new },
                { "cod_prospecto_old", ePro.cod_prospecto }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public List<T> ListarEmpresas_ProspectosMenu<T>(int opcion, string Usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_usuario", Usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_menu_empresas_prospectos", oDictionary);
            return myList;
        }
        public List<T> ListadoCombos<T>(int opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }
            };

            myList = sql.ListaconSP<T>("usp_lte_consultasvarias_lotes", oDictionary);
            return myList;
        }

        public T ObtenerProspecto<T>(int opcion, string cod_empresa, string cod_proyecto, string cod_prospecto) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_prospecto", cod_prospecto }
            };
            obj = sql.ConsultarEntidad<T>("usp_lte_Listar_prospectos", dictionary);
            return obj;
        }

        public T ActualizarEstadoEvento<T>(int opcion, eCampanha eCamp) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_actualizar_resultado_eventos";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_evento", eCamp.cod_evento },
                { "cod_prospecto", eCamp.cod_prospecto },
                { "cod_usuario", eCamp.cod_usuario }

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;

        }

        public List<T> ListarProspectos<T>(int opcion, string cod_empresa, string cod_proyecto, string cod_prospecto, string cod_usuario, string FiltroEje, string EstadoFiltro, string TipoContacto, string FiltroCitasPendientes = "", string FiltroCitasAsistidas = "", string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "", string TipoCanal = "", string TipoEjecutivo = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_prospecto", cod_prospecto },
                {"cod_usuario", cod_usuario },
                {"filtro", FiltroEje },
                {"filtro_estado", EstadoFiltro },
                {"filtro_tipocontacto", TipoContacto },
                {"FechaInicio", FechaInicio},
                {"FechaFin", FechaFin},
                {"cod_tipo_fecha", cod_tipo_fecha},
                {"flg_citas_pendientes", FiltroCitasPendientes},
                {"flg_citas_Asistidas", FiltroCitasAsistidas},
                {"filtro_tipocanal", TipoCanal},
                {"dsc_telefono_movil", ""},
                {"filtro_tipo_asesor", TipoEjecutivo}
            };
            myList = sql.ListaconSP<T>("usp_lte_Listar_prospectos", oDictionary);
            return myList;
        }

        public List<T> DashboardsProspectos<T>(int opcion, string cod_empresa = "", string cod_proyecto = "", string cod_prospecto = "", string cod_usuario = "", string FiltroEje = "", string EstadoFiltro = "", string TipoContacto = "", string FiltroCitasPendientes = "", string FiltroCitasAsistidas = "", string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "", string TipoCanal = "", string TipoEjecutivo = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_prospecto", cod_prospecto },
                {"cod_usuario", cod_usuario },
                {"filtro", FiltroEje },
                {"filtro_estado", EstadoFiltro },
                {"filtro_tipocontacto", TipoContacto },
                {"FechaInicio", FechaInicio},
                {"FechaFin", FechaFin},
                {"cod_tipo_fecha", cod_tipo_fecha},
                {"flg_citas_pendientes", FiltroCitasPendientes},
                {"flg_citas_Asistidas", FiltroCitasAsistidas},
                {"filtro_tipocanal", TipoCanal},
                {"dsc_telefono_movil", ""},
                {"filtro_tipo_asesor", TipoEjecutivo}
            };
            myList = sql.ListaconSP<T>("usp_lte_dashboards_prospectos_2", oDictionary);
            return myList;
        }

        public List<T> ListarProspectosReporte<T>(int opcion, string cod_empresa = "", string cod_proyecto = "", string cod_prospecto = "", string fechaInicio = "", string fechaFin = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_prospecto", cod_prospecto },
                { "fechaInicio", fechaInicio },
                { "fechaFin", fechaFin }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_prospectos_reporte", oDictionary);
            return myList;
        }
        public List<T> ListarReporteProspectos<T>(int opcion, string cod_empresa, string cod_proyecto, string cod_prospecto, string cod_usuario, string fecha_inicio, string fecha_fin, string EstadoFiltro, string TipoContacto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_prospecto", cod_prospecto },
                {"cod_usuario", cod_usuario },
                {"fecha_inicio", fecha_inicio },
                {"fecha_fin", fecha_fin },
                {"filtro_estado", EstadoFiltro },
                {"filtro_tipocontacto", TipoContacto }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_prospectos_eventos_detallado", oDictionary);
            return myList;
        }

        public T Guardar_Actualizar_prospecto<T>(eCampanha eCamp, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_prospectos";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", MiAccion == "Nuevo" ? "1" : "2" },
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_prospecto", eCamp.cod_prospecto },
                { "cod_tipo_persona", eCamp.cod_tipo_persona },
                { "dsc_prospecto", eCamp.dsc_prospecto },
                { "dsc_apellido_paterno", eCamp.dsc_apellido_paterno },
                { "dsc_apellido_materno", eCamp.dsc_apellido_materno },
                { "dsc_nombres", eCamp.dsc_nombres },
                { "cod_tipo_documento", eCamp.cod_tipo_documento },
                { "dsc_num_documento", eCamp.dsc_num_documento },
                { "dsc_email", eCamp.dsc_email },
                { "cod_sexo", eCamp.cod_sexo },
                { "cod_canal", eCamp.cod_canal },
                { "cod_origen_prospecto", eCamp.cod_origen_prospecto },
                { "cod_ejecutivo", eCamp.cod_ejecutivo },
                { "fch_fec_nac", (String.IsNullOrEmpty(eCamp.fch_fec_nac) || eCamp.fch_fec_nac.Contains("1/01/0001") ? "" : (object)Convert.ToDateTime(eCamp.fch_fec_nac))},
                { "dsc_telefono", eCamp.dsc_telefono },
                { "dsc_telefono_movil", eCamp.dsc_telefono_movil },
                { "dsc_profesion", eCamp.dsc_profesion },
                { "cod_estado_civil", eCamp.cod_estado_civil },
                { "cod_nacionalidad", eCamp.cod_nacionalidad },
                { "cod_pais", eCamp.cod_pais },
                { "cod_departamento", eCamp.cod_departamento },
                { "cod_provincia", eCamp.cod_provincia },
                { "cod_distrito", eCamp.cod_distrito },
                { "dsc_direccion", eCamp.dsc_direccion },
                { "cod_grupo_familiar", eCamp.cod_grupo_familiar },
                { "cod_rango_renta", eCamp.cod_rango_renta },
                { "dsc_observacion", eCamp.dsc_observacion },
                { "cod_estado_prospecto", eCamp.cod_estado_prospecto },
                { "cod_campanha", eCamp.cod_referencia_campanha },
                { "cod_usuario", eCamp.cod_usuario },
                { "cod_segmento", eCamp.cod_segmento }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public T Habilitar_prospecto<T>(eCampanha eCamp) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_prospectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", "6" },
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_prospecto", eCamp.cod_prospecto },
                { "cod_tipo_persona", "" },
                { "dsc_prospecto", "" },
                { "dsc_apellido_paterno", "" },
                { "dsc_apellido_materno", "" },
                { "dsc_nombres", "" },
                { "cod_tipo_documento", "" },
                { "dsc_num_documento", "" },
                { "dsc_email", "" },
                { "cod_sexo", "" },
                { "fch_fec_nac", "" },
                { "dsc_telefono", "" },
                { "dsc_telefono_movil", "" },
                { "dsc_profesion", "" },
                { "cod_estado_civil", "" },
                { "cod_nacionalidad", "" },
                { "cod_pais", "" },
                { "cod_departamento", "" },
                { "cod_provincia", "" },
                { "cod_distrito", "" },
                { "dsc_direccion", "" },
                { "cod_grupo_familiar", "" },
                { "cod_rango_renta", "" },
                { "dsc_observacion", "" },
                { "cod_estado_prospecto", eCamp.cod_estado_prospecto },
                { "cod_campanha", "" },
                { "cod_usuario", eCamp.cod_usuario },
                { "cod_segmento", "" }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public T Importar_Prospectos<T>(eCampanha eCamp, string opcion = "") where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_importar_prospectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_prospecto", "" },
                { "dsc_prospecto", "" },
                { "dsc_apellido_paterno", eCamp.dsc_apellido_paterno },
                { "dsc_nombres", eCamp.dsc_nombres },
                { "fch_fecha", Convert.ToDateTime(eCamp.fch_fecha) },
                { "dsc_telefono_movil", eCamp.dsc_telefono_movil },
                { "cod_campanha", eCamp.cod_referencia_campanha },
                { "cod_usuario", eCamp.cod_usuario },
                { "cod_segmento", eCamp.cod_segmento },
                { "dsc_segmento", eCamp.dsc_segmento }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Importar_Prospectos<T>(eProspectosXLote ePro, string opcion = "") where T : class, new()
        {
            T obj = new T();
            string procedure = "usp_lte_importar_prospectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", ePro.cod_empresa },
                { "cod_proyecto", ePro.cod_proyecto },
                { "cod_prospecto", "" },
                { "dsc_prospecto", "" },
                { "dsc_apellido_paterno", ePro.dsc_apellido_paterno },
                { "dsc_nombres", ePro.dsc_nombres },
                { "fch_fecha", (String.IsNullOrEmpty(ePro.fch_fecha) ? "" : (object)Convert.ToDateTime(ePro.fch_fecha))},
                { "dsc_telefono_movil", ePro.dsc_telefono_movil },
                { "cod_campanha", ePro.cod_campanha },
                { "cod_usuario", ePro.cod_usuario },
                { "cod_segmento", ePro.cod_segmento },
                { "dsc_segmento", ePro.dsc_segmento },
                { "cod_canal", ePro.cod_canal },
                { "cod_origen_prospecto", ePro.cod_origen_prospecto },
                { "dsc_observacion", ePro.dsc_observacion },
                { "cod_ejecutivo", ePro.cod_ejecutivo },
                { "fch_registro", (ePro.fch_registro.ToString().Contains("1/01/0001")) ? DBNull.Value : (object)Convert.ToDateTime(ePro.fch_registro)},
                { "dsc_num_documento", ePro.dsc_num_documento },
                { "num_tipo_estado_prospecto", ePro.num_tipo_estado_prospecto },
                { "cod_referencia_campanha", ePro.cod_referencia_campanha }
                //{ "cod_campanha", ePro.cod_referencia_campanha },

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }


        public DataTable Listar_Prospectos_Resumen(string cod_empresa, string cod_proyecto, string cod_prospecto, string cod_usuario, string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "")
        {
            DataTable myList = new DataTable();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", "0"},
                { "cod_empresa", cod_empresa },
                { "cod_proyecto", cod_proyecto },
                { "cod_prospecto", cod_prospecto },
                { "cod_usuario", cod_usuario },
                {"FechaInicio", FechaInicio},
                {"FechaFin", FechaFin},
                {"cod_tipo_fecha", cod_tipo_fecha}
            };

            myList = sql.ListaDatatable("usp_lte_listar_prospecto_resumen", oDictionary);
            return myList;
        }


        public List<T> ListarEventos<T>(int opcion, string cod_empresa, string cod_prospecto, string cod_usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_prospecto", cod_prospecto },
                {"cod_usuario", cod_usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_eventos_prospectos", oDictionary);
            return myList;
        }

        public List<T> ListarCombosImportarProspectos<T>(string opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion }
            };

            myList = sql.ListaconSP<T>("usp_lte_lista_combos_importar_prospectos", oDictionary);
            return myList;
        }

        public List<T> ListarGridFiltros<T>(int opcion, int año = 0, string cod_empresa = "", string cod_proyecto = "", string cod_usuario = "", string FiltroEje = "", string TipoContacto = "", string FechaInicio = "", string FechaFin = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"Year", año },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_usuario", cod_usuario },
                {"filtro", FiltroEje },
                {"filtro_tipocontacto", TipoContacto },
                {"FechaInicio", FechaInicio},
                {"FechaFin", FechaFin}
            };
            myList = sql.ListaconSP<T>("usp_lte_listar_filtro_grid", oDictionary);
            return myList;
        }
        public T Guardar_Actualizar_Eventos<T>(eCampanha eCamp, string opcion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_eventos_prospectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_evento", eCamp.cod_evento },
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_prospecto", eCamp.cod_prospecto },
                { "cod_tipo_evento", eCamp.cod_tipo_evento },
                { "fch_evento", Convert.ToDateTime(eCamp.fch_evento) },
                { "cod_tipo_contacto", eCamp.cod_tipo_contacto },
                { "cod_respuesta", eCamp.cod_respuesta },
                { "cod_detalle_respuesta", eCamp.cod_detalle_respuesta },
                { "dsc_observacion", eCamp.dsc_observacion },
                { "cod_expectativa", eCamp.cod_expectativa },
                { "flg_receptivo", eCamp.flg_receptivo },
                { "cod_ejecutivo_cita", eCamp.cod_ejecutivo_cita },
                { "cod_motivo", eCamp.cod_motivo },
                { "cod_evento_ref", eCamp.cod_evento_ref },
                { "cod_usuario", eCamp.cod_usuario },
                { "cod_evento_principal", eCamp.cod_evento_principal },
                { "cod_reconfirmacion", eCamp.cod_reconfirmacion }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public List<T> Listar_Eventos_Confirmacion<T>(int opcion, string cod_empresa, string cod_evento, string cod_prospecto, string cod_usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                { "cod_evento", cod_evento },
                {"cod_prospecto", cod_prospecto },
                {"cod_usuario", cod_usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_eventos_confirmacion_prospectos", oDictionary);
            return myList;
        }
        public T Guardar_Actualizar_Eventos_Confirmacion<T>(eCampanha eCamp, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_eventos_confirmacion_prospectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", MiAccion == "Nuevo" ? "1" : MiAccion },
                { "cod_confirmacion", eCamp.cod_confirmacion },
                { "cod_evento", eCamp.cod_evento },
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_prospecto", eCamp.cod_prospecto },
                { "fch_evento", Convert.ToDateTime(eCamp.fch_evento) },
                { "cod_tipo_contacto", eCamp.cod_tipo_contacto },
                { "cod_respuesta", eCamp.cod_respuesta },
                { "cod_detalle_respuesta", eCamp.cod_detalle_respuesta },
                { "dsc_observacion", eCamp.dsc_observacion },
                { "flg_receptivo", eCamp.flg_receptivo },
                { "cod_usuario", eCamp.cod_usuario },
                { "cod_reconfirmacion", eCamp.cod_reconfirmacion },
                { "cod_proximo_evento", eCamp.cod_proximo_evento },
                { "cod_confirmacion_evento", eCamp.cod_confirmacion_evento },
                { "cod_ejecutivo_cita", eCamp.cod_ejecutivo_cita }

            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public List<T> Listar_Eventos_Calendario<T>(int opcion, string cod_empresa, string cod_proyecto, string cod_ejecutivo, string cod_usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                { "cod_proyecto", cod_proyecto },
                {"cod_ejecutivo", cod_ejecutivo },
                {"cod_usuario", cod_usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_eventos_calendario_prospectos", oDictionary);
            return myList;
        }

        public T VolverAgendar_Proximo_Eventos<T>(eCampanha eCamp) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_eventos_prospectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", "6" },
                { "cod_evento", eCamp.cod_evento },
                { "cod_empresa", eCamp.cod_empresa },
                { "cod_proyecto", eCamp.cod_proyecto },
                { "cod_prospecto", eCamp.cod_prospecto },
                { "cod_tipo_evento", eCamp.cod_tipo_evento },
                { "fch_evento", Convert.ToDateTime(eCamp.fch_evento) },
                { "cod_tipo_contacto", eCamp.cod_tipo_contacto },
                { "cod_respuesta", eCamp.cod_respuesta },
                { "cod_detalle_respuesta", eCamp.cod_detalle_respuesta },
                { "dsc_observacion", eCamp.dsc_observacion },
                { "cod_expectativa", eCamp.cod_expectativa },
                { "flg_receptivo", eCamp.flg_receptivo },
                { "cod_ejecutivo_cita", eCamp.cod_ejecutivo_cita },
                { "cod_motivo", eCamp.cod_motivo },
                { "cod_evento_ref", eCamp.cod_evento_ref },
                { "cod_usuario", eCamp.cod_usuario },
                { "cod_evento_principal", eCamp.cod_evento_principal },
                { "cod_reconfirmacion", eCamp.cod_reconfirmacion }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public List<T> ListarAsignacionesHistorico<T>(int opcion, string cod_empresa, string cod_proyecto, string cod_prospecto, string cod_usuario) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_prospecto", cod_prospecto },
                {"cod_usuario", cod_usuario }
            };

            myList = sql.ListaconSP<T>("usp_lte_listar_asignacion_prospecto_historico", oDictionary);
            return myList;
        }


    }
}
