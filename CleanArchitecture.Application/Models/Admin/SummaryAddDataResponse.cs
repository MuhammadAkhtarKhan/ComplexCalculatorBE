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
    }

   
}
