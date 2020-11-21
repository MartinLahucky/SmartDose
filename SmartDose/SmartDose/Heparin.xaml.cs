using System;
using SmartDose.Helpers.Database_Models;
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
                CalculateButton.Text = "Kokot";
            }
            else
            {
                int idW = 0, idA = 0, dV = 0; // možná není nutno nastavit 0
                int v = int.Parse(CurrentSpeedEntry.Text);
                int m = int.Parse(WeightEntry.Text);
                float currentA = float.Parse(CurrentApttrEntry.Text);
                float targetA = float.Parse(CurrentSpeedEntry.Text);
                HeparinTable[] Tab = await App.Database.GetHeparrinConstants();
                float[] Apttr = {1.2f, 1.5f, 2.3f, 3f};

                // determine the index of weight:
                while (Tab[idW].Weight < m && !(Tab.Length <= idW)) idW++;
                // determine the index of aptt:
                while (Apttr[idA] < currentA && !(Apttr.Length <= idA)) idA++;

                switch (idA)
                {
                    case 0:
                        DoseLabel.Text = $"{m * Tab[idW].Bolus80}";
                        dV = Tab[idW].SpeedPlus4;
                        SpeedLabel.Text = $"{v + dV}";
                        break;
                    case 1:
                        DoseLabel.Text = $"{m*Tab[idW].Bolus40}";
                        dV = Tab[idW].SpeedPlus2;
                        SpeedLabel.Text = $"{v + dV}";
                        break;
                    case 2:
                        DoseLabel.Text = "0";
                        SpeedLabel.Text = $"{v}";
                        break;
                    case 3:
                        DoseLabel.Text = "0";
                        dV = Tab[idW].SpeedPlus2;
                        SpeedLabel.Text = $"{v - dV}";
                        break;
                    case 4:
                        DoseLabel.Text = "0";
                        SpeedLabel.Text = "0";
                        break;
                }


                Console.WriteLine("Toto je idW: " + idW);
                Console.WriteLine("Toto je idA: " + idA);
            }
        }
    }
}