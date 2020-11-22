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
            
            // Localization 
            SwitchLabel.Text = AppResource.FirstCalculation;
            WeightEntry.Placeholder = AppResource.Weight;
            CurrentApttrEntry.Placeholder = AppResource.CurrentAPTTr;
            WantedApttrEntry.Placeholder = AppResource.WantedAPPTr;
            NumberOfUnitsEntry.Placeholder = $"{2500}";
            CurrentRateEntry.Placeholder = AppResource.CurrentSpeed;
            VolumeEntry.Placeholder = $"{50}";
            CalculateButton.Text = AppResource.Calculate;
            WeightUnit.Text = AppResource.UnitWeight;
            CurrentRateUnit.Text = AppResource.UnitCurrentRate;
            NumberOfUnitsUnit.Text = AppResource.Unit;
            VolumeLabel.Text = AppResource.UnitVolume;
            RateTitleLabel.Text = AppResource.Rate;
            RateUnit.Text = AppResource.UnitCurrentRate;
            BolusTitleLabel.Text = AppResource.Bolus;
            BolusUnit.Text = AppResource.Unit;
        }

        private void CalculateButton_OnClicked(object sender, EventArgs e)
        {
            float weight = float.NaN, currentATTR = float.NaN;           // Weight, Current APTTR, Current Rate,
            int currentRate, numberOfUnits = 2500, volume = 50;                        // Number of Units, Volume
            
            weight = float.Parse(WeightEntry.Text);
            if (Single.IsNaN(weight)) DependencyService.Get<INativeFun>().ShortAlert(AppResource.NumberAlert);
            else
            {
                switch (FirstCalculationSwitch.IsToggled)
                {
                 case  true:
                     const int konst = 18;                                                          // Constant needed for evaluating the first rate
                     RateValueLabel.Text = $"{volume * weight * konst / numberOfUnits}";
                     BolusValueLabel.Text = "0";
                     break;
                 case  false:
                     try
                     {
                         currentRate = int.Parse(CurrentRateEntry.Text);
                         currentATTR = float.Parse(CurrentApttrEntry.Text);
                         numberOfUnits = int.Parse(NumberOfUnitsEntry.Text);
                         volume = int.Parse(VolumeEntry.Text);
                         if (!Single.IsNaN(currentATTR))
                         {
                             Calculate(currentRate, weight, currentATTR, numberOfUnits, volume);
                         }
                         else
                         {
                             DependencyService.Get<INativeFun>().ShortAlert(AppResource.NumberAlert);
                         }
                     }
                     catch
                     {
                         DependencyService.Get<INativeFun>().ShortAlert(AppResource.NumberAlert);
                     }
                     break;
                     
                }
            }
        }

        private async void Calculate(float currentRate, float weight, float currentATTR, int numberOfUnits = 2500, int volume = 50)
        {
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
            }
        }
        

        private void FirstCalculationSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            CurrentApttrEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentApttrImage.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentRateImage.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentRateEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
            BolusValueLabel.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentApttrFrame.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentRateFrame.IsVisible = !FirstCalculationSwitch.IsToggled;
            if (FirstCalculationSwitch.IsToggled) SwitchLabel.Text = AppResource.ChangeOfRate;
            else SwitchLabel.Text = AppResource.FirstCalculation;

        }
    }
}