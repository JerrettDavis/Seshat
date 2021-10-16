using JetBrains.Annotations;
using Seshat.Domain.Common;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class Part : PersistentEntity, IPublicEntity
    {
#pragma warning disable CS0649
        private int _id;
#pragma warning restore CS0649

        public Part(string name, Manufacturer manufacturer)
        {
            Name = name;
            Manufacturer = manufacturer;
        }

        public string Name { get; set; }
        public string? ModelNumber { get; set; }
        public string? Description { get; set; }

        public string PublicIdentifier { get; } = null!;
        
        public Manufacturer Manufacturer { get; set; }
        
        protected bool Equals(Part other)
        {
            return _id > 0 && _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Part) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _id;
        }
    }
}