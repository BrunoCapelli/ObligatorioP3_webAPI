using Data_Access.IRepositorios;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioEcosistemaMarino : IServicioEcosistemaMarino {

        private IRepositorioEcosistemaMarino _repoEcosistemaMarino;
        private IRepositorioEcosistemaMarinoEspecie _repoEcosistemaMarinoEspecie;
        private IRepositorioEstadoConservacion _repoEstadoConservacion;
        private IRepositorioEcosistemaAmenaza _repoEcosistemaAmenaza;
        private IRepositorioPais _repoPais;
        private IConfiguration _configuration;

        public ServicioEcosistemaMarino(IRepositorioEcosistemaMarino repoEcosistemaMarino, IRepositorioEstadoConservacion repoEstadoConservacion,IRepositorioPais repoPais, IRepositorioEcosistemaMarinoEspecie repoEcosistemaMarinoEspecie, IConfiguration configuration, IRepositorioEcosistemaAmenaza repoEcosistemaAmenaza) {
            _repoEcosistemaMarino = repoEcosistemaMarino;
            _repoEcosistemaMarinoEspecie = repoEcosistemaMarinoEspecie;
            _repoEstadoConservacion = repoEstadoConservacion;
            _repoPais = repoPais;
            _configuration = configuration;
            _repoEcosistemaAmenaza = repoEcosistemaAmenaza;
        }
        public EcosistemaMarinoDTO Add(EcosistemaMarinoDTO entity) {

            entity.NombreMin = extraerValor("ParametersTopes:NombreMin");
            entity.NombreMax = extraerValor("ParametersTopes:NombreMax");
            entity.Validate();
            //EcosistemaMarinoDTO eco = FindByName(entity.Nombre);
            
            if (entity != null) {

                int EstadoId = entity.EstadoConservacion.EstadoConservacionId;
                EstadoConservacion estado = _repoEstadoConservacion.GetEstado(EstadoId);

                EcosistemaMarino ecosistema = new EcosistemaMarino(entity, estado);

               

                EcosistemaMarino newEco = _repoEcosistemaMarino.Add(ecosistema);
               
                _repoEcosistemaMarino.Save();
                EcosistemaMarinoDTO newECODto = new EcosistemaMarinoDTO(newEco);
                return newECODto;

                // Audit Add
                //_servicioAudit.Log()

            } else {
                throw new Exception("El Ecosistema ingresado ya existe.");
            }
            

        }

        public void Update(EcosistemaMarinoDTO entity) {
            
        }

        public IEnumerable<EcosistemaMarinoDTO> GetAll()
        {
            List<EcosistemaMarinoDTO> res = new List<EcosistemaMarinoDTO>();
            IEnumerable<EcosistemaMarino> Ecosistemas = _repoEcosistemaMarino.GetAllEcosistemas();
            foreach(EcosistemaMarino e in  Ecosistemas) {
                EcosistemaMarinoDTO ecosistemaMarinoDTO = new EcosistemaMarinoDTO(e);
                Pais pais = _repoPais.GetPais(e.PaisId);
                ecosistemaMarinoDTO.PaisNombre = pais.Nombre;
                //Aca traigo nombre del pais
                res.Add(ecosistemaMarinoDTO);
            }
            
            return res;
        }

        public EcosistemaMarinoDTO FindByName(string nombre) {
            EcosistemaMarino eco = _repoEcosistemaMarino.GetEcosistemaByName(nombre);
            EcosistemaMarinoDTO ecoDTO = new EcosistemaMarinoDTO(eco);
            Pais pais = _repoPais.GetPais(eco.PaisId);
            ecoDTO.PaisNombre = pais.Nombre;
            //Aca traigo nombre del pais
            return ecoDTO; 
        }

        public EcosistemaMarinoDTO GetById(int Id)
        {
            EcosistemaMarino eBuscada = _repoEcosistemaMarino.GetById(Id);
            EcosistemaMarinoDTO eDTO = new EcosistemaMarinoDTO(eBuscada);

            return eDTO;
        }

        public void Remove(int id) {
            EcosistemaMarino eco = _repoEcosistemaMarino.GetById(id);
            IEnumerable<EcosistemaMarinoEspecie> EmEs = _repoEcosistemaMarinoEspecie.GetAll();
            IEnumerable<EcosistemaAmenaza> EmAm = _repoEcosistemaAmenaza.GetAll(); 
            int contador = 0;
            
            foreach(EcosistemaMarinoEspecie emEspecie in EmEs)
            {
                if(emEspecie.EcosistemaMarinoId == id && emEspecie.EspecieId != 0) contador++;
            }

            foreach(EcosistemaAmenaza emAmenaza in EmAm) {
                if(emAmenaza.EcosistemaMarinoId == id) {
                    _repoEcosistemaAmenaza.Remove(emAmenaza);
                }
            }


            if(contador == 0)
            {
                _repoEcosistemaMarino.Remove(eco);
                _repoEcosistemaMarino.Save();

            }
            else
            {
                throw new DatabaseException("No se puede eliminar un ecosistema que tenga una especie asociada");
            }

        }
        private int extraerValor(string clave) {
            int valor = 0;
            string strValor = _configuration[clave];
            Int32.TryParse(strValor, out valor);
            return valor;
        }
    }
}
