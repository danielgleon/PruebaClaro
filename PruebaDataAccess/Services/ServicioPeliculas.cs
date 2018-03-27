using Newtonsoft.Json.Linq;
using PruebaDataAccess.Interfaces;
using PruebaDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDataAccess.Services
{
    public class ServicioPeliculas : IServicioPeliculas
    {
        public async Task<List<Pelicula>> ObtenerPeliculas()
        {
            var uri = new Uri("https://mfwkweb-api.clarovideo.net//services/content/list?api_version=v5.8&authpn=webclient&authpt=tfg1h3j4k6fd7&format=json&region=mexico&device_id=web&device_category=web&device_model=web&device_type=web&device_manufacturer=generic&HKS=ua9pg9l2bh8qhmb7d23fb4pcq0&quantity=40&order_way=DESC&order_id=200&level_id=GPS&from=0&node_id=9869");
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(uri);
            var response = await responseGet.Content.ReadAsStringAsync();
            JObject ob = JObject.Parse(response);

            var resp = ob["response"]["groups"].Select(tk => ConversorPelicula(tk)).ToList();

            return resp;
        }

        public async Task<Pelicula> ObtenerDetallePelicula(int Id)
        {
            var uri = new Uri("https://mfwkweb-api.clarovideo.net/services/content/data?api_version=v5.8&authpn=webclient&authpt=tfg1h3j4k6fd7&format=json&region=mexico&device_id=web&device_category=web&device_model=web&device_type=web&device_manufacturer=generic&HKS=ua9pg9l2bh8qhmb7d23fb4pcq0&group_id=" + Id);
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(uri);
            var response = await responseGet.Content.ReadAsStringAsync();
            JObject ob = JObject.Parse(response);

            var resp = ConversorPelicula(ob["response"]["group"]["common"]);

            return resp;
        }

        private Pelicula ConversorPelicula(JToken token)
        {
            var response = new Pelicula
            {
                Id = (int)token["id"],
                Titulo = (string)token["title"],
                Descripcion = (string)token["description"],
                ImageLarge = (string)token["image_large"],
                ImageMedium = (string)token["image_medium"],
                ImageSmall = (string)token["image_small"],
                ImageBackground = (string)token["image_background"],
                ImageHorizontal = (string)token["image_base_horizontal"],
                ImageVertical = (string)token["image_base_vertical"],
            };

            if (token["extendedcommon"]?.Any() == true)
            {
                response.TituloOriginal = (string)token["extendedcommon"]["media"]["originaltitle"];
                response.Anio = (int)token["extendedcommon"]["media"]["publishyear"];
                response.Rating = ConversorRating(token["extendedcommon"]["media"]["rating"]);
                response.Duracion = TimeSpan.Parse((string)token["extendedcommon"]["media"]["duration"]);

                response.Generos = token["extendedcommon"]["genres"]["genre"].Select(tk => ConversorDato(tk)).ToList();

                response.Roles = token["extendedcommon"]["roles"]["role"].Select(tk => ConversorRol(tk)).ToList();
            }

            return response;
        }

        private DatoBase ConversorDato(JToken token)
        {
            return new DatoBase
            {
                Id = (int)token["id"],
                Nombre = (string)token["name"],
                Descripcion = (string)token["desc"],
            };
        }

        private Rating ConversorRating(JToken token)
        {
            return new Rating
            {
                Id = (int)token["id"],
                Codigo = (string)token["code"],
                Descripcion = (string)token["desc"],
            };
        }

        private Rol ConversorRol(JToken token)
        {
            return new Rol
            {
                Id = (int)token["id"],
                Nombre = (string)token["name"],
                Descripcion = (string)token["desc"],
                Talentos = token["talents"]["talent"].Select(tk => ConversorTalento(tk)).ToList()
            };
        }

        private Talento ConversorTalento(JToken token)
        {
            return new Talento
            {
                Id = (int)token["id"],
                Nombre = (string)token["name"],
                Apellido = (string)token["surname"],
                NombreCompleto = (string)token["fullname"],
            };
        }
    }
}
