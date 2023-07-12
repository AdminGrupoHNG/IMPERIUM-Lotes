using BE_GestionLotes;
using DevExpress.Images;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;
using UI_GestionLotes.Formularios.Marketing;

namespace UI_GestionLotes.Formularios.Gestion_Contratos
{
    public partial class frmListadoContratos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string cod_etapa = "", cod_etapasmultiple = "", cod_estado_contrato = "ALL";
        public string cod_proyecto = "", dsc_proyecto, cod_status = "ALL";
        public string cod_empresa = "";
        public string caption = "";
        string mensajeCUOTAS = "";
        string probandoHTML = "", probandoHTMLIMAGEN = "", probandoXML1 = "", probandoXML2 = "";
        System.Drawing.Image ImgFirmado = Properties.Resources.pencil_16px;
        System.Drawing.Image imgResuelto = Properties.Resources.agreement_delete_16px;
        System.Drawing.Image imgRecepcionado = Properties.Resources.reception_16px;
        System.Drawing.Image imgBolEmitido = Properties.Resources.credit_note_18px;
        List<eTreeProyEtaStatus> listadoTreeList = new List<eTreeProyEtaStatus>();
        List<eContratos> listContratos = new List<eContratos>();
        public List<eContratos.eContratos_Adenda_Financiamiento> mylistDetalleCuotasHTML = new List<eContratos.eContratos_Adenda_Financiamiento>();

        int validar = 0;

        private readonly UnitOfWork unit;

        public frmListadoContratos()
        {
            InitializeComponent();
            unit = new UnitOfWork();

        }

        private void frmListadoContratos_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarTreeListTresNodos();
            //InitTreeList();
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            pcChevron.BackColor = Program.Sesion.Colores.Verde;
            layoutControlItem2.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            layoutControlItem10.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            //layoutControlItem3.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Plomo;
            //lblTitulo.Text = "SEPARACIONES: " + navBarControl2.Groups[0].Caption.ToUpper();

            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            gpFechas.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            //lblTitulo.Text = navBarControl2.Groups[0].Caption + ": MÁS RECIENTE";
            //picTitulo.Image = navProyecto.ImageOptions.LargeImage;
            navBarControl2.Groups[0].SelectedLinkIndex = 0;
            //Buscar = true;
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
            //vistaResumen();
            cargarTitulo();
            System.Drawing.Image img = Properties.Resources.select_users_32px;
            btnAgruCli.ImageOptions.LargeImage = img;
            btnAgruCli.Caption = "Agrupar por Cliente";
            //AgruparPorLote();
            //gvListaContratos.RefreshData();

            //btnOcultarFiltro.PerformClick(); 
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantContratos frm = new frmMantContratos();
                if (Application.OpenForms["frmMantContratos"] != null)
                {
                    Application.OpenForms["frmMantContratos"].Activate();
                }
                else
                {
                    //frm.MiAccion = Proyecto.Nuevo;
                    frm.cod_proyecto = cod_proyecto;
                    frm.cod_empresa = cod_empresa;
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CargarListado(string opcion = "1")
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

                listContratos.Clear();
                listContratos = unit.Proyectos.ListarContratos<eContratos>(opcion, cod_proyecto,
                                cod_etapas_multiple: etapaMultiple,
                                grdbFecha.EditValue.ToString(),
                                Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                                cod_estado_contrato,
                                flag_activo = chkSeparacionActiva.Checked ? "SI" : "NO",
                                cod_status
                                );

                bsContratos.DataSource = null; bsContratos.DataSource = listContratos; gvListaContratos.RefreshData();

                string nTotal, nBolEmitido,nFirmados, nRecepcionado, nResueltos;
                nTotal = listContratos.Count().ToString();
                //if (cod_status == "ALL" && cod_estado_separacion == "ALL")
                //{
                //    tbiTotal.Elements[2].Text = nTotal + " ";
                //    nSeparados = listSeparaciones.FindAll(x => x.cod_status == "ESE00002").Count.ToString();
                //    nVendidos = listSeparaciones.FindAll(x => x.cod_status == "ESE00001").Count.ToString();
                //    nDesistido = listSeparaciones.FindAll(x => x.cod_status == "ESE00003").Count.ToString();
                //    tbiSeparado.Elements[2].Text = nSeparados + " ";
                //    tbiVendido.Elements[2].Text = nVendidos + " ";
                //    tbiDesistido.Elements[2].Text = nDesistido + " ";
                //    nRegistrado = listSeparaciones.FindAll(x => x.flg_registrado == "SI").Count.ToString();
                //    nValAdmin = listSeparaciones.FindAll(x => x.flg_Val_Admin == "SI").Count.ToString();
                //    nValBanco = listSeparaciones.FindAll(x => x.flg_Val_Banco == "SI").Count.ToString();
                //    nBoleteado = listSeparaciones.FindAll(x => x.flg_Boleteado == "SI").Count.ToString();

                //    tbiRegistrado.Elements[2].Text = nRegistrado + " ";
                //    tbiValAdminis.Elements[2].Text = nValAdmin + " ";
                //    tbiValBanco.Elements[2].Text = nValBanco + " ";
                //    tbiBoleteado.Elements[2].Text = nBoleteado + " ";
                //}
                if (cod_estado_contrato == "ALL")
                {
                    nBolEmitido = listContratos.FindAll(x => x.flg_abono == "SI").Count.ToString();
                    nFirmados = listContratos.FindAll(x => x.flg_firmado == "SI").Count.ToString();
                    nRecepcionado = listContratos.FindAll(x => x.flg_recepcionado == "SI").Count.ToString();
                    nResueltos = listContratos.FindAll(x => x.flg_resuelto == "SI").Count.ToString();
                    tbiTotal.Elements[2].Text = nTotal + " ";
                    tbiBolEmitida.Elements[2].Text = nBolEmitido + " ";
                    tbiFirmado.Elements[2].Text = nFirmados + " ";
                    tbiRecepcionado.Elements[2].Text = nRecepcionado + " ";
                    tbiResuelto.Elements[2].Text = nResueltos + " ";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void cargarTitulo()
        {
            eTreeProyEtaStatus objM = listadoTreeList.Find(x => x.cod_proyecto == cod_proyecto + "|" + cod_empresa);
            lblTitulo.Text = objM.dsc_proyecto;
            //lblTitulo.Text = objM.dsc_pro.ToString() + " : " + objM.dsc_proyecto.ToString();
            dsc_proyecto = objM.dsc_proyecto;

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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //limpiarFiltroTbi();
            cod_estado_contrato = "ALL";
            CargarListado();
            //CargarListadoResumen();
            cargarTitulo();
            //colfch_Separacion.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gvListaContratos.RefreshData();
        }

        public void frmListadoContratos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //SplashScreen.Open("Obteniendo listado", "Cargando...");

                //limpiarFiltroTbi();
                CargarListado();
                //CargarListadoResumen();
                //SplashScreenManager.CloseForm(false);

                //SplashScreen.Close();

                //SplashScreenManager.CloseForm();
            }
        }

        private void gvListaContratos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eContratos obj = gvListaContratos.GetFocusedRow() as eContratos;
                    frmMantContratos frm = new frmMantContratos(this);
                    frm.cod_proyecto = obj.cod_proyecto;
                    frm.dsc_proyecto = dsc_proyecto;
                    frm.cod_contrato = obj.cod_contrato;
                    frm.codigoMultiple = obj.cod_etapa;
                    //frm.cod_status = obj.cod_status;
                    frm.flg_activo = obj.flg_activo;
                    //frm.cod_cliente = obj.cod_cliente;
                    frm.MiAccion = obj.flg_firmado == "SI" || obj.flg_activo == "NO" ? Contratos.Vista : Contratos.Editar;


                    //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CargarListadoResumen()
        {

            DataTable odtDatos = new DataTable();
            odtDatos = unit.Proyectos.Listar_Contratos_Resumen("3", cod_proyecto, cod_etapas_multiple: cod_etapasmultiple,
                    grdbFecha.EditValue.ToString(),
                    Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                    Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                    chkSeparacionActiva.Checked ? "SI" : "NO");
            //listSeparaciones = unit.Proyectos.ListarSeparaciones<eLotes_Separaciones>("1", cod_proyecto, cod_etapas_multiple: etapaMultiple);
            if (odtDatos.Rows.Count > 0)
            {
                tbiFirmado.Elements[2].Text = odtDatos.Rows[0][0].ToString() + " ";
                tbiResuelto.Elements[2].Text = odtDatos.Rows[0][1].ToString() + " ";
                tbiTotal.Elements[2].Text = odtDatos.Rows[0][2].ToString() + " ";
            }
        }

        private void gvListaContratos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eContratos obj = gvListaContratos.GetRow(e.RowHandle) as eContratos;
                    if (e.Column.FieldName == "flg_abono") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_firmado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_resuelto") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_recepcionado") e.DisplayText = "";
                    e.DefaultDraw();
                    if (e.Column.FieldName == "flg_abono" && obj.flg_abono == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgBolEmitido, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_firmado" && obj.flg_firmado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgFirmado, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_resuelto" && obj.flg_resuelto == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgResuelto, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_recepcionado" && obj.flg_recepcionado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(imgRecepcionado, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiFirmado_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_contrato = "FI";
                CargarListado();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiResuelto_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_contrato = "RE";
                CargarListado();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbiTotal_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_contrato = "ALL";
                CargarListado();

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

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
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

        private void btnSeleccionMultiple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvListaContratos.OptionsSelection.MultiSelect == true)
            {
                gvListaContratos.OptionsSelection.MultiSelect = false; return;
            }
            if (gvListaContratos.OptionsSelection.MultiSelect == false)
            {
                gvListaContratos.OptionsSelection.MultiSelect = true; return;
            }
        }

        void AgruparCliente()
        {
            colnum_etapa.UnGroup();
            coldsc_manzana.UnGroup();
            coldsc_periodo.UnGroup();
            coldsc_cliente.GroupIndex = 0;
            coldsc_manzana.Visible = false;
            colnum_etapa.Visible = false;
            coldsc_periodo.Visible = false;

            //coldsc_manzana.GroupIndex = 1;

        }
        void AgruparPorLote()
        {
            coldsc_cliente.UnGroup();
            coldsc_periodo.UnGroup();
            colnum_etapa.GroupIndex = 0;
            coldsc_manzana.GroupIndex = 1;
            coldsc_periodo.SortOrder = DevExpress.Data.ColumnSortOrder.None;
            colfch_emitido.SortIndex = 0;
            colfch_emitido.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            coldsc_periodo.Visible = false;

        }

        void AgruparPeriodo()
        {
            colnum_etapa.UnGroup();
            coldsc_manzana.UnGroup();
            coldsc_cliente.UnGroup();
            colfch_emitido.SortOrder = DevExpress.Data.ColumnSortOrder.None;
            coldsc_periodo.SortIndex = 0;
            coldsc_periodo.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            coldsc_periodo.GroupIndex = 0;
            colfch_emitido.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            coldsc_manzana.Visible = false;
            colnum_etapa.Visible = false;
            //coldsc_manzana.GroupIndex = 1;
        }

        private void btnAgruCli_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (btnAgruCli.Caption != "Agrupar por Cliente")
            {
                System.Drawing.Image img = Properties.Resources.select_users_32px;
                btnAgruCli.ImageOptions.LargeImage = img;
                btnAgruCli.Caption = "Agrupar por Cliente";

                AgruparPorLote();
                //vistaResumen();

                return;
            }
            if (btnAgruCli.Caption != "Agrupar por Manzana")
            {
                System.Drawing.Image img = ImageResourceCache.Default.GetImage("images/chart/verticalaxistitles_verticaltext_32x32.png");
                btnAgruCli.ImageOptions.LargeImage = img;
                btnAgruCli.Caption = "Agrupar por Manzana";
                AgruparCliente();
                //vistaResumen();
                return;
            }
        }

        private void btnAgruPer_ItemClick(object sender, ItemClickEventArgs e)
        {
            AgruparPeriodo();
        }

        private void btnVerDatosCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eContratos obj = gvListaContratos.GetFocusedRow() as eContratos;
                if (obj == null) { return; }
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

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            frmPopupProyectoInfo frm = new frmPopupProyectoInfo();
            frm.cod_proyecto = cod_proyecto;
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
        }

        private void gvListaContratos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eContratos obj = gvListaContratos.GetFocusedRow() as eContratos;
                    if (validarCadenaVacio(obj.cod_separacion))
                    {
                        btnVerDatosSepa.Enabled = false;
                    }
                    else
                    {
                        btnVerDatosSepa.Enabled = true;
                    }
                    int validar = 0;

                    if (obj.flg_firmado == "NO") { btnFirmar.Enabled = true; }
                    else { btnAnular.Enabled = false; btnFirmar.Enabled = false; btnRecepcionado.Enabled = true; validar = 1; }

                    if (obj.flg_recepcionado == "NO") { if (obj.flg_firmado == "SI") { btnRecepcionado.Enabled = true; } }
                    else { btnAnular.Enabled = false; btnRecepcionado.Enabled = false;  btnResolver.Enabled = true; validar = 1; }

                    if (obj.flg_resuelto == "NO") { if (obj.flg_recepcionado == "SI") { btnResolver.Enabled = true; } }
                    else { btnAnular.Enabled = false; btnResolver.Enabled = false;  validar = 1; }

                    if (obj.flg_activo == "NO") { btnAnular.Enabled = false;   }
                    else { if (obj.flg_firmado == "NO") { btnFirmar.Enabled = true; } if (validar == 0) { btnAnular.Enabled = true; } }

                    //if (obj.flg_activo == "SI" && obj.flg_firmado == "NO")
                    //{
                    //    btnFirmar.Enabled = true;
                    //    btnAnularCon.Enabled = true;
                    //}
                    //else
                    //{
                    //    btnFirmar.Enabled = false;
                    //    btnAnularCon.Enabled = false;

                    //}
                    //cod_empresa = obj.cod_empresa;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private Boolean validarCadenaVacio(string valor)
        {
            if (valor == null)
            {
                return true;
            }
            if (valor == "")
            {
                return true;
            }
            return false;
        }

        private void btnVerDatosSepa_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eContratos obj = gvListaContratos.GetFocusedRow() as eContratos;
                if (obj == null) { return; }

                frmSepararLote frm = new frmSepararLote();
                frm.codigo = obj.cod_proyecto;
                frm.dsc_proyecto = dsc_proyecto;
                frm.cod_separacion = obj.cod_separacion;
                frm.codigoMultiple = obj.cod_etapa;
                //frm.cod_status = obj.cod_status;
                frm.flg_activo = obj.flg_activo;
                frm.MiAccion = Separacion.Vista;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tileBarTotal11_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            tileBarSeguimiento1.AllowSelectedItem = false;
            tileBarSeguimiento1.AllowSelectedItem = true;
        }

        private void tileBarSeguimiento1_SelectedItemChanged(object sender, TileItemEventArgs e)
        {

            tileBarTotal11.AllowSelectedItem = false;
            tileBarTotal11.AllowSelectedItem = true;
        }

        private void btnFirmar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaContratos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Firmar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Firmar contrato(s)?", "Firmar Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    foreach (int nRow in gvListaContratos.GetSelectedRows())
                    {
                        eContratos objLT = gvListaContratos.GetRow(nRow) as eContratos;
                        if (objLT.flg_firmado == "NO" && objLT.flg_resuelto == "NO")
                        {
                            string result = unit.Proyectos.Actualizar_Status_Contrato("1", objLT.cod_contrato, cod_proyecto);
                            if (result == "OK")
                            {
                                //XtraMessageBox.Show("Contrato Anulado.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            //string result = unit.Proyectos.Actualizar_Status_Separacion("3", objLT.cod_separacion, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_lote, Program.Sesion.Usuario.cod_usuario);
                            //if (result == "OK")
                            //{
                            //    XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingresar motivo de lote \"" + objLT.dsc_lote + "\" anulado.";
                            //    MemoEdit rbtntxtObser = new MemoEdit(); rbtntxtObser.Width = 120;
                            //    eLotesxProyecto objLotPro = new eLotesxProyecto();
                            //    rbtntxtObser.Properties.Name = "rtxtFrenteM";
                            //    args.Editor = rbtntxtObser;
                            //    var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                            //    if ((res == DialogResult.OK || res == DialogResult.Yes) && (rbtntxtObser.Text.Trim() != ""))
                            //    {
                            //        string mensajito = GuardarObservacionDesistir(rbtntxtObser.Text, objLT);

                            //    }

                            //}
                            gvListaContratos.RefreshData();


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
                    MessageBox.Show("Solo se firmó los contratos no resueltos", "Actualizar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                //CargarListado();
                //CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAnularCon_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaContratos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Anular Contrato", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Anular contrato(s)?", "Anular Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    foreach (int nRow in gvListaContratos.GetSelectedRows())
                    {
                        eContratos objLT = gvListaContratos.GetRow(nRow) as eContratos;
                        if (objLT.flg_firmado == "NO" && objLT.flg_recepcionado == "NO" && objLT.flg_resuelto == "NO" && objLT.flg_activo == "SI")
                        {
                            string result = unit.Proyectos.Actualizar_Status_Contrato("3", objLT.cod_contrato, cod_proyecto, objLT.cod_separacion);
                            if (result == "OK")
                            {
                                //XtraMessageBox.Show("Contrato Anulado.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            //string result = unit.Proyectos.Actualizar_Status_Separacion("3", objLT.cod_separacion, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_lote, Program.Sesion.Usuario.cod_usuario);
                            //if (result == "OK")
                            //{
                            //    XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingresar motivo de lote \"" + objLT.dsc_lote + "\" anulado.";
                            //    MemoEdit rbtntxtObser = new MemoEdit(); rbtntxtObser.Width = 120;
                            //    eLotesxProyecto objLotPro = new eLotesxProyecto();
                            //    rbtntxtObser.Properties.Name = "rtxtFrenteM";
                            //    args.Editor = rbtntxtObser;
                            //    var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                            //    if ((res == DialogResult.OK || res == DialogResult.Yes) && (rbtntxtObser.Text.Trim() != ""))
                            //    {
                            //        string mensajito = GuardarObservacionDesistir(rbtntxtObser.Text, objLT);

                            //    }

                            //}
                            gvListaContratos.RefreshData();


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
                    MessageBox.Show("Solo se anuló los contratos no firmados", "Actualizar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                //CargarListado();
                //CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Tools.Exportar().ExportarExcel(gcListaContratos, "Contratos");
        }


        private string CodigoDelFormato(eContratos eLotCon)
        {
            if (eLotCon.cod_estadocivil != "02" && validarCadenaVacio(eLotCon.cod_estadocivil_CO))
            {
                return "00002";
            }
            if (eLotCon.cod_estadocivil == "02" && validarCadenaVacio(eLotCon.cod_estadocivil_CO))
            {
                return "00003";
            }
            if (eLotCon.cod_estadocivil != "02" && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && eLotCon.cod_estadocivil_CO != "02")
            {
                return "00004";
            }
            if (eLotCon.cod_estadocivil == "02" && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && eLotCon.cod_estadocivil_CO != "02")
            {
                return "00005";
            }
            if (eLotCon.cod_estadocivil != "02" && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && eLotCon.cod_estadocivil_CO == "02")
            {
                return "00006";
            }
            if (eLotCon.cod_estadocivil == "02" && !validarCadenaVacio(eLotCon.cod_estadocivil_CO) && eLotCon.cod_estadocivil_CO == "02")
            {
                return "00007";
            }
            return null;
        }

        private void tbiRecepcionado_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_contrato = "RECEP";
                CargarListado();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRecepcionado_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaContratos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Recepcionar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Recepcionar contrato(s)?", "Recepcionar Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    foreach (int nRow in gvListaContratos.GetSelectedRows())
                    {
                        eContratos objLT = gvListaContratos.GetRow(nRow) as eContratos;
                        if (objLT.flg_firmado == "SI" && objLT.flg_recepcionado == "NO" && objLT.flg_resuelto == "NO" && objLT.flg_activo == "SI")
                        {
                            string result = unit.Proyectos.Actualizar_Status_Contrato("4", objLT.cod_contrato, cod_proyecto);
                            if (result == "OK")
                            {
                                //XtraMessageBox.Show("Contrato Anulado.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            //string result = unit.Proyectos.Actualizar_Status_Separacion("3", objLT.cod_separacion, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_lote, Program.Sesion.Usuario.cod_usuario);
                            //if (result == "OK")
                            //{
                            //    XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingresar motivo de lote \"" + objLT.dsc_lote + "\" anulado.";
                            //    MemoEdit rbtntxtObser = new MemoEdit(); rbtntxtObser.Width = 120;
                            //    eLotesxProyecto objLotPro = new eLotesxProyecto();
                            //    rbtntxtObser.Properties.Name = "rtxtFrenteM";
                            //    args.Editor = rbtntxtObser;
                            //    var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                            //    if ((res == DialogResult.OK || res == DialogResult.Yes) && (rbtntxtObser.Text.Trim() != ""))
                            //    {
                            //        string mensajito = GuardarObservacionDesistir(rbtntxtObser.Text, objLT);

                            //    }

                            //}
                            gvListaContratos.RefreshData();


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
                    MessageBox.Show("Solo se recepcionó los contratos firmados", "Actualizar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                //CargarListado();
                //CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnResolver_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var validar = 0;
                if (gvListaContratos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Firmar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult msgresult = MessageBox.Show("¿Firmar contrato(s)?", "Firmar Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    foreach (int nRow in gvListaContratos.GetSelectedRows())
                    {
                        eContratos objLT = gvListaContratos.GetRow(nRow) as eContratos;
                        if (objLT.flg_firmado == "SI" && objLT.flg_recepcionado == "SI" && objLT.flg_resuelto == "NO" && objLT.flg_activo == "SI")
                        {
                            string result = unit.Proyectos.Actualizar_Status_Contrato("2", objLT.cod_contrato, cod_proyecto, objLT.cod_separacion);
                            if (result == "OK")
                            {
                                //XtraMessageBox.Show("Contrato Anulado.", "Guardar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            //string result = unit.Proyectos.Actualizar_Status_Separacion("3", objLT.cod_separacion, objLT.cod_proyecto, objLT.cod_etapa, objLT.cod_lote, Program.Sesion.Usuario.cod_usuario);
                            //if (result == "OK")
                            //{
                            //    XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingresar motivo de lote \"" + objLT.dsc_lote + "\" anulado.";
                            //    MemoEdit rbtntxtObser = new MemoEdit(); rbtntxtObser.Width = 120;
                            //    eLotesxProyecto objLotPro = new eLotesxProyecto();
                            //    rbtntxtObser.Properties.Name = "rtxtFrenteM";
                            //    args.Editor = rbtntxtObser;
                            //    var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                            //    if ((res == DialogResult.OK || res == DialogResult.Yes) && (rbtntxtObser.Text.Trim() != ""))
                            //    {
                            //        string mensajito = GuardarObservacionDesistir(rbtntxtObser.Text, objLT);

                            //    }

                            //}
                            gvListaContratos.RefreshData();


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
                    MessageBox.Show("Solo se resolvió los contratos en estado emitido", "Actualizar Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                //CargarListado();
                //CargarListadoResumen();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnContratoFormato_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eContratos lst = gvListaContratos.GetFocusedRow() as eContratos;

                if (lst == null) { return; }
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando contrato", "Cargando...");

                string codigoFormato = "";
                codigoFormato = CodigoDelFormato(lst);

                if (codigoFormato == null) { SplashScreenManager.CloseForm(false); return; }


                var xml = new FormatoXmlHelper("@tabla1", codigoFormato, cod_empresa);

                string[] cabecera = new string[] { "CUOTA", "IMPORTE CUOTA", "FECHA DE PAGO" };
                List<String[]> filas = new List<string[]>();
                mylistDetalleCuotasHTML = unit.Proyectos.ObtenerListadoDetalleCuotas<eContratos.eContratos_Adenda_Financiamiento>("2", lst.cod_contrato, cod_proyecto, 0);

                foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotasHTML)
                {
                    if (obj.num_cuota > 0)
                    {
                        filas.Add(new string[] { obj.dsc_cuota, obj.dsc_cuotas, obj.dsc_vct_cuota });
                    }
                }
                eReportes abc = unit.Proyectos.ObtenerFormatoContrato<eReportes>(Convert.ToInt32(codigoFormato) - 1, lst.cod_cliente, lst.cod_contrato, cod_proyecto);

                xml.ShowReport(abc, cabecera, filas, lst.cod_forma_pago, "@tabla1");
                SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportarWord_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eContratos lst = gvListaContratos.GetFocusedRow() as eContratos;

                if (lst == null) { return; }
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando contrato", "Cargando...");

                string codigoFormato = "";
                codigoFormato = CodigoDelFormato(lst);

                if (codigoFormato == null) { SplashScreenManager.CloseForm(false); return; }


                var xml = new FormatoXmlHelper("@tabla1", codigoFormato, cod_empresa);

                string[] cabecera = new string[] { "CUOTA", "IMPORTE CUOTA", "FECHA DE PAGO" };
                List<String[]> filas = new List<string[]>();
                mylistDetalleCuotasHTML = unit.Proyectos.ObtenerListadoDetalleCuotas<eContratos.eContratos_Adenda_Financiamiento>("2", lst.cod_contrato, cod_proyecto, 0);

                foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotasHTML)
                {
                    if (obj.num_cuota > 0)
                    {
                        filas.Add(new string[] { obj.dsc_cuota, obj.dsc_cuotas, obj.dsc_vct_cuota });
                    }
                }
                eReportes abc = unit.Proyectos.ObtenerFormatoContrato<eReportes>(Convert.ToInt32(codigoFormato) - 1, lst.cod_cliente, lst.cod_contrato, cod_proyecto);

                xml.saveReport(abc, cabecera, filas, lst.cod_forma_pago, "@tabla1");
                SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void tbiBolEmitida_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                cod_estado_contrato = "ABO";
                CargarListado();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAutoContrato_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eContratos lst = gvListaContratos.GetFocusedRow() as eContratos;

                if (lst == null) { return; }
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando contrato", "Cargando...");

                string codigoFormato = "";
                codigoFormato = CodigoDelFormato(lst);

                if (codigoFormato == null) { SplashScreenManager.CloseForm(false); return; }


                var xml = new FormatoXmlHelper("@tabla1", codigoFormato, cod_empresa);

                string[] cabecera = new string[] { "CUOTA", "IMPORTE CUOTA", "FECHA DE PAGO" };
                List<String[]> filas = new List<string[]>();
                mylistDetalleCuotasHTML = unit.Proyectos.ObtenerListadoDetalleCuotas<eContratos.eContratos_Adenda_Financiamiento>("2", lst.cod_contrato, cod_proyecto, 0);

                foreach (eContratos.eContratos_Adenda_Financiamiento obj in mylistDetalleCuotasHTML)
                {
                    if (obj.num_cuota > 0)
                    {
                        filas.Add(new string[] { obj.dsc_cuota, obj.dsc_cuotas, obj.dsc_vct_cuota });
                    }
                }
                eReportes abc = unit.Proyectos.ObtenerFormatoContrato<eReportes>(Convert.ToInt32(codigoFormato) - 1, lst.cod_cliente, lst.cod_contrato, cod_proyecto);

                xml.ShowReport(abc, cabecera, filas, lst.cod_forma_pago, "@tabla1");
                SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);

                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void gvListaContratos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "dsc_lote")
                {
                    e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }
        }

        private string filaPintadaTablaBlanco(string periodo = "", int validatCuota = 0, string DesCuota = "", string ImpCuota = "", string FecVenPago = "")
        {
            string stilo1 = "", stilo2 = "", stilo3 = "", stilo4 = "", stilo5 = "", stilo6 = "", stilo7 = "", stilo8 = "", stilo9 = "", stilo10 = "", stilo11 = "", stilo12 = "";
            stilo1 = validatCuota % 2 == 0 ? "cs1D639760" : "cs2A40E650";
            stilo2 = validatCuota % 2 == 0 ? "csD88ED368" : "csD88ED368";
            stilo3 = validatCuota % 2 == 0 ? "csDC4A80" : "csDC4A80";
            stilo4 = validatCuota % 2 == 0 ? "cs13B55570" : "cs1969CE44";
            stilo5 = validatCuota % 2 == 0 ? "cs1159AD24" : "cs1159AD24";
            stilo6 = validatCuota % 2 == 0 ? "cs1B16EEB5" : "cs1B16EEB5";
            stilo7 = validatCuota % 2 == 0 ? "csFB2A2743" : "cs63F8FB84";
            stilo8 = validatCuota % 2 == 0 ? "csD88ED368" : "csD88ED368";
            stilo9 = validatCuota % 2 == 0 ? "cs1B16EEB5" : "cs1B16EEB5";


            string filaPintada = "";

            filaPintada = "<tr style=\"height: 18.05pt;\">" +
                $"<td class= \"{stilo1}\" valign=\"top\">" +
                $"<p class=\"{stilo2}\"><span class=\"{stilo3}\">{DesCuota}&nbsp;</span></p>" +
                "</td>" +
                $"<td class= \"{stilo4}\" valign=\"top\">" +
                $"<p class=\"{stilo5}\"><span class=\"{stilo6}\">{ImpCuota}&nbsp;</span></p>" +
                "</td>" +
                $"<td class=\"{stilo7}\" valign=\"top\">" +
                $"<p class=\"{stilo8}\"><span class=\"{stilo9}\"> {FecVenPago}</span></p>" +
                "</td>" +

                "</tr>";


            return filaPintada;
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

        private void gvListaContratos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaContratos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
    }

}