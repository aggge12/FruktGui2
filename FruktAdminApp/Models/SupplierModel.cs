using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FruktAdminApp.Models
{
    public class SupplierModel : INotifyPropertyChanged
    {
        public SupplierModel()
        {

        }

        public SupplierModel(int id, string Name)
        {
            this.id = id;
            this.Name = Name;
        }


        private int _id;
        public int id
        {
            get { return this._id; }
            set { this._id = value; OnPropertyChanged("_id"); }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged("name"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
