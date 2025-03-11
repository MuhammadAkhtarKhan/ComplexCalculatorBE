using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Application.Models.Admin
{
    public class SummaryAndAdminCalculationsResponse
    {
        public CalculatorSumModel? Summary { get; set; }
        public List<AdminCalculations>? Calculations { get; set; }
        public SummaryGridPannel? SummaryGrid { get; set; }
        public List<GroupDropDown>? Groups { get; set; }

    }

    public class GroupDropDown
    {
        public int GroupNo { get; set; }
    }
    public class SummaryGridPannel
    {
        public int ConnectedUsers { get; set; } = 0;
        public float WinOrLose { get; set; } = 0;
    }

    public class AdminCalculations
    {     
        public string? RawData { get; set; } = "";
        public string? IdentifiedData { get; set; } = "";
        public string? Name { get; set; } = "";
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }  
        public int Luozi { get; set; }
        public string? UserId { get; set; }    
        public int Version { get; set; }       
        public int? GroupNo { get; set; }
        public int? Shutting { get; set; }
        public string? InputByUser { get; set; }
        public float? WinOrLose { get; set; }
        public float? MainTube { get; set; }
        public float? SmallTable { get; set; }
    }

   
}
