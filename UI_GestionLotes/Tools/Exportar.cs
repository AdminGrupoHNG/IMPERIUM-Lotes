using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_GestionLotes.Tools
{
    internal class Exportar
    {
        private readonly UnitOfWork unit;
        public Exportar() { unit = new UnitOfWork(); }
        private string exportPath() { return unit.Globales.GetAppVariableValor("RutaArchivosLocalExportar"); }


        public void ExportarExcel(GridControl gControl, string fileName)
        {
            try
            {
                //string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                //string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\campanhas" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                //if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                //gvListaSeparaciones.ExportToXlsx(archivo);
                //if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    Process.Start(archivo);
                //}
                string path = $@"{exportPath()}\{fileName}.xlsx";
                gControl.ExportToXlsx(path);
                Process.Start(exportPath());
            }
            catch (Exception ex)
            {
                unit.Globales.Mensaje(false, ex.Message);
            }
        }


        public void GridCrontrolToPDF(GridControl gControl, string fileName)
        {
            string path = $@"{exportPath()}\{fileName}.pdf";



            DevExpress.XtraPrinting.PrintingSystem printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printLink = new DevExpress.XtraPrinting.PrintableComponentLink();



            DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();



            try
            {
                printLink.Component = gControl;   //Set to your GridControl instance  
                printLink.CreateDocument(printingSystem1);
                printLink.Landscape = true;



                printingSystem1.ShowPrintStatusDialog = true;
                printingSystem1.PageSettings.Landscape = true;
                printingSystem1.ExportToPdf(path, options);
                Process.Start(exportPath());
            }
            catch (Exception ex) { unit.Globales.Mensaje(false, ex.Message); }
            finally
            {
                printingSystem1.Dispose();
                printLink.Dispose();
            }
        }
        public void GridCrontrolToPDFII(GridControl gControl, string fileName)
        {
            string path = $@"{exportPath()}\{fileName}.pdf";
            gControl.ExportToPdf(path);
            Process.Start(exportPath());
        }
    }
    
}
