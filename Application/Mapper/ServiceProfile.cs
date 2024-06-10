using AutoMapper;

namespace Application.Mapper
{
    public class ServiceProfile: Profile

    {
        public ServiceProfile() { 

            CreateMap<Domain.Entities.Wish, Services.Wish.Response.WishResponse>().ReverseMap();
        }
    }
}
