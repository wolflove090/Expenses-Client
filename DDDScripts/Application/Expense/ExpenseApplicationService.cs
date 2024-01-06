using ExpenseDomain;

namespace ExpenseApplication
{
    public class ExpenseApplicationService
    {
        readonly IExpenseSummaryRepository summaryRepository;
        readonly IExpenseRecordRepository recordRepository;

        public ExpenseApplicationService(IExpenseSummaryRepository summaryRepository, IExpenseRecordRepository recordRepository)
        {
            this.summaryRepository = summaryRepository;
            this.recordRepository = recordRepository;
        }

        public ExpenseSummaryDataModel GetSummary()
        {
            var builder = new ExpenseSummaryDataModelBuilder();
            var summary =  this.summaryRepository.Get();
            summary.Notify(builder);

            return builder.Build();
        }

        public ExpenseRecordDataModel GetRecord(string id)
        {
            var builder = new ExpenseRecordDataModelBuilder();
            var record = this.recordRepository.Find(id);
            record.Notify(builder);

            return builder.Build();
        }

        public ExpenseRecordDataModel[] GetRecords()
        {
            var builder = new ExpenseRecordDataModelBuilder();
            var summary = this.GetSummary();
            var records = new ExpenseRecordDataModel[summary.recordIdLength];
            for(int i = 0; i < summary.recordIdLength; i++)
            {
                string id = summary.recordIds[i];
                var record = this.recordRepository.Find(id);
                record.Notify(builder);

                records[i] = builder.Build();
            }

            return records;
        }

        public ExpenseRecordDataModel GetTotalRecord()
        {
            var builder = new ExpenseRecordDataModelBuilder();
            var summary = this.GetSummary();
            var record = this.recordRepository.Find(summary.totalRecordId);
            record.Notify(builder);

            return builder.Build();
        }

        public void Record()
        {
            
        }
    }
}
