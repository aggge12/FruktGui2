using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruktAdminApp.Models
{
    class SupplierModel
    {
        public SupplierModel()
        {

        }

        public SupplierModel(int id, string Name)
        {
            this.id = id;
            this.Name = Name;
        }


        public int id { get; set; }


        public string Name { get; set; }
    }
}
