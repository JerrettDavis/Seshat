using System.Collections.Generic;

namespace Seshat.Application.TodoLists.Queries.GetTodos
{
    public class TodosVm
    {
        public IList<PriorityLevelDto> PriorityLevels { get; init; } = null!;

        public IList<TodoListDto> Lists { get; init; } = null!;
    }
}