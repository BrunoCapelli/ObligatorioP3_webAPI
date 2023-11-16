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
    public class RepositorioEcosistemaMarinoEspecie : IRepositorioEcosistemaMarinoEspecie
    {
        private IRestContext<EcosistemaMarinoEspecie> _restContext;

        public RepositorioEcosistemaMarinoEspecie(IRestContext<EcosistemaMarinoEspecie> restContext)
        {
            _restContext = restContext;
        }

        public EcosistemaMarinoEspecie Add(EcosistemaMarinoEspecie entity, string token)
        {
            _restContext.Add(entity,token).GetAwaiter().GetResult();
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
            string filters = "?" + id;
            Especie esp = null;
            IEnumerable<EcosistemaMarinoEspecie> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            foreach(var e in entity)
            {
                esp = e.Especie;
            }
            return esp;
        }

        public void Remove(EcosistemaMarinoEspecie entity)
        {
            _restContext.Remove(entity).GetAwaiter().GetResult();
        }

        public void Update(EcosistemaMarinoEspecie entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EcosistemaMarinoEspecie> GetAll()
        {
            string filters = "";
            IEnumerable<EcosistemaMarinoEspecie> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }
    }
}
