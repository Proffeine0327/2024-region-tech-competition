using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public static class ObserveUtility
{
    public static FrameObserver<TSource, TProperty> ObserveEveryValueChanged<TSource, TProperty>(this TSource source, Func<TSource, TProperty> propertySelector) where TSource : MonoBehaviour
    {
        return new FrameObserver<TSource, TProperty>(source, propertySelector);
    }
}