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
                accesoDBArticulos.SetConsulta("select a.Id, a.Codigo, a.Nombre,a.Descripcion,m.Descripcion as Marca,c.Descripcion as Categoria, a.ImagenUrl, a.Precio, IdMarca, IdCategoria from articulos a,categorias c, marcas m where m.Id = a.idMarca and c.id = a.IdCategoria");
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
                auxArticulos.ImagenUrl = (string)accesoDBArticulos.Lectura["ImagenUrl"];

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
            AgregarEnDB.SetConsulta($"insert into ARTICULOS (Codigo,Nombre,IdMarca,IdCategoria,Descripcion,ImagenUrl, Precio)values(@Codigo, @Nombre, @IdMarca, @IdCategoria, @Descripcion,@ImagenUrl, @Precio)");


            AgregarEnDB.SetInsert("@Codigo",(nuevo1.Codigo));
            AgregarEnDB.SetInsert("@Nombre",(nuevo1.Nombre));
            AgregarEnDB.SetInsert("@Descripcion",(nuevo1.Descripcion));
            AgregarEnDB.SetInsert("@ImagenUrl",(nuevo1.ImagenUrl));
            AgregarEnDB.SetInsert("@Precio", ((decimal)nuevo1.Precio));
            AgregarEnDB.SetInsert("@IdMarca", nuevo1.Marca.Id);
            AgregarEnDB.SetInsert("@IdCategoria", nuevo1.Categoria.Id);
            AgregarEnDB.EjecutarLectura();

            }
            catch (Exception ex)
            {

                
            }
            finally
            {
                AgregarEnDB.CerrarConexion();
            }

        }

       public void Modificar(Articulos modificar1)
        {
            ConexionDB ModificarEnDB = new ConexionDB();
            try
            {
            ModificarEnDB.ConexionRaiz();
            ModificarEnDB.SetConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Descripcion = @Descripcion, ImagenUrl = @ImagenUrl, Precio = @Precio where id = @Id");
            ModificarEnDB.SetInsert("Codigo", modificar1.Codigo);
            ModificarEnDB.SetInsert("Nombre", modificar1.Nombre);
            ModificarEnDB.SetInsert("IdMarca", modificar1.Marca.Id);
            ModificarEnDB.SetInsert("IdCategoria", modificar1.Categoria.Id);
            ModificarEnDB.SetInsert("Descripcion", modificar1.Descripcion);
            ModificarEnDB.SetInsert("ImagenUrl", modificar1.ImagenUrl);
            ModificarEnDB.SetInsert("Precio", modificar1.Precio);
            ModificarEnDB.SetInsert("Id", modificar1.Id);
            ModificarEnDB.EjecutarLectura();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ModificarEnDB.CerrarConexion();
            }

        }
        
       public void Eliminar(Articulos eliminar1)


        {
            ConexionDB EliminarEnDB = new ConexionDB();

            try
            {
            EliminarEnDB.ConexionRaiz();
            EliminarEnDB.SetConsulta("delete ARTICULOS where id = @Id");
            EliminarEnDB.SetInsert("Id",eliminar1.Id);
            EliminarEnDB.EjecutarLectura();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                EliminarEnDB.CerrarConexion();
            }
        }


       public List<Articulos> BusquedaConFiltro(string filtro, string buscar)
        {
            List<Articulos> ListaArticuloBusqueda = new List<Articulos>();
            ConexionDB BusquedaEnDB = new ConexionDB();
            string consulta = "select a.Id, a.Codigo, a.Nombre,a.Descripcion,m.Descripcion as Marca,c.Descripcion as Categoria, a.ImagenUrl, a.Precio, IdMarca, IdCategoria from articulos a,categorias c, marcas m where m.Id = a.idMarca and c.id = a.IdCategoria and ";
            switch (filtro)
            {
                case "Nombre":
                    consulta += ($"Nombre like '%{buscar}%'");
                break;

                case "Marca":
                    consulta += ($"m.Descripcion like '%{buscar}%'");
                break;
                
                case "Categoria":
                    consulta += ($"c.Descripcion like '%{buscar}%'");
                break;
                
                

            }

            BusquedaEnDB.ConexionRaiz();
            BusquedaEnDB.SetConsulta(consulta);
            BusquedaEnDB.ConexionOpen();
            while (BusquedaEnDB.Lectura.Read())
            {
                Articulos auxArticulos = new Articulos();

                auxArticulos.Id = (int)BusquedaEnDB.Lectura["Id"];
                auxArticulos.Codigo = (string)BusquedaEnDB.Lectura["Codigo"];
                auxArticulos.Nombre = (string)BusquedaEnDB.Lectura["Nombre"];
                auxArticulos.Descripcion = (string)BusquedaEnDB.Lectura["Descripcion"];

                auxArticulos.Marca = new Marcas();
                auxArticulos.Marca.Descripcion = (string)BusquedaEnDB.Lectura["Marca"];
                auxArticulos.Marca.Id = (int)BusquedaEnDB.Lectura["IdMarca"];

                auxArticulos.Categoria = new Categorias();
                auxArticulos.Categoria.Descripcion = (string)BusquedaEnDB.Lectura["Categoria"];
                auxArticulos.Categoria.Id = (int)BusquedaEnDB.Lectura["IdCategoria"];

                auxArticulos.Precio = (double)(decimal)BusquedaEnDB.Lectura["Precio"];
                auxArticulos.ImagenUrl = (string)BusquedaEnDB.Lectura["ImagenUrl"];

                if (!(BusquedaEnDB.Lectura["ImagenUrl"] is DBNull))
                {
                auxArticulos.ImagenUrl = (string)BusquedaEnDB.Lectura["ImagenUrl"];
                }


                ListaArticuloBusqueda.Add(auxArticulos);


            }
            return ListaArticuloBusqueda;
            BusquedaEnDB.CerrarConexion();

        }

    }
}
