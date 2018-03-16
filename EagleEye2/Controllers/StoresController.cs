using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EagleEye2.Models;

namespace EagleEye2.Controllers
{
    public class StoresController : ApiController
    {
        private EagleEye2Context db = new EagleEye2Context();

        // GET: api/Stores
        public IQueryable<StoreDTO> GetStores()
        {
            var stores = from s in db.Stores
                        select new StoreDTO()
                        {
                            Id = s.Id,
                            StoreName = s.Name
                        };

            return stores;
        }

        // GET: api/Stores/5
        [ResponseType(typeof(StoreDetailDTO))]
        public async Task<IHttpActionResult> GetStore(int id)
        {
            var store = await db.Stores.Select(s =>
            new StoreDetailDTO()
            {
                Id = s.Id,
                StoreName = s.Name,
                Location = s.Location
            }).SingleOrDefaultAsync(s => s.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/Stores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.Id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Stores
        [ResponseType(typeof(StoreDetailDTO))]
        public async Task<IHttpActionResult> PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);
            await db.SaveChangesAsync();

            var dto = new StoreDetailDTO()
            {
                Id = store.Id,
                StoreName = store.Name,
                Location = store.Location
            };

            return CreatedAtRoute("DefaultApi", new { id = store.Id }, dto);
        }

        // DELETE: api/Stores/5
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> DeleteStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.Id == id) > 0;
        }
    }
}