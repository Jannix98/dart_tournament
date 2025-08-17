using DartTournament.Application.DTO.Match;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Base.Services
{
    public interface IMatchPresentationService
    {
        Task UpdateMatchAsync(GameMatchUpdateDto updateDto);
    }
}