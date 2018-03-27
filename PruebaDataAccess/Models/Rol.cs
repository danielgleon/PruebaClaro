using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDataAccess.Models
{
    public class Rol : DatoBase
    {
        public List<Talento> Talentos { get; set; }
    }

    public class Talento : DatoBase
    {
        public string Apellido { get; set; }
        public string NombreCompleto { get; set; }
    }
}
