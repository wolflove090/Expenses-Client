using ExpenseDomain;

namespace ExpenseApplication
{
    public class ExpenseApplicationService
    {
        // readonly ISummaryRepository summaryRepository;
        readonly IRecordRepository recordRepository;
        readonly IPocketMoneyRecordRepository pocketMoneyRepository;

        public ExpenseApplicationService(IRecordRepository recordRepository, IPocketMoneyRecordRepository pocketMoneyRepository)
        {
            // this.summaryRepository = summaryRepository;
            this.recordRepository = recordRepository;
            this.pocketMoneyRepository = pocketMoneyRepository;
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
            Record record = this.recordRepository.GetSumRecord();
            return this._BuildRecordData(record);
        }

        public RecordDataModel GetHusbandPocketMoney()
        {
            Record record = this.pocketMoneyRepository.GetHusbandPocketMoney();
            return this._BuildRecordData(record);
        }

        public RecordDataModel GetWifePocketMoney()
        {
            Record record = this.pocketMoneyRepository.GetWifePocketMoney();
            return this._BuildRecordData(record);
        }

        public void Recording()
        {
            
        }

        RecordDataModel _BuildRecordData(Record record)
        {
            var builder = new RecordDataModelBuilder();
            record.Notify(builder);

            return builder.Build();
        }
    }
}
