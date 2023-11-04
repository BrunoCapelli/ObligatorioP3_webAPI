using Data_Access.IRepositorios;
using Domain.DTO;
using Domain.Entities;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioUsuario: IServicioUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IServicioAudit _servicioAudit;
        public ServicioUsuario(IRepositorioUsuario repoUsuario, IServicioAudit servicioAudit)
        {
            _repoUsuario = repoUsuario;
            _servicioAudit = servicioAudit;
        }

        public UsuarioDTO Add(UsuarioDTO userDTO)
        {
            userDTO.Validate();
            UsuarioDTO foundUserDTO = FindAlias(userDTO);

            if (foundUserDTO == null) // En este punto el Alias chequea que el Alias sea nulo, es decir, que no existe.
            {
                Usuario usuario = new Usuario(userDTO);
                if(usuario != null) //programacion defensiva
                {
                    usuario.Password = HashPassword(usuario.Password); // Guardo la contraseña hasheada. Si quiero ver si es correcta, hasheo la que entra y la comparo con la guardada en la base
                    DateTime fecha = DateTime.Now;
                    usuario.FechaAlta = fecha;
                    Usuario newUser = _repoUsuario.Add(usuario);
                    _repoUsuario.Save();

                    

                }

            }
            else
            {
                throw new Exception("El usuario ingresado ya existe.");
            }
            
            
            return userDTO;
        }

        public UsuarioDTO FindAlias(UsuarioDTO user)
        {

            Usuario aUser = new Usuario(user);
            UsuarioDTO foundUser = new UsuarioDTO();
            if(aUser != null)
            {
                aUser = _repoUsuario.GetUsuarioByAlias(aUser.Alias);

                if(aUser != null)
                {
                    foundUser = new UsuarioDTO(aUser);
                }
                else
                {
                    foundUser = null;
                }
            }

            return foundUser;
        }

        public UsuarioDTO FindUser(UsuarioDTO user)
        {

            Usuario aUser = new Usuario(user);
            UsuarioDTO foundUser = new UsuarioDTO();
            if (aUser != null)
            {
                try
                {
                    aUser = _repoUsuario.GetUsuarioByAlias(aUser.Alias);
                    if (aUser != null && HashPassword(user.Password) == aUser.Password )
                    {
                        foundUser = new UsuarioDTO(aUser);
                    }
                    else
                    {
                        throw new Exception("No se encontro el usuario");
                    }

                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return foundUser;
        }





        public void Remove(int id)
        {
            Usuario auxUser = _repoUsuario.GetUsuarioById(id);
            //Usuario auxUser = new Usuario(user);

            // Audit Remove
            // _servicioAudit.Log(auxUser.Alias, auxUser.UsuarioId, "Usuario (Remove)");

            _repoUsuario.Remove(auxUser);
        }

        public void Update(UsuarioDTO user)
        {
            Usuario auxUser = new Usuario(user);
            _repoUsuario.Update(auxUser);

            // Audit Update
            // _servicioAudit.Log(auxUser.Alias, auxUser.UsuarioId, "Usuario (Update)");
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
