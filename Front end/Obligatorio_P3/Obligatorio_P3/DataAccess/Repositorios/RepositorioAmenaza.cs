using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Access.Repositorios
{
    public class RepositorioAmenaza: IRepositorioAmenaza {

        private IRestContext<Amenaza> _restContext;
        public RepositorioAmenaza(IRestContext<Amenaza> restContext) {
            _restContext = restContext;
        }

        public Amenaza Add(Amenaza entity, string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Amenaza> GetAll()
        {
            string filters = "";
            return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }

        public Amenaza GetAmenazaById(int id) {
            
            Amenaza amenaza = _restContext.GetById(id).GetAwaiter().GetResult();

            return amenaza;
        }

        public void Remove(Amenaza entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Amenaza entity)
        {
            throw new NotImplementedException();
        }
    }
}
