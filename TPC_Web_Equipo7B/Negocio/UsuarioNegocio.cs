using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        // Método para verificar si un usuario puede iniciar sesión
        public bool Loguear(Usuarios usuarios)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("SELECT IDUsuario, Nombre, Email, Contraseña, IDTipoUsuario FROM Usuarios WHERE Nombre = @Nombre AND Contraseña = @Contraseña");
                datos.setearParametro("@Nombre", usuarios.Nombre);
                datos.setearParametro("@Contraseña", usuarios.Contraseña);

                datos.ejecutarAccion();
                while (datos.Lector.Read())
                {
                    usuarios.IDUsuario = (int)datos.Lector["IDUsuario"];
                    usuarios.Email = (string)datos.Lector["Email"];
                    usuarios.TipoUsuario = (int)datos.Lector["IDTipoUsuario"] == 1 ? TipoUsuario.Admin : TipoUsuario.Cliente;
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return false;
        }

        // Método para obtener la lista de todos los usuarios
        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> listaUsuarios = new List<Usuarios>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Nombre, Email, IDTipoUsuario FROM Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuarios usuario = new Usuarios
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Email = (string)datos.Lector["Email"],
                        TipoUsuario = (int)datos.Lector["IDTipoUsuario"] == 1 ? TipoUsuario.Admin : TipoUsuario.Cliente
                    };

                    listaUsuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaUsuarios;
        }

    }
}
