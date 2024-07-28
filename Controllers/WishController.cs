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

        //[HttpGet("GetWishByName")]
        //public IEnumerable<WishModel> WishByName(string name)
        //{
        //    var wishOfName = _wishDB.Where(x => x.Name == name).ToList();
        //    return wishOfName;
        //}

        [HttpPost("addWish")]
        public WishModel addWish(string name, string wish, int mosha)
        {
            var createdWish = new WishModel
            {
                Name = name,
                Wish = wish,
                Mosha = mosha,
                Viti = DateTime.Now,
            };

            _wishDB.Add(createdWish);

            return createdWish;
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



        // api qe u krijuat automatikisht

        //GET: api/<WishController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //GET api/<WishController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //POST api/<WishController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<WishController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<WishController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}