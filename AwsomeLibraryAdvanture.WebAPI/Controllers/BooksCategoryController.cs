using AwsomeLibraryAdvanture.Infrastructure.Core;
using AwsomeLibraryAdvanture.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwsomeLibraryAdvanture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksCategoryController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public AwsomeDbOperation _awsomeDbOperation;

        public BooksCategoryController(IConfiguration configuration)
        {
            _configuration = configuration;

            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");

            _awsomeDbOperation = new AwsomeDbOperation(new System.Data.SqlClient.SqlConnection(ConnectionString));
        }

        // GET: api/<BooksCategoryController>
        [HttpGet]
        public IEnumerable<BookCategory> Get()
        {
            var GetDataResult = _awsomeDbOperation.GetData("GetAllBaseCategories");

            List<BookCategory> bkct = new List<BookCategory>();

            while (GetDataResult.Read())
            {
                bkct.Add(new BookCategory { Id = Convert.ToInt32(GetDataResult["Id"]), Name = GetDataResult["Name"].ToString() });
            }

            return bkct;
        }

        // GET api/<BooksCategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BooksCategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
