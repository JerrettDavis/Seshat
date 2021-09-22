using Seshat.Domain.Common;
using Seshat.Domain.ValueObjects;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public Colour Colour { get; set; } = Colour.White;

        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}