using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eSolucion
    {
        public string cod_solucion { get; set; }
        public string dsc_solucion { get; set; }
        public string dsc_texto { get; set; }
        public string dsc_ruta_solucion { get; set; }
        public string dsc_icono { get; set; }
        public string dsc_version { get; set; }
        public string dsc_token_sesion { get; set; }
        public string dsc_key_encrypted { get; set; }
        public string dsc_ruta_descarga { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
    }
    public class eSolucionUsuario
    {
        public string cod_solucion_usuario { get; set; }
        public string cod_usuario { get; set; }
        public string cod_solucion { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
    }
    public class eSolucionUsuario_Consulta
    {
        public string dsc_solucion { get; set; }
        public string dsc_texto { get; set; }
        public string dsc_ruta_solucion { get; set; }
        public string dsc_icono { get; set; }
        public string dsc_version { get; set; }
        public string dsc_key_encrypted { get; set; }
        public int num_orden { get; set; }
        public string dsc_rgb_btnIconColor { get; set; }
        public bool flg_acceso { get; set; }
    }
}
