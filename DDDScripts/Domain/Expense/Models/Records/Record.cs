using System;

namespace ExpenseDomain
{
    public class Record : IEquatable<Record>
    {
        public readonly string id;

        readonly Category category;
        readonly int consumptionAmount;
        readonly int border;

        public int budgetRemaining => this.border - this.consumptionAmount;

        public Record(string categoryName, int amount, int border)
        {
            this.id = Guid.NewGuid().ToString();

            this.category = CategoryFactory.CreateCategory(categoryName);
            this.consumptionAmount = amount;
            this.border = border;
        }

        public void Notify(IRecordNotification note)
        {
            note.Category(this.category);
            note.ConsumptionAmount(this.consumptionAmount);
            note.Border(this.border);
            note.BudgetRemaining(this.budgetRemaining);
        }

        public bool Equals(Record other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }

        public static Record Sum(Record[] targets, string categoryName)
        {
            int consumptionAmount = 0;
            int border = 0;
            foreach(Record record in targets)
            {
                consumptionAmount += record.consumptionAmount;
                border += record.border;
            }

            return new Record(categoryName, consumptionAmount, border);
        }
    }
}
