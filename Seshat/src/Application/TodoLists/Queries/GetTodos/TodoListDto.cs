using Seshat.Application.Common.Mappings;
using Seshat.Domain.Entities;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Seshat.Application.TodoLists.Queries.GetTodos
{
    [PublicAPI]
    public class TodoListDto : IMapFrom<TodoList>
    {
        public TodoListDto()
        {
            Items = new List<TodoItemDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Colour { get; set; } = null!;

        public IList<TodoItemDto> Items { get; set; }
    }
}