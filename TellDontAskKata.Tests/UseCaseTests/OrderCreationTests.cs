using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Interfaces.Repository;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Main.UseCase.Exceptions;
using TellDontAskKata.Main.UseCase.Requests;
using Xunit;

namespace TellDontAskKata.Tests.UseCaseTests
{
    public class OrderCreationTests
    {
        [Fact]
        public void Unknown_Product_Name_Should_Throw_Exception()
        {
            // Arrange

            var order = new Order
            {
                Status = OrderStatus.Created,
                Items = new List<OrderItem>(),
                Currency = "EUR",
                Total = 0m,
                Tax = 0m
            };
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var productRepositoryMock = new Mock<IProductCatalogRepository>();
            var sut = new OrderCreationUseCase(orderRepositoryMock.Object, productRepositoryMock.Object);
            // Act
            var fixture = new Fixture();
            var product = fixture.Build<Product>()
                                 .With<string>(n => n.Name, "Definetly an unknown name")
                                 .Create();

            productRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(() => null);



            var requests = fixture.Create<SellItemsRequest>();
            Action run = () => sut.Run(order, requests);
            // Assert
            Assert.Throws<UnknownProductException>(run);
        }

        [Fact]
        public void OrderCreationUseCase_Create_Order_Successfuy()
        {
            // Arrange
            Order capturedOrder = null;
            var order = new Order
            {
                Status = OrderStatus.Created,
                Items = new List<OrderItem>(),
                Currency = "EUR",
                Total = 0m,
                Tax = 0m,
                Id=1
            };

            var orderRepositoryMock = new Mock<IOrderRepository>();
            var productRepositoryMock = new Mock<IProductCatalogRepository>();
            var sut = new OrderCreationUseCase(orderRepositoryMock.Object, productRepositoryMock.Object);

            var fixture = new Fixture();
            var product = fixture.Build<Product>()
                                 .With<string>(n => n.Name, "Definetly an unknown name")
                                 .Create();

            var orders = fixture.Build<Order>()
                                 .CreateMany(10);
            // Act
            orderRepositoryMock.SetupGet(repo => repo.Orders).Returns((orders.ToList()));
            orderRepositoryMock.Setup(x => x.Save(It.IsAny<Order>())).Callback<Order>(order => capturedOrder = order);
            //orderRepositoryMock.SetupSet(repo => repo.Orders.Add(capturedOrder));

            productRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(product);

            var requests = fixture.Create<SellItemsRequest>();
            Action run = () => sut.Run(order, requests);
            run();
            orderRepositoryMock.Object.Orders.Add(capturedOrder);
            // Assert

            var savedOrder = orderRepositoryMock.Object.Orders.Where(o=>o.Id == order.Id).FirstOrDefault();
            //Assert.Same(orderRepositoryMock.Object.GetById(order.Id), order);
            Assert.Equal<int>(savedOrder.Id, capturedOrder.Id);
            orderRepositoryMock.Verify(repo => repo.Save(order), Times.Once);

        }
    }
}
