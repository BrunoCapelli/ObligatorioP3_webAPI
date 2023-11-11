using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Access.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private IRestContext<Usuario> _restContext;

        public RepositorioUsuario(IRestContext<Usuario> restContext)
        {
            _restContext = restContext;
        }

        public Usuario GetUsuarioByAlias(string userAlias)
        {
            string filters = "?" + userAlias;
            IEnumerable<Usuario> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            Usuario user = null;
            foreach(Usuario usuario in entity) {
                user = usuario;
            }
            return user;
        }

        public Usuario GetUsuarioById(int id)
        {
            Usuario user = _restContext.GetById(id).GetAwaiter().GetResult();

            return user;
        }

        public IEnumerable<Usuario> GetAll()
        {
            string filters = "";
            return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }

        public Usuario Add(Usuario entity) {
            Usuario usuario = _restContext.Add(entity).GetAwaiter().GetResult();
            return usuario;
        }

        public void Update(Usuario entity) {
            throw new NotImplementedException();
        }

        public void Remove(Usuario entity) {
            throw new NotImplementedException();
        }
    }
}
