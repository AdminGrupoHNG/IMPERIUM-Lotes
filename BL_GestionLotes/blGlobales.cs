using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Mail;

using DevExpress.Utils.Taskbar.Core;
using DevExpress.Utils.Taskbar;
using System.Data;
using DevExpress.XtraTreeList;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList.Nodes;

using BE_GestionLotes;
using System.ComponentModel;
using DA_GestionLotes;
using DevExpress.XtraGrid;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace BL_GestionLotes
{
    public class blGlobales
    {
        private readonly blSistema blSis;
        private readonly blEncrypta blCrypt;
        private readonly eColor Colores;
        public blGlobales(daSQL sql, string key)
        {
            blCrypt = new blEncrypta(key);
            blSis = new blSistema(sql);
            Colores = ObtenerColores();
        }
        public eColor ObtenerColores() { return new eColor(GetColor("colorVerde"), GetColor("colorPlomo"), GetColor("colorEventRow"), GetColor("colorFocus")); }
        private Color GetColor(string argb)
        {

            var intArgb = blCrypt.Desencrypta(ConfigurationManager.AppSettings[blCrypt.Encrypta(argb)]).ToString().Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            if (intArgb == null) return new Color();
            return Color.FromArgb(intArgb[0], intArgb[1], intArgb[2]);
        }

        private Random random = new Random();
        public string ObtenerTokenString()
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(33, 77))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string ObtenerTokenString(int start, int end)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(start, end))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string ObtenerKeyString(int start, int end)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuwxyz0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(start, end))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void pKeyDown(TextEdit sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                sender.Text = "";
            }
        }
        public void keyPressOnlyNumber(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        public string pKeyPress(TextEdit sender, KeyPressEventArgs e)
        {
            string sAux = "";
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 45 || e.KeyChar == 32))
            {
                sAux = e.KeyChar.ToString();
                if (e.KeyChar == 45 || e.KeyChar == 32) { sAux = ""; }
                e.Handled = true;
            }
            return sAux;
        }

        public void Pintar_CabeceraColumnas(ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return;
            System.Drawing.Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);
            e.Graphics.FillRectangle(new SolidBrush(Colores.Verde), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }

        public void Pintar_CabeceraColumnasBandHeader(BandHeaderCustomDrawEventArgs e)
        {
            if (e.Band == null) return;
            System.Drawing.Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);
            e.Graphics.FillRectangle(new SolidBrush(Colores.Verde), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }

        public void Pintar_EstiloGrilla(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.Columns["flg_activo"] != null)
            {
                string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["flg_activo"]);
                if (estado == "NO") e.Appearance.ForeColor = Color.Red;
            }
            view.Appearance.EvenRow.BackColor = Colores.EventRow;
            view.Appearance.FocusedRow.BackColor = Colores.Focus;
            view.Appearance.FocusedRow.FontStyleDelta = FontStyle.Bold; view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedCell.BackColor = Colores.Focus;
            view.Appearance.FocusedCell.FontStyleDelta = FontStyle.Bold; view.Appearance.FocusedCell.ForeColor = Color.Black;
            view.Appearance.HideSelectionRow.BackColor = Colores.Focus;
            view.Appearance.HideSelectionRow.FontStyleDelta = FontStyle.Bold; view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.SelectedRow.BackColor = Colores.Focus;
            view.Appearance.SelectedRow.FontStyleDelta = FontStyle.Bold; view.Appearance.SelectedRow.ForeColor = Color.Black;
        }

        public void ConfigurarGridView_TreeStyle(GridControl gc, GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            gv.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gv.OptionsView.EnableAppearanceEvenRow = true;
            gv.OptionsView.ShowAutoFilterRow = false;
            gv.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gv.OptionsView.ShowColumnHeaders = false;
            //
            gv.OptionsFind.AllowFindPanel = true;
            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsBehavior.Editable = false;
            //dgvTrabajador.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gv.ColumnPanelRowHeight = 35;
            gv.Appearance.HeaderPanel.ForeColor = Color.White;
            gv.Appearance.HeaderPanel.Options.UseForeColor = true;
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            //gv.Appearance.GroupRow.BackColor = Color.White;
            //
            gc.UseEmbeddedNavigator = true;
            gc.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Append.Visible = false;

            gv.ExpandAllGroups();
        }
        public void ConfigurarGridView_ClasicStyle(GridControl gc, GridView gv, bool editable = false, bool showGroupPanel = false, bool allowFindPanel = false, bool ShowAutoFilterRow = true)
        {
            gv.OptionsView.ShowGroupPanel = showGroupPanel;
            //gv.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            //gv.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gv.OptionsView.EnableAppearanceEvenRow = true;
            gv.OptionsView.ShowAutoFilterRow = ShowAutoFilterRow;
            //gv.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gv.OptionsSelection.MultiSelect = true; ;
            //gv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gv.OptionsView.ShowColumnHeaders = true;
            //
            gv.OptionsFind.AllowFindPanel = allowFindPanel;
            gv.OptionsFind.AlwaysVisible = allowFindPanel;
            gv.OptionsBehavior.Editable = editable;
            //dgvTrabajador.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gv.ColumnPanelRowHeight = 35;
            gv.Appearance.HeaderPanel.ForeColor = Color.White;
            gv.Appearance.HeaderPanel.Options.UseForeColor = true;
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            //gv.Appearance.GroupRow.BackColor = Color.White;
            //
            gc.UseEmbeddedNavigator = true;
            gc.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Append.Visible = false;

            gv.ExpandAllGroups();

            gv.RowStyle += Gv_RowStyle;
            gv.CustomDrawColumnHeader += Gv_CustomDrawColumnHeader;
        }

        private void Gv_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            Pintar_CabeceraColumnas(e);
        }

        private void Gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            Pintar_EstiloGrilla(sender, e);
        }

        public void Abrir_SplashScreenManager(Type splashFormType, string sTitulo, string sSubTitulo = "Cargando...")
        {
            if (SplashScreenManager.Default == null)
            {
                SplashScreenManager.ShowForm(splashFormType);
                string[] oDatos = { sTitulo, sSubTitulo };
                SplashScreenManager.Default.SendCommand(SkinSplashScreenCommand.UpdateLoadingText, oDatos);
            }
            else
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        //public Boolean EnviarCorreoElectronico_Outlook(string mailDirection, string mailSubject, string mailContent)
        //{
        //    try
        //    {
        //        var oApp = new Microsoft.Office.Interop.Outlook.Application();
        //        Microsoft.Office.Interop.Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
        //        var f = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
        //        System.Threading.Thread.Sleep(1000);

        //        var mailItem = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
        //        mailItem.Subject = mailSubject;
        //        mailItem.HTMLBody = mailContent;
        //        mailItem.To = mailDirection;
        //        mailItem.Send();
        //        MessageBox.Show("El correo fue enviado, revise su bandeja de entrada.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show("El correo no pudo ser enviado.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }
        //    finally
        //    {
        //    }
        //    return true;
        //}

        public Boolean EnviarCorreoElectronico_SMTP(string sDestinatario, string sAsunto, string sCuerpo, string RutasAdjunto = "")
        {
            List<BE_GestionLotes.eSistema> eSist = blSis.Obtener_ParamterosSistema<BE_GestionLotes.eSistema>(11);
            String sCredencialUsuario = "", sCredencialClave = "";
            if (eSist.Count > 0)
            {
                sCredencialUsuario = eSist[0].dsc_clave;
                sCredencialClave = eSist[0].dsc_valor;
            }

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.To.Add(new System.Net.Mail.MailAddress(sDestinatario));
            correo.From = new System.Net.Mail.MailAddress(sCredencialUsuario);
            correo.Subject = sAsunto;
            correo.Body = sCuerpo;
            correo.IsBodyHtml = false;

            using (var client = new System.Net.Mail.SmtpClient("smtp.office365.com", 587))
            {
                client.Credentials = new NetworkCredential(sCredencialUsuario, sCredencialClave);
                client.EnableSsl = true;
                try
                {
                    client.Send(correo);
                    MessageBox.Show("El correo fue enviado, revise su bandeja de entrada.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El correo no pudo ser enviado.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        public Boolean EnviarCorreoElectronico_SMTP(string sDestinatario, string sCopia, string sAsunto,
            string sCuerpo, List<BE_GestionLotes.eSistema> credenciales,
            string RutasAdjunto = "", string[] ArchivosAdjunto = null)
        {

            String sCredencialUsuario = "", sCredencialClave = "";
            if (credenciales.Count > 0)
            {
                sCredencialUsuario = credenciales[0].dsc_clave;
                sCredencialClave = credenciales[0].dsc_valor;
            }

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            if (sDestinatario.Length > 0)
            {
                if (sDestinatario.Contains(","))
                {
                    string[] des = sDestinatario.Split(',');
                    foreach (string d in des)
                    {
                        correo.To.Add(new System.Net.Mail.MailAddress(d));
                    }
                }
                else
                {
                    correo.To.Add(new System.Net.Mail.MailAddress(sDestinatario.Trim()));
                }
            }

            correo.From = new System.Net.Mail.MailAddress(sCredencialUsuario);



            if (sCopia.Length > 0)
            {
                if (sCopia.Contains(","))
                {
                    string[] cc = sCopia.Split(',');
                    foreach (string c in cc)
                    {
                        correo.CC.Add(new System.Net.Mail.MailAddress(c));
                    }
                }
                else
                {
                    correo.CC.Add(new System.Net.Mail.MailAddress(sCopia));
                }
            }


            correo.Subject = sAsunto;
            correo.Body = sCuerpo;
            correo.IsBodyHtml = false;

            //correo.Attachments ;
            string attachmentsPath = RutasAdjunto;// @"C:\Himasagar";
            if (Directory.Exists(attachmentsPath))
            {
                string[] files = Directory.GetFiles(attachmentsPath);

                foreach (string adjuntar in ArchivosAdjunto)
                {
                    foreach (string fileName in files)
                    {
                        var fname = Path.GetFileName(fileName);

                        if (fname.Trim().Equals(adjuntar.ToString().Trim()))
                        {
                            //bool isRunning = Process.GetProcessesByName(fname.Replace(Path.GetExtension(fname), "")).FirstOrDefault(p => p.MainModule.FileName.StartsWith(fileName)) != default(Process);
                            var probando = false;
                            if (Path.GetExtension(fileName) == ".xlsx"|| Path.GetExtension(fileName) == ".xls" || Path.GetExtension(fileName) == ".doc" || Path.GetExtension(fileName) == ".docx")
                            {
                                probando = IsFileLocked(new FileInfo(fileName));
                            }
                            if (probando)
                            {
                                SplashScreenManager.CloseForm(false); MessageBox.Show($"Error al enviar, el archivo {fname} se encuentra abierto.", "Configuración de Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false;
                            }
                            Attachment file = new Attachment(fileName);
                            correo.Attachments.Add(file);
                            break;
                        }
                    }
                }
            }



            using (var client = new System.Net.Mail.SmtpClient("smtp.office365.com", 587))
            {
                client.Credentials = new NetworkCredential(sCredencialUsuario, sCredencialClave);
                client.EnableSsl = true;
                try
                {

                    client.Send(correo);
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show("El correo fue enviado, revise su bandeja de entrada.", "Configuración de Correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex);
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show($"El correo no pudo ser enviado.\n{ex.Message}", "Configuración de Correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            //return true;
        }

        public bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked
            return false;
        }

        public void CargarCombosGridLookup(DataTable tabla, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false)
        {
            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        public void CargarCombosGridLookup(DataTable tabla, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", bool valorDefecto = false)
        {
            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }





        public DevExpress.XtraTreeList.TreeList CargaTreeList(DevExpress.XtraTreeList.TreeList oTreeList, List<eCampanha> ListCampania)
        {
            //oTreeList.Appearance.Row.BackColor = Color.Transparent;
            //oTreeList.Appearance.Empty.BackColor = Color.Transparent;
            //oTreeList.BackColor = Color.Transparent;
            //oTreeList.CheckBoxFieldName = "Checked";
            //oTreeList.TreeViewFieldName = "Name";
            //oTreeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            //oTreeList.OptionsBehavior.Editable = false;
            //oTreeList.OptionsBehavior.ReadOnly = true;
            //oTreeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            var dataSource = GenerateDataSource_TreeList(ListCampania);
            oTreeList.DataSource = dataSource;
            //oTreeList.ForceInitialize();
            //oTreeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            //oTreeList.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            ////for (int x = 0; x <= ListCampania.Count - 1; x++)
            ////{
            ////    oTreeList.Nodes[0].Nodes[x].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            ////}
            oTreeList.ExpandAll();

            return oTreeList;
        }

        BindingList<Option_TreeList> GenerateDataSource_TreeList(List<eCampanha> ListCampania)
        {
            BindingList<Option_TreeList> _options = new BindingList<Option_TreeList>();
            foreach (eCampanha obj in ListCampania)
            {
                string nodo_padre = obj.cod_nodo_padre;
                string codigo = obj.cod_nodo;
                string descripcion = obj.dsc_nodo;

                if (codigo == nodo_padre)
                    _options.Add(new Option_TreeList() { ParentID = codigo, ID = codigo, Name = descripcion, Checked = false });
                else
                    _options.Add(new Option_TreeList() { ParentID = nodo_padre.ToString(), ID = codigo, Name = descripcion, Checked = false });
            }

            return _options;
        }

        public void Actualizar_appSettings(eGlobales globales)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;
                        switch (blCrypt.Desencrypta(node.Attributes[0].Value))
                        {
                            case "conexion": node.Attributes[1].Value = blCrypt.Encrypta(globales.Entorno); break;
                                //case "ServidorLOCAL": node.Attributes[1].Value = unit.Encriptacion.Encrypta(txtServidorLOCAL.Text); break;
                                //case "ServidorREMOTO": node.Attributes[1].Value = unit.Encriptacion.Encrypta(txtServidorREMOTO.Text); break;
                                //case "BBDD": node.Attributes[1].Value = unit.Encriptacion.Encrypta(txtBaseDatos.Text); break;
                                //case "FormatoFecha": node.Attributes[1].Value = unit.Encriptacion.Encrypta(grdbFormatoFecha.SelectedIndex == 0 ? "DD-MM-YYYY" : "YYYY-MM-DD"); break;
                                //case "UltimaEmpresa": node.Attributes[1].Value = blCrypt.Encrypta(lkpEmpresaInicio.EditValue.ToString()); break;
                                //case "SeparadorListas": node.Attributes[1].Value = unit.Encriptacion.Encrypta(CultureInfo.CurrentCulture.TextInfo.ListSeparator); break;
                        }
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }
        public enum TipoMensaje
        {
            OK = 0,
            Error = 1,
            Alerta = 2,
            YesNo = 3,
        }
        public DialogResult Mensaje(TipoMensaje tipo, string mensaje = "", string titulo = "")
        {

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            switch (tipo)
            {
                case TipoMensaje.OK:
                    break;
                case TipoMensaje.Error:
                    {
                        icon = MessageBoxIcon.Error;
                        break;
                    }
                case TipoMensaje.Alerta:
                    {
                        icon = MessageBoxIcon.Warning;
                        break;
                    }
                case TipoMensaje.YesNo:
                    {
                        buttons = MessageBoxButtons.YesNo;
                        icon = MessageBoxIcon.Question;
                        break;
                    }
            }

            return XtraMessageBox.Show(mensaje, titulo, buttons, icon);
        }

        public DialogResult Mensaje(bool errorSuccess, string mensaje = "", string titulo = "")
        {

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            switch (errorSuccess)
            {
                case false:
                    {
                        icon = MessageBoxIcon.Error;
                        break;
                    }
            }

            return XtraMessageBox.Show(mensaje, titulo, buttons, icon);
        }


        public PropertyInfo[] GetPropertyInfoArray<T>()
        {
            PropertyInfo[] props = null;
            try
            {
                Type type = typeof(T);
                object obj = Activator.CreateInstance(type);
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            catch (Exception)
            { }
            return props;
        }

        class Option_TreeList : INotifyPropertyChanged
        {
            public string ParentID { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            bool? checkedCore = false;
            public event PropertyChangedEventHandler PropertyChanged;
            public bool? Checked
            {
                get { return checkedCore; }
                set
                {
                    if (checkedCore == value)
                        return;
                    checkedCore = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        public string GetAppVariableValor(string appVariable)
        {
            var path = blCrypt.Desencrypta(ConfigurationManager.AppSettings[blCrypt.Encrypta(appVariable)]).ToString();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);



            return path;
        }






    }





}
