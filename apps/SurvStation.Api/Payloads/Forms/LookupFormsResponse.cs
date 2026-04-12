namespace SurvStation.Api.Payloads.Forms;

public record LookupFormsResponse
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid Id { get; set; }
    
}
