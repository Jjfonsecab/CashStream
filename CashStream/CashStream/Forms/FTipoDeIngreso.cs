using CashStream.Clases;
using CashStream.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashStream
{
    public partial class FTipoDeIngreso : Form
    {
        public FTipoDeIngreso()
        {
            InitializeComponent();
        }

        bool Editar;
        int IdTipoIngreso;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            if (!guardar()) return;

            finalizar();
            
        }

        private void finalizar()
        {
            ListarGrid();
            limpiar();
        }

        private void limpiar()
        {
            txtIngreso.Text = "";
            Editar = false;
        }

        private bool guardar()
        {
            TipoIngreso tipoIngreso = new TipoIngreso()
            {
                Denominacion = txtIngreso.Text,
                IdTipoIngreso = IdTipoIngreso
            };
            return TipoIngreso.Guardar(tipoIngreso, Editar);
        }

        private bool validacion()
        {
            if (string.IsNullOrWhiteSpace(txtIngreso.Text))
            {
                MessageBox.Show("Ingresar Denominacion");
                return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListarGrid();
        }

        private void ListarGrid()
        {
            dgvDatos.DataSource = TipoIngreso.Listar();
            DbDatos.OcultarIds(dgvDatos);
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdTipoIngreso = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdTipoIngreso"].Value);
            txtIngreso.Text = dgvDatos.CurrentRow.Cells["Denominacion"].Value.ToString();
            Editar = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtIngreso_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar el registro?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    int idTipoIngreso = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdTipoIngreso"].Value);

                    if (TipoIngreso.Eliminar(idTipoIngreso))
                    {
                        MessageBox.Show("Registro eliminado correctamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            ListarGrid();
            limpiar();
        }
    }
}
