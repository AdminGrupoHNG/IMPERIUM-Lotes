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
using DevExpress.XtraEditors.Controls;

using System.Data;
using System.Data.OleDb;

namespace UI_GestionLotes.Formularios.Lotes
{
    public partial class frmReporteListadoProspecto : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        
        bool Buscar = false;
        public string cod_empresa = "", cod_proyecto = "", cod_prospecto = "";
        public int MenuIndice;
        public string sEstadoFiltro = "",sTipoContactoFiltro="", sCod_ejecutivoFiltro = "", CodMenu, DscMenu; 

        List<eCampanha> ListProspectos_grilla = new List<eCampanha>();

        public string codEstadoProspectoNoAsignado = "", codEstadoProspectoAsignado = "", codEstadoProspectoEnProceso = "", codEstadoProspectoCerrado = "", codEstadoProspectoCliente = "";
        List<eCampanha> ListCalendario = new List<eCampanha>();


        public frmReporteListadoProspecto()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmReporteListadoProspecto_Load(object sender, EventArgs e)
        {
            codEstadoProspectoNoAsignado = ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoNoAsignado")].ToString();
            codEstadoProspectoAsignado = ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoAsignado")].ToString();
            codEstadoProspectoEnProceso = ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoEnProceso")].ToString();
            codEstadoProspectoCerrado = ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoCerrado")].ToString();
            codEstadoProspectoCliente = ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoCliente")].ToString();

            HabilitarBotones();
            Inicializar();
            sbBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            sbBuscar_Click(null,null);
            VerListadoEjecutivo();
        }
        internal void frmReporteListadoProspecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                lblTitulo.Visible = false;

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CodMenu = cod_empresa + "|" + cod_proyecto;
                MenuIndice = navBarControl1.Groups[0].SelectedLinkIndex;
                string sSeleccion = sCod_ejecutivoFiltro;
                CargarOpcionesMenu();
                lblTitulo.Visible = true;
                rgFiltroProyecto.EditValue = CodMenu;
                rgFiltroEstado.EditValue = sEstadoFiltro;
                rgFiltroTipoContacto.EditValue = sTipoContactoFiltro;
                sbBuscar_Click(null,null);
                SplashScreenManager.CloseForm();
            }
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;

            rgFiltroProyecto.SelectedIndex = 0;
            rgFiltroEstado.SelectedIndex = 4;
            rgFiltroTipoContacto.SelectedIndex = 4;
            Buscar = true;

            string FechaIni = "01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            de_FechaInicio.EditValue=Convert.ToDateTime(FechaIni);
            de_FechaFin.EditValue = DateTime.Now;
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
            List<eCampanha> Listprospectos = new List<eCampanha>();
            Listprospectos = unit.Campanha.ListarProyectoscampanhasMenu<eCampanha>(1, "", "", Program.Sesion.Usuario.cod_usuario);

            rgFiltroProyecto.Properties.Items.Clear();
            foreach (eCampanha obj in Listprospectos)
            {
                RadioGroupItem rgiDetalle = new RadioGroupItem();
                rgiDetalle.Value = obj.cod_nodo;
                rgiDetalle.Description = obj.dsc_nodo;
                rgFiltroProyecto.Properties.Items.Add(rgiDetalle);
            }
        }

        public void CargarListado()
        {
            try
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                string[] aCodigos = rgFiltroProyecto.EditValue.ToString().Split("|".ToCharArray());
                if (aCodigos.Length > 1)
                {
                    cod_empresa = aCodigos[0];
                    cod_proyecto = aCodigos[1];
                }
                else
                {
                    cod_empresa = "";
                    cod_proyecto = "";
                }

                lblTitulo.Text = "Proyecto : " + rgFiltroProyecto.Properties.Items[rgFiltroProyecto.SelectedIndex].Description;
                CodMenu = rgFiltroProyecto.EditValue.ToString();
                DscMenu = rgFiltroProyecto.Properties.Items[rgFiltroProyecto.SelectedIndex].Description;

                if (rgFiltroEstado.EditValue==null)
                {
                    sEstadoFiltro = codEstadoProspectoAsignado;
                }
                else
                {
                    sEstadoFiltro = rgFiltroEstado.EditValue.ToString();
                }

                if (rgFiltroTipoContacto.EditValue == null)
                {
                    sTipoContactoFiltro ="";
                }
                else
                {
                    sTipoContactoFiltro = rgFiltroTipoContacto.EditValue.ToString().Trim();
                }

                string sFechainicio="", sFechafin = "";

                if (de_FechaInicio.EditValue!=null) {
                    sFechainicio = de_FechaInicio.EditValue.ToString();
                }

                if (de_FechaFin.EditValue != null)
                {
                    sFechafin = de_FechaFin.EditValue.ToString();
                }

                ListProspectos_grilla = unit.Campanha.ListarReporteProspectos<eCampanha>(0, cod_empresa, cod_proyecto, cod_prospecto, Program.Sesion.Usuario.cod_usuario, sFechainicio, sFechafin, sEstadoFiltro, sTipoContactoFiltro);
                bsListaProspectos.DataSource = ListProspectos_grilla;
                gvListaprospecto.ExpandAllGroups();
                //SplashScreenManager.CloseForm();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }        


        void VerListadoEjecutivo()
        {
            //gvListaprospecto.Columns["dsc_ejecutivo"].Visible = false;
            //gvListaprospecto.Columns["dsc_email"].Visible = false;
            //gvListaprospecto.Columns["dsc_estado_prospecto"].Visible = false;
            //gvListaprospecto.Columns["dsc_respuesta"].Visible = false;
            //gvListaprospecto.Columns["dsc_expectativa"].Visible = false;
            //gvListaprospecto.Columns["dsc_motivo"].Visible = false;
            //gvListaprospecto.Columns["dsc_ejecutivo_citaProximo"].Visible = false;
        }
        void VerListadoSupervisor()
        {   
            //gvListaprospecto.Columns["dsc_ejecutivo"].Visible = true;
            //gvListaprospecto.Columns["dsc_email"].Visible = true;
            //gvListaprospecto.Columns["dsc_estado_prospecto"].Visible = true;
            //gvListaprospecto.Columns["dsc_respuesta"].Visible = true;
            //gvListaprospecto.Columns["dsc_expectativa"].Visible = true;
            //gvListaprospecto.Columns["dsc_motivo"].Visible = true;
            //gvListaprospecto.Columns["dsc_ejecutivo_citaProximo"].Visible = true;        
        }


        private void gvListaprospecto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void gvListaprospecto_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    //eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

                    //frmMantEvento frm = new frmMantEvento(this);
                    //frm.MiAccion = evento.Editar;
                    //
                    //
                    //
                    //

                    //frm.cod_empresa = cod_empresa;
                    //frm.user = user;
                    //frm.o_eCamp= obj;
                    //frm.sEstadoFiltro = sEstadoFiltro;
                    //frm.sTipoContactoFiltro = sTipoContactoFiltro;
                    //frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    //frm.CodMenu = CodMenu;
                    //frm.IndicadorConfirmacionAuto = obj.flg_confirmacion;
                    //frm.cod_ejecutivo = obj.cod_ejecutivo.ToString();
                    //frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void gvListaprospecto_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);

            //if (e.RowHandle >= 0)
            //{
            //    string sEstado = gvListaprospecto.GetRowCellValue(e.RowHandle, "flg_activo").ToString();
            //    string sEstadoConfirmacion = gvListaprospecto.GetRowCellValue(e.RowHandle, "flg_confirmacion").ToString();
            //    if (sEstado == "NO")
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }else if (sEstadoConfirmacion == "NO")
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }


        private void tbiAsignados_ItemClick(object sender, TileItemEventArgs e)
        {
            rgFiltroEstado.SelectedIndex = 0;
            sbBuscar_Click(null,null);
        }
        private void tbiEnproceso_ItemClick(object sender, TileItemEventArgs e)
        {
            rgFiltroEstado.SelectedIndex = 1;
            sbBuscar_Click(null, null);
        }
        private void tbiCerrados_ItemClick(object sender, TileItemEventArgs e)
        {
            rgFiltroEstado.SelectedIndex = 2;
            sbBuscar_Click(null, null);
        }

        private void gvListaprospecto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gvListaprospecto.RowCount > 0)
            {
                if (e.RowHandle >= 0)
                {
                    eCampanha obj = gvListaprospecto.GetRow(e.RowHandle) as eCampanha;
                    if (e.Column.FieldName == "lfch_asignacion" && obj.lfch_asignacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "lfch_fecha" && obj.lfch_fecha.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "lfch_fechaProximo" && obj.lfch_fechaProximo.ToString().Contains("1/01/0001")) e.DisplayText = "";
                }
            }
        }

        private void tbiClientes_ItemClick(object sender, TileItemEventArgs e)
        {
            rgFiltroEstado.SelectedIndex = 3;
            sbBuscar_Click(null, null);
        }

        


        private void sbBuscar_Click(object sender, EventArgs e)
        {
            CargarListado();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\prospectos" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListaprospecto.ExportToXlsx(archivo);
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
            gvListaprospecto.ShowPrintPreview();
        }
        private void btnImportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarProspecto frm = new frmImportarProspecto();
            
            
            
            

            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;

            frm.ShowDialog();
        }
        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //frmMantEvento frm = new frmMantEvento(this);
                //frm.MiAccion = evento.Nuevo_externo;
                //
                //
                //
                //
                //frm.user = user;
                //frm.sEstadoFiltro = sEstadoFiltro;
                //frm.sTipoContactoFiltro = sTipoContactoFiltro;
                //frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                //frm.CodMenu = CodMenu;
                //frm.IndicadorConfirmacionAuto = "";
                //frm.cod_ejecutivo = "";
                //frm.ShowDialog();
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
                if (gvListaprospecto.RowCount <= 0)
                {
                    MessageBox.Show("Seleccione un registro", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

                if (obj.flg_activo=="SI")
                {
                    XtraMessageBox.Show("El prospecto ya se encuentra activo.", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult msgresult = MessageBox.Show("¿Está seguro de activar el prospecto?", "Activar prospectos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCampanha eCamp_resultado = unit.Campanha.Activar_Inactivar_campanha<eCampanha>("4",obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    frmReporteListadoProspecto_KeyDown(this, new KeyEventArgs(Keys.F5));
                    //SplashScreenManager.CloseForm();
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
                if (gvListaprospecto.RowCount <= 0)
                {
                    MessageBox.Show("Seleccione un registro", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

                DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar el prospecto?", "Inactivar prospectos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCampanha eCamp_resultado = unit.Campanha.Activar_Inactivar_campanha<eCampanha>("5",obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    frmReporteListadoProspecto_KeyDown(this, new KeyEventArgs(Keys.F5));
                    //SplashScreenManager.CloseForm();
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
                if (gvListaprospecto.RowCount<=0)
                {
                    MessageBox.Show("Seleccione un registro", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar el prospecto?", "Eliminar prospectos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCampanha eCamp_resultado = unit.Campanha.Eliminar_campanha<eCampanha>(obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                    if (eCamp_resultado.cod_campanha == "") {
                        XtraMessageBox.Show(eCamp_resultado.mensaje, "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        frmReporteListadoProspecto_KeyDown(this, new KeyEventArgs(Keys.F5));
                        //SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lci_Filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (lci_Filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                return;
            }
        }
        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }
        private void btnVistaEjecutivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            VerListadoEjecutivo();
        }
        private void btnVistaSupervisor_ItemClick(object sender, ItemClickEventArgs e)
        {
            VerListadoSupervisor();
        }


    }
}