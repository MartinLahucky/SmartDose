using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            float dailyDose, currentGlucose, wantedGlucose, carbsInMeal;
            
            if (DailyDoseEntry.Text == String.Empty || CarbsInMealEntry.Text == String.Empty || WantedGlucoseEntry.Text == String.Empty || CurrentGlucoseEntry.Text == String.Empty)
            {
                
            }
            else
            {
                dailyDose = float.Parse(DailyDoseEntry.Text);
                carbsInMeal = float.Parse(CarbsInMealEntry.Text);
                wantedGlucose = float.Parse(WantedGlucoseEntry.Text);
                currentGlucose = float.Parse(CurrentGlucoseEntry.Text);

                DoseValueLabel.Text = (dailyDose * (currentGlucose - wantedGlucose) / 110 + carbsInMeal * dailyDose / 350).ToString();

                //J = TDDI × (Gly-Gly’) / 110  +   Mj ×TDDI / 350
            }
        }
    }
}