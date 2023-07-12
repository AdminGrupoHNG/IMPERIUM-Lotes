using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;

using static System.Windows.Forms.ImageList;
using DevExpress.Utils;

using System.IO;
using DevExpress.XtraEditors;
using BE_GestionLotes;
using BL_GestionLotes;
using UI_GestionLotes.Formularios.FormatoDocumentos;
using UI_GestionLotes.Tools;

namespace UI_GestionLotes.Formularios.Documento
{
    internal enum DocMaestro
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmFormatoMD_General : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;

        internal DocMaestro MiAccion = DocMaestro.Editar;
        FormatoMDHelper _helper;
        internal string codigo_empresa = string.Empty;
        public frmFormatoMD_General()
        {
            InitializeComponent();
            unit = new UnitOfWork();

            _helper = new FormatoMDHelper();
            ConfigurarFormulario();
            InicializarDatos();
        }
        private void ConfigurarFormulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoFormatos, gvListadoFormatos);
            gvListadoFormatos.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoFormatos.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoFormatos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvListadoFormatos.OptionsView.EnableAppearanceEvenRow = false;
            gvListadoFormatos.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gvListadoFormatos.OptionsSelection.MultiSelect = true;

            gvListadoFormatos.ExpandAllGroups();

            lblHelpParametro.Text = "";
            lblHelpParametro.AllowHtmlString = true;
            lblHelpParametro.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblHelpParametro.Appearance.Options.UseTextOptions = true;

            //lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
        }

        private void InicializarDatos()
        {
            CargarListadoFormatos();
            CargarParametros();
            CargarCombobox();
        }
        #region Cargar Datos
        private void CargarListadoFormatos()
        {
            var objFormato = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMDGeneral_Tree>(
                new BE_GestionLotes.pQFormatD() { Opcion = 3, Cod_solucion = Program.Sesion.Cod_solucion });

            bsListadoFormatos.DataSource = null;
            if (objFormato != null && objFormato.Count > 0)
            {
                var newList = objFormato
                   .OrderBy(o => o.num_jerarquia)/*.ThenBy(t => t.dsc_formatoMD_vinculo)*/.ToList();
                bsListadoFormatos.DataSource = newList;
                gvListadoFormatos.RefreshData();
                gvListadoFormatos.ExpandAllGroups();
            }
        }
        private void CargarCombobox()
        {
            unit.Factura.CargaCombosLookUp("GrupoFormatoDocumento", lkpGrupo, "cod_formatoMD_grupo", "dsc_formatoMD_grupo", "",
                valorDefecto: false, cod_usuario: Program.Sesion.Usuario.cod_usuario, cod_solucion: Program.Sesion.Cod_solucion);
            lkpGrupo.EditValue = "GRP01";


            unit.General.CargaCombosLookUp(TablaGeneral.TipoFormatoDocumento, lkpTipoFormato, "cod_tabla", "dsc_descripcion", "", cod_solucion: Program.Sesion.Cod_solucion);
        }

        /// <summary>
        /// Trae listado de parámetros que serán asignados a cada Formato.
        /// Esta información se almacena en "bsParametros.DataSource" para mostrar en la GridView
        /// </summary>
        private void CargarParametros()
        {
            var paramList = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_Parametro>(
               new pQFormatD() { Opcion = 6, Cod_solucion = Program.Sesion.Cod_solucion });

            if (paramList != null || paramList.Count() > 0)
            {
                bsParametros.DataSource = paramList.OrderBy(o => o.dsc_formatoMD_parametro).ToList();
            }
        }
        #endregion
        #region Validaciones
        private void Limpiar()
        {
            txtDescripcion.Text = "";
            txtFormato.Text = "";
            txtVersion.Text = "1";
            chkObligatorio.Checked = false;
        }
        void mostrarRibbon(bool value)
        {
            homeRibbon.Visible = value;
            insertRibbon.Visible = value;
            pageRibbon.Visible = value;
            viewRibbon.Visible = value;
        }
        private void AdministrarBotones()
        {
            lkpGrupo.Enabled = true;

            switch (MiAccion)
            {
                case DocMaestro.Nuevo:
                    {
                        grpEdicion.Text = "CREAR NUEVA PLANTILLA";
                        grpPlantilla.Text = "DISEÑAR PLANTILLA";
                        recPlantilla.WordMLText = "";
                        btnCrearPlantilla.Enabled = false;
                        btnModificar.Enabled = false;
                        btnVincular.Enabled = false;
                        btnPublicarPlantilla.Enabled = false;
                        btnImportarFormato.Enabled = true;
                        btnParametro.Enabled = true;
                        btnVistaFormatos.Enabled = false;
                        btnActivar.Enabled = false;
                        btnInactivar.Enabled = false;
                        btnEliminar.Enabled = false;
                        recPlantilla.ReadOnly = false;

                        groupFormatos.Visibility = LayoutVisibility.Never;
                        groupEdicion.Visibility = LayoutVisibility.Always;
                        btnGuardar.Text = "GUARDAR";
                        Limpiar();
                        mostrarRibbon(true);
                        break;
                    }
                case DocMaestro.Editar:
                    {
                        grpEdicion.Text = "ACTUALIZAR PLANTILLA";
                        btnCrearPlantilla.Enabled = false;
                        btnModificar.Enabled = false;
                        btnVincular.Enabled = false;
                        btnPublicarPlantilla.Enabled = false;
                        btnImportarFormato.Enabled = true;
                        btnParametro.Enabled = true;
                        btnVistaFormatos.Enabled = false;
                        btnActivar.Enabled = false;
                        btnInactivar.Enabled = false;
                        btnEliminar.Enabled = false;
                        recPlantilla.ReadOnly = false;

                        groupFormatos.Visibility = LayoutVisibility.Never;
                        groupEdicion.Visibility = LayoutVisibility.Always;
                        btnGuardar.Text = "ACTUALIZAR";
                        EjecutarMostrarPlantilla();
                        CargarParaActualizar();
                        mostrarRibbon(true);
                        break;
                    }
                case DocMaestro.Vista:
                    {
                        recPlantilla.WordMLText = "";
                        grpPlantilla.Text = "";
                        btnCrearPlantilla.Enabled = true;
                        btnModificar.Enabled = true;
                        btnVincular.Enabled = true;
                        btnPublicarPlantilla.Enabled = false;
                        btnImportarFormato.Enabled = false;
                        btnParametro.Enabled = false;
                        btnVistaFormatos.Enabled = true;

                        btnActivar.Enabled = true;
                        btnInactivar.Enabled = true;
                        btnEliminar.Enabled = true;
                        //btnActivar.Enabled = false;
                        //btnInactivar.Enabled = false;

                        recPlantilla.ReadOnly = true;
                        groupParametros.Visibility = LayoutVisibility.Never;

                        groupFormatos.Visibility = LayoutVisibility.Always;
                        groupEdicion.Visibility = LayoutVisibility.Never;

                        groupPlantilla.Text = "";
                        recPlantilla.WordMLText = "";
                        mostrarRibbon(false);
                        break;
                    }
            }
        }

        private void EjecutarMostrarPlantilla()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMDGeneral_Tree;
            if (obj != null)
            {
                MostrarFormatoDocumentoEnRichEditControl(obj.dsc_formatoMD_general, obj.cod_formatoMD_general);
            }
        }
        #endregion
        #region Obtener Valores

        #endregion









        /// <summary>
        /// Verifica que parámetros están asignados a cada Formato, eso se consulta 
        /// en el BindingSource "bsParametros"
        /// </summary>
        private void VerificarParametrosAsignados(bool limpiar)
        {
            var objParam = bsParametros.DataSource as List<eFormatoMD_Parametro>;
            //EL Objeto _helper ejecuta el proceso que permite el match.
            bsParametros.DataSource = limpiar ? _helper.LimpiarParametrosAsignados(objParam)
                : _helper.ObtenerParametrosAsignados(recPlantilla, objParam);
        }




        /// <summary>
        /// Método para mostrar El Formato del documento
        /// </summary>
        private void MostrarFormatoDocumentoEnRichEditControl(string titulo, string cod_formato)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Renderizando plantilla", "Cargando...");

            var plantilla = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_General>(
                new pQFormatD() { Opcion = 1, Cod_formatoMD_generalSplit = cod_formato, Cod_solucion = Program.Sesion.Cod_solucion });
            if (plantilla.Count() > 0 && plantilla != null)
            {
                var temp = plantilla.FirstOrDefault();
                grpPlantilla.Text = titulo.ToUpper();

                //grpPlantilla.BackColor= Program.Sesion.Colores.Verde;

                btnPublicarPlantilla.Enabled = false;
                //pnlTitle.BackColor = Color.Transparent;
                //lblTitulo.ForeColor = Program.Sesion.Colores.Verde;


                //Aquí se valida si el documento se ha modificado y falta publicar.
                if (temp.flg_editado.Equals("SI"))
                {

                    grpPlantilla.Text = $"{titulo.ToUpper()} | ¡Falta Publicar!";
                    //lblTitulo.BackColor = Color.FromArgb(201, 0, 119);
                    //pnlTitle.BackColor = Color.FromArgb(201, 0, 119);
                    //lblTitulo.ForeColor = Color.White;
                    btnPublicarPlantilla.Enabled = true;
                }
                CargarParaActualizar();

                var doc = recPlantilla.Document;
                doc.WordMLText = temp.dsc_wordMLText;
                recPlantilla.WordMLText = doc.WordMLText;
            }
            VerificarParametrosAsignados(false);//
            SplashScreenManager.CloseForm();
        }
        void CargarParaActualizar()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMDGeneral_Tree;
            if (obj == null)
            {
                MiAccion = DocMaestro.Vista;
                AdministrarBotones();
                return;
            }
            grpPlantilla.Text = $"ACTUALIZAR : {obj.dsc_formatoMD_general.ToUpper()}";

            var x = unit.FormatoDocumento.ConsultaVarias_FormatoMDocumento<eFormatoMD_General>(
                new pQFormatD() { Opcion = 1, Cod_formatoMD_generalSplit = obj.cod_formatoMD_general, Cod_solucion = Program.Sesion.Cod_solucion });
            if (x != null && x.Count() > 0)
            {
                lkpGrupo.EditValue = x[0].cod_formatoMD_grupo;
                lkpGrupo.Enabled = false;
                txtFormato.Text = x[0].dsc_formatoMD_general;
                txtDescripcion.Text = x[0].dsc_observacion;
                txtVersion.Text = x[0].num_modelo.ToString();
                chkObligatorio.Checked = x[0].flg_obligatorio == "SI";
            }
        }
        /// <summary>
        /// Guarda los cambios, Actualiza el formato o redirige a formulario para insertar nuevo formato.
        /// </summary>
        internal void GuardarCambios()
        {
            //AsignarValores
            var doc = recPlantilla.Document;
            var objFormato = new eFormatoMD_General()
            {
                dsc_wordMLText = doc.WordMLText,
                cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario
            };
            switch (MiAccion)
            {
                case DocMaestro.Nuevo:
                    {
                        objFormato = ObtenerValores() ?? null;
                        if (objFormato == null) return;
                        break;
                    }
                case DocMaestro.Editar:
                    {
                        objFormato = ObtenerValores() ?? null;
                        if (objFormato == null) return;

                        var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMDGeneral_Tree;



                        objFormato.cod_formatoMD_general = obj.cod_formatoMD_general;
                        objFormato.cod_formatoMD_grupo = obj.cod_formatoMD_grupo;
                        break;
                    }
            }

            objFormato.cod_solucion = Program.Sesion.Cod_solucion;
            objFormato.flg_activo = "SI";
            if (!Validar(objFormato)) return;
            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGeneral<eSqlMessage>(1, objFormato);
            unit.Globales.Mensaje(result.IsSuccess, result.Outmessage, "Registro de Formato de plantillas");
            //result.IsSuccess -> Te dice si ia inserción o actualización se ha realizado correctamente.
            // esta información viene de la DB.
            if (result.IsSuccess)
            {
                InicializarDatos();
                MiAccion = DocMaestro.Vista;
                AdministrarBotones();

            }
        }

        private eFormatoMD_General ObtenerValores()
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
            if (lkpTipoFormato.EditValue == null)
            {
                unit.Globales.Mensaje(false, "No se ha seleccionado el Tipo de Formato", "Edición de Formatos");
                return null;
            }

            return new eFormatoMD_General()
            {
                cod_formatoMD_general = ".",
                cod_formatoMD_grupo = lkpGrupo.EditValue.ToString(),
                dsc_formatoMD_general = txtFormato.Text.Trim(),
                dsc_observacion = txtDescripcion.Text.Trim(),
                num_modelo = int.Parse(txtVersion.Text),
                flg_obligatorio = chkObligatorio.Checked ? "SI" : "NO",
                dsc_wordMLText = recPlantilla.WordMLText,
                cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario,
                cod_tipo_formato = lkpTipoFormato.EditValue.ToString()
            };
        }

        /// <summary>
        /// Valida si los atributos que se van a insertar/actualizar de la clase eFormatoMD_General son válidos.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool Validar(eFormatoMD_General obj)
        {
            if (obj == null)
            {
                unit.Globales.Mensaje(false, "No se ha seleccionado ningúna plantilla para modificar", "Edición de Formatos");
                return false;
            }
            if (string.IsNullOrEmpty(obj.cod_formatoMD_general))
            {
                unit.Globales.Mensaje(false, "No se ha seleccionado ningúna plantilla para modificar", "Edición de Formatos");
                return false;
            }
            if (string.IsNullOrEmpty(obj.cod_formatoMD_grupo))
            {
                unit.Globales.Mensaje(false, "No se ha seleccionado ningúna plantilla para modificar", "Edición de Formatos");
                return false;
            }
            if (string.IsNullOrEmpty(obj.dsc_formatoMD_general))
            {
                unit.Globales.Mensaje(false, "No se ha asignado un nombre al documento.", "Edición de Formatos");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Muestra formulario para indicar el nombre, grupo y más obs. del nuevo Formato
        /// </summary>
        /// <param name="titulo">Título del formulario según acción</param>
        /// <param name="accion">Acción general.new/general.edit </param>
        /// <param name="codGrupo">Código del grupo de formatos, para ubicar en el ComboBox</param>
        /// <param name="codFormato">Código del formato para pintar información en los controles del formulario.</param>
        /// <returns></returns>
        //private eResponse CrearNuevoDocumento(string titulo,
        //    string accion, string codGrupo = "", string codFormato = "")
        //{
        //    eResponse result = new eResponse() { IsSuccess = false, Data = null };

        //    var frm = new frmFormatoMD_ModalEdicion();
        //    frm.MiModulo = ModuloF.General;
        //    frm.MiEstado = EstadoNuevo.Cancelado;
        //    frm.Title = titulo;
        //    frm.CargarDatos(accion: accion, codGrupo: codGrupo, codFormato: codFormato);
        //    //frm.ShowDialog();
        //    new ToolHelper.Forms().ShowDialog(frm);

        //    //Si el formulario se cierra al pulsar el button "Guardar" significa que
        //    //se va a realizar el registro.
        //    if (frm.MiEstado == EstadoNuevo.Realizado)
        //    {
        //        var dm = new eFormatoMD_General()
        //        {
        //            cod_formatoMD_grupo = frm.FormaGeneral.cod_formatoMD_grupo,
        //            dsc_formatoMD_general = frm.FormaGeneral.dsc_formatoMD_general,
        //            dsc_observacion = frm.FormaGeneral.dsc_observacion,
        //            num_modelo = frm.FormaGeneral.num_modelo,
        //            flg_obligatorio = frm.FormaGeneral.flg_obligatorio,
        //        };


        //        result.IsSuccess = true;
        //        result.Data = dm;
        //    }
        //    return result;
        //}

        /// <summary>
        /// Muestra el formulario para realizar la vinculación de los Formatos y Empresas.
        /// </summary>
        private void VincularFormatos()
        {
            var objList = new List<eFormatoMDGeneral_Tree>();

            foreach (var nRow in gvListadoFormatos.GetSelectedRows())
            {
                var obj = gvListadoFormatos.GetRow(nRow) as eFormatoMDGeneral_Tree;
                /*Aquí validamos que solo se vincule los formatos publicados.*/
                if (obj.flg_publicado.Equals("SI")) { objList.Add(obj); }
            }

            var distinctList = objList.Distinct().ToList();
            if (distinctList.Count > 0 && distinctList != null)
            {
                var frm = new frmFormatoMD_ModalVincular(this);
                frm.Text = "Vinculación de Formatos";
                frm.CargarDatos(distinctList, codigo_empresa);
                new ToolHelper.Forms().ShowDialog(frm);
            }
            else { unit.Globales.Mensaje(false, "No has seleccionado ningún documento a vincular o el Formato no está publicado. ¡Intenta una ves más!", "Vinculación de Documentos"); }
        }

        /// <summary>
        /// Permite publicar los cambios realiados en un Formato, de esta manera las demás empresas pueden ver
        /// si el cambio realiado es una mejora o no.
        /// </summary>
        private void PublicarCambios()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMDGeneral_Tree;

            if (string.IsNullOrWhiteSpace(obj.cod_formatoMD_general))
            {
                unit.Globales.Mensaje(false, "No se ha seleccionado el Formato a publicar", "Publicar formato");
                return;
            }

            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGeneral<eSqlMessage>(2,
                new eFormatoMD_General()
                {
                    cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario,
                    cod_formatoMD_general = obj.cod_formatoMD_general,
                    cod_solucion = Program.Sesion.Cod_solucion
                });

            unit.Globales.Mensaje(result.IsSuccess, result.Outmessage, "Publicar Formato");
            MiAccion = DocMaestro.Vista;
            //Si la publicación se realiza correctamente, se reprocesa la información de formatos.
            if (result.IsSuccess)
            {
                //CargarListadoFormato();
                recPlantilla.WordMLText = "";
                grpPlantilla.Text = "";
                btnPublicarPlantilla.Enabled = false;
                InicializarDatos();
            }
        }

        private void EstadoFormatos(string estado)
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMDGeneral_Tree;

            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGeneral<eSqlMessage>(4,
               new eFormatoMD_General()
               {
                   cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario,
                   cod_formatoMD_general = obj.cod_formatoMD_general,
                   cod_solucion = Program.Sesion.Cod_solucion,  
                   flg_activo= estado,
               });

            if (result.IsSuccess)
            {
                //CargarListadoFormato();
                recPlantilla.WordMLText = "";
                grpPlantilla.Text = "";
                btnPublicarPlantilla.Enabled = false;
                InicializarDatos();
            }
            else
            {
                HNGMessageBox.Show(result.Outmessage, "Estado de Formato", TypeMessage.Warning);
            }
        }
        private void EliminarFormato()
        {
            var obj = gvListadoFormatos.GetFocusedRow() as eFormatoMDGeneral_Tree;

            var result = unit.FormatoDocumento.InsertarActualizar_FormatoMDGeneral<eSqlMessage>(5,
              new eFormatoMD_General()
              {
                  cod_formatoMD_general = obj.cod_formatoMD_general,
                  cod_solucion = Program.Sesion.Cod_solucion
              });

            if (result.IsSuccess)
            {
                //CargarListadoFormato();
                recPlantilla.WordMLText = "";
                grpPlantilla.Text = "";
                btnPublicarPlantilla.Enabled = false;
                InicializarDatos();
            }
            else
            {
                HNGMessageBox.Show(result.Outmessage, "Eliminar Formato", TypeMessage.Warning);
            }
        }
        private void frmMaestroDocumento_Load(object sender, EventArgs e)
        {

        }


        private void btnModificar_ItemClick(object sender, ItemClickEventArgs e)
        {
            MiAccion = DocMaestro.Editar;
            AdministrarBotones();
        }
        private void btnCrearPlantilla_ItemClick(object sender, ItemClickEventArgs e)
        {
            MiAccion = DocMaestro.Nuevo;
            AdministrarBotones();
        }

        private void btnGuardarCambios_ItemClick(object sender, ItemClickEventArgs e)
        {


        }

        private void btnPublicarPlantilla_ItemClick(object sender, ItemClickEventArgs e)
        {
            PublicarCambios();
        }

        private void btnCancelarModificacion_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnVincular_ItemClick(object sender, ItemClickEventArgs e)
        {
            VincularFormatos();
        }
        private void btnParametro_ItemClick(object sender, ItemClickEventArgs e)
        {
            var value = btnParametro.Down;
            if (value)
            {
                btnParametro.Caption = "Ocultar Parámetros";
                groupParametros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                btnParametro.Caption = "Mostrar Parámetros";
                groupParametros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            lblHelpParametro.Text = value ? "      <b>Un Click</b> copia a portapapeles. <b>Dos Clicks</b> pega el contenido." : "";
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
                        e.Cache.DrawImage(Properties.Resources.ok_18px, e.Bounds.X + 15, e.Bounds.Y);
                        //e.Cache.DrawImage(Properties.Resources.ok_18px, e.Bounds.Location);
                    }
                    else
                    {
                        e.Cache.DrawImage(Properties.Resources.unchecked_radio_button_18px, e.Bounds.X + 15, e.Bounds.Y);
                    }
                }
            }
        }

        private void grdvParametro_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void grdvParametro_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void grdvParametro_Click(object sender, EventArgs e)
        {
            var data = grdvParametro.GetFocusedRow() as eFormatoMD_Parametro;
            if (data == null) return;

            Clipboard.SetDataObject(data.dsc_formatoMD_parametro);
            copyEvents.Caption = "Elemento en portapapeles";
            copyEvents.Enabled = true;
        }


        private void recPlantilla_DoubleClick(object sender, EventArgs e)
        {
            recPlantilla.Paste();
            Clipboard.Clear();
            copyEvents.Caption = "";
            copyEvents.Enabled = false;
        }



        private void txtVersion_Leave(object sender, EventArgs e)
        {
            if (txtVersion.Text.Trim().Length == 0) txtVersion.Text = "0";
        }

        private void txtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            unit.Globales.keyPressOnlyNumber(e);
        }
        void cancelarNuevoRegistro()
        {
            MiAccion = DocMaestro.Vista;
            AdministrarBotones();
            //layoutFormatos.Visibility = LayoutVisibility.Always;
            //layoutModal.Visibility = LayoutVisibility.Never;
            txtDescripcion.Text = "";
            txtFormato.Text = "";
            txtVersion.Text = "0";
            chkObligatorio.Checked = false;
        }



        private void gvListadoFormatos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoFormatos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "flg_obligatorio")
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                    var obj = gvListadoFormatos.GetRow(e.RowHandle) as eFormatoMDGeneral_Tree;
                    e.DefaultDraw();
                    if (obj.flg_obligatorio.Equals("SI"))
                    {
                        e.Cache.DrawImage(Properties.Resources.Checkmark_18px, e.Bounds.X + 15, e.Bounds.Y);
                    }
                    else
                    {
                        e.Cache.DrawImage(Properties.Resources.unchecked_radio_button_18px, e.Bounds.X + 15, e.Bounds.Y);
                    }
                }
                if (e.Column.FieldName == "flg_publicado")
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                    var obj = gvListadoFormatos.GetRow(e.RowHandle) as eFormatoMDGeneral_Tree;
                    e.DefaultDraw();
                    if (obj.flg_publicado.Equals("SI"))
                    {
                        e.Cache.DrawImage(Properties.Resources.Checkmark_18px, e.Bounds.X + 15, e.Bounds.Y);
                    }
                    else
                    {
                        e.Cache.DrawImage(Properties.Resources.unchecked_radio_button_18px, e.Bounds.X + 15, e.Bounds.Y);
                    }
                }
            }
        }

        private void gvListadoFormatos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MiAccion = DocMaestro.Vista;
            btnParametro.Down = false;
            AdministrarBotones();

        }

        private void btnImportarFormato_ItemClick(object sender, ItemClickEventArgs e)
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

        private void gvListadoFormatos_DoubleClick(object sender, EventArgs e)
        {
            EjecutarMostrarPlantilla();
        }



        private void btnVistaFormatos_ItemClick(object sender, ItemClickEventArgs e)
        {
            var value = btnVistaFormatos.Down;
            if (value)
            {
                btnVistaFormatos.Caption = "Ocultar Lista de Formatos";
                groupFormatos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //btnVistaFormatos.ImageOptions.Image = Properties.Resources.show_32x32;
                btnVistaFormatos.ImageOptions.LargeImage = Properties.Resources.hide_32x32;
            }
            else
            {
                btnVistaFormatos.Caption = "Mostrar Lista de Formatos";
                groupFormatos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //btnVistaFormatos.ImageOptions.Image = Properties.Resources.show_32x32;
                btnVistaFormatos.ImageOptions.LargeImage = Properties.Resources.show_32x32;
            }
            //lblHelpParametro.Text = value ? "      <b>Un Click</b> copia a portapapeles. <b>Dos Clicks</b> pega el contenido." : "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            btnParametro.Down = false;
            GuardarCambios();
        }

        private void btnNuevoGrupo_Click(object sender, EventArgs e)
        {
            AbrirGrupos();
        }

        private void AbrirGrupos()
        {
            var frm = new frmFormatoMD_GrupoNuevo
            {
                Text = "Grupo de Formatos"
            };
            frm.ShowDialog();

            //if (frm.ShowDialog() == DialogResult.OK)
            CargarCombobox();
        }
        private void btnConfigurarGrupo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AbrirGrupos();
        }

        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            EstadoFormatos("SI");
        }

        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            EstadoFormatos("NO");
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(HNGMessageBox.Show("Se va a eliminar el formato, esta acción es irreversible\n\n¿Desea contiuar con esta acción?","Eliminar Formato", TypeMessage.Question)== DialogResult.Yes)
            {
                EliminarFormato();
            }            
        }
    }
}