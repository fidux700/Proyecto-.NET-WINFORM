using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class MarcasNegocio
    {
        public List<Marcas> LLamarMarcas()
        {
        ConexionDB accesoDBMarcas = new ConexionDB();
        List<Marcas> ListaLLamarMarcas = new List<Marcas>();

            try
            {
                accesoDBMarcas.ConexionRaiz();
                accesoDBMarcas.SetConsulta("select Id, Descripcion from Marcas");
                accesoDBMarcas.ConexionOpen();

                while (accesoDBMarcas.Lectura.Read())
                {
                    Marcas auxMarcas = new Marcas();

                    auxMarcas.Id = (int)accesoDBMarcas.Lectura["Id"];
                    auxMarcas.Descripcion = (string)accesoDBMarcas.Lectura["Descripcion"];

                    ListaLLamarMarcas.Add(auxMarcas);

                }


                accesoDBMarcas.CerrarConexion();
                return ListaLLamarMarcas;
            }
            catch (Exception)
            {

                throw;
            }


        }



    }
}
