using UnityEngine;
using Expense.Models.Categories;

namespace Expense.Models.Records
{
    public class RecordDataModel
    {
        public CategoryDataModel category;
        public int consumptionAmount;
        public int border;
        public int budgetRemaining;

        public Color GetLabelColor()
        {
            if(this.consumptionAmount >= this.border)
                return _OverColor;

            // ボーダーの75%を越えたら危険カラー
            if(this.consumptionAmount >= this.border * BORDER_LINE)
                return _DengerColor;

            // ボーダーの50%を越えたら警告カラー
            if(this.consumptionAmount >= this.border * HALF_LINE)
                return _HalfColor;

            return _DefaultColor;
        }

        // 金額のテキストカラー設定
        static Color _DefaultColor = new Color(0,0.7f,0.04f, 1);
        static Color _HalfColor = new Color(1f,0.8f,0,1f);
        static Color _DengerColor = new Color(1,0.5f,0,1);
        static Color _OverColor = new Color(1,0,0,1);

        // ボーダライン
        static float BORDER_LINE = 0.75f;
        static float HALF_LINE = 0.5f;
    }
}
