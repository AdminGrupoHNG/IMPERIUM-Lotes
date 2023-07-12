using DA_GestionLotes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_GestionLotes
{
    public enum TyCmbChecked
    {
        TipoDocumento = 0,
        EstadoRegistro = 1,
        EstadoPago = 2,
        EmpresasUsuarios = 3,
        CargosEmpresa = 4,
    }
    public enum TablaGeneral
    {
        TipoAusencia = 0,
        TipoPagoAusencia = 1,
        TipoFirmaDocumento = 2,
        TipoFormatoDocumento = 3,
    }
    public class blGenerales
    {
        daSQL _sql;
        public blGenerales(daSQL sql) { _sql = sql; }

        public void CargaCombosChecked(TyCmbChecked entidad, CheckedComboBoxEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue, string cod_usuario = "")
        {
            combo.Text = "";
            string procedure = string.Empty;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                switch (entidad)
                {
                    case TyCmbChecked.TipoDocumento:
                        procedure = "usp_ConsultasVarias_FacturasProveedor";
                        dictionary.Add("opcion", 1);
                        _sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case TyCmbChecked.EstadoRegistro:
                        procedure = "usp_ConsultasVarias_FacturasProveedor";
                        dictionary.Add("opcion", 5);
                        _sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case TyCmbChecked.EstadoPago:
                        procedure = "usp_ConsultasVarias_FacturasProveedor";
                        dictionary.Add("opcion", 6);
                        _sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case TyCmbChecked.EmpresasUsuarios:
                        procedure = "usp_Consulta_ListarProveedores";
                        dictionary.Add("opcion", 11);
                        dictionary.Add("cod_usuario", cod_usuario);
                        _sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case TyCmbChecked.CargosEmpresa:
                        procedure = "Usp_RHU_ConsultasVarias_FormatoMDocumento";
                        dictionary.Add("opcion", 17);
                        dictionary.Add("cod_empresaSplit", cod_usuario);
                        _sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                }
            }
            catch (Exception generatedExceptionName)
            {
                throw generatedExceptionName;
            }
        }
        public void CargaCombosChecked(TablaGeneral entidad, CheckedComboBoxEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue, string cod_usuario = "")
        {
            combo.Text = "";
            string procedure = string.Empty;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                switch (entidad)
                {
                    case TablaGeneral.TipoAusencia:
                        dictionary.Add("cod_referencia", "01");
                        break;
                    case TablaGeneral.TipoPagoAusencia:
                        dictionary.Add("cod_referencia", "02");
                        break;
                    case TablaGeneral.TipoFirmaDocumento:
                        dictionary.Add("cod_referencia", "03");
                        break;
                    case TablaGeneral.TipoFormatoDocumento:
                        dictionary.Add("cod_referencia", "04");
                        break;
                }
                procedure = "Usp_RHU_ConsultasVarias_TablaGeneral";
                dictionary.Add("opcion", 1);
                _sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaCombosLookUp(TablaGeneral entidad, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_solucion = "")
        {
            combo.Text = "";
            string procedure = string.Empty;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();



            try
            {
                switch (entidad)
                {
                    case TablaGeneral.TipoAusencia:
                        dictionary.Add("cod_referencia", "01");
                        break;
                    case TablaGeneral.TipoPagoAusencia:
                        dictionary.Add("cod_referencia", "02");
                        break;
                    case TablaGeneral.TipoFirmaDocumento:
                        dictionary.Add("cod_referencia", "03");
                        break;
                    case TablaGeneral.TipoFormatoDocumento:
                        dictionary.Add("cod_referencia", "04");
                        dictionary.Add("cod_solucion", cod_solucion);
                        break;
                }



                procedure = "Usp_RHU_ConsultasVarias_TablaGeneral";
                dictionary.Add("opcion", 1);
                tabla = _sql.ListaDatatable(procedure, dictionary);
                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
                // if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
