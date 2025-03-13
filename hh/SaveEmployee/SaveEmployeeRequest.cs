using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveEmployee

{
    public class SaveEmployeeRequest : IRequest<SaveEmployeeResponse>
    {
      


        public string? FirstName { get; set; }

        public string? LastName { get; set; } 
        public string? Mobile { get; set; }
        public string? Address { get; set; } 
        public DateTime? CreateDate { get; set; }
        public Guid? Createdby { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? Updatedby { get; set; }
        public bool? IsActive { get; set; }
    }
}