using JetBrains.Annotations;
using Seshat.Domain.Common;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class User : IPublicEntity
    {
#pragma warning disable CS0649
        private readonly string _id;
#pragma warning restore CS0649

        public User(string id)
        {
            _id = id;
        }

        public string PublicIdentifier { get; private set; } = null!;
        
        protected bool Equals(User other)
        {
            return !string.IsNullOrWhiteSpace(_id) && _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _id.GetHashCode();
        }
    }
}