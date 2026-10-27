using System;
using UnityEngine;

// An event channel: an asset the subject and the observers share, without ever
// knowing each other. One asset per event, one type per shape of announcement.
public abstract class EventChannelSO<T> : ScriptableObject
{
    // "event": from the outside, only += and -= compile.
    public event Action<T> Raised;

    public void Raise(T value) => Raised?.Invoke(value);
}
