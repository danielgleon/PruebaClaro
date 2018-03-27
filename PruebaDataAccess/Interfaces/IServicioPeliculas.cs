using PruebaDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDataAccess.Interfaces
{
    public interface IServicioPeliculas
    {
        Task<List<Pelicula>> ObtenerPeliculas();
        Task<Pelicula> ObtenerDetallePelicula(int Id);
    }
}
