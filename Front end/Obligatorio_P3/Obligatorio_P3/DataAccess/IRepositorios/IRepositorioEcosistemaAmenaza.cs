using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioEcosistemaAmenaza<EcosistemaAmenaza>: IRepositorio<EcosistemaAmenaza>
    {
        List<EcosistemaAmenaza> GetByEcosistemaId(int id);
    }
}
