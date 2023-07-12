
using BE_GestionLotes;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UI_GestionLotes.Formularios.Documento
{
    internal class FormatoMDHelper
    {
        RichEditControl _rich;
        public FormatoMDHelper() { }
        public FormatoMDHelper(RichEditControl rich) { _rich = rich; }

        public List<eFormatoMD_Parametro> LimpiarParametrosAsignados(List<eFormatoMD_Parametro> paramList)
        {
            paramList.ForEach(_ => _.flg_asignado = "NO");
            return paramList.ToList();
        }
        public List<eFormatoMD_Parametro> ObtenerParametrosAsignados(RichEditControl richEdit, List<eFormatoMD_Parametro> paramList)
        {
            var doc = richEdit.Document;
            string tmp = doc.WordMLText.ToLower();
            paramList.ForEach(_ => _.flg_asignado = "NO");

            paramList.ToList()
                .ForEach((obj) =>
                {
                    string pms = obj.dsc_formatoMD_parametro.ToLower();
                    if (tmp.Contains(pms))
                    {
                        var index = paramList.IndexOf(obj);
                        paramList[index].flg_asignado = "SI";
                    }
                });
            return paramList.ToList();
        }
        public List<eFormatoMD_Parametro> ObtenerParametrosAsignados(string wordTemp, List<eFormatoMD_Parametro> paramList)
        {
            string tmp = wordTemp.ToLower();
            paramList.ForEach(_ => _.flg_asignado = "NO");

            paramList.ToList()
                .ForEach((obj) =>
                {
                    string pms = obj.dsc_formatoMD_parametro.ToLower();
                    if (tmp.Contains(pms))
                    {
                        var index = paramList.IndexOf(obj);
                        paramList[index].flg_asignado = "SI";
                    }
                });
            return paramList.ToList();
        }
        public class eTrabajadorDocumentoInfo { } // Eliminar si no se neesita
        public string ObtenerWordParametroValor(string wordTemp, List<eFormatoMD_Parametro> paramList,
            eTrabajadorDocumentoInfo trabajadorInfo)
        {
            var parametroList = ObtenerParametrosAsignados(wordTemp, paramList);


            var t = trabajadorInfo;
            parametroList.Where(f => f.flg_asignado.Equals("SI"))
                .ToList().ForEach((p) =>
                {
                    var properties = t.GetType().GetTypeInfo().GetProperties();
                    foreach (var k in properties)
                    {
                        if (k.Name.ToString().Equals(p.dsc_columna_asociada))
                        {
                            var value = k.GetValue(t);
                            if (value != null)
                            {
                                var param = p.dsc_formatoMD_parametro.ToString();
                                var newString = wordTemp.Replace(param, value.ToString());
                                wordTemp = newString;
                            }
                        }
                    }

                });
            //Limpiar parámetros no asignados
            parametroList.ForEach((pr) =>
            {
                var param = pr.dsc_formatoMD_parametro.ToString();
                var newString = wordTemp.Replace(param, "");
                wordTemp = newString;
            });

            return wordTemp;
        }

        /// <summary>
        /// Método que retorna, los códigos de la entidad alojada en los Nodos, 
        /// concatenado y separado por una ",". Necesita el listado de los Nodos Checados.
        /// </summary>
        /// <param name="listNodes">Lista de TreeListNode</param>
        /// <returns></returns>
        public string ObtenerCodigoConcatenadoDeNodo(List<TreeListNode> listNodes)
        {
            string split = "delete";
            listNodes.ForEach((tr) =>
            {
                if (!tr.HasChildren)
                {
                    split += $",{tr.GetValue("Codigo")}";
                }
            });
            split = split.Replace("delete,", "");
            return split.Trim();
        }

        /// <summary>
        /// Método que retorna, los códigos de una lista, 
        /// concatenado y separado por una ",". un listado de cualquier clase
        /// y que se indique el nombre de la columna/atributo a concatenar.
        /// </summary>
        /// <typeparam name="T">Entidad</typeparam>
        /// <param name="objList">Listado de la Entidad</param>
        /// <param name="cod_list">Columna a concatenar</param>
        /// <returns></returns>
        public string ObtenerValoresConcatenadoDeNodo<T>(List<T> objList, string cod_list)
        {
            string split = "delete";
            objList.ForEach((obj) =>
            {
                var properties = obj.GetType().GetTypeInfo().GetProperties();
                properties.ToList()
                .ForEach((k) =>
                {
                    if (k.Name.ToString().Equals(cod_list))
                    {
                        var value = k.GetValue(obj);
                        if (value != null)
                        {
                            split += $",{value}";
                        }
                    }
                });
            });
            split = split.Replace("delete,", "");
            return split.Trim();
        }



        public void MostrarDocumento_RichEditControl(string cod_documento, string cod_empresa)
        {
            var docList = new UnitOfWork().FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Vinculo>(
                new pQFormatD() { Opcion = 8, Cod_empresaSplit = cod_empresa, Cod_formatoMD_vinculoSplit = cod_documento, Cod_solucion=Program.Sesion.Cod_solucion });

            if (docList.Count() > 0 && docList != null)
            {
                var temp = docList.FirstOrDefault();

                var doc = _rich.Document;
                doc.WordMLText = temp.dsc_wordMLText;
                _rich.WordMLText = doc.WordMLText;
            }

        }
        public void MostrarDescripcion_Parametros(string cod_empresa, string cod_trabajador, List<eFormatoMD_Parametro> paramList)
        {


            string originalTemplate = _rich.WordMLText.ToString();
            var objTrabajador = new UnitOfWork().FormatoDocumento.ConsultaVarias_FormatoMDocumento<eTrabajadorDocumentoInfo>(
                new pQFormatD() { Opcion = 12, Cod_empresaSplit = cod_empresa, Cod_trabajadorSplit = cod_trabajador, Cod_solucion = Program.Sesion.Cod_solucion });


            if (objTrabajador.Count > 0 && objTrabajador != null)
            {
                originalTemplate = ObtenerWordParametroValor(originalTemplate, paramList, objTrabajador.FirstOrDefault());
                _rich.WordMLText = originalTemplate;
            }
        }

        public string ObtenerWord_ConParametrosDescritos(string cod_empresa, string cod_trabajador, string wordTemplate, List<eFormatoMD_Parametro> paramList)
        {
            string originalTemplate = wordTemplate;
            var objTrabajador = new UnitOfWork().FormatoDocumento.ConsultaVarias_FormatoMDocumento<eTrabajadorDocumentoInfo>(
                new pQFormatD() { Opcion = 12, Cod_empresaSplit = cod_empresa, Cod_trabajadorSplit = cod_trabajador, Cod_solucion = Program.Sesion.Cod_solucion });


            if (objTrabajador.Count > 0 && objTrabajador != null)
            {
                originalTemplate = ObtenerWordParametroValor(originalTemplate, paramList, objTrabajador.FirstOrDefault());
            }
            return originalTemplate;
        }


        public void MostrarDocumento_RichEditControl(string wordTemplate)
        {
            var doc = _rich.Document;
            doc.WordMLText = wordTemplate;
            _rich.WordMLText = doc.WordMLText;
        }
        private string GetFormatoWord(string cod_document, string cod_empresa)
        {
            var docList = new UnitOfWork().FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Vinculo>(
                new pQFormatD() { Opcion = 1, Cod_empresaSplit = cod_empresa, Cod_formatoMD_vinculoSplit = cod_document, Cod_solucion = Program.Sesion.Cod_solucion });

            if (docList.Count() > 0 && docList != null)
            {
                var temp = docList.FirstOrDefault();
                return temp.dsc_wordMLText;
            }
            else return null;
        }

    }
}
