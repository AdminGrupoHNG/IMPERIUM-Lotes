using BE_GestionLotes;
using DevExpress.Utils.Menu;
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
using UI_GestionLotes.Formularios.Documento;
using static BL_GestionLotes.blGlobales;

namespace UI_GestionLotes.Formularios.FormatoDocumentos
{
    public partial class frmFormatoMD_ModalVincular : Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        frmFormatoMD_General _frmHandler;
        private string _codEmpresa = string.Empty;
        public frmFormatoMD_ModalVincular(frmFormatoMD_General frmHandler)
        {
            InitializeComponent();
            unit = new UnitOfWork();
            ConfigurarFormulario();
            _frmHandler = frmHandler;
        }
        private void ConfigurarFormulario()
        {
            gcListadoFormatos.Cursor = Cursors.Default;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoFormatos, gvListadoFormatos);
            gvListadoFormatos.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoFormatos.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            //gvListadoFormatos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvListadoFormatos.OptionsView.EnableAppearanceEvenRow = false;
            //gvListadoFormatos.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gvListadoFormatos.OptionsSelection.MultiSelect = true;

            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoEmpresas, gvListadoEmpresas);
            gvListadoEmpresas.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoEmpresas.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoEmpresas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //gvListadoFormatos.OptionsView.EnableAppearanceEvenRow = false;
            gvListadoEmpresas.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gvListadoEmpresas.OptionsSelection.MultiSelect = true;


            this.TitleBackColor = Program.Sesion.Colores.Verde;
        }
        internal void CargarDatos(List<eFormatoMDGeneral_Tree> formatos, string codEmpresa = "")
        {
            _codEmpresa = codEmpresa;
            CargarEmpresa();
            CargarFormatos(formatos);
        }
        private void CargarEmpresa()
        {
            var objEmpresa = Program.Sesion.EmpresaList
                .Where(e => e.cod_empresa.Contains(_codEmpresa)).ToList();
            bsListadoEmpresas.DataSource = null;
            if (objEmpresa != null && objEmpresa.Count > 0)
            {
                bsListadoEmpresas.DataSource = objEmpresa;
                gvListadoEmpresas.RefreshData();
                gvListadoEmpresas.ExpandAllGroups();
                gvListadoEmpresas.SelectAll();
            }
            // else {  this.Close(); }
        }
        private void CargarFormatos(List<eFormatoMDGeneral_Tree> formatos)
        {
            bsListadoFormatos.DataSource = null;
            if (formatos != null && formatos.Count > 0)
            {
                bsListadoFormatos.DataSource = formatos;
                gvListadoFormatos.RefreshData();
                gvListadoFormatos.ExpandAllGroups();
            }
            // else { this.Close(); }
        }

        /// <summary>
        /// Obtiene los códigos de formatos y códigos de empresas para vincular en bloques
        /// </summary>
        private void VincularDocumentacion()
        {
            var objFormato = bsListadoFormatos.DataSource as List<eFormatoMDGeneral_Tree>;
            if (objFormato == null || objFormato.Count == 0)
            {
                unit.Globales.Mensaje(false, "No existe formatos para vincular..", "Vinculación de Formatos");
                return;
            }

            string split_empresa = "delete";
            foreach (var nRow in gvListadoEmpresas.GetSelectedRows())
            {
                var objEmp = gvListadoEmpresas.GetRow(nRow) as eProveedor_Empresas;
                split_empresa += $",{objEmp.cod_empresa}";
            }
            split_empresa = split_empresa.Replace("delete,", "").Trim();
            split_empresa = split_empresa.Replace("delete", "").Trim();


            if (split_empresa == null || split_empresa.Length == 0)
            {
                unit.Globales.Mensaje(false, "Debes seleccionar almenos una Empresa para la vinculación de Formatos.", "Vinculación de Formatos");
                return;
            }


            var sms = unit.Globales.Mensaje(TipoMensaje.YesNo, $"Estas seguro de vincular los documentos a la empresa seleccionada?", "Vinculación de Formatos");
            if (sms == DialogResult.Yes)
            {

                string split_documento = string.Empty;
                split_documento = new Tools.TreeListHelper()
                    .ObtenerValoresConcatenadoDeLista<eFormatoMDGeneral_Tree>(objFormato, "cod_formatoMD_general");

                var result = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eSqlMessage>(
                    new BE_GestionLotes.pQFormatD()
                    {
                        Opcion = 5,
                        Cod_empresaSplit = split_empresa,
                        Cod_formatoMD_generalSplit = split_documento,
                        Cod_solucion = Program.Sesion.Cod_solucion
                    });


                //unit.Globales.Mensaje(result[0].IsSuccess, result[0].Outmessage, "Vinculación de Documentos y Empresas");

                if (result[0].IsSuccess)
                {
                    this.DialogResult = DialogResult.OK;
                    if (!string.IsNullOrWhiteSpace(_codEmpresa)) _frmHandler.Dispose();
                    this.Close();
                }
            }
        }




        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            VincularDocumentacion();
        }

        private void gvListadoFormatos_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.RowHandle >= 0 && e.Button == MouseButtons.Right) popupMenu1.ShowPopup(MousePosition);
        }

        private void gvListadoFormatos_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            var row = gvListadoFormatos.GetFocusedRow();
            if (row != null)
            {
                e.Menu.Items.Add(new DXMenuItem("Retirar formato", new EventHandler(QuitarFormato)));
            }
        }
        private void QuitarFormato(object sender, EventArgs e)
        {
            var format = gvListadoFormatos.GetFocusedRow() as eFormatoMDGeneral_Tree;
            if (format != null)
            {
                var Items = bsListadoFormatos.DataSource as List<eFormatoMDGeneral_Tree>;
                int index = Items.FindIndex((idex) => idex.cod_formatoMD_general.Equals(format.cod_formatoMD_general));
                Items.RemoveAt(index);

                bsListadoFormatos.DataSource = null;
                bsListadoFormatos.DataSource = Items;
                gvListadoFormatos.RefreshData();
                gvListadoFormatos.ExpandAllGroups();
            }
        }
    }
}