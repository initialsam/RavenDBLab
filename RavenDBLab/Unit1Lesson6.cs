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
            while (true)
            {
                Console.WriteLine("Please, press:");
                Console.WriteLine("C - Create");
                Console.WriteLine("R - Retrieve");
                Console.WriteLine("U - Update");
                Console.WriteLine("D - Delete");
                Console.WriteLine("Q - Query all contacts (limit to 128 items)");
                Console.WriteLine("Other - Exit");

                var input = Console.ReadKey();
                Console.WriteLine("\n------------");
                switch (input.Key)
                {
                    case ConsoleKey.C:
                        CreateContact();
                        break;
                    case ConsoleKey.R:
                        RetrieveContact();
                        break;
                    case ConsoleKey.U:
                        UpdateContact();
                        break;
                    case ConsoleKey.D:
                        DeleteContact();
                        break;
                    case ConsoleKey.Q:
                        QueryAllContacts();
                        break;
                    default:
                        return;
                }
                Console.WriteLine("------------");
            }
        }

        private static void CreateContact()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Console.WriteLine("Name: ");
                var name = Console.ReadLine();

                Console.WriteLine("Email: ");
                var email = Console.ReadLine();

                var c = new NewContact
                {
                    Name = name,
                    Email = email
                };

                session.Store(c);
                Console.WriteLine($"New Contact ID = {c.Id}");
                session.SaveChanges();
            }
        }

        private static void RetrieveContact()
        {
            Console.WriteLine("Enter the contact id");
            var id = Console.ReadLine();
            var key = $"NewContacts/{id}-A";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contact = session.Load<NewContact>(key);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.");
                    return;
                }

                Console.WriteLine($"Name : {contact.Name}");
                Console.WriteLine($"Email: {contact.Email}");
            }
        }

        private static void UpdateContact()
        {
            Console.WriteLine("Enter the contact id");
            var id = Console.ReadLine();
            var key = $"NewContacts/{id}-A";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contact = session.Load<NewContact>(key);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.");
                    return;
                }

                Console.WriteLine($"Actual Name : {contact.Name}");
                Console.WriteLine("New name: ");
                contact.Name = Console.ReadLine();
                Console.WriteLine($"Actual Email: {contact.Email}");
                Console.WriteLine("New email address: ");
                contact.Email = Console.ReadLine();
                session.SaveChanges();
            }
        }

        private static void DeleteContact()
        {
            Console.WriteLine("Enter the contact id");
            var id = Console.ReadLine();
            var key = $"NewContacts/{id}-A";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contact = session.Load<NewContact>(key);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.");
                    return;
                }

                session.Delete(contact);
                session.SaveChanges();
            }
        }

        private static void QueryAllContacts()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contacts = session.Query<NewContact>()
                    .ToList();

                foreach (var contact in contacts)
                {
                    Console.WriteLine($"{contact.Id} - {contact.Name}, {contact.Email}");
                }

                Console.WriteLine($"{contacts.Count} contacts found.");
            }
        }
    }

    public class NewContact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
