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
    public partial class InstructionScreen : UserControl
    {
        public InstructionScreen()
        {
            InitializeComponent();

            defaultOverride();
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
            foreach (Control c in this.Controls)
            {
                if (c == instructionLabel)
                {
                    c.Location = new Point(50, 50);
                }
                if (c == exitButton)
                {
                    c.Location = new Point(ScreenControl.controlWidth / 2 -
                        c.Width / 2, ScreenControl.controlHeight - 50);
                }
            }
        }
    }
}
