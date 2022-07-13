using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class ConexionDB
    {
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        private SqlDataReader lectura;

        public SqlDataReader Lectura
        {
            get { return lectura; }
        }

        public void ConexionRaiz()
        {
           
            conexion.ConnectionString = "server=.\\SQLEXPRESS;database=CATALOGO_DB;integrated security=false;User ID=sa;Password=123456789";


        }

        public void SetConsulta(string comandoTexto)
        {
            
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = comandoTexto;

        }

        public void SetInsert(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void EjecutarLectura()
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
        }

        public void ConexionOpen()
        {
            comando.Connection = conexion;
            conexion.Open();
            lectura = comando.ExecuteReader();
        }

        public void CerrarConexion()
        {
            if (lectura != null)
            {
                lectura.Close();
                conexion.Close();
            }
        }

    }
}
