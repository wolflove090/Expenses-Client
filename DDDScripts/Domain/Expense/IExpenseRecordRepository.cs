namespace ExpenseDomain
{
    public interface IExpenseRecordRepository
    {
        ExpenseRecord[] FindAll();
        ExpenseRecord GetTotalRecord();
        ExpenseRecord Find(string id);
    }
}
