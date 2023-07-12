using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.Images;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_GestionLotes.Formularios.Operaciones;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    internal enum Proyecto
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantProyecto : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListadoProyectos frmHandler;
        public eUsuario user = new eUsuario();
        internal Proyecto MiAccion = Proyecto.Nuevo;
        internal Proyecto MiAccionEtapa = Proyecto.Nuevo;
        internal Proyecto MiAccionMemoria = Proyecto.Nuevo;
        public string cod_proyecto = "";
        public string cod_etapa = "";
        public string cod_manzana = "";
        public string cod_manzana_validar = "";
        public decimal cod_manzana_desde = 0;
        public decimal cod_manzana_hasta = 0;
        public string cod_tipoLote = "", cod_memoria_desc = "";
        public int num_linea = 0;
        public string ActualizarListadoTipoLote = "NO";
        string binarios = "";
        string base64Imagen1 = "", base64Imagen2 = "", base64Imagen3 = "", dsc_Imagen1 = "", dsc_Imagen2 = "", dsc_Imagen3 = "";
        byte[] archivoBytes;
        List<eVariablesGenerales> ListTipolote = new List<eVariablesGenerales>();
        List<eVariablesGenerales> ListTipoloteXetapa = new List<eVariablesGenerales>();
        List<eVariablesGenerales> ListEtapaXmanzana = new List<eVariablesGenerales>();
        List<eMemoDes> ListMemoDesc = new List<eMemoDes>();

        CookieContainer cokkie = new CookieContainer();


        public frmMantProyecto()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmMantProyecto(frmListadoProyectos frm)
        {
            InitializeComponent();
            unit = new UnitOfWork();
            frmHandler = frm;
        }
        private void frmMantProyecto_Load(object sender, EventArgs e)
        {
            layoutControlGroup2.AppearanceGroup.ForeColor = Program.Sesion.Colores.Verde;
            layoutControlGroup4.AppearanceGroup.ForeColor = Program.Sesion.Colores.Verde;
            simpleLabelItem1.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            labelControl1.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl2.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl3.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl4.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl5.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            labelControl6.Appearance.ForeColor = Program.Sesion.Colores.Verde;
            //btnNuevoEtapa.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnListarMnz.Appearance.BackColor = Program.Sesion.Colores.Verde;
            btnNuevoTipoLotePorEtapa.Appearance.BackColor = Program.Sesion.Colores.Verde;
            Inicializar();
            HabilitarBotones();
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) Ver(false, true, false, false);
            }

            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.SolucionAbrir.Solucion);
            eVentana oPerfilVisualizador = listPerfil.Find(x => x.cod_perfil == 9);
            if (oPerfilVisualizador != null)
            {
                if (MiAccion == Proyecto.Editar)
                {
                    Ver(false, true, false, true);
                }
                if (MiAccion == Proyecto.Vista)
                {
                    Ver(false, true, false, false);
                }
            }
            eVentana oPerfilRegistrador = listPerfil.Find(x => x.cod_perfil == 8);
            if (oPerfilRegistrador != null)
            {
                if (MiAccion == Proyecto.Editar)
                {
                    Ver(true, false, true, true);
                }
                if (MiAccion == Proyecto.Vista)
                {
                    Ver(true, false, true, false);
                }
            }
        }
        private void Ver(Boolean ReadOnlyBotones, Boolean ReadOnlyCampos, Boolean ReadOnlyGrilla, Boolean ReadOnlyFlechas)
        {

            btnNuevo.Enabled = ReadOnlyBotones;
            btnGuardar.Enabled = ReadOnlyBotones;
            picAnteriorProyecto.Enabled = ReadOnlyFlechas;
            picSiguienteProyecto.Enabled = ReadOnlyFlechas;
            chkEstado.ReadOnly = ReadOnlyCampos;
            txtNombre.ReadOnly = ReadOnlyCampos;
            mmDescripcion.ReadOnly = ReadOnlyCampos;
            picLogoProyecto.ReadOnly = ReadOnlyCampos;
            glkpEmpresa.ReadOnly = ReadOnlyCampos;
            txtJefeProyecto.ReadOnly = ReadOnlyCampos;
            txtArquitecto.ReadOnly = ReadOnlyCampos;
            dtFechaEntrega.ReadOnly = ReadOnlyCampos;
            btnFchEnt.Enabled = ReadOnlyCampos;
            dtFechaInicio.ReadOnly = ReadOnlyCampos;
            btnFchIni.Enabled = ReadOnlyCampos;
            dtFechaTermino.ReadOnly = ReadOnlyCampos;
            txtValorCompra.ReadOnly = ReadOnlyCampos;
            txtAlcabala.ReadOnly = ReadOnlyCampos;
            txtOtrosGastos.ReadOnly = ReadOnlyCampos;
            txtInversionInicial.ReadOnly = ReadOnlyCampos;
            lkpMoneda.ReadOnly = ReadOnlyCampos;
            btnFchTer.Enabled = ReadOnlyBotones;
            picImagenProyecto.ReadOnly = ReadOnlyCampos;
            picImagenProyecto2.ReadOnly = ReadOnlyCampos;
            gvListaEtapas.OptionsBehavior.Editable = ReadOnlyGrilla;
            txtAreaUsoExcluEtapa.ReadOnly = ReadOnlyCampos;
            txtAreaUsoComEtapa.ReadOnly = ReadOnlyCampos;
            mmDescripcionEtapa.ReadOnly = ReadOnlyCampos;
            btnNuevoEtapa.Enabled = ReadOnlyBotones;
            btnGuardarEtapa.Enabled = ReadOnlyBotones;
            btnEliminarEtapa.Enabled = ReadOnlyBotones;
            gvTipoLote.OptionsBehavior.Editable = ReadOnlyGrilla;
            btnNuevoTipoLotePorEtapa.Enabled = ReadOnlyBotones;
            gvManzana.OptionsBehavior.Editable = ReadOnlyGrilla;
            btnListarMnz.Enabled = ReadOnlyBotones;
            txtDesde.ReadOnly = ReadOnlyCampos;
            txtHasta.ReadOnly = ReadOnlyCampos;
            gvMemoDesc.OptionsBehavior.Editable = ReadOnlyGrilla;
            btnNuevaMemoriaDescriptiva.Enabled = ReadOnlyBotones;
            btnGuardarMemoriaDescriptiva.Enabled = ReadOnlyBotones;
            btnEliminarMemoriaDes.Enabled = ReadOnlyBotones;
            rtxtDescripcionMemoriaDescriptiva.ReadOnly = ReadOnlyCampos;
        }



        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string mensaje = validarCampos();
            if (mensaje == null)
            {
                try
                {
                    string result = "";
                    switch (MiAccion)
                    {
                        case Proyecto.Nuevo: result = Guardar(); break;
                        case Proyecto.Editar: result = Modificar(); break;
                    }

                    if (result == "OK")
                    {
                        XtraMessageBox.Show("Se guardó el proyecto de manera satisfactoria", "Guardar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (frmHandler != null)
                        {
                            int nRow = frmHandler.gvListaProyectos.FocusedRowHandle;
                            frmHandler.frmListadoProyectos_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                            frmHandler.gvListaProyectos.FocusedRowHandle = nRow;
                        }
                        if (MiAccion == Proyecto.Nuevo)
                        {
                            MiAccion = Proyecto.Editar;
                            xtraTabEtapaTipLot.PageEnabled = true;
                            xtraTabPage2.PageEnabled = true;
                            glkpEmpresa.Enabled = false;
                            MiAccionEtapa = Proyecto.Nuevo;
                            eProyecto ePro = new eProyecto();
                            ePro = unit.Proyectos.ObtenerProyecto<eProyecto>("2", cod_proyecto);
                            txtUsuarioRegistro.Text = ePro.cod_usuario_registro;
                            if (ePro.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = ePro.fch_registro; }
                            txtUsuarioCambio.Text = ePro.cod_usuario_cambio;
                            if (ePro.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = ePro.fch_cambio; }
                        }
                        if (!validarCadenaVacio(base64Imagen1)) { GuardarModificarImagenes("1"); }
                        if (!validarCadenaVacio(base64Imagen2)) { GuardarModificarImagenes("2"); }
                        if (!validarCadenaVacio(base64Imagen3)) { GuardarModificarImagenes("3"); }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                XtraMessageBox.Show(mensaje, "Guardar proyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GuardarEtapa()
        {

            eProyecto_Etapa eProEta = AsignarValoresProyectoEtapa();
            eProEta = unit.Proyectos.MantenimientoProyectoEtapa<eProyecto_Etapa>(eProEta);
            if (eProEta != null)
            {
                cod_etapa = eProEta.cod_etapa;
                txtCodigoEtapa.Text = cod_etapa;
                return "OK";
            }
            return null;
        }

        private string ModificarEtapa()
        {

            eProyecto_Etapa eProEta = AsignarValoresProyectoEtapa();
            eProEta = unit.Proyectos.MantenimientoProyectoEtapa<eProyecto_Etapa>(eProEta);

            if (eProEta != null)
            {
                cod_etapa = eProEta.cod_etapa;
                return "OK";
            }
            return null;
        }

        private string ModificarMedidasProyecto()
        {

            eProyecto eProMedidas = AsignarValoresMedidasProyecto();
            eProMedidas = unit.Proyectos.MantenimientoMedidasProyecto<eProyecto>(eProMedidas);

            if (eProMedidas != null)
            {
                cod_proyecto = eProMedidas.cod_proyecto;
                return "OK";
            }
            return null;
        }

        private string GuardarModificarImagenes(string dsc_numImg = "")
        {

            eProyecto ePro = AsignarValoresProyectoImagenes(dsc_numImg);
            ePro = unit.Proyectos.Mantenimiento_Proyecto_Imagenes<eProyecto>(ePro);
            if (ePro != null)
            {
                return "OK";
            }
            return null;
        }

        private string Guardar()
        {
            eProyecto ePro = AsignarValoresProyecto();
            ePro = unit.Proyectos.Mantenimiento_Proyecto<eProyecto>(ePro);
            if (ePro != null)
            {
                cod_proyecto = ePro.cod_proyecto;
                txtCodProyecto.Text = cod_proyecto;
                return "OK";
            }
            return null;
        }

        private string Modificar()
        {

            eProyecto ePro = AsignarValoresProyecto();
            ePro = unit.Proyectos.Mantenimiento_Proyecto<eProyecto>(ePro);

            if (ePro != null)
            {
                cod_proyecto = ePro.cod_proyecto;
                return "OK";
            }
            return null;
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case Proyecto.Nuevo:
                    CargarCombos();
                    //CargarListado("TODOS", "");

                    Nuevo();
                    break;
                case Proyecto.Editar:
                    CargarCombos();
                    CargarListadoEtapas("1");
                    Editar();
                    CargarListadoMemoriaDesc("1");
                    //CargarTreeListCuatroNodos();
                    break;
                case Proyecto.Vista:
                    CargarCombos();
                    CargarListadoEtapas("1");
                    //richEditControl1.WordMLText;
                    Editar();
                    CargarListadoMemoriaDesc("1");
                    Ver(false, true, false, false);
                    break;
            }
            //unit.Globales.ConfigurarGridView_ClasicStyle(gcListaMemoriaDescriptiva, gvListaMemoriaDescriptiva);
        }

        private void obtenerListadoTipoLoteXEtapa()
        {
            bsListadoTipoLote.DataSource = null; bsListadoTipoLote.DataSource = ListTipolote;
            if (MiAccionEtapa != Proyecto.Nuevo)
            {

                List<eVariablesGenerales> lista = unit.Proyectos.ListarTipoLotexEtapas<eVariablesGenerales>("1", cod_etapa, cod_proyecto);
                ListTipoloteXetapa = lista;



                foreach (eVariablesGenerales obj in lista)
                {
                    eVariablesGenerales oLoteEtap = ListTipolote.Find(x => x.cod_variable == obj.cod_variable);
                    if (oLoteEtap != null) { oLoteEtap.Seleccionado = true; oLoteEtap.num_total_lotes = obj.num_total_lotes; }
                }
            }
            gvTipoLote.RefreshData();
        }

        private void obtenerListadoManzanaXEtapa()
        {
            //bsManzana.DataSource = null; bsManzana.DataSource = ListEtapaXmanzana;
            if (MiAccionEtapa != Proyecto.Nuevo)
            {
                List<eVariablesGenerales> lista = unit.Proyectos.ListarManzanaxEtapas<eVariablesGenerales>("1", cod_etapa,cod_proyecto:cod_proyecto);
                ListEtapaXmanzana = lista;
                bsManzana.DataSource = null;
                bsManzana.DataSource = ListEtapaXmanzana;

            }
            gvManzana.RefreshData();

        }
        public void AgregarTipoLote()
        {
            //foreach(eVariablesGenerales obj in ListTipolote)
            //{
            //    contador -=  1;
            //    if(contador == 0) { ListTipolote.Add(ListAddTipoLote); break; }
            //}
            try
            {
                //int contador = ListTipolote.Count;
                eVariablesGenerales ListAddTipoLote = new eVariablesGenerales
                {
                    Seleccionado = true,
                    flg_activo = "SI",
                    cod_variable = "0"
                };
                cod_tipoLote = ListAddTipoLote.cod_variable;
                ListTipolote.Add(ListAddTipoLote);
                gvTipoLote.RefreshData();
                gvTipoLote.FocusedRowHandle = gvTipoLote.RowCount - 1;

                //rtxtNombre.EnableCustomMaskTextInput(args => { args.Cancel(); return; });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        public void AgregarMemoriaDesc()
        {

            try
            {
                int numOrden = ListMemoDesc.Count() + 1;
                eMemoDes ListAddMemoD = new eMemoDes
                {
                    cod_memoria_desc = "0",
                    num_orden = numOrden,
                    flg_activo = "SI"
                };
                cod_memoria_desc = ListAddMemoD.cod_memoria_desc;
                ListMemoDesc.Add(ListAddMemoD);
                gvMemoDesc.RefreshData();
                gvMemoDesc.FocusedRowHandle = gvMemoDesc.RowCount - 1;

                //rtxtNombre.EnableCustomMaskTextInput(args => { args.Cancel(); return; });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        public void CargarListadoManzanas(string accion, string rango1, string rango2)
        {
            try
            {
                List<eVariablesGenerales> ListProyectoManzana = new List<eVariablesGenerales>();
                ListProyectoManzana = unit.Proyectos.ListarManzana<eVariablesGenerales>(accion, rango1, rango2);
                if (ListEtapaXmanzana.Count <= 0)
                {
                    ListEtapaXmanzana = ListProyectoManzana; bsManzana.DataSource = null; bsManzana.DataSource = ListEtapaXmanzana;
                }
                else
                {
                    foreach (eVariablesGenerales obj in ListProyectoManzana)
                    {
                        eVariablesGenerales objM = ListEtapaXmanzana.Find(x => x.cod_variable == obj.cod_variable);
                        if (objM == null) ListEtapaXmanzana.Add(obj);
                    }
                    gvManzana.RefreshData();
                    return;
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
                List<eProyecto_Etapa> ListProyectoEtapa = new List<eProyecto_Etapa>();
                ListProyectoEtapa = unit.Proyectos.ListarEtapa<eProyecto_Etapa>(accion, cod_etapa, cod_proyecto);
                bsEtapaBindingSource.DataSource = null;
                bsEtapaBindingSource.DataSource = ListProyectoEtapa;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarListadoMemoriaDesc(string accion, string cod_memoria_desc = "")
        {
            try
            {
                int nRow = gvMemoDesc.FocusedRowHandle;

                ListMemoDesc = unit.Proyectos.ListarMemoriaDesc<eMemoDes>(accion, cod_proyecto, cod_memoria_desc);
                bsMemoDes.DataSource = ListMemoDesc;
                gvMemoDesc.FocusedRowHandle = nRow;
                gvListaMemoriaDescriptiva_FocusedRowChanged(gvMemoDesc, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(nRow - 1, nRow));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarListadoLote(string NombreGrupo, string Codigo)
        {
            try
            {
                string cod_proyecto = "", accion = "", cod_empresa = "";

                switch (NombreGrupo)
                {
                    case "TODOS": accion = "1"; break;
                    case "Por Proyecto": cod_empresa = Codigo; accion = "3"; break;
                }

                List<eVariablesGenerales> ListProyecto = new List<eVariablesGenerales>();
                ListProyecto = unit.Proyectos.ListarTipoLote<eVariablesGenerales>(accion, cod_proyecto);
                bsListadoTipoLote.DataSource = null;
                bsListadoTipoLote.DataSource = ListProyecto;
                ListTipolote = ListProyecto;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void CargarCombos()
        {
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "", "", valorDefecto: true);
            CargarCombosGridLookup("EmpresasUsuarios", glkpEmpresa, "cod_empresa", "dsc_empresa", "", "", valorDefecto: true);
            unit.Proyectos.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "");
            unit.Proyectos.CargaCombosLookUp("TipoMoneda", lkpMoneda, "cod_moneda", "dsc_simbolo", "");

        }
        //private void CargarCombosManzana()
        //{

        //    unit.Proyectos.CargaCombosLookUpManzana("Desde", lkpDesde, "cod_variable", "dsc_Nombre", "");
        //    unit.Proyectos.CargaCombosLookUpManzana("Hasta", lkpHasta, "cod_variable", "dsc_Nombre", "");


        //}

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = unit.Proyectos.ObtenerListadoGridLookup(nCombo, Program.Sesion.Usuario.cod_usuario);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];

            if (campoValueMember == "cod_empresa")
            {
                DataRow filaTabla = tabla.Rows[0];
                glkpEmpresa.EditValue = filaTabla[0];
            }
        }

        private void LimpiarCamposProyectoEtapa()
        {
            MiAccionEtapa = Proyecto.Nuevo;
            txtCodigoEtapa.Text = "";
            txtAreaUsoExcluEtapa.Text = "";
            txtAreaUsoComEtapa.Text = "";
            mmDescripcionEtapa.Text = "";
            txtDesde.Text = "";
            txtHasta.Text = "";
            bsListadoTipoLote.DataSource = null;
            bsManzana.DataSource = null;
            btnNuevoEtapa.Enabled = false;


        }

        private void LimpiarCamposProyecto()
        {
            MiAccion = Proyecto.Nuevo;
            txtCodProyecto.Text = "";
            txtNombre.Text = "";
            mmDescripcion.Text = "";
            glkpEmpresa.Enabled = true;
            txtJefeProyecto.Text = "";
            txtArquitecto.Text = "";
            //dtFechaInicio.EditValue = null;
            //dtFechaTermino.EditValue = null;
            //dtFechaEntrega.EditValue = null;
            txtValorCompra.Text = "";
            txtAlcabala.Text = "";
            txtOtrosGastos.Text = "";
            txtInversionInicial.Text = "";
            txtAreaTotalProyecto.Text = "";
            txtPorcUsoCom.Text = "";
            txtPorcUsoExc.Text = "";
            btnNuevo.Enabled = false;


        }

        private void Nuevo()
        {
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.dsc_usuario;
            txtUsuarioCambio.Text = "";
            dtFechaRegistro.EditValue = DateTime.Now;
            dtFechaModificacion.EditValue = null;
            lkpPais.EditValue = "00001";
            DateTime date = DateTime.Now;
            DateTime oInicioDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oTerminoDiaDelMes = oInicioDiaDelMes.AddMonths(1).AddDays(-1);
            DateTime oEntregaDiaDelMes = oInicioDiaDelMes.AddYears(1);
            dtFechaInicio.EditValue = oInicioDiaDelMes;
            dtFechaTermino.EditValue = oTerminoDiaDelMes;
            dtFechaEntrega.EditValue = oEntregaDiaDelMes;
            picSiguienteProyecto.Enabled = false;
            picAnteriorProyecto.Enabled = false;
            picLogoProyecto.EditValue = null;
            picImagenProyecto.EditValue = null;
            picImagenProyecto2.EditValue = null;
        }   

        private void Editar()
        {
            eProyecto ePro = new eProyecto();
            ePro = unit.Proyectos.ObtenerProyecto<eProyecto>("2", cod_proyecto);

            txtCodProyecto.Text = ePro.cod_proyecto;
            txtNombre.Text = ePro.dsc_nombre;
            mmDescripcion.Text = ePro.dsc_descripcion;
            lkpPais.EditValue = ePro.cod_pais;
            txtJefeProyecto.Text = ePro.dsc_jefe_proyecto;
            txtArquitecto.Text = ePro.dsc_arquitecto;
            lkpMoneda.EditValue = ePro.cod_moneda;
            txtValorCompra.Text = ePro.imp_precio_terreno.ToString();
            txtAlcabala.Text = ePro.imp_alcabala.ToString();
            txtOtrosGastos.Text = ePro.imp_otros_gastos.ToString();
            txtInversionInicial.Text = ePro.imp_invercion_inicial.ToString();
            txtAreaTotalProyecto.Text = ePro.num_total_suma_metros.ToString();
            txtPorcUsoExc.Text = ePro.prc_total_area_exclusiva.ToString();
            txtPorcUsoCom.Text = ePro.prc_total_area_comun.ToString();
            txtTotalUsoExc.Text = ePro.num_total_area_uex.ToString();
            txtTotalUsoCom.Text = ePro.num_total_area_uco.ToString();
            //mmPlanVias.Text = ePro.dsc_plan_vias;
            //txtNombreLegalPro.Text = ePro.dsc_Nombre_Legal_Pro;
            //txtRazonSocial.Text = ePro.dsc_razoc;
            //mmDireccion.Text = ePro.dsc_direccion;
            txtUsuarioRegistro.Text = ePro.cod_usuario_registro;
            if (ePro.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = ePro.fch_registro; }
            txtUsuarioCambio.Text = ePro.cod_usuario_cambio;
            if (ePro.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = ePro.fch_cambio; }
            if (ePro.fch_inicio.Year == 1) { dtFechaInicio.EditValue = null; } else { dtFechaInicio.EditValue = ePro.fch_inicio; }
            if (ePro.fch_termino.Year == 1) { dtFechaTermino.EditValue = null; } else { dtFechaTermino.EditValue = ePro.fch_termino; }
            if (ePro.fch_entrega.Year == 1) { dtFechaEntrega.EditValue = null; } else { dtFechaEntrega.EditValue = ePro.fch_entrega; }
            chkEstado.Checked = ePro.flg_activo == "SI" ? true : false;
            //glkpTipoDocumento.EditValue = ePro.cod_tipo_documento;
            glkpEmpresa.EditValue = ePro.cod_empresa;
            glkpEmpresa.Enabled = false;
            btnNuevo.Enabled = true;
            xtraTabEtapaTipLot.PageEnabled = true;
            xtraTabPage2.PageEnabled = true;
            picSiguienteProyecto.Enabled = true;
            picAnteriorProyecto.Enabled = true;
            List<eProyecto> eProImagen = new List<eProyecto>();
            eProImagen = unit.Proyectos.ObtenerProyectoImagenes<eProyecto>("1", cod_proyecto);

            if (eProImagen != null)
            {
                foreach (eProyecto obj in eProImagen)
                {
                    if (obj == null) continue;
                    if (obj.cod_imagenes == "00001") picLogoProyecto.EditValue = convertirBytes(eProImagen[0].dsc_base64_imagen);
                    if (obj.cod_imagenes == "00002") picImagenProyecto.EditValue = convertirBytes(eProImagen[1].dsc_base64_imagen);
                    if (obj.cod_imagenes == "00003") picImagenProyecto2.EditValue = convertirBytes(eProImagen[2].dsc_base64_imagen);
                }

                //byte[] byteBuffer = Convert.FromBase64String(eProImagen[0].dsc_base64_imagen); // esto convertiria lo de la base de datos para mostrar

                //MemoryStream memoryStream = new MemoryStream(byteBuffer);
                //memoryStream.Position = 0;
                //Image img = (Bitmap)Bitmap.FromStream(memoryStream);




            }
            //if (ePro.cod_proyecto == "00001")
            //{
             //   Image imgProyectoLogo = Properties.Resources.LogoCalifornia;
            //    picLogoProyecto.EditValue = imgProyectoLogo;
            //    Image imgProyectoImagen = Properties.Resources.TerrenoCalifornia;
            //    picImagenProyecto.EditValue = imgProyectoImagen;
            //    Image imgProyectoImagen2 = Properties.Resources.TerrenoCaliforniaLotes;
            //    picImagenProyecto2.EditValue = imgProyectoImagen2;
            //}
            //else if (eTrab.flg_sexo == "M")
            //{
            //    Image imgEmpresaLarge = Properties.Resources.Male64;
            //    picTrabajador.EditValue = imgEmpresaLarge;
            //}


        }

        private Bitmap convertirBytes(string base64_imagen)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64_imagen); // esto convertiria lo de la base de datos para mostrar

            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            return (Bitmap)Bitmap.FromStream(memoryStream);
        }


        //private void CargarTreeListCuatroNodos()
        //{
        //    var listadoEstado = unit.Proyectos.ListarTreeMemoriaDes<eMemoDes>(cod_proyecto);
        //    if (listadoEstado != null && listadoEstado.Count > 0)
        //    {
        //        new Tools.TreeListHelper(treeListMemoriaDes).
        //            TreeViewParaCuatroNodos<eMemoDes>(
        //            listadoEstado, "cod_memoria", "dsc_memoria",
        //            "cod_nodo1", "dsc_nodo1",
        //            "cod_nodo2", "dsc_nodo2", "cod_nodo3", "dsc_nodo3");

        //        treeListMemoriaDes.Refresh();

        //        //treeListProyectos.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
        //        //treeListProyectos.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;


        //        treeListMemoriaDes.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Radio;
        //        for (int i = 0; i < treeListMemoriaDes.Nodes.Count; i++)
        //        {
        //            treeListMemoriaDes.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
        //            for (int j = 0; j < treeListMemoriaDes.Nodes[i].Nodes.Count(); j++)
        //            {
        //                treeListMemoriaDes.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
        //                //final
        //                for (int k = 0; k < treeListMemoriaDes.Nodes[i].Nodes[j].Nodes.Count(); k++)
        //                {
        //                    treeListMemoriaDes.Nodes[i].Nodes[j].Nodes[k].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
        //                }

        //            }
        //        }
        //        treeListMemoriaDes.CheckAll();

        //    }
        //}

        private void glkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            eEmpresa eEmp = new eEmpresa();
            eEmp = unit.Proyectos.ObtenerListadoEmpresaSeleccionada<eEmpresa>("2", "", glkpEmpresa.EditValue.ToString());
            txtApoderado.Text = eEmp.dsc_apoderado;
            txtDocumento.Text = eEmp.dsc_ruc;
            txtPartidaRegistral.Text = eEmp.dsc_partida_registral;
            mmDireccion.Text = eEmp.dsc_direccion;
        }


        private eProyecto_Etapa AsignarValoresProyectoEtapa()
        {
            eProyecto_Etapa eProEta = new eProyecto_Etapa();
            eProEta.cod_etapa = txtCodigoEtapa.Text;
            eProEta.cod_empresa = glkpEmpresa.EditValue.ToString();
            eProEta.cod_proyecto = txtCodProyecto.Text;
            eProEta.dsc_descripcion = mmDescripcionEtapa.Text;
            eProEta.num_area_uex = Convert.ToDecimal(txtAreaUsoExcluEtapa.Text);
            eProEta.num_area_uco = Convert.ToDecimal(txtAreaUsoComEtapa.Text);
            eProEta.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eProEta.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            return eProEta;
        }

        private eProyecto AsignarValoresMedidasProyecto()
        {
            eProyecto eProMedidasProy = new eProyecto();
            eProMedidasProy.cod_empresa = glkpEmpresa.EditValue.ToString();
            eProMedidasProy.cod_proyecto = txtCodProyecto.Text;
            eProMedidasProy.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            return eProMedidasProy;
        }

        private eProyecto AsignarValoresProyectoImagenes(string dsc_numImg = "")
        {
            eProyecto ePro = new eProyecto();
            ePro.cod_imagenes = dsc_numImg == "1" ? "00001" : dsc_numImg == "2" ? "00002" : "00003";
            ePro.cod_empresa = glkpEmpresa.EditValue.ToString();
            ePro.cod_proyecto = txtCodProyecto.Text;
            ePro.dsc_nombre = dsc_numImg == "1" ? "Logo" : dsc_numImg == "2" ? "imagen referencial del proyecto 1" : "imagen referencial del proyecto 2";
            ePro.dsc_base64_imagen = dsc_numImg == "1" ? base64Imagen1 : dsc_numImg == "2" ? base64Imagen2 : base64Imagen3;
            ePro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            ePro.flg_activo = "SI";
            return ePro;
        }

        private eProyecto AsignarValoresProyecto()

        {
            eProyecto ePro = new eProyecto();
            ePro.cod_empresa = glkpEmpresa.EditValue.ToString();
            ePro.cod_proyecto = txtCodProyecto.Text;
            ePro.dsc_nombre = txtNombre.Text;
            //ePro.dsc_Nombre_Legal_Pro = txtNombreLegalPro.Text;
            ePro.dsc_descripcion = mmDescripcion.Text;
            //ePro.dsc_plan_vias = mmPlanVias.Text;
            ePro.cod_pais = lkpPais.EditValue.ToString();
            ePro.dsc_jefe_proyecto = txtJefeProyecto.Text;
            ePro.dsc_arquitecto = txtArquitecto.Text;
            ePro.cod_moneda = lkpMoneda.EditValue.ToString();
            //ePro.dsc_razoc = txtRazonSocial.Text;
            ePro.imp_precio_terreno = Convert.ToDecimal(txtValorCompra.Text);
            ePro.imp_alcabala = Convert.ToDecimal(txtAlcabala.Text);
            ePro.imp_otros_gastos = Convert.ToDecimal(txtOtrosGastos.Text);
            ePro.imp_invercion_inicial = Convert.ToDecimal(txtInversionInicial.Text);
            ePro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            ePro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            ePro.fch_inicio = dtFechaInicio.DateTime;
            ePro.fch_termino = dtFechaTermino.DateTime;
            ePro.fch_entrega = dtFechaEntrega.DateTime;
            ePro.flg_activo = chkEstado.CheckState == CheckState.Checked ? "SI" : "NO";
            ePro.bi_imagen = archivoBytes;
            //ePro.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();


            return ePro;
        }

        private void calcularTotal()
        {
            decimal total = 0;
            total = Convert.ToDecimal(txtValorCompra.Text) + Convert.ToDecimal(txtAlcabala.Text) + Convert.ToDecimal(txtOtrosGastos.Text);
            txtInversionInicial.Text = total.ToString();
        }

        private void btnGuardarEtapa_Click(object sender, EventArgs e)
        {
            string mensaje = validarCamposEtapa();
            if (mensaje == null)
            {
                try
                {
                    string result = "";
                    switch (MiAccionEtapa)
                    {
                        case Proyecto.Nuevo: result = GuardarEtapa(); break;
                        case Proyecto.Editar: result = ModificarEtapa(); break;
                    }

                    if (result == "OK")
                    {
                        XtraMessageBox.Show("Se guardó la etapa de manera satisfactoria", "Guardar Etapa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarListadoEtapas("1");
                        ModificarMedidasProyecto();

                        if (MiAccionEtapa == Proyecto.Nuevo)
                        {
                            CargarListadoLote("TODOS", "");

                            MiAccionEtapa = Proyecto.Editar;

                            //eProyecto_Etapa eProEta = new eProyecto_Etapa();
                            //ePro = unit.Proyectos.ObtenerProyecto<eProyecto>("2", cod_proyecto);
                            //txtUsuarioRegistro.Text = ePro.cod_usuario_registro;
                            //if (ePro.fch_registro.Year == 1) { dtFechaRegistro.EditValue = null; } else { dtFechaRegistro.EditValue = ePro.fch_registro; }
                            //txtUsuarioCambio.Text = ePro.cod_usuario_cambio;
                            //if (ePro.fch_cambio.Year == 1) { dtFechaModificacion.EditValue = null; } else { dtFechaModificacion.EditValue = ePro.fch_cambio; }
                        }
                        gvListaEtapas.FocusedRowHandle = gvListaEtapas.RowCount - 1;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                XtraMessageBox.Show(mensaje, "Guardar proyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gvListaEtapas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaEtapas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }



        private void rchkSeleccionado_CheckStateChanged(object sender, EventArgs e)
        {
            gvTipoLote.PostEditor();
        }

        private void gvTipoLote_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvTipoLote_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
            xtraTabEtapaTipLot.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            LimpiarCamposProyecto();
            bsEtapaBindingSource.DataSource = null;
            //btnNuevoEtapa.PerformClick();
            LimpiarCamposProyectoEtapa();
        }

        private void btnNuevoEtapa_Click(object sender, EventArgs e)
        {
            LimpiarCamposProyectoEtapa();
        }

        private void btnFchIni_Click(object sender, EventArgs e)
        {
            dtFechaInicio.ShowPopup();
        }

        private void btnFchTer_Click(object sender, EventArgs e)
        {
            dtFechaTermino.ShowPopup();
        }

        private void btnFchEnt_Click(object sender, EventArgs e)
        {
            dtFechaEntrega.ShowPopup();
        }

        private void dtFechaTermino_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaTermino.EditValue) < Convert.ToDateTime(dtFechaInicio.EditValue)) dtFechaTermino.EditValue = Convert.ToDateTime(dtFechaInicio.EditValue);

        }

        private void dtFechaEntrega_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaEntrega.EditValue) < Convert.ToDateTime(dtFechaTermino.EditValue)) dtFechaEntrega.EditValue = Convert.ToDateTime(dtFechaTermino.EditValue);
        }
        private void gvTipoLote_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                int nRow = e.RowHandle;
                eVariablesGenerales eVarGen = new eVariablesGenerales();
                eVariablesGenerales obj = gvTipoLote.GetRow(e.RowHandle) as eVariablesGenerales;
                if (e.Column.FieldName == "Seleccionado")
                {
                    eVariablesGenerales eVaGe = gvTipoLote.GetFocusedRow() as eVariablesGenerales;

                    string validateResult = CargarConfigLotesTipoLotes("ANULAR TIPO LOTE", obj);
                    if (validateResult != null)
                    {
                        MessageBox.Show(validateResult, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        gvTipoLote.RefreshData();
                        CargarListadoLote("TODOS", "");
                        obtenerListadoTipoLoteXEtapa();
                        return;
                    }
                    obj.cod_etapa = cod_etapa;
                    obj.cod_empresa = glkpEmpresa.EditValue.ToString();
                    obj.cod_proyecto = cod_proyecto;
                    obj.flg_activo = obj.Seleccionado ? "SI" : "NO";
                    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eVarGen = unit.Proyectos.Guardar_Actualizar_EtapasTipoLote<eVariablesGenerales>(obj);

                    if (eVarGen == null)
                    { MessageBox.Show("Error al vincular el tipo de lote", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    ActualizarListadoTipoLote = "SI";
                }

                if (e.Column.FieldName == "dsc_Nombre" && e.Value != null)
                {
                    coldsc_Nombre.OptionsColumn.AllowEdit = false;
                    cod_tipoLote = "";

                    //if (obj.dsc_Nombre == "" || obj.dsc_Nombre == null)
                    //{
                    //    MessageBox.Show("Descripción tipo de lote inválido", "Tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                    //    gvTipoLote.RefreshData();
                    //    return;
                    //}

                    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eVarGen = unit.Proyectos.Mantenimiento_TipoLote<eVariablesGenerales>(obj);
                    if (eVarGen == null) { MessageBox.Show("Error al agregar el tipo de lote", "Tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    obj.cod_variable = eVarGen.cod_variable;
                    obj.cod_etapa = cod_etapa;
                    obj.cod_empresa = glkpEmpresa.EditValue.ToString();
                    obj.cod_proyecto = cod_proyecto;
                    obj.flg_activo = obj.Seleccionado ? "SI" : "NO";
                    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                    eVarGen = unit.Proyectos.Guardar_Actualizar_EtapasTipoLote<eVariablesGenerales>(obj);

                    gvTipoLote.RefreshData();
                    if (eVarGen == null)

                    { MessageBox.Show("Error al vincular el tipo de lote", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    ActualizarListadoTipoLote = "SI";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void limpiarManzanasXEtapas()
        {
            txtHasta.Text = "";
            txtDesde.Text = "";
        }

        private void textEdit1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (txtHasta.Text.Length > 0)
            {
                txtHasta.Text = txtHasta.Text.Substring(0, txtHasta.Text.Count() - 1);
            }
        }

        private void textEdit1_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {

            if (txtDesde.Text.Length > 0)
            {
                txtDesde.Text = txtDesde.Text.Substring(0, txtDesde.Text.Count() - 1);
            }
        }

        private void btnListarMnz_Click(object sender, EventArgs e)
        {
            CargarListadoManzanas("1", txtDesde.Text, txtHasta.Text);
        }

        private void gvManzana_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvManzana_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private eLotesxProyecto AsignarLotesXProyecto(string dsc_lote = "", string dsc_manzana = "", int num_lote = 0)
        {
            //"Codigo Lote: " + objEtaMz.dsc_manzana + i + " Manzana: " + objEtaMz.dsc_manzana + " Lote: " + i + " Etapa: " + num_linea
            eLotesxProyecto eLoPro = new eLotesxProyecto();
            eLoPro.cod_lote = "";
            eLoPro.cod_status = "";
            eLoPro.cod_manzana = cod_manzana;
            eLoPro.cod_etapa = cod_etapa;
            eLoPro.cod_empresa = glkpEmpresa.EditValue.ToString();
            eLoPro.cod_proyecto = cod_proyecto;
            //eLoPro.cod_tipo_lote = Convert.ToDecimal(txtValorCompra.Text);
            eLoPro.dsc_manzana = dsc_manzana;
            eLoPro.dsc_lote = dsc_lote;
            eLoPro.num_etapa = num_linea;
            eLoPro.num_lote = num_lote;
            //eLoPro.num_area_uex = txtJefeProyecto.Text;
            //eLoPro.num_area_uco = txtArquitecto.Text;
            //eLoPro.num_frente = lkpMoneda.EditValue.ToString();
            //eLoPro.num_derecha = Convert.ToDecimal(txtValorCompra.Text);
            //eLoPro.num_izquierda = Program.Sesion.Usuario.cod_usuario;
            //eLoPro.num_fondo = Program.Sesion.Usuario.cod_usuario;
            //eLoPro.imp_prec_m_cuadrado = dtFechaInicio.DateTime;
            //eLoPro.imp_precio_total = Convert.ToDecimal(txtValorCompra.Text);
            eLoPro.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eLoPro.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;


            return eLoPro;
        }

        private string GuardarLotesXProyecto(eLotesxProyecto eLoPro)
        {

            //ePro = unit.Proyectos.Mantenimiento_Proyecto<eProyecto>(ePro);
            //if (ePro != null)
            //{
            //    cod_proyecto = ePro.cod_proyecto;
            //    txtCodProyecto.Text = cod_proyecto;
            //    return "OK";
            //}
            return null;
        }

        private void gvManzana_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            int nRow = e.RowHandle;
            eProyectoEtapaManzana eEtaMz = new eProyectoEtapaManzana();
            eProyectoEtapaManzana objEtaMz = new eProyectoEtapaManzana();
            eVariablesGenerales obj = gvManzana.GetRow(e.RowHandle) as eVariablesGenerales;

            if (e.Column.FieldName == "valor_3" && e.Value != null)

            {

                if (Convert.ToDecimal(e.Value) == 0)
                {
                    MessageBox.Show("El valor no puede ser 0", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    obtenerListadoManzanaXEtapa();
                    gvManzana.RefreshData();

                    return;
                }

                if (Convert.ToDecimal(e.Value) < cod_manzana_hasta)
                {
                    cod_manzana = obj.cod_variable;
                    cod_manzana_desde = Convert.ToDecimal(e.Value);
                    string mensajeResultado = ObtenerConfigLotesManzana();
                    if (mensajeResultado != null)
                    {
                        MessageBox.Show(mensajeResultado, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        obtenerListadoManzanaXEtapa();
                        gvManzana.RefreshData();
                        return;
                    }
                    else
                    {
                        DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta manzana? \nEsta acción es irreversible.", "Eliminar manzana", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (msgresult == DialogResult.Yes)
                        {
                            string result = unit.Proyectos.Eliminar_EtapaManzana("2", obj.cod_variable, cod_proyecto, cod_etapa, cod_manzana, cod_manzana_desde.ToString(), cod_manzana_hasta.ToString());
                            if (result == null)
                            {
                                MessageBox.Show("No se pudo eliminar la manzana " + result, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                obtenerListadoManzanaXEtapa();
                                gvManzana.RefreshData();
                                return;
                            }
                        }
                        else
                        {
                            obtenerListadoManzanaXEtapa();
                            gvManzana.RefreshData();
                            return;
                        }


                    }
                }

                objEtaMz.cod_etapa = cod_etapa;
                objEtaMz.cod_empresa = glkpEmpresa.EditValue.ToString();
                objEtaMz.cod_proyecto = cod_proyecto;
                objEtaMz.cod_manzana = obj.cod_variable;
                cod_manzana = obj.cod_variable;
                objEtaMz.dsc_manzana = obj.dsc_Nombre;
                objEtaMz.num_desde = Convert.ToInt32(obj.valor_1);
                objEtaMz.num_hasta = Convert.ToInt32(obj.valor_3);
                objEtaMz.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                objEtaMz.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
                eEtaMz = unit.Proyectos.Guardar_Actualizar_EtapasManzana<eProyectoEtapaManzana>(objEtaMz);

                if (eEtaMz == null)
                { MessageBox.Show("Error al vincular la manzana", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                foreach (eVariablesGenerales obje in ListEtapaXmanzana)
                {
                    if (obj.cod_variable == obje.cod_variable)
                    {
                        obj.flg_activo = "SI";
                    }

                }
                gvManzana.RefreshData();

                List<eLotesxProyecto> lista = unit.Proyectos.ListarManzanaxEtapas<eLotesxProyecto>("2", cod_etapa, cod_manzana, cod_proyecto);
                //ListEtapaXmanzana = lista;

                if (lista.Count > 0)
                {
                    for (int i = 1; i < objEtaMz.num_hasta + 1; i++)
                    {
                        eLotesxProyecto objM = lista.Find(x => x.cod_manzana == objEtaMz.cod_manzana && x.num_lote == i);
                        if (objM == null)
                        {
                            eLotesxProyecto eLoPro = AsignarLotesXProyecto(objEtaMz.dsc_manzana + i, objEtaMz.dsc_manzana, i);

                            eLoPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("", eLoPro);
                            if (eLoPro == null)
                            {
                                MessageBox.Show("Error al vincular la manzana", "Vincular tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                            }
                        }
                    }
                    gvManzana.RefreshData();
                    //foreach (eVariablesGenerales objLista in lista)
                    //{
                    //    eVariablesGenerales objM = ListEtapaXmanzana.Find(x => x.cod_variable == obj.cod_variable);
                    //    if (objM == null) ListEtapaXmanzana.Add(obj);
                    //}
                    //gvManzana.RefreshData();


                }
                else
                {
                    for (int i = 1; i < objEtaMz.num_hasta + 1; i++)
                    {

                        eLotesxProyecto eLoPro = AsignarLotesXProyecto(objEtaMz.dsc_manzana + i, objEtaMz.dsc_manzana, i);

                        eLoPro = unit.Proyectos.MantenimientoLotesXProyecto<eLotesxProyecto>("", eLoPro);
                        //if (eLoPro != null)
                        //{
                        //cod_proyecto = ePro.cod_proyecto;
                        //txtCodProyecto.Text = cod_proyecto;
                        //return "OK";
                        //MessageBox.Show("Codigo Lote: " +objEtaMz.dsc_manzana + i +  " Manzana: "+ objEtaMz.dsc_manzana+ " Lote: "+ i );
                        //}

                    }
                    gvManzana.RefreshData();
                }



                limpiarManzanasXEtapas();
                return;
                ActualizarListadoTipoLote = "SI";


            }
        }

        private void rbtnEliminarManzana_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                eVariablesGenerales eVaGe = gvManzana.GetFocusedRow() as eVariablesGenerales;
                cod_manzana = eVaGe.cod_variable;

                string validateResult = CargarConfigLotesManzana();
                if (validateResult != null)
                {
                    MessageBox.Show(validateResult, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta manzana? \nEsta acción es irreversible.", "Eliminar manzana", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {



                    string result = unit.Proyectos.Eliminar_EtapaManzana("1", eVaGe.cod_variable, cod_proyecto, cod_etapa, cod_manzana, eVaGe.valor_1, eVaGe.valor_3);
                    if (result == null)
                    {
                        MessageBox.Show("No se pudo eliminar la manzana " + result, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    gvManzana.RefreshData();
                    obtenerListadoManzanaXEtapa();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string CargarConfigLotesManzana()
        {
            try
            {

                List<eLotesxProyecto> ListConfLote = new List<eLotesxProyecto>();
                ListConfLote = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>("3", cod_proyecto, cod_etapa, cod_manzana);

                //foreach (eLotesxProyecto lst in ListConfLote)
                //{
                eLotesxProyecto obje = ListConfLote.Find(x => x.num_area_uco > 0 || x.num_area_uex > 0);
                if (obje != null)
                {
                    return "ERROR AL ELIMINAR, LOS LOTES CONFIGURADOS";
                }
                //}
                return null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }

        public string ObtenerConfigLotesManzana() //Consultaremos si existe configuracion de manzanas antes de eliminarlas
        {
            try
            {

                List<eLotesxProyecto> ListConfLote = new List<eLotesxProyecto>();
                ListConfLote = unit.Proyectos.obtenerConfLotesXManzana<eLotesxProyecto>(cod_proyecto, cod_etapa, cod_manzana, cod_manzana_desde + 1, cod_manzana_hasta);

                //foreach (eLotesxProyecto lst in ListConfLote)
                //{
                eLotesxProyecto obje = ListConfLote.Find(x => x.num_area_uco > 0 || x.num_area_uex > 0);
                if (obje != null)
                {
                    return "ERROR AL INGRESAR UN VALOR MENOR A " + cod_manzana_hasta + " LOTES CONFIGURADOS";
                }
                //}
                return null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }

        public string CargarConfigEtapas(string numAccion)
        {
            try
            {

                List<eProyecto_Etapa> ListConfEtapa = new List<eProyecto_Etapa>();
                ListConfEtapa = unit.Proyectos.ListarConfEtapaProyecto<eProyecto_Etapa>(numAccion, cod_etapa);

                //foreach (eLotesxProyecto lst in ListConfLote)
                //{
                //eProyecto_Etapa obje = ListConfLote.Find(x => x.cod_status != "STL00001");
                if (ListConfEtapa.Count > 0)
                {
                    eProyecto_Etapa obje = ListConfEtapa[0];
                    return obje.accion;
                }
                //}
                return null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        public string CargarConfigLotesTipoLotes(string accion, eVariablesGenerales eVaGe)
        {
            try
            {
                if (accion == "ELIMINAR TIPO LOTE")
                {
                    List<eLotesxProyecto> ListConfLote = new List<eLotesxProyecto>();
                    ListConfLote = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>("6");

                    eLotesxProyecto obje = ListConfLote.Find(x => x.cod_tipo_lote == eVaGe.cod_variable);
                    if (obje != null)
                    {
                        return "ERROR AL ELIMINAR, EL TIPO DE LOTE \"" + eVaGe.dsc_Nombre + "\" SE ENCUENTRA CONFIGURADO EN EL PROYECTO";
                    }
                }
                if (accion == "ANULAR TIPO LOTE")
                {
                    List<eLotesxProyecto> ListConfLote = new List<eLotesxProyecto>();
                    ListConfLote = unit.Proyectos.ListarConfLotesProy<eLotesxProyecto>("7", cod_proyecto, cod_etapa, cod_manzana, eVaGe.cod_variable);


                    eLotesxProyecto obje = ListConfLote.Find(x => x.cod_tipo_lote == eVaGe.cod_variable);
                    if (obje != null)
                    {
                        return "ERROR AL QUITAR, EL TIPO DE LOTE \"" + eVaGe.dsc_Nombre + "\" SE ENCUENTRA CONFIGURADO EN LA ETAPA DEL PROYECTO";
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



        //private void btnConfTipoLote_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{            
        //    try
        //    {
        //        frmConfLoteProyecto frm = new frmConfLoteProyecto();
        //        if (Application.OpenForms["frmConfLoteProyecto"] != null)
        //        {
        //            Application.OpenForms["frmConfLoteProyecto"].Activate();
        //        }
        //        else
        //        {
        //            frm.cod_etapa = cod_etapa;
        //            frm.cod_proyecto = cod_proyecto;
        //            frm.MiAccion = ControlLotesProyecto.Editar;
        //            
        //            
        //            
        //            
        //            frm.user = user;
        //            frm.ShowDialog();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void gvTipoLote_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Button == MouseButtons.Right) popupMenu1.ShowPopup(MousePosition);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgregarTipoLote();
        }

        private void gvTipoLote_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {


                    eVariablesGenerales obj = gvTipoLote.GetFocusedRow() as eVariablesGenerales;
                    if (obj.cod_variable == cod_tipoLote)
                    {
                        coldsc_Nombre.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        coldsc_Nombre.OptionsColumn.AllowEdit = false;
                        cod_tipoLote = "";
                    }


                    gvTipoLote.RefreshData();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbtnEditarTipoLote_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            eVariablesGenerales eVaGe = gvTipoLote.GetFocusedRow() as eVariablesGenerales;
            cod_tipoLote = eVaGe.cod_variable;
            coldsc_Nombre.OptionsColumn.AllowEdit = true;
        }

        private void rbtnEliminarTipoLote_Click(object sender, EventArgs e)
        {
            eVariablesGenerales eVaGe = gvTipoLote.GetFocusedRow() as eVariablesGenerales;

            string validateResult = CargarConfigLotesTipoLotes("ELIMINAR TIPO LOTE", eVaGe);
            if (validateResult != null)
            {
                MessageBox.Show(validateResult, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este tipo de lote? \nEsta acción es irreversible.", "Eliminar tipo de lote", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                string result = unit.Proyectos.Eliminar_EtapaTipoLote(eVaGe.cod_variable, cod_proyecto, cod_etapa);
                gvTipoLote.RefreshData();
                CargarListadoLote("TODOS", "");
                obtenerListadoTipoLoteXEtapa();

            }

        }

        private void gvListaEtapas_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {

                    eProyecto_Etapa obj = gvListaEtapas.GetFocusedRow() as eProyecto_Etapa;
                    cod_etapa = obj.cod_etapa;
                    num_linea = Convert.ToInt32(obj.num_linea);
                    txtCodigoEtapa.Text = cod_etapa;
                    mmDescripcionEtapa.Text = obj.dsc_descripcion;
                    txtAreaUsoExcluEtapa.Text = obj.num_area_uex.ToString();
                    txtAreaUsoComEtapa.Text = obj.num_area_uco.ToString();

                    CargarListadoLote("TODOS", "");
                    obtenerListadoTipoLoteXEtapa();
                    obtenerListadoManzanaXEtapa();
                    xtraTabTipoLote.PageEnabled = true;
                    xtraTabManzana.PageEnabled = true;
                    btnConfTipoLote.Enabled = true;
                    limpiarManzanasXEtapas();
                    if (frmHandler != null)
                    {
                        frmHandler.CargarListadoStatusXEtapas("5", obj.cod_etapa.ToString());
                    }

                    MiAccionEtapa = Proyecto.Editar;
                    if (MiAccion != Proyecto.Vista) { btnNuevoEtapa.Enabled = true; }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvTipoLote_Click(object sender, EventArgs e)
        {
            eVariablesGenerales oLoteEtap = ListTipolote.Find(x => x.cod_variable == "0" && x.dsc_Nombre == "" || x.dsc_Nombre == null);
            if (oLoteEtap != null)
            {
                ListTipolote.Remove(oLoteEtap);
                gvTipoLote.RefreshData();
            }
        }

        private void gvManzana_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eVariablesGenerales obj = gvManzana.GetFocusedRow() as eVariablesGenerales;
                    cod_manzana_desde = Convert.ToDecimal(obj.valor_1);
                    cod_manzana_hasta = Convert.ToDecimal(obj.valor_3);
                    if (obj.cod_variable == cod_manzana_validar)
                    {
                        colvalor_1.OptionsColumn.AllowEdit = true;
                        colvalor_3.OptionsColumn.AllowEdit = true;

                    }
                    else
                    {
                        colvalor_1.OptionsColumn.AllowEdit = false;
                        colvalor_3.OptionsColumn.AllowEdit = false;
                        cod_manzana_validar = "";
                    }

                    gvManzana.RefreshData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbtnEditarManzana_Click(object sender, EventArgs e)
        {
            eVariablesGenerales eVaGe = gvTipoLote.GetFocusedRow() as eVariablesGenerales;
            cod_manzana_validar = eVaGe.cod_variable;
            colvalor_1.OptionsColumn.AllowEdit = true;
            colvalor_3.OptionsColumn.AllowEdit = true;
        }

        private void btnNuevoTipoLotePorEtapa_Click(object sender, EventArgs e)
        {
            AgregarTipoLote();
        }

        private void btnEliminarEtapa_Click(object sender, EventArgs e)
        {
            eProyecto_Etapa eProEta = gvListaEtapas.GetFocusedRow() as eProyecto_Etapa;
            cod_etapa = eProEta.cod_etapa;
            string validateResultLote = CargarConfigEtapas("1");
            if (validateResultLote != null)
            {
                MessageBox.Show(validateResultLote, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string validateResultManzana = CargarConfigEtapas("2");
            if (validateResultManzana != null)
            {
                MessageBox.Show(validateResultManzana, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar la estapa \nEsta acción es irreversible." + eProEta.dsc_descripcion + " ?", "Eliminar etapa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {


                string result = unit.Proyectos.Eliminar_EtapaProyecto(cod_proyecto, cod_etapa);
                gvListaEtapas.RefreshData();
                CargarListadoEtapas("1");

            }
        }

        private void frmMantProyecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void picAnteriorProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaProyectos.RowCount - 1;
                int nRow = frmHandler.gvListaProyectos.FocusedRowHandle;
                frmHandler.gvListaProyectos.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                eProyecto obj = frmHandler.gvListaProyectos.GetFocusedRow() as eProyecto;
                cod_proyecto = obj.cod_proyecto;
                MiAccion = Proyecto.Editar;
                Nuevo();
                CargarListadoEtapas("1");
                Editar();
                CargarListadoMemoriaDesc("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListaProyectos.RowCount - 1;
                int nRow = frmHandler.gvListaProyectos.FocusedRowHandle;
                frmHandler.gvListaProyectos.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                eProyecto obj = frmHandler.gvListaProyectos.GetFocusedRow() as eProyecto;
                cod_proyecto = obj.cod_proyecto;
                MiAccion = Proyecto.Editar;
                Nuevo();
                CargarListadoEtapas("1");
                Editar();
                CargarListadoMemoriaDesc("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string validarCampos()
        {
            if (txtNombre.Text.Trim() == "")
            {
                txtNombre.Focus();
                return "Debe ingresar el nombre del proyecto";
            }
            if (mmDescripcion.Text.Trim() == "")
            {
                mmDescripcion.Focus();
                return "Debe ingresar la descripción";
            }
            if (txtJefeProyecto.Text.Trim() == "")
            {
                txtJefeProyecto.Focus();
                return "Debe ingresar el jefe de proyecto";
            }
            if (txtArquitecto.Text.Trim() == "")
            {
                txtArquitecto.Focus();
                return "Debe ingresar el Arquitecto";
            }

            if (txtValorCompra.Text.Trim() == "")
            {
                txtValorCompra.Focus();
                return "Debe ingresar el Valor de Compra del Terreno";
            }
            if (dtFechaInicio.EditValue == null)
            {
                dtFechaInicio.Focus();
                return "Debe ingresar la fecha de inicio";
            }
            if (dtFechaTermino.EditValue == null)
            {
                dtFechaTermino.Focus();
                return "Debe ingresar la fecha de termino";
            }
            if (dtFechaEntrega.EditValue == null)
            {
                dtFechaEntrega.Focus();
                return "Debe ingresar la fecha de entrega";
            }

            return null;
        }

        public string validarCamposEtapa()
        {
            if (txtAreaUsoComEtapa.Text.Trim() == "")
            {
                txtAreaUsoComEtapa.Focus();
                return "Debe ingresar el área de uso común";
            }
            if (txtAreaUsoExcluEtapa.Text.Trim() == "")
            {
                txtAreaUsoExcluEtapa.Focus();
                return "Debe ingresar el área de uso exclusivo";
            }
            if (mmDescripcionEtapa.Text.Trim() == "")
            {
                mmDescripcionEtapa.Focus();
                return "Debe ingresar la descripción de la etapa";
            }
            if (txtCodProyecto.Text.Trim() == "")
            {
                txtCodProyecto.Focus();
                return "Debe ingresar el código del proyecto";
            }


            return null;
        }

        private void btnGuardarMemoriaDescriptiva_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                result = GuardarActualizarMemoriaDescriptiva();


                if (result == "OK")
                {
                    XtraMessageBox.Show("Se guardó la memoria descriptiva de manera satisfactoria", "Guardar Memoria Descriptiva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarListadoMemoriaDesc("1");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GuardarActualizarMemoriaDescriptiva()
        {
            eMemoDes eMemoria = gvMemoDesc.GetFocusedRow() as eMemoDes;
            eMemoria.cod_empresa = glkpEmpresa.EditValue.ToString();
            eMemoria.cod_proyecto = cod_proyecto;
            eMemoria.dsc_descripcion = rtxtDescripcionMemoriaDescriptiva.WordMLText;
            eMemoria.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eMemoria.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            eMemoria.dsc_descripcion_html = rtxtDescripcionMemoriaDescriptiva.HtmlText;
            eMemoria = unit.Proyectos.MantenimientoMemoriaDescriptiva<eMemoDes>(eMemoria);
            if (eMemoria != null)
            {
                return "OK";
            }
            return null;
        }

        private string InsertMemoriaDescriptivaNombreOrden()
        {
            eMemoDes eMemoria = gvMemoDesc.GetFocusedRow() as eMemoDes;
            eMemoria.cod_empresa = glkpEmpresa.EditValue.ToString();
            eMemoria.cod_proyecto = cod_proyecto;
            eMemoria.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eMemoria.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            eMemoria = unit.Proyectos.MantenimientoMemoriaDescriptivaOrdenNombre<eMemoDes>(eMemoria);

            if (eMemoria != null)
            {
                return "OK";
            }
            return null;
        }

        private void gvListaMemoriaDescriptiva_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //gvListaMemoriaDescriptiva_FocusedRowChanged(gvListaMemoriaDescriptiva, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(e.RowHandle - 1, e.RowHandle));
            //if (e.Clicks == 2 && e.RowHandle >= 0)
            //{
            //    eMemoDes oListMemoDesc = ListMemoDesc.Find(x => x.cod_memoria_desc == "0" && x.dsc_nombre == "" || x.dsc_nombre == null);
            //    if (oListMemoDesc != null)
            //    {
            //        ListMemoDesc.Remove(oListMemoDesc);
            //        gvListaMemoriaDescriptiva.RefreshData();
            //    }
            //}
        }

        private void gvListaMemoriaDescriptiva_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rtxtDescripcionMemoriaDescriptiva.WordMLText = "";
                rtxtDescripcionMemoriaDescriptiva.WordMLText = null;

                if (e.FocusedRowHandle >= 0)
                {
                    eMemoDes eMemoria = gvMemoDesc.GetRow(e.FocusedRowHandle) as eMemoDes;
                    if (eMemoria == null) { return; }
                    if (eMemoria != null && eMemoria.cod_memoria_desc != "0")
                    {
                        eMemoria = unit.Proyectos.ListarMemoriaDesc<eMemoDes>("2", cod_proyecto, eMemoria.cod_memoria_desc)[0];

                        //rtxtDescripcionMemoriaDescriptiva.HtmlText = eMemoria.dsc_descripcion_html;
                        rtxtDescripcionMemoriaDescriptiva.WordMLText = eMemoria.dsc_descripcion; //comentado solo para puebas
                    }
                    if (eMemoria.cod_memoria_desc == cod_memoria_desc)
                    {
                        coldsc_nombre2.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        coldsc_nombre2.OptionsColumn.AllowEdit = false;
                        cod_memoria_desc = "";
                    }


                    gvMemoDesc.RefreshData();

                }
                else
                {
                    rtxtDescripcionMemoriaDescriptiva.WordMLText = "";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerMemoriaDescriptiva_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
               

                var formato = new FormatoWordHelper();
                formato.ShowWordReportFormatoGeneral(glkpEmpresa.EditValue.ToString(), "00009");
             
                SplashScreenManager.CloseForm(false);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //try
            //{
            //    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
            //    //eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
            //    if (cod_proyecto == null) { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            //    rptVerMemoriaDescriptiva report = new rptVerMemoriaDescriptiva();
            //    ReportPrintTool printTool = new ReportPrintTool(report);
            //    report.RequestParameters = false;
            //    printTool.AutoShowParametersPanel = false;
            //    report.Parameters["cod_proyecto"].Value = cod_proyecto;
            //    report.ShowPreviewDialog();
            //    SplashScreenManager.CloseForm(false);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnVerPadronAreaUE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");

                var formato = new FormatoWordHelper();
                formato.ShowWordReportFormatoGeneral(glkpEmpresa.EditValue.ToString(), "00010");

                
                SplashScreenManager.CloseForm(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //try
            //{
            //    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
            //    //eLotesxProyecto ePro = gvListaLotesProyectos.GetFocusedRow() as eLotesxProyecto;
            //    if (cod_proyecto == null) { MessageBox.Show("Debe seleccionar proyecto.", "Ficha del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            //    rptFichaPadronArea report = new rptFichaPadronArea();
            //    ReportPrintTool printTool = new ReportPrintTool(report);
            //    //detalleLotes printTool = new detalleLotes(report);
            //    report.RequestParameters = false;
            //    printTool.AutoShowParametersPanel = false;
            //    report.Parameters["cod_proyecto"].Value = cod_proyecto;
            //    //report.BackColor = Color.FromArgb(0, 157, 150);
            //    report.ShowPreviewDialog();
            //    SplashScreenManager.CloseForm(false);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }



        private void gvListaMemoriaDescriptiva_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //if (e.RowHandle < 0) return;
                //int nRow = e.RowHandle;
                //eMemoDes obj = gvListaMemoriaDescriptiva.GetRow(e.RowHandle) as eMemoDes;
                //if (obj.num_orden == 0)
                //{
                //    MiAccionMemoria = Proyecto.Nuevo;
                //}
                //else
                //{
                //    MiAccionMemoria = Proyecto.Editar;

                //}


                //if (e.Column.FieldName == "col_nombre" && e.Value != null)
                //{
                //    coldsc_nombrememo.OptionsColumn.AllowEdit = false;
                //    cod_memoria_desc = "";

                //    if (obj.dsc_nombre == "" || obj.dsc_nombre == null)
                //    {
                //        MessageBox.Show("Descripción de la memoria inválido", "Memoria Descriptiva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        gvListaMemoriaDescriptiva.RefreshData();
                //        return;
                //    }


                //    string result = "";
                //    switch (MiAccionMemoria)
                //    {
                //        case Proyecto.Nuevo: result = GuardarMemoriaDescriptiva(); break;
                //        case Proyecto.Editar: result = ModificarMemoriaDescriptiva(); break;
                //    }

                //    if (result == "OK")
                //    {
                //        XtraMessageBox.Show("Se guardó la memoria descriptiva de manera satisfactoria", "Guardar Memoria Descriptiva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        CargarListadoMemoriaDesc("1");
                //        if (MiAccionEtapa == Proyecto.Nuevo)
                //        {
                //            MiAccionEtapa = Proyecto.Editar;
                //        }
                //    }
                //    //eVarGen = unit.Proyectos.Mantenimiento_TipoLote<eVariablesGenerales>(obj);
                //    //if (eVarGen == null) { MessageBox.Show("Error al agregar el tipo de lote", "Tipo lote", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }



                //    gvListaMemoriaDescriptiva.RefreshData();

                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void rbtnEditar_Click(object sender, EventArgs e)
        //{
        //    eMemoDes eVaGe = gvListaMemoriaDescriptiva.GetFocusedRow() as eMemoDes;
        //    cod_memoria_desc = eVaGe.cod_memoria_desc;
        //    coldsc_nombrememo.OptionsColumn.AllowEdit = true;
        //}

        //private void gvListaMemoriaDescriptiva_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        eMemoDes obj = gvListaMemoriaDescriptiva.GetFocusedRow() as eMemoDes;
        //        if (obj == null) return;
        //        if (gvListaMemoriaDescriptiva.FocusedColumn.FieldName == "dsc_nombre")
        //        {
        //            e.Cancel = obj.num_orden >= 1 ? true : false;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void rbtnEdiMEmo_Click(object sender, EventArgs e)
        {
            eMemoDes eVaGe = gvMemoDesc.GetFocusedRow() as eMemoDes;
            cod_memoria_desc = eVaGe.cod_memoria_desc;
            coldsc_nombre2.OptionsColumn.AllowEdit = true;
        }

        private void gvMemoDesc_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvMemoDesc_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvMemoDesc_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gvMemoDesc_Click(object sender, EventArgs e)
        {
            eMemoDes oListMemoDesc = ListMemoDesc.Find(x => x.cod_memoria_desc == "0" && x.dsc_nombre == "" || x.dsc_nombre == null);
            if (oListMemoDesc != null)
            {
                ListMemoDesc.Remove(oListMemoDesc);
                gvMemoDesc.RefreshData();
            }

        }

        private void gvMemoDesc_ShowingEditor(object sender, CancelEventArgs e)
        {
            //try PARA USARLO LAS COLUMNAS TIENEN QUE ESTAR EN EDITABLES
            //{
            //    eMemoDes obj = gvMemoDesc.GetFocusedRow() as eMemoDes;
            //    if (obj == null) return;
            //    if (gvMemoDesc.FocusedColumn.FieldName == "dsc_nombre")
            //    {
            //        e.Cancel = obj.cod_memoria_desc == "0" ? false : true;
            //    }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void gvMemoDesc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string result = "";
            result = InsertMemoriaDescriptivaNombreOrden();


            if (result == "OK")
            {
                XtraMessageBox.Show("Se guardó la memoria descriptiva de manera satisfactoria", "Guardar Memoria Descriptiva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListadoMemoriaDesc("1");
            }
        }

        private void rbtnElimMemo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            try
            {
                eMemoDes eVaGe = gvMemoDesc.GetFocusedRow() as eMemoDes;


                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta memoria descriptiva? \nEsta acción es irreversible.", "Eliminar memoria descriptiva", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {


                    string result = unit.Proyectos.Eliminar_Memoria_Desc(eVaGe.cod_memoria_desc, cod_proyecto);
                    if (result == null)
                    {
                        MessageBox.Show("No se pudo eliminar la memoria descriptiva " + result, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    gvMemoDesc.RefreshData();
                    CargarListadoMemoriaDesc("1");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarMemoriaDes_Click(object sender, EventArgs e)
        {
            try
            {
                eMemoDes eVaGe = gvMemoDesc.GetFocusedRow() as eMemoDes;


                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar esta memoria descriptiva? \nEsta acción es irreversible.", "Eliminar memoria descriptiva", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {


                    string result = unit.Proyectos.Eliminar_Memoria_Desc(eVaGe.cod_memoria_desc, cod_proyecto);
                    if (result == null)
                    {
                        MessageBox.Show("No se pudo eliminar la memoria descriptiva " + result, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    gvMemoDesc.RefreshData();
                    CargarListadoMemoriaDesc("1");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtValorCompra_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtValorCompra.Text) >= 0)
            {
                calcularTotal();

            }
        }

        private void txtAlcabala_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAlcabala.Text) >= 0)
            {
                calcularTotal();

            }
        }

        private void txtOtrosGastos_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtOtrosGastos.Text) >= 0)
            {
                calcularTotal();
            }
        }

        private void picImagenProyecto_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            var fs = File.OpenRead(e.FileName);
            var ms = new MemoryStream();

            fs.CopyTo(ms);
            var bytes = ms.ToArray();

            base64Imagen2 = Convert.ToBase64String(bytes);
            dsc_Imagen2 = e.FileName;
        }

        private void picImagenProyecto2_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            var fs = File.OpenRead(e.FileName);
            var ms = new MemoryStream();

            fs.CopyTo(ms);
            var bytes = ms.ToArray();

            base64Imagen3 = Convert.ToBase64String(bytes);
            dsc_Imagen3 = e.FileName;
        }

        private void picLogoProyecto_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            var fs = File.OpenRead(e.FileName);
            var ms = new MemoryStream();

            fs.CopyTo(ms);
            var bytes = ms.ToArray();

            base64Imagen1 = Convert.ToBase64String(bytes); // Guardar en la base de datos
            dsc_Imagen1 = e.FileName;





        }

        private void btnNuevaMemoriaDescriptiva_Click(object sender, EventArgs e)
        {
            AgregarMemoriaDesc();
        }

        private Boolean validarCadenaVacio(string valor)
        {
            if (valor == null)
            {
                return true;
            }
            if (valor.Trim() == "")
            {
                return true;
            }
            if (valor.Trim().Length == 0)
            {
                return true;
            }
            return false;
        }
    }
}