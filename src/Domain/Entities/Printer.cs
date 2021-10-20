using JetBrains.Annotations;
using Seshat.Domain.Common;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class Printer : PersistentEntity, IPublicEntity
    {
        private int _id;

        private Printer(int id, string model)
        {
            _id = id;
            Model = model;
        }

        public Printer(
            Manufacturer manufacturer,
            string model)
        {
            Manufacturer = manufacturer;
            Model = model;
        }

        public string Model { get; set; }
        public string PublicIdentifier { get; } = null!;
        public Manufacturer Manufacturer { get; set; } = null!;

        protected bool Equals(Printer other)
        {
            return _id > 0 && _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Printer) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _id;
        }
    }
}