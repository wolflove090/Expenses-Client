using Cysharp.Threading.Tasks;
using UnityEngine;

using Expense.Models.Categories;
using Expense.Infrastructure;
using ExpenseApplication;

public class ExpensesController : ControllerBase<ExpensesViewModel>
{
    int[] _ValueArray = new int[5]{0,0,0,0,0};
    bool _IsMinus;

    // 家計簿サービス
    ExpenseApplicationService expenseService;

    CategoryDataModel selectCategory = null;

    async void Start()
    {
        await this._SetUp();

        // 基盤機能を上書きするためここで実行
        this._OnStart();
    }

    async UniTask _SetUp()
    {
        SpreadsheetRecordRepository recordRepository = new SpreadsheetRecordRepository();
        await recordRepository.SetUp(null);
        this.expenseService = new ExpenseApplicationService(recordRepository, new SpreadsheetPocketMoneyRecordRepository(recordRepository));
    }

    override protected void _OnStart()
    {
        // ---------- エラーキャッチ ---------- //
        Application.logMessageReceived += (logString, stackTrace, type) => 
        {
            if(type == LogType.Exception)
                this._ViewModel.ErrorDialogue.OnShow(logString);
        };

        // ---------- ヘッダー更新 ---------- //
        this._UpdateExpensesLabel();

        // ---------- ラベル設定 ---------- //

        this._ViewModel.ExpensesLabelGroup.OnClick = () => 
        {
            // ラベル表示をスイッチする
            if(this._ViewModel.TotalGroup.activeSelf)
            {
                this._ViewModel.TotalGroup.SetActive(false);
                this._ViewModel.AllowanceGroup.SetActive(true);
            }
            else
            {
                this._ViewModel.TotalGroup.SetActive(true);
                this._ViewModel.AllowanceGroup.SetActive(false);

                this._ViewModel.SimpleDialogue.OnHide();
            }
        };

        // ラベル生成
        var categories = this.expenseService.GetCategories();
        for(int i = 0; i < categories.Length; i++)
        {
            var category = categories[i];
            int index = i;
            var toggle = GameObject.Instantiate(this._ViewModel.LabelToggle, this._ViewModel.LabelGroup.gameObject.transform).GetComponent<ExpensesToggle>();
            toggle.InitAction();
            toggle.OnChange = (isOn) => 
            {
                if(isOn)
                    this.selectCategory = category;
                else
                    this.selectCategory = null;
            };
            var icon = Resources.Load<Sprite>(category.iconName);
            toggle.Label = category.name;
            toggle.Icon = icon;
        }

        this._ViewModel.LabelToggle.SetActive(false);

        this._ViewModel.LabelGroup.Init();

        // ---------- 数値表示 ---------- //

        this._ViewModel.ValueNum.text = this._GetValue().ToString("D5");

        // ---------- マイナストグル ---------- //

        this._ViewModel.SignToggle.OnChange = (isOn) => 
        {
            this._IsMinus = isOn;
        };

        // ---------- 上下ボタン設定 ---------- //

        // 上下ボタン生成
        for(int i = 0; i < 5; i++)
        {
            int index = i;

            // 上ボタン
            var upButton = GameObject.Instantiate(this._ViewModel.UpButton.gameObject, this._ViewModel.UpGroup.transform)
                            .GetComponent<ButtonBase>();

            upButton.transform.SetAsFirstSibling();
            upButton.OnClick = () => 
            {
                int value = this._ValueArray[index];
                if(value == 9)
                {
                    value = 0;
                }
                else
                {
                    value++;
                }
                this._SetValueFromIndex(index, value);
            };

            // 下ボタン
            var downButton = GameObject.Instantiate(this._ViewModel.DownButton.gameObject, this._ViewModel.DownGroup.transform)
                            .GetComponent<ButtonBase>();

            downButton.transform.SetAsFirstSibling();
            downButton.OnClick = () => 
            {
                int value = this._ValueArray[index];

                if(value == 0)
                {
                    value = 9;
                }
                else
                {
                    value--;
                }
                this._SetValueFromIndex(index, value);
            };
        }
        this._ViewModel.UpButton.gameObject.SetActive(false);
        this._ViewModel.DownButton.gameObject.SetActive(false);        

        // ---------- リスト ---------- //
        this._ViewModel.ExpensesTab.OnClick = () => 
        {
            var records = this.expenseService.GetRecords();
            
            var total = this.expenseService.GetTotalRecord();
            var husband = this.expenseService.GetHusbandPocketMoney();
            var wife = this.expenseService.GetWifePocketMoney();

            this._ViewModel.ExpensesList.OnShow(records, total, husband, wife);
        };

        // ---------- 送信ボタン設定 ---------- //

        this._ViewModel.UpdateButton.OnClick = () => 
        {
            this._RefreshExpense();
        };

        this._ViewModel.PostButton.OnClick = () => 
        {
            this._RegistExpense();
        };

        this._ViewModel.ResetButton.OnClick = () => 
        {
            this._ResetInput();
        };
    }

    void _SetValueFromIndex(int inIndex, int inValue)
    {
        this._ValueArray[inIndex] = inValue;
        // 描画更新
        this._ViewModel.ValueNum.text = this._GetValue(true).ToString("D5");
    }

    // 配列を数値にして返す
    // 引数で負の数を許容するか選択
    int _GetValue(bool isAbs = false)
    {
        int result = 0;

        int rate = 0;
        foreach(var value in this._ValueArray)
        {
            result += value * (int)Mathf.Pow(10, rate);
            rate++;
        }

        // マイナス値 
        if(!isAbs && this._IsMinus)
            result *= -1;

        return result;
    }

    // 入力項目のリセット
    void _ResetInput()
    {
        this.selectCategory = null;
        this._ValueArray = new int[5]{0,0,0,0,0};

        this._ViewModel.SignToggle.IsOn = false;
        this._ViewModel.LabelGroup.OffAllToggle();
        this._IsMinus = false;

        // 描画更新
        this._ViewModel.ValueNum.text = this._GetValue().ToString("D5");
    }

    void _UpdateExpensesLabel()
    {
        // ---------- 表示の更新 ---------- //

        var totalRecord = this.expenseService.GetTotalRecord();
        var husbandRecord = this.expenseService.GetHusbandPocketMoney();
        var wifeRecord = this.expenseService.GetWifePocketMoney();

        // 合計金額
        this._ViewModel.TotalAmountNum.text = totalRecord.budgetRemaining.ToString();
        this._ViewModel.TotalAmountNum.color = totalRecord.GetLabelColor();

        // お小遣い
        this._ViewModel.TatsukiAllowanceNum.text = husbandRecord.budgetRemaining.ToString();
        this._ViewModel.TatsukiAllowanceNum.color = husbandRecord.GetLabelColor();
        this._ViewModel.AkiAllowanceNum.text = wifeRecord.budgetRemaining.ToString();
        this._ViewModel.AkiAllowanceNum.color = wifeRecord.GetLabelColor();
    }

    // 情報の更新
    // サービスを再起動して情報の更新を行います
    async void _RefreshExpense()
    {
        await this._SetUp();
        this._UpdateExpensesLabel();
    }

    // 家計簿記録の登録
    async void _RegistExpense()
    {
        if(this.selectCategory == null)
            throw new System.Exception("ラベルが選択されていません");

        await this.expenseService.Regist(selectCategory.name, this._GetValue());
    }
}
