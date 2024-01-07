using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

using ExpenseDomain;

namespace ExpenseInfrastructure
{
    public class SpreadsheetRecordRepository : IRecordRepository
    {
        // ==================== public ==================== //

        public void SetUp(System.Action onComplete)
        {
            CoroutineManager.Run(this.GetRequest(), onComplete);
        }

        public Record Find(string id)
        {
            throw new System.NotImplementedException();
        }

        public Record[] FindAll()
        {
            return this.records.ToArray();
        }

        public ExpensesData GetBasicData()
        {
            return this.basicData;
        }

        public void Regist(string categoryName, int amount)
        {
            CoroutineManager.Run(this.PostRequest(categoryName, amount));
        }

        // ==================== private ==================== //

        ExpensesData basicData;
        List<Record> records = new List<Record>();

        // APIパス
        // 非公開フォルダにパスを記述
        string _ApiUrlValue;
        string _ApiUrl
        {
            get
            {
                if(string.IsNullOrEmpty(this._ApiUrlValue))
                {
                    var pathAsset = Resources.Load<TextAsset>("PrivateFiles/ApiPath");     
                    this._ApiUrlValue = pathAsset.text;
                }

                return this._ApiUrlValue;
            }
        }

        IEnumerator GetRequest()
        {
            string url = this._ApiUrl;

            using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                Debug.Log("通信開始");
                // TODO ダイアログ等表示物の対応が必要
                // this._ViewModel.SimpleDialogue.OnShow("通信中です...");
                yield return webRequest.SendWebRequest();
                // this._ViewModel.SimpleDialogue.OnHide();

                if(webRequest.isNetworkError)
                {
                    Debug.Log("error = " + webRequest.error);
                }
                else
                {
                    Debug.Log("sucess = " + webRequest.downloadHandler.text);
                    this.basicData = JsonUtility.FromJson<ExpensesData>(webRequest.downloadHandler.text);
                    this.records = this.ConvertFrom(this.basicData);
                    // this._UpdateExpensesLabel(webRequest.downloadHandler.text);
                }
            }
        }


        IEnumerator PostRequest(string categoryName, int amount)
        {
            if(string.IsNullOrEmpty(categoryName))
            {
                throw new System.Exception("カテゴリが指定されていません");
            }

            string url = this._ApiUrl;
            string labelParam = "label=" + categoryName;
            string expensesParam = "&expenses=" + amount.ToString();
            string debugParam = "&isDebug=";

    // エディタのみデバック機能を有効化
    #if UNITY_EDITOR
            debugParam += "FALSE";
    #else
            debugParam += "FALSE";
    #endif

            url += labelParam + expensesParam + debugParam;

            WWWForm form = new WWWForm();

            using(UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
            {
                Debug.Log("登録通信開始");
                yield return webRequest.SendWebRequest();
                
                Debug.Log("登録通信完了");

                if(webRequest.isNetworkError)
                {
                    Debug.Log("error = " + webRequest.error);
                }
                else
                {
                    Debug.Log("sucess = " + webRequest.downloadHandler.text);
                    this.basicData = JsonUtility.FromJson<ExpensesData>(webRequest.downloadHandler.text);
                    this.records = this.ConvertFrom(this.basicData);
                }
            }
        }

        List<Record> ConvertFrom(ExpensesData data)
        {
            List<Record> records = new List<Record>();
            records.Add(new Record("食費", data.foodAmount, data.foodBorder));
            records.Add(new Record("コストコ", data.costcoAmount, data.costcoBorder));
            records.Add(new Record("ガソリン", data.gasolineAmount, data.gasolineBorder));
            records.Add(new Record("日用品", data.itemAmount, data.itemBorder));
            records.Add(new Record("外食", data.restaurantAmount, data.restaurantBorder));
            records.Add(new Record("コンビニ", data.convenienceAmount, data.convenienceBorder));
            records.Add(new Record("お昼", data.lanchAmount, data.lanchBorder));
            records.Add(new Record("美容・服飾", data.beautyAmount, data.beautyBorder));
            records.Add(new Record("医療・健康", data.healthAmount, data.healthBorder));
            records.Add(new Record("ゲーム", data.gameAmount, data.gameBorder));
            records.Add(new Record("娯楽・レジャー", data.entertainmentAmount, data.entertainmentBorder));
            records.Add(new Record("教養", data.studyAmount, data.studyBorder));
            records.Add(new Record("交際費", data.presentAmount, data.presentBorder));

            // var husband = new Record("夫お小遣い", data.tatsukiAmount, data.tatsukiBorder);
            // var wife = new Record("妻お小遣い", data.akiAmount, data.akiBorder);

            // var total = new Record("合計", data.totalAmount, data.totalBorder);

            return records;
        }
    }
}
