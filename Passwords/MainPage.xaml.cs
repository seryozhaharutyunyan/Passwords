namespace Passwords
{
    public partial class MainPage : ContentPage
    {
        public bool flag = false;
        public MainPage()
        {
            InitializeComponent();
        }

        public void Reload(string url)
        {
#if Android
            var androidweb = blazorWebView.Handler.PlatformView as Android.Webkit.WebView;
            androidweb.LoadUrl(url);
#elif WINDOWS
            var windowweb = blazorWebView.Handler.PlatformView as Microsoft.UI.Xaml.Controls.WebView2;
            windowweb.Source = new Uri(url);
#elif IOS
            var iosweb = blazorWebView.Handler.PlatformView as WebKit.WKWebView;
            iosweb.LoadRequest(new Foundation.NSUrlRequest(new Uri(url)));
#endif
        }
    }
}
