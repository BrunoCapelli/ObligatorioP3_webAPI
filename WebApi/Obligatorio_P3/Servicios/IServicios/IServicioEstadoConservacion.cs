using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public interface IServicioEstadoConservacion: IServicio<EstadoConservacionDTO>
    {
        IEnumerable<EstadoConservacionDTO> GetAll();
        EstadoConservacionDTO GetEstado(int estado);
    }
}
