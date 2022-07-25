using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class Estado
    {
        private int id_estado;
        private string nombre_estado;

        public int Id_estado { get => id_estado; set => id_estado = value; }
        public string Nombre_estado { get => nombre_estado; set => nombre_estado = value; }
    }
}
