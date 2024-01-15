using CashStream.Clases;
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
    public partial class FPrincipal : Form
    {
        public FPrincipal()
        {
            InitializeComponent();
        }

        private void FPrincipal_Load(object sender, EventArgs e)
        {
            TabControl.SelectedIndex = 1;
            txtAño.Text = DateTime.Now.Year.ToString();
            cboMes.SelectedIndex = (DateTime.Now.Month - 1);
            ListarMovimiento();

            ResumenIngreso();
            ResumenGasto();
        }

        private void ResumenIngreso()
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@Mes", (cboMes.SelectedIndex + 1)),
                new Parametro("@Año", txtAño.Text)
            };

            dgvRIngreso.DataSource = DbDatos.Listar("IngresoResumen_Listar", parametros);
        }
        private void ResumenGasto()
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@Mes", (cboMes.SelectedIndex + 1)),
                new Parametro("@Año", txtAño.Text)
            };

            dgwRGastos.DataSource = DbDatos.Listar("GastosResumen_Listar", parametros);
        }

        private void ListarMovimiento()
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@Mes", (cboMes.SelectedIndex + 1)),
                new Parametro("@Año", txtAño.Text)
            };

            dgvMovimiento.DataSource = DbDatos.Listar("Movimiento_Listar", parametros);
            DbDatos.OcultarIds(dgvMovimiento);
            dgvMovimiento.Columns["Movimiento"].Visible = false;
            dgvMovimiento.Columns["Descripcion"].Width = 100;
            dgvMovimiento.Columns["Monto"].Width = 70;
            dgvMovimiento.Columns["Fecha"].Width = 80;
            

            pintar();

            ResumenIngreso();
            ResumenGasto();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FIngreso frm = new FIngreso();
            frm.ShowDialog();

            ListarMovimiento();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarMovimiento();
            pintar();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FGastos frm = new FGastos();
            frm.ShowDialog();

            ListarMovimiento();
        }
        public void pintar()
        {
            decimal TIngreso = 0, TGasto = 0;

            foreach (DataGridViewRow fila in dgvMovimiento.Rows)
            {
                string movimiento = fila.Cells["Movimiento"].Value.ToString();
                decimal monto = Convert.ToDecimal(fila.Cells["Monto"].Value);

                if (movimiento.Equals("I"))
                {
                    fila.DefaultCellStyle.BackColor = Color.Lime;
                    TIngreso += monto;
                }
                else
                {
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
                    TGasto += monto;
                }
            }

            txtIngreso.Text = TIngreso.ToString("N2");
            txtGasto.Text = TGasto.ToString("N2");
            txtSaldo.Text = (TIngreso - TGasto).ToString("N2");
        }

        private void editarMovimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMovimiento.CurrentRow != null)
            {
                string movimiento = dgvMovimiento.CurrentRow.Cells["Movimiento"].Value.ToString();
                int idMovimiento = Convert.ToInt32(dgvMovimiento.CurrentRow.Cells["IdMovimiento"].Value);
                string descripcion = dgvMovimiento.CurrentRow.Cells["Descripcion"].Value.ToString();
                string monto = dgvMovimiento.CurrentRow.Cells["Monto"].Value.ToString();
                string tipo = dgvMovimiento.CurrentRow.Cells["Tipo"].Value.ToString();
                string fecha = dgvMovimiento.CurrentRow.Cells["Fecha"].Value.ToString();

                if (movimiento.Equals("I"))
                {
                    FIngreso frm = new FIngreso();
                    frm.txtDescripcion.Text = descripcion;
                    frm.txtMonto.Text = monto;
                    frm.Tipo = tipo;
                    frm.IdIngreso = idMovimiento;
                    frm.Editar = true;
                    frm.dtpFecha.Value = Convert.ToDateTime(fecha).Date;
                    frm.ShowDialog();
                }
                else
                {
                    FGastos frm = new FGastos();
                    frm.txtDescripcion.Text = descripcion;
                    frm.txtMonto.Text = monto;
                    frm.Tipo = tipo;
                    frm.IdGasto = idMovimiento;
                    frm.Editar = true;
                    frm.dtpFecha.Value = Convert.ToDateTime(fecha).Date;
                    frm.ShowDialog();
                }

                ListarMovimiento();
            }
            
        }
    }
}
