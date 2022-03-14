using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TennisBall
{
    public class MyAnimation
    {
        //
        public static async Task BallAnimate(View view, double x, double y)
        {
            Console.WriteLine($"[VIEW]: X: {view.TranslationX}, Y:{view.TranslationY}");
            Console.WriteLine($"[VIEW]: X: {x}, Y:{y}");
            await view.TranslateTo(
                AccelerometerTest.ConvertRawCordinatesToPositions(
                    AccelerometerTest.ConvertAcceloToRawCoordinate(x,  MainPage.Width)),
                AccelerometerTest.ConvertRawCordinatesToPositions(
                    AccelerometerTest.ConvertAcceloToRawCoordinate(y, MainPage.Height)),
                200, Easing.Linear);
        }
    }
}
