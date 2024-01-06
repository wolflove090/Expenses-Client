namespace ExpenseDomain
{
    public class ExpenseRecordDataModelBuilder : IExpenseRecordNotification
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

        public ExpenseRecordDataModel Build()
        {
            return new ExpenseRecordDataModel()
            {
                categoryName = this.categoryName,
                consumptionAmount = this.consumptionAmount,
                border = this.border,
            };
        }
    }
}

