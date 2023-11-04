using Data_Access.IRepositorios;
using Data_Access.Repositorios;
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
    public class ServicioEcosistemaAmenaza: IServicioEcosistemaAmenaza
    {
        IRepositorioEcosistemaAmenaza _repositorioEAmenaza;
        IRepositorioAmenaza _repoAmenaza;
        IRepositorioEcosistemaMarino _repositorioEcosistemaMarino;
        public ServicioEcosistemaAmenaza(IRepositorioEcosistemaAmenaza repositorioEcosistemaAmenaza,
            IRepositorioEcosistemaMarino repositorioEcosistemaMarino,
            IRepositorioAmenaza repositorioAmenaza)
        {
            _repositorioEAmenaza = repositorioEcosistemaAmenaza;
            _repositorioEcosistemaMarino = repositorioEcosistemaMarino;
            _repoAmenaza = repositorioAmenaza;
        }

        public void Add(int AmenazaId, int EcosistemaId)
        {
            if (EcosistemaId > 0 && AmenazaId > 0)
            {
                EcosistemaMarino ecosistema = _repositorioEcosistemaMarino.GetById(EcosistemaId);
                Amenaza amenaza = _repoAmenaza.GetAmenazaById(AmenazaId);

                IEnumerable<EcosistemaAmenaza> ecosistemaAmenazas =  _repositorioEAmenaza.GetAll();
                foreach(EcosistemaAmenaza ea in ecosistemaAmenazas)
                {
                    if(ea.AmenazaId == AmenazaId && ea.EcosistemaMarinoId == EcosistemaId)
                    {
                        throw new DatabaseException("La asociacion ya existe");
                    }
                }

                EcosistemaAmenaza newEme = new EcosistemaAmenaza(ecosistema, amenaza);
                _repositorioEAmenaza.Add(newEme);
                _repositorioEcosistemaMarino.Save();
            }
            else
            {

                throw new Exception("El ecosistema o amenaza ingresado no es valido");
            }
        }

        public EcosistemaAmenaza Add(EcosistemaAmenaza entity)
        {
            throw new NotImplementedException();
        }

       

        public void Remove(int id) {
            throw new NotImplementedException();
        }

        public void Update(EcosistemaAmenaza entity)
        {
            throw new NotImplementedException();
        }
    }
}
