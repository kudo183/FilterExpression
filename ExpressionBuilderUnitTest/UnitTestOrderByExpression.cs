using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionBuilder;

namespace ExpressionBuilderUnitTest
{
    [TestClass]
    public class UnitTestOrderByExpression
    {
        [TestMethod]
        public void TestAddOrderByExpression()
        {
            var data = TestData.GetTestDataTodoItemList();
            var query = data.AsQueryable();
            var q = query.OrderBy(p => p.TodoList.Title).ThenByDescending(p => p.Title).ToString();

            var orderOptions = new List<OrderByExpression.OrderOption>()
                {
                    new OrderByExpression.OrderOption() { PropertyPath = "TodoList.Title",IsAscending = true},
                    new OrderByExpression.OrderOption() { PropertyPath = "Title",IsAscending = false},
                };

            var query1 = data.AsQueryable();
            var q1 = OrderByExpression.AddOrderByExpression(query1, orderOptions).ToString();

            Assert.AreEqual(q, q1);
        }
    }
}
