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


        private async void FindItem(object sender, RoutedEventArgs e)
        {
            try
            {

                string baseUrl = App.ApiBaseUrl;
                string parameterUrl = "/Suppliers/GetSupplier/";
                string itemId = inputName.Text;
                int convertId = 0;
                if (int.TryParse(itemId, out convertId))
                {

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
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
        private void AddNew(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SupplerFormTemplate), null);
        }
    }
}
