using System;
using SmartDose.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartDose
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Insulin : ContentPage
    {
        public Insulin()
        {
            InitializeComponent();

            // Setting default values
            ClearValues();
            
            InfoToolBarButton.IconImageSource = "outline_info_white_18dp.png";

            // Localization 
            ClearButton.Text = AppResource.Clear;
            DailyDoseEntry.Placeholder = AppResource.DailyDose;
            DailyDoseUnit.Text = AppResource.Unit;
            CarbsInMealUnit.Text = AppResource.UnitCarbsInMeal;
            WantedGlucoseUnit.Text = AppResource.UnitGlucose;
            CurrentGlucoseUnit.Text = AppResource.UnitGlucose;
            CalculateButton.Text = AppResource.Calculate;
            DoseUnit.Text = AppResource.Unit;
            DoseTitleLabel.Text = AppResource.Dose;
            CarbsInMealEntry.Placeholder = AppResource.CarbsInMeal;
            CurrentGlucoseEntry.Placeholder = AppResource.CurrentGlucose;
            WantedGlucoseEntry.Placeholder = AppResource.WantedGlucose;
        }

        private void CalculateButton_OnClicked(object sender, EventArgs e)
        {
            int dailyDose, currentGlucose, wantedGlucose, carbsInMeal;

            try
            {
                dailyDose = int.Parse(DailyDoseEntry.Text);
                carbsInMeal = int.Parse(CarbsInMealEntry.Text);
                currentGlucose = int.Parse(CurrentGlucoseEntry.Text);
                try
                {
                    wantedGlucose = int.Parse(WantedGlucoseEntry.Text);
                    DoseValueLabel.Text = $"{dailyDose * (currentGlucose - wantedGlucose) / 110 + carbsInMeal * dailyDose / 350}";
                }
                catch
                {
                    DoseValueLabel.Text = $"{dailyDose * (currentGlucose - 6) / 110 + carbsInMeal * dailyDose / 350}";
                }
            }
            catch
            {
                DependencyService.Get<INativeFun>().ShortAlert(AppResource.NumberAlert);
            }
        }
        private void ClearValues()
        {
            // Default values
            DailyDoseEntry.Text = String.Empty;
            CarbsInMealEntry.Text = String.Empty;
            WantedGlucoseEntry.Text = String.Empty;
            CurrentGlucoseEntry.Text = String.Empty;
            DoseValueLabel.Text = String.Empty;
        }

        private void ClearButton_OnClicked(object sender, EventArgs e)
        {
            ClearValues();
        }
        private async void InfoToolBarButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutUs());
        }
    }
}