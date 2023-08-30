
using Application.Features.Starships.Constants;
using Application.Services;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Starships.Rules;

public class StarshipBusinessRules: BaseBusinessRules
{
    private readonly IStarshipRepository _startshipRepository;

    public StarshipBusinessRules(IStarshipRepository startshipRepository)
    {
        _startshipRepository = startshipRepository;
    }

    public async Task StarshipNameCanNotBeDuplicatedWhenInsert(string name)
    {
        var character = await _startshipRepository.GetAsync(predicate: a => a.Name == name.ToLower());
        if (character != null)
        {
            throw new BusinessException(StarshipMessages.StarshipNameExist);
        }
    }
}
