using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameTemplate
{
    public class Alien
    {
        Rectangle alienRectangle;
        int xSpeed, ySpeed;

        /// <summary>
        /// Constructor for the alien class
        /// that builds the rectangle and the x and y speeds
        /// Also needs a score for the alien
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        /// <param name="_xSpeed"></param>
        /// <param name="_ySpeed"></param>
        public Alien(int _x, int _y, int _width, int _height, int _xSpeed, int _ySpeed)
        {
            alienRectangle.X = _x;
            alienRectangle.Y = _y;
            alienRectangle.Width = _width;
            alienRectangle.Height = _height;

            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        /// <summary>
        /// Returns the rectangle of the alien
        /// </summary>
        /// <returns></returns>
        public Rectangle getRect() { return alienRectangle; }

        /// <summary>
        /// Returns x position
        /// </summary>
        /// <returns></returns>
        public int getX()
        {
            return alienRectangle.X;
        }

        /// <summary>
        /// Returns y position
        /// </summary>
        /// <returns></returns>
        public int getY()
        {
            return alienRectangle.Y;
        }

        /// <summary>
        /// Sets the x and y position of the alien
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public void setPosition(int _x, int _y)
        {
            alienRectangle.X = _x;
            alienRectangle.Y = _y;
        }

        /// <summary>
        /// Moves the alien with the parameter speeds
        /// </summary>
        /// <param name="_xSpeed"></param>
        /// <param name="_ySpeed"></param>
        public void move(int _xSpeed, int _ySpeed)
        {
            alienRectangle.X += _xSpeed;
            alienRectangle.Y += _ySpeed;
        }

        /// <summary>
        /// Moves the alien with the stored speeds given or set
        /// </summary>
        public void move()
        {
            alienRectangle.X += xSpeed;
            alienRectangle.Y += ySpeed;
        }

        /// <summary>
        /// Checks collision with a bullet
        /// </summary>
        /// <param name="bullet"></param>
        public bool collision(Bullet bullet)
        {
            // return true if the alien rectangle collides with the bullet
            if (alienRectangle.IntersectsWith(bullet.getRect()))
            {
                return true;
            }

            return false;
        }
    }
}
