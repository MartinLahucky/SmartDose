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

            // Setting default values
            ClearValues();

            // Localization 
            ClearButton.Text = AppResource.Clear;
            SwitchLabel.Text = AppResource.ChangeOfRate;
            WeightEntry.Placeholder = AppResource.Weight;
            CurrentApttrEntry.Placeholder = AppResource.CurrentAPTTr;
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
            try
            {
                var weight = float.Parse(WeightEntry.Text); // Weight, Current APTTR, Current Rate,
                var numberOfUnits = string.IsNullOrEmpty(NumberOfUnitsEntry.Text)
                    ? int.Parse(NumberOfUnitsEntry.Text)
                    : 2500; // Current Rate
                var volume = string.IsNullOrEmpty(VolumeEntry.Text) ? int.Parse(VolumeEntry.Text) : 50; // Current Rate

                switch (FirstCalculationSwitch.IsToggled)
                {
                    case true:
                        var konst = 18; // Constant needed for evaluating the first rate
                        RateValueLabel.Text = $"{volume * weight * konst / numberOfUnits}";
                        BolusValueLabel.Text = "0";
                        break;

                    case false:
                        var currentRate = int.Parse(CurrentRateEntry.Text);
                        var currentATTR = float.Parse(CurrentApttrEntry.Text);
                        Calculate(currentRate, weight, currentATTR, numberOfUnits, volume);
                        break;
                }
            }
            catch
            {
                DependencyService.Get<INativeFun>().ShortAlert(AppResource.NumberAlert);
            }
        }

        private async void Calculate(float currentRate, float weight, float currentAttr, int numberOfUnits, int volume)
        {
            {
                int id = 0, bolus = 0; // Local data holders
                var hapArray = await App.Database.GetHeparrinConstants(); // Array of haparin constants
                int[] dRmulti = {4, 2, 0, -2, 0}; // Array of multiplier of delta rate

                if (currentAttr > hapArray[hapArray.Length - 1].Aptt)
                {
                    id = 4;
                    bolus = hapArray[hapArray.Length - 1].Bolus;
                }
                else
                {
                    for (; id < hapArray.Length - 1; id++)
                        if (currentAttr > hapArray[id].Aptt && currentAttr <= hapArray[id + 1].Aptt)
                            bolus = hapArray[id + 1].Bolus;
                }

                RateValueLabel.Text =
                    $"{Math.Round(-currentRate * Math.Sign(id - 4) + volume * weight * dRmulti[id] / numberOfUnits)}";
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
            BolusFrame.IsVisible = !FirstCalculationSwitch.IsToggled;
            if (FirstCalculationSwitch.IsToggled) SwitchLabel.Text = AppResource.FirstCalculation;
            else SwitchLabel.Text = AppResource.ChangeOfRate;
        }

        private void ClearValues()
        {
            // Default values
            WeightEntry.Text = string.Empty;
            NumberOfUnitsEntry.Text = string.Empty;
            CurrentApttrEntry.Text = string.Empty;
            CurrentRateEntry.Text = string.Empty;
            VolumeEntry.Text = string.Empty;
            RateValueLabel.Text = string.Empty;
            BolusValueLabel.Text = string.Empty;
        }

        private void ClearButton_OnClicked(object sender, EventArgs e)
        {
            ClearValues();
        }
    }
}