namespace ExpenseDomain
{
    public interface IRecordRepository
    {
        Record[] FindAll();
        Record GetTotalRecord();
        Record Find(string id);
    }
}
