using System;

namespace OpenFeed.Services.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }

    public class FakeDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _now;

        public FakeDateTimeProvider(DateTime now)
        {
            _now = now;
        }

        public DateTime Now() => _now;
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }

    public class UtcDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
