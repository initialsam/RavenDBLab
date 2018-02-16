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
                var p = session.Load<dynamic>("products/1-A");
                System.Console.WriteLine(p.Name);
            }
        }
    }
}
