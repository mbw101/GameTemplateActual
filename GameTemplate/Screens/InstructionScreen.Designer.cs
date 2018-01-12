namespace GameTemplate.Screens
{
    partial class InstructionScreen
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
            this.instructionLabel = new System.Windows.Forms.Label();
            this.instructionLabel2 = new System.Windows.Forms.Label();
            this.instructionLabel3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.YellowGreen;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Location = new System.Drawing.Point(107, 269);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 39);
            this.exitButton.TabIndex = 11;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionLabel.Location = new System.Drawing.Point(66, 47);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(381, 25);
            this.instructionLabel.TabIndex = 12;
            this.instructionLabel.Text = "Use left and right arrows to move. ";
            // 
            // instructionLabel2
            // 
            this.instructionLabel2.AutoSize = true;
            this.instructionLabel2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionLabel2.Location = new System.Drawing.Point(60, 88);
            this.instructionLabel2.Name = "instructionLabel2";
            this.instructionLabel2.Size = new System.Drawing.Size(199, 23);
            this.instructionLabel2.TabIndex = 13;
            this.instructionLabel2.Text = "Use space to shoot.";
            // 
            // instructionLabel3
            // 
            this.instructionLabel3.AutoSize = true;
            this.instructionLabel3.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionLabel3.Location = new System.Drawing.Point(63, 121);
            this.instructionLabel3.Name = "instructionLabel3";
            this.instructionLabel3.Size = new System.Drawing.Size(313, 23);
            this.instructionLabel3.TabIndex = 14;
            this.instructionLabel3.Text = "Destroy all aliens to gain a life.";
            // 
            // InstructionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.instructionLabel3);
            this.Controls.Add(this.instructionLabel2);
            this.Controls.Add(this.instructionLabel);
            this.Controls.Add(this.exitButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InstructionScreen";
            this.Size = new System.Drawing.Size(348, 310);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label instructionLabel;
        private System.Windows.Forms.Label instructionLabel2;
        private System.Windows.Forms.Label instructionLabel3;
    }
}
