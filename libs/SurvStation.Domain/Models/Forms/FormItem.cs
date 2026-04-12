namespace SurvStation.Domain.Models.Forms;

public class FormItem<TKey> : BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
{
    public TKey FormId { get; set; }
    public virtual FormDefinition<TKey>? Form { get; set; }
    public FormItemType Type { get; set; }
    public IEnumerable<string> Tags { get; set; } = new HashSet<string>();
    public Dictionary<string, object>? Config { get; set; }
}
