using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Zestaw3.Models.Games;

public partial class Genre
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required]
    public string? GenreName { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
