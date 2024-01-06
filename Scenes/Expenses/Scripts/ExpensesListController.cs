using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using ExpenseDomain;

public class ExpensesListController : ControllerBase<ExpensesTabViewModel>
{
    int _StartPos = -1000;
    int _EndPos = -100;

    RectTransform _Canvas;

    override protected void _OnStart()
    {
        this._ViewModel.HideButton.OnClick = () => 
        {
            this._Hide();
        };
        this._ViewModel.Root.SetActive(false);
        this._Hide();

        this._Canvas = this.GetComponent<RectTransform>();
    }

    Vector3 _MousePos;
    override protected void _OnUpdate()
    {
        // //  タップ初期位置を取得
        // if(Input.GetMouseButtonDown(0))
        //     this._MousePos = Input.mousePosition;

        // if(Input.GetMouseButton(0))
        // {
        //     var pos = Vector2.zero;
        //     var test = RectTransformUtility.WorldToScreenPoint(Camera.main, Input.mousePosition);
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(this._Canvas, test, Camera.main, out pos);

        //     Debug.Log("座標");
        //     Debug.Log(pos);
        //     Debug.Log(pos / 2f);
        //     this._ViewModel.Hoge.localPosition = pos * 0.5f;

        //     var mouseDiff = Input.mousePosition - this._MousePos;
        //     var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, mouseDiff);
        // }
    }

    public void OnShow(ExpensesData inData)
    {
        if(inData == null)
            throw new System.Exception("ExpensesData が null");

        this._ViewModel.Food.Init(inData.foodAmount, inData.foodBorder);
        this._ViewModel.Costco.Init(inData.costcoAmount, inData.costcoBorder);
        this._ViewModel.Gasoline.Init(inData.gasolineAmount, inData.gasolineBorder);
        this._ViewModel.Item.Init(inData.itemAmount, inData.itemBorder);
        this._ViewModel.Restaurant.Init(inData.restaurantAmount, inData.restaurantBorder);
        this._ViewModel.Convenience.Init(inData.convenienceAmount, inData.convenienceBorder);
        this._ViewModel.Lanch.Init(inData.lanchAmount, inData.lanchBorder);
        this._ViewModel.Beauty.Init(inData.beautyAmount, inData.beautyBorder);
        this._ViewModel.Health.Init(inData.healthAmount, inData.healthBorder);
        this._ViewModel.Game.Init(inData.gameAmount, inData.gameBorder);
        this._ViewModel.Entertainment.Init(inData.entertainmentAmount, inData.entertainmentBorder);
        this._ViewModel.Study.Init(inData.studyAmount, inData.studyBorder);
        this._ViewModel.Present.Init(inData.presentAmount, inData.presentBorder);
        this._ViewModel.Tatsuki.Init(inData.tatsukiAmount, inData.tatsukiBorder);
        this._ViewModel.Aki.Init(inData.akiAmount, inData.akiBorder);
        this._ViewModel.Total.Init(inData.totalAmount, inData.totalBorder);

        this._Show();
    }

    // レコード一覧を受け取って表示
    public void OnShow(ExpenseRecordDataModel[] records)
    {
        List<ExpensesListLabelController> tabList = new List<ExpensesListLabelController>();
        tabList.Add(this._ViewModel.Food);
        tabList.Add(this._ViewModel.Costco);
        tabList.Add(this._ViewModel.Gasoline);
        tabList.Add(this._ViewModel.Item);
        tabList.Add(this._ViewModel.Restaurant);
        tabList.Add(this._ViewModel.Convenience);
        tabList.Add(this._ViewModel.Lanch);
        tabList.Add(this._ViewModel.Beauty);
        tabList.Add(this._ViewModel.Health);
        tabList.Add(this._ViewModel.Game);
        tabList.Add(this._ViewModel.Entertainment);
        tabList.Add(this._ViewModel.Study);
        tabList.Add(this._ViewModel.Present);
        // tabList.Add(this._ViewModel.Tatsuki);
        // tabList.Add(this._ViewModel.Aki);
        // tabList.Add(this._ViewModel.Total);
        
        for(int i = 0; i < records.Length; i++)
        {
            var record = records[i];
            var tab = tabList[i];
            tab.Init(record.consumptionAmount, record.border);
            
        }

        this._Show();
    }

    public void OnHide()
    {
        this._Hide();
    }

    private void _Show()
    {
        this._ViewModel.Root.SetActive(true);
        this.gameObject.transform.DOLocalPath(new Vector3[]{new Vector3(this._StartPos,0,0), new Vector3(this._EndPos,0,0)}, 0.5f);
    }

    private void _Hide()
    {
        this.gameObject.transform.DOLocalPath(new Vector3[]{new Vector3(this._EndPos,0,0), new Vector3(this._StartPos,0,0)}, 0.5f)
            .OnComplete(() => {
                this._ViewModel.Root.SetActive(false);

            });
    }



}
