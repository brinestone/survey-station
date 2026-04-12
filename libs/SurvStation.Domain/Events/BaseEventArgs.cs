namespace SurvStation.Domain.Events;

public abstract class BaseEventArgs : EventArgs
{
    public DateTime RecordedAt { get; } = DateTime.UtcNow;
}