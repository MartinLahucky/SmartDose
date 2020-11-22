using SmartDose.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartDose
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwitchPage : TabbedPage
    {
        public SwitchPage()
        {
            InitializeComponent();

            Title = AppResource.SmartDose;
            
            Children.Add(new Heparin {Title = AppResource.Heparin});
            Children.Add(new Insulin {Title = AppResource.Insulin});
        }
    }
}