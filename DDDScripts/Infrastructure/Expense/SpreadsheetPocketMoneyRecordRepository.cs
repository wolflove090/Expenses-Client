using ExpenseDomain;

namespace ExpenseInfrastructure
{
    public class SpreadsheetPocketMoneyRecordRepository : IPocketMoneyRecordRepository
    {
        SpreadsheetRecordRepository _recordRepository;

        public SpreadsheetPocketMoneyRecordRepository(SpreadsheetRecordRepository recordRepository)
        {
            this._recordRepository = recordRepository;
        }

        public Record GetHusbandPocketMoney()
        {
            var data = this._recordRepository.GetBasicData();
            return new Record("夫お小遣い", data.tatsukiAmount, data.tatsukiBorder);
        }

        public Record GetWifePocketMoney()
        {
            var data = this._recordRepository.GetBasicData();
            return new Record("妻お小遣い", data.akiAmount, data.akiBorder);
        }
    }
}
