using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExpenseDomain;

public class ExpensesListLabelController : ControllerBase<ExpensesListLabelViewModel>
{
    override protected void _OnStart()
    {

    }

    public void Init(RecordDataModel record)
    {
        this._ViewModel.AmountNum.text = (record.border - record.consumptionAmount).ToString();
        this._ViewModel.AmountNum.color = record.GetLabelColor();
        this._ViewModel.BorderNum.text = record.border.ToString();
    }
}
