using AutoMapper;
using ComplexCalculator.Application.Models;
using ComplexCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Application.MappingProfiles
{   
    public class CalculatorProfile : Profile
    {
        public CalculatorProfile()
        {
            CreateMap<CalculatorResponseModel, Calculator>().ReverseMap();
            CreateMap<Calculator, CalculatorResponseModel>();
            CreateMap<Calculator, CalculatorSumModel>();
            //CreateMap<CreateLeaveTypeCommand, LeaveType>();
            //CreateMap<UpdateLeaveTypeCommand, LeaveType>();
        }
    }
}
