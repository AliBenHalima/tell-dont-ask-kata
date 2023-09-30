using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Repository;

namespace TellDontAskKata.Main.UseCase.Implementations.Repositories
{
    public class ProductCatalogRepository : IProductCatalogRepository
    {
        private IList<Product> _products = new List<Product>();

        public Product GetByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name == name);
        }
    }
}
