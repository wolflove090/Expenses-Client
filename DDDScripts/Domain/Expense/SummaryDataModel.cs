
namespace ExpenseDomain
{
    public class SummaryDataModel
    {
        public string[] recordIds;
        public string totalRecordId;
        public string husbandRecordId;
        public string wifeRecordId;

        public int recordIdLength => recordIds.Length;
    }
}
