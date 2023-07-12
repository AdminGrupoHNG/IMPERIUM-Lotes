using BE_GestionLotes;
using DevExpress.Utils.Drawing;
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

namespace UI_GestionLotes.Formularios.FormatoDocumentos
{
    public partial class frmFormatoMD_GrupoNuevo : Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        private string cod_formatoMD_grupo;
        public frmFormatoMD_GrupoNuevo()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            cod_formatoMD_grupo = string.Empty;
            configurar_formulario();
        }
        private void configurar_formulario()
        {
            TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoGrupoFormato, gvListadoGrupoFormato);
            //gvListadoGrupoFormato.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoGrupoFormato.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoGrupoFormato.OptionsView.EnableAppearanceEvenRow = false;
        }
        private void CargarListado()
        {
            var listGrupo = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Grupo>(
                new BE_GestionLotes.pQFormatD() { Opcion = 2, Cod_solucion = Program.Sesion.Cod_solucion });
            if (listGrupo != null && listGrupo.Count > 0)
            {
                bsListadoGrupoFormato.DataSource = listGrupo.OrderBy((j)=>j.num_jerarquia).ToList();
                gvListadoGrupoFormato.RefreshData();
            }

            Limpiar();
        }
        private void Guardar()
        {
            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGrupo<eSqlMessage>(AsignarValores(),1);
            if (result.IsSuccess)
            {
                CargarListado();
                Limpiar();
            }
            unit.Globales.Mensaje(result.IsSuccess, result.Outmessage);
        }
        private void Jeraquia(string accion)
        {
            var obj = gvListadoGrupoFormato.GetFocusedRow() as eFormatoMD_Grupo;
            if (obj == null) return;
            if (obj.num_jerarquia == 999) return; //El Personalizado tiene que ser el último.


            int value = 0;
            switch (accion)
            {
                case "subir":
                    {
                        value = +1;
                        break;
                    }
                case "bajar":
                    {
                        value = -1;
                        break;
                    }
            }
            obj.num_jerarquia = value;
            obj.cod_solucion = Program.Sesion.Cod_solucion;

            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGrupo<eSqlMessage>(obj,2);
            if (result.IsSuccess)
            {
                CargarListado();
                Limpiar();
            }


            //if (obj.num_jerarquia == 888) return;

            /* var list = bsListadoGrupoFormato.DataSource as List<eFormatoMD_Grupo>;
             if (list != null && list.Count > 0)
             {
                 // Si la jerarquía es uno o menos, imporsible bajar
                 if (obj.num_jerarquia <= 1 && accion.Equals("bajar")) return;

                 int value = 0;
                 switch (accion)
                 {
                     case "subir":
                         {
                             value = +555;
                             break;
                         }
                     case "bajar":
                         {
                             value = -555;
                             break;
                         }
                 }
                 this.cod_formatoMD_grupo = obj.cod_formatoMD_grupo;
                 var tempNuevaJerarquia = obj.num_jerarquia + value;
                 var tempActualJerarquia = obj.num_jerarquia;
                 // Cambiar el valor de la jerarquía seleccionada.
                 obj.num_jerarquia = 888;
                 obj.cod_solucion = Program.Sesion.Cod_solucion;
                 var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGrupo<eSqlMessage>(obj);
                 // Cambiar el valor de la jerarquía (abajo/arriba).
                 var newObj = list.FirstOrDefault((f) => f.num_jerarquia == tempNuevaJerarquia);
                 if (newObj == null) return;

                 newObj.num_jerarquia = tempActualJerarquia;
                 newObj.cod_solucion = Program.Sesion.Cod_solucion;
                 result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGrupo<eSqlMessage>(newObj);
                 // Establecer jerarquía correspondiente; a lo seleccionado.
                 obj.num_jerarquia = tempNuevaJerarquia;
                 result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGrupo<eSqlMessage>(obj);
                 if (result.IsSuccess)
                 {
                     CargarListado();
                     Limpiar();
                 }
                 //unit.Globales.Mensaje(result.IsSuccess, result.Outmessage);

             */
        }
        private void Limpiar()
        {
            this.cod_formatoMD_grupo = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtJerarquia.Text = "0";
            this.txtDescripcion.Select();

            var objList = bsListadoGrupoFormato.DataSource as List<eFormatoMD_Grupo>;
            if (objList != null && objList.Count > 0)
                txtJerarquia.Text = (objList.Where((j) => j.num_jerarquia < 777).Max((m) => m.num_jerarquia) + 1).ToString();
        }
        private eFormatoMD_Grupo AsignarValores()
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                txtDescripcion.Select();
                unit.Globales.Mensaje(false, "La descripción está vacío, intenta nuevamente.");
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtJerarquia.Text))
            {
                txtJerarquia.Select();
                unit.Globales.Mensaje(false, "El campo Jerarquía no tiene valores, intenta nuevamente.");
                return null;
            }

            return new eFormatoMD_Grupo()
            {
                cod_formatoMD_grupo = this.cod_formatoMD_grupo,
                dsc_formatoMD_grupo = this.txtDescripcion.Text,
                num_jerarquia = int.Parse(this.txtJerarquia.Text),
                cod_solucion = Program.Sesion.Cod_solucion,
            };
        }
        private void frmFormatoMD_GrupoNuevo_Load(object sender, EventArgs e)
        {
            CargarListado();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void txtJerarquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            unit.Globales.keyPressOnlyNumber(e);
        }

        private void txtJerarquia_Leave(object sender, EventArgs e)
        {
            if (txtJerarquia.Text.Length == 0) txtJerarquia.Text = "0";
        }

        private void gvListadoGrupoFormato_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var obj = gvListadoGrupoFormato.GetFocusedRow() as eFormatoMD_Grupo;
            if (obj == null) return;

            this.txtDescripcion.Text = obj.dsc_formatoMD_grupo;
            this.cod_formatoMD_grupo = obj.cod_formatoMD_grupo.ToString();
            this.txtJerarquia.Text = obj.num_jerarquia.ToString();
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            Jeraquia("bajar"); // arriba es abajo xD
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            Jeraquia("subir");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            //btnAdd.Appearance.BackColor = Color.FromArgb(89, 139, 125);
        }

        private void gvListadoGrupoFormato_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            //if (e.Column == null) return;
            //System.Drawing.Rectangle rect = e.Bounds;
            //rect.Inflate(-1, -1);
            //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 150, 150)), rect);
            //e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            //foreach (DrawElementInfo info in e.Info.InnerElements)
            //{
            //    if (!info.Visible) continue;
            //    ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            //}
            //e.Handled = true;
        }
    }
}