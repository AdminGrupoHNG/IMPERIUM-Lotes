using BE_GestionLotes;
using DevExpress.Sparkline;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Operaciones
{
    public partial class frmResumenXsemana : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        public List<eLotes_Separaciones.eResumen> listResumenSeparaciones = new List<eLotes_Separaciones.eResumen>();
        public List<eLotes_Separaciones.eResumen> ListSeparacionSemana = new List<eLotes_Separaciones.eResumen>();
        public DateTime dateSelection, oPrimerDiaDelMes, oUltimoDiaDelMes;
        public string cod_proyecto = "";
        //SparklineEdit sparkline = new SparklineEdit();
        public frmResumenXsemana()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void bdListaResumen_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
        private void frmResumenXsemana_Load(object sender, EventArgs e)
        {
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            CargarLookUpEdit();
            dtAño.EditValue = DateTime.Today;
            dtAño.DeselectAll();
            


            //InitializeComponent();

            //gridControl1.DataSource = CreateTable(20);
            //UpdateColors();
            //rSlEdit.LookAndFeel.StyleChanged += LookAndFeel_StyleChanged;


        }
        //void LookAndFeel_StyleChanged(object sender, EventArgs e)
        //{
        //    UpdateColors();
        //}
        //void UpdateColors()
        //{
        //    Color maxPointColor = repositoryItemSparklineEdit1.View.MaxPointColor;
        //    repositoryItemSparklineEdit1.View.MaxPointColor = Color.Red;
        //    //repositoryItemSparklineEdit1.View.MaxPointColor = maxPointColor;

        //    Color minPointColor = repositoryItemSparklineEdit1.View.MinPointColor;
        //    repositoryItemSparklineEdit1.View.MinPointColor = Color.Blue;
        //    //repositoryItemSparklineEdit1.View.MinPointColor = minPointColor;

        //    Color startPointColor = repositoryItemSparklineEdit1.View.StartPointColor;
        //    repositoryItemSparklineEdit1.View.StartPointColor = Color.Green;
        //    //repositoryItemSparklineEdit1.View.StartPointColor = startPointColor;

        //    Color endPointColor = repositoryItemSparklineEdit1.View.EndPointColor;
        //    repositoryItemSparklineEdit1.View.EndPointColor = Color.Blue;
        //    //repositoryItemSparklineEdit1.View.EndPointColor = endPointColor;

        //    Color _color = repositoryItemSparklineEdit1.View.Color;
        //    repositoryItemSparklineEdit1.View.Color = Color.Red;
        //    //repositoryItemSparklineEdit1.View.Color = _color;
        //}
        Color CalculateColor(ColorPickEdit colorPick)
        {
            return (colorPick.Color == colorPick.Properties.AutomaticColor) ? Color.Empty : colorPick.Color;
        }


        private void bdListaResumen_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                eLotes_Separaciones.eResumen obj = bdListaResumen.GetRow(e.RowHandle) as eLotes_Separaciones.eResumen;
                if (e.Column.FieldName == "dsc_mes") { e.Appearance.BackColor = Color.FromArgb(89, 139, 125); e.Appearance.ForeColor = Color.White; }
                if (e.Column.FieldName == "num_cantidad") e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                if (e.Column.FieldName == "imp_sum_separacion") e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                if (e.Column.FieldName == "imp_sum_contado") e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                if (e.Column.FieldName == "imp_venta_total") e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                if (e.Column.FieldName == "imp_sum_credito") e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                if (e.Column.FieldName == "num_sem1") e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                if (e.Column.FieldName == "num_sem2") e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                if (e.Column.FieldName == "num_sem3") e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                if (e.Column.FieldName == "num_sem4") e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                if (e.Column.FieldName == "num_sem5") e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                if (e.Column.FieldName == "num_sem6") e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                if (e.Column.FieldName == "lst_Cantidad_Semana") e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                if (e.Column.FieldName == "imp_sem1") e.Appearance.BackColor = Color.FromArgb(192, 192, 255);
                if (e.Column.FieldName == "imp_sem2") e.Appearance.BackColor = Color.FromArgb(192, 192, 255);
                if (e.Column.FieldName == "imp_sem3") e.Appearance.BackColor = Color.FromArgb(192, 192, 255);
                if (e.Column.FieldName == "imp_sem4") e.Appearance.BackColor = Color.FromArgb(192, 192, 255);
                if (e.Column.FieldName == "imp_sem5") e.Appearance.BackColor = Color.FromArgb(192, 192, 255);
                if (e.Column.FieldName == "imp_sem6") e.Appearance.BackColor = Color.FromArgb(192, 192, 255);
                if (e.Column.FieldName == "lst_Importe_Semana") e.Appearance.BackColor = Color.FromArgb(192, 192, 255);

            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                unit.Proyectos.CargaCombosChecked("Graficos", chkcbEstado, "cod_status", "dsc_status", "");
                chkcbEstado.CheckAll();
                //List<eFacturaProveedor> list = blProv.ListarEmpresasProveedor<eFacturaProveedor>(11, "", user.cod_usuario);
                //if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarListado()
        {
            try
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(splashScreenManager1), "Obteniendo reporte", "Cargando...");
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado...", "Cargando...");
                SplashScreen.Open("Obteniendo listado", "Cargando...");
                decimal imp_sum1 = 0, imp_sum2 = 0, imp_sum3 = 0, imp_sum4 = 0;
                //cod_proyecto = "";
                //string dsc_descripcion = "";
                //string proyectos = "";
                //string etapas = "";

                //var tools = new Tools.TreeListHelper(treeListProyectos);
                //var proyectoMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(1);
                //var etapaMultiple = tools.ObtenerCodigoConcatenadoDeNodoIndex(2);
                //cod_etapasmultiple = etapaMultiple;
                //cod_etapa = etapaMultiple;
                //cod_proyecto = proyectoMultiple;

                listResumenSeparaciones.Clear();
                listResumenSeparaciones = unit.Proyectos.ListarResumenSeparacion<eLotes_Separaciones.eResumen>(cod_proyecto, oPrimerDiaDelMes, oUltimoDiaDelMes, chkcbEstado.EditValue.ToString().Replace(" ", ""));
                DataTable tbl = new DataTable();
                tbl.Columns.Add("dsc_mes", typeof(string));
                tbl.Columns.Add("num_cantidad", typeof(int));
                tbl.Columns.Add("imp_sum_separacion", typeof(double));
                tbl.Columns.Add("imp_sum_contado", typeof(double));
                tbl.Columns.Add("imp_sum_credito", typeof(double));
                tbl.Columns.Add("imp_venta_total", typeof(double));
                tbl.Columns.Add("num_sem1", typeof(int));
                tbl.Columns.Add("num_sem2", typeof(int));
                tbl.Columns.Add("num_sem3", typeof(int));
                tbl.Columns.Add("num_sem4", typeof(int));
                tbl.Columns.Add("num_sem5", typeof(int));
                tbl.Columns.Add("num_sem6", typeof(int));
                tbl.Columns.Add("lst_Cantidad_Semana", typeof(object));
                tbl.Columns.Add("imp_sem1", typeof(double));
                tbl.Columns.Add("imp_sem2", typeof(double));
                tbl.Columns.Add("imp_sem3", typeof(double));
                tbl.Columns.Add("imp_sem4", typeof(double));
                tbl.Columns.Add("imp_sem5", typeof(double));
                tbl.Columns.Add("imp_sem6", typeof(double));
                tbl.Columns.Add("lst_Importe_Semana", typeof(object));

                foreach (eLotes_Separaciones.eResumen obj in listResumenSeparaciones)
                {
                    List<int> values = new List<int>();
                    values.Add(obj.num_sem1);
                    values.Add(obj.num_sem2);
                    values.Add(obj.num_sem3);
                    values.Add(obj.num_sem4);
                    values.Add(obj.num_sem5);
                    values.Add(obj.num_sem6);
                    List<decimal> values2 = new List<decimal>();
                    values2.Add(obj.imp_sem1);
                    values2.Add(obj.imp_sem2);
                    values2.Add(obj.imp_sem3);
                    values2.Add(obj.imp_sem4);
                    values2.Add(obj.imp_sem5);
                    values2.Add(obj.imp_sem6);
                    tbl.Rows.Add(new object[] { obj.dsc_mes, obj.num_cantidad, obj.imp_sum_separacion, obj.imp_sum_contado, obj.imp_sum_credito,obj.imp_venta_total,
                        obj.num_sem1, obj.num_sem2, obj.num_sem3, obj.num_sem4, obj.num_sem5, obj.num_sem6,
                    values, obj.imp_sem1, obj.imp_sem2, obj.imp_sem3, obj.imp_sem4, obj.imp_sem5, obj.imp_sem6, values2  });

                    imp_sum1 += obj.imp_sum_separacion;
                    imp_sum2 += obj.imp_sum_contado;
                    imp_sum3 += obj.imp_sum_credito;
                    imp_sum4 += obj.imp_venta_total;
                }



                gcListaResumen.DataSource = tbl;
                CargarformatConditionRuleDataBar(imp_sum1, imp_sum2, imp_sum3, imp_sum4);
                SplashScreen.Close();
                //SplashScreenManager.CloseForm(false);
                //List<double> values = new List<double>();
                //values.Add(1);
                //values.Add(6);
                //values.Add(4);
                //values.Add(8);
                //values.Add(2);
                //values.Add(1);
                //values.Add(3);




                //listResumenSeparaciones[6].lst_prueba = tbl;

                //bsResumenSeparacion.DataSource = null; bsResumenSeparacion.DataSource = listResumenSeparaciones;
                ////gridView2.DataSource = listResumenSeparaciones;
                //bdListaResumen.RefreshData(); // gridView2.RefreshData();


                //int count = cod_etapasmultiple.Count(f => f == ',');
                ////        lblTitulo.Text = navBarControl1.Groups[0].Caption + ": " + dsc_descripcion;
                //if (count == 0)
                //{

                //    CargarComboEnGrid(2);
                //    CargarListadoEtapas("2");
                //}
                //else
                //{
                //    CargarComboEnGrid(1);
                //    CargarListadoEtapas("1");
                //}


            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //private void bdListaResumen_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    try
        //    {
        //        //if (e.FocusedRowHandle >= 0)
        //        //{
        //        //    eLotes_Separaciones.eResumen obj = bdListaResumen.GetRow(e.FocusedRowHandle) as eLotes_Separaciones.eResumen;
        //        //    bsResumenSemana.DataSource = null;
        //        //}
        //        CargarListadoSemanas(e.FocusedRowHandle + 1);
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        public void CargarformatConditionRuleDataBar(decimal imp_sum1, decimal imp_sum2, decimal imp_sum3, decimal imp_sum4)
        {
            try
            {
                //Grafico de estrellas por mes mas vendido
                bdListaResumen.FormatRules.Clear();
                GridFormatRule gridFormatRule = new GridFormatRule();
                FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                icon1.PredefinedName = "Stars3_1.png";
                icon2.PredefinedName = "Stars3_2.png";
                icon3.PredefinedName = "Stars3_3.png";

                iconSet.ValueType = FormatConditionValueType.Percent;

                icon1.Value = 60;
                icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon2.Value = 1; 
                icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon3.Value = 0; 
                icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                iconSet.Icons.Add(icon1);
                iconSet.Icons.Add(icon2);
                iconSet.Icons.Add(icon3);

                gridFormatRule.Rule = formatConditionRuleIconSet;
                
                gridFormatRule.Column = colnum_cantidad;
               
                bdListaResumen.FormatRules.Add(gridFormatRule);






                ///Gracico de la columna imp_sum_credito
                GridFormatRule gridFormatRule1 = new GridFormatRule();
                FormatConditionRuleDataBar formatConditionRuleDataBar1 = new FormatConditionRuleDataBar();
                gridFormatRule1.Column = colimp_sum_separacion;
                formatConditionRuleDataBar1.PredefinedName = "Coral Gradient";
                formatConditionRuleDataBar1.Maximum = imp_sum1;
                formatConditionRuleDataBar1.MaximumType = FormatConditionValueType.Number;
                gridFormatRule1.Rule = formatConditionRuleDataBar1;
                bdListaResumen.FormatRules.Add(gridFormatRule1);

                ///Gracico de la columna imp_sum_credito
                GridFormatRule gridFormatRule2 = new GridFormatRule();
                FormatConditionRuleDataBar formatConditionRuleDataBar2 = new FormatConditionRuleDataBar();
                gridFormatRule2.Column = colimp_sum_contado;
                formatConditionRuleDataBar2.PredefinedName = "Mint Gradient";
                formatConditionRuleDataBar2.Maximum = imp_sum2;
                formatConditionRuleDataBar2.MaximumType = FormatConditionValueType.Number;
                gridFormatRule2.Rule = formatConditionRuleDataBar2;
                bdListaResumen.FormatRules.Add(gridFormatRule2);

                ///Gracico de la columna imp_sum_credito
                GridFormatRule gridFormatRule3 = new GridFormatRule();
                FormatConditionRuleDataBar formatConditionRuleDataBar3 = new FormatConditionRuleDataBar();
                gridFormatRule3.Column = colimp_sum_credito;
                formatConditionRuleDataBar3.PredefinedName = "Orange Gradient";
                formatConditionRuleDataBar3.Maximum = imp_sum3;
                formatConditionRuleDataBar3.MaximumType = FormatConditionValueType.Number;
                gridFormatRule3.Rule = formatConditionRuleDataBar3;
                bdListaResumen.FormatRules.Add(gridFormatRule3);

                GridFormatRule gridFormatRule4 = new GridFormatRule();
                FormatConditionRuleDataBar formatConditionRuleDataBar4 = new FormatConditionRuleDataBar();
                gridFormatRule4.Column = colimp_venta_total;
                formatConditionRuleDataBar4.PredefinedName = "Green Gradient";
                formatConditionRuleDataBar4.Maximum = imp_sum4;
                formatConditionRuleDataBar4.MaximumType = FormatConditionValueType.Number;
                gridFormatRule4.Rule = formatConditionRuleDataBar4;
                bdListaResumen.FormatRules.Add(gridFormatRule4);

                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarListadoSemanas(int num_mes)
        {
            try
            {

                ListSeparacionSemana = unit.Proyectos.ListarSeparacionSemana<eLotes_Separaciones.eResumen>(cod_proyecto, oPrimerDiaDelMes, oUltimoDiaDelMes, num_mes, chkcbEstado.EditValue.ToString().Replace(" ", ""));
                bsResumenSemana.DataSource = ListSeparacionSemana;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private SparklineEdit CreateSparkline()
        {
            // Create a Sparkline editor and set its dock style.
            SparklineEdit sparkline = new SparklineEdit();
            sparkline.Dock = DockStyle.Fill;

            sparkline.EditValue = CreateData();
            //rSlEdit.View = SparklineViewBase.CreateView((SparklineViewType)bdListaResumen.GetFocusedRow()) as eLotes_Separaciones.eResumen;
            // Create an Area view and assign it to the sparkline.
            AreaSparklineView view = new AreaSparklineView();
            sparkline.Properties.View = view;

            // Customize area appearance.
            view.Color = Color.Aqua;
            view.AreaOpacity = 50;

            // Show markers.
            view.HighlightStartPoint = true;
            view.HighlightEndPoint = true;
            view.HighlightMaxPoint = true;
            view.HighlightMinPoint = true;
            view.HighlightNegativePoints = true;
            view.SetSizeForAllMarkers(10);

            return sparkline;
        }

        private double[] CreateData()
        {
            return new double[] { 2, 4, 5, 1, -1, -2, -1, 2, 4, 5, 6, 3, 5, 4, 8, -1, 6 };
        }

        //private void InitData()
        //{
        //    dataTable1.Rows.Add(0, new double[] { 2, 4, 5, 1, -1, -2, -1, 2, 4, 5, 6, 3, 5, 4, 8, -1, 6 });
        //    dataTable1.Rows.Add(1, new double[] { 0, 5, 2, -1 });
        //}

        private void bdListaResumen_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //if (e.RowHandle == GridControl.AutoFilterRowHandle || e.Column != colnum_sem1 || e.RowHandle == GridControl.InvalidRowHandle)
            //    return;
            //DevExpress.XtraEditors.Repository.RepositoryItem ri = null;
            //GridView view = sender as GridView;
            //int rh = e.RowHandle;
            ////ri = GetRepositoryItem(view, rh);
            //e.RepositoryItem = rSlEdit;
        }

        private DataTable CreateTable(int RowCount)
        {
            List<double> values = new List<double>();
            values.Add(1);
            values.Add(6);
            values.Add(4);
            values.Add(8);
            values.Add(2);
            values.Add(1);
            values.Add(3);
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(object));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, values, DateTime.Now.AddDays(i) });
            return tbl;
        }

        private void pcFechaAño_Click(object sender, EventArgs e)
        {
            dtAño.ShowPopup();
        }

        private void chkcbEstado_EditValueChanged(object sender, EventArgs e)
        {
            if(dtAño.EditValue != null)
            {
                CargarListado();
            }
        }

        private void bdListaResumen_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    //CargarListadoSemanas(e.RowHandle + 1);
                    frmResumenDias frm = new frmResumenDias(this);

                    frm.num_mes = e.RowHandle + 1;
                    //oPrimerDiaDelMes, oUltimoDiaDelMes;


                    eLotes_Separaciones.eResumen oPerfil = listResumenSeparaciones.Find(x => x.num_mes == e.RowHandle + 1);
                    frm.cod_proyecto = cod_proyecto;
                    frm.dsc_mes = oPerfil.dsc_mes;
                    frm.oPrimerDiaDelMes = oPrimerDiaDelMes;
                    frm.oUltimoDiaDelMes = oUltimoDiaDelMes;
                    frm.cod_status = chkcbEstado.EditValue.ToString().Replace(" ", "");
                    //frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtAño_EditValueChanged(object sender, EventArgs e)
        {
            if (dtAño.EditValue == null) return;
            dateSelection = (DateTime)dtAño.EditValue;
            oPrimerDiaDelMes = new DateTime(dateSelection.Year, 1, 1);
            oUltimoDiaDelMes = oPrimerDiaDelMes.AddYears(1).AddDays(-1);
            CargarListado();

        }







        //private DevExpress.XtraEditors.Repository.RepositoryItem GetRepositoryItem(GridView view, int rh)
        //{
        //    DevExpress.XtraEditors.Repository.RepositoryItem ri;
        //    bool isPositive = (bool)view.GetRowCellValue(rh, colIsPositive);
        //    if (isPositive)
        //    {
        //        ri = repositoryItemSparklineEditPositive;
        //    }
        //    else
        //        ri = repositoryItemSparklineEditNegative;
        //    return ri;
        //}
    }
}