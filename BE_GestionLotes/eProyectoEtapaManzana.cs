using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eProyectoEtapaManzana
    {

            public string cod_etapa { get; set; }
            public string cod_empresa { get; set; }
            public string cod_proyecto { get; set; }
            public string cod_manzana { get; set; }
            public string cod_ref_manzana { get; set; }
            public string dsc_manzana { get; set; }
            public int num_desde { get; set; }
            public int num_hasta { get; set; }
            public string flg_activo { get; set; }
            public string fch_registro { get; set; }
            public string cod_usuario_registro { get; set; }
            public string fch_cambio { get; set; }
            public string cod_usuario_cambio { get; set; }
        
    }
}
