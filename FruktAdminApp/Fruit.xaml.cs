using FruktAdminApp.Models.FruitWebService.ReturnModels;
using System;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FruktAdminApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Fruit : Page
    {
        public Fruit()
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
            string parameterUrl = "/Fruits/GetFruit/";
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
                if (result != null )
                {
                    FruitModel fruit = JsonConvert.DeserializeObject<FruitModel>(result);
                    var parameters = fruit;
                    this.Frame.Navigate(typeof(FruitFormTemplate), parameters);
                }
            }
            // something something api request

            // new page or something?
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FruitFormTemplate), null);
        }


    }
}
