namespace ComplexCalculator.Application.Models.Admin
{
    public class DataPerRoundResponse
    {
        public DataPerRoundSum dataPerRoundSum { get; set; } = new DataPerRoundSum();
        public List<DataPerRoundSum> lstDataPerRounSum { get; set; } = new List<DataPerRoundSum>();
    }

}
