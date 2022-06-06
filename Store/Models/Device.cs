using System;

namespace Store.Models
{
    internal abstract class Device
    {
        string id;
        string name;
        string brand;
        int quantity;
        double price;
        double discout;
        string category;

        public string Id { get { return id; } set { id = Guid.NewGuid().ToString(); } }
        public string Name { get { return name; } set { name = !String.IsNullOrEmpty(value) ? value : throw new Exception("Name Is Empty");  } }
        public string Brand { get { return brand; } set { brand = !String.IsNullOrEmpty(value) ? value : throw new Exception("Brand Is Empty"); } }
        public double Price { get { return price; } set { price = value > 0 ? value : throw new Exception("Value Is Not Valid");  } }
        public int Quantity { get { return quantity; } set { quantity = value >= 0 ? value : throw new Exception("Value Is Not Valid");  } }
        public string Category { get { return category; } set { category = value; } }
        public double Discout { get { return discout; } set { discout = value; } }
        public double totalPrice { get { return price - (price * discout); } }

        public override string ToString()
        {
            return $"{this.GetType().Name},{this.Id},{this.Name},{this.Brand},{this.Price},{this.Quantity},{this.Category},{this.Discout}";
        }
    }
}
