using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3MovilFullstack.Models
{
    public class Pelicula
    {
        // Unique identifier for the movie.
        public int Id { get; set; }
        // Title of the movie.
        public string Titulo { get; set; }
        // Year the movie was released.
        public int Anio { get; set; }
        // Director of the movie.
        public string Director { get; set; }
        // Path to the local image file for the movie poster.
        public string ImagenPath { get; set; }
    }
}
