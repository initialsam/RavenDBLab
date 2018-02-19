using Raven.Client.Documents;
using RavenDBLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBLab
{
    public class Unit1Lesson5
    {
        public static void Demo()
        {
            while (true)
            {
                Console.WriteLine("Please, enter a company id (0 to exit): ");

                int companyId;
                if (!int.TryParse(Console.ReadLine(), out companyId))
                {
                    Console.WriteLine("Order # is invalid.");
                    continue;
                }

                if (companyId == 0) break;

                QueryCompanyOrders(companyId);
            }
        }
        private static void QueryCompanyOrders(int companyId)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                string key = $"companies/{companyId}-A";
                var orders = (
                    from order in session.Query<Order>()
                                         .Include(o => o.Company)
                    where order.Company == key
                    select order
                    ).ToList();

                var company = session.Load<Company>(key);

                if (company == null)
                {
                    Console.WriteLine("Company not found.");
                    return;
                }

                Console.WriteLine($"Orders for {company.Name}");

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id} - {order.OrderedAt}");
                }
            }
        }
    }
}
