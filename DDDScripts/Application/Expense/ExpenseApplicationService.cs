using ExpenseDomain;

namespace ExpenseApplication
{
    public class ExpenseApplicationService
    {
        readonly ISummaryRepository summaryRepository;
        readonly IRecordRepository recordRepository;

        public ExpenseApplicationService(ISummaryRepository summaryRepository, IRecordRepository recordRepository)
        {
            this.summaryRepository = summaryRepository;
            this.recordRepository = recordRepository;
        }

        public SummaryDataModel GetSummary()
        {
            var builder = new SummaryDataModelBuilder();
            var summary =  this.summaryRepository.Get();
            summary.Notify(builder);

            return builder.Build();
        }

        public RecordDataModel GetRecord(string id)
        {
            var builder = new RecordDataModelBuilder();
            var record = this.recordRepository.Find(id);
            record.Notify(builder);

            return builder.Build();
        }

        public RecordDataModel[] GetRecords()
        {
            var builder = new RecordDataModelBuilder();
            var summary = this.GetSummary();
            var records = new RecordDataModel[summary.recordIdLength];
            for(int i = 0; i < summary.recordIdLength; i++)
            {
                string id = summary.recordIds[i];
                var record = this.recordRepository.Find(id);
                record.Notify(builder);

                records[i] = builder.Build();
            }

            return records;
        }

        public RecordDataModel GetTotalRecord()
        {
            var builder = new RecordDataModelBuilder();
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
