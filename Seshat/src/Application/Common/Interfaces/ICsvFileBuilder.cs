using Seshat.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace Seshat.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}