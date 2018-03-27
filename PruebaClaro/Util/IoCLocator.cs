using CommonServiceLocator;
using PruebaClaro.ViewModels;
using PruebaDataAccess.Interfaces;
using PruebaDataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.ServiceLocation;

namespace PruebaClaro.Util
{
    public class Locator
    {
        public VMMainPage MainPage => ServiceLocator.Current.GetInstance<VMMainPage>();
        public VMDetallePelicula DetallePelicula => ServiceLocator.Current.GetInstance<VMDetallePelicula>();

        public static Locator Default => new Locator();

        static Locator()
        {
            var container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            container.RegisterType<VMMainPage>();
            container.RegisterType<VMDetallePelicula>();
            container.RegisterType<IServicioPeliculas, ServicioPeliculas>();
        }
    }
}
