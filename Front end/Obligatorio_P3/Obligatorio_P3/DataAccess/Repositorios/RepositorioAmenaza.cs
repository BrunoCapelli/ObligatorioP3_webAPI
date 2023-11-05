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
    public class RepositorioAmenaza: Repositorio<Amenaza>, IRepositorioAmenaza {

        private IRestContext<Amenaza> _restContext;
        public RepositorioAmenaza(IRestContext<Amenaza> restContext) {
            _restContext = restContext;
        }

        public Amenaza GetAmenazaById(int id) {
            
            Amenaza amenaza = _restContext.GetById(id).GetAwaiter().GetResult();

            return amenaza;
        }
    }
}
