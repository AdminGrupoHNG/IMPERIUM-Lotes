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
    public class blProyectos
    {
        readonly daSQL sql;
        public blProyectos(daSQL sql) { this.sql = sql; }

        public void CargaCombosChecked(string nCombo, CheckedComboBoxEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue, string cod_proyecto = "")
        {
            combo.Text = "";
            string procedure = "usp_lte_Consulta_ListarEtapas";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                switch (nCombo)
                {
                    case "Etapa":
                        dictionary.Add("accion", "1");
                        dictionary.Add("cod_etapa", "");
                        dictionary.Add("cod_proyecto", cod_proyecto);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "Graficos":
                        dictionary.Add("accion", "6");
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                }
            }
            catch (Exception generatedExceptionName)
            {
                throw;
            }
        }
        public List<T> ListarConfLotes<T>(string accion, string cod_proyecto, string cod_proyecto_multiple = "", string cod_etapas_multiple = "", string cod_status_multiple = "", string cod_status = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_proyecto", cod_proyecto },
                { "cod_proyecto_multiple", cod_proyecto_multiple },
                { "cod_etapas_multiple", cod_etapas_multiple },
                { "cod_status_multiple", cod_status_multiple },
                { "cod_status", cod_status}
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarLotesProyecto", oDictionary);
            return myList;
        }
        public List<T> ListarResumenSeparacion<T>(string cod_proyecto, DateTime fch_ini, DateTime fch_fin, string cod_status) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_proyecto", cod_proyecto },
                { "fch_ini", fch_ini },
                { "fch_fin", fch_fin },
                { "cod_status", cod_status }

            };

            myList = sql.ListaconSP<T>("usp_lte_ListarChartSeparacion", oDictionary);
            return myList;
        }
        public List<T> ListarSeparaciones<T>(string accion, string cod_proyecto, string cod_etapas_multiple = "", string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "", string cod_estado_separacion = "ALL", string flg_activo = "", string cod_status = "", string cod_separacion = "", string cod_usuario = "", string flg_firmado = "", string filtro_asesor = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapas_multiple", cod_etapas_multiple },
                { "cod_separacion", cod_separacion },
                { "cod_tipo_fecha", cod_tipo_fecha},
                { "FechaInicio", FechaInicio},
                { "FechaFin", FechaFin},
                { "cod_estado_separacion", cod_estado_separacion},
                { "flg_activo", flg_activo},
                { "cod_status", cod_status},
                { "cod_usuario", cod_usuario},
                { "flg_firmado", flg_firmado},
                { "filtro_asesor",  filtro_asesor}
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarSeparacion", oDictionary);
            return myList;
        }
        public List<T> ListarContratos<T>(string accion, string cod_proyecto, string cod_etapas_multiple = "", string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "", string cod_estado_contrato = "ALL", string flg_activo = "", string cod_status = "", string cod_contrato = "", string cod_usuario = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapas_multiple", cod_etapas_multiple },
                { "cod_contrato", cod_contrato },
                { "cod_tipo_fecha", cod_tipo_fecha},
                { "FechaInicio", FechaInicio},
                { "FechaFin", FechaFin},
                { "cod_estado_contrato", cod_estado_contrato},
                { "flg_activo", flg_activo},
                { "cod_status", cod_status},
                { "cod_usuario", cod_usuario}

            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarContrato", oDictionary);
            return myList;
        }

        public DataTable Listar_Contratos_Resumen(string accion, string cod_proyecto, string cod_etapas_multiple = "", string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "", string flg_activo = "", string cod_contrato = "")
        {
            DataTable myList = new DataTable();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapas_multiple", cod_etapas_multiple },
                { "cod_contrato", cod_contrato },
                { "cod_tipo_fecha", cod_tipo_fecha},
                { "FechaInicio", FechaInicio},
                { "FechaFin", FechaFin},
                { "cod_estado_contrato", ""},
                { "flg_activo", flg_activo}


            };

            myList = sql.ListaDatatable("usp_lte_Consulta_ListarContrato", oDictionary);
            return myList;
        }

        public DataTable Listar_Separaciones_Resumen(string accion, string cod_proyecto, string cod_etapas_multiple = "", string cod_tipo_fecha = "", string FechaInicio = "", string FechaFin = "", string flg_activo = "", string cod_separacion = "")
        {
            DataTable myList = new DataTable();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapas_multiple", cod_etapas_multiple },
                { "cod_separacion", cod_separacion },
                { "cod_tipo_fecha", cod_tipo_fecha},
                { "FechaInicio", FechaInicio},
                { "FechaFin", FechaFin},
                { "cod_estado_separacion", ""},
                { "flg_activo", flg_activo}

            };

            myList = sql.ListaDatatable("usp_lte_Consulta_ListarSeparacion", oDictionary);
            return myList;
        }
        public T ObtenerSeparaciones<T>(string accion, string cod_proyecto, string cod_etapas_multiple = "", string cod_separacion = "", string cod_lote = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "accion", accion},
                { "cod_proyecto", cod_proyecto},
                { "cod_etapas_multiple", cod_etapas_multiple },
                { "cod_separacion", cod_separacion },
                 { "cod_tipo_fecha", ""},
                { "FechaInicio", ""},
                { "FechaFin", ""},
                { "cod_estado_separacion", ""},
                { "flg_activo", ""},
                { "cod_status", ""},
                { "cod_lote", cod_lote}
            };

            obj = sql.ConsultarEntidad<T>("usp_lte_Consulta_ListarSeparacion", dictionary);
            return obj;
        }

        public T ObtenerContratos<T>(string accion, string cod_proyecto, string cod_etapas_multiple = "", string cod_contrato = "", string cod_lote = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "accion", accion},
                { "cod_proyecto", cod_proyecto},
                { "cod_etapas_multiple", cod_etapas_multiple },
                { "cod_contrato", cod_contrato },
                { "cod_tipo_fecha", ""},
                { "FechaInicio", ""},
                { "FechaFin", ""},
                { "cod_estado_contrato", ""},
                { "flg_activo", ""},
                { "cod_status", ""},
                { "cod_lote", cod_lote}
            };

            obj = sql.ConsultarEntidad<T>("usp_lte_Consulta_ListarContrato", dictionary);
            return obj;
        }

        public T Mantenimiento_documento_sep<T>(eLotes_Separaciones.eSeparaciones_Documentos eSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_DocumentoSeparacion";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                 { "cod_documento_separacion", eSep.cod_documento_separacion},
                { "dsc_nombre_doc", eSep.dsc_nombre_doc },
                { "dsc_descripcion_doc", eSep.dsc_nombre_doc_ref},
                 //{ "num_orden_doc", eSep.num_orden_doc},
                { "cod_separacion", eSep.cod_separacion},
                { "cod_empresa", eSep.cod_empresa },
                { "cod_proyecto", eSep.cod_proyecto},
                { "flg_activo_doc", eSep.flg_activo_doc},
                { "flg_PDF", eSep.flg_PDF},
                { "idPDF", eSep.idPDF},
                { "cod_usuario_registro", eSep.cod_usuario_registro},
                { "cod_usuario_cambio", eSep.cod_usuario_cambio}



            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
            //}
            //dictionary.Add("accion", NumAccion);

        }

        public T Mantenimiento_documento_contratos<T>(eContratos.eContratos_Documentos eSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_DocumentoContrato";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                 { "cod_documento_contrato", eSep.cod_documento_contrato},
                { "dsc_nombre_doc", eSep.dsc_nombre_doc },
                { "dsc_descripcion_doc", eSep.dsc_nombre_doc_ref},
                 //{ "num_orden_doc", eSep.num_orden_doc},
                { "cod_contrato", eSep.cod_contrato},
                { "cod_empresa", eSep.cod_empresa },
                { "cod_proyecto", eSep.cod_proyecto},
                { "flg_activo_doc", eSep.flg_activo_doc},
                { "flg_PDF", eSep.flg_PDF},
                { "idPDF", eSep.idPDF},
                { "cod_usuario_registro", eSep.cod_usuario_registro},
                { "cod_usuario_cambio", eSep.cod_usuario_cambio}

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
            //}
            //dictionary.Add("accion", NumAccion);

        }



        public T ObtenerDiasSeparaciones<T>(string cod_proyecto) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
               { "cod_proyecto", cod_proyecto}
            };

            obj = sql.ConsultarEntidad<T>("usp_lte_consultar_dias_vencimiento_separacion", dictionary);
            return obj;
        }

        public List<T> ListarProyectos<T>(string NumAccion, string cod_proyecto, string cod_empresa) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", NumAccion },
                { "cod_proyecto", cod_proyecto },
                {"cod_empresa", cod_empresa }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarProyecto", oDictionary);
            return myList;
        }

        public List<T> ListarConfLotesProy<T>(string NumAccion, string cod_proyecto = "", string cod_etapa = "", string cod_manzana = "", string cod_tipo_lote = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", NumAccion },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapa", cod_etapa },
                { "cod_manzana", cod_manzana },
                { "cod_tipo_lote", cod_tipo_lote }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarLotesProyectos", oDictionary);
            return myList;
        }

        public List<T> obtenerConfLotesXManzana<T>(string cod_proyecto = "", string cod_etapa = "", string cod_manzana = "", decimal rango1 = 0, decimal rango2 = 0) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_proyecto", cod_proyecto },
                { "cod_etapa", cod_etapa },
                { "cod_manzana", cod_manzana },
                { "rango1", rango1 },
                { "rango2", rango2 }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consultar_VariasManzana", oDictionary);
            return myList;
        }

        public List<T> obtenerConsultasVariasLotes<T>(int opcion = 0, decimal valor_2 = 0, string valor_4 = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "valor_2", valor_2 }, // opcion 19 imp_precio_venta
                { "valor_4", valor_4 } // opcion 19 cod_proyecto
            };

            myList = sql.ListaconSP<T>("usp_lte_consultasvarias_lotes", oDictionary);
            return myList;
        }
        public List<T> ObtenerListadoProformas<T>(int opcion = 0, string cod_proforma = "", string cod_proyecto = "", string fechaInicio = "", string fechaFin = "", string ejecutivo = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_proforma", cod_proforma },
                { "cod_proyecto", cod_proyecto },
                { "fechaInicio", fechaInicio },
                { "fechaFin", fechaFin },
                { "ejecutivo", ejecutivo }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarProforma", oDictionary);
            return myList;
        }

        public List<T> ListarConfEtapaProyecto<T>(string NumAccion, string cod_etapa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", NumAccion },
                { "cod_etapa", cod_etapa }
            };

            myList = sql.ListaconSP<T>("usp_lte_ConsultaVariasEtapa", oDictionary);
            return myList;
        }

        public List<T> ListarTipoLote<T>(string NumAccion, string cod_variable) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", NumAccion },
                { "cod_variable", cod_variable }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarTipoLote", oDictionary);
            return myList;
        }

        public T Mantenimiento_TipoLote<T>(eVariablesGenerales eVaGe) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_TipoLote";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_variable", eVaGe.cod_variable },
                { "dsc_Nombre", eVaGe.dsc_Nombre },
                { "cod_usuario_registro", eVaGe.cod_usuario_registro },
                { "cod_usuario_cambio", eVaGe.cod_usuario_cambio }
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public T Mantenimiento_Documento<T>(eLotes_Separaciones.eSeparaciones_Documentos eVaGe) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Documento_Separacion";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_variable", eVaGe.cod_documento_separacion },
                { "dsc_Nombre", eVaGe.dsc_nombre_doc_ref },
                { "cod_usuario_registro", eVaGe.cod_usuario_registro },
                { "cod_usuario_cambio", eVaGe.cod_usuario_cambio },
                { "cod_separacion", eVaGe.cod_separacion },
                { "cod_proyecto", eVaGe.cod_proyecto },
                { "flg_PDF", eVaGe.flg_PDF }
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Mantenimiento_Documento_Contrato<T>(eContratos.eContratos_Documentos eVaGe) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Documento_Contrato";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_variable", eVaGe.cod_documento_contrato },
                { "dsc_Nombre", eVaGe.dsc_nombre_doc_ref },
                { "cod_usuario_registro", eVaGe.cod_usuario_registro },
                { "cod_usuario_cambio", eVaGe.cod_usuario_cambio },
                 { "cod_contrato", eVaGe.cod_contrato },
                { "cod_proyecto", eVaGe.cod_proyecto },
                { "flg_PDF", eVaGe.flg_PDF }
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Mantenimiento_Documento_Cliente<T>(eCliente.eCliente_Documentos eVaGe) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Documento_Cliente";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_variable", eVaGe.cod_documento_cliente },
                { "dsc_Nombre", eVaGe.dsc_nombre_doc_ref },
                { "cod_usuario_registro", eVaGe.cod_usuario_registro },
                { "cod_usuario_cambio", eVaGe.cod_usuario_cambio },
                { "cod_cliente", eVaGe.cod_cliente },
                { "flg_PDF", eVaGe.flg_PDF }
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Mantenimiento_Proyecto_Imagenes<T>(eProyecto ePro) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Proyecto_Imagenes";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_imagenes", ePro.cod_imagenes },
                { "cod_empresa", ePro.cod_empresa },
                { "cod_proyecto", ePro.cod_proyecto },
                { "dsc_nombre", ePro.dsc_nombre },
                { "dsc_base64_imagen", ePro.dsc_base64_imagen },
                { "flg_activo", ePro.flg_activo },
                { "cod_usuario_registro", ePro.cod_usuario_registro },
                { "cod_usuario_cambio", ePro.cod_usuario_cambio }
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;

        }


        public T Mantenimiento_Proyecto<T>(eProyecto ePro) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_ProyectoLote";
            //if (NumAccion == "3" || NumAccion == "4" || NumAccion == "5")
            //{
            //    Dictionary<string, object> dictionary = new Dictionary<string, object>()
            //{
            //    { "accion", NumAccion },
            //    { "cod_proyecto", ePro.cod_proyecto }
            //};
            //    obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            //    return obj;
            //}
            //else
            //{
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", ePro.cod_empresa },
                { "cod_proyecto", ePro.cod_proyecto },
                { "dsc_nombre", ePro.dsc_nombre },
                { "dsc_descripcion", ePro.dsc_descripcion },
                { "cod_pais", ePro.cod_pais },
                { "dsc_jefe_proyecto", ePro.dsc_jefe_proyecto },
                { "dsc_arquitecto", ePro.dsc_arquitecto },
                { "cod_moneda", ePro.cod_moneda },
                { "imp_precio_terreno", ePro.imp_precio_terreno },
                { "fch_inicio", ePro.fch_inicio },
                { "fch_termino", ePro.fch_termino },
                { "fch_entrega", ePro.fch_entrega },
                { "flg_activo", ePro.flg_activo },
                { "cod_usuario_registro", ePro.cod_usuario_registro },
                { "cod_usuario_cambio", ePro.cod_usuario_cambio },
                { "imp_alcabala", ePro.imp_alcabala },
                { "imp_otros_gastos", ePro.imp_otros_gastos },
                { "imp_invercion_inicial", ePro.imp_invercion_inicial }
                //{ "@bi_imagen", ePro.bi_imagen }
                //{ "dsc_plan_vias", ePro.dsc_plan_vias }


            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
            //}
            //dictionary.Add("accion", NumAccion);

        }

        public T ObtenerProyecto<T>(string NumAccion, string cod_proyecto) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "accion", NumAccion},
                { "cod_proyecto", cod_proyecto}
            };

            obj = sql.ConsultarEntidad<T>("usp_lte_Consulta_ListarProyecto", dictionary);
            return obj;
        }

        public List<T> ObtenerProyectoImagenes<T>(string NumAccion, string cod_proyecto) where T : class, new()
        {
            List<T> obj = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "accion", NumAccion},
                { "cod_proyecto", cod_proyecto}
            };

            obj = sql.ListaconSP<T>("usp_lte_Consulta_ListarProyecto_imagen", dictionary);
            return obj;
        }

        public T ObtenerProyectoLotes<T>(string NumAccion, string cod_proyecto, string cod_lote) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "accion", NumAccion},
                { "cod_proyecto", cod_proyecto},
                { "cod_proyecto_multiple", cod_lote}
            };

            obj = sql.ConsultarEntidad<T>("usp_lte_Consulta_ListarLotesProyecto", dictionary);
            return obj;
        }


        public List<T> ListarOpcionesMenu<T>(string opcion) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", opcion }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_Proyectos", oDictionary);
            return myList;
        }
        public List<T> ListarTreeMemoriaDes<T>(string cod_proyecto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"cod_proyecto", cod_proyecto }
            };

            myList = sql.ListaconSP<T>("usp_listar_tree_memoria_descriptiva", oDictionary);
            return myList;
        }
        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_uno = "", string cod_dos = "", string cod_tres = "", string cod_cuatro = "", int num_linea = 0, int num_nivel = 0, string cod_condicion = "", bool valorDefecto = false)
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_Proyectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "LoteXmanza":
                        dictionary.Add("accion", cod_condicion);
                        dictionary.Add("cod_proyecto", cod_dos);
                        dictionary.Add("cod_etapa", cod_uno);
                        dictionary.Add("cod_manzana", cod_tres);
                        dictionary.Add("cod_lote", cod_cuatro);
                        procedure = "usp_lte_Consulta_ListarLotesProyectos";
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ManzanaXEtapa":
                        dictionary.Add("accion", cod_condicion);
                        dictionary.Add("cod_etapa", cod_uno);
                        dictionary.Add("cod_variable", "");
                        dictionary.Add("cod_proyecto", cod_dos);
                        dictionary.Add("cod_lote", cod_cuatro);
                        procedure = "usp_lte_Consulta_ListarEtapaManzana";
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoPais":
                        dictionary.Add("accion", 13);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoMoneda":
                        dictionary.Add("accion", 14);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Proyecto":
                        dictionary.Add("accion", 5);
                        procedure = "usp_lte_Consulta_ListarProyecto";
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "ProyectoUnico":
                        dictionary.Add("accion", 6);
                        procedure = "usp_lte_Consulta_ListarProyecto";
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Cuotas":
                        dictionary.Add("accion", 6);
                        dictionary.Add("cod_usuario", "");
                        dictionary.Add("cod_empresa", "");
                        dictionary.Add("cod_proyecto", cod_uno);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoContrato":
                        procedure = "usp_lte_Consulta_ListarTipoContrato";
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoLoteEtapa":
                        procedure = "usp_lte_Consulta_ListarEtapaLote";
                        dictionary.Add("accion", "2");
                        dictionary.Add("cod_etapa", cod_uno);
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

        public void CargaCombosLookUpManzana(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_cliente = "", string cod_nivel = "", int num_linea = 0, int num_nivel = 0, string cod_condicion = "", bool valorDefecto = false)
        {
            combo.Text = "";
            string procedure = "usp_lte_Consulta_ListarManzana";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {

                    case "Desde":
                        dictionary.Add("accion", 3);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Hasta":
                        dictionary.Add("accion", 3);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                }

                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
                if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
                if (campoValueMember == "cod_variable")
                {
                    DataRow filaTabla = tabla.Rows[0];
                    combo.EditValue = filaTabla[0];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable ObtenerListadoGridLookup(string nCombo, string cod_usuario = "", string cod_proyecto = "", string codigo = "", string codigoMultiple = "")
        {
            string procedure = "usp_ConsultasVarias_Proyectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {

                    case "EmpresasUsuarios":
                        dictionary.Add("accion", 11);
                        dictionary.Add("cod_usuario", cod_usuario);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break; sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoDocumento":

                        dictionary.Add("accion", 17);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EtapaXProyecto":
                        procedure = "usp_lte_Consulta_ListarEtapas";
                        dictionary.Add("accion", "1");
                        dictionary.Add("cod_etapa", "");
                        dictionary.Add("cod_proyecto", cod_proyecto);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "EtapasFiltroProyecto":
                        procedure = "usp_lte_Consulta_ListarEtapas";
                        dictionary.Add("accion", "5");
                        dictionary.Add("cod_etapa", "");
                        dictionary.Add("cod_proyecto", codigo);
                        dictionary.Add("cod_etapa_multiple", codigoMultiple);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                }
                return tabla;
            }
            catch (Exception ex)
            {
                return new DataTable();
                throw;
            }
        }

        public T ObtenerListadoEmpresaSeleccionada<T>(string nCombo, string cod_usuario = "", string cod_empresa = "", string cod_proyecto = "", string dsc_telefono_movil = "") where T : class, new()
        {
            T obj = new T();
            string procedure = "usp_ConsultasVarias_Proyectos";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", nCombo},
                { "cod_usuario", cod_usuario},
                { "cod_empresa", cod_empresa},
                { "cod_proyecto", cod_proyecto},
                { "dsc_telefono_movil", dsc_telefono_movil}
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;


        }
        public T MantenimientoProyectoEtapa<T>(eProyecto_Etapa eProEta) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_ProyectoEtapa";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_etapa", eProEta.cod_etapa },
                { "cod_empresa", eProEta.cod_empresa },
                { "cod_proyecto", eProEta.cod_proyecto },
                { "dsc_descripcion", eProEta.dsc_descripcion },
                { "num_area_uex", eProEta.num_area_uex },
                { "num_area_uco", eProEta.num_area_uco },
                { "cod_usuario_registro", eProEta.cod_usuario_registro },
                { "cod_usuario_cambio", eProEta.cod_usuario_cambio }

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T MantenimientoMemoriaDescriptiva<T>(eMemoDes eMemoria) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Actualizar_Insertar_Memoria_Descriptiva";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_memoria_desc", eMemoria.cod_memoria_desc },
                { "dsc_nombre", eMemoria.dsc_nombre },
                { "dsc_descripcion", eMemoria.dsc_descripcion },
                { "num_orden", eMemoria.num_orden },
                { "cod_empresa", eMemoria.cod_empresa },
                { "cod_proyecto", eMemoria.cod_proyecto },
                { "cod_usuario_registro", eMemoria.cod_usuario_registro },
                { "cod_usuario_cambio", eMemoria.cod_usuario_cambio },
                { "dsc_descripcion_html", eMemoria.@dsc_descripcion_html }


            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T MantenimientoMemoriaDescriptivaOrdenNombre<T>(eMemoDes eMemoria) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Actualizar_Insertar_NOMBRE_Y_ORDEN_Memoria";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_memoria_desc", eMemoria.cod_memoria_desc },
                { "dsc_nombre", eMemoria.dsc_nombre },
                { "num_orden", eMemoria.num_orden },
                { "cod_empresa", eMemoria.cod_empresa },
                { "cod_proyecto", eMemoria.cod_proyecto },
                { "cod_usuario_registro", eMemoria.cod_usuario_registro },
                { "cod_usuario_cambio", eMemoria.cod_usuario_cambio }


            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T ExtencionSeparacion<T>(string accion, eLotes_Separaciones eProSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Tipo_Separacion";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_tipo_separacion", eProSep.cod_tipo_separacion },
                { "cod_separacion", eProSep.cod_separacion },
                { "cod_proyecto", eProSep.cod_proyecto },
                { "cod_empresa", eProSep.cod_empresa },
                { "cod_cliente", eProSep.cod_cliente },
                { "cod_lote", eProSep.cod_lote },
                { "cod_etapa", eProSep.cod_etapa },
                { "cod_usuario_registro", eProSep.cod_usuario_cambio }
            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;


        }


        public T MantenimientoSeparaciones<T>(eLotes_Separaciones eProSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Separacion";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_separacion", eProSep.cod_separacion },
                { "cod_asesor", eProSep.cod_asesor },
                { "cod_estado_separacion", eProSep.cod_estado_separacion },
                { "cod_lote", eProSep.cod_lote },
                { "cod_etapa", eProSep.cod_etapa },
                { "cod_empresa", eProSep.cod_empresa },
                { "cod_proyecto", eProSep.cod_proyecto },
                { "cod_manzana", eProSep.cod_manzana },
                { "num_area_uex", eProSep.num_area_uex },
                { "cod_forma_pago", eProSep.cod_forma_pago },
                { "imp_precio_total", eProSep.imp_precio_total },
                { "prc_descuento", eProSep.prc_descuento },
                { "imp_descuento", eProSep.imp_descuento },
                { "imp_precio_final", eProSep.imp_precio_final },
                { "imp_separacion", eProSep.imp_separacion },
                { "imp_cuota_inicial", eProSep.imp_cuota_inicial },
                { "num_cuotas", eProSep.num_cuotas },
                { "num_fraccion", eProSep.num_fraccion },
                { "imp_valor_cuota", eProSep.imp_valor_cuota },
                { "flg_activo", eProSep.flg_activo },
                { "cod_cliente", eProSep.cod_cliente },
                { "cod_prospecto", eProSep.cod_prospecto },
                { "flg_prospecto", eProSep.flg_prospecto },
                { "flg_cliente", eProSep.flg_cliente },
                { "cod_usuario_registro", eProSep.cod_usuario_cambio },
                { "cod_usuario_cambio", eProSep.cod_usuario_cambio }

            };
            if (eProSep.fch_vct_cuota.ToString().Contains("1/01/0001")) { dictionary.Add("fch_vct_cuota", DBNull.Value); } else { dictionary.Add("fch_vct_cuota", eProSep.fch_vct_cuota); }
            if (eProSep.fch_vct_separacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_vct_separacion", DBNull.Value); } else { dictionary.Add("fch_vct_separacion", eProSep.fch_vct_separacion); }
            if (eProSep.fch_pago_separacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_separacion", DBNull.Value); } else { dictionary.Add("fch_pago_separacion", eProSep.fch_pago_separacion); }
            if (eProSep.fch_pago_cuota.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_cuota", DBNull.Value); } else { dictionary.Add("fch_pago_cuota", eProSep.fch_pago_cuota); }
            if (eProSep.fch_pago_total.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_total", DBNull.Value); } else { dictionary.Add("fch_pago_total", eProSep.fch_pago_total); }
            if (eProSep.fch_Separacion.ToString().Contains("1/01/0001")) { dictionary.Add("fch_Separacion", DBNull.Value); } else { dictionary.Add("fch_Separacion", eProSep.fch_Separacion); }
            dictionary.Add("cod_cuotas", eProSep.cod_cuotas);
            dictionary.Add("flg_es_extension", eProSep.flg_es_extension);
            dictionary.Add("cod_separacion_padre", eProSep.cod_separacion_padre);
            dictionary.Add("idCarpeta_separacion", eProSep.idCarpeta_separacion);
            dictionary.Add("cod_copropietario", eProSep.cod_copropietario);
            dictionary.Add("imp_precio_con_descuento", eProSep.imp_precio_con_descuento);
            dictionary.Add("imp_pendiente_pago", eProSep.imp_pendiente_pago);
            dictionary.Add("dsc_linea_contacto", eProSep.dsc_linea_contacto);
            dictionary.Add("dsc_linea_contacto_copro", eProSep.dsc_linea_contacto_copro);
            dictionary.Add("prc_interes", eProSep.prc_interes);
            dictionary.Add("imp_interes", eProSep.imp_interes);

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;


        }

        public T MantenimientoContratosAdenda<T>(string accion, eContratos.eContratos_Adenda_Financiamiento eProSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Adenda";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_contrato", eProSep.cod_contrato },
                { "cod_proyecto", eProSep.cod_proyecto },
                { "cod_empresa", eProSep.cod_empresa },
                { "num_adenda", eProSep.num_adenda },
                { "cod_tipo_adenda", eProSep.cod_tipo_adenda  },
                { "cod_cliente", eProSep.cod_cliente },
                { "cod_lote", eProSep.cod_lote },
                { "cod_etapa", eProSep.cod_etapa },
                { "cod_usuario_registro", eProSep.cod_usuario_cambio },
                };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T MantenimientoContratosFinanciamiento<T>(string accion, eContratos.eContratos_Adenda_Financiamiento eProSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_DetalleFi";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_contrato", eProSep.cod_contrato },
                { "cod_proyecto", eProSep.cod_proyecto },
                { "cod_empresa", eProSep.cod_empresa },
                { "num_adenda", eProSep.num_adenda },
                { "num_financiamiento", eProSep.num_financiamiento },
                { "cod_tipo_financiamiento", eProSep.cod_tipo_financiamiento  },
                { "cod_cuotas", eProSep.cod_cuotas },
                { "num_fraccion", eProSep.num_fraccion },
                { "imp_saldo_financiar", eProSep.imp_saldo_financiar },
                { "cod_usuario_registro", eProSep.cod_usuario_registro },
                };
            if (eProSep.fch_pago_cuota.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_cuota", DBNull.Value); } else { dictionary.Add("fch_pago_cuota", eProSep.fch_pago_cuota); }

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }
        public T MantenimientoContratosDetalleCuotas<T>(string accion, eContratos.eContratos_Adenda_Financiamiento eProSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Detalle_CUOTAs";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_contrato", eProSep.cod_contrato },
                { "cod_proyecto", eProSep.cod_proyecto },
                { "cod_empresa", eProSep.cod_empresa },
                { "num_financiamiento", eProSep.num_financiamiento },
                { "num_cuota", eProSep.num_cuota  },
                { "imp_cuotas", eProSep.imp_cuotas },
                { "cod_usuario_registro", eProSep.cod_usuario_registro },
                { "cod_usuario_cambio", eProSep.cod_usuario_cambio },
                { "num_orden_det_cuo", eProSep.num_orden_det_cuo },
                { "dsc_cuota", eProSep.dsc_cuota },
                { "imp_cuo_sin_interes", eProSep.imp_cuo_sin_interes },
                { "imp_interes", eProSep.imp_interes }
                };
            if (eProSep.fch_vct_cuota.ToString().Contains("1/01/0001")) { dictionary.Add("fch_vct_cuota", DBNull.Value); } else { dictionary.Add("fch_vct_cuota", eProSep.fch_vct_cuota); }

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T MantenimientoContratos<T>(eContratos eProSep) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Contrato";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_contrato", eProSep.cod_contrato },
                { "cod_separacion", eProSep.cod_separacion },
                { "cod_tipo_contrato", eProSep.cod_tipo_contrato  },
                { "cod_asesor", eProSep.cod_asesor },
                { "cod_cliente", eProSep.cod_cliente },
                { "cod_copropietario", eProSep.cod_copropietario },
                { "dsc_linea_contacto", eProSep.dsc_linea_contacto },
                { "dsc_linea_contacto_copro", eProSep.dsc_linea_contacto_copro },
                { "cod_etapa", eProSep.cod_etapa },
                { "cod_manzana", eProSep.cod_manzana },
                { "cod_lote", eProSep.cod_lote },
                { "num_area_uex", eProSep.num_area_uex },
                { "cod_forma_pago", eProSep.cod_forma_pago },
                { "imp_precio_lista", eProSep.imp_precio_lista },
                { "prc_descuento", eProSep.prc_descuento },
                { "imp_descuento", eProSep.imp_descuento },
                { "imp_precio_venta_final", eProSep.imp_precio_venta_final },
                //{ "imp_separacion", eProSep.imp_separacion },
                { "cod_empresa", eProSep.cod_empresa },
                { "cod_proyecto", eProSep.cod_proyecto },
                { "imp_cuota_inicial", eProSep.imp_cuota_inicial },
                { "imp_saldo_financiar", eProSep.imp_saldo_financiar },
                { "cod_cuotas", eProSep.cod_cuotas },
                { "num_fraccion", eProSep.num_fraccion },
                { "imp_valor_cuota", eProSep.imp_valor_cuota },
                //{ "imp_pendiente_pago", eProSep.imp_pendiente_pago },
                { "cod_usuario_registro", eProSep.cod_usuario_cambio },
                { "cod_usuario_cambio", eProSep.cod_usuario_cambio },
                { "idCarpeta_contrato", eProSep.idCarpeta_contrato }
                            };
            if (eProSep.fch_emitido.ToString().Contains("1/01/0001")) { dictionary.Add("fch_emitido", DBNull.Value); } else { dictionary.Add("fch_emitido", eProSep.fch_emitido); }
            if (eProSep.fch_vct_cuota.ToString().Contains("1/01/0001")) { dictionary.Add("fch_vct_cuota", DBNull.Value); } else { dictionary.Add("fch_vct_cuota", eProSep.fch_vct_cuota); }
            if (eProSep.fch_pago_cuota.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_cuota", DBNull.Value); } else { dictionary.Add("fch_pago_cuota", eProSep.fch_pago_cuota); }
            if (eProSep.fch_pago_contado.ToString().Contains("1/01/0001")) { dictionary.Add("fch_pago_contado", DBNull.Value); } else { dictionary.Add("fch_pago_contado", eProSep.fch_pago_contado); }
            dictionary.Add("prc_interes", eProSep.prc_interes);
            dictionary.Add("imp_interes", eProSep.imp_interes);

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T ObtenerDatosEmpresa<T>(int opcion, string cod_empresa, string dsc_ruc = "", string cod_tipo_gasto = "", string cod_und_negocio = "") where T : class, new()
        {
            T myList = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_empresa", cod_empresa);
            dictionary.Add("dsc_ruc", dsc_ruc);
            dictionary.Add("cod_tipo_gasto", cod_tipo_gasto);
            dictionary.Add("cod_und_negocio", cod_und_negocio);

            myList = sql.ConsultarEntidad<T>("usp_ConsultasVarias_FacturasProveedor", dictionary);
            return myList;
        }

        public T MantenimientoMedidasProyecto<T>(eProyecto eProMedidas) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_Proyecto_Medidas";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eProMedidas.cod_empresa },
                { "cod_proyecto", eProMedidas.cod_proyecto },
                { "cod_usuario_cambio", eProMedidas.cod_usuario_cambio }

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;


        }

        public T MantenimientoLotesXProspecto<T>(eProspectosXLote eLoPro) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_ProspectoLote";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_lote", eLoPro.cod_lote },
                { "cod_empresa", eLoPro.cod_empresa },
                { "cod_proyecto", eLoPro.cod_proyecto },
                { "cod_prospecto", eLoPro.cod_prospecto },
                { "cod_etapa", eLoPro.cod_etapa },
                { "cod_usuario_registro", eLoPro.cod_usuario_registro }

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;

        }

        public T MantenimientoLotesXProyecto<T>(string accion, eLotesxProyecto eLoPro) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_LoteSXProyecto";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion},
                { "cod_manzana", eLoPro.cod_manzana },
                { "cod_etapa", eLoPro.cod_etapa },
                { "cod_empresa", eLoPro.cod_empresa },
                { "cod_proyecto", eLoPro.cod_proyecto },
                { "dsc_manzana", eLoPro.dsc_manzana },
                { "dsc_lote", eLoPro.dsc_lote },
                { "num_etapa", eLoPro.num_etapa },
                { "num_lote", eLoPro.num_lote },
                { "cod_usuario_registro", eLoPro.cod_usuario_registro },
                { "cod_tipo_lote", eLoPro.cod_tipo_lote },
                { "imp_precio_m_cuadrado", eLoPro.imp_precio_m_cuadrado },
                { "cod_lote", eLoPro.cod_lote },
                { "cod_status", eLoPro.cod_status },
                { "num_area_uex", eLoPro.num_area_uex },
                { "num_area_uco", eLoPro.num_area_uco },
                { "prc_uso_exclusivo", eLoPro.prc_uso_exclusivo },
                { "prc_uso_exclusivo_part_mat", eLoPro.prc_uso_exclusivo_part_mat },
                { "prc_uso_comun_part_mat", eLoPro.prc_uso_comun_part_mat },
                { "num_frente", eLoPro.num_frente },
                { "num_derecha", eLoPro.num_derecha },
                { "num_izquierda", eLoPro.num_izquierda },
                { "num_fondo", eLoPro.num_fondo },
                 { "imp_precio_total", eLoPro.imp_precio_total },
                 { "cod_usuario_cambio", eLoPro.cod_usuario_cambio },
                 { "dsc_descripcion_html", eLoPro.dsc_descripcion_html },
                 { "dsc_descripcion_word", eLoPro.dsc_descripcion_word }

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;

        }

        public T AsignarCostosLotes<T>(string cod_etapa, string cod_proyecto) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Calcular_Costos_LoteSXProyecto";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_etapa", cod_etapa },
                { "cod_proyecto", cod_proyecto }

            };
            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;

        }

        public List<T> ListarManzana<T>(string NumAccion, string rango1, string rango2, string selecciones = "", string dsc_Nombre = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", NumAccion },
                { "rango1", rango1 },
                {"rango2", rango2 },
                { "selecciones", selecciones },
                {"dsc_Nombre", dsc_Nombre }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarManzana", oDictionary);
            return myList;
        }
        public List<T> ListarEtapa<T>(string NumAccion, string cod_etapa, string cod_proyecto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", NumAccion },
                { "cod_etapa", cod_etapa },
                {"cod_proyecto", cod_proyecto }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarEtapas", oDictionary);
            return myList;
        }
        public List<T> ListarSeparacionSemana<T>(string cod_proyecto, DateTime fch_ini, DateTime fch_fin, int num_mes, string cod_status) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"cod_proyecto", cod_proyecto },
                {"fch_ini", fch_ini },
                { "fch_fin", fch_fin },
                {"num_mes", num_mes },
                { "cod_status", cod_status }
            };

            myList = sql.ListaconSP<T>("usp_lte_ListarMesSeparaciones", oDictionary);
            return myList;
        }
        public List<T> ListarMemoriaDesc<T>(string accion, string cod_proyecto, string cod_memoria_desc) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", accion },
                { "cod_proyecto", cod_proyecto },
                {"cod_memoria_desc", cod_memoria_desc }
            };

            myList = sql.ListaconSP<T>("usp_listar_memoria_descriptiva", oDictionary);
            return myList;
        }

        public List<T> ListarvalidarSeparacion<T>(int opcion, string cod_cliente, int num_linea_contacto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_cliente", cod_cliente },
                {"num_linea_contacto", num_linea_contacto }
            };

            myList = sql.ListaconSP<T>("usp_Consulta_ListarContactosCliente", oDictionary);
            return myList;
        }

        public List<T> ListarDocumentoSeparacion<T>(string accion, string cod_separacion, string cod_proyecto, string cod_documento_separacion = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", accion },
                { "cod_separacion", cod_separacion },
                {"cod_proyecto", cod_proyecto },
                {"cod_documento_separacion", cod_documento_separacion }
            };

            myList = sql.ListaconSP<T>("usp_listar_Documentos_Separaciones", oDictionary);
            return myList;
        }

        public List<T> ListarDocumentoContratos<T>(string accion, string cod_contrato, string cod_proyecto, string cod_documento_contrato = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", accion },
                { "cod_contrato", cod_contrato },
                {"cod_proyecto", cod_proyecto },
                {"cod_documento_contrato", cod_documento_contrato }
            };

            myList = sql.ListaconSP<T>("usp_listar_Documentos_Contratos", oDictionary);
            return myList;
        }

        public List<T> ListarTipoSeparacion<T>(string accion, string cod_cliente = "", string cod_proyecto = "", string cod_lote = "", string cod_separacion = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", accion },
                {"cod_cliente", cod_cliente },
                {"cod_proyecto", cod_proyecto },
                {"cod_lote", cod_lote },
                {"cod_separacion", cod_separacion }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarTipoSeparacion", oDictionary);
            return myList;
        }

        public T Guardar_Actualizar_EtapasTipoLote<T>(eVariablesGenerales eEtaLo) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_EtapaLote";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_etapa", eEtaLo.cod_etapa },
                { "cod_empresa", eEtaLo.cod_empresa },
                { "cod_proyecto", eEtaLo.cod_proyecto },
                { "cod_variable", eEtaLo.cod_variable },
                { "flg_activo", eEtaLo.flg_activo },
                //{ "imp_prec_m_cuadrado", eEtaLo.imp_prec_m_cuadrado },
                { "cod_usuario_registro", eEtaLo.cod_usuario_registro },
                { "cod_usuario_cambio", eEtaLo.cod_usuario_cambio }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }



        public T Guardar_Actualizar_ClienteProyecto<T>(eCliente eEtaLo) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_ListarProyectosCliente";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", eEtaLo.cod_cliente },
                { "cod_empresa", eEtaLo.cod_empresa },
                { "cod_proyecto", eEtaLo.cod_proyecto },
                { "flg_activo", eEtaLo.flg_activo },
                //{ "imp_prec_m_cuadrado", eEtaLo.imp_prec_m_cuadrado },
                { "cod_usuario_registro", eEtaLo.cod_usuario_registro },
                { "cod_usuario_cambio", eEtaLo.cod_usuario_cambio }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Guardar_Actualizar_Proforma<T>(eProformas eProformas, int opcion = 0) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "ups_lte_insertar_actualizar_eliminar_proformas";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_proforma", eProformas.cod_proforma },
                { "cod_empresa", eProformas.cod_empresa },
                { "cod_proyecto", eProformas.cod_proyecto },
                { "cod_lote", eProformas.cod_lote },
                { "fch_proforma", eProformas.fch_proforma },
                { "cod_cliente", eProformas.cod_cliente },
                { "cod_ejecutivo", eProformas.cod_ejecutivo },
                { "dsc_nombre", eProformas.dsc_nombre },
                { "dsc_apellido_paterno", eProformas.dsc_apellido_paterno},
                { "dsc_apellido_materno", eProformas.dsc_apellido_materno },
                { "dsc_telefono", eProformas.dsc_telefono },
                { "cod_tipo_documento", eProformas.cod_tipo_documento },
                { "dsc_documento", eProformas.dsc_documento },
                { "cod_estado_civil", eProformas.cod_estado_civil },
                { "dsc_email", eProformas.dsc_email },
                { "dsc_observaciones", eProformas.dsc_observaciones },
                { "cod_estado", eProformas.cod_estado },
                { "cod_referencia", eProformas.cod_referencia },
                { "cod_variable", eProformas.cod_variable },
                { "cod_usuario", eProformas.cod_usuario }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Guardar_Actualizar_Proforma_Detalle<T>(eProformas.eProformas_Detalle eProformas, int opcion = 0) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "ups_lte_insertar_actualizar_eliminar_proformas_detalle";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_proforma", eProformas.cod_proforma },
                { "cod_empresa", eProformas.cod_empresa},
                { "cod_proyecto", eProformas.cod_proyecto},
                { "cod_variable", eProformas.cod_variable},
                { "cod_referencia", eProformas.cod_referencia},
                { "imp_precio_venta", eProformas.imp_precio_venta},
                { "prc_descuento", eProformas.prc_descuento},
                { "imp_descuento", eProformas.imp_descuento},
                { "imp_precio_final", eProformas.imp_precio_final},
                { "imp_separacion", eProformas.imp_separacion},
                { "imp_cuota_inicial", eProformas.imp_cuota_inicial},
                { "prc_interes", eProformas.prc_interes},
                { "imp_interes", eProformas.imp_interes},
                { "num_fraccion", eProformas.num_fraccion},
                { "imp_valor_cuota", eProformas.imp_valor_cuota},
                { "flg_seleccion", eProformas.flg_seleccion},
                { "cod_usuario", eProformas.cod_usuario},
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Guardar_Actualizar_prospecto<T>(eCampanha eCamp, string MiAccion) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_lte_insertar_actualizar_eliminar_prospectos";

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", MiAccion},
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
                { "fch_fec_nac", Convert.ToDateTime(eCamp.fch_fec_nac) },
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
                { "cod_referencia_campanha", eCamp.cod_referencia_campanha },
                { "cod_usuario", eCamp.cod_usuario }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public T Guardar_Actualizar_EtapasManzana<T>(eProyectoEtapaManzana eEtaMz) where T : class, new()
        {
            T obj = new T();
            string procedure = "";
            procedure = "usp_Insertar_Actualizar_EtapaManzana";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_etapa", eEtaMz.cod_etapa },
                { "cod_empresa", eEtaMz.cod_empresa },
                { "cod_proyecto", eEtaMz.cod_proyecto },
                { "cod_manzana", eEtaMz.cod_manzana },
                { "dsc_manzana", eEtaMz.dsc_manzana },
                { "num_desde", eEtaMz.num_desde },
                { "num_hasta", eEtaMz.num_hasta },
                { "cod_usuario_registro", eEtaMz.cod_usuario_registro },
                { "cod_usuario_cambio", eEtaMz.cod_usuario_cambio }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public List<T> ListarTipoLotexEtapas<T>(string accion, string cod_etapa, string cod_proyecto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", accion },
                { "cod_etapa", cod_etapa},
                { "cod_proyecto", cod_proyecto}
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarEtapaLote", oDictionary);
            return myList;
        }
        public List<T> ListarProyectoxCliente<T>(string accion, string cod_cliente) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", accion },
                { "cod_cliente", cod_cliente}
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarLotesAsignados", oDictionary);
            return myList;
        }

        public List<T> ListarManzanaxEtapas<T>(string accion, string cod_etapa, string cod_variable = "", string cod_proyecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"accion", accion },
                { "cod_etapa", cod_etapa},
                {"cod_variable", cod_variable },
                { "cod_proyecto", cod_proyecto}
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarEtapaManzana", oDictionary);
            return myList;
        }

        public T ValidacionEliminar<T>(string accion, string cod_etapa = "", string cod_manzana = "", string cod_proyecto = "") where T : class, new()
        {
            T obj = new T();
            string procedure = "usp_lte_Eliminar_Manzana";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_etapa", cod_etapa },
                { "cod_manzana", cod_manzana },
                { "cod_proyecto", cod_proyecto }
            };

            obj = sql.ConsultarEntidad<T>(procedure, dictionary);
            return obj;
        }

        public string Eliminar_EtapaManzana(string accion, string cod_variable, string cod_proyecto, string cod_etapa, string cod_manzana, string rango1, string rango2)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_variable", cod_variable },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapa", cod_etapa },
                { "cod_manzana", cod_manzana },
                { "rango1", rango1 },
                { "rango2", rango2 }
            };

            result = sql.ExecuteScalarWithParams("usp_lte_Eliminar_EtapaManzana", dictionary);
            return result;
        }

        public string Eliminar_Memoria_Desc(string cod_memoria_desc, string cod_proyecto)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_memoria_desc", cod_memoria_desc },
                { "cod_proyecto", cod_proyecto }
            };

            result = sql.ExecuteScalarWithParams("usp_lte_Eliminar_Memoria_Desc", dictionary);
            return result;
        }

        public string Eliminar_Configuracion_Proyecto(string accion, string cod_lote, string cod_proyecto, string cod_etapa, string cod_manzana, string cod_usuario_cambio)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_lote", cod_lote },
                { "cod_etapa", cod_etapa },
                { "cod_manzana", cod_manzana },
                { "cod_proyecto", cod_proyecto },
                { "cod_usuario_cambio", cod_usuario_cambio }
            };
            result = sql.ExecuteScalarWithParams("usp_lte_Eliminar_Configuracion_Proyecto", dictionary);
            return result;
        }

        public string Eliminar_EtapaTipoLote(string cod_variable, string cod_proyecto, string cod_etapa)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_variable", cod_variable },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapa", cod_etapa }
            };

            result = sql.ExecuteScalarWithParams("usp_lte_EliminarTipoLote", dictionary);
            return result;
        }
        public string Eliminar_Contacto(string cod_cliente = "", int num_linea_contacto = 0)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_cliente", cod_cliente },
                { "num_linea_contacto", num_linea_contacto }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_ClienteContactos", dictionary);
            return result;
        }
        public string Eliminar_TipoDocumento(string accion, string cod_documento_referencia = "", string valor1 = "", string valor2 = "", string flg_PDF = "")
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_documento_referencia", cod_documento_referencia },
                { "valor1", valor1 },
                { "valor2", valor2 },
                { "flg_PDF", flg_PDF }
            };

            result = sql.ExecuteScalarWithParams("usp_lte_EliminarTipoDocumento", dictionary);
            return result;
        }



        public string Eliminar_EtapaProyecto(string cod_proyecto, string cod_etapa)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_etapa", cod_etapa },
                { "cod_proyecto", cod_proyecto }

            };

            result = sql.ExecuteScalarWithParams("usp_lte_Eliminar_EtapaProyecto", dictionary);
            return result;
        }
        public string Liberar_ProspectoLote(string cod_lote, string cod_prospecto)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_lote", cod_lote },
                { "cod_prospecto", cod_prospecto }

            };

            result = sql.ExecuteScalarWithParams("usp_lte_Liberar_Prospecto", dictionary);
            return result;
        }
        public string Actualizar_Status_Separacion(string accion, string cod_separacion, string cod_proyecto, string cod_etapa, string cod_lote, string cod_usuario_cambio)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_separacion", cod_separacion },
                { "cod_proyecto", cod_proyecto },
                { "cod_etapa", cod_etapa },
                { "cod_lote", cod_lote },
                { "cod_usuario_cambio", cod_usuario_cambio }

            };

            result = sql.ExecuteScalarWithParams("usp_lte_Desistir_Separacion", dictionary);
            return result;
        }

        public string Actualizar_Status_Contrato(string accion, string cod_contrato, string cod_proyecto, string cod_separacion = "", DateTime fch_firma = new DateTime())
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_contrato", cod_contrato },
                { "cod_proyecto", cod_proyecto },
                { "cod_separacion", cod_separacion },
                { "fch_firma", fch_firma }

            };
            result = sql.ExecuteScalarWithParams("usp_lte_Actualizar_Estado_Contrato", dictionary);
            return result;
        }

        public string Actualizar_Lote_Contrato(string cod_lote, string cod_etapa, string cod_proyecto, string cod_usuario_cambio)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_lote", cod_lote },
                { "cod_etapa", cod_etapa },
                { "cod_proyecto", cod_proyecto },
                { "cod_usuario_cambio", cod_usuario_cambio }

            };

            result = sql.ExecuteScalarWithParams("usp_Insertar_Actualizar_Lote_Contrato", dictionary);
            return result;
        }

        public string Actualizar_Seguimiento_Separacion(string accion, string cod_separacion, string cod_proyecto, string cod_usuario_cambio)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_separacion", cod_separacion },
                { "cod_proyecto", cod_proyecto },
                { "cod_usuario_cambio", cod_usuario_cambio }

            };

            result = sql.ExecuteScalarWithParams("usp_Actualizar_Seguimiento_Separacion", dictionary);
            return result;
        }

        public List<T> CombosEnGridControl<T>(string nCombo, string dato = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            string procedure = "usp_lte_Consulta_ListarStatus";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //dictionary.Add("dato", dato);

            switch (nCombo)
            {

                case "Status":
                    dictionary.Add("accion", "1");
                    break;
                case "TipoLoteEtapa":
                    procedure = "usp_lte_Consulta_ListarEtapaLote";
                    dictionary.Add("accion", "2");
                    dictionary.Add("cod_etapa", dato);
                    break;
                case "TipoLotes":
                    procedure = "usp_lte_Consulta_ListarTipoLote";
                    dictionary.Add("accion", "1");
                    break;
                case "Proyecto":
                    procedure = "usp_lte_Consulta_ListarProyecto";
                    dictionary.Add("accion", "1");
                    break;
            }

            myList = sql.ListaconSP<T>(procedure, dictionary);
            return myList;
        }

        public List<T> ListarLotesProspectos<T>(int accion, string cod_proyecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", accion },
                {"cod_proyecto", cod_proyecto }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarProspectosProceso", oDictionary);
            //myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarLotesProspecto", oDictionary);
            return myList;
        }

        public List<T> ListarProspecto<T>(int accion, string cod_empresa = "", string cod_proyecto = "", string cod_prospecto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", accion },
                {"cod_empresa", cod_empresa },
                {"cod_proyecto", cod_proyecto },
                {"cod_prospecto", cod_prospecto }
            };

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarProspectosProceso", oDictionary);
            return myList;
        }

        public List<T> ListarLotesProspectosAsignados<T>() where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>();

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarProspectosAgrupados", oDictionary);
            return myList;
        }

        public List<T> Obtener_LineasDetalleSeparacion<T>(int opcion, string cod_separacion, string cod_proyecto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_separacion", cod_separacion);
            dictionary.Add("cod_proyecto", cod_proyecto);

            myList = sql.ListaconSP<T>("usp_Consulta_ListarObservacionSeparacion", dictionary);
            return myList;
        }

        public List<T> Obtener_LineasDetalleContrato<T>(int opcion, string cod_contrato, string cod_proyecto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("opcion", opcion);
            dictionary.Add("cod_contrato", cod_contrato);
            dictionary.Add("cod_proyecto", cod_proyecto);

            myList = sql.ListaconSP<T>("usp_Consulta_ListarObservacionContrato", dictionary);
            return myList;
        }

        public List<T> ObtenerListadoAdenda<T>(string cod_contrato, string cod_proyecto) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("cod_proyecto", cod_proyecto);
            dictionary.Add("cod_contrato", cod_contrato);

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarAdendas", dictionary);
            return myList;
        }
        public List<T> ObtenerListadoFinanciamiento<T>(string cod_contrato, string cod_proyecto, int num_adenda) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("cod_proyecto", cod_proyecto);
            dictionary.Add("cod_contrato", cod_contrato);
            dictionary.Add("num_adenda", num_adenda);

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarFinanciamiento", dictionary);
            return myList;
        }

        public List<T> ObtenerListadoDetalleCuotas<T>(string accion, string cod_contrato, string cod_proyecto, int num_financiamiento) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("accion", accion);
            dictionary.Add("cod_proyecto", cod_proyecto);
            dictionary.Add("cod_contrato", cod_contrato);
            dictionary.Add("num_financiamiento", num_financiamiento);

            myList = sql.ListaconSP<T>("usp_lte_Consulta_ListarDetalle_Cuota", dictionary);
            return myList;
        }

        public T InsertarObservacionesSeparacion<T>(eLotes_Separaciones.eSeparaciones_Observaciones eSep) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_separacion", eSep.cod_separacion },
                { "cod_proyecto", eSep.cod_proyecto },
                { "cod_empresa", eSep.cod_empresa },
                { "num_linea", eSep.num_linea },
                { "dsc_observacion", eSep.dsc_observaciones },
                { "cod_usuario_registro", eSep.cod_usuario_registro }
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ObservacionesSeparacion", dictionary);
            return obj;
        }

        public T InsertarObservacionesContratos<T>(eContratos.eContratos_Observaciones eSep) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_contrato", eSep.cod_contrato },
                { "cod_proyecto", eSep.cod_proyecto },
                { "cod_empresa", eSep.cod_empresa },
                { "num_linea", eSep.num_linea },
                { "dsc_observacion", eSep.dsc_observaciones },
                { "cod_usuario_registro", eSep.cod_usuario_registro },
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_ObservacionesContrato", dictionary);
            return obj;
        }

        public string Eliminar_SeparacionObservaciones(string cod_separacion, int num_linea)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_separacion", cod_separacion }, { "num_linea", num_linea }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_SeparacionObservaciones", dictionary);
            return result;
        }

        public string Eliminar_ContratosObservaciones(string cod_contrato, string cod_proyecto, int num_linea)
        {
            string result = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_contrato", cod_contrato },
                { "cod_proyecto", cod_proyecto },
                { "num_linea", num_linea }
            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_ContratosObservaciones", dictionary);
            return result;
        }

        public T ObtenerDatosOneDrive<T>(int opcion, string cod_empresa = "", string dsc_Carpeta = "", string cod_separacion = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa },
                { "dsc_Carpeta", dsc_Carpeta }
                //{ "cod_separacion", cod_separacion }
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Trabajador", oDictionary);
            return obj;
        }

        public T ObtenerDatosCorreoContrato<T>(string accion, string cod_empresa = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "accion", accion },
                { "cod_usuario", "" },
                { "cod_empresa", cod_empresa }
                //{ "cod_separacion", cod_separacion }
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_Proyectos", oDictionary);
            return obj;
        }

        public T ObtenerFormatoContrato<T>(int accion, string cod_cliente, string cod_contrato, string cod_proyecto) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "accion", accion.ToString() },
                { "cod_cliente", cod_cliente},
                { "cod_contrato", cod_contrato},
                { "cod_proyecto", cod_proyecto}
            };

            obj = sql.ConsultarEntidad<T>("usp_formato_contrato", dictionary);
            return obj;
        }



    }

}



