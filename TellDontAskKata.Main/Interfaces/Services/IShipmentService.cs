using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Main.Interfaces.Service
{
    public interface IShipmentService
    {
        void Ship(Order order);
    }
}
