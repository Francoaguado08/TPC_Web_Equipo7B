﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcasNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select ID, Nombre From Marcas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
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

        public void agregarMarcas(Marca nueva)
        {
            if (ExisteNombreMarca(nueva.Nombre))
            {
                throw new Exception("La marca ya existe.");
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Marcas (Nombre) VALUES (@Nombre)");
                datos.setearParametro("@Nombre", nueva.Nombre);
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


        public void eliminarMarca(int ID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Marcas WHERE ID = @ID");
                datos.setearParametro("@ID", ID);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void modificarMarca(Marca MarcaMod)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Marcas SET Nombre = @Nombre WHERE ID = @ID");
                datos.setearParametro("@ID", MarcaMod.ID);
                datos.setearParametro("@Nombre", MarcaMod.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public bool ExisteNombreMarca(string nombre, int idExcluido = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Marcas WHERE Nombre = @Nombre AND ID != @ID");
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




    }
}

