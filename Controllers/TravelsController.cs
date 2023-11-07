using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;

namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TravelsController : ControllerBase
  {
    private readonly TravelApiContext _db;
    public TravelsController(TravelApiContext db)
    {
      _db = db;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Travel>>> Get(string city, string country, string user_name)
    {
      IQueryable<Travel> query = _db.Travels.AsQueryable();
      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }
      if (country != null)
      {
        query = query.Where(entry => entry.Country == country);
      }
      if (user_name != null)
      {
        query = query.Where(entry => entry.User_Name == user_name);
      }
      return await query.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Travel>> GetTravel(int id)
    {
      Travel travel = await _db.Travels.FindAsync(id);
      if (travel == null)
      {
        return NotFound();
      }
      return travel;
    }
    [HttpPost]
    public async Task<ActionResult<Travel>> Post(Travel travel)
    {
      _db.Travels.Add(travel);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetTravel), new { id = travel.TravelId }, travel);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Travel travel)
    {
      if (id != travel.TravelId)
      {
        return BadRequest();
      }
      _db.Travel.Update(travel);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TravelExists(id))
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
    private bool TravelExists(int id)
    {
      return _db.Travels.Any(e => e.TravelId == id);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTravel(int id)
    {
      Travel travel = await _db.Travels.FindAsync(id);
      if (travel == null)
      {
        return NotFound();
      }
      _db.Travels.Remove(travel);
      await _db.SaveChangesAsync();
      return NoContent();
    }
  }
}