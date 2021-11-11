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
            txtClaveAutor.Text = "A0023";
            txtClaveCat.Clear();
            txtClaveLibro.Clear();
            txtTituloLibro.Clear();
            txtClaveLibro.Focus();
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
            validarTextos();
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ELibro libro;
            LNLibro lnLibro= new LNLibro(Config.getCadenaConexion);
            if (validarTextos())
            {
                libro = new ELibro(txtClaveLibro.Text,txtTituloLibro.Text,txtClaveAutor.Text,categoria, false);

                try
                {
                    if (!lnLibro.libroRepetido(libro))
                    {
                        if (!lnLibro.claveLibroRepetida(libro.ClaveLibro))
                        {
                            if (lnLibro.insertarLibro(libro) > 0 )
                            {
                                MessageBox.Show("Guardado con exito");
                            }
                            else
                            {

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

        }

        private void llenarDVG(string condicion = "")
        {
            DataSet dataSet;
            LNLibro lnLibro = new LNLibro(Config.getCadenaConexion);

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

           // dataGridView1.AutoResizeColumn(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }


        private void mensajeError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
