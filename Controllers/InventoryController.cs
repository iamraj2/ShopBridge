using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ShopBridge.Data;
using ShopBridge.Models;

namespace ShopBridge.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        readonly IInventoryRepository _repository; // = new InventoryRepository();

        public InventoryController() {
            _repository = new InventoryRepository();
        }
        // GET: api/Inventory
        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await _repository.GetAllItemsAsync());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Inventory/5
        [Route("{id}", Name = "GetItem")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.GetItemAsync(id);
                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Inventory
        [Route()]
        public async Task<IHttpActionResult> Post(Item model)
        {
            try
            {
                if (await _repository.GetItemAsync(model.ItemId) != null)
                {
                    ModelState.AddModelError("ItemId", "Id in use");
                }

                if (ModelState.IsValid)
                {
                    _repository.AddItem(model);

                    if (await _repository.SaveChangesAsync())
                    {
                        return CreatedAtRoute("GetItem", new { id = model.ItemId }, model);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Inventory/5
        [Route("{itemId}")]
        public async Task<IHttpActionResult> Put(int itemId, Item model)
        {
            try
            {
                var item = await _repository.GetItemAsync(itemId);
                if (item == null) return NotFound();
                if (ModelState.IsValid)
                {
                    item.Name = model.Name;
                    item.Description = model.Description;
                    item.Price = model.Price;
                    item.ExpiryDate = model.ExpiryDate;

                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(item);
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Inventory/5
        [Route("{itemId}")]
        public async Task<IHttpActionResult> Delete(int itemId)
        {
            try
            {
                var item = await _repository.GetItemAsync(itemId);
                if (item == null) return NotFound();

                _repository.DeleteItem(item);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
