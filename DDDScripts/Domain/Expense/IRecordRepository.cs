namespace ExpenseDomain
{
    public interface IRecordRepository
    {
        Record[] FindAll();
        Record GetTotalRecord();
        Record GetWifePocketMoney();
        Record GetHusbandPocketMoney();
        Record Find(string id);
    }
}
