using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameTemplate
{
    public class Bullet
    {
        Rectangle bulletRect;

        // the bullet will only have a y speed
        int ySpeed;

        /// <summary>
        /// Constructor for the bullet
        /// Creates the rectangle and only needs a y speed
        /// If isPlayerBullet is true, then it is the a player's bullet.
        /// Thus, it will be destroyed when it collides with an alien bullet.
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        /// <param name="_ySpeed"></param>
        public Bullet(int _x, int _y, int _width, int _height, int _ySpeed)
        {
            bulletRect.X = _x;
            bulletRect.Y = _y;
            bulletRect.Width = _width;
            bulletRect.Height = _height;
            ySpeed = _ySpeed;
        }

        /// <summary>
        /// Returns the rectangle of the bullet
        /// </summary>
        /// <returns></returns>
        public Rectangle getRect() { return bulletRect; }

        /// <summary>
        /// Moves the bullet with the parameter speeds
        /// </summary>
        /// <param name="_xSpeed"></param>
        /// <param name="_ySpeed"></param>
        public void setPosition(int _x, int _y)
        {
            bulletRect.X = _x;
            bulletRect.Y = _y;
        }

        /// <summary>
        /// Returns x position
        /// </summary>
        /// <returns></returns>
        public int getX()
        {
            return bulletRect.X;
        }

        /// <summary>
        /// Returns y position
        /// </summary>
        /// <returns></returns>
        public int getY()
        {
            return bulletRect.Y;
        }

        /// <summary>
        /// Moves the bullet with the given y speed
        /// </summary>
        public void move()
        {
            bulletRect.Y += ySpeed;
        }

        /// <summary>
        /// Moves the bullet with the parameter speeds
        /// </summary>
        /// <param name="_xSpeed"></param>
        /// <param name="_ySpeed"></param>
        public void move(int _xSpeed, int _ySpeed)
        {
            bulletRect.X += _xSpeed;
            bulletRect.Y += _ySpeed;
        }

        /// <summary>
        /// Checks collision with a bullet
        /// Returns true if it has collided
        /// </summary>
        /// <param name="bullet"></param>
        /// <returns></returns>
        public bool collides(Bullet bullet)
        {
            if (bulletRect.IntersectsWith(bullet.getRect()))
            {
                return true;
            }

            return false;
        }
    }
}
