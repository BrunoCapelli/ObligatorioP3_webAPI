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
    public class RepositorioAudit: IRepositorioAudit
    {
        private IRestContext<Audit> _restContext;

        public RepositorioAudit(IRestContext<Audit> restContext)
        {
            _restContext = restContext;
        }

        public Audit Add(Audit entity, string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Audit> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Audit entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Audit entity)
        {
            throw new NotImplementedException();
        }
    }
}
