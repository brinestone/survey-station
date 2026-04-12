namespace SurvStation.Domain.Models.Forms;

// [Table("form_version_items")]
// [PrimaryKey(nameof(Key), nameof(FormId), nameof(FormVersionId))]
// [Index(nameof(FormId))]
// [Index(nameof(ItemId))]
public class FormVersionItem<TKey> : BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
{
    public TKey? ItemId { get; set; }
    public TKey FormId { get; set; }
    public TKey FormVersionId { get; set; }
    public TKey? ParentId { get; set; }
    // [ForeignKey($"{nameof(ParentId)},{nameof(FormId)},{nameof(FormVersionId)},{nameof(ItemId)}")]
    public virtual FormVersionItem<TKey>? Parent { get; set; } = null;
    // [ForeignKey(nameof())]
    public virtual FormVersion<TKey>? FormVersion { get; set; } = null;
    public virtual FormDefinition<TKey>? Form { get; set; }
    public virtual FormItem<TKey>? FormItem { get; set; }
    public string? Path { get; set; }
    public string? MetaTag { get; set; }
    public FormItemRelevance? Relevance { get; set; }
    public Dictionary<string, object>? Config { get; set; }
}
