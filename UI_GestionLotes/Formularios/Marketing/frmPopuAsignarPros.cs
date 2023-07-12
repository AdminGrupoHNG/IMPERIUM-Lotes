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

namespace UI_GestionLotes.Formularios.Marketing
{
    public partial class frmPopuAsignarPros : UI_GestionLotes.Tools.SimpleModalForm
    {
        private readonly UnitOfWork unit;
        public Boolean Aceptar = false;
        public frmPopuAsignarPros(int opcion = 1)
        {
            InitializeComponent();
            TitleBackColor = Program.Sesion.Colores.Verde;
            btnAsignar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            unit = new UnitOfWork();
            if(opcion == 2)
            {
                btnEnviar.Appearance.BackColor = Program.Sesion.Colores.Verde;
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }


        private void frmPopuAsignarPros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            Aceptar = true;
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Aceptar = true;
            this.Close();
        }
    }
}