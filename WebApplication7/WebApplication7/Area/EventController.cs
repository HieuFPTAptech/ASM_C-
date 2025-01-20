using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;


namespace WebApplication7.Area;
[Route("api/EventModels")]
[ApiController]
public class EventModelController : ControllerBase
{
    private readonly Application _context;

    public EventModelController(Application context)
    {
        _context = context;
    }

    // GET: api/EventModels
    [HttpGet]
    public async Task<IActionResult> GetEventModels(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10, 
        [FromQuery] string search = null, 
        [FromQuery] string order = "Date")
    {
        // Base query
        var query = _context.EventModel.AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => e.Name.Contains(search) || e.Location.Contains(search));
        }

        // Sorting
        query = order switch
        {
            "Name" => query.OrderBy(e => e.Name),
            "Location" => query.OrderBy(e => e.Location),
            "Date" => query.OrderBy(e => e.Date),
            _ => query.OrderBy(e => e.Date) 
        };

        // Pagination
        var totalItems = await query.CountAsync();
        var EventModels = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Response
        var response = new
        {
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            EventModels = EventModels
        };

        return Ok(response);
    }
    
    // GET: api/EventModels/5 
    [HttpGet("{id}")] 
    public async Task<ActionResult<EventModel>> GetEventModel(long id) 
    { 
        var @Event = await _context.EventModel.FindAsync(id); 
 
        if (@Event == null) 
        { 
            return NotFound(); 
        } 
 
        return @Event; 
    } 
 
    // PUT: api/EventModels/5 
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
    [HttpPut("{id}")] 
    public async Task<IActionResult> PutEventModel(long id, EventModel @Event) 
    { 
        if (id != @Event.Id) 
        { 
            return BadRequest(); 
        } 
 
        _context.Entry(@Event).State = EntityState.Modified; 
 
        try 
        { 
            await _context.SaveChangesAsync(); 
        } 
        catch (DbUpdateConcurrencyException) 
        { 
            if (!EventModelExists(id)) 
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
    
    // POST: api/Events
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Migrations.EventModel>> PostEvent(EventModel @event)
    {
        _context.EventModel.Add(@event);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEventModels", new { id = @event.Id }, @event);
    }
         
    // DELETE: api/EventModels/5 
    [HttpDelete("{id}")] 
    public async Task<IActionResult> DeleteEventModel(long id) 
    { 
        var @Event = await _context.EventModel.FindAsync(id); 
        if (@Event == null) 
        { 
            return NotFound(); 
        } 
 
        _context.EventModel.Remove(@Event); 
        await _context.SaveChangesAsync(); 
 
        return NoContent(); 
    } 
 
    private bool EventModelExists(long id) 
    { 
        return _context.EventModel.Any(e => e.Id == id); 
    } 
} 

