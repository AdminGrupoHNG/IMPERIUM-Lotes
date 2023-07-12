using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eCampanha
    {
        public int num_tipo_estado_prospecto { get; set; }
        public int num_cantidad_citas { get; set; }
        public string flg_habilitado { get; set; }
        public string dsc_estado_cita { get; set; }
        public string dsc_filtro { get; set; }
        public string dsc_lote { get; set; }
        public int num_lotes { get; set; }
        public string cod_reconfirmacion { get; set; }
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string cod_sede_empresa { get; set; }
        public string dsc_sede_empresa { get; set; }
        public string cod_proyecto { get; set; }
        public string dsc_proyecto { get; set; }
        public string cod_campanha { get; set; }
        public string dsc_campanha { get; set; }
        public string flg_eventos_pendientes { get; set; }
        public string flg_eventos_asistidos { get; set; }
        public DateTime fecha_evento_asistido { get; set; }
        public string fch_inicio_campanha { get; set; }
        public string fch_fin_campanha { get; set; }
        public string fch_cierre_campanha { get; set; }
        public string flg_activo { get; set; }
        public string flg_importar { get; set; }
        public string flg_activo_excel { get; set; }
        public string cod_usuario { get; set; }
        public string estado { get; set; }
        public string dsc_descripcion { get; set; }
        public string dsc_comentario { get; set; }
        public string flg_tiene_proximo_evento { get; set; }
        public string flg_tiene_confirmacion_evento { get; set; }
        public string cod_proximo_evento { get; set; }
        public string cod_confirmacion_evento { get; set; }
        public string dsc_objetivo { get; set; }
        public string cod_responsable { get; set; }
        public string dsc_responsable { get; set; }
        public string cod_tipo_campanha { get; set; }
        public string dsc_tipo_campanha { get; set; }
        public string cod_moneda { get; set; }
        public string dsc_moneda { get; set; }
        public decimal imp_importe { get; set; }
        public decimal imp_importe_facturado { get; set; }
        public string cod_tipo_frecuencia { get; set; }
        public string dsc_tipo_frecuencia { get; set; }
        public string cod_referencia_campanha { get; set; }
        public string cod_canal { get; set; }
        public string dsc_canal { get; set; }
        public string cod_segmento { get; set; }
        public string dsc_segmento { get; set; }
        public string dsc_valor_segmento { get; set; }
        public bool Seleccionado { get; set; }




        public string cod_usuario_registro { get; set; }
        public DateTime fch_registro { get; set; }
        public string cod_usuario_cambio { get; set; }
        public DateTime fch_cambio { get; set; }
        public string fila { get; set; }
        public string chk { get; set; }
        public int cod_mensaje { get; set; }
        public string mensaje { get; set; }



        public string cod_origen_prospecto { get; set; }
        public string dsc_origen_prospecto { get; set; }
        public string cod_prospecto { get; set; }
        public string dsc_prospecto { get; set; }
        public string cod_ejecutivo { get; set; }
        public string dsc_ejecutivo { get; set; }
        public int cnt_gestiones { get; set; }



        public int prc_asignacion { get; set; }



        public string cod_tipo_persona { get; set; }
        public string dsc_tipo_persona { get; set; }
        public string dsc_persona { get; set; }
        public string dsc_apellido_paterno { get; set; }
        public string dsc_apellido_materno { get; set; }
        public string dsc_nombres { get; set; }
        public string cod_tipo_documento { get; set; }
        public string dsc_tipo_documento { get; set; }
        public string dsc_num_documento { get; set; }
        public string dsc_email { get; set; }
        public string fch_fec_nac { get; set; }
        public string dsc_telefono { get; set; }
        public string dsc_telefono_movil { get; set; }
        public string dsc_profesion { get; set; }
        public string cod_estado_civil { get; set; }
        public string dsc_estado_civil { get; set; }
        public string cod_nacionalidad { get; set; }
        public string dsc_nacionalidad { get; set; }
        public string cod_sexo { get; set; }
        public string dsc_sexo { get; set; }
        public string cod_pais { get; set; }
        public string dsc_pais { get; set; }
        public string cod_departamento { get; set; }
        public string dsc_departamento { get; set; }
        public string cod_provincia { get; set; }
        public string dsc_provincia { get; set; }
        public string cod_distrito { get; set; }
        public string dsc_distrito { get; set; }
        public string dsc_direccion { get; set; }
        public string grupo { get; set; }
        public string cod_grupo_familiar { get; set; }
        public string dsc_grupo_familiar { get; set; }
        public string cod_rango_renta { get; set; }
        public string dsc_rango_renta { get; set; }
        public string dsc_observacion { get; set; }
        public string cod_estado_prospecto { get; set; }
        public string dsc_estado_prospecto { get; set; }


        public string cod_evento { get; set; }
        public string cod_evento_principal { get; set; }
        public string cod_evento_ref { get; set; }
        public string cod_tipo_evento { get; set; }
        public string dsc_tipo_evento { get; set; }
        public DateTime fch_evento { get; set; }
        public string fch_fecha { get; set; }
        public string fch_hora { get; set; }
        public string cod_tipo_contacto { get; set; }
        public string dsc_tipo_contacto { get; set; }
        public string cod_respuesta { get; set; }
        public string dsc_respuesta { get; set; }
        public string cod_detalle_respuesta { get; set; }
        public string dsc_detalle_respuesta { get; set; }
        public string fch_asignacion { get; set; }
        public string dsc_observacion_e { get; set; }
        public string flg_confirmacion { get; set; }
        public string flg_receptivo { get; set; }
        public string cod_evento_cita_proxima { get; set; }
        public string cod_ejecutivo_cita { get; set; }
        public string dsc_ejecutivo_cita { get; set; }
        public string cod_confirmacion { get; set; }
        public string cod_expectativa { get; set; }
        public string dsc_expectativa { get; set; }
        public string cod_motivo { get; set; }
        public string dsc_motivo { get; set; }
        public string msj_recordatorio { get; set; }


        public string cod_eventoProximo { get; set; }
        public string fch_fechaProximo { get; set; }
        public string fch_horaProximo { get; set; }
        public string cod_tipo_contactoProximo { get; set; }
        public string dsc_tipo_contactoProximo { get; set; }
        public string cod_ejecutivo_citaProximo { get; set; }
        public string dsc_ejecutivo_citaProximo { get; set; }
        public string dsc_observacionProximo { get; set; }


        public string fch_fechaEventoConfirmacion { get; set; }
        public string fch_horaEventoConfirmacion { get; set; }
        public string cod_tipo_contactoEventoConfirmacion { get; set; }
        public string cod_tipo_eventoConfirmacion { get; set; }

        public string dsc_tipo_contactoEventoConfirmacion { get; set; }


        public string cod_nodo { get; set; }
        public string dsc_nodo { get; set; }
        public string cod_nodo_padre { get; set; }
        public string filtro { get; set; }
        public string valor_1 { get; set; }
        public string valor_4 { get; set; }
        public int valor_5 { get; set; }

        public DateTime lfch_asignacion { get; set; }
        public DateTime lfch_asignacion_Original { get; set; }
        public DateTime lfch_fecha { get; set; }
        public DateTime lfch_fechaProximo { get; set; }

        public class eCampanha_Detalle : eCampanha
        {
            public string cod_campanha_detalle { get; set; }
            public DateTime fch_registro_detalle { get; set; }
            public string dsc_identificador_transaccion { get; set; }
            public string dsc_metodo_pago { get; set; }
            public decimal imp_importe_facturado_total { get; set; }
            public string cod_referencia_detalle_campanha { get; set; }
            public string cod_estado_pago { get; set; }
            public string dsc_estado_pago { get; set; }

        }
        public class eCampanha_Grafico : eCampanha
        {
            public string dsc_titulo { get; set; }
            public string dsc_mes { get; set; }
            public int num_mes { get; set; }
            public int num_cantidad_eventos { get; set; }
        }


        //************************************************************************************************

    }
}
