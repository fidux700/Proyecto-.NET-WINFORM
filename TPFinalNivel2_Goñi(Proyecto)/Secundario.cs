using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Dominio;
using Negocio;

namespace TPFinalNivel2_Goñi_Proyecto_
{
    public partial class Secundario : Form
    {
        //HELPER----------------------------------------------------------
        private Articulos ArtVariable = null;
        private void ConfigDesplegables()
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
            comboBoxCategoria.SelectedIndex = -1;

        }
        private void DatosACargar()
        {
            if (ArtVariable == null)
            {
                ArtVariable = new Articulos();
            }

            ArtVariable.Codigo = textBoxCodigo.Text;
            ArtVariable.Nombre = textBoxNombre.Text;
            ArtVariable.Marca = (Marcas)comboBoxMarca.SelectedItem;
            ArtVariable.Categoria = (Categorias)comboBoxCategoria.SelectedItem;
            ArtVariable.Descripcion = textBoxDescripcion.Text;
            ArtVariable.ImagenUrl = textBoxImagen.Text;
            ArtVariable.Precio = double.Parse(textBoxPrecio.Text);
        }
        private void DatosAModificar()
        {
            if (ArtVariable != null)
            {
                textBoxCodigo.Text = ArtVariable.Codigo;
                textBoxNombre.Text = ArtVariable.Nombre;
                comboBoxMarca.SelectedValue = ArtVariable.Marca.Id;
                comboBoxCategoria.SelectedValue = ArtVariable.Categoria.Id;
                textBoxDescripcion.Text = ArtVariable.Descripcion;
                textBoxImagen.Text = ArtVariable.ImagenUrl;
                ValidacionCargaImagen();
                textBoxPrecio.Text = ArtVariable.Precio.ToString();
            }
        }
        private void ValidacionesCamposText()
        {
            if (ArtVariable.Codigo == "" || ArtVariable.Nombre == "" || ArtVariable.Marca == null || ArtVariable.Categoria == null)
            {
                DialogResult MsjAlerta1;
                MsjAlerta1 = MessageBox.Show("Debe completar el campo obligatorio", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MsjAlerta1 = DialogResult.None;
            }
            else if (ArtVariable.Precio == 0)
            {
                DialogResult MsjAlerta2;
                MsjAlerta2 = MessageBox.Show("El valor debe ser mayor a cero", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MsjAlerta2 = DialogResult.None;
            }
            else
            {
                MessageBox.Show("Guardado exitosamente");
                Close();
            }


        }
        private void ValidacionesPrecioText(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 0)
            {
                buttonGuardar.Enabled = true;

            }


            if (e.KeyChar == 44 && textBoxPrecio.Text.IndexOf(',') != -1)
            {
                e.Handled = true;
                return;
            }

            if ((e.KeyChar >= 32 && e.KeyChar <= 43) || (e.KeyChar >= 45 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {

                DialogResult advertencia = MessageBox.Show("Por favor, ingrese solo numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }
        private void ValidacionCargaImagen()
        {
            try
            {
                pictureBoxImagen.Load(ArtVariable.ImagenUrl);

            }
            catch (Exception ex)
            {

                pictureBoxImagen.Load("https://www.agora-gallery.com/advice/wp-content/uploads/2015/10/image-placeholder.png");
            }
        }
        

        //HELPER-FIN----------------------------------------------------------
        public Secundario()
        {
            InitializeComponent();
            Text = "ARTICULO A AGREGAR";
            pictureBoxImagen.Load("https://www.agora-gallery.com/advice/wp-content/uploads/2015/10/image-placeholder.png");
            buttonGuardar.Enabled = false;
            labelCampoObligatorio.Visible = false;
        }
        public Secundario(Articulos ArtVariable)
        {
            InitializeComponent();
            this.ArtVariable = ArtVariable;
            Text = "ARTICULO A MODIFICAR";
        }
        private void Secundario_Load(object sender, EventArgs e)
        {
            ConfigDesplegables();
            DatosAModificar();
        }


        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            ArticulosNegocio AgregarArticulosNegocio = new ArticulosNegocio();

            DatosACargar();
            if (ArtVariable.Id != 0){ 
            AgregarArticulosNegocio.Modificar(ArtVariable);
                ValidacionesCamposText();
            }
            else{ 
            AgregarArticulosNegocio.Agregar(ArtVariable);
                ValidacionesCamposText();
            }
        }

        private void textBoxPrecio_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            ValidacionesPrecioText(sender,e);
        }


        private void buttonExaminarImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivoImg = new OpenFileDialog();//CREACION DE INSTANCIA
            archivoImg.Filter = "jpg|*.jpg;|png|*.png";//TIPO DE FORMATO A EXAMINAR
            if(archivoImg.ShowDialog() == DialogResult.OK)// SI SE LE DA OK SE CUMPLE LA CONDICION
            {
                textBoxImagen.Text = archivoImg.FileName;//TEXBOX IMAGE VA A CONTENER LA IMG SELECCIONADA
                pictureBoxImagen.Load(archivoImg.FileName);// EN EL PICTUREBOX SE CARGARA LA IMAGEN SELECCIONADA
                
            }
        }

        private void textBoxCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                DialogResult advertencia2 = MessageBox.Show("Por favor, ingrese solo numeros(Sin espacios)", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void textBoxPrecio_TextChanged(object sender, EventArgs e)
        {
            if(textBoxPrecio.Text.Length == 0)
            {
                buttonGuardar.Enabled = false;
                labelCampoObligatorio.Visible = true;
            }
        }
        private void buttonAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Secundario_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
