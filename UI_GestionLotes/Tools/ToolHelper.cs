using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Tools;

namespace UI_GestionLotes.Tools
{
    public class ToolHelper
    {
        internal class Conversion
        {
            public string CapitalLetter(string stringValue)
            {
                stringValue = stringValue.ToLower();
                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                return myTI.ToTitleCase(stringValue);
            }
        }
        internal class MoveControls
        {
            private const int WM_NCLBUTTONDOWN = 0xA1;
            private const int HT_CAPTION = 0x2;

            [DllImportAttribute("user32.dll")]
            private static extern int SendMessage(IntPtr hWdn, int Msg, int wParam, int lParam);
            [DllImportAttribute("user32.dll")]
            private static extern bool ReleaseCapture();
            public static void Form(Form form)
            {
                ReleaseCapture();
                SendMessage(form.Handle, 0x112, 0xf012, 0);
            }
        }
        internal class Forms
        {
            public void Display(Form fHijo, PanelControl panel)
            {
                bool isOpen = false;
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == fHijo.Name)
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }
                if (isOpen == false)
                {
                    fHijo.TopLevel = false;
                    fHijo.Dock = DockStyle.Fill;
                    panel.Controls.Add(fHijo);
                    fHijo.Show();
                    fHijo.BringToFront();
                }
            }
            public DialogResult ShowDialog(Form childForm)
            {
                var bg = new Form();
                using (childForm)
                {
                    bg.StartPosition = FormStartPosition.CenterScreen;
                    bg.FormBorderStyle = FormBorderStyle.None;
                    bg.Opacity = 0.3d;
                    bg.BackColor = Color.Black;
                    //bg.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    //bg.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    bg.Width = Screen.FromControl(childForm).WorkingArea.Width;
                    bg.Height = Screen.FromControl(childForm).WorkingArea.Height;

                    bg.TopMost = false;
                    bg.ShowInTaskbar = false;
                    bg.Show();
                    childForm.TopMost = false;
                    childForm.Tag = "";
                    childForm.Owner = bg;
                    var result = childForm.ShowDialog();
                    bg.Dispose();
                    return result;
                }
            }
        }

        public class ShadowBox
        {
            #region Shadowing

            #region Fields

            //private readonly bool _isAeroEnabled = false;
            //private readonly bool _isDraggingEnabled = false;
#pragma warning disable IDE0051 // Remove unused private members
            private const int WM_NCHITTEST = 0x84;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
            private const int WS_MINIMIZEBOX = 0x20000;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
            private const int HTCLIENT = 0x1;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
            private const int HTCAPTION = 0x2;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
            private const int CS_DBLCLKS = 0x8;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
            private const int CS_DROPSHADOW = 0x00020000;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
            private const int WM_NCPAINT = 0x0085;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
            private const int WM_ACTIVATEAPP = 0x001C;
#pragma warning restore IDE0051 // Remove unused private members

            #endregion

            #region Structures

            [EditorBrowsable(EditorBrowsableState.Never)]
            public struct MARGINS
            {
                public int leftWidth;
                public int rightWidth;
                public int topHeight;
                public int bottomHeight;
            }

            #endregion

            #region Methods

            #region Public

            [DllImport("dwmapi.dll")]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

            [DllImport("dwmapi.dll")]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

            [DllImport("dwmapi.dll")]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static bool IsCompositionEnabled()
            {
                if (Environment.OSVersion.Version.Major < 6) return false;

                bool enabled;
                DwmIsCompositionEnabled(out enabled);

                return enabled;
            }

            #endregion

            #region Private

            [DllImport("dwmapi.dll")]
            private static extern int DwmIsCompositionEnabled(out bool enabled);

            [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
             );

            private bool CheckIfAeroIsEnabled()
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    int enabled = 0;
                    DwmIsCompositionEnabled(ref enabled);

                    return (enabled == 1) ? true : false;
                }
                return false;
            }

            #endregion

            #region Overrides

            public void Form(Form form)
            {
                var v = 2;
                DwmSetWindowAttribute(form.Handle, 2, ref v, 4);
                MARGINS margins = new MARGINS()
                {
                    bottomHeight = 1,
                    leftWidth = 0,
                    rightWidth = 0,
                    topHeight = 0
                };
                DwmExtendFrameIntoClientArea(form.Handle, ref margins);
            }

           

            #endregion

            #endregion

            #endregion
        }
    }
}

namespace UI_GestionLotes
{
    public enum TypeMessage { Success, Error, Warning, Question }
    internal class HNGMessageBox
    {
        public static DialogResult Show(string mensaje, string titulo, TypeMessage type)
        {
            MessageBoxIcon categoría = MessageBoxIcon.Question;
            switch (type)
            {
                case TypeMessage.Success:
                    categoría = MessageBoxIcon.Information;
                    break;
                case TypeMessage.Error:
                    categoría = MessageBoxIcon.Error;
                    break;
                case TypeMessage.Warning:
                    categoría = MessageBoxIcon.Warning;
                    break;
            }



            var f = new frmMessageBox(mensaje, titulo, categoría); return new ToolHelper.Forms().ShowDialog(f);
        }
        public static DialogResult Show(string mensaje, string titulo, string btn_text1, string btn_text2, string category_text = "")
        { var f = new frmMessageBox(mensaje, titulo, MessageBoxIcon.Question, btn_text1, btn_text2, category_text); return new ToolHelper.Forms().ShowDialog(f); }
    }
}

