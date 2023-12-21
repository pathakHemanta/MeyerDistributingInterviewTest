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
            // Bug Fix 2: Orders from previous customers existed in the OrderRepository
            // Checks the repository for every customer and clears it before creating any order
            if (_orderRepository.Get().Count != 0)
            {
                foreach(var item in _orderRepository.Get())
                {
                    _orderRepository.Remove(item);
                }
            }

            _orderRepository.Add(order);
            
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

           foreach (var  orders in GetOrders())
            {
                foreach(var order in orders.Products)
                {
                    //Console.WriteLine($"{order.Product.GetProductNumber()} : {order.Product.GetSellingPrice()}");
                   totalSales += order.Product.GetSellingPrice();
                }
            }
           
           //Console.WriteLine($"totalSales: {totalSales}");

           return totalSales;
           // throw new NotImplementedException();
        }

        public float GetTotalReturns()
        {
            float totalReturns = 0;

            foreach (var returns in GetReturns())
            {
                foreach(var eachReturn in returns.ReturnedProducts)
                {
                    totalReturns = eachReturn.OrderProduct.Product.GetSellingPrice();
                }
            }
            
            return totalReturns;
           // throw new NotImplementedException();
        }

        public float GetTotalProfit()
        {
            float totalProfit = 0;

            totalProfit = GetTotalSales() - GetTotalReturns();

            return totalProfit;
            //throw new NotImplementedException();
        }
    }
}
