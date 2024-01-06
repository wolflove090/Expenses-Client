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

            return new Summary(recordIds, total.id);
        }
    }
}
