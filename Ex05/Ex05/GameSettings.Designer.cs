namespace Ex05
{
    internal partial class GameSettings
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
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonVsPlayer = new System.Windows.Forms.Button();
            this.buttonVsComputer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Location = new System.Drawing.Point(7, 7);
            this.buttonBoardSize.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(275, 51);
            this.buttonBoardSize.TabIndex = 0;
            this.buttonBoardSize.Text = "Board Size: {0}x{0} (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonVsPlayer
            // 
            this.buttonVsPlayer.Location = new System.Drawing.Point(152, 70);
            this.buttonVsPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonVsPlayer.Name = "buttonVsPlayer";
            this.buttonVsPlayer.Size = new System.Drawing.Size(130, 51);
            this.buttonVsPlayer.TabIndex = 1;
            this.buttonVsPlayer.Text = "Play against another player";
            this.buttonVsPlayer.UseVisualStyleBackColor = true;
            this.buttonVsPlayer.Click += new System.EventHandler(this.buttonVsPlayer_Click);
            // 
            // buttonVsComputer
            // 
            this.buttonVsComputer.Location = new System.Drawing.Point(7, 70);
            this.buttonVsComputer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonVsComputer.Name = "buttonVsComputer";
            this.buttonVsComputer.Size = new System.Drawing.Size(130, 51);
            this.buttonVsComputer.TabIndex = 2;
            this.buttonVsComputer.Text = "Play against the computer";
            this.buttonVsComputer.UseVisualStyleBackColor = true;
            this.buttonVsComputer.Click += new System.EventHandler(this.buttonVsComputer_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 131);
            this.Controls.Add(this.buttonVsComputer);
            this.Controls.Add(this.buttonVsPlayer);
            this.Controls.Add(this.buttonBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GameSettings";
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonVsComputer;
        private System.Windows.Forms.Button buttonVsPlayer;
    }
}