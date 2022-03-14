using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ValutaConverterApp.Models;
using ValutaConverterApp.Services;
using Xamarin.Forms;

namespace ValutaConverterApp.ViewModels
{
    public class CurrencyViewModel : BaseViewModel
    {
        //TODO: order currencies
        //TODO: OPtimize search 
        //TODO: refresh with new data when clear search bar
        private RestService _restService;

        private List<Currency> _currencies;
        public ObservableCollection<Currency> Currencies { get; }

        public ObservableCollection<Currency> CurrenciesSearchResult { get; }

        public Command LoadCurrenciesCommand { get; }

        public CurrencyViewModel()
        {
            _restService = new RestService();
            Title = "Currencies";
            Currencies = new ObservableCollection<Currency>();

            LoadCurrenciesCommand = new Command<string>(async (string query) => await ExecuteLoadCurrenciesCommand(query));
        }


        async Task ExecuteLoadCurrenciesCommand(string query)
        {
            IsBusy = true;

            try
            {

                Currencies.Clear();
                _currencies = query != "" && query is string
                    ? await _restService.GetSearchedCurrencies(query)
                    : await _restService.GetCurrencies();

                foreach (var item in _currencies.OrderBy(c => c.Id))
                {
                    Currencies.Add(item);
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public List<Currency> GetCurrenciesSearchResult(string queryString)
        {
            return Currencies.Where(c => c.CurrencyName.ToLowerInvariant().Contains(queryString)).ToList();
        }

        public ICommand PerfromSearch => new Command(() =>
        {
            IsBusy = true;
            //IsBusy = false;

        });
    }
}