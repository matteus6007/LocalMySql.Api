using System;

namespace LocalMySql.Api.Contracts.Models
{
    public class CustomerV1
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
