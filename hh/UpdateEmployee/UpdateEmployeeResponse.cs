namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateEmployee;

public sealed record UpdateEmployeeResponse
{
    public bool IsUserFound { get; set; }
    public bool IsSuccess {  get; set; }
    public bool NotUpdated {  get; set; }

}
