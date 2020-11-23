using System;
using SmartDose.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartDose
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwitchPage : TabbedPage
    {
        private long lastPress;
        public SwitchPage()
        {
            InitializeComponent();

            Title = AppResource.SmartDose;
            InfoToolBarButton.IconImageSource = "outline_info_white_18dp.png";
            
            Children.Add(new Heparin {Title = AppResource.Heparin});
            Children.Add(new Insulin {Title = AppResource.Insulin});
        }
        
        protected override bool OnBackButtonPressed()
        {
            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            
            if (currentTime - lastPress > 5000)
            {
                DependencyService.Get<INativeFun>().LongtAlert(AppResource.WarningExit);
                lastPress = currentTime;
                return true;
            }
            else
            {
                base.OnBackButtonPressed();
                return false;
            }
        }
        private async void InfoToolBarButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutUs());
        }
    }
}