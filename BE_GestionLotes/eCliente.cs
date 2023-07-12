using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eCliente
    {
        public string idCarpeta_cliente { get; set; }
        public bool Seleccionado { get; set; }
        public string cod_prospecto { get; set; }
        public string dsc_estado_civil { get; set; }
        public string dsc_nombre { get; set; }
        public string cod_proyecto { get; set; }
        public string cod_empresa { get; set; }
        public string flg_activo { get; set; }
        public string flg_prospecto { get; set; }
        public string flg_proyecto { get; set; }
        public string cod_asesor { get; set; }
        public string cod_tipo_documento_conyuge { get; set; }
        public string dsc_profesion { get; set; }
        public string dsc_documento_conyuge { get; set; }
        public string dsc_apellido_paterno_conyuge { get; set; }
        public string dsc_apellido_materno_conyuge { get; set; }
        public string dsc_nombre_conyuge { get; set; }
        public DateTime fch_nacimiento_conyuge { get; set; }
        public string cod_estadocivil_conyuge { get; set; }
        public string dsc_profesion_conyuge { get; set; }
        public string dsc_direccion_conyuge { get; set; }
        public string dsc_email_conyuge { get; set; }
        public string dsc_telefono_1_conyuge { get; set; }



        public string cod_cliente { get; set; }
        public string dsc_razon_social { get; set; }
        public string dsc_apellido_paterno { get; set; }
        public string dsc_apellido_materno { get; set; }
        public string dsc_proyecto_vinculado { get; set; }
        public string cod_proveedor_ERP { get; set; }
        public string dsc_razon_comercial { get; set; }
        public string flg_juridico { get; set; }
        public string cod_tipo_documento { get; set; }
        public string dsc_tipo_documento { get; set; }
        public string dsc_documento { get; set; }
        public string cod_calificacion { get; set; }
        public string dsc_calificacion { get; set; }
        public string dsc_email { get; set; }
        public string dsc_telefono_1 { get; set; }
        public string dsc_telefono_2 { get; set; }
        public string dsc_cliente { get; set; }
        public string cod_tipo_contacto { get; set; }
        public string dsc_tipo_contacto { get; set; }
        public string cod_usuario { get; set; }
        public string cod_usuario_registro { get; set; }
        public string dsc_usuario_registro { get; set; }
        public DateTime fch_registro { get; set; }        
        public DateTime fch_cambio { get; set; }
        public string cod_usuario_cambio { get; set; }
        public string dsc_usuario_cambio { get; set; }
        public string flg_codigo_autogenerado { get; set; }
        public string cod_sexo { get; set; }
        public string cod_estadocivil { get; set; }
        public string cod_categoria { get; set; }
        public string dsc_categoria { get; set; }
        public string cod_cliente_antiguo { get; set; }
        public DateTime fch_fallecimiento { get; set; }
        public DateTime fch_nacimiento { get; set; }
        public string dsc_mail_trabajo { get; set; }
        public string cod_tipo_cliente { get; set; }
        public string dsc_tipo_cliente { get; set; }
        public string flg_domiciliado { get; set; }
        public string flg_bienes_separados { get; set; }
        public string cod_vendedor { get; set; }
        public string dsc_vendedor { get; set; }
        public string cod_modalidad_venta { get; set; }
        public string flg_vinculada { get; set; }
        //public string cod_tarjeta_cliente { get; set; }
        public string dsc_mail_fe { get; set; }
        public string cod_cliente_interno { get; set; }
        public string flg_padron_envio { get; set; }
        public DateTime fch_afiliacion { get; set; }
        public string cod_empresa_interna { get; set; }
        public string dsc_cargo { get; set; }
        public string dsc_carben { get; set; }
        public string flg_tipo_planilla { get; set; }
        public Int16 num_dias_gracia { get; set; }
        public string cod_modulo { get; set; }
        public string cod_modular { get; set; }
        public string dsc_contacto { get; set; }


        public string dsc_sexo { get; set; }
        public string cod_tipo_direccion { get; set; }
		public string dsc_tipo_direccion { get; set; }
		public string cod_pais { get; set; }
		public string dsc_pais { get; set; }
		public string cod_distrito { get; set; }
		public string dsc_distrito { get; set; }
		public string cod_provincia { get; set; }
		public string dsc_provincia { get; set; }
		public string cod_departamento { get; set; }

        public string dsc_departamento { get; set; }
        public string dsc_cadena_direccion { get; set; }

        public string cod_empresas_vinculadas { get; set; }
        public string dsc_empresas_vinculadas { get; set; }

        public string cod_proyectos_vinculadas { get; set; }
        public string dsc_proyectos_vinculadas { get; set; }

        public decimal valorRating { get; set; }
        public string dsc_lotes_asig { get; set; }

        public class eCliente_Observaciones : eCliente
        {
            public Int32 num_linea { get; set; }
            public string dsc_observaciones { get; set; }
        }

        public class eCliente_Contactos : eCliente
        {
            public Int32 num_linea_contacto { get; set; }
            public string num_linea_contacto_string { get; set; }
            public string dsc_nombre_contacto { get; set; }
            public string dsc_celular_contacto { get; set; }
            public string dsc_telef_contacto { get; set; }
            public string dsc_email_contacto { get; set; }

        }

        public class eCliente_Documentos : eCliente
        {
            public string cod_documento_cliente { get; set; }
            public string dsc_nombre_doc { get; set; }
            public string dsc_nombre_doc_ref { get; set; }
            public string dsc_descripcion_cli { get; set; }
            public Int32 num_orden_doc { get; set; }
            public string cod_documento_cliente_referencia { get; set; }

            public string flg_PDF { get; set; }
            public string idPDF { get; set; }
            public string flg_activo_cli { get; set; }
        }
    }
    
}
