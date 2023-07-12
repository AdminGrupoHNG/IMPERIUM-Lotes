using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BE_GestionLotes
{
    public class eJsonEmpresa
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string estado { get; set; }
        public string condicion { get; set; }
        public string direccion { get; set; }
        public string ubigeo { get; set; }
        public string viaTipo { get; set; }
        public string viaNombre { get; set; }
        public string zonaCodigo { get; set; }
        public string zonaTipo { get; set; }
        public string numero { get; set; }
        public string interior { get; set; }
        public string lote { get; set; }
        public string dpto { get; set; }
        public string manzana { get; set; }
        public string kilometro { get; set; }
        public string distrito { get; set; }
        public string provincia { get; set; }
        public string departamento { get; set; }
    }


    public class eJsonPersona
    {
        public string nombre { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string estado { get; set; }
        public string condicion { get; set; }
        public string direccion { get; set; }
        public string ubigeo { get; set; }
        public string viaTipo { get; set; }
        public string viaNombre { get; set; }
        public string zonaCodigo { get; set; }
        public string zonaTipo { get; set; }
        public string numero { get; set; }
        public string interior { get; set; }
        public string lote { get; set; }
        public string dpto { get; set; }
        public string manzana { get; set; }
        public string kilometro { get; set; }
        public string distrito { get; set; }
        public string provincia { get; set; }
        public string departamento { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
    }
}
