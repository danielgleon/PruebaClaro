using PruebaClaro.Util;
using PruebaDataAccess.Interfaces;
using PruebaDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaClaro.ViewModels
{
    public class VMDetallePelicula : VMBase
    {
        public VMDetallePelicula(IServicioPeliculas servicio)
        {
            _servicio = servicio;
        }


        #region Propiedades
        private IServicioPeliculas _servicio;

        private Pelicula _pelicula;
        public Pelicula Pelicula
        {
            get { return _pelicula; }
            set
            {
                _pelicula = value;

                Escritores = Pelicula?.Roles?.SingleOrDefault(x => x.Nombre == "Writer")?.Talentos;
                Actores = Pelicula?.Roles?.SingleOrDefault(x => x.Nombre == "Actor")?.Talentos;
                Directores = Pelicula?.Roles?.SingleOrDefault(x => x.Nombre == "Director")?.Talentos;
                Productores = Pelicula?.Roles?.SingleOrDefault(x => x.Nombre == "Productor")?.Talentos;

                NotifyPropertyChanged();
            }
        }

        private List<Talento> _escritores;
        public List<Talento> Escritores
        {
            get { return _escritores; }
            set
            {
                _escritores = value;
                NotifyPropertyChanged();
            }
        }

        private List<Talento> _directores;
        public List<Talento> Directores
        {
            get { return _directores; }
            set
            {
                _directores = value;
                NotifyPropertyChanged();
            }
        }

        private List<Talento> _productores;
        public List<Talento> Productores
        {
            get { return _productores; }
            set
            {
                _productores = value;
                NotifyPropertyChanged();
            }
        }

        private List<Talento> _actores;
        public List<Talento> Actores
        {
            get { return _actores; }
            set
            {
                _actores = value;
                NotifyPropertyChanged();
            }
        }


        private bool _cargando;
        public bool Cargando
        {
            get { return _cargando; }
            set
            {
                _cargando = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Metodos
        public async void ObtenerDetallePelicula(int idPelicula)
        {
            Cargando = true;
            Pelicula = await _servicio.ObtenerDetallePelicula(idPelicula);
            Cargando = false;
        }
        #endregion
    }
}
