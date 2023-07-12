using DevExpress.XtraEditors;
using HNG_Tools;
using System;
using System.Linq;
using System.Windows.Forms;
using BE_GestionLotes;

namespace UI_GestionLotes
{
    internal class HNG
    {
        public HNG() { }

        public static DialogResult MessageError(string message, string title) => new HNG_Tools.HNG().Messages.Show(mensaje: message, titulo: title, type: HNG_Tools.Interfaces.TypeMessage.Error, shadow_backgroud: false);
        public static DialogResult MessageWarning(string message, string title) => new HNG_Tools.HNG().Messages.Show(mensaje: message, titulo: title, type: HNG_Tools.Interfaces.TypeMessage.Warning, shadow_backgroud: false);
        public static DialogResult MessageSuccess(string message, string title) => new HNG_Tools.HNG().Messages.Show(mensaje: message, titulo: title, type: HNG_Tools.Interfaces.TypeMessage.Success, shadow_backgroud: false);
        public static DialogResult MessageQuestion(string message, string title) => new HNG_Tools.HNG().Messages.Show(mensaje: message, titulo: title, type: HNG_Tools.Interfaces.TypeMessage.Question, shadow_backgroud: false);
        public static DialogResult MessageInformation(string message, string title) => new HNG_Tools.HNG().Messages.Show(mensaje: message, titulo: title, type: HNG_Tools.Interfaces.TypeMessage.Information, shadow_backgroud: false);

        public static DialogResult OpenModalForm(Form modalForm) => new HNG_Tools.HNG().Forms.ShowDialog(modalForm);
        public static void OpenFormInPanel(Form form, PanelControl container) => new HNG_Tools.HNG().Forms.Display(form, container);

        public static IHNGExcel Excel => (new HNG_Tools.HNG(Program.Sesion.Global.RutaArchivosLocalExportar).Excel);

        public class SistemasHNG
        {
            readonly IMPERIUM_Sistema.UnitHNG _unit;
            public SistemasHNG()
            {
                IMPERIUM_Sistema.BE_Sistema.EUsuario User = Convert<eUsuario, IMPERIUM_Sistema.BE_Sistema.EUsuario>(Program.Sesion.Usuario);
                IMPERIUM_Sistema.BE_Sistema.EGlobales Global = Convert<eGlobales, IMPERIUM_Sistema.BE_Sistema.EGlobales>(Program.Sesion.Global);
                IMPERIUM_Sistema.BE_Sistema.EColor Color = Convert<eColor, IMPERIUM_Sistema.BE_Sistema.EColor>(Program.Sesion.Colores);

                _unit = new IMPERIUM_Sistema.UnitHNG(Global, Color, User);
            }

            public IMPERIUM_Sistema.UnitHNG Unit => _unit;
            private TDestination Convert<TSource, TDestination>(TSource source)
            {
                var destination = Activator.CreateInstance<TDestination>();
                var destProperties = destination.GetType()
                                                .GetProperties()
                                                .ToDictionary(x => x.Name);

                foreach (var prop in source.GetType().GetProperties())
                {
                    if (destProperties.ContainsKey(prop.Name))
                    {
                        destProperties[prop.Name].SetValue(destination, prop.GetValue(source));
                    }
                }

                return destination;
            }
        }
    }
}
