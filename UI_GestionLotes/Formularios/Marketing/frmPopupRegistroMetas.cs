using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Marketing
{
    public enum AsignarPros2
    {
        Prospecto = 1,
        Proyecto = 2
    }
    public partial class frmPopupRegistroMetas : UI_GestionLotes.Tools.ModalForm
    {
        
        private readonly UnitOfWork unit;
        public Boolean Aceptar = false;
        public frmPopupRegistroMetas()
        {
            InitializeComponent();
            TitleBackColor = Program.Sesion.Colores.Verde;
            unit = new UnitOfWork();

        }

        private void prueba_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Aceptar = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}