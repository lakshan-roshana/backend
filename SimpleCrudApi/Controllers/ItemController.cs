using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Models;
using SimpleCrudApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _service;

        public ItemController(ItemService service) => _service = service;

        [HttpGet]
        public async Task<List<Item>> Get() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(string id)
        {
            var item = await _service.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Item item)
        {
            await _service.CreateAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Item item)
        {
            var existingItem = await _service.GetAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingItem = await _service.GetAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
