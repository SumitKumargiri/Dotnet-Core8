namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser;

public sealed record GetEmployeeResponse
{
    public Guid? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? Createdby { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? Updatedby { get; set; }
    public bool? IsActive { get; set; }
}



