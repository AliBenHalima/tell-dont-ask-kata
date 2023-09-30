using System.Collections.Generic;
using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Main.Interfaces.Repository
{
    public interface IOrderRepository
    {
        public IList<Order> Orders { get; set; }
        void Save(Order order);

        Order GetById(int orderId);
    }
}
