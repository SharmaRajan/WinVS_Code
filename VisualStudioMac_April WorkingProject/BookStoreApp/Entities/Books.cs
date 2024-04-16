using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Entities
{
    public class Books
    {
        [Key] // Represents Primary Key
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}