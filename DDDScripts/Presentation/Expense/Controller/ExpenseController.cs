using UnityEngine;

using ExpenseInfrastructure;
using ExpenseApplication;
using ExpensePresenter.View;

namespace ExpensePresenter
{
    public class ExpenseController : MonoBehaviour
    {
        [SerializeField] ExpenseHeaderView headerView;

        void Start()
        {
            // TODO プレゼンテーション層まで実装するとコストが高いので、このスクリプトはオミットして既存のコントローラ側を改修する

            // ExpenseApplicationService service = new ExpenseApplicationService(new InMemoryExpenseSummaryRepository());
            // var summary = service.GetSummary();
            // headerView.Show(summary.totalConsumptionAmount.ToString());
        }
    }

}
