using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental.Data;
using BikeRental.Model;
using BikeRental.Service;

namespace BikeRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly BikeRentalDbContext _context;
        private readonly ICalculation calculation;

        public RentalsController(BikeRentalDbContext context, ICalculation calculation)
        {
            _context = context;
            this.calculation = calculation;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals
                .Include(r => r.Bike)
                .Include(r => r.Customer)
                .ToListAsync();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Bike)
                .Include(r => r.Customer)
                .Where(r => r.RentalId == id)
                .FirstOrDefaultAsync();

            if (rental == null)
            {
                return NotFound();
            }

            return rental;
        }

        // POST: api/Rentals
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
            var bike = await _context.Bikes.FindAsync(rental.BikeId);
            var customer = await _context.Customers.FindAsync(rental.CustomerId);

            if(bike.IsRented)
            {
                return BadRequest();
            }

            if(customer.Rentals != null && customer.Rentals.Last().RentalEnd != DateTime.MaxValue)
            {
                return BadRequest();
            }

            bike.IsRented = true;

            rental.RentalBegin = DateTime.Now;
            rental.RentalEnd = DateTime.MaxValue;
            rental.RentalCost = default;

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new { id = rental.RentalId }, rental);
        }

        // PUT: api/Rentals/0/end
        [HttpPut("{id}/end")]
        public async Task<ActionResult<Rental>> EndRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if(rental == null)
            {
                return NotFound();
            }

            if (rental.RentalEnd != DateTime.MaxValue)
            {
                return BadRequest("The Rental already ended");
            }


            rental.RentalEnd = DateTime.Now;
            rental.Bike.IsRented = false;
            rental.RentalCost = calculation.CalculateCost(rental.RentalBegin, rental.RentalEnd, rental.Bike.RentalPriceFirstHour, rental.Bike.RentalPriceAdditionalHour);

            _context.Entry(rental).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Rentals/5/paid
        [HttpPut("{id}/paid")]
        public async Task<IActionResult> PayRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            if (rental.RentalCost <= 0)
            {
                return BadRequest();
            }

            rental.Paid = true;

            _context.Entry(rental).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rental>> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalId == id);
        }
    }
}
