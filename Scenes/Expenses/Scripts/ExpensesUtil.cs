using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpensesUtil
{
    // 金額のテキストカラー設定
    static Color _DefaultColor = new Color(0,0.7f,0.04f, 1);
    static Color _HalfColor = new Color(1f,0.8f,0,1f);
    static Color _DengerColor = new Color(1,0.5f,0,1);
    static Color _OverColor = new Color(1,0,0,1);

    // ボーダライン
    static float BORDER_LINE = 0.75f;
    static float HALF_LINE = 0.5f;

    public static Color _GetLabelColor(int inAmout, int inBorder)
    {
        if(inAmout >= inBorder)
            return _OverColor;

        // ボーダーの75%を越えたら危険カラー
        if(inAmout >= inBorder * BORDER_LINE)
            return _DengerColor;

        // ボーダーの50%を越えたら警告カラー
        if(inAmout >= inBorder * HALF_LINE)
            return _HalfColor;

        return _DefaultColor;
    }

}
