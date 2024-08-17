using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace books_management.Models;

[Index("Name", IsUnique = true)]
public partial class Author
{
    [Key]
    public int AuthorId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Biography { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? DateOfBirth { get; set; }

    public string Nationality { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}

