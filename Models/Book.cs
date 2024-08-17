using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace books_management.Models;

[Index("Isbn", IsUnique = true)]
public partial class Book
{
    [Key]
    public int BookId { get; set; }

    [Required]
    public string Title { get; set; }

    public int AuthorId { get; set; }

    [Required]
    [Column("ISBN")]
    public string Isbn { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? PublishedDate { get; set; }

    public int? CopiesAvailable { get; set; }

    public string Language { get; set; }

    public int? NumberOfPages { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    public virtual Author Author { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}
