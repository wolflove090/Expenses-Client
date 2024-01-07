namespace ExpenseDomain
{
    public interface IRecordRepository
    {
        Record[] FindAll();
        Record Find(string id);
    }
}
