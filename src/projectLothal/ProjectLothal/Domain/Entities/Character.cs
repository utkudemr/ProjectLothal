using Core.Persistance.Models;
namespace Domain.Entities;

public class Character : Entity<Guid>
{

    public string Name { get; set; }
    public string Height { get; set; }
    public string HairColor { get; set; }
    public string ImageUrl { get; set; }
    public Guid PlanetId { get; set; }
    public virtual Planet? Planet { get; set; }
    public virtual ICollection<Starship>? Starships { get; set; }
    public virtual ICollection<Film>? Films { get; set; }


    public Character()
    {
        Starships = new HashSet<Starship>();
        Films = new HashSet<Film>();
    }
    public Character(string name, string height,string haircolor,string imageUrl,Guid planetId) : this()
    {
        Name = name;
        Height = height;
        HairColor = haircolor;
        ImageUrl = imageUrl;
        PlanetId = planetId;
    }
}



