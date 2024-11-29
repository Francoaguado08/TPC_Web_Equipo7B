using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class EstadisticasNegocio
    {

        public int ObtenerCantidadUsuarios()
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                datosAcceso.setearConsulta("SELECT COUNT(*) FROM Usuarios");
                return Convert.ToInt32(datosAcceso.ejecutarScalar() );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cantidad de usuarios registrados.", ex);
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }
        }




        public int ObtenerCantidadCompras2024()
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                datosAcceso.setearConsulta("SELECT COUNT(*) FROM Pedidos WHERE YEAR(FechaPedido) = 2024");
                return Convert.ToInt32(datosAcceso.ejecutarScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cantidad de compras realizadas en 2024.", ex);
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }
        }










    }
}
