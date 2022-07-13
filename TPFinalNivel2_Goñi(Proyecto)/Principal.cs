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

       //HELPER----------------------------------------------------------
       public void CargaTablaArticulos()
        {
            try
            {
                ArticulosNegocio negocio1 = new ArticulosNegocio();
                ListaArticulosMain = negocio1.LlamarArticulos();
                dgvArticulos.DataSource = ListaArticulosMain;
                pictureBoxArticulos.Load(ListaArticulosMain[0].ImagenUrl);
                dgvArticulos.Columns["Codigo"].Visible = false;
                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["Descripcion"].Visible = false;
                dgvArticulos.Columns["ImagenUrl"].Visible = false;
                panel1.Visible = false;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }

       //----------------------------------------------------------------







        private void Principal_Load(object sender, EventArgs e)
        {
            CargaTablaArticulos();
            
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulos Seleccion = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            dgvArticulos.Columns["Id"].Visible = false;
            pictureBoxArticulos.Load(Seleccion.ImagenUrl);
            textBoxNombre.Text = Seleccion.Nombre.ToUpper();
            textBoxCodArticulo.Text = Seleccion.Codigo.ToUpper();
            textBoxDescripcion.Text = Seleccion.Descripcion.ToUpper();

        }

        private void listBoxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listBoxDescripcion.Enabled = false;
            //listBoxDescripcion.BackColor = Color.LightGray;
        }

        private void buttonDetalle_Click(object sender, EventArgs e)
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

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            Agregar AgregarArticulo = new Agregar();
            AgregarArticulo.ShowDialog();




        }
    }
}
