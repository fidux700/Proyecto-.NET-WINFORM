using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriasNegocio
    {
        public List<Categorias> LlamarCategorias()
        {
            ConexionDB accesoDBCategorias = new ConexionDB();
            List<Categorias> ListaLlamarCategorias = new List<Categorias>();

            try
            {
                accesoDBCategorias.ConexionRaiz();
                accesoDBCategorias.SetConsulta("select Id, Descripcion from Categorias");
                accesoDBCategorias.ConexionOpen();

                while (accesoDBCategorias.Lectura.Read())
                {
                    Categorias auxCategorias = new Categorias();
                    auxCategorias.Id = (int)accesoDBCategorias.Lectura["Id"];
                    auxCategorias.Descripcion = (string)accesoDBCategorias.Lectura["Descripcion"];
                    
                    ListaLlamarCategorias.Add(auxCategorias);

                }

                accesoDBCategorias.CerrarConexion();
                return ListaLlamarCategorias;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




    }
}
