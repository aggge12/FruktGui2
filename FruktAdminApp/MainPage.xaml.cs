using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FruktAdminApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Frame rootFrame = Window.Current.Content as Frame;
        }

        private void Suppliers_btn_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(typeof(Suppliers));
        }

        private void Fruit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(typeof(Fruit));
        }

        private void SetApiUrl(object sender, RoutedEventArgs e)
        {
            if (ApiUrl.Text != "" && ApiUrl.Text != null)
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                    localSettings.Values["ApiBaseUri"] = ApiUrl.Text;
                    App.ApiBaseUrl = ApiUrl.Text;
                    SucessLbl.Text = "Saved";
                }
                catch
                {
                    SucessLbl.Text = "Failed to save";
                }
            }

        }
    }
}
