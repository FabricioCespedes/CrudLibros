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
    public partial class frmPrestamos : Form
    {
        LNPrestamo lNPrestamo = new LNPrestamo(Config.getCadenaConexion);

        EPrestamo ePrestamo = new EPrestamo();
        public frmPrestamos()
        {
            InitializeComponent();
        }

        private void llenarDGVEjemplares(string condicion = "")
        {
            DataSet dataSet;

            try
            {
                dataSet = lNPrestamo.listarEjemplares(condicion);


                dvgEjemplares.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }


            dvgEjemplares.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dvgEjemplares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void mensajeError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmPrestamos_Load(object sender, EventArgs e)
        {
            try
            {
                llenarDGVEjemplares();
                llenarDGVUsuarios();
                llenarDGVPrestamos();
                
               
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }




        }

        


        private void llenarDGVUsuarios(string condicion = "")
        {
            List<EUsuario> listaUsuarios;

            try
            {
                listaUsuarios = lNPrestamo.listarUsuarios(condicion);

                dgvUsuarios.DataSource = listaUsuarios;
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }


            dgvUsuarios.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvUsuarios.Columns[1].Visible = false;
            dgvUsuarios.Columns[4].Visible = false;
            dgvUsuarios.Columns[5].Visible = false;
            dgvUsuarios.Columns[6].Visible = false;
            dgvUsuarios.Columns[7].Visible = false;


        }

        private void llenarDGVPrestamos(string condicion = "")
        {
            DataSet dataSet;

            try
            {
                dataSet = lNPrestamo.listarPrestamos(condicion);

                dgvPrestamos.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }


            dgvPrestamos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgvPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        


        private void dgvUsuarios_DoubleClick(object sender, EventArgs e)
        {
            int indiceFila = dgvUsuarios.CurrentRow.Index;

            string clave = dgvUsuarios[0, indiceFila].Value.ToString();

            string condicion = $" claveUsuario= '{clave}' ";

            try
            {
                if (lNPrestamo.listarUsuarios(condicion).Count > 0)
                {
                    txtClaveUsuario.Text = " ";
                    txtClaveUsuario.Text = lNPrestamo.listarUsuarios(condicion)[0].ClaveUsuario;
                }


            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }



        private void dvgEjemplares_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indiceFila = dvgEjemplares.CurrentRow.Index;

            string clave = dvgEjemplares[0, indiceFila].Value.ToString();

            string titulo = dvgEjemplares[4, indiceFila].Value.ToString();
            string condicion = $" E.claveEjemplar= '{clave}' and L.Titulo = '{titulo}'";

            try
            {
                if (lNPrestamo.listarEjemplares(condicion).Tables[0].Rows.Count > 0)
                {

                    txtCodigoEjemplar.Text = " ";
                    txtCodigoEjemplar.Text = clave;
                    txtTitulo.Text = " ";
                    txtTitulo.Text = titulo;
                }


            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void dgvPrestamos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indiceFila = dgvPrestamos.CurrentRow.Index;

            string clave = dgvPrestamos[0, indiceFila].Value.ToString();

            string condicion = $" P.clavePrestamo= '{clave}' ";

            try
            {
                if (lNPrestamo.listarPrestamos(condicion).Tables[0].Rows.Count > 0)
                {
                    txtPrestamo.Text = clave;
                    txtClaveUsuario.Text = dgvPrestamos[1, indiceFila].Value.ToString();

                    txtCodigoEjemplar.Text = dgvPrestamos[3, indiceFila].Value.ToString();
                    txtTitulo.Text = dgvPrestamos[4, indiceFila].Value.ToString();
                    dtpDevolucion.Value = Convert.ToDateTime(dgvPrestamos[5, indiceFila].Value);

                    dtpPrestamo.Value = Convert.ToDateTime(dgvPrestamos[6, indiceFila].Value);
                    btnEliminar.Enabled = true;
                    btnActualizar.Enabled = true;
                }


            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        


        private void insertarPrestamo()
        {
            try
            {
                string condicion = $" P.clavePrestamo= '{txtPrestamo.Text.ToString()}' ";
                if (lNPrestamo.listarPrestamos(condicion).Tables[0].Rows.Count == 0)
                {
                    condicion = $" EST.descripcion = 'Disponible' AND E.claveEjemplar = '{txtCodigoEjemplar.Text.ToString()}' AND L.titulo = '{txtTitulo.Text.ToString()}' ";
                    if (lNPrestamo.listarEjemplares(condicion).Tables[0].Rows.Count > 0)
                    {
                        condicion = $" U.claveUsuario = '{txtClaveUsuario.Text.ToString()}' AND EST.descripcion = 'Prestamo'; ";

                        if (lNPrestamo.listarPrestamos(condicion).Tables[0].Rows.Count == 0)
                        {
                            condicion = $" E.claveEjemplar= '{txtCodigoEjemplar.Text.ToString()}' ";
                            if (lNPrestamo.listarEjemplares(condicion).Tables[0].Rows.Count > 0)
                            {
                                condicion = $" claveUsuario= '{txtClaveUsuario.Text.ToString()}' ";
                                if (lNPrestamo.listarUsuarios(condicion).Count > 0)
                                {
                                    EEjemplar eEjemplar = new EEjemplar();
                                    eEjemplar.ClaveEjemplar = txtCodigoEjemplar.Text.ToString();
                                    EUsuario eUsuario = new EUsuario();
                                    eUsuario.ClaveUsuario = txtClaveUsuario.Text.ToString();
                                    ePrestamo.ClavePrestamo = txtPrestamo.Text.ToString();
                                    ePrestamo.EUsuario = eUsuario;
                                    ePrestamo.EEjemplar = eEjemplar;
                                    ePrestamo.FechaDevolucion = dtpDevolucion.Value;
                                    ePrestamo.FechaPrestamo = dtpPrestamo.Value;
                                    int num = lNPrestamo.insertarPrestamos(ePrestamo);
                                    if (num != 0 )
                                    {
                                        MessageBox.Show("Guardado con exito");
                                    }
                                    else
                                    {
                                        MessageBox.Show("NO se ha guardado");
                                    }
                                   
                                }
                                else
                                {
                                    MessageBox.Show("No existe un usuario con ese codigo");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No existe un ejemplar con ese codigo");
                                txtCodigoEjemplar.Focus();
                            }
                    }
                        else
                        {
                            MessageBox.Show("El usuario tiene libros pendientes");
                            txtClaveUsuario.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("El ejemplar seleccionado no esta disponible");
                        txtCodigoEjemplar.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ese codigo de prestamo ya esta en uso");
                    txtPrestamo.Focus();
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarTextos())
            {
                insertarPrestamo();
            }


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            txtClaveUsuario.Text = "";
            txtCodigoEjemplar.Text = "";
            txtPrestamo.Text = "";
            txtTitulo.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EEjemplar eEjemplar = new EEjemplar();
                eEjemplar.ClaveEjemplar = txtCodigoEjemplar.Text.ToString();
                EUsuario eUsuario = new EUsuario();
                eUsuario.ClaveUsuario = txtClaveUsuario.Text.ToString();
                ePrestamo.ClavePrestamo = txtPrestamo.Text.ToString();
                ePrestamo.EUsuario = eUsuario;
                ePrestamo.EEjemplar = eEjemplar;
                ePrestamo.FechaDevolucion = dtpDevolucion.Value;
                ePrestamo.FechaPrestamo = dtpPrestamo.Value;
                int num = lNPrestamo.eliminar(ePrestamo);
                if (num != 0)
                {
                    MessageBox.Show("Eliminado con exito");
                }
                else
                {
                    MessageBox.Show("NO se ha eliminado");
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private bool validarTextos()
        {
            bool resultado = false;
            string msj = "";
            if (string.IsNullOrEmpty(txtPrestamo.Text))
            {
                msj = "Debe agregar una clave de prestamos";
                txtPrestamo.Focus();
            }
            else if (string.IsNullOrEmpty(txtCodigoEjemplar.Text))
            {
                msj = "Debe agregar un codigo de ejemplar";
                txtCodigoEjemplar.Focus();
            }
            else if (string.IsNullOrEmpty(txtClaveUsuario.Text))
            {
                msj = "Debe agregar la clave del usuario";
                txtClaveUsuario.Focus();
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                if (validarTextos())
                {

                    EEjemplar eEjemplar = new EEjemplar();
                    eEjemplar.ClaveEjemplar = txtCodigoEjemplar.Text.ToString();
                    EUsuario eUsuario = new EUsuario();
                    eUsuario.ClaveUsuario = txtClaveUsuario.Text.ToString();
                    ePrestamo.ClavePrestamo = txtPrestamo.Text.ToString();
                    ePrestamo.EUsuario = eUsuario;
                    ePrestamo.EEjemplar = eEjemplar;
                    ePrestamo.FechaDevolucion = dtpDevolucion.Value;
                    ePrestamo.FechaPrestamo = dtpPrestamo.Value;
                    num =  lNPrestamo.modificar(ePrestamo);
                }
                if (num != 0)
                {
                    MessageBox.Show("Actualizado con exito");
                }
                else
                {
                    MessageBox.Show("No se ha actualizado");
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void txtBuscarLibro_TextChanged(object sender, EventArgs e)
        {
            string cadena = txtBuscarLibro.Text;

            string condicion = $" L.titulo like '%{cadena}%'  ";
            llenarDGVEjemplares(condicion);
        }

        private void txtBuscarUsuario_TextChanged(object sender, EventArgs e)
        {
            string cadena = txtBuscarUsuario.Text;

            string condicion = $" nombre like '%{cadena}%' or apPaterno like '%{cadena}%' ";
            llenarDGVUsuarios(condicion);
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (validarTextos())
                {
                    string condicion = $" P.clavePrestamo= '{txtPrestamo.Text.ToString()}' ";
                    if (lNPrestamo.listarPrestamos(condicion).Tables[0].Rows.Count == 1) // Validar si el codigo de prestamo existe
                    {
                        condicion = $" EST.descripcion = 'Prestamo' AND E.claveEjemplar = '{txtCodigoEjemplar.Text.ToString()}' AND L.titulo = '{txtTitulo.Text.ToString()}' ";

                        // Validar si el libro esta prestado
                        if (lNPrestamo.listarEjemplares(condicion).Tables[0].Rows.Count > 0) 
                        {

                            condicion = $" e.claveEjemplar = '{txtCodigoEjemplar.Text}' AND l.titulo = '{txtTitulo.Text}' ";

                            ELibro libro = lNPrestamo.listarLibros(condicion);

                            

                        }
                        else
                        {
                            MessageBox.Show("El ejemplar seleccionado esta disponible");
                            txtCodigoEjemplar.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El codigo del prestamo no existe");
                        txtPrestamo.Focus();
                    }

                }

            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }
    }
}
