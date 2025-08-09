using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3MovilFullstack.Models
{
    public class Usuarios
    {
        // Unique identifier for the user.
        public int Id { get; set; }
        // User's name.
        public string? Nombre { get; set; }
        // User's email, used for login.
        public string? Email { get; set; }
        // User's password (for demonstration, in a real app this should be hashed).
        public string? Password { get; set; }
        // Path to the local image file for the user.
        public string? ImagenPath { get; set; }
    }
}
