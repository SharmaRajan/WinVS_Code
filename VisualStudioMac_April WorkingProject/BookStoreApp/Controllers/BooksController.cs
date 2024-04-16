using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo _bookRepo;

        public BooksController(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks(){
            var books = await _bookRepo.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute]int id){
            var book = await _bookRepo.GetBooksByIdAsync(id);
            if(book == null){
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody]BookModel bookModel){
            var id = await _bookRepo.AddBookAsync(bookModel);
            
            // return CreatedAtAction(nameof(GetBookById),new {id = id, Controller= "books"}, id);
            return Created("~/api/books/"+id,id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id,[FromBody]BookModel bookModel){
            await _bookRepo.UpdateBookByIdAsync(id,bookModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBook([FromRoute] int id,[FromBody]JsonPatchDocument bookModel){
            await _bookRepo.PatchBook(id,bookModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookById([FromRoute] int id){
            await _bookRepo.DeleteBookAsync(id);
            return Ok();
        }
    }
}