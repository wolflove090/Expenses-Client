using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpensesListLabelController : ControllerBase<ExpensesListLabelViewModel>
{
    override protected void _OnStart()
    {

    }

    public void Init(int inAmount, int inBorder)
    {
        this._ViewModel.AmountNum.text = (inBorder - inAmount).ToString();
        this._ViewModel.AmountNum.color = ExpensesUtil._GetLabelColor(inAmount, inBorder);
        this._ViewModel.BorderNum.text = inBorder.ToString();
    }
}
