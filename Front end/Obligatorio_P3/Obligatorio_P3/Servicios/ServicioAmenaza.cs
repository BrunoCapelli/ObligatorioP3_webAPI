using Data_Access.IRepositorios;
using Domain.DTO;
using Domain.Entities;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioAmenaza : IServicioAmenaza {

        private IRepositorioAmenaza _repoAmenaza;
        public ServicioAmenaza(IRepositorioAmenaza repoAmenaza) {
            _repoAmenaza = repoAmenaza;
        }
        public AmenazaDTO Add(AmenazaDTO entity) {
            throw new NotImplementedException();
        }

        public IEnumerable<AmenazaDTO> GetAll() {
            List<AmenazaDTO> res = new List<AmenazaDTO>();
            IEnumerable<Amenaza> amenazas = _repoAmenaza.GetAll();
            foreach (Amenaza amenaza in amenazas) {
                AmenazaDTO amenazaDTO = new AmenazaDTO(amenaza);
                res.Add(amenazaDTO);
            }
            return res;
        }

        public AmenazaDTO GetAmenaza(int id) {
            AmenazaDTO res = new AmenazaDTO(_repoAmenaza.GetAmenazaById(id));
            return res;
        }

        public void Remove(int id) {
            throw new NotImplementedException();
        }

        public void Update(AmenazaDTO entity) {
            throw new NotImplementedException();
        }
    }
}
