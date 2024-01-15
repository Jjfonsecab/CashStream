using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CashStream.Clases;

namespace CashStream.Models
{
    class TipoIngreso
    {
        public int IdTipoIngreso { get; set; }
        public string Denominacion { get; set; }
        public static bool Guardar(TipoIngreso tipoIngreso, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Denominacion", tipoIngreso.Denominacion),
                new Parametro("@IdTipoIngreso", tipoIngreso.IdTipoIngreso),
                new Parametro("@Editar", editar)
            };
            return DbDatos.Ejecutar("TipoIngreso_Agregar", parametros);
        }
        public static DataTable Listar()
        {
            return DbDatos.Listar("TipoDeIngreso_Listar");
        }
        public static void ListarCombo(ComboBox comboBox)
        {
            DbDatos.ListarCombo(Listar(), "Denominacion", "IdTipoIngreso", comboBox);
        }

        internal static bool Eliminar(int idTipoIngreso)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@IdTipoIngreso", idTipoIngreso)
            };

            return DbDatos.Ejecutar("TipoIngreso_Eliminar", parametros);
        }
    }
}
