using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidoNegocio
    {
        public bool RegistrarPedido(int? idUsuario, CarritoCompras carrito, DatosPersonales datos)
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                // Registrar datos personales solo si el usuario está autenticado y no tiene datos previos
                if (idUsuario.HasValue && !DatosPersonalesExisten(idUsuario.Value, datosAcceso))
                {
                    datosAcceso.setearConsulta(@"
                    INSERT INTO DatosPersonales 
                    (IDUsuario, DNI, Nombre, Apellido, Domicilio, Pais, Provincia, Telefono) 
                    VALUES (@IDUsuario, @DNI, @Nombre, @Apellido, @Domicilio, @Pais, @Provincia, @Telefono)");
                    datosAcceso.setearParametro("@IDUsuario", idUsuario.Value);
                    datosAcceso.setearParametro("@DNI", datos.DNI);
                    datosAcceso.setearParametro("@Nombre", datos.Nombre);
                    datosAcceso.setearParametro("@Apellido", datos.Apellido);
                    datosAcceso.setearParametro("@Domicilio", datos.Domicilio);
                    datosAcceso.setearParametro("@Pais", datos.Pais);
                    datosAcceso.setearParametro("@Provincia", datos.Provincia);
                    datosAcceso.setearParametro("@Telefono", datos.Telefono);
                    datosAcceso.ejecutarAccion();
                }

                // Registrar el pedido
                datosAcceso.setearConsulta(@"
                INSERT INTO Pedidos (IDUsuario, Estado) 
                OUTPUT INSERTED.ID 
                VALUES (@IDUsuario, 'Pendiente')");
                datosAcceso.setearParametro("@IDUsuario", idUsuario ?? (object)DBNull.Value);
                int idPedido = datosAcceso.ejecutarScalar();

                // Registrar detalles del pedido
                foreach (Articulo producto in carrito.ObtenerProductos())
                {
                    datosAcceso.setearConsulta(@"
                    INSERT INTO DetallesPedidos 
                    (IDPedido, IDArticulo, Cantidad, PrecioUnitario) 
                    VALUES (@IDPedido, @IDArticulo, @Cantidad, @PrecioUnitario)");
                    datosAcceso.setearParametro("@IDPedido", idPedido);
                    datosAcceso.setearParametro("@IDArticulo", producto.ID);
                    datosAcceso.setearParametro("@Cantidad", producto.Cantidad);
                    datosAcceso.setearParametro("@PrecioUnitario", producto.Precio);
                    datosAcceso.ejecutarAccion();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el pedido.", ex);
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }
        }

        private bool DatosPersonalesExisten(int idUsuario, AccesoDatos datosAcceso)
        {
            datosAcceso.setearConsulta("SELECT COUNT(*) FROM DatosPersonales WHERE IDUsuario = @IDUsuario");
            datosAcceso.setearParametro("@IDUsuario", idUsuario);
            int cantidad = (int)datosAcceso.ejecutarScalar();
            return cantidad > 0;
        }



        public List<Pedido> ObtenerPedidosPorUsuario(int idUsuario)
        {
            List<Pedido> pedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consultar todos los pedidos del usuario
                datos.setearConsulta("SELECT ID, FechaPedido, Estado FROM Pedidos WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        ID = (int)datos.Lector["ID"],
                        FechaPedido = (DateTime)datos.Lector["FechaPedido"],
                        Estado = (string)datos.Lector["Estado"]
                    };
                    pedidos.Add(pedido);
                }

                return pedidos;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener los pedidos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



    }
}
