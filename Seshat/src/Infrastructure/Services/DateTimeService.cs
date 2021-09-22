using Seshat.Application.Common.Interfaces;
using System;

namespace Seshat.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}