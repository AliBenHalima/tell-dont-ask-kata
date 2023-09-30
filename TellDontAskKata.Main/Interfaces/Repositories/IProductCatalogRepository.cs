using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Main.Interfaces.Repository
{
    public interface IProductCatalogRepository
    {
        Product GetByName(string name);
    }
}
