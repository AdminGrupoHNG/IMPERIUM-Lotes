using BE_GestionLotes;
using DevExpress.CodeParser;
using DevExpress.LookAndFeel;
using DevExpress.Pdf.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UI_GestionLotes.Formularios.Gestion_Contratos;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using Table = DevExpress.XtraRichEdit.API.Native.Table;

namespace UI_GestionLotes
{
    public class FormatoXmlHelper
    {
        private readonly UnitOfWork unit;
        private string template;
        private string cod_empresa;
        private string param;
        private string archivo;
        private string image;
        private string textoXML = "", textoPagoCont = "", textoPagoFinan = "", sApertura = "", sCierre = "", parametoAReemplazar = "", sApertura3 = "", sCierre3 = "", XMLIMAGEN = "", txtXMLImagen = "", sApertura2 = "", sCierre2 = "";
        public FormatoXmlHelper(string @params, string @codformato, string @empresa) // @params --> no estoy pasando nada si quiero lo saco GetParamImagesOldLinderos
        {
            unit = new UnitOfWork();
            template = GetTemplate(empresa, codformato);
            param = @codformato == "00008" ? "" : GetParamValues(@params);
            image = @codformato == "00008" ? GetParamImagesOldLinderos() : GetParamImagesOld();
            cod_empresa = @empresa;

        }
        private string GetTemplate(string cod_empresa, string cod_formato)
        {
            var tmp = unit.FormatoDocumento.Obtener_PlantillaDeFormatos(cod_empresa: cod_empresa, cod_formato: cod_formato);
            if (tmp == null) return null;
            archivo = tmp.dsc_formatoMD_vinculo;
            return tmp.dsc_wordMLText;
        }
        private string GenerateHeader(string[] content)
        {
            string xml = $"{HeaderStyle()} {OpenHeader()}";
            for (int i = 0; i < content.Count(); i++)
            { xml += HeaderContent(content[i]); }
            xml += $" {CloseRowHeader}";
            return xml;
        }
        private string GenerateRows(string[] content, int j = 0)
        {
            string xml = $"{OpenRows()} ";
            for (int i = 0; i < content.Count(); i++)
            { xml += $"{RowContent(content[i], j: j)}"; }
            xml += $" {CloseRowHeader}";
            return xml;
        }

        private string GetParamValues(string @params)
        {
            try
            {
                //template

                int nPosA = -1; int nPosB = -1;
                if (nPosA == -1) { nPosA = template.IndexOf("CRONOGRAMA DE CUOTAS"); sApertura = "<w:tbl>"; }
                if (nPosB == -1) { nPosB = template.IndexOf("@tabla1"); sCierre = "</w:tr></w:tbl>"; }
                string textP1 = template.Substring(nPosA, (nPosB - nPosA) + sCierre.Length + 40);

                if (nPosA > 1) { nPosA = textP1.IndexOf("<w:tbl><w:tblPr>"); sApertura = "<w:tbl><w:tblPr>"; }
                if (nPosB > 1) { nPosB = textP1.IndexOf("</w:tr></w:tbl>"); sCierre = "</w:tr></w:tbl>"; }
                textoXML = textP1.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);
                return textoXML;

            }
            catch (Exception ex)
            {
                HNGMessageBox.Show(ex.Message, "Obtener Parámetros", TypeMessage.Error);
                return null;
            }
        }
        private string GetParamcontado()
        {
            try
            {
                int nPosA = -1; int nPosB = -1;
                if (nPosA == -1) { nPosA = template.IndexOf("{Descripcion de separacion}"); sApertura = "<w:p><w:pPr><w:jc"; }
                if (nPosB == -1) { nPosB = template.IndexOf("{Descripcion Pago Contado}"); sCierre = "</w:t></w:r></w:p>"; }
                string textP1 = template.Substring(nPosA, (nPosB - nPosA) + sCierre.Length + 40);
                if (nPosA > 1) { nPosA = textP1.IndexOf("<w:p><w:pPr><w"); sApertura = "<w:p><w:pPr><w:"; }
                if (nPosB > 1) { nPosB = textP1.IndexOf("Contado}</w:t></w:r></w:p>"); sCierre = "Contado}</w:t></w:r></w:p>"; }
                textoPagoCont = textP1.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);
                return textoPagoCont;
            }
            catch (Exception ex)
            {
                HNGMessageBox.Show(ex.Message, "Obtener Parámetros", TypeMessage.Error);
                return null;
            }
        }

        private string GetParamfinanciado()
        {
            try
            {
                int nPosA = -1; int nPosB = -1;
                if (nPosA == -1) { nPosA = template.IndexOf("{Descripcion Pago Contado}"); sApertura = "<w:p><w:pPr><w:jc"; }
                if (nPosB == -1) { nPosB = template.IndexOf("{Numero de Cuotas}"); sCierre = "</w:t></w:r></w:p>"; }
                string textP1 = template.Substring(nPosA, (nPosB - nPosA) + sCierre.Length + 40);
                if (nPosA > 1) { nPosA = textP1.IndexOf("<w:p><w:pPr><w:"); sApertura = "<w:p><w:pPr><w:jc"; }
                if (nPosB > 1) { nPosB = textP1.IndexOf("Cuotas}</w:t></w:r></w:p>"); sCierre = "Cuotas}</w:t></w:r></w:p>"; }
                textoPagoFinan = textP1.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);
                return textoPagoFinan;
            }
            catch (Exception ex)
            {
                HNGMessageBox.Show(ex.Message, "Obtener Parámetros", TypeMessage.Error);
                return null;
            }
        }
        private string GetParamImagesNew(string @params)
        {
            try
            {
                int nPosA2 = -1; int nPosB2 = -1; int nPosC2 = -1;
                if (nPosA2 == -1) { nPosA2 = @params.IndexOf("<w:p><w:"); sApertura2 = "<w:p><w:"; }
                if (nPosB2 == -1) { nPosB2 = @params.IndexOf("</w:pict></w:r></w:p>"); sCierre2 = "</w:pict></w:r></w:p>"; }
                string textP2 = @params.Substring(nPosA2, (nPosB2 - nPosA2) + sCierre2.Length);
                if (nPosA2 > 1) { nPosA2 = textP2.IndexOf("<w:p><w:"); sApertura2 = "<w:p><w:"; }
                if (nPosB2 > 1) { nPosB2 = textP2.IndexOf("</w:pict></w:r></w:p>"); sCierre2 = "</w:pict></w:r></w:p>"; }
                txtXMLImagen = textP2.Substring(nPosA2, (nPosB2 - nPosA2) + sCierre2.Length);
                txtXMLImagen = txtXMLImagen.Replace("image1", "imagen200");

                //nPosA2 = txtXMLImagen.IndexOf("<w:binData"); sApertura2 = "<w:binData";
                //nPosB2 = txtXMLImagen.IndexOf("</w:pPr>"); sCierre2 = "</w:pPr>";
                //if (nPosA2 > 1 && nPosB2 > 1) { txtXMLImagen = txtXMLImagen.Replace(txtXMLImagen.Substring(nPosA2, (nPosB2 - nPosA2) + sCierre2.Length), ""); }
                return txtXMLImagen;
            }
            catch (Exception ex)
            {
                HNGMessageBox.Show(ex.Message, "Obtener Parámetros", TypeMessage.Error);
                return null;
            }
        }
        private string GetParamImagesOld()
        {
            try
            {
                if (template != null)
                {
                    int nPosA3 = -1; int nPosB3 = -1; int nPosC3 = -1;
                    if (nPosA3 == -1) { nPosA3 = template.IndexOf("LINDEROS:"); sApertura3 = "<w:p><w:"; }
                    if (nPosB3 == -1) { nPosB3 = template.IndexOf("LINDEROS:"); sCierre3 = "</w:pict></w:r></w:p>"; }
                    string textP3 = template.Substring(nPosA3, (nPosB3 - nPosA3) + sCierre3.Length + 999999);

                    if (nPosA3 > 1) { nPosA3 = textP3.IndexOf("<w:p><w:"); sApertura3 = "<w:p><w:"; }
                    if (nPosB3 > 1) { nPosB3 = textP3.IndexOf("</w:pict></w:r></w:p>"); sCierre3 = "</w:pict></w:r></w:p>"; }

                    XMLIMAGEN = textP3.Substring(nPosA3, (nPosB3 - nPosA3) + sCierre3.Length);

                    return XMLIMAGEN;
                }
                return null;
            }
            catch (Exception ex)
            {
                HNGMessageBox.Show(ex.Message, "Obtener Parámetros", TypeMessage.Error);
                return null;
            }
        }
        private string GetParamImagesOldLinderos()
        {
            try
            {
                if (template != null)
                {
                    int nPosA3 = -1; int nPosB3 = -1; int nPosC3 = -1;
                    if (nPosA3 == -1) { nPosA3 = template.IndexOf("LINDEROS:"); sApertura3 = "<w:p><w:"; }
                    if (nPosB3 == -1) { nPosB3 = template.IndexOf("</w:pict></w:r></w:p>"); sCierre3 = "</w:pict></w:r></w:p>"; }
                    string textP3 = template.Substring(nPosA3 + 30, (nPosB3 - nPosA3) + sCierre3.Length + 20);

                    if (nPosA3 > 1) { nPosA3 = textP3.IndexOf("<w:p><w:"); sApertura3 = "<w:p><w:"; }
                    if (nPosB3 > 1) { nPosB3 = textP3.IndexOf("</w:pict></w:r></w:p>"); sCierre3 = "</w:pict></w:r></w:p>"; }

                    XMLIMAGEN = textP3.Substring(nPosA3, (nPosB3 - nPosA3) + sCierre3.Length);

                    return XMLIMAGEN;
                }
                return null;
            }
            catch (Exception ex)
            {
                HNGMessageBox.Show(ex.Message, "Obtener Parámetros", TypeMessage.Error);
                return null;
            }
        }
        private string GetCustomLetra(string @params)
        {
            var result = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Parametro>(
                new pQFormatD() { Opcion = 6, Cod_solucion = Program.Sesion.Cod_solucion });
            if (result == null) return null;

            var entity = result.FirstOrDefault((w) => w.dsc_formatoMD_parametro.Trim().Equals(@params.Trim())
            && w.cod_tipo_parametro.Equals("TBL"));
            if (entity == null) return null;
            //var ex = template.Contains(entity.dsc_valor_parametro);

            return entity.dsc_valor_parametro;
        }

        private string GetCustomtable(string[] HeaderValues, List<string[]> RowsValues)
        {
            string xml = $"{GenerateHeader(HeaderValues)} ";
            for (int i = 0; i < RowsValues.Count(); i++)
            {
                xml += $"{GenerateRows(RowsValues[i], i + 1)}";
            }
            return string.Concat(xml, CloseTable);
        }

        public void ShowReport(eReportes query, string[] HeaderValues, List<string[]> RowsValues, string @wayToPay, string @params)
        {
            //Probando
            var table = @wayToPay == "FI" ? GetCustomtable(HeaderValues, RowsValues) : GetCustomLetra(@params);
            var imagenLotes = String.IsNullOrEmpty(query.dsc_img_lote) ? "" : query.dsc_img_lote;
            frmReporteGeneral frm = new frmReporteGeneral();
            //RichEditControl edit = new RichEditControl();
            //edit.CreateRibbon();
            var newXmlOne = template.Replace(param, table);
            var newXmlTwo = newXmlOne.Replace(image, GetParamImagesNew(imagenLotes));
            var newXmlThree = @wayToPay == "FI" ? newXmlTwo.Replace(GetParamcontado(), "") : newXmlTwo.Replace(GetParamfinanciado(), "");
            //GetParamImagesNew
            frm.rchEditControl.WordMLText = GetTemplatesWithValues<eReportes>(query, newXmlThree);
            query.dsc_documento_contrato = frm.rchEditControl.WordMLText;
            frm.camposContrato = query;
            frm.descripcionArchivo = query.dsc_nombre_cliente;
            frm.cod_empresa = cod_empresa;
            //edit.SaveDocument(DocumentFormat.OpenXml);
            SplashScreenManager.CloseForm(false);
            frm.ShowDialog();
            frm.Dispose();

            //DateTime FechaRegistro = DateTime.Today;

            //SaveFileDialog dialog = new SaveFileDialog
            //{
            //    Filter = "Archivos (*.*)|; *.*",
            //    FileName = query.dsc_nombre_cliente + "-" + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + ".docx",
            //    RestoreDirectory = true,
            //    CheckFileExists = false,
            //    CheckPathExists = true,
            //    OverwritePrompt = true,
            //    DereferenceLinks = true,
            //    ValidateNames = true,
            //    AddExtension = false,
            //    InitialDirectory = "C:\\",
            //    FilterIndex = 1
            //};
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{

            //    edit.SaveDocument(dialog.FileName, DocumentFormat.OpenXml);
            //    string path = Path.GetDirectoryName(dialog.FileName);
            //    System.Diagnostics.Process.Start(path);
            //}

            //ba
            //richeditdocumentserver richserver = new richeditdocumentserver();
            //// specify default formatting
            //richserver.document.defaultparagraphproperties.alignment = paragraphalignment.center;
            //richserver.savedocument(documentformat.openxml);

            //// specify page settings
            //richserver.document.sections[0].page.landscape = true;
            //richserver.document.sections[0].page.height = devexpress.office.utils.units.inchestodocumentsf(10.0f);
            //richserver.document.sections[0].page.width = devexpress.office.utils.units.inchestodocumentsf(4.5f);
            //// add document content
            //richserver.document.appendtext("this content is created programmatically\n");
            //richserver.document.paragraphs.append();
            //////create a table
            ////table _table = richserver.document.tables.create(richserver.document.selection.start, 8, 8, autofitbehaviortype.fixedcolumnwidth);
            ////_table.beginupdate();
            ////_table.borders.insidehorizontalborder.linethickness = 1;
            ////_table.borders.insidehorizontalborder.linestyle = tableborderlinestyle.double;
            ////_table.borders.insideverticalborder.linethickness = 1;
            ////_table.borders.insideverticalborder.linestyle = tableborderlinestyle.double;
            ////_table.tablealignment = tablerowalignment.center;

            ////_table.foreachcell((cell, rowindex, columnindex) =>
            ////{
            ////    richserver.document.inserttext(cell.range.start, string.format("{0}*{1} is {2}",
            ////        rowindex + 2, columnindex + 2, (rowindex + 2) * (columnindex + 2)));
            ////});
            ////_table.endupdate();

            //// invoke the print preview dialog
            //using (printingsystem printingsystem = new printingsystem())
            //{
            //    using (printablecomponentlink link = new printablecomponentlink(printingsystem))
            //    {
            //        link.component = richserver;
            //        link.createdocument();
            //        link.showpreviewdialog();
            //    }
            //}


            //Probando tambien 
            //RichEditDocumentServer server = new RichEditDocumentServer();
            //using (Stream stream = new FileStream("FirstLook.docx", FileMode.Open))
            //{
            //    stream.Seek(0, SeekOrigin.Begin);
            //    server.LoadDocument(stream, DocumentFormat.OpenXml);
            //    stream.Close();
            //}
            //server.SaveDocument("Result.docx", DocumentFormat.OpenXml);
            //System.Diagnostics.Process.Start("explorer.exe", "/select," + "Result.docx");
        }


        public void saveReport(eReportes query, string[] HeaderValues, List<string[]> RowsValues, string @wayToPay, string @params)
        {

            //Probando
            var table = @wayToPay == "FI" ? GetCustomtable(HeaderValues, RowsValues) : GetCustomLetra(@params);

            RichEditControl edit = new RichEditControl();
            //edit.CreateRibbon();
            var newXmlOne = template.Replace(param, table);
            var newXmlTwo = newXmlOne.Replace(image, GetParamImagesNew(query.dsc_img_lote));
            var newXmlThree = @wayToPay == "FI" ? newXmlTwo.Replace(GetParamcontado(), "") : newXmlTwo.Replace(GetParamfinanciado(), "");
            //GetParamImagesNew
            edit.WordMLText = GetTemplatesWithValues<eReportes>(query, newXmlThree);
            
            DateTime FechaRegistro = DateTime.Today;

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Archivos (*.*)|; *.*",
                FileName = query.dsc_nombre_cliente + "-" + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + ".docx",
                RestoreDirectory = true,
                CheckFileExists = false,
                CheckPathExists = true,
                OverwritePrompt = true,
                DereferenceLinks = true,
                ValidateNames = true,
                AddExtension = false,
                InitialDirectory = "C:\\",
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                edit.SaveDocument(dialog.FileName, DocumentFormat.OpenXml);
                string path = Path.GetDirectoryName(dialog.FileName);
                System.Diagnostics.Process.Start(path);
            }
           
        }


        public void ShowReportLindero(eReportes query)
        {
            //using (Stream stream = new FileStream("FirstLook.docx", FileMode.Open))
            //{
            //RichEditControl edit = new RichEditControl();
            if (query.dsc_img_lote != null && query.dsc_img_lote != "")
            {
                frmReporteGeneral frm = new frmReporteGeneral();

                var newXmlOne = template.Replace(image, GetParamImagesNew(query.dsc_img_lote));
                frm.rchEditControl.WordMLText = GetTemplatesWithValues<eReportes>(query, newXmlOne);
                frm.descripcionArchivo = archivo;
                frm.cod_empresa = cod_empresa;
                SplashScreenManager.CloseForm(false);
                frm.ShowDialog();
                frm.Dispose();
            }
            else
            {
                SplashScreenManager.CloseForm(false);
                HNGMessageBox.Show("Falta configurar la imagen del lote.", "Denominación y Linderos", TypeMessage.Warning);

            }



            //PrintingSystem printingSystem = new PrintingSystem();
            //PrintableComponentLink link = new PrintableComponentLink();
            //link.Component = edit;
            //link.CreateDocument(printingSystem);
            //link.ShowRibbonPreview(UserLookAndFeel.Default);



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
        private String GetTemplatesWithValues<T>(T query, string formato) where T : class, new()
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
                                var newString = formato.Replace(param, value.ToString());
                                formato = newString;
                            }
                        }
                    }
                });

            paramList.ForEach((pr) =>
            {
                var param = pr.dsc_formatoMD_parametro.ToString();
                var newString = formato.Replace(param, "");
                formato = newString;
            });

            return formato;
        }
        private string HeaderStyle()
        {
            string header = @"
            <w:tbl>
                <w:tblPr>
                    <w:tblW w:w=""6480"" w:type=""auto"" />
                    <w:tblInd w:w=""0"" w:type=""dxa"" />
                    <w:tblBorders>
                        <w:top w:val=""nil"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""000000"" />
                        <w:left w:val=""nil"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""000000"" />
                        <w:bottom w:val=""nil"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""000000"" />
                        <w:right w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:insideH w:val=""none"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""auto"" />
                        <w:insideV w:val=""none"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""auto"" />
                    </w:tblBorders>
                    <w:tblCellMar>
                        <w:top w:w=""0"" w:type=""dxa"" />
                        <w:left w:w=""0"" w:type=""dxa"" />
                        <w:bottom w:w=""0"" w:type=""dxa"" />
                        <w:right w:w=""0"" w:type=""dxa"" />
                    </w:tblCellMar>
                </w:tblPr>
                <w:tblGrid />
            ";
            return header;
        }
        private string OpenHeader(int height = 482)
        {
            return
                $@"
                <w:tr>
                    <w:trPr>
                        <w:trHeight w:hRule=""at-least"" w:val=""{height}"" />
                    </w:trPr>
               ";
        }
        private string HeaderContent(string value, int width = 2830)
        {
            return
            $@"
            <w:tc>
                <w:tcPr>
                    <w:tcW w:w=""{width}"" w:type=""dxa"" />
                    <w:tcBorders>
                        <w:top w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:left w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:bottom w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:right w:val=""nil"" />
                    </w:tcBorders>
                    <w:shd w:val=""clear"" w:color=""auto"" w:fill=""376092"" />
                    <w:tcMar>
                        <w:top w:w=""0"" w:type=""dxa"" />
                        <w:left w:w=""108"" w:type=""dxa"" />
                        <w:bottom w:w=""0"" w:type=""dxa"" />
                        <w:right w:w=""108"" w:type=""dxa"" />
                    </w:tcMar>
                    <w:vAlign w:val=""center"" />
                    <w:hideMark />
                </w:tcPr>
            <w:p>
                <w:pPr>
                    <w:shd w:val=""clear"" w:fill=""376092"" />
                    <w:spacing w:before=""0"" w:after=""0"" />
                    <w:ind w:first-line=""0"" w:left=""0"" w:right=""0"" />
                    <w:jc w:val=""center"" />
                    <w:rPr>
                        <w:shd w:val=""clear"" w:color=""auto"" w:fill=""376092"" />
                    </w:rPr>
                </w:pPr>
                <w:r>
                    <w:rPr>
                        <w:rFonts w:ascii=""Calibri"" w:h-ansi=""Calibri"" w:cs=""Calibri"" w:fareast=""Calibri"" />
                        <w:b w:val=""on"" />
                        <w:i w:val=""off"" />
                        <w:color w:val=""FFFFFF"" />
                        <w:sz w:val=""22"" />
                        <w:sz-cs w:val=""22"" />
                    </w:rPr>
                    <w:t>{value}</w:t>
                </w:r>
            </w:p>
        </w:tc>    
            ";
        }
        private string CloseRowHeader { get { return @"</w:tr>"; } }
        private string CloseTable { get { return @"</w:tbl>"; } }
        private string OpenRows(int height = 362)
        {
            return
                $@"
                <w:tr>
                    <w:trPr>
                        <w:trHeight w:hRule=""at-least"" w:val=""{height}"" />
                    </w:trPr>
               ";
        }
        private string RowContent(string value, int width = 1830, int j = 0)
        {
            string stilo1 = j % 2 == 0 ? "DCDCDC" : "FFFFFF";
            return $@"
            <w:tc>
                <w:tcPr>
                    <w:tcW w:w=""{width}"" w:type=""dxa"" />
                    <w:tcBorders>
                        <w:top w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:left w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:bottom w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:right w:val=""nil"" />
                    </w:tcBorders>
                    <w:shd w:val=""clear"" w:color=""auto"" w:fill=""{stilo1}"" />
                    <w:tcMar>
                        <w:top w:w=""0"" w:type=""dxa"" />
                        <w:left w:w=""108"" w:type=""dxa"" />
                        <w:bottom w:w=""0"" w:type=""dxa"" />
                        <w:right w:w=""108"" w:type=""dxa"" />
                    </w:tcMar>
                    <w:vAlign w:val=""top"" />
                    <w:hideMark />
                </w:tcPr>
                <w:p>
                    <w:pPr>
                        <w:shd w:val=""clear"" w:fill=""{stilo1}"" />
                        <w:spacing w:before=""100"" w:after=""100"" />
                        <w:ind w:first-line=""0"" w:left=""0"" w:right=""0"" />
                        <w:jc w:val=""center"" />
                        <w:rPr>
                            <w:shd w:val=""clear"" w:color=""auto"" w:fill=""{stilo1}"" />
                        </w:rPr>
                    </w:pPr>
                    <w:r>
                        <w:rPr>
                            <w:rFonts w:ascii=""Calibri"" w:h-ansi=""Calibri"" w:cs=""Calibri"" w:fareast=""Calibri"" />
                            <w:b w:val=""on"" />
                            <w:i w:val=""off"" />
                            <w:color w:val=""000000"" />
                            <w:sz w:val=""22"" />
                            <w:sz-cs w:val=""22"" />
                        </w:rPr>
                        <w:t>{value}</w:t>
                    </w:r>
                </w:p>
            </w:tc>      
            ";
        }


    }
}
