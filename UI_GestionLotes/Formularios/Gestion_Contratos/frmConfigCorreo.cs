using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using UI_SGC.Formularios.Informacion_General.Entidades;
using BE_GestionLotes;
using BL_GestionLotes;
//using UI_SGC.Formularios.Cotizaciones_Y_Files.Files;
using System.Configuration;
using System.IO;
using DevExpress.XtraBars;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraReports.UI;
using Outlook = Microsoft.Office.Interop.Outlook;
using UI_GestionLotes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DevExpress.XtraPrinting;
using UI_GestionLotes.Formularios.Operaciones;
//using UI_SGC.Formularios.Operaciones.Ordenes;

namespace UI_GestionLotes.Formularios.Gestion_Contratos
{
    public partial class frmConfigCorreo : UI_GestionLotes.Tools.SimpleModalForm
    {
        internal enum MiEntidad
        {
            Reservas = 1,
            OrdenServicio = 2,
            Voucher = 3
        }

        internal MiEntidad entidad = MiEntidad.Reservas;
        //public eReservasFile obj = new eReservasFile();
        //blReservas objblReservas = new blReservas();
        //public eOrdenServicio objOrdServ = new eOrdenServicio();
        //blOrdenServicio oblOrdenServicio = new blOrdenServicio();
        //public eVouchers objVoucher = new eVouchers();
        public int tamañoScroll = 0;
        public eUsuario user = new eUsuario();
        public eReportes camposContrato = new eReportes();
        private readonly UnitOfWork unit;
        public List<string> lstDocumentos = new List<string>();
        public RichEditControl documentoWord = new RichEditControl();
        public int variableTamaño = 0;
        frmReporteGeneral frmHandler;
        public string codigoDocumento = "", descripRuta = "", descripArchivo = "",cod_empresa = "";
        public eEmpresa eEmp = new eEmpresa();

        public frmConfigCorreo()
        {
            InitializeComponent();
            TitleBackColor = Program.Sesion.Colores.Verde;
            unit = new UnitOfWork();
        }
        internal frmConfigCorreo(frmReporteGeneral frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }
        private void frmConfigCorreo_Load(object sender, EventArgs e)
        {
            //lblTitulo.Appearance.ForeColor =  Program.Sesion.Colores.Verde;
            lbltitulo1.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            lblAdjunto.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            DateTime FechaRegistro = DateTime.Today;
            Inicializar();

            //   ddbWord.Text = FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + ".docx";
        }

        public void Inicializar()
        {
            //if (camposContrato.dsc_nombre_cliente != null && camposContrato.dsc_nombre_cliente.Trim() != "")
            //{
                documentoWord.WordMLText = camposContrato.dsc_documento_contrato;
            cargarCampos();
            cargarCuerpoMensaje(camposContrato.dsc_nombre_cliente);
            //}
            exportPdforWord();

        }

        public string validarCamposCorreo()
        {
            if (mmCorreoEnvio.Text.Trim().Length > 0)
            {
                if (mmCorreoEnvio.Text.Contains(";"))
                {
                    int validar = 0;
                    string correoInvalido = "";
                    string[] des = mmCorreoEnvio.Text.Split(';');
                    foreach (string d in des)
                    {
                        if (!new EmailAddressAttribute().IsValid(d.Trim()))
                        {
                            mmCorreoEnvio.Focus();
                            validar = 1;
                            correoInvalido = d;
                        }
                    }
                    if (validar == 1) { return "Correo \"" + correoInvalido + "\"" + " invalido."; }
                }
                else
                {
                    if (!new EmailAddressAttribute().IsValid(mmCorreoEnvio.Text.Trim()))
                    {
                        mmCorreoEnvio.Focus();
                        return "Debe ingresar un correo válido.";
                    }
                }

                
            }
            if (mmCorreoEnvio.Text.Trim() == "")
            {
                mmCorreoEnvio.Focus();
                return "Debe ingresar un correo.";
            }
            return null;
        }
        public void cargarCampos()
        {
            mmCorreoEnvio.Text = camposContrato.dsc_email_cliente;
            meCopiaCorreo.Text = String.IsNullOrEmpty(camposContrato.dsc_email_copropietario) ? "" : camposContrato.dsc_email_copropietario;
            meAsuntoCorreo.Text = "VALIDACIÓN DE CONTRATO DE COMPRAVENTA";
            eEmp = unit.Proyectos.ObtenerDatosCorreoContrato<eEmpresa>("21", cod_empresa);
        }

        public void cargarCuerpoMensaje(string nombreCliente, string correoEmpresa = "clientes.california@corpterramas.com", string telefono = "51-991-118-562", string nombreProyecto = "")
        {
            var cuerpoMensaje =
                "<!DOCTYPE html PUBLIC  \" -//W3C//DTD XHTML 1.0 Transitional//EN\" <!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd \">" +
                "<html xmlns=\"http://www.w3.org/1999/xhtml \">" +
                    "<head>" +
                        "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>" +
                            "</title>" +
                                "<style type=\"text/css\">" +
                                ".csEF169D0F{text-align:justify;text-indent:0pt;margin:0pt 0pt 0pt 0pt;background-color:#FFFFFF}" +
                                ".csCDFC1413{color:#242424;background-color:#FFFFFF;font-family:'Microsoft Sans Serif';font-size:10.5pt;font-weight:normal;font-style:normal;}" +
                            ".csFC5568{color:#242424;background-color:#FFFFFF;font-family:'Microsoft Sans Serif';font-size:10.5pt;font-weight:normal;font-style:normal;text-decoration: none;}" +
                            ".cs2DBE58EF{color:#4F52B2;background-color:transparent;font-family:'Microsoft Sans Serif';font-size:10.5pt;font-weight:normal;font-style:normal;text-decoration: underline;}" +
                            ".cs80D9435B{text-align:justify;text-indent:0pt;margin:0pt 0pt 0pt 0pt}" +
                                "</style>" +
                    "</head>" +
                    "<body>" +
                        "<p class=\"csEF169D0F\"><a name=\"_dx_frag_StartFragment\"></a><span class=\"csCDFC1413\">&nbsp;</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\">" +
                        $"Estimado Sr(a) {nombreCliente} " +
                        "</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\">&nbsp;</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\">Junto con saludarlo y desear que se encuentre bien, aprovechamos para agradecerle por su separaci&oacute;n y adjuntarle la versi&oacute;n preliminar del contrato de C/V de Acciones y Derechos, para su validaci&oacute;n de datos final.&nbsp;</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\">&nbsp;</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\">En caso tenga alguna duda y/o consulta, no dude en ponerse en contacto con nuestro equipo de Post Venta " +
                        $"(correo:&nbsp;&nbsp;<a class=\"csFC5568\" href=\"mailto:{eEmp.dsc_UsuarioEmailContratos}\" target=\"_blank\" title=\"mailto:{eEmp.dsc_UsuarioEmailContratos} \"><span class=\"cs2DBE58EF\">{eEmp.dsc_UsuarioEmailContratos}</span></a></span><span class=\"csCDFC1413\">&nbsp;&nbsp;o tel&eacute;fono: +{eEmp.dsc_telefono})." +
                        $"&nbsp;</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\">&nbsp;</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\"> Felicitaciones por ser parte del proyecto {camposContrato.dsc_proyecto} y muchas gracias por su confianza.&nbsp;</span></p><p class=\"csEF169D0F\"><span class=\"csCDFC1413\">&nbsp;</span></p><p class=\"cs80D9435B\"><span class=\"csCDFC1413\">Saludos cordiales.&nbsp;</span><a name=\"_dx_frag_EndFragment\"></a></p>" +
                    "</body>" +
                "</html>";
            recTextoCorreo.HtmlText = cuerpoMensaje;
        }
        private void frmConfigCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
            if (e.KeyCode == Keys.Delete) DeleteAttachment();
        }

        private void picEnviar_Click(object sender, EventArgs e)
        {
            //xscaPanelLabel.Controls.OfType<Label>().ToList().ForEach((oo) =>
            //{
            //    MessageBox.Show(oo.Name);
            //});
            EnviarMail();
        }

        private void EnviarMail()
        {

            //Traer credenciales de todas las empresas
            //           var formatoEnvio = unit.Globales.Mensaje(TipoMensaje.YesNo,
            //"¿Seguro de continuar con el envío?", "Formato envío masivo de Boletas");
            //           if (formatoEnvio == DialogResult.No)
            //           {
            //               //Se cnacela el proceso.
            //               //flagEnviado = false;
            //               //lblProceso.Text = $"Status: enviados <b>(0)</b> de <b>({numSeleccionadosPorEnviar})</b>  boletas.";
            //               return;
            //           }
            string validarCampos = validarCamposCorreo();
            if (validarCampos == null)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Enviado Correo...", "Cargando...");

                bool enviado = false;
                // foreach (var item in pnlPara.Controls.OfType<Label>().ToList())
                {
                    //  if (item.Name.Contains("_to"))
                    {
                        string email = mmCorreoEnvio.Text.Replace(";", ","); //GetEmailDestinoSplit();// item.Text.ToLower().Trim();
                        string copia = meCopiaCorreo.Text.Replace(";", ",");
                        //if (new EmailAddressAttribute().IsValid(email))
                        //{
                        //Obtener Credencial asignado a cada empresa
                        // ver si las credenciales trae  valores vacíos.
                        //eEmp = unit.Proyectos.ObtenerDatosCorreoContrato<eEmpresa>("21", "00010");
                        var credencial = new List<eSistema>();
                        credencial.Add(new eSistema()
                        {
                            cod_clave = eEmp.cod_empresa,//"CC000", //Esto viene de la tabla: scfma_empresa
                            dsc_clave = eEmp.dsc_UsuarioEmailContratos,
                            dsc_valor = eEmp.dsc_ClaveEmailContratos,
                        });//GetCredenciales().Where((e) => e.cod_clave.Equals("00010")).ToList();

                        enviado = unit.Globales.EnviarCorreoElectronico_SMTP(
                        sDestinatario: mmCorreoEnvio.Text.Trim(), sCopia: copia, sAsunto: meAsuntoCorreo.Text,
                        sCuerpo: recTextoCorreo.Text, credenciales: credencial,
                        RutasAdjunto: GetDirectorioAjunto(), ArchivosAdjunto: GetArrayArchivoAdjunto());
                        //MessageBox.Show(__credencial[0].dsc_valor.ToString());
                        // arreglar   probar el doWorker
                        //}
                    }
                }
                SplashScreenManager.CloseForm(false);

            }
            else
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(validarCampos, "Configuración de correos.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //if (enviado)
            //{
            //    MessageBox.Show("Enviado");
            //}


        }

        private string GetDirectorioAjunto()
        {
            string directory = "";
            foreach (var item in xscaPanelLabel.Controls.OfType<Label>().ToList())
            {
                if (item.Name.Contains("_att"))
                {
                    directory = Path.GetDirectoryName(item.Tag.ToString().Trim());
                    break;
                }
            }
            return directory;
        }
        private string[] GetArrayArchivoAdjunto()
        {
            string[] path = new string[xscaPanelLabel.Controls.Count/* - 1*/];
            int i = -1;
            foreach (var item in xscaPanelLabel.Controls.OfType<Label>().ToList())
            {
                //MessageBox.Show(item.Name);

                if (item.Name.Contains("_att"))
                {
                    //MessageBox.Show("entro");
                    i++;
                    path[i] = item.Text.ToString().Trim();
                }
            }

            //foreach (var item in pnlAdjuntos.Controls.OfType<Label>().ToList())
            //{
            //    var dd = item;
            //    MessageBox.Show(item.Name);
            //}


            //foreach (var item in pnlAdjuntos.Controls.OfType<Label>().ToList())
            //{
            //    var dd = item;
            //    MessageBox.Show(item.Name);
            //}

            return path;
        }

        //private List<eSistema> GetCredenciales()
        //{
        //    var empresaParaAsuntoMail = unit.EmailingBoleta.ConsultaVarias_EmailingBoletas<eEmpresaEmail>
        //       (new pEmailingBoleta() { Opcion = 7 });



        //    // Obtener Listado de Credenciales para el correo electrónico.
        //    return empresaParaAsuntoMail.Select((obj) => new eSistema()
        //    {
        //        cod_clave = obj.cod_empresa,//"CC000", //Esto viene de la tabla: scfma_empresa
        //        dsc_clave = obj.dsc_UsuarioEmailBoletas,
        //        dsc_valor = obj.dsc_ClaveEmailBoletas,
        //    }).ToList();
        //}

        private void AdjuntarDocumentos()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Archivos de cotratos|*.pdf;*.docx;*.doc;*.xlsx;*.xls;*.jpg;*.jpeg;*.png";
            openFileDialog.DefaultExt = ".pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // var name = openFileDialog.FileName;
                var ruta = openFileDialog.FileName;// Path.GetDirectoryName(openFileDialog.FileName);

                var files = openFileDialog.FileNames;
                //string[] archivosSeleccionado = new string[2];
                //int i = xscaPanelLabel.Controls.Count;

                var max = xscaPanelLabel.Controls.OfType<Label>().ToList().Max((m) => m.Name);
                int i = ToInt(max) + 1;
                //   att3  att4 att5
                //att3 

                foreach (var file in files)
                {

                    Label doc = CrearLabelAdjunto(file, Path.GetExtension(file), i++, file);
                    xscaPanelLabel.Controls.Add(doc);
                    tamañoScroll += doc.Width;
                    //i++;
                }

                if (tamañoScroll > 915)
                {
                    xscaPanelLabel.MinimumSize = new Size(145, 42);
                }
            }
        }

        private void deseleccionarCampos()
        {
            recTextoCorreo.DeselectAll();
            mmCorreoEnvio.DeselectAll();
            meCopiaCorreo.DeselectAll();
            meAsuntoCorreo.DeselectAll();
        }
        private int ToInt(string stringText)
        {
            string value = (stringText != null && stringText != "" ? stringText : "0");
            var result = Regex.Match(value, @"\d+").Value;
            return Convert.ToInt32(result);
        }



        private void picAdjuntar_Click(object sender, EventArgs e)
        {
            AdjuntarDocumentos();
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
            //  abrir  seleccionar  documentos:
            //  obtener  todos los documentos seleccionados:
            // validar  si no se ha seleccionado anda...  return


        }

        private void exportPdforWord()
        {
            DateTime FechaRegistro = DateTime.Today;

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "PDF (*.pdf*)|*.pdf*", /*| Documento de Word (*.docx*)|*.docx* | Archivos (*.*)|; *.**/
                FileName = descripArchivo + "-" + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + ".pdf",
                Title = "Guardar Contrato - Enviar Correo",
                DefaultExt = "pdf",
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
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Adjuntando contrato", "Cargando...");

                //string numero =  dialog.FilterIndex;
                //dialog.DefaultExt = Path.GetExtension(dialog.filter);
                PdfExportOptions options = new PdfExportOptions();
                options.PdfUACompatibility = PdfUACompatibility.PdfUA1;
                try
                {
                    if (frmHandler != null)
                    {
                        frmHandler.rchEditControl.ExportToPdf(dialog.FileName, options);
                    }
                    else
                    {
                        documentoWord.ExportToPdf(dialog.FileName, options);
                    }
                }
                catch(Exception ex)
                {
                    //var ruta2 = dialog.FileName;
                    //Label doc2 = CrearLabelAdjunto(ruta2, Path.GetExtension(ruta2), 0, ruta2);
                    //xscaPanelLabel.Controls.Add(doc2);
                    //tamañoScroll += doc2.Width;
                    //SplashScreenManager.CloseForm(false);
                }
                
                
                var ruta = dialog.FileName;
                Label doc = CrearLabelAdjunto(ruta, Path.GetExtension(ruta), 0, ruta);
                xscaPanelLabel.Controls.Add(doc);
                tamañoScroll += doc.Width;
                SplashScreenManager.CloseForm(false);

                //string path = Path.GetDirectoryName(dialog.FileName);                
                //System.Diagnostics.Process.Start(path);

            }

        }

        private Label CrearLabelAdjunto(string texto, string extencion, int id, string rutaCarpeta)
        {
            Image icon = Properties.Resources.picture_23px;
            switch (extencion)
            {
                case ".pdf":
                    icon = Properties.Resources.adobe_acrobat_reader_23px;
                    break;
                case ".docx":
                    icon = Properties.Resources.microsoft_word_2019_23px;
                    break;
                case ".doc":
                    icon = Properties.Resources.microsoft_word_2019_23px;
                    break;
                case ".xlsx":
                    icon = Properties.Resources.xls_23px;
                    break;
                case ".xls":
                    icon = Properties.Resources.xls_23px;
                    break;
            }

            string name = $"_att{id}";
            Label label = new Label()
            {
                Text = $"        {Path.GetFileName(texto)}",
                Tag = rutaCarpeta,
                AutoSize = true,
                Image = icon,
                Dock = DockStyle.Left,
                Cursor = Cursors.Hand,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleLeft,
                //Width = 165,
                MinimumSize = new Size(145, 26),
                Name = name
            };

            // MessageBox.Show(label.Name);
            label.DoubleClick += (sender, args) => OpenFile();
            label.MouseClick += (sender, args) => OpenPopup(name, args, rutaCarpeta);
            label.BringToFront();

            return label;
        }

        private void DeleteAttachment()
        {
            xscaPanelLabel.Controls.OfType<Label>().ToList().ForEach((l) => { if (l.Name.Equals(codigoDocumento)) { xscaPanelLabel.Controls.Remove(l); tamañoScroll -= l.Width; if (tamañoScroll < 915)
                    {
                        xscaPanelLabel.MaximumSize = new Size(915, 28);
                        layoutControlItem6.MaxSize = new Size(919, 32);
                    }
                } });

        }

        private void OpenPopup(string id, MouseEventArgs args, string ruta)
        {
            codigoDocumento = id;
            descripRuta = ruta;
            meCopiaCorreoOculto.Focus();
            xscaPanelLabel.Controls.OfType<Label>().ToList().ForEach((l) => { if (l.Name.Equals(codigoDocumento)) { l.BorderStyle = BorderStyle.FixedSingle; } else { l.BorderStyle = BorderStyle.None; } });
            if (args.Button == MouseButtons.Right) popupMenu1.ShowPopup(MousePosition);
        }

        private void OpenFile()
        {
            System.Diagnostics.Process.Start(descripRuta);

        }

        private void picGuardar_Click(object sender, EventArgs e)
        {
            //foreach (var item in GetArrayArchivoAdjunto())
            //{
            //    MessageBox.Show(item);
            //}
            var html = recTextoCorreo.HtmlText;
            GetArrayArchivoAdjunto();
            //foreach (var item in pnlAdjuntos.Controls.OfType<Label>().ToList())
            //{
            //    var dd = item;
            //    MessageBox.Show(item.Name);
            //}

        }

        private void picGuardar_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void picEnviar_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnQuitarAdjunto_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteAttachment();
        }

        private void lblEnviar_Click(object sender, EventArgs e)
        {
            EnviarMail();
        }

       

        private void layoutControlItem7_Click(object sender, EventArgs e)
        {
            AdjuntarDocumentos();
        }

        private void picAdjuntar_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAbrirDoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFile();
        }

        //private void CargarDatos()
        //{
        //    try
        //    {
        //        string rutaPdf = ConfigurationManager.AppSettings["rutaPdf"].ToString();

        //        switch (entidad)
        //        {
        //            case MiEntidad.Reservas:
        //                if (System.IO.File.Exists(rutaPdf + obj.NombreArchivo + ".jpg"))
        //                {
        //                    ddbImagen.Text = obj.NombreArchivo + ".jpg";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }

        //                if (System.IO.File.Exists(rutaPdf + obj.NombreArchivo + ".docx"))
        //                {
        //                    ddbWord.Text = obj.NombreArchivo + ".docx";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }

        //                if (System.IO.File.Exists(rutaPdf + obj.NombreArchivo + ".xlsx"))
        //                {
        //                    ddbExcel.Text = obj.NombreArchivo + ".xlsx";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }


        //                btnActualizar.Visibility = BarItemVisibility.Always;
        //                btnEnviar.Visibility = BarItemVisibility.Always;
        //                btnEnviarOrden.Visibility = BarItemVisibility.Never;
        //                meCorreoEnvio.Text = obj.correoEnvio;
        //                meCopiaCorreo.Text = obj.CopiaCorreo;
        //                meCopiaCorreoOculto.Text = obj.CopiaCorreoOculto;
        //                meAsuntoCorreo.Text = obj.AsuntoCorreo;
        //                recTextoCorreo.HtmlText = obj.TextoCorreo;
        //                break;
        //            case MiEntidad.OrdenServicio:
        //                if (System.IO.File.Exists(rutaPdf + objOrdServ.NombreArchivo + ".jpg"))
        //                {
        //                    ddbImagen.Text = objOrdServ.NombreArchivo + ".jpg";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }

        //                if (System.IO.File.Exists(rutaPdf + objOrdServ.NombreArchivo + ".docx"))
        //                {
        //                    ddbWord.Text = objOrdServ.NombreArchivo + ".docx";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }

        //                if (System.IO.File.Exists(rutaPdf + objOrdServ.NombreArchivo + ".xlsx"))
        //                {
        //                    ddbExcel.Text = objOrdServ.NombreArchivo + ".xlsx";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }


        //                btnActualizar.Visibility = BarItemVisibility.Never;
        //                btnEnviar.Visibility = BarItemVisibility.Never;
        //                btnEnviarOrden.Visibility = BarItemVisibility.Always;
        //                meCorreoEnvio.Text = objOrdServ.CorreoEnvio;
        //                meCopiaCorreo.Text = objOrdServ.CopiaCorreo;
        //                meCopiaCorreoOculto.Text = objOrdServ.CopiaCorreoOculto;
        //                meAsuntoCorreo.Text = objOrdServ.AsuntoCorreo;
        //                recTextoCorreo.HtmlText = objOrdServ.TextoCorreo;
        //                break;

        //            case MiEntidad.Voucher:
        //                if (System.IO.File.Exists(rutaPdf + objVoucher.IdVoucher + ".jpg"))
        //                {
        //                    ddbImagen.Text = objVoucher.IdVoucher + ".jpg";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }

        //                if (System.IO.File.Exists(rutaPdf + objVoucher.IdVoucher + ".docx"))
        //                {
        //                    ddbWord.Text = objVoucher.IdVoucher + ".docx";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }

        //                if (System.IO.File.Exists(rutaPdf + objVoucher.IdVoucher + ".xlsx"))
        //                {
        //                    ddbExcel.Text = objVoucher.IdVoucher + ".xlsx";
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                    layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                }
        //                else
        //                {
        //                    sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                    layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                }


        //                btnActualizar.Visibility = BarItemVisibility.Never;
        //                btnEnviar.Visibility = BarItemVisibility.Never;
        //                btnEnviarOrden.Visibility = BarItemVisibility.Never;

        //                if (opcion == 0)
        //                {
        //                    meCorreoEnvio.Text = objVoucher.EnvioCorreo;
        //                    meCopiaCorreo.Text = objVoucher.CopiaCorreo;
        //                    meCopiaCorreoOculto.Text = objVoucher.CopiaCorreoOculto;
        //                    meAsuntoCorreo.Text = objVoucher.AsuntoCorreo;
        //                    recTextoCorreo.HtmlText = objVoucher.TextoCorreo;
        //                }
        //                else if (opcion == 1)
        //                {
        //                    meCorreoEnvio.Text = objVoucher.EnvioCorreoE;
        //                    meCopiaCorreo.Text = objVoucher.CopiaCorreoE;
        //                    meCopiaCorreoOculto.Text = objVoucher.CopiaCorreoOcultoE;
        //                    meAsuntoCorreo.Text = objVoucher.AsuntoCorreoE;
        //                    recTextoCorreo.HtmlText = objVoucher.TextoCorreoE;
        //                }
        //                else if (opcion == 2)
        //                {
        //                    meCorreoEnvio.Text = objVoucher.EnvioCorreoC;
        //                    meCopiaCorreo.Text = objVoucher.CopiaCorreoC;
        //                    meCopiaCorreoOculto.Text = objVoucher.CopiaCorreoOcultoC;
        //                    meAsuntoCorreo.Text = objVoucher.AsuntoCorreoC;
        //                    recTextoCorreo.HtmlText = objVoucher.TextoCorreoC;
        //                }

        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        string result = "";
        //        switch (entidad)
        //        {
        //            case MiEntidad.Reservas:

        //                blFiles blfile = new blFiles();
        //                obj.correoEnvio = meCorreoEnvio.Text;
        //                obj.CopiaCorreo = meCopiaCorreo.Text;
        //                obj.CopiaCorreoOculto = meCopiaCorreoOculto.Text;
        //                obj.AsuntoCorreo = meAsuntoCorreo.Text;
        //                obj.TextoCorreo = recTextoCorreo.HtmlText;
        //                obj.opcion = 7;

        //                result = blfile.ActualizaReserva(obj);

        //                if (result == "OK")
        //                {
        //                    MessageBox.Show("Configuración guardada satisfactoriamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //                break;
        //            case MiEntidad.OrdenServicio:
        //                blOrdenServicio oblOrdenServicio = new blOrdenServicio();
        //                objOrdServ.CorreoEnvio = meCorreoEnvio.Text;
        //                objOrdServ.CopiaCorreo = meCopiaCorreo.Text;
        //                objOrdServ.CopiaCorreoOculto = meCopiaCorreoOculto.Text;
        //                objOrdServ.AsuntoCorreo = meAsuntoCorreo.Text;
        //                objOrdServ.TextoCorreo = recTextoCorreo.HtmlText;
        //                objOrdServ.Opcion = 1;

        //                result = oblOrdenServicio.ActualizarOrdenServicio(objOrdServ);

        //                if (result == "OK")
        //                {
        //                    MessageBox.Show("Configuración guardada satisfactoriamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //                break;

        //            case MiEntidad.Voucher:
        //                blVouchers oblVouchers = new blVouchers();

        //                if (opcion == 0)
        //                {
        //                    objVoucher.EnvioCorreo = meCorreoEnvio.Text;
        //                    objVoucher.CopiaCorreo = meCopiaCorreo.Text;
        //                    objVoucher.CopiaCorreoOculto = meCopiaCorreoOculto.Text;
        //                    objVoucher.AsuntoCorreo = meAsuntoCorreo.Text;
        //                    objVoucher.TextoCorreo = recTextoCorreo.HtmlText;
        //                }
        //                if (opcion == 1)
        //                {
        //                    objVoucher.EnvioCorreoE = meCorreoEnvio.Text;
        //                    objVoucher.CopiaCorreoE = meCopiaCorreo.Text;
        //                    objVoucher.CopiaCorreoOcultoE = meCopiaCorreoOculto.Text;
        //                    objVoucher.AsuntoCorreoE = meAsuntoCorreo.Text;
        //                    objVoucher.TextoCorreoE = recTextoCorreo.HtmlText;
        //                }
        //                if (opcion == 2)
        //                {
        //                    objVoucher.EnvioCorreoC = meCorreoEnvio.Text;
        //                    objVoucher.CopiaCorreoC = meCopiaCorreo.Text;
        //                    objVoucher.CopiaCorreoOcultoC = meCopiaCorreoOculto.Text;
        //                    objVoucher.AsuntoCorreoC = meAsuntoCorreo.Text;
        //                    objVoucher.TextoCorreoC = recTextoCorreo.HtmlText;
        //                }

        //                result = oblVouchers.ModificarVoucher(objVoucher, user);

        //                if (result == "OK")
        //                {
        //                    MessageBox.Show("Configuración guardada satisfactoriamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void btnAdjuntar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        string rutaPdf = ConfigurationManager.AppSettings["rutaPdf"].ToString();
        //        OpenFileDialog myFileDialog = new OpenFileDialog();
        //        myFileDialog.Filter = "Image files (*.jpg, *.png) | *.jpg; ; *.png";
        //        myFileDialog.FilterIndex = 1;
        //        myFileDialog.InitialDirectory = "C:\\";
        //        myFileDialog.Title = "Abrir archivo";
        //        myFileDialog.CheckFileExists = false;
        //        DialogResult result = myFileDialog.ShowDialog();

        //        string saveToFullPath = "";

        //        if ((result == DialogResult.OK))
        //        {
        //            string fileName = myFileDialog.FileName;

        //            switch (entidad)
        //            {
        //                case MiEntidad.Reservas:
        //                    saveToFullPath = Path.Combine(rutaPdf, obj.NombreArchivo + ".jpg");
        //                    break;
        //                case MiEntidad.OrdenServicio:
        //                    saveToFullPath = Path.Combine(rutaPdf, objOrdServ.NombreArchivo + ".jpg");
        //                    break;
        //                case MiEntidad.Voucher:
        //                    saveToFullPath = Path.Combine(rutaPdf, objVoucher.IdVoucher + ".jpg");
        //                    break;
        //            }

        //            Image image = Image.FromFile(fileName);
        //            Image thumb = image.GetThumbnailImage(1024, 768, () => false, IntPtr.Zero);
        //            thumb.Save(saveToFullPath);

        //            //System.IO.File.Copy(fileName, saveToFullPath);

        //            if (System.IO.File.Exists(saveToFullPath))
        //            {
        //                switch (entidad)
        //                {
        //                    case MiEntidad.Reservas:
        //                        ddbImagen.Text = obj.NombreArchivo + ".jpg";
        //                        break;
        //                    case MiEntidad.OrdenServicio:
        //                        ddbImagen.Text = objOrdServ.NombreArchivo + ".jpg";
        //                        break;
        //                    case MiEntidad.Voucher:
        //                        ddbImagen.Text = objVoucher.IdVoucher + ".jpg";
        //                        break;
        //                }
        //                sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                MessageBox.Show("Se adjuntó exitosamente la imagen", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                layoutControlImagen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                MessageBox.Show("No se adjuntó la imagen seleccionada", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void frmConfigCorreo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Escape) this.Close();
        //}

        //private void btnAdjuntarExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        string rutaPdf = ConfigurationManager.AppSettings["rutaPdf"].ToString();
        //        OpenFileDialog myFileDialog = new OpenFileDialog();
        //        myFileDialog.Filter = "Excel files (*.xlsx) | *.xlsx";
        //        myFileDialog.FilterIndex = 1;
        //        myFileDialog.InitialDirectory = "C:\\";
        //        myFileDialog.Title = "Abrir archivo";
        //        myFileDialog.CheckFileExists = false;
        //        DialogResult result = myFileDialog.ShowDialog();

        //        if ((result == DialogResult.OK))
        //        {
        //            string fileName = myFileDialog.FileName;
        //            string saveToFullPath = "";
        //            switch (entidad)
        //            {
        //                case MiEntidad.Reservas:
        //                    saveToFullPath = Path.Combine(rutaPdf, obj.NombreArchivo + ".xlsx");
        //                    break;
        //                case MiEntidad.OrdenServicio:
        //                    saveToFullPath = Path.Combine(rutaPdf, objOrdServ.NombreArchivo + ".xlsx");
        //                    break;
        //                case MiEntidad.Voucher:
        //                    saveToFullPath = Path.Combine(rutaPdf, objVoucher.IdVoucher + ".xlsx");
        //                    break;
        //            }

        //            if (System.IO.File.Exists(saveToFullPath))
        //            {
        //                System.IO.File.Delete(saveToFullPath);
        //            }

        //            System.IO.File.Copy(fileName, saveToFullPath);

        //            if (System.IO.File.Exists(saveToFullPath))
        //            {
        //                switch (entidad)
        //                {
        //                    case MiEntidad.Reservas:
        //                        ddbExcel.Text = obj.NombreArchivo + ".xlsx";
        //                        break;
        //                    case MiEntidad.OrdenServicio:
        //                        ddbExcel.Text = objOrdServ.NombreArchivo + ".xlsx";
        //                        break;
        //                    case MiEntidad.Voucher:
        //                        ddbExcel.Text = objVoucher.IdVoucher + ".xlsx";
        //                        break;
        //                }
        //                sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                MessageBox.Show("Se adjuntó exitosamente el archivo", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                layoutControlExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                MessageBox.Show("No se adjuntó el archivo seleccionado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void btnAdjuntarWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        string rutaPdf = ConfigurationManager.AppSettings["rutaPdf"].ToString();
        //        OpenFileDialog myFileDialog = new OpenFileDialog();
        //        myFileDialog.Filter = "Word files (*.docx) | *.docx";
        //        myFileDialog.FilterIndex = 1;
        //        myFileDialog.InitialDirectory = "C:\\";
        //        myFileDialog.Title = "Abrir archivo";
        //        myFileDialog.CheckFileExists = false;
        //        DialogResult result = myFileDialog.ShowDialog();

        //        if ((result == DialogResult.OK))
        //        {
        //            string fileName = myFileDialog.FileName;
        //            string saveToFullPath = "";
        //            switch (entidad)
        //            {
        //                case MiEntidad.Reservas:
        //                    saveToFullPath = Path.Combine(rutaPdf, obj.NombreArchivo + ".docx");
        //                    break;
        //                case MiEntidad.OrdenServicio:
        //                    saveToFullPath = Path.Combine(rutaPdf, objOrdServ.NombreArchivo + ".docx");
        //                    break;
        //                case MiEntidad.Voucher:
        //                    saveToFullPath = Path.Combine(rutaPdf, objVoucher.IdVoucher + ".docx");
        //                    break;
        //            }

        //            if (System.IO.File.Exists(saveToFullPath))
        //            {
        //                System.IO.File.Delete(saveToFullPath);
        //            }

        //            System.IO.File.Copy(fileName, saveToFullPath);

        //            if (System.IO.File.Exists(saveToFullPath))
        //            {
        //                switch (entidad)
        //                {
        //                    case MiEntidad.Reservas:
        //                        ddbWord.Text = obj.NombreArchivo + ".docx";
        //                        break;
        //                    case MiEntidad.OrdenServicio:
        //                        ddbWord.Text = objOrdServ.NombreArchivo + ".docx";
        //                        break;
        //                    case MiEntidad.Voucher:
        //                        ddbWord.Text = objVoucher.IdVoucher + ".docx";
        //                        break;
        //                }
        //                sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                MessageBox.Show("Se adjuntó exitosamente el archivo", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                sliAdjunto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                layoutControlWord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                MessageBox.Show("No se adjuntó el archivo seleccionado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void btnActualizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        SplashScreenManager.ShowDefaultWaitForm("Actualizando reserva...", "Cargando...");

        //        string path = "";
        //        string nombreArchivo = "";
        //        string ruta = ConfigurationManager.AppSettings["rutaPdf"].ToString();
        //        string TextoCorreo = "";

        //        obj.NombreArchivo = obj.TipoFile + obj.AnhoFile + obj.NumeroFile + "-P" + obj.IdProveedor + "E" + obj.IdEstabl + "-R" + obj.IdReserva;
        //        nombreArchivo = obj.NombreArchivo + ".html";


        //        if (obj.Estado == 3)
        //        {
        //            RptReconfirmacion report = new RptReconfirmacion();
        //            ReportPrintTool printTool = new ReportPrintTool(report);
        //            report.RequestParameters = false;
        //            report.Parameters["TipoFile"].Value = obj.TipoFile;
        //            report.Parameters["AnhoFile"].Value = obj.AnhoFile;
        //            report.Parameters["NumeroFile"].Value = obj.NumeroFile;
        //            report.Parameters["IdProveedor"].Value = obj.IdProveedor;
        //            report.Parameters["IdEstabl"].Value = obj.IdEstabl;
        //            report.Parameters["TipoPernocte"].Value = obj.Pernocte;
        //            report.Parameters["IdCiudad"].Value = obj.IdCiudad;
        //            report.Parameters["IdUser"].Value = user.IdUser;
        //            report.Parameters["TipoFile"].Visible = false;
        //            report.Parameters["AnhoFile"].Visible = false;
        //            report.Parameters["NumeroFile"].Visible = false;
        //            report.Parameters["IdProveedor"].Visible = false;
        //            report.Parameters["IdEstabl"].Visible = false;
        //            report.Parameters["TipoPernocte"].Visible = false;
        //            report.Parameters["IdCiudad"].Visible = false;
        //            report.Parameters["IdUser"].Visible = false;
        //            path = ruta + nombreArchivo;
        //            report.ExportOptions.Docx.TableLayout = true;
        //            report.ExportToHtml(path);
        //            report.Dispose();

        //            obj.AsuntoCorreo = "Reconfirmación reserva " + obj.NroFile + " - " + obj.Grupo.TrimEnd() + " - " + obj.Proveedor;
        //            TextoCorreo = @"<HTML><BODY><p style='font-family:Arial;font-size:13px;'>Estimado:</p><p style='font-family:Arial;font-size:13px;'>A continuación, envío reconfirmación de reserva, por favor, confirmar</p>";

        //            RichEditDocumentServer richText = new RichEditDocumentServer();
        //            if (System.IO.File.Exists(path))
        //            {
        //                using (System.IO.FileStream logStream = System.IO.File.Open(path, FileMode.Open))
        //                {
        //                    richText.LoadDocument(logStream, DocumentFormat.Html);
        //                    recTextoCorreo.HtmlText = TextoCorreo + richText.HtmlText;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (obj.Pernocte == 0)
        //            {
        //                if (obj.IdProveedor == 84 || obj.IdProveedor == 375)
        //                {
        //                    RptReservaServicioColtur report = new RptReservaServicioColtur();
        //                    ReportPrintTool printTool = new ReportPrintTool(report);
        //                    report.RequestParameters = false;
        //                    report.Parameters["TipoFile"].Value = obj.TipoFile;
        //                    report.Parameters["AnhoFile"].Value = Convert.ToInt32(obj.AnhoFile);
        //                    report.Parameters["NumeroFile"].Value = Convert.ToInt32(obj.NumeroFile);
        //                    report.Parameters["IdProveedor"].Value = Convert.ToInt32(obj.IdProveedor);
        //                    report.Parameters["IdEstabl"].Value = Convert.ToInt32(obj.IdEstabl);
        //                    report.Parameters["TipoPernocte"].Value = obj.Pernocte;
        //                    report.Parameters["IdCiudad"].Value = obj.IdCiudad;
        //                    report.Parameters["IdUser"].Value = user.IdUser;
        //                    report.Parameters["TipoFile"].Visible = false;
        //                    report.Parameters["AnhoFile"].Visible = false;
        //                    report.Parameters["NumeroFile"].Visible = false;
        //                    report.Parameters["IdProveedor"].Visible = false;
        //                    report.Parameters["IdEstabl"].Visible = false;
        //                    report.Parameters["TipoPernocte"].Visible = false;
        //                    report.Parameters["IdCiudad"].Visible = false;
        //                    report.Parameters["IdUser"].Visible = false;
        //                    path = ruta + nombreArchivo;
        //                    report.ExportOptions.Docx.TableLayout = true;
        //                    report.ExportToHtml(path);
        //                    report.Dispose();
        //                }
        //                else
        //                {
        //                    RptReservaServicio report = new RptReservaServicio();
        //                    ReportPrintTool printTool = new ReportPrintTool(report);
        //                    report.RequestParameters = false;
        //                    report.Parameters["TipoFile"].Value = obj.TipoFile;
        //                    report.Parameters["AnhoFile"].Value = Convert.ToInt32(obj.AnhoFile);
        //                    report.Parameters["NumeroFile"].Value = Convert.ToInt32(obj.NumeroFile);
        //                    report.Parameters["IdProveedor"].Value = Convert.ToInt32(obj.IdProveedor);
        //                    report.Parameters["IdEstabl"].Value = Convert.ToInt32(obj.IdEstabl);
        //                    report.Parameters["TipoPernocte"].Value = obj.Pernocte;
        //                    report.Parameters["IdCiudad"].Value = obj.IdCiudad;
        //                    report.Parameters["IdUser"].Value = user.IdUser;
        //                    report.Parameters["TipoFile"].Visible = false;
        //                    report.Parameters["AnhoFile"].Visible = false;
        //                    report.Parameters["NumeroFile"].Visible = false;
        //                    report.Parameters["IdProveedor"].Visible = false;
        //                    report.Parameters["IdEstabl"].Visible = false;
        //                    report.Parameters["TipoPernocte"].Visible = false;
        //                    report.Parameters["IdCiudad"].Visible = false;
        //                    report.Parameters["IdUser"].Visible = false;
        //                    path = ruta + nombreArchivo;
        //                    report.ExportOptions.Docx.TableLayout = true;
        //                    report.ExportToHtml(path);
        //                    report.Dispose();
        //                }
        //            }
        //            else
        //            {
        //                RptReservaHabitacion report = new RptReservaHabitacion();
        //                report.RequestParameters = false;
        //                report.Parameters["TipoFile"].Value = obj.TipoFile;
        //                report.Parameters["AnhoFile"].Value = Convert.ToInt32(obj.AnhoFile);
        //                report.Parameters["NumeroFile"].Value = Convert.ToInt32(obj.NumeroFile);
        //                report.Parameters["IdProveedor"].Value = Convert.ToInt32(obj.IdProveedor);
        //                report.Parameters["IdEstabl"].Value = Convert.ToInt32(obj.IdEstabl);
        //                report.Parameters["TipoPernocte"].Value = obj.Pernocte;
        //                report.Parameters["IdCiudad"].Value = obj.IdCiudad;
        //                report.Parameters["IdUser"].Value = user.IdUser;
        //                report.Parameters["TipoFile"].Visible = false;
        //                report.Parameters["AnhoFile"].Visible = false;
        //                report.Parameters["NumeroFile"].Visible = false;
        //                report.Parameters["IdProveedor"].Visible = false;
        //                report.Parameters["IdEstabl"].Visible = false;
        //                report.Parameters["TipoPernocte"].Visible = false;
        //                report.Parameters["IdCiudad"].Visible = false;
        //                report.Parameters["IdUser"].Visible = false;
        //                path = ruta + nombreArchivo;
        //                report.ExportOptions.Docx.TableLayout = true;
        //                report.ExportToHtml(path);
        //                report.Dispose();
        //            }

        //            obj.AsuntoCorreo = "Reserva " + obj.NroFile + " - " + obj.Grupo.TrimEnd() + " - " + obj.Proveedor;
        //            TextoCorreo = @"<HTML><BODY><p style='font-family:Arial;font-size:13px;'>Estimado:</p>
        //                            <p style='font-family:Arial;font-size:13px;'>A continuación, envío solicitud de reserva, por favor, confirmar</p>";

        //            RichEditDocumentServer richText = new RichEditDocumentServer();
        //            if (System.IO.File.Exists(path))
        //            {
        //                using (System.IO.FileStream logStream = System.IO.File.Open(path, FileMode.Open))
        //                {
        //                    richText.LoadDocument(logStream, DocumentFormat.Html);
        //                    recTextoCorreo.HtmlText = TextoCorreo + richText.HtmlText;
        //                }
        //            }

        //        }

        //        SplashScreenManager.CloseDefaultWaitForm();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }

        //}

        //private void btnEnviar_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    string result = "";
        //    string result2 = "";

        //    if (meCorreoEnvio.Text != "")
        //    {
        //        DialogResult boton = MessageBox.Show("¿Está seguro de enviar la reserva?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        //        if (boton == DialogResult.OK)
        //        {
        //            string path = "";
        //            string nombreArchivo = "";
        //            string nombreArchivoImagen = "";
        //            string nombreArchivoExcel = "";
        //            string nombreArchivoWord = "";
        //            string ruta = ConfigurationManager.AppSettings["rutaPdf"].ToString();

        //            //Guardar
        //            blFiles blfile = new blFiles();
        //            obj.correoEnvio = meCorreoEnvio.Text;
        //            obj.CopiaCorreo = meCopiaCorreo.Text;
        //            obj.CopiaCorreoOculto = meCopiaCorreoOculto.Text;
        //            obj.AsuntoCorreo = meAsuntoCorreo.Text;
        //            obj.TextoCorreo = recTextoCorreo.HtmlText;
        //            obj.opcion = 7;

        //            result = blfile.ActualizaReserva(obj);

        //            if (result == "OK")
        //            {
        //                try
        //                {
        //                    if (!Directory.Exists(ConfigurationManager.AppSettings["rutaPdf"].ToString()))
        //                    {
        //                        Directory.CreateDirectory(ConfigurationManager.AppSettings["rutaPdf"].ToString());
        //                    }

        //                    obj.NombreArchivo = obj.TipoFile + obj.AnhoFile + obj.NumeroFile + "-P" + obj.IdProveedor + "E" + obj.IdEstabl + "-R" + obj.IdReserva;
        //                    nombreArchivo = obj.NombreArchivo + ".html";
        //                    nombreArchivoImagen = obj.NombreArchivo + ".jpg";
        //                    nombreArchivoExcel = obj.NombreArchivo + ".xlsx";
        //                    nombreArchivoWord = obj.NombreArchivo + ".docx";

        //                    path = ruta + nombreArchivo;

        //                    Outlook.Application oApp;
        //                    oApp = new Microsoft.Office.Interop.Outlook.Application();
        //                    Outlook.MailItem oMsg;
        //                    oMsg = oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        //                    oMsg.To = obj.correoEnvio;
        //                    oMsg.CC = obj.CopiaCorreo;
        //                    oMsg.BCC = obj.CopiaCorreoOculto;
        //                    oMsg.Subject = obj.AsuntoCorreo;
        //                    oMsg.HTMLBody = obj.TextoCorreo;

        //                    if (System.IO.File.Exists(ruta + nombreArchivoImagen))
        //                    {
        //                        oMsg.Attachments.Add(ruta + nombreArchivoImagen);
        //                    }

        //                    if (System.IO.File.Exists(ruta + nombreArchivoExcel))
        //                    {
        //                        oMsg.Attachments.Add(ruta + nombreArchivoExcel);
        //                    } 

        //                    if (System.IO.File.Exists(ruta + nombreArchivoWord))
        //                    {
        //                        oMsg.Attachments.Add(ruta + nombreArchivoWord);
        //                    }

        //                    oMsg.Send();

        //                    //actualiza datos de envio de reserva

        //                    if (obj.Estado == 3)
        //                    {
        //                        obj.opcion = 4;
        //                        obj.FechaConfirma2 = DateTime.Now;
        //                        obj.UsuarioConfirma2 = user.IdUser;
        //                    }
        //                    else {
        //                        obj.opcion = 2;
        //                        obj.FechaEnvio = DateTime.Now;
        //                        obj.UsuarioEnvio = user.IdUser;
        //                    }

        //                    result2 = objblReservas.ActualizaReserva(obj);

        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                }
        //                finally
        //                {
        //                    if (System.IO.File.Exists(path))
        //                    {
        //                        System.IO.File.Delete(path);
        //                    }

        //                    if (System.IO.File.Exists(ruta + nombreArchivoImagen))
        //                    {
        //                        System.IO.File.Delete(ruta + nombreArchivoImagen);
        //                    }

        //                    if (System.IO.File.Exists(ruta + nombreArchivoExcel))
        //                    {
        //                        System.IO.File.Delete(ruta + nombreArchivoExcel);
        //                    }

        //                    if (System.IO.File.Exists(ruta + nombreArchivoWord))
        //                    {
        //                        System.IO.File.Delete(ruta + nombreArchivoWord);
        //                    }
        //                }

        //                if (result2 == "OK")
        //                {
        //                    MessageBox.Show("Reserva enviada satisfactoriamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    this.Close();
        //                }
        //            }

        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Por favor ingrese el correo de destino", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}

        //private void btnEnviarOrden_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    string result = "";
        //    string result2 = "";

        //    DialogResult boton = MessageBox.Show("¿Está seguro de enviar la orden de servicio?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //    if (boton == DialogResult.OK)
        //    {

        //        string path = "";
        //        string nombreArchivo = "";
        //        string nombreImagen = "";
        //        string nombreArchivoExcel = "";
        //        string nombreArchivoWord = "";

        //        string rutaPdf = ConfigurationManager.AppSettings["rutaPdf"].ToString();

        //        objOrdServ.CorreoEnvio = meCorreoEnvio.Text;
        //        objOrdServ.CopiaCorreo = meCopiaCorreo.Text;
        //        objOrdServ.CopiaCorreoOculto = meCopiaCorreoOculto.Text;
        //        objOrdServ.AsuntoCorreo = meAsuntoCorreo.Text;
        //        objOrdServ.TextoCorreo = recTextoCorreo.HtmlText;
        //        objOrdServ.Opcion = 1;

        //        result = oblOrdenServicio.ActualizarOrdenServicio(objOrdServ);

        //        if (result == "OK")
        //        {
        //            if (objOrdServ.CorreoEnvio.ToString() != "")
        //            {
        //                try
        //                {
        //                    if (objOrdServ.AsuntoCorreo == null || objOrdServ.AsuntoCorreo == "")
        //                    {
        //                        objOrdServ.AsuntoCorreo = "File " + objOrdServ.File + " - Orden de Servicio N° " + objOrdServ.NroOrdServicio;
        //                        objOrdServ.TextoCorreo = @"<HTML><BODY><p style='font-family:century gothic;'>Estimado:</p>
        //                            <p style='font-family:century gothic;'>Se adjunta documento de orden de servicio.</p>
        //                            <p style='font-family:century gothic;'>Gracias</p>
        //                            <p style='font-family:century gothic;'><b> " + user.Descripcion + "</b></p></BODY></HTML>";
        //                    }

        //                    objOrdServ.NombreArchivo = objOrdServ.TipoFile + objOrdServ.AnhoFile + objOrdServ.NumeroFile + "-O" + objOrdServ.NroOrdServicio;

        //                    nombreArchivo = objOrdServ.NombreArchivo + ".pdf";
        //                    nombreImagen = objOrdServ.NombreArchivo + ".jpg";
        //                    nombreArchivoExcel = objOrdServ.NombreArchivo + ".xlsx";
        //                    nombreArchivoWord = objOrdServ.NombreArchivo + ".docx";

        //                    RptOrdenServicio report = new RptOrdenServicio();
        //                    ReportPrintTool printTool = new ReportPrintTool(report);
        //                    report.RequestParameters = false;
        //                    report.Parameters["NroOrdServicio"].Value = Convert.ToInt32(objOrdServ.NroOrdServicio);
        //                    report.Parameters["NroOrdServicio"].Visible = false;

        //                    path = rutaPdf + nombreArchivo;
        //                    report.ExportToPdf(path);
        //                    report.Dispose();

        //                    Outlook.Application oApp;
        //                    oApp = new Outlook.Application();
        //                    Outlook.MailItem oMsg;
        //                    oMsg = oApp.CreateItem(Outlook.OlItemType.olMailItem);

        //                    oMsg.To = objOrdServ.CorreoEnvio;
        //                    oMsg.CC = objOrdServ.CopiaCorreo;
        //                    oMsg.BCC = objOrdServ.CopiaCorreoOculto;
        //                    oMsg.Subject = objOrdServ.AsuntoCorreo;
        //                    oMsg.HTMLBody = objOrdServ.TextoCorreo;

        //                    //oMsg.Display(true);                            

        //                    if (System.IO.File.Exists(path))
        //                    {
        //                        oMsg.Attachments.Add(path, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
        //                    }

        //                    if (System.IO.File.Exists(rutaPdf + nombreImagen))
        //                    {
        //                        oMsg.Attachments.Add(rutaPdf + nombreImagen);
        //                    }

        //                    if (System.IO.File.Exists(rutaPdf + nombreArchivoExcel))
        //                    {
        //                        oMsg.Attachments.Add(rutaPdf + nombreArchivoExcel);
        //                    }

        //                    if (System.IO.File.Exists(rutaPdf + nombreArchivoWord))
        //                    {
        //                        oMsg.Attachments.Add(rutaPdf + nombreArchivoWord);
        //                    }

        //                    oMsg.Send();

        //                    //actualiza datos 

        //                    objOrdServ.UserUpdate = user.IdUser;
        //                    objOrdServ.Opcion = 0;

        //                    result2 = oblOrdenServicio.ActualizarOrdenServicio(objOrdServ);
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                }
        //                finally
        //                {
        //                    if (System.IO.File.Exists(path))
        //                    {
        //                        // If file found, delete it    
        //                        System.IO.File.Delete(path);
        //                    }

        //                    if (System.IO.File.Exists(rutaPdf + nombreImagen))
        //                    {
        //                        System.IO.File.Delete(rutaPdf + nombreImagen);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Por favor ingrese el correo de destino", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }


        //            if (result2 == "OK")
        //            {
        //                MessageBox.Show("Orden de servicio enviada satisfactoriamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }

        //    }

        //}



    }
}