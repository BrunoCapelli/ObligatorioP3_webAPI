using Data_Access.IRepositorios;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servicios.Servicios
{
    public class ServicioEspecie: IServicioEspecie
    {
        private IRepositorioEspecie _repoEspecie;
        private IRepositorioEstadoConservacion _repoEstadoConservacion;
        private IRepositorioEcosistemaMarinoEspecie _repoEcosistemaMarinoEspecie;
        private IRepositorioEcosistemaMarino _repoEcosistemaMarino;
        public ServicioEspecie(IRepositorioEspecie repoEspecie, IRepositorioEstadoConservacion repoEstadoConservacion, IRepositorioEcosistemaMarinoEspecie repoEcosistemaMarinoEspecie,
            IRepositorioEcosistemaMarino repositorioEcosistemaMarino)
        {
            _repoEspecie = repoEspecie;
            _repoEstadoConservacion = repoEstadoConservacion;
            _repoEcosistemaMarinoEspecie = repoEcosistemaMarinoEspecie;
            _repoEcosistemaMarino = repositorioEcosistemaMarino;
        }

        public EspecieDTO Add(EspecieDTO especieDTO)
        {
            especieDTO.Validate();

            if(especieDTO != null)
            {
                int EstadoId = especieDTO.EstadoConservacion.EstadoConservacionId;
                EstadoConservacion estado = _repoEstadoConservacion.GetEstado(EstadoId);

                Especie newEspecie = new Especie(especieDTO, estado);

                Especie especieAdded = _repoEspecie.Add(newEspecie);
                _repoEspecie.Save();

                EspecieDTO especieDTO1 = new EspecieDTO(especieAdded);
                return especieDTO1;
            }
            else
            {
                throw new Exception("La especie es invalida");
            }

        }

        public IEnumerable<EspecieDTO> GetAll()
        {
            List<EspecieDTO> especiesDTO = new List<EspecieDTO>();
            IEnumerable<EcosistemaMarinoEspecie> EcosistemaEspecies = _repoEcosistemaMarinoEspecie.GetAll();

            IEnumerable<Especie> especies = _repoEspecie.GetAllEspecies();
            foreach (Especie e in especies)
            {
                EspecieDTO especieDTO = new EspecieDTO(e);
                especieDTO.EcosistemasHabitados = new List<EcosistemaMarinoDTO>();
                foreach (EcosistemaMarinoEspecie ecoesp in EcosistemaEspecies) { 
                    if(ecoesp.EspecieId == especieDTO.EspecieId) {
                        EcosistemaMarinoDTO ecoDTO = new EcosistemaMarinoDTO(ecoesp.EcosistemaMarino);
                        especieDTO.EcosistemasHabitados.Add(ecoDTO);
                    }
                }
                especiesDTO.Add(especieDTO);
            }
            return especiesDTO;

        }

        public void Update(EspecieDTO entity)
        {
            throw new NotImplementedException();
        }

        public EspecieDTO GetById(int Id)
        {
            Especie eBuscada = _repoEspecie.GetById(Id);
            EspecieDTO eDTO = new EspecieDTO(eBuscada);

            return eDTO;
        }

        public IEnumerable<EspecieDTO> FiltrarPorNombreCientifico(string nombre)
        {
            
            IEnumerable<Especie> especies = _repoEspecie.GetEspecieByName(nombre);

            List<EspecieDTO> especiesFiltradas = new List<EspecieDTO>();
            foreach (Especie especie in especies)
            {
                EspecieDTO eDTO = new EspecieDTO(especie);
                especiesFiltradas.Add(eDTO);
            }


            return especiesFiltradas;
        }

        
        public IEnumerable<EspecieDTO> FiltrarPorGradoDeConservacion(int estadoId)
        {
            IEnumerable<Especie> especies = _repoEspecie.GetEspecieByGradoConservacion(estadoId);
            List<EspecieDTO> especieFiltradas = new List<EspecieDTO>();

            foreach(Especie especie in especies)
            {
                EspecieDTO especieDTO = new EspecieDTO(especie);
                especieFiltradas.Add(especieDTO);
            }

            return especieFiltradas;
        }

        public IEnumerable<EspecieDTO> FiltrarPorPeso(int pesoDesde, int pesoHasta)
        {
            IEnumerable<Especie> especies = _repoEspecie.GetEspecieByPeso(pesoDesde, pesoHasta);
            List<EspecieDTO> especieFiltradas = new List<EspecieDTO>();

            foreach(Especie e in especies)
            {
                EspecieDTO eDTO = new EspecieDTO(e);
                especieFiltradas.Add(eDTO);
            }

            return especieFiltradas;
        }

        public IEnumerable<EspecieDTO> FiltrarPorEcosistema(int EcosistemaId)
        {

            IEnumerable<EcosistemaMarinoEspecie> Ecosistemas = _repoEcosistemaMarinoEspecie.GetEspeciesByEcosistemaId(EcosistemaId);
             List<EspecieDTO> especieFiltradas = new List<EspecieDTO>();
             
            foreach(EcosistemaMarinoEspecie em in Ecosistemas)
            {                
                EspecieDTO eDTO = new EspecieDTO(em.Especie);
                especieFiltradas.Add(eDTO);
            }

             return especieFiltradas;
        }
        public IEnumerable<EcosistemaMarinoDTO> FiltrarPorEspecieQueNoHabita(int EspecieId)
        {

            IEnumerable<EcosistemaMarinoEspecie> EcosistemaEspecie = _repoEcosistemaMarinoEspecie.GetAll();
            IEnumerable<EcosistemaMarino> Ecosistemas = _repoEcosistemaMarino.GetAllEcosistemas();
            List<EcosistemaMarino> EcosistemasNoHabitados = new List<EcosistemaMarino>();
            //List<EspecieDTO> especieFiltradas = new List<EspecieDTO>();
            List<EcosistemaMarinoDTO> EcosistemasFiltrados = new List<EcosistemaMarinoDTO>();


            foreach (EcosistemaMarinoEspecie eco in EcosistemaEspecie)
            {
                if (eco.EspecieId != EspecieId)
                {
                    EcosistemasNoHabitados.Add(eco.EcosistemaMarino);
                }

            }

            foreach(EcosistemaMarino eco in EcosistemasNoHabitados)
            {
                foreach(EcosistemaMarino ecosistema in Ecosistemas)
                {
                    if(eco.EcosistemaMarinoId == ecosistema.EcosistemaMarinoId)
                    {
                        EcosistemaMarinoDTO ecoDTO = new EcosistemaMarinoDTO(eco);
                        EcosistemasFiltrados.Add(ecoDTO);
                    }

                }
            }


            return EcosistemasFiltrados;
        }

        public void Remove(int id) 
        {

            Especie esp = _repoEspecie.GetById(id);
            IEnumerable<EcosistemaMarinoEspecie> EmEs = _repoEcosistemaMarinoEspecie.GetAll();
            
            foreach (EcosistemaMarinoEspecie emEspecie in EmEs)
            {
                if(emEspecie.EspecieId == id)
                {
                    _repoEcosistemaMarinoEspecie.Remove(emEspecie);
                }
            }

            _repoEspecie.Remove(esp);
            _repoEspecie.Save();
  
        }
    }
}
