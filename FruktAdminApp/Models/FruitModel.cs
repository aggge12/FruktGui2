using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FruktAdminApp.Models
{

    namespace FruitWebService.ReturnModels
    {
        public class FruitModel : INotifyPropertyChanged
        {
            public FruitModel()
            {

            }

            public FruitModel(int id, string Name, int qtt)
            {
                this.Id = id;
                this.Name = Name;
                this.QuantityInSupply = qtt.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private int id;
            public int Id
            {
                get { return id; }
                set { this.id = value; }
            }

            private string name;
            public string Name
            {
                get { return name; }
                set { this.name = value; this.OnPropertyChanged("Name"); }
            }

            private string quantityInSupply;
            public string QuantityInSupply
            {
                get { return quantityInSupply; }
                set { this.quantityInSupply = value; this.OnPropertyChanged("QuantityInSupply"); }
            }

            
            protected void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }
    }
}
