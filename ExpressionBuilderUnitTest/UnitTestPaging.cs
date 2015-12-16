using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using ExpressionBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionBuilderUnitTest
{
    [TestClass]
    public class UnitTestPaging
    {
        [TestMethod]
        public void TestAddPaging()
        {
            const int pageSize = 2;
            const int pageIndex = 3;
            var data = TestData.GetTestDataTodoItemList();
            var query = data.AsQueryable();
            var q = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToString();

            var query1 = data.AsQueryable();
            int pageCount = 0;
            var q1 = Paging.AddPaging(query1, pageIndex, pageSize, out pageCount).ToString();

            Assert.AreEqual(q, q1);
        }
    }
}
