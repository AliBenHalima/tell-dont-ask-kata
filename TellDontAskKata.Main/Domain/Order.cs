using System.Collections.Generic;
using TellDontAskKata.Main.UseCase.Exceptions;
using TellDontAskKata.Main.UseCase.Requests;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public IList<OrderItem> Items { get; set; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; set; }

        public Order SetOrder(Product product, SellItemRequest itemRequest)
        {
            var unitaryTax = Round(CalculateUnitTax(product.Price, product.Category.TaxPercentage));
            var unitaryTaxedAmount = Round(CalculateSum(product.Price, unitaryTax));
            var taxedAmount = Round(CalculateMultiply(unitaryTaxedAmount, itemRequest.Quantity));
            var taxAmount = Round(CalculateMultiply(unitaryTax, itemRequest.Quantity));

            var orderItem = new OrderItem
            {
                Product = product,
                Quantity = itemRequest.Quantity,
                Tax = taxAmount,
                TaxedAmount = taxedAmount
            };
            this.Items.Add(orderItem);
            this.Total += taxedAmount;
            this.Tax += taxAmount;
            return this;
        }

        public Order SetApprovedOrder(OrderApprovalRequest request)
        {
            if (this.Status == OrderStatus.Shipped)
            {
                throw new ShippedOrdersCannotBeChangedException();
            }

            if (request.Approved && this.Status == OrderStatus.Rejected)
            {
                throw new RejectedOrderCannotBeApprovedException();
            }

            if (!request.Approved && this.Status == OrderStatus.Approved)
            {
                throw new ApprovedOrderCannotBeRejectedException();
            }

            this.Status = request.Approved ? OrderStatus.Approved : OrderStatus.Rejected;

            return this;
        }


        public Order SetShippedOrder()
        {

            if (this.Status == OrderStatus.Created || this.Status == OrderStatus.Rejected)
            {
                throw new OrderCannotBeShippedException();
            }

            if (this.Status == OrderStatus.Shipped)
            {
                throw new OrderCannotBeShippedTwiceException();
            }
            return this;

        }
        private static decimal Round(decimal amount)
        {
            return decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
        }
        private static decimal CalculateUnitTax(decimal price, decimal percentage)
        {
            return (price / 100m) * percentage;
        }
        private static decimal CalculateSum(decimal priceOne, decimal priceTwo)
        {
            return priceOne + priceTwo;
        }
        private static decimal CalculateMultiply(decimal priceOne, decimal priceTwo)
        {
            return priceOne * priceTwo;
        }
    }
}
