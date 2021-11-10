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
            if (!string.IsNullOrEmpty(txtClaveLibro.Text))
            {
                msj = "Debe agregar una clave del libro";
                txtClaveLibro.Focus();
            }
            else if (!string.IsNullOrEmpty(txtTituloLibro.Text))
            {
                msj = "Debe agregar un título del libro";
                txtTituloLibro.Focus();
            }
            else if (!string.IsNullOrEmpty(txtClaveAutor.Text))
            {
                msj = "Debe agregar la clave del autor";
                txtClaveAutor.Focus();
            }
            else if (!string.IsNullOrEmpty(txtClaveCat.Text))
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
            LNLibro lnLibro= new LNLibro();
            if (validarTextos())
            {
                libro = new ELibro(txtClaveLibro.Text,txtTituloLibro.Text,txtClaveAutor.Text,categoria, false);

                try
                {
                    if (!lnLibro.libroRepetido(libro))
                    {
                        if (!lnLibro.claveLibroRepetida(libro.ClaveLibro))
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
