using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.ApiServices.Shopping;
using Checkout.ApiServices.Shopping.Dto;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new ShopService();
            //var t = x.GetAllShopItemAsync(); // working...
            //x.DeleteShopItem("Water Vital"); //working...
            //x.AddShopItem(new ShopItemDto //working
            //{
            //    Name = "Water vital",
            //    Count = 4
            //});

            //x.UpdateShopItem(new ShopItemDto //working
            //{
            //    Name = "Fanta",
            //    Count = 2,
            //    UnitPrice = 35.00
            //});

            //var v = x.GetShopItemById("Pepsi");
            Console.ReadKey();
        }
    }
}
