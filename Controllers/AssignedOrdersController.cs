using HBHB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HBHB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedOrdersController : ControllerBase
    {
        // This is a sample method to demonstrate assigning orders to registered users
        [HttpPost]
        public IActionResult AssignOrderToUser(string userId, int orderId)
        {
            // Check if the user is registered for a career
            if (!IsUserRegisteredForCareer(userId))
            {
                return BadRequest("User is not registered for a career. Please complete the career registration first.");
            }

            // Perform logic to assign the order to the user
            // Example: Store the assignment in the database

            // Return a success response
            return Ok(new { message = $"Order {orderId} assigned to user {userId} successfully." });
        }

        // Sample method to check if a user is registered for a career
        private bool IsUserRegisteredForCareer(string userId)
        {
            // Add your logic to check if the user is registered for a career
            // This could involve querying the database to see if the user's career profile exists

            // For demonstration purposes, return true if the user ID is not null or empty
            return !string.IsNullOrEmpty(userId);
        }
    }
}
