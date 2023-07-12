using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eVariablesGenerales
    {
        public bool Seleccionado { get; set; }
        public string cod_variable { get; set; }
        public string cod_tipo_lote { get; set; }
        public string cod_status { get; set; }
        public string cod_empresa { get; set; }
        public string cod_proyecto { get; set; }
        public string dsc_Nombre { get; set; }
        public string cod_referencia { get; set; }
        public string cod_etapa { get; set; }
        public string valor_1 { get; set; }
        public decimal valor_2 { get; set; }
        public string valor_3 { get; set; }
        public string valor_4 { get; set; }
        public decimal imp_prec_m_cuadrado { get; set; }
        public string flg_activo { get; set; }
        public string fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public string fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public int num_total_lotes { get; set; }
    }
}
