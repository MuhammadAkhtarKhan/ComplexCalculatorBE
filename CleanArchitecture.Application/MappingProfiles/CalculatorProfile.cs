using AutoMapper;
using ComplexCalculator.Application.Models;
using ComplexCalculator.Application.Models.Admin;
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
            CreateMap<Calculator, CalculatorSumModel>();
            CreateMap<CalculatorSumModel, Calculator>().ReverseMap();
            CreateMap<Calculator, GridCalculatorModel>();
            CreateMap<CalculatorSumModel, CalculatorResponse>();
            CreateMap<TempCalculatorResponseModel, TempCalculator>().ReverseMap();
            CreateMap<AdminCalculations, Calculator>().ReverseMap();
            //CreateMap<CreateLeaveTypeCommand, LeaveType>();
            //CreateMap<UpdateLeaveTypeCommand, LeaveType>();
        }
    }
}
