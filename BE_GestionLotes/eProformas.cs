using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eProformas
    {
        public string cod_proforma { get; set; }
        public string cod_etapa { get; set; }
        public string cod_manzana { get; set; }
        public string cod_lote { get; set; }
        public string dsc_lote { get; set; }
        public string cod_proyecto { get; set; }
        public string cod_empresa { get; set; }
        public string fch_proforma { get; set; }
        public string cod_cliente { get; set; }
        public string dsc_cliente { get; set; }
        public string cod_ejecutivo { get; set; }
        public string dsc_asesor { get; set; }
        public string dsc_asesor_correo { get; set; }
        public string dsc_asesor_telefono { get; set; }
        public string dsc_nombre { get; set; }
        public string dsc_apellido_paterno { get; set; }
        public string dsc_apellido_materno { get; set; }
        public string dsc_cadena_direccion { get; set; }
        public string dsc_telefono { get; set; }
        public string cod_tipo_documento { get; set; }
        public string dsc_documento { get; set; }
        public string cod_estado_civil { get; set; }
        public string dsc_email { get; set; }
        public string dsc_observaciones { get; set; }
        public string cod_estado { get; set; }
        public string dsc_estado { get; set; }
        public string cod_referencia { get; set; }
        public string cod_variable { get; set; }
        public string dsc_interes { get; set; }
        public string cod_usuario { get; set; }
        public string dsc_usuario_registro { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public DateTime fch_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_separacion { get; set; }
        public decimal monto { get; set; }


        public class eProformas_Detalle : eProformas
        {
            public string cod_variable { get; set; }
            public string cod_referencia { get; set; }
            public string flg_seleccion { get; set; }
            public decimal imp_precio_venta { get; set; }
            public decimal prc_descuento { get; set; }
            public decimal imp_descuento { get; set; }
            public decimal imp_precio_final { get; set; }
            public decimal imp_separacion { get; set; }
            public decimal prc_cuota_inicial { get; set; }
            public decimal imp_cuota_inicial { get; set; }
            public decimal prc_interes { get; set; }
            public decimal prc_descuento_maximo { get; set; }
            public decimal imp_interes { get; set; }
            public decimal imp_valor_cuota { get; set; }
            public string dsc_nombre_detalle { get; set; }
            public int num_fraccion { get; set; }
        }
    }
}
