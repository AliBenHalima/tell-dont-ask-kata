using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Repository;

namespace TellDontAskKata.Tests.Doubles
{
    public class InMemoryProductCatalog : IProductCatalogRepository
    {
        private readonly IList<Product> _products;

        public InMemoryProductCatalog(IList<Product> products)
        {
            _products = products;
        }

        public Product GetByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name == name);
        }
    }
}
