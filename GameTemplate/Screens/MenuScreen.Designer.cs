namespace GameTemplate.Screens
{
    partial class MenuScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exitButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.gameTitle = new System.Windows.Forms.Label();
            this.instructionButton = new System.Windows.Forms.Button();
            this.creditLabel = new System.Windows.Forms.Label();
            this.alien1Label = new System.Windows.Forms.Label();
            this.alien2Label = new System.Windows.Forms.Label();
            this.alien3Label = new System.Windows.Forms.Label();
            this.randomAlienLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.White;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(97, 226);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 39);
            this.exitButton.TabIndex = 10;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.Tomato;
            this.playButton.Location = new System.Drawing.Point(97, 67);
            this.playButton.Margin = new System.Windows.Forms.Padding(2);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(120, 39);
            this.playButton.TabIndex = 6;
            this.playButton.Tag = "GameScreen";
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // gameTitle
            // 
            this.gameTitle.AutoSize = true;
            this.gameTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameTitle.ForeColor = System.Drawing.Color.White;
            this.gameTitle.Location = new System.Drawing.Point(128, 27);
            this.gameTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gameTitle.Name = "gameTitle";
            this.gameTitle.Size = new System.Drawing.Size(138, 25);
            this.gameTitle.TabIndex = 5;
            this.gameTitle.Text = "Space Invaders";
            // 
            // instructionButton
            // 
            this.instructionButton.BackColor = System.Drawing.Color.White;
            this.instructionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.instructionButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionButton.Location = new System.Drawing.Point(97, 135);
            this.instructionButton.Margin = new System.Windows.Forms.Padding(2);
            this.instructionButton.Name = "instructionButton";
            this.instructionButton.Size = new System.Drawing.Size(120, 39);
            this.instructionButton.TabIndex = 7;
            this.instructionButton.Text = "How To Play";
            this.instructionButton.UseVisualStyleBackColor = false;
            this.instructionButton.Click += new System.EventHandler(this.instructionButton_Click);
            // 
            // creditLabel
            // 
            this.creditLabel.AutoSize = true;
            this.creditLabel.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditLabel.ForeColor = System.Drawing.Color.White;
            this.creditLabel.Location = new System.Drawing.Point(28, 271);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(457, 25);
            this.creditLabel.TabIndex = 11;
            this.creditLabel.Text = "Made by Avery Cairns and Malcolm Wright";
            // 
            // alien1Label
            // 
            this.alien1Label.ForeColor = System.Drawing.Color.Black;
            this.alien1Label.Image = global::GameTemplate.Properties.Resources.alien10altBig;
            this.alien1Label.Location = new System.Drawing.Point(218, 67);
            this.alien1Label.Name = "alien1Label";
            this.alien1Label.Size = new System.Drawing.Size(100, 23);
            this.alien1Label.TabIndex = 12;
            // 
            // alien2Label
            // 
            this.alien2Label.Image = global::GameTemplate.Properties.Resources.alien20altBig;
            this.alien2Label.Location = new System.Drawing.Point(218, 120);
            this.alien2Label.Name = "alien2Label";
            this.alien2Label.Size = new System.Drawing.Size(100, 23);
            this.alien2Label.TabIndex = 13;
            // 
            // alien3Label
            // 
            this.alien3Label.Image = global::GameTemplate.Properties.Resources.alien40Big;
            this.alien3Label.Location = new System.Drawing.Point(222, 168);
            this.alien3Label.Name = "alien3Label";
            this.alien3Label.Size = new System.Drawing.Size(100, 23);
            this.alien3Label.TabIndex = 14;
            // 
            // randomAlienLabel
            // 
            this.randomAlienLabel.Image = global::GameTemplate.Properties.Resources.alienRandomBig;
            this.randomAlienLabel.Location = new System.Drawing.Point(230, 226);
            this.randomAlienLabel.Name = "randomAlienLabel";
            this.randomAlienLabel.Size = new System.Drawing.Size(100, 23);
            this.randomAlienLabel.TabIndex = 15;
            // 
            // MenuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.randomAlienLabel);
            this.Controls.Add(this.alien3Label);
            this.Controls.Add(this.alien2Label);
            this.Controls.Add(this.alien1Label);
            this.Controls.Add(this.creditLabel);
            this.Controls.Add(this.instructionButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.gameTitle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MenuScreen";
            this.Size = new System.Drawing.Size(321, 296);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Label gameTitle;
        private System.Windows.Forms.Button instructionButton;
        private System.Windows.Forms.Label creditLabel;
        private System.Windows.Forms.Label alien1Label;
        private System.Windows.Forms.Label alien2Label;
        private System.Windows.Forms.Label alien3Label;
        private System.Windows.Forms.Label randomAlienLabel;
    }
}
