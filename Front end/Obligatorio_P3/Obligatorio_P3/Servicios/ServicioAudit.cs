using Data_Access.IRepositorios;
using Domain.Entities;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioAudit: IServicioAudit
    {
        private IRepositorioAudit _repoAudit;
        public ServicioAudit(IRepositorioAudit repoAudit)
        {
            _repoAudit = repoAudit;
        }

        public void Log(string user, int idEntidadModificada, string TipoEntidad)
        {
            DateTime fecha = DateTime.Now;
            Audit audit = new Audit(user,fecha, idEntidadModificada, TipoEntidad);
            _repoAudit.Add(audit);
            _repoAudit.Save();
        }

    }
}
