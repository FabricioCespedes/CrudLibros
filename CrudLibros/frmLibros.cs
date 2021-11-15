using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using LogicaNegocio;

namespace CrudLibros
{
    public partial class frmLibros : Form
    {
        ECategoria categoria = new ECategoria("C0001", "Comic");
        LNLibro lnLibro = new LNLibro(Config.getCadenaConexion);
        ELibro libro;

        public frmLibros()
        {
            InitializeComponent();
        }

        private void frmLibros_Load(object sender, EventArgs e)
        {
            llenarDVG();
        }

        private void limpiar()
        {
            txtClaveAutor.Text = "";
            txtClaveCat.Clear();
            txtClaveLibro.Clear();
            txtTituloLibro.Clear();
            txtClaveLibro.Focus();
            libro = null;
            btnEliminar.Enabled = false;
        }

        private bool validarTextos()
        {
            bool resultado= false;
            string msj = "";
            if (string.IsNullOrEmpty(txtClaveLibro.Text))
            {
                msj = "Debe agregar una clave del libro";
                txtClaveLibro.Focus();
            }
            else if (string.IsNullOrEmpty(txtTituloLibro.Text))
            {
                msj = "Debe agregar un título del libro";
                txtTituloLibro.Focus();
            }
            else if (string.IsNullOrEmpty(txtClaveAutor.Text))
            {
                msj = "Debe agregar la clave del autor";
                txtClaveAutor.Focus();
            }
            else if (string.IsNullOrEmpty(txtClaveCat.Text))
            {
                msj = "Debe agregar la clave de la categoria";
                txtClaveCat.Focus();
            }
            else
            {
                resultado = true;
            }
            if (!resultado)
            {
                MessageBox.Show(msj, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return resultado;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (validarTextos())
            {
                if (libro == null)
                {
                    libro = new ELibro(txtClaveLibro.Text, txtTituloLibro.Text, txtClaveAutor.Text, categoria, false);

                }
                else
                {
                    lnLibro.modificar(libro, "");
                }

                if (!libro.Existe)
                {
                    insertarLibro();
                }


            }

        }

        private void insertarLibro()
        {
            try
            {
                if (!lnLibro.libroRepetido(libro))
                {
                    if (!lnLibro.claveLibroRepetida(libro.ClaveLibro))
                    {
                        EAutor eAutor = new EAutor();
                        eAutor.ClaveAutor = txtClaveAutor.Text;
                        if (lnLibro.claveAutorExiste(eAutor))
                        {

                            if (lnLibro.claveCategoriaExiste(categoria))
                            {
                                if (lnLibro.insertarLibro(libro) > 0)
                                {
                                    MessageBox.Show("Guardado con exito");
                                }
                                else
                                {
                                    MessageBox.Show("No existe una categoria con ese código");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No existe una categoria con ese código");
                                txtClaveCat.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existe un autor con esa clave");
                            txtClaveAutor.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Clave de libro repetida");
                        txtClaveLibro.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ese título ya existe para el autor indicado");
                    txtTituloLibro.Focus();
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void llenarDVG(string condicion = "")
        {
            DataSet dataSet;

            try
            {
                dataSet = lnLibro.listarTodos(condicion);
              //  dataSet = lnLibro.listarTodos("titulo like %amor%");

                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
            dataGridView1.Columns[0].HeaderText = "Clave de libro";
            dataGridView1.Columns[1].HeaderText = "Título";
            dataGridView1.Columns[2].HeaderText = "Clave autor";
            dataGridView1.Columns[3].HeaderText = "Clave categoria";

           dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
           dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void mensajeError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta;
            
            if (libro != null && libro.Existe)
            {
                respuesta = MessageBox.Show($"Esta seguro de eliminar el libro {libro.Titulo} codigo {libro.ClaveLibro}", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    string msj = lnLibro.eliminarProcedure(libro);
                    MessageBox.Show(msj);
                    limpiar();
                    llenarDVG();
                    //if (lnLibro.eliminar(libro) == 1)
                    //{
                    //    MessageBox.Show("Elimino un libro con exito");
                    //    limpiar();
                    //    llenarDVG();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Error al eliminar libro", "Error");
                    //}
                }
                else limpiar();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int indiceFila = dataGridView1.CurrentRow.Index;

            string clave = dataGridView1[0, indiceFila].Value.ToString();

            string condicion = $" claveLibro= '{clave}' ";

            try
            {
                libro = lnLibro.devolverLibro(condicion);
                if (libro != null)
                {
                    libro.Existe = true;
                    txtClaveLibro.Text = libro.ClaveLibro;
                    txtClaveAutor.Text = libro.ClaveAutor;
                    txtClaveCat.Text = libro.ClaveCategoria.ClaveCategoria;
                    txtTituloLibro.Text = libro.Titulo;

                    btnEliminar.Enabled = true;
                }

                
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
