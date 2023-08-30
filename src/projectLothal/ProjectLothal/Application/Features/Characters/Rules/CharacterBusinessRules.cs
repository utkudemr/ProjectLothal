using Application.Features.Characters.Constants;
using Application.Services;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Characters.Rules
{
    public  class CharacterBusinessRules
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterBusinessRules(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task CharacterNameCanNotBeDuplicatedWhenInsert(string name)
        {
            var character=await _characterRepository.GetAsync(predicate:a=>a.Name == name.ToLower());
            if(character != null) {
                throw new BusinessException(CharacterMessages.CharacterNameExist);
            }
        }
    }
}
