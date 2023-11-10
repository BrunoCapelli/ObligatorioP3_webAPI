﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioEcosistemaMarinoEspecie<EcosistemaMarinoEspecie>: IRepositorio<EcosistemaMarinoEspecie>
    {
        Especie GetByEspecieId(int id);
        EcosistemaMarino GetByEcosistemaId(int id);
        List<EcosistemaMarinoEspecie> GetEspeciesByEcosistemaId(int id);
    }
}
