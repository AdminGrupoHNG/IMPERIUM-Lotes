using DevExpress.CodeParser;
using DevExpress.DirectX.Common.Direct2D;
using DevExpress.XtraEditors;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UI_GestionLotes.Formularios.Shared.frmBusquedas;

namespace UI_GestionLotes.Tools
{
    public partial class ExcelSplash : Tools.SimpleModalForm
    {
        private readonly ReadExcel _readExcel;
        internal DataTable DtExcel = null;
        private string _path;
        public ExcelSplash(string path)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            _readExcel = new ReadExcel();
            _path = path;
            lblPath.Text = $"Ruta: {(path.Length > 46 ? path.Substring(0, 46) : path)}";
            lblFile.Text = $"Archivo exportado: {Path.GetFileName(path)}";
        }
        private void Importar()
        {
            var result = _readExcel.ToDataTable(_path);
            if (result != null && result.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                DtExcel = result;
            }
        }

        private void ExcelSplash_Shown(object sender, EventArgs e) { StartProcess(); }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Importar();
            if (worker.CancellationPending == true) { e.Cancel = true; }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) { }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true) { }
            else if (e.Error != null) { /*new Tools.Message().MessageBox(e.Error.Message);*/ }
            else
            {
                this.DialogResult = DialogResult.OK; CancelProcess();
                this.Close();
            }
        }
        private void StartProcess() { if (worker.IsBusy != true) { worker.RunWorkerAsync(); } }
        private void CancelProcess() { worker.CancelAsync(); }
    }
}