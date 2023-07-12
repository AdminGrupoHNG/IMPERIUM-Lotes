using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using UI_GestionLotes.Formularios.Gestion_Contratos;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;

namespace UI_GestionLotes.Formularios.Operaciones
{
    internal enum ListCliSeparacion
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmListarClienteSeparaciones : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmSepararLote frmHandlerSeparacion;
        frmMantContratos frmHandlerMantContratos;
        List<eProspectosXLote> Listcampanha_grilla = new List<eProspectosXLote>();
        List<eLotes_Separaciones> listLotes = new List<eLotes_Separaciones>();
        public eCliente clienteProforma = new eCliente();
        public eProspectosXLote prospectoProforma = new eProspectosXLote();

        public string cod_cliente = "", cod_empresa = "", cod_prospecto = "", codigo_proyecto = "", dsc_proyecto = "", codigoMultiple = "", cod_lote = "", cod_etapa = "", cod_manzana = "", cod_separacion = "", dsc_cliente = "", dsc_lote = "", prc_uso_exclusivo = "";
        public decimal imp_precio_final = 0;
        public bool cotizacion = false;
        public bool proforma = false, copropietario = false, cliente = false, titular = false;

        public frmListarClienteSeparaciones()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        internal frmListarClienteSeparaciones(frmSepararLote frm, frmMantContratos frmCont)
        {
            InitializeComponent();
            frmHandlerSeparacion = frm;
            frmHandlerMantContratos = frmCont;

            unit = new UnitOfWork();
        }

        private void frmListarClienteSeparaciones_Load(object sender, EventArgs e)
        {
            //radioGroup1.SelectedIndex = 0;
            //radioGroup1_SelectedIndexChanged(radioGroup1, new EventArgs());
            CargarListado();
            CargarListadoProspectos();
            cargarListado();
            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnNuevaSeparacion.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnAgregar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            //btnAgregar2.Appearance.BackColor = Program.Sesion.Colores.Verde;
            //btnAgregar3.Appearance.BackColor = Program.Sesion.Colores.Verde;

            btnVincularCliente.Appearance.BackColor = Program.Sesion.Colores.Verde;
            lblTituloProspecto.ForeColor = Program.Sesion.Colores.Verde;
            lblTituloProformas.ForeColor = Program.Sesion.Colores.Verde;
            lblTituloProspecto.Text = "PROSPECTOS DE TODOS LOS PROYECTOS";//dsc_proyecto;
            lblTituloCliente.Text = dsc_proyecto;
            lblTituloProformas.Text = dsc_proyecto;
            lblTituloCliente.ForeColor = Program.Sesion.Colores.Verde;
            if (cotizacion)
            {
                xtraTabControl1.SelectedTabPageIndex = 3;
                xtraTabPage1.PageVisible = false;
                xtraTabPage2.PageVisible = false;
                xtraTabPage3.PageVisible = true;
                xtraTabPage4.PageVisible = false;
            }
            if (proforma)
            {
                xtraTabControl1.SelectedTabPageIndex = 2;
                xtraTabPage1.PageVisible = true;
                xtraTabPage2.PageVisible = true;
                xtraTabPage3.PageVisible = false;
                xtraTabPage4.PageVisible = false;
            }
            if (copropietario || titular)
            {
                xtraTabControl1.SelectedTabPageIndex = 2;
                xtraTabPage1.PageVisible = true;
                xtraTabPage2.PageVisible = false;
                xtraTabPage3.PageVisible = false;
                xtraTabPage4.PageVisible = false;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente(null, frmHandlerSeparacion, frmHandlerMantContratos);
                frm.MiAccionSeparacion = Cliente.Nuevo;
                frm.cod_proyecto = codigo_proyecto;
                frm.cod_empresa = cod_empresa;
                frm.copropietario = copropietario;
                frm.cod_etapas_multiple = codigoMultiple;
                frm.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaClientes_CustomDrawColumnHeader_1(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaClientes_RowStyle_1(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaAsigProspecto_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaAsigProspecto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaClientes_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    if (cotizacion)
                    {
                        eLotes_Separaciones objLotes = gvListaLotesSeparados.GetFocusedRow() as eLotes_Separaciones;
                        dsc_lote = objLotes.dsc_lote;
                        cod_cliente = objLotes.cod_cliente;
                        dsc_cliente = objLotes.dsc_cliente;
                        imp_precio_final = objLotes.imp_precio_final;
                        cod_lote = objLotes.cod_lote;
                        cod_etapa = objLotes.cod_etapa;
                        cod_manzana = objLotes.cod_manzana;
                        prc_uso_exclusivo = objLotes.prc_uso_exclusivo_desc;
                        this.Close();

                    }
                    if (proforma)
                    {
                        clienteProforma = gvListaClientes.GetFocusedRow() as eCliente;
                        this.Close();
                    }

                    if (!cotizacion && !proforma)
                    {
                        eCliente obj = gvListaClientes.GetFocusedRow() as eCliente;

                        if (frmHandlerSeparacion != null)
                        {
                            if (copropietario)
                            {
                                frmHandlerSeparacion.transferirDatosCopropietario(obj);
                            }
                            else
                            {
                                frmHandlerSeparacion.transferirDatos(obj);
                            }
                        }
                        if (frmHandlerMantContratos != null)
                        {
                            if (titular && cliente)
                            {
                                frmHandlerMantContratos.transferirDatos(obj);
                            }
                            else
                            {
                                frmHandlerMantContratos.transferirDatosCopropietario(obj);

                            }
                        }


                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaAsigProspecto_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    if (proforma)
                    {
                        prospectoProforma = gvListaAsigProspecto.GetFocusedRow() as eProspectosXLote;
                        this.Close(); return;
                    }
                    eProspectosXLote obj = gvListaAsigProspecto.GetFocusedRow() as eProspectosXLote;
                    frmMantCliente frm = new frmMantCliente(null, frmHandlerSeparacion, null);
                    DialogResult msgresult = MessageBox.Show("Este prospecto no tiene un registro de cliente. \n Debe crear el cliente.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msgresult == DialogResult.Yes)
                    {
                        //frm.campos_prospecto = obj;
                        frm.AsignarCamposClientesProspecto(obj);
                        frm.cod_proyecto = codigo_proyecto;
                        frm.cod_empresa = cod_empresa;
                        frm.cod_etapas_multiple = codigoMultiple;

                        frm.ShowDialog();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmListarClienteSeparaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }


        private void gvListarCotizacion_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        //private void gvListaAsigProspecto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    if (e.Column.FieldName == "dsc_ejecutivo" && e.RowHandle >= 0 && e.Column.GroupIndex >= 0)
        //    {
        //        GridView view = sender as GridView;
        //        int groupRowHandle = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);

        //        if (view.IsValidRowHandle(groupRowHandle))
        //        {
        //            string groupSummaryText = view.GetGroupSummaryText(groupRowHandle);
        //            int recordCount = int.Parse(groupSummaryText); // Asumiendo que el texto es el número de registros
        //            string recordCountText = string.Format("({0} registros)", recordCount);

        //            e.Appearance.DrawString(e.Cache, e.DisplayText + " " + recordCountText, e.Bounds);
        //            e.Handled = true;
        //        }
        //    }
        //}


        private void gvListarCotizacion_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cotizacion)
                {
                    eLotes_Separaciones objLotes = gvListaLotesSeparados.GetFocusedRow() as eLotes_Separaciones;
                    dsc_lote = objLotes.dsc_lote;
                    cod_cliente = objLotes.cod_cliente;
                    dsc_cliente = objLotes.dsc_cliente;
                    imp_precio_final = objLotes.imp_precio_final;
                    cod_lote = objLotes.cod_lote;
                    cod_etapa = objLotes.cod_etapa;
                    cod_manzana = objLotes.cod_manzana;
                    prc_uso_exclusivo = objLotes.prc_uso_exclusivo_desc;
                    this.Close();

                }
                if (proforma)
                {
                    clienteProforma = gvListaClientes.GetFocusedRow() as eCliente;
                    this.Close();
                }

                if (!cotizacion && !proforma)
                {
                    eCliente obj = gvListaClientes.GetFocusedRow() as eCliente;
                    if(obj == null) { return; }

                    if (frmHandlerSeparacion != null)
                    {
                        if (copropietario)
                        {
                            frmHandlerSeparacion.transferirDatosCopropietario(obj);
                        }
                        else
                        {
                            frmHandlerSeparacion.transferirDatos(obj);
                        }
                    }
                    if (frmHandlerMantContratos != null)
                    {
                        if (titular && cliente)
                        {
                            frmHandlerMantContratos.transferirDatos(obj);
                        }
                        else
                        {
                            frmHandlerMantContratos.transferirDatosCopropietario(obj);

                        }
                    }
                    this.Close();
                }

                //if (cotizacion)
                //{
                //    eLotes_Separaciones objLotes = gvListaLotesSeparados.GetFocusedRow() as eLotes_Separaciones;
                //    dsc_lote = objLotes.dsc_lote;
                //    cod_cliente = objLotes.cod_cliente;
                //    dsc_cliente = objLotes.dsc_cliente;
                //    imp_precio_final = objLotes.imp_precio_final;
                //    cod_lote = objLotes.cod_lote;
                //    this.Close();

                //}
                //if (proforma)
                //{
                //    clienteProforma = gvListaClientes.GetFocusedRow() as eCliente;
                //    this.Close();
                //}

                //if (!cotizacion && !proforma)
                //{
                //    eCliente obj = gvListaClientes.GetFocusedRow() as eCliente;
                //    if (copropietario)
                //    {
                //        frmHandlerSeparacion.transferirDatosCopropietario(obj);
                //    }
                //    else
                //    {
                //        frmHandlerSeparacion.transferirDatos(obj);
                //    }

                //    if (frmHandlerMantContratos != null)
                //    {
                //        if (copropietario)
                //        {
                //            frmHandlerMantContratos.transferirDatosCopropietario(obj);
                //        }
                //        else
                //        {
                //            frmHandlerMantContratos.transferirDatos(obj);
                //        }
                //    }
                //    this.Close();
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //CargarListado();
        }

        private void gvListarCotizacion_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProformas.eProformas_Detalle obj = gvListarCotizacion.GetFocusedRow() as eProformas.eProformas_Detalle;
                    if (validarCadenaVacio(obj.cod_cliente))
                    {
                        DialogResult msgresult = MessageBox.Show("Esta proforma no tiene un registro de cliente. \n ¿Crear un cliente?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (msgresult == DialogResult.Yes)
                        {
                            frmMantCliente frm = new frmMantCliente(null, frmHandlerSeparacion,null);
                            frm.campos_proforma = obj;
                            frm.AsignarCamposClientesProforma(obj);
                            frm.cod_proyecto = obj.cod_proyecto;
                            frm.cod_empresa = obj.cod_empresa;
                            frm.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        frmHandlerSeparacion.transferirDatosProforma(obj);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevaSeparacion_Click(object sender, EventArgs e)
        {

        }

        private void rgLotes_EditValueChanged(object sender, EventArgs e)
        {
            if (rgLotes.EditValue.ToString() == "SE")
            {
                List<eLotes_Separaciones> lstSeparados = listLotes.Where(x => x.dsc_cliente != "").ToList();
                bsListaLotesSep.DataSource = lstSeparados;
            }
            else
            {
                List<eLotes_Separaciones> lstNoSeparados = listLotes.Where(x => x.dsc_cliente == "").ToList();
                bsListaLotesSep.DataSource = lstNoSeparados;
            }
        }

        private void gvListaLotesSeparados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaLotesSeparados_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaClientes_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnVincularCliente_Click(object sender, EventArgs e)
        {
            try
            {
                eProspectosXLote obj = gvListaAsigProspecto.GetFocusedRow() as eProspectosXLote;
                if (obj != null)
                {
                    DialogResult msgresult = MessageBox.Show("Este prospecto no tiene un registro de cliente. \n Debe crear el cliente.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msgresult == DialogResult.Yes)
                    {

                        frmMantCliente frm = new frmMantCliente(null, frmHandlerSeparacion, null);
                        //frm.campos_prospecto = obj;
                        frm.AsignarCamposClientesProspecto(obj);
                        frm.cod_proyecto = codigo_proyecto;
                        frm.cod_empresa = cod_empresa;
                        frm.cod_etapas_multiple = codigoMultiple;
                        frm.ShowDialog();
                        this.Close();
                    }
                }



            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void cargarListado()
        {
            try
            {
                List<eProformas.eProformas_Detalle> ListProformas = new List<eProformas.eProformas_Detalle>();
                ListProformas = unit.Proyectos.ObtenerListadoProformas<eProformas.eProformas_Detalle>(4, cod_proyecto: codigo_proyecto);
                bsListadoCronograma.DataSource = ListProformas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void gvListaClientes_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        internal ListCliSeparacion MiAccion = ListCliSeparacion.Nuevo;

        //private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    //lkpFiltros.EditValue = null;
        //    //lkpFiltros.Properties.DataSource = null;
        //    //lkpFiltros.Properties.ValueMember = null;
        //    //lkpFiltros.Properties.DisplayMember = null;

        //    if (radioGroup1.SelectedIndex == 0)
        //    {

        //        unit.Proyectos.CargaCombosLookUp("Proyecto", lkpFiltros, "cod_codigo", "dsc_nombre", codigo_proyecto, valorDefecto: true);
        //    }
        //    if (radioGroup1.SelectedIndex == 1)
        //    {

        //        unit.Clientes.CargaCombosLookUp("Empresa", lkpFiltros, "cod_codigo", "dsc_nombre", "ALL", valorDefecto: true);
        //    }
        //    if (radioGroup1.SelectedIndex == 2)
        //    {
        //        unit.Clientes.CargaCombosLookUp("Calificacion", lkpFiltros, "cod_codigo", "dsc_nombre", "ALL", valorDefecto: true);
        //    }
        //    if (radioGroup1.SelectedIndex == 3)
        //    {
        //        unit.Clientes.CargaCombosLookUp("Categoria", lkpFiltros, "cod_codigo", "dsc_nombre", "ALL", valorDefecto: true);
        //    }
        //    if (radioGroup1.SelectedIndex == 4)
        //    {
        //        unit.Clientes.CargaCombosLookUp("Tipo", lkpFiltros, "cod_codigo", "dsc_nombre", "ALL", valorDefecto: true);
        //    }

        //    //lkpFiltros.Refresh();
        //}

        public void CargarListado()
        {
            try
            {
                int opcion = 22;
                string cod_tipo_cliente = "", cod_categoria = "", cod_tipo_documento = "", cod_calificacion = "", cod_tipo_contacto = "", cod_empresa = "", cod_proyecto = "";
                //switch (radioGroup1.SelectedIndex)
                //{
                //    case 4: cod_tipo_cliente = lkpFiltros.EditValue.ToString(); break;
                //    case 3: cod_categoria = lkpFiltros.EditValue.ToString(); break;
                //    case 2: cod_calificacion = lkpFiltros.EditValue.ToString(); break;
                //    case 1: cod_empresa = lkpFiltros.EditValue.ToString(); break;
                //    case 0: cod_proyecto = lkpFiltros.EditValue.ToString();  break;
                //}

                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                List<eCliente> ListCliente = new List<eCliente>();
                ListCliente = unit.Clientes.ListarClientes<eCliente>(opcion, cod_tipo_cliente, cod_categoria, cod_tipo_documento, cod_calificacion, cod_tipo_contacto, cod_empresa, codigo_proyecto);
                /*bsListaClientes.DataSource = null; */
                bsListaClientes.DataSource = ListCliente;
                //SplashScreenManager.CloseForm();

                if (cotizacion)
                {
                    var cod_usuario_validar = validarCadenaVacio(cod_proyecto) ? Program.Sesion.Usuario.cod_usuario : "";
                    listLotes = unit.Proyectos.ListarSeparaciones<eLotes_Separaciones>("5", codigo_proyecto, cod_usuario: cod_usuario_validar);
                    bsListaLotesSep.DataSource = listLotes;

                    List<eLotes_Separaciones> lstNoSeparados = listLotes.Where(x => x.dsc_cliente == "").ToList();
                    bsListaLotesSep.DataSource = lstNoSeparados;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        public void CargarListadoProspectos()
        {
            try
            {
                Listcampanha_grilla = unit.Proyectos.ListarLotesProspectos<eProspectosXLote>(2, codigo_proyecto);

                bsListaAsigProspecto.DataSource = Listcampanha_grilla;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}