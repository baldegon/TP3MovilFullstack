using System.Text.Json;
using TP3MovilFullstack.Models;

namespace TP3MovilFullstack.Services
{
    public class UserService
    {
        private readonly List<Usuario> _usuarios;
        private const string UsersFileName = "usuarios.json";

        public UserService()
        {
            _usuarios = new List<Usuario>();
            LoadUsersFromJsonFile();

            // Si la lista está vacía, crea un administrador por defecto
            if (!_usuarios.Any())
            {
                _usuarios.Add(new Usuario { Id = 1, Nombre = "Admin", Email = "admin@admin.com", Password = "admin", Rol = Rol.Administrador });
                _usuarios.Add(new Usuario { Id = 2, Nombre = "Usuario Común", Email = "user@user.com", Password = "user", Rol = Rol.Usuario });
                SaveChanges();
            }
        }

        private string GetFilePath() => Path.Combine(FileSystem.AppDataDirectory, UsersFileName);

        public List<Usuario> GetAll() => _usuarios;

        public Usuario? GetUsuario(int id) => _usuarios.FirstOrDefault(u => u.Id == id);

        public Usuario? ValidateLogin(string email, string password)
        {
            return _usuarios.FirstOrDefault(u =>
                u.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true &&
                u.Password == password);
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
