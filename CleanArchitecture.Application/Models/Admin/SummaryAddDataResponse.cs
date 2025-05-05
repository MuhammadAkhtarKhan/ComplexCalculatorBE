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

    public class TotalScoreBoradModelResponse
    {
        public List<ScoreBoardGrid> LstScoreBoardGrid { get; set; } = new List<ScoreBoardGrid>();
        public ScoreBoardTotal ScoreBoardTotal { get; set; } = new ScoreBoardTotal();
    }
    public class ScoreBoardGrid
    {
        public float MainTube { get; set; } = 0;
        public int Luozi { get; set; } = 0;
        public float SmallTable { get; set; } = 0;
        public float Statistics { get; set; } = 0;
        public int InputByUser { get; set; }
        public int Shutting { get; set; }
    }

    public class ScoreBoardTotal
    {
        public int One { get; set; } = 0;
        public int Two { get; set; } = 0;
        public int Three { get; set; } = 0;
        public int Four { get; set; } = 0;
        public int Five { get; set; } = 0;
        public int Six { get; set; } = 0;
        public float TotalMainTube { get; set; } = 0;
        public int TotalLuozi { get; set; } = 0;
        public float TotalSmallTable { get; set; } = 0;      
        public float TotalWinOrLose { get; set; } = 0;      
        public int TotalCurrentlyOnlineUsers { get; set; } = 0;
        public int TotalSignedInUsers { get; set; } = 0;
    }

}
