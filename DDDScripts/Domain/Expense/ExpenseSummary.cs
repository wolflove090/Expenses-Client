using System;

namespace ExpenseDomain
{
    public class ExpenseSummary : IEquatable<ExpenseSummary>
    {
        readonly string id;

        readonly string[] recordIds;

        readonly string totalRecordId;

        // お小遣い

        public ExpenseSummary(string[] recordIds, string totalRecordId)
        {
            this.id = Guid.NewGuid().ToString();
            this.recordIds = recordIds;
            this.totalRecordId = totalRecordId;
        }

        public void Notify(IExpenseSummaryNotification note)
        {
            note.RecordIds(this.recordIds);
            note.TotalRecordId(this.totalRecordId);
        }


        public bool Equals(ExpenseSummary other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }
    }
}

