namespace OutingRepository.Repository;

public class Outing
{
    public enum EventType { Golf, Bowling, AmusementPark, Concert }

    public EventType Type { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime Date { get; set; }
    public decimal CostPerPerson { get; set; }
    public decimal TotalCost { get; set; }

    public Outing(EventType type, int numberOfPeople, DateTime date, decimal costPerPerson)
    {
        Type = type;
        NumberOfPeople = numberOfPeople;
        Date = date;
        CostPerPerson = costPerPerson;
        TotalCost = costPerPerson * numberOfPeople;
    }
}