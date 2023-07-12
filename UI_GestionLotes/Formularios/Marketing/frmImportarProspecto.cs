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
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using System.Drawing;
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
using Excel = Microsoft.Office.Interop.Excel;

using System.Data.OleDb;
using UI_GestionLotes.Tools;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;
using System.Globalization;
using System.Data.Common.CommandTrees;
using DevExpress.XtraEditors.Repository;
using UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos;

namespace UI_GestionLotes.Formularios.Lotes
{
    public partial class frmImportarProspecto : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        Brush proceso = Brushes.Red;
        int markWidth = 16;
        frmListadoAsigProspecto frmHandler;
        public eUsuario user = new eUsuario();
        internal campanha MiAccion = campanha.Nuevo;
        List<eCampanha> Listcampanhas = new List<eCampanha>();
        List<eCampanha> ListCanal = new List<eCampanha>();

        private static IEnumerable<eProspectosXLote> lstPuntoContacto;
        private static IEnumerable<eProspectosXLote> lstCanal;
        private static IEnumerable<eProspectosXLote> lstCampanha;
        public string cod_campanha = "", cod_empresa = "", cod_proyecto = "";
        public string ActualizarListado = "NO";
        public string tipoImporte = "";
        public string GrupoSeleccionado = "";
        public string ItemSeleccionado = "";
        public eCampanha o_eCamp;
        int cargados = 0, existentes = 0, errores = 0;
        string strPath = "";
        DataTable oDatosExcel = new DataTable();
        List<eProyecto> listaComboProyectos = new List<eProyecto>();
        List<eVariablesGenerales> lstTipoEvento = new List<eVariablesGenerales>();
        List<eVariablesGenerales> lstTipoEventoValidado = new List<eVariablesGenerales>();
        List<eVariablesGenerales> lstResultado = new List<eVariablesGenerales>();
        List<eVariablesGenerales> lstExitoso = new List<eVariablesGenerales>();
        List<eVariablesGenerales> lstExpectativa = new List<eVariablesGenerales>();
        List<eVariablesGenerales> lstcanal = new List<eVariablesGenerales>();
        List<eVariablesGenerales> lstcodOrigenPros = new List<eVariablesGenerales>();
        List<eVariablesGenerales> lstMotivoNoInteres = new List<eVariablesGenerales>();

        public frmImportarProspecto()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmImportarProspecto(frmListadoAsigProspecto frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmImportarProspecto_Load(object sender, EventArgs e)
        {
            if (tipoImporte == "campanha")
            {
                frmImportarProspecto.ActiveForm.Text = "Importar Campaña";
            }
            if (tipoImporte == "prospecto")
            {
                frmImportarProspecto.ActiveForm.Text = "Importar Prospecto";
            }
            if (tipoImporte == "prospectoNuevo")
            {
                frmImportarProspecto.ActiveForm.Text = "Importar Prospecto";
            }
            if (tipoImporte == "prospectoEjecutivo")
            {
                frmImportarProspecto.ActiveForm.Text = "Importar Prospecto por Ejecutivo";
            }
            grcInformacion.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();
            HabilitarBotones();
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
            CargarComboExcel();
        }
        private void Nuevo()
        {
            LimpiarCampos();
        }
        public string TipoExtension_Excel(string fileExtension, string strPath)
        {
            string conexion = "";

            switch (fileExtension)
            {
                case ".xls":
                    conexion = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={strPath};" + "Extended Properties=\"Excel 8.0; HDR=Yes; IMEX=1\"";
                    break;
                case ".xlsx":
                    conexion = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={strPath};" + "Extended Properties=\"Excel 12.0; HDR=Yes; IMEX=1\"";
                    break;
            }
            return conexion;
        }

        //private class Prospecto_FormatoNuevo
        //{
        //    public string fch_registro { get; set; }
        //    public string created_time { get; set; }
        //    public string ad_id { get; set; }
        //    public string ad_name { get; set; }
        //    public string adset_id { get; set; }
        //    public string adset_name { get; set; }
        //    public string campaign_id { get; set; }
        //    public string campaign_name { get; set; }
        //    public string form_id { get; set; }
        //    public string form_name { get; set; }
        //    public string is_organic { get; set; }
        //    public string platform { get; set; }
        //    public string first_name { get; set; }
        //    public string last_name { get; set; }
        //    public string phone_number { get; set; }
        //    public string cod_mensaje { get; set; }
        //    public string estado { get; set; }
        //}

        private class ProspectoEjecutivo
        {
            public string dsc_telefono { get; set; }
            public string dsc_tipo_prospecto { get; set; }
            public DateTime fch_submission_date { get; set; }
            public string dsc_submission_id { get; set; }
            public string cod_proyecto { get; set; }
            public string dsc_canal { get; set; }
            public string cod_ejecutivo { get; set; }
            public string dsc_documento { get; set; }
            public string dsc_nombre { get; set; }
            public string dsc_apellido { get; set; }
            public DateTime fch_fecha_actual { get; set; }
            public string dsc_hora_actual { get; set; }
            public string dsc_tipo_evento_actual { get; set; }
            public string dsc_resultado { get; set; }
            public string dsc_exitoso { get; set; }
            public string dsc_sin_respuesta { get; set; }
            public string dsc_expectativa { get; set; }
            public string dsc_observacion_actual { get; set; }
            public string dsc_motivo_no_interes { get; set; }
            public DateTime fch_fecha_prox { get; set; }
            public string dsc_hora_prox { get; set; }
            public string dsc_tipo_evento_prox { get; set; }
            public string dsc_observacion_prox { get; set; }
            public string cod_mensaje { get; set; }
            public string dsc_estado { get; set; }
        }

        private class Prospecto
        {
            public string id { get; set; }
            public string created_time { get; set; }
            public string ad_id { get; set; }
            public string ad_name { get; set; }
            public string adset_id { get; set; }
            public string adset_name { get; set; }
            public string campaign_id { get; set; }
            public string campaign_name { get; set; }
            public string form_id { get; set; }
            public string form_name { get; set; }
            public string is_organic { get; set; }
            public string platform { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string phone_number { get; set; }
            public string cod_mensaje { get; set; }
            public string estado { get; set; }
        }

        private class Campanha
        {
            public string inicio_informe { get; set; }
            public string fin_informe { get; set; }
            public string nombre_campanha { get; set; }
            public string entrega_campanha { get; set; }
            public string inicio { get; set; }
            public string fin { get; set; }
            public string presupuesto_anuncios { get; set; }
            public string tipo_presupuesto { get; set; }
            public string configuracion_atribucion { get; set; }
            public string resultado { get; set; }
            public string indicador_resultado { get; set; }
            public string alcance { get; set; }
            public string impresiones { get; set; }
            public string clics_enlace { get; set; }
            public string coste_resultados { get; set; }
            public string importe_gastado { get; set; }
            public string identificador_campanha { get; set; }
            public string frecuencia { get; set; }
            public string cod_mensaje { get; set; }
            public string estado { get; set; }
        }

        private void gvExcel_ocultarFechas(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gvExcel.RowCount > 0)
            {
                if (e.RowHandle >= 0)
                {
                    ProspectoEjecutivo obj = gvExcel.GetRow(e.RowHandle) as ProspectoEjecutivo;
                    if (e.Column.FieldName == "fch_submission_date" && obj.fch_submission_date.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_fecha_actual" && obj.fch_fecha_actual.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_fecha_prox" && obj.fch_fecha_prox.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "1")
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "2")
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "3")
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if(e.Column.FieldName == "dsc_estado" && obj.cod_mensaje == "1")
                    {
                        e.Appearance.BackColor= Color.DarkGreen;
                    }
                    if (e.Column.FieldName == "dsc_estado" && obj.cod_mensaje == "2")
                    {
                        e.Appearance.BackColor = Color.DarkOrange;
                    }
                    if (e.Column.FieldName == "dsc_estado" && obj.cod_mensaje == "3")
                    {
                        e.Appearance.BackColor = Color.DarkRed;
                    }
                }
            }
        }
        private void CargarComboExcel()
        {
            try
            {
                if (tipoImporte == "prospectoEjecutivo")
                {
                    List<ProspectoEjecutivo> prospList = new List<ProspectoEjecutivo>();

                    string ruta = new ReadExcel().ObtenerExcel();
                    if (String.IsNullOrEmpty(ruta)) { return; }
                    var frmExcel = new ExcelSplash(ruta);
                    if (frmExcel.ShowDialog() == DialogResult.OK)
                    {
                        var dt = frmExcel.DtExcel;

                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (String.IsNullOrEmpty(dt.Rows[i][1].ToString())) continue;
                                prospList.Add(new ProspectoEjecutivo()
                                {
                                    fch_submission_date = String.IsNullOrEmpty(dt.Rows[i][0].ToString()) ? DateTime.MinValue : DateTime.ParseExact(dt.Rows[i][0].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                    dsc_submission_id = dt.Rows[i][1] == null ? "" : dt.Rows[i][1].ToString(),
                                    cod_proyecto = dt.Rows[i][2] == null ? "" : dt.Rows[i][2].ToString(),
                                    dsc_canal = dt.Rows[i][3] == null ? "" : dt.Rows[i][3].ToString(),
                                    dsc_telefono = dt.Rows[i][4] == null ? "" : dt.Rows[i][4].ToString(),
                                    cod_ejecutivo = dt.Rows[i][5] == null ? "" : dt.Rows[i][5].ToString(),
                                    dsc_documento = dt.Rows[i][6] == null ? "" : dt.Rows[i][6].ToString(),
                                    dsc_nombre = dt.Rows[i][7] == null ? "" : dt.Rows[i][7].ToString(),
                                    dsc_apellido = dt.Rows[i][8] == null ? "" : dt.Rows[i][8].ToString(),
                                    dsc_tipo_prospecto = dt.Rows[i][9] == null ? "" : dt.Rows[i][9].ToString(),
                                    fch_fecha_actual = String.IsNullOrEmpty(dt.Rows[i][10].ToString()) ? DateTime.MinValue : DateTime.ParseExact(dt.Rows[i][10].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                    dsc_hora_actual = String.IsNullOrEmpty(dt.Rows[i][11].ToString()) ? "00:00:00" : DateTime.ParseExact(dt.Rows[i][11].ToString(), "h:mm tt", CultureInfo.InvariantCulture).ToString("HH:mm:ss"),
                                    dsc_tipo_evento_actual = dt.Rows[i][12] == null ? "" : reemplazoNombre(dt.Rows[i][12].ToString()),
                                    dsc_resultado = dt.Rows[i][13] == null ? "" : validarTipoEvento(reemplazoNombre(dt.Rows[i][13].ToString()), dt.Rows[i][13].ToString()),
                                    dsc_exitoso = dt.Rows[i][14] == null ? "" : dt.Rows[i][14].ToString(),
                                    dsc_sin_respuesta = dt.Rows[i][15] == null ? "" : dt.Rows[i][15].ToString(),
                                    dsc_expectativa = dt.Rows[i][16] == null ? "" : dt.Rows[i][16].ToString(),
                                    dsc_observacion_actual = dt.Rows[i][17] == null ? "" : dt.Rows[i][17].ToString(),
                                    dsc_motivo_no_interes = dt.Rows[i][18] == null ? "" : dt.Rows[i][18].ToString(),
                                    fch_fecha_prox = String.IsNullOrEmpty(dt.Rows[i][19].ToString()) ? DateTime.MinValue : DateTime.ParseExact(dt.Rows[i][19].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                    dsc_hora_prox = String.IsNullOrEmpty(dt.Rows[i][20].ToString()) ? "00:00:00" : DateTime.ParseExact(dt.Rows[i][20].ToString(), "h:mm tt", CultureInfo.InvariantCulture).ToString("HH:mm:ss"),
                                    dsc_tipo_evento_prox = dt.Rows[i][21] == null ? "" : reemplazoNombre(dt.Rows[i][21].ToString()),
                                    dsc_observacion_prox = dt.Rows[i][22] == null ? "" : dt.Rows[i][22].ToString()
                                });
                            }
                            gcExcel.DataSource = prospList.ToList();

                            RepositoryItemLookUpEdit lookUpEdit = new RepositoryItemLookUpEdit();
                            listaComboProyectos = unit.Proyectos.CombosEnGridControl<eProyecto>("Proyecto");
                            lookUpEdit.ValueMember = "cod_proyecto";
                            lookUpEdit.DisplayMember = "dsc_nombre";
                            lookUpEdit.DataSource = listaComboProyectos;

                            gvExcel.Columns["dsc_submission_id"].Visible = false;
                            gvExcel.Columns["dsc_documento"].Visible = false;
                            //gvExcel.Columns["dsc_tipo_prospecto"].Visible = false;
                            gvExcel.Columns["dsc_documento"].Visible = false;
                            //gvExcel.Columns["dsc_fecha_actual"].Visible = false;
                            //gvExcel.Columns["dsc_hora_actual"].Visible = false;
                            //gvExcel.Columns["dsc_resultado"].Visible = false;
                            gvExcel.Columns["dsc_exitoso"].Visible = false;
                            gvExcel.Columns["dsc_expectativa"].Visible = false;
                            gvExcel.Columns["dsc_observacion_actual"].Visible = false;
                            gvExcel.Columns["dsc_motivo_no_interes"].Visible = false;
                            //gvExcel.Columns["dsc_fecha_prox"].Visible = false;
                            //gvExcel.Columns["dsc_hora_prox"].Visible = false;
                            gvExcel.Columns["dsc_observacion_prox"].Visible = false;
                            gvExcel.Columns["dsc_sin_respuesta"].Visible = false;
                            gvExcel.Columns["cod_mensaje"].Visible = false;
                            gvExcel.Columns["dsc_tipo_prospecto"].Caption = "Tipo Prospecto";
                            gvExcel.Columns["dsc_tipo_prospecto"].Width = 100;
                            gvExcel.Columns["dsc_tipo_prospecto"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["dsc_tipo_prospecto"].AppearanceCell.ForeColor = Color.Navy;
                            gvExcel.Columns["dsc_tipo_prospecto"].AppearanceCell.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                            gvExcel.Columns["fch_submission_date"].Caption = "Fecha Prospecto";
                            gvExcel.Columns["fch_submission_date"].Width = 100;
                            gvExcel.Columns["fch_submission_date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gvExcel.Columns["fch_submission_date"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["cod_proyecto"].Caption = "Proyecto";
                            gvExcel.Columns["cod_proyecto"].Width = 100;
                            gvExcel.Columns["cod_proyecto"].ColumnEdit = lookUpEdit;
                            gvExcel.Columns["cod_proyecto"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["dsc_canal"].Caption = "Canal";
                            gvExcel.Columns["dsc_canal"].Width = 100;
                            gvExcel.Columns["dsc_canal"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["cod_ejecutivo"].Caption = "Ejecutivo";
                            gvExcel.Columns["cod_ejecutivo"].Width = 100;
                            gvExcel.Columns["cod_ejecutivo"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["dsc_nombre"].Caption = "Nombres";
                            gvExcel.Columns["dsc_nombre"].Width = 100;
                            gvExcel.Columns["dsc_nombre"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["dsc_apellido"].Caption = "Apellidos";
                            gvExcel.Columns["dsc_apellido"].Width = 100;
                            gvExcel.Columns["dsc_apellido"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["dsc_telefono"].Caption = "Teléfono";
                            gvExcel.Columns["dsc_telefono"].Width = 100;
                            gvExcel.Columns["dsc_telefono"].AppearanceCell.BackColor = Color.FromArgb(255, 224, 192);
                            gvExcel.Columns["fch_fecha_actual"].Caption = "Fecha Evento Actual";
                            gvExcel.Columns["fch_fecha_actual"].Width = 100;
                            gvExcel.Columns["fch_fecha_actual"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gvExcel.Columns["fch_fecha_actual"].AppearanceCell.BackColor = Color.FromArgb(192, 255, 192);
                            gvExcel.Columns["dsc_hora_actual"].Caption = "Hora Evento Actual";
                            gvExcel.Columns["dsc_hora_actual"].Width = 100;
                            gvExcel.Columns["dsc_hora_actual"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gvExcel.Columns["dsc_hora_actual"].AppearanceCell.BackColor = Color.FromArgb(192, 255, 192);
                            gvExcel.Columns["dsc_tipo_evento_actual"].Caption = "Tipo Evento Actual";
                            gvExcel.Columns["dsc_tipo_evento_actual"].Width = 100;
                            gvExcel.Columns["dsc_tipo_evento_actual"].AppearanceCell.BackColor = Color.FromArgb(192, 255, 192);
                            gvExcel.Columns["dsc_resultado"].Caption = "Resultado";
                            gvExcel.Columns["dsc_resultado"].Width = 100;
                            gvExcel.Columns["dsc_resultado"].AppearanceCell.BackColor = Color.FromArgb(192, 255, 192);
                            gvExcel.Columns["fch_fecha_prox"].Caption = "Fecha Evento Próximo";
                            gvExcel.Columns["fch_fecha_prox"].Width = 100;
                            gvExcel.Columns["fch_fecha_prox"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gvExcel.Columns["fch_fecha_prox"].AppearanceCell.BackColor = Color.FromArgb(192, 192, 255);
                            gvExcel.Columns["dsc_hora_prox"].Caption = "Hora Evento Próximo";
                            gvExcel.Columns["dsc_hora_prox"].Width = 100;
                            gvExcel.Columns["dsc_hora_prox"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gvExcel.Columns["dsc_hora_prox"].AppearanceCell.BackColor = Color.FromArgb(192, 192, 255);
                            gvExcel.Columns["dsc_tipo_evento_prox"].Caption = "Tipo Evento Próximo";
                            gvExcel.Columns["dsc_tipo_evento_prox"].Width = 100;
                            gvExcel.Columns["dsc_tipo_evento_prox"].AppearanceCell.BackColor = Color.FromArgb(192, 192, 255);
                            gvExcel.Columns["dsc_estado"].Caption = "E";
                            gvExcel.Columns["dsc_estado"].ToolTip = "Estado";
                            gvExcel.Columns["dsc_estado"].Width = 25;

                            gvExcel.RefreshData();

                            gvExcel.CustomDrawCell += (sender2, e2) => gvExcel_ocultarFechas(sender2, e2);
                        }
                    }
                }
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando prospectos", "Cargando...");
                //var excel = new ReadExcel();
                if (tipoImporte == "prospecto")
                {
                    List<Prospecto> prospList = new List<Prospecto>();

                    string ruta = new ReadExcel().ObtenerExcel();
                    if (String.IsNullOrEmpty(ruta)) { return; }
                    var frmExcel = new ExcelSplash(ruta);
                    if (frmExcel.ShowDialog() == DialogResult.OK)
                    {

                        var dt = frmExcel.DtExcel;

                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                prospList.Add(new Prospecto()
                                {
                                    created_time = dt.Rows[i][1].ToString(),
                                    first_name = dt.Rows[i][12].ToString(),
                                    last_name = dt.Rows[i][13].ToString(),
                                    phone_number = dt.Rows[i][14].ToString(),
                                    campaign_id = dt.Rows[i][6].ToString(),
                                    adset_id = dt.Rows[i][4].ToString(),
                                    adset_name = dt.Rows[i][5].ToString(),
                                });
                            }

                            gcExcel.DataSource = prospList.ToList();

                            gvExcel.Columns["id"].Visible = false;
                            gvExcel.Columns["ad_id"].Visible = false;
                            gvExcel.Columns["ad_name"].Visible = false;
                            gvExcel.Columns["adset_id"].Visible = false;
                            gvExcel.Columns["adset_name"].Visible = false;
                            gvExcel.Columns["campaign_id"].Visible = false;
                            gvExcel.Columns["campaign_name"].Visible = false;
                            gvExcel.Columns["form_id"].Visible = false;
                            gvExcel.Columns["form_name"].Visible = false;
                            gvExcel.Columns["is_organic"].Visible = false;
                            gvExcel.Columns["platform"].Visible = false;
                            gvExcel.Columns["cod_mensaje"].Visible = false;

                            gvExcel.Columns["created_time"].Caption = "Fecha creación";
                            gvExcel.Columns["created_time"].Width = 200;
                            gvExcel.Columns["first_name"].Caption = "Nombres";
                            gvExcel.Columns["first_name"].Width = 230;
                            gvExcel.Columns["last_name"].Caption = "Apellidos";
                            gvExcel.Columns["last_name"].Width = 230;
                            gvExcel.Columns["phone_number"].Caption = "Telefono";
                            gvExcel.Columns["phone_number"].Width = 230;
                            gvExcel.Columns["estado"].Caption = "Estado";
                            gvExcel.Columns["estado"].Width = 50;
                            gvExcel.RefreshData();
                        }



                    }

                    //    var listado = excel.ListarExcel<Prospecto>(out string algo);
                    //    if (listado != null)
                    //    {
                    //        for (int i = 0; i < listado.Count(); i++)
                    //        {
                    //            double inicio = Convert.ToDouble(listado[i].created_time);
                    //            listado[i].created_time = (DateTime.FromOADate(inicio)).ToString();
                    //        }
                    //        gcExcel.DataSource = listado.ToList();
                    //        gvExcel.Columns["created_time"].Visible = true;
                    //        gvExcel.Columns["first_name"].Visible = true;
                    //        gvExcel.Columns["last_name"].Visible = true;
                    //        gvExcel.Columns["phone_number"].Visible = true;
                    //        gvExcel.Columns["id"].Visible = false;
                    //        gvExcel.Columns["ad_id"].Visible = false;
                    //        gvExcel.Columns["ad_name"].Visible = false;
                    //        gvExcel.Columns["adset_id"].Visible = false;
                    //        gvExcel.Columns["adset_name"].Visible = false;
                    //        gvExcel.Columns["campaign_id"].Visible = false;
                    //        gvExcel.Columns["campaign_name"].Visible = false;
                    //        gvExcel.Columns["form_id"].Visible = false;
                    //        gvExcel.Columns["form_name"].Visible = false;
                    //        gvExcel.Columns["is_organic"].Visible = false;
                    //        gvExcel.Columns["platform"].Visible = false;

                    //        gvExcel.Columns["created_time"].Caption = "Fecha creación";
                    //        gvExcel.Columns["created_time"].Width = 200;
                    //        gvExcel.Columns["first_name"].Caption = "Nombres";
                    //        gvExcel.Columns["first_name"].Width = 230;
                    //        gvExcel.Columns["last_name"].Caption = "Apellidos";
                    //        gvExcel.Columns["last_name"].Width = 230;
                    //        gvExcel.Columns["phone_number"].Caption = "Telefono";
                    //        gvExcel.Columns["phone_number"].Width = 230;
                    //    }
                }
                if (tipoImporte == "campanha")
                {
                    layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    eCampanha tmpCamp = new eCampanha();
                    unit.Campanha.CargarCombos_TablasMaestras("1", "proyectos", lkpProyecto, "cod_proyecto", "dsc_proyecto", tmpCamp);
                    unit.Campanha.CargarCombos_TablasMaestras("1", "canal", lkpCanal, "cod_canal", "dsc_canal", tmpCamp);

                    List<Campanha> campList = new List<Campanha>();

                    string ruta = new ReadExcel().ObtenerExcel();
                    if (String.IsNullOrEmpty(ruta)) { LimpiarCampos(); return; }
                    var frmExcel = new ExcelSplash(ruta);
                    if (frmExcel.ShowDialog() == DialogResult.OK)
                    {

                        var dt = frmExcel.DtExcel;

                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                campList.Add(new Campanha()
                                {
                                    nombre_campanha = dt.Rows[i][2].ToString(),
                                    entrega_campanha = dt.Rows[i][3].ToString(),
                                    inicio = dt.Rows[i][4].ToString(),
                                    fin = dt.Rows[i][5].ToString(),
                                    identificador_campanha = dt.Rows[i][16].ToString(),
                                });
                            }

                            gcExcel.DataSource = campList.ToList();
                            gvExcel.Columns["inicio_informe"].Visible = false;
                            gvExcel.Columns["fin_informe"].Visible = false;
                            gvExcel.Columns["nombre_campanha"].Visible = true;
                            gvExcel.Columns["entrega_campanha"].Visible = true;
                            gvExcel.Columns["inicio"].Visible = true;
                            gvExcel.Columns["fin"].Visible = true;
                            gvExcel.Columns["presupuesto_anuncios"].Visible = false;
                            gvExcel.Columns["tipo_presupuesto"].Visible = false;
                            gvExcel.Columns["configuracion_atribucion"].Visible = false;
                            gvExcel.Columns["resultado"].Visible = false;
                            gvExcel.Columns["indicador_resultado"].Visible = false;
                            gvExcel.Columns["alcance"].Visible = false;
                            gvExcel.Columns["impresiones"].Visible = false;
                            gvExcel.Columns["clics_enlace"].Visible = false;
                            gvExcel.Columns["coste_resultados"].Visible = false;
                            gvExcel.Columns["importe_gastado"].Visible = false;
                            gvExcel.Columns["identificador_campanha"].Visible = true;
                            gvExcel.Columns["frecuencia"].Visible = false;
                            gvExcel.Columns["cod_mensaje"].Visible = false;

                            gvExcel.Columns["nombre_campanha"].Caption = "Nombre Campaña";
                            gvExcel.Columns["nombre_campanha"].Width = 200;
                            gvExcel.Columns["entrega_campanha"].Caption = "Entrega Campaña";
                            gvExcel.Columns["entrega_campanha"].Width = 230;
                            gvExcel.Columns["inicio"].Caption = "Inicio";
                            gvExcel.Columns["inicio"].Width = 230;
                            gvExcel.Columns["fin"].Caption = "Fin";
                            gvExcel.Columns["fin"].Width = 230;
                            gvExcel.Columns["identificador_campanha"].Caption = "Identificador Campaña";
                            gvExcel.Columns["identificador_campanha"].Width = 230;
                            gvExcel.Columns["estado"].Caption = "Estado";
                            gvExcel.Columns["estado"].Width = 50;
                            gvExcel.RefreshData();
                        }



                    }
                }
                //    var listado = excel.ListarExcel<Campanha>(out string algo);
                //    if (listado != null)
                //    {
                //        for (int i = 0; i < listado.Count(); i++)
                //        {
                //            double inicio = Convert.ToDouble(listado[i].inicio);
                //            listado[i].inicio = (DateTime.FromOADate(inicio)).ToString("dd/MM/yyyy");

                //            double fin = Convert.ToDouble(listado[i].fin);
                //            listado[i].fin = (DateTime.FromOADate(fin)).ToString("dd/MM/yyyy");
                //        }

                //        gcExcel.DataSource = listado.ToList();
                //        gvExcel.Columns["inicio_informe"].Visible = false;
                //        gvExcel.Columns["fin_informe"].Visible = false;
                //        gvExcel.Columns["nombre_campanha"].Visible = true;
                //        gvExcel.Columns["entrega_campanha"].Visible = true;
                //        gvExcel.Columns["inicio"].Visible = true;
                //        gvExcel.Columns["fin"].Visible = true;
                //        gvExcel.Columns["presupuesto_anuncios"].Visible = false;
                //        gvExcel.Columns["tipo_presupuesto"].Visible = false;
                //        gvExcel.Columns["configuracion_atribucion"].Visible = false;
                //        gvExcel.Columns["resultado"].Visible = false;
                //        gvExcel.Columns["indicador_resultado"].Visible = false;
                //        gvExcel.Columns["alcance"].Visible = false;
                //        gvExcel.Columns["impresiones"].Visible = false;
                //        gvExcel.Columns["clics_enlace"].Visible = false;
                //        gvExcel.Columns["coste_resultados"].Visible = false;
                //        gvExcel.Columns["importe_gastado"].Visible = false;
                //        gvExcel.Columns["identificador_campanha"].Visible = true;
                //        gvExcel.Columns["frecuencia"].Visible = false;

                //        gvExcel.Columns["nombre_campanha"].Caption = "Nombre Campaña";
                //        gvExcel.Columns["nombre_campanha"].Width = 200;
                //        gvExcel.Columns["entrega_campanha"].Caption = "Entrega Campaña";
                //        gvExcel.Columns["entrega_campanha"].Width = 230;
                //        gvExcel.Columns["inicio"].Caption = "Inicio";
                //        gvExcel.Columns["inicio"].Width = 230;
                //        gvExcel.Columns["fin"].Caption = "Fin";
                //        gvExcel.Columns["fin"].Width = 230;
                //        gvExcel.Columns["identificador_campanha"].Caption = "Identificador Campaña";
                //        gvExcel.Columns["identificador_campanha"].Width = 230;
                //    }
                //SplashScreenManager.CloseForm();

                if (tipoImporte == "prospectoNuevo")
                {
                    layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    rlkpAsesor.DataSource = unit.Campanha.ListarEjecutivosVentasMenu<eUsuario>(1, "", cod_proyecto, Program.Sesion.Usuario.cod_usuario);
                    lstPuntoContacto = unit.Campanha.CombosEnGridControl<eProspectosXLote>("Punto Contacto");
                    rlkpPuntoContacto.DataSource = lstPuntoContacto;
                    lstCanal = unit.Campanha.CombosEnGridControl<eProspectosXLote>("Canal");
                    rlkpCanal.DataSource = lstCanal;
                    lstCampanha = unit.Campanha.CombosEnGridControl<eProspectosXLote>("Campanha");
                    rlkpCampanha.DataSource = lstCampanha;

                    //layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //eCampanha tmpCamp = new eCampanha();
                    //unit.Campanha.CargarCombos_TablasMaestras("1", "proyectos", lkpProyecto, "cod_proyecto", "dsc_proyecto", tmpCamp);
                    //unit.Campanha.CargarCombos_TablasMaestras("1", "canal", lkpCanal, "cod_canal", "dsc_canal", tmpCamp);

                    List<eProspectosXLote> prosList = new List<eProspectosXLote>();

                    string ruta = new ReadExcel().ObtenerExcel();
                    if (String.IsNullOrEmpty(ruta)) { LimpiarCampos(); return; }
                    var frmExcel = new ExcelSplash(ruta);
                    if (frmExcel.ShowDialog() == DialogResult.OK)
                    {

                        var dt = frmExcel.DtExcel;

                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()) && !string.IsNullOrEmpty(dt.Rows[i][3].ToString()) &&
                                    !string.IsNullOrEmpty(dt.Rows[i][4].ToString()) && (validarNumeroTelefonico(dt.Rows[i][9].ToString()) == 0 && validarCantidadNumerosCadena(dt.Rows[i][9].ToString()) >= 9)/*!string.IsNullOrEmpty(dt.Rows[i][8].ToString())*/)
                                {
                                    prosList.Add(new eProspectosXLote()
                                    {
                                        fch_fecha = dt.Rows[i][0].ToString(),
                                        cod_campanha = dt.Rows[i][1].ToString(),
                                        cod_referencia_campanha = dt.Rows[i][2].ToString(),
                                        cod_canal = dt.Rows[i][3].ToString(),
                                        cod_origen_prospecto = dt.Rows[i][4].ToString(),
                                        cod_segmento = dt.Rows[i][5].ToString(),
                                        dsc_segmento = dt.Rows[i][6].ToString(),
                                        dsc_apellido_paterno = dt.Rows[i][7].ToString(),
                                        dsc_nombres = dt.Rows[i][8].ToString(),
                                        dsc_telefono_movil = dt.Rows[i][9].ToString(),
                                        dsc_observacion = dt.Rows[i][10].ToString(),
                                        cod_ejecutivo = dt.Rows[i][11].ToString(),
                                    });
                                    if (!string.IsNullOrEmpty(prosList[i].cod_canal))
                                    {
                                        prosList[i].cod_canal = obtenerCanal(dt.Rows[i][3].ToString());
                                        if (!string.IsNullOrEmpty(prosList[i].cod_origen_prospecto))
                                        {
                                            prosList[i].cod_origen_prospecto = obtenerPuntoContacto(prosList[i].cod_canal, dt.Rows[i][4].ToString());
                                        }
                                    }
                                    else
                                    {
                                        prosList[i].cod_canal = null;
                                        prosList[i].cod_origen_prospecto = null;
                                    }
                                }
                            }

                            bsProspectos.DataSource = prosList;

                            //gcExcel.DataSource = prosList.ToList();
                            gvProspectoExcel.RefreshData();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.Message);
            }

            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Excel Archivo(.xls)|*.xls| Excel Archivo(.xlsx)| *.xlsx";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando prospectos", "Cargando...");
            //        txt_ruta.Text = ofd.FileName;

            //        strPath = ofd.FileName;
            //        string connStr = TipoExtension_Excel(Path.GetExtension(ofd.FileName), strPath);

            //        OleDbConnection conn = new OleDbConnection(connStr);
            //        conn.Open();

            //        DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

            //        string[] strTableNames = new string[dtSheetName.Rows.Count];
            //        for (int k = 0; k < dtSheetName.Rows.Count; k++)
            //        {
            //            strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
            //            glkpHojas.Properties.Items.Add(strTableNames[k]);
            //        }
            //        conn.Close();

            //        if (glkpHojas.Properties.Items.Count>0)
            //        {
            //            glkpHojas.SelectedIndex = 0;
            //        }
            //        SplashScreenManager.CloseForm();
            //        //OleDbDataAdapter myCommand = null;
            //        //DataTable dt = new DataTable();
            //        //// Datos de consulta del nombre de la tabla especificado, primero puede enumerar todos los nombres de las tablas a la selección de usuarios
            //        //string strExcel = "select * from [" + strTableNames[0] + "]";
            //        //myCommand = new OleDbDataAdapter(strExcel, connStr);
            //        //myCommand.Fill(dt);
            //        //gcExcel.DataSource = dt;
            //    }
            //    catch (Exception ex)
            //    {
            //        SplashScreenManager.CloseForm();
            //        MessageBox.Show(ex.Message);
            //    }

        }

        private int validarNumeroTelefonico(string campoValidar)
        {
            string str = campoValidar;
            List<string> chars = new List<string>();
            chars.AddRange(str.Select(c => c.ToString()));

            int nueves = 0;
            int ceros = 0;
            foreach (var a in chars)
            {
                if (a == "9") nueves += 1;
                if (a == "0") ceros += 1;

            }
            if (nueves >= 9) { return nueves; }
            if (ceros >= 9) { return ceros; }

            return 0;
        }

        private int validarCantidadNumerosCadena(string campoValidar)
        {
            string str = campoValidar;
            List<string> chars = new List<string>();
            chars.AddRange(str.Select(c => c.ToString()));


            int n = 0;
            foreach (var a in chars)
            {
                int[] k = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (int b in k)
                {
                    if (b.ToString() == a) n += 1;
                }
            }
            return n;
        }

        public bool Valida_Formatos()
        {
            bool bResultado = true;
            if (rgOrigen.EditValue.ToString() == "F")
            {
                bResultado = Valida_FormatoFormulario();
            }
            else if (rgOrigen.EditValue.ToString() == "O")
            {
                bResultado = Valida_FormatoOtros();
            }
            return bResultado;
        }
        public bool Valida_FormatoFormulario()
        {
            bool bRespuesta = true;

            if (oDatosExcel.Rows.Count > 0)
            {
                if (oDatosExcel.Columns.Count != 15)
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[0].ColumnName != "id")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[1].ColumnName != "created_time")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[2].ColumnName != "ad_id")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[3].ColumnName != "ad_name")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[4].ColumnName != "adset_id")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[5].ColumnName != "adset_name")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[6].ColumnName != "campaign_id")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[7].ColumnName != "campaign_name")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[8].ColumnName != "form_id")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[9].ColumnName != "form_name")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[10].ColumnName != "is_organic")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[11].ColumnName != "platform")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[12].ColumnName != "first_name")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[13].ColumnName != "last_name")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[14].ColumnName != "phone_number")
                {
                    bRespuesta = false;
                }
            }
            else
            {
                bRespuesta = false;
            }

            return bRespuesta;
        }
        public bool Valida_FormatoOtros()
        {
            bool bRespuesta = true;

            if (oDatosExcel.Rows.Count > 0)
            {
                if (oDatosExcel.Columns.Count != 18)
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[0].ColumnName != "Inicio del informe")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[1].ColumnName != "Fin del informe")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[2].ColumnName != "Nombre de la campaña")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[3].ColumnName != "Entrega de la campaña")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[4].ColumnName != "Inicio")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[5].ColumnName != "Fin")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[6].ColumnName != "Presupuesto del conjunto de anuncios")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[7].ColumnName != "Tipo de presupuesto del conjunto de anuncios")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[8].ColumnName != "Configuración de atribución")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[9].ColumnName != "Resultados")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[10].ColumnName != "Indicador de resultado")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[11].ColumnName != "Alcance")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[12].ColumnName != "Impresiones")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[13].ColumnName != "Clics en el enlace")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[14].ColumnName != "Coste por resultados")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[15].ColumnName != "Importe gastado (PEN)")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[16].ColumnName != "Identificador de la campaña")
                {
                    bRespuesta = false;
                }
                else if (oDatosExcel.Columns[17].ColumnName != "Frecuencia")
                {
                    bRespuesta = false;
                }
            }
            else
            {
                bRespuesta = false;
            }

            return bRespuesta;
        }


        void Formatea_Columnas()
        {
            if (rgOrigen.EditValue.ToString() == "F")
            {
                Formatea_Columnas_Formulario();
            }
            else if (rgOrigen.EditValue.ToString() == "O")
            {

            }
        }
        void Formatea_Columnas_Formulario()
        {
            for (int i = 0; i <= gvExcel.Columns.Count - 1; i++)
            {
                gvExcel.Columns[i].Visible = false;
            }
            gvExcel.Columns["created_time"].Visible = true;
            //gvExcel.Columns["campaign_id"].Visible = true;
            gvExcel.Columns["first_name"].Visible = true;
            gvExcel.Columns["last_name"].Visible = true;
            gvExcel.Columns["phone_number"].Visible = true;

            gvExcel.Columns["created_time"].Caption = "Fecha creación";
            gvExcel.Columns["created_time"].Width = 200;
            //gvExcel.Columns["campaign_id"].Caption = "Campañas";
            //gvExcel.Columns["campaign_id"].Width = 120;
            gvExcel.Columns["first_name"].Caption = "Nombres";
            gvExcel.Columns["first_name"].Width = 230;
            gvExcel.Columns["last_name"].Caption = "Apellidos";
            gvExcel.Columns["last_name"].Width = 230;
            gvExcel.Columns["phone_number"].Caption = "Telefono";
            gvExcel.Columns["phone_number"].Width = 230;

        }

        private void LimpiarCampos()
        {
            gvExcel.Columns.Clear();
            gcExcel.DataSource = null;
            //gcProspectoExcel.DataSource = null;
            //bsProspectos.DataSource = null;
            bsProspectos.DataSource = null;
            gvProspectoExcel.RefreshData();

            glkpHojas.Properties.Items.Clear();
            glkpHojas.Text = "";
            glkpHojas.EditValue = "";
            cargados = 0; existentes = 0; errores = 0;
            txtCargados.EditValue = 0; txtExistentes.EditValue = 0; txtErrores.EditValue = 0;

            lkpTipoCampanha.EditValue = null;
            lkpCanal.EditValue = null;
            lkpResponsable.EditValue = null;
            lkpProyecto.EditValue = null;

        }
        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnNuevo.Enabled = Enabled;
            btnGuardar.Enabled = Enabled;
        }
        private string Guardar()
        {
            string result = "";
            if (gvExcel.RowCount == 0 && gvProspectoExcel.RowCount == 0) { MessageBox.Show("No se encontraron registros para guardar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); return ""; }
            if (tipoImporte == "prospectoNuevo")
            {
                txtExistentes.Focus();
                eProspectosXLote ePro = AsignarValoresProspectoNuevo();
                if (ePro != null)
                {
                    result = "OK";
                }
            }
            else if (tipoImporte == "prospectoEjecutivo")
            {
                eCampanha eCamp = AsignarValoresProspectoEjecutivo();
                if (eCamp != null)
                {
                    result = "OK";
                }
            }
            else
            {
                eCampanha eCamp = tipoImporte == /*"prospectoNuevo" ? AsignarValoresProspectoNuevo() : tipoImporte ==*/ "campanha" ? AsignarValoresCampanha() : AsignarValoresProspecto();
                if (eCamp != null)
                {
                    //cod_prospecto = eCamp.cod_prospecto;
                    lkpProyecto.EditValue = null;
                    lkpResponsable.EditValue = null;
                    lkpTipoCampanha.EditValue = null;
                    result = "OK";
                }
            }

            return result;
        }

        private eProspectosXLote AsignarValoresProspectoNuevo()
        {
            eProspectosXLote eCampResul = new eProspectosXLote();
            eProspectosXLote eCodSeg = new eProspectosXLote();
            string codigoSegmento = "", nombreSegmento = "";
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando prospectos", "Cargando...");
            for (int i = 0; i <= gvProspectoExcel.RowCount - 1; i++)
            {
                if (gvProspectoExcel.GetRowCellValue(i, "cod_canal") == null || gvProspectoExcel.GetRowCellValue(i, "cod_origen_prospecto") == null)
                {
                    return null;
                }
                if (gvProspectoExcel.GetRowCellValue(i, "cod_canal").ToString() == "" || gvProspectoExcel.GetRowCellValue(i, "cod_origen_prospecto").ToString() == "")
                {
                    return null;
                }
            }
            for (int i = 0; i <= gvProspectoExcel.RowCount - 1; i++)
            {
                eProspectosXLote eCamp = new eProspectosXLote();

                eCamp.cod_empresa = cod_empresa;
                eCamp.cod_proyecto = cod_proyecto;
                eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

                eCamp.fch_fecha = gvProspectoExcel.GetRowCellValue(i, "fch_fecha").ToString();
                eCamp.cod_campanha = gvProspectoExcel.GetRowCellValue(i, "cod_campanha").ToString();
                eCamp.cod_referencia_campanha = gvProspectoExcel.GetRowCellValue(i, "cod_referencia_campanha").ToString();
                eCamp.dsc_nombres = gvProspectoExcel.GetRowCellValue(i, "dsc_nombres").ToString();
                eCamp.dsc_apellido_paterno = gvProspectoExcel.GetRowCellValue(i, "dsc_apellido_paterno").ToString();
                eCamp.dsc_telefono_movil = gvProspectoExcel.GetRowCellValue(i, "dsc_telefono_movil").ToString();
                eCamp.dsc_observacion = gvProspectoExcel.GetRowCellValue(i, "dsc_observacion").ToString();
                eCamp.cod_canal = gvProspectoExcel.GetRowCellValue(i, "cod_canal").ToString();
                eCamp.cod_origen_prospecto = gvProspectoExcel.GetRowCellValue(i, "cod_origen_prospecto").ToString();
                eCamp.cod_segmento = gvProspectoExcel.GetRowCellValue(i, "cod_segmento").ToString();
                eCamp.cod_ejecutivo = gvProspectoExcel.GetRowCellValue(i, "cod_ejecutivo").ToString();

                if (!string.IsNullOrEmpty(gvProspectoExcel.GetRowCellValue(i, "cod_segmento").ToString()))
                {
                    if (codigoSegmento != gvProspectoExcel.GetRowCellValue(i, "cod_segmento").ToString())
                    {
                        //codigoSegmento = gvExcel.GetRowCellValue(i, "adset_id").ToString();
                        eCodSeg.fch_fecha = eCamp.fch_fecha;
                        eCodSeg.cod_segmento = gvProspectoExcel.GetRowCellValue(i, "cod_segmento").ToString();
                        eCodSeg.dsc_segmento = gvProspectoExcel.GetRowCellValue(i, "dsc_segmento").ToString();
                        eCodSeg.cod_usuario = Program.Sesion.Usuario.cod_usuario;

                        eCodSeg = unit.Campanha.Importar_Prospectos<eProspectosXLote>(eCodSeg, "2");

                        codigoSegmento = eCodSeg.cod_segmento;
                        nombreSegmento = eCodSeg.dsc_segmento;
                        //eCamp.cod_segmento = eCodSeg.cod_segmento;
                        //eCamp.dsc_segmento = eCodSeg.dsc_segmento;
                    }
                }
                else
                {
                    nombreSegmento = "";
                    codigoSegmento = "";
                }

                eCamp.cod_segmento = codigoSegmento;
                eCamp.dsc_segmento = nombreSegmento;

                eCampResul = unit.Campanha.Importar_Prospectos<eProspectosXLote>(eCamp, "1");

                if (eCampResul != null)
                {
                    if (eCampResul.cod_mensaje == 1) { gvProspectoExcel.SetRowCellValue(i, "cod_mensaje", "1"); cargados = cargados + 1; }
                    if (eCampResul.cod_mensaje == 2) { gvProspectoExcel.SetRowCellValue(i, "cod_mensaje", "2"); existentes = existentes + 1; }
                    if (eCampResul.cod_mensaje == 3) { gvProspectoExcel.SetRowCellValue(i, "cod_mensaje", "3"); errores = errores + 1; }
                }
                if (eCampResul == null) { gvProspectoExcel.SetRowCellValue(i, "cod_mensaje", "3"); errores = errores++; }


            }
            return eCampResul;
        }

        private string reemplazoNombre(string dsc_tipo_evento)
        {
            if (dsc_tipo_evento == "CITA OFICINA/CASA") { dsc_tipo_evento = "CITA OFICINA"; }
            if (dsc_tipo_evento == "VISITA") { dsc_tipo_evento = "CITA TERRENO"; }
            return dsc_tipo_evento;
        }

        private string validarTipoEvento(string dsc_tipo_evento, string respuesta)
        {
            if (lstTipoEventoValidado.Count == 0)
            {
                lstTipoEventoValidado = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("6");
                if (lstTipoEventoValidado.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstTipoEventoValidado.Find(x => x.dsc_Nombre == dsc_tipo_evento);
                    if (oListCanal == null) { return respuesta; }
                    if (respuesta == "EXITOSO") { respuesta = "ASISTIO"; } else { respuesta = "NO ASISTIO"; }
                    return respuesta;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstTipoEventoValidado.Find(x => x.dsc_Nombre == dsc_tipo_evento);
                if (oListCanal == null) { return respuesta; }
                if (respuesta == "EXITOSO") { respuesta = "ASISTIO"; } else { respuesta = "NO ASISTIO"; }
                return respuesta;
            }
            return respuesta;
        }
        private string valueTipoEventoContacto(string dsc_tipo_evento)
        {
            if (lstTipoEvento.Count == 0)
            {
                lstTipoEvento = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("1");
                if (lstTipoEvento.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstTipoEvento.Find(x => x.dsc_Nombre == dsc_tipo_evento);
                    if (oListCanal == null) { return ""; }
                    return oListCanal.cod_variable;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstTipoEvento.Find(x => x.dsc_Nombre == dsc_tipo_evento);
                if (oListCanal == null) { return ""; }
                return oListCanal.cod_variable;
            }
        }

        private string valueDetalleRespuesta(string dsc_exitoso)
        {
            if (lstExitoso.Count == 0)
            {
                lstExitoso = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("3");
                if (lstExitoso.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstExitoso.Find(x => x.dsc_Nombre == dsc_exitoso);
                    if (oListCanal == null) { return ""; }
                    return oListCanal.cod_variable;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstExitoso.Find(x => x.dsc_Nombre == dsc_exitoso);
                if (oListCanal == null) { return ""; }
                return oListCanal.cod_variable;
            }
        }

        private string valueTipoRespuesta(string dsc_tipo_evento)
        {

            if (lstResultado.Count == 0)
            {
                lstResultado = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("2");
                if (lstResultado.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstResultado.Find(x => x.dsc_Nombre == dsc_tipo_evento);
                    if (oListCanal == null) { return ""; }
                    return oListCanal.cod_variable;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstResultado.Find(x => x.dsc_Nombre == dsc_tipo_evento);
                if (oListCanal == null) { return ""; }
                return oListCanal.cod_variable;
            }
        }

        private string valueMotivoNoInteres(string dsc_motivo)
        {
            if (lstMotivoNoInteres.Count == 0)
            {
                lstMotivoNoInteres = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("8");
                if (lstMotivoNoInteres.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstMotivoNoInteres.Find(x => x.dsc_Nombre == dsc_motivo.Trim());
                    if (oListCanal == null) { return ""; }
                    return oListCanal.cod_variable;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstMotivoNoInteres.Find(x => x.dsc_Nombre == dsc_motivo.Trim());
                if (oListCanal == null) { return ""; }
                return oListCanal.cod_variable;
            }
        }

        private string valueExpectativa(string dsc_expecativa)
        {
            if (lstExpectativa.Count == 0)
            {
                lstExpectativa = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("4");
                if (lstExpectativa.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstExpectativa.Find(x => x.dsc_Nombre == dsc_expecativa);
                    if (oListCanal == null) { return ""; }
                    return oListCanal.cod_variable;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstExpectativa.Find(x => x.dsc_Nombre == dsc_expecativa);
                if (oListCanal == null) { return ""; }
                return oListCanal.cod_variable;
            }
        }

        private string valueOrigenProspecto(string cod_canal)
        {
            if (lstcodOrigenPros.Count == 0)
            {
                lstcodOrigenPros = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("7");
                if (lstcodOrigenPros.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstcodOrigenPros.Find(x => x.valor_1 == cod_canal);
                    if (oListCanal == null) { return ""; }
                    return oListCanal.cod_variable;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstcodOrigenPros.Find(x => x.valor_1 == cod_canal);
                if (oListCanal == null) { return ""; }
                return oListCanal.cod_variable;
            }
        }

        private string valueCanal(string dsc_canal)
        {
            if (lstcanal.Count == 0)
            {
                lstcanal = unit.Campanha.ListarCombosImportarProspectos<eVariablesGenerales>("5");
                if (lstcanal.Count == 0) { return ""; }
                else
                {
                    eVariablesGenerales oListCanal = new eVariablesGenerales();
                    oListCanal = lstcanal.Find(x => x.dsc_Nombre == dsc_canal);
                    if (oListCanal == null) { return ""; }
                    return oListCanal.cod_variable;
                }
            }
            else
            {
                eVariablesGenerales oListCanal = new eVariablesGenerales();
                oListCanal = lstcanal.Find(x => x.dsc_Nombre == dsc_canal);
                if (oListCanal == null) { return ""; }
                return oListCanal.cod_variable;
            }
        }

        private DateTime valueFechaEvento(DateTime fechaActual, string horaActual)
        {
            string fechaHora = fechaActual.ToString("dd-MM-yyyy");
            DateTime fecha = Convert.ToDateTime(fechaHora);
            DateTime hora = DateTime.ParseExact(horaActual, "HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime fechaConvertida = fecha.AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second);

            return fechaConvertida;
        }
        private eCampanha agregarEventoActual(string cod_prospecto, eProspectosXLote ePro)
        {
            eCampanha eCamp = new eCampanha();
            eCampanha eReg = new eCampanha();
            eCamp.cod_evento = "";
            eCamp.cod_evento_ref = "";
            eCamp.cod_empresa = ePro.cod_empresa;
            eCamp.cod_proyecto = ePro.cod_proyecto;
            eCamp.cod_prospecto = cod_prospecto;
            eCamp.cod_tipo_evento = "TPEVN001";
            eCamp.fch_evento = valueFechaEvento(ePro.fch_fecha_actual, ePro.dsc_hora_actual);
            eCamp.cod_tipo_contacto = valueTipoEventoContacto(ePro.dsc_tipo_evento_actual);
            eCamp.cod_respuesta = valueTipoRespuesta(ePro.dsc_resultado);
            eCamp.cod_detalle_respuesta = valueTipoRespuesta(ePro.dsc_resultado) == "RECON001" || valueTipoRespuesta(ePro.dsc_resultado) == "RECON004" ? valueDetalleRespuesta(ePro.dsc_exitoso) : valueDetalleRespuesta(ePro.dsc_sin_respuesta);
            eCamp.cod_expectativa = valueExpectativa(ePro.dsc_expectativa);
            eCamp.cod_motivo = valueMotivoNoInteres(ePro.cod_motivo);
            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            eCamp.flg_receptivo = "NO";
            eCamp.dsc_observacion = ePro.dsc_observacion1;
            eCamp.cod_ejecutivo_cita = ePro.cod_ejecutivo;
            eReg = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp, "0");
            return eReg;
        }

        private eCampanha agregarEventoProx(string cod_prospecto, eCampanha ecamp, eProspectosXLote ePro, int validarActualNuevo = 0)
        {
            eCampanha eCamp = new eCampanha();
            eCampanha eReg = new eCampanha();
            eCamp.cod_evento = "";
            eCamp.cod_evento_ref = ecamp.cod_evento;
            eCamp.cod_evento_principal = validarActualNuevo == 0 ? ecamp.cod_evento : ecamp.cod_evento_ref;
            eCamp.cod_empresa = ePro.cod_empresa;
            eCamp.cod_proyecto = ePro.cod_proyecto;
            eCamp.cod_prospecto = cod_prospecto;
            eCamp.cod_tipo_evento = "TPEVN002";
            eCamp.fch_evento = valueFechaEvento(ePro.fch_fecha_prox, ePro.dsc_hora_prox);
            eCamp.cod_tipo_contacto = valueTipoEventoContacto(ePro.dsc_tipo_evento_prox);
            eCamp.cod_respuesta = "";
            eCamp.cod_detalle_respuesta = ""; // valueDetalleRespuesta(ePro.dsc_exitoso);
            eCamp.cod_expectativa = ""; // valueExpectativa(ePro.dsc_expectativa);
            eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            eCamp.flg_receptivo = "NO";
            eCamp.cod_ejecutivo_cita = "";
            eCamp.dsc_observacion = ePro.dsc_observacion2;
            eCamp.cod_ejecutivo_cita = ePro.cod_ejecutivo;
            eCamp.cod_motivo = "";
            eReg = unit.Campanha.Guardar_Actualizar_Eventos<eCampanha>(eCamp, "2");
            return eReg;


        }

        private eCampanha AsignarValoresProspectoEjecutivo()
        {
            eCampanha eCampResul = new eCampanha();
            string codigoSegmento = "", nombreSegmento = "", valorSegmento = "";
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando prospectos", "Cargando...");
            foreach (int i in Enumerable.Range(0, gvExcel.RowCount).Reverse())
            {
                eProspectosXLote eCamp = new eProspectosXLote();
                eProyecto oPerfil = listaComboProyectos.Find(x => x.cod_proyecto == gvExcel.GetRowCellValue(i, "cod_proyecto").ToString());
                if (oPerfil == null) { continue; }
                eCamp.fch_registro = (DateTime)gvExcel.GetRowCellValue(i, "fch_submission_date");
                eCamp.cod_canal = valueCanal(gvExcel.GetRowCellValue(i, "dsc_canal").ToString().Trim());
                eCamp.cod_proyecto = gvExcel.GetRowCellValue(i, "cod_proyecto").ToString();
                eCamp.cod_empresa = oPerfil.cod_empresa;
                eCamp.dsc_telefono_movil = gvExcel.GetRowCellValue(i, "dsc_telefono").ToString();
                eCamp.cod_ejecutivo = gvExcel.GetRowCellValue(i, "cod_ejecutivo").ToString();
                eCamp.cod_origen_prospecto = valueOrigenProspecto(valueCanal(gvExcel.GetRowCellValue(i, "dsc_canal").ToString().Trim()));
                eCamp.dsc_num_documento = gvExcel.GetRowCellValue(i, "dsc_documento").ToString();
                eCamp.dsc_nombres = gvExcel.GetRowCellValue(i, "dsc_nombre").ToString();
                eCamp.dsc_apellido_paterno = gvExcel.GetRowCellValue(i, "dsc_apellido").ToString();
                eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                eCamp.num_tipo_estado_prospecto = gvExcel.GetRowCellValue(i, "dsc_tipo_prospecto").ToString().ToUpper() == "NUEVO" ? 0 : 1;
                eCampResul = unit.Campanha.Importar_Prospectos<eCampanha>(eCamp, "3");

                if (eCampResul != null)
                {
                    if (eCampResul.cod_mensaje == 1) { gvExcel.SetRowCellValue(i, "cod_mensaje", "1"); cargados = cargados + 1; }
                    if (eCampResul.cod_mensaje == 2) { gvExcel.SetRowCellValue(i, "cod_mensaje", "2"); existentes = existentes + 1; }
                    if (eCampResul.cod_mensaje == 3) { gvExcel.SetRowCellValue(i, "cod_mensaje", "3"); errores = errores + 1; }
                    if (!gvExcel.GetRowCellValue(i, "fch_fecha_actual").ToString().Contains("1/01/0001") || !String.IsNullOrEmpty(gvExcel.GetRowCellValue(i, "fch_fecha_actual").ToString()))
                    {
                        eCamp.fch_fecha_actual = (DateTime)gvExcel.GetRowCellValue(i, "fch_fecha_actual");
                        eCamp.dsc_hora_actual = gvExcel.GetRowCellValue(i, "dsc_hora_actual").ToString();
                        eCamp.dsc_tipo_evento_actual = gvExcel.GetRowCellValue(i, "dsc_tipo_evento_actual").ToString();
                        eCamp.dsc_resultado = gvExcel.GetRowCellValue(i, "dsc_resultado").ToString();
                        eCamp.dsc_exitoso = gvExcel.GetRowCellValue(i, "dsc_exitoso").ToString();
                        eCamp.dsc_expectativa = gvExcel.GetRowCellValue(i, "dsc_expectativa").ToString();
                        eCamp.dsc_observacion1 = gvExcel.GetRowCellValue(i, "dsc_observacion_actual").ToString();
                        eCamp.cod_motivo = gvExcel.GetRowCellValue(i, "dsc_motivo_no_interes").ToString();
                        eCampanha eventoActual = agregarEventoActual(eCampResul.cod_prospecto, eCamp);
                        if (eventoActual != null && (!gvExcel.GetRowCellValue(i, "fch_fecha_prox").ToString().Contains("1/01/0001") || !String.IsNullOrEmpty(gvExcel.GetRowCellValue(i, "fch_fecha_prox").ToString())))
                        {
                            eCamp.fch_fecha_prox = (DateTime)gvExcel.GetRowCellValue(i, "fch_fecha_prox");
                            eCamp.dsc_hora_prox = gvExcel.GetRowCellValue(i, "dsc_hora_prox").ToString();
                            eCamp.dsc_tipo_evento_prox = gvExcel.GetRowCellValue(i, "dsc_tipo_evento_prox").ToString();
                            eCamp.dsc_observacion2 = gvExcel.GetRowCellValue(i, "dsc_observacion_prox").ToString();
                            
                            eCampanha eventoProx = agregarEventoProx(eCampResul.cod_prospecto, eventoActual, eCamp, gvExcel.GetRowCellValue(i, "dsc_tipo_prospecto").ToString().ToUpper() == "NUEVO" ? 0 : 1);
                            if (eventoProx != null)
                            {

                            }
                        }
                    }
                }
                if (eCampResul == null) { gvExcel.SetRowCellValue(i, "cod_mensaje", "3"); errores = errores++; }


            }
            SplashScreenManager.CloseForm();
            return eCampResul;
        }

        private eCampanha AsignarValoresProspecto()
        {
            eCampanha eCampResul = new eCampanha();
            string codigoSegmento = "", nombreSegmento = "", valorSegmento = "";
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando prospectos", "Cargando...");
            for (int i = 0; i <= gvExcel.RowCount - 1; i++)
            {
                eCampanha eCamp = new eCampanha();

                eCamp.cod_empresa = cod_empresa;
                eCamp.cod_proyecto = cod_proyecto;
                eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;

                eCamp.fch_fecha = gvExcel.GetRowCellValue(i, "created_time").ToString();
                eCamp.cod_referencia_campanha = gvExcel.GetRowCellValue(i, "campaign_id").ToString();
                eCamp.dsc_nombres = gvExcel.GetRowCellValue(i, "first_name").ToString();
                eCamp.dsc_apellido_paterno = gvExcel.GetRowCellValue(i, "last_name").ToString();
                eCamp.dsc_telefono_movil = gvExcel.GetRowCellValue(i, "phone_number").ToString();

                if (!string.IsNullOrEmpty(gvExcel.GetRowCellValue(i, "adset_id").ToString()))
                {
                    if (valorSegmento != gvExcel.GetRowCellValue(i, "adset_id").ToString())
                    {
                        eCampanha eCodSeg = new eCampanha();
                        //codigoSegmento = gvExcel.GetRowCellValue(i, "adset_id").ToString();
                        eCodSeg.fch_fecha = eCamp.fch_fecha;
                        eCodSeg.cod_segmento = gvExcel.GetRowCellValue(i, "adset_id").ToString();
                        eCodSeg.dsc_segmento = gvExcel.GetRowCellValue(i, "adset_name").ToString();
                        eCodSeg.cod_usuario = Program.Sesion.Usuario.cod_usuario;

                        eCodSeg = unit.Campanha.Importar_Prospectos<eCampanha>(eCodSeg, "2");
                        valorSegmento = eCodSeg.dsc_valor_segmento;
                        codigoSegmento = eCodSeg.cod_segmento;
                        nombreSegmento = eCodSeg.dsc_segmento;
                        //eCamp.cod_segmento = eCodSeg.cod_segmento;
                        //eCamp.dsc_segmento = eCodSeg.dsc_segmento;
                    }
                }
                else
                {
                    valorSegmento = "";
                    nombreSegmento = "";
                    codigoSegmento = "";
                }

                eCamp.cod_segmento = codigoSegmento;
                eCamp.dsc_segmento = nombreSegmento;

                eCampResul = unit.Campanha.Importar_Prospectos<eCampanha>(eCamp, "1");

                if (eCampResul != null)
                {
                    if (eCampResul.cod_mensaje == 1) { gvExcel.SetRowCellValue(i, "cod_mensaje", "1"); cargados = cargados + 1; }
                    if (eCampResul.cod_mensaje == 2) { gvExcel.SetRowCellValue(i, "cod_mensaje", "2"); existentes = existentes + 1; }
                    if (eCampResul.cod_mensaje == 3) { gvExcel.SetRowCellValue(i, "cod_mensaje", "3"); errores = errores + 1; }
                }
                if (eCampResul == null) { gvExcel.SetRowCellValue(i, "cod_mensaje", "3"); errores = errores++; }
            }
            SplashScreenManager.CloseForm();
            return eCampResul;
        }

        private eCampanha AsignarValoresCampanha()
        {
            eCampanha eCampResul = new eCampanha();
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando prospectos", "Cargando...");
            for (int i = 0; i <= gvExcel.RowCount - 1; i++)
            {
                eCampanha eCamp = new eCampanha();
                string[] aCodigos = lkpProyecto.EditValue.ToString().Split("|".ToCharArray());
                if (aCodigos.Length > 1)
                {
                    eCamp.cod_empresa = aCodigos[0];
                    eCamp.cod_proyecto = aCodigos[1];
                }
                else
                {
                    eCamp.cod_empresa = "";
                    eCamp.cod_proyecto = "";
                }



                eCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
                eCamp.cod_moneda = "SOL";
                eCamp.cod_responsable = lkpResponsable.EditValue.ToString();
                eCamp.cod_canal = lkpCanal.EditValue.ToString();
                eCamp.cod_tipo_campanha = lkpTipoCampanha.EditValue.ToString();
                eCamp.flg_activo_excel = gvExcel.GetRowCellValue(i, "entrega_campanha").ToString();
                eCamp.fch_inicio_campanha = gvExcel.GetRowCellValue(i, "inicio").ToString();
                eCamp.fch_fin_campanha = gvExcel.GetRowCellValue(i, "fin").ToString();
                eCamp.dsc_campanha = gvExcel.GetRowCellValue(i, "nombre_campanha").ToString();
                eCamp.dsc_descripcion = gvExcel.GetRowCellValue(i, "nombre_campanha").ToString();
                eCamp.cod_referencia_campanha = gvExcel.GetRowCellValue(i, "identificador_campanha").ToString();



                eCampResul = unit.Campanha.Guardar_Actualizar_campanha<eCampanha>(eCamp, "Nuevo");

                if (eCampResul != null)
                {
                    if (eCampResul.cod_mensaje == 1) { gvExcel.SetRowCellValue(i, "cod_mensaje", "1"); cargados = cargados + 1; }
                    if (eCampResul.cod_mensaje == 2) { gvExcel.SetRowCellValue(i, "cod_mensaje", "2"); existentes = existentes + 1; }
                }
                if (eCampResul == null) { gvExcel.SetRowCellValue(i, "cod_mensaje", "3"); errores = errores++; }
            }
            SplashScreenManager.CloseForm();
            return eCampResul;
        }


        private void gvExcel_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void glkpHojas_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string strConn = TipoExtension_Excel(Path.GetExtension(txt_ruta.Text), strPath);

                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                OleDbDataAdapter myCommand = null;
                DataTable dt = new DataTable();
                string strExcel = "select * from [" + glkpHojas.EditValue.ToString() + "]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(dt);
                oDatosExcel = dt;
                conn.Close();

                if (Valida_Formatos() == true)
                {
                    gcExcel.DataSource = dt;
                    Formatea_Columnas();
                }
                else
                {
                    MessageBox.Show("Formato de excel incorrecto", "Importar prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarCampos();
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void rgOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarComboExcel();
        }

        private void btnCargarexcel_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarComboExcel();
        }

        private void gvExcel_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }





        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LimpiarCampos();
        }

        private void lkpProyecto_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpProyecto.EditValue != null)
            {
                CargarCombosResponsableCampaña(lkpResponsable);
            }
        }

        private void gvExcel_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (tipoImporte == "prospecto")
            //    {
            //        eProspectosXLote obj = gvProspectoExcel.GetRow(e.RowHandle) as eProspectosXLote;
            //        if (obj.cod_mensaje == 4)
            //        {
            //            Brush b;
            //            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //            e.Appearance.ForeColor = Color.Red; e.Appearance.FontStyleDelta = FontStyle.Bold;
            //            b = proceso;
            //            //b = ConCriterios;
            //            if (e.Column.FieldName == "proceso") { e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth)); }
            //            if (e.Column.FieldName == "estado") { e.Appearance.BackColor = Color.OrangeRed; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }
            //        }
            //    }
            //}
        }

        private void gvExcel_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (tipoImporte == "campanha")
                {
                    Campanha obj = gvExcel.GetRow(e.RowHandle) as Campanha;
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "1")
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "2")
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "3")
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }
                if (tipoImporte == "prospecto")
                {
                    Prospecto obj = gvExcel.GetRow(e.RowHandle) as Prospecto;
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "1")
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "2")
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == "3")
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }


                //eLotesxProyecto obj = gvExcel.GetRow(e.RowHandle) as eLotesxProyecto;
                //if (e.Column.FieldName == "cod_tipo_lote" && obj.cod_tipo_lote == null) e.Appearance.BackColor = Color.LightSalmon;
                ////if (e.Column.FieldName == "cod_status" && obj.cod_status == "STL00004") e.Appearance.BackColor = Color.LightYellow;
                //if (e.Column.FieldName == "imp_precio_m_cuadrado") e.Appearance.BackColor = Color.LightSalmon;
                //if (e.Column.FieldName == "imp_precio_total") e.Appearance.BackColor = Color.LightSalmon;

            }
        }

        private void gvProspectoExcel_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvProspectoExcel_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void rlkpCanal_EditValueChanged(object sender, EventArgs e)
        {
            this.gvProspectoExcel.PostEditor();
            this.gvProspectoExcel.SetFocusedRowCellValue("cod_origen_prospecto", null);
        }

        private void gvProspectoExcel_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;
            if (view.FocusedColumn.FieldName == "cod_origen_prospecto")
            {
                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                string cod_canal = Convert.ToString(view.GetFocusedRowCellValue("cod_canal"));
                editor.Properties.DataSource = obtenerSedes(cod_canal);
            }
        }

        public static List<eProspectosXLote> obtenerSedes(string cod_canal)
        {
            return lstPuntoContacto.Where(c => c.cod_canal == cod_canal).ToList();
        }
        public string obtenerCanal(string dsc_canal)
        {
            try
            {
                return lstCanal.Where(c => c.dsc_canal == dsc_canal).First().cod_canal.ToString();
            }
            catch
            {
                return null;
            }
        }
        public string obtenerPuntoContacto(string cod_canal, string dsc_origen_prospecto)
        {
            try
            {
                return lstPuntoContacto.Where(c => c.dsc_tipo_campanha == dsc_origen_prospecto && c.cod_canal == cod_canal).First().cod_tipo_campanha.ToString();
            }
            catch
            {
                return null;
            }
        }

        private void gvProspectoExcel_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {

                if (tipoImporte == "prospectoNuevo")
                {
                    eProspectosXLote obj = gvProspectoExcel.GetRow(e.RowHandle) as eProspectosXLote;
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == 1)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == 2)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                    if (e.Column.FieldName == "estado" && obj.cod_mensaje == 3)
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }

            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();

            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            //objExcel.Visible = true;

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Opciones";

                objExcel.Cells[1, 1] = "Cod. Campaña";
                objExcel.Cells[1, 2] = "Descripción Campaña";
                objExcel.Cells[1, 4] = "Canal";
                objExcel.Cells[1, 6] = "Punto de contacto";
                objExcel.Cells[1, 8] = "Cod. Segmento";
                objExcel.Cells[1, 9] = "Segmento";

                string procedure = "";
                int fila = 2;
                string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
                string server = unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
                string bd = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
                string user = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
                string pass = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());
                string AppName = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("AppName")].ToString());

                string cnxl = "ODBC;DRIVER=SQL Server;SERVER=" + server + ";UID=" + user + ";PWD=" + pass + ";APP=SGI_Excel;DATABASE=" + bd + "";
                procedure = "usp_lte_consultasvarias_lotes  @opcion = '27', @flg_activo = 'SI'";
                unit.Campanha.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                if (fila > 1) objExcel.Rows[fila].Delete();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                //objExcel.Range["A2:A" + fila].Delete();

                procedure = "usp_lte_consultasvarias_lotes  @opcion = '17'";
                fila = 2;
                unit.Campanha.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "D" + fila, true);
                if (fila > 1) objExcel.Rows[fila].Delete();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                objExcel.Range["D2:D" + fila].Delete();

                procedure = "usp_lte_consultasvarias_lotes  @opcion = '24'";
                fila = 2;
                unit.Campanha.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "F" + fila, true);
                if (fila > 1) objExcel.Rows[fila].Delete();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

                procedure = $"usp_lte_consultasvarias_lotes  @opcion = '22', @cod_proyecto = '{cod_empresa + "|" + cod_proyecto}'";
                fila = 2;
                unit.Campanha.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "H" + fila, true);
                if (fila > 1) objExcel.Rows[fila].Delete();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                //objExcel.Range["I2:I" + fila].Insert();

                objExcel.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGreen;
                objExcel.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbWhite;
                objExcel.Cells[1, 1].ColumnWidth = 26;
                objExcel.Cells[1, 1].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                objExcel.Cells[1, 1].EntireRow.Font.Bold = true;
                objExcel.Cells[1, 1].Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                for (int i = 2; i <= 9; i += 2)
                {
                    objExcel.Cells[1, i].Interior.Color = Excel.XlRgbColor.rgbGreen;
                    objExcel.Cells[1, i].Font.Color = Excel.XlRgbColor.rgbWhite;
                    objExcel.Cells[1, i].ColumnWidth = i == 2 ? 35 : 26;
                    objExcel.Cells[1, i].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    objExcel.Cells[1, i].EntireRow.Font.Bold = true;
                    objExcel.Cells[1, i].Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                }
                for (int i = 3; i <= 9; i += 2)
                {
                    objExcel.Cells[1, i].ColumnWidth = 1;
                    if (i > 8)
                    {
                        objExcel.Cells[1, i].Interior.Color = Excel.XlRgbColor.rgbGreen;
                        objExcel.Cells[1, i].Font.Color = Excel.XlRgbColor.rgbWhite;
                        objExcel.Cells[1, i].ColumnWidth = 35;
                        objExcel.Cells[1, i].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        objExcel.Cells[1, i].EntireRow.Font.Bold = true;
                        objExcel.Cells[1, i].Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);
                    }
                }

                //objExcel.ActiveWindow.DisplayGridlines = false;
                //objExcel.Range["A:A"].ColumnWidth = 3; objExcel.Range["B:B"].ColumnWidth = 3; objExcel.Range["C:C"].ColumnWidth = 5; objExcel.Range["D:D"].ColumnWidth = 44;
                //objExcel.Cells[6, 2] = "Cliente:"; objExcel.Cells[6, 4] = txtCliente.EditValue.ToString();
                //objExcel.Cells[7, 2] = "Dirección:"; objExcel.Cells[7, 4] = eDir.dsc_cadena_direccion;
                //objExcel.Cells[8, 2] = "Servicio:"; objExcel.Cells[8, 4] = lkpTipoServicio.Text;
                //objExcel.Range["D6:D8"].Select(); objExcel.Selection.Font.Bold = true;


                objExcel.Sheets.Add();
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Prospectos";
                objExcel.Cells[1, 1] = "Fecha del Informe";
                objExcel.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbRed;
                objExcel.Cells[1, 2] = "Cod. Campaña";
                objExcel.Cells[1, 2].Interior.Color = Excel.XlRgbColor.rgbGreen;
                objExcel.Cells[1, 3] = "Cód. Referencia Campaña";
                objExcel.Cells[1, 3].Interior.Color = Excel.XlRgbColor.rgbGreen;
                objExcel.Cells[1, 4] = "Canal";
                objExcel.Cells[1, 4].Interior.Color = Excel.XlRgbColor.rgbRed;
                objExcel.Cells[1, 5] = "Punto de contacto";
                objExcel.Cells[1, 5].Interior.Color = Excel.XlRgbColor.rgbGreen;                
                objExcel.Cells[1, 6] = "Cod. Segmento";
                objExcel.Cells[1, 6].Interior.Color = Excel.XlRgbColor.rgbGreen;
                objExcel.Cells[1, 7] = "Segmento";
                objExcel.Cells[1, 7].Interior.Color = Excel.XlRgbColor.rgbGreen;
                objExcel.Cells[1, 8] = "Apellidos";
                objExcel.Cells[1, 8].Interior.Color = Excel.XlRgbColor.rgbRed;
                objExcel.Cells[1, 9] = "Nombres";
                objExcel.Cells[1, 9].Interior.Color = Excel.XlRgbColor.rgbRed;
                objExcel.Cells[1, 10] = "Teléfono móvil";
                objExcel.Cells[1, 10].Interior.Color = Excel.XlRgbColor.rgbRed;
                objExcel.Cells[1, 11] = "Observación";
                objExcel.Cells[1, 11].Interior.Color = Excel.XlRgbColor.rgbGreen;
                objExcel.Cells[1, 12] = "Asesor";
                objExcel.Cells[1, 12].Interior.Color = Excel.XlRgbColor.rgbGreen;
                for (int i = 1; i <= 12; i++)
                {
                    objExcel.Cells[1, i].Font.Color = Excel.XlRgbColor.rgbWhite;
                    objExcel.Cells[1, i].ColumnWidth = 24;
                    objExcel.Cells[1, i].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    objExcel.Cells[1, i].EntireRow.Font.Bold = true;
                }

                objExcel.Range["A1:L1"].Select(); objExcel.Selection.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.FromArgb(0, 0, 0);


                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvProspectoExcel_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle >= 0)
            //    {

            //        if (tipoImporte == "prospectoNuevo")
            //        {
            //            eProspectosXLote obj = gvProspectoExcel.GetRow(e.RowHandle) as eProspectosXLote;
            //            if (obj.cod_mensaje == 4)
            //            {
            //                Brush b; 
            //                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //                e.Appearance.ForeColor = Color.Red; e.Appearance.FontStyleDelta = FontStyle.Bold;
            //                b = proceso; 
            //                //b = ConCriterios;
            //                if(e.Column.FieldName == "proceso") { e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth)); }
            //                if (e.Column.FieldName == "estado") { e.Appearance.BackColor = Color.OrangeRed; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }
            //            }
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void CargarCombosResponsableCampaña(LookUpEdit combo)
        {
            DataTable tabla = new DataTable();
            string[] aCodigos = lkpProyecto.EditValue.ToString().Split("|".ToCharArray());
            if (aCodigos.Length > 1)
            {
                cod_empresa = aCodigos[0];
            }
            else
            {
                cod_empresa = "";
            }

            tabla = unit.Campanha.ObtenerListadoGridLookup("responsablecampanha", Program.Sesion.Usuario.cod_usuario, cod_empresa);
            unit.Globales.CargarCombosGridLookup(tabla, combo, "cod_responsable", "dsc_responsable", "", false);
        }

        private void lkpCanal_EditValueChanged(object sender, EventArgs e)
        {
            eCampanha tmpCamp = new eCampanha();
            tmpCamp.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            tmpCamp.cod_empresa = cod_empresa;
            tmpCamp.cod_proyecto = "";
            tmpCamp.valor_1 = lkpCanal.EditValue == null ? "" : lkpCanal.EditValue.ToString();
            lkpTipoCampanha.EditValue = null;
            unit.Campanha.CargarCombos_TablasMaestras("1", "tipo_campanha", lkpTipoCampanha, "cod_tipo_campanha", "dsc_tipo_campanha", tmpCamp);
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (txtcampanha.Text.Trim() == "") { MessageBox.Show("Debe ingresar ell nombre de la campaña", "Registro de campaña", MessageBoxButtons.OK, MessageBoxIcon.Error); txtcampanha.Focus(); return; }
                cargados = 0;
                existentes = 0;
                errores = 0;

                string result = "";
                if (tipoImporte == "campanha")
                {
                    if (lkpProyecto.EditValue == null) { MessageBox.Show("Seleccione un proyecto", "Importar Campañas", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpProyecto.Focus(); return; }
                    if (lkpResponsable.EditValue == null) { MessageBox.Show("Seleccione un responsable", "Importar Campañas", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpResponsable.Focus(); return; }
                    if (lkpCanal.EditValue == null) { MessageBox.Show("Seleccione un canal", "Importar Campañas", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpCanal.Focus(); return; }
                    if (lkpTipoCampanha.EditValue == null) { MessageBox.Show("Seleccione un punto de contacto", "Importar Campañas", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpTipoCampanha.Focus(); return; }
                }
                result = Guardar();

                if (result == "OK")
                {
                    if (tipoImporte == "campanha") { MessageBox.Show("Se importaron las campañas de manera satisfactoria", "Importar campañas", MessageBoxButtons.OK); }
                    if (tipoImporte == "prospecto") { MessageBox.Show("Se importaron los prospectos de manera satisfactoria", "Importar prospectos", MessageBoxButtons.OK); }
                    if (tipoImporte == "prospectoNuevo") { MessageBox.Show("Se importaron los prospectos de manera satisfactoria", "Importar prospectos", MessageBoxButtons.OK); }
                    ActualizarListado = "SI";
                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvListaAsigProspecto.FocusedRowHandle;
                        frmHandler.frmListadoAsigProspecto_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        frmHandler.gvListaAsigProspecto.FocusedRowHandle = nRow;
                    }

                    //gcExcel.DataSource = null;
                    gcExcel.RefreshDataSource();
                    //if (MiAccion == campanha.Nuevo)
                    //{
                    //    MiAccion = campanha.Editar;
                    //    LimpiarCampos();
                    //}
                }
                if (result == "")
                {
                    MessageBox.Show("Favor de revisar que todos los datos estén correctamente llenados", "Importar prospectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtCargados.EditValue = cargados;
                txtExistentes.EditValue = existentes;
                txtErrores.EditValue = errores;

                txtCargados.BackColor = Color.DarkGreen;
                txtCargados.ForeColor = Color.White;

                txtExistentes.BackColor = Color.DarkOrange;
                txtExistentes.ForeColor = Color.White;

                txtErrores.BackColor = Color.DarkRed;
                txtErrores.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmImportarProspecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) this.Refresh();
        }
    }
}