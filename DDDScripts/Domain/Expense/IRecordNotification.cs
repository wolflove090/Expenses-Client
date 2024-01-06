
namespace ExpenseDomain
{
    public interface IRecordNotification
    {
        void CategoryName(string name);
        void ConsumptionAmount(int amount);
        void Border(int border);
    }
}
