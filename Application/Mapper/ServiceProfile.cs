using AutoMapper;
using static Application.Services.Wish.Response;

namespace Application.Mapper
{
    public class ServiceProfile: Profile
    {
        public ServiceProfile() {
            CreateMap<Domain.Entities.Wish, WishResponse>().ReverseMap();
        }
    }
}
