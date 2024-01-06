using System;

namespace ExpenseDomain
{
    public class Summary : IEquatable<Summary>
    {
        readonly string id;

        readonly string[] recordIds;

        readonly string totalRecordId;

        // お小遣い

        public Summary(string[] recordIds, string totalRecordId)
        {
            this.id = Guid.NewGuid().ToString();
            this.recordIds = recordIds;
            this.totalRecordId = totalRecordId;
        }

        public void Notify(ISummaryNotification note)
        {
            note.RecordIds(this.recordIds);
            note.TotalRecordId(this.totalRecordId);
        }


        public bool Equals(Summary other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }
    }
}

