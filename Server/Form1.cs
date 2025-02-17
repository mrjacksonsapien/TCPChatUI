namespace Server;

public partial class Form1 : Form
{
    private bool _running;
    private Server _server;
    
    public Form1()
    {
        _running = false;
        InitializeComponent();
    }

    public void AddMessageInChatBox(string message)
    {
        chatBox.AppendText(message + Environment.NewLine);
        chatBox.SelectionStart = chatBox.Text.Length;
        chatBox.ScrollToCaret();
    }

    private void ToggleServer_Click(object sender, EventArgs e)
    {
        _running = !_running;
        ToggleServer.Text = "...";
        PortNumber.Enabled = !_running;

        if (_running)
        {
            try
            {
                _server = new Server(int.Parse(PortNumber.Text));

                _server.OnMessageReceived += messsage =>
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

                _server.Start();

                ToggleServer.Text = "Stop";
            }
            catch (Exception exception)
            {
                _running = false;
                ToggleServer.Text = "Start";
                PortNumber.Enabled = true;
                AddMessageInChatBox(exception.Message);
                _server = null;
            }
        }
        else
        {
            _server.Cts?.Cancel();
            ToggleServer.Text = "Start";
            _server = null;
        }
    }
}