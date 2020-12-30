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
    public class BooksController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public AwsomeDbOperation _awsomeDbOperation;

        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;

            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");

            _awsomeDbOperation = new AwsomeDbOperation(ConnectionString);
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IEnumerable<Books> Get(int? categoryId = null)
        {

            if (categoryId != null)
            {
                return GetAllBooksByCategory((int)categoryId);
            }

            var GetAllBooks = _awsomeDbOperation.GetData("GetAllBooks");

            List<Books> bk = new List<Books>();

            while (GetAllBooks.Read())
            {
                bk.Add(GetBook(Convert.ToInt32(GetAllBooks["Id"])));
            }

            return bk;
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public Books Get(int id)
        {
            return GetBook(id);
        }

        //POST api/<BooksController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //PUT api/<BooksController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //DELETE api/<BooksController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        protected Books GetBook(int id)
        {
            var book = _awsomeDbOperation.GetData("GetBookById", new SqlParameter[] { new SqlParameter("@Id", id) });

            if (book.Read())
            {
                return new Books
                {
                    Id = Convert.ToInt32(book["Id"]),
                    Name = book["Name"].ToString(),
                    Author = GetAuthor(Convert.ToInt32(book["AuthorId"])),
                    Category = GetBookCategory(Convert.ToInt32(book["CategoryId"])),
                    ISBN = book["ISBN"].ToString(),
                    Publisher = book["Publisher"].ToString()
                };
            }

            return null;
        }
        
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

        protected BookCategory GetBookCategory(int id)
        {
            var bookCat = _awsomeDbOperation.GetData("GetCategoryById", new SqlParameter[] { new SqlParameter("@Id", id) });

            if (bookCat.Read())
            {
                return new BookCategory
                {
                    Id = Convert.ToInt32(bookCat["Id"]),
                    Name = bookCat["Name"].ToString()
                };
            }

            return null;
        }

        protected List<Books> GetAllBooksByCategory(int Id)
        {
            var books = _awsomeDbOperation.GetData("GetBooksByCategoryId", new SqlParameter[] { new SqlParameter("@CategoryId", Id) });

            var result = new List<Books>();

            while (books.Read())
            {
                result.Add(new Books
                {
                    Id = Convert.ToInt32(books["Id"]),
                    Name = books["Name"].ToString(),
                    Author = GetAuthor(Convert.ToInt32(books["AuthorId"])),
                    Category = GetBookCategory(Convert.ToInt32(books["CategoryId"])),
                    ISBN = books["ISBN"].ToString(),
                    Publisher = books["Publisher"].ToString()
                });
            }

            return result;
        }
    }
}
