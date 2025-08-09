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
                new Pelicula { Id = 1, Titulo = "El Origen", Anio = 2010, Director = "Christopher Nolan", ImagenPath = "perro.jpg" },
                new Pelicula { Id = 2, Titulo = "Interstellar", Anio = 2014, Director = "Christopher Nolan", ImagenPath = "dotnet_bot.svg" },
                new Pelicula { Id = 3, Titulo = "Parasite", Anio = 2019, Director = "Bong Joon-ho", ImagenPath = "dotnet_bot.svg" }
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

        public bool UpdatePelicula(Pelicula peliculaActualizada)
        {
            var peliculaExistente = peliculas.FirstOrDefault(p => p.Id == peliculaActualizada.Id);
            if (peliculaExistente != null)
            {
                peliculaExistente.Titulo = peliculaActualizada.Titulo;
                peliculaExistente.Anio = peliculaActualizada.Anio;
                peliculaExistente.Director = peliculaActualizada.Director;
                peliculaExistente.ImagenPath = peliculaActualizada.ImagenPath;
                return true;
            }
            return false;
        }

        public bool DeletePelicula(int id)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);
            if (pelicula != null)
            {
                peliculas.Remove(pelicula);
                return true;
            }
            return false;
        }

    }
}
