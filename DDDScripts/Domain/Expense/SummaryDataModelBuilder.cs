namespace ExpenseDomain
{
    public class SummaryDataModelBuilder : ISummaryNotification
    {
        string[] recordIds;
        string totalRecordId;
        string husbandRecordId;
        string wifeRecordId;

        public void TotalRecordId(string id)
        {
            this.totalRecordId = id;
        }

        public void RecordIds(string[] ids)
        {
            this.recordIds = ids;
        }

        public void HusbandRecordId(string id)
        {
            this.husbandRecordId = id;
        }

        public void WifeRecordId(string id)
        {
            this.wifeRecordId = id;
        }

        public SummaryDataModel Build()
        {
            return new SummaryDataModel()
            {
                recordIds = this.recordIds,
                totalRecordId = this.totalRecordId,
                husbandRecordId = this.husbandRecordId,
                wifeRecordId = this.wifeRecordId,
            };
        }
    }
}
