
namespace ExpenseDomain
{
    public class SummaryDataModel
    {
        public string[] recordIds;
        public string totalRecordId;

        public int recordIdLength => recordIds.Length;
    }
}
