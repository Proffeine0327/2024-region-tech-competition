using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateType { Coroutine, Update }

public class TweenInvoker : MonoBehaviour
{
    private static TweenInvoker instance;
    public static TweenInvoker Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("[Tween]");
                instance = obj.AddComponent<TweenInvoker>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public void DOFloat(float sValue, float eValue, float time, Action<float> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(FloatTween(sValue, eValue, time, x, ease, updateType));
    public void DOVector2(Vector2 sValue, Vector2 eValue, float time, Action<Vector2> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(Vector2Tween(sValue, eValue, time, x, ease, updateType));
    public void DOVector3(Vector3 sValue, Vector3 eValue, float time, Action<Vector3> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(Vector3Tween(sValue, eValue, time, x, ease, updateType));
    public void DOColor(Color sValue, Color eValue, float time, Action<Color> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(ColorTween(sValue, eValue, time, x, ease, updateType));

    private IEnumerator FloatTween(float sValue, float eValue, float time, Action<float> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Mathf.LerpUnclamped(sValue, eValue, TweenUtility.GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }
    private IEnumerator Vector2Tween(Vector2 sValue, Vector2 eValue, float time, Action<Vector2> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Vector2.LerpUnclamped(sValue, eValue, TweenUtility.GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }
    private IEnumerator Vector3Tween(Vector3 sValue, Vector3 eValue, float time, Action<Vector3> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Vector3.LerpUnclamped(sValue, eValue, TweenUtility.GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }
    private IEnumerator ColorTween(Color sValue, Color eValue, float time, Action<Color> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Color.LerpUnclamped(sValue, eValue, TweenUtility.GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }
}