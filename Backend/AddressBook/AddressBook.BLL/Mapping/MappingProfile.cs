using AddressBook.BLL.DTOs;
using AddressBook.DAL.Entities;
using AutoMapper;

namespace AddressBook.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureContactMappings();

            ConfigureJobMappings();

            ConfigureDepartmentMappings();
        }

        private void ConfigureContactMappings()
        {
            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.JobName,
                    opt => opt.MapFrom(src => src.Job.Name))

                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CreateContactDto, Contact>()
                .ForMember(
                    dest => dest.Photo,
                    opt => opt.Ignore()
                );

            CreateMap<UpdateContactDto, Contact>()
                .ForMember(
                    dest => dest.Photo,
                    opt => opt.Ignore()
                );
        }

        private void ConfigureJobMappings()
        {
            CreateMap<Job, JobDto>()
                .ReverseMap();
        }

        private void ConfigureDepartmentMappings()
        {
            CreateMap<Department, DepartmentDto>()
                .ReverseMap();
        }
    }
}