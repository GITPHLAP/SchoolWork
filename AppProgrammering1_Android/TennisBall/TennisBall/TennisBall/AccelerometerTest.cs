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

        public AccelerometerTest(View _view)
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            view = _view;
        }

        async void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            await MyAnimation.BallAnimate(view, -1*data.Acceleration.X, data.Acceleration.Y);
            Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
            // Process Acceleration X, Y, and Z
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        #region Accelerometer value converter to absolutelayout 

        double accelerometerMaxValue = 0.9999989;


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
