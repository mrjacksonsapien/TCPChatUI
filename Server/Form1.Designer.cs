namespace Server;

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
        chatBox = new System.Windows.Forms.RichTextBox();
        PortNumber = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        ToggleServer = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // chatBox
        // 
        chatBox.BackColor = System.Drawing.SystemColors.Window;
        chatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        chatBox.Location = new System.Drawing.Point(26, 74);
        chatBox.Name = "chatBox";
        chatBox.ReadOnly = true;
        chatBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        chatBox.Size = new System.Drawing.Size(739, 343);
        chatBox.TabIndex = 4;
        chatBox.Text = "";
        // 
        // PortNumber
        // 
        PortNumber.Location = new System.Drawing.Point(71, 19);
        PortNumber.Name = "PortNumber";
        PortNumber.Size = new System.Drawing.Size(132, 23);
        PortNumber.TabIndex = 5;
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(26, 19);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(39, 23);
        label1.TabIndex = 6;
        label1.Text = "Port:";
        // 
        // ToggleServer
        // 
        ToggleServer.Location = new System.Drawing.Point(221, 19);
        ToggleServer.Name = "ToggleServer";
        ToggleServer.Size = new System.Drawing.Size(85, 23);
        ToggleServer.TabIndex = 7;
        ToggleServer.Text = "Start";
        ToggleServer.UseVisualStyleBackColor = true;
        ToggleServer.Click += ToggleServer_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(ToggleServer);
        Controls.Add(label1);
        Controls.Add(PortNumber);
        Controls.Add(chatBox);
        Location = new System.Drawing.Point(15, 15);
        Text = "Chat Server";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button ToggleServer;

    private System.Windows.Forms.TextBox PortNumber;
    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.RichTextBox chatBox;

    #endregion
}