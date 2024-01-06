using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExpenseDomain;

namespace ExpenseInfrastructure
{
    public class InMemoryRecordRepository : IRecordRepository
    {
        Record[] records = null;

        Record total;

        public Record[] FindAll()
        {
            if(this.records != null) return this.records;

            Debug.Log("FindAll");

            int recordNum = 3;
            this.records = new Record[recordNum];
            for(int i = 0; i < recordNum; i++)
            {
                int num = i + 1;
                Record record = new Record($"カテゴリ{num}", 100 * num, 200 * num);
                this.records[i] = record;
            }

            return this.records;
        }

        public Record GetTotalRecord()
        {
            if(this.total != null) return total;

            this.total = new Record("合計", 1000, 2000);

            return this.total;
        } 

        public Record Find(string id)
        {
            if(this.records == null) return null;

            if(this.total.id == id) return this.total;

            foreach(var record in this.records)
            {
                if(record.id == id)
                {
                    return record;
                }
            }

            return null;
        }
    }
}

