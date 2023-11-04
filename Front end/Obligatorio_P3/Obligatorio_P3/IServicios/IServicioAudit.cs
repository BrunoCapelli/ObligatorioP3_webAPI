using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public interface IServicioAudit
    {
        void Log(string user, int idEntidadModificada, string TipoEntidad);
        
    }
}
