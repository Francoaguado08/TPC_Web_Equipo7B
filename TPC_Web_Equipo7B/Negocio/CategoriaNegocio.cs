﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select ID, Nombre From Categorias");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.ID = (int)datos.Lector["ID"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

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
        
    
        public void agregarCategoria(Categoria nueva)
    {
        AccesoDatos datos = new AccesoDatos();
        try
        {
            datos.setearConsulta("insert into Categorias (Nombre) VALUES (@Nombre)");
            datos.setearParametro("@Nombre", nueva.Nombre);
            datos.ejecutarAccion();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { datos.cerrarConexion(); }
    }
        public void eliminarCategoria(int ID)
         {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Categorias WHERE ID = @ID");
                datos.setearParametro("@ID", ID);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public void modificarCategoria(Categoria CategoriaMod)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Categorias SET Nombre = @Nombre WHERE ID = @ID");
                datos.setearParametro("@ID", CategoriaMod.ID);
                datos.setearParametro("@Nombre", CategoriaMod.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public bool ExisteNombreCategoria(string nombre, int idExcluido = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Categorias WHERE Nombre = @Nombre AND ID != @ID");
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@ID", idExcluido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0] > 0;
                }

                return false;
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


    }//cierre de CategoriaNegocio
}
