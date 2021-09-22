using Seshat.Domain.Common;
using Seshat.Domain.Enums;
using Seshat.Domain.Events;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class TodoItem : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public TodoList List { get; set; } = null!;

        public int ListId { get; set; }

        public string Title { get; set; } = null!;

        public string? Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Reminder { get; set; }

        private bool _done;

        public bool Done
        {
            get => _done;
            set
            {
                if (value && !_done) DomainEvents.Add(new TodoItemCompletedEvent(this));

                _done = value;
            }
        }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}