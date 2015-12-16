using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Todo item entity
    /// </summary>
    public class TodoItem
    {
        public int TodoItemId { get; set; }

        public string Title { get; set; }
        public bool IsDone { get; set; }

        public int TodoListId { get; set; }
        public virtual TodoList TodoList { get; set; }
    }

    /// <summary>
    /// Todo list entity
    /// </summary>
    public class TodoList
    {
        public int TodoListId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public virtual List<TodoItem> Todos { get; set; }
    }

    public class TestData
    {
        public static List<TodoItem> GetTestDataTodoItemList()
        {
            var t2 = new TodoList() { TodoListId = 2, UserId = "HUY", Title = "list2" };
            var t4 = new TodoList() { TodoListId = 4, UserId = "KUDO", Title = "list4" };
            var t3 = new TodoList() { TodoListId = 3, UserId = "HUY", Title = "list3" };
            var t1 = new TodoList() { TodoListId = 1, UserId = "HUY", Title = "list1" };
            var t5 = new TodoList() { TodoListId = 5, UserId = "KUDO", Title = "list5" };
            return new List<TodoItem>()
                       {
                           new TodoItem() {IsDone = true, Title = "item1", TodoItemId = 1, TodoList = t1},
                           new TodoItem() {IsDone = false, Title = "item4", TodoItemId = 4, TodoList = t3},
                           new TodoItem() {IsDone = true, Title = "item2", TodoItemId = 2, TodoList = t1},
                           new TodoItem() {IsDone = true, Title = "item5", TodoItemId = 5, TodoList = t2},
                           new TodoItem() {IsDone = true, Title = "item6", TodoItemId = 6, TodoList = t3},
                           new TodoItem() {IsDone = false, Title = "item3", TodoItemId = 3, TodoList = t1},
                       };
        }
    }
}
