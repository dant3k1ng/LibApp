using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibApp.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks(string query = null)
        {
            var bookDtos = _bookRepository.GetAvailableBooksBy(query)
                .ToList()
                .Select(_mapper.Map<Book, BookDto>);

            return Ok(bookDtos);
        }

        [HttpPost]
        [Authorize(Roles = "StorageManager, Owner")]
        public IActionResult CreateBook(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            _bookRepository.AddBook(book);
            _bookRepository.SaveChanges();

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.SingleOrDefault(id);

            if (book == null)
                return NotFound("Book not found");

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "StoreManager, Owner")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var bookInDb = _bookRepository.GetBookById(id);

            if (bookInDb == null)
                return NotFound("Book not found");

            bookInDb.Name = book.Name;
            bookInDb.AuthorName = book.AuthorName;
            bookInDb.GenreId = book.GenreId;
            bookInDb.ReleaseDate = book.ReleaseDate;
            bookInDb.NumberInStock = book.NumberInStock;
            bookInDb.NumberAvailable = book.NumberAvailable;

            _bookRepository.SaveChanges();

            return Ok(_mapper.Map<BookDto>(bookInDb));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "StoreManager, Owner")]
        public IActionResult DeleteBook(int id)
        {
            _bookRepository.DeleteBookById(id);
            _bookRepository.SaveChanges();

            return NoContent();
        }
    }
}
