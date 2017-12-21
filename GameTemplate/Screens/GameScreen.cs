using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using GameTemplate.Dialogs;

namespace GameTemplate.Screens
{
    public partial class GameScreen : UserControl
    {
        int lives, score;
        int barrier1health = 16, barrier2health = 16,
            barrier3health = 16, barrier4health = 16;
        bool bulletOnScreen = false;
        bool leftKeyDown, shootKeyDown, rightKeyDown, exit;

        Point[] alienPoints;

        // Graphics
        Graphics offScreen;
        Graphics onScreen;
        Bitmap bm;
        SolidBrush solidBrush, greenBrush;
        Pen pen;
        Font titleFont, menuFont, subFont;
        Image alien1, alien2, alien3, ufo, player,
             barrier1, barrier2, barrier3, barrier4;
        // we can change the barrier image depending on the health

        Rectangle playerRect, barrier1Rect,
            barrier2Rect, barrier3Rect, barrier4Rect;


        // sounds and images
        SoundPlayer playerBullet, alienBullet, alienHit, playerHit,
            ufoHit, ufoSound;

        // constants
        const int ALIEN1_SCORE = 10;
        const int ALIEN2_SCORE = 20;
        const int ALIEN3_SCORE = 40;
        const int BULLET_SPEED = 10;
        const int PLAYER_SPEED = 4;
        const int ALIEN_SPEED = 1;
        const int ALIEN_WIDTH = 11;
        const int ALIEN_HEIGHT = 10;

        public GameScreen()
        {
            InitializeComponent();

           // this.BackColor = Color.Gray;

            pen = new Pen(Color.White, 10);
            solidBrush = new SolidBrush(Color.White);
            greenBrush = new SolidBrush(Color.Green);

            titleFont = new Font("Consolas", 36, FontStyle.Regular);
            menuFont = new Font("Consolas", 24, FontStyle.Regular);
            subFont = new Font("Consolas", 12, FontStyle.Regular);

            // set up rectangles
            playerRect.X = 20;
            playerRect.Y = 550;
            playerRect.Width = 45;
            playerRect.Height = 24;

            barrier1Rect.Width = 72;
            barrier1Rect.X = 200 - (barrier1Rect.Width / 2);
            barrier1Rect.Y = 450;
            barrier1Rect.Height = 54;

            barrier2Rect.X = 400 - (barrier1Rect.Width / 2);
            barrier2Rect.Y = 450;
            barrier2Rect.Width = 72;
            barrier2Rect.Height = 54;

            barrier3Rect.X = 600 - (barrier1Rect.Width / 2);
            barrier3Rect.Y = 450;
            barrier3Rect.Width = 72;
            barrier3Rect.Height = 54;

            // barrier4

            // load sounds
            playerBullet = new SoundPlayer(Properties.Resources.player_shoot);
            ufoSound = new SoundPlayer(Properties.Resources.ufo_onscreen);
            ufoHit = new SoundPlayer(Properties.Resources.ufo_killed);
            alienBullet = new SoundPlayer(Properties.Resources.invader_shoot);

            player = new Bitmap(Properties.Resources.playerBig);
            alien1 = new Bitmap(Properties.Resources.alien10);
            alien2 = new Bitmap(Properties.Resources.alien20);
            alien3 = new Bitmap(Properties.Resources.alien40);
            ufo = new Bitmap(Properties.Resources.alienRandom);
            barrier1 = new Bitmap(Properties.Resources.coverFull);
            barrier2 = barrier1;
            barrier3 = barrier1;
        }

        #region required global values - DO NOT CHANGE

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown;

        //player2 button control keys - DO NOT CHANGE
        Boolean aDown, sDown, dDown, wDown, cDown, vDown, xDown, zDown;

        #endregion

        //TODO - Place game global variables here 
        //---------------------------------------
     

        //----------------------------------------

        // PreviewKeyDown required for UserControl instead of KeyDown as on a form
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pauseGame();
            }

            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.B:
                    bDown = true;
                    break;
                case Keys.N:
                    nDown = true;
                    break;
                case Keys.M:
                    mDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
            }

            //player 2 button presses
            switch (e.KeyCode)
            {
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.C:
                    cDown = true;
                    break;
                case Keys.V:
                    vDown = true;
                    break;
                case Keys.X:
                    xDown = true;
                    break;
                case Keys.Z:
                    zDown = true;
                    break;
                default:
                    break;
            }
        }
        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.B:
                    bDown = false;
                    break;
                case Keys.N:
                    nDown = false;
                    break;
                case Keys.M:
                    mDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }

            //player 2 button releases
            switch (e.KeyCode)
            {
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.C:
                    cDown = false;
                    break;
                case Keys.V:
                    vDown = false;
                    break;
                case Keys.X:
                    xDown = false;
                    break;
                case Keys.Z:
                    zDown = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// All game update logic must be placed in this event method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            #region main character movements

            if (leftArrowDown == true && playerRect.X > 0)
            {
                playerRect.X -= PLAYER_SPEED;
            }

            if (rightArrowDown == true && playerRect.X < (Width - 45) - PLAYER_SPEED)
            {
                playerRect.X += PLAYER_SPEED;
            }

            #endregion

            #region monster movements - TO BE COMPLETED


            #endregion

            #region collision detection - TO BE COMPLETED
            

            #endregion


            //refresh the screen, which causes the GameScreen_Paint method to run
            Refresh();
        }

        /// <summary>
        /// Open the pause dialog box and gets Cancel or Abort result from it
        /// </summary>
        private void pauseGame()
        {
            gameTimer.Enabled = false;
            rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = false;

            DialogResult result = PauseDialog.Show();

            if (result == DialogResult.Cancel)
            {
                gameTimer.Enabled = true;
            }
            if (result == DialogResult.Abort)
            {
                ScreenControl.changeScreen(this, "MenuScreen");
            }
        }

        /// <summary>
        /// All drawing, (and only drawing), to be done here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw rectangle to screen
            e.Graphics.DrawImage(player, playerRect.X, playerRect.Y, 
                playerRect.Width, playerRect.Height);

            if (barrier1health == 16)
            {
                e.Graphics.DrawImage(barrier1, barrier1Rect.X, barrier1Rect.Y,
                    barrier1Rect.Width, barrier1Rect.Height);                   
            }
            if (barrier2health == 16)
            {
                e.Graphics.DrawImage(barrier2, barrier2Rect.X,
                    barrier2Rect.Y, barrier2Rect.Width,
                    barrier2Rect.Height);
            }
            if (barrier3health == 16)
            {
                e.Graphics.DrawImage(barrier3, barrier3Rect.X,
                    barrier3Rect.Y, barrier3Rect.Width,
                    barrier3Rect.Height);
            }
            /*if (barrier3health == 16)
            {
                e.Graphics.DrawImage(barrier2, barrier2Rect.X,
                    barrier2Rect.Y, barrier2Rect.Width,
                    barrier2Rect.Height);
            } */
        }
    }
}
