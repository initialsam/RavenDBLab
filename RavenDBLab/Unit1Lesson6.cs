using Raven.Client.Documents;
using RavenDBLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBLab
{
    public class Unit1Lesson6
    {
        public static void Exercise1()
        {
            // storing a new document
            string categoryId;
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var newCategory = new Category
                {
                    Name = "My New Category",
                    Description = "Description of the new category"
                };

                session.Store(newCategory);
                categoryId = newCategory.Id;
                session.SaveChanges();
            }

            // loading and modifying
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var storedCategory = session
                    .Load<Category>(categoryId);

                storedCategory.Name = "abcd許蓋";

                session.SaveChanges();
            }

            // deleting
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Delete(categoryId);
                session.SaveChanges();
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
