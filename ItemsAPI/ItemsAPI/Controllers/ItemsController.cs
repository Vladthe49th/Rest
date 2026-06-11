using Microsoft.AspNetCore.Mvc;
using ItemsApi.Models;
using ItemsApi.Data;

namespace ItemsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        // GET: api/items
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            return Ok(ItemStorage.Items);
        }

        // GET: api/items/1
        [HttpGet("{id}")]
        public ActionResult<Item> GetById(int id)
        {
            var item = ItemStorage.Items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound($"Item with ID {id} was not found.");
            }

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public ActionResult<Item> Create(Item item)
        {
            int newId = ItemStorage.Items.Count == 0
                ? 1
                : ItemStorage.Items.Max(x => x.Id) + 1;

            item.Id = newId;

            ItemStorage.Items.Add(item);

            return CreatedAtAction(
                nameof(GetById),
                new { id = item.Id },
                item);
        }

        // PUT: api/items/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Item updatedItem)
        {
            var existingItem = ItemStorage.Items.FirstOrDefault(x => x.Id == id);

            if (existingItem == null)
            {
                return NotFound($"Item with ID {id} was not found.");
            }

            existingItem.Name = updatedItem.Name;
            existingItem.Price = updatedItem.Price;

            return NoContent();
        }

        // DELETE: api/items/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = ItemStorage.Items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound($"Item with ID {id} was not found.");
            }

            ItemStorage.Items.Remove(item);

            return NoContent();
        }
    }
}