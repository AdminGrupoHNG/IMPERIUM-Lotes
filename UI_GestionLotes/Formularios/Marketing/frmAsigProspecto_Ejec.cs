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
using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.XtraGrid.Views.Grid;
using UI_GestionLotes.Formularios.Clientes_Y_Proveedores.Clientes;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;
using System.Net;
using Tesseract;
using RestSharp;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using DevExpress.XtraGrid;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
//using System.Collections;
//using System.Windows.Documents;

namespace UI_GestionLotes.Formularios.Lotes
{

    public partial class frmAsigProspecto_Ejec : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListadoAsigProspecto frmHandler;
        
        public string cod_empresa = "", cod_proyecto = "", tfila = "", tcod_ejecutivo = "", tdsc_ejecutivo = "", CodIndicadorCompleto_xOrigen = "SI";
        public string[] acodigos;
        public int TotalSuma = 100;
        public string ActualizarListado = "NO";
        DataTable tEjecutivo = new DataTable();
        public int nEstadoFiltro;
        

        public string estado_conf = "OK";
        //public string GrupoSeleccionado = "";
        //public string ItemSeleccionado = "";
        public eCampanha o_eCamp;

        public frmAsigProspecto_Ejec()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmAsigProspecto_Ejec(frmListadoAsigProspecto frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmAsigProspecto_Ejec_Load(object sender, EventArgs e)
        {
            Inicializar();
            simpleLabelItem1.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            simpleLabelItem3.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            gcFiltro.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
        }
        private void frmAsigProspecto_Ejec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.SolucionAbrir.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) BloqueoControles(false, true, false);
            }
        }
        private void Inicializar()
        {
            CargarListado_Configuracion();
            CargarListado_Asignaciones();

            if (gvResumen.RowCount == 0 | gvTotales.RowCount == 0 | gvFiltroOrigen.RowCount == 0)
            {
                MessageBox.Show("No se encontraron registros.", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string Guardar_Asignacion()
        {
            string result = "";

            if (gvResumen.RowCount <= 0)
            {
                MessageBox.Show("La asignacion se encuentra vacía.", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = "NO";
                return result;
            }

            if (CodIndicadorCompleto_xOrigen == "NO")
            {
                MessageBox.Show("Los totales no cuadran", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "NO";
            }

            if (Convert.ToInt32(gvResumen.Columns["porasignar"].SummaryItem.SummaryValue.ToString())==0)
            {
                MessageBox.Show("No se encontraron prospectos por asignar", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "NO";
            }

            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Guardando asignación", "Cargando...");

            eCampanha eCamp = new eCampanha();
            for (int f = 0; f <= gvFiltroOrigen.RowCount - 1; f++)
            {
                acodigos = gvFiltroOrigen.GetRowCellValue(0, "codigos").ToString().Split('|');

                for (int c = 3; c <= gvFiltroOrigen.Columns.Count - 2; c++)
                {
                    string sNomColumna = gvFiltroOrigen.Columns[c].FieldName.ToString();

                    eCampanha ebCamp = new eCampanha();

                    ebCamp.cod_empresa = cod_empresa;
                    ebCamp.cod_proyecto = cod_proyecto;
                    ebCamp.cod_ejecutivo = gvFiltroOrigen.GetRowCellValue(f, "cod_ejecutivo").ToString();
                    ebCamp.cod_prospecto = "";
                    ebCamp.cod_origen_prospecto = acodigos[c - 3];
                    ebCamp.cnt_gestiones = Convert.ToInt32(gvFiltroOrigen.GetRowCellValue(f, sNomColumna).ToString());
                    ebCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

                    if (ebCamp.cnt_gestiones > 0)
                    {
                        eCamp = unit.Campanha.Modificar_AsignacionProspecto<eCampanha>(0, ebCamp);
                    }

                }
            }

            if (eCamp != null)
            {
                result = "OK";
            }

            SplashScreenManager.CloseForm();
            return result;
        }
        private string Guardar_Configuracion()
        {
            string result = "";
            eCampanha eCamp = new eCampanha();
            int contador = 0;

            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Guardando asignación", "Cargando...");

            for (int i = 3; i <= gvConsolidado.Columns.Count - 1; i++)
            {
                string sNomColumna = gvConsolidado.Columns[i].FieldName.ToString();

                if (Convert.ToInt32(gvConsolidado.Columns[sNomColumna].SummaryItem.SummaryValue.ToString()) != 100)
                {
                    contador = contador + 1;
                }
            }

            if (contador > 0)
            {
                MessageBox.Show("Los totales no cuadran", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "NO";
            }

            //Consolidado
            for (int i = 0; i <= gvConsolidado.RowCount - 1; i++)
            {
                eCampanha ebCamp = new eCampanha();
                ebCamp.cod_empresa = cod_empresa;
                ebCamp.cod_proyecto = cod_proyecto;
                ebCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                ebCamp.cod_ejecutivo = gvConsolidado.GetRowCellValue(i, "cod_ejecutivo").ToString();
                ebCamp.cod_origen_prospecto = "";
                ebCamp.prc_asignacion = Convert.ToInt32(gvConsolidado.GetRowCellValue(i, "prc_asignacion").ToString());
                eCamp = unit.Campanha.Guardar_Actualizar_AsignacionProspectoConf<eCampanha>(ebCamp);
            }

            if (eCamp != null)
            {
                result = "OK";
            }
            SplashScreenManager.CloseForm();
            return result;
        }

        public void CargarListado_Asignaciones()
        {
            try
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                DataSet oDataSet = new DataSet();
                oDataSet = unit.Campanha.ListarPreviewProspectosAsignados(0, cod_empresa, cod_proyecto, Program.Sesion.Usuario.cod_usuario);

                gcResumen.DataSource = oDataSet.Tables[0];

                gcTotales.DataSource= oDataSet.Tables[2];
                gcFiltroOrigen.DataSource = oDataSet.Tables[1];
                if (gvFiltroOrigen.Columns.Count > 0)
                {
                    gvFiltroOrigen.Columns[0].Visible = false;
                    gvFiltroOrigen.Columns[1].Visible = false;
                    gvFiltroOrigen.Columns[2].Caption = "ASESORES";
                    gvFiltroOrigen.Columns[2].OptionsColumn.AllowEdit = false;
                    gvFiltroOrigen.Columns[2].Width = 170;
                    for (int j = 2; j <= gvFiltroOrigen.Columns.Count - 1; j++)
                    {
                        string sNomColumna = gvFiltroOrigen.Columns[j].FieldName.ToString();
                        if (gvFiltroOrigen.Columns[sNomColumna].Summary.Count > 0)
                        {
                            gvFiltroOrigen.Columns[sNomColumna].Summary.RemoveAt(1);
                            gvFiltroOrigen.Columns[sNomColumna].Summary.RemoveAt(0);
                        }

                        if (j==2)
                        {
                            gvFiltroOrigen.Columns[sNomColumna].Summary.Add(SummaryItemType.Custom, sNomColumna, "DIFERENCIAS : ");
                            gvFiltroOrigen.Columns[sNomColumna].Summary.Add(SummaryItemType.Custom, sNomColumna, "TOTALES : ");
                        }
                        else
                        {
                            RepositoryItemTextEdit repositoryItems = new RepositoryItemTextEdit();
                            repositoryItems.AutoHeight = false;
                            repositoryItems.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
                            repositoryItems.MaskSettings.Set("mask", "n0");
                            repositoryItems.Name = "rit_" + sNomColumna;
                            repositoryItems.UseMaskAsDisplayFormat = true;
                            gvFiltroOrigen.Columns[sNomColumna].ColumnEdit = repositoryItems;

                            gvFiltroOrigen.Columns[sNomColumna].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gvFiltroOrigen.Columns[sNomColumna].Summary.Add(SummaryItemType.Custom, sNomColumna, "{0}");
                            gvFiltroOrigen.Columns[sNomColumna].SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, "0");
                            gvFiltroOrigen.Columns[sNomColumna].Summary.Add(DevExpress.Data.SummaryItemType.Sum, sNomColumna, "{0}");
                            //gvFiltroOrigen.Columns[sNomColumna].Caption = sNomColumna + " (" + gvTotales.GetRowCellValue(0, sNomColumna).ToString() + ")";

                            if (oDataSet.Tables[2].Rows.Count>0)
                            {
                                gvFiltroOrigen.Columns[sNomColumna].Caption = sNomColumna + " (" + gvTotales.GetRowCellValue(0, sNomColumna).ToString() + ")";
                            }
                            else
                            {
                                gvFiltroOrigen.Columns[sNomColumna].Caption = sNomColumna + " (0)";
                            }

                            if (j == gvFiltroOrigen.Columns.Count - 1)
                            {
                                gvFiltroOrigen.Columns[sNomColumna].Caption = sNomColumna;
                                gvFiltroOrigen.Columns[sNomColumna].OptionsColumn.AllowEdit = false;
                                gvFiltroOrigen.Columns[sNomColumna].AppearanceCell.BackColor = Color.FromArgb(255, 255, 128);
                            }
                        }
                    }
                    ActualizarTotalLeeds_ColumnaFinal();
                    gvFiltroOrigen.RefreshData();
                }

                //SplashScreenManager.CloseForm();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void CargarListado_Configuracion()
        {
            try
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                List<eCampanha> ListConfiguracion_grilla = new List<eCampanha>();
                ListConfiguracion_grilla = unit.Campanha.ListarAsignacionProspectoConf<eCampanha>(0, cod_empresa, cod_proyecto, Program.Sesion.Usuario.cod_usuario);
                gcConsolidado.DataSource = ListConfiguracion_grilla;

                Guardar_Configuracion();
                //SplashScreenManager.CloseForm();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void ActualizarGrillaResumen()
        {
            
            for (int f = 0; f <= gvFiltroOrigen.RowCount - 1; f++)
            {
                string sCodtempEjecutivo = gvFiltroOrigen.GetRowCellValue(f, "cod_ejecutivo").ToString();
                int SumaxEjecutivo = 0;
                for (int c = 3; c <= gvFiltroOrigen.Columns.Count - 2; c++)
                {
                    string sNomColumna = gvFiltroOrigen.Columns[c].FieldName.ToString();
                    SumaxEjecutivo = SumaxEjecutivo+ Convert.ToInt32(gvFiltroOrigen.GetRowCellValue(f, sNomColumna).ToString());
                }

                for (int j = 0; j <= gvResumen.RowCount - 1; j++)
                {
                    if (gvResumen.GetRowCellValue(j, "cod_ejecutivo").ToString()== sCodtempEjecutivo)
                    {
                        gvResumen.SetRowCellValue(j, "porasignar", SumaxEjecutivo);
                    }
                }
            }


            for (int j = 0; j <= gvResumen.RowCount - 1; j++)
            {
                int total = Convert.ToInt32(gvResumen.GetRowCellValue(j, "asignados").ToString())+ Convert.ToInt32(gvResumen.GetRowCellValue(j, "porasignar").ToString());
                gvResumen.SetRowCellValue(j, "total", total);
            }

        }

        void ActualizarTotalLeeds_ColumnaFinal()
        {
            if (gvFiltroOrigen.RowCount>0)
            {
                for (int ff = 0; ff <= gvFiltroOrigen.RowCount - 1; ff++)
                {
                    int SumaxFilaTotal = 0;
                    for (int cf = 3; cf <= gvFiltroOrigen.Columns.Count - 2; cf++)
                    {
                        string sNomColumna = gvFiltroOrigen.Columns[cf].FieldName.ToString();
                        SumaxFilaTotal = SumaxFilaTotal + Convert.ToInt32(gvFiltroOrigen.GetRowCellValue(ff, sNomColumna).ToString());

                        if (cf == gvFiltroOrigen.Columns.Count - 2)
                        {
                            gvFiltroOrigen.SetRowCellValue(ff, "TOTAL", SumaxFilaTotal);
                        }
                    }
                }
            }
        }
        public bool ValidaTotalesxOrigen(string Columna)
        {
            bool bRespuesta = true;

            int TotalSumaPorAsignar = 0, TotalSumaPorAsignar_leeds = 0,TotalDiferencia=0;
            TotalSumaPorAsignar = Convert.ToInt32(gvResumen.Columns["porasignar"].SummaryItem.SummaryValue.ToString());

            for (int c = 3; c <= gvFiltroOrigen.Columns.Count - 1; c++)
            {
                string sNomColumna = gvFiltroOrigen.Columns[c].FieldName.ToString();
                TotalSumaPorAsignar_leeds = 0;
                for (int f = 0; f <= gvFiltroOrigen.RowCount - 1; f++)
                {
                    string Valor = gvFiltroOrigen.GetRowCellValue(f, sNomColumna).ToString();
                    if (Valor=="") { Valor = "0"; }
                    TotalSumaPorAsignar_leeds = TotalSumaPorAsignar_leeds + Convert.ToInt32(Valor);
                }

                if (Columna == "" || Columna == sNomColumna)
                {
                    TotalDiferencia = Convert.ToInt32(gvTotales.GetRowCellValue(0, sNomColumna).ToString()) - TotalSumaPorAsignar_leeds;
                    gvFiltroOrigen.Columns[Columna].SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, TotalDiferencia.ToString());

                    if (TotalDiferencia != 0)
                    {
                        bRespuesta = false;
                        return bRespuesta;
                    }
                }
            }

            return bRespuesta;
        }


        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevo.Enabled = Enabled;
            btnGuardar.Enabled = Enabled;
        }



        private void gv_campanha_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void gv_campanha_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }


        private void gvFiltroOrigen_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
        private void gvFiltroOrigen_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void gvFiltroOrigen_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.ToString() != "TOTAL")
            {
                CodIndicadorCompleto_xOrigen = "SI";
                if (e.Value.ToString() == "")
                {
                    gvFiltroOrigen.SetRowCellValue(e.RowHandle, e.Column, "0");
                }

                gvFiltroOrigen.RefreshData();


                for (int c = 3; c <= gvFiltroOrigen.Columns.Count - 1; c++)
                {
                    if (gvFiltroOrigen.Columns[c].FieldName.ToString() != "TOTAL")
                    {
                        //e.Column.FieldName.ToString()
                        if (ValidaTotalesxOrigen(gvFiltroOrigen.Columns[c].FieldName.ToString()) == false)
                        {
                            gvFiltroOrigen.Columns[gvFiltroOrigen.Columns[c].FieldName.ToString()].AppearanceCell.ForeColor = Color.Red;
                            CodIndicadorCompleto_xOrigen = "NO";
                        }
                        else
                        {
                            gvFiltroOrigen.Columns[gvFiltroOrigen.Columns[c].FieldName.ToString()].AppearanceCell.ForeColor = Color.Black;
                            //CodIndicadorCompleto_xOrigen = "SI";
                            ActualizarGrillaResumen();
                        }
                    }
                }

                
                ActualizarTotalLeeds_ColumnaFinal();
            }
        }
        private void gvFiltroOrigen_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.Name=="coldsc_ejecutivo")
            {
                e.Appearance.BackColor = Color.Transparent;
            }
        }

        private void gvConsolidado_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
        private void gvConsolidado_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void gvConsolidado_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value.ToString() == "")
            {
                gvConsolidado.SetRowCellValue(e.RowHandle, e.Column, "0");
            }

            gvConsolidado.RefreshData();
            TotalSuma = Convert.ToInt32(gvConsolidado.Columns[e.Column.FieldName.ToString()].SummaryItem.SummaryValue.ToString());

            if (TotalSuma != 100)
            {
                gvConsolidado.Columns[e.Column.FieldName].AppearanceCell.ForeColor = Color.Red;
            }
            else
            {
                gvConsolidado.Columns[e.Column.FieldName].AppearanceCell.ForeColor = Color.Black;
            }
        }


        private void gvResumen_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void gvResumen_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }      


        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gvResumen.RowCount == 0 | gvTotales.RowCount == 0 | gvFiltroOrigen.RowCount == 0)
                {
                    MessageBox.Show("No se encontraron registros.", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string result = "";

                Guardar_Configuracion();
                result = Guardar_Asignacion();

                if (result == "OK")
                {
                    MessageBox.Show("Se asignaron los prospectos de manera satisfactoria", "Asignación de prospectos", MessageBoxButtons.OK);
                    ActualizarListado = "SI";
                    CargarListado_Asignaciones();

                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvListaAsigProspecto.FocusedRowHandle;
                        frmHandler.nEstadoFiltro = nEstadoFiltro;
                        frmHandler.frmListadoAsigProspecto_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        frmHandler.gvListaAsigProspecto.FocusedRowHandle = nRow;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvResumen.RowCount == 0 | gvTotales.RowCount == 0 | gvFiltroOrigen.RowCount == 0)
                {
                    MessageBox.Show("No se encontraron registros.", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gvConsolidado.RowCount > 0)
                {
                    for (int x = gvConsolidado.RowCount - 1; x >= 0; x--)
                    {
                        int cntAsignacion = Convert.ToInt32(gvConsolidado.GetRowCellValue(x, "prc_asignacion"));
                        if (cntAsignacion < 0)
                        {
                            MessageBox.Show("El porcentaje de asignación no debe ser menor a 0.", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                if (TotalSuma != 100) {
                    MessageBox.Show("La suma de los porcentajes de asignación debe dar 100.", "Asignación de prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string result = "";
                result = Guardar_Configuracion();
                if (result == "OK")
                {
                    MessageBox.Show("Se realizo la configuración de manera satisfactoria", "Asignación de prospectos", MessageBoxButtons.OK);
                    ActualizarListado = "SI";
                    CargarListado_Asignaciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}