using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    static CoroutineManager _singletonValue;
    static CoroutineManager _singleton
    {
        get
        {
            if(_singletonValue == null)
            {
                var obj = new GameObject(nameof(CoroutineManager));
                _singletonValue = obj.AddComponent<CoroutineManager>();
                GameObject.DontDestroyOnLoad(obj);
            }

            return _singletonValue;
        }
    }

    public static void Run(IEnumerator coroutine, System.Action onComplete = null)
    {
        _singleton._Run(coroutine, onComplete);
    }

    void _Run(IEnumerator coroutine, System.Action onComplete)
    {
        this.StartCoroutine(this._Coroutine(coroutine, onComplete));
    }

    IEnumerator _Coroutine(IEnumerator coroutine, System.Action onComplete)
    {
        yield return coroutine;
        onComplete?.Invoke();
    }
}
