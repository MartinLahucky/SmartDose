using Foundation;
using SmartDose.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(NativeFun))]

namespace SmartDose.iOS
{
    public class NativeFun : INativeFun
    {
        const double LONG_DELAY = 3.5;
        private const double SHORT_DELAY = 2.0;
        private NSTimer alertDelay;

        private UIAlertController alert;

        public void ShortAlert(string message)
        {
            ShowAlert(message, SHORT_DELAY);
        }

        public void LongtAlert(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }

        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) => { dismissMessage(); });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }

            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}