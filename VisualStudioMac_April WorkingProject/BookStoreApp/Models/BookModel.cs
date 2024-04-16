using System.ComponentModel.DataAnnotations;

namespace BookStoreApp;

public class BookModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Title is empty")]
    // [EmailAddress]
    [StringLength(100)]
    public string? Title { get; set; }

    public string? Description { get; set; }
}
