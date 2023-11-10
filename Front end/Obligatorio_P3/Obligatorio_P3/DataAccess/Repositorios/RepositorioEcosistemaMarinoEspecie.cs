using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioEcosistemaMarinoEspecie : IRepositorioEcosistemaMarinoEspecie<EcosistemaMarinoEspecie>
    {
        private IRestContext<EcosistemaMarinoEspecie> _restContext;

        public RepositorioEcosistemaMarinoEspecie(IRestContext<EcosistemaMarinoEspecie> restContext)
        {
            _restContext = restContext;
        }

        public EcosistemaMarinoEspecie Add(EcosistemaMarinoEspecie entity)
        {
            _restContext.Add(entity).GetAwaiter().GetResult();
            return entity;
        }

        public IEnumerable<EcosistemaMarinoEspecie> GetByEcosistemaId(int id) // Hay que cambiar filtros para que funque
        {
            string filters = "?" + id;
            IEnumerable<EcosistemaMarinoEspecie> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }

        public IEnumerable<EcosistemaMarinoEspecie> GetEspeciesByEcosistemaId(int id) // Hay que cambiar filtros para que funque
        {
            string filters = "?" + id;
            IEnumerable<EcosistemaMarinoEspecie> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }
        

        public Especie GetByEspecieId(int id) // Hay que cambiar filtros para que funque
        {
            return Context.Especies.FirstOrDefault(em => em.EspecieId == id);
        }

        public void Remove(EcosistemaMarinoEspecie entity)
        {
            Context.Set<EcosistemaMarinoEspecie>().Remove(entity);
        }

        public void Update(EcosistemaMarinoEspecie entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EcosistemaMarinoEspecie> IRepositorio<EcosistemaMarinoEspecie>.GetAll()
        {
            return Context.Set<EcosistemaMarinoEspecie>()
                .Include(eme => eme.EcosistemaMarino)
                .Include(eme => eme.Especie)
                .ToList();
        }
    }
}
