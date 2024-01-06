namespace ExpenseDomain
{
    public class RecordDataModelBuilder : IRecordNotification
    {
        string categoryName;
        int consumptionAmount;
        int border;

        public void Border(int border)
        {
            this.border = border;
        }

        public void CategoryName(string name)
        {
            this.categoryName = name;
        }

        public void ConsumptionAmount(int amount)
        {
            this.consumptionAmount = amount;
        }

        public RecordDataModel Build()
        {
            return new RecordDataModel()
            {
                categoryName = this.categoryName,
                consumptionAmount = this.consumptionAmount,
                border = this.border,
            };
        }
    }
}

