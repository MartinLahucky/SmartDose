using Foundation;
using SmartDose.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(NativeFun))]

namespace SmartDose.iOS
{
    public class NativeFun : INativeFun
    {
        private const double SHORT_DELAY = 2.0;
        private NSTimer alertDelay;
        
        private UIAlertController alert; 
        
        public void ShortAlert(string message)
        {
            ShowAlert(message, SHORT_DELAY);
        }
        
        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
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