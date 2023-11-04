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
    public class ServicioEcosistemaMarinoEspecie : IServicioEcosistemaMarinoEspecie
    {
        private IRepositorioEcosistemaMarinoEspecie _repositorioEcosistemaMarinoEspecie;
        private IRepositorioEcosistemaMarino _repositorioEcosistemaMarino;
        private IRepositorioEspecie _repositorioEspecie;
        IRepositorioEspecieAmenaza _repositorioEspecieAmenaza;
        private IRepositorioEcosistemaAmenaza _repositorioEcosistemaAmenaza;
        public ServicioEcosistemaMarinoEspecie(IRepositorioEcosistemaMarinoEspecie repositorioEcosistemaMarinoEspecie,
            IRepositorioEcosistemaMarino repositorioEcosistemaMarino, 
            IRepositorioEspecie repositorioEspecie,
            IRepositorioEcosistemaAmenaza repositorioEcosistemaAmenaza,
            IRepositorioEspecieAmenaza repositorioEspecieAmenaza
            )
        {
            _repositorioEcosistemaMarinoEspecie = repositorioEcosistemaMarinoEspecie;
            _repositorioEcosistemaMarino = repositorioEcosistemaMarino;
            _repositorioEcosistemaMarino = repositorioEcosistemaMarino;
            _repositorioEspecie = repositorioEspecie;
            _repositorioEcosistemaAmenaza = repositorioEcosistemaAmenaza;
            _repositorioEspecieAmenaza = repositorioEspecieAmenaza;
        }

        public EcosistemaMarinoEspecieDTO Add(int ecosistemaId, int especieId)
        {
            
            if(ecosistemaId > 0 && especieId > 0)
            {
                EcosistemaMarino ecosistema = _repositorioEcosistemaMarino.GetById(ecosistemaId);
                Especie especie = _repositorioEspecie.GetById(especieId);

                IEnumerable<EcosistemaMarinoEspecie> ecosistemaEspecies = _repositorioEcosistemaMarinoEspecie.GetAll();
                foreach (EcosistemaMarinoEspecie ee in ecosistemaEspecies)
                {
                    if (ee.EcosistemaMarinoId == ecosistema.EcosistemaMarinoId && ee.EspecieId == especie.EspecieId)
                    {
                        throw new DatabaseException("La asociacion ya existe");
                    }
                }

                EcosistemaMarinoEspecie newEme = new EcosistemaMarinoEspecie();

                if (isApto(especie.EspecieId, ecosistema.EcosistemaMarinoId))
                {
                    newEme = new EcosistemaMarinoEspecie(ecosistema, especie);
                    especie.EcosistemasHabitados.Add(ecosistema);
                    _repositorioEspecie.Update(especie); // Luego de asociado, actualizo el EcosistemaID en la base

                    _repositorioEcosistemaMarinoEspecie.Add(newEme);
                    _repositorioEcosistemaMarino.Save();


                }
                EcosistemaMarinoEspecieDTO resultado = new EcosistemaMarinoEspecieDTO(newEme) ?? new EcosistemaMarinoEspecieDTO();

                return resultado;
                

            }
            else
            {

                throw new Exception("El ecosistema o especie ingresado no es valido");
            }
            
        }

        public EcosistemaMarinoEspecie Add(EcosistemaMarinoEspecie entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(EcosistemaMarinoEspecie entity)
        {
            throw new NotImplementedException();
        }

        public bool isApto(int especieId, int ecosistemaId)
        {
            bool resultado = false;
            //EcosistemaMarino eM = GetEcosistemasById(ecosistemaId);
            //Especie e = GetEspecieById(especieId);

            EcosistemaMarino eM = _repositorioEcosistemaMarino.GetById(ecosistemaId);
            List<EcosistemaAmenaza> amenazasEco = _repositorioEcosistemaAmenaza.GetByEcosistemaId(ecosistemaId);

            Especie e = _repositorioEspecie.GetById(especieId);
            List<EspecieAmenaza> amenazasE = _repositorioEspecieAmenaza.GetByEspecieId(especieId);

            // Chequeo que el estado de conservación del ecosistema no sea peor que el de la especie que se le está asociando
            if (eM.EstadoConservacion.ValorDesde >= e.EstadoConservacion.ValorDesde)
            {
                resultado = true;
            }
            else
            {
                throw new Exception("El nivel de estado de conservacion no es suficiente para esta asociacion");
            }

            // Chequeo que la especie y el ecosistema no sufran la misma amenaza
            foreach (EcosistemaAmenaza ecoAm in amenazasEco)
            {
                foreach(EspecieAmenaza espAm in amenazasE)
                {
                    if(ecoAm.AmenazaId == espAm.AmenazaId)
                    {
                        throw new AmenazaException("No se pueden asociar por las amenazas que enfrentan.");
                        resultado = false;
                    }
                }

            }

            return resultado;
        }

        public EcosistemaMarino GetEcosistemasById(int Id)
        {
            EcosistemaMarino ecoBuscado = new EcosistemaMarino();
            IEnumerable<EcosistemaMarino> ecos = _repositorioEcosistemaMarino.GetAllEcosistemas();
            foreach(EcosistemaMarino eco in ecos)
            {
                if(eco.EcosistemaMarinoId == Id)
                {
                    ecoBuscado = eco;
                }
            }

            return ecoBuscado;
        }

        public Especie GetEspecieById(int Id)
        {
            Especie ecoBuscado = new Especie();
            IEnumerable<Especie> especies = _repositorioEspecie.GetAllEspecies();
            foreach (Especie e in especies)
            {
                if (e.EspecieId == Id)
                {
                    ecoBuscado = e;
                }
            }

            return ecoBuscado;
        }
    }
}
