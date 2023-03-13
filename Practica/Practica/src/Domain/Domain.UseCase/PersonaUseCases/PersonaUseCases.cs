using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.PersonaUseCases
{
    public class PersonaUseCases : IPersonaUseCases
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaUseCases(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public List<PersonaEntity> FindAll()
        {
            return _personaRepository.GetAll();
        }
    }
}