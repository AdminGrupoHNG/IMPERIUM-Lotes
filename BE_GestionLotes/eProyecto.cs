using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eProyecto
    {
        public string cod_cliente { get; set; }
        public string cod_imagenes { get; set; }

        public bool Seleccionado { get; set; }
        public string cod_empresa { get; set; }
        public string cod_codigo { get; set; }
        public string cod_proyecto { get; set; }
        public string dsc_nombre { get; set; }
        public string dsc_base64_imagen { get; set; }
        public string dsc_plan_vias { get; set; }
        public string dsc_descripcion { get; set; }
        public string cod_pais { get; set; }
        public string dsc_jefe_proyecto { get; set; }
        public string dsc_arquitecto { get; set; }
        public string cod_moneda { get; set; }        
        public string num_total_metros { get; set; }        
        public string num_etapas { get; set; }        
        public string num_etapas_lotes { get; set; }
        public string num_lotes_libres { get; set; }
        public decimal imp_precio_terreno { get; set; }
        public decimal imp_alcabala { get; set; }
        public decimal imp_otros_gastos { get; set; }
        public decimal imp_invercion_inicial { get; set; }
        public decimal num_total_suma_metros { get; set; }
        public decimal prc_total_area_exclusiva { get; set; }
        public decimal prc_total_area_comun { get; set; }
        public decimal num_total_area_uex { get; set; }
        public decimal num_total_area_uco { get; set; }
        public byte[] bi_imagen { get; set; }
        public DateTime fch_inicio { get; set; }
        public DateTime fch_termino { get; set; }
        public DateTime fch_entrega { get; set; }   
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_lotes_asig { get; set; }
    }
}
