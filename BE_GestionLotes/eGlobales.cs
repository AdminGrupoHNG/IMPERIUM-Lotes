using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionLotes
{
    public class eGlobales
    {
        public string Entorno { get; set; }
        public string ServidorLOCAL { get; set; }
        public string ServidorREMOTO { get; set; }
        public string BBDD { get; set; }
        public string FormatoFecha { get; set; }
        public string UltimaEmpresa { get; set; }
        public string UltimoUsuario { get; set; }
        public string UltimoLocalidad { get; set; }
        public string SeparadorListas { get; set; }
        public string SeparadorDecimal { get; set; }
        public string VersionApp { get; set; }
        public string dsc_solucion { get; set; }
        public string Solucion { get; set; }
        public string CodSolucion { get; set; }

        public string Servidor { get; set; }
        public string UserSQL { get; set; }
        public string PasswordSQL { get; set; }
        public string ApplicationName { get; set; }
        public string RutaArchivosLocalExportar { get; set; }

        public string RutaDescarga { get; set; }
    }
    public class eColor
    {
        public eColor(int[] verde, int[] plomo, int[] eventRow, int[] focus)
        {
            _verde = Color.FromArgb(verde[0], verde[1], verde[2]);
            _plomo = Color.FromArgb(plomo[0], plomo[1], plomo[2]);
            _eventRow = Color.FromArgb(eventRow[0], eventRow[1], eventRow[2]);
            _focus = Color.FromArgb(focus[0], focus[1], focus[2]);
        }
        public eColor(Color verde, Color plomo, Color eventRow, Color focus)
        {
            _verde = verde;
            _plomo = plomo;
            _eventRow = eventRow;
            _focus = focus;
        }

        private Color _verde;
        private Color _plomo;
        private Color _eventRow;
        private Color _focus;



        public Color Verde { get => _verde; }
        public Color Plomo { get => _plomo; }
        public Color EventRow { get => _eventRow; }
        public Color Focus { get => _focus; }
    }

    public class eSqlMessage
    {
        public eSqlMessage()
        {
            _outMessage = string.Empty;
            _isSuccess = false;
        }
        private string _outMessage;
        private bool _isSuccess;

        public string Outmessage { get => _outMessage; set => _outMessage = value; }
        public bool IsSuccess { get => _isSuccess; set => _isSuccess = value; }

        public override string ToString()
        {
            return $"{_outMessage}";
        }
    }
    public class eResponse
    {
        public eResponse()
        {
            _isSuccess = false;
            _data = null;
        }
        private bool _isSuccess;
        private object _data;

        public bool IsSuccess { get => _isSuccess; set => _isSuccess = value; }
        public object Data { get => _data; set => _data = value; }
    }
    public class eFiltroBase
    {
        public string cod_todos { get; set; }
        public string dsc_todos { get; set; }
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string cod_sede { get; set; }
        public string dsc_sede { get; set; }
        public string cod_area { get; set; }
        public string dsc_area { get; set; }
    }
}
