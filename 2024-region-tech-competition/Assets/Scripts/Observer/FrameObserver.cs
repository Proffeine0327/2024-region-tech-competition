using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameObserver<TSource, TProperty> where TSource : MonoBehaviour
{
    private static readonly IEqualityComparer<TProperty> EqualityComparer = EqualityComparer<TProperty>.Default;

    private readonly TSource source;
    private readonly Func<TSource, TProperty> propertySelector;

    private TProperty prev;
    private Action<TProperty> action;

    public FrameObserver(TSource source, Func<TSource, TProperty> propertySelector)
    {
        this.source = source;
        this.propertySelector = propertySelector;

        prev = propertySelector(source);
        source.StartCoroutine(ObserveRoutine());
    }

    private IEnumerator ObserveRoutine()
    {
        while(source != null)
        {
            if(!EqualityComparer.Equals(prev, propertySelector(source)))
            {
                prev = propertySelector(source);
                action?.Invoke(propertySelector(source));
            }
            yield return null;
        }
    }

    public void Subscribe(Action<TProperty> action)
    {
        action?.Invoke(propertySelector(source));
        this.action += action;
    }
}