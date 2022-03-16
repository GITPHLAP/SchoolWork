using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TennisBall
{
    public partial class MainPage : ContentPage
    {
        public static double _Width;
        public static double _Height;

        //TODO: change image 
        //TODO: refactor some classes
        //TODO: Clean up

        public MainPage()
        {
            InitializeComponent();
            AccelerometerTest accelerometer = new AccelerometerTest(ball);
            accelerometer.ToggleAccelerometer();


            //width = maindisplayinfo.Width;
            //height = maindisplayinfo.Height;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //await MyAnimation.BallAnimate(ellipse, 100, 100);
            var maindisplayinfo = DeviceDisplay.MainDisplayInfo;
            //MyAnimation.BallAnimate(ellipse, 0.9, 0);

            //MyAnimation.BallAnimate(ellipse, -0.9, 0);


            // await logoImage.TranslateTo(logoImage.TranslationX, logoImage.TranslationY , 500, Easing.Linear);
            //await Animate.BallAnimate(this.logoImage, 50, 10, 5);
            // await logoImage.TranslateTo(logoImage.TranslationX, logoImage.TranslationY , 500, Easing.Linear);
        }


    }
}
