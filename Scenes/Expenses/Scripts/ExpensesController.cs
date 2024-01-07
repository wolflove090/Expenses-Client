using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using ExpenseApplication;
using ExpenseInfrastructure;

// ボタン情報
struct ButtonInfo
{
    public string _label;
    public string _iconName;
}

public class ExpensesController : ControllerBase<ExpensesViewModel>
{
    int[] _ValueArray = new int[5]{0,0,0,0,0};
    bool _IsMinus;
    IButton[] _buttons = {new Food(), new Costco(), new Gasoline(), new Item(), new Restaurant(), new Convenience(), new Lunch(), new Beauty(),new Helth(),new Game(),new Entertainment(),new Study(),new Present(),new Tatsuki(),new Aki()};

    // APIパス
    // 非公開フォルダにパスを記述
    // string _ApiUrl
    // {
    //     get
    //     {
    //         if(string.IsNullOrEmpty(this._ApiUrlValue))
    //         {
    //             var pathAsset = Resources.Load<TextAsset>("PrivateFiles/ApiPath");     
    //             this._ApiUrlValue = pathAsset.text;
    //         }

    //         return this._ApiUrlValue;
    //     }
    // }
    // string _ApiUrlValue;

    // 金額のテキストカラー設定
    Color _DefaultColor = new Color(0,0.7f,0.04f, 1);
    Color _DengerColor = new Color(1,0.5f,0,1);
    Color _OverColor = new Color(1,0,0,1);

    // 金額データ
    ExpensesData _ExpensesData;

    // 家計簿サービス
    ExpenseApplicationService expenseService;

    int _LabelIndex = -1;

    void _SetUp()
    {
        SpreadsheetRecordRepository recordRepository = new SpreadsheetRecordRepository();
        recordRepository.SetUp(null);
        this.expenseService = new ExpenseApplicationService(recordRepository, new SpreadsheetPocketMoneyRecordRepository(recordRepository));
    }

    override protected void _OnStart()
    {
        this._SetUp();

        // ---------- エラーキャッチ ---------- //
        Application.logMessageReceived += (logString, stackTrace, type) => 
        {
            if(type == LogType.Exception)
                this._ViewModel.ErrorDialogue.OnShow(logString);
        };

        // ---------- ラベル設定 ---------- //

        this._ViewModel.ExpensesLabelGroup.OnClick = () => 
        {
            // ラベル表示をスイッチする
            if(this._ViewModel.TotalGroup.active)
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
        int count = 0;
        foreach(var button in this._buttons)
        {
            int index = count;
            var toggle = GameObject.Instantiate(this._ViewModel.LabelToggle, this._ViewModel.LabelGroup.gameObject.transform).GetComponent<ExpensesToggle>();
            toggle.Init();
            toggle.OnChange = (isOn) => 
            {
                if(isOn)
                    this._LabelIndex = index;
                else
                    this._LabelIndex = -1;
            };
            var icon = Resources.Load<Sprite>(button._iconName);
            toggle.Label = button._label;
            toggle.Icon = icon;
            
            count++;
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
            // this._ViewModel.ExpensesList.OnShow(this._ExpensesData);

            var records = this.expenseService.GetRecords();
            
            var total = this.expenseService.GetTotalRecord();
            var husband = this.expenseService.GetHusbandPocketMoney();
            var wife = this.expenseService.GetWifePocketMoney();

            this._ViewModel.ExpensesList.OnShow(records, total, husband, wife);
        };

        // ---------- 送信ボタン設定 ---------- //

        // this._ViewModel.UpdateButton.OnClick = () => 
        // {
        //     Debug.Log("get");
        //     StartCoroutine(this.GetRequest());
        // };

        // this._ViewModel.PostButton.OnClick = () => {
        //     Debug.Log("post");
        //     StartCoroutine(this.PostRequest());
        // };

        this._ViewModel.ResetButton.OnClick = () => 
        {
            Debug.Log("reset");
            this._ResetInput();
        };

        // ---------- 初期通信 ---------- //
        // StartCoroutine(this.GetRequest());
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
        this._LabelIndex = -1;
        this._ValueArray = new int[5]{0,0,0,0,0};

        this._ViewModel.SignToggle.IsOn = false;
        this._ViewModel.LabelGroup.OffAllToggle();
        this._IsMinus = false;

        // 描画更新
        this._ViewModel.ValueNum.text = this._GetValue().ToString("D5");
    }

//     IEnumerator PostRequest()
//     {
//         if(this._LabelIndex < 0)
//             throw new System.Exception("ラベルが選択されていません");

//         Debug.Log(this._LabelIndex);

//         string url = this._ApiUrl;
//         string labelParam = "label=" + this._buttons[this._LabelIndex]._label;
//         string expensesParam = "&expenses=" + this._GetValue().ToString();
//         string debugParam = "&isDebug=";

// // エディタのみデバック機能を有効化
// #if UNITY_EDITOR
//         debugParam += "FALSE";
// #else
//         debugParam += "FALSE";
// #endif

//         url += labelParam + expensesParam + debugParam;

//         WWWForm form = new WWWForm();

//         using(UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
//         {
//             this._ViewModel.SimpleDialogue.OnShow("通信中です...");
//             yield return webRequest.SendWebRequest();
//             this._ViewModel.SimpleDialogue.OnHide();

//             if(webRequest.isNetworkError)
//             {
//                 Debug.Log("error = " + webRequest.error);
//             }
//             else
//             {
//                 Debug.Log("sucess = " + webRequest.downloadHandler.text);
//                 this._UpdateExpensesLabel(webRequest.downloadHandler.text);
//                 this._ResetImput();

//             }
//         }
//     }

    void _UpdateExpensesLabel(string inJson)
    {
        this._ExpensesData = JsonUtility.FromJson<ExpensesData>(inJson);

        // ---------- 表示の更新 ---------- //

        // 合計金額
        this._ViewModel.TotalAmountNum.text = (this._ExpensesData.totalBorder - this._ExpensesData.totalAmount).ToString();
        this._ViewModel.TotalAmountNum.color = ExpensesUtil._GetLabelColor(this._ExpensesData.totalAmount, this._ExpensesData.totalBorder);

        // お小遣い
        this._ViewModel.TatsukiAllowanceNum.text = (this._ExpensesData.tatsukiBorder - this._ExpensesData.tatsukiAmount).ToString();
        this._ViewModel.TatsukiAllowanceNum.color = ExpensesUtil._GetLabelColor(this._ExpensesData.tatsukiAmount, this._ExpensesData.tatsukiBorder);
        this._ViewModel.AkiAllowanceNum.text = (this._ExpensesData.akiBorder - this._ExpensesData.akiAmount).ToString();
        this._ViewModel.AkiAllowanceNum.color = ExpensesUtil._GetLabelColor(this._ExpensesData.akiAmount, this._ExpensesData.akiBorder);

        // ---------- [テスト用]ドメイン情報で上書き ---------- //

        var totalRecord = this.expenseService.GetTotalRecord();
        var husbandRecord = this.expenseService.GetHusbandPocketMoney();
        var wifeRecord = this.expenseService.GetWifePocketMoney();

        // 合計金額
        this._ViewModel.TotalAmountNum.text = totalRecord.consumptionAmount.ToString();
        this._ViewModel.TotalAmountNum.color = ExpensesUtil._GetLabelColor(this._ExpensesData.totalAmount, this._ExpensesData.totalBorder);

        // お小遣い
        this._ViewModel.TatsukiAllowanceNum.text = husbandRecord.consumptionAmount.ToString();
        this._ViewModel.TatsukiAllowanceNum.color = ExpensesUtil._GetLabelColor(this._ExpensesData.tatsukiAmount, this._ExpensesData.tatsukiBorder);
        this._ViewModel.AkiAllowanceNum.text = wifeRecord.consumptionAmount.ToString();
        this._ViewModel.AkiAllowanceNum.color = ExpensesUtil._GetLabelColor(this._ExpensesData.akiAmount, this._ExpensesData.akiBorder);
    }
}
