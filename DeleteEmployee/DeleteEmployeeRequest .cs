using System;
using MediatR;

namespace App.EnglishBuddy.Application.Features.DeleteEmployee
{
    public class DeleteEmployeeRequest : IRequest<DeleteEmployeeResponse>
    {
        public Guid Id { get; set; }
    }
}
