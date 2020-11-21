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
        }

        private async void CalculateButton_OnClicked(object sender, EventArgs e)
        {
            if (WeightEntry.Text == String.Empty || CurrentApttrEntry.Text == String.Empty || TargetApttrEntry.Text == String.Empty )
            {
                CalculateButton.Text = "Kokot";
            }
            else
            {
                int idW = 0, idA = 0;
                int m = int.Parse(WeightEntry.Text);
                float currentA = float.Parse(CurrentApttrEntry.Text);
                float targetA = float.Parse(TargetApttrEntry.Text);
                HeparinTable[] Tab = await App.Database.GetHeparrinConstants();
                HeparinTableAPTT[] Aptt = await App.Database.GetHeparrinConstantsAPTT();
            
                // determine the index of weight:
                while (Tab[idW].Weight < m && !(Tab.Length <= idW)) idW++;
                // determine the index of aptt:
                while (Aptt[idA].APTTR < currentA && !(Aptt.Length <= idA)) idA++;
                
                

                
                Console.WriteLine("Toto je idW: " + idW);
                Console.WriteLine("Toto je idA: " + idA);
            }
        }
    }
}