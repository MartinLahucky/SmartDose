using Android.Widget;
using SmartDose.Android;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(NativeFun))]

namespace SmartDose.Android
{
    public class NativeFun : INativeFun
    {
        public void ShortAlert(string message)
        {
            Toast.MakeText(AndroidApp.Context, message, ToastLength.Short).Show();
        }
    }
}