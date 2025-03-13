
using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("employees")]
    public class Employees : BaseEntity
    {
        [Column("phone")]
        public string? Mobile { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }


        [Column("address")]
        public string? Address { get; set; }
    }
}