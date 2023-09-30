using System;
using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Repository;
using TellDontAskKata.Main.UseCase.Exceptions;
using TellDontAskKata.Main.UseCase.Requests;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCatalogRepository _productCatalog;

        public OrderCreationUseCase(IOrderRepository orderRepository, IProductCatalogRepository productCatalog)
        {
            _orderRepository = orderRepository;
            _productCatalog = productCatalog;
        }

        public void Run(Order order, SellItemsRequest request)
        {
            foreach(var itemRequest in request.Requests){
                var product = _productCatalog.GetByName(itemRequest.ProductName);

                if (product is null)
                {
                    throw new UnknownProductException();
                }
                order = order.SetOrder(product, itemRequest);
            }
            _orderRepository.Save(order);
        }
    }
}
