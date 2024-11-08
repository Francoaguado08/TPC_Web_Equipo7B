using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuarios usuarios)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select IDUsuario,Nombre,Email,Contraseña,IDTipoUsuario from Usuario Where Nombre = @Nombre AND Contraseña = @Contraseña");
                datos.setearParametro("@Nombre", usuarios.Nombre);
                datos.setearParametro("@Contraseña", usuarios.Contraseña);

                datos.ejecutarAccion();
                while (datos.Lector.Read())
                {
                    usuarios.IDUsuario = (int)datos.Lector["IDUsuario"];
                    usuarios.Email = (string)datos.Lector["email"];
                    usuarios.TipoUsuario = (int)(datos.Lector["TipoUsuario"]) == 1 ? TipoUsuario.Admin : TipoUsuario.Cliente;
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
    }
}
