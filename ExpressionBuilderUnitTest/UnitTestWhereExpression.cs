using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using ExpressionBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionBuilderUnitTest
{
    [TestClass]
    public class UnitTestWhereExpression
    {
        [TestMethod]
        public void TestAddWhereExpression()
        {
            var data = TestData.GetTestDataTodoItemList();
            var query = data.AsQueryable();
            var q = query.Where(p => p.TodoList.UserId.Contains("U")).ToString();

            var query1 = data.AsQueryable();
            var q1 = WhereExpression.AddWhereExpression(query1, "TodoList.UserId", "*", "U").ToString();

            Assert.AreEqual(q, q1);
        }

        [TestMethod]
        public void TestAddWhereExpressionOptionList()
        {
            var data = TestData.GetTestDataTodoItemList();
            var query = data.AsQueryable();
            var q = query
                .Where(p => p.TodoList.UserId.Contains("U"))
                .Where(p => p.TodoItemId >= 2).ToString();

            var query1 = data.AsQueryable();
            var options = new List<WhereExpression.WhereOption>()
                              {
                                  new WhereExpression.WhereOption()
                                      {PropertyPath = "TodoList.UserId", Predicate = "*", Value = "U"},
                                  new WhereExpression.WhereOption()
                                      {PropertyPath = "TodoItemId", Predicate = ">=", Value = "2"}
                              };
            var q1 = WhereExpression.AddWhereExpression(query1, options).ToString();

            Assert.AreEqual(q, q1);
        }

        [TestMethod]
        public void TestAddWhereExpressionInt()
        {
            var data = TestData.GetTestDataTodoItemList();
            var query = data.AsQueryable();
            var q = query.Where(p => p.TodoItemId >= 2).ToString();

            var query1 = data.AsQueryable();
            var q1 = WhereExpression.AddWhereExpression(query1, "TodoItemId", ">=", "2").ToString();

            Assert.AreEqual(q, q1);
        }

        [TestMethod]
        public void TestAddWhereExpressionBoolean()
        {
            var data = TestData.GetTestDataTodoItemList();
            var query = data.AsQueryable();
            var q = query.Where(p => p.IsDone == true).ToString();

            var query1 = data.AsQueryable();
            var q1 = WhereExpression.AddWhereExpression(query1, "IsDone", "=", "true").ToString();

            Assert.AreEqual(q, q1);
        }
    }
}
