﻿using FruktAdminApp.Models.FruitWebService.ReturnModels;
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
                fruitId.Text = fruit.Id;
                fruitName.Text = fruit.Name;
                fruitqty.Text = fruit.QuantityInSupply;
                newItem = false;

            }
            else
            {
                newItem = true;
            }
        }


        public void saveChanges(object sender, RoutedEventArgs e)
        {
            if (newItem)
            {
                // check ID then put
            }
            else
            {
                //update
            }

            // API update
            responseMsg.Text = "response from Api";
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
