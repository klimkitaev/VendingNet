using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingNet.Models
{
    /// <summary>
    /// Типы видов продукции
    /// </summary>
    public enum ProductTypes { Tea, Coffee, MilkCoffee, Juice }

    public class Product : IUnit<ProductTypes>
    {

        private ProductTypes _type;

        public Product(ProductTypes type)
        {
            _type = type;
        }

        public ProductTypes Type
        {
            get { return _type; }
        }

        public Info Info
        {
            get { return Product.GetInfo(_type); }
        }

        public static Info GetInfo(ProductTypes type)
        {
            switch (type)
            {
                case ProductTypes.Tea:
                    return new Info { Title = "Чай", Price = 13 };
                case ProductTypes.Coffee:
                    return new Info { Title = "Кофе", Price = 18 };
                case ProductTypes.MilkCoffee:
                    return new Info { Title = "Кофе с молоком", Price = 21 };
                case ProductTypes.Juice:
                    return new Info { Title = "Сок", Price = 35 };
                default:
                    return null;
            }
        }
    }
}