using Backend_website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;


namespace Backend_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PasswordService _passwordService;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _passwordService = new PasswordService();
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Register(Registration registration)
        {
            string connectionString = _configuration.GetConnectionString("DBConnection");

            registration.Role = (registration.Email == "ahmedkamal@gmail.com") ? "Admin" : "User";

            // Hash the password before storing
            string hashedPassword = _passwordService.HashPassword(registration.Password);

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
                        command.Parameters.AddWithValue("@Password", hashedPassword); // Store hashed password
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
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return StatusCode(500, "An unexpected error occurred. Please try again.");
                }
            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            string connectionString = _configuration.GetConnectionString("DBConnection");
            string query = "SELECT Password, Role FROM Registration WHERE Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashedPassword = reader["Password"].ToString();
                                string role = reader["Role"].ToString();

                                if (_passwordService.VerifyPassword(password, storedHashedPassword))
                                {
                                    return Ok(new { Message = "Login Successful", Role = role });
                                }
                                else
                                {
                                    return Unauthorized("Invalid email or password");
                                }
                            }
                            else
                            {
                                return Unauthorized("Invalid email or password");
                            }
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

    public class PasswordService
    {

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            Message = "An unexpected error occurred.",
            Details = exception.Message
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}