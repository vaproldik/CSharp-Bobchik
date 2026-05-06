using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1.Services
{
    public class ChatService : IDisposable
    {
        private const string PipeName = "TaskManager_Chat";

        private CancellationTokenSource? _cts;
        private NamedPipeServerStream? _server;

        public event Action<string>? MessageReceived;

        // -------------------------------------------------------
        // Запустить сервер чата (слушаем входящие сообщения)
        // -------------------------------------------------------
        public void StartServer()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        _server = new NamedPipeServerStream(
                            PipeName,
                            PipeDirection.In,
                            NamedPipeServerStream.MaxAllowedServerInstances,
                            PipeTransmissionMode.Message,
                            PipeOptions.Asynchronous);

                        await _server.WaitForConnectionAsync(token);

                        using var reader = new StreamReader(
                            _server, Encoding.UTF8, leaveOpen: false);

                        string? message = await reader.ReadToEndAsync();

                        if (!string.IsNullOrWhiteSpace(message))
                            MessageReceived?.Invoke(message);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Chat server error: {ex.Message}");
                        await Task.Delay(500, token);
                    }
                    finally
                    {
                        _server?.Dispose();
                        _server = null;
                    }
                }
            }, token);
        }

        // -------------------------------------------------------
        // Отправить сообщение в чат (клиентская часть)
        // -------------------------------------------------------
        public async Task SendMessageAsync(string username, string message)
        {
            try
            {
                await using var client = new NamedPipeClientStream(
                    ".", PipeName,
                    PipeDirection.Out,
                    PipeOptions.Asynchronous);

                await client.ConnectAsync(timeout: 2000);

                string formatted = $"[{DateTime.Now:HH:mm}] {username}: {message}";
                byte[] data = Encoding.UTF8.GetBytes(formatted);

                await client.WriteAsync(data);
                await client.FlushAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendMessage error: {ex.Message}");
            }
        }

        public void StopServer() => _cts?.Cancel();

        public void Dispose()
        {
            _cts?.Cancel();
            _server?.Dispose();
        }
    }
}