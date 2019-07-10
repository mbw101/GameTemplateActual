using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameTemplate
{
    public class Player
    {
        Rectangle playerRectangle;
        int xSpeed, ySpeed;

        /// <summary>
        /// Player constructor that saves the rectangle
        /// and the x and y speeds
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        /// <param name="_xSpeed"></param>
        /// <param name="_ySpeed"></param>
        public Player(int _x, int _y, int _width, int _height, int _xSpeed, int _ySpeed)
        {
            playerRectangle.X = _x;
            playerRectangle.Y = _y;
            playerRectangle.Width = _width;
            playerRectangle.Height = _height;

            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        /// <summary>
        /// Moves the player with the parameter speeds
        /// </summary>
        /// <param name="_xSpeed"></param>
        /// <param name="_ySpeed"></param>
        public void move(int _xSpeed, int _ySpeed)
        {
            playerRectangle.X += _xSpeed;
            playerRectangle.Y += _ySpeed;
        }

        /// <summary>
        /// Moves the player with the stored speeds given or set
        /// </summary>
        public void move()
        {
            playerRectangle.X += xSpeed;
            playerRectangle.Y += ySpeed;
        }

        /// <summary>
        /// Save the parameter speeds in the class
        /// </summary>
        /// <param name="_xSpeed"></param>
        /// <param name="_ySpeed"></param>
        public void setSpeed(int _xSpeed, int _ySpeed)
        {
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        /// <summary>
        /// Sets the x and y position of the player
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public void setPosition(int _x, int _y)
        {
            playerRectangle.X = _x;
            playerRectangle.Y = _y;
        }

        /// <summary>
        /// Returns x position
        /// </summary>
        /// <returns></returns>
        public int getX()
        {
            return playerRectangle.X;
        }

        /// <summary>
        /// Returns y position
        /// </summary>
        /// <returns></returns>
        public int getY()
        {
            return playerRectangle.Y;
        }

        /// <summary>
        /// Returns the player rectangle
        /// </summary>
        /// <returns></returns>
        public Rectangle getRect()
        {
            return playerRectangle;
        }


        public void collision(Form1 form)
        {

        }

        /// <summary>
        /// Checks collision with a bullet
        /// </summary>
        /// <param name="bullet"></param>
        public void collision(Bullet bullet)
        {
            // TODO: Complete this method
        }
    }
}
