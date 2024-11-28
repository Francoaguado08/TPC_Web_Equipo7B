using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.ID, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.Stock, " +
                                     "M.Nombre AS Marca, C.Nombre AS Categoria, I.ImagenURL " +
                                     "FROM ARTICULOS A " +
                                     "LEFT JOIN MARCAS M ON A.IDMarca = M.Id " +
                                     "LEFT JOIN CATEGORIAS C ON A.IDCategoria = C.ID " +
                                     "LEFT JOIN IMAGENES I ON A.ID = I.IDArticulo");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idActual = (int)datos.Lector["ID"];
                    Articulo aux = lista.FirstOrDefault(a => a.ID == idActual);

                    if (aux == null)
                    {
                        aux = new Articulo
                        {
                            ID = (int)datos.Lector["ID"],
                            Codigo = (string)datos.Lector["Codigo"],
                            Nombre = (string)datos.Lector["Nombre"],
                            Descripcion = (string)datos.Lector["Descripcion"],
                            Precio = (decimal)datos.Lector["Precio"],
                            Stock = datos.Lector["Stock"] != DBNull.Value ? (int)datos.Lector["Stock"] : 0, // Manejo del campo Stock
                            Marca = new Marca
                            {
                                Nombre = datos.Lector["Marca"] != DBNull.Value ? (string)datos.Lector["Marca"] : ""
                            },
                            Categoria = new Categoria
                            {
                                Nombre = datos.Lector["Categoria"] != DBNull.Value ? (string)datos.Lector["Categoria"] : ""
                            },
                            ImagenURL = new List<string>() // Inicialización de la lista para imágenes
                        };
                        lista.Add(aux);
                    }

                    // Agregar URL de imagen si existe
                    if (!Convert.IsDBNull(datos.Lector["ImagenURL"]))
                    {
                        aux.ImagenURL.Add((string)datos.Lector["ImagenURL"]);
                    }
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


        public void agregar(Articulo nuevo)
        {
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                // Agregamos Stock en la consulta SQL
                acceso.setearConsulta("INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, Stock, IDMarca, IDCategoria) " +
                                      "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @Stock, @IDMarca, @IDCategoria)");

                acceso.setearParametro("@Codigo", nuevo.Codigo);
                acceso.setearParametro("@Nombre", nuevo.Nombre);
                acceso.setearParametro("@Descripcion", nuevo.Descripcion);
                acceso.setearParametro("@Precio", nuevo.Precio);
                acceso.setearParametro("@Stock", nuevo.Stock); // Incluimos el parámetro Stock
                acceso.setearParametro("@IDMarca", nuevo.Marca.ID);
                acceso.setearParametro("@IDCategoria", nuevo.Categoria.ID);

                acceso.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acceso.cerrarConexion();
            }
        }


        public int agregarr(Articulo nuevo)
        {
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                // Incluimos Stock en la consulta SQL
                acceso.setearConsulta("INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, Stock, IDMarca, IDCategoria) " +
                                      "OUTPUT INSERTED.ID " +
                                      "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @Stock, @IDMarca, @IDCategoria)");

                acceso.setearParametro("@Codigo", nuevo.Codigo);
                acceso.setearParametro("@Nombre", nuevo.Nombre);
                acceso.setearParametro("@Descripcion", nuevo.Descripcion);
                acceso.setearParametro("@Precio", nuevo.Precio);
                acceso.setearParametro("@Stock", nuevo.Stock); // Incluimos el parámetro Stock
                acceso.setearParametro("@IDMarca", nuevo.Marca.ID);
                acceso.setearParametro("@IDCategoria", nuevo.Categoria.ID);

                int idArticulo = acceso.ejecutarScalar(); // Devuelve el ID del artículo insertado
                return idArticulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acceso.cerrarConexion();
            }
        }


        public void agregarImagen(int idArticulo, string imagenURL)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Imagenes (IDArticulo, ImagenURL) VALUES (@IDArticulo, @ImagenURL)");
                datos.setearParametro("@IDArticulo", idArticulo);
                datos.setearParametro("@ImagenURL", imagenURL);
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

        public void eliminarArticulo(int ID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Articulos WHERE ID = @ID");
                datos.setearParametro("@ID", ID);
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

        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE Articulos SET " +
                                  "Codigo = @Codigo, " +
                                  "Nombre = @Nombre, " +
                                  "Precio = @Precio, " +
                                  "Descripcion = @Descripcion, " +
                                  "Stock = @Stock " + // Agregamos el campo Stock
                                  "WHERE ID = @ID";

                datos.setearConsulta(consulta);
                datos.setearParametro("@ID", modificar.ID);
                datos.setearParametro("@Codigo", modificar.Codigo);
                datos.setearParametro("@Nombre", modificar.Nombre);
                datos.setearParametro("@Descripcion", modificar.Descripcion);
                datos.setearParametro("@Precio", modificar.Precio);
                datos.setearParametro("@Stock", modificar.Stock); // Incluimos el parámetro Stock

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


        public Articulo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.ID, A.Codigo, A.Nombre, A.Descripcion, A.IDCategoria, A.IDMarca, A.Precio, A.Stock, " +
                                  "C.Nombre AS NombreCategoria, M.Nombre AS NombreMarca " +
                                  "FROM Articulos A " +
                                  "INNER JOIN Categorias C ON A.IDCategoria = C.ID " +
                                  "INNER JOIN Marcas M ON A.IDMarca = M.ID " +
                                  "WHERE A.ID = @ID";

                datos.setearConsulta(consulta);
                datos.setearParametro("@ID", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo
                    {
                        ID = (int)datos.Lector["ID"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        Precio = (decimal)datos.Lector["Precio"],
                        Stock = (int)datos.Lector["Stock"], // Incluyendo el campo Stock
                        Categoria = new Categoria
                        {
                            ID = (int)datos.Lector["IDCategoria"],
                            Nombre = (string)datos.Lector["NombreCategoria"]
                        },
                        Marca = new Marca
                        {
                            ID = (int)datos.Lector["IDMarca"],
                            Nombre = (string)datos.Lector["NombreMarca"]
                        }
                    };

                    return articulo;
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


        public void AgregarStock(int idArticulo, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET Stock = Stock + @Cantidad WHERE ID = @ID");
                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@ID", idArticulo);
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

        public void modificarMarcaArticulo(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET IDMarca = @IDMarca WHERE ID = @IDArticulo");
                datos.setearParametro("@IDArticulo", modificar.ID);
                datos.setearParametro("@IDMarca", modificar.Marca.ID);
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

        public void modificarCategoriaArticulo(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET IDCategoria = @IDCategoria WHERE ID = @IDArticulo");
                datos.setearParametro("@IDArticulo", modificar.ID);
                datos.setearParametro("@IDCategoria", modificar.Categoria.ID);
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

        public List<Articulo> listarFiltrados(string campo, string criterio)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion AS Descripcion, " +
                                  "M.Nombre AS Marca, C.Nombre AS Categoria, A.Precio, A.Stock, " + // Incluimos Stock
                                  "M.Id AS IDMarca, C.Id AS IDCategoria " +
                                  "FROM Articulos AS A " +
                                  "INNER JOIN Marcas AS M ON M.Id = A.IdMarca " +
                                  "INNER JOIN Categorias AS C ON C.Id = A.IdCategoria ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Ascendente":
                            consulta += "ORDER BY A.Precio ASC";
                            break;
                        case "Descendente":
                            consulta += "ORDER BY A.Precio DESC";
                            break;
                    }
                }
                else if (campo == "Categoría")
                {
                    consulta += "WHERE C.Nombre = @criterio ";
                }
                else if (campo == "Marca")
                {
                    consulta += "WHERE M.Nombre = @criterio ";
                }

                datos.setearConsulta(consulta);
                if (campo == "Categoría" || campo == "Marca")
                {
                    datos.setearParametro("@criterio", criterio);
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        ID = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        Precio = (decimal)datos.Lector["Precio"],
                        Stock = datos.Lector["Stock"] != DBNull.Value ? (int)datos.Lector["Stock"] : 0, // Manejo del campo Stock
                        Marca = new Marca
                        {
                            Nombre = (string)datos.Lector["Marca"],
                            ID = (int)datos.Lector["IDMarca"]
                        },
                        Categoria = new Categoria
                        {
                            Nombre = (string)datos.Lector["Categoria"],
                            ID = (int)datos.Lector["IDCategoria"]
                        }
                    };

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

    }
}
