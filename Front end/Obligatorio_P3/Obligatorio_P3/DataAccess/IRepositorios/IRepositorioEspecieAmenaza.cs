using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioEspecieAmenaza: IRepositorio<EspecieAmenaza>
    {
        List<EspecieAmenaza> GetByEspecieId(int id);
    }
}
