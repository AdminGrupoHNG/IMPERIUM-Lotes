using BE_GestionLotes;
using DevExpress.XtraRichEdit;
using DevExpress.XtraScheduler.VCalendar;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UI_GestionLotes.Formularios.Gestion_Contratos;

namespace UI_GestionLotes
{
    public class FormatoWordHelper
    {
        private readonly UnitOfWork unit;
        private string template;
        public FormatoWordHelper()
        {
            this.unit = new UnitOfWork();
        }

        private List<eFormatoMD_Parametro> GetParams()
        {
            var paramList = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Parametro>(
                 new pQFormatD() { Opcion = 6, Cod_solucion = Program.Sesion.Cod_solucion });



            string tmp = template.ToLower();
            paramList.ForEach(j => j.flg_asignado = "NO");



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



        private String GetTemplatesWithValues<T>(List<T> query) where T : class, new()
        {
            var entity = query.First();
            var paramList = GetParams();

            paramList.Where((fil) => fil.flg_asignado.EndsWith("SI"))
                .ToList().ForEach((p) =>
                {
                    var properties = entity.GetType().GetTypeInfo().GetProperties();
                    foreach (var k in properties)
                    {
                        if (k.Name.ToString().ToLower().Trim().Equals(p.dsc_columna_asociada.ToLower().Trim()))
                        {
                            var value = k.GetValue(entity);
                            if (value != null)
                            {
                                var param = p.dsc_formatoMD_parametro.ToString();
                                var newString = template.Replace(param, value.ToString());
                                template = newString;
                            }
                        }
                    }
                });

            paramList.ForEach((pr) =>
            {
                var param = pr.dsc_formatoMD_parametro.ToString();
                var newString = template.Replace(param, "");
                template = newString;
            });

            return template;
        }
        private String GetTemplatesWithValues<T>(T query) where T : class, new()
        {
            var entity = query;
            var paramList = GetParams();

            paramList.Where((fil) => fil.flg_asignado.EndsWith("SI"))
                .ToList().ForEach((p) =>
                {
                    var properties = entity.GetType().GetTypeInfo().GetProperties();
                    foreach (var k in properties)
                    {
                        if (k.Name.ToString().ToLower().Trim().Equals(p.dsc_columna_asociada.ToLower().Trim()))
                        {
                            var value = k.GetValue(entity);
                            if (value != null)
                            {
                                var param = p.dsc_formatoMD_parametro.ToString();
                                var newString = template.Replace(param, value.ToString());
                                template = newString;
                            }
                        }
                    }
                });

            paramList.ForEach((pr) =>
            {
                var param = pr.dsc_formatoMD_parametro.ToString();
                var newString = template.Replace(param, "");
                template = newString;
            });

            return template;
        }

        public void ShowWordReport<T>(List<T> query, string cod_empresa, string cod_formato) where T : class, new()
        {
            var plantilla = unit.FormatoDocumento.Obtener_PlantillaDeFormatos(cod_empresa: cod_empresa, cod_formato: cod_formato);
            if (plantilla != null)
            {
                this.template = plantilla.dsc_wordMLText;

                RichEditControl edit = new RichEditControl();
                edit.WordMLText = GetTemplatesWithValues<T>(query);
                edit.ShowPrintPreview();
                edit.Dispose();
            }
            else
            {
                SplashScreenManager.CloseForm(false);
                HNGMessageBox.Show("No se ha cargado la plantilla correctamente", "Cargar Plantilla", TypeMessage.Error);
            }
        }
        public void ShowWordReport<T>(T query, string cod_empresa, string cod_formato) where T : class, new()
        {
            var plantilla = unit.FormatoDocumento.Obtener_PlantillaDeFormatos(cod_empresa: cod_empresa, cod_formato: cod_formato);
            if (plantilla != null)
            {
                this.template = plantilla.dsc_wordMLText;
                RichEditControl edit = new RichEditControl();
                edit.WordMLText = GetTemplatesWithValues<T>(query);
                edit.ShowPrintPreview();
                edit.Dispose();
            }
            else
            {
                SplashScreenManager.CloseForm(false);
                HNGMessageBox.Show("No se ha cargado la plantilla correctamente", "Cargar Plantilla", TypeMessage.Error);
            }
        }
        //Soloparacontrato
        public void ShowWordReportContrato<T>(T query, string cod_empresa, string cod_formato, string plantilla) where T : class, new()
        {
            //var plantilla = unit.FormatoDocumento.Obtener_PlantillaDeFormatos(cod_empresa: cod_empresa, cod_formato: cod_formato);
            if (plantilla != null)
            {
                this.template = plantilla;

                RichEditControl edit = new RichEditControl();
                edit.WordMLText = GetTemplatesWithValues<T>(query);
                edit.ShowPrintPreview();
                edit.Dispose();
            }
            else
            {
                SplashScreenManager.CloseForm(false);
                HNGMessageBox.Show("No se ha cargado la plantilla correctamente", "Cargar Plantilla", TypeMessage.Error);
            }
        }
        public void ShowWordReportFormatoGeneral(string cod_empresa, string cod_formato)
        {
            var plantilla = unit.FormatoDocumento.Obtener_PlantillaDeFormatos(cod_empresa: cod_empresa, cod_formato: cod_formato);
            if (plantilla != null)
            {
                //this.template = plantilla;
                frmReporteGeneral frm = new frmReporteGeneral();
                //RichEditControl edit = new RichEditControl();
                frm.rchEditControl.WordMLText = plantilla.dsc_wordMLText;
                frm.descripcionArchivo = plantilla.dsc_formatoMD_vinculo;
                frm.cod_empresa = cod_empresa;
                SplashScreenManager.CloseForm(false);
                frm.ShowDialog();
                frm.Dispose();
            }
            else
            {
                SplashScreenManager.CloseForm(false);
                HNGMessageBox.Show("No se ha cargado la plantilla correctamente", "Cargar Plantilla", TypeMessage.Error);
            }
        }
    }
}
