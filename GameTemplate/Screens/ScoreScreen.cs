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
    public partial class ScoreScreen : UserControl
    {
        public ScoreScreen()
        {
            InitializeComponent();

            ScreenControl.setComponentValues(this);
            defaultOverride();
            
            showScores();
        }

        public void showScores()
        {
            label1.Text = "";

            foreach (int i in ScreenControl.highScores)
            {
                label1.Text += i + "\n";
            }         
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ScreenControl.changeScreen(this, "MenuScreen");
        }

        /// <summary>
        /// Change any control default values here
        /// </summary>
        public void defaultOverride()
        {

        }
    }
}
