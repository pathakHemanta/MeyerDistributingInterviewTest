using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;

namespace InterviewTest.Customers
{
    public abstract class CustomerBase : ICustomer
    {
        private readonly OrderRepository _orderRepository;
        private readonly ReturnRepository _returnRepository;

        protected CustomerBase(OrderRepository orderRepo, ReturnRepository returnRepo)
        {
            _orderRepository = orderRepo;
            _returnRepository = returnRepo;
        }

        public abstract string GetName();

        public void CreateOrder(IOrder order)
        {
            // Calls the method to set the date and time of order
            SetOrderDate(order);
            _orderRepository.Add(order);

        }

        // Sets the local time and date when an order is created
        public void SetOrderDate(IOrder order)
        {
            DateTime localDate = DateTime.Now;
            order.OrderDate = localDate.ToString();
        }

        public List<IOrder> GetOrders()
        {
            return _orderRepository.Get();
        }

        public void CreateReturn(IReturn rga)
        {
            _returnRepository.Add(rga);
        }

        public List<IReturn> GetReturns()
        {
            return _returnRepository.Get();
        }

        public float GetTotalSales()
        {
            float totalSales = 0;

            // Gets products from each order placed by a customer and 
            // adds their price to the total return amount
            foreach (var orders in GetOrders())
            {
                // Bug 2: Customers were not being checked while calculating total
                // Checks this customer with customers in the orders repository 
                if (orders.Customer.GetName() == GetName())
                {
                    foreach (var order in orders.Products)
                    {
                        totalSales += order.Product.GetSellingPrice();
                    }
                }
            }

            return totalSales;
        }

        public float GetTotalReturns()
        {
            float totalReturns = 0;

            // Gets products from each return placed on an order by a customer
            // and adds their price to the total return amount 
            foreach (var returns in GetReturns())
            {
                if (returns.OriginalOrder.Customer.GetName() == GetName())
                {
                    foreach (var eachReturn in returns.ReturnedProducts)
                    {
                        totalReturns += eachReturn.OrderProduct.Product.GetSellingPrice();
                    }
                }
            }

            return totalReturns;
        }

        public float GetTotalProfit()
        {
            float totalProfit;

            totalProfit = GetTotalSales() - GetTotalReturns();

            return totalProfit;
        }
    }
}
