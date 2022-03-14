using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValutaConverterApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValutaConverterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;
        public HomePage()
        {
            InitializeComponent();
            viewModel = (HomeViewModel)BindingContext;
        }

        private void ValutaEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (ToValuta_Entry.Text != null &&
                ToValuta_Entry.Text != "" && 
                FromValuta_Entry.Text != "" && 
                FromValuta_Entry.Text != null)
            {
                viewModel.GetRate(FromValuta_Entry.Text, ToValuta_Entry.Text);
            }
        }

        private void ToAmount_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            //TODO: switch currency and calculate rate again 
            viewModel.GetRate(ToValuta_Entry.Text, FromValuta_Entry.Text, true);
        }

        private void FromAmount_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel.GetRate(FromValuta_Entry.Text, ToValuta_Entry.Text);
        }
    }
}