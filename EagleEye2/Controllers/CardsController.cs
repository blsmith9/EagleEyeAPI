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
    public class CardsController : ApiController
    {
        private EagleEye2Context db = new EagleEye2Context();

        // GET: api/Cards
        public IQueryable<CardDTO> GetCards()
        {
            var cards = from c in db.Cards
                        select new CardDTO()
                        {
                            Id = c.Id,
                            UserName = c.User.Name
                        };
            return cards;
        }

        // GET: api/Cards/5
        [ResponseType(typeof(CardDetailDTO))]
        public async Task<IHttpActionResult> GetCard(int id)
        {
            var card = await db.Cards.Include(c => c.User).Select(c =>
            new CardDetailDTO()
            {
                Id = c.Id,
                UserName = c.User.Name,
                UserDepartment = c.User.Department
            }).SingleOrDefaultAsync(c => c.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // PUT: api/Cards/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCard(int id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != card.Id)
            {
                return BadRequest();
            }

            db.Entry(card).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        [ResponseType(typeof(Card))]
        public async Task<IHttpActionResult> PostCard(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cards.Add(card);
            await db.SaveChangesAsync();

            db.Entry(card).Reference(x => x.User).Load();

            var dto = new CardDTO()
            {
                Id = card.Id,
                UserName = card.User.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = card.Id }, dto);
        }

        // DELETE: api/Cards/5
        [ResponseType(typeof(Card))]
        public async Task<IHttpActionResult> DeleteCard(int id)
        {
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            db.Cards.Remove(card);
            await db.SaveChangesAsync();

            return Ok(card);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardExists(int id)
        {
            return db.Cards.Count(e => e.Id == id) > 0;
        }
    }
}