using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Access.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private IRestContextUsuario<Usuario> _restContext;

        public RepositorioUsuario(IRestContextUsuario<Usuario> restContext)
        {
            _restContext = restContext;
        }

        public Usuario GetUsuario(string userAlias, string userPassword)
        {
            //string filters = "/Login?Alias=" + userAlias + "&password=" + userPassword;
            Usuario user = _restContext.Login(userAlias, userPassword).GetAwaiter().GetResult();
            
            return user;
        }

        public Usuario GetUsuarioById(int id)
        {
            //Usuario user = _restContext.GetById(id).GetAwaiter().GetResult();

            //return user;
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            string filters = "";
            throw new NotImplementedException();
            // return _restContext.GetAll(filters).GetAwaiter().GetResult();
        }

        

        public Usuario GetUsuarioByAlias(string userAlias)
        {
            // string filters = "?Alias=" + userAlias;
            //// IEnumerable<Usuario> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            // Usuario user = null;
            // foreach (Usuario usuario in entity)
            // {
            //     user = usuario;
            // }
            // return user;
            throw new NotImplementedException();
        }

        public Usuario Add(Usuario entity, string token)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
