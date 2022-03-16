using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TennisBall
{
    public class MyAnimation
    {
        /// <summary>
        /// Move the tennisBall according the mobiles motion
        /// 
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static async Task BallAnimate(View view, double x, double y)
        {


            //Console.WriteLine($"[VIEW]: X: {view.TranslationX}, Y:{view.TranslationY}");
            //Console.WriteLine($"[VIEW]: X: {x}, Y:{y}");
            await view.TranslateTo(
                AccelerometerTest.ConvertRawCordinatesToPositions(
                    AccelerometerTest.ConvertAcceloToRawCoordinate(x,  MainPage._Width)),
                AccelerometerTest.ConvertRawCordinatesToPositions(
                    AccelerometerTest.ConvertAcceloToRawCoordinate(y, MainPage._Height)),
                250, Easing.Linear);
        }

        /// <summary>
        /// Rotate the tennisBall according the mobiles gyroscope
        /// 
        /// gyroY is in radian
        /// </summary>
        /// <param name="view"></param>
        /// <param name="gyroY"> used to determ angel for X-axis (used to rotation)</param>
        /// <returns></returns>
        public static async Task BallRotationAnimation(View view, double gyroY)
        {
            await view.RelRotateTo(gyroY * (180 / Math.PI), 100, Easing.Linear); //calculate gyro to degrees 

        }

    }

}
