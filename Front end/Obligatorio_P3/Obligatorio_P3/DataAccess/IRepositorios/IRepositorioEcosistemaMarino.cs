using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access.IRepositorios
{
    public interface IRepositorioEcosistemaMarino<EcosistemaMarino> : IRepositorio<EcosistemaMarino>
    {
        IEnumerable<EcosistemaMarino> GetEcosistemaByName(string nombre);

        IEnumerable<EcosistemaMarino> GetAllEcosistemas();
        IEnumerable<EcosistemaMarino> GetById(int id);  
    }
}
