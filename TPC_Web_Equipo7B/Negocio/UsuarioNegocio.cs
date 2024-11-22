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
                datos.setearConsulta("SELECT IDUsuario, Nombre, Email, Contraseña, Rol FROM Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        username = (string)datos.Lector["Nombre"],
                        Email = (string)datos.Lector["Email"],
                        password = (string)datos.Lector["Contraseña"],
                        tipousuario = (string)datos.Lector["Rol"]
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
                datos.setearConsulta("INSERT INTO Usuarios (Nombre, Email, Contraseña, Rol) VALUES (@Nombre, @Email, @Contraseña, @Rol)");
                datos.setearParametro("@Nombre", nuevo.username);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Contraseña", nuevo.password);
                datos.setearParametro("@Rol", nuevo.tipousuario);
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
                datos.setearConsulta("UPDATE Usuarios SET Nombre = @Nombre, Email = @Email, Contraseña = @Contraseña, Rol = @Rol WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", usuario.IDUsuario);
                datos.setearParametro("@Nombre", usuario.username);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Contraseña", usuario.password);
                datos.setearParametro("@Rol", usuario.tipousuario);
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


        public Usuario autenticar(string email, string contraseña)
        {
            Usuario usuario = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Nombre, Email, Contraseña, Rol FROM Usuarios WHERE Email = @Email AND Contraseña = @Contraseña");
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Contraseña", contraseña);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        username = (string)datos.Lector["Nombre"],
                        Email = (string)datos.Lector["Email"],
                        password = (string)datos.Lector["Contraseña"],
                        tipousuario = (string)datos.Lector["Rol"]
                    };
                }

                return usuario;
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


        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta para verificar email y contraseña
                datos.setearConsulta(@"
            SELECT IDUsuario, Rol 
            FROM Usuarios 
            WHERE Email = @Email AND Contraseña = @Contraseña");

                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Contraseña", usuario.password); // Aquí deberías usar una contraseña hasheada
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.IDUsuario = (int)datos.Lector["IDUsuario"];
                    usuario.tipousuario = (string)datos.Lector["Rol"];
                    return true; // Inicio de sesión exitoso
                }

                return false; // Credenciales inválidas
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar loguearse", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }








    }
 }

