namespace SurvStation.Domain.Models.Forms;

// [Table("form_submissions")]

public class FormSubmission<TKey> : BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
{
    public long Index { get; set; }
    // [Required]
    // [Column("_index")]
    public TKey FormVersionId { get; set; }
    // [Required]
    // [Column("form_id")]
    public TKey FormId { get; set; }
    // [Column("archived_at")]
    public DateTime? ArchivedAt { get; set; }
    // [ForeignKey(nameof(FormVersionId))]
    public virtual FormVersion<TKey>? FormVersion { get; set; }
    // [ForeignKey(nameof(FormId))]
    public virtual FormDefinition<TKey>? Form { get; set; }
    public virtual IList<FormSubmissionResponse<TKey>> Responses { get; set; } = [];
}
