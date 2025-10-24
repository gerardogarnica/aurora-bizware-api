namespace Aurora.Common.Application.Time;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
    DateOnly Today { get; }
}