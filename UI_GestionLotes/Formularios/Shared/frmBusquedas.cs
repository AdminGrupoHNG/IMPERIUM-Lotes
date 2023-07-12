using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BL_GestionLotes;
using BE_GestionLotes;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using UI_GestionLotes.Formularios.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Clientes_Y_Proveedores.Clientes;
using UI_GestionLotes.Formularios.Clientes_Y_Proveedores.Proveedores;

namespace UI_GestionLotes.Formularios.Shared
{
    public partial class frmBusquedas : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public int BotonAgregarVisible = 0;  //0 se hace visible la botonera de agregar; 1 se visualiza
        
        internal enum MiEntidad
        {
            Cliente = 1,
            ContactoxCliente = 2,
            Trabajador = 3,
            Proveedor = 4,
            ContactosCliente = 5,
            ContactosProveedor= 6,
            ProveedorMultiple = 7,
            ProveedorTipoServicio = 8,
            ClienteEmpresa = 9
        }

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string cod_condicion1 { get; set; }
        public string dsc_condicion1 { get; set; }
        public string cod_condicion2 { get; set; }
        public string dsc_condicion2 { get; set; }
        public string ruc { get; set; }
        public DateTime fch_generica { get; set; }

        internal MiEntidad entidad = MiEntidad.Cliente;
        public string filtro = "";
        public string cod_cliente = "";
        public string cod_proveedor = "";
        public string filtroRUC = "NO";
        public string cod_empresa = "";
        public string cod_sede_empresa = "";
        public string cod_almacen = "";
        public string cod_proyecto = "";
        public string cod_tipo_servicio = "";
        public string flg_transportista = "";

        List<eCliente> ListCliente = new List<eCliente>();
        List<eProveedor> ListProveedor = new List<eProveedor>();
        List<eTrabajador> ListTrabajador = new List<eTrabajador>();
        List<eProveedor_Servicios> ListServicios = new List<eProveedor_Servicios>();
        List<eProveedor_Marca> ListMarca = new List<eProveedor_Marca>();
        public List<eProveedor> ListProv = new List<eProveedor>();

        public frmBusquedas()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        private void frmBusquedas_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        public void Inicializar()
        {
            LlenarDataGrid();
        }

        public void LlenarDataGrid()
        {

            try
            {
                switch (entidad)
                {
                    case MiEntidad.Cliente:
                        ListCliente = unit.Sistema.ListarEntidad<eCliente>(1, "");
                        gcAyuda.DataSource = ListCliente;

                        this.Text = "Busqueda de Clientes";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_cliente" || col.FieldName == "dsc_cliente" || col.FieldName == "dsc_tipo_documento" || col.FieldName == "dsc_documento") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_cliente"].Width = 50;
                        gvAyuda.Columns["dsc_cliente"].Width = 170;
                        gvAyuda.Columns["dsc_tipo_documento"].Width = 45;
                        gvAyuda.Columns["dsc_documento"].Width = 70;


                        gvAyuda.Columns["cod_cliente"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cliente"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_tipo_documento"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_documento"].VisibleIndex = 3;

                        gvAyuda.Columns["cod_cliente"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_cliente"].Caption = "Còdigo";
                        gvAyuda.Columns["dsc_cliente"].Caption = "Cliente";
                        gvAyuda.Columns["dsc_tipo_documento"].Caption = "Tipo Doc";
                        gvAyuda.Columns["dsc_documento"].Caption = "Documento";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_cliente"];
                        gvAyuda.SetAutoFilterValue(gvAyuda.Columns["dsc_cliente"], filtro, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ContactoxCliente:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                        btnAgregar.Text = "Agregar Contacto";

                        ListCliente = unit.Sistema.ListarEntidad<eCliente>(2, cod_condicion1, cod_condicion2);
                        gcAyuda.DataSource = ListCliente;

                        this.Text = "Busqueda de Contactos del Cliente";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "dsc_cadena_direccion" || col.FieldName == "dsc_cliente" || col.FieldName == "dsc_contacto") { col.Visible = true; }
                        }

                        gvAyuda.Columns["dsc_contacto"].Width = 100;
                        gvAyuda.Columns["dsc_cadena_direccion"].Width = 120;
                        gvAyuda.Columns["dsc_cliente"].Width = 120;


                        gvAyuda.Columns["dsc_contacto"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cadena_direccion"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_cliente"].VisibleIndex = 2;

                        //gvAyuda.Columns["cod_tipo_cliente"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


                        gvAyuda.Columns["dsc_contacto"].Caption = "Contacto";
                        gvAyuda.Columns["dsc_cadena_direccion"].Caption = "Dirección";
                        gvAyuda.Columns["dsc_cliente"].Caption = "Cliente";
                        break;

                    case MiEntidad.Proveedor:
                        layoutControlItem2.Visibility = LayoutVisibility.Always;
                        ListProveedor = unit.Sistema.ListarEntidad<eProveedor>(3, "", "", flg_transportista, cod_empresa);
                        gcAyuda.DataSource = ListProveedor;

                        this.Text = "Busqueda de Proveedores";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "num_documento" || /*col.FieldName == "cod_proveedor" ||*/ col.FieldName == "dsc_razon_social" || col.FieldName == "dsc_razon_comercial") { col.Visible = true; }
                        }
                        gvAyuda.Columns["num_documento"].Width = 50;
                        gvAyuda.Columns["dsc_razon_social"].Width = 170;
                        gvAyuda.Columns["dsc_razon_comercial"].Width = 170;

                        gvAyuda.Columns["num_documento"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_razon_social"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_razon_comercial"].VisibleIndex = 2;

                        gvAyuda.Columns["num_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["dsc_razon_comercial"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["num_documento"].Caption = "N° documento";
                        gvAyuda.Columns["dsc_razon_social"].Caption = "Razón Social";
                        gvAyuda.Columns["dsc_razon_comercial"].Caption = "Nombre Comercial";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        if (filtroRUC == "SI")
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["num_documento"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["num_documento"], filtro);
                        }
                        else
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_razon_social"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_razon_social"], filtro);
                        }
                        
                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ContactosCliente:
                        List<eCliente_Contactos> ListClienteContacto = unit.Clientes.ListarContactos<eCliente_Contactos>(7, cod_cliente, 0);
                        gcAyuda.DataSource = ListClienteContacto;

                        this.Text = "Busqueda de Contactos";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_contacto" || col.FieldName == "dsc_nombre_completo" || col.FieldName == "dsc_cargo") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_contacto"].Width = 50;
                        gvAyuda.Columns["dsc_nombre_completo"].Width = 170;
                        gvAyuda.Columns["dsc_cargo"].Width = 70;

                        gvAyuda.Columns["cod_contacto"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_nombre_completo"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_cargo"].VisibleIndex = 2;

                        gvAyuda.Columns["cod_contacto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_cargo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_contacto"].Caption = "Código";
                        gvAyuda.Columns["dsc_nombre_completo"].Caption = "Contacto";
                        gvAyuda.Columns["dsc_cargo"].Caption = "Cargo";

                        break;

                    case MiEntidad.ContactosProveedor:
                        List<eProveedor_Contactos> ListProveedorContacto = unit.Sistema.ListarEntidad<eProveedor_Contactos>(5, cod_condicion1);
                        gcAyuda.DataSource = ListProveedorContacto;
                        //dsc_cargo es dsc_proveedor, solo que econtacto no fue declarado
                        this.Text = "Busqueda de Contactos del Proveedor";
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "dsc_nombre" || col.FieldName == "dsc_cargo") { col.Visible = true; }
                        }
                       
                        gvAyuda.Columns["dsc_nombre"].Width = 170;
                        gvAyuda.Columns["dsc_cargo"].Width = 170;

                      
                        gvAyuda.Columns["dsc_nombre"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cargo"].VisibleIndex = 1;

                      
                        gvAyuda.Columns["dsc_nombre"].Caption = "Contacto";
                        gvAyuda.Columns["dsc_cargo"].Caption = "Proveedor";

                        break;

                    case MiEntidad.Trabajador:
                        layoutControlItem2.Visibility = LayoutVisibility.Never;
                        layoutAgregar.Visibility = LayoutVisibility.Never;
                        ListTrabajador = unit.Trabajador.ListarTrabajadores<eTrabajador>(1, "", cod_empresa);
                        gcAyuda.DataSource = ListTrabajador;

                        this.Text = "Busqueda de Trabajadores";
                         
                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_trabajador" || col.FieldName == "dsc_nombres_completos") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_trabajador"].Width = 50;
                        gvAyuda.Columns["dsc_nombres_completos"].Width = 200;
                        gvAyuda.Columns["dsc_empresa"].Width = 100;
                        
                        gvAyuda.Columns["cod_trabajador"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_nombres_completos"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_empresa"].VisibleIndex = 2;

                        gvAyuda.Columns["cod_trabajador"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_trabajador"].Caption = "Código";
                        gvAyuda.Columns["dsc_nombres_completos"].Caption = "Nombre Completo";
                        gvAyuda.Columns["dsc_empresa"].Caption = "Empresa";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_nombres_completos"];
                        gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_nombres_completos"], filtro);

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ProveedorMultiple:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = LayoutVisibility.Always;
                        }
                        gvAyuda.OptionsSelection.MultiSelect = true;
                        gvAyuda.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                        gvAyuda.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
                        layoutControlItem2.Visibility = LayoutVisibility.Always;
                        ListProveedor = unit.Sistema.ListarEntidad<eProveedor>(3, "");
                        gcAyuda.DataSource = ListProveedor;

                        this.Text = "Busqueda de Proveedores";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "num_documento" || /*col.FieldName == "cod_proveedor" ||*/ col.FieldName == "dsc_razon_social" || col.FieldName == "dsc_razon_comercial") { col.Visible = true; }
                        }
                        gvAyuda.Columns["num_documento"].Width = 50;
                        gvAyuda.Columns["dsc_razon_social"].Width = 170;
                        gvAyuda.Columns["dsc_razon_comercial"].Width = 170;

                        gvAyuda.Columns["num_documento"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_razon_social"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_razon_comercial"].VisibleIndex = 2;

                        gvAyuda.Columns["num_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["dsc_razon_comercial"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["num_documento"].Caption = "N° documento";
                        gvAyuda.Columns["dsc_razon_social"].Caption = "Razón Social";
                        gvAyuda.Columns["dsc_razon_comercial"].Caption = "Nombre Comercial";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        if (filtroRUC == "SI")
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["num_documento"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["num_documento"], filtro);
                        }
                        else
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_razon_social"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_razon_social"], filtro);
                        }

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ProveedorTipoServicio:
                        if (BotonAgregarVisible == 1)
                        {
                            this.layoutAgregar.Visibility = LayoutVisibility.Always;
                            this.layoutEspacioAgregar.Visibility = LayoutVisibility.Always;
                        }
                        gvAyuda.OptionsSelection.MultiSelect = true;
                        gvAyuda.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                        gvAyuda.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
                        //layoutControlItem2.Visibility = LayoutVisibility.Always;
                        ListProveedor = unit.Proveedores.ListarMarcasProveedor<eProveedor>(13, "", cod_tipo_servicio);
                        gcAyuda.DataSource = ListProveedor;

                        this.Text = "Busqueda de Proveedores";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "num_documento" || /*col.FieldName == "cod_proveedor" ||*/ col.FieldName == "dsc_razon_social" || col.FieldName == "dsc_razon_comercial") { col.Visible = true; }
                        }
                        gvAyuda.Columns["num_documento"].Width = 50;
                        gvAyuda.Columns["dsc_razon_social"].Width = 170;
                        gvAyuda.Columns["dsc_razon_comercial"].Width = 170;

                        gvAyuda.Columns["num_documento"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_razon_social"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_razon_comercial"].VisibleIndex = 2;

                        gvAyuda.Columns["num_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gvAyuda.Columns["dsc_razon_comercial"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["num_documento"].Caption = "N° documento";
                        gvAyuda.Columns["dsc_razon_social"].Caption = "Razón Social";
                        gvAyuda.Columns["dsc_razon_comercial"].Caption = "Nombre Comercial";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        if (filtroRUC == "SI")
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["num_documento"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["num_documento"], filtro);
                        }
                        else
                        {
                            gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_razon_social"];
                            gvAyuda.SetRowCellValue(GridControl.AutoFilterRowHandle, gvAyuda.Columns["dsc_razon_social"], filtro);
                        }

                        gvAyuda.ShowEditor();
                        break;

                    case MiEntidad.ClienteEmpresa:
                        layoutControlItem2.Visibility = LayoutVisibility.Always;
                        btnNuevoProveedor.Text = "Nuevo Cliente";

                        ListCliente = unit.Sistema.ListarEntidad<eCliente>(6, cod_condicion1);
                        gcAyuda.DataSource = ListCliente;

                        this.Text = "Busqueda de Clientes";

                        foreach (GridColumn col in gvAyuda.Columns)
                        {
                            col.Visible = false;
                            if (col.FieldName == "cod_cliente" || col.FieldName == "dsc_cliente" || col.FieldName == "dsc_tipo_documento" || col.FieldName == "dsc_documento") { col.Visible = true; }
                        }
                        gvAyuda.Columns["cod_cliente"].Width = 50;
                        gvAyuda.Columns["dsc_cliente"].Width = 170;
                        gvAyuda.Columns["dsc_tipo_documento"].Width = 45;
                        gvAyuda.Columns["dsc_documento"].Width = 70;


                        gvAyuda.Columns["cod_cliente"].VisibleIndex = 0;
                        gvAyuda.Columns["dsc_cliente"].VisibleIndex = 1;
                        gvAyuda.Columns["dsc_tipo_documento"].VisibleIndex = 2;
                        gvAyuda.Columns["dsc_documento"].VisibleIndex = 3;

                        gvAyuda.Columns["cod_cliente"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvAyuda.Columns["dsc_documento"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        gvAyuda.Columns["cod_cliente"].Caption = "Còdigo";
                        gvAyuda.Columns["dsc_cliente"].Caption = "Cliente";
                        gvAyuda.Columns["dsc_tipo_documento"].Caption = "Tipo Doc";
                        gvAyuda.Columns["dsc_documento"].Caption = "Documento";

                        //focus en el campo autofilter
                        gcAyuda.Select();
                        gcAyuda.ForceInitialize();
                        gvAyuda.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        gvAyuda.FocusedColumn = gvAyuda.Columns["dsc_cliente"];
                        gvAyuda.SetAutoFilterValue(gvAyuda.Columns["dsc_cliente"], filtro, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);

                        gvAyuda.ShowEditor();
                        break;

                }

            }
            catch
            {
            }
        }

        public void PasarDatos()
        {
            //DataGridViewRow row = (DataGridViewRow)gvAyuda.GetFocusedRow();
            switch (entidad)
            {
                case MiEntidad.Cliente:
                    eCliente eCliente = gvAyuda.GetFocusedRow() as eCliente;
                    descripcion = eCliente.dsc_cliente;
                    codigo = eCliente.cod_cliente;
                    break;
                case MiEntidad.ContactoxCliente:
                    eCliente eContactoxCliente = gvAyuda.GetFocusedRow() as eCliente;
                    codigo = eContactoxCliente.cod_tipo_cliente;
                    cod_condicion1 = eContactoxCliente.cod_cliente;
                    dsc_condicion1 = eContactoxCliente.dsc_cliente;
                    cod_condicion2 = eContactoxCliente.cod_departamento;
                    break;
                case MiEntidad.Proveedor:
                    eProveedor eProv = gvAyuda.GetFocusedRow() as eProveedor;
                    descripcion = eProv.dsc_proveedor;
                    codigo = eProv.cod_proveedor;
                    ruc = eProv.num_documento;
                    cod_condicion1 = eProv.dsc_placavehiculo;
                    break;
                case MiEntidad.ContactosCliente:
                    eCliente_Contactos eContact = gvAyuda.GetFocusedRow() as eCliente_Contactos;
                    descripcion = eContact.dsc_nombre_completo;
                    codigo = eContact.cod_contacto.ToString();
                    break;
                case MiEntidad.ContactosProveedor:
                    eProveedor_Contactos eContactProv = gvAyuda.GetFocusedRow() as eProveedor_Contactos;
                    descripcion = eContactProv.dsc_nombre;
                    codigo = eContactProv.cod_contacto.ToString();
                    cod_condicion1 = eContactProv.cod_proveedor;
                    dsc_condicion1 = eContactProv.dsc_cargo;
                    break;
                case MiEntidad.Trabajador:
                    eTrabajador eTrab = gvAyuda.GetFocusedRow() as eTrabajador;
                    descripcion = eTrab.dsc_nombres_completos;
                    codigo = eTrab.cod_trabajador;
                    break;
                case MiEntidad.ClienteEmpresa:
                    eCliente eClienteEmp = gvAyuda.GetFocusedRow() as eCliente;
                    descripcion = eClienteEmp.dsc_cliente;
                    codigo = eClienteEmp.cod_cliente;
                    break;
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            switch (entidad)
            {
                case MiEntidad.ContactoxCliente:
                    frmMantContactoDireccionCliente frm = new frmMantContactoDireccionCliente();
                    frm.MiAccion = frmMantContactoDireccionCliente.Cliente.NuevoContactoDesdeIncidente;
                    frm.ShowDialog();
                    codigo = frm.codigo;
                    cod_condicion1 = frm.cod_condicion1;
                    dsc_condicion1 = frm.dsc_condicion1;
                    cod_condicion2 = frm.cod_condicion2;
                    this.Close();
                    break;
                    gvAyuda.RefreshData();
                    SplashScreenManager.CloseForm();
                    this.Close();
                    break;
                case MiEntidad.ProveedorMultiple:
                    foreach (int nRow in gvAyuda.GetSelectedRows())
                    {
                        eProveedor objP = gvAyuda.GetRow(nRow) as eProveedor;
                        if (objP != null) ListProv.Add(objP);
                    }
                    this.Close();
                    break;
                case MiEntidad.ProveedorTipoServicio:
                    foreach (int nRow in gvAyuda.GetSelectedRows())
                    {
                        eProveedor objP = gvAyuda.GetRow(nRow) as eProveedor;
                        if (objP != null) ListProv.Add(objP);
                    }
                    this.Close();
                    break;
            }
        }

        private void gvAyuda_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gvAyuda.OptionsSelection.MultiSelectMode != DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect)
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    PasarDatos();
                    this.Close();
                }
            }
        }

        private void gvAyuda_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void frmBusquedas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void gvAyuda_ShownEditor(object sender, EventArgs e)
        {
            if (gvAyuda.FocusedRowHandle == GridControl.AutoFilterRowHandle)
            {
                var editor = (TextEdit)gvAyuda.ActiveEditor;
                editor.SelectionLength = 0;
                editor.SelectionStart = editor.Text.Length;
            }
        }

        private void gvAyuda_KeyDown(object sender, KeyEventArgs e)
        {
            if (gvAyuda.FocusedRowHandle >= 0 && e.KeyCode == Keys.Enter)
            {
                PasarDatos();
                this.Close();
            }
        }

        private void gvAyuda_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            switch (entidad)
            {
                case MiEntidad.ClienteEmpresa:
                    frmMantCliente frmCliente = new frmMantCliente();
                    frmCliente.MiAccion = Cliente.Nuevo;
                    frmCliente.ShowDialog();
                    Inicializar();
                    break;
                case MiEntidad.Proveedor:
                    frmMantProveedor frm = new frmMantProveedor();
                    
                    
                    
                    
                    frm.ShowDialog();
                    Inicializar();
                    break;
            }
        }
    }
}