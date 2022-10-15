using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpensesToggle : ToggleBase
{
    TextMeshProUGUI _OnLabel;
    TextMeshProUGUI _OffLabel;
    Image _OnIcon;
    Image _OffIcon;
    
    public string Label
    {
        set
        {
            if(this._OnLabel == null)
                return;

            this._OnLabel.text = value;
            this._OffLabel.text = value;
        }
        get
        {
            return this._OnLabel.text;
        }
    }

    public Sprite Icon
    {
        set
        {
            this._OnIcon.sprite = value;
            this._OffIcon.sprite = value;
        }
    }

    public void Init()
    {
        this.Start();
    }

    void Start()
    {
        Sprite hoge = Resources.Load<Sprite>("btn_line_black_bag");
        Debug.Log(hoge);

        this._OnLabel = this.transform.Find("On/Label").GetComponent<TextMeshProUGUI>();
        this._OffLabel = this.transform.Find("Off/Label").GetComponent<TextMeshProUGUI>();

        {
            Transform icon = this.transform.Find("On/IconGroup/Icon");
            if(icon != null)
                this._OnIcon = icon.GetComponent<Image>();
        }


        {
            Transform icon = this.transform.Find("Off/IconGroup/Icon");
            if(icon != null)
                this._OffIcon = icon.GetComponent<Image>();
        }

        //this._OnIcon = this.transform.Find("On/IconGroup/Icon").GetComponent<Image>();
        //this._OffIcon = this.transform.Find("Off/IconGroup/Icon").GetComponent<Image>();

        base.Start();
    }

    void Update()
    {
        
    }
}
