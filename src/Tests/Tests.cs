using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurants.Corp.Core.BusinessLogic.Concrete;
using Restaurants.Corp.Core.BusinessLogic.Interfaces;
using Restaurants.Corp.Core.Dtos;
using Restaurants.Corp.Core.Restaurants;
using Restaurants.Corp.Dal;
using Restaurants.Corp.Domain;
using Restaurants.Corp.Domain.Messaging;
using Restaurants.Corp.Domain.StockOrders;
using Stock.Supplier.Dispatcher;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        Mock<IPublisher> _mockPubliher;
        private Order _fakeOrder;

        [TestInitialize]
        public void Setup() {

            _mockPubliher = new Mock<IPublisher>();
            SetUpSingleFakeOrder();
        }

        private void SetUpSingleFakeOrder()
        {
            var orderItems = new List<OrderItem>
            {
                CreateFakeOrderItem(3),
                CreateFakeOrderItem(3),
                CreateFakeOrderItem(2),
            };
            _fakeOrder = CreateFakeOrder(DateTime.Now, orderItems);
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Repository_GetRecentOrdersForRestaurantByCorrelationId_ShouldReturnOrdersFromRestaurantX()
        {
            var repo = new OrderRepository();
            var result = repo.GetRecentOrdersForRestaurantByCorrelationId(Guid.Parse("d6366e9d-c6af-4ca2-b0ea-e5e5d5737bb3")); //c0f57ed8-b528-4bfc-b3ca-dd2c27b8c313

            Assert.IsNotNull(result);
            foreach (var item in result)
                Assert.AreEqual(1, item.RestaurantId);
            
        }
        [TestMethod]
        [TestCategory("Fast")]
        public void StockQuanity_PlaceStockOrder_ShouldOrderTheRightAmountOfEggsToMakeTiramisu()
        {
            // arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(x => x.GetOrderByCorrelationId(It.IsAny<Guid>()))
                .Returns(_fakeOrder);

            IStockQuantityControl sut = new StockQuantityControl();
            var orderingProcess = new StockOrderingProcess(mockRepo.Object, sut, _mockPubliher.Object);

            var mockMechanism = new Mock<IStockOrderMechanism>();

            orderingProcess.OrderMechanism = mockMechanism.Object;

            var msg = new Message
            {
                CorrelationId = Guid.NewGuid()
            };

            //act
            orderingProcess.UpdateStock(msg);

            // assert
            mockMechanism.Verify(x => x.PlaceStockOrder(It.Is<RestaurantStock>(y => y.Eggs == 6)), Times.Once);
        }

        [TestMethod]
        [TestCategory("Fast")]
        public void StockControlNotification_Upate_ShouldSetQuantityOfStockSentByTheWarehouseForEachMonth()
        {
            // arrange
            var fakeOrderData = GetFakeOrderData();
            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(x => x.GetRecentOrdersForRestaurantByCorrelationId(It.IsAny<Guid>()))
                .Returns(fakeOrderData);

            IStockQuantityControl sut = new StockQuantityControl();
            var stockControlNotification = new StockControlNotification(mockRepo.Object, sut);

            var mockRestaurant = new Mock<IRestaurant>();

            stockControlNotification.Restaurant = mockRestaurant.Object;

            var msg = new Message {
                CorrelationId = Guid.NewGuid(),
                DataAsString = "ManualOrder"
            };

            //act
            stockControlNotification.Update(msg);

            // assert
            mockRestaurant.Verify(x => x.SetStockQuantity(It.Is<RestaurantStock>(y => y.Eggs == 33)), Times.Once);
        }

        [TestMethod]
        [TestCategory("Fast")]
        public void StockQuanity_PlaceStockOrder_ShouldCreateJsonFileWithDataOfStockOrdered()
        {
            // arrange
            var fakeOrderData = GetFakeOrderData();
            var mockRepo = new Mock<IRepository>();

            mockRepo
                .Setup(x => x.GetOrderByCorrelationId(It.IsAny<Guid>()))
                .Returns(_fakeOrder);

            IStockQuantityControl sut = new StockQuantityControl();
            var orderingProcess = new StockOrderingProcess(mockRepo.Object, sut, _mockPubliher.Object);

            var mockMechanism = new Mock<IStockOrderMechanism>();

            orderingProcess.OrderMechanism = new ManualOrder();

            var msg = new Message
            {
                CorrelationId = Guid.NewGuid()
            };

            //act
            orderingProcess.UpdateStock(msg);

            // assert
            var result = System.IO.File.Exists("c:\\temp\\stockorder.json");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("Fast")]
        public void Restaurant_SetStockQuantity_ShouldCreateJsonFileWithDataOfStockShipped()
        {
            // arrange
            var fakeOrderData = GetFakeOrderData();
            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(x => x.GetRecentOrdersForRestaurantByCorrelationId(It.IsAny<Guid>()))
                .Returns(fakeOrderData);

            IStockQuantityControl sut = new StockQuantityControl();
            var stockControlNotification = new StockControlNotification(mockRepo.Object, sut);

            stockControlNotification.Restaurant = new RestaurantX();

            var msg = new Message
            {
                CorrelationId = Guid.NewGuid()
            };

            //act
            stockControlNotification.Update(msg);

            // assert
            var result = System.IO.File.Exists("c:\\temp\\stockorder.json");
            Assert.IsTrue(result);
        }

        #region Setup Fake Data
        private List<Order> GetFakeOrderData()
        {
            const int bruschetta = 1;
            const int margherita = 2;
            const int tiramisu = 3;

            return new List<Order> {
                CreateFakeOrder(DateTime.Now,
                    new List<OrderItem> {
                        CreateFakeOrderItem(bruschetta),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(tiramisu)
                    }
                ),
                CreateFakeOrder(DateTime.Now.AddDays(-1),
                    new List<OrderItem> {
                        CreateFakeOrderItem(bruschetta),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(tiramisu)
                    }
                ),
                CreateFakeOrder(DateTime.Now.AddDays(-2),
                    new List<OrderItem> {
                        CreateFakeOrderItem(bruschetta),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(bruschetta)
                    }
                ),
                CreateFakeOrder(DateTime.Now.AddDays(-2),
                    new List<OrderItem> {
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(tiramisu),
                        CreateFakeOrderItem(tiramisu)
                    }
                ),
                CreateFakeOrder(DateTime.Now.AddDays(-2),
                    new List<OrderItem> {
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(margherita),
                        CreateFakeOrderItem(margherita)
                    }
                )
            };
        }
        private Order CreateFakeOrder(DateTime date, List<OrderItem> items)
        {
            return new Order
            {
                Created = date,
                CorrelationId = Guid.NewGuid(),
                Items = items,
                RestaurantId = 1
            };
        }

        private OrderItem CreateFakeOrderItem(int menuItem)
        {
            return new OrderItem
            {
                MenuItem = menuItem
            };
        }
        #endregion

    }
}
