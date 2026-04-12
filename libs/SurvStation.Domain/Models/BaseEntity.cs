namespace SurvStation.Domain.Models;

public abstract class BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
{
    // [Column("id")]
    public TKey? Key { get; set; }
    // [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    // [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
