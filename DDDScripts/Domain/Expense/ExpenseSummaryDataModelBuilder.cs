namespace ExpenseDomain
{
    public class ExpenseSummaryDataModelBuilder : IExpenseSummaryNotification
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

        public ExpenseSummaryDataModel Build()
        {
            return new ExpenseSummaryDataModel()
            {
                recordIds = recordIds,
                totalRecordId = this.totalRecordId,
            };
        }
    }
}
