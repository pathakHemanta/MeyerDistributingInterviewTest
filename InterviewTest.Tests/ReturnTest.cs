﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewTest.Orders;
using InterviewTest.Returns;
using InterviewTest.Customers;
using InterviewTest.Products;

namespace InterviewTest.Tests
{
    [TestClass]
    public class ReturnTest
    {
        private OrderRepository _orderRepository;
        private ReturnRepository _returnRepository;
        private CarDealershipCustomer customer;
        private IOrder originalOrder;

        [TestInitialize]
        public void TestInitialize()
        {
            _orderRepository = new OrderRepository();
            _returnRepository = new ReturnRepository();
            customer = new CarDealershipCustomer(_orderRepository, _returnRepository);
            originalOrder = new Order("MyOrder123", customer);
        }

        [TestMethod]
        public void NewReturnForACustomerIsStarted()
        {
            string returnNumber = "MyOrderReturn123";
            IReturn returns = new Return(returnNumber,  originalOrder);

            Assert.AreEqual("MyOrderReturn123", returns.ReturnNumber);

        }

        [TestMethod]
        public void ItemsToBeRemovedFromTheOrderAdded()
        {
            IProduct product1 = new BedLiner();
            IProduct product2 = new HitchAdapter();
            originalOrder.AddProduct(product1);
            originalOrder.AddProduct(product2);
            
            string returnNumber = "MyOrderReturn123";
            IReturn returns = new Return(returnNumber, originalOrder);

            returns.AddProduct(originalOrder.Products[1]);

            Assert.AreEqual(returns.ReturnedProducts.Count, 1);
        }

        [TestMethod]
        public void SelectedItemsRemovedFromOrder()
        {
            IProduct product1 = new BedLiner();
            IProduct product2 = new HitchAdapter();
            originalOrder.AddProduct(product1);
            originalOrder.AddProduct(product2);
            customer.CreateOrder(originalOrder);

            string returnNumber = "MyOrderReturn123";
            IReturn returns = new Return(returnNumber, originalOrder);
            returns.AddProduct(originalOrder.Products[0]);

            customer.CreateReturn(returns);

            Assert.AreEqual(customer.GetReturns().Count, 1);

        }
    }
}