using BE_GestionLotes;
using DevExpress.Data;
using DevExpress.Images;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Marketing
{
    public partial class frmListReportCitasAsis : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        List<eCampanha> ListFiltros = new List<eCampanha>();
        List<eCampanha> ListCitas = new List<eCampanha>();
        List<eTreeProyEtaStatus> listadoTreeList = new List<eTreeProyEtaStatus>();
        public int validar = 0, num_minutos = 5, validar_alerta = 0;
        public string cod_empresa = "", cod_proyecto = "", cod_prospecto = "";
        public string sEstadoFiltro = "", sTipoContactoFiltro = "", sCod_ejecutivoFiltro = "", CodMenu, DscMenu, sFiltroVisitasPendientes = "", sFiltroVisitasAsistidas = "";//, perfil = ""; 
        private readonly UnitOfWork unit;

        public object ChartElementType { get; private set; }

        public frmListReportCitasAsis()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurarFiltros();
        }
        private void configurarFiltros()
        {
            //unit.Globales.ConfigurarGridView_TreeStyle(gcFiltros, gvFiltros);
        }
        private void frmListReportCitasAsis_Load(object sender, EventArgs e)
        {
            //ListarFiltros();
            Inicializar();
            CargarTreeListTresNodos();
            mostrarFechas();
            ListarCitas();
            CargarListadoSemanas();

        }
        private void Inicializar()
        {
            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
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
                        if (j == 0)
                        {
                            tlEjecutivos.Nodes[i].Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;

                        }
                        else
                        {
                            tlEjecutivos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        }

                    }
                }
                tlEjecutivos.CheckAll();
                deseleccionarNodos(new List<string> { "TPCON003", "TPCON004", "TPCON005" });
            }
        }
        public void CargarListadoSemanas()
        {
            try
            {
                List<eReporteEvento> ListReporte = new List<eReporteEvento>();
                ListReporte = unit.Campanha.ListarGridFiltros<eReporteEvento>(2, dtFechaHasta.DateTime.Year, cod_empresa, cod_proyecto,
                    Program.Sesion.Usuario.cod_usuario, sCod_ejecutivoFiltro, sTipoContactoFiltro,
                    Convert.ToDateTime(dtFechaDesde.EditValue).ToString("yyyyMMdd"),
                    Convert.ToDateTime(dtFechaHasta.EditValue).ToString("yyyyMMdd"));
                bsGrafico.DataSource = ListReporte;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void deseleccionarNodos(List<string> nodesToUncheck)
        {
            nodesToUncheck.ForEach((n) => { tlEjecutivos.FindNodeByFieldValue("Codigo", n).Checked = false; });
        }
        void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
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

        private void gvFiltros_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private void mostrarFechas()
        {
            DateTime firstDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 21);
            firstDate = firstDate.AddMonths(-1);
            DateTime secondDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 20);
            dtFechaDesde.EditValue = firstDate;
            dtFechaHasta.EditValue = secondDate;
        }
        private void mostrarFechasPorReporte(int Month = 0)
        {
                DateTime firstDate = new DateTime(DateTime.Today.Year, Month, 20);
                DateTime secondDate = firstDate.AddMonths(-1).AddDays(1);
                dtFechaDesde.EditValue = secondDate;
                dtFechaHasta.EditValue = firstDate;
               
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                ListarCitas();
                CargarListadoSemanas();
                SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (layoutControlItem20.ContentVisible == true)
            {
                layoutControlItem20.ContentVisible = false;
                layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                return;
            }
            if (layoutControlItem20.ContentVisible == false)
            {
                layoutControlItem20.ContentVisible = true;
                layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                return;
            }
        }

        private void gvReporte_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void dtFechaDesde_EditValueChanged(object sender, EventArgs e)
        {
            if (dtFechaDesde.EditValue == null) { return; }
            dtFechaHasta.EditValue = dtFechaDesde.DateTime.AddMonths(1).AddDays(1);
        }

        private int GetMonthNumber(string monthName)
        {
            switch (monthName)
            {
                case "Enero":
                    return 1;
                case "Febrero":
                    return 2;
                case "Marzo":
                    return 3;
                case "Abril":
                    return 4;
                case "Mayo":
                    return 5;
                case "Junio":
                    return 6;
                case "Julio":
                    return 7;
                case "Agosto":
                    return 8;
                case "Septiembre":
                    return 9;
                case "Octubre":
                    return 10;
                case "Noviembre":
                    return 11;
                case "Diciembre":
                    return 12;
                default:
                    return 0;
            }
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                DevExpress.XtraCharts.ChartControl chart = sender as DevExpress.XtraCharts.ChartControl;

                if (chart.Series != null && chart.Series[0].Points.Count > 0)
                {
                    Point point = e.Location;
                    ChartHitInfo hitInfo = chart.CalcHitInfo(point);
                    if (hitInfo.InSeries && hitInfo.SeriesPoint != null)
                    {
                        SeriesPoint seriesPoint = hitInfo.SeriesPoint;
                        int month = GetMonthNumber(seriesPoint.Argument.ToString());
                        mostrarFechasPorReporte(month);
                        double yValue = seriesPoint.Values[0];
                        if (yValue > 0)
                        {
                            ListarCitas();
                        }
                        //MessageBox.Show($"Seleccionado el mes {month} con YValue={yValue}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           

        }

        private void gvFiltros_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            int level = view.GetRowLevel(e.RowHandle);
            int indent = level * 20;
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.Location.X + indent, e.Bounds.Location.Y, e.Bounds.Width, e.Bounds.Height));
            e.Handled = true;
        }
        public void ListarFiltros()
        {
            ListFiltros = unit.Campanha.ListarGridFiltros<eCampanha>(0);
            bsFiltros.DataSource = ListFiltros;
        }

        public void ListarCitas()
        {

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

            sCod_ejecutivoFiltro = etapaMultiple[1];
            sTipoContactoFiltro = etapaMultiple[2];

            ListCitas = unit.Campanha.ListarGridFiltros<eCampanha>(1, dtFechaHasta.DateTime.Year, cod_empresa, cod_proyecto,
                    Program.Sesion.Usuario.cod_usuario, sCod_ejecutivoFiltro, sTipoContactoFiltro,
                    Convert.ToDateTime(dtFechaDesde.EditValue).ToString("yyyyMMdd"),
                    Convert.ToDateTime(dtFechaHasta.EditValue).ToString("yyyyMMdd"));
            bsCitasEjecutivo.DataSource = ListCitas;
        }
        //public void AsigarDAtosReporte()
        //{
        //    etReporte.Porcentaje = 5.5;
        //    etReporte.Cantidad = 8;

        //}
        private void gvReporte_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (e.Column.FieldName == "YourColumnName" && e.Value1.ToString() == e.Value2.ToString())
            {
                e.Result = 0;
                e.Handled = true;
            }
        }

        private void gvReporte_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Tools.Exportar().ExportarExcel(gcReporte, "Eventos");
        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvReporte.ShowRibbonPrintPreview();
        }
        void AgruparPorEjecutivo()
        {
            coldsc_ejecutivo_cita.GroupIndex = 0;
            colfch_evento.SortIndex = 0;
            colfch_evento.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

        }
        void DesagruparPorEjecutivo()
        {
            coldsc_ejecutivo_cita.UnGroup();
            colfch_evento.SortIndex = 1;
            coldsc_ejecutivo_cita.SortIndex = 0;
            coldsc_ejecutivo_cita.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            colfch_evento.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
        }

        private void btnAgruparAsesor_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnAgruparAsesor.Caption != "Agrupar por Asesor")
            {
                btnAgruparAsesor.Caption = "Agrupar por Asesor";
                DesagruparPorEjecutivo();
                return;
            }
            if (btnAgruparAsesor.Caption != "Desagrupar")
            {
                btnAgruparAsesor.Caption = "Desagrupar";
                AgruparPorEjecutivo();
                return;
            }
        }
    }
}