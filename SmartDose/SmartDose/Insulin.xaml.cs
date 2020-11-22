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

            DailyDoseEntry.Text = String.Empty;
            CarbsInMealEntry.Text = String.Empty;
            WantedGlucoseEntry.Text = String.Empty; // TODO 6
            CurrentGlucoseEntry.Text = String.Empty;
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
    }
}