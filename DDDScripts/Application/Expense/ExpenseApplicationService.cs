using ExpenseDomain;
using ExpenseDomain.Service;

namespace ExpenseApplication
{
    public class ExpenseApplicationService
    {
        readonly IRecordRepository recordRepository;
        readonly IPocketMoneyRecordRepository pocketMoneyRepository;

        readonly ExpenseService expenseService;

        public ExpenseApplicationService(IRecordRepository recordRepository, IPocketMoneyRecordRepository pocketMoneyRepository)
        {
            this.recordRepository = recordRepository;
            this.pocketMoneyRepository = pocketMoneyRepository;
            this.expenseService = new ExpenseService(this.recordRepository);
        }

        public RecordDataModel[] GetRecords()
        {
            var builder = new RecordDataModelBuilder();
            var records = this.recordRepository.FindAll();
            var recordModels = new RecordDataModel[records.Length];
            for(int i = 0; i < records.Length; i++)
            {
                var record = records[i];
                record.Notify(builder);

                recordModels[i] = builder.Build();
            }

            return recordModels;
        }

        public CategoryDataModel[] GetCategories()
        {
            var builder = new CategoryDataModelBuilder();
            var categories = this.expenseService.GetCategories();
            var categoryModels = new CategoryDataModel[categories.Length];
            for(int i = 0; i < categories.Length; i++)
            {
                var category = categories[i];
                category.Notify(builder);

                categoryModels[i] = builder.Build();
            }

            return categoryModels;
        }

        public RecordDataModel GetTotalRecord()
        {
            Record record = this.expenseService.GetSumExpenseRecord();
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

        public void Regist(string categoryName, int amount)
        {
            this.recordRepository.Regist(categoryName, amount);
        }

        RecordDataModel _BuildRecordData(Record record)
        {
            var builder = new RecordDataModelBuilder();
            record.Notify(builder);

            return builder.Build();
        }
    }
}
