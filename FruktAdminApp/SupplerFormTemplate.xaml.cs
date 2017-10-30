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

        public void deleteItem(object sender, RoutedEventArgs e)
        {
            if (Suppl != null && newItem == false)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(App.ApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = client.DeleteAsync("/Suppliers/DeleteSupplier/" + Suppl.id).Result;
                using (HttpContent content = response.Content)
                {
                }
                if (response.IsSuccessStatusCode) // clear all fields since item is removed
                {
                    newItem = true;
                    deleteButton.IsEnabled = false;
                    responseMsg.Text = "OK";
                    supplierName.Text = "";
                    supplierId.Text = "";
                    Suppl = new SupplierModel();

                }
                else
                {
                    responseMsg.Text = "Removal failed";
                }
            }

        }
        public void saveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                if (newItem) // if its a new item or if its an existing item
                {
                    // check ID then post
                    Suppl.Name = supplierName.Text;
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(App.ApiBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    var stringContent = new StringContent(JsonConvert.SerializeObject(Suppl), System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("/Suppliers/PostSupplier", stringContent).Result; // posting new supplier 
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().Result;
                        Suppl = JsonConvert.DeserializeObject<SupplierModel>(json);
                        supplierId.Text = Suppl.id.ToString();
                        newItem = false;
                        deleteButton.IsEnabled = true;
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
                    client.BaseAddress = new Uri("http://localhost:8081");
                    client.DefaultRequestHeaders.Accept.Clear();

                    var stringContent = new StringContent(JsonConvert.SerializeObject(Suppl), System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync("/Suppliers/PutSupplier" + "/" + Suppl.id, stringContent).Result; // updating existing supplier
                    if (response.IsSuccessStatusCode)
                    {
                        responseMsg.Text = "OK";

                    }
                    else
                    {
                        responseMsg.Text = "Failed";
                    }


                }

            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}
