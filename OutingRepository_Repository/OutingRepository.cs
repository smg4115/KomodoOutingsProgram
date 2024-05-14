namespace OutingRepository.Repository;

public class OutingRepo
{
    private List<Outing> _outings = new List<Outing>();

    public void AddOuting(Outing outing)
    {
        _outings.Add(outing);
    }

    public List<Outing> GetAllOutings()
    {
        return _outings;
    }

    public decimal GetTotalCostForAllOutings()
    {
        return _outings.Sum(o => o.TotalCost);
    }

    public decimal GetTotalCostByEventType(Outing.EventType type)
    {
        return _outings.Where(o => o.Type == type).Sum(o => o.TotalCost);
    }
}