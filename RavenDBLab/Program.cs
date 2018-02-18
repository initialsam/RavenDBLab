using Raven.Client.Documents;
using RavenDBLab.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBLab
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please, enter an order # (0 to exit): ");
                string orderNumber = Console.ReadLine();
                PrintOrder(orderNumber);
            }

        }

        private static void PrintOrder(string orderNumber)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var order = session
                    .Include<Order>(o => o.Company)
                    .Include(o => o.Employee)
                    .Include(o => o.Lines.Select(l => l.Product))
                    .Load(orderNumber);

                if (order == null)
                {
                    Console.WriteLine($"Order #{orderNumber} not found.");
                    return;
                }

                Console.WriteLine($"Order #{orderNumber}");

                var c = session.Load<Company>(order.Company);
                Console.WriteLine($"Company : {c.Id} - {c.Name}");

                var e = session.Load<Employee>(order.Employee);
                Console.WriteLine($"Employee: {e.Id} - {e.LastName}, {e.FirstName}");

                foreach (var orderLine in order.Lines)
                {
                    var p = session.Load<Product>(orderLine.Product);
                    Console.WriteLine($"   - {orderLine.ProductName}," +
                              $" {orderLine.Quantity} x {p.QuantityPerUnit}");
                }
            }
        }
    }

    
   
}
