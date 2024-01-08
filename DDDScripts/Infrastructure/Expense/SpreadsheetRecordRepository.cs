using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

using Expense.Models.Records;

namespace Expense.Infrastructure
{
    public class SpreadsheetRecordRepository : IRecordRepository
    {
        // ==================== public ==================== //

        public async UniTask SetUp(System.Action onComplete)
        {
            await this.GetRequest();
            onComplete?.Invoke();
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

        public async UniTask Regist(string categoryName, int amount)
        {
            await this.PostRequest(categoryName, amount);
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

        async UniTask GetRequest()
        {
            string url = this._ApiUrl;

            using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                Debug.Log("通信開始");
                // TODO ダイアログ等表示物の対応が必要
                // this._ViewModel.SimpleDialogue.OnShow("通信中です...");
                var operation =  webRequest.SendWebRequest();
                // this._ViewModel.SimpleDialogue.OnHide();

                await operation.ToUniTask();

                Debug.Log("通信完了");

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

        async UniTask PostRequest(string categoryName, int amount)
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
                var operation = webRequest.SendWebRequest();
                await operation.ToUniTask();
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
            
            return records;
        }
    }
}
