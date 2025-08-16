using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3MovilFullstack.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<string>? Rol { get; set; } = new();// Rol del usuario
        public string? ImagenPath { get; set; }

        // Propiedad para la UI, no se guarda
        public string? ImagenDataUrl { get; set; }
    }
}

