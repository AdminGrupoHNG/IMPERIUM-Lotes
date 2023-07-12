using BE_GestionLotes;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes
{
    internal class Sesion
    {
        public Sesion()
        {
            _usuario = null;
            _colores = null;
            _global = null;
            _empresaList = null;
            _version = string.Empty;
            _solucion = string.Empty;
            _Cod_solucion = string.Empty;
            _rutadescarga = string.Empty;
            _key = string.Empty;
        }
        private eUsuario _usuario;
        private eColor _colores;
        private eGlobales _global;//
        private List<eProveedor_Empresas> _empresaList;
        private string _version;
        private string _solucion;
        private string _Cod_solucion;
        private string _rutadescarga;
        private string _key;

        public eUsuario Usuario { get => _usuario; set => _usuario = value; }
        public eColor Colores { get => _colores; set => _colores = value; }
        public eGlobales Global { get => _global; set => _global = value; }
        public List<eProveedor_Empresas> EmpresaList { get => _empresaList; set => _empresaList = value; }
        public string Version { get => _version; set => _version = value; }
        public string Cod_solucion { get => _Cod_solucion; set => _Cod_solucion = value; }
        public string Solucion { get => _solucion; set => _solucion = value; }
        public string RutaDescarga { get => _rutadescarga; set => _rutadescarga = value; }
        public string Key { get => _key; set => _key = value; }


        #region Espacio para Soluciones
        public void CrearButtonModulos(List<eSolucionUsuario_Consulta> modulos, Control control)
        {
            control.Controls.Clear();
            var mods = modulos.Where(o => o.dsc_solucion != Solucion).ToList();
            foreach (var mo in mods)
            {
                var icono = mo.dsc_icono;
                var btn = new SimpleButton()
                {
                    Cursor = Cursors.Hand,
                    PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light,
                    Dock = DockStyle.Top,
                    Font = new Font("Tahoma", 11, FontStyle.Regular),
                    Size = new Size(260, 46),
                    Text = mo.dsc_texto,
                };
                btn.ImageOptions.Image = GetIcono(icono);
                btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

                btn.Click += (sender, args) =>
                AbrirModulos(mo.dsc_ruta_solucion, mo.dsc_solucion, mo.dsc_key_encrypted);
                control.Controls.Add(btn);
            }
            control.Height = (mods.Count() * 46) + 10;
        }

        public void CrearButtonModulos(List<eSolucionUsuario_Consulta> modulos, RibbonPageGroup control)
        {
            control.ItemLinks.Clear();
            var mods = modulos.Where(o => o.dsc_solucion != Solucion).ToList();
            foreach (var mo in mods)
            {
                var icono = mo.dsc_icono;
                var btn = new BarButtonItem()
                {
                    Caption = mo.dsc_texto,
                    RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large,
                    //Font = new Font("Tahoma", 11, FontStyle.Regular),
                    //Size = new Size(260, 46),
                    //Text = mo.dsc_texto,
                    Enabled = mo.flg_acceso
                };
                btn.ImageOptions.Image = GetIcono(icono);
                //btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                //btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

                btn.ItemClick += (sender, args) =>
                AbrirModulos(mo.dsc_ruta_solucion, mo.dsc_solucion, mo.dsc_key_encrypted);
                control.ItemLinks.Add(btn);
            }
            // control.Height = (mods.Count() * 46) + 10;
        }

        private Image GetIcono(string file)
        {
            var unit = new UnitOfWork();
            var pathFromApp = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaIconoSolucion")]).ToString();
            if (!Directory.Exists(pathFromApp))
                Directory.CreateDirectory(pathFromApp);

            string path = $@"{pathFromApp}\{file}";
            if (!File.Exists(path))
                return null;

            return new Bitmap(path);
        }
        private string GetSolucionPath()
        {
            var unit = new UnitOfWork();
            var pathFromApp = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaSoluciones")]).ToString();
            if (!Directory.Exists(pathFromApp))
                Directory.CreateDirectory(pathFromApp);

            return pathFromApp;
        }
        private void AbrirModulos(string solucionRuta, string dsc_solucion, string key) { EjecutarAplicacion(solucionRuta, dsc_solucion, key); }
        private void EjecutarAplicacion(string pathExe, string dsc_solucion, string key)
        {
            try
            {
                if (!RegistrarToken(dsc_solucion, out string token)) return;

                string path = $@"{GetSolucionPath()}\{pathExe}.exe";
                if (!File.Exists(path)) { throw (new Exception($"La solución {path}, no existe.")); }
                Process process = new Process();
                process.StartInfo.FileName = path;
                string parametro = $"{dsc_solucion},{token},{Usuario.cod_usuario},{key},{Global.Entorno}";
                /*-------*Guardar los datos de acceso de Solución*-------*/
                Program.SolucionAbrir = new Program.Acceso()
                {
                    Solucion = dsc_solucion,
                    Token = token,
                    User = Usuario.cod_usuario,
                    Key = key,
                    Entorno = Global.Entorno
                };
                //Se envía en ordern de 0 a más: 0:Solucion, 1:token_sesion, 2:cod_usuario,
                //process.StartInfo.Arguments = $"{dsc_solucion},{token},{Usuario.cod_usuario},{key}.{Global.Entorno}";
                Process.Start(path, parametro);
                //process.Start();



                //Application.Exit();
            }
            catch (Exception ex)
            {
                new UnitOfWork().Globales.Mensaje(false, $"{ex.Message}", "Abrir Solución");
            }
        }
        private bool RegistrarToken(string dsc_solucion, out string token)
        {
            token = new UnitOfWork().Globales.ObtenerTokenString();
            var result = new UnitOfWork().Sistema.ListarSolucion<eSqlMessage>
                (opcion: 3, dsc_token_sesion: token, cod_usuario: Usuario.cod_usuario,
                dsc_solucion: dsc_solucion);

            return result[0].IsSuccess;
        }
        #endregion

    }
}
