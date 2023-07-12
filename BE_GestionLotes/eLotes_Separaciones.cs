using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    
public class eLotes_Separaciones
    {
        public string cod_tipo_documento_CO { get; set; }
        public string dsc_nombre_CO { get; set; }
        public string dsc_documento_CO { get; set; }
        public string dsc_telefono_1_CO { get; set; }
        public string cod_estadocivil_CO { get; set; }
        public string dsc_copropietario { get; set; }
        public string dsc_estado_civil { get; set; }
        public string dsc_email_CO { get; set; }
        public string dsc_cadena_direccion_CO { get; set; }
        public string dsc_descripcion_html { get; set; }
        public string dsc_descripcion_word { get; set; }

        public string dsc_periodo { get; set; }
        public string dsc_meses { get; set; }
        public string dsc_Separacion { get; set; }
        public string dsc_tipo_separacion { get; set; }
        public string cod_separacion_padre { get; set; }
        public string dsc_linea_contacto_copro { get; set; }
        public string dsc_linea_contacto { get; set; }
        public string cod_tipo_separacion { get; set; }
        public string flg_tiene_extension { get; set; }
        public string flg_es_extension { get; set; }
        public string dsc_vct_separacion { get; set; }
        public string dsc_cantidad { get; set; }
        public int num_cantidad_separaciones1 { get; set; }
        public int num_cantidad_separaciones2 { get; set; }
        public int num_cantidad_separaciones3 { get; set; }
        public int num_cantidad_separaciones4 { get; set; }
        public int num_cantidad_separaciones5 { get; set; }
        public int num_cantidad_año { get; set; }
        public decimal imp_separacion_monto1 { get; set; }
        public decimal imp_separacion_monto2 { get; set; }
        public decimal imp_separacion_monto3 { get; set; }
        public decimal imp_separacion_monto4 { get; set; }
        public decimal imp_separacion_monto5 { get; set; }
        public string dsc_titulo2 { get; set; }
        public string dsc_titulo { get; set; }
        public string dsc_cadena_direccion { get; set; }
        public string cod_cliente { get; set; }
        public string cod_copropietario { get; set; }
        public Int32 num_dias_venc_sep { get; set; }
        public string cod_separacion { get; set; }
        public string cod_asesor { get; set; }
        public string dsc_asesor { get; set; }
        public string dsc_cliente { get; set; }
        public string dsc_apellido_paterno { get; set; }
        public string dsc_apellido_materno { get; set; }
        public string dsc_nombre { get; set; }
        public string cod_tipo_documento { get; set; }
        public string dsc_documento { get; set; }
        public string dsc_observacion { get; set; }
        public string dsc_persona { get; set; }
        public string dsc_email { get; set; }
        public string dsc_telefono_1 { get; set; }
        public string flg_registrado { get; set; }
        public string flg_Val_Admin { get; set; }
        public string flg_Val_Banco { get; set; }
        public string cod_sexo { get; set; }
        public string flg_Boleteado { get; set; }

        public DateTime fch_vct_cuota { get; set; }
        public DateTime fch_vct_separacion { get; set; }
        public DateTime fch_vct_pago_total { get; set; }
        public DateTime fch_pago_separacion { get; set; }
        public DateTime fch_pago_cuota { get; set; }
        public DateTime fch_pago_total { get; set; }
        public DateTime fch_pago { get; set; }
        public DateTime fch_Separacion { get; set; }
        public DateTime fch_nacimiento { get; set; }
        public string cod_usuario_Val_Admin { get; set; }
        public DateTime fch_Reg_Val_Admin { get; set; }
        public string cod_usuario_Desistimiento { get; set; }
        public DateTime fch_Desistimiento { get; set; }
        public string cod_usuario_Reg_Val_Banco { get; set; }
        public DateTime fch_Reg_Val_Banco { get; set; }
        public string cod_usuario_Anulacion { get; set; }
        public DateTime fch_Anulacion { get; set; }
        public string cod_usuario_Boleteado { get; set; }
        public DateTime fch_Reg_Boleteado { get; set; }
        public string cod_estadocivil { get; set; }
        public string dsc_ocupacion { get; set; }
        public string dsc_direccion { get; set; }
        public string cod_distrito { get; set; }
        public string dsc_distrito { get; set; }
        public string cod_provincia { get; set; }
        public string dsc_provincia { get; set; }
        public string cod_estado_separacion { get; set; }
        public string dsc_estado_separacion { get; set; }
        public string cod_status { get; set; }
        public string dsc_status { get; set; }
        public string cod_ref_estado_separacion { get; set; }
        public string cod_lote { get; set; }
        public string cod_etapa { get; set; }
        public int num_etapa { get; set; }
        public string dsc_manzana { get; set; }
        public string dsc_lote { get; set; }
        public int num_lote { get; set; }
        public int fila { get; set; }
        public string cod_empresa { get; set; }
        public string cod_manzana { get; set; }
        public string idCarpeta_separacion { get; set; }
        public string cod_proyecto { get; set; }
        public string cod_prospecto { get; set; }
        public decimal num_area_uex { get; set; }
        public decimal num_area_uco { get; set; }
        public string cod_forma_pago { get; set; }
        public string dsc_forma_pago { get; set; }
        public decimal imp_precio_total { get; set; }
        public decimal prc_descuento { get; set; }
        public decimal imp_descuento { get; set; }
        public decimal imp_precio_final { get; set; }
        public decimal imp_precio_final_uex { get; set; }
        public decimal imp_precio_final_uco { get; set; }
        public decimal imp_separacion { get; set; }
        public decimal imp_separacion_uex { get; set; }
        public decimal imp_separacion_uco { get; set; }
        public decimal imp_precio_con_descuento { get; set; }
        public decimal imp_cuota_inicial { get; set; }
        public decimal imp_pendiente_pago { get; set; }
        public string dsc_cuotas { get; set; }
        public string cod_cuotas { get; set; }
        public int num_cuotas { get; set; }
        public int num_fraccion { get; set; }
        public decimal prc_interes { get; set; }
        public decimal imp_interes { get; set; }

        public decimal prc_uso_exclusivo_descripcion { get; set; }
        public string prc_uso_exclusivo_desc { get; set; }
        public decimal prc_uso_exlusivo { get; set; }
        public decimal prc_uso_exclusivo { get; set; }
        public decimal imp_valor_cuota { get; set; }
        public string flg_activo { get; set; }
        public string flg_prospecto { get; set; }
        public string flg_cliente { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }

        public string cod_tipo_lote { get; set; }
        public string dsc_tipo_lote { get; set; }
        public DateTime fecha_contrato_firmado { get; set; } 
        public string contrato_firmado { get; set; }
        public string boleta_emitida { get; set; }
        public DateTime fecha_boleta_emitida { get; set; } //LDAC - Se agrega campo para efectos del dashboard Ventas
        public decimal total_venta { get; set; } //LDAC - Se agrega campo para efectos del dashboard Ventas


        public class eSeparaciones_Observaciones : eLotes_Separaciones
        {
            public Int32 num_linea { get; set; }
            public string dsc_observaciones { get; set; }

        }
        public class eSeparaciones_Documentos : eLotes_Separaciones
        {
            public string cod_documento_separacion { get; set; }
            public string dsc_nombre_doc { get; set; }
            public string dsc_nombre_doc_ref { get; set; }
            public string dsc_descripcion_doc { get; set; }
            public Int32 num_orden_doc { get; set; }
            public string cod_documento_separacion_referencia { get; set; }
            public string flg_PDF { get; set; }
            public string idPDF { get; set; }
            public string flg_activo_doc { get; set; }


        }

        public class eCotizaciones : eLotes_Separaciones
        {
            public int num_cuota { get; set; }
            public DateTime fch_cuota { get; set; }
            public int num_dias { get; set; }
            public decimal imp_capitalinicial { get; set; }
            public decimal imp_capitalfinal { get; set; }
            public decimal imp_amortizacion { get; set; }
            public decimal imp_interes { get; set; }
            public decimal imp_coutaigv { get; set; }
            public decimal imp_cuotasinigv { get; set; }
            public decimal imp_montoporpagar { get; set; }
            public decimal imp_cuotaconigv { get; set; }
        }
        public class eResumen : eLotes_Separaciones
        {
            public int Orden { get; set; }
            public string dsc_dia { get; set; }
            public string dsc_imp_separacion { get; set; }
            public string dsc_mes { get; set; }
            public int num_cantidad { get; set; }
            public decimal imp_venta_total { get; set; }
            public decimal imp_sum_separacion { get; set; }
            public decimal imp_sum_contado { get; set; }
            public decimal imp_sum_credito { get; set; }
            public int num_mes { get; set; }
            public int num_sem1 { get; set; }
            public int num_sem2 { get; set; }
            public int num_sem3 { get; set; }
            public int num_sem4 { get; set; }
            public int num_sem5 { get; set; }
            public int num_sem6 { get; set; }
            //public List<int> lst_prueba { get; set; }
            public decimal imp_sem1 { get; set; }
            public decimal imp_sem2 { get; set; }
            public decimal imp_sem3 { get; set; }
            public decimal imp_sem4 { get; set; }
            public decimal imp_sem5 { get; set; }
            public decimal imp_sem6 { get; set; }
        }

    }
    

}
