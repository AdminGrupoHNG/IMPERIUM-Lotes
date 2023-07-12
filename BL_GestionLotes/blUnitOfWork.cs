using DA_GestionLotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_GestionLotes
{
    public class blUnitOfWork : IDisposable
    {
        daSQL _sql;
        public blUnitOfWork(string key)
        {
            _sql = new daSQL(key);

            Encripta = new blEncrypta(key);
            ConsultaSunat = new blConsultaSunat(key);
            Globales = new blGlobales(_sql, key);
            Campanha = new blCampanha(_sql, key);

            Clientes = new blClientes(_sql);
            Factura = new blFactura(_sql);
            Proveedores = new blProveedores(_sql);
            Proyectos = new blProyectos(_sql);
            Sistema = new blSistema(_sql);
            Trabajador = new blTrabajador(_sql);
            Usuario = new blUsuario(_sql);
            Version = new blVersion(_sql);
            FormatoDocumento = new blFormatoDocumento(_sql);
            General = new blGenerales(_sql);
        }


        // Solo Llave
        public blEncrypta Encripta { get; private set; }
        public blConsultaSunat ConsultaSunat { get; private set; }
        // Llave y SQL
        public blGlobales Globales { get; private set; }
        public blCampanha Campanha { get; private set; }
        // SQL

        public blClientes Clientes { get; private set; }
        public blFactura Factura { get; private set; }
        public blProveedores Proveedores { get; private set; }
        public blProyectos Proyectos { get; private set; }
        public blSistema Sistema { get; private set; }
        public blTrabajador Trabajador { get; private set; }
        public blUsuario Usuario { get; private set; }
        public blVersion Version { get; private set; }

        public blFormatoDocumento FormatoDocumento { get; private set; }
        public blGenerales General { get; private set; }
        public void Dispose()
        {
            // destruir instancia
        }

    }
}
