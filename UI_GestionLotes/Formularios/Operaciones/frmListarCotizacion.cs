using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.Images;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class frmListarCotizacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string sEstadoFiltro = "", sTipoContactoFiltro = "", sCod_ejecutivoFiltro = "", CodMenu, DscMenu;//, perfil = "";
        private readonly UnitOfWork unit;
        public eProformas af = new eProformas();
        List<eLotes_Separaciones.eCotizaciones> listCronograma = new List<eLotes_Separaciones.eCotizaciones>();
        List<eTreeProyEtaStatus> listadoTreeList = new List<eTreeProyEtaStatus>();
        int ctd_proyecto = 0;
        string cod_proyecto = "";
        public frmListarCotizacion()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurarFormulario();
        }


        void configurarFormulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListarCotizacion, gvListarCotizacion);
            dtFechaInicio.EditValue = DateTime.Today;
            dtFechaFin.EditValue = DateTime.Today;

            List<eCampanha> ListEjecutivos = new List<eCampanha>();
            ListEjecutivos = unit.Campanha.ListarEjecutivosVentasMenu<eCampanha>(3, "", cod_proyecto, Program.Sesion.Usuario.cod_usuario);
            //perfil = ListEjecutivos[0].estado;
            tlEjecutivos = unit.Globales.CargaTreeList(tlEjecutivos, ListEjecutivos);
            tlEjecutivos.CheckAll();

            fGuardarFiltroBusqueda();

            //if (perfil == "VISUALIZADOR")
            //{
            //    btnNuevo.Enabled = false;
            //    btnEliminar.Enabled = false;
            //    btnExportarExcel.Enabled = false;
            //    btnImprimir.Enabled = false;
            //}
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
            }
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
                    sCod_ejecutivoFiltro = "";
                }
            }
            else
            {
                sCod_ejecutivoFiltro = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            fGuardarFiltroBusqueda();
            cargarListado();
        }

        private void btnCalcularCronograma_Click(object sender, EventArgs e)
        {
            decimal valor = 0;
            if (Convert.ToDecimal(txtMontoSolicitado.EditValue) <= 0) { MessageBox.Show("El monto solicitado no puede ser 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoSolicitado.Focus(); return; }
            if (Convert.ToDecimal(txtSeparacion.EditValue) < 0) { MessageBox.Show("La separación no puede ser un número negativo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSeparacion.Focus(); return; }
            if (Convert.ToDecimal(txtCuotaInicial.EditValue) < 0) { MessageBox.Show("La cuota inicial no puede ser un número negativo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCuotaInicial.Focus(); return; }

            valor = Convert.ToDecimal(txtMontoSolicitado.EditValue);
            listCronograma.Clear();
            for (int i=0; i < 4; i++)
            {
                eLotes_Separaciones.eCotizaciones obj = new eLotes_Separaciones.eCotizaciones();

                if (i == 0) obj.num_cuota = 12;
                if (i == 1) obj.num_cuota = 24;
                if (i == 2) obj.num_cuota = 36;
                if (i == 3) obj.num_cuota = 48;

                obj.imp_cuotasinigv = valor / obj.num_cuota;
                obj.imp_cuotaconigv = valor;

                listCronograma.Add(obj);
            }

            bsListadoCronograma.DataSource = listCronograma; gvListarCotizacion.RefreshData();
            /*
            decimal imp_cuota = 0, tasaMes = 0, prc_igv = 0, imp_couta1 = 0; DateTime fecha_old = DateTime.Today, fecha_new = DateTime.Today;
            fecha_old = Convert.ToDateTime(dtFechaPago.EditValue);
            int dia_fecha = fecha_old.Day;
            int mes_fecha = fecha_old.Month;
            int dia_pago = Convert.ToInt32(txtDiaPago.EditValue);
            fecha_new = dia_fecha >= 5 && dia_fecha <= 19 ?
                new DateTime(mes_fecha == 12 ? fecha_old.Year + 1 : fecha_old.Year, mes_fecha == 12 ? 1 : mes_fecha + 1, dia_pago) :
                dia_fecha >= 20 ? new DateTime(mes_fecha >= 11 ? fecha_old.Year + 1 : fecha_old.Year, mes_fecha >= 11 ? fecha_old.AddMonths(2).Month : mes_fecha + 2, dia_pago) :
                new DateTime(mes_fecha == 12 ? fecha_old.Year + 1 : fecha_old.Year, mes_fecha == 12 ? 1 : mes_fecha + 1, dia_pago);
            imp_couta1 = Convert.ToDecimal(txtMontoSolicitado.EditValue);
            prc_igv = (decimal)0.18;
//            tasaMes = Math.Round(Convert.ToDecimal(txtTasaMensual.EditValue), 6);
            imp_cuota = Math.Round(Convert.ToDecimal(txtMontoSolicitado.EditValue) / Convert.ToDecimal((-1 * Convert.ToDouble(txtCuotas.EditValue)) ), 6);
            listCronograma.Clear();

            //eLotes_Separaciones objCab = new eLotes_Separaciones();
            //objCab.imp_Capital = Convert.ToDecimal(txtMontoSolicitado.EditValue);
            //objCab.num_cuotas = Convert.ToInt32(txtCuotas.EditValue);
            //objCab.num_tasaanual = Convert.ToDecimal(txtTasaAnual.EditValue);
            //objCab.num_tasamensual = tasaMes;

            for (int x = 1; x <= Convert.ToInt32(txtCuotas.EditValue); x++)
            {
                eLotes_Separaciones.eCotizaciones obj = new eLotes_Separaciones.eCotizaciones();
                obj.num_cuota = x;
                obj.fch_cuota = fecha_new;
                obj.num_dias = Convert.ToInt32((fecha_new - fecha_old).TotalDays);
                obj.imp_capitalinicial = imp_couta1;
                obj.imp_cuotasinigv = imp_cuota;
                obj.imp_interes = Math.Round(obj.imp_capitalinicial * tasaMes, 6);
                obj.imp_coutaigv = Math.Round(obj.imp_interes * prc_igv, 6);
                obj.imp_cuotaconigv = Math.Round(imp_cuota + obj.imp_coutaigv, 6);
                obj.imp_amortizacion = Math.Round(obj.imp_cuotasinigv - obj.imp_interes, 6);
                obj.imp_capitalfinal = Math.Round(obj.imp_capitalinicial - obj.imp_amortizacion, 6);
                obj.imp_interes = Math.Round(obj.imp_interes, 6);
                obj.imp_coutaigv = Math.Round(obj.imp_coutaigv, 6);
                obj.imp_coutaigv = Math.Round(obj.imp_coutaigv, 6);
                obj.imp_cuotasinigv = Math.Round(obj.imp_cuotasinigv, 6);
                obj.imp_montoporpagar = obj.imp_cuotaconigv;
                imp_couta1 = obj.imp_capitalfinal;
                fecha_old = obj.fch_cuota;
                fecha_new = fecha_old.AddMonths(1);

                listCronograma.Add(obj);
            }
            bsListadoCronograma.DataSource = listCronograma; gvListarCotizacion.RefreshData();

            /*double[] tmpCashflows = new double[] { -2000, 213.91, 213.12, 212.31, 211.47, 210.60, 209.70, 208.78, 207.83, 206.85, 205.84, 204.79, 203.72 };
            double[] tmpCashflows = new double[listCronograma.Count + 1];
            tmpCashflows[0] = -1 * Convert.ToDouble(txtMontoSolicitado.EditValue);
            int y = 1;
            foreach (eCreditoVehicular.eCronogramaDetalle cc in listCronograma)
            {
                tmpCashflows[y] = Convert.ToDouble(cc.imp_cuotaconigv);
                y++;
            }
            double tmpIrr = Microsoft.VisualBasic.Financial.IRR(ref tmpCashflows, 0);

            //txtTIRM.EditValue = Math.Round(tmpIrr, 6);
            //txtTIRAnual.EditValue = Math.Round(Convert.ToDecimal(Math.Pow((1 + tmpIrr), 12) - 1), 6);
            //txtTotalCapital.EditValue = txtMontoSolicitado.EditValue;
            //txtTotalCredito.EditValue = txtMontoSolicitado.EditValue;
            */
        }
        void Inicializar()
        {
            CargarTreeListTresNodos();
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFchInicio.EditValue = oPrimerDiaDelMes;
            dtFchFin.EditValue = oUltimoDiaDelMes;
            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            gpFechas.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            cargarListado();
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

            listadoTreeList = unit.Proyectos.ListarOpcionesMenu<eTreeProyEtaStatus>("19");
            if (listadoTreeList != null && listadoTreeList.Count > 0)
            {
                new Tools.TreeListHelper(treeListProyectos).
                    TreeViewParaDosNodos<eTreeProyEtaStatus>(
                    listadoTreeList, "cod_pro", "dsc_pro",
                    "cod_proyecto", "dsc_proyecto");

                treeListProyectos.Refresh();

                //treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                //treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;


                treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int i = 0; i < treeListProyectos.Nodes.Count; i++)
                {
                    treeListProyectos.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
                    /*for (int j = 0; j < treeListProyectos.Nodes[i].Nodes.Count(); j++)
                    {
                        treeListProyectos.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        //final
                        //for (int k = 0; k < treeListProyectos.Nodes[i].Nodes[j].Nodes.Count(); k++)
                        //{
                        //    treeListProyectos.Nodes[i].Nodes[j].Nodes[k].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                        //}

                    }*/
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

        void cargarListado()
        {
            try
            {
                //SplashScreen.Open("Por favor espere...", "Cargando...");

                var tools = new Tools.TreeListHelper(treeListProyectos);
                var proyectoMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(1);
                cod_proyecto = proyectoMultiple;
                List<eProformas.eProformas_Detalle> ListProformas = new List<eProformas.eProformas_Detalle>();
                ListProformas = unit.Proyectos.ObtenerListadoProformas<eProformas.eProformas_Detalle>(1, cod_proyecto: cod_proyecto, fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"), ejecutivo: sCod_ejecutivoFiltro);
                bsListadoCronograma.DataSource = ListProformas;
                //SplashScreen.Close();
            }
            catch (Exception ex)
            {
                //SplashScreen.Close();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                frmListarClienteSeparaciones frm = new frmListarClienteSeparaciones();
                frm.MiAccion = ListCliSeparacion.Nuevo;

                frm.cotizacion = true;
                frm.codigo_proyecto = "ALL";
                frm.cod_empresa = "";
                frm.dsc_proyecto = "";
                frm.ShowDialog();

                if (frm.dsc_lote != "")
                {
                    txtCodCliente.Text = frm.cod_cliente;
                    txtNroLte.Text = frm.dsc_lote;
                    txtDscCliente.Text = frm.dsc_cliente;
                    txtMontoSolicitado.Text = frm.imp_precio_final.ToString();
                    txtMontoInicial.Text = frm.imp_precio_final.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmListarCotizacion_Load(object sender, EventArgs e)
        {
            HabilitarBotones();
            Inicializar();
        }

        private void gvListarCotizacion_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            //unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListarCotizacion_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void txtCuotas_EditValueChanged(object sender, EventArgs e)
        {
            txtMontoSolicitado.Text = (Convert.ToDecimal(txtMontoInicial.EditValue) - Convert.ToDecimal(txtCuotaInicial.EditValue)- Convert.ToDecimal(txtSeparacion.EditValue)).ToString();
        }

        private void txtDiaPago_EditValueChanged(object sender, EventArgs e)
        {
            txtMontoSolicitado.Text = (Convert.ToDecimal(txtMontoInicial.EditValue) - Convert.ToDecimal(txtCuotaInicial.EditValue) - Convert.ToDecimal(txtSeparacion.EditValue)).ToString();
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantProformas frm = new frmMantProformas();
                frm.MiAccion = Proforma.Nuevo;
                frm.cod_proyecto = cod_proyecto;
                frm.ShowDialog();

                cargarListado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListarCotizacion_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProformas obj = gvListarCotizacion.GetFocusedRow() as eProformas;
                    frmMantProformas frm = new frmMantProformas();
                    frm.cod_proforma = obj.cod_proforma;
                    frm.cod_proyecto = cod_proyecto;
                    frm.MiAccion = Proforma.Editar;
                    //frm.perfil = perfil;

                    //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void tlEjecutivos_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            tlEjecutivos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlEjecutivos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            tlEjecutivos.Refresh();
        }

        private void tlEjecutivos_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
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
                gvListarCotizacion.ExportToXlsx(archivo);
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

        private void btnMostrarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (layoutControlItem20.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnMostrarFiltro.ImageOptions.LargeImage = img;
                btnMostrarFiltro.Caption = "Mostrar Filtro";
                layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (layoutControlItem20.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnMostrarFiltro.ImageOptions.LargeImage = img;
                btnMostrarFiltro.Caption = "Ocultar Filtro";
                layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                return;
            }
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eProformas obj = gvListarCotizacion.GetFocusedRow() as eProformas;
				if(obj == null) { return; }
                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este registro?", "Eliminar registo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    obj.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                    obj = unit.Proyectos.Guardar_Actualizar_Proforma<eProformas>(obj, 2);
                    MessageBox.Show("Se eliminó el registro de manera correcta.", "Eliminar registo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //blGlobal.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    cargarListado();
                    //SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListarCotizacion.ShowPrintPreview();
        }
    }
}