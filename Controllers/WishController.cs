using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private static List<WishModel> _wishDB = new();
        private readonly IConfiguration _config;

        public IConfiguration Config { get; }

        public WishController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetAllWishes")]
        public async Task<IActionResult> GetWish()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var wishes = await connection.QueryAsync<WishModel>("SELECT * FROM Wish");
            return Ok(wishes);
        }

        [HttpGet("GetWishByName")]
        public async Task<IActionResult> GetWishByName(string name)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var wishe = await connection.QueryFirstAsync<WishModel>("SELECT * FROM Wish WHERE Name = @Name",
                new { Name = name });
            return Ok(wishe);
        }

        [HttpPost("MakeAWish")]
        public async Task<IActionResult> CreateWish(WishModel wish)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO Wish (Name, Wish, Mosha, Viti) VALUES (@Name, @Wish, @Mosha, @Viti)", wish);
            return Ok(wish);
        }
    }
}