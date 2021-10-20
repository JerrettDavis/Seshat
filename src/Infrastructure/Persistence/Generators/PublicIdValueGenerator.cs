using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Seshat.Application.Common.Interfaces;

namespace Seshat.Infrastructure.Persistence.Generators
{
    public class PublicIdValueGenerator : ValueGenerator<string>
    {
        public override string Next(EntityEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            
            var gen = entry.Context.GetService<IUniqueIdGenerator>();
            return gen.CreateId();
        }

        public override bool GeneratesTemporaryValues => false;
    }
}