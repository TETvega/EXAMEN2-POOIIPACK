namespace Examen2Poo.Dto.Amortitation
{
    public class dtoprueba
    {
            public string Name { get; set; }
            public string IdentytyNumber { get; set; }
            public double LoadAmount { get; set; }
            public double ComisionRate { get; set; }
            public double InteresRest { get; set; }
            public int Ter { get; set; }
            public DateTime DateTimeDesembolso { get; set; }
            public DateTime DeteTimePrimerPago { get; set; }
            public List<string> AmortizationIds { get; set; }
    }
}
