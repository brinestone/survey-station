namespace SurvStation.Domain.Models.Forms;

// [Table("form_versions")]
// [PrimaryKey(nameof(Key), nameof(FormId))]
public class FormVersion<TKey> : BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
{
    // [Required]
    // [Column("form")]
    public TKey FormId { get; set; }
    // [Column("parent_id")]
    // public TKey? ParentId { get; set; }
    // [Column("is_current")]
    public bool IsCurrent { get; set; } = false;
    public DateTime? ArchivedAt { get; set; }
    // [ForeignKey($"{nameof(FormId)},{nameof(ParentId)}")]
    // public virtual FormVersion<TKey> Parent { get; set; } = null!;
    // [ForeignKey(nameof(FormId))]
    public virtual FormDefinition<TKey> Form { get; set; } = null!;
    // [Many]
    public virtual IList<FormVersionItem<TKey>> Items { get; set; } = [];
}
