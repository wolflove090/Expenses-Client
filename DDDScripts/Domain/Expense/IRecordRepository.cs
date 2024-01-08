using Cysharp.Threading.Tasks;

namespace ExpenseDomain
{
    public interface IRecordRepository
    {
        Record[] FindAll();
        Record Find(string id);
        UniTask Regist(string categoryName, int amount);
    }
}
