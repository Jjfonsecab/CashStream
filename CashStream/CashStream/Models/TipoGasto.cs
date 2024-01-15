using CashStream.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashStream.Models
{
    class TipoGasto
    {
        public int IdTipoGasto { get; set; }
        public string Denominacion { get; set; }
        public static bool Guardar(TipoGasto tipoGasto, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Denominacion", tipoGasto.Denominacion),
                new Parametro("@IdTipoGasto", tipoGasto.IdTipoGasto),
                new Parametro("@Editar", editar)
            };
            return DbDatos.Ejecutar("TipoGasto_Agregar", parametros);
        }
        public static DataTable Listar()
        {
            return DbDatos.Listar("TipoDeGasto_Listar");
        }

        public static void ListarCombo(ComboBox comboBox)
        {
            DbDatos.ListarCombo(Listar(), "Denominacion", "IdTipoGasto", comboBox);
        }

        public static bool Eliminar(int idTipoGasto)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@IdTipoGasto", idTipoGasto)
            };

            return DbDatos.Ejecutar("TipoGasto_Eliminar", parametros);
        }
    }
}
