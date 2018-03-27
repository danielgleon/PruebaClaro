using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDataAccess.Models
{
    public class Pelicula : DatoBase
    {
        public string Titulo { get; set; }
        public string TituloOriginal { get; set; }

        public string ImageHorizontal { get; set; }
        public string ImageVertical { get; set; }
        public string ImageLarge { get; set; }
        public string ImageMedium { get; set; }
        public string ImageSmall { get; set; }
        public string ImageBackground { get; set; }


        public TimeSpan Duracion { get; set; }
        public int Anio { get; set; }
        public Rating Rating { get; set; }

        public List<DatoBase> Generos { get; set; }
        public List<Rol> Roles { get; set; }
    }

    public class Rating : DatoBase
    {
        public string Codigo { get; set; }
    }
}
