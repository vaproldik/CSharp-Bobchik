using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Task1.Data;
using Task1.Models;

namespace Task1.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public UserModel? CurrentUser { get; private set; }

        public AuthService()
        {
            _context = new AppDbContext();

            // Создаём БД если не существует
            _context.Database.EnsureCreated();
        }

        // Регистрация
        public async Task<(bool Success, string Message)> RegisterAsync(
            string username, string password, string displayName)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Введите имя пользователя.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
                return (false, "Пароль должен быть не менее 4 символов.");

            bool exists = await _context.Users.AnyAsync(u =>
                u.Username.ToLower() == username.ToLower());

            if (exists)
                return (false, "Пользователь уже существует.");

            var user = new UserModel
            {
                Username = username.Trim(),
                PasswordHash = Hash(password),
                DisplayName = string.IsNullOrWhiteSpace(displayName)
                               ? username : displayName.Trim()
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return (true, "Регистрация успешна!");
        }

        // Вход
        public async Task<(bool Success, string Message)> LoginAsync(
            string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                return (false, "Введите логин и пароль.");

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username.ToLower() == username.ToLower());

            if (user == null)
                return (false, "Пользователь не найден.");

            if (user.PasswordHash != Hash(password))
                return (false, "Неверный пароль.");

            CurrentUser = user;
            return (true, $"Добро пожаловать, {user.DisplayName}!");
        }

        public void Logout() => CurrentUser = null;

        private static string Hash(string input)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }
}