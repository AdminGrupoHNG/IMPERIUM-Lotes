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
using UI_GestionLotes.Formularios.Lotes;

namespace UI_GestionLotes.Formularios.Marketing
{
    public partial class frmAlerta : UI_GestionLotes.Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        frmListadoProspecto frmHandler;
        //public DateTime dtTimeFechaEvendo;
        public int num_minutos = 0;
        public frmAlerta()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            TitleBackColor = Program.Sesion.Colores.Verde;
            btnGuardar.Appearance.BackColor = Program.Sesion.Colores.Verde;
        }
        internal frmAlerta(frmListadoProspecto frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }
        private void frmAlerta_Load(object sender, EventArgs e)
        {
            unit.Campanha.CargaCombosLookUp("Minutos", lkpMinutos, "cod_minutos", "dsc_minutos", "NOT0006");


           // HNGMessageBox.Show("MENSAJE", "TITULO", TypeMessage.Warning);
        }

        private void lkpMinutos_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMinutos.EditValue != null)
            {
                //MessageBox.Show("" + lkpLote.Text + " " + lkpLote.EditValue.ToString());

                LookUpEdit lookUp = sender as LookUpEdit;
                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                // Assign the row's Picture field value to the PictureEdit control
                if (dataRow != null)
                {
                        num_minutos = Convert.ToInt32(dataRow["num_minutos"]);
                    
                }

            }
        }
        private string validarCampos()
        {
            if(lkpMinutos.EditValue == null)
            {
                lkpMinutos.ShowPopup();
                return "Debe seleccionar los minutos.";
            }
            //if(dtTimeFechaEvendo.AddMinutes(-num_minutos) < DateTime.Now)
            //{
            //    lkpMinutos.ShowPopup();
            //    return "Debe seleccionar minutos sin pasar la hora actual.";
            //}
            return null;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = validarCampos();
            if (mensaje == null)
            {
                if (frmHandler != null)
                {
                    frmHandler.validar_alerta = 1;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(mensaje, "Notificación de eventos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (frmHandler != null)
            {
                frmHandler.validar_alerta = 0;
            }
            this.Close();
        }

        private void picCampana_Click(object sender, EventArgs e)
        {
            lkpMinutos.ShowPopup();
        }
    }
}
