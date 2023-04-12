using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LocalMySql.Api.Contracts.Models;
using LocalMySql.Api.Contracts.Requests;
using LocalMySql.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocalMySql.Api.Controllers
{
    /// <summary>
    /// Manage Customers
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Manage Customers
        /// </summary>
        /// <param name="customerService">Customer Service</param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Create new Customer
        /// </summary>
        /// <param name="request">Create Customer request</param>
        /// <returns>New <see cref="CustomerV1"/></returns>
        /// <response code="201">Customer created</response>
        /// <response code="400">Request is not valid</response>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerV1), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateCustomerRequestV1 request)
        {
            var customer = new CustomerV1
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedOn = DateTime.UtcNow
            };

            await _customerService.SaveAsync(customer);

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Get Customer by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns><see cref="CustomerV1"/></returns>
        /// <response code="200">Customer returned</response>
        /// <response code="404">Customer not found</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerV1), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <returns><see cref="List{CustomerV1}"/></returns>
        /// <response code="200">Customers returned</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerV1>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAll();

            return Ok(customers);
        }
    }
}
