using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eContratos
    {
        public string cod_contrato { get; set; }
        public string cod_tipo_contrato { get; set; }
        public string dsc_periodo { get; set; }
        public string cod_separacion { get; set; }
        public string flg_separacion { get; set; }
        public string flg_bienes_separados { get; set; }
        public string flg_emitido { get; set; }
        public DateTime fch_emitido { get; set; }
        public string flg_firmado { get; set; }
        public DateTime fch_abono { get; set; }
        public string flg_abono { get; set; }
        public DateTime fch_firmado { get; set; }
        public string flg_resuelto { get; set; }
        public DateTime fch_resuelto { get; set; }
        public string flg_recepcionado { get; set; }
        public DateTime fch_recepcionado { get; set; }
        public string flg_activo { get; set; }
        public DateTime fch_anulado { get; set; }
        public string cod_cliente { get; set; }
        public string dsc_linea_contacto { get; set; }
        public string cod_copropietario { get; set; }
        public string dsc_linea_contacto_copro { get; set; }
        public string cod_asesor { get; set; }
        public string cod_etapa { get; set; }
        public string cod_manzana { get; set; }
        public string cod_ref_manzana { get; set; }
        public Int32 num_cuotas { get; set; }
        public string dsc_cliente { get; set; }
        public string dsc_asesor { get; set; }
        public string cod_lote { get; set; }
        public decimal num_area_uex { get; set; }
        public string  cod_empresa { get; set; }
        public string  cod_proyecto { get; set; }
        public string dsc_lote { get; set; }
        public string dsc_forma_pago { get; set; }
        public string cod_tipo_documento { get; set; }
        public string dsc_documento { get; set; }
        public string dsc_nombre { get; set; }
        public string cod_estadocivil { get; set; }
        public string dsc_cadena_direccion { get; set; }
        public string dsc_manzana { get; set; }
        public DateTime fch_pago { get; set; }
        public int num_etapa { get; set; }
        public string cod_forma_pago { get; set; }
        public decimal imp_separacion { get; set; }
        public decimal imp_precio_lista { get; set; }
        public decimal  prc_descuento { get; set; }
        public decimal imp_descuento { get; set; }
        public decimal imp_precio_venta_final { get; set; }
        public decimal  imp_cuota_inicial { get; set; }
        public DateTime fch_pago_cuota { get; set; }
        public decimal imp_saldo_financiar { get; set; }
        public decimal imp_saldo_financiar_uex { get; set; }
        public decimal imp_saldo_financiar_uco { get; set; }
        public decimal imp_pendiente_pago { get; set; }
        public string cod_cuotas { get; set; }
        public int num_fraccion { get; set; }
        public decimal imp_valor_cuota { get; set; }
        public DateTime fch_vct_cuota { get; set; }
        public DateTime fch_pago_contado { get; set; }
        public DateTime fch_Separacion { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_tipo_adenda { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public string cod_usuario_cambio { get; set; }

        public string idCarpeta_contrato { get; set; }

        public string cod_tipo_documento_CO { get; set; }
        public string dsc_nombre_CO { get; set; }
        public string dsc_documento_CO { get; set; }
        public string dsc_telefono_1_CO { get; set; }
        public string cod_estadocivil_CO { get; set; }
        public string dsc_email_CO { get; set; }
        public string dsc_cadena_direccion_CO { get; set; }
        public string flg_bienes_separados_CO { get; set; }
        public decimal prc_interes { get; set; }
        public decimal imp_interes { get; set; }


        public class eContratos_Adenda_Financiamiento : eContratos
        {
            public Int32 num_adenda { get; set; }
            public string dsc_tipo_adenda { get; set; }
            public DateTime fch_adenda { get; set; }
            public Int32 num_cuota { get; set; }
            public Int32 num_orden_det_cuo { get; set; }
            public string dsc_cuota { get; set; }
            public string dsc_periodo { get; set; }
            public string dsc_vct_cuota { get; set; }
            public string dsc_cuotas { get; set; }
            public Int32 num_financiamiento { get; set; }
            public string cod_tipo_financiamiento { get; set; }
            public string dsc_tipo_financiamiento { get; set; }
            public string cod_estado { get; set; }
            public string dsc_estado { get; set; }
            public decimal imp_cuotas { get; set; }

            public decimal imp_cuo_sin_interes { get; set; }
            public decimal imp_interes { get; set; }

            public DateTime fch_financiamiento { get; set; }
        }

        public class eContratos_Observaciones : eContratos
        {
            public Int32 num_linea { get; set; }
            public string dsc_observaciones { get; set; }

        }

        

        public class eContratos_Documentos : eContratos
        {
            public string cod_documento_contrato { get; set; }
            public string dsc_nombre_doc { get; set; }
            public string dsc_nombre_doc_ref { get; set; }
            public string dsc_descripcion_doc { get; set; }
            public Int32 num_orden_doc { get; set; }
            public string cod_documento_contratos_referencia { get; set; }
            public string flg_PDF { get; set; }
            public string idPDF { get; set; }
            public string flg_activo_doc { get; set; }


        }

    }
}
