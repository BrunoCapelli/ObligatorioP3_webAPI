using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public interface IServicioEcosistemaMarinoEspecie: IServicio<EcosistemaMarinoEspecie>
    {
        EcosistemaMarinoEspecieDTO Add(int idEcosistema, int idEspecie);

        bool isApto(int especieId, int ecosistemaId);
    }
}
