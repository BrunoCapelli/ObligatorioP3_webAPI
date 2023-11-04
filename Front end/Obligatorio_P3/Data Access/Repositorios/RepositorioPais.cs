using Data_Access.IRepositorios;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioPais : Repositorio<Pais>, IRepositorioPais {
        
        
        public RepositorioPais(MiContexto context) {
            Context = context;
        }

        public Pais GetPais(int id) {

            Pais p = Context.Paises.FirstOrDefault(p => p.PaisId == id);

            return p;

        }
    }
}
