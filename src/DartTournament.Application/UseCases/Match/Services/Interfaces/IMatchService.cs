using DartTournament.Application.DTO.Match;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Match.Services.Interfaces
{
    public interface IMatchService
    {
        Task UpdateMatchAsync(GameMatchUpdateDto updateDto);
    }
}