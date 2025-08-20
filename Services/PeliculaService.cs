using System.Text.Json;
using TP3MovilFullstack.Models;
using TP3MovilFullstack.Utils;

namespace TP3MovilFullstack.Services
{
    public class PeliculaService
    {
        private readonly List<Pelicula> _peliculas = new();
        private const string PeliculasFileName = "peliculas.json";

        public PeliculaService()
        {
            LoadPeliculasFromJsonFile();
        }

        private string GetFilePath()
        {
            return Path.Combine(FileSystem.AppDataDirectory, PeliculasFileName);
        }

        public List<Pelicula> GetAllPeliculas()
        {
            return _peliculas;
        }

        public Pelicula? GetById(int id)
        {
            return _peliculas.FirstOrDefault(p => p.Id == id);
        }

        public void AddPelicula(Pelicula pelicula)
        {
            pelicula.Id = _peliculas.Any() ? _peliculas.Max(p => p.Id) + 1 : 1;
            pelicula.ImagenDataUrl = ImageHelper.ToDataUrl(pelicula.ImagenPath);
            _peliculas.Add(pelicula);
            SavePeliculasToJsonFile();
        }

        public void Update(Pelicula pelicula)
        {
            var peliculaExistente = _peliculas.FirstOrDefault(p => p.Id == pelicula.Id);
            if (peliculaExistente != null)
            {
                peliculaExistente.Titulo = pelicula.Titulo;
                peliculaExistente.Anio = pelicula.Anio;
                peliculaExistente.Director = pelicula.Director;

                // Actualizar la imagen solo si se proporcionó una nueva
                if (!string.IsNullOrWhiteSpace(pelicula.ImagenPath))
                {
                    peliculaExistente.ImagenPath = pelicula.ImagenPath;
                    peliculaExistente.ImagenContentType = pelicula.ImagenContentType;
                    peliculaExistente.ImagenDataUrl = ImageHelper.ToDataUrl(pelicula.ImagenPath);
                }
            }
            SavePeliculasToJsonFile();
        }

        public void DeletePelicula(int id)
        {
            var peliculaToRemove = _peliculas.FirstOrDefault(p => p.Id == id);
            if (peliculaToRemove != null)
            {
                // Opcional: Eliminar el archivo de imagen del disco
                if (!string.IsNullOrWhiteSpace(peliculaToRemove.ImagenPath) && File.Exists(peliculaToRemove.ImagenPath))
                {
                    try
                    {
                        File.Delete(peliculaToRemove.ImagenPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error eliminando archivo de imagen: {ex.Message}");
                    }
                }
                _peliculas.Remove(peliculaToRemove);
                SavePeliculasToJsonFile();
            }
        }

        private void SavePeliculasToJsonFile()
        {
            string fullPath = GetFilePath();
            string jsonString = JsonSerializer.Serialize(_peliculas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fullPath, jsonString);
        }

        private void LoadPeliculasFromJsonFile()
        {
            string fullPath = GetFilePath();
            if (File.Exists(fullPath))
            {
                string jsonString = File.ReadAllText(fullPath);
                var peliculasFromFile = JsonSerializer.Deserialize<List<Pelicula>>(jsonString);
                if (peliculasFromFile != null)
                {
                    _peliculas.Clear();
                    _peliculas.AddRange(peliculasFromFile);
                    foreach (var p in _peliculas)
                    {
                        p.ImagenDataUrl = ImageHelper.ToDataUrl(p.ImagenPath);
                    }
                }
            }
        }
    }
}
