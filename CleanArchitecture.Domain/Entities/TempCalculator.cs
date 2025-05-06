using System.ComponentModel.DataAnnotations;

namespace ComplexCalculator.Domain.Entities
{
    public class TempCalculator
    {
        public int Id { get; set; }
        public int? Changci { get; set; }
        public int? Tongshu { get; set; }

        [MaxLength(100, ErrorMessage = "Only allow max 100 characters")]
        public string? Name { get; set; } = "";
        public string? InputByUser { get; set; } = null;
        public int? One { get; set; }
        public int? Two { get; set; }
        public int? Three { get; set; }
        public int? Four { get; set; }
        public int? Five { get; set; }
        public int? Six { get; set; }     
        public int? Luozi { get; set; }
        public string? UserId { get; set; } = null;       
        public int? Version { get; set; }
        public int? BatchNo { get; set; }
        public int? GroupNo { get; set; }
        public int? Shutting { get; set; }
        public int? SmallTable { get; set; } = 0;
        public float? WinOrLose { get; set; }
        public float? MainTube { get; set; }
  
        public bool? EndThread { get; set; } = false;

    }
}
