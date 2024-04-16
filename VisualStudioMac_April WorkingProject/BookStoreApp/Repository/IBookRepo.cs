using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApp.Repository
{
    public interface IBookRepo
    {
        Task<List<BookModel>> GetAllBooksAsync();

        Task<BookModel> GetBooksByIdAsync(int bookId);

        Task<int> AddBookAsync(BookModel bookModel);

        Task UpdateBookByIdAsync(int bookId, BookModel bookModel);

        Task PatchBook(int id, JsonPatchDocument json);

        Task DeleteBookAsync(int id);
    }
}