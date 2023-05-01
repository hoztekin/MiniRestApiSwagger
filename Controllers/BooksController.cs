using BookDemo.Data;
using BookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookDemo.Controllers
{
	[Route("api/books")]
	[ApiController]
	public class BooksController : ControllerBase
	{


		[HttpGet]
		public IActionResult GetAllBooks()
		{
			var books = ApplicationContext.Books;
			return Ok(books);
		}



		[HttpGet("{id:int}")]
		public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
		{
			var book = ApplicationContext.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
			if (book is null)
			{
				return NotFound(); //404
			}
			return Ok(book);
		}


		[HttpPost]
		public IActionResult CreateOneBook([FromBody] Book book)
		{
			try
			{
				if (book is null)
				{
					return BadRequest(); //400
				}

				ApplicationContext.Books.Add(book);
				return StatusCode(201, book);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}



		[HttpPut("{id:int}")]
		public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
		{
			var hasone = ApplicationContext.Books.Find(x => x.Id.Equals(id));
			if (hasone is null)
			{
				return NotFound(); //404
			}
			if (id != book.Id)
			{
				return BadRequest(); //400
			}
			ApplicationContext.Books.Remove(hasone);
			book.Id = hasone.Id;
			ApplicationContext.Books.Add(book);
			return Ok(book);
		}




		[HttpDelete]
		public IActionResult DeleteAllBook()
		{
			ApplicationContext.Books.Clear();
			return NoContent(); //204
		}




		[HttpDelete("{id:int}")]
		public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
		{
			var hasone = ApplicationContext.Books.Find(x => x.Id.Equals(id));
			if (hasone is null)
			{
				return NotFound(new { StatusCode = 404, message = $"Book with id:{id} could not found" });//404
			}
			ApplicationContext.Books.Remove(hasone);
			return NoContent();
		}



		[HttpPatch("{id:int}")]
		public IActionResult PartialUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> jsonPatch)
		{
			var hasone = ApplicationContext.Books.Find(x => x.Id.Equals(id));
			if (hasone is null)
			{
				return NotFound();//404
			}
			jsonPatch.ApplyTo(hasone);
			return NoContent(); //204
		}




	}
}
