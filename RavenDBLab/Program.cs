using Raven.Client.Documents;
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
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var p1 = session.Load<Product>("products/1-A");
                var p2 = session.Load<Product>("products/1-A");
                //ReferenceEquals(p1, p2) is true
                Console.WriteLine("ReferenceEquals(p1, p2) is "+ ReferenceEquals(p1, p2));

                //4.0 好像不支援用index取資料了
                //Product product = session.Load<Product>(1);
                //Product[] products = session.Load<Product>(1, 2, 3);

                Dictionary<string, Product> products = session.Load<Product>(new[] {
                                                            "products/1-A",
                                                            "products/2-A",
                                                            "products/3-A"
                                                        });

                //make two remote calls and this is not a good thing
                var p = session.Load<Product>("products/1-A");
                var c = session.Load<Category>(p.Category);

                //1) Find a document with the key: products/1-A
                //2) Read its Category property value.
                //3) Find a document with that key.
                //4) Send both documents back to the client.
                var pp = session.Include<Product>(x => x.Category)
                                .Load("products/1-A");

                var cc = session.Load<Category>(p.Category);
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
    public class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
   
}
