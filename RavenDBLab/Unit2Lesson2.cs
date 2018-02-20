using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using RavenDBLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBLab
{
    public class Unit2Lesson2
    {
        public static void CreateIndex()
        {
            var store = DocumentStoreHolder.Store;
            new Employees_ByFirstAndLastNameUseAPI().Execute(store);
        }

        public static void Exercise1()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Query<Employee, Employees_ByFirstAndLastNameUseAPI>()
                    .Where(x => x.FirstName == "Robert")
                    .ToList();

                foreach (var employee in results)
                {
                    Console.WriteLine($"{employee.LastName}, {employee.FirstName}");
                }
            }
        }
    }
    public class Employees_ByFirstAndLastNameUseAPI : AbstractIndexCreationTask<Employee>
    {
        public Employees_ByFirstAndLastNameUseAPI()
        {
            Map = (employees) =>
                from employee in employees
                select new
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName
                };
        }
    }
}
