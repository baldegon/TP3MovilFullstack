using System.Text.Json.Serialization;

namespace TP3MovilFullstack.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int Anio { get; set; }
        public string? Director { get; set; }

        // Ruta del archivo guardado en el dispositivo (no se persiste en JSON)
        [JsonIgnore]
        public string? ImagenPath { get; set; }
    }
}
