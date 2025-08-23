namespace TP3MovilFullstack.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int Anio { get; set; }
        public string? Director { get; set; }

        // Ruta del archivo guardado en el dispositivo
        public string? ImagenPath { get; set; }

        // Tipo de contenido de la imagen (ej: "image/jpeg")
        public string? ImagenContentType { get; set; }
    }
}
