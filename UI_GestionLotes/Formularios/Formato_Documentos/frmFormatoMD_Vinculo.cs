
using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BL_GestionLotes.blGlobales;

namespace UI_GestionLotes.Formularios.Documento
{
    internal enum DocVinculo
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2,
        Clonar = 3,
        Version = 4
    }
    public partial class frmFormatoMD_Vinculo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;

        internal DocVinculo MiAccion = DocVinculo.Editar;
        //private string CodEmpresa;
        //private string CodVinculo;
        //private string CodGrupo;
        FormatoMDHelper _helper;
        //List<eFormatoMD_Vinculo_Filtro> formatoList;

        //Diagrama de secuencia ...
        public frmFormatoMD_Vinculo()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            //CodEmpresa = string.Empty;
            //CodVinculo = string.Empty;
            _helper = new FormatoMDHelper();
            ConfigurarFormulario();


            InicializarDatos();

        }
        private void ConfigurarFormulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoFormatos, gvListadoFormatos);
            gvListadoFormatos.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoFormatos.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoFormatos.OptionsView.EnableAppearanceEvenRow = false;

            //gvListadoFormatos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            //gvListadoFormatos.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            //gvListadoFormatos.OptionsSelection.MultiSelect = true;

            gvListadoFormatos.ExpandAllGroups();

            lblHelpParametro.Text = "";
            lblHelpParametro.AllowHtmlString = true;
            lblHelpParametro.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblHelpParametro.Appearance.Options.UseTextOptions = true;

            //lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
        }
        private void InicializarDatos()
        {
            CargarListadoEmpresa();
            CargarListadoFormatos();

            CargarParametros();
        }
        #region Cargar Datos
        private void CargarListadoFormatos()
        {
            string codEmpresa = lkpEmpresa.EditValue.ToString();
            var objFormato = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Vinculo_Filtro>(
                new BE_GestionLotes.pQFormatD() { Opcion = 9, Cod_empresaSplit = codEmpresa, Cod_solucion = Program.Sesion.Cod_solucion });

            bsListadoFormatos.DataSource = null;
            if (objFormato != null && objFormato.Count > 0)
            {
                var estado = getCheckSeleccionado();
                var newList = objFormato
                   .Where(w => w.flg_estado.ToLower().Contains(estado.ToLower()))
                   .OrderBy(o => o.num_jerarquia)/*.ThenBy(t => t.dsc_formatoMD_vinculo)*/.ToList();
                bsListadoFormatos.DataSource = newList;
                gvListadoFormatos.RefreshData();
                gvListadoFormatos.ExpandAllGroups();
            }
        }
        private void CargarListadoEmpresa()
        {
            unit.Factura.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
            lkpEmpresa.EditValue = "00008";// Program.Sesion.EmpresaList[0].cod_empresa;
        }

        private void CargarCombobox(string cod_empresa)
        {
            unit.Factura.CargaCombosLookUp("GrupoFormatoDocumento", lkpGrupo, "cod_formatoMD_grupo", "dsc_formatoMD_grupo", "", valorDefecto: false, cod_usuario: Program.Sesion.Usuario.cod_usuario, cod_solucion: Program.Sesion.Cod_solucion);
            lkpGrupo.EditValue = "GRP01";

            unit.General.CargaCombosChecked(TyCmbChecked.CargosEmpresa,
                cbxFirma, "cod_cargo", "dsc_cargo", "00000", cod_empresa);
        }

        #endregion

        #region Validaciones
        private void Limpiar()
        {
            txtDescripcion.Text = "";
            txtFormato.Text = "";
            cbxFirma.EditValue = "";
            chkObligatorio.Checked = false;
            chkSeguimiento.Checked = false;
            txtVersion.Text = "1";
        }
        void mostrarRibbon(bool value)
        {
            homeRibbon.Visible = value;
            insertRibbon.Visible = value;
            pageLayoutRibbon.Visible = value;
            viewRibbon.Visible = value;
        }
        private void AdministrarBotones()
        {
            txtDescripcion.Enabled = true;
            cbxFirma.Enabled = true;
            chkSeguimiento.Enabled = true;
            txtVersion.Enabled = true;
            txtFormato.Enabled = true;
            recPlantilla.ReadOnly = true;
            btnPublicar.Enabled = false;

            switch (MiAccion)
            {
                case DocVinculo.Nuevo:
                    {
                        var codEmpresa = lkpEmpresa.EditValue.ToString();
                        CargarCombobox(codEmpresa);

                        grpEdicion.Text = "CREAR NUEVA PLANTILLA";
                        grpPlantilla.Text = "DISEÑAR PLANTILLA";
                        recPlantilla.WordMLText = "";
                        lkpGrupo.Enabled = true;
                        btnCrearNuevoFormato.Enabled = false;
                        btnPersonalizar.Enabled = false;
                        btnClonarDocumento.Enabled = false;
                        btnBuscarEnMaestros.Enabled = false;
                        btnCrearNuevaVersion.Enabled = false;
                        btnActivar.Enabled = false;
                        btnInactivar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnImportarExistente.Enabled = true;
                        btnTablaParametros.Enabled = true;
                        btnVistaFormatos.Enabled = false;
                        recPlantilla.ReadOnly = false;
                        groupFormatos.Visibility = LayoutVisibility.Never;
                        groupEdicion.Visibility = LayoutVisibility.Always;
                        btnGuardar.Text = "GUARDAR";
                        Limpiar();
                        mostrarRibbon(true);
                        break;
                    }
                case DocVinculo.Editar:
                    {
                        lkpGrupo.Enabled = false;
                        recPlantilla.ReadOnly = false;
                        btnCrearNuevoFormato.Enabled = false;
                        btnPersonalizar.Enabled = false;
                        btnClonarDocumento.Enabled = false;
                        btnBuscarEnMaestros.Enabled = false;
                        btnCrearNuevaVersion.Enabled = false;
                        btnActivar.Enabled = false;
                        btnInactivar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnImportarExistente.Enabled = true;
                        btnTablaParametros.Enabled = true;
                        btnVistaFormatos.Enabled = false;
                        groupFormatos.Visibility = LayoutVisibility.Never;
                        groupEdicion.Visibility = LayoutVisibility.Always;
                        btnGuardar.Text = "ACTUALIZAR";
                        EjecutarMostrarPlantilla();
                        CargarParaActualizar();
                        mostrarRibbon(true);
                        break;
                    }
                case DocVinculo.Vista:
                    {
                        recPlantilla.WordMLText = "";
                        grpPlantilla.Text = "";
                        btnParametro.Enabled = false;
                        btnPublicar.Enabled = true;
                        btnCrearNuevoFormato.Enabled = true;
                        btnPersonalizar.Enabled = true;
                        btnClonarDocumento.Enabled = true;
                        btnBuscarEnMaestros.Enabled = true;
                        btnCrearNuevaVersion.Enabled = true;
                        btnActivar.Enabled = true;
                        btnInactivar.Enabled = true;
                        btnEliminar.Enabled = true;
                        btnImportarExistente.Enabled = false;
                        btnTablaParametros.Enabled = false;
                        btnTablaParametros.Down = false;
                        btnVistaFormatos.Enabled = true;
                        groupFormatos.Visibility = LayoutVisibility.Always;
                        groupParametros.Visibility = LayoutVisibility.Never;
                        groupEdicion.Visibility = LayoutVisibility.Never;

                        mostrarRibbon(false);
                        break;
                    }
                case DocVinculo.Clonar:
                    {
                        lkpGrupo.Enabled = false;
                        btnCrearNuevoFormato.Enabled = false;
                        btnPersonalizar.Enabled = false;
                        btnClonarDocumento.Enabled = false;
                        btnBuscarEnMaestros.Enabled = false;
                        btnCrearNuevaVersion.Enabled = false;
                        btnActivar.Enabled = false;
                        btnInactivar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnImportarExistente.Enabled = false;
                        btnTablaParametros.Enabled = false;
                        btnVistaFormatos.Enabled = false;
                        groupFormatos.Visibility = LayoutVisibility.Never;
                        groupEdicion.Visibility = LayoutVisibility.Always;
                        txtDescripcion.Enabled = false;
                        cbxFirma.Enabled = false;
                        chkSeguimiento.Enabled = false;
                        txtVersion.Enabled = false;
                        btnGuardar.Text = "CLONAR";
                        CargarParaActualizar();
                        mostrarRibbon(false);
                        break;
                    }
                case DocVinculo.Version:
                    {
                        lkpGrupo.Enabled = false;
                        btnCrearNuevoFormato.Enabled = false;
                        btnPersonalizar.Enabled = false;
                        btnClonarDocumento.Enabled = false;
                        btnBuscarEnMaestros.Enabled = false;
                        btnCrearNuevaVersion.Enabled = false;
                        btnActivar.Enabled = false;
                        btnInactivar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnImportarExistente.Enabled = false;
                        btnTablaParametros.Enabled = false;
                        btnVistaFormatos.Enabled = false;
                        groupFormatos.Visibility = LayoutVisibility.Never;
                        groupEdicion.Visibility = LayoutVisibility.Always;
                        btnGuardar.Text = "N. VERSIÓN";
                        CargarParaActualizar();
                        mostrarRibbon(false);
                        break;
                    }
            }
        }
        private void EjecutarMostrarPlantilla()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMD_Vinculo_Filtro;
            if (obj != null)
            {
                MostrarFormatoDocumentoEnRichEditControl(obj.dsc_formatoMD_vinculo, obj.cod_formatoMD_vinculo);
            }
        }
        #endregion

        #region Obtener valores
        string getCheckSeleccionado()
        {
            return chkTodos.Checked ? "" :
                chkInactivo.Checked ? "NO" : "SI";
        }
        #endregion




        /// <summary>
        /// Trae listado de parámetros de información de trabajadores asociados a los formatos
        /// </summary>
        private void CargarParametros()
        {
            var paramList = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Parametro>(
               new pQFormatD()
               {
                   Opcion = 6,
                   Cod_solucion = Program.Sesion.Cod_solucion
               });
            if (paramList != null || paramList.Count() > 0)
            {
                bsParametros.DataSource = paramList.OrderBy(o => o.dsc_formatoMD_parametro).ToList();
            }
        }

        private void RestablecerTituloFormato()
        {
            //lblTitulo.Text = "";
            //pnlTitle.BackColor = Color.Transparent;
            //lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            //recPlantilla.WordMLText = "";
        }

        /// <summary>
        /// Muestra el formato en el RichEditControl
        /// </summary>
        private void MostrarFormatoDocumentoEnRichEditControl(string titulo, string cod_document)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Renderizando plantilla", "Cargando...");

            string codEmpresa = lkpEmpresa.EditValue.ToString();
            var docList = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Vinculo>(
                new pQFormatD() { Opcion = 8, Cod_empresaSplit = codEmpresa, Cod_formatoMD_vinculoSplit = cod_document, Cod_solucion = Program.Sesion.Cod_solucion });

            if (docList.Count() > 0 && docList != null)
            {
                // RestablecerTituloFormato();
                var temp = docList.FirstOrDefault();
                grpPlantilla.Text = titulo;
                if (temp.flg_cambio_maestro.Equals("SI"))
                {
                    grpPlantilla.Text = $"{titulo} : ¡Este documento tiene actualizacion en el texto, verificar el maestro!";
                    ////lblTitulo.BackColor = Color.FromArgb(201, 0, 119);
                    //pnlTitle.BackColor = Color.FromArgb(201, 0, 119);
                    //lblTitulo.ForeColor = Color.White;
                    ////   rpgPublicacion.Enabled = true;
                }

                //btnPublicar.Enabled = false;
                if (temp.flg_publicado.Equals("NO"))
                {
                    //  btnPublicar.Enabled = true;
                }

                CargarParaActualizar();

                var doc = recPlantilla.Document;
                doc.WordMLText = temp.dsc_wordMLText;
                recPlantilla.WordMLText = doc.WordMLText;
            }
            VerificarParametrosAsignados(false);
            SplashScreenManager.CloseForm(false);
        }


        void CargarParaActualizar()
        {

            var codEmpresa = lkpEmpresa.EditValue.ToString();
            CargarCombobox(codEmpresa);
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMD_Vinculo_Filtro;
            if (obj == null)
            {
                MiAccion = DocVinculo.Vista;
                AdministrarBotones();
                return;
            }

            var x = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Vinculo>(
                new BE_GestionLotes.pQFormatD() { Opcion = 8, Cod_formatoMD_vinculoSplit = obj.cod_formatoMD_vinculo, Cod_empresaSplit = codEmpresa, Cod_solucion = Program.Sesion.Cod_solucion });
            if (x != null && x.Count() > 0)
            {
                lkpGrupo.EditValue = obj.cod_formatoMD_grupo;
                lkpGrupo.Enabled = false;
                txtFormato.Text = x[0].dsc_formatoMD_vinculo;
                txtDescripcion.Text = x[0].dsc_observacion;
                txtVersion.Text = x[0].dsc_version;
                chkObligatorio.Checked = x[0].flg_obligatorio == "SI";
                chkSeguimiento.Checked = x[0].flg_seguimiento == "SI";
                cbxFirma.EditValue = x[0].cod_cargo_firma;
            }

            if (MiAccion == DocVinculo.Version)
            {
                var ver = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<NF>(
                    new pQFormatD() { Opcion = 21, Cod_empresaSplit = codEmpresa, Cod_formatoMD_vinculoSplit = obj.cod_formatoMD_vinculo, Cod_solucion = Program.Sesion.Cod_solucion });
                if (ver != null)
                {
                    txtVersion.Text = ver[0].num_version.ToString(); ;
                }
            }
        }
        private void Publicar()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMD_Vinculo_Filtro;
            if (obj == null) return;

            if (obj.flg_publicado.Equals("SI")) return;

            var strCodEmpresa = lkpEmpresa.EditValue.ToString();
            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDVinculo<eSqlMessage>(4,
                new eFormatoMD_Vinculo()
                {
                    cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario,
                    cod_empresa = strCodEmpresa,
                    cod_formatoMD_vinculo = obj.cod_formatoMD_vinculo,
                    cod_solucion = Program.Sesion.Cod_solucion
                });

            if (result.IsSuccess)
            {
                CargarListadoFormatos();
            };
        }

        class NF { public int num_version { get; set; } }
        //private void GrdvParametro_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Permite visitar el formulario del Maestro de Formatos para buscar documento xx.
        /// </summary>
        private void BuscarEnMaestros()
        {
            var frm = new frmFormatoMD_General();
            //frm.rpgEdicionBasica.Enabled = false;
            frm.btnNuevoGrupo.Visible = true; //
            frm.rpgReportes.Enabled = false;
            //frm.rpgPersonalizaVista.Enabled = false;
            frm.btnImportarFormato.Enabled = false;
            frm.btnVincular.Enabled = true;
            frm.codigo_empresa = lkpEmpresa.EditValue.ToString();
            frm.ShowDialog();
            CargarListadoFormatos();
        }



        /// <summary>
        /// 
        /// </summary>
        private void GuardarCambios()
        {
            var codEmpresa = lkpEmpresa.EditValue.ToString();
            var doc = recPlantilla.Document;
            var edoc = new eFormatoMD_Vinculo()
            {
                cod_empresa = codEmpresa,
                dsc_wordMLText = doc.WordMLText,
                cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario,
            };

            switch (MiAccion)
            {
                case DocVinculo.Nuevo:
                    {
                        edoc = ObtenerValores() ?? null;
                        if (edoc == null) return;
                        break;
                    }
                case DocVinculo.Editar:
                    {
                        var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMD_Vinculo_Filtro;
                        edoc = ObtenerValores() ?? null;
                        if (edoc == null) return;
                        edoc.cod_formatoMD_vinculo = obj.cod_formatoMD_vinculo;
                        break;
                    }
            }

            edoc.cod_solucion = Program.Sesion.Cod_solucion;
            if (!Validar(edoc)) return;
            //Guardar
            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDVinculo<eSqlMessage>(1, edoc);
            unit.Globales.Mensaje(result.IsSuccess, result.Outmessage, "Registro de Mis Formatos");

            if (result.IsSuccess)
            {
                //CargarListadoFormato();
                VerificarParametrosAsignados(false);
                RestablecerTituloFormato();
                MiAccion = DocVinculo.Vista;
                AdministrarBotones();
                CargarListadoFormatos();
            }
        }

        private eFormatoMD_Vinculo ObtenerValores()
        {
            if (string.IsNullOrWhiteSpace(lkpGrupo.EditValue.ToString()))
            {
                lkpGrupo.Select();
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtFormato.Text.ToString()))
            {
                txtFormato.Select();
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text.ToString()))
            {
                txtDescripcion.Select();
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtVersion.Text.ToString()))
            {
                txtVersion.Select();
                return null;
            }
            if (string.IsNullOrWhiteSpace(recPlantilla.Text.ToString()))
            {
                unit.Globales.Mensaje(false, "No se ha seleccionado ningúna plantilla para modificar", "Edición de Formatos");
                return null;
            }

            var firmas = cbxFirma.EditValue.ToString().Trim();
            firmas = firmas.Replace(" ", "");
            return new eFormatoMD_Vinculo()
            {
                //  guarda en  general -> de copia a vinculo.
                cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario,
                cod_formatoMD_general = "DM000",
                cod_empresa = lkpEmpresa.EditValue.ToString(),
                cod_formatoMD_vinculo = lkpGrupo.EditValue.ToString(),
                dsc_formatoMD_vinculo = txtFormato.Text.Trim(),
                dsc_observacion = txtDescripcion.Text.Trim(),
                dsc_version = txtVersion.Text,
                flg_obligatorio = chkObligatorio.Checked ? "SI" : "NO",
                dsc_wordMLText = recPlantilla.WordMLText,
                flg_seguimiento = chkSeguimiento.Checked ? "SI" : "NO",
                cod_cargo_firma = firmas
            };
        }

        private bool Validar(eFormatoMD_Vinculo edoc)
        {
            if (string.IsNullOrEmpty(edoc.cod_empresa))
            {
                unit.Globales.Mensaje(false, "El campo Empresa no tiene valores, vuelve a reasignar valores.", "Edición de Formatos");
                return false;
            }
            if (string.IsNullOrEmpty(edoc.cod_formatoMD_vinculo))
            {
                unit.Globales.Mensaje(false, "El campo Código no tiene valores, vuelve a reasignar valores.", "Edición de Formatos");
                return false;
            }
            if (string.IsNullOrEmpty(edoc.dsc_formatoMD_vinculo))
            {
                unit.Globales.Mensaje(false, "El campo Descripción no tiene valores, vuelve a reasignar valores.", "Edición de Formatos");
                return false;
            }
            if (string.IsNullOrEmpty(edoc.dsc_wordMLText))
            {
                unit.Globales.Mensaje(false, "El campo FormatoWord no tiene valores, vuelve a reasignar valores", "Edición de Formatos");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Permite clonar un formato existente
        /// </summary>
        private void ClonarDocumento()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMD_Vinculo_Filtro;
            if (obj == null)
            {
                unit.Globales.Mensaje(false, "Antes de Clonar, debes visualizar el formato en plantilla.", "Clonar Formato");
                return;
            }
            if (string.IsNullOrEmpty(obj.cod_formatoMD_vinculo))
            {
                unit.Globales.Mensaje(false, "Antes de Clonar, debes visualizar el formato en plantilla.", "Clonar Formato");
                return;
            }

            //Abrir modal  para clonar/crear_version, el valor del nombre que retorna

            //enviar como para metro para clonar.
            var nombreFormatoClonado = txtFormato.Text;
            var obs = txtDescripcion.Text;
            var version = txtVersion.Text;
            var codEmpresa = lkpEmpresa.EditValue.ToString();

            var sms = unit.Globales.Mensaje(TipoMensaje.YesNo, $"Se va a clonar el documento {obj.dsc_formatoMD_vinculo}.\n" +
                $"¿Estás seguro de continuar con la clonación de este documento?", "Clonar Documento");
            if (sms == DialogResult.Yes)
            {
                var result = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eSqlMessage>(
                    new pQFormatD()
                    {
                        Opcion = (MiAccion == DocVinculo.Clonar ? 10 : MiAccion == DocVinculo.Version ? 13 : -1),
                        Cod_empresaSplit = codEmpresa,
                        Cod_formatoMD_vinculoSplit = obj.cod_formatoMD_vinculo,
                        Dsc_formatoMD_general = nombreFormatoClonado,
                        Cod_formatoMD_generalSplit = obs, // Observación :: se envía a traves de ese parámetro.
                        Cod_trabajadorSplit = version,// Version :: se envía a través de ese parámetro.
                        Cod_solucion = Program.Sesion.Cod_solucion
                    });

                if (result != null && result.Count() > 0)
                {
                    unit.Globales.Mensaje(result[0].IsSuccess, result[0].Outmessage, "Clonar Vesión del Formato");
                    if (result[0].IsSuccess)
                    {
                        InicializarDatos();
                        MiAccion = DocVinculo.Vista;
                        AdministrarBotones();
                        CargarListadoFormatos();
                    }

                }
            }
        }

        private void CambiarEstado(bool estado)
        {

            string _estado = estado ? "SI" : "NO";
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMD_Vinculo_Filtro;
            if (obj == null) return;

            var response = unit.FormatoDocumento.InsertarActualizar_FormatoMDVinculo<eSqlMessage>(
                      3, new eFormatoMD_Vinculo()
                      {
                          flg_estado = _estado,
                          cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario,
                          cod_empresa = lkpEmpresa.EditValue.ToString(),
                          cod_formatoMD_vinculo = obj.cod_formatoMD_vinculo,
                          cod_solucion = Program.Sesion.Cod_solucion
                      });
            //  ejecutar proceso para actualizar.
            if (response.IsSuccess)
            {
                CargarListadoFormatos();
            }
            unit.Globales.Mensaje(response.IsSuccess, response.Outmessage, "Actualización de Formato");
        }

        private void EliminarFormato()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMD_Vinculo_Filtro;
            if (obj == null) return;

            var response = unit.FormatoDocumento.InsertarActualizar_FormatoMDVinculo<eSqlMessage>(
                      5, new eFormatoMD_Vinculo()
                      {
                          cod_empresa = lkpEmpresa.EditValue.ToString(),
                          cod_formatoMD_vinculo = obj.cod_formatoMD_vinculo,
                          cod_solucion = Program.Sesion.Cod_solucion
                      });
            //  ejecutar proceso para actualizar.
            if (response.IsSuccess)
            {
                CargarListadoFormatos();
            }
            unit.Globales.Mensaje(response.IsSuccess, response.Outmessage, "Eliminar Formato");
        }


        /// <summary>
        /// 
        /// </summary>
        private void VerificarParametrosAsignados(bool limpiar)
        {
            var objParam = bsParametros.DataSource as List<eFormatoMD_Parametro>;
            bsParametros.DataSource = limpiar ? _helper.LimpiarParametrosAsignados(objParam)
                : _helper.ObtenerParametrosAsignados(recPlantilla, objParam);
        }
        //private void ModificarValores(object sender, EventArgs e)
        //{
        //    ////if (CodVinculo != null) return;

        //    //var sel = (TreeViewOptionMultiple)treeList.GetFocusedRow();
        //    //if (sel == null) return;
        //    //if (sel.ParentID == 0) return;

        //    //MostrarDocumento();
        //    //ActualizarDatosDelFormato();
        //}
        private void frmMisDocumentos_Load(object sender, EventArgs e)
        {

        }
        private void btnBuscarEnMaestros_ItemClick(object sender, ItemClickEventArgs e)
        {
            BuscarEnMaestros();
        }

        private void btnPersonalizar_ItemClick(object sender, ItemClickEventArgs e)
        {
            MiAccion = DocVinculo.Editar;
            AdministrarBotones();

        }

        private void btnCancelaPersonalizar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnGuardaPersonalizacion_ItemClick(object sender, ItemClickEventArgs e)
        {

        }


        private void btnClonarDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            MiAccion = DocVinculo.Clonar;
            AdministrarBotones();
        }
        private void btnCrearNuevaVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            MiAccion = DocVinculo.Version;
            AdministrarBotones();
        }
        private void btnCrearNuevoFormato_ItemClick(object sender, ItemClickEventArgs e)
        {
            MiAccion = DocVinculo.Nuevo;
            AdministrarBotones();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MiAccion = DocVinculo.Vista;
            btnParametro.Down = false;
            AdministrarBotones();

        }
        private void btnEditarDatos_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ModificarValores(sender, null);
        }

        private void grdvParametro_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void grdvParametro_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "flg_asignado")
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                    var obj = grdvParametro.GetRow(e.RowHandle) as eFormatoMD_Parametro;
                    e.DefaultDraw();
                    if (obj.flg_asignado.Equals("SI"))
                    {
                        e.Cache.DrawImage(Properties.Resources.ok_18px, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                    else
                    {
                        e.Cache.DrawImage(Properties.Resources.unchecked_radio_button_18px, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            //grdvParametro.CustomDrawCell += grdvParametro_CustomDrawCell;
        }

        private void grdvParametro_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void grdvParametro_Click(object sender, EventArgs e)
        {
            var data = grdvParametro.GetFocusedRow() as eFormatoMD_Parametro;
            if (data == null) return;

            Clipboard.SetDataObject(data.dsc_formatoMD_parametro);
            copyEvent.Caption = "Elemento en portapapeles";
            copyEvent.Enabled = true;
        }


        private void btnTablaParametros_ItemClick(object sender, ItemClickEventArgs e)
        {
            var value = btnTablaParametros.Down;
            if (value)
            {
                btnTablaParametros.Caption = "Ocultar Parámetros";
                groupParametros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                btnTablaParametros.Caption = "Mostrar Parámetros";
                groupParametros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            lblHelpParametro.Text = value ? "      <b>Un Click</b> copia a portapapeles. <b>Dos Clicks</b> pega el contenido." : "";
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked)
            {
                chkActivo.Checked = false;
                chkInactivo.Checked = false;
                CargarListadoFormatos();
            }

        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivo.Checked)
            {
                chkInactivo.Checked = false;
                chkTodos.Checked = false;
                CargarListadoFormatos();
            }
        }

        private void chkInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInactivo.Checked)
            {
                chkActivo.Checked = false;
                chkTodos.Checked = false;
                CargarListadoFormatos();
            }
        }


        private void ActivarFormato(object sender, EventArgs e)
        {
            CambiarEstado(true);
        }
        private void InactivarFormato(object sender, EventArgs e)
        {
            CambiarEstado(false);
        }




        private void btnRefrescar_ItemClick(object sender, ItemClickEventArgs e)
        {
            InicializarDatos();
        }

        private void recPlantilla_DoubleClick(object sender, EventArgs e)
        {

            //RichEditControl richEdit = e.Item.Tag as RichEditControl;
            recPlantilla.Paste();
            Clipboard.Clear();
            copyEvent.Caption = "";
            copyEvent.Enabled = false;
        }


        private void txtVersion_Leave(object sender, EventArgs e)
        {
            if (txtVersion.Text.Trim().Length == 0) txtVersion.Text = "0";
        }

        private void txtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            unit.Globales.keyPressOnlyNumber(e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            btnParametro.Down = false;
            switch (MiAccion)
            {
                case DocVinculo.Nuevo:
                    GuardarCambios();
                    break;
                case DocVinculo.Editar:
                    GuardarCambios();
                    break;
                case DocVinculo.Vista:

                    break;
                case DocVinculo.Clonar:
                    ClonarDocumento();
                    break;
                case DocVinculo.Version:
                    ClonarDocumento();
                    break;
            }
        }

        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (chkActivo.Checked) return;
            ActivarFormato(sender, e);
        }

        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (chkInactivo.Checked) return;
            InactivarFormato(sender, e);
        }

        private void btnImportarExistente_ItemClick(object sender, ItemClickEventArgs e)
        {
            string path = string.Empty;
            var oFile = new System.Windows.Forms.OpenFileDialog();
            oFile.Filter = "Todos los ficheros soportados (*.rtf;*.doc;*.htm;*.html;*.mht;*.docx;*.docm;*.dotx;*.dotm;*.odt;*.dot;*.xml;*.epub;*.txt)|*.rtf;*.doc;*.htm;*.html;*.mht;*.docx;*.docm;*.dotx;*.dotm;*.odt;*.dot;*.xml;*.epub;*.txt";
            oFile.Title = "Open File";
            oFile.ShowDialog();
            if (oFile.FileName != "")
                path = Path.GetFullPath(oFile.FileName);

            if (File.Exists(path))
            {
                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    recPlantilla.LoadDocument(stream);
                }
            }
        }

        private void gvListadoFormatos_RowStyle(object sender, RowStyleEventArgs e)
        {
            unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoFormatos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "flg_obligatorio")
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                    var obj = gvListadoFormatos.GetRow(e.RowHandle) as eFormatoMD_Vinculo_Filtro;
                    e.DefaultDraw();
                    if (obj.flg_obligatorio.Equals("SI"))
                    {
                        e.Cache.DrawImage(Properties.Resources.Ok_icon20, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                    else
                    {
                        e.Cache.DrawImage(Properties.Resources.unchecked_radio_button_18px, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                }
                if (e.Column.FieldName == "flg_seguimiento")
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                    var obj = gvListadoFormatos.GetRow(e.RowHandle) as eFormatoMD_Vinculo_Filtro;
                    e.DefaultDraw();
                    if (obj.flg_seguimiento.Equals("SI"))
                    {
                        e.Cache.DrawImage(Properties.Resources.Ok_icon20, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                    else
                    {
                        e.Cache.DrawImage(Properties.Resources.unchecked_radio_button_18px, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                }
                if (e.Column.FieldName == "flg_publicado")
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                    var obj = gvListadoFormatos.GetRow(e.RowHandle) as eFormatoMD_Vinculo_Filtro;
                    e.DefaultDraw();
                    if (obj.flg_publicado.Equals("SI"))
                    {
                        e.Cache.DrawImage(Properties.Resources.Ok_icon20, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                    else
                    {
                        e.Cache.DrawImage(Properties.Resources.unchecked_radio_button_18px, e.Bounds.X + (e.Column.Width / 2), e.Bounds.Y);
                    }
                }
            }
        }

        private void gvListadoFormatos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnVistaFormatos_ItemClick(object sender, ItemClickEventArgs e)
        {
            var value = btnVistaFormatos.Down;
            if (value)
            {
                btnVistaFormatos.Caption = "Ocultar Lista de Formatos";
                groupFormatos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnVistaFormatos.ImageOptions.LargeImage = Properties.Resources.hide_32x32;
            }
            else
            {
                btnVistaFormatos.Caption = "Mostrar Lista de Formatos";
                groupFormatos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btnVistaFormatos.ImageOptions.LargeImage = Properties.Resources.show_32x32;
            }
            //lblHelpParametro.Text = value ? "      <b>Un Click</b> copia a portapapeles. <b>Dos Clicks</b> pega el contenido." : "";
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            CargarListadoFormatos();
        }

        private void gvListadoFormatos_DoubleClick(object sender, EventArgs e)
        {
            EjecutarMostrarPlantilla();
        }

        private void btnPublicar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Publicar();
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (HNGMessageBox.Show("Se va a eliminar el formato, esta acción es irreversible\n\n¿Desea contiuar con esta acción?", "Eliminar Formato", TypeMessage.Question) == DialogResult.Yes)
            {
                EliminarFormato();
            }
        }











        //public static void CustomDrawCell(GridControl gridControl, GridView gridView)
        //{
        //    // Handle this event to paint cells manually
        //    gridView.CustomDrawCell += (s, e) => {
        //        if (e.Column.VisibleIndex != 2) return;
        //        e.Cache.FillRectangle(Color.Salmon, e.Bounds);
        //        e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
        //        e.Handled = true;
        //    };
        //}

    }
}