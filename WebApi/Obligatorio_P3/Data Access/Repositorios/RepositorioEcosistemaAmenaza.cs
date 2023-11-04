using Data_Access.IRepositorios;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioEcosistemaAmenaza: Repositorio<EcosistemaAmenaza>,IRepositorioEcosistemaAmenaza
    {
        public RepositorioEcosistemaAmenaza(MiContexto contexto)
        {
            Context = contexto;
        }
        public EcosistemaAmenaza Add(EcosistemaAmenaza entity)
        {
            Context.Set<EcosistemaAmenaza>().Add(entity);
            return entity;
        }
        public List<EcosistemaAmenaza> GetByEcosistemaId(int id)
        {
            List<EcosistemaAmenaza> entity = Context.Set<EcosistemaAmenaza>().Where(ea => ea.EcosistemaMarinoId == id).ToList();
            return entity;
        }

    }
}
