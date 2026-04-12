namespace SurvStation.Domain.Models.Forms;

public class FormSubmissionResponse<TKey> : BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
{
    public TKey SubmissionId { get; set; }
    public TKey FormId { get; set; }
    public TKey? FormVersionId { get; set; }
    public TKey? FormItemId { get; set; }
    public byte[] Value { get; set; } = [];
    public virtual FormSubmission<TKey>? Submission { get; set; }
    public virtual FormDefinition<TKey>? Form { get; set; }
    public virtual FormVersion<TKey>? FormVersion { get; set; }
    public virtual FormVersionItem<TKey>? FormItem { get; set; }
}
