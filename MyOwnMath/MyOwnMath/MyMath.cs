using System;

namespace MyOwnMath
{
    public class MyMath
    {

        /// <summary>
        /// Equal to Math.Sin 
        /// </summary>
        /// <param name="a"></param>
        /// <returns>return the numeric value instead of radian</returns>
        public static double SinN(double a)
        {
            return Math.Sin(a * Math.PI / 180);
        }

        /// <summary>
        /// Equal to Math.Cos
        /// </summary>
        /// <param name="a"></param>
        /// <returns>return the numeric value instead of radian</returns>
        public static double CosN(double a)
        {
            return Math.Cos(a * Math.PI / 180);
        }

        /// <summary>
        /// Equal to Math.ACos
        /// </summary>
        /// <param name="a"></param>
        /// <returns>return the numeric value instead of radian</returns>
        public static double AcosN(double a)
        {
            return Math.Acos(a) * 180 / Math.PI;
        }

        /// <summary>
        /// Calculate the distance between two points on earth
        /// </summary>
        /// <param name="longA"></param>
        /// <param name="latiA"></param>
        /// <param name="longB"></param>
        /// <param name="latiB"></param>
        /// <returns></returns>

    }
}
