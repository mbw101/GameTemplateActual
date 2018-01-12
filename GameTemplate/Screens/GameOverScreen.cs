using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTemplate.Screens
{
    public partial class GameOverScreen : UserControl
    {
        public GameOverScreen()
        {
            InitializeComponent();

             ScreenControl.setComponentValues(this);

             playButton.Focus();

             defaultOverride();
        }

        private void defaultOverride()
        {
            this.BackColor = Color.Black;

            foreach (Control c in this.Controls)
            {
                if (c != exitButton)
                {
                    c.Location = new Point(c.Location.X, c.Location.Y + 75);
                }
                else
                {
                    c.Location = new Point(c.Location.X, c.Location.Y + c.Height + 50);
                }

                if (c == loseTitleLabel)
                {
                    c.Font = new Font("Verdana", 45);

                    c.Location = new Point(ScreenControl.controlWidth / 2 - c.Width / 2, 40);
                }

                if (c == scoreLabel)
                {
                    c.Font = new Font("Verdana", 30);

                    c.Text += GameScreen.score;

                    c.Location = new Point(ScreenControl.controlWidth / 2 - c.Width / 2, 140);
                }
            }

        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // switch to game screen
            ScreenControl.changeScreen(this, "GameScreen");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // switch to main menu
            ScreenControl.changeScreen(this, "MenuScreen");
        }
    }
}
