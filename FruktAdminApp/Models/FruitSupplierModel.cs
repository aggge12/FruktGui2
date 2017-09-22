using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruktAdminApp.Models
{
    class FruitSupplierModel
    {
        public int Fruit { get; set; }

        public int Supplier { get; set; }

        public int id { get; set; }

        public FruitSupplierModel()
        {

        }

        public FruitSupplierModel(int id, int Fruit, int Supplier)
        {
            this.id = id;
            this.Fruit = Fruit;
            this.Supplier = Supplier;
        }

        public FruitSupplierModel(int Fruit, int Supplier)
        {
            this.Fruit = Fruit;
            this.Supplier = Supplier;
        }
    }
}
