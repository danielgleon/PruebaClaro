using PruebaClaro.Util;
using PruebaDataAccess.Interfaces;
using PruebaDataAccess.Models;
using PruebaDataAccess.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PruebaClaro.ViewModels
{
    public class VMMainPage : VMBase
    {
        public VMMainPage(IServicioPeliculas servicio)
        {
            _servicio = servicio;
            ObtenerPeliculas();
        }

        #region Propiedades
        private IServicioPeliculas _servicio;

        private ObservableCollection<Pelicula> _coleccionPeliculas;
        public ObservableCollection<Pelicula> ColeccionPeliculas
        {
            get { return _coleccionPeliculas; }
            set
            {
                _coleccionPeliculas = value;
                NotifyPropertyChanged();
            }
        }


        private List<Pelicula> _listaPeliculas;
        public List<Pelicula> ListaPeliculas
        {
            get { return _listaPeliculas; }
            set
            {
                _listaPeliculas = value;

                ColeccionPeliculas = new ObservableCollection<Pelicula>(_listaPeliculas);
                NotifyPropertyChanged();
            }
        }

        private string _textoBusqueda = "algo";
        public string TextoBusqueda
        {
            get { return _textoBusqueda; }
            set
            {
                _textoBusqueda = value;

                if (_listaPeliculas != null)
                    if (!string.IsNullOrWhiteSpace(_textoBusqueda) && _textoBusqueda.Length >= 3)
                        ColeccionPeliculas = new ObservableCollection<Pelicula>(_listaPeliculas.Where(x => x.Titulo.ToUpperInvariant().Contains(_textoBusqueda.ToUpperInvariant())));
                    else
                        ColeccionPeliculas = new ObservableCollection<Pelicula>(_listaPeliculas);

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
        public async void ObtenerPeliculas()
        {
            TextoBusqueda = string.Empty;
            Cargando = true;
            ListaPeliculas = await _servicio.ObtenerPeliculas();
            Cargando = false;
        }
        #endregion  
    }
}
