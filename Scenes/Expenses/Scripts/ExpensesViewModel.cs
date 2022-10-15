using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ExpensesViewModel : ViewModelBase
{
    // 複製用
    public GameObject LabelToggle;
    public ButtonBase UpButton;
    public ButtonBase DownButton;
   
    // 金額表示
    public ButtonBase ExpensesLabelGroup;

    // トータル
    public GameObject TotalGroup;
    public TextMeshProUGUI TotalAmountNum;

    // お小遣い
    public GameObject AllowanceGroup;
    public TextMeshProUGUI TatsukiAllowanceNum;
    public TextMeshProUGUI AkiAllowanceNum;

    public ToggleGroup LabelGroup;

    public TextMeshProUGUI ValueNum;

    public ExpensesToggle SignToggle;
    public GameObject UpGroup;
    public GameObject DownGroup;

    // 更新ボタン
    public ButtonBase UpdateButton;
    public ButtonBase PostButton;
    public ButtonBase ResetButton;

    // リスト
    public ButtonBase ExpensesTab;
    public ExpensesListController ExpensesList;

    // ---------- ダイアログ ---------- //

    public DialogueBase SimpleDialogue;
    public ErrorDialogue ErrorDialogue;
}
