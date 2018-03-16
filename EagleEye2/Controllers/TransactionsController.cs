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
    public class TransactionsController : ApiController
    {
        private EagleEye2Context db = new EagleEye2Context();

        // GET: api/Transactions
        public IQueryable<TransactionDTO> GetTransactions()
        {
            var transactions = from t in db.Transactions
                               select new TransactionDTO()
                               {
                                   Id = t.Id,
                                   Amount = t.Amount,
                                   CardId = t.Card.Id,
                                   StoreID = t.Store.Id
                               };

            return transactions;
        }

        // GET: api/Transactions/5
        [ResponseType(typeof(TransactionDetailDTO))]
        public async Task<IHttpActionResult> GetTransaction(int id)
        {
            var transaction = await db.Transactions.Include(t => t.Card).Include(t => t.Store).Select(t =>
            new TransactionDetailDTO()
            {
                Id = t.Id,
                Amount = t.Amount,
                CardId = t.Card.Id,
                StoreId = t.Store.Id,
                StoreName = t.Store.Name
            }).SingleOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        [ResponseType(typeof(TransactionDTO))]
        public async Task<IHttpActionResult> PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transactions.Add(transaction);
            await db.SaveChangesAsync();

            db.Entry(transaction).Reference(x => x.Card).Load();
            db.Entry(transaction).Reference(x => x.Store).Load();

            var dto = new TransactionDTO()
            {
                Id = transaction.Id,
                CardId = transaction.Card.Id,
                Amount = transaction.Amount,
                StoreID = transaction.Store.Id
            };

            return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, dto);
        }

        // DELETE: api/Transactions/5
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> DeleteTransaction(int id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();

            return Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}