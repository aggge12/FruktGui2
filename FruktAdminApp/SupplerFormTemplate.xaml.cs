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
    public sealed partial class SupplerFormTemplate : Page
    {
        SupplierModel Suppl { get; set; }
        Boolean newItem;

        public SupplerFormTemplate() // create new
        {
            this.InitializeComponent();
            DataContext = Suppl;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this.Suppl = e.Parameter as SupplierModel;
                supplierId.Text = Suppl.id.ToString();
                supplierName.Text = Suppl.Name;
                newItem = false;

            }
            else
            {
                Suppl = new SupplierModel();
                newItem = true;
            }
        }


        public void saveChanges(object sender, RoutedEventArgs e)
        {
            if (newItem)
            {
                // check ID then post
                Suppl.Name = supplierName.Text;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8081/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(Suppl), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("Suppliers/PostSupplier", stringContent).Result;
                using (HttpContent content = response.Content)
                {
                    var json = content.ReadAsStringAsync().Result;
                    Suppl = JsonConvert.DeserializeObject<SupplierModel>(json);
                    supplierId.Text = Suppl.id.ToString();
                    newItem = false;
                }
                if (response.IsSuccessStatusCode)
                {
                    responseMsg.Text = "OK";

                }
                else
                {
                    responseMsg.Text = "Failed";
                }

            }
            else
            {
                Suppl.Name = supplierName.Text;
                Suppl.id = int.Parse(supplierId.Text);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8081/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(Suppl), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("Suppliers/PutSupplier" + "/" + Suppl.id, stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseMsg.Text = "OK";

                }
                else
                {
                    responseMsg.Text = "Failed";
                }


            }

            // API update
        }

        public void Back_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Suppliers), null);
        }
    }
}
