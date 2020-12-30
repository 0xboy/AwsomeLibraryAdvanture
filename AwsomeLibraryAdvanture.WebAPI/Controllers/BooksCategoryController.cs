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
    public class BooksCategoryController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public AwsomeDbOperation _awsomeDbOperation;

        public BooksCategoryController(IConfiguration configuration)
        {
            _configuration = configuration;

            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");

            _awsomeDbOperation = new AwsomeDbOperation(ConnectionString);
        }

        // GET: api/<BooksCategoryController>
        [HttpGet]
        public IEnumerable<BookCategory> Get()
        {
            var GetAllBaseCategories = _awsomeDbOperation.GetData("GetAllBaseCategories");

            List<BookCategory> bkct = new List<BookCategory>();

            while (GetAllBaseCategories.Read())
            {
                bkct.Add(GetBookCategory(Convert.ToInt32(GetAllBaseCategories["Id"])));
            }

            return bkct;
        }

        // GET api/<BooksCategoryController>/5
        [HttpGet("{id}")]
        public BookCategory Get(int id)
        {
            return GetBookCategory(id);
        }

        // POST api/<BooksCategoryController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //PUT api/<BooksCategoryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //DELETE api/<BooksCategoryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


        protected BookCategory GetBookCategory(int id)
        {
            var bookCat = _awsomeDbOperation.GetData("GetCategoryById", new SqlParameter[] { new SqlParameter("@Id", id) });

            if (bookCat.Read())
            {
                return new BookCategory
                {
                    Id = Convert.ToInt32(bookCat["Id"]),
                    Name = bookCat["Name"].ToString(),
                    BaseId = Convert.ToInt32(bookCat["BaseId"]),
                    BaseCategory = GetBaseBookCategory(Convert.ToInt32(bookCat["BaseId"])),
                    SubCategories = GetAllSubBookCategory(Convert.ToInt32(bookCat["Id"]))
                };
            }

            return null;
        }

        protected BookCategory GetBaseBookCategory(int id)
        {
            var bookCat = _awsomeDbOperation.GetData("GetCategoryById", new SqlParameter[] { new SqlParameter("@Id", id) });

            if (bookCat.Read())
            {
                return new BookCategory
                {
                    Id = Convert.ToInt32(bookCat["Id"]),
                    Name = bookCat["Name"].ToString(),
                    BaseId = Convert.ToInt32(bookCat["BaseId"]),
                };
            }

            return null;
        }

        protected List<BookCategory> GetAllSubBookCategory(int BaseId)
        {
            var bookCat = _awsomeDbOperation.GetData("GetCategoryByBaseId", new SqlParameter[] { new SqlParameter("@BaseId", BaseId) });

            var result = new List<BookCategory>();

            while (bookCat.Read())
            {
                result.Add(new BookCategory
                {
                    Id = Convert.ToInt32(bookCat["Id"]),
                    Name = bookCat["Name"].ToString(),
                    BaseId = Convert.ToInt32(bookCat["BaseId"]),
                });
            }

            return result;
        }
    }
}
