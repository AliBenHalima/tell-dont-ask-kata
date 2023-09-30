using System;
using System.Collections.Generic;
using System.Text;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Service;

namespace TellDontAskKata.Main.UseCase.Implementations.Services
{
    public class ShipmentService : IShipmentService
    {
        private Order _shippedOrder;
        public void Ship(Order order)
        {
            _shippedOrder = order;
        }

    }
}
