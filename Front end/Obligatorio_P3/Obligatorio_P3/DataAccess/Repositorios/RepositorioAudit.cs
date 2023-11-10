using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioAudit : IRepositorioAudit
    {
        private IRestContext<Audit> _restContext;

        public RepositorioAudit(IRestContext<Audit> restContext)
        {
            _restContext = restContext;
        }

    }
}
