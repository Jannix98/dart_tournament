using System;

namespace DartTournament.Application.DTO.Match
{
    public class GameMatchUpdateDto
    {
        public Guid Id { get; set; }
        public Guid IdGameEntityA { get; set; }
        public Guid IdGameEntityB { get; set; }
        public Guid WinnerId { get; set; }
    }
}