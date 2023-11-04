using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class UbiGeografica:IValidable
    {
        public double Latitud { get; private set; }
        public double Longitud { get; private set;}
        public int GradoPeligro { get; private set; }
        public UbiGeografica() { }
        public UbiGeografica(double latitud, double longitud, int gradoPeligro) {
            this.GradoPeligro = gradoPeligro;
            this.Longitud = longitud;
            this.Latitud = latitud;
        }

        public void Validate() {
            if (GradoPeligro <= 0 || GradoPeligro > 10) {
                throw new RangoException("El grado de peligro debe estar entre 1 y 10");
            }
        }
    }
}
