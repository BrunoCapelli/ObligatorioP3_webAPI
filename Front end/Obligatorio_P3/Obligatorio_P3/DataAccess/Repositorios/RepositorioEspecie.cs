using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioEspecie: IRepositorioEspecie
    {
        private IRestContext<Especie> _restContext;

        public RepositorioEspecie(IRestContext<Especie> restContext) 
        { 
            _restContext = restContext;
        }

        public IEnumerable<Especie> GetAllEspecies() {
            string filters = "";
            return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }

        public Especie GetById(int id)
        {
            return _restContext.GetById(id).GetAwaiter().GetResult();
        }

        public IEnumerable<Especie> GetEspecieByName(string name)
        {
            string filters = "/nombre/" + name;
            return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }
        public IEnumerable<Especie> GetEspecieByGradoConservacion(int grado)
        {
            string filters = "/Grado/" + grado;
            return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }

        public IEnumerable<Especie> GetEspecieByPeso(int pesoDesde, int pesoHasta)
        {
            string filters = "/Peso/" + pesoDesde + "/" + pesoHasta;
            return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }

        public Especie Add(Especie entity, string token) {
            Especie especie = _restContext.Add(entity, token).GetAwaiter().GetResult();
            return especie;
        }

        public void Update(Especie entity) {
            throw new NotImplementedException();
        }

        public void Remove(Especie entity, string token) {
            _restContext.Remove(entity.EspecieId, token).GetAwaiter().GetResult();
        }

        public IEnumerable<Especie> GetAll() {
            string filters = "";
            return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }
    }
}
