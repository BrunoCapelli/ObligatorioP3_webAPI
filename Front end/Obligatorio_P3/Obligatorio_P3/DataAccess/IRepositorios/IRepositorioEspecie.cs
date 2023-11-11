using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioEspecie: IRepositorio<Especie>
    {
        IEnumerable<Especie> GetAllEspecies();
        Especie GetById(int id);
        IEnumerable<Especie> GetEspecieByName(string name);
        IEnumerable<Especie> GetEspecieByGradoConservacion(int grado);
        IEnumerable<Especie> GetEspecieByPeso(int pesoDesde, int pesoHasta);
    }
}
