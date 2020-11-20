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

        private void WeightEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void CurrentApttrEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void TargetApttrEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}