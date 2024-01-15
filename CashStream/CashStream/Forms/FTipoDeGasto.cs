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

namespace CashStream.Forms
{
    public partial class FTipoDeGasto : Form
    {
        public FTipoDeGasto()
        {
            InitializeComponent();
        }

        bool Editar;
        int IdTipoGasto;

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
            txtGasto.Text = "";
            Editar = false;
        }

        private bool guardar()
        {
            TipoGasto tipoGasto = new TipoGasto()
            {
                Denominacion = txtGasto.Text,
                IdTipoGasto = IdTipoGasto
            };
            return TipoGasto.Guardar(tipoGasto, Editar);
        }

        private bool validacion()
        {
            if (string.IsNullOrWhiteSpace(txtGasto.Text))
            {
                MessageBox.Show("Ingresar Denominacion");
                return false;
            }
            return true;
        }
        private void FTipoDeGasto_Load(object sender, EventArgs e)
        {
            btnGuardar.Text = Editar ? "Actualizar" : "Agregar";
            ListarGrid();
        }

        private void ListarGrid()
        {
            dgvDatos.DataSource = TipoGasto.Listar();
            DbDatos.OcultarIds(dgvDatos);
        }


        private void editarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            IdTipoGasto = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdTipoGasto"].Value);
            txtGasto.Text = dgvDatos.CurrentRow.Cells["Denominacion"].Value.ToString();
            Editar = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar el registro?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    int idTipoGasto = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdTipoGasto"].Value);
                    
                    if (TipoGasto.Eliminar(idTipoGasto))
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
