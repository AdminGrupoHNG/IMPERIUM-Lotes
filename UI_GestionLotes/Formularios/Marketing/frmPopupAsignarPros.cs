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
using static UI_GestionLotes.Formularios.Clientes_Y_Proveedores.Clientes.frmBusquedaVendedor;

namespace UI_GestionLotes.Formularios.Marketing
{
    public enum AsignarPros
    {
        Prospecto = 1,
        Proyecto = 2
    }
    
    public partial class frmPopupAsignarPros : UI_GestionLotes.Tools.SimpleModalForm
    {
        public AsignarPros MiAccion = AsignarPros.Prospecto;
        private readonly UnitOfWork unit;
        public Boolean Aceptar = false;
        
        public frmPopupAsignarPros(int opcion = 1)
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

        private void frmPopupAsignarPros_Load(object sender, EventArgs e)
        {
            if (MiAccion != AsignarPros.Prospecto) this.Size = new Size(335, 224);
        }

        private void frmPopupAsignarPros_KeyDown(object sender, KeyEventArgs e)
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