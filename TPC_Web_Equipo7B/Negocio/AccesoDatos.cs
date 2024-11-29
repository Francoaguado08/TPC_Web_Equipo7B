using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AccesoDatos
    {

        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=EcommerceDB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearProcedimiento(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;


        }

        public int EJECUTARACCION()
        {
            try
            {
                return comando.ExecuteNonQuery(); // Retorna el número de filas afectadas
            }
            catch (Exception ex)
            {
                throw ex; // Lanza la excepción para ser manejada en niveles superiores
            }
        }





        public void setearConsulta(string consulta)
        {
            if (conexion.State == ConnectionState.Open)
            {
                cerrarConexion(); // Cierra la conexión abierta antes de configurar una nueva
            }

            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conexion.Close(); }
        }

        public int ejecutarScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                int cantidad = (int)comando.ExecuteScalar();
                return cantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerrarConexion()
        {
            if (lector != null && !lector.IsClosed)
            {
                lector.Close();  // Cierra el lector si está abierto
            }

            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();  // Cierra la conexión si está abierta
            }
        }


        public void limpiarParametros()
        {
            if (comando.Parameters != null)
            {
                comando.Parameters.Clear();
            }
        }



    }



}

