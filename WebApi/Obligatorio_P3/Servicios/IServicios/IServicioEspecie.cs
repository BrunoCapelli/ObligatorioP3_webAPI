using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public interface IServicioEspecie: IServicio<EspecieDTO>
    {
        IEnumerable<EspecieDTO> GetAll();
        EspecieDTO GetById(int Id);
        IEnumerable<EspecieDTO> FiltrarPorNombreCientifico(string nombre);
        IEnumerable<EspecieDTO> FiltrarPorGradoDeConservacion(int estadoId);
        IEnumerable<EspecieDTO> FiltrarPorPeso(int pesoDesde, int pesoHasta);
        IEnumerable<EspecieDTO> FiltrarPorEcosistema(int EcosistemaId);
        IEnumerable<EcosistemaMarinoDTO> FiltrarPorEspecieQueNoHabita(int EspecieId);
    }
}
