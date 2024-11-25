using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                datos.setearConsulta("SELECT IDUsuario, Email, Contraseña, tipoUsuario FROM Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        Email = datos.Lector["Email"].ToString(),
                        password = datos.Lector["Contraseña"].ToString(),
                        tipousuario = (int)datos.Lector["tipoUsuario"]

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
                datos.setearConsulta("INSERT INTO Usuarios (Email, Contraseña, tipoUsuario) VALUES (@Email, @Contraseña, @tipoUsuario)");
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Contraseña", nuevo.password);
                datos.setearParametro("@tipoUsuario", nuevo.tipousuario); // Ahora es un entero

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
                datos.setearConsulta("UPDATE Usuarios SET Email = @Email, Contraseña = @Contraseña, tipoUsuario = @tipoUsuario WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", usuario.IDUsuario);
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
                datos.setearConsulta("SELECT IDUsuario, Email, Contraseña, tipoUsuario FROM Usuarios WHERE Email = @Email AND Contraseña = @Contraseña");
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Contraseña", password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        Email = datos.Lector["Email"].ToString(),
                        password = datos.Lector["Contraseña"].ToString(),
                        tipousuario = (int)datos.Lector["tipoUsuario"]
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

        public bool EmailExiste(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email";
                datos.setearConsulta(query);
                datos.setearParametro("@Email", email);

                int count = datos.ejecutarScalar();
                return count > 0; // Devuelve true si el email ya existe
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el email: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int ObtenerIdPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDUsuario FROM Usuarios WHERE Email = @Email");
                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IDUsuario"];
                }

                throw new Exception("No se pudo obtener el ID del usuario.");
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


    }
}
