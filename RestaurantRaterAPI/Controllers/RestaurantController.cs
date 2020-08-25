using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDdContext _context = new RestaurantDdContext();

        // Create
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty");
            }

            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        // Read
        // Get by ID
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        // Get All
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        // Update
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Restaurant updatedRestaurant)
        {
            if (ModelState.IsValid)
            {
                // Find and update restaurant
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);
                if (restaurant != null)
                {
                    // Update restaurant
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Rating = updatedRestaurant.Rating;

                    await _context.SaveChangesAsync();

                    return Ok("Restaurant has been updated");
                }
                // Didn't find restaurant
                return NotFound();
            }
            // Return bad request
            return BadRequest(ModelState);
        }

        // Delete

    }
}
