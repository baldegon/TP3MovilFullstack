using System.Text.Json;
using TP3MovilFullstack.Models;

namespace TP3MovilFullstack.Services
{
    public class UserService
    {
        private readonly List<Usuario> _usuarios;
        private const string UsersFileName = "usuarios.json";

        public Usuario? CurrentUser { get; private set; }

        public UserService()
        {
            _usuarios = new List<Usuario>();
            LoadUsersFromJsonFile();

            // Si la lista está vacía, crea un administrador por defecto
            if (!_usuarios.Any())
            {
                // Imágenes predeterminadas para los usuarios iniciales
                var adminImage = Path.Combine("Resources", "Images", "dotnet_bot.svg");
                var userImage = Path.Combine("Resources", "Images", "perro.jpg");

                _usuarios.Add(new Usuario
                {
                    Id = 1,
                    Nombre = "Admin",
                    Email = "admin@admin.com",
                    Password = "admin",
                    Rol = [],
                    ImagenPath = adminImage
                });
                _usuarios.Add(new Usuario
                {
                    Id = 2,
                    Nombre = "Usuario Común",
                    Email = "user@user.com",
                    Password = "user",
                    Rol = [],
                    ImagenPath = userImage
                });
                SaveChanges();
            }
        }

        private string GetFilePath() => Path.Combine(FileSystem.AppDataDirectory, UsersFileName);

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

        public void AddUser(Usuario newUser)
        {
            newUser.Id = _usuarios.Any() ? _usuarios.Max(u => u.Id) + 1 : 1;
            _usuarios.Add(newUser);
            SaveChanges();
        }

        public void UpdateUser(Usuario updatedUser)
        {
            var index = _usuarios.FindIndex(u => u.Id == updatedUser.Id);
            if (index != -1)
            {
                _usuarios[index] = updatedUser;
                SaveChanges();
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
                SaveChanges();
                if (CurrentUser?.Id == id)
                {
                    CurrentUser = null;
                }
            }
        }

        private void SaveChanges()
        {
            var filePath = GetFilePath();
            var json = JsonSerializer.Serialize(_usuarios, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private void LoadUsersFromJsonFile()
        {
            var filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var users = JsonSerializer.Deserialize<List<Usuario>>(json);
                if (users != null)
                {
                    _usuarios.Clear();
                    _usuarios.AddRange(users);
                }
            }
        }
    }
}
