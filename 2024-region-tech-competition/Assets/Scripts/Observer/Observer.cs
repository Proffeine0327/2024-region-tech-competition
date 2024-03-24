using System;
using System.Collections.Generic;

public class Observer<T>
{
    static readonly IEqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;

    private T value;
    private Action<T> action;
    
    public T Value
    {
        get => value;
        set
        {
            if(!EqualityComparer.Equals(value, this.value))
            {
                this.value = value;
                action?.Invoke(this.value);
            }
        }
    }

    public void SetValueAndNofity(T value)
    {
        this.value = value;
        action?.Invoke(this.value);
    }

    public void Subcribe(Action<T> action)
    {
        action?.Invoke(value);
        this.action += action;
    }
}