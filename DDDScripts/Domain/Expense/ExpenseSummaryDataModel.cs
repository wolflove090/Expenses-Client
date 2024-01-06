
namespace ExpenseDomain
{
    public class ExpenseSummaryDataModel
    {
        public string[] recordIds;
        public string totalRecordId;

        public int recordIdLength => recordIds.Length;
    }
}
