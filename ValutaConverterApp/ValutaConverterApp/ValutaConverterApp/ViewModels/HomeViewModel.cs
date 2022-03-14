using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ValutaConverterApp.Models;
using ValutaConverterApp.Services;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ValutaConverterApp.ViewModels
{

    class HomeViewModel : BaseViewModel
    {
        private RestService _restService;
        private ExchangeRate exchangeRate;
        private string fromValuta;
        private string toValuta;
        private double fromAmount;
        private double toAmount;

        private double result;
        public HomeViewModel()
        {
            Title = "Home";
            _restService = new RestService();
        }


        public string FromValuta
        {
            get => fromValuta;
            set => SetProperty(ref fromValuta, value);
        }

        public string ToValuta
        {
            get => toValuta;
            set => SetProperty(ref toValuta, value);
        }

        public double FromAmount
        {
            get => fromAmount;
            set => SetProperty(ref fromAmount, value);
        }

        public double ToAmount
        {
            get => toAmount;
            set => SetProperty(ref toAmount, value);
        }

        public double Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        public void GetRate(string from, string to, bool reverseCalculation = false)
        {
            Task.Run(async () =>
            {
                exchangeRate = await _restService.GetRate(from, to);

                if (!reverseCalculation)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ToAmount = CalculateResult(fromAmount);
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        FromAmount = CalculateResult(toAmount);
                    });
                }
                
            });

        }

        public double CalculateResult(double amount)
        {
            //Check that all properties is not empty
            if (amount != 0 &&
                exchangeRate != null &&
                exchangeRate.Rate != 0)
            {
                return amount * exchangeRate.Rate;
            }
            return 0;
        }

    }
}
