using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_GestionLotes.Tools
{
    public partial class EtiquetaReporteMes : UserControl
    {
        public double Porcentaje { set { obtenerPorcentaje(value); } }
        public int Cantidad { set { label1.Text = value.ToString(); } }
        public EtiquetaReporteMes()
        {
            InitializeComponent();
        }

        private void obtenerPorcentaje(double porcentaje)
        {
            label4.Text = $"       {porcentaje}% más \nque el mes anterior";
        }
    }
}
