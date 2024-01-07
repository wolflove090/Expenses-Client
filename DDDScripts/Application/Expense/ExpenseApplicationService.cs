using ExpenseDomain;

namespace ExpenseApplication
{
    public class ExpenseApplicationService
    {
        // readonly ISummaryRepository summaryRepository;
        readonly IRecordRepository recordRepository;

        public ExpenseApplicationService(IRecordRepository recordRepository)
        {
            // this.summaryRepository = summaryRepository;
            this.recordRepository = recordRepository;
        }

        // public SummaryDataModel GetSummary()
        // {
        //     var builder = new SummaryDataModelBuilder();
        //     var summary =  this.summaryRepository.Get();
        //     summary.Notify(builder);

        //     return builder.Build();
        // }

        // public RecordDataModel GetRecord(string id)
        // {
        //     var builder = new RecordDataModelBuilder();
        //     var record = this.recordRepository.Find(id);
        //     record.Notify(builder);

        //     return builder.Build();
        // }

        public RecordDataModel[] GetRecords()
        {
            var builder = new RecordDataModelBuilder();
            // var summary = this.GetSummary();
            var records = this.recordRepository.FindAll();
            var recordModels = new RecordDataModel[records.Length];
            for(int i = 0; i < records.Length; i++)
            {
                // string id = summary.recordIds[i];
                var record = records[i];
                record.Notify(builder);

                recordModels[i] = builder.Build();
            }

            return recordModels;
        }

        public RecordDataModel GetTotalRecord()
        {
            var builder = new RecordDataModelBuilder();
            // var summary = this.GetSummary();
            var record = this.recordRepository.GetSumRecord();
            record.Notify(builder);

            return builder.Build();
        }

        // public RecordDataModel GetHusbandPocketMoney()
        // {
        //     var summary = this.GetSummary();
        //     return this.GetRecord(summary.husbandRecordId);
        // }

        // public RecordDataModel GetWifePocketMoney()
        // {
        //     var summary = this.GetSummary();
        //     return this.GetRecord(summary.wifeRecordId);
        // }

        public void Record()
        {
            
        }
    }
}
