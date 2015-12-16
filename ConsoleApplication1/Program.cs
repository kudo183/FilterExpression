using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Data;
using ExpressionBuilder;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new System.Diagnostics.Stopwatch();
            while (true)
            {
                var data = TestData.GetTestDataTodoItemList();
                var query = data.AsQueryable();
                var q = query.Where(p => p.TodoItemId >= 2 && p.TodoList.UserId.Contains("U")).ToString();

                var query1 = data.AsQueryable();
                var options = new List<WhereExpression.WhereOption>()
                              {
                                  new WhereExpression.WhereOption()
                                      {PropertyPath = "TodoList.UserId", Predicate = "*", Value = "U"},
                                  new WhereExpression.WhereOption()
                                      {PropertyPath = "TodoItemId", Predicate = ">=", Value = "2"}
                              };
                var q1 = WhereExpression.AddWhereExpression(query1, options).ToString();

                Console.Read();
            }
        }

    }
}
