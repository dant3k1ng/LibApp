using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class CustomSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Genre.Any())
                {
                    context.Genre.AddRange(
                        new Genre
                        {
                            Name = "Novel"
                        },
                        new Genre
                        {
                            Name = "Horror"
                        },
                        new Genre
                        {
                            Name = "Belle Letters"
                        }
                        );
                }

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book
                        {
                            Name = "O miłości",
                            AuthorName = "Charles Bukowski",
                            GenreId = 3,
                            DateAdded = DateTime.Now,
                            ReleaseDate = new DateTime(2016, 02, 02),
                            NumberInStock = 100,
                            NumberAvailable = 150
                        },
                        new Book
                        {
                            Name = "Miasteczko Salem",
                            AuthorName = "Stephen King",
                            GenreId = 2,
                            DateAdded = DateTime.Now,
                            ReleaseDate = new DateTime(1984, 03, 01),
                            NumberInStock = 500,
                            NumberAvailable = 1200
                        },
                        new Book
                        {
                            Name = "Miasto ślepców",
                            AuthorName = "José Saramago",
                            GenreId = 1,
                            DateAdded = DateTime.Now,
                            ReleaseDate = new DateTime(1999, 01, 01),
                            NumberInStock = 600,
                            NumberAvailable = 1600
                        }
                    );
                }

                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(
                         new Customer
                         {
                             Name = "Jan Kowalski",
                             HasNewsletterSubscribed = true,
                             MembershipTypeId = 1,
                             Birthdate = new DateTime(1999, 3, 10)
                         },
                         new Customer
                         {
                             Name = "Marian Lichtman",
                             HasNewsletterSubscribed = false,
                             MembershipTypeId = 2,
                             Birthdate = new DateTime(1970, 9, 10)
                         },
                         new Customer
                         {
                             Name = "Luke Shaw",
                             HasNewsletterSubscribed = true,
                             MembershipTypeId = 3,
                             Birthdate = new DateTime(1985, 1, 1)
                         }
                     );
                }

                if (!context.Rentals.Any())
                {
                    context.Rentals.AddRange(
                        new Rental
                        {
                            CustomerId = 1,
                            BookId = 1,
                            DateRented = DateTime.Now
                        },
                        new Rental
                        {
                            CustomerId = 2,
                            BookId = 2,
                            DateRented = DateTime.Now
                        },
                        new Rental
                        {
                            CustomerId = 3,
                            BookId = 3,
                            DateRented = DateTime.Now
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
