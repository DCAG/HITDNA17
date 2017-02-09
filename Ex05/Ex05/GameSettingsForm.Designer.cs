namespace Ex05
{
    partial class GameSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.ButtonBoardSize = new System.Windows.Forms.Button();
            this.buttonVsPlayer = new System.Windows.Forms.Button();
            this.buttonVsComputer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonBoardSize
            // 
            this.ButtonBoardSize.Location = new System.Drawing.Point(12, 12);
            this.ButtonBoardSize.Name = "ButtonBoardSize";
            this.ButtonBoardSize.Size = new System.Drawing.Size(505, 95);
            this.ButtonBoardSize.TabIndex = 0;
            this.ButtonBoardSize.Text = "Board Size: {0}x{0} (click to increase)";
            this.ButtonBoardSize.UseVisualStyleBackColor = true;
            this.ButtonBoardSize.Click += new System.EventHandler(this.ButtonBoardSize_Click);
            // 
            // buttonVsPlayer
            // 
            this.buttonVsPlayer.Location = new System.Drawing.Point(279, 130);
            this.buttonVsPlayer.Name = "buttonVsPlayer";
            this.buttonVsPlayer.Size = new System.Drawing.Size(238, 95);
            this.buttonVsPlayer.TabIndex = 1;
            this.buttonVsPlayer.Text = "Play against another player";
            this.buttonVsPlayer.UseVisualStyleBackColor = true;
            this.buttonVsPlayer.Click += new System.EventHandler(this.buttonVsPlayer_Click);
            // 
            // buttonVsComputer
            // 
            this.buttonVsComputer.Location = new System.Drawing.Point(12, 130);
            this.buttonVsComputer.Name = "buttonVsComputer";
            this.buttonVsComputer.Size = new System.Drawing.Size(238, 95);
            this.buttonVsComputer.TabIndex = 2;
            this.buttonVsComputer.Text = "Play against the computer";
            this.buttonVsComputer.UseVisualStyleBackColor = true;
            this.buttonVsComputer.Click += new System.EventHandler(this.buttonVsComputer_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 241);
            this.Controls.Add(this.buttonVsComputer);
            this.Controls.Add(this.buttonVsPlayer);
            this.Controls.Add(this.ButtonBoardSize);
            this.Name = "GameSettingsForm";
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonBoardSize;
        private System.Windows.Forms.Button buttonVsComputer;
        private System.Windows.Forms.Button buttonVsPlayer;
    }
}