using System.ComponentModel.DataAnnotations;

namespace LocalMySql.Api.Contracts.Requests
{
    public class CreateCustomerRequestV1
    {
        /// <summary>
        /// Gets or sets Customer First Name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Customer Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; }
    }
}
