using BE_GestionLotes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Gestion_Contratos
{
    public partial class frmReporteGeneral : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public eReportes camposContrato = new eReportes();
        public string descripcionArchivo = "", cod_empresa = "";
        public frmReporteGeneral()
        {
            InitializeComponent();
        }

        private void frmReporteGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void btnEditarFormato_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rchEditControl.ReadOnly == true)
            {
                rchEditControl.ReadOnly = false;
                return;
            }
            if (rchEditControl.ReadOnly == false)
            {
                rchEditControl.ReadOnly = true;
                return;
            }
        }

        private void btnEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmConfigCorreo frm = new frmConfigCorreo(this);
            frm.descripArchivo = descripcionArchivo;
            if (camposContrato.dsc_documento_contrato == null || camposContrato.dsc_documento_contrato.Trim() == "")
            {
                camposContrato.dsc_documento_contrato = rchEditControl.WordMLText;
            }
            rchEditControl.Refresh();
            frm.camposContrato = camposContrato;
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
        }
    }
}