using System.ComponentModel.DataAnnotations;

namespace ComplexCalculator.Domain.Entities
{
    public class Calculator
    {
        public int Id { get; set; }
        public int? Changci { get; set; }
        public int? Tongshu { get; set; }

        [StringLength(100,ErrorMessage = "Only allow max 100 characters")]
        public string? RawData { get; set; } = "";

        [MaxLength(100, ErrorMessage = "Only allow max 100 characters")]
        public string? IdentifiedData { get; set; } = "";


        [MaxLength(100, ErrorMessage = "Only allow max 100 characters")]
        public string? Name { get; set; } = "";
        public string InputByUser { get; set; } = "";
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }

        [MaxLength(1, ErrorMessage = "Only allow number 1-6")]
        public int? A { get; set; }

        [MaxLength(1, ErrorMessage = "Only allow number 1-6")]
        public int? B { get; set; }

        [MaxLength(1, ErrorMessage = "Only allow number 1-6")]
        public int? C { get; set; }

        [MaxLength(1, ErrorMessage = "Only allow number 1-6")]
        public int? D { get; set; }

        [MaxLength(1,ErrorMessage ="Only allow number 1-6")]
        public int? E { get; set; }
        [MaxLength(50)]
        public string? Format { get; set; }

        [MaxLength(200)]
        public string? AccumulatedRawData { get; set; }
        public int Luozi { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Version { get; set; }
        public int BatchNo { get; set; }
        public int? GroupNo { get; set; }
        public int? Shutting { get; set; }
        public float? WinOrLose { get; set; }
        public float? MainTube { get; set; }
        public bool? IsError { get; set; } = false;
        public bool IsPrevious { get; set; } = false;
        public bool? EndThread { get; set; } = false;

    }
}
