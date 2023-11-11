using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioEcosistemaAmenaza: IRepositorio<EcosistemaAmenaza>
    {
        IEnumerable<EcosistemaAmenaza> GetByEcosistemaId(int id);
    }
}
