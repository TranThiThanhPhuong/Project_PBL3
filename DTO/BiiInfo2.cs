using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillInfo2
    {
        public string FoodName { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }

        public BillInfo2(DataRow row)
        {
            this.FoodName = row["FoodName"].ToString();
            this.Amount = (int)row["Amount"];
            this.Price = (int)row["Price"];
            this.TotalPrice = (int)row["TotalPrice"];
        }
    }
}
