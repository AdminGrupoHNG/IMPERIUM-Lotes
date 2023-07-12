using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using BL_GestionLotes;
using System.Data;
using System.Windows.Forms;

namespace UI_GestionLotes.Tools
{
    public class ReadExcel
    {
        private readonly UnitOfWork unit;
        private string _fullPath;
        public ReadExcel()
        {
            unit = new UnitOfWork();
        }
        public string ObtenerExcel() { obtenerRutaExcel(out string fileName, out _fullPath); return _fullPath; }

        private void obtenerRutaExcel(out string fileName, out string path)
        {
            fileName = path = "";
            using (var o = new System.Windows.Forms.OpenFileDialog())
            {
                //o.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                o.Filter = "Ms-Excel (uptoday) |*.xlsx|Ms-Excel (old)|*.xls";
                o.Title = "Open File";
                o.ShowDialog();
                if (o.FileName != "")
                {
                    path = o.FileName;
                    fileName = Path.GetFileName(o.FileName).ToLower();
                }
            }
        }

        public const string sNullable = "Nullable`1";
        public List<T> ListarExcel<T>(out string path) where T : class, new()
        {
            path = "";
            Excel.Application xlApp;
            xlApp = new Excel.Application(); Excel.Workbook xlWorkBook; Excel.Worksheet xlWorkSheet; Excel.Range range;
            try
            {
                int row = 0, rw = 0, cl = 0;

                xlApp = new Excel.Application();
                //open the excel
                obtenerRutaExcel(out string _fileName, out string _fullPath);
                if (string.IsNullOrEmpty(_fullPath)) return null;

                path = _fullPath;// para mostrar en textbox.
                xlWorkBook = xlApp.Workbooks.Open(_fullPath);//@"C:\Reporte\Plantilla-carga-masiva-personal.xlsx");
                                                             //get the first sheet of the excel
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                // get the total row count
                rw = range.Rows.Count;
                //get the total column count
                cl = range.Columns.Count;

                var objList = new List<T>();
                // traverse all the row in the excel
                for (row = 2; row <= rw; row++)
                {
                    T obj = new T();
                    int totalProperties = obj.GetType().GetProperties()/*.OrderBy(o=>o.Name)*/.Count();
                    for (int y = 1; y <= totalProperties; y++)
                    {
                        //  var value = (string)(range.Cells[row, y] as Excel.Range).Value2.ToString();
                        string value = "";
                        var rango = range.Cells[row, y];
                        if (!string.IsNullOrWhiteSpace(rango.FormulaLocal))
                        {
                            value = (string)(rango as Excel.Range).Value2.ToString();//??"";
                        }
                        var objEntidad = obj.GetType().GetProperties();
                        PropertyInfo propiedad = objEntidad[y - 1];
                        propiedad.SetValue(obj, value, null);
                    }

                    objList.Add(obj);
                }
                //release the resources
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                //Liberar Excel del proceso.
                //while (Marshal.ReleaseComObject(xlApp) != 0) { }
                xlApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return objList;
            }
            catch (Exception ex)
            {
                //Liberar Excel del proceso.
                xlApp.Quit();
                //while (Marshal.ReleaseComObject(xlApp) != 0) { }
                xlApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                unit.Globales.Mensaje(blGlobales.TipoMensaje.Error, "Verificar formatos: " + ex.Message, "Leer Excel");
                return null;
            }
        }
        public List<T> ListarExcel<T>(out string path, int iniciaRow, int iniciaRangoColumn, int terminaRangoColumn) where T : class, new()
        {
            path = "";
            Excel.Application xlApp;
            xlApp = new Excel.Application(); Excel.Workbook xlWorkBook; Excel.Worksheet xlWorkSheet; Excel.Range range;
            try
            {
                int row = 0, rw = 0, cl = 0;


                //open the excel
                obtenerRutaExcel(out string _fileName, out _fullPath);
                if (string.IsNullOrEmpty(_fullPath)) return null;

                path = _fullPath;// para mostrar en textbox.
                xlWorkBook = xlApp.Workbooks.Open(_fullPath);//@"C:\Reporte\Plantilla-carga-masiva-personal.xlsx");
                                                             //get the first sheet of the excel
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                // get the total row count
                rw = range.Rows.Count;
                //get the total column count
                cl = range.Columns.Count;

                var objList = new List<T>();
                // traverse all the row in the excel
                for (row = iniciaRow; row <= rw; row++)
                {
                    T obj = new T();
                    // int totalProperties = obj.GetType().GetProperties()/*.OrderBy(o=>o.Name)*/.Count();
                    int index = 0;
                    for (int y = iniciaRangoColumn; y <= terminaRangoColumn; y++)
                    {
                        index++;
                        string value = "";
                        var rango = range.Cells[row, y];
                        if (!string.IsNullOrWhiteSpace(rango.FormulaLocal))
                        {
                            value = (string)(rango as Excel.Range).Value2.ToString();//??"";
                        }

                        var objEntidad = obj.GetType().GetProperties();
                        PropertyInfo propiedad = objEntidad[index - 1];
                        propiedad.SetValue(obj, value, null);
                    }

                    objList.Add(obj);
                }
                //release the resources
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                //Liberar Excel del proceso.
                //while (Marshal.ReleaseComObject(xlApp) != 0) { }
                xlApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return objList;
            }
            catch (Exception ex)
            {
                //Liberar Excel del proceso.
                xlApp.Quit();
                //while (Marshal.ReleaseComObject(xlApp) != 0) { }
                xlApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                unit.Globales.Mensaje(blGlobales.TipoMensaje.Error, "Verificar formatos: " + ex.Message, "Leer Excel");
                return null;
            }
        }
        public List<T> ListarExcel<T>(int iniciaRow, int iniciaRangoColumn, int terminaRangoColumn) where T : class, new()
        {
            Excel.Application xlApp;
            xlApp = new Excel.Application(); Excel.Workbook xlWorkBook; Excel.Worksheet xlWorkSheet; Excel.Range range;
            try
            {
                int row = 0, rw = 0, cl = 0;
                if (string.IsNullOrEmpty(_fullPath)) return null;

                xlWorkBook = xlApp.Workbooks.Open(_fullPath);//@"C:\Reporte\Plantilla-carga-masiva-personal.xlsx");
                                                             //get the first sheet of the excel
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                // get the total row count
                rw = range.Rows.Count;
                //get the total column count
                cl = range.Columns.Count;

                var objList = new List<T>();
                // traverse all the row in the excel
                for (row = iniciaRow; row <= rw; row++)
                {
                    T obj = new T();
                    // int totalProperties = obj.GetType().GetProperties()/*.OrderBy(o=>o.Name)*/.Count();
                    int index = 0;
                    for (int y = iniciaRangoColumn; y <= terminaRangoColumn; y++)
                    {
                        index++;
                        //var value = (string)(range.Cells[row, y] as Excel.Range).Value2.ToString();
                        string value = "";
                        var rango = range.Cells[row, y];
                        //  if (!string.IsNullOrWhiteSpace(rango.FormulaLocal))
                        {
                            value = (string)(rango as Excel.Range).Value2.ToString();//??"";
                        }

                        var objEntidad = obj.GetType().GetProperties();
                        PropertyInfo propiedad = objEntidad[index - 1];
                        propiedad.SetValue(obj, value, null);
                    }

                    objList.Add(obj);
                }
                //release the resources
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                //Liberar Excel del proceso.
                //while (Marshal.ReleaseComObject(xlApp) != 0) { }
                xlApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return objList;
            }
            catch (Exception ex)
            {
                //Liberar Excel del proceso.
                xlApp.Quit();
                //while (Marshal.ReleaseComObject(xlApp) != 0) { }
                xlApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                unit.Globales.Mensaje(blGlobales.TipoMensaje.Error, "Verificar formatos: " + ex.Message, "Leer Excel");
                return null;
            }
        }

        public DataTable ToDataTable()//String filePath, String sheetName)
        {
            DataTable dt = new DataTable();
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlBook = null;
            Microsoft.Office.Interop.Excel.Range xlRange = null;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;
            try
            {
                //open the excel
                obtenerRutaExcel(out string _fileName, out _fullPath);
                if (string.IsNullOrEmpty(_fullPath)) return null;



                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlBook = xlApp.Workbooks.Open(_fullPath);
                xlSheet = xlBook.Worksheets.get_Item(1);//[sheetName];
                xlRange = xlSheet.UsedRange;
                DataRow row = null;
                for (int i = 1; i <= xlRange.Rows.Count; i++)
                {
                    if (i != 1)
                        row = dt.NewRow();
                    for (int j = 1; j <= xlRange.Columns.Count; j++)
                    {
                        if (i == 1)
                            dt.Columns.Add(xlRange.Cells[1, j].value);
                        else
                            row[j - 1] = xlRange.Cells[i, j].value;
                    }
                    if (row != null)
                        dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                xlBook.Close();
                xlApp.Quit();
            }
            return dt;
        }

        public DataTable ToDataTable(string filePath)//String filePath, String sheetName)
        {
            DataTable dt = new DataTable();
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlBook = null;
            Microsoft.Office.Interop.Excel.Range xlRange = null;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlBook = xlApp.Workbooks.Open(filePath);
                xlSheet = xlBook.Worksheets.get_Item(1);//[sheetName];
                xlRange = xlSheet.UsedRange;
                DataRow row = null;
                for (int i = 1; i <= xlRange.Rows.Count; i++)
                {
                    if (i != 1)
                        row = dt.NewRow();
                    for (int j = 1; j <= xlRange.Columns.Count; j++)
                    {
                        if (i == 1)
                            dt.Columns.Add(xlRange.Cells[1, j].value);
                        else
                            row[j - 1] = xlRange.Cells[i, j].value;
                    }
                    if (row != null)
                        dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                xlBook.Close();
                xlApp.Quit();
            }
            return dt;
        }

        private void kill_process()
        {
            //Process[] workers = Process.GetProcessesByName("miprocesso");
            //foreach (Process worker in workers)
            //{
            //    worker.Kill();
            //    worker.WaitForExit();
            //    worker.Dispose();
            //}

            Process myProcess = Process.GetProcessById(222);
            myProcess.Kill();
            myProcess.WaitForExit();
            myProcess.Dispose();

            Process[] processlist = Process.GetProcesses();

            foreach (Process p in processlist)
            {
                Console.WriteLine("Process: {0} ID: {1}", p.ProcessName, p.Id);
            }
        }
    }
}
