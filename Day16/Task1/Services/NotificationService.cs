using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1.Services
{
    public class NotificationService : IDisposable
    {
        private const string MmfName = "TaskManager_Notifications";
        private const int BufferSize = 4096;

        private MemoryMappedFile? _mmf;
        private CancellationTokenSource? _cts;

        public event Action<string>? NotificationReceived;

        public NotificationService()
        {
            try
            {
                // Создаём или открываем Memory-Mapped File
                _mmf = MemoryMappedFile.CreateOrOpen(MmfName, BufferSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MMF error: {ex.Message}");
            }
        }

        // -------------------------------------------------------
        // Отправить уведомление всем пользователям
        // -------------------------------------------------------
        public void SendNotification(string username, string taskTitle)
        {
            if (_mmf == null) return;

            try
            {
                string message = $"[{DateTime.Now:HH:mm}] " +
                                 $"{username} добавил задачу: «{taskTitle}»";

                byte[] data = Encoding.UTF8.GetBytes(message.PadRight(BufferSize / 2, '\0'));

                using var accessor = _mmf.CreateViewAccessor(0, BufferSize);
                accessor.WriteArray(0, data, 0,
                    Math.Min(data.Length, BufferSize));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendNotification error: {ex.Message}");
            }
        }

        // -------------------------------------------------------
        // Начать прослушивание уведомлений (фоновый поток)
        // -------------------------------------------------------
        public void StartListening()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(async () =>
            {
                string lastMessage = string.Empty;

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        if (_mmf != null)
                        {
                            using var accessor =
                                _mmf.CreateViewAccessor(0, BufferSize);

                            byte[] buffer = new byte[BufferSize];
                            accessor.ReadArray(0, buffer, 0, buffer.Length);

                            string message = Encoding.UTF8
                                .GetString(buffer)
                                .TrimEnd('\0')
                                .Trim();

                            // Уведомляем только если сообщение изменилось
                            if (!string.IsNullOrEmpty(message) &&
                                message != lastMessage)
                            {
                                lastMessage = message;
                                NotificationReceived?.Invoke(message);
                            }
                        }
                    }
                    catch { /* игнорируем */ }

                    await Task.Delay(1000, token);
                }
            }, token);
        }

        public void StopListening() => _cts?.Cancel();

        public void Dispose()
        {
            _cts?.Cancel();
            _mmf?.Dispose();
        }
    }
}