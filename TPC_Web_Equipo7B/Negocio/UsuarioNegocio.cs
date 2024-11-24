using System;
using System.Collections.Generic;
using Dominio;


namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Nombre, Email, Contraseña, tipoUsuario FROM Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        username = datos.Lector["Nombre"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        password = datos.Lector["Contraseña"].ToString(),
                        tipousuario = datos.Lector["tipoUsuario"].ToString()
                    };

                    lista.Add(usuario);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Nombre, Email, Contraseña, tipoUsuario) VALUES (@Nombre, @Email, @Contraseña, @tipoUsuario)");
                datos.setearParametro("@Nombre", nuevo.username);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Contraseña", nuevo.password);
                datos.setearParametro("@tipoUsuario", nuevo.tipousuario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Nombre = @Nombre, Email = @Email, Contraseña = @Contraseña, tipoUsuario = @tipoUsuario WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", usuario.IDUsuario);
                datos.setearParametro("@Nombre", usuario.username);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Contraseña", usuario.password);
                datos.setearParametro("@tipoUsuario", usuario.tipousuario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Usuarios WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario autenticar(string email, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Nombre, Email, Contraseña, tipoUsuario FROM Usuarios WHERE Email = @Email AND Contraseña = @Contraseña");
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Contraseña", password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        username = datos.Lector["Nombre"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        password = datos.Lector["Contraseña"].ToString(),
                        tipousuario = datos.Lector["tipoUsuario"].ToString()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Loguear(int idUsuario)
        {
            // Implementar lógica específica de logging si se requiere.
            Console.WriteLine($"Usuario con ID {idUsuario} ha iniciado sesión.");
        }
    }
}
