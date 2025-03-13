namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;

public sealed record GetAllEmployeeResponse
{
   
    public string? Name { get; set; }
   
    public string? Email { get; set; }
  

    public Guid UserId { get; set; }
}