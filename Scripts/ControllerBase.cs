using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase<VIEWMODEL> : MonoBehaviour
    where VIEWMODEL : ViewModelBase
{
    VIEWMODEL _ActualViewModel;
    protected VIEWMODEL _ViewModel
    {
        get
        {
            if (this._ActualViewModel == null)
                this._ActualViewModel = this.gameObject.GetComponent<VIEWMODEL>();

            return this._ActualViewModel;
        }

    }

    public void Reset()
    {
        // this._ViewModel = this.gameObject.GetComponent<VIEWMODEL>();
        // if (this._ViewModel == null)
        //     this._ViewModel = this.gameObject.AddComponent<VIEWMODEL>();

        this.Bind();
    }

    // Start is called before the first frame update
    void Start()
    {
        // if (this._ViewModel == null)
            // this._ViewModel = this.gameObject.GetComponent<VIEWMODEL>();

        this._OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        this._OnUpdate();
    }

    // ViewModelのパラメータをBind
    void Bind()
    {
        var type = typeof(VIEWMODEL);
        foreach (var field in type.GetFields())
        {
            var name = field.Name;
            Debug.Log(name);
            var fieldType = field.FieldType;
            Debug.Log(fieldType);

            foreach (Transform child in this.transform)
            {
                var target = this._SerchChild(child, name);
                if (target != null)
                {
                    //var component = target.GetComponent(fieldType);
                    var component = this._GetComponent(target, fieldType);
                    if (component != null)
                    {
                        field.SetValue(this._ViewModel, component);
                    }
                    else
                        Debug.LogWarning("Not found component = " + target, target);

                    break;
                }
            }
        }
    }

    // 対象の子供を検索して
    // 対象がなければnullを返す
    Transform _SerchChild(Transform parent, string name)
    {
        if (parent.gameObject.name == name)
            return parent;

        foreach (Transform child in parent)
        {
            var result = this._SerchChild(child, name);
            if(result != null)
                return result;            
        }

        return null;
    }

    System.Object _GetComponent(Transform target, Type type)
    {
        if (type == typeof(GameObject))
            return target.gameObject;

        return target.GetComponent(type);
    }

    // -------------------- 継承先で実装 -------------------- //

    // Start
    protected virtual void _OnStart()
    {

    }

    // Update
    protected virtual void _OnUpdate()
    {

    }
}
