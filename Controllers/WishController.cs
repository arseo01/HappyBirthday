using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private readonly string _connectionString = "Server=mysql-birthday.alwaysdata.net;Port=3306; Database=birthdayapp; User=birthday; Password=8mKPNN2jQcVG&UjVdMvw&15UPQXz4XTZ;";

        [HttpPost("MakeAWish")]
        public async Task<IActionResult> MakeAWish([FromBody] WishModel wish)
        {
            if (wish == null || string.IsNullOrEmpty(wish.Wish))
            {
                return BadRequest("Wish is required.");
            }

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Insert the wish into the database
                    string sql = "INSERT INTO Wishes (Name, Wish, Mosha, Viti) VALUES (@Name, @Wish, @Mosha, @Viti)";

                    using var cmd = new MySqlCommand(sql, conn);
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Name", wish.Name ?? (object)DBNull.Value); // If name is null, insert NULL
                    cmd.Parameters.AddWithValue("@Wish", wish.Wish); //Not Nullable
                    cmd.Parameters.AddWithValue("@Mosha", wish.Mosha ?? (object)DBNull.Value); // If Mosha is null, insert NULL
                    cmd.Parameters.AddWithValue("@Viti", DateTime.Now); // Never NULL, Automatically set Viti to the current date

                    await cmd.ExecuteNonQueryAsync();
                }

                return Ok("Your wish has been saved!");
            }
            catch (MySqlException ex)
            {
                // Return an error if the connection or query fails
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //class Program
        //{
        //    static void Main()
        //    {
        //        string connstring = "Server=mysql-birthday.alwaysdata.net;Port=3306;Database=birthday_app;User Id=alban1angh057;Password=\"8mKPNN2jQcVG&UjVdMvw&15UPQXz4XTZ\"";

        //        MySqlConnection conn = new MySqlConnection(connstring);

        //        try
        //        {
        //            // Open the connection to the database
        //            conn.Open();
        //            Console.WriteLine("Connection to the database was successful.");
        //        }
        //        catch (MySqlException ex)
        //        {
        //            // Handle any errors that occur during the connection
        //            Console.WriteLine($"Error: {ex.Message}");
        //        }
        //        finally
        //        {
        //            // Close the connection
        //            conn.Close();
        //        }
        //    }
        //}
    }
}