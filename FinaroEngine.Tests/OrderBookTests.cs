using FinaroEngine.Library;
using System;
using Xunit;

namespace FinaroEngine.xUnit
{
    public class OrderBookTests
    {
        [Fact]
        public void Test_Orders_On_Book_1()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act
            orderBook.Add(new SpreadData(6, 40, Side.Plus, new DateTime(2018, 12, 10, 11, 0, 0)));
            orderBook.Add(new SpreadData(6.5, 75, Side.Plus, new DateTime(2018, 12, 11, 6, 0, 0)));
            orderBook.Add(new SpreadData(6.5, 120, Side.Plus, new DateTime(2018, 12, 11, 7, 0, 0)));
            orderBook.Add(new SpreadData(5.5, 40, Side.Plus, new DateTime(2018, 12, 11, 12, 0, 0)));

            orderBook.Add(new SpreadData(-5, 20, Side.Minus, new DateTime(2018, 12, 10, 9, 0, 0)));
            orderBook.Add(new SpreadData(-4.5, 100, Side.Minus, new DateTime(2018, 12, 11, 7, 0, 0)));
            orderBook.Add(new SpreadData(-5, 55, Side.Minus, new DateTime(2018, 12, 11, 8, 0, 0)));
            orderBook.Add(new SpreadData(-5.5, 60, Side.Minus, new DateTime(2018, 12, 11, 10, 0, 0)));

            //Assert
            Assert.True(orderBook.Book1.Count == 3);

            Assert.True(orderBook.Book1[5.5].AllOrders[0].Spread == 5.5);
            Assert.True(orderBook.Book1[5.5].AllOrders[0].WagerAmount == 0);
            Assert.True(DateCompare(orderBook.Book1[5.5].AllOrders[0].OrderPlacedOn, "12/11/2018 12:00:00 PM"));
            Assert.True(orderBook.Book1[5.5].AllOrders[0].Status == Status.Filled);

            Assert.True(orderBook.Book1[6].AllOrders[0].Spread == 6);
            Assert.True(orderBook.Book1[6].AllOrders[0].WagerAmount == 40);
            Assert.True(DateCompare(orderBook.Book1[6].AllOrders[0].OrderPlacedOn, "12/10/2018 11:00:00 AM"));
            Assert.True(orderBook.Book1[6].AllOrders[0].Status == Status.Open);

            Assert.True(orderBook.Book1[6.5].AllOrders[0].Spread == 6.5);
            Assert.True(orderBook.Book1[6.5].AllOrders[0].WagerAmount == 75);
            Assert.True(DateCompare(orderBook.Book1[6.5].AllOrders[0].OrderPlacedOn, "12/11/2018 6:00:00 AM"));
            Assert.True(orderBook.Book1[6.5].AllOrders[0].Status == Status.Open);

            Assert.True(orderBook.Book1[6.5].AllOrders[1].Spread == 6.5);
            Assert.True(orderBook.Book1[6.5].AllOrders[1].WagerAmount == 120);
            Assert.True(DateCompare(orderBook.Book1[6.5].AllOrders[1].OrderPlacedOn, "12/11/2018 7:00:00 AM"));
            Assert.True(orderBook.Book1[6.5].AllOrders[1].Status == Status.Open);

        }

        [Fact]
        public void Test_Orders_On_Book_2()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act
            orderBook.Add(new SpreadData(6, 40, Side.Plus, new DateTime(2018, 12, 10, 11, 0, 0)));
            orderBook.Add(new SpreadData(6.5, 75, Side.Plus, new DateTime(2018, 12, 11, 6, 0, 0)));
            orderBook.Add(new SpreadData(6.5, 120, Side.Plus, new DateTime(2018, 12, 11, 7, 0, 0)));
            orderBook.Add(new SpreadData(5.5, 40, Side.Plus, new DateTime(2018, 12, 11, 12, 0, 0)));

            orderBook.Add(new SpreadData(-5, 20, Side.Minus, new DateTime(2018, 12, 10, 9, 0, 0)));
            orderBook.Add(new SpreadData(-4.5, 100, Side.Minus, new DateTime(2018, 12, 11, 7, 0, 0)));
            orderBook.Add(new SpreadData(-5, 55, Side.Minus, new DateTime(2018, 12, 11, 8, 0, 0)));
            orderBook.Add(new SpreadData(-5.5, 60, Side.Minus, new DateTime(2018, 12, 11, 10, 0, 0)));

            //Assert
            Assert.True(orderBook.Book2.Count == 3);

            Assert.True(orderBook.Book2[-5.5].AllOrders[0].Spread == -5.5);
            Assert.True(orderBook.Book2[-5.5].AllOrders[0].WagerAmount == 20);
            Assert.True(DateCompare(orderBook.Book2[-5.5].AllOrders[0].OrderPlacedOn, "12/11/2018 10:00:00 AM"));
            Assert.True(orderBook.Book2[-5.5].AllOrders[0].Status == Status.Partial);

            Assert.True(orderBook.Book2[-5].AllOrders[0].Spread == -5);
            Assert.True(orderBook.Book2[-5].AllOrders[0].WagerAmount == 20);
            Assert.True(DateCompare(orderBook.Book2[-5].AllOrders[0].OrderPlacedOn, "12/10/2018 9:00:00 AM"));
            Assert.True(orderBook.Book2[-5].AllOrders[0].Status == Status.Open);

            Assert.True(orderBook.Book2[-5].AllOrders[1].Spread == -5);
            Assert.True(orderBook.Book2[-5].AllOrders[1].WagerAmount == 55);
            Assert.True(DateCompare(orderBook.Book2[-5].AllOrders[1].OrderPlacedOn, "12/11/2018 8:00:00 AM"));
            Assert.True(orderBook.Book2[-5].AllOrders[1].Status == Status.Open);

            Assert.True(orderBook.Book2[-4.5].AllOrders[0].Spread == -4.5);
            Assert.True(orderBook.Book2[-4.5].AllOrders[0].WagerAmount == 100);
            Assert.True(DateCompare(orderBook.Book2[-4.5].AllOrders[0].OrderPlacedOn, "12/11/2018 7:00:00 AM"));
            Assert.True(orderBook.Book2[-4.5].AllOrders[0].Status == Status.Open);

        }

        [Fact]
        public void Test_Orders_Two_Matching_Orders_For_Fill_Status()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act
            orderBook.Add(new SpreadData(6.5, 40, Side.Plus, new DateTime(2018, 12, 10, 10, 0, 0)));
            orderBook.Add(new SpreadData(-6.5, 40, Side.Minus, new DateTime(2018, 12, 10, 11, 0, 0)));

            //Assert
            Assert.True(orderBook.Book1[6.5].AllOrders[0].Spread == 6.5);
            Assert.True(orderBook.Book1[6.5].AllOrders[0].WagerAmount == 0);
            Assert.True(DateCompare(orderBook.Book1[6.5].AllOrders[0].OrderPlacedOn, "12/10/2018 10:00:00 AM"));
            Assert.True(orderBook.Book1[6.5].AllOrders[0].Status == Status.Filled);

            Assert.True(orderBook.Book2[-6.5].AllOrders[0].Spread == -6.5);
            Assert.True(orderBook.Book2[-6.5].AllOrders[0].WagerAmount == 0);
            Assert.True(DateCompare(orderBook.Book2[-6.5].AllOrders[0].OrderPlacedOn, "12/10/2018 11:00:00 AM"));
            Assert.True(orderBook.Book2[-6.5].AllOrders[0].Status == Status.Filled);
        }

        [Fact]
        public void Test_Orders_Two_Matching_Orders_And_One_Open_For_Fill_Status()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act
            orderBook.Add(new SpreadData(12, 123, Side.Plus, new DateTime(2018, 12, 13, 21, 33, 0)));
            orderBook.Add(new SpreadData(-12, 123, Side.Minus, new DateTime(2018, 12, 13, 21, 34, 0)));
            orderBook.Add(new SpreadData(-12, 123, Side.Minus, new DateTime(2018, 12, 13, 21, 35, 0)));

            //Assert
            Assert.True(orderBook.Book1[12].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book1[12].AllOrders[0].Status == Status.Filled);

            Assert.True(orderBook.Book2[-12].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book2[-12].AllOrders[0].Status == Status.Filled);

            Assert.True(orderBook.Book2[-12].AllOrders[1].WagerAmount == 123);
            Assert.True(orderBook.Book2[-12].AllOrders[1].Status == Status.Open);
        }

        [Fact]
        public void Test_Orders_Two_Matching_Orders_And_Then_Add_Two_More_Fills_For_Fill_Status()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act
            orderBook.Add(new SpreadData(12, 123, Side.Plus, new DateTime(2018, 12, 13, 21, 33, 0)));
            orderBook.Add(new SpreadData(-12, 123, Side.Minus, new DateTime(2018, 12, 13, 21, 34, 0)));
            orderBook.Add(new SpreadData(-12, 123, Side.Minus, new DateTime(2018, 12, 13, 21, 35, 0)));
            orderBook.Add(new SpreadData(12, 123, Side.Plus, new DateTime(2018, 12, 13, 21, 36, 0)));

            //Assert
            Assert.True(orderBook.Book1[12].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book1[12].AllOrders[0].Status == Status.Filled);

            Assert.True(orderBook.Book2[-12].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book2[-12].AllOrders[0].Status == Status.Filled);

            Assert.True(orderBook.Book2[-12].AllOrders[1].WagerAmount == 0);
            Assert.True(orderBook.Book2[-12].AllOrders[1].Status == Status.Filled);

            Assert.True(orderBook.Book1[12].AllOrders[1].WagerAmount == 0);
            Assert.True(orderBook.Book1[12].AllOrders[1].Status == Status.Filled);
        }

        [Fact]
        public void Test_Orders_Two_Opens_Orders_Get_Lifted_By_One_Then_Add_Another()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act
            orderBook.Add(new SpreadData(-12, 100, Side.Minus, new DateTime(2018, 12, 13, 21, 33, 0)));
            orderBook.Add(new SpreadData(-12, 100, Side.Minus, new DateTime(2018, 12, 13, 21, 34, 0)));
            orderBook.Add(new SpreadData(12, 200, Side.Plus, new DateTime(2018, 12, 13, 21, 35, 0)));
            orderBook.Add(new SpreadData(-12, 100, Side.Minus, new DateTime(2018, 12, 13, 21, 34, 0)));

            //Assert
            Assert.True(orderBook.Book2[-12].AllOrders[2].WagerAmount == 100);
            Assert.True(orderBook.Book2[-12].AllOrders[2].Status == Status.Open);
        }

        [Fact]
        public void Test_Orders_Where_Spreads_Are_Greater_Than_Or_Equal_To()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act            
            orderBook.Add(new SpreadData(-13, 100, Side.Minus, new DateTime(2018, 12, 13, 18, 34, 0)));
            orderBook.Add(new SpreadData(-12.8, 100, Side.Minus, new DateTime(2018, 12, 13, 19, 34, 0)));
            orderBook.Add(new SpreadData(-12.7, 100, Side.Minus, new DateTime(2018, 12, 13, 20, 34, 0)));
            orderBook.Add(new SpreadData(-12.5, 100, Side.Minus, new DateTime(2018, 12, 13, 21, 34, 0)));
            orderBook.Add(new SpreadData(-12.2, 100, Side.Minus, new DateTime(2018, 12, 13, 22, 34, 0)));
            orderBook.Add(new SpreadData(12.5, 1000, Side.Plus, new DateTime(2018, 12, 13, 23, 34, 0)));

            //Assert
            Assert.True(orderBook.Book1[12.5].AllOrders[0].WagerAmount == 600);
            Assert.True(orderBook.Book1[12.5].AllOrders[0].Status == Status.Partial);
            Assert.True(orderBook.Book2[-13].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book2[-13].AllOrders[0].Status == Status.Filled);
            Assert.True(orderBook.Book2[-12.8].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book2[-12.8].AllOrders[0].Status == Status.Filled);
            Assert.True(orderBook.Book2[-12.7].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book2[-12.7].AllOrders[0].Status == Status.Filled);
            Assert.True(orderBook.Book2[-12.5].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book2[-12.5].AllOrders[0].Status == Status.Filled);
        }

        [Fact]
        public void Test_Orders_Where_Spreads_Are_Less_Than_Or_Equal_To()
        {
            //Arrange
            var orderBook = new OrderBook();

            //Act            
            orderBook.Add(new SpreadData(14, 100, Side.Plus, new DateTime(2018, 12, 13, 18, 34, 0)));
            orderBook.Add(new SpreadData(14.5, 100, Side.Plus, new DateTime(2018, 12, 13, 19, 34, 0)));
            orderBook.Add(new SpreadData(15, 100, Side.Plus, new DateTime(2018, 12, 13, 20, 34, 0)));
            orderBook.Add(new SpreadData(15.1, 100, Side.Plus, new DateTime(2018, 12, 13, 21, 34, 0)));
            orderBook.Add(new SpreadData(16, 100, Side.Plus, new DateTime(2018, 12, 13, 22, 34, 0)));
            orderBook.Add(new SpreadData(-16, 450, Side.Minus, new DateTime(2018, 12, 13, 23, 34, 0)));

            //Assert
            Assert.True(orderBook.Book1[14].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book1[14].AllOrders[0].Status == Status.Filled);
            Assert.True(orderBook.Book1[14.5].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book1[14.5].AllOrders[0].Status == Status.Filled);
            Assert.True(orderBook.Book1[15].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book1[15].AllOrders[0].Status == Status.Filled);
            Assert.True(orderBook.Book1[15.1].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book1[15.1].AllOrders[0].Status == Status.Filled);
            Assert.True(orderBook.Book1[16].AllOrders[0].WagerAmount == 50);
            Assert.True(orderBook.Book1[16].AllOrders[0].Status == Status.Partial);
            Assert.True(orderBook.Book2[-16].AllOrders[0].WagerAmount == 0);
            Assert.True(orderBook.Book2[-16].AllOrders[0].Status == Status.Filled);

        }


        public bool DateCompare(DateTime dt1, string dt2)
        {
            return DateTime.Parse(dt1.ToString()) == DateTime.Parse(dt2);
        }
    }
}
