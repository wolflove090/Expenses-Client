using System;

namespace ExpenseDomain
{
    public class Record : IEquatable<Record>
    {
        public readonly string id;

        readonly string categoryName;
        readonly int consumptionAmount;
        readonly int border;

        public Record(string categoryName, int amount, int border)
        {
            this.id = Guid.NewGuid().ToString();

            this.categoryName = categoryName;
            this.consumptionAmount = amount;
            this.border = border;
        }

        public void Notify(IRecordNotification note)
        {
            note.CategoryName(this.categoryName);
            note.ConsumptionAmount(this.consumptionAmount);
            note.Border(this.border);
        }

        public bool Equals(Record other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }

        public static Record Sum(Record[] targets)
        {
            int consumptionAmount = 0;
            int border = 0;
            foreach(Record record in targets)
            {
                consumptionAmount += record.consumptionAmount;
                border += record.border;
            }

            return new Record("合計", consumptionAmount, border);
        }
    }
}
