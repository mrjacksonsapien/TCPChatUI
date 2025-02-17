using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client;

public class Client
{
    private IPAddress _remoteIp;
    private int _remotePort;
    
    public event Action<string>? OnMessageReceived;
    public event Action<string>? OnMessageSent;
    public event Action? ConnectionStarted;
    
    private CancellationTokenSource? _cts;

    public Client(string remoteIp, int remotePort)
    {
        _remoteIp = IPAddress.Parse(remoteIp);
        _remotePort = remotePort;
    }
    
    private async Task<string> GetServerMessageAsync(Socket clientSocket)
    {
        byte[] buffer = new byte[1024];
        try
        {
            int bytesRead = await clientSocket.ReceiveAsync(buffer);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.ConnectionReset || ex.SocketErrorCode == SocketError.ConnectionAborted)
        {
            return String.Empty;
        }
    }

    private async void HandleIncomingMessagesAsync(Socket clientSocket)
    {
        try
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                Task<string> getServerMessageTask = GetServerMessageAsync(clientSocket);
                Task cancelTask = Task.Delay(Timeout.Infinite, _cts.Token);
                
                Task completedTask = await Task.WhenAny(getServerMessageTask, cancelTask);
                
                if (completedTask == cancelTask)
                {
                    break;
                }
                
                string message = getServerMessageTask.Result;
                
                if (string.IsNullOrEmpty(message))
                {
                    _cts.Cancel();
                    OnMessageReceived?.Invoke("Le serveur a mis fin à la connexion.");
                    break;
                }

                OnMessageReceived?.Invoke(message);
            }
        }
        catch (SocketException exception) when (exception.SocketErrorCode == SocketError.ConnectionReset || exception.SocketErrorCode == SocketError.ConnectionAborted)
        {
            _cts.Cancel();
            OnMessageReceived?.Invoke("Le serveur a mis fin à la connexion.");
        }
    }

    public void SendMessage(string message)
    {
        if (message.Trim() != "")
        {
            OnMessageSent?.Invoke(message);
        }
    }
    
    public async Task Start(string username)
    {
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        async Task sendToServerAsync(string message)
        {
            byte[] messageData = Encoding.UTF8.GetBytes(message);
            await clientSocket.SendAsync(messageData);
        }

        try
        {
            await clientSocket.ConnectAsync(new IPEndPoint(_remoteIp, _remotePort));
        }
        catch (SocketException exception) when (exception.SocketErrorCode == SocketError.ConnectionRefused)
        {
            OnMessageReceived?.Invoke("Erreur de connexion au serveur.");
            return;
        }

        OnMessageReceived?.Invoke("Connexion réussi.");
        
        _cts = new CancellationTokenSource();

        try
        {
            await GetServerMessageAsync(clientSocket);
            await sendToServerAsync(username);
            string messageDeBienvenue = await GetServerMessageAsync(clientSocket);
            OnMessageReceived?.Invoke(messageDeBienvenue);

            ConnectionStarted?.Invoke();
            
            _ = Task.Run(() => HandleIncomingMessagesAsync(clientSocket));

            OnMessageSent += async message =>
            {
                await sendToServerAsync(message);
                
                if (message == "exit")
                {
                    _cts.Cancel();
                }
            };

            await Task.Delay(Timeout.Infinite, _cts.Token);
        }
        catch (SocketException exception) when (exception.SocketErrorCode == SocketError.ConnectionReset) {}
        catch (TaskCanceledException) {}
        
        clientSocket.Close();
        OnMessageReceived?.Invoke("Déconnecté.");
    }
}