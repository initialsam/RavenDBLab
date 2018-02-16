using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBLab
{
    public static class DocumentStoreHolder
    {
        /*
        Removed ConnectionStringName
        As the configuration system has been changed in .NET Core,
        we removed the ConnectionStringName property. 
        Instead you can use the .NET core configuration mechanism, 
        retrieve the connection string entry from appsettings.json, 
        convert it, and manually set Urls and Database properties.
        */
        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Urls = new string[] { "http://localhost:8080" },
                    Database = "Northwind"
                };

                return store.Initialize();
            });

        public static IDocumentStore Store =>
            LazyStore.Value;
    }
}
