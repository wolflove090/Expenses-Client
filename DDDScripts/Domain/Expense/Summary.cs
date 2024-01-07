using System;

namespace ExpenseDomain
{
    public class Summary : IEquatable<Summary>
    {
        readonly string id;

        readonly string[] recordIds;

        readonly string totalRecordId;
        readonly string husbandRecordId;
        readonly string wifeRecordId;

        readonly Record[] records;
        readonly Record totalRecord;
        readonly Record husbandRecord;
        readonly Record wifeRecord;

        // TODO 消す 
        public Summary(string[] recordIds, string totalRecordId, string husbandRecordId, string wifeRecordId)
        {
            this.id = Guid.NewGuid().ToString();
            this.recordIds = recordIds;
            this.totalRecordId = totalRecordId;
            this.husbandRecordId = husbandRecordId;
            this.wifeRecordId = wifeRecordId;
        }

        public Summary(Record[] records, Record total, Record husband, Record wife)
        {
            this.records = records;
            this.totalRecord = total;
            this.husbandRecord = husband;
            this.wifeRecord = wife;
        }

        public void Notify(ISummaryNotification note)
        {
            note.RecordIds(this.recordIds);
            note.TotalRecordId(this.totalRecordId);
            note.HusbandRecordId(this.husbandRecordId);
            note.WifeRecordId(this.wifeRecordId);
        }

        public bool Equals(Summary other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }
    }
}

