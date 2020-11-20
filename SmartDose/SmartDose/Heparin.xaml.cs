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
            int id = 0;
            int m = int.Parse(WeightEntry.Text);
            float currentA = float.Parse(CurrentApttrEntry.Text);
            float targetA = float.Parse(TargetApttrEntry.Text);
            HeparinTable[] Tab = await App.Database.GetHeparrinConstants();
            HeparinTableAPTT[] Aptt = await App.Database.GetHeparrinConstantsAPTT();

            while (Tab[id].Weight < m)
            {
                id++;
            }
            
            Console.WriteLine("Toto je hmotnost: " + m.ToString());
            Console.WriteLine("Toto je index: " + Tab[id].Weight.ToString());
        }
    }
}