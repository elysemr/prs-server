using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server.Models;

namespace prs_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsCapstoneDbContext _context;

        public RequestsController(PrsCapstoneDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //GET: reviews/UserID
        //[HttpGet("reviews/{userId}")]
        //public async Task<ActionResult<IEnumerable<Request>>> GetRequestsInReview(int userId)
        //{
        //    var request = new Request() { };
        //    var requests = await _context.Request.ToListAsync();
        //    var user = await _context.Request.FindAsync(userId);
        //    if (request == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //   request.Status = (from r in _context.Request
        //                    where r.Status = "REVIEW"
        //                    and r.UserId != userId
        //                    select r);

        //    if (request.Status == "REVIEW" && request.UserId != userId)
        //    {
        //        return requests;
        //    }

        //}


        //PUT: api/requests/review


        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
        // set request to review
       [HttpPut("review")]
       public async Task<IActionResult> SetRequestToReview(Request request)
        {
            request = new Request() { };
            var review = await _context.Request.ToListAsync();
            if(request == null)
            {
                return NotFound();
            }
            if(request.Total <= 50)
            {
                request.Status = "APPROVED";
                return await PutRequest(request.Id, request);
            }
            else
            {
                request.Status = "REVIEW";
                return await PutRequest(request.Id, request);
            }
        }

        //set request to approve
        [HttpPut("approve")]
        public async Task<IActionResult> SetRequestToApprove(Request request)
        {
            request = new Request() { };
            if(request == null)
            {
                return NotFound();
            }
            request.Status = "APPROVED";
            return await PutRequest(request.Id, request);
        }

        //set request to rejected
        [HttpPut("reject")]
        public async Task<IActionResult> SetRequestToRejected(Request request)
        {
            request = new Request() { };
            if(request == null)
            {
                return NotFound();
            }
            request.Status = "REJECTED";
            return await PutRequest(request.Id, request);
        }


        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
