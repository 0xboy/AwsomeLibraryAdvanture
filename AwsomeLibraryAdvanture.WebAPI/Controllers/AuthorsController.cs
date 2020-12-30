using AwsomeLibraryAdvanture.Infrastructure.Core;
using AwsomeLibraryAdvanture.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwsomeLibraryAdvanture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public AwsomeDbOperation _awsomeDbOperation;

        public AuthorsController(IConfiguration configuration)
        {
            _configuration = configuration;

            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");

            _awsomeDbOperation = new AwsomeDbOperation(ConnectionString);
        }

        //GET: api/<AuthorsController>
        //[HttpGet]
        //public IEnumerable<Authors> Get()
        //{
        //    return null;
        //}

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public Authors Get(int id)
        {
            return GetAuthor(id);
        }

        //POST api/<AuthorsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //PUT api/<AuthorsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //DELETE api/<AuthorsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        protected Authors GetAuthor(int id)
        {
            var Author = _awsomeDbOperation.GetData("GetAuthorById", new SqlParameter[] { new SqlParameter("@Id", id) });

            if (Author.Read())
            {
                return new Authors
                {
                    Id = Convert.ToInt32(Author["Id"]),
                    Name = Author["Name"].ToString(),
                    Surname = Author["Surname"].ToString(),
                    Biography = Author["Biography"].ToString()
                };
            }

            return null;
        }
    }
}
