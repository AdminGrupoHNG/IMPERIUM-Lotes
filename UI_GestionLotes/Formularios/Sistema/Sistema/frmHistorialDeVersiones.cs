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

namespace UI_GestionLotes.Formularios.Sistema.Sistema
{
    public partial class frmHistorialVersiones : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;

        public frmHistorialVersiones()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmHistorialVersiones_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            DateTime date = DateTime.Now;
            dtpFecPublicacion.EditValue = date;

            BuscarVersiones();
            DesactivarOpciones();
        }

        private void DesactivarOpciones()
        {
            if (Program.Sesion.Usuario.cod_usuario != "ADMINISTRADOR")
            {
                dtpFecPublicacion.Enabled = false;
                btnPublicar.Enabled = false;
                gvVersiones.OptionsBehavior.Editable = false;
                gvDetalle.OptionsBehavior.Editable = false;
            }
        }

        private void BuscarVersiones()
        {
            List<eVersion> versiones = unit.Version.ListarHistorialVersiones<eVersion>(1, dsc_solucion: Program.SolucionAbrir.Solucion);
            if (versiones.Count == 0) return;
            List<eVersion.eVersionDetalle> detVersiones = unit.Version.Cargar_HistorialVersiones_Detalle<eVersion.eVersionDetalle>(2, versiones[0].cod_version, versiones[0].dsc_version, Program.SolucionAbrir.Solucion);

            bsListadoVersiones.DataSource = versiones;
            bsListadoDetalle.DataSource = detVersiones;

            gvVersiones.FocusedRowHandle = 0;
            //gvVersiones_RowClick(gvVersiones, new DevExpress.XtraGrid.Views.Grid.RowClickEventArgs(null, 0));
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            try
            {
                eVersion eVer = gvVersiones.GetFocusedRow() as eVersion;

                if (eVer.dsc_version != null)
                {
                    string respuesta = "";
                    DialogResult result = MessageBox.Show("¿Desea publicar la versión " + eVer.dsc_version.ToString() + "?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No) return;

                    respuesta = unit.Version.Publicar_Version(eVer.dsc_version, Program.SolucionAbrir.Solucion);

                    if (respuesta == "OK") MessageBox.Show("Cambios Publicados con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Publicar los Cambios.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvVersiones_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                eVersion obj = gvVersiones.GetFocusedRow() as eVersion;
                List<eVersion.eVersionDetalle> detVersiones = new List<eVersion.eVersionDetalle>();
                if (obj != null) detVersiones = unit.Version.Cargar_HistorialVersiones_Detalle<eVersion.eVersionDetalle>(2, obj.cod_version, obj.dsc_version, Program.SolucionAbrir.Solucion);
                bsListadoDetalle.DataSource = detVersiones;
            }
        }

        private void gvVersiones_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvVersiones.FocusedRowHandle >= 0)
            {
                if (gvVersiones.FocusedColumn.Name != "colbtn_eliminar")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvVersiones_HiddenEditor(object sender, EventArgs e)
        {
            if (gvVersiones.FocusedColumn.Name != "colbtn_eliminar")
            {
                eVersion eVer = gvVersiones.GetFocusedRow() as eVersion;
                if (eVer == null) return;
                if (eVer.dsc_version != null)
                {
                    eVer.fch_publicacion = Convert.ToDateTime(dtpFecPublicacion.EditValue);
                    eVer.dsc_solucion = Program.SolucionAbrir.Solucion;
                    eVer = unit.Version.Ins_Act_HistorialVersiones<eVersion>(eVer, Program.Sesion.Usuario.cod_usuario);
                }

                BuscarVersiones();
            }
        }

        private void gvVersiones_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvVersiones_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvVersiones_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvVersiones_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void rbtnElimVersion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string respuesta = "";

            try
            {
                eVersion obj = gvVersiones.GetRow(gvVersiones.FocusedRowHandle) as eVersion;

                respuesta = unit.Version.Elim_HistorialVersiones(obj.cod_version, obj.dsc_version, Program.SolucionAbrir.Solucion);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarVersiones();
        }

        private void gvDetalle_HiddenEditor(object sender, EventArgs e)
        {
            eVersion eVer = gvVersiones.GetFocusedRow() as eVersion;
            eVersion.eVersionDetalle eDet = gvDetalle.GetFocusedRow() as eVersion.eVersionDetalle;
            if (eDet == null) return;
            eDet.cod_version = eVer.cod_version;
            eDet.dsc_version = eVer.dsc_version;
            eDet.dsc_solucion = Program.SolucionAbrir.Solucion;
            eDet = unit.Version.Ins_Act_Detalle_HistorialVersiones<eVersion.eVersionDetalle>(eDet, Program.Sesion.Usuario.cod_usuario);

            BuscarVersiones();
        }

        private void gvDetalle_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvDetalle_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDetalle_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvDetalle_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void rbtnElimDetalle_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string respuesta = "";

            try
            {
                eVersion.eVersionDetalle obj = gvDetalle.GetRow(gvDetalle.FocusedRowHandle) as eVersion.eVersionDetalle;

                respuesta = unit.Version.Elim_HistorialVersiones_Detalle(obj.cod_version, obj.dsc_version, obj.num_item, Program.SolucionAbrir.Solucion);

                bsListadoDetalle.Remove(obj);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmHistorialVersiones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}