﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Application.Models
{
    public class CalculatorResponseModel
    {
        public int? Id { get; set; }

        public int? Changci { get; set; }
        public int? Tongshu { get; set; }
        public string? RawData { get; set; } = "";
        public string? IdentifiedData { get; set; } = "";
        public string? Name { get; set; } = "";
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }
        public int? A { get; set; }
        public int? B { get; set; }
        public int? C { get; set; }
        public int? D { get; set; }
        public int? E { get; set; }
        public string? Format { get; set; }
        public int Luozi { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Version { get; set; }
        public int BatchNo { get; set; }
        public int? GroupNo { get; set; }
        public int? Shutting { get; set; }
        public string? InputByUser { get; set; }
        public string? AccumulatedRawData { get; set; }
        public float? WinOrLose { get; set; }
        public float? MainTube { get; set; }
        public bool IsPrevious { get; set; } = false;
        public bool? EndThread { get; set; } = false;
        public int EntryNo { get; set; }

    }

}
