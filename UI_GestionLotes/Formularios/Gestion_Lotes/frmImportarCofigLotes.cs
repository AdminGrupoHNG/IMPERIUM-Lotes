using BE_GestionLotes;
using BL_GestionLotes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Formularios.Gestion_Lotes.Proyectos
{
    public partial class frmImportarCofigLotes : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListadoControlLotes frmHandler;
        
        public string cod_empresa = "", cod_proyecto = "";
        public eLotesxProyecto o_eLotPro;
        string strPath = "";
        DataTable oDatosExcel = new DataTable();

        public frmImportarCofigLotes()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        internal frmImportarCofigLotes(frmListadoControlLotes frm)
        {
            InitializeComponent();
            frmHandler = frm;
        }

       

        private void CargarComboExcel()
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Archivo(.xls)|*.xls| Excel Archivo(.xlsx)| *.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txt_ruta.Text = ofd.FileName;

                    strPath = ofd.FileName;
                    string connStr = TipoExtension_Excel(Path.GetExtension(ofd.FileName), strPath);

                    OleDbConnection conn = new OleDbConnection(connStr);
                    conn.Open();

                    DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

                    string[] strTableNames = new string[dtSheetName.Rows.Count];
                    for (int k = 0; k < dtSheetName.Rows.Count; k++)
                    {
                        strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
                        glkpHojas.Properties.Items.Add(strTableNames[k]);
                    }
                    conn.Close();

                    if (glkpHojas.Properties.Items.Count > 0)
                    {
                        glkpHojas.SelectedIndex = 0;
                    }

                    //OleDbDataAdapter myCommand = null;
                    //DataTable dt = new DataTable();
                    //// Datos de consulta del nombre de la tabla especificado, primero puede enumerar todos los nombres de las tablas a la selección de usuarios
                    //string strExcel = "select * from [" + strTableNames[0] + "]";
                    //myCommand = new OleDbDataAdapter(strExcel, connStr);
                    //myCommand.Fill(dt);
                    //gcExcel.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void LimpiarCampos()
        {
            gvExcel.Columns.Clear();
            gcExcel.DataSource = null;
            glkpHojas.Properties.Items.Clear();
            glkpHojas.Text = "";
            glkpHojas.EditValue = "";
        }

        private void gvExcel_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvExcel_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void frmImportarCofigLotes_Load(object sender, EventArgs e)
        {
            grcInformacion.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();
        }
        private void Inicializar()
        {
            CargarComboExcel();
        }
    }
}