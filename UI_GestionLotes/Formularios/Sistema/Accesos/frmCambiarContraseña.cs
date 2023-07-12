using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using BE_GestionLotes;
using BL_GestionLotes;

namespace UI_GestionLotes.Formularios.Sistema.Accesos
{
    public partial class frmCambiarContraseña : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;

        public frmCambiarContraseña()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmCambiarContraseña_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Program.Sesion.Usuario.flg_cambiar_clave != null) { if (Program.Sesion.Usuario.flg_cambiar_clave == "SI") { Application.Exit(); } }
        }

        private void frmCambiarContraseña_Load(object sender, EventArgs e)
        {
           
        }

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            eUsuario obj = new eUsuario();
            //string ClaveAntigua = DesEncriptar(txtContraseñaActual.Text);
            //}
            obj = unit.Usuario.ObtenerUsuario<eUsuario>(2, Program.Sesion.Usuario.cod_usuario);

            if (txtContraseñaActual.Text == "")
            {
                MessageBox.Show("Insertar la contraseña actual", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); ; txtContraseñaActual.Focus(); return;
            }
            if (txtContraseñaActual.Text.ToUpper() != obj.dsc_clave.ToUpper())
            {
                MessageBox.Show("La contraseña actual no es correcta", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtContraseñaActual.Text = ""; txtContraseñaActual.Focus(); return;
            }
            if (txtNuevaContraseña.Text == "")
            {
                MessageBox.Show("Insertar una nueva contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNuevaContraseña.Focus(); return;
            }
            if (txtReconfirmarContraseña.Text == "")
            {
                MessageBox.Show("Falta confirmar su contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtReconfirmarContraseña.Focus(); return;
            }
            if (txtReconfirmarContraseña.Text != txtNuevaContraseña.Text)
            {
                MessageBox.Show("La nueva contraseña y la reconfirmación no coinciden", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtReconfirmarContraseña.Text = ""; txtReconfirmarContraseña.Focus(); return;
            }

            // string ClaveNueva = Encriptar(txtConfirmarClave.Text);
            obj.dsc_clave = txtReconfirmarContraseña.Text;
            Program.Sesion.Usuario = unit.Usuario.Guardar_Actualizar_Usuario<eUsuario>(obj, "Actualizar", Program.Sesion.Usuario.cod_usuario);
            MessageBox.Show("Contraseña Actualizada", "Modificación de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void frmCambiarContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}