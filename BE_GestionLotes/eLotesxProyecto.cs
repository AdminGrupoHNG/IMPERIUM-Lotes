using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eLotesxProyecto
    {
        [Display (Name = "Padron area UE")]
        public string cod_prospecto { get; set; }
        public string configurado { get; set; }
        public string cod_lote { get; set; }
        public string cod_status { get; set; }
        public string cod_ref_status { get; set; }
        public string cod_manzana { get; set; }
        public string cod_ref_manzana { get; set; }
        public string cod_etapa { get; set; }
        public string cod_empresa { get; set; }
        public string cod_proyecto { get; set; }
        public string cod_tipo_lote { get; set; }
        public string cod_ref_tipo_lote { get; set; }
        public string dsc_manzana { get; set; }
        public string dsc_descripcion_html { get; set; }
        public string dsc_descripcion_word { get; set; }
        public string dsc_Nombre { get; set; }
        public decimal imp_sum_precio_total { get; set; }
        public string imp_sum_precio_total_moneda { get; set; }
        public string prc_status { get; set; }
        public string dsc_lote { get; set; }
        public int num_filas { get; set; }
        public int num_etapa { get; set; }
        public int num_lote { get; set; }
        public decimal num_area_uex { get; set; }
        public decimal num_area_uco { get; set; }
        public decimal num_frente { get; set; }
        public decimal num_derecha { get; set; }
        public decimal num_izquierda { get; set; }
        public decimal num_fondo { get; set; }
        public decimal imp_precio_m_cuadrado { get; set; }
        public decimal imp_prec_m_cuadrado { get; set; }
        public decimal imp_precio_total { get; set; }
        public decimal imp_costo_m_cuadrado { get; set; }
        public decimal imp_costo_total { get; set; }
        public decimal prc_uso_exclusivo { get; set; }
        public string prc_uso_exclusivo_descripcion { get; set; }
        public decimal prc_uso_exclusivo_part_mat { get; set; }
        public decimal prc_uso_comun_part_mat { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_num_documento { get; set; }
        public string imagen { get; set; }
        public string dsc_persona { get; set; }
        public string dsc_telefono_movil { get; set; }
        public string dsc_email { get; set; }
        public string num_total_lotes { get; set; }
        public string dsc_sum_precio_total { get; set; }



        //LDAC-Variables para dashboard
        public string orden_listado { get; set; }
        public string dsc_serie { get; set; }
        public int valor_serie_1 { get; set; }
        public int valor_serie_2 { get; set; }
        public string total_valor_serie_1 { get; set; }
        public string total_valor_serie_2 { get; set; }
    }
}
