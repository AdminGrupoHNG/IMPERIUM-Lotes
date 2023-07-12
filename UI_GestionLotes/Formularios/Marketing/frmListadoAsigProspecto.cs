using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;
using BE_GestionLotes;
using BL_GestionLotes;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using UI_GestionLotes.Formularios.Marketing;

namespace UI_GestionLotes.Formularios.Lotes
{
    public partial class frmListadoAsigProspecto : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;

        bool Buscar = false;
        public string sCodigoEmpresaTab;
        public string CodMenu, DscMenu, cod_empresa = "", cod_proyecto = "", Cod_campnhaFiltro = "";
        public int nEstadoFiltro = 2, MenuIndice;
        public string IndTipoAsig = "Asignación";
        List<eCampanha> Listcampanha_grilla = new List<eCampanha>();

        public frmListadoAsigProspecto()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoAsigProspecto_Load(object sender, EventArgs e)
        {
            //gc_FiltrosAvan.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            HabilitarBotones();
            Inicializar();
            cargarFechas();
            cargarTitulo("Todos ");

            //Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
            //btnOcultarFiltro.ImageOptions.LargeImage = img;
            //btnOcultarFiltro.Caption = "Mostrar Filtro";
            //lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        internal void frmListadoAsigProspecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                lblTitulo.Visible = false;

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CodMenu = cod_empresa + "|" + cod_proyecto;
                MenuIndice = navBarControl1.Groups[0].SelectedLinkIndex;
                CargarOpcionesMenu();
                lblTitulo.Visible = true;
                rgFiltroProyecto.EditValue = CodMenu;
                cargarTitulo("Todos ");
                llenarListarDatos();
                //rgFiltroProyecto_SelectedIndexChanged(null, null);
                //rgFiltroEstado.SelectedIndex = nEstadoFiltro;
                //rgFiltroEstado_SelectedIndexChanged(null, null);
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            rgFiltroProyecto.SelectedIndex = 0;
            sbBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            Buscar = true;
            //rgFiltroEstado.SelectedIndex = 2;
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eCampanha> Listcampanha = new List<eCampanha>();
            Listcampanha = unit.Campanha.ListarProyectoscampanhasMenu<eCampanha>(1, "", "", Program.Sesion.Usuario.cod_usuario);

            rgFiltroProyecto.Properties.Items.Clear();
            foreach (eCampanha obj in Listcampanha)
            {
                RadioGroupItem rgiDetalle = new RadioGroupItem();
                rgiDetalle.Value = obj.cod_nodo;
                rgiDetalle.Description = obj.dsc_nodo;
                rgFiltroProyecto.Properties.Items.Add(rgiDetalle);
            }
            cargarProyectoEmpresa();
        }

        /////////////////////////////////// 
        //private void NavDetalle_empresa_LinkClicked(object sender, NavBarLinkEventArgs e)
        //{
        //    lblTitulo.Text = "Proyecto : " + e.Link.Caption;
        //    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
        //    CodMenu = e.Link.Item.Tag.ToString();
        //    DscMenu = e.Link.Group.Caption + ": " + e.Link.Caption;
        //    CargarListado(CodMenu);
        //    SplashScreenManager.CloseForm();
        //}
        
        private void llenarListarDatos()
        {
            //lblTitulo.Text = rgFiltroProyecto.Properties.Items[rgFiltroProyecto.SelectedIndex].Description;
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");

            CodMenu = rgFiltroProyecto.EditValue.ToString();
            DscMenu = rgFiltroProyecto.Properties.Items[rgFiltroProyecto.SelectedIndex].Description;
            CargarListado(CodMenu);
            SplashScreenManager.CloseForm(false);

        }

        private void rgFiltroProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProyectoEmpresa();
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");

            //SplashScreenManager.CloseForm(false);
        }

        private void rgFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            RadioGroup oRadioGroup = sender as RadioGroup;
            if (oRadioGroup != null)
            {
                nEstadoFiltro = oRadioGroup.SelectedIndex;
            }
            fFiltrarGrilla();
            //SplashScreenManager.CloseForm();
        }
        private void cargarFechas()
        {
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = oPrimerDiaDelMes.AddMonths(-1);
            dtFechaFin.EditValue = oUltimoDiaDelMes;
            grdbFecha.SelectedIndex = 0;
            //dtFechaInicio
        }

        private void cargarTitulo(string estado = "")
        {
            var descFecha = grdbFecha.SelectedIndex == 0 ? "de Registro" :  "de Asignación";
            lblTitulo.Text = $"Listado de Prospectos {estado}del {Convert.ToDateTime(dtFechaInicio.EditValue).ToString("dd/MM/yyyy")} al {Convert.ToDateTime(dtFechaFin.EditValue).ToString("dd/MM/yyyy")} según Fecha {descFecha}";
        }

        public void cargarProyectoEmpresa()
        {
            if (rgFiltroProyecto.EditValue == null) return;
            string[] aCodigos = rgFiltroProyecto.EditValue.ToString().Split("|".ToCharArray());
            if (aCodigos.Length > 1)
            {
                cod_empresa = aCodigos[0];
                cod_proyecto = aCodigos[1];
            }
        }

        public void CargarListado(string sCodigoEmpresaTab)
        {
            try
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                //SplashScreen.Open("Por favor espere...", "Cargando...");
                string[] aCodigos = sCodigoEmpresaTab.Split("|".ToCharArray());
                int asignados = 0, sinAsignar = 0;
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
                Listcampanha_grilla = unit.Campanha.ListarProspectos<eCampanha>(1, cod_empresa, cod_proyecto, "", Program.Sesion.Usuario.cod_usuario, "", "", "","","",
                     grdbFecha.EditValue.ToString(),
                    Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                    Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));

                tbiTodos.Elements[1].Text = Listcampanha_grilla.Count().ToString();

                for (int i = 0; i < Listcampanha_grilla.Count(); i++)
                {
                    if (Listcampanha_grilla[i].cod_ejecutivo != "") asignados += 1;
                    if (Listcampanha_grilla[i].cod_ejecutivo == "") sinAsignar += 1;
                }
                tbiAsignados.Elements[1].Text = asignados.ToString();
                tbiSinAsignar.Elements[1].Text = sinAsignar.ToString();

                bsListaAsigProspecto.DataSource = Listcampanha_grilla;

                //fFiltrarGrilla();
                //SplashScreen.Close();
                //SplashScreenManager.CloseForm();
            }
            catch (Exception e)
            {
                //SplashScreen.Close();
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void fFiltrarGrilla()
        {
            if (nEstadoFiltro == 0)
            {
                bsListaAsigProspecto.DataSource = Listcampanha_grilla;
            }
            else if (nEstadoFiltro == 1)
            {
                List<eCampanha> Listcampanha_filtro = Listcampanha_grilla.Where(x => x.cod_ejecutivo != "").ToList();
                bsListaAsigProspecto.DataSource = Listcampanha_filtro;
            }
            else if (nEstadoFiltro == 2)
            {
                List<eCampanha> Listcampanha_filtro = Listcampanha_grilla.Where(x => x.cod_ejecutivo == "").ToList();
                bsListaAsigProspecto.DataSource = Listcampanha_filtro;
            }
        }


        public void asignacion(string AsesorSeleccionado)
        {
            if (gvListaAsigProspecto.OptionsSelection.MultiSelect == false)
            {
                if (AsesorSeleccionado.Trim() != gvListaAsigProspecto.GetRowCellValue(gvListaAsigProspecto.FocusedRowHandle, "cod_ejecutivo").ToString())
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Prospecto", "Cargando...");

                    eCampanha eCampResultado = new eCampanha();
                    eCampanha eCamp = new eCampanha();
                    eCamp.cod_ejecutivo = AsesorSeleccionado.Trim();
                    eCamp.cod_empresa = cod_empresa;
                    eCamp.cod_proyecto = cod_proyecto;
                    eCamp.cod_prospecto = gvListaAsigProspecto.GetRowCellValue(gvListaAsigProspecto.FocusedRowHandle, "cod_prospecto").ToString();
                    eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                    eCamp.cod_origen_prospecto = "";
                    eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(1, eCamp);

                    SplashScreenManager.CloseForm(false);

                    if (eCampResultado.cod_prospecto != null)
                    {
                        string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                        MessageBox.Show("Se " + sMen + " el prospecto de manera satisfactoria", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                        CargarListado(CodMenu);
                        fFiltrarGrilla();
                    }
                }

            }
            else
            {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Prospecto", "Cargando...");
                    foreach (int nRow in gvListaAsigProspecto.GetSelectedRows())
                    {
                        eCampanha objLT = gvListaAsigProspecto.GetRow(nRow) as eCampanha;
                        if (AsesorSeleccionado != objLT.cod_ejecutivo)
                        {
                            eCampanha eCampResultado = new eCampanha();
                            eCampanha eCamp = new eCampanha();
                            eCamp.cod_ejecutivo = AsesorSeleccionado;
                            eCamp.cod_empresa = cod_empresa;
                            eCamp.cod_proyecto = cod_proyecto;
                            eCamp.cod_prospecto = objLT.cod_prospecto;
                            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                            eCamp.cod_origen_prospecto = "";
                            eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(1, eCamp);
                            //if (eCampResultado.cod_prospecto == null) { validar = 1; }
                        }
                    }
                    SplashScreenManager.CloseForm(false);
                    string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                    MessageBox.Show("Se " + sMen + " los prospectos de manera satisfactoria", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                    CargarListado(CodMenu);
                    fFiltrarGrilla();
                }
        }

        void AsignarEjecutivoPersonalizado(int opcion)
        {
            try
            {
                if (gvListaAsigProspecto.RowCount > 0)
                {
                    eCampanha objCamp = gvListaAsigProspecto.GetFocusedRow() as eCampanha;
                    if (objCamp == null) { return; }
                    eCampanha tmpCamp = new eCampanha();
                    tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                    tmpCamp.cod_empresa = cod_empresa;
                    tmpCamp.cod_proyecto = cod_proyecto;
                    List<eCampanha> lstProspecto = new List<eCampanha>();
                    lstProspecto = unit.Campanha.ListarEjecuPros<eCampanha>(7, tmpCamp.cod_usuario, tmpCamp.cod_empresa, tmpCamp.cod_proyecto, tmpCamp.valor_1, tmpCamp.valor_4);


                    if (gvListaAsigProspecto.OptionsSelection.MultiSelect == false)
                    {
                        if (opcion == 1 && objCamp.cod_ejecutivo != "") { IndTipoAsig = "Reasignación"; }
                        if (opcion == 3 && objCamp.cod_ejecutivo_citaProximo != "") { IndTipoAsig = "Reasignación"; }

                    }
                    else
                    {
                        if (gvListaAsigProspecto.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignación Manual", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                        foreach (int nRow in gvListaAsigProspecto.GetSelectedRows())
                        {
                            eCampanha objLT = gvListaAsigProspecto.GetRow(nRow) as eCampanha;
                            if (opcion == 1 && objLT.cod_ejecutivo != "") { IndTipoAsig = "Reasignación"; }
                            if (opcion == 3 && objLT.cod_ejecutivo_citaProximo != "") { IndTipoAsig = "Reasignación"; }
                        }
                    }

                    frmPopupGeneralAsignar frm = new frmPopupGeneralAsignar();
                    frm.Text = IndTipoAsig + " de Prospecto";
                    foreach (eCampanha obj in lstProspecto)
                    {
                        RadioGroupItem radio = new RadioGroupItem();
                        radio.Description = obj.dsc_ejecutivo;
                        radio.Value = obj.cod_ejecutivo;
                        frm.rgAsesores.Properties.Items.Add(radio);
                    }
                    frm.rgAsesores.SelectedIndex = 0;
                    frm.btnGuardar.Text = "ASIGNAR";

                    frm.ShowDialog();
                    if (frm.Aceptar)
                    {
                        asignacion(frm.rgAsesores.EditValue.ToString(), opcion, objCamp);
                    }

                }
                else
                {
                    MessageBox.Show("Seleccione un prospecto", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void asignacion(string AsesorSeleccionado, int opcion, eCampanha objCamp)
        {
            if (gvListaAsigProspecto.OptionsSelection.MultiSelect == false)
            {
                //opcion == 1 &&
                //opcion == 3 &&
                //if ((opcion == 1 && AsesorSeleccionado.Trim() != objCamp.cod_ejecutivo) || (opcion == 3 && AsesorSeleccionado.Trim() != objCamp.cod_ejecutivo_citaProximo))
                //{
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Prospecto", "Cargando...");

                eCampanha eCampResultado = new eCampanha();
                eCampanha eCamp = new eCampanha();
                eCamp.cod_ejecutivo = AsesorSeleccionado.Trim();
                eCamp.cod_empresa = cod_empresa;
                eCamp.cod_proyecto = cod_proyecto;
                eCamp.cod_prospecto = objCamp.cod_prospecto;
                eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                eCamp.cod_origen_prospecto = "";
                eCamp.cod_evento = String.IsNullOrEmpty(objCamp.cod_eventoProximo) ? "" : objCamp.cod_eventoProximo;
                eCamp.cod_evento_cita_proxima = String.IsNullOrEmpty(objCamp.cod_evento_cita_proxima) ? "" : objCamp.cod_evento_cita_proxima;
                eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(opcion, eCamp);

                SplashScreenManager.CloseForm(false);

                if (eCampResultado.cod_prospecto != null)
                {
                    string msjProspecto = opcion == 3 ? " la cita" : " el prospecto";
                    string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                    MessageBox.Show("Se " + sMen + msjProspecto + " de manera satisfactoria", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                    llenarListarDatos();
                    //fFiltrarGrilla();
                }
                //}

            }
            else
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), IndTipoAsig + " de Prospecto", "Cargando...");
                foreach (int nRow in gvListaAsigProspecto.GetSelectedRows())
                {
                    eCampanha objLT = gvListaAsigProspecto.GetRow(nRow) as eCampanha;
                    //if ((opcion == 1 && AsesorSeleccionado != objLT.cod_ejecutivo) || (opcion == 3 && AsesorSeleccionado != objLT.cod_ejecutivo_citaProximo)) //validacion de cod_ejecutivo_cita
                    //{
                    eCampanha eCampResultado = new eCampanha();
                    eCampanha eCamp = new eCampanha();
                    eCamp.cod_ejecutivo = AsesorSeleccionado;
                    eCamp.cod_empresa = cod_empresa;
                    eCamp.cod_proyecto = cod_proyecto;
                    eCamp.cod_prospecto = objLT.cod_prospecto;
                    eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                    eCamp.cod_origen_prospecto = "";
                    eCamp.cod_evento = objLT.cod_eventoProximo;
                    eCamp.cod_evento_cita_proxima = String.IsNullOrEmpty(objCamp.cod_evento_cita_proxima) ? "" : objCamp.cod_evento_cita_proxima;
                    eCampResultado = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(opcion, eCamp);
                    //if (eCampResultado.cod_prospecto == null) { validar = 1; }
                    //}
                }
                SplashScreenManager.CloseForm(false);
                string sMen = IndTipoAsig.Replace("ación", "o").ToLower();
                MessageBox.Show("Se " + sMen + " los prospectos de manera satisfactoria", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
                llenarListarDatos();
                //fFiltrarGrilla();
            }
        }


        //void AsignarEjecutivoPersonalizado()
        //{
        //    try
        //    {
        //        if (gvListaAsigProspecto.RowCount > 0)
        //        {

        //            eCampanha tmpCamp = new eCampanha();
        //            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
        //            tmpCamp.cod_empresa = cod_empresa;
        //            tmpCamp.cod_proyecto = cod_proyecto;
        //            List<eCampanha> lstProspecto = new List<eCampanha>();
        //            lstProspecto = unit.Campanha.ListarEjecuPros<eCampanha>(7, tmpCamp.cod_usuario, tmpCamp.cod_empresa, tmpCamp.cod_proyecto, tmpCamp.valor_1, tmpCamp.valor_4);


        //            if (gvListaAsigProspecto.OptionsSelection.MultiSelect == false)
        //            {
        //                if (gvListaAsigProspecto.GetRowCellValue(gvListaAsigProspecto.FocusedRowHandle, "cod_ejecutivo").ToString() != "") { IndTipoAsig = "Reasignación"; }

        //            }
        //            else
        //            {
        //                if (gvListaAsigProspecto.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Asignación Manual", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        //                foreach (int nRow in gvListaAsigProspecto.GetSelectedRows())
        //                {
        //                    eCampanha objLT = gvListaAsigProspecto.GetRow(nRow) as eCampanha;
        //                    if (objLT.cod_ejecutivo != "") { IndTipoAsig = "Reasignación"; }
        //                }
        //            }

        //            frmPopupAsignarPros frm = new frmPopupAsignarPros();
        //            frm.Text = IndTipoAsig + " de Prospecto";
        //            foreach (eCampanha obj in lstProspecto)
        //            {
        //                RadioGroupItem radio = new RadioGroupItem();
        //                radio.Description = obj.dsc_ejecutivo;
        //                radio.Value = obj.cod_ejecutivo;
        //                frm.rgAsesores.Properties.Items.Add(radio);
        //            }
        //            frm.rgAsesores.SelectedIndex = 0;
                    

        //            frm.ShowDialog();
        //            if (frm.Aceptar)
        //            {
        //                asignacion(frm.rgAsesores.EditValue.ToString());
        //            }
                    
        //        }
        //        else
        //        {
        //            MessageBox.Show("Seleccione un prospecto", IndTipoAsig + " de Prospecto", MessageBoxButtons.OK);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}


        void ActiveGroupChanged(string caption, Image imagen)
        {
            lblTitulo.Text = caption;
        }



        private void gvListaAsigProspecto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaAsigProspecto_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);

            if (e.RowHandle >= 0)
            {
                string sEstado = gvListaAsigProspecto.GetRowCellValue(e.RowHandle, "flg_activo").ToString();
                if (sEstado == "NO")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }



        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }

        private void btnImportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarProspecto frm = new frmImportarProspecto(this);
            frm.tipoImporte = "prospecto";
            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;
            frm.ShowDialog();
        }



        private void ExportarExcel()
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\asignaciones" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListaAsigProspecto.ExportToXlsx(archivo);
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



        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListaAsigProspecto.ShowRibbonPrintPreview();
        }

        private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            nEstadoFiltro = 1;
            cargarTitulo(tbiAsignados.Elements[0].Text + " ");
            fFiltrarGrilla();
            //llenarListarDatos();
            //rgFiltroEstado.SelectedIndex = 1;
        }

        private void tbiSinAsignar_ItemClick(object sender, TileItemEventArgs e)
        {
            nEstadoFiltro = 2;
            cargarTitulo(tbiSinAsignar.Elements[0].Text + " ");
            fFiltrarGrilla();
            //llenarListarDatos();
            //rgFiltroEstado.SelectedIndex = 2;
        }

        private void tbiTodos_ItemClick(object sender, TileItemEventArgs e)
        {
            nEstadoFiltro = 0;
            cargarTitulo(tbiTodos.Elements[0].Text + " ");
            fFiltrarGrilla();
            //llenarListarDatos();
            //rgFiltroEstado.SelectedIndex = 0;
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            string cod_proyecto = "", cod_empresa = "";

            string[] aCodigos = rgFiltroProyecto.EditValue.ToString().Split("|".ToCharArray());
            if (aCodigos.Length > 1)
            {
                cod_empresa = aCodigos[0];
                cod_proyecto = aCodigos[1];
            }
            frmPopupProyectoInfo frm = new frmPopupProyectoInfo();
            frm.cod_proyecto = cod_proyecto;
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
        }

        private void lblTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void bbi_Asignaprospecto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (cod_empresa == "" || cod_proyecto == "ALL")
                {
                    MessageBox.Show("Seleccione un proyecto", "Listado de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frmAsigProspecto_Ejec frm = new frmAsigProspecto_Ejec(this);
                frm.cod_empresa = cod_empresa;
                frm.cod_proyecto = cod_proyecto;
                frm.nEstadoFiltro = nEstadoFiltro;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void bbi_Asigna_ItemClick(object sender, ItemClickEventArgs e)
        {
            //AsignarEjecutivoPersonalizado();
            AsignarEjecutivoPersonalizado(1);
        }

        private void btnExcelProspectoLeads_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImportarProspecto frm = new frmImportarProspecto();

            frm.tipoImporte = "prospectoNuevo";
            frm.cod_empresa = cod_empresa;
            frm.cod_proyecto = cod_proyecto;

            frm.ShowDialog();
        }

        private void btnSeleccionMultriple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvListaAsigProspecto.OptionsSelection.MultiSelect == false)
            {
                gvListaAsigProspecto.OptionsSelection.MultiSelect = true; return;
            }
            if (gvListaAsigProspecto.OptionsSelection.MultiSelect == true)
            {
                gvListaAsigProspecto.OptionsSelection.MultiSelect = false; return;
            }

        }

        private void gvListaAsigProspecto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCampanha obj = gvListaAsigProspecto.GetRow(e.RowHandle) as eCampanha;
                    if (e.Column.FieldName == "fch_registro" && obj.fch_registro.ToString().Contains("1/01/0001")) e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sbBuscar_Click(object sender, EventArgs e)
        {
            llenarListarDatos();
        }

        private void dtFechaInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sbBuscar.PerformClick();
            }
        }

        private void dtFechaFin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sbBuscar.PerformClick();
            }
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantEvento frm = new frmMantEvento();
            frm.MiAccion = evento.Nuevo_externo;
            frm.btnControl = false;
            frm.cod_empresa = cod_empresa;
            frm.o_eCamp = null;
            frm.sEstadoFiltro = "";
            frm.sTipoContactoFiltro = "";
            frm.sCod_ejecutivoFiltro = "";
            frm.CodMenu = "";
            frm.IndicadorConfirmacionAuto = "";
            frm.ShowDialog();
        }
        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lci_Filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (lci_Filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                lci_Filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                return;
            }
        }

    }
}