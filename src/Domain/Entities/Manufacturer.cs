using System.Collections.Generic;
using JetBrains.Annotations;
using Seshat.Domain.Common;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class Manufacturer : PersistentEntity, IPublicEntity, IHasDomainEvent
    {
#pragma warning disable CS0649
        private int _id;
#pragma warning restore CS0649

        public Manufacturer(string name)
        {
            Printers = new HashSet<Printer>();
            
            Name = name;
        }
        
        public string Name { get; }
        public string PublicIdentifier { get; private set; } = null!;
        public ICollection<Printer> Printers { get; }

        protected bool Equals(Manufacturer other)
        {
            return _id > 0 && _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Manufacturer) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _id;
        }
        
        public bool IsEntity(string publicIdentifier) => PublicIdentifier.Equals(publicIdentifier);
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}