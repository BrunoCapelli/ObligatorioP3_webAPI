using Domain.DataAccess.IRepositorios;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataAccess.Repositorios
{
    public class RepositoryPlanta : IRepositoryPlanta<Planta>
    {


        private IRestContext<Planta> _restContext;

        public RepositoryPlanta(IRestContext<Planta> restContext)
        {
            //_repositoryPlantaRest = new RestContext<Planta>("https://localhost:7111/api/plantas");
            _restContext = restContext;
        }


        public Planta Add(Planta entity)
        {
            return _restContext.Add(entity).GetAwaiter().GetResult();
        }

        public IEnumerable<Planta> GetByName(string name)
        {
            return _restContext.GetAll(name).GetAwaiter().GetResult();
        }

        public Planta GetById(int id)
        {
            return _restContext.GetById(id).GetAwaiter().GetResult();
        }

        public void Remove(Planta entity)
        {
            _restContext.Remove(entity.PlantaId);
        }

        public void Save()
        {

        }

        public void Update(Planta entity)
        {
            _restContext.Update(entity.PlantaId, entity);
        }

        public IEnumerable<Planta> GetBetweenDates(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
