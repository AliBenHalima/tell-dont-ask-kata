using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Repository;

namespace TellDontAskKata.Main.UseCase.Implementations.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public IList<Order> Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Order GetById(int orderId)
        {
           return Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void Save(Order order)
        {
            Orders.Add(order);
        }
    }
}
