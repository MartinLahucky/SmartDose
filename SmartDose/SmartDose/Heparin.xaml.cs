using System;
using SmartDose.Helpers.Database_Models;
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
            CurrentRateEntry.Text = String.Empty;
            VolumeEntry.Text = String.Empty;
            
            // Placeholder localization 
            SwitchLabel.Text = AppResource.FirstCalculation;
            WeightEntry.Placeholder = AppResource.Weight;
            WantedApttrEntry.Placeholder = AppResource.WantedAPPTr;
            NumberOfUnitsEntry.Placeholder = $"{2500}";
            CurrentApttrEntry.Placeholder = AppResource.CurrentAPTTr;
            CurrentRateEntry.Placeholder = AppResource.CurrentSpeed;
            VolumeEntry.Placeholder = $"{50}";
            CalculateButton.Text = AppResource.Calculate;
            
        }

        private void CalculateButton_OnClicked(object sender, EventArgs e)
        {
            float weight, currentATTR, currentRate;           // Weight, Current APTTR, Current Rate,
            int numberOfUnits, volume;                        // Number of Units, Volume
            
            try
            {
                currentRate = int.Parse(CurrentRateEntry.Text);
                weight = float.Parse(WeightEntry.Text);
                currentATTR = float.Parse(CurrentApttrEntry.Text);
                try
                {
                    numberOfUnits = int.Parse(NumberOfUnitsEntry.Text);
                    volume = int.Parse(VolumeEntry.Text);
                    Calculate(currentRate, weight, currentATTR, numberOfUnits, volume);
                }
                catch
                {
                    Calculate(currentRate, weight, currentATTR);   
                }
            }
            catch
            {
                DependencyService.Get<INativeFun>().ShortAlert(AppResource.NumberAlert);
            }
        }

        private async void Calculate(float currentRate,float weight = 0, float currentATTR = 0, int numberOfUnits = 2500, int volume = 50)
        {
            switch (FirstCalculationSwitch.IsToggled)
            {
                // For the first calculation:
                case true:
                {
                    const int konst = 18;                                                          // Constant needed for evaluating the first rate
                    RateValueLabel.Text = $"{volume * weight * konst / numberOfUnits}";
                    BolusValueLabel.Text = "0";
                    break;
                }
                // Fot every next calculation:
                case false:
                {
                    int id = 0, bolus = 0;                                                          // Local data holders
                    HeparinTableAPTT[] hapArray = await App.Database.GetHeparrinConstants();        // Array of haparin constants
                    int[] dRmulti = {4, 2, 0, -2, 0};                                               // Array of multiplier of delta rate
                    
                    if (currentATTR > hapArray[hapArray.Length - 1].Aptt)
                    {
                        id = 4;
                        bolus = hapArray[hapArray.Length - 1].Bolus;
                    }
                    else
                    {
                        for (int i = 0; i < hapArray.Length - 1; i++)
                        {
                            if (currentATTR > hapArray[i].Aptt && currentATTR <= hapArray[i + 1].Aptt)
                            {
                                id = i;
                                bolus = hapArray[i].Bolus;
                            }
                        }
                    }
                    RateValueLabel.Text = $"{Math.Round(-currentRate * Math.Sign(id - 4) + volume * weight * dRmulti[id] / numberOfUnits)}";
                    BolusValueLabel.Text = $"{Math.Round(weight * bolus / 500) * 500}"; 
                    break;
                }
            }
        }

        private void FirstCalculationSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            CurrentApttrEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentRateEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
            BolusValueLabel.IsVisible = !FirstCalculationSwitch.IsToggled;
        }
    }
}