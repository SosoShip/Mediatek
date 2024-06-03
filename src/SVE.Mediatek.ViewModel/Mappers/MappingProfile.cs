using AutoMapper;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.ViewModel.Mappers
{
    /// <summary>
    /// Enables mapping an ObjectEntity from the database to an ObjectModel usable by the application, and vice versa
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Configuration of objects that can be mapped
            CreateMap<StaffEntity, StaffModel>()
                .ReverseMap().ValidateMemberList(MemberList.Source);
            CreateMap<AbsenceEntity, AbsenceModel>()
                .ReverseMap().ValidateMemberList(MemberList.Source);
            CreateMap<ManagerEntity, ManagerModel>()
                .ReverseMap().ValidateMemberList(MemberList.Source);
            CreateMap<Department, DepartmentModel>()
                .ReverseMap().ValidateMemberList(MemberList.Source);
            CreateMap<Reason, ReasonModel>()
                .ReverseMap().ValidateMemberList(MemberList.Source);
        }
    }
}
