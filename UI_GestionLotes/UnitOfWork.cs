using BL_GestionLotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_GestionLotes
{
    internal class UnitOfWork : blUnitOfWork { public UnitOfWork() : base(Program.Sesion.Key) { } }
}
