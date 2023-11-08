using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Servicios.IServicios
{
    public interface IServicioPais:IServicio<PaisDTO>
    {
        IEnumerable<PaisDTO> GetAll();
        Task<IEnumerable<PaisDTO>> LoadPaisesAsync();
        PaisDTO GetPais(int id);
    }
}
