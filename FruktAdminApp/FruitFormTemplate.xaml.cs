using FruktAdminApp.Models.FruitWebService.ReturnModels;
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
    public sealed partial class FruitFormTemplate : Page
    {
        public FruitModel fruit { get; set; }
        Boolean newItem;

        public FruitFormTemplate() // create new
        {
            this.InitializeComponent();
            DataContext = fruit;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this.fruit = e.Parameter as FruitModel;
                fruitId.Text = "ID : " + fruit.Id;
                fruitName.Text = fruit.Name;
                fruitqty.Text = fruit.QuantityInSupply;
                newItem = false;

            }
            else
            {
                fruit = new FruitModel();
                newItem = true;
            }
        }


        public void saveChanges(object sender, RoutedEventArgs e)
        {
            if (newItem)
            {
                // check ID then post
                fruit.Name = fruitName.Text;
                fruit.QuantityInSupply = fruitqty.Text;
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:8081/");
                    client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(fruit), System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("Fruits/PostFruit", stringContent).Result;
                using (HttpContent content = response.Content)
                {
                    var json = content.ReadAsStringAsync().Result;
                    fruit = JsonConvert.DeserializeObject<FruitModel>(json);
                    fruitId.Text = "ID : " + fruit.Id;
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
                //update
            }

            // API update
            
        }

        public void Back_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Fruit), null);
        }
        public void ManageSuppliers(object sender, RoutedEventArgs e)
        {
            var parameters = fruit;
            this.Frame.Navigate(typeof(FruitSupplier), parameters);
        }
    }
}
