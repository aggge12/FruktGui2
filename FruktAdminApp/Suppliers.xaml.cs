using FruktAdminApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FruktAdminApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Suppliers : Page
    {
        public Suppliers()
        {
            this.InitializeComponent();
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private async void FindItem(object sender, RoutedEventArgs e)
        {
            string baseUrl = "http://localhost:8081";
            string parameterUrl = "/Suppliers/";
            string itemId = inputName.Text;
            int convertId = 0;
            int.TryParse(itemId, out convertId);
            if (convertId == 0)
            {
                throw new Exception("can not be alphabetical or 0");
            }

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(baseUrl + parameterUrl + itemId))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                string result = await content.ReadAsStringAsync();

                // ... Display the result.
                if (result != null)
                {
                    SupplierModel suppl = JsonConvert.DeserializeObject<SupplierModel>(result);
                    var parameters = suppl;
                    this.Frame.Navigate(typeof(SupplerFormTemplate), parameters);
                }
            }
            // something something api request

            // new page or something?
        }
    }
}
