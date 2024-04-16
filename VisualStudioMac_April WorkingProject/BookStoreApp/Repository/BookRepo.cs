using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.Data;
using BookStoreApp.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly BookStoreContext _context;

        // AutoMapper
        private readonly IMapper _mapper;

        public BookRepo(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            // var books = await _context.Books.Select(x => new BookModel()
            // {
            //     Id = x.Id,
            //     Title = x.Title,
            //     Description = x.Description
            // }).ToListAsync();

            // return books;

            // using AutoMapper
            var books = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> GetBooksByIdAsync(int bookId)
        {

            // if (bookId == 0)
            // {
            //     throw new ArgumentException("Invalid book ID");
            // }

            // var book = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            // {
            //     Id = x.Id,
            //     Title = x.Title,
            //     Description = x.Description
            // }).FirstOrDefaultAsync();

            // if (book == null)
            // {
            //     throw new KeyNotFoundException("Book not found");
            // }

            // return book;

            // using AutoMapper
            var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(book);
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books(){
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookByIdAsync(int bookId, BookModel bookModel)
        {

            // if (bookId == 0)
            // {
            //     throw new ArgumentException("Invalid book ID");
            // }

            // var book = await _context.Books.FindAsync(bookId);

            // if (book == null)
            // {
            //     throw new KeyNotFoundException("Book not found");
            // }

            // book.Title = bookModel.Title;
            // book.Description = bookModel.Description;
            // await _context.SaveChangesAsync();

            var book = new Books(){
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task PatchBook(int id, JsonPatchDocument json)
        {
            var book = await _context.Books.FindAsync(id);
            if(book != null){
                json.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            //var book = _context.Books.Find(id);
            //var book = _context.Books.Where(x => x.Title == "").FirstOrDefault();

            var book = new Books(){ Id = id};
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}