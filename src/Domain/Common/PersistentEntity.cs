using System;

namespace Seshat.Domain.Common
{
    public class PersistentEntity : AuditableEntity
    {
        public DateTime? Deleted { get; set; }
    }
}