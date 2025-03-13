using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;

public sealed record GetAllEmployeeRequest : IRequest<List<GetAllEmployeeResponse>>
{
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
       

}


