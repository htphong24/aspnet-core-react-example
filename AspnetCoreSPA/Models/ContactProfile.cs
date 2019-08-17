using AutoMapper;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Models
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
