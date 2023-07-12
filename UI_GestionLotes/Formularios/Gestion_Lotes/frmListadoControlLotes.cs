using BE_GestionLotes;
using BL_GestionLotes;
//using UI_GestionLotes;
using DevExpress.Images;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Formularios.Marketing;
using UI_GestionLotes.Formularios.Operaciones;
using UI_GestionLotes.Tools;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    public partial class frmListadoControlLotes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        Brush ConCriterios = Brushes.Green;
        Brush SinCriterios = Brushes.Red;
        Brush NAplCriterio = Brushes.Orange;
        Brush Mensaje = Brushes.Transparent;
        int markWidth = 16;

        List<eVariablesGenerales> lstStatus = new List<eVariablesGenerales>();
        List<eProyecto_Etapa> ListProyectoEtapa = new List<eProyecto_Etapa>();
        List<eTreeProyEtaStatus> listadoTreeList = new List<eTreeProyEtaStatus>();
        List<eLotesxProyecto> listLotesProyecto = new List<eLotesxProyecto>();
        List<eVariablesGenerales> lstTipoLote = new List<eVariablesGenerales>();
        BindingList<Option> dataSource = new BindingList<Option>();

        public string cod_etapa = "";
        public string cod_proyecto = "", cod_etapasmultiple = "", dsc_proyecto = "";
        public string cod_empresa = "";
        public string caption = "";
        int ctd_proyecto = 0;
        bool Buscar = false;

        public frmListadoControlLotes()
        {
            InitializeComponent();
            //InitTreeList();
            unit = new UnitOfWork();
        }

        private void frmListadoControlLotes_Load(object sender, EventArgs e)
        {
            CargarTreeListTresNodos();
            //InitTreeList();
            Inicializar();
            //navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            //btnBuscar.PerformClick();
            CargarListado();//carga el listado señalado por el treelist
            vistaResumen();
            cargarTitulo();
            //navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            //navBarControl1.OptionsNavPane.CollapsedWidth = 50;
            //navBarControl1.OptionsNavPane.ExpandedWidth = 200;
            //btnOcultarFiltro.PerformClick();
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
                //ribbonPageGroup1.Enabled = listPermisos[0].flg_escritura;
                //grupoPersonalizarVistas.Enabled = listPermisos[0].flg_escritura;
                //btnCargaMasivaEMO.Enabled = listPermisos[0].flg_escritura;
            }
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 9);
            if (oPerfil != null)
            {
                //btnMaestroLotes.Enabled = false;
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eProyecto> ListProyectos = unit.Proyectos.ListarProyectos<eProyecto>("4", "", "");
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavProyecto = navBarControl1.Groups.Add();
            NavProyecto.Caption = "Por Proyecto"; NavProyecto.Expanded = true; NavProyecto.SelectedLinkIndex = 0;
            NavProyecto.GroupCaptionUseImage = NavBarImage.Large; NavProyecto.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavProyecto.ImageOptions.LargeImage = imgEmpresaLarge; NavProyecto.ImageOptions.SmallImage = imgEmpresaSmall;

            foreach (eProyecto obj in ListProyectos)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_proyecto; NavDetalle.Name = obj.cod_proyecto;
                NavDetalle.Caption = obj.dsc_nombre; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavProyecto.ItemLinks.Add(NavDetalle);
            }
        }

        //internal void CargarOpcionesMenuEtapas()
        //{
        //    List<eProyecto_Etapa> ListProEtapas = unit.Proyectos.ListarEtapa<eProyecto_Etapa>("4", "", "");
        //    Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
        //    Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

        //    NavBarGroup NavProyecto = navBarControl3.Groups.Add();
        //    NavProyecto.Caption = "Por Etapa"; NavProyecto.Expanded = true; NavProyecto.SelectedLinkIndex = 0;
        //    NavProyecto.GroupCaptionUseImage = NavBarImage.Large; NavProyecto.GroupStyle = NavBarGroupStyle.SmallIconsText;
        //    NavProyecto.ImageOptions.LargeImage = imgEmpresaLarge; NavProyecto.ImageOptions.SmallImage = imgEmpresaSmall;

        //    foreach (eProyecto_Etapa obj in ListProEtapas)
        //    {
        //        NavBarItem NavDetalle = navBarControl3.Items.Add();
        //        NavDetalle.Tag = obj.cod_etapa; NavDetalle.Name = obj.cod_etapa;
        //        NavDetalle.Caption = obj.dsc_descripcion; //NavDetalle.LinkClicked += NavDetalle_LinkClicked;

        //        NavProyecto.ItemLinks.Add(NavDetalle);
        //    }
        //}

        private void gvListaLotesProyectos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaLotesProyectos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            //CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString(),"1");
            //cod_proyecto = e.Link.Item.Tag.ToString();
            //caption = e.Link.Group.Caption;
            //SplashScreenManager.CloseForm();
        }

        private string MetodoValidarEliminarEtapa()
        {
            try
            {
                foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                {
                    eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                    if (objLT.cod_status != "STL00001")
                    {
                        return "No se pudo eliminar. \nLote \"" + objLT.dsc_lote + "\" Configurado.";
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }

        private void btnAsignarStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Tipo Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Seleccione el Status";
                LookUpEdit rlkpStatus = new LookUpEdit(); rlkpStatus.Width = 120;
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rlkpStatus.Properties.ValueMember = "cod_status"; rlkpStatus.Properties.DisplayMember = "dsc_Nombre";
                rlkpStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[]
                {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_Nombre", "Status"),
                });
                rlkpStatus.Properties.DataSource = lstStatus;
                args.Editor = rlkpStatus;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rlkpStatus.EditValue != null)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.cod_status = rlkpStatus.EditValue.ToString();
                        eVariablesGenerales obj = lstStatus.Find(x => x.cod_status == rlkpStatus.EditValue.ToString());
                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("2", objLT);

                    }
                    //SplashScreen.Close();
                    SplashScreenManager.CloseForm(false);
                    gvListaLotesProyectos.RefreshData();
                    //if(cod_proyecto != "")
                    //{
                    //    CargarListado(caption, cod_proyecto, "1");
                    //}
                    //else
                    //{
                    //    CargarListado(navBarControl1.Groups[0].Caption, "", "4");
                    //}

                    CargarListado();
                    //CargarControlLotes();
                }

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarTipoLote_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                AsignarTipoLote();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AsignarTipoLote()
        {
            gvListaLotesProyectos.RefreshData();
            if (gvListaLotesProyectos.SelectedRowsCount > 0)
            {
                //if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Tipo Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Seleccione el Tipo Lote";
                LookUpEdit lkpTipoLote = new LookUpEdit(); lkpTipoLote.Width = 120;
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                lkpTipoLote.Properties.ValueMember = "cod_tipo_lote"; lkpTipoLote.Properties.DisplayMember = "dsc_Nombre";
                lkpTipoLote.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[]
                {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_Nombre", "Tipo de Lote"),
                });
                lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLotes", cod_etapa);
                lkpTipoLote.Properties.DataSource = lstTipoLote;
                args.Editor = lkpTipoLote;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && lkpTipoLote.EditValue != null)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;

                        objLT.cod_tipo_lote = lkpTipoLote.EditValue.ToString();

                        eVariablesGenerales obj = lstTipoLote.Find(x => x.cod_tipo_lote == lkpTipoLote.EditValue.ToString());
                        objLT.imp_precio_m_cuadrado = obj.imp_prec_m_cuadrado;

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("1", objLT);
                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.ClearSelection();
                    gvListaLotesProyectos.RefreshData();
                }
            }
            else
            {
                eLotesxProyecto objLT = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Seleccione el Tipo Lote";
                LookUpEdit lkpTipoLote = new LookUpEdit(); lkpTipoLote.Width = 120;
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                lkpTipoLote.Properties.ValueMember = "cod_tipo_lote"; lkpTipoLote.Properties.DisplayMember = "dsc_Nombre";
                lkpTipoLote.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[]
                {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_Nombre", "Tipo de Lote"),
                });
                lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLotes", objLT.cod_etapa);
                lkpTipoLote.Properties.DataSource = lstTipoLote;
                args.Editor = lkpTipoLote;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && lkpTipoLote.EditValue != null)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    objLT.cod_tipo_lote = lkpTipoLote.EditValue.ToString();
                    eVariablesGenerales obj = lstTipoLote.Find(x => x.cod_tipo_lote == lkpTipoLote.EditValue.ToString());
                    objLT.imp_precio_m_cuadrado = obj.imp_prec_m_cuadrado;
                    objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("1", objLT);
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();
                }
            }
        }

        //public void CargarListado(string NombreGrupo, string Codigo,string NumAccion)
        //{
        //    try
        //    {
        //        string cod_proyecto = "", accion = "", cod_empresa = "";

        //        switch (NombreGrupo)
        //        {
        //            case "Por Proyecto": cod_proyecto = Codigo; accion = NumAccion; break;
        //        }
        //        List<eLotesxProyecto> ListLotesProyecto = new List<eLotesxProyecto>();
        //        ListLotesProyecto = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>(accion, cod_proyecto, cod_empresa);

        //        bsLotesxProyecto.DataSource = ListLotesProyecto;
        //        gvListaLotesProyectos.RefreshData();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
        public void CargarComboEnGrid(int opcion)
        {
            try
            {
                lstStatus = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("Status");
                rlkpStatus.DataSource = lstStatus;
                //lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLote");
                if (opcion == 1)
                {
                    rlkpTipoLote.DataSource = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLotes");
                    colcod_tipo_lote.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    lstTipoLote = unit.Proyectos.CombosEnGridControl<eVariablesGenerales>("TipoLoteEtapa", cod_etapasmultiple);
                    rlkpTipoLote.DataSource = lstTipoLote;
                    colcod_tipo_lote.OptionsColumn.AllowEdit = true;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
        private void Inicializar()
        {
            //CargarOpcionesMenu();

            //CargarListado(navBarControl1.Groups[0].Caption, "", "4");
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            lblTitulo.Text = navBarControl1.Groups[0].Caption + ": MÁS RECIENTE";
            //lblTitulo.Text = navBarControl2.Groups[0].Caption + ": MÁS RECIENTE" + navBarControl2.SelectedLink.Item.Caption;
            //picTitulo.Image = navProyecto.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
            //CargarOpcionesMenuEtapas();
        }

        void cargarTitulo()
        {
            eTreeProyEtaStatus objM = listadoTreeList.Find(x => x.cod_proyecto == cod_proyecto);
            lblTitulo.Text = objM.dsc_proyecto.ToString();
            //lblTitulo.Text = objM.dsc_pro.ToString() + " : " + objM.dsc_proyecto.ToString();
            dsc_proyecto = objM.dsc_proyecto.ToString();

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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarListado();
            cargarTitulo();
            //CargarListadoNombre();
        }

        //void cargarTitulo()
        //{
        //    eTreeProyEtaStatus objM = listadoTreeList.Find(x => x.cod_proyecto == cod_proyecto);
        //    lblTitulo.Text = objM.dsc_pro.ToString() + " : " + objM.dsc_proyecto.ToString();
        //}

        private void gvListaLotesProyectos_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //gvListaLotesProyectos.RefreshData();
            if (gvListaLotesProyectos.GetSelectedRows().Length > 0)
            {
                var validate = 0;
                //gvListaLotesProyectos.RefreshData();            
                if (gvListaLotesProyectos.GetSelectedRows()[0] != 0)
                {
                    validate = gvListaLotesProyectos.GetSelectedRows()[0];

                }
                else
                {
                    if (gvListaLotesProyectos.GetSelectedRows().Length > 1)
                    {
                        validate = gvListaLotesProyectos.GetSelectedRows()[1];
                    }
                    else
                    {
                        return;
                    }
                }

                eLotesxProyecto validar = gvListaLotesProyectos.GetRow(validate) as eLotesxProyecto;
                cod_proyecto = validar.cod_proyecto;
                cod_etapa = validar.cod_etapa;
                foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                {
                    eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                    if (objLT.cod_etapa != validar.cod_etapa)
                    {

                        gvListaLotesProyectos.ClearSelection();
                        gvListaLotesProyectos.RefreshData();
                    }
                }
            }

        }



        private void gvListaLotesProyectos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                eLotesxProyecto obj = gvListaLotesProyectos.GetRow(e.RowHandle) as eLotesxProyecto;
                if (e.Column.FieldName == "cod_tipo_lote" && obj.cod_tipo_lote == null) e.Appearance.BackColor = Color.LightSalmon;
                //if (e.Column.FieldName == "cod_status" && obj.cod_status == "STL00004") e.Appearance.BackColor = Color.LightYellow;
                if (e.Column.FieldName == "imp_precio_m_cuadrado") e.Appearance.BackColor = Color.LightSalmon;
                if (e.Column.FieldName == "imp_precio_total") e.Appearance.BackColor = Color.LightSalmon;

            }
        }

        private void gvListaLotesProyectos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Button == MouseButtons.Right) popupMenu1.ShowPopup(MousePosition);
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eLotesxProyecto obj = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                    frmMantLote frm = new frmMantLote(this);
                    frm.cod_lote = obj.cod_lote;
                    frm.cod_proyecto = obj.cod_proyecto;
                    frm.dsc_proyecto = dsc_proyecto;
                    frm.cod_etapasmultiple = obj.cod_etapa;
                    frm.MiAccion = Lote.Editar;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaLotesProyectos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {

                if (e.RowHandle < 0) return;

                int nRow = e.RowHandle;
                eLotesxProyecto eLotPro = new eLotesxProyecto();
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                eLotesxProyecto obj = gvListaLotesProyectos.GetRow(e.RowHandle) as eLotesxProyecto;
                if (obj.cod_status == "STL00001" && e.Column.FieldName != "cod_status")
                {
                    if (e.Column.FieldName == "num_area_uex" && e.Value != null)
                    {
                        decimal num_area_uex = 0;
                        decimal num_area_uco = 0;
                        decimal num_area_total = 0;

                        //eProyecto_Etapa oLoteEtap = ListProyectoEtapa.Find(x => x.cod_etapa == obj.cod_etapa);
                        //if (oLoteEtap != null)
                        //{
                        //    num_area_uex = oLoteEtap.num_area_uex;
                        //    num_area_uco = oLoteEtap.num_area_uco;
                        //    num_area_total = num_area_uex + num_area_uco;
                        //}
                        if (ListProyectoEtapa.Count > 0)
                        {
                            foreach (eProyecto_Etapa obj2 in ListProyectoEtapa)
                            {
                                if (obj2.num_area_uex > 0)
                                {
                                    num_area_uex += obj2.num_area_uex;
                                }
                                if (obj2.num_area_uco > 0)
                                {
                                    num_area_uco += obj2.num_area_uco;
                                }
                            }
                            num_area_total = num_area_uex + num_area_uco;
                        }
                        obj.prc_uso_exclusivo = obj.num_area_uex / num_area_uex;
                        obj.prc_uso_exclusivo_part_mat = obj.num_area_uex / num_area_total;
                        obj.num_area_uco = obj.prc_uso_exclusivo * num_area_uco;
                        obj.prc_uso_comun_part_mat = obj.num_area_uco / num_area_total;

                        eLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("3", obj);

                        if (eLotPro == null)
                        { MessageBox.Show("Error al vincular el área total m² uso común", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        if (objLotPro != null)
                        {
                            //gvListaLotesProyectos.RefreshData();
                        }
                    }
                    if (e.Column.FieldName == "num_frente" || e.Column.FieldName == "num_derecha" || e.Column.FieldName == "num_izquierda" || e.Column.FieldName == "num_fondo" && e.Value != null)
                    {
                        eLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", obj);
                        if (objLotPro != null)
                        {
                            gvListaLotesProyectos.RefreshData();
                        }
                    }
                    if (e.Column.FieldName == "cod_tipo_lote" && e.Value != null)
                    {
                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("1", obj);
                        if (objLotPro != null)
                        {
                            gvListaLotesProyectos.RefreshData();
                        }

                    }

                    if (e.Column.FieldName == "imp_precio_m_cuadrado" && e.Value != null)
                    {
                        obj.imp_precio_total = obj.num_area_uex * obj.imp_precio_m_cuadrado;
                        eLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("5", obj);
                        if (objLotPro != null)
                        {
                            gvListaLotesProyectos.RefreshData();
                        }
                    }
                }
                else if (e.Column.FieldName == "cod_status" && e.Value != null)
                {
                    objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("2", obj);
                    if (objLotPro != null)
                    {
                        gvListaLotesProyectos.RefreshData();
                    }

                }
                else
                {
                    MessageBox.Show("Solo se puede modificar los lotes libres.", "Configuración de lotes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarListado();
                    vistaResumen();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAsignarFrente_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Frente(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Frente(m)";
                TextEdit rbtntxtFrente = new TextEdit(); rbtntxtFrente.Width = 120;

                rbtntxtFrente.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtFrente.Properties.Name = "rtxtFrenteM";
                args.Editor = rbtntxtFrente;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtFrente.EditValue != null && rbtntxtFrente.Text.Trim().Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_frente = Convert.ToDecimal(rbtntxtFrente.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();

                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarTotalUsoExclusivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListaLotesProyectos.RefreshData();
            if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Área Total (m²) Uso Exclusivo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Área Total (m²) Uso Exclusivo";
            TextEdit rbtntxtUsoExclusivo = new TextEdit(); rbtntxtUsoExclusivo.Width = 120;
            rbtntxtUsoExclusivo.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
            {
                settings.MaskExpression = "n2";
                settings.AutoHideDecimalSeparator = false;
                settings.HideInsignificantZeros = true;
            });
            eLotesxProyecto eLotPro = new eLotesxProyecto();
            rbtntxtUsoExclusivo.Properties.Name = "rtxtUsoExclusivo";
            args.Editor = rbtntxtUsoExclusivo;
            var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
            if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtUsoExclusivo.EditValue != null && rbtntxtUsoExclusivo.Text.Replace(" ", String.Empty).Length > 0)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                //SplashScreen.Open("Configurando los lotes", "Cargando...");
                foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                {
                    eLotesxProyecto obj = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                    decimal num_area_uex = 0;
                    decimal num_area_uco = 0;
                    decimal num_area_total = 0;


                    eProyecto_Etapa oLoteEtap = ListProyectoEtapa.Find(x => x.cod_etapa == obj.cod_etapa);
                    if (oLoteEtap != null)
                    {
                        num_area_uex = oLoteEtap.num_area_uex;
                        num_area_uco = oLoteEtap.num_area_uco;
                        num_area_total = num_area_uex + num_area_uco;

                    }
                    obj.num_area_uex = Convert.ToDecimal(rbtntxtUsoExclusivo.Text);
                    obj.prc_uso_exclusivo = obj.num_area_uex / num_area_uex;
                    obj.prc_uso_exclusivo_part_mat = obj.num_area_uex / num_area_total;
                    obj.num_area_uco = obj.prc_uso_exclusivo * num_area_uco;
                    obj.prc_uso_comun_part_mat = obj.num_area_uco / num_area_total;

                    eLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("3", obj);
                    if (eLotPro == null)
                    { MessageBox.Show("Error al vincular el área total m² uso común", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


                }
                SplashScreenManager.CloseForm(false);
                //SplashScreen.Close();
                gvListaLotesProyectos.RefreshData();

                gvListaLotesProyectos.ClearSelection();
                // btnBuscar.PerformClick();
            }
        }

        public void frmListadoControlLotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //XtraMessageBox.Show("Actualizado", "F5", MessageBoxButtons.OK, MessageBoxIcon.Information);
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //SplashScreen.Open("Obteniendo listado", "Cargando...");
                //NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                if (bsLotesxProyecto.Count > 0)
                {
                    CargarListado();
                }
                //SplashScreen.Close();
                SplashScreenManager.CloseForm(false);
            }
            //if(e.KeyCode == Keys.Control && e.KeyCode == Keys.Tab)
            //{
            //    navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            //    navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            //    navBarControl1.OptionsNavPane.CollapsedWidth = 50;
            //    navBarControl1.OptionsNavPane.ExpandedWidth = 200;
            //}
        }

        private void btnDatosProyecto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                frmMantProyecto frm = new frmMantProyecto();
                frm.cod_proyecto = cod_proyecto;
                frm.MiAccion = Proyecto.Vista;
                frm.MiAccionEtapa = Proyecto.Vista;




                //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCalcularCostos_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("¿Calcular Costo Directo de Lotes?", "Costo Directo Lotes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                eLotesxProyecto eLotPro = unit.Proyectos.AsignarCostosLotes<eLotesxProyecto>(cod_etapa, cod_proyecto);
                CargarListado();
                return;

            }
        }

        //BOTON DE IMPRIMIR HACIENDO PRUEBAS DEL NAVBARCONTROL
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListaLotesProyectos.ShowRibbonPrintPreview();
            //btnBuscar.PerformClick();
            //navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            //navBarControl1.OptionsNavPane.CollapsedWidth = 200;
            //navBarControl1.ShowNavPaneForm();
        }

        private void navBarControl1_NavPaneMinimizedGroupFormShowing(object sender, NavPaneMinimizedGroupFormShowingEventArgs e)
        {
            //InitTreeList();
            //treeListProyectos.Refresh();
            //e.NavPaneForm.Refresh();

            //e.NavPaneForm.Close();
        }

        private void btnAsignarDerecha_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Derecha(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Derecha(m)";
                TextEdit rbtntxtDerecha = new TextEdit(); rbtntxtDerecha.Width = 120;
                rbtntxtDerecha.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtDerecha.Properties.Name = "rtxtDerechaM";
                args.Editor = rbtntxtDerecha;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);

                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtDerecha.EditValue != null && rbtntxtDerecha.Text.Replace(" ", String.Empty).Length > 0) // para que no salga error con espacios en blanco
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_derecha = Convert.ToDecimal(rbtntxtDerecha.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();

                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarIzquierda_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Izquierda(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Izquierda(m)";
                TextEdit rbtntxtIzquierda = new TextEdit(); rbtntxtIzquierda.Width = 120;

                rbtntxtIzquierda.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtIzquierda.Properties.Name = "rtxtrbtntxtIzquierdaM";
                args.Editor = rbtntxtIzquierda;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtIzquierda.EditValue != null && rbtntxtIzquierda.Text.Replace(" ", String.Empty).Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_izquierda = Convert.ToInt32(rbtntxtIzquierda.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();

                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarFondo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Fondo(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Fondo(m)";
                TextEdit rbtntxtFondo = new TextEdit(); rbtntxtFondo.Width = 120;

                rbtntxtFondo.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtFondo.Properties.Name = "rtxtrbtntxtIzquierdaM";
                args.Editor = rbtntxtFondo;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtFondo.EditValue != null && rbtntxtFondo.Text.Replace(" ", String.Empty).Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_fondo = Convert.ToDecimal(rbtntxtFondo.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();
                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarPreciom2Tarifario_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Precio m² Tarifario", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Precio m² Tarifario";
                TextEdit rbtntxtPrecioM2Tarifario = new TextEdit(); rbtntxtPrecioM2Tarifario.Width = 120;

                rbtntxtPrecioM2Tarifario.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtPrecioM2Tarifario.Properties.Name = "rtxtrbtntxtIzquierdaM";
                args.Editor = rbtntxtPrecioM2Tarifario;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtPrecioM2Tarifario.EditValue != null && rbtntxtPrecioM2Tarifario.Text.Replace(" ", String.Empty).Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.imp_precio_m_cuadrado = Convert.ToDecimal(rbtntxtPrecioM2Tarifario.Text);
                        objLT.imp_precio_total = objLT.num_area_uex * objLT.imp_precio_m_cuadrado;
                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("5", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();
                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmPopupControlLotes frm = new frmPopupControlLotes(this);
                if (Application.OpenForms["frmPopupControlLotes"] != null)
                {
                    Application.OpenForms["frmPopupControlLotes"].Activate();
                }
                else
                {
                    frm.cod_proyecto = cod_proyecto;




                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        public void CargarListado()
        {
            try
            {
                cod_proyecto = "";
                string dsc_descripcion = "";
                string proyectos = "";
                string etapas = "";
                int nRowHandle = 0;

                var tools = new Tools.TreeListHelper(treeListProyectos);
                var proyectoMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(1);
                var etapaMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(2);
                cod_etapasmultiple = etapaMultiple;
                cod_etapa = etapaMultiple;
                cod_proyecto = proyectoMultiple;

                nRowHandle = gvListaLotesProyectos.FocusedRowHandle;
                listLotesProyecto.Clear();
                listLotesProyecto = unit.Proyectos.ListarConfLotes<eLotesxProyecto>("1", cod_proyecto, cod_proyecto_multiple: cod_proyecto, cod_etapas_multiple: cod_etapasmultiple);
                bsLotesxProyecto.DataSource = null; bsLotesxProyecto.DataSource = listLotesProyecto; gvListaLotesProyectos.RefreshData();
                int count = cod_etapasmultiple.Count(f => f == ',');
                //        lblTitulo.Text = navBarControl1.Groups[0].Caption + ": " + dsc_descripcion;
                if (count == 0)
                {
                    CargarComboEnGrid(2);
                    CargarListadoEtapas("2");
                }
                else
                {
                    CargarComboEnGrid(1);
                    CargarListadoEtapas("1");
                }

                gvListaLotesProyectos.FocusedRowHandle = nRowHandle;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //public void CargarListadoNombre()
        //{
        //    try
        //    {
        //        cod_proyecto = "";
        //        string dsc_descripcion = "";
        //        string proyectos = "";
        //        string etapas = "";
        //        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode n in treeListProyectos.GetAllCheckedNodes())
        //        {
        //            //if (n.GetValue("cod_proyecto").ToString().Length == 5)
        //            //{
        //            //    //proyectos = n.GetValue("ID").ToString() + "," + proyectos;
        //            //    dsc_descripcion = n.GetValue("dsc_nombre").ToString();
        //            //}
        //            //else etapas = n.GetValue("ID").ToString() + "," + etapas;

        //        }

        //        //int count = etapas.Count(f => f == ',') - 1;
        //        //lblTitulo.Text = navBarControl1.Groups[0].Caption + ": " + dsc_descripcion;
        //        //if (count == 1)
        //        //{
        //        //    cod_etapa = etapas.Substring(etapas.IndexOf("-") + 1, etapas.Length - etapas.IndexOf(",") + 2);
        //        //    CargarComboEnGrid(2);
        //        //}
        //        //else
        //        //{
        //        //    cod_etapa = "";
        //        //    CargarComboEnGrid(1);
        //        //}


        //        //listLotesProyecto.Clear();
        //        //listLotesProyecto = unit.Proyectos.ListarConfLotes<eLotesxProyecto>("1", cod_proyecto, cod_proyecto_multiple: proyectos, cod_etapas_multiple: etapas);
        //        //bsLotesxProyecto.DataSource = null; bsLotesxProyecto.DataSource = listLotesProyecto; gvListaLotesProyectos.RefreshData();
        //        //string str = "Something @to ,Write.;';";
        //        //str = string.Join("", str.Split('@', ',', '.', ';', '\''));

        //        //cod_proyecto = proyectos;
        //        //cod_proyecto = string.Join("", cod_proyecto.Split(','));
        //        //CargarListadoEtapas("1");
        //        ////gvListaLotesProyectos.RowCellStyle += (sender, e) =>
        //        ////{

        //        ////    if (e.Column.FieldName == "cod_lote")
        //        ////    {       
        //        ////            e.Appearance.BackColor = Color.LightGreen;
        //        ////    }
        //        ////};
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        public void CargarListadoManzanas(int num_linea, string cod_etapalkp = "", string dsc_manzana = "", int cantidad = 0)
        {
            try
            {
                eProyectoEtapaManzana eEtaMz = new eProyectoEtapaManzana();

                string codigoManzana = null;
                int contador = 0;

                foreach (eLotesxProyecto obj in listLotesProyecto)
                {
                    cod_empresa = obj.cod_empresa.ToString();

                    if (obj.cod_etapa == cod_etapalkp && obj.dsc_manzana == dsc_manzana)
                    {

                        codigoManzana = obj.cod_manzana.ToString();
                        contador++;
                    }
                }

                if (contador <= 0)
                {
                    for (int i = 1; i < cantidad + 1; i++)
                    {

                        eLotesxProyecto eLoPro = AsignarLotesXProyecto(dsc_manzana + i, dsc_manzana, i, codigoManzana, num_linea, cod_etapalkp);
                        eLoPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("", eLoPro);  //Comentado antes de pruebas
                        if (eLoPro == null)
                        {
                            MessageBox.Show("Error al vincular la manzana", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                        }

                    }
                    eProyectoEtapaManzana objEtaMz = new eProyectoEtapaManzana();
                    objEtaMz.cod_etapa = cod_etapalkp;
                    objEtaMz.cod_empresa = cod_empresa;
                    objEtaMz.cod_proyecto = cod_proyecto;
                    objEtaMz.cod_manzana = ObtenerCodigoManzana(dsc_manzana);
                    objEtaMz.dsc_manzana = dsc_manzana;
                    objEtaMz.num_desde = 1;
                    objEtaMz.num_hasta = cantidad;
                    objEtaMz.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    objEtaMz.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eEtaMz = unit.Proyectos.Guardar_Actualizar_EtapasManzana<eProyectoEtapaManzana>(objEtaMz);
                    CargarListado();

                    if (eEtaMz == null)
                    { MessageBox.Show("Error al vincular la manzana", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                }
                else
                {
                    for (int i = contador + 1; i < contador + cantidad + 1; i++)
                    {
                        eLotesxProyecto eLoPro = AsignarLotesXProyecto(dsc_manzana + i, dsc_manzana, i, codigoManzana, num_linea, cod_etapalkp);
                        eLoPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("", eLoPro);  //Comentado antes de pruebas
                        if (eLoPro == null)
                        {
                            MessageBox.Show("Error al vincular la manzana", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                        }

                    }
                    eProyectoEtapaManzana objEtaMz = new eProyectoEtapaManzana();
                    objEtaMz.cod_etapa = cod_etapalkp;
                    objEtaMz.cod_empresa = cod_empresa;
                    objEtaMz.cod_proyecto = cod_proyecto;
                    objEtaMz.cod_manzana = ObtenerCodigoManzana(dsc_manzana);
                    objEtaMz.dsc_manzana = dsc_manzana;
                    objEtaMz.num_desde = 1;
                    objEtaMz.num_hasta = contador + cantidad;
                    objEtaMz.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    objEtaMz.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eEtaMz = unit.Proyectos.Guardar_Actualizar_EtapasManzana<eProyectoEtapaManzana>(objEtaMz);
                    CargarListado();

                    if (eEtaMz == null)
                    { MessageBox.Show("Error al vincular la manzana", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }

                //gvManzana.RefreshData();
                return;
                //}

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListaLotesProyectos.RefreshData();
            if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Eliminar Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string validarStadoLote = MetodoValidarEliminarEtapa();
            if (validarStadoLote != null)
            {
                MessageBox.Show(validarStadoLote, "Eliminar Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gvListaLotesProyectos.ClearSelection();
                gvListaLotesProyectos.RefreshData();
                return;
            }
            else
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar lotes seleccionados? \nEsta acción es irreversible.", "Eliminar manzana", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    string result = "";
                    if (gvListaLotesProyectos.SelectedRowsCount == 1)
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(gvListaLotesProyectos.GetSelectedRows()[0]) as eLotesxProyecto;
                        result = unit.Proyectos.Eliminar_Configuracion_Proyecto("1", objLT.cod_lote, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_manzana, Program.Sesion.Usuario.cod_usuario);
                        if (result != null)
                        {
                            MessageBox.Show("Eliminado con éxito", "Eliminar Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CargarListado();
                        }
                    }
                    else
                    {

                        foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                        {
                            eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                            result = unit.Proyectos.Eliminar_Configuracion_Proyecto("1", objLT.cod_lote, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_manzana, Program.Sesion.Usuario.cod_usuario);

                        }
                        if (result != null)
                        {
                            MessageBox.Show("Eliminado con exito", "Eliminar Lote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CargarListado();
                        }
                    }

                    //string result = unit.Proyectos.Eliminar_EtapaManzana("2", obj.cod_variable, cod_proyecto, cod_etapa, cod_manzana, cod_manzana_desde.ToString(), cod_manzana_hasta.ToString());
                    //if (result == null)
                    //{
                    //    MessageBox.Show("No se Pudo eliminar la manzana " + result, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    obtenerListadoManzanaXEtapa();
                    //    gvManzana.RefreshData();
                    //    return;
                    //}
                }
                else
                {
                    gvListaLotesProyectos.ClearSelection();
                    gvListaLotesProyectos.RefreshData();
                    return;
                }
            }
        }

        private void btnAsignarTotalUsoExclusivo_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {

                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Área Total (m²) Uso Exclusivo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Área Total (m²) Uso Exclusivo";
                TextEdit rbtntxtUsoExclusivo = new TextEdit(); rbtntxtUsoExclusivo.Width = 120;
                rbtntxtUsoExclusivo.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto eLotPro = new eLotesxProyecto();
                rbtntxtUsoExclusivo.Properties.Name = "rtxtUsoExclusivo";
                args.Editor = rbtntxtUsoExclusivo;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtUsoExclusivo.EditValue != null && rbtntxtUsoExclusivo.Text.Replace(" ", String.Empty).Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto obj = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        decimal num_area_uex = 0;
                        decimal num_area_uco = 0;
                        decimal num_area_total = 0;


                        eProyecto_Etapa oLoteEtap = ListProyectoEtapa.Find(x => x.cod_etapa == obj.cod_etapa);
                        if (oLoteEtap != null)
                        {
                            num_area_uex = oLoteEtap.num_area_uex;
                            num_area_uco = oLoteEtap.num_area_uco;
                            num_area_total = num_area_uex + num_area_uco;

                        }
                        obj.num_area_uex = Convert.ToDecimal(rbtntxtUsoExclusivo.Text);
                        obj.prc_uso_exclusivo = obj.num_area_uex / num_area_uex;
                        obj.prc_uso_exclusivo_part_mat = obj.num_area_uex / num_area_total;
                        obj.num_area_uco = obj.prc_uso_exclusivo * num_area_uco;
                        obj.prc_uso_comun_part_mat = obj.num_area_uco / num_area_total;

                        eLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("3", obj);
                        if (eLotPro == null)
                        { MessageBox.Show("Error al vincular el área total m² uso común", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();

                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarDerecha_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Derecha(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Derecha(m)";
                TextEdit rbtntxtDerecha = new TextEdit(); rbtntxtDerecha.Width = 120;
                rbtntxtDerecha.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtDerecha.Properties.Name = "rtxtDerechaM";
                args.Editor = rbtntxtDerecha;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);

                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtDerecha.EditValue != null && rbtntxtDerecha.Text.Replace(" ", String.Empty).Length > 0) // para que no salga error con espacios en blanco
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_derecha = Convert.ToDecimal(rbtntxtDerecha.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();

                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarFrente_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Frente(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Frente(m)";
                TextEdit rbtntxtFrente = new TextEdit(); rbtntxtFrente.Width = 120;

                rbtntxtFrente.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtFrente.Properties.Name = "rtxtFrenteM";
                args.Editor = rbtntxtFrente;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtFrente.EditValue != null && rbtntxtFrente.Text.Trim().Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_frente = Convert.ToDecimal(rbtntxtFrente.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    //SplashScreen.Close();
                    SplashScreenManager.CloseForm(false);
                    gvListaLotesProyectos.RefreshData();

                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarIzquierda_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Izquierda(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Izquierda(m)";
                TextEdit rbtntxtIzquierda = new TextEdit(); rbtntxtIzquierda.Width = 120;

                rbtntxtIzquierda.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtIzquierda.Properties.Name = "rtxtrbtntxtIzquierdaM";
                args.Editor = rbtntxtIzquierda;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtIzquierda.EditValue != null && rbtntxtIzquierda.Text.Replace(" ", String.Empty).Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_izquierda = Convert.ToInt32(rbtntxtIzquierda.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();

                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarFondo_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Fondo(m)", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Fondo(m)";
                TextEdit rbtntxtFondo = new TextEdit(); rbtntxtFondo.Width = 120;

                rbtntxtFondo.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtFondo.Properties.Name = "rtxtrbtntxtIzquierdaM";
                args.Editor = rbtntxtFondo;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtFondo.EditValue != null && rbtntxtFondo.Text.Replace(" ", String.Empty).Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.num_fondo = Convert.ToDecimal(rbtntxtFondo.Text);

                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("4", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();
                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarPreciom2Tarifario_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListaLotesProyectos.RefreshData();
                if (gvListaLotesProyectos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignar Precio m² Tarifario", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Asignar Precio m² Tarifario";
                TextEdit rbtntxtPrecioM2Tarifario = new TextEdit(); rbtntxtPrecioM2Tarifario.Width = 120;

                rbtntxtPrecioM2Tarifario.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings =>
                {
                    settings.MaskExpression = "n2";
                    settings.AutoHideDecimalSeparator = false;
                    settings.HideInsignificantZeros = true;
                });
                eLotesxProyecto objLotPro = new eLotesxProyecto();
                rbtntxtPrecioM2Tarifario.Properties.Name = "rtxtrbtntxtIzquierdaM";
                args.Editor = rbtntxtPrecioM2Tarifario;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && rbtntxtPrecioM2Tarifario.EditValue != null && rbtntxtPrecioM2Tarifario.Text.Replace(" ", String.Empty).Length > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando los lotes", "Cargando...");
                    //SplashScreen.Open("Configurando los lotes", "Cargando...");
                    foreach (int nRow in gvListaLotesProyectos.GetSelectedRows())
                    {
                        eLotesxProyecto objLT = gvListaLotesProyectos.GetRow(nRow) as eLotesxProyecto;
                        objLT.imp_precio_m_cuadrado = Convert.ToDecimal(rbtntxtPrecioM2Tarifario.Text);
                        objLT.imp_precio_total = objLT.num_area_uex * objLT.imp_precio_m_cuadrado;
                        objLotPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("5", objLT);

                    }
                    SplashScreenManager.CloseForm(false);
                    //SplashScreen.Close();
                    gvListaLotesProyectos.RefreshData();
                    gvListaLotesProyectos.ClearSelection();
                    // btnBuscar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnImportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarCofigLotes frm = new frmImportarCofigLotes(this);




            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;
            frm.ShowDialog();
        }

        private void btnSeleccionMultriple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvListaLotesProyectos.OptionsSelection.MultiSelect == true)
            {
                gvListaLotesProyectos.OptionsSelection.MultiSelect = false; return;
            }
            if (gvListaLotesProyectos.OptionsSelection.MultiSelect == false)
            {
                gvListaLotesProyectos.OptionsSelection.MultiSelect = true; return;
            }

        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {

            navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            navBarControl1.OptionsNavPane.CollapsedWidth = 50;
            navBarControl1.OptionsNavPane.ExpandedWidth = 200;
            if (layoutControlItem6.ContentVisible == true)
            {
                layoutControlItem6.ContentVisible = false;
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                return;
            }
            if (layoutControlItem6.ContentVisible == false)
            {
                layoutControlItem6.ContentVisible = true;
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                return;
            }

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

        private void vistaResumen()
        {
            colnum_area_uco.Visible = false;
            colimp_costo_m_cuadrado.Visible = false;
            colimp_costo_total.Visible = false;
            //colprc_uso_exclusivo.Visible = false;
            colprc_uso_exclusivo_part_mat.Visible = false;
            colprc_uso_comun_part_mat.Visible = false;
        }
        private void vistaDetallada()
        {
            colnum_area_uco.Visible = true;
            colimp_costo_m_cuadrado.Visible = true;
            colimp_costo_total.Visible = true;
            //colprc_uso_exclusivo.Visible = true;
            colprc_uso_exclusivo_part_mat.Visible = true;
            colprc_uso_comun_part_mat.Visible = true;
            gvListaLotesProyectos.Columns["configurado"].VisibleIndex = 1;
            gvListaLotesProyectos.Columns["dsc_lote"].VisibleIndex = 2;
            gvListaLotesProyectos.Columns["cod_status"].VisibleIndex = 3;
            gvListaLotesProyectos.Columns["cod_tipo_lote"].VisibleIndex = 4;
            gvListaLotesProyectos.Columns["prc_uso_exclusivo"].VisibleIndex = 5;
            gvListaLotesProyectos.Columns["num_area_uex"].VisibleIndex = 6;
            gvListaLotesProyectos.Columns["num_area_uco"].VisibleIndex = 7;
            gvListaLotesProyectos.Columns["imp_costo_m_cuadrado"].VisibleIndex = 8;
            gvListaLotesProyectos.Columns["imp_costo_total"].VisibleIndex = 9;
            gvListaLotesProyectos.Columns["imp_precio_m_cuadrado"].VisibleIndex = 10;
            gvListaLotesProyectos.Columns["imp_precio_total"].VisibleIndex = 11;
            gvListaLotesProyectos.Columns["num_frente"].VisibleIndex = 12;
            gvListaLotesProyectos.Columns["num_derecha"].VisibleIndex = 13;
            gvListaLotesProyectos.Columns["num_izquierda"].VisibleIndex = 14;
            gvListaLotesProyectos.Columns["num_fondo"].VisibleIndex = 15;
            gvListaLotesProyectos.Columns["prc_uso_exclusivo_part_mat"].VisibleIndex = 16;
            gvListaLotesProyectos.Columns["prc_uso_comun_part_mat"].VisibleIndex = 17;
        }

        private void gvListaLotesProyectos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eLotesxProyecto obj = gvListaLotesProyectos.GetRow(e.RowHandle) as eLotesxProyecto;
                    if (obj.cod_status == "STL00004") { e.Appearance.BackColor = Color.LightGreen; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    if (obj.cod_status == "STL00003") { e.Appearance.BackColor = Color.LightYellow; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }

                    if (e.Column.FieldName == "configurado") e.DisplayText = "";
                    e.DefaultDraw();
                    if (e.Column.FieldName == "configurado")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "NO") { b = SinCriterios; } else if (cellValue == "SI") { b = ConCriterios; } else { b = NAplCriterio; }
                        //b = ConCriterios;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaLotesProyectos_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int offset = 5, posInical = 0;
            e.DefaultDraw(); e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = Mensaje; Rectangle markRectangle;
            string priorityText = "Leyenda :";
            for (int i = 0; i < 3; i++)
            {
                if (i == 1) { b = ConCriterios; priorityText = " - Lotes configurados"; }
                if (i == 2) { b = SinCriterios; priorityText = " - Lotes sin configurar"; }
                //if (i == 3) { b = NAplCriterio; priorityText = " - Documentos de Inventario o Activo Fijo"; }
                //markRectangle = new Rectangle(e.Bounds.X + offset, e.Bounds.Y + offset + (markWidth + offset) * i, markWidth, markWidth);
                //markRectangle = new Rectangle(e.Bounds.X * (i * 200) + offset, e.Bounds.Y + 10, markWidth, markWidth);
                posInical = i == 0 ? 0 : i == 1 ? 120 : i == 2 ? 400 : 680;
                markRectangle = new Rectangle(e.Bounds.X * (posInical) + offset, e.Bounds.Y + 10, markWidth, markWidth);
                e.Graphics.FillEllipse(b, markRectangle);
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                e.Appearance.Options.UseTextOptions = true;
                e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            }
        }

        private void navBarControl1_Paint(object sender, PaintEventArgs e)
        {
            ListarRadiosCheck();
        }

        private string ObtenerCodigoManzana(string dsc_manzana)
        {
            List<eVariablesGenerales> ListManzana = new List<eVariablesGenerales>();
            ListManzana = unit.Proyectos.ListarManzana<eVariablesGenerales>("4", "", "", "", dsc_manzana);
            string cod_manzana = ListManzana[0].cod_variable;
            return cod_manzana;
        }
        private eLotesxProyecto AsignarLotesXProyecto(string dsc_lote = "", string dsc_manzana = "", int num_lote = 0, string cod_manzana = "", int num_linea = 0, string cod_etapa = "")
        {
            eLotesxProyecto eLoPro = new eLotesxProyecto();
            if (cod_manzana != null)
            {
                eLoPro.cod_manzana = cod_manzana;
            }
            else
            {
                eLoPro.cod_manzana = ObtenerCodigoManzana(dsc_manzana);
            }
            eLoPro.cod_etapa = cod_etapa;
            eLoPro.cod_empresa = cod_empresa;
            eLoPro.cod_proyecto = cod_proyecto;
            eLoPro.dsc_manzana = dsc_manzana;
            eLoPro.dsc_lote = dsc_lote;
            eLoPro.num_etapa = num_linea;
            eLoPro.num_lote = num_lote;
            eLoPro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eLoPro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;


            return eLoPro;
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

                //TreeListNode node = treeListProyectos.FindNodeByFieldValue(dsc_proyecto, cod_proyecto);
                //if (node != null)
                //{
                //    ArrayList selectedNodes = new ArrayList();
                //    selectChildren(node, selectedNodes);
                //    treeListProyectos.Selection.Set(selectedNodes);
                //}

                treeListProyectos.CheckAll();

            }
        }

        void selectChildren(TreeListNode parent, ArrayList selectedNodes)
        {
            IEnumerator en = parent.Nodes.GetEnumerator();
            TreeListNode child;
            while (en.MoveNext())
            {
                child = (TreeListNode)en.Current;
                selectedNodes.Add(child);
                if (child.HasChildren) selectChildren(child, selectedNodes);
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            frmPopupProyectoInfo frm = new frmPopupProyectoInfo();
            frm.cod_proyecto = cod_proyecto;
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
        }

        private void btnExportar_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Tools.Exportar().ExportarExcel(gcListaLotesProyectos, "Maestro Bienes y Precios");
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                //eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                if (cod_proyecto == null) { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                rptFichaPadronArea report = new rptFichaPadronArea();
                ReportPrintTool printTool = new ReportPrintTool(report);
                //detalleLotes printTool = new detalleLotes(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_proyecto"].Value = cod_proyecto;
                //report.BackColor = Color.FromArgb(0, 157, 150);
                SplashScreenManager.CloseForm(false);

                report.ShowPreviewDialog();
                //SplashScreen.Close();
                //SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVerPadronAreaUE_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                if (ePro == null) { MessageBox.Show("Debe seleccionar lote.", "Padrón Áreas UE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                var formato = new FormatoWordHelper();
                formato.ShowWordReportFormatoGeneral(ePro.cod_empresa, "00010");

                ////eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                //if (cod_proyecto == null) { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                //rptFichaPadronArea report = new rptFichaPadronArea();
                //ReportPrintTool printTool = new ReportPrintTool(report);
                ////detalleLotes printTool = new detalleLotes(report);
                //report.RequestParameters = false;
                //printTool.AutoShowParametersPanel = false;
                //report.Parameters["cod_proyecto"].Value = cod_proyecto;
                ////report.BackColor = Color.FromArgb(0, 157, 150);
                ////SplashScreen.Close();
                SplashScreenManager.CloseForm(false);

                //report.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDenominacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                eLotesxProyecto obj = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
                if (obj == null) { MessageBox.Show("Debe seleccionar lote.", "Ficha del lote", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //SplashScreen.Open("Obteniendo reporte", "Cargando...");
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");



                eReportes ePro = new eReportes();
                ePro = unit.Proyectos.ObtenerProyectoLotes<eReportes>("3", obj.cod_proyecto, obj.cod_lote);
                var xml = new FormatoXmlHelper("@tabla1", "00008", obj.cod_empresa);
                xml.ShowReportLindero(ePro);
                SplashScreenManager.CloseForm(false);
                //rptFichaDenominacion report = new rptFichaDenominacion();
                //ReportPrintTool printTool = new ReportPrintTool(report);
                ////detalleLotes printTool = new detalleLotes(report);
                //report.RequestParameters = false;
                //printTool.AutoShowParametersPanel = false;
                //report.Parameters["cod_proyecto"].Value = obj.cod_proyecto;
                //report.Parameters["cod_lote"].Value = obj.cod_lote;
                ////report.BackColor = Color.FromArgb(0, 157, 150);
                ////SplashScreen.Close();
                //SplashScreenManager.CloseForm(false);
                //report.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ListarRadiosCheck();


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

    }

}