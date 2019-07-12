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
using System.Threading;
using System.IO;

// Avery Cairns and Malcolm Wright
// January 17th, 2018
// Space Invaders

namespace GameTemplate.Screens
{
    public partial class GameScreen : UserControl
    {
        public int lives = 3;
        public static int score = 0;
        int barrier1health = 60, barrier2health = 60,
            barrier3health = 60, barrier4health = 60;
        bool bulletOnScreen = false, playerHit = false,
            ufoOnScreen = false;
        bool alienKilled = false;

        enum Direction
        {
            LEFT,
            RIGHT
        }

        Random randGen = new Random();
        int randomShootTime = 0;
        Direction alienDirection;

        bool alienMovedown = false;

        // Graphics
        SolidBrush solidBrush, greenBrush;
        Pen pen;
        Font titleFont, menuFont, subFont;
        Image alien1, alien2, alien3, ufo, playerImage, playerExplosion,
             barrier1, barrier2, barrier3, barrier4, bullet, alienExplosion;
        // we can change the barrier image depending on the health

        Rectangle barrier1Rect, barrier2Rect,
            barrier3Rect, barrier4Rect, ufoRect, destroyedAlienRect;

        Player player;
        Bullet playerBullet;

        List<Alien> row1 = new List<Alien>(11);
        List<Alien> row2 = new List<Alien>(11);
        List<Alien> row3 = new List<Alien>(11);
        List<Alien> row4 = new List<Alien>(11);
        List<Alien> row5 = new List<Alien>(11);

        List<Bullet> alienBullets = new List<Bullet>(MAX_ALIEN_BULLETS);

        // sounds and images
        System.Windows.Media.MediaPlayer playerBulletSound, alienBullet,
            alienHit, playerHitSound,
            ufoHit;

        SoundPlayer ufoSound;
        
        // constants
        const int ALIEN1_SCORE = 10;
        const int ALIEN2_SCORE = 20;
        const int ALIEN3_SCORE = 40;
        const int BULLET_SPEED = 8;
        const int ALIEN_BULLET_SPEED = 7;
        const int MAX_ALIEN_BULLETS = 3;
        const int PLAYER_SPEED = 6;
        const int ALIEN_DOWNSPEED = 24;
        const int ALIEN_WIDTH = 36;
        const int ALIEN_HEIGHT = 24;
        const int UFO_WIDTH = 48;
        const int UFO_HEIGHT = 21;
        const int UFO_SPEED = 4; 
        const int UFO_WAIT_TIME = 350;
        const int UFO_SCORE = 100;
        const int MOVEMENT_TIME = 500;
        const int ALIEN_SHOOT_TIME = 600;
        const int MAX_ALIEN_SHOOT_TIME = 800;
        const int ALIEN_SLOW_SHOOT_TIME = 400;
        const int BULLET_WIDTH = 3;
        const int BULLET_HEIGHT = 9;
        const int BOUNDARY_LEFT = 5;

        // counters
        int elapsed = 0;
        int alienAnimationCounter = 0;
        int timeSinceLastShot = 0;
        int ufoCounter = 0;
        int explosionCounter = 0;
        int levelCounter = 1;

        // alien speeds when they move down further
        int ALIEN_SPEED = 6; 
        int ALIEN_THREE_QUARTER_SPEED = 10;
        int ALIEN_HALF_SPEED = 14;
        int ALIEN_QUARTER_SPEED = 18;

        public GameScreen()
        {
            InitializeComponent();

            // reset score
            score = 0;

            pen = new Pen(Color.White, 10);
            solidBrush = new SolidBrush(Color.White);
            greenBrush = new SolidBrush(Color.Green);

            titleFont = new Font("Verdana", 36, FontStyle.Regular);
            menuFont = new Font("Verdana", 24, FontStyle.Regular);
            subFont = new Font("Verdana", 24, FontStyle.Regular);

            // set up rectangles
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

            barrier4Rect.X = 800 - (barrier1Rect.Width / 2);
            barrier4Rect.Y = 450;
            barrier4Rect.Width = 72;
            barrier4Rect.Height = 54;

            int playerWidth = 45;
            int playerHeight = 24;

            player = new Player(barrier1Rect.X + barrier1Rect.Width / 2 - playerWidth / 2,
                550, playerWidth, playerHeight, 0, 0);

            playerBullet = new Bullet(0, 0, BULLET_WIDTH, BULLET_HEIGHT, -BULLET_SPEED);

            ufoRect.X = 0;
            ufoRect.Y = 0;
            ufoRect.Width = UFO_WIDTH;
            ufoRect.Height = UFO_HEIGHT;

            // load sounds
            playerBulletSound = new System.Windows.Media.MediaPlayer();
            playerBulletSound.Open(new Uri(Application.StartupPath + "/Resources/player_shoot.wav"));

            ufoSound = new SoundPlayer(Properties.Resources.ufo_onscreen);

            ufoHit = new System.Windows.Media.MediaPlayer();
            ufoHit.Open(new Uri(Application.StartupPath + "/Resources/ufo_killed.wav"));

            alienBullet = new System.Windows.Media.MediaPlayer();
            alienBullet.Open(new Uri(Application.StartupPath + "/Resources/invader_shoot.wav"));

            alienHit = new System.Windows.Media.MediaPlayer();
            alienHit.Open(new Uri(Application.StartupPath + "/Resources/alienHit.wav"));

            playerHitSound = new System.Windows.Media.MediaPlayer();
            playerHitSound.Open(new Uri(Application.StartupPath + "/Resources/playerExplosion.wav"));

            bullet = new Bitmap(Properties.Resources.bullet);
            playerImage = new Bitmap(Properties.Resources.playerBig);
            playerExplosion = new Bitmap(Properties.Resources.playerExplosionBig);
            alienExplosion = new Bitmap(Properties.Resources.explosionBig);
            alien1 = new Bitmap(Properties.Resources.alien10Big);
            alien2 = new Bitmap(Properties.Resources.alien20Big);
            alien3 = new Bitmap(Properties.Resources.alien40Big);
            ufo = new Bitmap(Properties.Resources.alienRandomBig);
            barrier1 = new Bitmap(Properties.Resources.coverFullBig);
            barrier2 = barrier1;
            barrier3 = barrier1;
            barrier4 = barrier1;

            for (int i = 0; i < row1.Capacity; i++)
            {
                Alien temp1 = new Alien(100 + (45 * i), 100, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp2 = new Alien(100 + (45 * i), 150, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp3 = new Alien(100 + (45 * i), 200, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp4 = new Alien(100 + (45 * i), 250, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp5 = new Alien(100 + (45 * i), 300, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);

                row1.Add(temp1); // top
                row2.Add(temp2);
                row3.Add(temp3); // middle
                row4.Add(temp4);
                row5.Add(temp5); // bottom
            }
        }

        public void GameOver()
        {
            lives = 0;

            gameTimer.Enabled = false;

            ScreenControl.changeScreen(this, "GameOverScreen");
        }

        public void MoveAliensDown()
        {
            if (alienMovedown)
            {
                #region Moving each row of aliens 
                for (int i = 0; i < row1.Count(); i++)
                {
                    row1[i].move(0, ALIEN_SPEED);

                    // Check if row 1 has moved past the barriers
                    if (row1[i].getY() >= barrier1Rect.Y + ALIEN_HEIGHT || row1[i].getY() >= barrier2Rect.Y + ALIEN_HEIGHT
                    || row1[i].getY() >= barrier3Rect.Y + ALIEN_HEIGHT || row1[i].getY() >= barrier4Rect.Y + ALIEN_HEIGHT)
                    {
                        // end game
                        GameOver();
                    }
                }

                for (int i = 0; i < row2.Count(); i++)
                {
                    row2[i].move(0, ALIEN_SPEED);
                }

                for (int i = 0; i < row3.Count(); i++)
                {
                    row3[i].move(0, ALIEN_SPEED);
                }

                for (int i = 0; i < row4.Count(); i++)
                {
                    row4[i].move(0, ALIEN_SPEED);
                }

                for (int i = 0; i < row5.Count(); i++)
                {
                    row5[i].move(0, ALIEN_SPEED);

                    // check to see if the 5th row has passed the barriers
                    if (row5[i].getY() >= barrier1Rect.Y + ALIEN_HEIGHT || row5[i].getY() >= barrier2Rect.Y + ALIEN_HEIGHT
                    || row5[i].getY() >= barrier3Rect.Y + ALIEN_HEIGHT || row5[i].getY() >= barrier4Rect.Y + ALIEN_HEIGHT)
                    {
                        // end game
                        lives = 0;
                    }
                }
                #endregion
                // make the aliens move down
                alienMovedown = false;
            }
        }

        #region required global values - DO NOT CHANGE

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown;

        //player2 button control keys - DO NOT CHANGE
        Boolean aDown, sDown, dDown, wDown, cDown, vDown, xDown, zDown;

        #endregion

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
        }

        /// <summary>
        /// All game update logic must be placed in this event method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            #region main character movements

            if (leftArrowDown == true && player.getX() > 0) 
            {
                player.setSpeed(-PLAYER_SPEED, 0);
                player.move();
            }

            if (rightArrowDown == true && player.getX() < (Width - 45) - PLAYER_SPEED)
            {
                player.setSpeed(PLAYER_SPEED, 0);
                player.move();
            }

            if (spaceDown && !bulletOnScreen)
            {
                bulletOnScreen = true;

                playerBulletSound.Stop();
                playerBulletSound.Play();

                // move the bullet over the player
                int startX = player.getX() + (player.getRect().Width / 2);
                int startY = player.getY() - 5;

                playerBullet.setPosition(startX, startY);
            }

            if (bulletOnScreen)
            {
                // move bullet if it is on screen
                playerBullet.move();
            }

            if (playerBullet.getY() <= 0)
            {
                bulletOnScreen = false;
            }

            if (playerHit)
            {
                Thread.Sleep(1000);

                alienBullets.Clear();

                playerHit = false;

                // place rect below the first barrier
                int playerX = barrier1Rect.X + barrier1Rect.Width / 2 - player.getRect().Width / 2;
                player.setPosition(playerX, player.getY());
            }

            #endregion

            #region monster movements 

            if (levelCounter == 2)
            {
                ALIEN_SPEED = 8;
                ALIEN_THREE_QUARTER_SPEED = 12; 
                ALIEN_HALF_SPEED = 16;
                ALIEN_QUARTER_SPEED = 20;
            }
            else if (levelCounter == 3)
            {
                ALIEN_SPEED = 11;
                ALIEN_THREE_QUARTER_SPEED = 15;
                ALIEN_HALF_SPEED = 19;
                ALIEN_QUARTER_SPEED = 23;
            }
            else if (levelCounter >= 4)
            {
                ALIEN_SPEED = 14;
                ALIEN_THREE_QUARTER_SPEED = 18;
                ALIEN_HALF_SPEED = 22;
                ALIEN_QUARTER_SPEED = 26;
            }

            if (alienKilled && explosionCounter <= 6)
            {
                explosionCounter ++;            
            }
            else
            {
                alienKilled = false;
                explosionCounter = 0;
            }

            elapsed += gameTimer.Interval;

            if (elapsed >= MOVEMENT_TIME)
            {
                elapsed = 0;

                int sum = row1.Count + row2.Count + row3.Count + row4.Count + row5.Count;

                for (int i = 0; i < row1.Count(); i++)
                {
                    // move aliens based on direction
                    if (alienDirection == Direction.LEFT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row1[i].move(-ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row1[i].move(-ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row1[i].move(-ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            // if everything else is not true
                            // move at regular speed
                            row1[i].move(-ALIEN_SPEED, 0);
                        }
                    }
                    else if (alienDirection == Direction.RIGHT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row1[i].move(ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row1[i].move(ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row1[i].move(ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            // move at regular speed
                            row1[i].move(ALIEN_SPEED, 0);
                        }
                    }
                }

                for (int i = 0; i < row2.Count(); i++)
                {
                    // move aliens based on direction
                    if (alienDirection == Direction.LEFT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row2[i].move(-ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row2[i].move(-ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row2[i].move(-ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            row2[i].move(-ALIEN_SPEED, 0);              
                        }
                    }
                    else if (alienDirection == Direction.RIGHT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row2[i].move(ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row2[i].move(-ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row2[i].move(-ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            // move at regular speed
                            row2[i].move(ALIEN_SPEED, 0);
                        }
                    }
                }

                for (int i = 0; i < row3.Count(); i++)
                {
                    // move aliens based on direction
                    if (alienDirection == Direction.LEFT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row3[i].move(-ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row3[i].move(-ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row3[i].move(-ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            row3[i].move(-ALIEN_SPEED, 0);
                        }
                    }
                    else if (alienDirection == Direction.RIGHT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row3[i].move(ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row3[i].move(ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row3[i].move(ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            // move at regular speeds
                            row3[i].move(ALIEN_SPEED, 0);
                        }
                    }
                }

                for (int i = 0; i < row4.Count(); i++)
                {
                    // move aliens based on direction
                    if (alienDirection == Direction.LEFT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row4[i].move(-ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row4[i].move(-ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row4[i].move(-ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            // move at regular speed
                            row4[i].move(-ALIEN_SPEED, 0);
                        }
                    }
                    else if (alienDirection == Direction.RIGHT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row4[i].move(ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row4[i].move(ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row4[i].move(ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            // move at regular speed
                            row4[i].move(ALIEN_SPEED, 0);
                        }
                    }
                }

                for (int i = 0; i < row5.Count(); i++)
                {
                    // move aliens based on direction
                    if (alienDirection == Direction.LEFT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row5[i].move(-ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row5[i].move(-ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row5[i].move(-ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            row5[i].move(-ALIEN_SPEED, 0);
                        }
                    }
                    else if (alienDirection == Direction.RIGHT)
                    {
                        // check to see if there is a quarter gone
                        if (sum <= 49 && sum >= 34)
                        {
                            row5[i].move(ALIEN_QUARTER_SPEED, 0);
                        }
                        // check to see if there is half gone
                        else if (sum <= 33 && sum >= 18)
                        {
                            row5[i].move(ALIEN_HALF_SPEED, 0);
                        }
                        // check to see if there are three quarters gone
                        else if (sum <= 17 && sum >= 2)
                        {
                            row5[i].move(ALIEN_THREE_QUARTER_SPEED, 0);
                        }
                        else
                        {
                            // move at regular speed
                            row5[i].move(ALIEN_SPEED, 0);
                        }
                    }
                }

                // make sure no rows are empty
                if (row5.Count != 0)
                {
                    // check bounds
                    if (row4.Count == 0)
                    {
                        if (row1[0].getX() <= 5 || row2[0].getX() <= 5
                       || row3[0].getX() <= 5 || row5[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }

                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH
                            || row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH
                            || row3[row3.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH
                            || row5[row5.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else if (row3.Count == 0)
                    {
                        if (row1[0].getX() <= 5 || row2[0].getX() <= 5 || row5[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH
                            || row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH
                            || row5[row5.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else if (row2.Count == 0)
                    {
                        if (row1[0].getX() <= 5 || row5[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH
                            || row5[row5.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else if (row1.Count == 0)
                    {
                        if (row5[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row5[row5.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else
                    {
                        if (row1[0].getX() <= 5 || row2[0].getX() <= 5 || row3[0].getX() <= 5
                            || row4[0].getX() <= 5 || row5[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                            || row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10 
                            || row3[row3.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                            || row4[row4.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                            || row5[row5.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                }
                else if (row4.Count != 0)
                {
                    // check bounds
                    if (row1.Count == 0)
                    {
                        if (row2[0].getX() <= 5 || row3[0].getX() <= 5
                            || row4[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row3[row3.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row4[row4.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else if (row2.Count == 0)
                    {
                        if (row1[0].getX() <= 5 || row4[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }

                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row4[row4.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else if (row3.Count == 0)
                    {
                        if (row2[0].getX() <= 5 ||
                            row4[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row4[row4.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else
                    {
                        if (row1[0].getX() <= 5 || row2[0].getX() <= 5 || row3[0].getX() <= 5 ||
                            row4[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }

                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row3[row3.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row4[row4.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    // check bounds
                }
                else if (row3.Count != 0)
                {
                    // check bounds
                    if (row1.Count == 0)
                    {
                        if (row2[0].getX() <= 5 || row3[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row3[row3.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else if (row2.Count == 0)
                    {
                        if (row1[0].getX() <= 5 || row3[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }

                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row3[row3.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else if (row3.Count == 0)
                    {
                        if (row2[0].getX() <= 5 ||
                            row4[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row4[row4.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else
                    {
                        if (row1[0].getX() <= 5 || row2[0].getX() <= 5 || row3[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }

                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                        || row3[row3.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                }
                else if (row2.Count != 0)
                {
                    // check bounds
                    if (row1.Count == 0)
                    {
                        if (row2[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }


                        if (row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                    else
                    {
                        // check bounds
                        if (row1[0].getX() <= 5 || row2[0].getX() <= 5)
                        {
                            // change direction to right
                            alienDirection = Direction.RIGHT;

                            // move down
                            alienMovedown = true;
                        }

                        if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10
                            || row2[row2.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                        {
                            // change direction to left
                            alienDirection = Direction.LEFT;

                            // move down
                            alienMovedown = true;
                        }
                    }
                }
                else if (row1.Count != 0)
                {
                    // check bounds
                    if (row1[0].getX() <= 5)
                    {
                        // change direction to right
                        alienDirection = Direction.RIGHT;

                        // move down
                        alienMovedown = true;
                    }

                    if (row1[row1.Count() - 1].getX() >= ScreenControl.controlWidth - ALIEN_WIDTH - 10)
                    {
                        // change direction to left
                        alienDirection = Direction.LEFT;

                        // move down
                        alienMovedown = true;
                    }
                }
                // move all aliens down
                MoveAliensDown();
            }

            //alienAnimationCounter += gameTimer.Interval;
            if (elapsed == 16 && alienAnimationCounter == 0)
            {
                alien1 = new Bitmap(Properties.Resources.alien10Big);
                alien2 = new Bitmap(Properties.Resources.alien20Big);
                alien3 = new Bitmap(Properties.Resources.alien40Big);

                alienAnimationCounter++;
            }
            else if (elapsed == 16 && alienAnimationCounter == 1)
            {
                alien1 = new Bitmap(Properties.Resources.alien10altBig);
                alien2 = new Bitmap(Properties.Resources.alien20altBig);
                alien3 = new Bitmap(Properties.Resources.alien40altBig);

                alienAnimationCounter = 0;
            }
            #endregion

            #region Monster Shooting

            if (row5.Count == 1 || (row4.Count == 1 && row5.Count == 0) ||
                (row3.Count == 1 && row4.Count == 0) || (row2.Count == 1 && row3.Count == 0)
                || (row1.Count == 1 && row2.Count == 0))
            {
                randomShootTime = randGen.Next(ALIEN_SHOOT_TIME * 2, 1000 * 2);
            }
            else
            {
                randomShootTime = randGen.Next(ALIEN_SHOOT_TIME, MAX_ALIEN_SHOOT_TIME + 1);
            }

            timeSinceLastShot += gameTimer.Interval;

            switch (alienBullets.Count)
            {
                case 1:
                    randomShootTime /= 2;
                    break;

                case 2:
                    randomShootTime /= 3;
                    break;

            }
            if (timeSinceLastShot >= randomShootTime)
            {
                timeSinceLastShot = 0;

                // check to see if the row isn't empty
                // and generate an alien to shoot from
                if (row5.Count != 0 && alienBullets.Count() < MAX_ALIEN_BULLETS)
                {
                    int range = row5.Count;
                    int randAlien = randGen.Next(0, range);

                    int x = row5[randAlien].getX() + ALIEN_WIDTH / 2;
                    int y = row5[randAlien].getY() + ALIEN_HEIGHT;

                    Bullet tempBullet = new Bullet(x, y, BULLET_WIDTH, BULLET_HEIGHT, 0);

                    alienBullets.Add(tempBullet);
                }
                if (row4.Count != 0 && alienBullets.Count() < MAX_ALIEN_BULLETS && row5.Count <= 5)
                {
                    int range = row4.Count;
                    int randAlien = randGen.Next(0, range);

                    int x = row4[randAlien].getX() + ALIEN_WIDTH / 2;
                    int y = row4[randAlien].getY() + ALIEN_HEIGHT;

                    Bullet tempBullet = new Bullet(x, y, BULLET_WIDTH, BULLET_HEIGHT, 0);

                    alienBullets.Add(tempBullet);
                }
                if (row3.Count != 0 && alienBullets.Count() < MAX_ALIEN_BULLETS && row4.Count <= 5)
                {
                    int range = row3.Count;
                    int randAlien = randGen.Next(0, range);

                    int x = row3[randAlien].getX() + ALIEN_WIDTH / 2;
                    int y = row3[randAlien].getY() + ALIEN_HEIGHT;

                    Bullet tempBullet = new Bullet(x, y, BULLET_WIDTH, BULLET_HEIGHT, 0);

                    alienBullets.Add(tempBullet);
                }
                if (row2.Count != 0 && alienBullets.Count() < MAX_ALIEN_BULLETS && row3.Count <= 5)
                {
                    int range = row2.Count;
                    int randAlien = randGen.Next(0, range);

                    int x = row2[randAlien].getX() + ALIEN_WIDTH / 2;
                    int y = row2[randAlien].getY() + ALIEN_HEIGHT;

                    Bullet tempBullet = new Bullet(x, y, BULLET_WIDTH, BULLET_HEIGHT, 0);

                    alienBullets.Add(tempBullet);             
                }
                if (row1.Count != 0 && alienBullets.Count() < MAX_ALIEN_BULLETS && row2.Count <= 5)
                {
                    int range = row1.Count;
                    int randAlien = randGen.Next(0, range);

                    int x = row1[randAlien].getX() + ALIEN_WIDTH / 2;
                    int y = row1[randAlien].getY() + ALIEN_HEIGHT;

                    Bullet tempBullet = new Bullet(x, y, BULLET_WIDTH, BULLET_HEIGHT, 0);

                    alienBullets.Add(tempBullet);
                }

                alienBullet.Stop();
                alienBullet.Play();
            }

            // go through all the enemy bullets
            // and move them 
            for (int i = 0; i < alienBullets.Count(); i++)
            {
                alienBullets[i].move(0, BULLET_SPEED);

                // remove if they reach the bottom
                if (alienBullets[i].getY() >= ScreenControl.controlHeight)
                {
                    alienBullets.Remove(alienBullets[i]);

                    break;
                }

                // check to see if an alien bullet hits with the player's
                // bullet
                if (alienBullets[i].collides(playerBullet))
                {
                    // get rid of player bullet
                    bulletOnScreen = false;
                }
            }

            #endregion

            #region collision detection 

            // only check collision if bullet is on screen
            if (bulletOnScreen)
            {
                #region Barrier Collision
                if (playerBullet.getRect().IntersectsWith(barrier1Rect) && barrier1health != 0)
                {
                    bulletOnScreen = false;
                    barrier1health--;
                }
                if (playerBullet.getRect().IntersectsWith(barrier2Rect) && barrier2health != 0)
                {
                    bulletOnScreen = false;
                    barrier2health--;
                }
                if (playerBullet.getRect().IntersectsWith(barrier3Rect) && barrier3health != 0)
                {
                    bulletOnScreen = false;
                    barrier3health--;
                }
                if (playerBullet.getRect().IntersectsWith(barrier4Rect) && barrier4health != 0)
                {
                    bulletOnScreen = false;
                    barrier4health--;
                }
                #endregion

                // alien collision
                foreach (Alien alien in row1)
                {
                    if (alien.collision(playerBullet))
                    {
                        // play explosion
                        alienHit.Play();

                        alienKilled = true;

                        destroyedAlienRect = alien.getRect();

                        row1.Remove(alien);

                        score += ALIEN3_SCORE;

                        // get rid of bullet
                        bulletOnScreen = false;
                        break;
                    }
                }

                foreach (Alien alien in row2)
                {
                    if (alien.collision(playerBullet))
                    {
                        // play explosion
                        alienHit.Play();

                        alienKilled = true;

                        destroyedAlienRect = alien.getRect();

                        row2.Remove(alien);

                        score += ALIEN2_SCORE;

                        // get rid of bullet
                        bulletOnScreen = false;
                        break;
                    }
                }

                foreach (Alien alien in row3)
                {
                    if (alien.collision(playerBullet))
                    {
                        // play explosion
                        alienHit.Play();

                        alienKilled = true;

                        destroyedAlienRect = alien.getRect();

                        row3.Remove(alien);

                        score += ALIEN2_SCORE;

                        // get rid of bullet
                        bulletOnScreen = false;
                        break;
                    }
                }

                foreach (Alien alien in row4)
                {
                    if (alien.collision(playerBullet))
                    {
                        // play explosion
                        alienHit.Play();

                        destroyedAlienRect = alien.getRect();

                        alienKilled = true;

                        row4.Remove(alien);

                        score += ALIEN1_SCORE;

                        // get rid of bullet
                        bulletOnScreen = false;
                        break;
                    }
                }

                foreach (Alien alien in row5)
                {
                    if (alien.collision(playerBullet))
                    {
                        // play explosion
                        alienHit.Play();

                        destroyedAlienRect = alien.getRect();

                        alienKilled = true;

                        // get rid of bullet
                        bulletOnScreen = false;
                        
                        score += ALIEN1_SCORE;

                        row5.Remove(alien);
                        break;
                    }
                }

                if (ufoOnScreen)
                {
                    if (playerBullet.getRect().IntersectsWith(ufoRect))
                    {
                        // stop sound
                        ufoSound.Stop();

                        ufoHit.Play();

                        // generate a random score
                        int scoreMultiplier = randGen.Next(1, 4);

                        score += UFO_SCORE * scoreMultiplier;

                        // get rid of ufo and player bullet
                        ufoOnScreen = false;
                        bulletOnScreen = false;
                    }
                }
            }

            // check to see if there are any
            // alien bullets on the screen
            if (alienBullets.Count() >= 1)
            {
                // see if any of them intersect with the barriers
                for (int i = 0; i < alienBullets.Count(); i++)
                {
                    if (alienBullets[i].getRect().IntersectsWith(barrier1Rect) && barrier1health >= 1)
                    {
                        barrier1health--;
                        alienBullets.Remove(alienBullets[i]);
                        break;
                    }
                    if (alienBullets[i].getRect().IntersectsWith(barrier2Rect) && barrier2health >= 1)
                    {
                        barrier2health--;
                        alienBullets.Remove(alienBullets[i]);
                        break;
                    }
                    if (alienBullets[i].getRect().IntersectsWith(barrier3Rect) && barrier3health >= 1)
                    {
                        barrier3health--;
                        alienBullets.Remove(alienBullets[i]);
                        break;
                    }
                    if (alienBullets[i].getRect().IntersectsWith(barrier4Rect) && barrier4health >= 1)
                    {
                        barrier4health--;
                        alienBullets.Remove(alienBullets[i]);
                        break;
                    }

                    // check to see if they collide with the player
                    if (alienBullets[i].getRect().IntersectsWith(player.getRect()))
                    {
                        if (ufoOnScreen)
                        {
                            ufoOnScreen = false;
                            ufoSound.Stop();
                        }

                        if (lives == 0)
                        {
                            // show game over screen
                            GameOver();
                        }
                        else
                        {
                            lives--;
                            alienBullets.Remove(alienBullets[i]);
                            playerHit = true;

                            playerHitSound.Play();
                        }

                        break;
                    }
                
                    }
            }

            #endregion

            #region barrier logic
            if (barrier1health == 48)
            {
                barrier1 = new Bitmap(Properties.Resources.coverDmg1Big);
            }
            else if (barrier1health == 36)
            {
                barrier1 = new Bitmap(Properties.Resources.coverDmg2Big);
            }
            else if (barrier1health == 24)
            {
                barrier1 = new Bitmap(Properties.Resources.coverDmg3Big);
            }
            else if (barrier1health == 12)
            {
                barrier1 = new Bitmap(Properties.Resources.coverDmg4Big);
            }

            if (barrier2health == 48)
            {
                barrier2 = new Bitmap(Properties.Resources.coverDmg1Big);
            }
            else if (barrier2health == 36)
            {
                barrier2 = new Bitmap(Properties.Resources.coverDmg2Big);
            }
            else if (barrier2health == 24)
            {
                barrier2 = new Bitmap(Properties.Resources.coverDmg3Big);
            }
            else if (barrier2health == 12)
            {
                barrier2 = new Bitmap(Properties.Resources.coverDmg4Big);
            }

            if (barrier3health == 48)
            {
                barrier3 = new Bitmap(Properties.Resources.coverDmg1Big);
            }
            else if (barrier3health == 36)
            {
                barrier3 = new Bitmap(Properties.Resources.coverDmg2Big);
            }
            else if (barrier3health == 24)
            {
                barrier3 = new Bitmap(Properties.Resources.coverDmg3Big);
            }
            else if (barrier3health == 12)
            {
                barrier3 = new Bitmap(Properties.Resources.coverDmg4Big);
            }

            if (barrier4health == 48)
            {
                barrier4 = new Bitmap(Properties.Resources.coverDmg1Big);
            }
            else if (barrier4health == 36)
            {
                barrier4 = new Bitmap(Properties.Resources.coverDmg2Big);
            }
            else if (barrier4health == 24)
            {
                barrier4 = new Bitmap(Properties.Resources.coverDmg3Big);
            }
            else if (barrier4health == 12)
            {
                barrier4 = new Bitmap(Properties.Resources.coverDmg4Big);
            }
            #endregion

            #region UFO logic

            if (!ufoOnScreen)
            {
                ufoSound.Stop();
            }
            int ufoTime = randGen.Next(UFO_WAIT_TIME, UFO_WAIT_TIME * 2);
            ufoCounter++;

            if (ufoCounter >= ufoTime)
            {
                ufoCounter = 0;
                int randomNum = randGen.Next(1, 101);

                // 50% for UFO
                if (randomNum > 50 && !ufoOnScreen)
                {
                    ufoOnScreen = true;

                    ufoSound.PlayLooping();

                    ufoRect.X = ScreenControl.controlWidth - UFO_WIDTH;
                    ufoRect.Y = UFO_HEIGHT;
                }
            }

            if (ufoOnScreen)
            {
                // figure out how to loop sound

                ufoRect.X -= UFO_SPEED;
            }

            if (ufoRect.X <= -UFO_WIDTH)
            {
                ufoOnScreen = false;

                // stop ufo sound
                ufoSound.Stop();
            }
            #endregion

            // reset game when all aliens are gone
            if (row1.Count == 0 &&
                row2.Count == 0 &&
                row3.Count == 0 &&
                row4.Count == 0 &&
                row5.Count == 0)
            {

                resetGame();
            }

            //refresh the screen, which causes the GameScreen_Paint method to run
            Refresh();
        }

        public void resetGame()
        {
            // add a life
            lives++;

            // increase level
            levelCounter++;

            // get rid of ufo
            ufoOnScreen = false;

            for (int i = 0; i < row1.Capacity; i++)
            {
                Alien temp1 = new Alien(100 + (45 * i), 100, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp2 = new Alien(100 + (45 * i), 150, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp3 = new Alien(100 + (45 * i), 200, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp4 = new Alien(100 + (45 * i), 250, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);
                Alien temp5 = new Alien(100 + (45 * i), 300, ALIEN_WIDTH, ALIEN_HEIGHT, 0, 0);

                row1.Add(temp1);
                row2.Add(temp2);
                row3.Add(temp3);
                row4.Add(temp4);
                row5.Add(temp5);
            }

            // get rid of all alien bullets
            alienBullets.Clear();

            Thread.Sleep(2500);
        }

        /// <summary>
        /// Open the pause dialog box and gets Cancel or Abort result from it
        /// </summary>
        private void pauseGame()
        {
            if (ufoOnScreen)
            {
                ufoSound.Stop();
            }

            gameTimer.Enabled = false;
            rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = false;

            DialogResult result = PauseDialog.Show();

            if (result == DialogResult.Cancel)
            {
                if (ufoOnScreen)
                {
                    ufoSound.Stop();
                }

                gameTimer.Enabled = true;
            }
            if (result == DialogResult.Abort)
            {
                ufoOnScreen = false;

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
            e.Graphics.Clear(Color.Black);

            //draw rectangle to screen
            if (!playerHit)
            {
                e.Graphics.DrawImage(playerImage, player.getRect());
            }
            else
            {
                e.Graphics.DrawImage(playerExplosion, player.getRect());
            }

            e.Graphics.DrawString("Score: " + score, subFont,
                solidBrush, 25, 25);

            e.Graphics.DrawString("Lives: " + lives, subFont,
                solidBrush, Width - 160, 25);

            #region Barrier Drawing
            if (barrier1health != 0)
            {
                e.Graphics.DrawImage(barrier1, barrier1Rect.X, barrier1Rect.Y,
                    barrier1Rect.Width, barrier1Rect.Height);
            }

            if (barrier2health != 0)
            {
                e.Graphics.DrawImage(barrier2, barrier2Rect.X,
                    barrier2Rect.Y, barrier2Rect.Width,
                    barrier2Rect.Height);
            }
            if (barrier3health != 0)
            {
                e.Graphics.DrawImage(barrier3, barrier3Rect.X,
                    barrier3Rect.Y, barrier3Rect.Width,
                    barrier3Rect.Height);
            }

            if (barrier4health != 0)
            {
                e.Graphics.DrawImage(barrier4, barrier4Rect.X,
                    barrier4Rect.Y, barrier4Rect.Width,
                    barrier4Rect.Height);
            }
            #endregion

            if (bulletOnScreen)
            {
                e.Graphics.DrawImage(bullet, playerBullet.getRect());
            }

            for (int i = 0; i < alienBullets.Count(); i++)
            {
                e.Graphics.DrawImage(bullet, alienBullets[i].getRect());
            }


            if (alienKilled)
            {
                // draw explosion
                e.Graphics.DrawImage(alienExplosion, destroyedAlienRect);
            }

            foreach (Alien alien in row1)
            {
                e.Graphics.DrawImage(alien3, alien.getRect());
            }
            foreach (Alien alien in row2)
            {
                e.Graphics.DrawImage(alien2, alien.getRect());
            }
            foreach (Alien alien in row3)
            {
                e.Graphics.DrawImage(alien2, alien.getRect());
            }
            foreach (Alien alien in row4)
            {
                e.Graphics.DrawImage(alien1, alien.getRect());
            }
            foreach (Alien alien in row5)
            {
                e.Graphics.DrawImage(alien1, alien.getRect());
            }

            if (ufoOnScreen)
            {
                e.Graphics.DrawImage(ufo, ufoRect);
            }
        }
    }
}