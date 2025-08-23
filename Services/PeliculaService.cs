using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using TP3MovilFullstack.Models;

namespace TP3MovilFullstack.Services
{
    public class PeliculaService
    {
        private readonly List<Pelicula> _peliculas = new();

        public List<Pelicula> GetAllPeliculas() => _peliculas;

        public Pelicula? GetById(int id) => _peliculas.FirstOrDefault(p => p.Id == id);

        public void AddPelicula(Pelicula pelicula)
        {
            pelicula.Id = _peliculas.Any() ? _peliculas.Max(p => p.Id) + 1 : 1;
            _peliculas.Add(pelicula);
        }

        public void Update(Pelicula pelicula)
        {
            var peliculaExistente = _peliculas.FirstOrDefault(p => p.Id == pelicula.Id);
            if (peliculaExistente != null)
            {
                peliculaExistente.Titulo = pelicula.Titulo;
                peliculaExistente.Anio = pelicula.Anio;
                peliculaExistente.Director = pelicula.Director;

                if (!string.IsNullOrWhiteSpace(pelicula.ImagenPath))
                {
                    peliculaExistente.ImagenPath = pelicula.ImagenPath;
                    peliculaExistente.ImagenContentType = pelicula.ImagenContentType;
                }
            }
        }

        public void DeletePelicula(int id)
        {
            var peliculaToRemove = _peliculas.FirstOrDefault(p => p.Id == id);
            if (peliculaToRemove != null)
            {
                if (!string.IsNullOrWhiteSpace(peliculaToRemove.ImagenPath) && File.Exists(peliculaToRemove.ImagenPath))
                {
                    try
                    {
                        File.Delete(peliculaToRemove.ImagenPath);
                    }
                    catch (Exception)
                    {
                        // ignore delete errors
                    }
                }

                _peliculas.Remove(peliculaToRemove);
            }
        }
    }
}

