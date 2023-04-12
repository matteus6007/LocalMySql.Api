using System;
using System.ComponentModel.DataAnnotations;

namespace LocalMySql.Api.Infrastructure.Models
{
    public class Customer
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
