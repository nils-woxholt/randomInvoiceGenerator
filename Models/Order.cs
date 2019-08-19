using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace invoice_generator.Models
{
    public class Order 
    {
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string Comments { get; set; }
        public List<OrderLine> OrderLines { get; set; }

        public decimal SubTotal
        {
            get
            {
                return OrderLines.Sum(line => line.Price);
            }
        }
        public decimal Shipping
        {
            get
            {
                // 10% of order value
                return (OrderLines.Sum(line => line.Price)) * 0.1m;
            }
        }

        public decimal Tax {
            get {
                // 14% of order value
                return (OrderLines.Sum(line => line.Price)) * 0.14m;
            }
        }

        public decimal Total {
            get
            {
                return Tax + SubTotal + Shipping;
            }
        }

    }

    public class Customer
    {
        public string CompanyName { get; set; }
        [DisplayName("Ship To")]
        public string ShipTo1 { get; set; }
        public string ShipTo2 { get; set; }
        public string ShipTo3 { get; set; }
        public string ShipTo4 { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [DisplayName("Customer")]
        public string CustomerCode { get; set; }
        [DisplayName("Supplier")]
        public string SupplierCode { get; set; }
        [DisplayName("Tax No")]
        public string TaxNumber { get; set; }
    }

    public class OrderLine
    {
        public OrderLine ()
        {
            StockItem = new StockItem();
        }
        public StockItem StockItem { get; set; }
        public int Qty { get; set; }
        public decimal Price
        {
            get
            {
                return Qty * (StockItem.UnitPrice);
            }
        }
    }
    public class StockItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public static class RandomExtensions
    {
        public static double NextDouble(
            this Random random,
            double minValue,
            double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }

    public class OrderModel
    {
        List<StockItem> Stock = new List<StockItem>();
        List<Customer> Customers = new List<Customer>();
        public OrderModel()
        {
            Random random = new Random();

            var descList = new string[] {
                "CREALITY CR-10S PRO LARGE SIZE DIY 3D PRINTER KIT",
                "CREALITY CR-X LARGE SIZE DIY 3D PRINTER KIT",
                "CREALITY CR-10S LARGE SIZE DIY 3D PRINTER KIT",
                "CREALITY ENDER 3 PRO DIY 3D PRINTER",
                "CREALITY ENDER-2 3D PRINTER",
                "OCTOPI 3D PRINTING UPGRADE KIT",
                "CREALITY ENDER 5 3D PRINTER",
                "DAVINCI LAB PLA FILAMENT (1.75MM 1KG) WHITE",
                "DAVINCI LAB ABS FILAMENT (1.75MM 1KG) WHITE",
                "DAVINCI LAB PLA FILAMENT (1.75MM 1KG) YELLOW",
                "DAVINCI LAB ABS FILAMENT (1.75MM 1KG) YELLOW",
                "DAVINCI LAB PLA FILAMENT (1.75MM 1KG) GREEN",
                "DAVINCI LAB ABS FILAMENT (1.75MM 1KG) GREEN",
                "DAVINCI LAB PLA FILAMENT (1.75MM 1KG) BLUE",
                "DAVINCI LAB ABS FILAMENT (1.75MM 1KG) BLUE",
                "DAVINCI LAB PLA FILAMENT (1.75MM 1KG) RED",
                "DAVINCI LAB ABS FILAMENT (1.75MM 1KG) RED",
                "XTC-3D 680G PROTECTIVE COATING FOR SMOOTHING AND FINISHING 3D PRINTED PARTS",
                "MICRO SD TO SD EXTENSION CABLE",
                "MICRO SD TO MICRO SD EXTENSION CABLE",
                "3 IN 1 FUSE POWER SUPPLY SOCKET",
                "1.75MM BRASS FEED EXTRUDER WHEEL DRIVE GEAR FOR REPRAP 3D PRINTER",
                "M6X25 EXTRUDER 1.75MM THREAD NOZZLE THROAT WITH TEFLON FOR 3D PRINTER",
                "GEEKCREIT® ASSEMBLED ALUMINUM HEATING BLOCK EXTRUDER HOT END FOR 3D PRINTER 1.75MM MK8 0.4MM NOZZLE",
                "CREALITY LIMIT SWITCH 3D PRINTER PART",
                "24V/15A UNIVERSAL REGULATED SWITCHING MODE POWER SUPPLY FOR ENDER SERIES 3D PRINTER ENDER-2 ENDER-3 ",
                "CREALITY 3D® 40X40X10MM 24V HIGH SPEED DC BRUSHLESS 4010 NOZZLE COOLING FAN FOR 3D PRINTER ENDER-3",
                "UPGRADE REMOTE METAL EXTRUDER KIT FOR CREALITY 3D PRINTER",
                "MACHIFIT 8MM LOCK COLLAR FOR T8 LEAD SCREW",
                "CREALITY 3D ENDER-3 V1.1.4 REPLACEMENT MAINBOARD PRINT SIZE 220X220X250MM FOR ENDER-3 3D PRINTER",
                "10PCS 3MM THICKNESS 3D PRINTER HEATING BLOCK COTTON HOTEND NOZZLE HEAT INSULATION COTTON"
            };

            // generate random inventory
            for (int i = 0; i < descList.Length; i++)
            {
                Stock.Add(new StockItem { Code = $"ABC-{i}", Description = descList[i], UnitPrice = Convert.ToDecimal(random.NextDouble(0, 999.99)) });
            }

            // Add random customers
            Customers.Add(new Customer { CompanyName = "ABC Suppliers", ContactPerson = "Bob Simpson Jr.", CustomerCode = "ABC001", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Twilight Technologies", ContactPerson = "Sarette Bakkes", CustomerCode = "ABC0012", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Goblin's Unlimited Solution Warehouse", ContactPerson = "Carl Thorn", CustomerCode = "A-HH-C001", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Golden Road Corporation", ContactPerson = "Harry Frost", CustomerCode = "ABC001917", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Dynamico", ContactPerson = "Trevor West", CustomerCode = "ABC00981kkj-19171", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Whirlwindustries", ContactPerson = "Phil Collins", CustomerCode = "ABC/lksj/001", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Primedia", ContactPerson = "Jon Moore", CustomerCode = "ABC0012", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Phoenixolutions", ContactPerson = "Gavin Prescott", CustomerCode = "ABC0013", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Fireworld", ContactPerson = "Conor Cooper", CustomerCode = "ABC0014", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
            Customers.Add(new Customer { CompanyName = "Apachewater", ContactPerson = "Johanika Raubenheimer", CustomerCode = "XBC001", Email = "abc@example.com", Fax = "021 1726 626", ShipTo1 = "123 Carnaby Street, Kenilworth", ShipTo2 = "Somerset West", ShipTo3 = "South Africa", ShipTo4 = "9017", SupplierCode = "OHG-10928/1232", TaxNumber = "1826 shs - sgsf", Telephone = "021 123 4567" });
        }
        public Order GetRandomOrder()
        {
            var random = new Random();

            var order = new Order { OrderDate = DateTime.Now, OrderLines = new List<OrderLine>()};

            // random order number
            order.OrderNumber = $"PO-{random.Next(1000)}-{random.Next(1000)}";
            // get a comment
            order.Comments = "Please deliver to the back gate!";
            // assign a random customer
            order.Customer = Customers[random.Next(Customers.Count - 1)];

            // get a random number of lines
            int orderLines = random.Next(1, Stock.Count);

            // randomly populate the lines
            for (int i = 0; i < orderLines; i++)
            {
                order.OrderLines.Add(new OrderLine {Qty = random.Next(1,25), StockItem = Stock[random.Next(Stock.Count)] });
            }

            return order;
        }

    }

}