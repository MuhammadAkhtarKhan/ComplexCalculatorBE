namespace ComplexCalculator.Domain.Entities
{
    public class Calculator
    {
        public int Id { get; set; }
        public float? Name { get; set; } = 0.0F;
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }
        public int? A { get; set; }
        public int? B { get; set; }
        public int? C { get; set; }
        public string? Format { get; set; }
        public int Luozi { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
