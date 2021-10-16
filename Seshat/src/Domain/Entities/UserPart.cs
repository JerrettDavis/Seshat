using JetBrains.Annotations;
using Seshat.Domain.Common;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class UserPart : PersistentEntity, IPublicEntity, IPurchasable
    {
        private int _id;

        private UserPart(int id, string nickname)
        {
            _id = id;
            Nickname = nickname;
        }


        public UserPart(string nickname, Part part)
        {
            Nickname = nickname;
            Part = part;
        }

        public string  Nickname { get; set; }
        public string? Summary { get; set; }

        public string PublicIdentifier { get; } = null!;

        public Part Part { get; set; } = null!;
        
        
        protected bool Equals(UserPart other)
        {
            return _id > 0 && _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UserPart) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _id;
        }

        public decimal? PurchasePrice { get; set; }
        public string? PurchaseUrl { get; set; }
    }
}