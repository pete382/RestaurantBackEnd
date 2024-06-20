using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantBackendAPI.Models;

namespace RestaurantBackendAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ordersAPI : ControllerBase
	{
		RestaurantDbContext DBcontext = new RestaurantDbContext();


		//api/Order

		[HttpGet()]
		public IActionResult GetOrders(string? name, bool? Orderagain)
		{
			List<Order> result = DBcontext.Orders.ToList();
			if (name != null)
			{
				result = DBcontext.Orders.Where(o => o.Restaurant.ToLower().Contains(name.ToLower())).ToList();
			}
				if (Orderagain != null)
				{
					result = DBcontext.Orders.Where(o => o.Orderagain == Orderagain).ToList();
				
				}
			return Ok(result);
		}


		[HttpDelete("{id}")]

		public IActionResult DeleteOrders(int id) {

			Order result = DBcontext.Orders.Find(id);
			if (result == null) { return NotFound(); }	
			DBcontext.Orders.Remove(result);
			DBcontext.SaveChanges();
				return NoContent();
				}

		[HttpPost()]
		public IActionResult AddOrder([FromBody] Order newOrder) { 
		
		DBcontext.Orders.Add(newOrder);
			DBcontext.SaveChanges();
			return Created($"/api/ordersAPI/{newOrder.Id}", newOrder);

		}

		[HttpGet("{id}")]

		public IActionResult GetById(int id) {

			Order result = DBcontext.Orders.Find(id);
			if (result == null) { return NotFound(); }
				
				return Ok(result);
				
		}


		[HttpPut("{id}")]

		public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder) {

			if (updatedOrder.Id == null) { return BadRequest(); }
			if(!DBcontext.Orders.Any(o => o.Id == id)) { return NotFound(); }
			DBcontext.Update(updatedOrder);
			DBcontext.SaveChanges();
			return NoContent();
		
		}





































		}
		













	}
		

		

