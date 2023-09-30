using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Repository;
using TellDontAskKata.Main.Interfaces.Service;
using TellDontAskKata.Main.UseCase.Exceptions;
using TellDontAskKata.Main.UseCase.Requests;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderShipmentUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShipmentService _shipmentService;

        public OrderShipmentUseCase(
            IOrderRepository orderRepository,
            IShipmentService shipmentService)
        {
            _orderRepository = orderRepository;
            _shipmentService = shipmentService;
        }

        public void Run(OrderShipmentRequest request)
        {
            var order = _orderRepository.GetById(request.OrderId);
            order = order.SetShippedOrder();
            _shipmentService.Ship(order);
            order.Status = OrderStatus.Shipped;
            _orderRepository.Save(order);
        }
    }
}
