using AutoMapper;
using SqlServerDataAccess.EF;
// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ContactId));
            CreateMap<ContactModel, Contact>()
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.Id));
        }
    }
}