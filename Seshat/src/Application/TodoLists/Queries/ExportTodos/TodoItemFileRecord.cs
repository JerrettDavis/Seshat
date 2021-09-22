using Seshat.Application.Common.Mappings;
using Seshat.Domain.Entities;

namespace Seshat.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; } = null!;

        public bool Done { get; set; }
    }
}