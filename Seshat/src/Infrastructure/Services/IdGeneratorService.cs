using System;
using IdGen;
using Seshat.Application.Common.Interfaces;

namespace Seshat.Infrastructure.Services
{
    public class IdGeneratorService : IUniqueIdGenerator
    {
        private readonly IdGenerator _generator;

        public IdGeneratorService()
        {
            var epoch = new DateTime(2021, 9, 18, 0, 0, 0, DateTimeKind.Utc);
            var options = new IdGeneratorOptions(timeSource: new DefaultTimeSource(epoch));
            
            _generator = new IdGenerator(0, options);
        }

        public string CreateId()
        {
            return _generator.CreateId().ToString();
        }
    }
}