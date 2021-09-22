using AutoMapper;
using Seshat.Application.Common.Mappings;
using Seshat.Domain.Entities;

namespace Seshat.Application.TodoLists.Queries.GetTodos
{
    public class TodoItemDto : IMapFrom<TodoItem>
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; } = null!;

        public bool Done { get; set; }

        public int Priority { get; set; }

        public string? Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int) s.Priority));
        }
    }
}