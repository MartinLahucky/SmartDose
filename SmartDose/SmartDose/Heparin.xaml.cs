using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartDose
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Heparin : ContentPage
    {
        public Heparin()
        {
            InitializeComponent();
            WeightEntry.Text = String.Empty; 
            CurrentApttrEntry.Text = String.Empty;
            CurrentSpeedEntry.Text = String.Empty;
        }

        private async void CalculateButton_OnClicked(object sender, EventArgs e)
        {
            if (WeightEntry.Text == String.Empty || CurrentApttrEntry.Text == String.Empty || CurrentSpeedEntry.Text == String.Empty )
            {
                
            }
            else
            {

                
            }
        }
    }
}