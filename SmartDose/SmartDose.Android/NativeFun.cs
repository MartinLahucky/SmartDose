using Android.Widget;
using AndroidApp = Android.App.Application;

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