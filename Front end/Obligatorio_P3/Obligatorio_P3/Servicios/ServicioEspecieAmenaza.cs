using Data_Access.IRepositorios;
using Data_Access.Repositorios;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioEspecieAmenaza: IServicioEspecieAmenaza
    {
        IRepositorioEspecieAmenaza _repositorioEspecieAmenaza;
        IRepositorioAmenaza _repositorioAmenaza;
        IRepositorioEspecie _repositorioEspecie;
        public ServicioEspecieAmenaza(IRepositorioEspecieAmenaza repositorioEspecieAmenaza, IRepositorioAmenaza repositorioAmenaza, IRepositorioEspecie repositorioEspecie)
        {
            _repositorioEspecieAmenaza = repositorioEspecieAmenaza;
            _repositorioAmenaza = repositorioAmenaza;
            _repositorioEspecie = repositorioEspecie;
        }

        public EspecieAmenaza Add(EspecieAmenaza entity)
        {
            throw new NotImplementedException();
        }

        public EspecieAmenazaDTO Add(int AmenazaId, int EspecieId)
        {
            if (AmenazaId > 0 && EspecieId > 0)
            {
                Amenaza amenaza = _repositorioAmenaza.GetAmenazaById(AmenazaId);
                Especie especie = _repositorioEspecie.GetById(EspecieId);

                IEnumerable<EspecieAmenaza> especieAmenazas = _repositorioEspecieAmenaza.GetAll(); 
                foreach (EspecieAmenaza eA in especieAmenazas)
                {
                    if (eA.AmenazaId == amenaza.AmenazaId && eA.EspecieId == especie.EspecieId)
                    {
                        throw new DatabaseException("La asociacion ya existe");
                    }
                }

                EspecieAmenaza especieAmenaza = new EspecieAmenaza(amenaza, especie);
                _repositorioEspecieAmenaza.Add(especieAmenaza);
                _repositorioEspecieAmenaza.Save();
                EspecieAmenazaDTO eaDTO = new EspecieAmenazaDTO(especieAmenaza);
                return eaDTO;


            }
            else
            {

                throw new Exception("La amenaza o especie ingresado no es valido");
            }

            
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(EspecieAmenaza entity)
        {
            throw new NotImplementedException();
        }
    }
}
