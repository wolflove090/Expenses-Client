namespace ExpenseDomain.Service
{
    public class ExpenseService
    {
        IRecordRepository recordRepository;

        public ExpenseService(IRecordRepository recordRepository)
        {
            this.recordRepository = recordRepository;
        }

        public Record GetSumExpenseRecord()
        {
            Record[] records = this.recordRepository.FindAll();
            return Record.Sum(records);
        }
    }
}

