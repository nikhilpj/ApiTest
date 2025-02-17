using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApiController(ApplicationDbContext db)
        {

            _db = db;
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<Ticket>> GetTickets(int id)
        {
           var tickets = _db.Tickets.Where(t=>t.UserId == id).ToList();
            if(tickets==null && tickets.Count==0)
            {
                return NotFound();
            }
            return Ok(tickets);
            
        }

        [HttpPost]
        public ActionResult<Ticket> CreateTicket(Ticket ticket)
        {
            Ticket newTicket = new Ticket()
            {
                Id = ticket.Id,
                UserId = ticket.UserId,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status
            };
            _db.Tickets.Add(newTicket);
            _db.SaveChanges();
            return Ok(newTicket);
        }

    }
}
