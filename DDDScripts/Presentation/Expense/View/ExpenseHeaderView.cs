using UnityEngine;
using TMPro;

namespace ExpensePresenter.View
{
    public class ExpenseHeaderView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI amountLabel;

        public void Show(string amountText)
        {
            this.amountLabel.text = amountText;
        }
    }
}
