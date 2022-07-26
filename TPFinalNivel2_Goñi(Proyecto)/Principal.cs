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
using System.Data.SqlClient;


namespace TPFinalNivel2_Goñi_Proyecto_
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private List<Articulos> ListaArticulosMain;

       //HELPER------------------------------------------------------------

       public void CargaTablaArticulos()
        {
               ArticulosNegocio negocio1 = new ArticulosNegocio();
                ListaArticulosMain = negocio1.LlamarArticulos();
                dgvArticulos.DataSource = ListaArticulosMain;
                
                    if (dgvArticulos.Rows.Count < 1 || dgvArticulos.Rows == null)
                    {

                        buttonDetalle.Enabled = false;
                        buttonModificar.Enabled = false;
                        buttonEliminar.Enabled = false;
                        buttonBusqueda.Enabled = false;
                        

                    }
                    else
                    {
                        buttonDetalle.Enabled = true;
                        buttonModificar.Enabled = true;
                        buttonEliminar.Enabled = true;
                        buttonBusqueda.Enabled = true;
                        dgvArticulos.CurrentCell = dgvArticulos.Rows[0].Cells[2];
                    }
                
            
        }
       public void CargaImagen(string imagen)
        {
            try
            {
                pictureBoxArticulos.Load(imagen);
            }
            catch (Exception)
            {

                pictureBoxArticulos.Load("https://www.agora-gallery.com/advice/wp-content/uploads/2015/10/image-placeholder.png");
            }

        }
        public void BotonAgregar()
        {
            Secundario AgregarArticulo = new Secundario();
            AgregarArticulo.ShowDialog();
            CargaTablaArticulos();
        }
        public void BotonModificar()
        {
            Articulos ArtVariable1;
            ArtVariable1 = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            Secundario ModificarArticulo = new Secundario(ArtVariable1);
            ModificarArticulo.ShowDialog();
            CargaTablaArticulos();
        }
        public void BotonEliminar()
        {
               
                if (!buttonEliminar.Enabled)
                {
                    MessageBox.Show("Primero debe ingresar un registro para que se pueda activar el boton");
                }
                else
                {
                    DialogResult = MessageBox.Show("¿Está seguro que desea eliminar?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult == DialogResult.No)
                    {
                        CargaTablaArticulos();
                    }
                    else
                    {
                        Articulos eliminar2;
                        eliminar2 = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                        ArticulosNegocio EliminarArticulo = new ArticulosNegocio();
                        EliminarArticulo.Eliminar(eliminar2);
                        if (panel1.Visible)
                        {
                            buttonDetalle.Text = "VER DETALLE";
                            panel1.Visible = false;

                        }
                        CargaTablaArticulos();
                    }
                }
                
            
            
            

        }
        public void BotonVerDetalle()
        {
            if (!buttonDetalle.Enabled)
            {
                MessageBox.Show("Primero debe ingresar un registro para que se pueda activar el boton");
            }
            else
            {
            if (panel1.Visible)
            {
                buttonDetalle.Text = "VER DETALLE";
                panel1.Visible = false;
            }
            else
            {
                buttonDetalle.Text = "OCULTAR DETALLE";
                panel1.Visible = true;
            }

            }

        }

       //HELPERFIN----------------------------------------------------------------







        private void Principal_Load(object sender, EventArgs e)
        {       
                CargaTablaArticulos();
                buttonQuitarFiltro.Visible = false;
                comboBoxFiltro.SelectedIndex = -1;
                comboBoxFiltro.Items.Add("Nombre");
                comboBoxFiltro.Items.Add("Marca");
                comboBoxFiltro.Items.Add("Categoria");
                dgvArticulos.Columns["Codigo"].Visible = false;
                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["Descripcion"].Visible = false;
                dgvArticulos.Columns["ImagenUrl"].Visible = false;
                panel1.Visible = false;
            
        }
        

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulos Seleccion = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            dgvArticulos.Columns["Id"].Visible = false;
            CargaImagen(Seleccion.ImagenUrl);
            textBoxNombre.Text = Seleccion.Nombre;
            textBoxCodArticulo.Text = Seleccion.Codigo;
            textBoxDescripcion.Text = Seleccion.Descripcion;
        }
            
        private void buttonDetalle_Click(object sender, EventArgs e)
        {
            BotonVerDetalle();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            BotonAgregar();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            BotonModificar();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
                BotonEliminar();
        }

        private void comboBoxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxBuscar.Clear();     
        }

        private void buttonBusqueda_Click(object sender, EventArgs e)
        {
            ArticulosNegocio articulosNegocioFiltro = new ArticulosNegocio();
            try
            {
                string filtro = comboBoxFiltro.SelectedItem.ToString();
                string buscar = textBoxBuscar.Text;
                buscar = buscar.ToUpper();
                dgvArticulos.DataSource = articulosNegocioFiltro.BusquedaConFiltro(filtro, buscar);
                dgvArticulos.Visible = true;
                buttonQuitarFiltro.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese el filtro y el contenido a buscar","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
            
        }

        private void buttonQuitarFiltro_Click(object sender, EventArgs e)
        {
            CargaTablaArticulos();
            buttonQuitarFiltro.Visible = false;
        }

        private void utilidadDeAplicaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El propósito del programa tiene como deber realizar la simulación de un catálogo de productos el cual lleva la conexión a una base de datos en donde se almacena cada inserción y modificación de los mismos.","Información");
        }

        private void desarrolloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aplicación desarrollada por Fernando Martín Goñi","Información");
        }

        
        private void AtajoAgregar_Click(object sender, EventArgs e)
        {
            BotonAgregar();
        }

        private void AtajoModificar_Click(object sender, EventArgs e)
        {
            try
            {
            BotonModificar();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Primero debe ingresar un registro para que se pueda activar el boton");
            }
        }
        private void AtajoEliminar_Click(object sender, EventArgs e)
        {
            
            BotonEliminar();
        }

        private void AtajoVerDetalle_Click(object sender, EventArgs e)
        {
            
            BotonVerDetalle();
            
            
        }
        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.Alt && e.KeyCode == Keys.D1)
                {
                AtajoAgregar.Enabled = true;
                }
                else if (e.Alt && e.KeyCode == Keys.D2)
                {
                    AtajoModificar.Enabled = true;
                }
                else if (e.Alt && e.KeyCode == Keys.D3)
                {
                    AtajoEliminar.Enabled = true;
                }
                else if (e.Alt && e.KeyCode == Keys.D)
                {
                    AtajoVerDetalle.Enabled = true;
                }

            
        }

        
    }
}
