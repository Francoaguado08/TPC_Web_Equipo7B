﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {

        // Modifico el metodo listar para manejar multiples imagenes si es que lo contiene el articulo....
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>(); // Crea una lista de los 
            AccesoDatos datos = new AccesoDatos();


            try
            {
                //datos.setearProcedimiento("SP_ListarArticulos");
                datos.setearConsulta("SELECT A.ID, A.Codigo, A.Nombre, A.Descripcion AS Descripcion, M.Nombre AS Marca, C.Nombre AS Categoria, A.Precio, I.ImagenURL FROM ARTICULOS  AS A LEFT JOIN MARCAS M ON A.IDMarca = M.Id LEFT JOIN CATEGORIAS C ON A.IDCategoria = C.ID LEFT JOIN IMAGENES I ON A.ID = I.IDArticulo");
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

                            Marca = new Marca
                            {
                               Nombre = datos.Lector["Marca"] != DBNull.Value ? (string)datos.Lector["Marca"] : ""
                            },
                           // Este código solo está asignando el Nombre de la marca(columna "Marca" en el resultado de la consulta).
                            Categoria = new Categoria
                            {
                               Nombre = datos.Lector["Categoria"] != DBNull.Value ? (string)datos.Lector["Categoria"] : ""
                            }
                        };
                        lista.Add(aux);
                    }

                    if (!Convert.IsDBNull(datos.Lector["ImagenURL"]))
                    {
                        aux.ImagenURL.Add(   (string)datos.Lector["ImagenURL"]);
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

                acceso.setearConsulta("insert into Articulos (Codigo, Nombre, Descripcion, Precio, IDMarca, IDCategoria ) VALUES (@Codigo, @Nombre, @Descripcion,@Precio, @IDMarca, @IDCategoria)");

                acceso.setearParametro("@Codigo", nuevo.Codigo);
                acceso.setearParametro("@Nombre", nuevo.Nombre);
                acceso.setearParametro("@Descripcion", nuevo.Descripcion);

                acceso.setearParametro("@Precio", nuevo.Precio);

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
                // Cerrar la conexión
                acceso.cerrarConexion();
            }
        }



        //este metodo de agregar devuelve INT, lo utilizo en el btnGuardar de alta de Producto.
        public int agregarr(Articulo nuevo)
        {
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                acceso.setearConsulta("INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, IDMarca, IDCategoria) " +
                                      "OUTPUT INSERTED.ID " +
                                      "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IDMarca, @IDCategoria)");

                acceso.setearParametro("@Codigo", nuevo.Codigo);
                acceso.setearParametro("@Nombre", nuevo.Nombre);
                acceso.setearParametro("@Descripcion", nuevo.Descripcion);
                acceso.setearParametro("@Precio", nuevo.Precio);
                acceso.setearParametro("@IDMarca", nuevo.Marca.ID);
                acceso.setearParametro("@IDCategoria", nuevo.Categoria.ID);

                int idArticulo = acceso.ejecutarScalar(); // Cambiar a un método que devuelva el valor
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



        //Perfecto!
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
            finally { datos.cerrarConexion(); }
        }

        
        
        
        // --- MODIFICAR (arranca acá) ---
        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET Codigo = @Codigo, Nombre = @Nombre, Precio = @Precio, Descripcion = @Descripcion WHERE ID = @ID");
                datos.setearParametro("@ID", modificar.ID);
                datos.setearParametro("@Codigo", modificar.Codigo);
                datos.setearParametro("@Nombre", modificar.Nombre);
                datos.setearParametro("@Descripcion", modificar.Descripcion);
                datos.setearParametro("@Precio", modificar.Precio);
                datos.cerrarConexion();
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCategoriaArticulo(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Actualiza la categoría de un artículo (IDCategoria) en la tabla Articulos
                datos.setearConsulta("UPDATE Articulos SET IDCategoria = @IDCategoria WHERE ID = @IDArticulo");

                // Establecer los parámetros de la consulta
                datos.setearParametro("@IDArticulo", modificar.ID); // El ID del artículo a modificar
                datos.setearParametro("@IDCategoria", modificar.Categoria.ID); // El ID de la nueva categoría

                // Ejecutar la acción (update) antes de cerrar la conexión
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Asegurarse de cerrar la conexión
                datos.cerrarConexion();
            }
        }

        public void modificarMarcaArticulo(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Actualiza la marca de un artículo (IDMarca) en la tabla Articulos
                datos.setearConsulta("UPDATE Articulos SET IDMarca = @IDMarca WHERE ID = @IDArticulo");

                // Establecer los parámetros de la consulta
                datos.setearParametro("@IDArticulo", modificar.ID); // El ID del artículo a modificar
                datos.setearParametro("@IDMarca", modificar.Marca.ID); // El ID de la nueva marca

                // Ejecutar la acción antes de cerrar la conexión
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Asegurarse de cerrar la conexión
                datos.cerrarConexion();
            }
        }


        
        public void modificarImagenArticulo(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Verificar si hay al menos una imagen en la lista
                if (modificar.ImagenURL != null && modificar.ImagenURL.Count > 0)
                {
                    // Actualiza la URL de la primera imagen asociada al artículo
                    datos.setearConsulta("UPDATE Imagenes SET ImagenURL = @ImagenURL WHERE ID = (SELECT TOP 1 ID FROM Imagenes WHERE IDArticulo = @IDArticulo)");

                    // Establecer los parámetros de la consulta
                    datos.setearParametro("@IDArticulo", modificar.ID); // El ID del artículo cuyo imagen se modificará
                    datos.setearParametro("@ImagenURL", modificar.ImagenURL[0]); // La nueva URL de la imagen (suponiendo que es la primera imagen)

                    // Ejecutar la acción antes de cerrar la conexión
                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Asegurarse de cerrar la conexión
                datos.cerrarConexion();
            }
        }

        //public List<Articulo> listarFiltrados(string campo, string criterio)
        //{
        //    throw new NotImplementedException();
        //}



        // AGREGO FUNCION LISTARFILTADOS PARA MI DEFAULT.ASPX
        public List<Articulo> listarFiltrados(string campo, string criterio)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion AS Descripcion, " +
                                  "M.Nombre AS Marca, C.Nombre AS Categoria, A.Precio, " +
                                  "M.Id AS IDMarca, C.Id AS IDCategoria " +
                                  "FROM Articulos AS A " +
                                  "INNER JOIN Marcas AS M ON M.Id = A.IdMarca " +
                                  "INNER JOIN Categorias AS C ON C.Id = A.IdCategoria ";

                // Aplicar filtros según el campo
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

                // Imprimir la consulta generada (para depuración)
                Console.WriteLine(consulta);

                // Configurar parámetros para evitar inyección SQL
                datos.setearConsulta(consulta);
                if (campo == "Categoría" || campo == "Marca")
                {
                    datos.setearParametro("@criterio", criterio);
                }

                // Ejecutar lectura de datos
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.ID = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca
                    {
                        Nombre = (string)datos.Lector["Marca"],
                        ID = (int)datos.Lector["IDMarca"]
                    };

                    aux.Categoria = new Categoria
                    {
                        Nombre = (string)datos.Lector["Categoria"],
                        ID = (int)datos.Lector["IDCategoria"]
                    };

                    lista.Add(aux);
                }

                return lista;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de SQL: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }


        // Método para obtener un artículo por su ID






        public Articulo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.ID, A.Codigo, A.Nombre, A.Descripcion, A.IDCategoria, A.IDMarca, A.Precio, " +
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

                return null; // Si no se encuentra el artículo
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
















    }//Fin
}
