using AutoMapper;
using InfraConnect.Application.DTOs.Requests.Auth;
using InfraConnect.Application.DTOs.Responses;
using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Factories;

namespace InfraConnect.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserBase, UserBaseResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<ExternalAgent, ExternalAgentResponse>();

            CreateMap<AddressRequest, Address>()
                .ConstructUsing(src =>
                    AddressFactory.Create(
                        src.Street,
                        src.Number,
                        src.Neighborhood,
                        src.City,
                        src.State,
                        src.ZipCode,
                        src.Complement
                    ));

            CreateMap<RegisterUserRequest, UserProfile>()
                .ConstructUsing((src, ctx) =>
                    UserProfileFactory.Create(
                        src.FullName,
                        src.CPF,
                        src.Department,
                        src.JobTitle,
                        ctx.Mapper.Map<Address>(src.Address),
                        src.RG,
                        src.BirthDate,
                        src.Phone,
                        src.AdmissionDate,
                        src.ProfileImageUrl
                    ));
        }
    }
}