using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewTest.Orders;
using InterviewTest.Returns;
using InterviewTest.Customers;
using InterviewTest.Products;

namespace InterviewTest.Tests
{
    [TestClass]
    public class OrderTest
    {
        private OrderRepository _orderRepository;
        private ReturnRepository _returnRepository;
        private CarDealershipCustomer customer;


        [TestInitialize]
        public void TestInitialize()
        {
            _orderRepository = new OrderRepository();
            _returnRepository = new ReturnRepository();
            customer = new CarDealershipCustomer(_orderRepository, _returnRepository);
        }

        [TestMethod]
        public void NewOrderForACustomerIsStarted()
        {
            string orderNumber = "SomeOrder123";
            IOrder newOrder = new Order(orderNumber, customer);

            Assert.AreEqual("SomeOrder123", newOrder.OrderNumber);
        }

        [TestMethod]
        public void MultipleProductsAreAddedToAnOrder()
        {
            IProduct product1 = new BedLiner();
            IProduct product2 = new HitchAdapter();
            string orderNumber = "MyOrder123";

            IOrder newOrder = new Order(orderNumber, customer);
            newOrder.AddProduct(product1);
            newOrder.AddProduct(product2);

            Assert.AreEqual(newOrder.Products.Count, 2);
            Assert.AreEqual(newOrder.Products[0].Product, product1);
            Assert.AreEqual(newOrder.Products[1].Product, product2);

        }

        [TestMethod]
        public void NewOrderCreatedAndStoredInRepository()
        {
            IOrder newOrder = new Order("MyOrder123", customer);
            customer.CreateOrder(newOrder);

            Assert.AreEqual(customer.GetOrders().Count, 1);
            Assert.AreEqual(customer.GetOrders()[0].OrderNumber, newOrder.OrderNumber);

        }

    }
}
