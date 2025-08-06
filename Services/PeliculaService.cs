using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3MovilFullstack.Models;

namespace TP3MovilFullstack.Services
{
    public class PeliculaService
    {
        private readonly List<Pelicula> peliculas;
        public PeliculaService()
        {
            peliculas = new List<Pelicula>
            {
                new Pelicula { Id = 1, Titulo = "El Origen", Anio = 2010, Director = "Christopher Nolan", ImagenPath = "images/poster_origen.jpg" },
                new Pelicula { Id = 2, Titulo = "Interstellar", Anio = 2014, Director = "Christopher Nolan", ImagenPath = "images/poster_interstellar.jpg" },
                new Pelicula { Id = 3, Titulo = "Parasite", Anio = 2019, Director = "Bong Joon-ho", ImagenPath = "images/poster_parasite.jpg" }
            };
        }

        public List<Pelicula> GetAll() => peliculas;


        public Pelicula? GetPelicula(int id)
        {
            return peliculas.FirstOrDefault(p => p.Id == id);
        
        }

        public void AddPelicula(Pelicula pelicula)
        {
            //pelicula.Id = peliculas.Max(p => p.Id) + 1;
            pelicula.Id = peliculas.Any() ? peliculas.Max(p => p.Id) + 1 : 1;
            peliculas.Add(pelicula);
        }

    }
}
