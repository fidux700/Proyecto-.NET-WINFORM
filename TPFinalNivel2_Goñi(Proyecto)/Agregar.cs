using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPFinalNivel2_Goñi_Proyecto_
{
    public partial class Agregar : Form
    {
        public Agregar()
        {
            InitializeComponent();
        }
        
        //HELPER----------------------------------------------------------

        //private Articulos Articulo1 = ;

        private void Agregar_Load(object sender, EventArgs e)
        {
            MarcasNegocio ObjMarcasNegocio = new MarcasNegocio();
            CategoriasNegocio ObjCategoriasNegocio = new CategoriasNegocio();

            comboBoxMarca.ValueMember = "Id";
            comboBoxMarca.DisplayMember = "Marca";
            comboBoxMarca.DataSource = ObjMarcasNegocio.LLamarMarcas();
            comboBoxMarca.SelectedIndex = -1;

            comboBoxCategoria.ValueMember = "Id";
            comboBoxCategoria.DisplayMember = "Categoria";
            comboBoxCategoria.DataSource = ObjCategoriasNegocio.LlamarCategorias();
            comboBoxCategoria.SelectedIndex  = -1;




        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            

            ArticulosNegocio AgregarArticulosNegocio = new ArticulosNegocio();
            //Articulos AgregarArticulos = new Articulos();
            Articulos Articulo1 = new Articulos();

            Articulo1.Codigo = textBoxCodigo.Text;
            Articulo1.Nombre = textBoxNombre.Text;
            Articulo1.Marca = (Marcas)comboBoxMarca.SelectedItem;
            Articulo1.Categoria = (Categorias)comboBoxCategoria.SelectedItem;
            Articulo1.Descripcion = textBoxDescripcion.Text;
            Articulo1.Precio = (double)(decimal)textBoxPrecio.Text.Length;
            AgregarArticulosNegocio.Agregar(Articulo1);
            MessageBox.Show("Guardado");
            Close();

        }
    }
}
