using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access.IRepositorios
{
    public interface IRepositorioEcosistemaMarino: IRepositorio<EcosistemaMarino>
    {
        EcosistemaMarino GetEcosistemaByName(string nombre);

        IEnumerable<EcosistemaMarino> GetAllEcosistemas();
        EcosistemaMarino GetById(int id);  
    }
}
