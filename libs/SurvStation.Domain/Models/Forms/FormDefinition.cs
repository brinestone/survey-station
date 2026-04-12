namespace SurvStation.Domain.Models.Forms;

public enum FormItemType
{
    Field, Note, Image, Separator, Group
}

public class FormDefinition<TKey> : BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
{
    public string? Slug { get; set; }
    public string? Logo { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public virtual IList<FormVersion<TKey>> Versions { get; set; } = [];
}
