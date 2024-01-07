
namespace ExpenseDomain
{
    public interface IRecordNotification
    {
        void Category(Category category);
        void ConsumptionAmount(int amount);
        void Border(int border);
        void BudgetRemaining(int remaining);
    }
}
