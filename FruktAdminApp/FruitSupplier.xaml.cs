﻿using Newtonsoft.Json;
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
    public sealed partial class FruitSupplier : Page
    {
        public string fruitID;
        public string supplierID;
        public FruitSupplier()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                // receive fruit ID
            }
        }

        private async void FindItem(object sender, RoutedEventArgs e)
        {
            string baseUrl = "http://localhost:8081";
            string parameterUrl = "/Fruits/";
            string itemId = "5";
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
                   // FruitModel fruit = JsonConvert.DeserializeObject<FruitModel>(result);
                   // var parameters = fruit;
                  //  this.Frame.Navigate(typeof(FruitFormTemplate), parameters);
                }
            }
            // something something api request

            // new page or something?
        }
        public void saveChanges(object sender, RoutedEventArgs e)
        {
            // API update
            responseMsg.Text = "response from Api";
        }

        public void Back_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Fruit), null);
        }
    }
}
