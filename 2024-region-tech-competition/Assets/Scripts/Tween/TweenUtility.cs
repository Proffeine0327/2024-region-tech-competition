using System;
using UnityEngine;
using UnityEngine.UI;

public static class TweenUtility
{
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