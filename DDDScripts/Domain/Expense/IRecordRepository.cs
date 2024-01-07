namespace ExpenseDomain
{
    public interface IRecordRepository
    {
        Record[] FindAll();
        Record GetSumRecord();
        // Record GetWifePocketMoney();
        // Record GetHusbandPocketMoney();
        Record Find(string id);
    }
}
