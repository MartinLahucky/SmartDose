using System;
using SmartDose.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartDose
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUs : ContentPage
    {
        public AboutUs()
        {
            InitializeComponent();

            Title = AppResource.AboutApp;
            AboutProjectLabelStatic.Text = AppResource.AboutProject;
            AboutProjectLabel.Text = AppResource.AboutProjectDescription;
            AboutUsLabelStatic.Text = AppResource.AboutUs;
            AboutUsLabel.Text = AppResource.AboutUsDescription;
            UsageLabel.Text = AppResource.UsageDescription;
            UsageLabelStatic.Text = AppResource.Usage;
        }
    }
}