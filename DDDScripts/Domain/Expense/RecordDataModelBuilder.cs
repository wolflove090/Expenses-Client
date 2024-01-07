namespace ExpenseDomain
{
    public class RecordDataModelBuilder : IRecordNotification
    {
        Category category;
        int consumptionAmount;
        int border;

        public void Border(int border)
        {
            this.border = border;
        }

        public void Category(Category category)
        {
            this.category = category;
        }

        public void ConsumptionAmount(int amount)
        {
            this.consumptionAmount = amount;
        }

        public RecordDataModel Build()
        {
            var builder = new CategoryDataModelBuilder();
            category.Notify(builder);

            return new RecordDataModel()
            {
                category = builder.Build(),
                consumptionAmount = this.consumptionAmount,
                border = this.border,
            };
        }
    }
}

