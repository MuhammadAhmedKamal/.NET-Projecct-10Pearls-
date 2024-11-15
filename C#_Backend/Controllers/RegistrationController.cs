using Backend_website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Backend_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Register(Registration registration)
        {
            string connectionString = _configuration.GetConnectionString("DBConnection");


            registration.Role = (registration.Email == "ahmedkamal@gmail.com") ? "Admin" : "User";


            string nonQueryCommand = "INSERT INTO Registration (FirstName, LastName, Email, Password, IsActive, Role) " +
                                     "VALUES (@FirstName, @LastName, @Email, @Password, @IsActive, @Role)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(nonQueryCommand, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", registration.FirstName);
                        command.Parameters.AddWithValue("@LastName", registration.LastName);
                        command.Parameters.AddWithValue("@Email", registration.Email);
                        command.Parameters.AddWithValue("@Password", registration.Password); // Insert plain password
                        command.Parameters.AddWithValue("@IsActive", registration.IsActive);
                        command.Parameters.AddWithValue("@Role", registration.Role);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Data Inserted Successfully");
                        }
                        else
                        {
                            return StatusCode(500, "Failed to insert data.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Log the error message for better diagnostics
                    Console.WriteLine($"Error occurred: {ex.Message}");  // For development purposes
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected errors
                    Console.WriteLine($"Unexpected error: {ex.Message}");  // For development purposes
                    return StatusCode(500, "An unexpected error occurred. Please try again.");
                }
            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            string connectionString = _configuration.GetConnectionString("DBConnection");

            // Query to validate the user credentials
            string query = "SELECT Role FROM Registration WHERE Email = @Email AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        object role = command.ExecuteScalar();

                        if (role != null)
                        {
                            return Ok(new { Message = "Login Successful", Role = role.ToString() });
                        }
                        else
                        {
                            return Unauthorized("Invalid email or password");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

        [HttpGet]
        [Route("getRole")]
        public IActionResult GetRole(string email)
        {
            string connectionString = _configuration.GetConnectionString("DBConnection");
            string query = "SELECT Role FROM Registration WHERE Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        object role = command.ExecuteScalar();
                        if (role != null)
                        {
                            return Ok(new { Role = role.ToString() });
                        }
                        else
                        {
                            return NotFound("User not found or invalid email");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }
}
