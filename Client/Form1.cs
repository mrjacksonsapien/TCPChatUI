namespace Client;

public partial class Form1 : Form
{
    private Client _client;
    private bool _connected;
    
    public Form1()
    {
        InitializeComponent();
        
        _connected = false;
    }
    
    public void AddMessageInChatBox(string message)
    {
        ChatBox.AppendText(message + Environment.NewLine);
        ChatBox.SelectionStart = ChatBox.Text.Length;
        ChatBox.ScrollToCaret();
    }

    private async void ConnectButton_Click(object sender, EventArgs e)
    {
        ConnectButton.Enabled = false;
        ServerAddress.Enabled = false;
        ServerPort.Enabled = false;
        Nom.Enabled = false;

        try
        {
            _client = new Client(ServerAddress.Text, int.Parse(ServerPort.Text));

            _client.OnMessageReceived += messsage =>
            {
                if (InvokeRequired)
                {
                    Invoke(() => AddMessageInChatBox(messsage));
                }
                else
                {
                    AddMessageInChatBox(messsage);
                }
            };

            _client.ConnectionStarted += () =>
            {
                ClientMessage.Enabled = true;
                SendButton.Enabled = true;
                _connected = true;
            };

            AddMessageInChatBox("Connexion...");
            await _client.Start(Nom.Text);
        }
        catch (Exception exception)
        {
            AddMessageInChatBox(exception.Message);
        }
        
        ConnectButton.Enabled = true;
        ServerAddress.Enabled = true;
        ServerPort.Enabled = true;
        Nom.Enabled = true;
        ClientMessage.Enabled = false;
        ClientMessage.Text = "";
        SendButton.Enabled = false;

        _connected = false;
        _client = null;
    }

    private void Send()
    {
        string message = ClientMessage.Text;
        _client.SendMessage(message);
        ClientMessage.Text = "";
    }

    private void SendButton_Click(object sender, EventArgs e)
    {
        Send();
    }

    private void Form1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter && _connected)
        {
            Send();
        }
    }
}