using FruktAdminApp.Models;
using FruktAdminApp.Models.FruitWebService.ReturnModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        List<SupplierModel> fruitSupplierList = new List<SupplierModel>();
        public ObservableCollection<SupplierModel> SupplierList { get; set; }
        public FruitModel fruit;
        public FruitSupplier()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this.fruit = e.Parameter as FruitModel;
                GetSuppliersForFruit();
            }
        }

        private async void GetSuppliersForFruit() // get the suppliers for the specified Fruit
        {
            string baseUrl = App.ApiBaseUrl;
            string parameterUrl = "/FruitSuppliers/GetFruitSupplierByFruit";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(baseUrl + parameterUrl + "/" + fruit.Id))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                if (result != null)
                {
                    fruitSupplierList = (List<SupplierModel>)JsonConvert.DeserializeObject<IEnumerable<SupplierModel>>(result); 
                  
                    ListOfAddedSuppliersResult.ItemsSource = fruitSupplierList;

                }
            }
        }

        private async void GetSuppliersByName(object sender, RoutedEventArgs e)
        {
            string baseUrl = App.ApiBaseUrl;
            string parameterUrl = "/Suppliers/GetSuppliersByName/";
            string itemName = InputSupplierSearch.Text;

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(baseUrl + parameterUrl + itemName))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                if (result != null)
                {
                    SupplierList = new ObservableCollection<SupplierModel>(JsonConvert.DeserializeObject<IEnumerable<SupplierModel>>(result));
                    CheckDuplicatesInLists();
                    ListOfSuppliersResult.ItemsSource = SupplierList;
                    
                }
            }
        }

        private async void RemoveSupplier(object sender, RoutedEventArgs e)
        {
            if (ListOfAddedSuppliersResult.SelectedItems.Count > 0)
            {
                SupplierModel item = (SupplierModel)ListOfAddedSuppliersResult.SelectedItems[0];
                string baseUrl = App.ApiBaseUrl;
                string parameterUrl = "/FruitSuppliers/GetFruitSupplierByFruitAndSupplier/";

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(baseUrl + parameterUrl + fruit.Id + "/" + item.id))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {

                        FruitSupplierModel fruitSupplierModel = JsonConvert.DeserializeObject<FruitSupplierModel>(result);

                        parameterUrl = "/FruitSuppliers/DeleteFruitSupplier/";

     
                        using (HttpResponseMessage deleteResponse = await client.DeleteAsync(baseUrl + parameterUrl + fruitSupplierModel.id))
                        {

                            if (deleteResponse.IsSuccessStatusCode)
                            {
                                GetSuppliersForFruit();
                                CheckDuplicatesInLists();
                                ListOfSuppliersResult.ItemsSource = SupplierList;
                            }
                        }
                    }
                }
            }
        }

        public void AddSupplier(object sender, RoutedEventArgs e)
        {
            SupplierModel item = (SupplierModel)ListOfSuppliersResult.SelectedItems[0];


            FruitSupplierModel FSModel = new FruitSupplierModel(fruit.Id, item.id);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(App.ApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringContent = new StringContent(JsonConvert.SerializeObject(FSModel), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/FruitSuppliers/PostFruitSupplier", stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                GetSuppliersForFruit();
                CheckDuplicatesInLists();
                ListOfSuppliersResult.ItemsSource = SupplierList;
            }
            else
            {
            }
        }
        public void saveChanges(object sender, RoutedEventArgs e)
        {
            // API update
            responseMsg.Text = "response from Api";
        }


        private void LoadSuppliers(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.ItemsSource = SupplierList;
        }

        private void CheckDuplicatesInLists()
        {
            if (SupplierList != null)
            {
                foreach (SupplierModel supplier in SupplierList.ToList())
                {
                    if (fruitSupplierList.Any(prod => prod.id == supplier.id))
                    {
                        SupplierList.Remove(supplier);
                    }
                }
            }
        }
    }
}
