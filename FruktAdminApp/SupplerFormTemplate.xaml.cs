using FruktAdminApp.Models;
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
           
            responseMsg.Text = "response from Api";
        }

        public void Back_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Suppliers), null);
        }
    }
}
