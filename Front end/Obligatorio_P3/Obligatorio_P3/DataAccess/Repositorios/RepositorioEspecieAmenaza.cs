using Data_Access.IRepositorios;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioEspecieAmenaza: Repositorio<EspecieAmenaza>, IRepositorioEspecieAmenaza
    {
        public RepositorioEspecieAmenaza(MiContexto context)
        {
            Context = context;
        }

        public List<EspecieAmenaza> GetByEspecieId(int id)
        {
            List<EspecieAmenaza> entity = Context.Set<EspecieAmenaza>().Where(e => e.EspecieId == id).ToList();
            return entity;
        }
    }
}
