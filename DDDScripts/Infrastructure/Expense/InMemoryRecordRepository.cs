using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

using Expense.Models.Records;

namespace Expense.Infrastructure
{
    public class InMemoryRecordRepository : IRecordRepository
    {
        Record[] records = null;
        List<Record> recordList = new List<Record>();

        Record total;
        Record husbandPocketMoney;
        Record wifePocketMoney;

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

        public Record GetSumRecord()
        {
            if(this.total != null) return total;

            this.total = new Record("合計", 1000, 2000);

            return this.total;
        }

        public Record GetHusbandPocketMoney()
        {
            if(this.husbandPocketMoney != null) return husbandPocketMoney;

            this.husbandPocketMoney = new Record("夫お小遣い", 3000, 3000);

            return this.husbandPocketMoney;
        }

        public Record GetWifePocketMoney()
        {
            if(this.wifePocketMoney != null) return wifePocketMoney;

            this.wifePocketMoney = new Record("妻お小遣い", 2000, 4000);

            return this.wifePocketMoney;
        }

        public Record Find(string id)
        {
            if(this.records == null) return null;

            if(this.total.id == id) return this.total;

            if(this.husbandPocketMoney.id == id) return this.husbandPocketMoney;

            if(this.wifePocketMoney.id == id) return this.wifePocketMoney;

            foreach(var record in this.records)
            {
                if(record.id == id)
                {
                    return record;
                }
            }

            return null;
        }

        public UniTask Regist(string categoryName, int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}

