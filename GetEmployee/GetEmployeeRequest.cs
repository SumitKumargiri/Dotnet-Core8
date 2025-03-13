using MediatR;
using System.Numerics;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser
{

    public class GetEmployeeRequest : IRequest<GetEmployeeResponse>
    {
        public  Guid Id { get; set; }
    }
}