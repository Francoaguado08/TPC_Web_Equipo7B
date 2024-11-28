using Dominio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidoNegocio
    {

       /// ------------------------------------------------------------------------------------------------------
        
        
        //Metodo principal para registrar el pedido.
        public bool RegistrarPedidoCompleto(int? idUsuario, CarritoCompras carrito, DatosPersonales datos)
        {
            try
            {
                // Validar carrito
                if (carrito == null || carrito.ObtenerProductos().Count == 0)
                {
                    throw new Exception("El carrito está vacío.");
                }

                // Validar datos personales
                if (datos == null)
                {
                    throw new Exception("No se proporcionaron datos personales.");
                }

                PedidoNegocio pedidoNegocio = new PedidoNegocio();

                // 1. Registrar datos personales si aplica
                if (idUsuario.HasValue)
                {
                    pedidoNegocio.RegistrarDatosPersonales(idUsuario.Value, datos);
                }

                // 2. Registrar el pedido
                int idPedido = pedidoNegocio.RegistrarPedido(idUsuario);

                // 3. Registrar los detalles del pedido
                pedidoNegocio.RegistrarDetallesPedido(idPedido, carrito.ObtenerProductos());

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al completar el registro del pedido.", ex);
            }
        }





       // Si el usuario es registrado y no tiene datos personales asociados, o si es un cliente no registrado,
       // insertamos sus datos en la tabla DatosPersonales.
        public void RegistrarDatosPersonales(int idUsuario, DatosPersonales datosPersonales)
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                // Verificar si ya existen datos personales
                datosAcceso.setearConsulta("SELECT COUNT(*) FROM DatosPersonales WHERE IDUsuario = @IDUsuario");
                datosAcceso.setearParametro("@IDUsuario", idUsuario);
            
                int existe = Convert.ToInt32(datosAcceso.ejecutarScalar());

                if (existe == 0) // Si no existen los datos, registrar --->
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



        //Este método registra el pedido y devuelve el ID generado.
        public int RegistrarPedido(int? idUsuario)
        {
            AccesoDatos datosAcceso = new AccesoDatos();

            try
            {
                // INSERTED.ID --> devuelve el valor de la columna ID del nuevo registro.
                //Para que lo quiero ?  datosAcceso.ejecutarScalar(): Ejecuta la consulta y obtiene el valor de INSERTED.ID (el ID del pedido recién creado).
                //
                datosAcceso.setearConsulta(@"
                INSERT INTO Pedidos (IDUsuario, FechaPedido, Estado) 
                OUTPUT INSERTED.ID 
                VALUES (@IDUsuario, GETDATE(), 'Pendiente')");
                datosAcceso.setearParametro("@IDUsuario", idUsuario ?? (object)DBNull.Value); //si idUsuario tiene un valor, se usa directamente.
                                                                                              //Si es null, se pasa DBNull.Value a la base de datos.
                // Si idUsuario es null, se pasa DBNull.Value a la base de datos

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



        //Este método agrega los productos del carrito a la tabla DetallesPedidos.
        public void RegistrarDetallesPedido(int idPedido, List<Articulo> productos)
        {
            foreach (var producto in productos)
            {
                AccesoDatos datosAcceso = new AccesoDatos(); // Crear una nueva instancia en cada iteración
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
                    // Asegúrate de cerrar la conexión y limpiar parámetros después de cada iteración
                    datosAcceso.cerrarConexion();
                    datosAcceso.limpiarParametros();
                }
            }
        }





        /// ------------------------------------------------------------------------------------------------------------------


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
