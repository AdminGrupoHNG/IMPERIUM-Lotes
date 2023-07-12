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
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;
using BE_GestionLotes;
using BL_GestionLotes;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using UI_GestionLotes.Formularios.Marketing;

namespace UI_GestionLotes.Formularios.Lotes
{
    public partial class frmListadocampanha : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;

        bool Buscar = false;
        public string sCodempresa = "", sCodproyecto = "";
        public string sEstadoFiltro = "";

        public frmListadocampanha()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadocampanha_Load(object sender, EventArgs e)
        {;
            HabilitarBotones();
            Inicializar();
            sbBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;

            //Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
            //btnOcultarFiltro.ImageOptions.LargeImage = img;
            //btnOcultarFiltro.Caption = "Mostrar Filtro";
            //lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        internal void frmListadocampanha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");

                string sSeleccion = sCodproyecto;
                CargarOpcionesMenu();
                fObtenerSeleccionFiltroBusqueda(sSeleccion);
                rgFiltroEstado.EditValue = sEstadoFiltro;
                CargarListado();
                SplashScreenManager.CloseForm();
             }
        }

        void fGuardarFiltroBusqueda()
        {
            string sfiltro_general = "";
            int nCantidad = tlProyectos.Nodes[0].Nodes.Count;
            for (int x = 0; x <= nCantidad - 1; x++)
            {
                if (tlProyectos.Nodes[0].Nodes[x].Checked == true)
                {
                    sfiltro_general = sfiltro_general + tlProyectos.Nodes[0].Nodes[x].GetValue("ID").ToString() + ",";
                }
            }
            if (sfiltro_general.Length > 0)
            {
                sCodproyecto = sfiltro_general.Substring(0, sfiltro_general.Length - 1);
            }
            else
            {
                sCodproyecto = "";
            }
         
        }
        void fObtenerSeleccionFiltroBusqueda(string sSeleccion)
        {
            if (sSeleccion != "")
            {
                string[] aCodigos = sSeleccion.Split(",".ToCharArray());
                int nCantidad = tlProyectos.Nodes[0].Nodes.Count;
                for (int x = 0; x <= nCantidad - 1; x++)
                {
                    for (int y = 0; y <= aCodigos.Length - 1; y++)
                    {
                        if (tlProyectos.Nodes[0].Nodes[x].GetValue("ID").ToString() == aCodigos[y].ToString())
                        {
                            tlProyectos.Nodes[0].Nodes[x].Checked = true;
                        }
                    }
                }
            }
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            Buscar = true;
            fGuardarFiltroBusqueda();
            CargarListado();
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eCampanha> Listcampanha = new List<eCampanha>();
            Listcampanha = unit.Campanha.ListarProyectoscampanhasMenu<eCampanha>(0,"","", Program.Sesion.Usuario.cod_usuario);
            tlProyectos=unit.Globales.CargaTreeList(tlProyectos, Listcampanha);
            tlProyectos.CheckAll();
        }

        //private void nbc_Proyectos_NavPaneMinimizedGroupFormShowing(object sender, NavPaneMinimizedGroupFormShowingEventArgs e)
        //{
        //    tlProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
        //    tlProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
        //    tlProyectos.Refresh();
        //}

        private void tl_proyectos_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }

        private void tl_proyectos_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }
        private void tl_proyectos_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            tlProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlProyectos.Refresh();
        }


        private void sbBuscar_Click(object sender, EventArgs e)
        {
            fGuardarFiltroBusqueda();
            CargarListado();
        }

        public void CargarListado()
        {
            try
            {
                //var tools = new Tools.TreeListHelper(tlProyectos);
                //var proyectoMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(1);
                if (rgFiltroEstado.EditValue == null)
                {
                    sEstadoFiltro = "";
                }
                else
                {
                    sEstadoFiltro = rgFiltroEstado.EditValue.ToString();
                }
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //SplashScreen.Open("Por favor espere...", "Cargando...");
                List<eCampanha> Listcampanha = new List<eCampanha>();
                Listcampanha = unit.Campanha.Listarcampanhas<eCampanha>(0, sCodproyecto, Program.Sesion.Usuario.cod_usuario, estado: sEstadoFiltro);
                bsListaClientes.DataSource = Listcampanha;
                //SplashScreen.Close();
                ////SplashScreenManager.CloseForm(true);
                //foreach (Form splash in System.Windows.Forms.Application.OpenForms)
                //{
                //    if (splash.Name.Equals("FrmSplashCarga"))
                //    {
                //        //splash.Close();
                //        SplashScreenManager.CloseForm(true);
                //        break;
                //    }
                //}
            }
            catch (Exception e)
            {
                SplashScreen.Close();
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvListacampanha_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListacampanha_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eCampanha obj = gvListacampanha.GetFocusedRow() as eCampanha;

                    frmMantcampanha frm = new frmMantcampanha(this);
                    frm.cod_campanha = obj.cod_campanha;
                    frm.MiAccion = campanha.Editar;
                    frm.cod_empresa = sCodempresa;
                    frm.cod_proyecto = sCodproyecto;
                    obj = unit.Campanha.Listarcampanhas<eCampanha>(0, sCodproyecto, Program.Sesion.Usuario.cod_usuario, obj.cod_campanha).First();
                    frm.o_eCamp= obj;
                    frm.ShowDialog();

                    CargarListado();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListacampanha_DoubleClick(object sender, EventArgs e)
        {
        }
        
        private void gvListacampanha_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);

            if (e.RowHandle >= 0)
            {
                string sEstado = gvListacampanha.GetRowCellValue(e.RowHandle, "flg_activo").ToString();
                if (sEstado == "NO")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }



        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\campanhas" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListacampanha.ExportToXlsx(archivo);
                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListacampanha.ShowRibbonPrintPreview();
        }

        
        

        

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantcampanha frm = new frmMantcampanha(this);
                frm.MiAccion = campanha.Nuevo;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                /*
                if (gvListacampanha.RowCount <= 0)
                {
                    MessageBox.Show("Seleccione un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                eCampanha obj = gvListacampanha.GetFocusedRow() as eCampanha;

                if (obj.flg_activo=="SI")
                {
                    XtraMessageBox.Show("La campaña ya se encuentra activa.", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult msgresult = MessageBox.Show("¿Está seguro de activar la campaña?", "Activar campaña", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCampanha eCamp_resultado = unit.Campanha.Activar_Inactivar_campanha<eCampanha>("4",obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                    //SplashScreenManager.CloseForm();
                }*/

                gvListacampanha.PostEditor(); gvListacampanha.RefreshData();
                if (gvListacampanha.GetSelectedRows().Count() > 0)
                {
                    foreach (int nRow in gvListacampanha.GetSelectedRows())
                    {
                        eCampanha obj = gvListacampanha.GetRow(nRow) as eCampanha;
                        if (obj.flg_activo == "SI") { MessageBox.Show("Favor de revisar que todos los campañas seleccionados no se encuentren activas.", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }

                    DialogResult msgresult = MessageBox.Show("¿Está seguro de activar las campañas seleccionadas?", "Listado de campañas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        foreach (int nRow in gvListacampanha.GetSelectedRows())
                        {
                            eCampanha obj = gvListacampanha.GetRow(nRow) as eCampanha;
                            unit.Campanha.Activar_Inactivar_campanha<eCampanha>("4", obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());                            
                        }                        
                        MessageBox.Show("Todos las campañas seleccionados han sido activadas satisfactoriamente", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione al menos un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //if (gvListacampanha.RowCount <= 0)
                //{
                //    MessageBox.Show("Seleccione un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //eCampanha obj = gvListacampanha.GetFocusedRow() as eCampanha;

                //DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar la campaña?", "Inactivar campaña", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (msgresult == DialogResult.Yes)
                //{
                //    eCampanha eCamp_resultado = unit.Campanha.Activar_Inactivar_campanha<eCampanha>("5",obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                //    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //    frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                //    //SplashScreenManager.CloseForm();
                //}

                gvListacampanha.PostEditor(); gvListacampanha.RefreshData();
                if (gvListacampanha.GetSelectedRows().Count() > 0)
                {
                    foreach (int nRow in gvListacampanha.GetSelectedRows())
                    {
                        eCampanha obj = gvListacampanha.GetRow(nRow) as eCampanha;
                        if (obj.flg_activo == "NO") { MessageBox.Show("Favor de revisar que todos los campañas seleccionados no se encuentren inactivadas.", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }

                    DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar las campañas seleccionadas?", "Listado de campañas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        foreach (int nRow in gvListacampanha.GetSelectedRows())
                        {
                            eCampanha obj = gvListacampanha.GetRow(nRow) as eCampanha;
                            unit.Campanha.Activar_Inactivar_campanha<eCampanha>("5", obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                        }
                        MessageBox.Show("Todos las campañas seleccionados han sido desactivadas satisfactoriamente", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione al menos un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //if (gvListacampanha.RowCount<=0)
                //{
                //    MessageBox.Show("Seleccione un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //eCampanha obj = gvListacampanha.GetFocusedRow() as eCampanha;

                //DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar la campaña? Esta acción es irreversible", "Eliminar campaña", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (msgresult == DialogResult.Yes)
                //{
                //    eCampanha eCamp_resultado = unit.Campanha.Eliminar_campanha<eCampanha>(obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                //    if (eCamp_resultado.cod_campanha == "") {
                //        XtraMessageBox.Show(eCamp_resultado.mensaje, "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    }
                //    else
                //    {
                //        //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //        frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                //        //SplashScreenManager.CloseForm();
                //    }
                //}

                gvListacampanha.PostEditor(); gvListacampanha.RefreshData();
                if (gvListacampanha.GetSelectedRows().Count() > 0)
                {
                    DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar las campañas seleccionadas? Esta acción es irreversible", "Listado de campañas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        foreach (int nRow in gvListacampanha.GetSelectedRows())
                        {
                            eCampanha obj = gvListacampanha.GetRow(nRow) as eCampanha;
                            eCampanha eCamp_resultado = unit.Campanha.Eliminar_campanha<eCampanha>(obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                        }
                        MessageBox.Show("Todos las campañas seleccionados han sido eliminadas satisfactoriamente", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione al menos un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarProspecto frm = new frmImportarProspecto();
            frm.tipoImporte = "campanha";
            frm.ShowDialog();
            CargarListado();
        }

        private void btnCompletado_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //if (gvListacampanha.RowCount <= 0)
                //{
                //    MessageBox.Show("Seleccione un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //eCampanha obj = gvListacampanha.GetFocusedRow() as eCampanha;

                //DialogResult msgresult = MessageBox.Show("¿Está seguro de cambiar el estado de la campaña a completado?", "Completar campaña", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (msgresult == DialogResult.Yes)
                //{
                //    eCampanha eCamp_resultado = unit.Campanha.Activar_Inactivar_campanha<eCampanha>("6", obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                //    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //    frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                //    //SplashScreenManager.CloseForm();
                //}

                gvListacampanha.PostEditor(); gvListacampanha.RefreshData();
                if (gvListacampanha.GetSelectedRows().Count() > 0)
                {
                    foreach (int nRow in gvListacampanha.GetSelectedRows())
                    {
                        eCampanha obj = gvListacampanha.GetRow(nRow) as eCampanha;
                        if (obj.flg_activo == "CO") { MessageBox.Show("Favor de revisar que todos los campañas seleccionados no se encuentren completadas.", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }

                    DialogResult msgresult = MessageBox.Show("¿Está seguro de completar las campañas seleccionadas?", "Listado de campañas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgresult == DialogResult.Yes)
                    {
                        foreach (int nRow in gvListacampanha.GetSelectedRows())
                        {
                            eCampanha obj = gvListacampanha.GetRow(nRow) as eCampanha;
                            unit.Campanha.Activar_Inactivar_campanha<eCampanha>("6", obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                        }
                        MessageBox.Show("Todos las campañas seleccionados han sido completadas satisfactoriamente", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListadocampanha_KeyDown(this, new KeyEventArgs(Keys.F5));
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione al menos un registro", "Listado de campañas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionMultriple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvListacampanha.OptionsSelection.MultiSelect == true)
            {
                gvListacampanha.OptionsSelection.MultiSelect = false; return;
            }
            if (gvListacampanha.OptionsSelection.MultiSelect == false)
            {
                gvListacampanha.OptionsSelection.MultiSelect = true; 
                gvListacampanha.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                return;
            }
        }

        private void btnSegmentos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (String.IsNullOrEmpty(sCodproyecto)) { XtraMessageBox.Show("Debe seleccionar un proyecto", "Registro de Segmentos", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            frmMantSegmento frm = new frmMantSegmento();
            frm.cod_proyectos_todos = sCodproyecto;
            frm.ShowDialog();
        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lci_Filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (lci_Filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                return;
            }
        }

    }
}