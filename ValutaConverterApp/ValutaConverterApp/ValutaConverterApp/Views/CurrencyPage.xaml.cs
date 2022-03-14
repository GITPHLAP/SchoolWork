using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValutaConverterApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValutaConverterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyPage : ContentPage
    {
        CurrencyViewModel _viewModel;
        public CurrencyPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CurrencyViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            CurrencyListView.ItemsSource = _viewModel.GetCurrenciesSearchResult(searchBar.Text);
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBar.Text)) // when search bar is cleared...
            {
                //When isbusy its true then it will refresh refreshView
                _viewModel.IsBusy = true;
            }
        }
    }
}