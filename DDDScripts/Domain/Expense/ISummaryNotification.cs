namespace ExpenseDomain
{
    public interface ISummaryNotification
    {
        void RecordIds(string[] ids);
        void TotalRecordId(string id);
        void HusbandRecordId(string id);
        void WifeRecordId(string id);
    }
}
