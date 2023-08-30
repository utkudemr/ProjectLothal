

namespace Application.Features.Characters.Queries.GetListByDynamic;

public class GetListByDynamicCharacterListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PlanetName { get; set; }
    public string Height { get; set; }
    public string HairColor { get; set; }
    public string ImageUrl { get; set; }
    public List<GetListCharactersStarshipDynamicListItemDto> Starships { get; set; }
    public string PlanetImageUrl { get; set; }
}
