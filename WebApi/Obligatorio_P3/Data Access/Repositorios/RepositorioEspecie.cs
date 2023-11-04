using Data_Access.IRepositorios;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioEspecie: Repositorio<Especie>, IRepositorioEspecie
    {
        public RepositorioEspecie(MiContexto context) 
        { 
            Context = context;
        }

        public IEnumerable<Especie> GetAllEspecies() { 
            return Context.Set<Especie>().Include(es => es.EstadoConservacion).ToList();
        }

        public Especie GetById(int id)
        {
            return Context.Especies.Include(e => e.EstadoConservacion).FirstOrDefault(e => e.EspecieId == id);
        }

        public List<Especie> GetEspecieByName(string name)
        {
            return Context.Especies.Where(e=> e.NombreCientifico.Contains(name))
                .Include(e => e.EstadoConservacion)
                .Include(e => e.EcosistemasHabitados)
                .ToList();
        }
        public List<Especie> GetEspecieByGradoConservacion(int grado)
        {
            return Context.Especies.Where(e => e.EstadoConservacion.EstadoConservacionId.Equals(grado))
                .Include(e => e.EstadoConservacion)
                .Include(e => e.EcosistemasHabitados)
                .ToList();
        }

        public List<Especie> GetEspecieByPeso(int pesoDesde, int pesoHasta)
        {
            return Context.Especies.Where(e => e.PesoMin >= pesoDesde && e.PesoMax <= pesoHasta)
                .Include(e => e.EstadoConservacion)
                .Include(e => e.EcosistemasHabitados)
                .ToList();
        }

    }
}
