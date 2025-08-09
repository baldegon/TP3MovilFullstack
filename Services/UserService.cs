using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3MovilFullstack.Models;

namespace TP3MovilFullstack.Services
{
    public class UserService
    {
        private readonly List<Usuarios> usuarios;

        public UserService()
        {
            usuarios = new List<Usuarios>
            {
                new Usuarios { Id = 1, Nombre = "Juan Pérez", Email = "juan@ejemplo.com", Password = "123456", ImagenPath = "perro.jpg" },
                new Usuarios { Id = 2, Nombre = "María García", Email = "maria@ejemplo.com", Password = "123456", ImagenPath = "dotnet_bot.svg" },
                new Usuarios { Id = 3, Nombre = "Carlos López", Email = "carlos@ejemplo.com", Password = "123456", ImagenPath = "perro.jpg" },
                new Usuarios { Id = 4, Nombre = "Ana Martínez", Email = "ana@ejemplo.com", Password = "123456", ImagenPath = null }
            };
        }

        // Obtener todos los usuarios
        public List<Usuarios> GetAll() => usuarios;

        // Obtener usuario por ID
        public Usuarios? GetUsuario(int id)
        {
            return usuarios.FirstOrDefault(u => u.Id == id);
        }

        // Obtener usuario por email (para login)
        public Usuarios? GetUsuarioByEmail(string email)
        {
            return usuarios.FirstOrDefault(u => u.Email?.ToLower() == email?.ToLower());
        }

        // Agregar nuevo usuario
        public void AddUsuario(Usuarios usuario)
        {
            usuario.Id = usuarios.Any() ? usuarios.Max(u => u.Id) + 1 : 1;
            usuarios.Add(usuario);
        }

        // Actualizar usuario existente
        public bool UpdateUsuario(Usuarios usuarioActualizado)
        {
            var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuarioActualizado.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuarioActualizado.Nombre;
                usuarioExistente.Email = usuarioActualizado.Email;
                usuarioExistente.Password = usuarioActualizado.Password;
                usuarioExistente.ImagenPath = usuarioActualizado.ImagenPath;
                return true;
            }
            return false;
        }

        // Eliminar usuario
        public bool DeleteUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
                return true;
            }
            return false;
        }

        // Validar credenciales para login
        public Usuarios? ValidateLogin(string email, string password)
        {
            return usuarios.FirstOrDefault(u => 
                u.Email?.ToLower() == email?.ToLower() && 
                u.Password == password);
        }

        // Verificar si un email ya existe (para validación)
        public bool EmailExists(string email, int excludeId = 0)
        {
            return usuarios.Any(u => 
                u.Email?.ToLower() == email?.ToLower() && 
                u.Id != excludeId);
        }
    }
}
