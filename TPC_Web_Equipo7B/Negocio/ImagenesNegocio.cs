using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenesNegocio
    {



        //CONSULTA SQL CHEQUEADA!
        public List<Imagen> listar()
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ID, IDArticulo, ImagenURL FROM Imagenes ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.ID = (int)datos.Lector["ID"];
                    aux.IDArticulo = (int)datos.Lector["IDArticulo"];
                    aux.ImagenURl = (string)datos.Lector["ImagenURL"];

                    lista.Add(aux);
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

        public List<Imagen> listarImagenesArticuloSeleccionado(int idArticulo)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = $"SELECT ImagenURL FROM Imagenes WHERE IDArticulo = {idArticulo}";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen
                    {
                        IDArticulo = idArticulo,
                        ImagenURl = (string)datos.Lector["ImagenUrl"]
                    };
                    lista.Add(aux);
                }

                // Si no hay imágenes, agrega una imagen predeterminada
                if (lista.Count == 0)
                {
                    lista.Add(new Imagen { ImagenURl = "https://nayemdevs.com/wp-content/uploads/2020/03/default-product-image.png" });
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

        public Articulo listarArticulo(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            Articulo articulo = null;

            try
            {
                // Consulta SQL actualizada para incluir las tablas MARCAS y CATEGORIAS
                string consulta = @"
                SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, 
                M.Descripcion AS Marca, C.Descripcion AS Categoria
                FROM ARTICULOS AS A 
                LEFT JOIN MARCAS AS M ON A.Id = M.Id
                LEFT JOIN CATEGORIAS AS C ON A.Id = C.Id
                WHERE A.Id = @IdArticulo";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // Inicializar el objeto articulo con los valores leídos
                    articulo = new Articulo
                    {
                        ID = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        Precio = (decimal)datos.Lector["Precio"],
                        Marca = new Marca
                        {
                            Nombre = datos.Lector["Marca"] != DBNull.Value ? (string)datos.Lector["Marca"] : null
                        },
                        Categoria = new Categoria
                        {
                            Nombre = datos.Lector["Categoria"] != DBNull.Value ? (string)datos.Lector["Categoria"] : null
                        }
                    };

                    // Mensaje en caso de que la categoría no esté disponible
                    if (articulo.Categoria.Nombre == null)
                    {
                        Console.WriteLine("No disponible");
                    }
                }

                return articulo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //Consulta SQL CHEQUEADA!
        public void agregarImagen(Imagen nuevaImagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Validar que el artículo existe
                datos.setearConsulta("SELECT COUNT(*) FROM Articulos WHERE ID = @IdArticulo");
                datos.setearParametro("@IdArticulo", nuevaImagen.IDArticulo);
                int existeArticulo = datos.ejecutarScalar();

                if (existeArticulo == 0)
                {
                    throw new Exception("El artículo especificado no existe en la base de datos.");
                }

                // Insertar en Imagenes
                string consulta = "INSERT INTO Imagenes (IDArticulo, ImagenURL) VALUES (@IdArticulo, @ImagenUrl);";
                datos.setearConsulta(consulta);
                datos.setearParametro("@IdArticulo", nuevaImagen.IDArticulo);
                datos.setearParametro("@ImagenUrl", nuevaImagen.ImagenURl);
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

        //Consulta SQL CHEQUEADA!
        public void actualizarImagen(Imagen nuevaImagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Actualizar IMAGENES
                string consulta = "UPDATE Imagenes SET ImagenURL = @ImagenUrl WHERE IDArticulo = @IdArticulo AND ID = @ID;";
                datos.setearConsulta(consulta);

                // Establecer parámetros para la imagen
                // Si no especificamos ID, se actualizarían TODAS las imágenes dadas de cierto artículo
                datos.setearParametro("@IdArticulo", nuevaImagen.IDArticulo);
                datos.setearParametro("@ImagenUrl", nuevaImagen.ImagenURl);
                datos.setearParametro("@ID", nuevaImagen.ID);
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
        //Consulta SQL CHEQUEADA!
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM Imagenes WHERE ID = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Verificada !
        public bool TieneProductosAsociados(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            // Consulta SQL para contar los productos asociados a la imagen
            datos.setearConsulta("SELECT COUNT(*) FROM Imagenes I INNER JOIN Articulos A ON I.IDArticulo= A.ID WHERE I.ID = @IDImagen;");
            datos.setearParametro("@IDImagen", imagen.ID);
            // Verifica cuántos productos asociados a la imagen hay
            int cantidadProductos = datos.ejecutarScalar();

            return cantidadProductos > 0;
        }
        
        public void vincularImagenes(List<Articulo> articulos, List<Imagen> imagenes)
        {

            foreach (Articulo miArticulo in articulos)
            {
                foreach (Imagen miImagen in imagenes)
                {


                    if (miImagen.IDArticulo.ToString() == miArticulo.ID.ToString())
                    {
                        miArticulo.ImagenURL.Add(miImagen.ImagenURl);
                    }
                }
            }
        }

    }
}
