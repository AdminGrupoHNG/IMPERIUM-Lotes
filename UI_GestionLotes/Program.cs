using BE_GestionLotes;
using BL_GestionLotes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes
{
    static class Program
    {
        internal static Sesion Sesion;
        internal static Acceso SolucionAbrir;
        public static bool IsMinimized = false;
        public static List<Timer> timersExistentes = new List<Timer>();
        public static Timer timercada5minutos = new Timer();
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            /*  Comentar cuando pase a producción
             *  TU_SOLUCION: Colocar el nombre de la solucion; BACKOFFICE,SERVICIOS,PRODUCCION,LOTES,RRHH
             *  HNG: Es el Token de sesión; si no entra con sultar la tabla "scfma_solucion".
             *  ADMINISTRADOR: Usuario.
             *  Versión: 2022.0.7, es la versión de la APP, se saca del App.config
             */
            //string __token = "kUyc6buyPoQrTH2o91VXQV77SOHE75Inn8MPLcUAzcxk2n4qB1DQFz"; //buscar de la DB
            //string __key = "28dy646e8w8en3a5t08zsbtkjmmq79jg"; //buscar de la DB
            //if (ObtenerAcceso(args) == null)
            //{
            //    var __solucion = "LOTES";
            //    var __usuario = "ADMINISTRADOR";
            //    var __entorno = "LOCAL";
            //    args = new string[] { $"{__solucion},", $"{__token},", $"{__usuario},", $"{__key}", $",{__entorno}" };
            //    ////string[] args = new string[] { $"{sol},", $"{sis[0].dsc_token_sesion},", $"{usu}" };
            //}


            //int[] colorVerde, colorPlomo, colorEventRow, colorFocus;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //colorVerde = ConfigurationManager.AppSettings["colorVerde"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            //colorPlomo = ConfigurationManager.AppSettings["colorPlomo"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            //colorEventRow = ConfigurationManager.AppSettings["colorEventRow"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            //colorFocus = ConfigurationManager.AppSettings["colorFocus"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.Basic.DefaultSkin.PineLight);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.Basic);

            Sesion = new Sesion();
            //Sesion.Key = __key;


            /* Validar los parámetros enviados desde el login. */
            var acceso = ObtenerAcceso(args);
            if (acceso == null)
            {
                Application.Exit();
                return;
            }
            /*-------*Guardar los datos de acceso de Solución*-------*/
            SolucionAbrir = new Program.Acceso()
            {
                Solucion = acceso.Solucion,
                Token = acceso.Token,
                User = acceso.User,
                Key = acceso.Key,
                Entorno = acceso.Entorno
            };
            /* Inicializamos el parámetro general "Sesion" */

            Sesion.Global = new eGlobales() // Cargar todos los parametros del app config???
            {
                Entorno = acceso.Entorno,
            };

            Cifrado(acceso.Key);


            Sesion.Key = acceso.Key;
            var unit = new UnitOfWork();
            Sesion.Colores = unit.Globales.ObtenerColores();
            unit.Globales.Actualizar_appSettings(Sesion.Global);// Ver en que momento se va actualizar cambios. quiza en algunas venrtanas

            /* Obtener el token de la sesión: corresponde a cada solución.*/
            var sistema = unit.Sistema.ListarSolucion<eSolucion>(opcion: 2, dsc_solucion: acceso.Solucion);
            /*Primera validación: La consulta debe retornar algún valor.*/
            if (sistema == null && sistema.Count <= 0) { Application.Exit(); return; }
            /* Segunda validación: Si el Token enviado desde el login está registrado en la DB.*/
            if (sistema[0].dsc_token_sesion.ToString().Trim().Equals(acceso.Token.Trim()))
            {


                /*  Cargar información del usuario*/
                Sesion.Usuario = new eUsuario();
                Sesion.Usuario = unit.Usuario.ObtenerUsuario<eUsuario>(opcion: 2, cod_usuario: acceso.User);
                /*  Si el usuario no tiene info; se cierra la APP*/
                if (Sesion.Usuario == null) { Application.Exit(); }

                /*  Cargar las empresas asociadas al usuario*/
                Sesion.EmpresaList = unit.Proveedores.ListarEmpresasProveedor<eProveedor_Empresas>(
                    opcion: 11, cod_proveedor: "", cod_usuario: acceso.User);

                /*  Cargar la Versión, esto se valida en el SplashScreen*/
                Sesion.Version = sistema[0].dsc_version;
                Sesion.RutaDescarga = sistema[0].dsc_ruta_descarga;
                Sesion.Solucion = sistema[0].dsc_solucion;
                Sesion.Cod_solucion = sistema[0].cod_solucion;
                /*  Iniciar el SplasScreen*/
                unit.Dispose();
                Application.Run(new frmSplashScreen());
            }
        }
        /// <summary>
        /// Parámetro args, recibe arreglo separados por espacio.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Retorna credenciales de acceso:[0]:Sesion, [1]:token, [2]:usuario, [3]:key, [4]:Entorno </returns>
        static Acceso ObtenerAcceso(string[] args)
        {
            string values = "";
            args.ToList().ForEach(a => { values += a.ToString().Trim(); });

            // debería recibir 5 parámetros... (Agregar el valor si se requiere más parámetros).
            var array = values.Split(',');
            if (array.Count() < 5) return null;

            return new Acceso()
            {
                Solucion = array[0].ToString(),
                Token = array[1].ToString(),
                User = array[2].ToString(),
                Key = array[3].ToString(),
                Entorno = array[4].ToString(),
            };
        }
        internal class Acceso
        {
            public string Solucion { get; set; }
            public string Token { get; set; }
            public string User { get; set; }
            public string Key { get; set; }
            public string Entorno { get; set; }
        }
        private static void Cifrado(string key)
        {
            var blEncryp = new blEncrypta(key); // se puede declarar como  globar para llamar desde cualquier formulario...
            try { blEncryp.DesencryptaAppSettings(); } catch { }
            blEncryp.EncryptaAppSettings();
        }
    }
}
