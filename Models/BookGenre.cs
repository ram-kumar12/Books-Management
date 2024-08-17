using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace books_management.Models;

[Index("BookId", "GenreId", IsUnique = true)]
public partial class BookGenre
{
    [Key]
    public int BookGenreId { get; set; }

    public int BookId { get; set; }

    public int GenreId { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime AddedDate { get; set; }

    public string AssignedBy { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BookGenres")]
    public virtual Book Book { get; set; }

    [ForeignKey("GenreId")]
    [InverseProperty("BookGenres")]
    public virtual Genre Genre { get; set; }
}
