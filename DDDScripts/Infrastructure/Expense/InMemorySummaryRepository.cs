using ExpenseDomain;

namespace ExpenseInfrastructure
{
    public class InMemorySummaryRepository : ISummaryRepository
    {
        IRecordRepository recordRepository;

        public InMemorySummaryRepository(IRecordRepository recordRepository)
        {
            this.recordRepository = recordRepository;
        }

        public Summary Get()
        {
            var records = this.recordRepository.FindAll();
            string[] recordIds = new string[records.Length];
            for(int i = 0; i < records.Length; i++)
            {
                recordIds[i] = records[i].id;
            }

            var total = this.recordRepository.GetTotalRecord();
            var husband = this.recordRepository.GetHusbandPocketMoney();
            var wife = this.recordRepository.GetWifePocketMoney();

            return new Summary(recordIds, total.id, husband.id, wife.id);
        }
    }
}
