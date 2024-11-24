using System;
using System.Collections.Generic;
using Negocio;
using Dominio;


public class DatoPersonalNegocio
{
    public List<DatosPersonales> Listar()
    {
        List<DatosPersonales> lista = new List<DatosPersonales>();
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setearConsulta("SELECT * FROM DatosPersonales");
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                DatosPersonales dato = new DatosPersonales
                {
                    ID = Convert.ToInt32(datos.Lector["ID"]),
                    IDUsuario = Convert.ToInt32(datos.Lector["IDUsuario"]),
                    DNI = datos.Lector["DNI"].ToString(),
                    Nombre = datos.Lector["Nombre"].ToString(),
                    Apellido = datos.Lector["Apellido"].ToString(),
                    Domicilio = datos.Lector["Domicilio"].ToString(),
                    Pais = datos.Lector["Pais"].ToString(),
                    Provincia = datos.Lector["Provincia"].ToString(),
                    Telefono = datos.Lector["Telefono"].ToString()
                };
                lista.Add(dato);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            datos.cerrarConexion();
        }

        return lista;
    }

    public bool Agregar(DatosPersonales datosPersonales)
    {
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setearConsulta("INSERT INTO DatosPersonales (IDUsuario, DNI, Nombre, Apellido, Domicilio, Pais, Provincia, Telefono) " +
                                 "VALUES (@IDUsuario, @DNI, @Nombre, @Apellido, @Domicilio, @Pais, @Provincia, @Telefono)");
            datos.setearParametro("@IDUsuario", datosPersonales.IDUsuario);
            datos.setearParametro("@DNI", datosPersonales.DNI);
            datos.setearParametro("@Nombre", datosPersonales.Nombre);
            datos.setearParametro("@Apellido", datosPersonales.Apellido);
            datos.setearParametro("@Domicilio", datosPersonales.Domicilio);
            datos.setearParametro("@Pais", datosPersonales.Pais);
            datos.setearParametro("@Provincia", datosPersonales.Provincia);
            datos.setearParametro("@Telefono", datosPersonales.Telefono);

            datos.ejecutarAccion();
            return true;
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

    public bool Modificar(DatosPersonales datosPersonales)
    {
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setearConsulta("UPDATE DatosPersonales SET IDUsuario = @IDUsuario, DNI = @DNI, Nombre = @Nombre, Apellido = @Apellido, " +
                                 "Domicilio = @Domicilio, Pais = @Pais, Provincia = @Provincia, Telefono = @Telefono WHERE ID = @ID");
            datos.setearParametro("@ID", datosPersonales.ID);
            datos.setearParametro("@IDUsuario", datosPersonales.IDUsuario);
            datos.setearParametro("@DNI", datosPersonales.DNI);
            datos.setearParametro("@Nombre", datosPersonales.Nombre);
            datos.setearParametro("@Apellido", datosPersonales.Apellido);
            datos.setearParametro("@Domicilio", datosPersonales.Domicilio);
            datos.setearParametro("@Pais", datosPersonales.Pais);
            datos.setearParametro("@Provincia", datosPersonales.Provincia);
            datos.setearParametro("@Telefono", datosPersonales.Telefono);

            datos.ejecutarAccion();
            return true;
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

    public bool Eliminar(int id)
    {
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setearConsulta("DELETE FROM DatosPersonales WHERE ID = @ID");
            datos.setearParametro("@ID", id);

            datos.ejecutarAccion();
            return true;
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

