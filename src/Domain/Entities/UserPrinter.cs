using JetBrains.Annotations;
using Seshat.Domain.Common;

namespace Seshat.Domain.Entities
{
    [PublicAPI]
    public class UserPrinter : PersistentEntity, IPublicEntity, IPurchasable
    {
        private int _id;

        private UserPrinter(int id, string nickname)
        {
            _id = id;
            Nickname = nickname;
        }

        public UserPrinter(string nickname, Printer printer)
        {
            Nickname = nickname;
            Printer = printer;
        }

        public string  Nickname { get; set; }
        public string? Summary { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string? PurchaseUrl { get; set; }

        public string PublicIdentifier { get; } = null!;

        public Printer Printer { get; set; } = null!;
         
        
        
        protected bool Equals(UserPrinter other)
        {
            return _id > 0 && _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UserPrinter) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _id;
        }
    }
}