﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Configuration.ConfigurationManager;

namespace CashStream.Clases
{
    class DbDatos
    {
        public static string stringConnection = ConnectionStrings["stringConnection"].ConnectionString;
        static SqlConnection connection = new SqlConnection(stringConnection);

        static void AbrirConexion()
        {
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
        }
        static void CerrarConexion()
        {
            if (connection.State == System.Data.ConnectionState.Closed) connection.Close();
        }
        public static bool Ejecutar(string nombreProcedimiento, List<Parametro> parametros = null)
        {
            try
            {
                AbrirConexion();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        if (!parametro.Salida)
                        {
                            cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                        }
                        else
                        {
                            cmd.Parameters.Add(parametro.Nombre, SqlDbType.VarChar, 150).Direction = ParameterDirection.Output;
                        }
                    }
                }
                int e = cmd.ExecuteNonQuery();

                for (int i = 0; i < parametros.Count; i++)
                {
                    string mensaje = cmd.Parameters[i].Value.ToString();
                    if (string.IsNullOrEmpty(mensaje))
                    {
                        MessageBox.Show(mensaje);
                    }
                }
                return e > 0 ? true : false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }
        public static DataTable Listar(string nombreProcedimiento, List<Parametro> parametros = null)
        {
            try
            {
                AbrirConexion();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }
                }
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                CerrarConexion();
            }
        }
        public static void OcultarIds(DataGridView dataGrid)
        {
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                if (column.Name.Substring(0, 2).ToUpper().Equals("ID"))
                {
                    dataGrid.Columns[column.Index].Visible = false;
                }
            }
        }
        public static void ListarCombo(DataTable dataTable, string valor, string id, ComboBox comboBox)
        {
            try
            {
                if(comboBox.Items.Count > 0)
                {
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                }

                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = dataTable.Columns[valor].ColumnName;
                comboBox.ValueMember = dataTable.Columns[id].ColumnName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
