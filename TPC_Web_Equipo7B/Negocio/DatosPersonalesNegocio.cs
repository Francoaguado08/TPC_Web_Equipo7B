using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DatosPersonalesNegocio
    {



        public DatosPersonales ObtenerDatosPersonales(int idUsuario)
        {
            AccesoDatos datosAcceso = new AccesoDatos();
            DatosPersonales datos = null;

            try
            {
                datosAcceso.setearConsulta("SELECT * FROM DatosPersonales WHERE IDUsuario = @IDUsuario");
                datosAcceso.setearParametro("@IDUsuario", idUsuario);

                datosAcceso.ejecutarLectura();

                if (datosAcceso.Lector.Read())
                {
                    datos = new DatosPersonales
                    {
                        ID = datosAcceso.Lector["ID"] != DBNull.Value ? (int)datosAcceso.Lector["ID"] : 0,
                        IDUsuario = datosAcceso.Lector["IDUsuario"] != DBNull.Value ? (int)datosAcceso.Lector["IDUsuario"] : 0,
                        DNI = datosAcceso.Lector["DNI"] != DBNull.Value ? datosAcceso.Lector["DNI"].ToString() : string.Empty,
                        Nombre = datosAcceso.Lector["Nombre"] != DBNull.Value ? datosAcceso.Lector["Nombre"].ToString() : string.Empty,
                        Apellido = datosAcceso.Lector["Apellido"] != DBNull.Value ? datosAcceso.Lector["Apellido"].ToString() : string.Empty,
                        Domicilio = datosAcceso.Lector["Domicilio"] != DBNull.Value ? datosAcceso.Lector["Domicilio"].ToString() : string.Empty,
                        Pais = datosAcceso.Lector["Pais"] != DBNull.Value ? datosAcceso.Lector["Pais"].ToString() : string.Empty,
                        Provincia = datosAcceso.Lector["Provincia"] != DBNull.Value ? datosAcceso.Lector["Provincia"].ToString() : string.Empty,
                        Telefono = datosAcceso.Lector["Telefono"] != DBNull.Value ? datosAcceso.Lector["Telefono"].ToString() : string.Empty
                    };
                }

                datosAcceso.Lector.Close();
            }
            catch (Exception ex)
            {
                // Manejar el error adecuadamente
                throw ex;
            }
            finally
            {
                datosAcceso.cerrarConexion();
            }

            return datos;
        }

    }
}
