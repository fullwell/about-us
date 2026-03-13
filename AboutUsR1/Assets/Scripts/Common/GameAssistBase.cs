using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssistBase : MonoBehaviour
{
    /*
    #region 携程控制
    private Dictionary<CoroutineName, Coroutine> _Coroutines = new Dictionary<CoroutineName, Coroutine>();
    public void RunCoroutine(CoroutineName name, Coroutine coroutine)
    {
        KillCoroutine(name);
        _Coroutines.Add(name, coroutine);
        //Debug.LogWarning($"【{gameObject.name}】【Run  {name} 】");
    }
    public void KillCoroutine(CoroutineName name)
    {
        if (_Coroutines.ContainsKey(name))
        {
            if (null != _Coroutines[name])
            {
                StopCoroutine(_Coroutines[name]);
                //Debug.LogWarning($"Kill ( {name} )  : {gameObject.name}");
            }
            _Coroutines.Remove(name);
        }
    }
    public void KillAllCoroutine()
    {
        StopAllCoroutines();
        _Coroutines.Clear();
        //Debug.LogWarning($"Kill All : {gameObject.name}");
    }
    #endregion
    */
}
