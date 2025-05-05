using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Application.Models
{
    public class TempCalculatorResponseModel
    {
        public int Id { get; set; }
        public int? Changci { get; set; }
        public int? Tongshu { get; set; }

        [MaxLength(100, ErrorMessage = "Only allow max 100 characters")]
        public string? Name { get; set; } = null;
        public string InputByUser { get; set; } = null;
        public int? One { get; set; } = 0;
        public int? Two { get; set; } = 0;
        public int? Three { get; set; } = 0;
        public int? Four { get; set; } = 0;
        public int? Five { get; set; } = 0;
        public int? Six { get; set; } = 0;
        public int? Luozi { get; set; } = 0;
        public string? UserId { get; set; }
        public int? Version { get; set; } = 0;
        public int? BatchNo { get; set; }
        public int? GroupNo { get; set; }
        public int? Shutting { get; set; }
        public float? WinOrLose { get; set; } = 0;
        public float? MainTube { get; set; } = 0;
        public bool? EndThread { get; set; } = false;
    }
}
