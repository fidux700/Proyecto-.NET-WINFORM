using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class ArticulosNegocio
    {
        public List<Articulos> LlamarArticulos()
        {
                ConexionDB accesoDBArticulos = new ConexionDB();
                List<Articulos> listaArticulos = new List<Articulos>();
            
                accesoDBArticulos.ConexionRaiz();
                accesoDBArticulos.SetConsulta("select a.Id, a.Codigo, a.Nombre,a.Descripcion,m.Descripcion as Marca,c.Descripcion as Categoria, a.ImagenUrl, a.Precio, a.IdMarca,a.IdCategoria from articulos a,categorias c, marcas m where m.Id = a.idMarca and c.id = a.IdCategoria");
                accesoDBArticulos.ConexionOpen();
            
                while (accesoDBArticulos.Lectura.Read())
                {
                    Articulos auxArticulos = new Articulos();

                auxArticulos.Id = (int)accesoDBArticulos.Lectura["Id"];
                auxArticulos.Codigo = (string)accesoDBArticulos.Lectura["Codigo"];
                auxArticulos.Nombre = (string)accesoDBArticulos.Lectura["Nombre"];
                auxArticulos.Descripcion = (string)accesoDBArticulos.Lectura["Descripcion"];

                auxArticulos.Marca = new Marcas();
                auxArticulos.Marca.Descripcion = (string)accesoDBArticulos.Lectura["Marca"];
                auxArticulos.Marca.Id = (int)accesoDBArticulos.Lectura["IdMarca"];

                auxArticulos.Categoria = new Categorias();
                auxArticulos.Categoria.Descripcion = (string)accesoDBArticulos.Lectura["Categoria"];
                auxArticulos.Categoria.Id = (int)accesoDBArticulos.Lectura["IdCategoria"];

                auxArticulos.Precio = (double)(decimal)accesoDBArticulos.Lectura["Precio"];

                if (!(accesoDBArticulos.Lectura["ImagenUrl"] is DBNull))
                {
                auxArticulos.ImagenUrl = (string)accesoDBArticulos.Lectura["ImagenUrl"];
                }
                

                    listaArticulos.Add(auxArticulos);

                

                }
            
                accesoDBArticulos.CerrarConexion();
                return listaArticulos;

        }

       public void Agregar(Articulos nuevo1)
        {
            ConexionDB AgregarEnDB = new ConexionDB();
            try
            {
            AgregarEnDB.ConexionRaiz();
            AgregarEnDB.SetConsulta($"insert into ARTICULOS (Codigo,Nombre,IdMarca,IdCategoria,Descripcion,Precio)values({nuevo1.Codigo},'{nuevo1.Nombre}',@IdMarca,@IdCategoria,'{nuevo1.Descripcion}',{nuevo1.Precio})");
            AgregarEnDB.SetInsert("@IdMarca", nuevo1.Marca.Id);
            AgregarEnDB.SetInsert("@IdCategoria", nuevo1.Categoria.Id);
            AgregarEnDB.EjecutarLectura();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                AgregarEnDB.CerrarConexion();
            }

        }

    }
}
