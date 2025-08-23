using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using TP3MovilFullstack.Models;

namespace TP3MovilFullstack.Services
{
    public class UserService
    {
        private readonly List<Usuario> _usuarios;

        public Usuario? CurrentUser { get; private set; }

        public UserService()
        {
            _usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nombre = "Admin",
                    Email = "admin@admin.com",
                    Password = "admin",
                    Rol = "admin",
                    ImagenPath = Path.Combine("Resources", "Images", "dotnet_bot.svg")
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Usuario Común",
                    Email = "user@user.com",
                    Password = "user",
                    Rol = "user",
                    ImagenPath = Path.Combine("Resources", "Images", "perro.jpg")
                }
            };
        }

        public List<Usuario> GetAll() => _usuarios;

        public Usuario? GetUsuario(int id) => _usuarios.FirstOrDefault(u => u.Id == id);

        public Usuario? ValidateLogin(string email, string password)
        {
            var user = _usuarios.FirstOrDefault(u =>
                u.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true &&
                u.Password == password);

            if (user != null)
            {
                CurrentUser = user;
            }

            return user;
        }

        public void Logout() => CurrentUser = null;

        public void AddUser(Usuario newUser)
        {
            newUser.Id = _usuarios.Any() ? _usuarios.Max(u => u.Id) + 1 : 1;
            _usuarios.Add(newUser);
        }

        public void UpdateUser(Usuario updatedUser)
        {
            var index = _usuarios.FindIndex(u => u.Id == updatedUser.Id);
            if (index != -1)
            {
                _usuarios[index] = updatedUser;
                if (CurrentUser?.Id == updatedUser.Id)
                {
                    CurrentUser = updatedUser;
                }
            }
        }

        public void DeleteUser(int id)
        {
            var user = _usuarios.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _usuarios.Remove(user);
                if (CurrentUser?.Id == id)
                {
                    CurrentUser = null;
                }
            }
        }
    }
}

