using BE_GestionLotes;
using DevExpress.Images;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
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

namespace UI_GestionLotes.Formularios.Marketing
{
    public partial class frmListadoReporteProspecto : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int sTipo = 0;
        int altoEdad = 0, anchoEdad = 0, altoEdad2 = 0, anchoEdad2 = 0;
        int altoSexo = 0, anchoSexo = 0, altoSexo2 = 0, anchoSexo2 = 0;
        int altoCivil = 0, anchoCivil = 0, altoCivil2 = 0, anchoCivil2 = 0;
        int altoProfesion = 0, anchoProfesion = 0, altoProfesion2 = 0, anchoProfesion2 = 0;
        int altoProvincia = 0, anchoProvincia = 0, altoProvincia2 = 0, anchoProvincia2 = 0;
        int altoDistrito = 0, anchoDistrito = 0, altoDistrito2 = 0, anchoDistrito2 = 0;
        private readonly UnitOfWork unit;
        public frmListadoReporteProspecto()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurarFormulario();
        }

        private void frmListadoReporteProspecto_Load(object sender, EventArgs e)
        {
            Inicializar();
            btnBuscar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            ccEdad.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);
        }

        private void Inicializar()
        {
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFchInicio.EditValue = oPrimerDiaDelMes;
            dtFchFin.EditValue = oUltimoDiaDelMes;
            cargarListado();
        }

        void configurarFormulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoProspecto, gvListadoProspecto, editable: false, ShowAutoFilterRow: true);
        }

        private void cargarListado()
        {
            try
            {
                if (rgTipo.EditValue == null)
                {
                    sTipo = 0;
                }
                else
                {
                    sTipo = Convert.ToInt32(rgTipo.EditValue.ToString());
                }

                List<eProspectosXLote> lstEdad = new List<eProspectosXLote>();
                lstEdad = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(1, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
                bsEdad.DataSource = lstEdad;
                //bsListado.DataSource = lstEdad;

                List<eProspectosXLote> lstSexo = new List<eProspectosXLote>();
                lstSexo = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(2, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
                bsSexo.DataSource = lstSexo;

                List<eProspectosXLote> lstEstadoCivil = new List<eProspectosXLote>();
                lstEstadoCivil = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(3, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
                bsEstadoCivil.DataSource = lstEstadoCivil;

                List<eProspectosXLote> lstProfesion = new List<eProspectosXLote>();
                lstProfesion = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(4, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
                bsProfesion.DataSource = lstProfesion;

                List<eProspectosXLote> lstProvincia = new List<eProspectosXLote>();
                lstProvincia = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(5, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
                bsProvincia.DataSource = lstProvincia;

                List<eProspectosXLote> lstDistrito = new List<eProspectosXLote>();
                lstDistrito = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(6, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
                bsDistrito.DataSource = lstDistrito;

                //if (sTipo == 1) { }
                //if (sTipo == 2) {
                //    gvListadoProspecto.Columns["dsc_prospecto"].VisibleIndex = 1;
                //    gvListadoProspecto.Columns["dsc_cantidad_masculino"].Visible = true;
                //    gvListadoProspecto.Columns["dsc_cantidad_masculino"].VisibleIndex = 2;
                //    gvListadoProspecto.Columns["dsc_cantidad_femenino"].Visible = true;
                //    gvListadoProspecto.Columns["dsc_cantidad_femenino"].VisibleIndex = 3;
                //    gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].Visible = true;
                //    gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].VisibleIndex = 4;
                //    gvListadoProspecto.Columns["dsc_cantidad_prospecto"].VisibleIndex = 5;
                //    gvListadoProspecto.Columns["prc_cantidad_prospecto"].VisibleIndex = 6;
                //    List<eProspectosXLote> lstProsSexo = new List<eProspectosXLote>();
                //    lstProsSexo = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(8, "", "00001");
                //    bsListado.DataSource = lstProsSexo;
                //}
                //if (sTipo == 3) {
                //    gvListadoProspecto.Columns["dsc_prospecto"].VisibleIndex = 1;
                //    gvListadoProspecto.Columns["dsc_cantidad_masculino"].Visible = true;
                //    gvListadoProspecto.Columns["dsc_cantidad_masculino"].VisibleIndex = 2;
                //    gvListadoProspecto.Columns["dsc_cantidad_femenino"].Visible = true;
                //    gvListadoProspecto.Columns["dsc_cantidad_femenino"].VisibleIndex = 3;
                //    gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].Visible = true;
                //    gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].VisibleIndex = 4;
                //    gvListadoProspecto.Columns["dsc_cantidad_prospecto"].VisibleIndex = 5;
                //    gvListadoProspecto.Columns["prc_cantidad_prospecto"].VisibleIndex = 6;
                //    List<eProspectosXLote> lstProsSexo = new List<eProspectosXLote>();
                //    lstProsSexo = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(7, "", "00001");
                //    bsListado.DataSource = lstProsSexo;
                //}
                //if (sTipo == 4) { }
                //if (sTipo == 5) { }
                //if (sTipo == 6) { }
            }
            catch (Exception e)
            {
                SplashScreen.Close();
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //try
            //{
            //    List<eProspectosXLote> lstEdades = new List<eProspectosXLote>();
            //    lstEdades = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(1, "", "00001");
            //    bsProspectoEdad.DataSource = lstEdades;

            //    List<eProspectosXLote> lstSexo = new List<eProspectosXLote>();
            //    lstSexo = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(2, "", "00001");
            //    bsProspectoXSexo.DataSource = lstSexo;

            //    List<eProspectosXLote> lstEstadoCivil = new List<eProspectosXLote>();
            //    lstEstadoCivil = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(3, "", "00001");
            //    bsEstadoCivil.DataSource = lstEstadoCivil;

                

            //    List<eProspectosXLote> lstProvincia = new List<eProspectosXLote>();
            //    lstProvincia = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(5, "", "00001");
            //    bsProvincia.DataSource = lstProvincia;

            //    List<eProspectosXLote> lstDistrito = new List<eProspectosXLote>();
            //    lstDistrito  = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(6, "", "00001");
            //    bsDistrito.DataSource = lstDistrito;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarListado();
        }

        private void ocultarCharts()
        {
            gvListadoProspecto.Columns["dsc_cantidad_masculino"].Visible = false;
            gvListadoProspecto.Columns["dsc_cantidad_femenino"].Visible = false;
            gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].Visible = false;
        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lci_filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";
                lci_filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (lci_filtros.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                lci_filtros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                return;
            }
        }

        private void btnListado_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (layoutControlItem9.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                Image img = ImageResourceCache.Default.GetImage("images/Spreadsheet/freezapanes_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Listado";
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                splitterItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (layoutControlItem9.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                Image img = ImageResourceCache.Default.GetImage("images/Spreadsheet/deletesheetcells_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Listado";
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                splitterItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                return;
            }

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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\prospectos" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListadoProspecto.ExportToXlsx(archivo);
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
            gvListadoProspecto.ShowPrintPreview();
        }

        private void ccEdad_MouseEnter(object sender, EventArgs e)
        {
            ccEdad.Size = new System.Drawing.Size(477, 273);
        }

        private void ccEdad_Click(object sender, EventArgs e)
        {
            ocultarCharts();
            List<eProspectosXLote> lstEdad = new List<eProspectosXLote>();
            lstEdad = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(1, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
            bsListado.DataSource = lstEdad;
        }

        private void ccSexo_Click(object sender, EventArgs e)
        {
            ocultarCharts();

            gvListadoProspecto.Columns["dsc_prospecto"].VisibleIndex = 1;
            gvListadoProspecto.Columns["dsc_cantidad_masculino"].Visible = true;
            gvListadoProspecto.Columns["dsc_cantidad_masculino"].VisibleIndex = 2;
            gvListadoProspecto.Columns["dsc_cantidad_femenino"].Visible = true;
            gvListadoProspecto.Columns["dsc_cantidad_femenino"].VisibleIndex = 3;
            gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].Visible = true;
            gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].VisibleIndex = 4;
            gvListadoProspecto.Columns["dsc_cantidad_prospecto"].VisibleIndex = 5;
            gvListadoProspecto.Columns["prc_cantidad_prospecto"].VisibleIndex = 6;
            List<eProspectosXLote> lstProsSexo = new List<eProspectosXLote>();
            lstProsSexo = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(8, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
            bsListado.DataSource = lstProsSexo;
        }

        private void ccEstadoCivil_Click(object sender, EventArgs e)
        {
            ocultarCharts();

            gvListadoProspecto.Columns["dsc_prospecto"].VisibleIndex = 1;
            gvListadoProspecto.Columns["dsc_cantidad_masculino"].Visible = true;
            gvListadoProspecto.Columns["dsc_cantidad_masculino"].VisibleIndex = 2;
            gvListadoProspecto.Columns["dsc_cantidad_femenino"].Visible = true;
            gvListadoProspecto.Columns["dsc_cantidad_femenino"].VisibleIndex = 3;
            gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].Visible = true;
            gvListadoProspecto.Columns["dsc_cantidad_sin_especificar"].VisibleIndex = 4;
            gvListadoProspecto.Columns["dsc_cantidad_prospecto"].VisibleIndex = 5;
            gvListadoProspecto.Columns["prc_cantidad_prospecto"].VisibleIndex = 6;
            List<eProspectosXLote> lstProsSexo = new List<eProspectosXLote>();
            lstProsSexo = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(7, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
            bsListado.DataSource = lstProsSexo;
        }

        private void ccProfesion_Click(object sender, EventArgs e)
        {
            ocultarCharts();
            List<eProspectosXLote> lstProfesion = new List<eProspectosXLote>();
            lstProfesion = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(4, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
            bsListado.DataSource = lstProfesion;
        }

        private void ccProvincia_Click(object sender, EventArgs e)
        {
            ocultarCharts();
            List<eProspectosXLote> lstProvincia = new List<eProspectosXLote>();
            lstProvincia = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(5, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
            bsListado.DataSource = lstProvincia;
        }      

        private void ccDistrito_Click(object sender, EventArgs e)
        {
            ocultarCharts();
            List<eProspectosXLote> lstDistrito = new List<eProspectosXLote>();
            lstDistrito = unit.Campanha.ListarProspectosReporte<eProspectosXLote>(6, "", "00001", fechaInicio: Convert.ToDateTime(dtFchInicio.EditValue).ToString("yyyyMMdd"), fechaFin: Convert.ToDateTime(dtFchFin.EditValue).ToString("yyyyMMdd"));
            bsListado.DataSource = lstDistrito;
        }

        private void ccEdad_MouseHover(object sender, EventArgs e)
        {
            //altoEdad = layoutControlItem1.Size.Height;
            //anchoEdad = layoutControlItem1.Size.Width;

            //altoEdad2 = ccEdad.Size.Height;
            //anchoEdad2 = ccEdad.Size.Width;

            //layoutControlItem1.Size = new System.Drawing.Size(anchoEdad + 100, altoEdad + 100); //919 185
            //ccEdad.Size = new System.Drawing.Size(anchoEdad2 + 100, altoEdad2 + 100);
        }

        private void ccEdad_MouseLeave(object sender, EventArgs e)
        {
            //layoutControlItem1.Size = new System.Drawing.Size(anchoEdad, altoEdad); //919 185
            //ccEdad.Size = new System.Drawing.Size(anchoEdad2, altoEdad2);
        }

        private void ccSexo_MouseHover(object sender, EventArgs e)
        {
            //altoSexo = layoutControlItem2.Size.Height; //238
            //anchoSexo = layoutControlItem2.Size.Width; //919

            //altoSexo2 = ccEdad.Size.Height;  //234
            //anchoSexo2 = ccEdad.Size.Width;  //915

            //layoutControlItem2.Size = new System.Drawing.Size(anchoSexo + 100, altoSexo + 100); //919 185
            //ccSexo.Size = new System.Drawing.Size(anchoSexo2 + 100, altoSexo2 + 100);
        }

        private void ccSexo_MouseLeave(object sender, EventArgs e)
        {
            //layoutControlItem2.Size = new System.Drawing.Size(anchoSexo, altoSexo); //919 185
            //ccSexo.Size = new System.Drawing.Size(anchoSexo2, altoSexo2);
        }

        private void ccEstadoCivil_MouseHover(object sender, EventArgs e)
        {
            //altoCivil = layoutControlItem3.Size.Height; //238
            //anchoCivil = layoutControlItem3.Size.Width; //919

            //altoCivil2 = ccEstadoCivil.Size.Height;  //234
            //anchoCivil2 = ccEstadoCivil.Size.Width;  //915

            //layoutControlItem3.Size = new System.Drawing.Size(anchoCivil + 100, altoCivil + 100); //919 185
            //ccEstadoCivil.Size = new System.Drawing.Size(anchoCivil2 + 100, altoCivil2 + 100);
        }

        private void ccEstadoCivil_MouseLeave(object sender, EventArgs e)
        {
            //layoutControlItem3.Size = new System.Drawing.Size(anchoCivil, altoCivil); //919 185
            //ccEstadoCivil.Size = new System.Drawing.Size(anchoCivil2, altoCivil2);
        }

        private void ccProfesion_MouseHover(object sender, EventArgs e)
        {
            //altoProfesion = layoutControlItem4.Size.Height; //238
            //anchoProfesion = layoutControlItem4.Size.Width; //919

            //altoProfesion2 = ccProfesion.Size.Height;  //234
            //anchoProfesion2 = ccProfesion.Size.Width;  //915

            //layoutControlItem4.Size = new System.Drawing.Size(anchoProfesion + 100, altoProfesion + 100); //919 185
            //ccProfesion.Size = new System.Drawing.Size(anchoProfesion2 + 100, altoProfesion2 + 100);
        }

        private void ccProfesion_MouseLeave(object sender, EventArgs e)
        {
            //layoutControlItem4.Size = new System.Drawing.Size(anchoProfesion, altoProfesion); //919 185
            //ccProfesion.Size = new System.Drawing.Size(anchoProfesion2, altoProfesion2);
        }

        private void ccProvincia_MouseHover(object sender, EventArgs e)
        {
            //altoProvincia = layoutControlItem5.Size.Height; //238
            //anchoProvincia = layoutControlItem5.Size.Width; //919

            //altoProvincia2 = ccProvincia.Size.Height;  //234
            //anchoProvincia2 = ccProvincia.Size.Width;  //915

            //layoutControlItem5.Size = new System.Drawing.Size(anchoProvincia + 100, altoProvincia + 100); //919 185
            //ccProvincia.Size = new System.Drawing.Size(anchoProvincia2 + 100, altoProvincia2 + 100);
        }

        private void ccProvincia_MouseLeave(object sender, EventArgs e)
        {
            //layoutControlItem5.Size = new System.Drawing.Size(anchoProvincia, altoProvincia); //919 185
            //ccProvincia.Size = new System.Drawing.Size(anchoProvincia2, altoProvincia2);
        }

        private void ccDistrito_MouseHover(object sender, EventArgs e)
        {
            //altoDistrito = layoutControlItem6.Size.Height; //319
            //anchoProvincia = layoutControlItem6.Size.Width; //919

            //altoDistrito2 = ccDistrito.Size.Height;  //315
            //anchoDistrito2 = ccDistrito.Size.Width;  //915

            //layoutControlItem6.Size = new System.Drawing.Size(anchoDistrito + 100, altoDistrito + 100); //919 185
            //ccDistrito.Size = new System.Drawing.Size(anchoDistrito2 + 100, altoDistrito2 + 100);
        }

        private void ccDistrito_MouseLeave(object sender, EventArgs e)
        {
            //layoutControlItem6.Size = new System.Drawing.Size(anchoDistrito, altoDistrito); //919 185
            //ccDistrito.Size = new System.Drawing.Size(anchoDistrito2, altoDistrito2);
        }

    }
}