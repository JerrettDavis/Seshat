namespace Seshat.Domain.Common
{
    public interface IPublicEntity
    {
        string PublicIdentifier { get; }
        bool IsEntity(string publicIdentifier) => PublicIdentifier.Equals(publicIdentifier);
    }
}