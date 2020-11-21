using System;
using SmartDose.Helpers.Resources;
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
            // Default values
            WeightEntry.Text = String.Empty;
            WantedApttrEntry.Text = String.Empty;
            NumberOfUnitsEntry.Text = String.Empty;
            CurrentApttrEntry.Text = String.Empty;
            CurrentSpeedEntry.Text = String.Empty;
            Volume.Text = String.Empty;
            // Placeholder localization 
            SwitchLabel.Text = AppResource.FirstCalculation;
            WeightEntry.Placeholder = AppResource.Weight;
            WantedApttrEntry.Placeholder = AppResource.WantedAPPTr;
            NumberOfUnitsEntry.Placeholder = AppResource.NumberOfUnits;
            CurrentApttrEntry.Placeholder = AppResource.CurrentAPTTr;
            CurrentSpeedEntry.Placeholder = AppResource.CurrentSpeed;
            Volume.Placeholder = AppResource.Volume;
            CalculateButton.Text = AppResource.Calculate;
            
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