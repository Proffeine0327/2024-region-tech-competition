using System;
using UnityEngine;
using UnityEngine.UI;

public enum Ease { None, InQuad, OutQuad, InQuart, OutQuart, InBack, OutBack }

public static class TweenUtility
{
    public static float GetEaseValue(Ease ease, float x)
    {
        switch (ease)
        {
            case Ease.None: return x;
            case Ease.InQuad: return x * x;
            case Ease.OutQuad: return 1 - Mathf.Pow(1 - x, 2);
            case Ease.InQuart: return Mathf.Pow(x, 4);
            case Ease.OutQuart: return 1 - Mathf.Pow(1 - x, 4);
            case Ease.InBack:
                {
                    var c1 = 1.70158f;
                    var c3 = c1 + 1;
                    return c3 * Mathf.Pow(x, 3) - c1 * Mathf.Pow(x, 2);
                }
            case Ease.OutBack:
                {
                    var c1 = 1.70158f;
                    var c3 = c1 + 1;
                    return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
                }
            default: return x;
        }
    }

    public static void DOFloat(float from, float to, float duration, Action<float> apply, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOFloat(from, to, duration, apply, ease, updateType);

    public static void DOMove(this Transform t, Vector3 vec, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOVector3(t.position, vec, duration, x => t.position = x, ease, updateType);

    public static void DOLocalMove(this Transform t, Vector3 vec, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
         => TweenInvoker.Instance.DOVector3(t.localPosition, vec, duration, x => t.localPosition = x, ease, updateType);

    public static void DORotate(this Transform t, Vector3 rot, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOVector3(t.eulerAngles, rot, duration, x => t.eulerAngles = x, ease, updateType);

    public static void DOLocalRotate(this Transform t, Vector3 rot, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOVector3(t.localEulerAngles, rot, duration, x => t.localEulerAngles = x, ease, updateType);

    public static void DOScale(this Transform t, Vector3 vec, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOVector3(t.localScale, vec, duration, x => t.localScale = x, ease, updateType);

    public static void DOAnchorMove(this RectTransform t, Vector2 vec, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOVector2(t.anchoredPosition, vec, duration, x => t.anchoredPosition = x, ease, updateType);

    public static void DOSizeDelta(this RectTransform t, Vector2 vec, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOVector2(t.sizeDelta, vec, duration, x => t.sizeDelta = x, ease, updateType);

    public static void DOColor(this Graphic graphic, Color color, float duration, Ease ease = Ease.None, UpdateType updateType = UpdateType.Coroutine)
        => TweenInvoker.Instance.DOColor(graphic.color, color, duration, x => graphic.color = x, ease, updateType);
}