namespace ExpenseDomain
{
    public interface IPocketMoneyRecordRepository
    {
        Record GetHusbandPocketMoney();
        Record GetWifePocketMoney();
    }
}
