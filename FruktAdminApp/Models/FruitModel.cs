using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruktAdminApp.Models
{

    namespace FruitWebService.ReturnModels
    {
        public class FruitModel
        {
            public FruitModel()
            {

            }

            public FruitModel(int id, string Name, int qtt)
            {
                this.id = id;
                this.Name = Name;
                this.QuantityInSupply = qtt;
            }

            public int id { get; set; }

            public string Name { get; set; }

            public int QuantityInSupply { get; set; }

        }
    }
}
