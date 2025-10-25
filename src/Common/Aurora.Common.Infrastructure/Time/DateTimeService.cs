using Aurora.Common.Application.Time;

namespace Aurora.Common.Infrastructure.Time;

internal sealed class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateOnly Today => DateOnly.FromDateTime(UtcNow);
}