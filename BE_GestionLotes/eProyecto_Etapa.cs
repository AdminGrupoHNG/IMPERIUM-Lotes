using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eProyecto_Etapa
    {
        public string accion { get; set; }
        public string cod_etapa { get; set; }
        public int num_linea { get; set; }
        public string cod_empresa { get; set; }
        public string cod_proyecto { get; set; }
        public string dsc_descripcion { get; set; }
        public decimal num_area_uex { get; set; }
        public decimal num_area_uco { get; set; }
        public decimal num_total_lotizacion { get; set; }
        public decimal num_total_suma_metros { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
    }
}
