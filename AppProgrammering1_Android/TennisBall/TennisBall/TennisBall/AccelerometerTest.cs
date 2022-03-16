using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TennisBall
{
    public class AccelerometerTest
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        View view;
        private double lastX;
        private double lastY;

        public AccelerometerTest(View _view)
        {
            // Register for reading changes
            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;

            view = _view;
        }

        private async void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            //Task.Run(async () =>
            //{
                var data = e.Reading;
                await MyAnimation.BallRotationAnimation(view, data.AngularVelocity.Y);
            //});

        }

        async void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            //Task.Run(async () =>
            //{
                var data = e.Reading;
                double newX = data.Acceleration.X;
                double newY = data.Acceleration.Y;
                if (newX != lastX || newY != lastY)
                {
                    await MyAnimation.BallAnimate(view, -1 * newX, newY);
                    //Console.WriteLine($"Reading: X: {newX}, Y: {newY}, Z: {data.Acceleration.Z}");

                    lastX = newX;
                    lastY = newY;
                }
            //});
            
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);

                if (Gyroscope.IsMonitoring)
                    Gyroscope.Stop();
                else
                    Gyroscope.Start(speed);
            }
            catch
            {
                throw;
            }
        }

        #region Accelerometer value converter to absolutelayout 

        //Map accelerometer value to 

        /// <summary>
        /// Convert the accelerometer value to raw coordinate offset from center 
        /// </summary>
        /// <param name="input">accelerometer value</param>
        /// <param name="screenOrintationLength"> the size of the screen  ex. if input x cordinate its the width of screen</param>
        /// <returns></returns>
        public static double ConvertAcceloToRawCoordinate(double input, double screenOrintationLength)
        {
            //calculate the range from center in screen 
            double outputRange_start = -screenOrintationLength / 2;
            double outputRange_end = screenOrintationLength / 2;

            double inputRange_start = -0.9999989;
            double inputRange_end = 0.9999989;

            return ((input - inputRange_start) * ((outputRange_end - outputRange_start) / (inputRange_end - inputRange_start))) + outputRange_start;
        }

        public static double ConvertRawCordinatesToPositions(double rawCoordinate)
        {
            return rawCoordinate / DeviceDisplay.MainDisplayInfo.Density;
        }

        #endregion
    }
}
