using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.Images;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraPrinting.Caching;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Formularios.Gestion_Contratos;
using UI_GestionLotes.Formularios.Marketing;
using UI_GestionLotes.Formularios.Operaciones;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    public partial class frmSeparacionLotesProspectos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        System.Drawing.Image ImgRegistrado = Properties.Resources.pencil_16px;
        System.Drawing.Image imgValAdmin = Properties.Resources.manager_16px;
        System.Drawing.Image imgValBanco = Properties.Resources.bank_16px;
        System.Drawing.Image imgBoleteado = Properties.Resources.clipboard_16px;
        System.Drawing.Image imgTieneExtension = Properties.Resources.clone_figure_16px;
        System.Drawing.Image imgEsExtension = Properties.Resources.orthogonal_view_16px;
        private readonly UnitOfWork unit;
        Brush ConCriterios = Brushes.Green;
        Brush SinCriterios = Brushes.Red;
        Brush NAplCriterio = Brushes.Orange;
        Brush PenCriterio = Brushes.Yellow;
        Brush Mensaje = Brushes.Transparent;
        int markWidth = 16;

        List<eTreeProyEtaStatus> listadoTreeList = new List<eTreeProyEtaStatus>();
        List<eLotes_Separaciones> listSeparaciones = new List<eLotes_Separaciones>();
        List<eVariablesGenerales> lstStatus = new List<eVariablesGenerales>();
        List<eProyecto_Etapa> ListProyectoEtapa = new List<eProyecto_Etapa>();
        List<eVariablesGenerales> lstTipoLote = new List<eVariablesGenerales>();
        public eCampanha campos_prospecto = new eCampanha();
        BindingList<Option> dataSource = new BindingList<Option>();
        public string formName;

        public string cod_etapa = "", cod_etapasmultiple = "", cod_estado_separacion = "ALL";
        public string cod_proyecto = "", dsc_proyecto, cod_status = "ALL";
        public string cod_empresa = "";
        public string caption = "";
        int ctd_proyecto = 0;

        int validar = 0, validar2 = 0, size = 150;
        
        bool Buscar = false;

        public frmSeparacionLotesProspectos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmSeparacionLotesProspectos_Load(object sender, EventArgs e)
        {
            Inicializar();
            HabilitarBotones();
        }

        void VistaSimple()
        {
            colnum_etapa.UnGroup();
            coldsc_manzana.UnGroup();
            coldsc_periodo.UnGroup();
            coldsc_cliente.UnGroup();
            colnum_etapa.Visible = true;
        }
        void AgruparCliente()
        {
            colnum_etapa.UnGroup();
            coldsc_manzana.UnGroup();
            coldsc_periodo.UnGroup();
            coldsc_cliente.GroupIndex = 0;
            coldsc_manzana.Visible = false;
            colnum_etapa.Visible = false;
            //coldsc_manzana.GroupIndex = 1;

        }
        void AgruparPorLote()
        {
            coldsc_cliente.UnGroup();
            coldsc_periodo.UnGroup();
            colnum_etapa.GroupIndex = 0;
            coldsc_manzana.GroupIndex = 1;
            coldsc_periodo.SortOrder = DevExpress.Data.ColumnSortOrder.None;
            colfch_Separacion.SortIndex = 0;
            colfch_Separacion.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
        }
        void AgruparPeriodo()
        {
            colnum_etapa.UnGroup();
            coldsc_manzana.UnGroup();
            coldsc_cliente.UnGroup();
            colfch_Separacion.SortOrder = DevExpress.Data.ColumnSortOrder.None;
            coldsc_periodo.SortIndex = 0;
            coldsc_periodo.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            coldsc_periodo.GroupIndex = 0;
            colfch_Separacion.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            coldsc_manzana.Visible = false;
            colnum_etapa.Visible = false;
            //coldsc_manzana.GroupIndex = 1;

        }
        public void CargarListadoResumen()
        {
            DataTable odtDatos = new DataTable();
            odtDatos = unit.Proyectos.Listar_Separaciones_Resumen("3", cod_proyecto, cod_etapas_multiple: cod_etapasmultiple,
                    grdbFecha.EditValue.ToString(),
                    Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                    Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                    chkSeparacionActiva.Checked ? "SI" : "NO");
            //listSeparaciones = unit.Proyectos.ListarSeparaciones<eLotes_Separaciones>("1", cod_proyecto, cod_etapas_multiple: etapaMultiple);
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
                tbiRegistrado.Elements[2].Text = odtDatos.Rows[0][0].ToString() + " ";
                tbiValAdminis.Elements[2].Text = odtDatos.Rows[0][1].ToString() + " ";
                tbiValBanco.Elements[2].Text = odtDatos.Rows[0][2].ToString() + " ";
                tbiBoleteado.Elements[2].Text = odtDatos.Rows[0][3].ToString() + " ";
                tbiTotal.Elements[2].Text = odtDatos.Rows[0][4].ToString() + " ";
                tbiSeparado.Elements[2].Text = odtDatos.Rows[0][5].ToString() + " ";
                tbiFirmados.Elements[2].Text = odtDatos.Rows[0][6].ToString() + " ";
                tbiEmitidos.Elements[2].Text = odtDatos.Rows[0][7].ToString() + " ";
                tbiDesistido.Elements[2].Text = odtDatos.Rows[0][8].ToString() + " ";


            }
        }
        //internal void CargarOpcionesMenu()
        //{
        //    List<eVariablesGenerales> ListProyecEmp = unit.Proyectos.ListarOpcionesMenu<eVariablesGenerales>("3");
        //    Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
        //    Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

        //    NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
        //    NavEmpresa.Caption = "Por Estado"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
        //    NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
        //    NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

        //    foreach (eVariablesGenerales obj in ListProyecEmp)
        //    {
        //        NavBarItem NavDetalle = navBarControl1.Items.Add();
        //        NavDetalle.Tag = obj.cod_variable; NavDetalle.Name = obj.cod_empresa;
        //        NavDetalle.Caption = obj.dsc_Nombre; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

        //        NavEmpresa.ItemLinks.Add(NavDetalle);
        //    }
        //}
        //private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        //{
        //    lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
        //    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
        //    //CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
        //    SplashScreenManager.CloseForm();
        //}
         


        public void CargarListado(string opcion = "1",string flg_firmado = "")
        {
            try
            {
                cod_proyecto = "";
                cod_empresa = "";
                string dsc_descripcion = "";
                string proyectos = "", flag_activo = "";
                string etapas = "";

                var tools = new Tools.TreeListHelper(treeListProyectos);
                var proyectoMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(1);
                var etapaMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(2);
                cod_etapasmultiple = etapaMultiple;


                string[] aCodigos = proyectoMultiple.Split("|".ToCharArray());
                if (aCodigos.Length > 1)
                {
                    cod_proyecto = aCodigos[0];
                    cod_empresa = aCodigos[1];
                }
                else
                {
                    cod_empresa = "";
                    cod_proyecto = "";
                }
                //cod_proyecto = proyectoMultiple;

                listSeparaciones.Clear();
                listSeparaciones = unit.Proyectos.ListarSeparaciones<eLotes_Separaciones>(opcion, cod_proyecto,
                                cod_etapas_multiple: etapaMultiple,
                                grdbFecha.EditValue.ToString(),
                                Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                                cod_estado_separacion,
                                flag_activo = chkSeparacionActiva.Checked ? "SI" : "NO",
                                cod_status, flg_firmado :flg_firmado
                                );

                bsLotesSeparaciones.DataSource = null; bsLotesSeparaciones.DataSource = listSeparaciones; gvListaSeparaciones.RefreshData();

                string nTotal, nSeparados, nFirmado, nEmitido, nDesistido, nRegistrado, nValAdmin, nValBanco, nBoleteado;
                nTotal = listSeparaciones.Count().ToString();
                //if (cellValue == "DESISTIMIENTO" || obj.cod_status.ToString() == "ESE00003") { b = SinCriterios; } //rojo
                //else if (cellValue == "SEPARACION" && obj.contrato_firmado.ToString() == "NO") { b = NAplCriterio; } //naranja 
                //else if (cellValue == "VENDIDO" && obj.contrato_firmado.ToString() == "SI") { b = ConCriterios; } //verde
                //else { b = PenCriterio; } //amarillo
                if (cod_status == "ALL" && cod_estado_separacion == "ALL")
                {
                    tbiTotal.Elements[2].Text = nTotal + " ";
                    nSeparados = listSeparaciones.FindAll(x => x.cod_status == "ESE00002"  && (x.contrato_firmado != "NO" && x.contrato_firmado != "SI")).Count.ToString();
                    nEmitido = listSeparaciones.FindAll(x => x.cod_status == "ESE00002" && x.contrato_firmado == "NO").Count.ToString();
                    nFirmado = listSeparaciones.FindAll(x => x.cod_status == "ESE00001" && x.contrato_firmado == "SI").Count.ToString();
                    nDesistido = listSeparaciones.FindAll(x => x.cod_status == "ESE00003").Count.ToString();
                    tbiEmitidos.Elements[2].Text = nEmitido + " ";
                    tbiSeparado.Elements[2].Text = nSeparados + " ";
                    tbiFirmados.Elements[2].Text = nFirmado + " ";
                    tbiDesistido.Elements[2].Text = nDesistido + " ";
                    nRegistrado = listSeparaciones.FindAll(x => x.flg_registrado == "SI").Count.ToString();
                    nValAdmin = listSeparaciones.FindAll(x => x.flg_Val_Admin == "SI").Count.ToString();
                    nValBanco = listSeparaciones.FindAll(x => x.flg_Val_Banco == "SI").Count.ToString();
                    nBoleteado = listSeparaciones.FindAll(x => x.flg_Boleteado == "SI").Count.ToString();

                    tbiRegistrado.Elements[2].Text = nRegistrado + " ";
                    tbiValAdminis.Elements[2].Text = nValAdmin + " ";
                    tbiValBanco.Elements[2].Text = nValBanco + " ";
                    tbiBoleteado.Elements[2].Text = nBoleteado + " ";
                }
                if (cod_estado_separacion == "ALL")
                {
                    nRegistrado = listSeparaciones.FindAll(x => x.flg_registrado == "SI").Count.ToString();
                    nValAdmin = listSeparaciones.FindAll(x => x.flg_Val_Admin == "SI").Count.ToString();
                    nValBanco = listSeparaciones.FindAll(x => x.flg_Val_Banco == "SI").Count.ToString();
                    nBoleteado = listSeparaciones.FindAll(x => x.flg_Boleteado == "SI").Count.ToString();

                    tbiRegistrado.Elements[2].Text = nRegistrado + " ";
                    tbiValAdminis.Elements[2].Text = nValAdmin + " ";
                    tbiValBanco.Elements[2].Text = nValBanco + " ";
                    tbiBoleteado.Elements[2].Text = nBoleteado + " ";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void ListarRadiosCheck()
        {
            treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            for (int i = 0; i < treeListProyectos.Nodes.Count; i++)
            {
                treeListProyectos.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                for (int j = 0; j < treeListProyectos.Nodes[i].Nodes.Count(); j++)
                {
                    treeListProyectos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                    //final
                    //for (int k = 0; k < treeListProyectos.Nodes[i].Nodes[j].Nodes.Count(); k++)
                    //{
                    //    treeListProyectos.Nodes[i].Nodes[j].Nodes[k].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                    //}

                }
            }



            //ANTES SE ASIA ASI

            //treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            //treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
            //for (int x = 0; x <= ctd_proyecto - 1; x++)
            //{
            //    treeListProyectos.Nodes[0].Nodes[x].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            //}
            //treeListProyectos.CheckAll();
            treeListProyectos.ExpandAll();
        }

        public void CargarListadoEtapas(string accion)
        {
            try
            {

                ListProyectoEtapa = unit.Proyectos.ListarEtapa<eProyecto_Etapa>(accion, cod_etapa, cod_proyecto);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
            }
        }

        private void Inicializar()
        {
            CargarTreeListTresNodos();
            //InitTreeList();
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            pcChevron.BackColor = Program.Sesion.Colores.Verde;
            layoutControlItem12.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            layoutControlItem10.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            layoutControlItem3.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            //lblTitulo.Text = "SEPARACIONES: " + navBarControl2.Groups[0].Caption.ToUpper();

            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            gpFechas.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            //lblTitulo.Text = navBarControl2.Groups[0].Caption + ": MÁS RECIENTE";
            //picTitulo.Image = navProyecto.ImageOptions.LargeImage;
            navBarControl2.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = new DateTime(DateTime.Today.Year, 1, 1);
            dtFechaFin.EditValue = oUltimoDiaDelMes;
            grdbFecha.SelectedIndex = 0;

            //CargarOpcionesMenu();
            //CargarListado("TODOS", "");

            //lblTitulo.ForeColor = Program.Sesion.Colores.Verde;

            //lblTitulo.Text = navBarControl1.SelectedLink.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            //picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            //navBarControl1.Groups[0].SelectedLinkIndex = 0;


            CargarListado();
            //CargarListadoResumen();
            vistaResumen();
            cargarTitulo();
            System.Drawing.Image img = Properties.Resources.select_users_32px;
            btnAgruparCliente.ImageOptions.LargeImage = img;
            btnAgruparCliente.Caption = "Agrupar por Cliente";
            VistaSimple();
            gvListaSeparaciones.RefreshData();

            //btnOcultarFiltro.PerformClick(); 
        }

        private void CargarTreeListCuatroNodos()
        {
            var listadoEstado = unit.Proyectos.ListarOpcionesMenu<eTreeProyEtaStatus>("4");
            if (listadoEstado != null && listadoEstado.Count > 0)
            {
                new Tools.TreeListHelper(treeListProyectos).
                    TreeViewParaCuatroNodos<eTreeProyEtaStatus>(
                    listadoEstado, "cod_pro", "dsc_pro",
                    "cod_proyecto", "dsc_proyecto",
                    "cod_etapa", "dsc_etapa", "cod_estado", "dsc_estado");

                treeListProyectos.Refresh();

                //treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                //treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;


                treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int i = 0; i < treeListProyectos.Nodes.Count; i++)
                {
                    treeListProyectos.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                    for (int j = 0; j < treeListProyectos.Nodes[i].Nodes.Count(); j++)
                    {
                        treeListProyectos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                        //final
                        for (int k = 0; k < treeListProyectos.Nodes[i].Nodes[j].Nodes.Count(); k++)
                        {
                            treeListProyectos.Nodes[i].Nodes[j].Nodes[k].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        }

                    }
                }
                treeListProyectos.CheckAll();

            }
        }

        private void CargarTreeListTresNodos()
        {
            treeListProyectos.Appearance.Row.BackColor = Color.Transparent;
            treeListProyectos.Appearance.Empty.BackColor = Color.Transparent;
            treeListProyectos.BackColor = Color.Transparent;
            treeListProyectos.TreeViewFieldName = "Name";
            treeListProyectos.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            treeListProyectos.OptionsBehavior.Editable = false;
            treeListProyectos.OptionsBehavior.ReadOnly = true;
            treeListProyectos.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeListProyectos.NodeCellStyle += OnNodeCellStyle;
            treeListProyectos.BeforeFocusNode += OnBeforeFocusNode;

            listadoTreeList = unit.Proyectos.ListarOpcionesMenu<eTreeProyEtaStatus>("5");
            if (listadoTreeList != null && listadoTreeList.Count > 0)
            {
                new Tools.TreeListHelper(treeListProyectos).
                    TreeViewParaTresNodos<eTreeProyEtaStatus>(
                    listadoTreeList, "cod_pro", "dsc_pro",
                    "cod_proyecto", "dsc_proyecto",
                    "cod_etapa", "dsc_etapa");

                treeListProyectos.Refresh();

                //treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                //treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;


                treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int i = 0; i < treeListProyectos.Nodes.Count; i++)
                {
                    treeListProyectos.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                    for (int j = 0; j < treeListProyectos.Nodes[i].Nodes.Count(); j++)
                    {
                        treeListProyectos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        //final
                        //for (int k = 0; k < treeListProyectos.Nodes[i].Nodes[j].Nodes.Count(); k++)
                        //{
                        //    treeListProyectos.Nodes[i].Nodes[j].Nodes[k].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        //}

                    }
                }
                treeListProyectos.CheckAll();

            }
        }

        BindingList<Option> GenerateDataSource()
        {
            BindingList<Option> _options = new BindingList<Option>();

            List<eProyecto> ListProyectos = unit.Proyectos.ListarProyectos<eProyecto>("4", "", "");
            ctd_proyecto = ListProyectos.Count;
            _options.Add(new Option() { ParentID = "0", ID = "1", Name = "PROYECTO", Checked = true });
            foreach (eProyecto obj in ListProyectos)
            {
                _options.Add(new Option() { ParentID = "1", ID = obj.cod_proyecto, Name = obj.dsc_nombre, Checked = true });

                List<eProyecto_Etapa> ListEtapas = unit.Proyectos.ListarEtapa<eProyecto_Etapa>("3", "", obj.cod_proyecto);
                foreach (eProyecto_Etapa objEtapa in ListEtapas)
                {
                    _options.Add(new Option() { ParentID = obj.cod_proyecto, ID = obj.cod_proyecto + "-" + objEtapa.cod_etapa, Name = objEtapa.dsc_descripcion, Checked = true });
                }
            }

            return _options;
        }
        class Option : INotifyPropertyChanged
        {
            public string ParentID { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            bool? checkedCore = false;

            public event PropertyChangedEventHandler PropertyChanged;

            public bool? Checked
            {
                get { return checkedCore; }
                set
                {
                    if (checkedCore == value)
                        return;
                    checkedCore = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        private void gvListaLotesProyectos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetRow(e.RowHandle) as eLotes_Separaciones;
                    if (e.Column.FieldName == "fch_Separacion" && obj.fch_Separacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_vct_separacion" && obj.fch_vct_separacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago_separacion" && obj.fch_pago_separacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago" && obj.fch_pago.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_vct_cuota" && obj.fch_vct_cuota.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_cambio" && obj.fch_cambio.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_registro" && obj.fch_registro.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "dsc_status") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_registrado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Val_Admin") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Val_Banco") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_Boleteado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_tiene_extension") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_es_extension") e.DisplayText = "";

                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Ene")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Ene", "1"); /*e.CellValue = 1;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Feb")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Feb", "2"); /*e.CellValue = 2;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Mar")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Mar", "3"); /*e.CellValue = 3;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Abr")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Abr", "4"); /*e.CellValue = 4;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("May")) e.DisplayText = obj.dsc_periodo.ToString().Replace("May", "5"); /*e.CellValue = 5;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Jun")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Jun", "6"); /*e.CellValue = 6;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Jul")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Jul", "7"); /*e.CellValue = 7;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Ago")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Ago", "8"); /*e.CellValue = 8;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Sep")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Sep", "9"); /*e.CellValue = 9;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Oct")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Oct", "10");/* e.CellValue = 10;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Nov")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Nov", "11");/* e.CellValue = 11;*/
                    //if (e.Column.FieldName == "dsc_periodo" && obj.dsc_periodo.ToString().Substring(obj.dsc_periodo.ToString().Length - 3, 3).Contains("Dic")) e.DisplayText = obj.dsc_periodo.ToString().Replace("Dic", "12");/* e.CellValue = 12;*/

                    e.DefaultDraw();
                    if (e.Column.FieldName == "dsc_status")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "DESISTIMIENTO") { b = SinCriterios; } //rojo
                        else if (cellValue == "SEPARACION" && obj.contrato_firmado.ToString() == "NO") { b = NAplCriterio; } //naranja 
                        else if (cellValue == "VENDIDO" && obj.contrato_firmado.ToString() == "SI") { b = ConCriterios; } //verde
                        else { b = PenCriterio; } //amarillo
                        //b = ConCriterios;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                    if (e.Column.FieldName == "flg_registrado" && obj.flg_registrado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgRegistrado, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_Val_Admin" && obj.flg_Val_Admin == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgValAdmin, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_Val_Banco" && obj.flg_Val_Banco == "SI")
                    {

                        e.Handled = true; e.Graphics.DrawImage(imgValBanco, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_Boleteado" && obj.flg_Boleteado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgBoleteado, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_tiene_extension" && obj.flg_tiene_extension == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgTieneExtension, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    if (e.Column.FieldName == "flg_es_extension" && obj.flg_es_extension == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgEsExtension, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));

                    }
                    //if (e.Column.FieldName == "ctd_CECO") e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void gvListaLotesProyectos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaLotesProyectos_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //int offset = 5, posInical = 0;
            //e.DefaultDraw(); e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Brush b = Mensaje; Rectangle markRectangle;
            //string priorityText = "Leyenda :";
            //for (int i = 0; i < 4; i++)
            //{
            //    if (i == 1) { b = ConCriterios; priorityText = " - Lotes vendidos"; }
            //    if (i == 2) { b = NAplCriterio; priorityText = " - Lotes separados"; }
            //    if (i == 3) { b = SinCriterios; priorityText = " - Lotes que desistieron"; }
            //    //markRectangle = new Rectangle(e.Bounds.X + offset, e.Bounds.Y + offset + (markWidth + offset) * i, markWidth, markWidth);
            //    //markRectangle = new Rectangle(e.Bounds.X * (i * 200) + offset, e.Bounds.Y + 10, markWidth, markWidth);
            //    posInical = i == 0 ? 0 : i == 1 ? 120 : i == 2 ? 400 : 680;
            //    markRectangle = new Rectangle(e.Bounds.X * (posInical) + offset, e.Bounds.Y + 10, markWidth, markWidth);
            //    e.Graphics.FillEllipse(b, markRectangle);
            //    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
            //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            //    e.Appearance.Options.UseTextOptions = true;
            //    e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            //}
        }

        public void asignarProspectoLote()
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Asignando prospectos", "Cargando...");
                //SplashScreen.Open("Asignando prospectos", "Cargando...");

                eProspectosXLote objLotPro = new eProspectosXLote();
                foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                {
                    eLotesxProyecto objLT = gvListaSeparaciones.GetRow(nRow) as eLotesxProyecto;
                    //objLT.imp_precio_m_cuadrado = Convert.ToDecimal(rbtntxtPrecioM2Tarifario.Text);
                    //objLT.imp_precio_total = objLT.num_area_uex * objLT.imp_precio_m_cuadrado;
                    objLotPro.cod_lote = objLT.cod_lote;
                    objLotPro.cod_empresa = objLT.cod_empresa;
                    objLotPro.cod_proyecto = objLT.cod_proyecto;
                    objLotPro.cod_prospecto = campos_prospecto.cod_prospecto;
                    objLotPro.cod_etapa = objLT.cod_etapa;
                    objLotPro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    objLotPro = unit.Proyectos.MantenimientoLotesXProspecto<eProspectosXLote>(objLotPro);
                    if (objLotPro.mensaje != null)
                    {
                        MessageBox.Show(objLotPro.mensaje.ToString(), "Lote Asignado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //SplashScreen.Close();
                        SplashScreenManager.CloseForm(false);


                        //SplashScreenManager.CloseForm();
                        gvListaSeparaciones.RefreshData();
                        gvListaSeparaciones.ClearSelection();
                        CargarListado();
                        CargarListadoResumen();
                        return;
                    }
                    else
                    {
                        cambiarStatusLote(objLT.cod_lote.ToString(), "ASIGNADO");
                    }
                }

                SplashScreenManager.CloseForm(false);

                //SplashScreenManager.CloseForm();
                gvListaSeparaciones.RefreshData();
                gvListaSeparaciones.ClearSelection();
                CargarListado();
                CargarListadoResumen();
                //cambiarStatusLote();


            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        public void cambiarStatusLote(string cod_lote, string status)
        {
            try
            {

                eLotesxProyecto objLotPro = new eLotesxProyecto();



                foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                {
                    eLotes_Separaciones objLT = gvListaSeparaciones.GetRow(nRow) as eLotes_Separaciones;
                    //objLT.cod_status = status;
                    objLotPro.cod_lote = objLT.cod_lote;
                    objLotPro.cod_lote = objLT.cod_lote;
                    //eLotesxProyecto obj = lstStatus.Find(x => x.cod_lote == cod_lote);
                    if (objLT.cod_lote.ToString() == cod_lote)
                    {
                        //objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("2", objLT);
                    }

                }






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarComboEnGrid(int opcion)
        {
            try
            {
                //lstStatus = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("Status");
                //rlkpStatus.DataSource = lstStatus;
                ////lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLote");
                //if (opcion == 1)
                //{
                //    rlkpTipoLote.DataSource = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLotes");
                //    coldsc_documento.OptionsColumn.AllowEdit = false;
                //}
                //else
                //{
                //    lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLoteEtapa", cod_etapa);
                //    rlkpTipoLote.DataSource = lstTipoLote;
                //    coldsc_documento.OptionsColumn.AllowEdit = true;
                //}


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvListaLotesProyectos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnAsignarVenta_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaSeparaciones.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Vender Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Vender la(s) separación(es)?", "Vender Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                    {
                        eLotes_Separaciones objLT = gvListaSeparaciones.GetRow(nRow) as eLotes_Separaciones;
                        if (objLT.cod_status == "ESE00002")
                        {
                            string result = unit.Proyectos.Actualizar_Status_Separacion("2", objLT.cod_separacion, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_lote, Program.Sesion.Usuario.cod_usuario);
                            if (result == "OK")
                            {
                                //cambiarStatusLote(objLT.cod_lote.ToString(), "LIBRE");
                            }
                            gvListaSeparaciones.RefreshData();
                        }
                        else
                        {
                            validar = 1;
                        }
                    }
                }
                if (validar == 1)
                {
                    MessageBox.Show("Solo se modificó las separaciones con estado separado.", "Vender Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                CargarListado();
                CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //if (gvListaSeparaciones.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Prospecto por Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            //formName = "Prospecto";

            //frmAsignarProspecto frm = new frmAsignarProspecto(this, null);

            //
            //
            //
            //
            //frm.cod_empresa = cod_empresa;
            //frm.cod_proyecto = cod_proyecto;

            //frm.user = user;
            //frm.ShowDialog();

        }

        public void limpiarFiltroTbi()
        {
            cod_status = "ALL"; 
            cod_estado_separacion = "ALL";
            tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
            tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
            tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
            tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
            tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarFiltroTbi();
            CargarListado();
            //CargarListadoResumen();
            cargarTitulo();
            //colfch_Separacion.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gvListaSeparaciones.RefreshData();
        }


        void cargarTitulo()
        {
            eTreeProyEtaStatus objM = listadoTreeList.Find(x => x.cod_proyecto == cod_proyecto + "|" + cod_empresa);
            lblTitulo.Text = objM.dsc_proyecto;
            //lblTitulo.Text = objM.dsc_pro.ToString() + " : " + objM.dsc_proyecto.ToString();
            dsc_proyecto = objM.dsc_proyecto;

        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {

            //navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            //navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            //navBarControl2.OptionsNavPane.CollapsedWidth = 50;
            //navBarControl2.OptionsNavPane.ExpandedWidth = 200;
            if (layoutControlItem6.ContentVisible == true)
            {
                layoutControlItem6.ContentVisible = false;
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                return;
            }
            if (layoutControlItem6.ContentVisible == false)
            {
                layoutControlItem6.ContentVisible = true;
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                return;
            }
        }

        private void btnSeleccionMultriple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvListaSeparaciones.OptionsSelection.MultiSelect == true)
            {
                gvListaSeparaciones.OptionsSelection.MultiSelect = false; return;
            }
            if (gvListaSeparaciones.OptionsSelection.MultiSelect == false)
            {
                gvListaSeparaciones.OptionsSelection.MultiSelect = true; return;
            }
        }

        private void gvListaLotesProyectos_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            gvListaSeparaciones.RefreshData();
            //if (gvListaSeparaciones.GetSelectedRows().Length > 0)
            //{
            //    int broke = 0;
            //    var validate = 0;
            //gvListaLotesProyectos.RefreshData();            
            //if (gvListaSeparaciones.GetSelectedRows()[0] != 0)
            //{
            //    validate = gvListaSeparaciones.GetSelectedRows()[0];

            //}
            //else
            //{
            //    if (gvListaSeparaciones.GetSelectedRows().Length > 1)
            //    {
            //        validate = gvListaSeparaciones.GetSelectedRows()[1];
            //    }
            //    else
            //    {
            //        eLotesxProyecto obj = gvListaSeparaciones.GetFocusedRow() as eLotesxProyecto;
            //        if (obj.configurado == "SI")
            //        {
            //            btnAsignarProspecto.Enabled = false;
            //            btnLiberarLote.Enabled = true;
            //        }
            //        if (obj.configurado == "NO")
            //        {
            //            btnAsignarProspecto.Enabled = true;
            //            btnLiberarLote.Enabled = false;
            //        }
            //        return;
            //    }
            //}

            //eLotes_Separaciones validar = gvListaSeparaciones.GetRow(validate) as eLotes_Separaciones;
            //cod_proyecto = validar.cod_proyecto;
            //cod_etapa = validar.cod_etapa;
            //if (validar.configurado == "SI")
            //{
            //    btnAsignarProspecto.Enabled = false;
            //    btnLiberarProspecto.Enabled = true;
            //}
            //if (validar.configurado == "NO")
            //{
            //        btnAsignarProspecto.Enabled = true;
            //        btnLiberarProspecto.Enabled = false;


            //}
            //    foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
            //    {
            //        eLotesxProyecto objLT = gvListaSeparaciones.GetRow(nRow) as eLotesxProyecto;

            //        if (objLT.cod_etapa != validar.cod_etapa)
            //        {

            //            gvListaSeparaciones.ClearSelection();
            //            gvListaSeparaciones.RefreshData();
            //        }

            //        if (objLT.configurado == "SI")
            //        {
            //            broke = 1;
            //            btnAsignarProspecto.Enabled = false;
            //            btnLiberarLote.Enabled = true;
            //        }
            //        if (objLT.configurado == "NO")
            //        {
            //            if (broke == 0)
            //            {
            //                btnAsignarProspecto.Enabled = true;
            //                btnLiberarLote.Enabled = false;
            //            }

            //        }
            //    }
            //}
            //if (gvListaSeparaciones.GetSelectedRows().Length == 0)
            //{
            //    btnAsignarProspecto.Enabled = true;
            //    btnLiberarLote.Enabled = true;
            //}
        }

        private void btnLiberarProspecto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaSeparaciones.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Desistir Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Está seguro de desistir la(s) separación(es)? \nEsta acción es irreversible.", "Desistir Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                    {
                        eLotes_Separaciones objLT = gvListaSeparaciones.GetRow(nRow) as eLotes_Separaciones;
                        if (objLT.cod_status == "ESE00002")
                        {

                            string result = unit.Proyectos.Actualizar_Status_Separacion("1", objLT.cod_separacion, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_lote, Program.Sesion.Usuario.cod_usuario);
                            if (result == "OK")
                            {
                                //cambiarStatusLote(objLT.cod_lote.ToString(), "LIBRE");
                            }
                            gvListaSeparaciones.RefreshData();


                        }
                        else
                        {
                            validar = 1;
                        }

                    }
                    if (validar == 1)
                    {
                        MessageBox.Show("Solo se modificó los separados", "Desistir Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    }

                }
                CargarListado();
                CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvListaSeparaciones_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                    frmSepararLote frm = new frmSepararLote(this);
                    frm.codigo = obj.cod_proyecto;
                    frm.dsc_proyecto = dsc_proyecto;
                    frm.cod_separacion = obj.cod_separacion;
                    frm.codigoMultiple = obj.cod_etapa;
                    frm.cod_status = obj.cod_status;
                    frm.flg_activo = obj.flg_activo;
                    //frm.cod_cliente = obj.cod_cliente;
                    frm.MiAccion = obj.cod_status != "ESE00002" || obj.flg_activo == "NO" ? Separacion.Vista : Separacion.Editar;


                    //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiRegistrado_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "RE";
                CargarListado();
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiValAdminis_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "VA";
                CargarListado();
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiValBanco_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "VB";
                CargarListado();
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiBoleteado_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "BO";
                CargarListado();
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVistaDetallada_ItemClick(object sender, ItemClickEventArgs e)
        {
            vistaDetallada();
        }

        private void vistaDetallada()
        {
            colimp_cuota_inicial.Visible = true;
            colnum_cuotas.Visible = true;
            colimp_valor_cuota.Visible = true;
            colfch_vct_cuota.Visible = true;
            colprc_descuento.Visible = true;
            colimp_precio_final.Visible = true;
            //gvListaLotesProyectos.Columns["dsc_lote"].VisibleIndex = 0;

            gvListaSeparaciones.Columns["dsc_lote"].VisibleIndex = 1;
            gvListaSeparaciones.Columns["dsc_status"].VisibleIndex = 2;
            gvListaSeparaciones.Columns["flg_tiene_extension"].VisibleIndex = 3;
            gvListaSeparaciones.Columns["flg_es_extension"].VisibleIndex = 4;
            gvListaSeparaciones.Columns["flg_Val_Admin"].VisibleIndex = 5;
            gvListaSeparaciones.Columns["flg_Val_Banco"].VisibleIndex = 6;
            gvListaSeparaciones.Columns["flg_Boleteado"].VisibleIndex = 7;
            //gvListaSeparaciones.Columns["dsc_estado_separacion"].VisibleIndex = 2;
            gvListaSeparaciones.Columns["fch_Separacion"].VisibleIndex = 8;
            gvListaSeparaciones.Columns["dsc_cliente"].VisibleIndex = 9;
            gvListaSeparaciones.Columns["dsc_asesor"].VisibleIndex = 10;
            gvListaSeparaciones.Columns["imp_separacion"].VisibleIndex = 11;
            //gvListaSeparaciones.Columns["imp_separacion_uex"].VisibleIndex = 12;
            //gvListaSeparaciones.Columns["imp_separacion_uco"].VisibleIndex = 13;
            gvListaSeparaciones.Columns["fch_vct_separacion"].VisibleIndex = 12;
            gvListaSeparaciones.Columns["dsc_forma_pago"].VisibleIndex = 13;
            gvListaSeparaciones.Columns["fch_pago"].VisibleIndex = 14;
            gvListaSeparaciones.Columns["imp_precio_total"].VisibleIndex = 15;
            gvListaSeparaciones.Columns["prc_descuento"].VisibleIndex = 16;
            gvListaSeparaciones.Columns["imp_descuento"].VisibleIndex = 17;
            gvListaSeparaciones.Columns["imp_precio_con_descuento"].VisibleIndex = 18;
            gvListaSeparaciones.Columns["imp_cuota_inicial"].VisibleIndex = 19;
            gvListaSeparaciones.Columns["imp_precio_final"].VisibleIndex = 20;
            gvListaSeparaciones.Columns["num_cuotas"].VisibleIndex = 21;
            gvListaSeparaciones.Columns["imp_valor_cuota"].VisibleIndex = 22;
            gvListaSeparaciones.Columns["fch_vct_cuota"].VisibleIndex = 23;
            //gvListaSeparaciones.Columns["dsc_observacion"].VisibleIndex = 24;
        }

        private void vistaResumen()
        {
            colfch_vct_cuota.Visible = false;
            colprc_descuento.Visible = false;
            colnum_area_uex.Visible = true;
            coldsc_forma_pago.Visible = true;
            colimp_cuota_inicial.Visible = true;
            colimp_precio_final.Visible = true;
            colimp_valor_cuota.Visible = true;
            colnum_cuotas.Visible = true;
            coldsc_tipo_lote.Visible = true;

            //colimp_separacion_uex.Visible = false;
            //colimp_separacion_uco.Visible = false;
            //colimp_precio_final_uex.Visible = false;
            //colimp_precio_final_uco.Visible = false;
            //colfch_pago_separacion.Visible = false;
            //coldsc_observacion.Visible = false;
            gvListaSeparaciones.Columns["dsc_lote"].VisibleIndex = 1;
            gvListaSeparaciones.Columns["dsc_tipo_lote"].VisibleIndex = 2;
            gvListaSeparaciones.Columns["dsc_asesor"].VisibleIndex = 3;
            gvListaSeparaciones.Columns["num_etapa"].VisibleIndex = 4;
            gvListaSeparaciones.Columns["num_area_uex"].VisibleIndex = 5;
            gvListaSeparaciones.Columns["dsc_cliente"].VisibleIndex = 6;
            gvListaSeparaciones.Columns["dsc_status"].VisibleIndex = 7;
            gvListaSeparaciones.Columns["dsc_forma_pago"].VisibleIndex = 8;
            gvListaSeparaciones.Columns["fch_Separacion"].VisibleIndex = 9;
            gvListaSeparaciones.Columns["imp_separacion"].VisibleIndex = 10;
            gvListaSeparaciones.Columns["fch_pago"].VisibleIndex = 11;
            gvListaSeparaciones.Columns["imp_precio_total"].VisibleIndex = 12;
            gvListaSeparaciones.Columns["imp_descuento"].VisibleIndex = 13;
            gvListaSeparaciones.Columns["imp_precio_con_descuento"].VisibleIndex = 14;
            gvListaSeparaciones.Columns["imp_cuota_inicial"].VisibleIndex = 15;
            gvListaSeparaciones.Columns["imp_precio_final"].VisibleIndex = 16;
            gvListaSeparaciones.Columns["imp_valor_cuota"].VisibleIndex = 17;
            gvListaSeparaciones.Columns["num_cuotas"].VisibleIndex = 18;

            gvListaSeparaciones.Columns["flg_tiene_extension"].Visible = false;
            gvListaSeparaciones.Columns["flg_es_extension"].Visible = false;
            gvListaSeparaciones.Columns["flg_Val_Admin"].Visible = false;
            gvListaSeparaciones.Columns["flg_Val_Banco"].Visible = false;
            gvListaSeparaciones.Columns["flg_Boleteado"].Visible = false;
            //gvListaSeparaciones.Columns["dsc_estado_separacion"].VisibleIndex = 2;
            //gvListaSeparaciones.Columns["imp_separacion_uex"].VisibleIndex = 12;
            //gvListaSeparaciones.Columns["imp_separacion_uco"].VisibleIndex = 13;
            gvListaSeparaciones.Columns["fch_vct_separacion"].Visible = false;
            
            //gvListaSeparaciones.Columns["imp_precio_final"].VisibleIndex = 18;
            //gvListaSeparaciones.Columns["imp_precio_final_uex"].VisibleIndex = 21;
            //gvListaSeparaciones.Columns["imp_precio_final_uco"].VisibleIndex = 22;
            //gvListaSeparaciones.Columns["fch_pago_separacion"].VisibleIndex = 23;
            //gvListaSeparaciones.Columns["dsc_observacion"].VisibleIndex = 24;
        }

        private void btnVistaResumen_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnVistaResumen.Caption != "Vista Resumen")
            {
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/filter%20elements/combobox_32x32.png");
                btnVistaResumen.ImageOptions.LargeImage = img;
                btnVistaResumen.Caption = "Vista Resumen";
                vistaDetallada();
                return;
            }
            if (btnVistaResumen.Caption != "Vista Detallada")
            {
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/filter%20elements/listbox_32x32.png");
                btnVistaResumen.ImageOptions.LargeImage = img;
                btnVistaResumen.Caption = "Vista Detallada";
                vistaResumen();
                return;
            }

        }

        private void navBarControl2_Paint(object sender, PaintEventArgs e)
        {
            ListarRadiosCheck();
        }

        public void frmSeparacionLotesProspectos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //SplashScreen.Open("Obteniendo listado", "Cargando...");

                limpiarFiltroTbi();
                CargarListado();
                //CargarListadoResumen();
                SplashScreenManager.CloseForm(false);

                //SplashScreen.Close();

                //SplashScreenManager.CloseForm();
            }
        }



        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmSepararLote frm = new frmSepararLote();
                frm.MiAccion = Separacion.Nuevo;
                frm.codigo = cod_proyecto;
                frm.codigoMultiple = cod_etapasmultiple;
                frm.cod_empresa = cod_empresa;
                frm.dsc_proyecto = dsc_proyecto;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiSeparado_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "ALL";
                cod_status = "ESE00002";
                CargarListado("4", flg_firmado: "");
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiDesistido.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiVendido.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiSeparado.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pcChevron_Click(object sender, EventArgs e)
        {
            if (validar == 0)
            {
                System.Drawing.Image imgProyectoLogo = Properties.Resources.chevron_down_20px;
                pcChevron.EditValue = imgProyectoLogo;
                validar = 1;
                lytOcultar.ContentVisible = false;
                lytOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                pcChevron.ToolTipTitle = "Mostrar Filtros";
                return;
            }
            if (validar == 1)
            {
                System.Drawing.Image imgProyectoImagen = Properties.Resources.chevron_up_20px;
                pcChevron.EditValue = imgProyectoImagen;
                validar = 0;
                lytOcultar.ContentVisible = true;
                lytOcultar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                pcChevron.ToolTipTitle = "Ocultar Filtros";
                return;
            }
        }

        private void tileBarSeguimiento_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            tileBarEstadoDeSeparacion.AllowSelectedItem = false;
            tileBarEstadoDeSeparacion.AllowSelectedItem = true;
            tileBarTotal.AllowSelectedItem = false;
            tileBarTotal.AllowSelectedItem = true;

        }

        private void tileBarEstadoDeSeparacion_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            tileBarSeguimiento.AllowSelectedItem = false;
            tileBarSeguimiento.AllowSelectedItem = true;
            tileBarTotal.AllowSelectedItem = false;
            tileBarTotal.AllowSelectedItem = true;
        }

        private void tileBarTotal_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            tileBarSeguimiento.AllowSelectedItem = false;
            tileBarSeguimiento.AllowSelectedItem = true;
            tileBarEstadoDeSeparacion.AllowSelectedItem = false;
            tileBarEstadoDeSeparacion.AllowSelectedItem = true;
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            frmPopupProyectoInfo frm = new frmPopupProyectoInfo();
            frm.cod_proyecto = cod_proyecto;
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
        }

        private void gvListaSeparaciones_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "dsc_lote")
                {
                    e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }
        }

        private void btnReciboSimple_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //frmProbandoRich prueba = new frmProbandoRich();
                //prueba.ShowDialog();

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado...", "Cargando...");
                //SplashScreen.Open("Obteniendo listado", "Cargando...");

                eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                if (obj == null) { MessageBox.Show("Debe seleccionar lote.", "Ficha del lote", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); SplashScreenManager.CloseForm(false); return; }
                //var storage = new MemoryDocumentStorage();

                RichEditControl edit = new RichEditControl();
                rptFichaReciboSimple report = new rptFichaReciboSimple();
                ReportPrintTool printTool = new ReportPrintTool(report);
                //detalleLotes printTool = new detalleLotes(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_proyecto"].Value = obj.cod_proyecto;
                report.Parameters["cod_separacion"].Value = obj.cod_separacion;
                //var cachedReportSource = new CachedReportSource(report, storage);
                //report.BackColor = Color.FromArgb(0, 157, 150);
                report.ShowRibbonPreview();
                //cachedReportSource.ShowRibbonPreview();
                //SplashScreen.Close();
                SplashScreenManager.CloseForm(false);

                //SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreen.Close();
                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFormatoSimple_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                
                if (obj == null) { MessageBox.Show("Debe seleccionar lote.", "Ficha del lote", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); SplashScreenManager.CloseForm(false);  return; }


                if (obj.cod_forma_pago == "FI")
                {
                    rptFichaSeparacionFinanciado reportFinanciado = new rptFichaSeparacionFinanciado();
                    ReportPrintTool printTool = new ReportPrintTool(reportFinanciado);
                    //detalleLotes printTool = new detalleLotes(report);
                    reportFinanciado.RequestParameters = false;
                    printTool.AutoShowParametersPanel = false;
                    reportFinanciado.Parameters["cod_proyecto"].Value = obj.cod_proyecto;
                    reportFinanciado.Parameters["cod_separacion"].Value = obj.cod_separacion;
                    //report.BackColor = Color.FromArgb(0, 157, 150);
                    reportFinanciado.ShowPreviewDialog();
                }
                else
                {
                    rptFichaSeparacionContado reportContado = new rptFichaSeparacionContado();
                    ReportPrintTool printTool = new ReportPrintTool(reportContado);
                    //detalleLotes printTool = new detalleLotes(report);
                    reportContado.RequestParameters = false;
                    printTool.AutoShowParametersPanel = false;
                    reportContado.Parameters["cod_proyecto"].Value = obj.cod_proyecto;
                    reportContado.Parameters["cod_separacion"].Value = obj.cod_separacion;
                    //report.BackColor = Color.FromArgb(0, 157, 150);
                    reportContado.ShowPreviewDialog();
                }
                SplashScreenManager.CloseForm(false);

                //SplashScreen.Close();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                //SplashScreen.Close();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnFrmResumen_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmResumenXsemana frm = new frmResumenXsemana();
            frm.MdiParent = MdiParent;
            frm.cod_proyecto = cod_proyecto;
            //frm.cod_empresa = cod_empresa;
            frm.Activate();
            frm.Show();
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Tools.Exportar().ExportarExcel(gcListaSeparaciones, "Separaciones");
        }

        private void btnAgruparCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnAgruparCliente.Caption != "Agrupar por Cliente")
            {
                System.Drawing.Image img = Properties.Resources.select_users_32px;
                btnAgruparCliente.ImageOptions.LargeImage = img;
                btnAgruparCliente.Caption = "Agrupar por Cliente";
                AgruparPorLote();
                vistaResumen();

                return;
            }
            if (btnAgruparCliente.Caption != "Agrupar por Manzana")
            {
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/chart/verticalaxistitles_verticaltext_32x32.png");
                btnAgruparCliente.ImageOptions.LargeImage = img;
                btnAgruparCliente.Caption = "Agrupar por Manzana";

                AgruparCliente();
                vistaResumen();
                return;
            }
        }

        private void btnAgruparLote_ItemClick(object sender, ItemClickEventArgs e)
        {
            //MessageBox.Show(btnAgruparLote.Images);
            AgruparPorLote();
        }

        private void btnValAdmin_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaSeparaciones.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Actualizar seguimiento de la(s) separación(es)?", "Actualizar Seguimiento Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                    {
                        eLotes_Separaciones objLT = gvListaSeparaciones.GetRow(nRow) as eLotes_Separaciones;
                        if (objLT.cod_status == "ESE00002")
                        {
                            string result = unit.Proyectos.Actualizar_Seguimiento_Separacion("1", objLT.cod_separacion, objLT.cod_proyecto, Program.Sesion.Usuario.cod_usuario);
                            if (result == "OK")
                            {


                            }
                            gvListaSeparaciones.RefreshData();


                        }
                        else
                        {
                            validar = 1;
                        }
                    }
                    //limpiarFiltroTbi();
                    CargarListado();
                    //CargarListadoResumen();
                    //cargarTitulo();

                }
                if (validar == 1)
                {
                    MessageBox.Show("Solo se modificó las separaciones con estado separado.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                CargarListado();
                CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void btnValBanco_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaSeparaciones.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Actualizar seguimiento de la(s) separación(es)?", "Actualizar Seguimiento Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                    {
                        eLotes_Separaciones objLT = gvListaSeparaciones.GetRow(nRow) as eLotes_Separaciones;
                        if (objLT.cod_status == "ESE00002")
                        {
                            string result = unit.Proyectos.Actualizar_Seguimiento_Separacion("2", objLT.cod_separacion, objLT.cod_proyecto, Program.Sesion.Usuario.cod_usuario);
                            if (result == "OK")
                            {


                            }
                            gvListaSeparaciones.RefreshData();


                        }
                        else
                        {
                            validar = 1;
                        }
                    }
                    //limpiarFiltroTbi();
                    CargarListado();
                    //CargarListadoResumen();
                    //cargarTitulo();

                }
                if (validar == 1)
                {
                    MessageBox.Show("Solo se modificó las separaciones con estado separado.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                CargarListado();
                CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBoleteado_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaSeparaciones.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Actualizar seguimiento de la(s) separación(es)?", "Actualizar Seguimiento Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                    {
                        eLotes_Separaciones objLT = gvListaSeparaciones.GetRow(nRow) as eLotes_Separaciones;
                        if (objLT.cod_status == "ESE00002")
                        {
                            string result = unit.Proyectos.Actualizar_Seguimiento_Separacion("3", objLT.cod_separacion, objLT.cod_proyecto, Program.Sesion.Usuario.cod_usuario);
                            if (result == "OK")
                            {


                            }
                            gvListaSeparaciones.RefreshData();


                        }
                        else
                        {
                            validar = 1;
                        }
                    }
                    //limpiarFiltroTbi();
                    CargarListado();
                    //CargarListadoResumen();
                    //cargarTitulo();

                }
                if (validar == 1)
                {
                    MessageBox.Show("Solo se modificó las separaciones con estado separado.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                CargarListado();
                CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAnular_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaSeparaciones.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Anular la(s) separación(es)?", "Anular Separación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    foreach (int nRow in gvListaSeparaciones.GetSelectedRows())
                    {
                        eLotes_Separaciones objLT = gvListaSeparaciones.GetRow(nRow) as eLotes_Separaciones;
                        if (objLT.cod_status == "ESE00002")
                        {
                            string result = unit.Proyectos.Actualizar_Status_Separacion("3", objLT.cod_separacion, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_lote, Program.Sesion.Usuario.cod_usuario);
                            if (result == "OK")
                            {
                                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingresar motivo de lote \"" + objLT.dsc_lote + "\" anulado.";
                                MemoEdit rbtntxtObser = new MemoEdit(); rbtntxtObser.Width = 120;
                                eLotesxProyecto objLotPro = new eLotesxProyecto();
                                rbtntxtObser.Properties.Name = "rtxtFrenteM";
                                args.Editor = rbtntxtObser;
                                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                                if ((res == DialogResult.OK || res == DialogResult.Yes) && (rbtntxtObser.Text.Trim() != ""))
                                {
                                    string mensajito = GuardarObservacionDesistir(rbtntxtObser.Text, objLT);

                                }

                            }
                            gvListaSeparaciones.RefreshData();


                        }
                        else
                        {
                            validar = 1;
                        }
                    }
                    //limpiarFiltroTbi();
                    CargarListado();
                    //CargarListadoResumen();
                    //cargarTitulo();

                }
                if (validar == 1)
                {
                    MessageBox.Show("Solo se modificó las separaciones con estado separado.", "Actualizar Seguimiento Separación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                CargarListado();
                CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string GuardarObservacionDesistir(string observacion, eLotes_Separaciones obj)
        {
            eLotes_Separaciones.eSeparaciones_Observaciones objObservacion = new eLotes_Separaciones.eSeparaciones_Observaciones();
            objObservacion.cod_separacion = obj.cod_separacion; objObservacion.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario; objObservacion.num_linea = 0;
            objObservacion.dsc_observaciones = observacion;
            objObservacion = unit.Proyectos.InsertarObservacionesSeparacion<eLotes_Separaciones.eSeparaciones_Observaciones>(objObservacion);

            if (objObservacion != null) { return "OK"; }
            return null;

        }

        private void btnExtender_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                frmSepararLote frm = new frmSepararLote(this);
                frm.codigo = obj.cod_proyecto;
                frm.dsc_proyecto = dsc_proyecto;
                frm.cod_separacion = obj.cod_separacion;
                frm.codigoMultiple = obj.cod_etapa;
                frm.extension = true;
                frm.cod_status = obj.cod_status;
                //frm.MiAccion = obj.cod_status != "ESE00002" || obj.flg_activo == "NO" ? Separacion.Vista : Separacion.Editar;
                frm.flg_activo = obj.flg_activo;
                frm.MiAccion = obj.cod_status != "ESE00002" || obj.flg_activo == "NO" ? Separacion.Vista : Separacion.Editar;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaSeparaciones_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                    if (obj.cod_status == "ESE00002" && obj.flg_activo == "SI" /*&& obj.cod_forma_pago != "CO"*/)
                    {
                        btnExtenSep.Enabled = true;
                    }
                    else
                    {
                        btnExtenSep.Enabled = false;
                    }
                    //cod_empresa = obj.cod_empresa;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnVerCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eLotes_Separaciones obj = gvListaSeparaciones.GetFocusedRow() as eLotes_Separaciones;
                if(obj == null) { return; }
                frmMantCliente frm = new frmMantCliente();
                frm.cod_cliente = obj.cod_cliente;
                frm.MiAccion = Cliente.Vista;
                frm.cod_proyecto_titulo = obj.cod_proyecto;
                frm.dsc_proyecto_titulo = dsc_proyecto;
                frm.cod_empresa = obj.cod_empresa;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgruPeriodo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AgruparPeriodo();
        }

        private void dtFechaInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void dtFechaFin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void tileBarEstadoDeSeparacion_Click(object sender, EventArgs e)
        {

        }

        private void tbiEmitidos_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "ALL";
                cod_status = "ESE00002";
                CargarListado("4",flg_firmado : "NO");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiFirmado_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "ALL";
                cod_status = "ESE00001";
                CargarListado("9");
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiDesistido.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiVendido.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblTitulo_CalcGroupClientHeight(object sender, NavBarCalcGroupClientHeightEventArgs e)
        {
            e.Height = 40;
        }



        private void tbiTotal_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "ALL"; cod_status = "ALL";
                CargarListado();
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void lblTitulo_ItemChanged(object sender, EventArgs e)
        //{

        //    if (validar == 0)
        //    {
        //        lytTitulo.Size = new System.Drawing.Size(1391, 120);
        //        validar = 1;
        //        return;
        //    }
        //    if (validar == 1 && validar2 == 1)
        //    {
        //        lytTitulo.Size = new System.Drawing.Size(1391, 40);
        //        validar = 0;
        //        return;
        //    }
        //    validar2 = 1;
        //    size = 120;
        //}



        private void tbiDesistido_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_separacion = "ALL";
                cod_status = "ESE00003";
                CargarListado("4");
                //tbiRegistrado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValAdminis.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiValBanco.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiBoleteado.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiTotal.AppearanceItem.Normal.BorderColor = Color.Transparent;
                //tbiDesistido.AppearanceItem.Normal.BorderColor = Color.FromArgb(192, 0, 0);


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void InitTreeList()
        {
            //layoutControl2.UseLocalBindingContext = true;
            treeListProyectos.Appearance.Row.BackColor = Color.Transparent;
            treeListProyectos.Appearance.Empty.BackColor = Color.Transparent;
            treeListProyectos.BackColor = Color.Transparent;
            treeListProyectos.TreeViewFieldName = "Name";
            treeListProyectos.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            treeListProyectos.OptionsBehavior.Editable = false;
            treeListProyectos.OptionsBehavior.ReadOnly = true;
            treeListProyectos.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeListProyectos.NodeCellStyle += OnNodeCellStyle;
            treeListProyectos.BeforeFocusNode += OnBeforeFocusNode;
            if (dataSource.Count <= 0)
            {
                dataSource = GenerateDataSource();
            }
            treeListProyectos.DataSource = dataSource;
            treeListProyectos.ForceInitialize();
            treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
            for (int x = 0; x <= ctd_proyecto - 1; x++)
            {
                treeListProyectos.Nodes[0].Nodes[x].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            }
            treeListProyectos.CheckAll();
            treeListProyectos.ExpandAll();

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



    }



}