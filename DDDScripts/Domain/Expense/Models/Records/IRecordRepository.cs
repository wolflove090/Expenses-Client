using Cysharp.Threading.Tasks;

namespace Expense.Models.Records
{
    public interface IRecordRepository
    {
        Record[] FindAll();
        Record Find(string id);
        UniTask Regist(string categoryName, int amount);
    }
}
