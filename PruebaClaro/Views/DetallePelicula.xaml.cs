using PruebaClaro.Util;
using PruebaClaro.ViewModels;
using PruebaDataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace PruebaClaro.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class DetallePelicula : Page
    {
        public DetallePelicula()
        {
            this.InitializeComponent();
            VM = Locator.Default.DetallePelicula;
        }

        public VMDetallePelicula VM { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var seleccion = e.Parameter as Pelicula;
            VM.ObtenerDetallePelicula(seleccion.Id);

            var currentView = SystemNavigationManager.GetForCurrentView();
            if (this.Frame.CanGoBack)
            {
                currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

                currentView.BackRequested += BackButton_Tapped;
            }
            else
            {
                currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.BackRequested -= BackButton_Tapped;

            base.OnNavigatedFrom(e);
        }

        private void BackButton_Tapped(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
