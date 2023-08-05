using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Item
    {
        public int InitialPrice { get; set; }
        public int SellingPrice { get; set; }
        public string CompanyName { get; set; }
        public string PrivateName { get; set; }
        public string Quantity { get; set; }
        public long Barcode { get; set; }
        public bool IsSelected { get; set; }
        public Item()
        {

        }
        public Item selectItem()
        {
            return new Item(this.InitialPrice, this.SellingPrice, this.CompanyName, this.PrivateName, this.Barcode, this.Quantity, true);        
        }
        public Item(int initialPrice, int sellingPrice, string companyName, string privateName, long barcode,string quantity)
        {
            InitialPrice = initialPrice;
            SellingPrice = sellingPrice;
            CompanyName = companyName;
            PrivateName = privateName;
            Barcode = barcode;
            Quantity = quantity;
        }
        public Item(int initialPrice, int sellingPrice, string companyName, string privateName, long barcode, string quantity,bool select)
        {
            InitialPrice = initialPrice;
            SellingPrice = sellingPrice;
            CompanyName = companyName;
            PrivateName = privateName;
            Barcode = barcode;
            Quantity = quantity;
            IsSelected = select;
        }
        public void toString()
        {
            MainWindow.writeToLogs("InitialPrice: " + InitialPrice);
            MainWindow.writeToLogs("SellingPrice: " + SellingPrice);
            MainWindow.writeToLogs("CompanyName: " + CompanyName);
            MainWindow.writeToLogs("PrivateName: " + PrivateName);
            MainWindow.writeToLogs("Barcode: " + Barcode);
            MainWindow.writeToLogs("Quantity: " + Quantity);
        }




   
    }
}
