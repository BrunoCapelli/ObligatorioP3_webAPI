using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioEspecie<Especie>: IRepositorio<Especie>
    {
        IEnumerable<Especie> GetAllEspecies();
        Especie GetById(int id);
        List<Especie> GetEspecieByName(string name);
        List<Especie> GetEspecieByGradoConservacion(int grado);
        List<Especie> GetEspecieByPeso(int pesoDesde, int pesoHasta);
    }
}
