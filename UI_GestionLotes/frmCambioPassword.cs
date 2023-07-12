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
using BE_GestionLotes;

namespace UI_GestionLotes
{
    public partial class frmCambioPassword : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public eUsuario user = new eUsuario();
        public bool PasswordCambiado = false;
        public frmCambioPassword()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmCambioPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardarPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPasswordAntiguo.Text == "" || txtPasswordNuevo.Text == "" || txtPasswordNuevoReconfirmar.Text == "")
                {
                    MessageBox.Show("Los campos no pueden estar vacíos, por favor completar.", "Cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtPasswordAntiguo.Text != user.dsc_clave)
                {
                    MessageBox.Show("La contraseña antigua es inválida.", "Cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtPasswordAntiguo.Text == txtPasswordNuevo.Text)
                {
                    MessageBox.Show("La contraseña nueva debe ser diferente a la contraseña antigua.", "Cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtPasswordNuevo.Text != txtPasswordNuevoReconfirmar.Text)
                {
                    MessageBox.Show("La contraseña nueva y la contraseña de reconfirmación no coinciden.", "Cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    user.dsc_clave = txtPasswordNuevo.Text.ToUpper();
                    user.flg_noexpira = "NO";
                    user.fch_cambioclave = DateTime.Today.AddDays(user.num_dias_cambio_contraseña);
                    string result = unit.Usuario.Actualizar_ClaveUsuario(user);
                    if(result == "OK")
                    {
                        XtraMessageBox.Show("Se guardó la nueva contraseña de manera satisfactoria.", "Cambiar contraseña", MessageBoxButtons.OK);
                        PasswordCambiado = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar contraseña.", "Cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        PasswordCambiado = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}