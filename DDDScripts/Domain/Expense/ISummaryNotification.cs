namespace ExpenseDomain
{
    public interface ISummaryNotification
    {
        void RecordIds(string[] ids);
        void TotalRecordId(string id);
    }
}
