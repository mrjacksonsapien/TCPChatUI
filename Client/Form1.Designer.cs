namespace Client;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ServerAddressLabel = new System.Windows.Forms.Label();
        ServerPortLabel = new System.Windows.Forms.Label();
        ServerAddress = new System.Windows.Forms.TextBox();
        ServerPort = new System.Windows.Forms.TextBox();
        ChatBox = new System.Windows.Forms.RichTextBox();
        ClientMessage = new System.Windows.Forms.TextBox();
        ConnectButton = new System.Windows.Forms.Button();
        SendButton = new System.Windows.Forms.Button();
        NomClientLabel = new System.Windows.Forms.Label();
        Nom = new System.Windows.Forms.TextBox();
        SuspendLayout();
        // 
        // ServerAddressLabel
        // 
        ServerAddressLabel.Location = new System.Drawing.Point(24, 12);
        ServerAddressLabel.Name = "ServerAddressLabel";
        ServerAddressLabel.Size = new System.Drawing.Size(120, 19);
        ServerAddressLabel.TabIndex = 0;
        ServerAddressLabel.Text = "Addresse du serveur:";
        // 
        // ServerPortLabel
        // 
        ServerPortLabel.Location = new System.Drawing.Point(24, 41);
        ServerPortLabel.Name = "ServerPortLabel";
        ServerPortLabel.Size = new System.Drawing.Size(98, 19);
        ServerPortLabel.TabIndex = 1;
        ServerPortLabel.Text = "Port du serveur:";
        // 
        // ServerAddress
        // 
        ServerAddress.Location = new System.Drawing.Point(163, 8);
        ServerAddress.Name = "ServerAddress";
        ServerAddress.Size = new System.Drawing.Size(143, 23);
        ServerAddress.TabIndex = 2;
        // 
        // ServerPort
        // 
        ServerPort.Location = new System.Drawing.Point(163, 37);
        ServerPort.Name = "ServerPort";
        ServerPort.Size = new System.Drawing.Size(143, 23);
        ServerPort.TabIndex = 3;
        // 
        // ChatBox
        // 
        ChatBox.Location = new System.Drawing.Point(24, 103);
        ChatBox.Name = "ChatBox";
        ChatBox.ReadOnly = true;
        ChatBox.Size = new System.Drawing.Size(738, 287);
        ChatBox.TabIndex = 4;
        ChatBox.Text = "";
        // 
        // ClientMessage
        // 
        ClientMessage.Enabled = false;
        ClientMessage.Location = new System.Drawing.Point(24, 404);
        ClientMessage.Name = "ClientMessage";
        ClientMessage.Size = new System.Drawing.Size(642, 23);
        ClientMessage.TabIndex = 5;
        // 
        // ConnectButton
        // 
        ConnectButton.Location = new System.Drawing.Point(325, 12);
        ConnectButton.Name = "ConnectButton";
        ConnectButton.Size = new System.Drawing.Size(100, 23);
        ConnectButton.TabIndex = 6;
        ConnectButton.Text = "Se Connecter";
        ConnectButton.UseVisualStyleBackColor = true;
        ConnectButton.Click += ConnectButton_Click;
        // 
        // SendButton
        // 
        SendButton.Enabled = false;
        SendButton.Location = new System.Drawing.Point(672, 404);
        SendButton.Name = "SendButton";
        SendButton.Size = new System.Drawing.Size(90, 23);
        SendButton.TabIndex = 7;
        SendButton.Text = "Envoyer";
        SendButton.UseVisualStyleBackColor = true;
        SendButton.Click += SendButton_Click;
        // 
        // NomClientLabel
        // 
        NomClientLabel.Location = new System.Drawing.Point(24, 70);
        NomClientLabel.Name = "NomClientLabel";
        NomClientLabel.Size = new System.Drawing.Size(98, 19);
        NomClientLabel.TabIndex = 8;
        NomClientLabel.Text = "Nom:";
        // 
        // Nom
        // 
        Nom.Location = new System.Drawing.Point(163, 66);
        Nom.Name = "Nom";
        Nom.Size = new System.Drawing.Size(143, 23);
        Nom.TabIndex = 9;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(Nom);
        Controls.Add(NomClientLabel);
        Controls.Add(SendButton);
        Controls.Add(ConnectButton);
        Controls.Add(ClientMessage);
        Controls.Add(ChatBox);
        Controls.Add(ServerPort);
        Controls.Add(ServerAddress);
        Controls.Add(ServerPortLabel);
        Controls.Add(ServerAddressLabel);
        KeyPreview = true;
        Text = "Chat Client";
        KeyPress += Form1_KeyPress;
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label NomClientLabel;
    private System.Windows.Forms.TextBox Nom;

    private System.Windows.Forms.Button SendButton;

    private System.Windows.Forms.Button ConnectButton;

    private System.Windows.Forms.RichTextBox ChatBox;
    private System.Windows.Forms.TextBox ClientMessage;

    private System.Windows.Forms.TextBox ServerAddress;
    private System.Windows.Forms.TextBox ServerPort;

    private System.Windows.Forms.Label ServerPortLabel;

    private System.Windows.Forms.Label ServerAddressLabel;

    #endregion
}