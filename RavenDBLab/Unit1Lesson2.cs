using Raven.Client.Documents;
using RavenDBLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBLab
{
    public class Unit1Lesson2
    {
        public static void Exercise1()
        {
            var documentStore = new DocumentStore
            {
                Urls = new string[] { "http://localhost:8080" },
                Database = "Northwind"
            };

            documentStore.Initialize();
            using (var session = documentStore.OpenSession())
            {
                var p = session.Load<dynamic>("products/1-A");
                System.Console.WriteLine(p.Name);
            }

        }

        public static void Exercise2()
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
}
