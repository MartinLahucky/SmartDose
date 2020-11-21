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
            if (WeightEntry.Text == String.Empty || WantedApttrEntry.Text == String.Empty || NumberOfUnitsEntry.Text == String.Empty || Volume.Text == String.Empty || (CurrentApttrEntry.IsVisible && CurrentApttrEntry.Text == String.Empty) || (CurrentSpeedEntry.IsVisible && CurrentSpeedEntry.Text == String.Empty))                                
            {
                CalculateButton.Text = "Please Enter All Values";
            }
            else
            {
                CalculateButton.Text = "Calculated";
            }
        }

        private void FirstCalculationSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            CurrentApttrEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentSpeedEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
        }
    }
}