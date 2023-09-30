﻿using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Repository;
using TellDontAskKata.Main.UseCase.Exceptions;
using TellDontAskKata.Main.UseCase.Requests;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderApprovalUseCase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderApprovalUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Run(OrderApprovalRequest request)
        {
            var order = _orderRepository.GetById(request.OrderId);
            order = order.SetApprovedOrder(request);
            _orderRepository.Save(order);
        }
    }
}
