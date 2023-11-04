using Data_Access.IRepositorios;
using Domain.DTO;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Servicios.Servicios
{
    public class ServicioPais : IServicioPais {

        private IRepositorioPais _repoPais;
        public ServicioPais(IRepositorioPais repoPais) {
            _repoPais = repoPais;
        }

        public PaisDTO Add(PaisDTO entity) {
            throw new NotImplementedException();
        }

        public void Remove(int id) {
            throw new NotImplementedException();
        }

        public void Update(PaisDTO entity) {
            throw new NotImplementedException();
        }

        public IEnumerable<PaisDTO> GetAll() {
            List<PaisDTO> res = new List<PaisDTO>();
            IEnumerable<Pais> Paises = _repoPais.GetAll();
            foreach (Pais p in Paises) {
                PaisDTO paisDTO = new PaisDTO(p);
                res.Add(paisDTO);
            }
            return res;
        }

        public PaisDTO GetPais(int id) {
            PaisDTO res = new PaisDTO(_repoPais.GetPais(id));
            return res;
        }
    }
}
