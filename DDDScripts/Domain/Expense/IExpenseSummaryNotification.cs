namespace ExpenseDomain
{
    public interface IExpenseSummaryNotification
    {
        void RecordIds(string[] ids);
        void TotalRecordId(string id);
    }
}
