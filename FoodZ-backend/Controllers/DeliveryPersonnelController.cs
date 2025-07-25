using Foodz.API.DTOs.DeliveryPersonnel;
using Foodz.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foodz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // All endpoints restricted to Admin
    public class DeliveryPersonnelController : ControllerBase
    {
        private readonly IDeliveryPersonnelService _deliveryPersonnelService;

        public DeliveryPersonnelController(IDeliveryPersonnelService deliveryPersonnelService)
        {
            _deliveryPersonnelService = deliveryPersonnelService;
        }

        // GET: api/DeliveryPersonnel
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personnel = await _deliveryPersonnelService.GetAllAsync();
            return Ok(personnel);
        }

        // GET: api/DeliveryPersonnel/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _deliveryPersonnelService.GetByIdAsync(id);
            return Ok(person);
        }

        // POST: api/DeliveryPersonnel
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DeliveryPersonnelCreateDto dto)
        {
            var createdPerson = await _deliveryPersonnelService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
        }

        // DELETE: api/DeliveryPersonnel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _deliveryPersonnelService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
