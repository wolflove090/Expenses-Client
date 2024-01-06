using ExpenseDomain;

namespace ExpenseInfrastructure
{
    public class InMemoryExpenseSummaryRepository : IExpenseSummaryRepository
    {
        IExpenseRecordRepository recordRepository;

        public InMemoryExpenseSummaryRepository(IExpenseRecordRepository recordRepository)
        {
            this.recordRepository = recordRepository;
        }

        public ExpenseSummary Get()
        {
            var records = this.recordRepository.FindAll();
            string[] recordIds = new string[records.Length];
            for(int i = 0; i < records.Length; i++)
            {
                recordIds[i] = records[i].id;
            }

            var total = this.recordRepository.GetTotalRecord();

            return new ExpenseSummary(recordIds, total.id);
        }
    }
}
