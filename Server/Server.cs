using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public class Server
{
    private IPAddress localIp;
    private int localPort;
    private List<Socket> clients = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    
    public event Action<string>? OnMessageReceived;
    public CancellationTokenSource? Cts { get; private set; }

    private IPAddress getLocalIpAddress()
    {
        foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip;
            }
        }
        
        throw new InvalidOperationException("No IPv4 address found for this machine.");
    }
    
    public Server(int localPort)
    {
        localIp = getLocalIpAddress();
        this.localPort = localPort;
    }

    private async Task sendToClientAsync(string message, Socket clientSocket)
    {
        byte[] messageData = Encoding.UTF8.GetBytes(message);
        await clientSocket.SendAsync(messageData);
    }

    private async void HandleClientAsync(Socket clientSocket)
    {
        async Task<string> GetClientMessageAsync()
        {
            byte[] buffer = new byte[1024];
            try
            {
                int bytesRead = await clientSocket.ReceiveAsync(buffer);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.ConnectionReset || ex.SocketErrorCode == SocketError.ConnectionAborted)
            {
                return "exit";
            }
        }
            
        string clientName = clientSocket.RemoteEndPoint.ToString();
        OnMessageReceived?.Invoke(clientName + " s'est connecté. Demande de nom en cours...");

        try
        {
            await sendToClientAsync("Entrez votre nom: ", clientSocket);
            clientName = await GetClientMessageAsync() + " (" + clientSocket.RemoteEndPoint + ")";

            await _semaphore.WaitAsync();
            clients.Add(clientSocket);
            _semaphore.Release();
            
            string joinedSessionMessage = clientName + " a rejoin le chat.";
            OnMessageReceived?.Invoke(joinedSessionMessage);
            
            await _semaphore.WaitAsync();
            foreach (var socket in clients)
            {
                if (socket != clientSocket)
                {
                    await sendToClientAsync(joinedSessionMessage, socket);
                }
            }
            _semaphore.Release();
            
            string messageDeBienvenue = "Bienvenue dans le chat TCP! Tapez 'exit' pour quitter.";
            await sendToClientAsync(messageDeBienvenue, clientSocket);

            while (!Cts.Token.IsCancellationRequested)
            {
                Task<string> getClientMessageTask = GetClientMessageAsync();
                Task cancelTask = Task.Delay(Timeout.Infinite, Cts.Token);

                Task completedTask = await Task.WhenAny(getClientMessageTask, cancelTask);

                if (completedTask == cancelTask)
                {
                    break;
                }

                if (getClientMessageTask.Result.Trim() == "exit")
                {
                    if (clients.Contains(clientSocket))
                    {
                        await _semaphore.WaitAsync();
                        clients.Remove(clientSocket);
                        _semaphore.Release();
                    }
        
                    string quitSessionMessage = clientName + " a quitté le chat.";
                    OnMessageReceived?.Invoke(quitSessionMessage);
                    
                    await _semaphore.WaitAsync();
                    foreach (var socket in clients)
                    {
                        await sendToClientAsync(quitSessionMessage , socket);
                    }
                    _semaphore.Release();
                    break;
                }
                
                string clientMessageLog = clientName + ": " + getClientMessageTask.Result;
                OnMessageReceived?.Invoke(clientMessageLog);

                await _semaphore.WaitAsync();
                foreach (var socket in clients)
                {
                    if (socket != clientSocket)
                    {
                        await sendToClientAsync(clientMessageLog, socket);
                    }
                    else
                    {
                        await sendToClientAsync("You: " + getClientMessageTask.Result, socket);
                    }
                }
                _semaphore.Release();
            }
        }
        catch (SocketException exception) when (exception.SocketErrorCode == SocketError.ConnectionReset) {}

        string clientIp = clientSocket.RemoteEndPoint.ToString();
        clientSocket.Close();
        OnMessageReceived?.Invoke(clientIp + " s'est déconnecté.");
    }

    public async void Start()
    {
        Cts = new CancellationTokenSource();
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        serverSocket.Bind(new IPEndPoint(localIp, localPort));
        
        serverSocket.Listen(10);
        OnMessageReceived?.Invoke("Serveur démaré sur le port " + localPort);

        while (!Cts.Token.IsCancellationRequested)
        {
            Task<Socket> acceptTask = serverSocket.AcceptAsync();
            Task cancelTask = Task.Delay(Timeout.Infinite, Cts.Token);

            Task completedTask = await Task.WhenAny(acceptTask, cancelTask);

            if (completedTask == cancelTask)
            {
                break;
            }

            Socket clientSocket = acceptTask.Result;
            _ = Task.Run(() => HandleClientAsync(clientSocket));
        }
        
        serverSocket.Close();
        OnMessageReceived?.Invoke("Serveur arrêté.");
    }
}