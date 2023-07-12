using BE_GestionLotes;
using DA_GestionLotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_GestionLotes
{
    public class blFormatoDocumento
    {
        private readonly daSQL sql;
        public blFormatoDocumento(daSQL sql) { this.sql = sql; }

        public eFormatoMD_Vinculo Obtener_PlantillaDeFormatos(string cod_empresa, string cod_formato, string cod_solucion = "004")
        {
            var result = ConsultaVarias_FormatoMDocumento<eFormatoMD_Vinculo>(new pQFormatD()
            {
                Opcion = 8,
                Cod_empresaSplit = cod_empresa,
                Cod_solucion = cod_solucion,
                Cod_formatoMD_vinculoSplit = cod_formato
            });

            return result.Count > 0 && result != null ? result.FirstOrDefault() : null;
        }



        public List<T> ConsultaVarias_FormatoMDocumento<T>(pQFormatD param) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", param.Opcion },
                { "cod_usuario", param.Cod_usuario},
                { "cod_estado", param.Cod_estado},
                { "cod_formatoMD_seguimiento", param.Cod_formatoMD_seguimiento},
                { "cod_empresaSplit", param.Cod_empresaSplit},
                { "dsc_formatoMD_general", param.Dsc_formatoMD_general},
                { "cod_formatoMD_generalSplit", param.Cod_formatoMD_generalSplit},
                { "cod_formatoMD_vinculoSplit", param.Cod_formatoMD_vinculoSplit},
                { "cod_trabajadorSplit", param.Cod_trabajadorSplit},
                { "cod_solucion", param.Cod_solucion},
            };

            myList = this.sql.ListaconSP<T>("Usp_RHU_ConsultasVarias_FormatoMDocumento", oDictionary);
            return myList;
        }

        public T InsertarActualizar_FormatoMDGeneral<T>(int opcion, eFormatoMD_General eDoc) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_formatoMD_general", eDoc.cod_formatoMD_general },
                { "cod_formatoMD_grupo", eDoc.cod_formatoMD_grupo },
                { "cod_tipo_formato", eDoc.cod_tipo_formato },
                { "cod_solucion", eDoc.cod_solucion },
                { "dsc_formatoMD_general", eDoc.dsc_formatoMD_general },
                { "num_modelo", eDoc.num_modelo },
                { "dsc_observacion", eDoc.dsc_observacion},
                { "dsc_wordMLText", eDoc.dsc_wordMLText },
                { "flg_obligatorio", eDoc.flg_obligatorio},
                { "cod_usuario_cambio", eDoc.cod_usuario_cambio },
                { "flg_activo", eDoc.flg_activo },
            };

            obj = this.sql.ConsultarEntidad<T>("Usp_RHU_InsertarActualizar_FormatoMD_General", dictionary);
            return obj;
        }

        public T InsertarActualizar_FormatoMDVinculo<T>(int opcion, eFormatoMD_Vinculo eDoc) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", eDoc.cod_empresa },
                { "cod_formatoMD_general", eDoc.cod_formatoMD_general },
                { "cod_formatoMD_vinculo", eDoc.cod_formatoMD_vinculo },
                { "dsc_formatoMD_vinculo", eDoc.dsc_formatoMD_vinculo },
                { "cod_solucion", eDoc.cod_solucion },
                { "dsc_observacion", eDoc.dsc_observacion },
                { "flg_obligatorio", eDoc.flg_obligatorio },
                { "flg_seguimiento", eDoc.flg_seguimiento },
                { "cod_cargo_firma", eDoc.cod_cargo_firma },
                { "dsc_version", eDoc.dsc_version },
                { "flg_estado", eDoc.flg_estado },
                { "dsc_wordMLText", eDoc.dsc_wordMLText },
                { "cod_usuario_cambio", eDoc.cod_usuario_cambio },
            };

            obj = this.sql.ConsultarEntidad<T>("Usp_RHU_InsertarActualizar_FormatoMD_Vinculo", dictionary);
            return obj;
        }
        public T InsertarActualizar_FormatoMDGrupo<T>(eFormatoMD_Grupo eGrupo, int opcion) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_formatoMD_grupo", eGrupo.cod_formatoMD_grupo },
                { "cod_solucion", eGrupo.cod_solucion },
                { "dsc_formatoMD_grupo", eGrupo.dsc_formatoMD_grupo },
                { "num_jerarquia", eGrupo.num_jerarquia }
            };

            obj = this.sql.ConsultarEntidad<T>("Usp_RHU_InsertarActualizar_FormatoMD_Grupo", dictionary);
            return obj;
        }
    }
}
