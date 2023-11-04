using Data_Access.IRepositorios;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Access.Repositorios
{
    public class RepositorioAmenaza: Repositorio<Amenaza>, IRepositorioAmenaza {
        public RepositorioAmenaza(MiContexto context) {
            Context = context;
        }

        public Amenaza GetAmenazaById(int id) {
            Amenaza amenaza = Context.Amenazas.FirstOrDefault(e => e.AmenazaId == id);

            return amenaza;
        }
    }
}
