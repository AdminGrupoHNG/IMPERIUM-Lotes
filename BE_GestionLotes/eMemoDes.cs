using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eMemoDes
    {
        public string cod_memoria_desc { get; set; }
        public string dsc_nombre { get; set; }
        public string dsc_descripcion_html { get; set; }
        public string dsc_descripcion { get; set; }
        public int num_orden { get; set; }
        public string cod_empresa { get; set; }
        public string cod_proyecto { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }

        public string cod_memoria { get; set; }

            public string dsc_memoria { get; set; }
            public string cod_nodo1 { get; set; }
            public string dsc_nodo1 { get; set; }
            public string cod_nodo2 { get; set; }
            public string dsc_nodo2 { get; set; }
            public string cod_nodo3 { get; set; }
            public string dsc_nodo3 { get; set; }
        
    }
}
