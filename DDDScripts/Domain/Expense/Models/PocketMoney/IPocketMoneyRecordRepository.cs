using Expense.Models.Records;

namespace Expense.Models.PocketMoney
{
    public interface IPocketMoneyRecordRepository
    {
        Record GetHusbandPocketMoney();
        Record GetWifePocketMoney();
    }
}
