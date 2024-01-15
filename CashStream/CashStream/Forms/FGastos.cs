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
    public partial class FGastos : Form
    {
        public FGastos()
        {
            InitializeComponent();
        }
        public int IdGasto;
        public bool Editar;
        public string Tipo;

        private void FGastos_Load(object sender, EventArgs e)
        {
            btnGuardar.Text = Editar ? "Actualizar" : "Agregar";
            ListarCombo();

            if(Editar)
            {
                cboTipoGasto.Text = Tipo;
            }
        }
        private void ListarCombo()
        {
            TipoGasto.ListarCombo(cboTipoGasto);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!agregar()) return;
            finalizar();
        }

        private void finalizar()
        {
            txtDescripcion.Text = "";
            txtMonto.Text = "";
            Editar = false;
            cboTipoGasto.SelectedIndex = -1;
        }

        private bool agregar()
        {
            Gastos gasto = new Gastos() 
            {
                Descripcion = txtDescripcion.Text,
                Fecha = dtpFecha.Value,
                Monto = Convert.ToDecimal(txtMonto.Text),
                IdGasto = IdGasto,
                IdTipoGasto = Convert.ToInt32(cboTipoGasto.SelectedValue)
            };
            
            if(Gastos.Agregar(gasto, Editar))
            {
                MessageBox.Show("Operacion Correcta");
                return true;
            }
            else return false;
        }

        private void btnTipoGasto_Click(object sender, EventArgs e)
        {
            FTipoDeGasto frm = new FTipoDeGasto();
            frm.ShowDialog();
        }

        private void cboTipoGasto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                ListarCombo();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            finalizar();
        }
    }
}
