using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{

    [Route("api/tickets")]
    [ApiController]
    public class ApiController : ControllerBase
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
           var userIdParam = new SqlParameter("@UserId",ticket.UserId);
            var titleParam = new SqlParameter("@Title", ticket.Title);
            var descParam = new SqlParameter("@Description", ticket.Description);
            var statusParam = new SqlParameter("@Status", ticket.Status);

            var newTicket = _db.Tickets.FromSqlRaw("EXEC CreateTicket @UserId,@Title,@Description,@Status",
                userIdParam, titleParam, descParam, statusParam).AsEnumerable().FirstOrDefault();

            if(newTicket == null)
            {
                return NotFound();
            }
            return Ok(newTicket);
        }


        [HttpPut("{id:int}/status")]
        public IActionResult UpdateTicketStatus(int id, [FromBody] string newStatus)
        {
            int result = _db.Database.ExecuteSqlRaw("EXEC UpdateTicketStatus @p0, @p1", id, newStatus);
            if (result == 0)
            {
                return BadRequest("Invalid status transition or ticket not found.");
            }
            return Ok(newStatus);

        }
    }
}
