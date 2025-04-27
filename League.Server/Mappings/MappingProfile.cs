using AutoMapper;
using League.Server.DTOs;
using League.Server.Models;

namespace League.Server.Mappings
{
    /// <summary>
    /// AutoMapper profile that defines the mapping configurations between domain models and DTOs
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the MappingProfile class and configures all the model to DTO mappings
        /// </summary>
        public MappingProfile()
        {
            // League mappings
            CreateMap<Models.League, LeagueDto>()
                .ReverseMap()
                .ForMember(dest => dest.Conferences, opt => opt.Ignore()); // Ignore Conferences property during mapping

            // Conference mappings
            CreateMap<Conference, ConferenceDto>()
                .ReverseMap()
                .ForMember(dest => dest.Divisions, opt => opt.Ignore()); // Ignore Divisions property during mapping

            // Division mappings
            CreateMap<Division, DivisionDto>()
                .ReverseMap()
                .ForMember(dest => dest.Teams, opt => opt.Ignore()); // Ignore Teams property during mapping

            // Team mappings
            CreateMap<Team, TeamDto>()
                .ReverseMap()
                .ForMember(dest => dest.Players, opt => opt.Ignore());

            // Player mappings
            CreateMap<Player, PlayerDto>()
                .ReverseMap()
                .ForMember(dest => dest.Team, opt => opt.Ignore()) // Ignore Team property during mapping
                .ForMember(dest => dest.TeamId, opt => opt.Ignore()); // Ignore TeamId property during mapping
        }
    }
}
