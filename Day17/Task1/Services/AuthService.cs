using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Task1.Models;

namespace Task1.Services
{
    public class AuthService
    {
        private readonly string _usersFilePath;
        private List<UserModel> _users = new();

        public UserModel? CurrentUser { get; private set; }

        public AuthService()
        {
            // Файл хранится рядом с exe
            string dataDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Data");

            Directory.CreateDirectory(dataDir);
            _usersFilePath = Path.Combine(dataDir, "users.json");

            LoadUsers();
        }

        // -------------------------------------------------------
        // Регистрация
        // -------------------------------------------------------
        public (bool Success, string Message) Register(
            string username, string password, string displayName)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Введите имя пользователя.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
                return (false, "Пароль должен быть не менее 4 символов.");

            if (_users.Any(u => u.Username.Equals(
                    username, StringComparison.OrdinalIgnoreCase)))
                return (false, "Пользователь с таким именем уже существует.");

            var user = new UserModel
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Username = username.Trim(),
                PasswordHash = HashPassword(password),
                DisplayName = string.IsNullOrWhiteSpace(displayName)
                               ? username : displayName.Trim()
            };

            _users.Add(user);
            SaveUsers();

            return (true, "Регистрация успешна!");
        }

        // -------------------------------------------------------
        // Вход
        // -------------------------------------------------------
        public (bool Success, string Message) Login(
            string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                return (false, "Введите имя пользователя и пароль.");

            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return (false, "Пользователь не найден.");

            if (user.PasswordHash != HashPassword(password))
                return (false, "Неверный пароль.");

            CurrentUser = user;
            return (true, $"Добро пожаловать, {user.DisplayName}!");
        }

        public void Logout() => CurrentUser = null;

        // -------------------------------------------------------
        // Вспомогательные методы
        // -------------------------------------------------------
        private void LoadUsers()
        {
            if (!File.Exists(_usersFilePath))
            {
                _users = new List<UserModel>();
                return;
            }

            try
            {
                string json = File.ReadAllText(_usersFilePath);
                _users = JsonSerializer.Deserialize<List<UserModel>>(json)
                         ?? new List<UserModel>();
            }
            catch
            {
                _users = new List<UserModel>();
            }
        }

        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersFilePath, json);
        }

        private static string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        public List<UserModel> GetAllUsers() => new List<UserModel>(_users);
    }
}