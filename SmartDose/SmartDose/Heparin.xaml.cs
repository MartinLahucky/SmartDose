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
            NumberOfUnitsEntry.Placeholder = AppResource.NumberOfUnits;
            CurrentApttrEntry.Placeholder = AppResource.CurrentAPTTr;
            CurrentRateEntry.Placeholder = AppResource.CurrentSpeed;
            VolumeEntry.Placeholder = AppResource.Volume;
            CalculateButton.Text = AppResource.Calculate;
            
        }

        private async void CalculateButton_OnClicked(object sender, EventArgs e)
        {
            // TODO když se nedosadí hodnoty do cA tak dát default text  
            
            float m, wA, cA;                                                                // Weight, Wanted APTTR, Current APTTR
            const float konst = 18;                                                         // Constant needed for evaluating the first rate
            float cR, nU, v;                                                                // Current Rate, Number of Units, Volume, ID of the values in arrays below
            int id = 0;
            int bolus = 0;

            HeparinTableAPTT[] hapArray = await App.Database.GetHeparrinConstants();        // Array of haparin constants
            int[] dRmulti = {4, 2, 0, -2, 0};                                               // List of multiplier of delta rate 
            
            if (WeightEntry.Text == String.Empty || WantedApttrEntry.Text == String.Empty || NumberOfUnitsEntry.Text == String.Empty || VolumeEntry.Text == String.Empty || (CurrentApttrEntry.IsVisible && CurrentApttrEntry.Text == String.Empty) || (CurrentRateEntry.IsVisible && CurrentRateEntry.Text == String.Empty))                                
            {
            }
            else
            {
                //
                m = float.Parse(WeightEntry.Text);
                wA = float.Parse(WantedApttrEntry.Text);
                nU = int.Parse(NumberOfUnitsEntry.Text);
                v = int.Parse(VolumeEntry.Text);
                //
                switch (FirstCalculationSwitch.IsToggled)
                {
                    case true:
                        // For the first calculation:
                        RateValueLabel.Text = $"{v * m * konst / nU}";
                        BolusValueLabel.Text = "0";
                        break;
                    case false:
                        // Fot every next calculation:
                        cA = float.Parse(CurrentApttrEntry.Text);
                        cR = int.Parse(CurrentRateEntry.Text);

                        if (cA > hapArray[hapArray.Length - 1].Aptt)
                        {
                            id = 4;
                            bolus = hapArray[hapArray.Length - 1].Bolus;
                        }
                        else
                        {
                            for (int i = 0; i < hapArray.Length - 2; i++)
                            {
                                if (cA > hapArray[i].Aptt && cA <= hapArray[i + 1].Aptt)
                                {
                                    id = i;
                                    bolus = hapArray[i].Bolus;
                                }
                            }
                        }
                        
                        RateValueLabel.Text = $"{Math.Round(-cR * Math.Sign(id - 4) + v * m * dRmulti[id] / nU)}";
                        BolusValueLabel.Text = $"{(Math.Round((m * bolus) / 500)) * 500}"; 
                        break;
                }
            }
        }

        private void FirstCalculationSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            CurrentApttrEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
            CurrentRateEntry.IsVisible = !FirstCalculationSwitch.IsToggled;
        }
    }
}