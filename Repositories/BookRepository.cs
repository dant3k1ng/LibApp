using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public void DeleteBookById(int bookId)
        {
            var book = GetBookById(bookId);

            if (book != null)
                _context.Books.Remove(book);
        }

        public Book GetBookById(int bookId)
        {
            return _context.Books.Find(bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.Include(b => b.Genre);
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Book SingleOrDefault(int bookId)
        {
            return _context.Books
                .Include(b => b.Genre)
                .SingleOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetAvailableBooksBy(string query)
        {
            var booksQuery = _context.Books
                .Include(b => b.Genre)
                .Where(b => b.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                booksQuery = booksQuery.Where(b => b.Name.Contains(query));
            }

            return booksQuery;
        }
    }
}
