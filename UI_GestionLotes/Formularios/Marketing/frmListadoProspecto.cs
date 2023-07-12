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
using UI_GestionLotes.Formularios.Marketing;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraBars.Alerter;
using System.Data;
using System.Data.OleDb;
using DevExpress.XtraBars.ToastNotifications;
using UI_GestionLotes.Tools;
using DevExpress.Utils.Extensions;
using UI_GestionLotes.Formularios.Shared;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace UI_GestionLotes.Formularios.Lotes
{
    public partial class frmListadoProspecto : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        string cod_ejecutivo = "";
        bool Buscar = false;
        public string cod_empresa = "", cod_proyecto = "", cod_prospecto = "";
        public int MenuIndice;
        public string sEstadoFiltro = "", sTipoCanalFiltro = "", sTipoAsesor = "", sTipoContactoFiltro = "", sCod_ejecutivoFiltro = "", CodMenu, DscMenu, sFiltroVisitasPendientes = "", sFiltroVisitasAsistidas = "";//, perfil = ""; 
        public string IndTipoAsig = "Asignación";
        public List<eCampanha> ListProspectos_grilla = new List<eCampanha>();
        eCampanha ListProspectos_temp = new eCampanha();
        public int validar = 0, num_minutos = 5, validar_alerta = 0;
        public string codEstadoProspectoNoAsignado = "", codEstadoProspectoAsignado = "", codEstadoProspectoEnProceso = "", codEstadoProspectoCerrado = "", codEstadoProspectoCliente = "";
        List<eTreeProyEtaStatus> listadoTreeList = new List<eTreeProyEtaStatus>();
        List<eCampanha> ListCalendario = new List<eCampanha>();
        List<eCampanha> ListTemp = new List<eCampanha>();
        List<eCampanha> ListTemp2 = new List<eCampanha>();

        public frmListadoProspecto()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoProspecto_Load(object sender, EventArgs e)
        {
            codEstadoProspectoNoAsignado = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoNoAsignado")].ToString());
            codEstadoProspectoAsignado = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoAsignado")].ToString());
            codEstadoProspectoEnProceso = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoEnProceso")].ToString());
            codEstadoProspectoCerrado = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoCerrado")].ToString());
            codEstadoProspectoCliente = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("codEstadoProspectoCliente")].ToString());

            HabilitarBotones();
            Inicializar();
            sbBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            cargarFechas();
            cargarTitulo();
            //BuscarListado();
            ccCalendarioEventos.TodayDate = DateTime.Now;
            //VerListadoEjecutivo();

            //Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
            //btnOcultarFiltro.ImageOptions.LargeImage = img;
            //btnOcultarFiltro.Caption = "Mostrar Filtro";
            //lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lciCalendario.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private void cargarTitulo(string estado = "")
        {
            var descFecha = grdbFecha.SelectedIndex == 0 ? "de Registro" : grdbFecha.SelectedIndex == 1 ? "de Asignación" : "del Próximo Evento";
            lblTitulo.Text = $"Listado de Prospectos {estado}del {Convert.ToDateTime(dtFechaInicio.EditValue).ToString("dd/MM/yyyy")} al {Convert.ToDateTime(dtFechaFin.EditValue).ToString("dd/MM/yyyy")} según Fecha {descFecha}";
        }
        internal void frmListadoProspecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                lblTitulo.Visible = false;

                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CodMenu = cod_empresa + "|" + cod_proyecto;
                MenuIndice = navBarControl1.Groups[0].SelectedLinkIndex;
                string sSeleccion = sCod_ejecutivoFiltro;
                //CargarOpcionesMenu();
                lblTitulo.Visible = true;
                //fObtenerSeleccionFiltroBusqueda(sSeleccion);
                //rgFiltroProyecto.EditValue = CodMenu;
                //rgFiltroEstado.EditValue = sEstadoFiltro;
                //rgFiltroTipoContacto.EditValue = sTipoContactoFiltro;

                //sEstadoFiltro = "";
                //sFiltroVisitasAsistidas = "";
                //sFiltroVisitasPendientes = "";
                BuscarListado();
                //tileBar1.SelectedItem = tbiTotal;


                //SplashScreenManager.CloseForm(false);
            }
        }

        private void Inicializar()
        {
            //CargarOpcionesMenu();
            CargarTreeListTresNodos();
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            fGuardarFiltroBusqueda();

            //rgFiltroProyecto.SelectedIndex = 0;
            sEstadoFiltro = "";
            //rgFiltroEstado.SelectedIndex = 0;
            //rgFiltroTipoContacto.SelectedIndex = 4;
            Buscar = true;

            //if(perfil == "VISUALIZADOR")
            //{
            //    btnNuevo.Enabled = false;
            //    btnImportarExcel.Enabled = false;
            //    btnExportarExcel.Enabled = false;
            //    btnImprimir.Enabled = false;
            //}
        }
        void OnNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }
        void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }
        private void CargarTreeListTresNodos()
        {
            tlEjecutivos.Appearance.Row.BackColor = Color.Transparent;
            tlEjecutivos.Appearance.Empty.BackColor = Color.Transparent;
            tlEjecutivos.BackColor = Color.Transparent;
            tlEjecutivos.TreeViewFieldName = "Name";
            tlEjecutivos.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            tlEjecutivos.OptionsBehavior.Editable = false;
            tlEjecutivos.OptionsBehavior.ReadOnly = true;
            tlEjecutivos.OptionsBehavior.AllowRecursiveNodeChecking = true;
            tlEjecutivos.NodeCellStyle += OnNodeCellStyle;
            tlEjecutivos.BeforeFocusNode += OnBeforeFocusNode;

            listadoTreeList = unit.Campanha.ListarOpcionesMenu<eTreeProyEtaStatus>(Program.Sesion.Usuario.cod_usuario);
            if (listadoTreeList != null && listadoTreeList.Count > 0)
            {
                new Tools.TreeListHelper(tlEjecutivos).
                    TreeViewParaTresNodos<eTreeProyEtaStatus>(
                    listadoTreeList, "cod_pro", "dsc_pro",
                    "cod_proyecto", "dsc_proyecto",
                    "cod_etapa", "dsc_etapa");

                tlEjecutivos.Refresh();

                //treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                //treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;


                tlEjecutivos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int i = 0; i < tlEjecutivos.Nodes.Count; i++)
                {
                    tlEjecutivos.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                    for (int j = 0; j < tlEjecutivos.Nodes[i].Nodes.Count(); j++)
                    {

                        //tlEjecutivos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        if (j == 0 || j == 1)
                        {
                            tlEjecutivos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                        }
                        else
                        {
                            tlEjecutivos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        }

                    }
                }

                tlEjecutivos.CheckAll();

            }
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
                //ribbonPageGroup1.Enabled = listPermisos[0].flg_escritura;
                //ribbonPageGroup3.Enabled = listPermisos[0].flg_escritura;
                //btnCargaMasivaEMO.Enabled = listPermisos[0].flg_escritura;
            }
            //List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);
            //eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 1 || x.cod_perfil == 8);
            //btnAreaEmpresa.Enabled = oPerfil != null ? true : false;
            //btnCargoEmpresa.Enabled = oPerfil != null ? true : false;
        }

        //private void HabilitarBotones()
        //{
        //    List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

        //    if (listPermisos.Count > 0)
        //    {
        //        grupoEdicion.Enabled = listPermisos[0].flg_escritura;

        //        for (int i = 0; i < listPermisos.Count; i++)
        //        {
        //            foreach (var item in ribbon.Items)
        //            {
        //                if (item.GetType() == typeof(BarButtonItem))
        //                {
        //                    if (((BarButtonItem)item).Name == listPermisos[i].dsc_menu)
        //                    {
        //                        ((BarButtonItem)item).Enabled = true;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //}
        internal void CargarOpcionesMenu()
        {
            List<eCampanha> Listprospectos = new List<eCampanha>();
            Listprospectos = unit.Campanha.ListarProyectoscampanhasMenu<eCampanha>(1, "", "", Program.Sesion.Usuario.cod_usuario);

            //rgFiltroProyecto.Properties.Items.Clear();
            foreach (eCampanha obj in Listprospectos)
            {
                RadioGroupItem rgiDetalle = new RadioGroupItem();
                rgiDetalle.Value = obj.cod_nodo;
                rgiDetalle.Description = obj.dsc_nodo;
                //rgFiltroProyecto.Properties.Items.Add(rgiDetalle);
            }

            //List<eCampanha> ListEjecutivos = new List<eCampanha>();
            //ListEjecutivos = unit.Campanha.ListarEjecutivosVentasMenu<eCampanha>(0, cod_empresa, cod_proyecto, Program.Sesion.Usuario.cod_usuario);
            ////perfil = ListEjecutivos[0].estado;
            //tlEjecutivos = unit.Globales.CargaTreeList(tlEjecutivos, ListEjecutivos);
            //tlEjecutivos.CheckAll();
        }

        void CargarListadoResumen()
        {
            DataTable odtDatos = new DataTable();
            odtDatos = unit.Campanha.Listar_Prospectos_Resumen(cod_empresa, cod_proyecto, cod_prospecto, sCod_ejecutivoFiltro,
                grdbFecha.EditValue.ToString(),
                    Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                    Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));

            if (odtDatos.Rows.Count > 0)
            {
                //if (odtDatos.Rows[0][0].ToString()=="SI")
                //{
                //    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //}
                //else if (odtDatos.Rows[0][0].ToString() == "NO")
                //{
                //    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //}
                tbiCreados.Elements[2].Text = odtDatos.Rows[0][1].ToString() + " ";
                tbiAsignados.Elements[2].Text = odtDatos.Rows[0][2].ToString() + " ";
                tbiEnproceso.Elements[2].Text = odtDatos.Rows[0][3].ToString() + " ";
                tbiCerrados.Elements[2].Text = odtDatos.Rows[0][4].ToString() + " ";
                tbiClientes.Elements[2].Text = odtDatos.Rows[0][5].ToString() + " ";

            }
        }
        public void CargarListado()
        {
            try
            {
                //if (validar == 0)
                //{
                //    SplashScreen.Open("Por favor espere...", "Cargando...");
                //}
                //else
                //{
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //}
                int nRowHandle = 0;
                nRowHandle = gvListaprospecto.FocusedRowHandle;
                // SplashScreenManager.CloseForm(false);

                var tools = new Tools.TreeListHelper(tlEjecutivos);
                //var proyectoMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(1);
                var etapaMultiple = tools.ObtenerCodigoConcatenadoDeNodoArray(2);


                //string[] aCodigos = rgFiltroProyecto.EditValue.ToString().Split("|".ToCharArray());
                string[] aCodigos = etapaMultiple[0].ToString().Split("|".ToCharArray());
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

                sTipoAsesor = etapaMultiple[1];
                sTipoContactoFiltro = etapaMultiple[2];
                sTipoCanalFiltro = etapaMultiple[3];
                sCod_ejecutivoFiltro = etapaMultiple[4];

                ListProspectos_grilla.Clear();
                ListProspectos_grilla = unit.Campanha.ListarProspectos<eCampanha>(0, cod_empresa, cod_proyecto, cod_prospecto,
                    Program.Sesion.Usuario.cod_usuario, sCod_ejecutivoFiltro, sEstadoFiltro, sTipoContactoFiltro,
                    sFiltroVisitasPendientes, sFiltroVisitasAsistidas,
                     grdbFecha.EditValue.ToString(),
                    Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                    Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                     sTipoCanalFiltro, sTipoAsesor);
                bsListaProspectos.DataSource = null;
                bsListaProspectos.DataSource = ListProspectos_grilla;

                string nTotal, nVisitasAsistidas, nVisitasPendientes, nAsignados, nEnProceso, nCerrados, nClientes, nCerradosCitas, nClientesCitas;
                nTotal = ListProspectos_grilla.Count().ToString();

                if (sEstadoFiltro == "")
                {
                    nVisitasAsistidas = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR003" && x.flg_eventos_asistidos == "SI" && x.flg_eventos_pendientes == "NO").Count.ToString();
                    nVisitasPendientes = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR003" && ((x.flg_eventos_asistidos == "NO" && x.flg_eventos_pendientes == "SI") || (x.flg_eventos_asistidos == "SI" && x.flg_eventos_pendientes == "SI"))).Count.ToString();
                    nAsignados = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR002").Count.ToString();
                    nEnProceso = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR003" && (x.flg_eventos_asistidos == "NO" && x.flg_eventos_pendientes == "NO" )).Count.ToString();
                    nCerrados = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR004").Count.ToString();
                    nCerradosCitas = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR004" && ((x.flg_eventos_asistidos == "NO" && x.flg_eventos_pendientes == "SI") || (x.flg_eventos_asistidos == "SI" && x.flg_eventos_pendientes == "SI") || (x.flg_eventos_asistidos == "SI" && x.flg_eventos_pendientes == "NO"))).Count.ToString();
                    nClientes = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR005").Count.ToString();                                                                                                                                     
                    nClientesCitas = ListProspectos_grilla.FindAll(x => x.cod_estado_prospecto == "ESTPR005" && ((x.flg_eventos_asistidos == "NO" && x.flg_eventos_pendientes == "SI") || (x.flg_eventos_asistidos == "SI" && x.flg_eventos_pendientes == "SI") || (x.flg_eventos_asistidos == "SI" && x.flg_eventos_pendientes == "NO"))).Count.ToString();

                    tbiTotal.Elements[2].Text = nTotal + " ";
                    tbiAsignados.Elements[2].Text = nAsignados + " ";
                    tbiEnproceso.Elements[2].Text = nEnProceso + " ";
                    tbiCerrados.Elements[2].Text = Convert.ToInt32(nCerrados) - Convert.ToInt32(nCerradosCitas) + " ";
                    tbiCerrados.Elements[4].Text = nCerradosCitas + " ";
                    tbiClientes.Elements[2].Text = Convert.ToInt32(nClientes) - Convert.ToInt32(nClientesCitas) + " ";
                    tbiClientes.Elements[4].Text = nClientesCitas + " ";
                    tbiCitas.Elements[2].Text = nVisitasPendientes + " ";
                    tbiCitasAsistidas.Elements[2].Text = nVisitasAsistidas + " ";
                }

                //CargarListadoResumen();
                // SplashScreenManager.CloseForm(false);

                //fFiltrarGrilla();
                //if (validar == 0)
                //{
                //    SplashScreen.Close();
                //}
                //else
                //{
                gvListaprospecto.RefreshData();
                gvListaprospecto.FocusedRowHandle = nRowHandle;
                SplashScreenManager.CloseForm(false);
                //}
                //validar = 1;

            }
            catch (Exception e)
            {
                //SplashScreen.Close();
                SplashScreenManager.CloseForm(false);

                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void ListarCalendario()
        {
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            //string cod_ejecutivo = "";
            //if (gvListaprospecto.RowCount > 0 && gvListaprospecto.FocusedRowHandle >= 0)
            //{
            //    cod_ejecutivo = gvListaprospecto.GetRowCellValue(gvListaprospecto.FocusedRowHandle, "cod_ejecutivo").ToString();
            //}
            //else
            //{
            //    if (ListProspectos_grilla.Count > 0)
            //    {
            //        cod_ejecutivo = ListProspectos_grilla[0].cod_ejecutivo.ToString();
            //    }
            //}

            ListCalendario = unit.Campanha.Listar_Eventos_Calendario<eCampanha>(0, cod_empresa, cod_proyecto, sCod_ejecutivoFiltro, Program.Sesion.Usuario.cod_usuario);
            bsListaCalendario.DataSource = null;
            ccCalendarioEventos.Refresh();
            ccCalendarioEventos_EditValueChanged(null, null);
            //bsCalendario.DataSource = ListCalendario;
            //SplashScreenManager.CloseForm();
        }

        void fGuardarFiltroBusqueda()
        {
            string sfiltro_general = "";
            if (tlEjecutivos.Nodes.Count > 0)
            {
                int nCantidad = tlEjecutivos.Nodes[0].Nodes.Count;
                for (int x = 0; x <= nCantidad - 1; x++)
                {
                    if (tlEjecutivos.Nodes[0].Nodes[x].Checked == true)
                    {
                        sfiltro_general = sfiltro_general + tlEjecutivos.Nodes[0].Nodes[x].GetValue("ID").ToString() + ",";
                    }
                }
                if (sfiltro_general.Length > 0)
                {
                    sCod_ejecutivoFiltro = sfiltro_general.Substring(0, sfiltro_general.Length - 1);
                }
                else
                {
                    sCod_ejecutivoFiltro = "-";
                }
            }
            else
            {
                sCod_ejecutivoFiltro = "-";
            }
        }
        void fObtenerSeleccionFiltroBusqueda(string sSeleccion)
        {
            if (sSeleccion != "")
            {
                string[] aCodigos = sSeleccion.Split(",".ToCharArray());
                int nCantidad = tlEjecutivos.Nodes[0].Nodes.Count;
                tlEjecutivos.UncheckAll();
                for (int x = 0; x <= nCantidad - 1; x++)
                {
                    for (int y = 0; y <= aCodigos.Length - 1; y++)
                    {
                        string CodigoTemp = tlEjecutivos.Nodes[0].Nodes[x].GetValue("ID").ToString();
                        if (CodigoTemp == aCodigos[y].ToString())
                        {
                            tlEjecutivos.Nodes[0].Nodes[x].Checked = true;
                        }
                    }
                }
            }
        }


        void VerListadoEjecutivo()
        {
            gvListaprospecto.Columns["dsc_ejecutivo"].Visible = false;
            gvListaprospecto.Columns["dsc_email"].Visible = false;
            gvListaprospecto.Columns["dsc_estado_prospecto"].Visible = false;
            gvListaprospecto.Columns["dsc_respuesta"].Visible = false;
            gvListaprospecto.Columns["dsc_expectativa"].Visible = false;
            gvListaprospecto.Columns["dsc_motivo"].Visible = false;
            gvListaprospecto.Columns["dsc_ejecutivo_citaProximo"].Visible = false;
            gvListaprospecto.Columns["dsc_tipo_contactoEventoConfirmacion"].Visible = false;
        }

        void VerListadoSupervisor()
        {
            gvListaprospecto.Columns["dsc_ejecutivo"].Visible = true;
            gvListaprospecto.Columns["dsc_email"].Visible = true;
            gvListaprospecto.Columns["dsc_estado_prospecto"].Visible = true;
            gvListaprospecto.Columns["dsc_respuesta"].Visible = true;
            gvListaprospecto.Columns["dsc_expectativa"].Visible = true;
            gvListaprospecto.Columns["dsc_motivo"].Visible = true;
            gvListaprospecto.Columns["dsc_ejecutivo_citaProximo"].Visible = true;
            gvListaprospecto.Columns["dsc_tipo_contactoEventoConfirmacion"].Visible = false;

            gvListaprospecto.Columns["dsc_ejecutivo"].VisibleIndex = 1;
            gvListaprospecto.Columns["dsc_prospecto"].VisibleIndex = 1;
            gvListaprospecto.Columns["lfch_asignacion"].VisibleIndex = 2;
            gvListaprospecto.Columns["dsc_estado_prospecto"].VisibleIndex = 3;
            gvListaprospecto.Columns["dsc_telefono_movil"].VisibleIndex = 4;
            gvListaprospecto.Columns["dsc_email"].VisibleIndex = 5;
            gvListaprospecto.Columns["dsc_origen_prospecto"].VisibleIndex = 6;
            gvListaprospecto.Columns["cnt_gestiones"].VisibleIndex = 7;
            gvListaprospecto.Columns["lfch_fecha"].VisibleIndex = 8;
            gvListaprospecto.Columns["dsc_tipo_contacto"].VisibleIndex = 9;
            gvListaprospecto.Columns["dsc_respuesta"].VisibleIndex = 10;
            gvListaprospecto.Columns["dsc_detalle_respuesta"].VisibleIndex = 11;
            gvListaprospecto.Columns["dsc_expectativa"].VisibleIndex = 12;
            gvListaprospecto.Columns["dsc_motivo"].VisibleIndex = 13;
            gvListaprospecto.Columns["lfch_fechaProximo"].VisibleIndex = 14;
            gvListaprospecto.Columns["dsc_tipo_contactoProximo"].VisibleIndex = 15;
            gvListaprospecto.Columns["dsc_ejecutivo_citaProximo"].VisibleIndex = 16;
            gvListaprospecto.Columns["dsc_tipo_contactoEventoConfirmacion"].VisibleIndex = 17;
            gvListaprospecto.Columns["fch_fechaEventoConfirmacion"].VisibleIndex = 18;
            gvListaprospecto.Columns["flg_confirmacion"].VisibleIndex = 19;
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
                    eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;
                    eProspectosXLote obj2 = unit.Proyectos.ListarProspecto<eProspectosXLote>(3, obj.cod_empresa,obj.cod_proyecto, obj.cod_prospecto).FirstOrDefault();
                    ListProspectos_temp = unit.Campanha.ObtenerProspecto<eCampanha>(3, obj.cod_empresa, obj.cod_proyecto, obj.cod_prospecto);
                    frmMantEvento frm = new frmMantEvento(this);
                    frm.MiAccion = evento.Editar;
                    frm.cod_empresa = cod_empresa;
                    frm.o_eCamp = ListProspectos_temp;
                    frm.o_eProspecto = obj2;

                    frm.o_eCamp.cod_estado_prospecto = obj.cod_estado_prospecto.Replace("delete,", "");
                    frm.sEstadoFiltro = sEstadoFiltro;
                    frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                    frm.sTipoContactoFiltro = sTipoContactoFiltro;
                    frm.CodMenu = CodMenu;
                    //frm.perfil = perfil;
                    frm.IndicadorConfirmacionAuto = obj.flg_confirmacion;
                    frm.cod_ejecutivo = obj.cod_ejecutivo;
                    frm.ShowDialog();
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

            if (e.RowHandle >= 0)
            {
                eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;
                if (obj == null || String.IsNullOrEmpty(gvListaprospecto.GetRowCellValue(e.RowHandle, "flg_activo").ToString()) || String.IsNullOrEmpty(gvListaprospecto.GetRowCellValue(e.RowHandle, "flg_confirmacion").ToString())) { return; }
                //string sEstado = gvListaprospecto.GetRowCellValue(e.RowHandle, "flg_activo").ToString();
                //string sEstadoConfirmacion = gvListaprospecto.GetRowCellValue(e.RowHandle, "flg_confirmacion").ToString();
                //if (sEstado == "NO")
                //{
                //    e.Appearance.ForeColor = Color.Black;
                //}
                //else if (sEstadoConfirmacion == "NO")
                //{
                //    e.Appearance.ForeColor = Color.Red;
                //    //GridView view = sender as GridView;
                //    //e.Appearance.ForeColor = Color.FromArgb(247, 177, 32);
                //    //e.Appearance.FontStyleDelta = FontStyle.Bold;
                //    //view.Appearance.FocusedRow.FontStyleDelta = FontStyle.Bold; 
                //    //view.Appearance.FocusedRow.ForeColor = Color.FromArgb(23, 97, 143);
                //    //view.Appearance.FocusedCell.FontStyleDelta = FontStyle.Bold; 
                //    //view.Appearance.FocusedCell.ForeColor = Color.FromArgb(23, 97, 143);
                //    //view.Appearance.HideSelectionRow.FontStyleDelta = FontStyle.Bold; 
                //    //view.Appearance.HideSelectionRow.ForeColor = Color.FromArgb(23, 97, 143);
                //    //view.Appearance.SelectedRow.FontStyleDelta = FontStyle.Bold; 
                //    //view.Appearance.SelectedRow.ForeColor = Color.FromArgb(23, 97, 143);
                //}


            }
        }
        private void gvListaprospecto_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvListaprospecto.RowCount > 0)
            {
                if (gvListaprospecto.FocusedRowHandle >= 0)
                {
                    //ccCalendarioEventos.ResetBackColor();
                    //ccCalendarioEventos.Refresh();
                    ListarCalendario();
                }
            }
        }

        private void tbiAsignados_ItemClick(object sender, TileItemEventArgs e)
        {
            //rgFiltroEstado.SelectedIndex = 0;
            sEstadoFiltro = "ESTPR002";
            sFiltroVisitasPendientes = "";
            sFiltroVisitasAsistidas = "";
            fGuardarFiltroBusqueda();
            CargarListado();
            cargarTitulo(tbiAsignados.Elements[0].Text + " ");
        }
        private void tbiEnproceso_ItemClick(object sender, TileItemEventArgs e)
        {
            //rgFiltroEstado.SelectedIndex = 1;
            sEstadoFiltro = "ESTPR003";
            sFiltroVisitasPendientes = "NO";
            sFiltroVisitasAsistidas = "NO";
            fGuardarFiltroBusqueda();
            CargarListado();
            cargarTitulo("en Proceso ");
        }
        private void tbiCerrados_ItemClick(object sender, TileItemEventArgs e)
        {
            //rgFiltroEstado.SelectedIndex = 2;
            sEstadoFiltro = "ESTPR004";
            sFiltroVisitasPendientes = "";
            sFiltroVisitasAsistidas = "";
            fGuardarFiltroBusqueda();
            CargarListado();
            cargarTitulo(tbiCerrados.Elements[0].Text + " ");
        }
        private void tbiClientes_ItemClick(object sender, TileItemEventArgs e)
        {
            //rgFiltroEstado.SelectedIndex = 3;
            sEstadoFiltro = "ESTPR005";
            sFiltroVisitasPendientes = "";
            sFiltroVisitasAsistidas = "";
            fGuardarFiltroBusqueda();
            CargarListado();
            cargarTitulo(tbiClientes.Elements[0].Text + " ");
        }

        private void tvEtiquetaCal_ItemDoubleClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

            frmMantEvento frm = new frmMantEvento(this);
            frm.MiAccion = evento.Editar;
            frm.cod_empresa = cod_empresa;
            frm.o_eCamp = obj;
            frm.sEstadoFiltro = sEstadoFiltro;
            frm.sTipoContactoFiltro = sTipoContactoFiltro;
            frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
            frm.CodMenu = CodMenu;
            frm.IndicadorConfirmacionAuto = obj.flg_confirmacion;
            frm.cod_ejecutivo = obj.cod_ejecutivo.ToString();
            frm.sIndCalendario = "1";
            frm.ShowDialog();
        }
        private void tlEjecutivos_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }
        private void tlEjecutivos_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }
        private void tlEjecutivos_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            tlEjecutivos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlEjecutivos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlEjecutivos.Refresh();
        }


        private void sbBuscar_Click(object sender, EventArgs e)
        {
            sEstadoFiltro = "";
            sFiltroVisitasPendientes = "";
            sFiltroVisitasAsistidas = "";
            BuscarListado();
            tileBar1.SelectedItem = tbiTotal;
        }

        private void BuscarListado()
        {
            fGuardarFiltroBusqueda();
            CargarListado();
            ListarCalendario();
            ccCalendarioEventos.Refresh();
            cargarTitulo();
        }
        private void btnReporteEventos_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmReporteListadoProspecto frm = new frmReporteListadoProspecto();





            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;

            frm.ShowDialog();
        }

        private void gvListaprospecto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gvListaprospecto.RowCount > 0)
            {
                if (e.RowHandle >= 0)
                {
                    eCampanha obj = gvListaprospecto.GetRow(e.RowHandle) as eCampanha;
                    if (e.Column.FieldName == "fch_fechaEventoConfirmacion" && obj.cod_tipo_eventoConfirmacion != "TPEVN003") e.DisplayText = "";
                    if (e.Column.FieldName == "dsc_tipo_contactoEventoConfirmacion" && obj.cod_tipo_eventoConfirmacion != "TPEVN003") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_confirmacion" && obj.cod_tipo_eventoConfirmacion != "TPEVN003" && (obj.cod_tipo_contactoProximo == "TPCON003" || obj.cod_tipo_contactoProximo == "TPCON004" || obj.cod_tipo_contactoProximo == "TPCON005" || obj.cod_tipo_contactoProximo == "TPCON007")) e.DisplayText = "";
                    if (e.Column.FieldName == "lfch_asignacion" && obj.lfch_asignacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "lfch_fecha" && obj.lfch_fecha.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "lfch_fechaProximo" && obj.lfch_fechaProximo.ToString().Contains("1/01/0001")) e.DisplayText = "";
                   
                    e.Appearance.FontStyleDelta = FontStyle.Bold;

                    //if (obj.flg_importar == "SI") e.Appearance.ForeColor = Color.Red; 
                }
            }
        }

        private void btnVistaResumen_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnVistaResumen.Caption != "Vista Resumen")
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter%20elements/combobox_32x32.png");
                btnVistaResumen.ImageOptions.LargeImage = img;
                btnVistaResumen.Caption = "Vista Resumen";
                VerListadoSupervisor();
                return;
            }
            if (btnVistaResumen.Caption != "Vista Detallada")
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter%20elements/listbox_32x32.png");
                btnVistaResumen.ImageOptions.LargeImage = img;
                btnVistaResumen.Caption = "Vista Detallada";
                VerListadoEjecutivo();
                return;
            }

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {


            //string[] aCodigos = rgFiltroProyecto.EditValue.ToString().Split("|".ToCharArray());
            //if (aCodigos.Length > 1)
            //{
            //    cod_empresa = aCodigos[0];
            //    cod_proyecto = aCodigos[1];
            //}
            frmPopupProyectoInfo frm = new frmPopupProyectoInfo();

            frm.cod_proyecto = cod_proyecto;
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
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
            gvListaprospecto.ShowRibbonPrintPreview();
        }
        private void btnImportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarProspecto frm = new frmImportarProspecto();
            frm.tipoImporte = "prospecto";
            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;

            frm.ShowDialog();
        }

        private void btnExcelProspectoNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarProspecto frm = new frmImportarProspecto();
            frm.tipoImporte = "prospectoNuevo";
            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;

            frm.ShowDialog();
        }

        private void btnExportarFormato_ItemClick(object sender, ItemClickEventArgs e)
        {
            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();

            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            //objExcel.Visible = true;

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Prospectos";

                //objExcel.ActiveWindow.DisplayGridlines = false;
                //objExcel.Range["A:A"].ColumnWidth = 3; objExcel.Range["B:B"].ColumnWidth = 3; objExcel.Range["C:C"].ColumnWidth = 5; objExcel.Range["D:D"].ColumnWidth = 44;
                //objExcel.Cells[6, 2] = "Cliente:"; objExcel.Cells[6, 4] = txtCliente.EditValue.ToString();
                //objExcel.Cells[7, 2] = "Dirección:"; objExcel.Cells[7, 4] = eDir.dsc_cadena_direccion;
                //objExcel.Cells[8, 2] = "Servicio:"; objExcel.Cells[8, 4] = lkpTipoServicio.Text;
                //objExcel.Range["D6:D8"].Select(); objExcel.Selection.Font.Bold = true;
                objExcel.Cells[1, 1] = "Fecha del Informe";
                objExcel.Cells[1, 2] = "Cód. Referencia Campaña";
                objExcel.Cells[1, 3] = "Canal";
                objExcel.Cells[1, 4] = "Punto de contacto";
                objExcel.Cells[1, 5] = "Cod. Segmento";
                objExcel.Cells[1, 6] = "Segmento";
                objExcel.Cells[1, 7] = "Apellidos";
                objExcel.Cells[1, 8] = "Nombres";
                objExcel.Cells[1, 9] = "Teléfono móvil";
                objExcel.Cells[1, 10] = "Observación";
                objExcel.Cells[1, 11] = "Asesor";

                objExcel.Range["A1:J1"].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);



                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            frmCalendarioProspecto frm = new frmCalendarioProspecto();
            frm.ListCalendario = ListTemp;
            frm.fecha = Convert.ToDateTime(ccCalendarioEventos.EditValue);
            frm.ShowDialog();
        }

        private void btnSeleccionMultriple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvListaprospecto.OptionsSelection.MultiSelect == false)
            {
                gvListaprospecto.OptionsSelection.MultiSelect = true; return;
            }
            if (gvListaprospecto.OptionsSelection.MultiSelect == true)
            {
                gvListaprospecto.OptionsSelection.MultiSelect = false; return;
            }

        }
        void AsignarEjecutivoPersonalizado(int opcion)
        {
            try
            {
                if (gvListaprospecto.RowCount > 0)
                {
                    eCampanha objCamp = gvListaprospecto.GetFocusedRow() as eCampanha;
                    if(objCamp == null) { return; }
                    eCampanha tmpCamp = new eCampanha();
                    tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                    tmpCamp.cod_empresa = cod_empresa;
                    tmpCamp.cod_proyecto = cod_proyecto;
                    List<eCampanha> lstProspecto = new List<eCampanha>();
                    lstProspecto = unit.Campanha.ListarEjecuPros<eCampanha>(7, tmpCamp.cod_usuario, tmpCamp.cod_empresa, tmpCamp.cod_proyecto, tmpCamp.valor_1, tmpCamp.valor_4);


                    if (gvListaprospecto.OptionsSelection.MultiSelect == false)
                     {
                        if (opcion == 1 && objCamp.cod_ejecutivo != "") { IndTipoAsig = "Reasignación"; }
                        if (opcion == 3 && objCamp.cod_ejecutivo_citaProximo != "") { IndTipoAsig = "Reasignación"; }

                    }
                    else
                    {
                        if (gvListaprospecto.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignación Manual", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                        foreach (int nRow in gvListaprospecto.GetSelectedRows())
                        {
                            eCampanha objLT = gvListaprospecto.GetRow(nRow) as eCampanha;
                            if (opcion == 1 && objLT.cod_ejecutivo != "" ) { IndTipoAsig = "Reasignación"; }
                            if (opcion == 3 && objLT.cod_ejecutivo_citaProximo != "" ) { IndTipoAsig = "Reasignación"; }
                        }
                    }

                    frmPopupGeneralAsignar frm = new frmPopupGeneralAsignar();
                    frm.Text = IndTipoAsig + " de Prospecto";
                    foreach (eCampanha obj in lstProspecto)
                    {
                        RadioGroupItem radio = new RadioGroupItem();
                        radio.Description = obj.dsc_ejecutivo;
                        radio.Value = obj.cod_ejecutivo;
                        frm.rgAsesores.Properties.Items.Add(radio);
                    }
                    frm.rgAsesores.SelectedIndex = 0;
                    frm.btnGuardar.Text = "ASIGNAR";

                    frm.ShowDialog();
                    if (frm.Aceptar)
                    {
                        asignacion(frm.rgAsesores.EditValue.ToString(),opcion, objCamp);
                    }

                }
                else
                {
                    MessageBox.Show("Seleccione un prospecto", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void AsignarNuevoProyectoaProspecto()
        {
            try
            {
                if (gvListaprospecto.RowCount > 0)
                {
                    List<eProyecto> lstProspecto = new List<eProyecto>();
                    lstProspecto = unit.Proyectos.ListarProyectos<eProyecto>("6", "","");
                    IndTipoAsig = "Reasignación";

                    frmPopupGeneralAsignar frm = new frmPopupGeneralAsignar();
                    frm.MiAccion = AsignarPros2.Proyecto;
                    frm.Text = IndTipoAsig + " de Proyecto";
                    foreach (eProyecto obj in lstProspecto)
                    {
                        RadioGroupItem radio = new RadioGroupItem();
                        radio.Description = obj.dsc_nombre;
                        radio.Value = obj.cod_codigo;
                        radio.Tag = obj.cod_empresa;
                        frm.rgAsesores.Properties.Items.Add(radio);
                    }
                    frm.rgAsesores.SelectedIndex = 0;
                    frm.btnGuardar.Text = "ASIGNAR";

                    frm.ShowDialog();
                    if (frm.Aceptar)
                    {
                        int selectedIndex = frm.rgAsesores.SelectedIndex;
                        if (selectedIndex >= 0)
                        {
                            RadioGroupItem selectedItem = frm.rgAsesores.Properties.Items[selectedIndex];
                            string tagValue = selectedItem.Tag.ToString();
                            reasignacionProyecto(tagValue, frm.rgAsesores.EditValue.ToString());
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Seleccione un prospecto", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void tbiCitas_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                sEstadoFiltro = "ESTPR003";
                sFiltroVisitasPendientes = "CITAS"; //SI
                sFiltroVisitasAsistidas = "CITAS";  //NO
                fGuardarFiltroBusqueda();
                CargarListado();
                cargarTitulo(tbiCitas.Elements[0].Text + " ");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiCitasAsistidas_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                sEstadoFiltro = "ESTPR003";
                sFiltroVisitasPendientes = "ASISTIDAS"; //NO SI
                sFiltroVisitasAsistidas = "ASISTIDAS"; //SI
                fGuardarFiltroBusqueda();
                CargarListado();
                cargarTitulo(tbiCitasAsistidas.Elements[0].Text + " ");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiTotal_ItemClick(object sender, TileItemEventArgs e)
        {
            sEstadoFiltro = "";
            sFiltroVisitasPendientes = "";
            sFiltroVisitasAsistidas = "";
            fGuardarFiltroBusqueda();
            CargarListado();
            cargarTitulo();
        }

        //private void chkFechaAvanzada_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(chkFechaAvanzada.CheckState == CheckState.Unchecked) { lytFiltroAvansadoFecha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; return; }
        //    if(chkFechaAvanzada.CheckState == CheckState.Checked) { lytFiltroAvansadoFecha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; return; }
        //}

        private void cargarFechas()
        {
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = oPrimerDiaDelMes.AddMonths(-1);
            dtFechaFin.EditValue = oUltimoDiaDelMes;
            grdbFecha.SelectedIndex = 1;
            //dtFechaInicio
        }

        //public class Message
        //{

        //    public Message()
        //    {
        //        this.Caption = "CITA TERRENO";
        //        this.Text = "TIENE UNA CITRA CON EL CLIENTE MAS MOLESTOSO";

        //        this.Image = Properties.Resources.alarm_32px;
        //    }
        //    public string Caption { get; set; }
        //    public string Text { get; set; }
        //    public Image Image { get; set; }
        //    public bool AutoCloseFormOnClick { get { return true; } }


        //}

        private Timer CrearTimerAdjunto(int intervalo, eMessage msg)
        {
            Timer time = new Timer()
            {
                Interval = intervalo
            };
            time.Tick += (sender2, e2) => Timer1_Tick(sender2, e2, msg);
            time.Start();
            return time;
        }
        private void mostrarAlertaSinNotificaciones()
        {
            AlertControl alert = new AlertControl();
            alert.FormShowingEffect = AlertFormShowingEffect.SlideHorizontal;
            alert.AutoFormDelay = 5000;
            alert.ShowAnimationType = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.FadeIn;
            alert.Show(this.FindForm(), "Activar Notificaciones", "No se encontraron próximos eventos para el día de hoy.", "", Properties.Resources.warning_32px);
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (Program.timersExistentes.Count > 0)
            {
                StopTimers();
            }

            ListTemp2 = unit.Campanha.Listar_Eventos_Calendario<eCampanha>(1, cod_empresa, cod_proyecto, cod_ejecutivo, Program.Sesion.Usuario.cod_usuario);
            if (ListTemp2.Count == 0)
            {
                Image img = Properties.Resources.alarm_32px;
                btnNotificacion.ImageOptions.LargeImage = img;
                btnNotificacion.Caption = "Activar Notificaciones";
                mostrarAlertaSinNotificaciones();
                Program.timercada5minutos.Stop();
                return;
            }
            else
            {
                Image img = Properties.Resources.no_reminders_32px;
                btnNotificacion.ImageOptions.LargeImage = img;
                btnNotificacion.Caption = "Desactivar Notificaciones";
                MostrarAlertas();
            }
        }

        private void btnNotificacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnNotificacion.Caption == "Desactivar Notificaciones")
            {
                Image img = Properties.Resources.alarm_32px;
                btnNotificacion.ImageOptions.LargeImage = img;
                btnNotificacion.Caption = "Activar Notificaciones";
                if (Program.timersExistentes.Count > 0)
                {
                    StopTimers();
                }
                Program.timercada5minutos.Stop();
                return;
            }
            if (gvListaprospecto.RowCount > 0 && gvListaprospecto.FocusedRowHandle >= 0)
            {
                cod_ejecutivo = gvListaprospecto.GetRowCellValue(gvListaprospecto.FocusedRowHandle, "cod_ejecutivo").ToString();
            }
            else
            {
                if (ListProspectos_grilla.Count > 0)
                {
                    cod_ejecutivo = ListProspectos_grilla[0].cod_ejecutivo.ToString();
                }
            }

            ListTemp2 = unit.Campanha.Listar_Eventos_Calendario<eCampanha>(1, cod_empresa, cod_proyecto, cod_ejecutivo, Program.Sesion.Usuario.cod_usuario);

            if (ListTemp2.Count == 0)
            {
                mostrarAlertaSinNotificaciones(); return;
            }
            frmAlerta frm = new frmAlerta(this);
            frm.ShowDialog();
            if (validar_alerta == 0) { return; }
            num_minutos = frm.num_minutos;
            MostrarAlertas();
            Program.timercada5minutos.Interval = 300000;
            Program.timercada5minutos.Tick += (sender2, e2) => Timer2_Tick(sender2, e2);
            Program.timercada5minutos.Start();
        }
        private void StopTimers()
        {
            try
            {
                foreach (Timer timer in Program.timersExistentes)
                {
                    timer.Stop();
                    //timersExistentes.Remove(timer);
                }
                Program.timersExistentes.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
            }

        }

        private void MostrarAlertas()
        {
            foreach (eCampanha obj in ListTemp2)
            {
                DateTime fecha1 = DateTime.Now;
                DateTime fecha2 = obj.fch_evento.AddMinutes(-num_minutos);
                TimeSpan diferencia = fecha2 - fecha1;
                eMessage msg = new eMessage();
                msg.Caption = obj.dsc_tipo_contacto;
                msg.Text = obj.dsc_prospecto + ".\n Hoy a las " + obj.fch_evento.ToString("HH:mm") + ".";
                msg.Image = Properties.Resources.alarm_32px;
                msg.prospecto = obj.cod_prospecto;
                msg.empresa = obj.cod_empresa;
                msg.proyecto = obj.cod_proyecto;
                if (diferencia.TotalMilliseconds > 0)
                {
                    Timer time = CrearTimerAdjunto(Convert.ToInt32(diferencia.TotalMilliseconds), msg);
                    Program.timersExistentes.Add(time);
                }
            }
            if (Program.timersExistentes.Count > 0)
            {
                Image img = Properties.Resources.no_reminders_32px;
                btnNotificacion.ImageOptions.LargeImage = img;
                btnNotificacion.Caption = "Desactivar Notificaciones";
            }
            else
            {
                mostrarAlertaSinNotificaciones();
                Image img = Properties.Resources.alarm_32px;
                btnNotificacion.ImageOptions.LargeImage = img;
                btnNotificacion.Caption = "Activar Notificaciones";
            }

        }
        private void alertControl1_ButtonClick(object sender, DevExpress.XtraBars.Alerter.AlertButtonClickEventArgs e, eMessage obj)
        {
            if (e.ButtonName == "Button 1")
            {
                popupMenu1.Tag = obj.Caption + "|" + obj.Text + "|" + obj.prospecto + "|" + obj.empresa + "|" + obj.proyecto;
                popupMenu1.ShowPopup(MousePosition);
            }
        }
        private void Timer1_Tick(object sender, EventArgs e, eMessage msg)
        {
            AlertControl alert2 = new AlertControl();
            AlertButton miBoton = new AlertButton();
            miBoton.Name = "Button 1";
            miBoton.Hint = "Posponer Alerta";
            miBoton.Image = Properties.Resources.alarm_clock_16px;
            alert2.Buttons.Add(miBoton);
            alert2.FormShowingEffect = AlertFormShowingEffect.SlideHorizontal;
            alert2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12, FontStyle.Bold);
            alert2.AppearanceHotTrackedText.Font = new Font("Tahoma", 9, FontStyle.Underline | FontStyle.Bold);
            alert2.AppearanceHotTrackedText.ForeColor = Color.FromArgb(46, 75, 88);
            alert2.AutoFormDelay = 5000;
            alert2.ButtonClick += (sender2, e2) => alertControl1_ButtonClick(sender2, e2, msg);
            alert2.AlertClick += (sender2, e2) => AlertControl1_AlertClick(sender2, e2, msg);
            alert2.ShowAnimationType = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.FadeIn;
            alert2.Show(this.FindForm(), msg.Caption, msg.Text, msg.Text, msg.Image, msg, msg.AutoCloseFormOnClick);
            foreach (Timer timer in Program.timersExistentes)
            {
                if (timer.Enabled)
                {
                    timer.Stop();
                    Program.timersExistentes.Remove(timer);
                    break;
                }
            }
            if (Program.timersExistentes.Count == 0)
            {
                Image img = Properties.Resources.alarm_32px;
                btnNotificacion.ImageOptions.LargeImage = img;
                btnNotificacion.Caption = "Activar Notificaciones";
            }
            else
            {
                Image img = Properties.Resources.no_reminders_32px;
                btnNotificacion.ImageOptions.LargeImage = img;
                btnNotificacion.Caption = "Desactivar Notificaciones";
            }
        }

        private void barbtn5min_ItemClick(object sender, ItemClickEventArgs e)
        {
            int minutes = 5;
            int miliseconds = minutes * 60 * 1000;
            var array = popupMenu1.Tag.ToString().Split('|');
            eMessage msg = new eMessage();
            msg.Caption = array[0];
            msg.Text = array[1];
            msg.Image = Properties.Resources.alarm_32px;
            msg.prospecto = array[2];
            msg.empresa = array[3];
            msg.proyecto = array[4];
            Timer time = CrearTimerAdjunto(miliseconds, msg);
            Program.timersExistentes.Add(time);
            Image img = Properties.Resources.no_reminders_32px;
            btnNotificacion.ImageOptions.LargeImage = img;
            btnNotificacion.Caption = "Desactivar Notificaciones";
        }

        private void barbtn1Omin_ItemClick(object sender, ItemClickEventArgs e)
        {
            int minutes = 10;
            int miliseconds = minutes * 60 * 1000;
            var array = popupMenu1.Tag.ToString().Split('|');
            eMessage msg = new eMessage();
            msg.Caption = array[0];
            msg.Text = array[1];
            msg.Image = Properties.Resources.alarm_32px;
            msg.prospecto = array[2];
            msg.empresa = array[3];
            msg.proyecto = array[4];
            Timer time = CrearTimerAdjunto(miliseconds, msg);
            Program.timersExistentes.Add(time);
            Image img = Properties.Resources.no_reminders_32px;
            btnNotificacion.ImageOptions.LargeImage = img;
            btnNotificacion.Caption = "Desactivar Notificaciones";
        }

        private void barbtn15min_ItemClick(object sender, ItemClickEventArgs e)
        {
            int minutes = 15;
            int miliseconds = minutes * 60 * 1000;
            var array = popupMenu1.Tag.ToString().Split('|');
            eMessage msg = new eMessage();
            msg.Caption = array[0];
            msg.Text = array[1];
            msg.Image = Properties.Resources.alarm_32px;
            msg.prospecto = array[2];
            msg.empresa = array[3];
            msg.proyecto = array[4];
            Timer time = CrearTimerAdjunto(miliseconds, msg);
            Program.timersExistentes.Add(time);
            Image img = Properties.Resources.no_reminders_32px;
            btnNotificacion.ImageOptions.LargeImage = img;
            btnNotificacion.Caption = "Desactivar Notificaciones";
        }

        private void barbtn3Omin_ItemClick(object sender, ItemClickEventArgs e)
        {
            int minutes = 30;
            int miliseconds = minutes * 60 * 1000;
            var array = popupMenu1.Tag.ToString().Split('|');
            eMessage msg = new eMessage();
            msg.Caption = array[0];
            msg.Text = array[1];
            msg.Image = Properties.Resources.alarm_32px;
            msg.prospecto = array[2];
            msg.empresa = array[3];
            msg.proyecto = array[4];
            Timer time = CrearTimerAdjunto(miliseconds, msg);
            Program.timersExistentes.Add(time);
            Image img = Properties.Resources.no_reminders_32px;
            btnNotificacion.ImageOptions.LargeImage = img;
            btnNotificacion.Caption = "Desactivar Notificaciones";
        }

        private void barbtn6Omin_ItemClick(object sender, ItemClickEventArgs e)
        {
            int minutes = 60;
            int miliseconds = minutes * 60 * 1000;
            var array = popupMenu1.Tag.ToString().Split('|');
            eMessage msg = new eMessage();
            msg.Caption = array[0];
            msg.Text = array[1];
            msg.Image = Properties.Resources.alarm_32px;
            msg.prospecto = array[2];
            msg.empresa = array[3];
            msg.proyecto = array[4];
            Timer time = CrearTimerAdjunto(miliseconds, msg);
            Program.timersExistentes.Add(time);
            Image img = Properties.Resources.no_reminders_32px;
            btnNotificacion.ImageOptions.LargeImage = img;
            btnNotificacion.Caption = "Desactivar Notificaciones";
        }

        private void btnImpFormtProsp_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarProspecto frm = new frmImportarProspecto();
            frm.tipoImporte = "prospectoEjecutivo";
            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;

            frm.ShowDialog();
        }

        private void btnEnviarEventosCorreo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var tools = new Tools.TreeListHelper(tlEjecutivos);
                var etapaMultiple = tools.ObtenerCodigoConcatenadoDeNodoArray(2);
                string[] aCodigos = etapaMultiple[0].ToString().Split("|".ToCharArray());
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
                eCampanha tmpCamp = new eCampanha();
                tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                tmpCamp.cod_empresa = cod_empresa;
                tmpCamp.cod_proyecto = cod_proyecto;
                List<eCampanha> lstProspecto = new List<eCampanha>();
                lstProspecto = unit.Campanha.ListarEjecuPros<eCampanha>(7, tmpCamp.cod_usuario, tmpCamp.cod_empresa, tmpCamp.cod_proyecto, tmpCamp.valor_1, tmpCamp.valor_4);

                frmPopupGeneralAsignar frm = new frmPopupGeneralAsignar(); //2 -> Opcion para enviar correos
                frm.Text = "Seleccionar Asesor";
                
                foreach (eCampanha obj in lstProspecto)
                {
                    RadioGroupItem radio = new RadioGroupItem();
                    radio.Description = obj.dsc_ejecutivo;
                    radio.Value = obj.cod_ejecutivo;
                    frm.rgAsesores.Properties.Items.Add(radio);
                }
                frm.rgAsesores.SelectedIndex = 0;
                frm.btnGuardar.Text = "Enviar";
                frm.ShowDialog();
                if (frm.Aceptar)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(FrmSplashCarga), "Generando correo", "Cargando...");
                    sCod_ejecutivoFiltro = frm.rgAsesores.EditValue.ToString();
                    List<eCampanha> ListProspectos = new List<eCampanha>();
                    ListProspectos = unit.Campanha.ListarProspectos<eCampanha>(4, cod_empresa, cod_proyecto, cod_prospecto,
                        Program.Sesion.Usuario.cod_usuario, sCod_ejecutivoFiltro, sEstadoFiltro, sTipoContactoFiltro,
                        sFiltroVisitasPendientes, sFiltroVisitasAsistidas);

                    List<eCampanha> ListProspectos2 = new List<eCampanha>();
                    ListProspectos2 = unit.Campanha.ListarProspectos<eCampanha>(5, cod_empresa, cod_proyecto, cod_prospecto,
                        Program.Sesion.Usuario.cod_usuario, sCod_ejecutivoFiltro, sEstadoFiltro, sTipoContactoFiltro,
                        sFiltroVisitasPendientes, sFiltroVisitasAsistidas);

                    string sCorreoEjecutivo = unit.Campanha.ListarEjecuPros<eCampanha>(28, usuario: sCod_ejecutivoFiltro).Select(x => x.dsc_ejecutivo).FirstOrDefault().ToString();
                    string desde1 = "", hasta1 = "", desde2 = "", hasta2 = "";
                    if (ListProspectos.Count > 0)
                    {
                        desde1 = ListProspectos.Select(x => x.fch_asignacion).FirstOrDefault().ToString();
                        hasta1 = ListProspectos.Select(x => x.fch_fechaProximo).FirstOrDefault().ToString();
                    }
                    if(ListProspectos2.Count > 0)
                    {
                        desde2 = ListProspectos2.Select(x => x.fch_asignacion).FirstOrDefault().ToString();
                        hasta2 = ListProspectos2.Select(x => x.fch_fechaProximo).FirstOrDefault().ToString();
                    }
                    

                    CustomizeEmail email = new CustomizeEmail(desde1,hasta1,desde2,hasta2);
                    SplashScreenManager.CloseForm(false);
                    email.ConfigurarEnvioEmail_LotesEventoProspecto(ListProspectos, ListProspectos2, sCorreoEjecutivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnReasignarProyecto_ItemClick(object sender, ItemClickEventArgs e)
        {
            AsignarNuevoProyectoaProspecto();
        }

        private void btnReasignarAsesorCitas_ItemClick(object sender, ItemClickEventArgs e)
        {
            AsignarEjecutivoPersonalizado(3);
        }

        private void AlertControl1_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e, eMessage msg)
        {
            ListProspectos_temp = unit.Campanha.ObtenerProspecto<eCampanha>(3, msg.empresa, msg.proyecto, msg.prospecto);
            frmMantEvento frm = new frmMantEvento(this);
            frm.MiAccion = evento.Editar;
            frm.cod_empresa = cod_empresa;
            frm.o_eCamp = ListProspectos_temp;
            frm.o_eCamp.cod_estado_prospecto = ListProspectos_temp.cod_estado_prospecto;
            frm.sEstadoFiltro = sEstadoFiltro;
            frm.sTipoContactoFiltro = sTipoContactoFiltro;
            frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
            frm.CodMenu = CodMenu;
            frm.ShowDialog();
        }

        private void btnHabilitar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Iniciar_HabilitarProspecto();
        }

       

        private void frmListadoProspecto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnNotificacion.Caption == "Desactivar Notificaciones")
            {
                
                e.Cancel = true;
                Program.IsMinimized = true;
                this.Hide(); 
            }
            else
            {
                Program.IsMinimized = false;
            }
        }
        void Iniciar_HabilitarProspecto()
        {
            try
            {

                DialogResult msgresult = MessageBox.Show("¿Está seguro de habilitar este prospecto?", "Habilitar Prospecto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    string result = "";
                    result = Habilitar_Prospecto();

                    if (result == "OK")
                    {
                        MessageBox.Show("Se habilitó el prospecto de manera satisfactoria", "Mantenimiento de prospectos", MessageBoxButtons.OK);
                        sEstadoFiltro = "";
                        sFiltroVisitasPendientes = "";
                        sFiltroVisitasAsistidas = "";
                        BuscarListado();
                        tileBar1.SelectedItem = tbiTotal;
                    }
                    else
                    {
                        MessageBox.Show("Solo se puede habilitar los prospectos cerrados", "Mantenimiento de prospectos", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string Habilitar_Prospecto()
        {
            if (gvListaprospecto.OptionsSelection.MultiSelect == false)
            {
                eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

                if (obj.cod_estado_prospecto == codEstadoProspectoCerrado)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando Prospectos", "Cargando...");

                    eCampanha eCampResultado = new eCampanha();
                    eCampanha eCamp = new eCampanha();
                    eCamp.cod_empresa = obj.cod_empresa;
                    eCamp.cod_proyecto = obj.cod_proyecto;  
                    eCamp.cod_prospecto = obj.cod_prospecto; 
                    eCamp.cod_estado_prospecto = codEstadoProspectoNoAsignado;
                    eCampResultado = unit.Campanha.Habilitar_prospecto<eCampanha>(eCamp);
                    //eCampResultado = unit.Campanha.Modificar_Reasignacion_Proyecto<eCampanha>(eCamp, EmpresaSeleccionada, ProyectoSeleccionado);

                    SplashScreenManager.CloseForm(false);
                    if (eCampResultado.cod_prospecto != null)
                    {
                        return "OK";
                    }
                    return null;
                }
                else
                {
                    return null;
                }
                   
                

            }
            else
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando Prospectos", "Cargando...");
                eCampanha eCampResultado = new eCampanha();

                foreach (int nRow in gvListaprospecto.GetSelectedRows())
                {
                    eCampanha objLT = gvListaprospecto.GetRow(nRow) as eCampanha;
                    if (objLT.cod_estado_prospecto == codEstadoProspectoCerrado)
                    {
                        eCampanha eCamp = new eCampanha();
                        eCamp.cod_empresa = objLT.cod_empresa; 
                        eCamp.cod_proyecto = objLT.cod_proyecto; 
                        eCamp.cod_prospecto = objLT.cod_prospecto; 
                        eCamp.cod_estado_prospecto = codEstadoProspectoNoAsignado;
                        eCampResultado = unit.Campanha.Habilitar_prospecto<eCampanha>(eCamp);
                    }
                }
                SplashScreenManager.CloseForm(false);
                if (eCampResultado.cod_prospecto != null)
                {
                    return "OK";
                }
                return null;
                //fFiltrarGrilla();
            }
        }

        public void reasignacionProyecto(string EmpresaSeleccionada, string ProyectoSeleccionado)
        {
            if (gvListaprospecto.OptionsSelection.MultiSelect == false)
            {
                if (ProyectoSeleccionado.Trim() != gvListaprospecto.GetRowCellValue(gvListaprospecto.FocusedRowHandle, "cod_proyecto").ToString())
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Proyecto", "Cargando...");

                    eCampanha eCampResultado = new eCampanha();
                    eCampanha eCamp = new eCampanha();
                    eCamp.cod_empresa = gvListaprospecto.GetRowCellValue(gvListaprospecto.FocusedRowHandle, "cod_empresa").ToString(); 
                    eCamp.cod_proyecto = gvListaprospecto.GetRowCellValue(gvListaprospecto.FocusedRowHandle, "cod_proyecto").ToString(); 
                    eCamp.cod_prospecto = gvListaprospecto.GetRowCellValue(gvListaprospecto.FocusedRowHandle, "cod_prospecto").ToString();
                    eCampResultado = unit.Campanha.Modificar_Reasignacion_Proyecto<eCampanha>(eCamp, EmpresaSeleccionada, ProyectoSeleccionado);

                    SplashScreenManager.CloseForm(false);

                    if (eCampResultado.cod_prospecto != null)
                    {
                        string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                        MessageBox.Show("Se " + sMen + " de proyecto de manera satisfactoria", IndTipoAsig + " de Proyecto", MessageBoxButtons.OK);
                        CargarListado();
                        //fFiltrarGrilla();
                    }
                }

            }
            else
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Prospecto", "Cargando...");
                foreach (int nRow in gvListaprospecto.GetSelectedRows())
                {
                    eCampanha objLT = gvListaprospecto.GetRow(nRow) as eCampanha;
                    if (ProyectoSeleccionado != objLT.cod_proyecto)
                    {
                        eCampanha eCampResultado = new eCampanha();
                        eCampanha eCamp = new eCampanha();
                        eCamp.cod_empresa = objLT.cod_empresa; 
                        eCamp.cod_proyecto = objLT.cod_proyecto;
                        eCamp.cod_prospecto = objLT.cod_prospecto;
                        eCampResultado = unit.Campanha.Modificar_Reasignacion_Proyecto<eCampanha>(eCamp, EmpresaSeleccionada, ProyectoSeleccionado);

                    }
                }
                SplashScreenManager.CloseForm(false);
                string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                MessageBox.Show("Se " + sMen + " de proyecto de manera satisfactoria", IndTipoAsig + " de Proyecto", MessageBoxButtons.OK);
                CargarListado();
                //fFiltrarGrilla();
            }
        }

        public void asignacion(string AsesorSeleccionado, int opcion, eCampanha objCamp)
        {
            if (gvListaprospecto.OptionsSelection.MultiSelect == false)
            {
                //opcion == 1 &&
                //opcion == 3 &&
                //if ((opcion == 1 && AsesorSeleccionado.Trim() != objCamp.cod_ejecutivo) || (opcion == 3 && AsesorSeleccionado.Trim() != objCamp.cod_ejecutivo_citaProximo))
                //{
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Prospecto", "Cargando...");

                    eCampanha eCampResultado = new eCampanha();
                    eCampanha eCamp = new eCampanha();
                    eCamp.cod_ejecutivo = AsesorSeleccionado.Trim();
                    eCamp.cod_empresa = cod_empresa;
                    eCamp.cod_proyecto = cod_proyecto;
                    eCamp.cod_prospecto = objCamp.cod_prospecto;
                    eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                    eCamp.cod_origen_prospecto = "";
                    eCamp.cod_evento = String.IsNullOrEmpty(objCamp.cod_eventoProximo) ? "" : objCamp.cod_eventoProximo;
                    eCamp.cod_evento_cita_proxima = String.IsNullOrEmpty(objCamp.cod_evento_cita_proxima) ? "" : objCamp.cod_evento_cita_proxima;
                    eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(opcion, eCamp);

                    SplashScreenManager.CloseForm(false);

                    if (eCampResultado.cod_prospecto != null)
                    {
                        string msjProspecto = opcion == 3 ? " la cita" : " el prospecto";
                        string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                        MessageBox.Show("Se " + sMen + msjProspecto + " de manera satisfactoria", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                        CargarListado();
                        //fFiltrarGrilla();
                    }
                //}

            }
            else
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Prospecto", "Cargando...");
                foreach (int nRow in gvListaprospecto.GetSelectedRows())
                {
                    eCampanha objLT = gvListaprospecto.GetRow(nRow) as eCampanha;
                    //if ((opcion == 1 && AsesorSeleccionado != objLT.cod_ejecutivo) || (opcion == 3 && AsesorSeleccionado != objLT.cod_ejecutivo_citaProximo)) //validacion de cod_ejecutivo_cita
                    //{
                        eCampanha eCampResultado = new eCampanha();
                        eCampanha eCamp = new eCampanha();
                        eCamp.cod_ejecutivo = AsesorSeleccionado;
                        eCamp.cod_empresa = cod_empresa;
                        eCamp.cod_proyecto = cod_proyecto;
                        eCamp.cod_prospecto = objLT.cod_prospecto;
                        eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                        eCamp.cod_origen_prospecto = "";
                        eCamp.cod_evento = objLT.cod_eventoProximo;
                        eCamp.cod_evento_cita_proxima = String.IsNullOrEmpty(objCamp.cod_evento_cita_proxima) ? "" : objCamp.cod_evento_cita_proxima;
                        eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(opcion, eCamp);
                        //if (eCampResultado.cod_prospecto == null) { validar = 1; }
                    //}
                }
                SplashScreenManager.CloseForm(false);
                string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                MessageBox.Show("Se " + sMen + " los prospectos de manera satisfactoria", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                CargarListado();
                //fFiltrarGrilla();
            }
        }

        private void bbi_Asigna_ItemClick(object sender, ItemClickEventArgs e)
        {
            AsignarEjecutivoPersonalizado(1);
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantEvento frm = new frmMantEvento(this);
                frm.MiAccion = evento.Nuevo_externo;



                frm.cod_empresa = cod_empresa;
                frm.cod_proyecto = cod_proyecto;
                frm.sEstadoFiltro = sEstadoFiltro;
                frm.sTipoContactoFiltro = sTipoContactoFiltro;
                frm.sCod_ejecutivoFiltro = sCod_ejecutivoFiltro;
                frm.CodMenu = CodMenu;
                frm.IndicadorConfirmacionAuto = "";
                frm.cod_ejecutivo = "";
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
                if (gvListaprospecto.RowCount <= 0)
                {
                    MessageBox.Show("Seleccione un registro", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

                if (obj.flg_activo == "SI")
                {
                    XtraMessageBox.Show("El prospecto ya se encuentra activo.", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult msgresult = MessageBox.Show("¿Está seguro de activar el prospecto?", "Activar prospectos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCampanha eCamp_resultado = unit.Campanha.Activar_Inactivar_campanha<eCampanha>("4", obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    frmListadoProspecto_KeyDown(this, new KeyEventArgs(Keys.F5));
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
                    eCampanha eCamp_resultado = unit.Campanha.Activar_Inactivar_campanha<eCampanha>("5", obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    frmListadoProspecto_KeyDown(this, new KeyEventArgs(Keys.F5));
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
                if (gvListaprospecto.RowCount <= 0)
                {
                    MessageBox.Show("Seleccione un registro", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                eCampanha obj = gvListaprospecto.GetFocusedRow() as eCampanha;

                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar el prospecto?", "Eliminar prospectos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eCampanha eCamp_resultado = unit.Campanha.Eliminar_campanha<eCampanha>(obj.cod_campanha.ToString(), Program.Sesion.Usuario.cod_usuario.ToString());
                    if (eCamp_resultado.cod_campanha == "")
                    {
                        XtraMessageBox.Show(eCamp_resultado.mensaje, "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        frmListadoProspecto_KeyDown(this, new KeyEventArgs(Keys.F5));
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
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciCalendario.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (lci_Filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciCalendario.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
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
        private void ccCalendarioEventos_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (ListCalendario.Count > 0)
            {
                List<eCampanha> ListTemp = ListCalendario.Where(x => Convert.ToDateTime(x.fch_fecha) == Convert.ToDateTime(e.Date.ToString("dd/MM/yyyy"))).ToList();
                if (ListTemp.Count > 0)
                {
                    e.Style.BackColor = Color.FromArgb(146, 185, 99);
                }
                else
                {
                    e.Style.BackColor = Color.Transparent;
                }
            }
        }
        private void ccCalendarioEventos_EditValueChanged(object sender, EventArgs e)
        {
            ListTemp = ListCalendario.Where(x => Convert.ToDateTime(x.fch_fecha) == Convert.ToDateTime(ccCalendarioEventos.EditValue)).ToList();

            bsListaCalendario.DataSource = ListTemp;

            if (ListTemp.Count() <= 1) { layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; }
            else { layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }
        }

    }
}