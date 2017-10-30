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
            try
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
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private async void GetSuppliersByName(object sender, RoutedEventArgs e) // get the suppliers by name
        {
            try
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
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private async void RemoveSupplier(object sender, RoutedEventArgs e) // removes a suppler - fruit relationship
        {
            try
            {
                if (ListOfAddedSuppliersResult.SelectedItems.Count > 0)
                {
                    SupplierModel item = (SupplierModel)ListOfAddedSuppliersResult.SelectedItems[0];
                    string baseUrl = App.ApiBaseUrl;
                    string parameterUrl = "/FruitSuppliers/GetFruitSupplierByFruitAndSupplier/";

                    using (HttpClient client = new HttpClient())
                    using (HttpResponseMessage response = await client.GetAsync(baseUrl + parameterUrl + fruit.Id + "/" + item.id)) // gets the relationship by providing fruit and supplier id's to the api
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {

                            FruitSupplierModel fruitSupplierModel = JsonConvert.DeserializeObject<FruitSupplierModel>(result);

                            parameterUrl = "/FruitSuppliers/DeleteFruitSupplier/";


                            using (HttpResponseMessage deleteResponse = await client.DeleteAsync(baseUrl + parameterUrl + fruitSupplierModel.id)) // removes the relationship
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
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        public void AddSupplier(object sender, RoutedEventArgs e) // adds a supplier for a fruit
        {
            try
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
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }


        private void LoadSuppliers(object sender, RoutedEventArgs e) // fills list with suppliers 
        {
            var listView = (ListView)sender;
            listView.ItemsSource = SupplierList;
        }

        private void CheckDuplicatesInLists() // we dont want a supplier for a search result where the fruit is already provided by that supplier
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
