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
            if(this.husbandPocketMoney == null)
            {
                var data = this._recordRepository.GetBasicData();
                this.husbandPocketMoney = new Record("夫お小遣い", data.tatsukiAmount, data.tatsukiBorder);
            } 

            return this.husbandPocketMoney;
        }

        public Record GetWifePocketMoney()
        {
            if(this.wifePocketMoney == null)
            {
                var data = this._recordRepository.GetBasicData();
                this.wifePocketMoney = new Record("妻お小遣い", data.akiAmount, data.akiBorder);
            }

            return this.wifePocketMoney;
        }

        Record husbandPocketMoney;
        Record wifePocketMoney;
    }
}
