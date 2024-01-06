using System;

namespace ExpenseDomain
{
    public class ExpenseRecord : IEquatable<ExpenseRecord>
    {
        public readonly string id;

        readonly string categoryName;
        readonly int consumptionAmount;
        readonly int border;

        public ExpenseRecord(string categoryName, int amount, int border)
        {
            this.id = Guid.NewGuid().ToString();

            this.categoryName = categoryName;
            this.consumptionAmount = amount;
            this.border = border;
        }

        public void Notify(IExpenseRecordNotification note)
        {
            note.CategoryName(this.categoryName);
            note.ConsumptionAmount(this.consumptionAmount);
            note.Border(this.border);
        }

        public bool Equals(ExpenseRecord other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }
    }
}
