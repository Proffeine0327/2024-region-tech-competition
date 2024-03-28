using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateType { Coroutine, Update }
public enum Ease { None, InQuad, OutQuad, InBack, OutBack }

public class TweenInvoker : MonoBehaviour
{
    public static TweenInvoker Instance { get; private set; }

    public static float GetEaseValue(Ease ease, float value)
    {
        switch (ease)
        {
            case Ease.InQuad: return Instance.inQuad.Evaluate(value);
            case Ease.OutQuad: return Instance.outQuad.Evaluate(value);
            case Ease.InBack: return Instance.inBack.Evaluate(value);
            case Ease.OutBack: return Instance.outBack.Evaluate(value);

            case Ease.None:
            default: return value;
        }
    }

    [SerializeField] private AnimationCurve inQuad;
    [SerializeField] private AnimationCurve outQuad;
    [SerializeField] private AnimationCurve inBack;
    [SerializeField] private AnimationCurve outBack;

    public Coroutine DOFloat(float sValue, float eValue, float time, Action<float> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(FloatTween(sValue, eValue, time, x, ease, updateType));
    public Coroutine DOVector2(Vector2 sValue, Vector2 eValue, float time, Action<Vector2> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(Vector2Tween(sValue, eValue, time, x, ease, updateType));
    public Coroutine DOVector3(Vector3 sValue, Vector3 eValue, float time, Action<Vector3> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(Vector3Tween(sValue, eValue, time, x, ease, updateType));
    public Coroutine DOColor(Color sValue, Color eValue, float time, Action<Color> x, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => StartCoroutine(ColorTween(sValue, eValue, time, x, ease, updateType));

    private IEnumerator FloatTween(float sValue, float eValue, float time, Action<float> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Mathf.LerpUnclamped(sValue, eValue, GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }
    private IEnumerator Vector2Tween(Vector2 sValue, Vector2 eValue, float time, Action<Vector2> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Vector2.LerpUnclamped(sValue, eValue, GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }
    private IEnumerator Vector3Tween(Vector3 sValue, Vector3 eValue, float time, Action<Vector3> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Vector3.LerpUnclamped(sValue, eValue, GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }
    private IEnumerator ColorTween(Color sValue, Color eValue, float time, Action<Color> x, Ease ease, UpdateType updateType)
    {
        var wait = updateType == UpdateType.Coroutine ? null : new WaitForSecondsRealtime(0);
        for (float progress = 0; progress < time; progress += updateType == UpdateType.Coroutine ? Time.deltaTime : Time.unscaledDeltaTime)
        {
            x(Color.LerpUnclamped(sValue, eValue, GetEaseValue(ease, progress / time)));
            yield return wait;
        }
        x(eValue);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}