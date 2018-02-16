using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var documentStore = new DocumentStore
            {
                Urls = new string[] { "http://localhost:8080" },
                Database = "Northwind"
            };

            documentStore.Initialize();
            using (var session = documentStore.OpenSession())
            {
                var p = session.Load<Product>("products/1-A");
                System.Console.WriteLine(p.Name);
                System.Console.WriteLine(p.Supplier);
                System.Console.WriteLine(p.Category);
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }
        public string QuantityPerUnit { get; set; }
        public float PricePerUnit { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public int ReorderLevel { get; set; }
    }
}
