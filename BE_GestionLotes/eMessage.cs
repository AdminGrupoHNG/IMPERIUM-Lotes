using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eMessage
    {
        public string Caption { get; set; }
        public string Text { get; set; }
        public Image Image { get; set; }
        public bool AutoCloseFormOnClick { get { return true; } }
        public string empresa { get; set; }

        public string proyecto { get; set; }

        public string prospecto { get; set; }

    }
}
