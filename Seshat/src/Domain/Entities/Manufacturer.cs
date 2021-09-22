using System.Collections.Generic;
using JetBrains.Annotations;
using Seshat.Domain.Common;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class Manufacturer : PersistentEntity, IPublicEntity
    {
        private int _id;

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
            if (obj.GetType() != GetType()) return false;
            return Equals((Manufacturer) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _id;
        }
    }
}