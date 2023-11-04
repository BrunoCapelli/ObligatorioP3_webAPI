using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public  interface IServicioEcosistemaAmenaza: IServicio<EcosistemaAmenaza>
    {
        void Add(int AmenazaId, int EcosistemaId); 
    }
}
