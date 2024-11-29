using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Negocio
{
    public class PedidoNegocio
    {
        public bool RegistrarPedidoCompleto(int? idUsuario, CarritoCompras carrito, DatosPersonales datos)
        {
            try
            {
                if (carrito == null || carrito.ObtenerProductos().Count == 0)
                {
                    throw new Exception("El carrito está vacío.");
                }

                if (datos == null)
                {
                    throw new Exception("No se proporcionaron datos personales.");
                }

                PedidoNegocio pedidoNegocio = new PedidoNegocio();

                if (idUsuario.HasValue)
                {
                    pedidoNegocio.RegistrarDatosPersonales(idUsuario.Value, datos);
                }

                int idPedido = pedidoNegocio.RegistrarPedido(idUsuario);

                pedidoNegocio.RegistrarDetallesPedido(idPedido, carrito.ObtenerProductos());

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al completar el registro del pedido.", ex);
            }
        }

        public void RegistrarDatosPersonales(int idUsuario, DatosPersonales datosPersonales)
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                datosAcceso.setearConsulta("SELECT COUNT(*) FROM DatosPersonales WHERE IDUsuario = @IDUsuario");
                datosAcceso.setearParametro("@IDUsuario", idUsuario);

                int existe = Convert.ToInt32(datosAcceso.ejecutarScalar());

                if (existe == 0)
                {
                    datosAcceso.setearConsulta(@"INSERT INTO DatosPersonales 
                                    (IDUsuario, DNI, Nombre, Apellido, Domicilio, Pais, Provincia, Telefono) 
                                    VALUES (@IDUsuario, @DNI, @Nombre, @Apellido, @Domicilio, @Pais, @Provincia, @Telefono)");

                    datosAcceso.setearParametro("@IDUsuario", idUsuario);
                    datosAcceso.setearParametro("@DNI", datosPersonales.DNI);
                    datosAcceso.setearParametro("@Nombre", datosPersonales.Nombre);
                    datosAcceso.setearParametro("@Apellido", datosPersonales.Apellido);
                    datosAcceso.setearParametro("@Domicilio", datosPersonales.Domicilio);
                    datosAcceso.setearParametro("@Pais", datosPersonales.Pais);
                    datosAcceso.setearParametro("@Provincia", datosPersonales.Provincia);
                    datosAcceso.setearParametro("@Telefono", datosPersonales.Telefono);
                    datosAcceso.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar datos personales.", ex);
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }
        }

        public int RegistrarPedido(int? idUsuario)
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                datosAcceso.setearConsulta(@"
                INSERT INTO Pedidos (IDUsuario, FechaPedido, Estado) 
                OUTPUT INSERTED.ID 
                VALUES (@IDUsuario, GETDATE(), 'Pendiente')");
                datosAcceso.setearParametro("@IDUsuario", idUsuario ?? (object)DBNull.Value);

                return Convert.ToInt32(datosAcceso.ejecutarScalar());
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

        public void RegistrarDetallesPedido(int idPedido, List<Articulo> productos)
        {
            foreach (var producto in productos)
            {
                AccesoDatos datosAcceso = new AccesoDatos();

                try
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
                catch (Exception ex)
                {
                    throw new Exception($"Error al registrar el producto con ID {producto.ID}.", ex);
                }
                finally
                {
                    datosAcceso.cerrarConexion();
                    datosAcceso.limpiarParametros();
                }
            }
        }

        public List<Pedido> ObtenerPedidosPorUsuario(int idUsuario)
        {
            List<Pedido> pedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
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
                throw new Exception("Error al obtener los pedidos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<DetallesPedidos> ObtenerDetallesPorPedido(int idPedido)
        {
            List<DetallesPedidos> detalles = new List<DetallesPedidos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT IDDetallePedido, IDPedido, IDArticulo, Cantidad, PrecioUnitario 
            FROM DetallesPedidos 
            WHERE IDPedido = @IDPedido");

                datos.setearParametro("@IDPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallesPedidos detalle = new DetallesPedidos
                    {
                        IDDetallePedido = (int)datos.Lector["IDDetallePedido"],
                        IDPedido = (int)datos.Lector["IDPedido"],
                        IDArticulo = (int)datos.Lector["IDArticulo"],
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]
                    };
                    detalles.Add(detalle);
                }

                return detalles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los detalles del pedido", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ModificarEstadoPedido(int idPedido, string nuevoEstado)
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            Pedido pedido = ObtenerPedidoPorId(idPedido);
            if (pedido.Estado == nuevoEstado)
            {
                // Esto lanza una excepción si el estado es el mismo que antes
                throw new Exception("El pedido ya está en el estado seleccionado.");
            }

            try
            {
                datosAcceso.setearConsulta("UPDATE Pedidos SET Estado = @Estado WHERE ID = @IDPedido");
                datosAcceso.setearParametro("@Estado", nuevoEstado);
                datosAcceso.setearParametro("@IDPedido", idPedido);
                datosAcceso.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado del pedido.", ex);
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }
        }



        public List<Pedido> ObtenerTodosLosPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Modificamos la consulta para que traiga todos los pedidos
                datos.setearConsulta("SELECT ID, FechaPedido, Estado FROM Pedidos");

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
                throw new Exception("Error al obtener todos los pedidos.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Pedido ObtenerPedidoPorId(int idPedido)
        {
            Pedido pedido = null;
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                string query = "SELECT * FROM Pedidos WHERE ID = @ID";
                datosAcceso.setearConsulta(query);
                datosAcceso.setearParametro("@ID", idPedido);

                datosAcceso.ejecutarLectura();

                if (datosAcceso.Lector.Read())
                {
                    pedido = new Pedido();

                    if (datosAcceso.Lector["ID"] != DBNull.Value)
                    {
                        pedido.ID = Convert.ToInt32(datosAcceso.Lector["ID"]);
                    }

                    if (datosAcceso.Lector["FechaPedido"] != DBNull.Value)
                    {
                        pedido.FechaPedido = Convert.ToDateTime(datosAcceso.Lector["FechaPedido"]);
                    }

                    if (datosAcceso.Lector["Estado"] != DBNull.Value)
                    {
                        pedido.Estado = datosAcceso.Lector["Estado"].ToString();
                    }
                    else
                    {
                        pedido.Estado = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el pedido por ID.", ex);
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }

            return pedido;
        }



    }
}
