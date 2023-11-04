using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Audit
    {
        public int AuditId { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdEntidad { get; set; }
        public string TipoEntidad { get; set; }

        public Audit() { }
        public Audit(string User, DateTime Fecha, int IdEntidadModificada, string TipoEntidad) 
        {
            this.Usuario = User;
            this.FechaModificacion = Fecha;
            this.IdEntidad = IdEntidadModificada;
            this.TipoEntidad = TipoEntidad;
        }
    }
}
