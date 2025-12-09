namespace Zestaw2.Models.Heroes;

public partial class Race
{
    public int Id { get; set; }

    public string? Race1 { get; set; }

    public virtual ICollection<Superhero> Superheroes { get; set; } = new List<Superhero>();
}
