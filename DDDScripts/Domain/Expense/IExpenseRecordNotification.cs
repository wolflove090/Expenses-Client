
namespace ExpenseDomain
{
    public interface IExpenseRecordNotification
    {
        void CategoryName(string name);
        void ConsumptionAmount(int amount);
        void Border(int border);
    }
}
