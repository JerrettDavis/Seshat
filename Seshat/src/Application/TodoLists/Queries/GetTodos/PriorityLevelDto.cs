namespace Seshat.Application.TodoLists.Queries.GetTodos
{
    public class PriorityLevelDto
    {
        public PriorityLevelDto(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int Value { get; set; }

        public string Name { get; set; }
    }
}