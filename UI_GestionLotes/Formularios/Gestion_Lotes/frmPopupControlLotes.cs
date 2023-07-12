using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    public partial class frmPopupControlLotes : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        DataTable tabla = new DataTable();
        frmListadoControlLotes frmHandler;
        public string cod_proyecto = "";
        public string cod_etapa = "";
        public string cod_manzana = "";
        

        private void frmPopupControlLotes_Load(object sender, EventArgs e)
        {
            btnAgregar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            CargarCombos();

        }

        public frmPopupControlLotes()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmPopupControlLotes(frmListadoControlLotes frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();

        }
        private void txtManzana_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (txtManzana.Text.Length > 0)
            {
                txtManzana.Text = txtManzana.Text.Substring(0, txtManzana.Text.Count() - 1);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string validar  = validarCampos();
            if(validar != null) { MessageBox.Show(validar); return; }
            this.Close();
            int num_linea = 0;
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                if (tabla.Rows[i]["cod_etapa"].ToString() == lkpEtapa.EditValue.ToString())
                {
                    num_linea = Convert.ToInt32(tabla.Rows[i]["num_linea"]);

                }
            }
            frmHandler.CargarListadoManzanas(num_linea, lkpEtapa.EditValue.ToString(), txtManzana.Text, Convert.ToInt32(txtCantidad.Text));
            

            //DataTable oLoteEtap = tabla.Find(x => x.cod_etapa == lkpEtapa.EditValue.ToString());
            

        }


        public string validarCampos()
        {
            if (lkpEtapa.EditValue == null)
            {
                lkpEtapa.ShowPopup();
                return "Debe seleccionar la etapa";
            }

            //if (!Regex.IsMatch(txtManzana.Text.Trim(), "^[a-zA-Z0-9]*$"))
            //{
            //    txtManzana.Focus();
            //    return "Debe ingresar la Manzana";
            //}

            return null;
        }

        private void CargarCombos()
        {
            CargarCombosGridLookup("EtapaXProyecto", lkpEtapa, "cod_etapa", "dsc_descripcion", "", "", valorDefecto: true);
            
        }
        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            
            tabla = unit.Proyectos.ObtenerListadoGridLookup(nCombo, Program.Sesion.Usuario.cod_usuario,cod_proyecto);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            
            
        }
    }
}