using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Characters.Queries.GetList;

public class GetListCharacterListItemIDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PlanetName { get; set; }
    public List<GetListCharactersStarshipListItemDto> Starships { get; set; }
    public string Height { get; set; }
    public string HairColor { get; set; }
    public string ImageUrl { get; set; }
    public string PlanetImageUrl { get; set; }
}

