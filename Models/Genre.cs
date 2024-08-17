using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace books_management.Models;

[Index("GenreName", IsUnique = true)]
public partial class Genre
{
    [Key]
    public int GenreId { get; set; }

    [Required]
    public string GenreName { get; set; }

    public string Description { get; set; }

    public int? PopularityRank { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? CreatedDate { get; set; }

    [InverseProperty("Genre")]
    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}
