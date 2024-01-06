namespace ExpenseDomain
{
    public class SummaryDataModelBuilder : ISummaryNotification
    {
        string[] recordIds;
        string totalRecordId;

        public void TotalRecordId(string id)
        {
            this.totalRecordId = id;
        }

        public void RecordIds(string[] ids)
        {
            this.recordIds = ids;
        }

        public SummaryDataModel Build()
        {
            return new SummaryDataModel()
            {
                recordIds = recordIds,
                totalRecordId = this.totalRecordId,
            };
        }
    }
}
